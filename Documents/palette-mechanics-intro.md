# Palette Mechanics Introduction

This document describes the layering of palette classes, redirectors, and renderers in the Krypton Toolkit to help new users understand how theming and painting work.

## 1. `PaletteBase`

`PaletteBase` is the root abstract class that defines the public API for retrieving all palette values:

- Colors (`GetBackColor1`, `GetBorderColor1`, etc.)
- Images (`GetBackImage`, etc.)
- Typography (`GetContentShortTextFont`, etc.)
- Layout metrics (`GetMetricPadding`, etc.)
- Hints (`GetBackGraphicsHint`, etc.)

Concrete built-in palettes inherit from `PaletteBase` and override these methods to supply theme-specific values.

## 2. Built-in Base Palettes

From the plenty built-in base palettes, for example:

- **Office 2013**: `PaletteOffice2013Base`  
  Returns Office 2013 values for colors, paddings, fonts, and images.

- **Microsoft 365**: `PaletteMicrosoft365Base`  
  Returns the updated palette values for the Microsoft 365 look.

Each of these base palettes also overrides:

```csharp
public override IRenderer GetRenderer() => KryptonManager.RenderOffice2013;
// or
public override IRenderer GetRenderer() => KryptonManager.RenderMicrosoft365;
```

to select the renderer implementation.

## 3. Palette Redirectors

A `PaletteRedirect` sits between your control and a base palette to:

- Dynamically redirect requests to alternate palettes (e.g. inherited global palette, custom palettes).
- Allow overriding a subset of values via a custom palette.

Controls and Visual classes often use a `PaletteRedirect` instance to decouple drawing logic from a specific palette.

## 4. Visual Classes

Visual classes (e.g. `VisualForm`, `VisualButton`, `VisualControlHost`) implement the drawing logic that:

1. Queries the palette (via a redirector) for the values needed.

2. Calls into the selected `IRenderer` to perform GDI+ or rendering operations.

3. Manages non-client painting, borders, backgrounds, and content drawing with consistent logic.

These classes do *not* hard-code any theme values.

## 5. Renderer Implementations

Renderer classes in `Krypton.Toolkit.Rendering` (e.g. `RenderOffice2013`, `RenderMicrosoft365`) :

- Subclass a common base (such as `RenderOffice2010`) which implements the bulk of drawing logic.
- Override only the small bits of painting that differ per theme (e.g. gradients, blends, special corner treatments).
- Are selected by the base palette’s `GetRenderer()` override.

## 6. Putting It All Together

1. A control’s `ViewManager` or Visual class queries:

```csharp
IRenderer renderer = currentPalette.GetRenderer();
```

2. The Visual code asks the palette (or redirector) for values:

```csharp
Color back = palette.GetBackColor1(style, state);
Padding pad = palette.GetMetricPadding(...);
```

3. The Visual code then calls the renderer:

```csharp
renderer.Draw...(..., back, pad, ...);
```

4. The renderer uses these values and implements the final drawing calls.

This separation allows:

- **Easy theming**: Add a new base palette + renderer, and everything repaints with new look.
- **Dynamic theming**: Switch a palette redirector at runtime and all controls update.
- **Centralized drawing**: Visual classes contain the paint algorithms; palette & renderer supply the data.

## 7. Color Tables for ToolStrip Rendering

Krypton integrates with the .NET ToolStrip rendering pipeline via a family of "color table" classes. These are separate from the main Krypton renderer and are used exclusively by `MenuStrip`, `ToolStrip`, `StatusStrip`, and `ContextMenuStrip`.

- **what they are**: `KryptonColorTable` extends `ProfessionalColorTable` and exposes the color properties that the .NET `ToolStripProfessionalRenderer` consumes while painting menus/strips. Krypton supplies its own renderer `KryptonProfessionalRenderer` that takes a `KryptonColorTable` instance.

- **where they come from**: Each built-in palette exposes a `ColorTable` tailored to its theme by creating a concrete `KryptonColorTable*`:

  ```csharp
  public override KryptonColorTable ColorTable =>
      Table ??= new KryptonColorTable365(BaseColors!.ToArray(), InheritBool.True, this);
  ```

- **where they are used**: When Krypton sets up a menu/strip renderer, it passes the palette's color table into `KryptonProfessionalRenderer`, which ultimately drives all `ToolStrip*` painting via the standard WinForms pipeline.

### 7.1 Key classes and relationships

- **`KryptonColorTable` (base)**
  - Inherits `ProfessionalColorTable` and adds a reference to the owning `PaletteBase` (`Palette` property) and convenience properties for text colors and fonts (`MenuItemText`, `MenuStripText`, `ToolStripText`, `StatusStripText`, `MenuStripFont`, etc.).
  - Acts as the root for theme-specific tables.

- **`KryptonColorTable365` and variants**
  - Constructed with `Color[] colors`, `InheritBool roundedEdges`, and the owning `PaletteBase`.
  - The `colors` array is produced by the palette (typically from `KryptonColorSchemeBase`), and properties index into it using the `SchemeBaseColors` enum. Examples:
    - `GripLight => colors[(int)SchemeBaseColors.GripLight]`
    - `ToolStripBorder => colors[(int)SchemeBaseColors.ToolStripBorder]`
    - `MenuStripText => colors[(int)SchemeBaseColors.StatusStripText]`
  - Some tracking/selection fills come from theme constants inside the table (e.g., `_buttonSelectedBegin`, `_menuItemSelectedBegin/End`) to match the target look.
  - Fonts are resolved from system fonts at runtime and updated on user preference changes.

- **`KryptonProfessionalKCT` (Professional/System/Office 2003 family)**
  - Used by `PaletteProfessionalSystem` and `PaletteProfessionalOffice2003`.
  - These palettes generate a base `KryptonColorTable` reflecting system/visual-style colors, then synthesize a small header color array and create `KryptonProfessionalKCT`. On OS with classic Office 2003 schemes, the palette substitutes scheme-specific arrays.
  - For advanced overrides, `KryptonProfessionalCustomKCT` indexes into a `PaletteColorIndex` array to selectively replace `ProfessionalColorTable` properties.

### 7.2 Execution flow for menus/strips

1. A palette exposes its table via `PaletteBase.ColorTable` (theme-specific `KryptonColorTable*`).
2. Krypton hooks WinForms menus/strips with `KryptonProfessionalRenderer(colorTable)`.
3. During painting, the renderer queries the color table properties (e.g., `MenuItemSelectedGradientBegin`, `ToolStripGradientMiddle`, `StatusStripGradientEnd`).

This is distinct from `GetRenderer()` used by Krypton Visual classes. Visual classes render all other Krypton controls; color tables are only for the ToolStrip family.

### 7.3 Sources at a glance

- **Microsoft 365 / Office 2013/2010/2007 palettes**: color tables pull from the palette's `BaseColors` (`KryptonColorSchemeBase`) via `SchemeBaseColors` indices, plus a handful of theme constants for tracking/selection.
- **Professional/System/Office 2003 palettes**: color tables derive from system/visual-style colors and (when applicable) predefined Office 2003 scheme arrays.
- **Fonts/text**: color tables provide per-surface text colors and fonts for MenuStrip/ToolStrip/StatusStrip; these update on system preference changes.

### 7.4 Practical mapping examples

- `MenuItem` hover: `MenuItemSelectedGradientBegin/End` (theme constants in 365 table) and `MenuItemBorder` (often `SchemeBaseColors.ButtonBorder`).
- `ToolStrip` background: `ToolStripGradientBegin/Middle/End` (from `SchemeBaseColors.ToolStripBegin/Middle/End`).
- `StatusStrip` background: `StatusStripGradientBegin/End` (from `SchemeBaseColors.StatusStripLight/Dark`).

With this separation, palettes supply both:

- A main `IRenderer` for Krypton controls (via `GetRenderer()`), and
- A `KryptonColorTable` for menus/strips (via `ColorTable`).

Switching palettes updates both paths so menus/strips and Krypton controls remain visually consistent.
