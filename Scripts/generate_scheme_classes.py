#!/usr/bin/env python3
"""
**
** New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
** Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
**

Generate `*Scheme.cs` classes that implement `AbstractBaseColorScheme` for
existing palette files that still declare a `_schemeBaseColors` array.

Key facts
---------
* Each generated property has the form
  `public override Color Xxx { get; set; } = <expr>;` – i.e. **writable** auto-properties

Usage (run from repo root)
--------------------------
python Scripts/generate_scheme_classes.py -f <palette.cs>

CLI options
-----------
--dry-run                 Preview actions without writing files
-o / --output <DIR>       Write all generated files into *DIR* instead of next
                          to the palette files
-f / --file <PATH>        Convert exactly one palette file
-d / --directory <DIR>    Convert palette files in *DIR* (use -r to recurse)
-r / --recursive          With --directory, search sub-directories as well

If neither --file nor --directory is supplied the script prints its help and
exits – preventing accidental bulk generation.

What the script does
--------------------
1. Parse `PaletteEnumerations.cs` to obtain the ordered list of
   `SchemeBaseColors` names.
2. Locate palette `.cs` files that still contain the array declaration.
3. Extract the colour expressions (comments are ignored).
4. Emit `Palette<Theme>NameScheme.cs` with one auto-property per colour.
   Missing indices are initialised with `GlobalStaticValues.EMPTY_COLOR` and
   marked `// missing value` so reviewers spot gaps instantly.

The script is **idempotent** – it never overwrites an existing scheme file.
"""

import argparse
import os
import re
import sys
from pathlib import Path
from itertools import zip_longest

# custom formatter to shift help text
HelpFmt = lambda prog: argparse.HelpFormatter(prog, max_help_position=29)

# ---------------------------------------------------------------------------
# Config
# ---------------------------------------------------------------------------
ROOT = Path(__file__).resolve().parents[1]
BASE_DIR = ROOT / "Source" / "Krypton Components" / "Krypton.Toolkit" / "Palette Builtin"
ENUM_FILE = BASE_DIR / "Enumerations" / "PaletteEnumerations.cs"
ARRAY_MARKER = "private static readonly Color[] _schemeBaseColors"

# Names of menu colours that are optionally defined by most palettes
MENU_NAMES: set[str] = {
    "MenuItemText",
    "MenuMarginGradientStart",
    "MenuMarginGradientMiddle",
    "MenuMarginGradientEnd",
    "DisabledMenuItemText",
    "MenuStripText",
}

# ---------------------------------------------------------------------------
# Helpers
# ---------------------------------------------------------------------------

def parse_enum_names() -> list[str]:
    pattern_start = re.compile(r"\benum\s+SchemeBaseColors\b")
    enum_names: list[str] = []
    inside = False
    with ENUM_FILE.open(encoding="utf-8") as f:
        for line in f:
            if not inside:
                if pattern_start.search(line):
                    inside = True
                continue
            # inside enum
            if "}" in line:
                break
            if line.strip().startswith("//"):
                continue
            m = re.match(r"\s*([A-Za-z0-9_]+)\s*([,=]|$)", line)
            if m:
                enum_names.append(m.group(1))
    return enum_names


def collect_palette_files(root: Path, recursive: bool = True) -> list[Path]:
    """Return .cs paths under *root* that contain the _schemeBaseColors array.
    When *recursive* is False only direct children are examined."""
    result: list[Path] = []
    iterable = root.rglob("*.cs") if recursive else root.glob("*.cs")
    for p in iterable:
        if p.name.endswith(".Designer.cs"):
            continue
        try:
            txt = p.read_text(encoding="utf-8")
        except UnicodeDecodeError:
            continue
        if ARRAY_MARKER in txt:
            result.append(p)
    return result


def extract_color_expressions(palette_path: Path) -> list[str]:
    """Return list of raw Color expressions in declaration order."""
    lines = palette_path.read_text(encoding="utf-8").splitlines()
    start = None
    for idx, line in enumerate(lines):
        if ARRAY_MARKER in line:
            start = idx
            break
    if start is None:
        return []
    colors: list[str] = []
    current = ""
    paren_balance = 0  # track '(' vs ')'

    def flush_current():
        nonlocal current, paren_balance
        if current:
            expr = current.strip().rstrip(',')
            if expr:
                colors.append(expr)
        current = ""
        paren_balance = 0

    for line in lines[start + 1:]:
        if "]" in line and current == "":  # reached end of array
            break

        code_part = line.split("//", 1)[0].strip()
        if not code_part or code_part in {"[", "]"}:
            continue

        # accumulate expression lines
        if current:
            current += " " + code_part
        else:
            current = code_part

        # update parenthesis balance
        paren_balance += code_part.count('(') - code_part.count(')')

        # when balance is zero and line ends with ',' or ')', expression complete
        if paren_balance == 0:
            flush_current()

    flush_current()
    return colors


def _normalize_comment(token: str) -> str:
    if not token:
        return token
    aliases: dict[str, str] = {
        "ButtonNormalBorder1": "ButtonNormalBorder",
        "ButtonNormalBorder2": "ButtonNormalDefaultBorder",
        "ContextMenuHeading": "ContextMenuHeadingBack",
        "AppButtonMenuDocs": "AppButtonMenuDocsBack",
    }
    token = aliases.get(token, token)
    token = token.replace("Inctive", "Inactive")
    if token.lower() in {"color", "fromargb"}:
        return ""
    return token


def extract_array_comments(palette_path: Path) -> list[str]:
    lines = palette_path.read_text(encoding="utf-8").splitlines()
    names: list[str] = []
    inside = False
    for line in lines:
        if not inside:
            if ARRAY_MARKER in line:
                inside = True
            continue
        if "]" in line:
            break
        trimmed = line.lstrip()
        if trimmed.startswith("//"):
            continue
        idx = line.rfind("//")
        if idx >= 0:
            comment_part = line[idx + 2 :]
            m = re.match(r"\s*([A-Za-z0-9_]+)", comment_part)
            if m:
                names.append(_normalize_comment(m.group(1).strip()))
                continue
        if ")" in line or "EMPTY_COLOR" in line:
            names.append("")
    return names


def _print_diff(palette_name: str, enum_names: list[str], array_names: list[str]) -> bool:
    array_lookup: dict[str, int] = {
        n: i for i, n in enumerate(array_names) if n
    }
    missing = unlabeled = out_of_order = 0
    extras: list[str] = []

    for i, enum_name in enumerate(enum_names):
        if enum_name not in array_lookup:
            unlabeled_here = i < len(array_names) and not array_names[i]
            if unlabeled_here:
                unlabeled += 1
            else:
                missing += 1
        else:
            arr_idx = array_lookup[enum_name]
            if arr_idx != i:
                out_of_order += 1

    extras = [k for k in array_lookup.keys() if k not in enum_names and k not in MENU_NAMES]
    clean = not (missing or unlabeled or out_of_order or extras)
    if clean:
        print(f"{palette_name} - Ok!\n")
    else:
        print(f"=== {palette_name} ===")
        print(f"❌ {missing} missing, {unlabeled} unlabelled, {out_of_order} out-of-order, {len(extras)} extra entries.\n")
    return clean


def make_class_name(palette_path: Path) -> str:
    # PaletteMicrosoft365Black.cs -> PaletteMicrosoft365BlackScheme
    base = palette_path.stem  # without ".cs"
    return f"{base}Scheme"


def generate_scheme_code(
    class_name: str,
    colors: list[str],
    enum_names: list[str],
    missing_flags: list[bool] | None = None,
    namespace: str = "Krypton.Toolkit",
) -> str:
    total = len(enum_names)
    # ensure list length
    if len(colors) < total:
        colors = colors + ["GlobalStaticValues.EMPTY_COLOR"] * (total - len(colors))
        if missing_flags is not None:
            missing_flags.extend([True] * (total - len(missing_flags)))
    header = """#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace {namespace};

public sealed class {class_name} : AbstractBaseColorScheme
{{
""".format(namespace=namespace, class_name=class_name)

    props = []
    indent = " " * 4
    brace_col = 60  # 0-based column where "{" should appear
    if missing_flags is None:
        missing_flags = [False] * total

    for name, expr, missing in zip(enum_names, colors, missing_flags):
        prefix = f"{indent}public override Color {name} "
        pad = brace_col - len(prefix)
        if pad < 1:
            pad = 1  # ensure at least a single space
        spaces = " " * pad
        comment = " // missing value" if (missing and name not in MENU_NAMES) else ""
        props.append(f"{prefix}{spaces}{{ get; set; }} = {expr};{comment}")

    body = "\n".join(props)
    code = f"{header}{body}\n}}\n"
    return code


def _align_colors(
    enum_names: list[str],
    colors: list[str],
    comment_names: list[str],
) -> tuple[list[str], list[bool]]:
    """Return (*aligned_colors*, *missing_flags*).

    *aligned_colors* – list of colour expressions positioned according
    to *enum_names*.
    *missing_flags* – parallel list of booleans where **True** marks a
    missing/index-less colour (i.e. the value had to be filled with
    `EMPTY_COLOR`).
    """
    # build lookup from label -> colour expression
    lookup: dict[str, str] = {}
    for label, colour in zip_longest(comment_names, colors, fillvalue=""):
        if label:
            lookup.setdefault(label, colour)
    empty = "GlobalStaticValues.EMPTY_COLOR"
    aligned: list[str] = []
    missing_flags: list[bool] = []
    for name in enum_names:
        if name in lookup:
            aligned.append(lookup[name])
            missing_flags.append(False)
        else:
            aligned.append(empty)
            missing_flags.append(True)
    return aligned, missing_flags

# ---------------------------------------------------------------------------
# Main
# ---------------------------------------------------------------------------

def main():
    # -------------------------------- CLI --------------------------------
    argp = argparse.ArgumentParser(description="Generate *Scheme.cs classes for Krypton palettes", formatter_class=HelpFmt)

    argp.add_argument("--dry-run", action="store_true", help="Preview actions without writing files")
    argp.add_argument("-o", "--output", type=str, help="Directory to place all generated files instead of alongside palette files")

    # selection: palette file OR directory OR default (entire BASE_DIR)
    sel_grp = argp.add_mutually_exclusive_group()
    sel_grp.add_argument("-f", "--file", type=str, help="Convert one specific palette .cs file")
    sel_grp.add_argument("-d", "--directory", type=str, help="Convert palette files under the given directory")

    argp.add_argument("-r", "--recursive", action="store_true", help="With -d/--directory, also search sub-directories")

    args = argp.parse_args()

    output_dir: Path | None = None
    if args.output:
        output_dir = Path(args.output).expanduser().resolve()
        if not args.dry_run:
            output_dir.mkdir(parents=True, exist_ok=True)

    enum_names = parse_enum_names()
    if not enum_names:
        print("Failed to parse enumeration names from", ENUM_FILE, file=sys.stderr)
        sys.exit(1)

    # Determine target palette files based on CLI args
    if args.file:
        target = Path(args.file).expanduser().resolve()
        if not target.is_file():
            print(f"File not found: {target}", file=sys.stderr)
            sys.exit(1)
        palette_files = [target]
    elif args.directory:
        dir_root = Path(args.directory).expanduser().resolve()
        if not dir_root.is_dir():
            print(f"Directory not found: {dir_root}", file=sys.stderr)
            sys.exit(1)
        palette_files = collect_palette_files(dir_root, recursive=args.recursive)
    else:
        print("No --file or --directory specified. Nothing to do.\n", file=sys.stderr)
        argp.print_help(sys.stderr)
        sys.exit(1)

    print(f"Found {len(palette_files)} palette files with _schemeBaseColors")

    for palette in palette_files:
        colors_raw = extract_color_expressions(palette)
        comment_names = extract_array_comments(palette)
        if comment_names:
            _print_diff(palette.name, enum_names, comment_names)
        else:
            print(f"=== {palette.name} ===")
            print("No comment names found – unable to validate palette indices.\n")

        if not colors_raw:
            continue

        # Ensure both lists have the same length for safe zipping in _align_colors
        if len(comment_names) < len(colors_raw):
            comment_names.extend([""] * (len(colors_raw) - len(comment_names)))
        elif len(colors_raw) < len(comment_names):
            colors_raw.extend(["GlobalStaticValues.EMPTY_COLOR"] * (len(comment_names) - len(colors_raw)))

        colors_aligned, missing_flags = _align_colors(enum_names, colors_raw, comment_names)
        class_name = make_class_name(palette)
        scheme_cs_path = (output_dir / f"{class_name}.cs") if output_dir else palette.with_name(f"{class_name}.cs")
        if scheme_cs_path.exists():
            print(f"Skip {scheme_cs_path.relative_to(ROOT)} (already exists)")
            continue
        code = generate_scheme_code(class_name, colors_aligned, enum_names, missing_flags)
        if args.dry_run:
            print(f"Would create {scheme_cs_path.relative_to(ROOT)}")
        else:
            scheme_cs_path.write_text(code, encoding="utf-8")
            print(f"Created {scheme_cs_path.relative_to(ROOT)}")

if __name__ == "__main__":
    main()