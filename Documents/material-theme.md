# Material Theme Family – Implementation Plan

## Goal

Introduce a first-class Material theme family for Krypton Toolkit with a dedicated family base (analogous to `PaletteMicrosoft365Base`) and the rendering/mapping necessary to achieve a flatter, denser UI (notably reduced/removed window chrome padding/borders) while preserving Toolkit behaviors and extensibility.

### Scope and assumptions

- Start with two variants: Material Light and Material Dark. Additional accents can layer on later.
- Prioritize form chrome density, flat backgrounds, solid fills, simplified borders, and consistent corner radii across controls.
- Do not change control behavior or public APIs beyond adding new modes and renderer/palette types.

### Architecture reference points in the Toolkit

- Palettes and color/metric APIs: `Krypton.Toolkit.PaletteBase` with overrides such as `GetBackColor1/2`, `GetBorderWidth`, `GetBorderRounding`, `GetMetricPadding`, `GetMetricInt`, `GetContent…` etc.
  
  - See `Source/Krypton Components/Krypton.Toolkit/Palette Builtin/Microsoft 365/Bases/PaletteMicrosoft365Base.cs` as the “family base” pattern to mirror.

- Renderer selection and singletons: `Krypton.Toolkit.KryptonManager`
  
  - `GetRendererForMode(RendererMode mode)` maps `RendererMode` to singleton renderer instances like `RenderMicrosoft365`.
  - Palettes select their renderer via `public override IRenderer GetRenderer()`.

- Renderers: `Krypton.Toolkit.Rendering`
  
  - Base: `RenderBase`, plus layered implementations e.g. `RenderOffice2010`, `RenderOffice2013`, `RenderMicrosoft365`.
  - Border padding used by layout: `RenderStandard.GetBorderDisplayPadding(...)` is consulted by `ViewDrawCanvas`/`ViewDrawDocker` to inset layouts from themed borders.

- Form chrome and non-client metrics:
  
  - `KryptonForm`/`VisualForm` and `PaletteFormBorder` compute real window borders using `VisualForm.RealWindowBorders` and palette-returned widths. `KryptonManager.GlobalUseThemeFormChromeBorderWidth` integrates theme border widths with the OS sizing logic.
  - `CommonHelper.GetWindowBorders(...)` and `PaletteFormBorder.BorderWidths(...)` are key to border thickness behavior.

- Ribbon shape/metrics: palette general ribbon APIs: `PaletteRibbonGeneralInherit`, `PaletteRibbonGeneral`, and `PaletteRibbonShape`.

- Theme discovery/wiring:
  
  - Modes enumeration: `Krypton.Toolkit.PaletteMode` and `RendererMode`.
  - Theme name mapping: `PaletteModeStrings.SupportedThemes` (must stay in lock-step order with `PaletteMode`).
  - Mode-to-palette factory: `KryptonManager.GetPaletteForMode(PaletteMode mode)`.
  - Demo/test selection: `KryptonThemeComboBox`, `ThemeManager`, `PaletteViewerForm`.

### Deliverables and changes by subsystem

#### 1) Rendering – new Material renderer

- Add `RenderMaterial : RenderOffice2010` in `Source/Krypton Components/Krypton.Toolkit/Rendering/RenderMaterial.cs`.
  
  - Initial focus areas:
    - Override border-related calculations to present thinner, simpler borders and smaller effective insets used by layout:
      - Tune `GetBorderDisplayPadding(...)` via inherited logic by pairing with palette values (`GetBorderWidth`, `GetBorderRounding`) kept intentionally small for Material.
      - Ensure flat, solid backgrounds for common elements where prior themes used gradients (mirror `RenderMicrosoft365` techniques for flattening but push further for Material).
    - Form chrome visuals (caption area and frame): keep flat fills, minimal separators, gentle rounding; avoid ornamental gradients.
    - Ribbon-specific overrides: ensure flat group/title backgrounds, subtle separators, and minimal cluster edges (follow the approach used by `RenderMicrosoft365.DrawRibbonClusterEdge`, but neutralize gradients and opacity effects).
    - ToolStrip rendering: return a professional renderer configured for flat (solid) fills and simplified borders from `RenderBase.RenderToolStrip(...)`.
  
  - Optional phase 2: glyph painting for checkboxes/radios via the renderer’s glyph channel for vector look; phase 1 can reuse palette-provided images.

- Extend `RendererMode` enum with `Material` and wire `KryptonManager.GetRendererForMode(RendererMode.Material)` to return a `RenderMaterial` singleton via `KryptonManager.RenderMaterial`.

#### 2) Palettes – Material family base and variants

- Add a new family base `PaletteMaterialBase : PaletteBase` in `Source/Krypton Components/Krypton.Toolkit/Palette Builtin/Material/Bases/PaletteMaterialBase.cs`.
  
  - Responsibilities:
    - Provide the Material family’s scheme color array (`protected override Color[] SchemeColors`) and `Get*Color*` overrides that map controls and states to those scheme entries.
    - Return the Material renderer: `public override IRenderer GetRenderer() => KryptonManager.RenderMaterial;`.
    - Define Material-specific content paddings and gaps (denser than 365): reduce `GetMetricPadding(...)` for headers, bars, and input controls; keep adjacent gaps tight via `GetContentAdjacentGap(...)`.
    - Reduce border thickness and rounding across controls: small `GetBorderWidth(...)`, small uniform `GetBorderRounding(...)` (e.g., 4–6 device-independent px) to keep layout inset minimal.
    - Keep flat backgrounds: return `PaletteColorStyle.Solid` where appropriate.
    - Keep typography defaults to Toolkit base (`Segoe UI`), allowing app-level font override via `BaseFont` if desired.

- Add initial variants, inheriting from `PaletteMaterialBase` with variant-specific `SchemeColors` (and subtle metric tweaks if needed):
  - `PaletteMaterialLight`
  - `PaletteMaterialDark`

- Checkbox/Radio imagery:
  - Phase 1: ship Material-appropriate checkbox/radio images as palette images (mirroring the 365 pattern that injects image lists/arrays into the base).
  - Phase 2: move to vector-rendered glyphs via renderer glyph API for DPI independence.

- Optional: extend `BasePaletteType` to include `Material` if the type is surfaced anywhere in diagnostics/viewers (non-breaking but nice-to-have).

#### 3) Modes and theme wiring

- Extend `PaletteMode` enum with:
  - `MaterialLight`
  - `MaterialDark`

- Update `PaletteModeStrings.SupportedThemes` to include display names for these entries, preserving strict order with the enum as required by the Toolkit.

- Update `KryptonManager.GetPaletteForMode(PaletteMode mode)` to return singletons for `PaletteMaterialLight` and `PaletteMaterialDark`.

- Expose a singleton `KryptonManager.RenderMaterial` and wire it in `GetRendererForMode(RendererMode.Material)`.

- No changes needed for `KryptonThemeComboBox` or `ThemeManager`; they will pick up modes automatically when the dictionary and enum are updated.

#### 4) Form chrome density and window border integration

- Ensure the Material family reduces form chrome thickness by theme, not via user code:
  - Keep `KryptonManager.GlobalUseThemeFormChromeBorderWidth = true` (default) so `PaletteFormBorder.Width` and `CommonHelper.GetWindowBorders(...)` respect the palette’s border width.
  - In `PaletteMaterialBase`, provide small `GetBorderWidth(PaletteBorderStyle.Form, ...)` and small `GetBorderRounding(...)` so `RenderStandard.GetBorderDisplayPadding(...)` yields minimal insets.
  - Keep caption/header metrics tight via `GetMetricInt(...)` and `GetMetricPadding(...)` for header styles.

- Validate ribbon caption integration:
  - `ViewDrawRibbonCaptionArea` integrates with the form based on `RealWindowBorders.Top`. The Material border should keep sufficient height for caption buttons and hit-testing while staying minimal. Verify against `MIN_INTEGRATED_HEIGHT` in ribbon integration.

#### 5) Ribbon specifics (optional initial pass)

- Keep `PaletteRibbonShape` as-is initially, returning `Microsoft365` shape for MVP to avoid extra shape plumbing, while delivering Material visuals via colors/metrics/renderer.
- If a distinct look is desired later, add `PaletteRibbonShape.Material` and update any switch statements in renderers that branch on shape.

#### 6) Resource packaging (phase 1)

- Add Material check/radio/state images (normal, tracking, pressed, disabled) into Toolkit resources and surface them via the new Material family base, mirroring the 365 pattern with image lists.
- Provide any needed monochrome toolbar glyphs consistent with flat, solid fills.

### Behavior specifics and suggested defaults

- Borders: 1 px for most controls; 0–1 px for internal separators; solid color, high-contrast in dark mode.
- Corner radius: uniform small radius (4–6) across buttons, inputs, containers.
- Backgrounds: solid fills; eliminate gradients; hover/pressed states lighten/darken base by a small factor.
- Inputs: reduced internal padding but maintain accessibility hit targets (e.g., 28–32 px control height typical).
- Form caption: flat background; minimal separator at content edge; standard glyphs for sys buttons.

### Testing and demos

- Verify selection/addition in:
  - Test app `KryptonThemeComboBox` – new themes should appear once `PaletteModeStrings` is updated.
  - `PaletteViewerForm` – color arrays should display/edit correctly for Material variants.
  - Ribbon integration – confirm top border height, hit-testing, and minimize/maximize/close alignment.

### Phased delivery plan

1) MVP (single PR):
   - Add `RenderMaterial` and `KryptonManager.RenderMaterial` wiring (`RendererMode.Material`).
   - Add `PaletteMaterialBase`, `PaletteMaterialLight`, `PaletteMaterialDark` with scheme colors, paddings, border width/rounding, and `GetRenderer()` override.
   - Extend `PaletteMode` and `PaletteModeStrings.SupportedThemes`; update `KryptonManager.GetPaletteForMode` to return Material variants.
   - Ship basic checkbox/radio images.

2) Visual polish:
   - Tune corner radii and state colors per control categories; refine ribbon cluster edges and tab separators.
   - Optional: add `PaletteRibbonShape.Material` and handle in renderers.

3) Vector glyphs and DPI refinement:
   - Implement vector checkbox/radio glyphs in the renderer’s glyph drawing channel.
   - Audit high-DPI scaling across controls with Material metrics.

### Risks and considerations

- Enum/dictionary order coupling: `PaletteMode` and `PaletteModeStrings.SupportedThemes` must remain in strict lock-step order.
- Non-client area behavior varies with Windows versions; keep testing for maximized/MDI combinations (`CommonHelper.IsFormMaximized` paths) to avoid off-by-ones on borders.
- Ribbon caption integration depends on border height; ensure hit areas remain compliant.
- Fonts: keep defaults to avoid unexpected reflow; allow app developers to set `BaseFont` explicitly for Material typography.

### Adoption snippet

```csharp
// At app startup
var manager = new KryptonManager();
manager.GlobalPaletteMode = PaletteMode.MaterialLight; // or MaterialDark
// The palette will select RenderMaterial internally
```

### File map (new)

- `Source/Krypton Components/Krypton.Toolkit/Rendering/RenderMaterial.cs`
- `Source/Krypton Components/Krypton.Toolkit/Palette Builtin/Material/Bases/PaletteMaterialBase.cs` (phase 2)
- `Source/Krypton Components/Krypton.Toolkit/Palette Builtin/Material/PaletteMaterialLight.cs`
- `Source/Krypton Components/Krypton.Toolkit/Palette Builtin/Material/PaletteMaterialDark.cs`
- Resource updates for checkbox/radio images (phase 1)

### Minimal code touch-points (existing files to update)

- `Krypton.Toolkit/Rendering/RenderDefinitions.cs` – add `RendererMode.Material`.
- `Krypton.Toolkit/Controls Toolkit/KryptonManager.cs` – add `RenderMaterial` singleton property and extend `GetRendererForMode`.
- `Krypton.Toolkit/Palette Base/PaletteMode.cs` – add `MaterialLight`, `MaterialDark` (maintain order).
- `Krypton.Toolkit/Translations/Converters/PaletteModeStrings.cs` – add display names in the ordered map.
- `Krypton.Toolkit/Controls Toolkit/KryptonManager.cs` – extend `GetPaletteForMode` to return Material variants.

### Out-of-scope (for initial delivery)

- Ink ripple animations and elevation/shadow systems for all controls.
- New control types or layout paradigms; we focus on theming.
