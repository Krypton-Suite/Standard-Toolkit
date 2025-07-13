#!/usr/bin/env python3
"""
find_searchterms.py – generic multi-term file search helper

Recursively search for C# source files (*.cs) that contain **all** provided
search terms (case-sensitive substring matches).

Up to **five** search terms can be supplied.  The starting directory is the current working
working directory unless you specify one with the `-d`/`--dir` option.

Search terms containing spaces should be enclosed in double-quotes when invoked
from a shell, e.g.:

    python find_searchterms.py -d "C:/code/Standard-Toolkit" "_trackBarColours" "_trackBarColors"

If you omit the root path:

    python find_searchterms.py "_trackBarColours" "_trackBarColors"

When **no** search term is supplied the script prints this help message.
"""

from __future__ import annotations

import sys
from pathlib import Path
from typing import Sequence, List


MAX_TERMS = 5


def file_contains_all_terms(path: Path, terms: Sequence[str]) -> bool:
    """Return *True* if every term in *terms* appears somewhere inside *path*.

    Unreadable files are skipped with a warning.
    """
    try:
        text = path.read_text(encoding="utf-8", errors="ignore")
    except Exception as exc:  # pragma: no cover – informative only
        print(f"[warn] Skipping {path} ({exc})", file=sys.stderr)
        return False

    return all(term in text for term in terms)


def parse_cli() -> tuple[Path, List[str]]:
    """Parse command-line arguments into *(root_path, search_terms)*."""
    args = sys.argv[1:]

    if not args:
        print(__doc__)
        sys.exit(0)

    root = Path.cwd()

    # Explicit directory option ("-d" or "--dir")
    if args and args[0] in {"-d", "--dir"}:
        if len(args) < 2:
            sys.exit("Error: the -d/--dir option requires a directory path argument.")

        root = Path(args[1]).expanduser()
        if not root.exists():
            sys.exit(f"Error: directory does not exist: {root}")

        args = args[2:]

    if not args:
        sys.exit("Error: please provide at least one search term after the root path.")

    if len(args) > MAX_TERMS:
        print(f"[warn] More than {MAX_TERMS} search terms supplied – only the first {MAX_TERMS} will be used.", file=sys.stderr)

    terms = args[:MAX_TERMS]
    return root, terms


def main() -> None:
    root, search_terms = parse_cli()

    matching_files = [p for p in root.rglob("*.cs") if file_contains_all_terms(p, search_terms)]

    terms_display = ", ".join(search_terms)
    if matching_files:
        print(f"Files containing: {terms_display}\n")
        for p in matching_files:
            print(p.as_posix())
    else:
        print(f"No .cs files found containing all of: {terms_display}")


if __name__ == "__main__":
    main()