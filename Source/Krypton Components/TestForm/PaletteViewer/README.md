# PaletteViewer

PaletteViewer is a WinForms utility, built with .NET 4.7.2 and Krypton Toolkit, that visualises every colour exposed by the Krypton palette API across multiple themes. It condenses the entire colour surface into a single, scrollable `KryptonDataGridView` so that developers and designers can **audit, compare and explore** palette data with confidence.

---

## Why you might need it

Krypton palettes store their theme data in a 232-entry colour array that is indexed directly by the `SchemeBaseColors` enumeration. When that array is mis-aligned—too short, out-of-order or returning transparent colours—the Toolkit will surface incorrect hues or, worse, throw `IndexOutOfRangeException`. PaletteViewer makes those problems obvious by:

* displaying the raw colour for every `SchemeBaseColors` slot;
* highlighting missing, unlabelled, out-of-order or extra indices for each theme;
* warning when a palette attempts to read beyond its own array.

If you maintain custom themes or contribute to the Toolkit itself, PaletteViewer helps catch alignment errors before they ship.

---

## What you'll see on-screen

1. **Toolbar** A `KryptonPanel` hosts drop-downs and buttons for loading or removing palettes, switching the app's own theme and exporting data. Palette names originate from the Toolkit's built-in `PaletteMode` values—no external files are required.
2. **Grid** The first three columns are fixed and read-only: the numerical index of `SchemeBaseColors`, the enum name itself, and a list of public palette API call(s) that resolve to that colour slot. Every additional column represents a loaded palette, and each of those cells embeds a `KryptonLabel` whose background is the palette's colour and whose text shows the hex value.
3. **Visual cues**
   * *Slots whose colour entry is missing, unlabelled, out-of-order or extra are shown with the grid's red error icon; the tooltip repeats the diagnostic text (e.g. "Missing index").*
   * *Fully transparent colours appear as italic "Transparent" (or "EMPTY") text and do not raise an error glyph.*
   * *Foreground and selection colours adjust automatically to maintain WCAG contrast against the background swatch.*
   * *Each palette column header summarises the theme's health (⚠n missing or Δ m/u/o/x).*
   * *Δ m/u/o/x is a compact four-number diagnostic: **m** = missing indices, **u** = unlabelled entries, **o** = out-of-order slots (a colour exists but is sitting in the wrong index, so the palette will serve the wrong hue for that `SchemeBaseColors` value), **x** = extra colours beyond the enum length.*

Click in any colour cell to be able to copy its hex string to the clipboard; hover for a tooltip listing the method or property that exposes that colour.

---

## Source Path Requirement

**Important**: PaletteViewer requires the source path to the Krypton Toolkit sources to be configured for full palette analysis functionality. Without this path configured:

* All palette operation controls (Add Palette, Add All, Remove, etc.) will remain **disabled**
* The application can only display basic palette information but cannot perform comprehensive analysis
* Advanced diagnostics like unlabelled entries, out-of-order values, and extra colours beyond enum length will not be available

To configure the source path:
1. Use the **Browse** button in the toolbar to select the root folder of your Krypton Toolkit source code
2. The path should point to the folder containing the "Source" folder
3. Once a valid path is set, all palette controls will become enabled
4. Use the **Clear** button to remove the source path setting if needed

The source path setting is essential because PaletteViewer needs access to the actual palette implementation files to perform static source analysis and provide the complete diagnostic information that makes this tool valuable for palette development and maintenance.

---

## Under the hood

PaletteViewer discovers and renders palette information at runtime; nothing is hard-coded.  The workflow for **Add Palette** is roughly:

1. **Compatibility filter**   The chosen `PaletteMode` is skipped if it belongs to the Sparkle or legacy Professional families.  Custom palettes (and their derived modes) are supported, provided they expose a **`ColorTable.Colors/Colours`** array.
2. **Column setup**   A new `DataGridViewTextBoxColumn` is added.  The header text is the palette's display name, broken across words to fit, and later gains diagnostic suffixes (see step 6).
3. **Colour extraction**   `TryGetPaletteColors` queries the palette's **`ColorTable.Colors`** (or **`Colours`**) property and returns that array.  Palettes whose array is shorter than the 232-entry `SchemeBaseColors` enum are flagged:<br/>
   • Missing indices raise a red ⚠ "Missing index" glyph and increment a per-column counter.<br/>
   • Fully transparent entries appear as italic **Transparent** (or **EMPTY**) labels and are *not* considered errors.
4. **API mapping**   For each unique palette *base* class, `BuildMethodEnumMapping` reflects over every public instance method returning `Color` plus all `Color` properties on the `ColorTable`.  Each returned colour is matched to its enum slot and the method/property names are written into the **API Call(s)** column.  This happens only once per base class to keep performance acceptable during *Add All*.
5. **Static source diagnostics**   If the user has pointed PaletteViewer at a Toolkit *source* folder, `ThemeArrayInspector.GetIssues` provides deeper analysis: unlabelled entries, out-of-order values and "extra" colours that sit beyond the enum length.  A concise `Δ m/u/o/x` summary (missing / unlabelled / out-of-order / extra) is appended to the column header and affected cells receive explanatory error glyphs.
6. **Styling & accessibility**   Each cell's foreground, background and selection colours are calculated to maintain minimum WCAG contrast.  Column widths are auto-sized once, then frozen; row heights are doubled for slots whose API mapping spans multiple method names.

Because every step runs dynamically, PaletteViewer automatically adapts to new Toolkit releases, custom themes and API extensions without needing code changes.

---

## Building & running

The project targets **.NET Framework 4.7.2** and has no dependencies beyond the Krypton Toolkit packages. In Visual Studio 2022:

1. Open `Krypton Toolkit Suite 2022 - VS2022.sln`.
2. Set **TestForm** as the startup project.
3. Build and run.
4. In the running application select *PaletteViewer* from the test harness menu.

---

## Exporting data

The toolbar offers CSV, JSON or XML export. Choose a format, click **Save** and you'll receive a serialised snapshot of the current grid—ideal for diffing palette revisions in version control.

---

## Contributing

Found a bug, need an extra export format or want support for custom palettes? Please open an issue or submit a pull request. When contributing code, keep to C# 7.3 standards, CR/LF line endings and avoid inline comments.

---

## License

PaletteViewer is distributed under the same license as the Krypton Toolkit. See `LICENSE` for details.
This tool was developed and contributed by [@tobitege](https://github.com/tobitege).
