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