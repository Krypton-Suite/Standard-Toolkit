# Material Theme Family – TODO

- MVP
  - Add `RenderMaterial` (derive from `RenderOffice2010`) and wire into manager
    - Extend `RendererMode` with `Material`
    - Add `KryptonManager.RenderMaterial` and case in `GetRendererForMode`
  - Introduce Material palette modes
    - Extend `PaletteMode` with `MaterialLight`, `MaterialDark` (order synced with map)
    - Add display names in `PaletteModeStrings.SupportedThemes` before `Custom`
    - Map modes to palette classes in `PaletteClassTypeConverter`
    - Wire `KryptonManager.GetPaletteForMode` with singletons for Material palettes
    - Include in `UpdatePaletteImages` switch (use Microsoft365 toolbar images initially)
  - Scaffold palette classes
    - `PaletteMaterialLight` (inherits existing light 365 variant for now, overrides `GetRenderer` to `RenderMaterial`)
    - `PaletteMaterialDark` (inherits existing dark 365 variant, overrides `GetRenderer`)

- Phase 2
  - Add `PaletteMaterialBase` (family base) with scheme colors and compact metrics
  - Replace light/dark variants to inherit from `PaletteMaterialBase` with Material schemes
  - Tune renderer: flatten borders/backgrounds, minimal insets, ribbon cluster edges

- Resources (phase 1)
  - Material check/radio images; hook via palette image lists

- Validation
  - Verify theme appears in `KryptonThemeComboBox` and `PaletteViewerForm`
  - Confirm form chrome border thickness and ribbon caption integration

```csharp
// Adoption
var manager = new KryptonManager();
manager.GlobalPaletteMode = PaletteMode.MaterialLight; // or MaterialDark
```

---

PHASE 1 SUMMARY:

Status: I created a concise TODO based on your plan and implemented the MVP scaffolding for a Material theme family with minimal, surgical changes. I added the Material renderer, two palette variants, and wired them through the enums, name map, converter, and manager. Lint is clean.

TODO created
    - Documents/material-theme-todo.md

Key edits implemented

- Renderer
  - Added new renderer class: `Source/Krypton Components/Krypton.Toolkit/Rendering/RenderMaterial.cs`
  - Extended `RendererMode` with `Material` in `Rendering/RenderDefinitions.cs`
  - Wired `KryptonManager` to expose `RenderMaterial` singleton and return it from `GetRendererForMode(RendererMode.Material)`

- Palettes
  - Added MVP variants:
    - `Source/Krypton Components/Krypton.Toolkit/Palette Builtin/Material/PaletteMaterialLight.cs` (inherits `PaletteMicrosoft365BlueLightMode`, overrides `GetRenderer()` to Material)
    - `Source/Krypton Components/Krypton.Toolkit/Palette Builtin/Material/PaletteMaterialDark.cs` (inherits `PaletteMicrosoft365BlackDarkMode`, overrides `GetRenderer()` to Material)
  - Extended `PaletteMode` with `MaterialLight` and `MaterialDark` and preserved enum/dictionary lock-step
  - Updated theme name map with display names:
    - `PaletteModeStrings.SupportedThemes`: “Material - Light”, “Material - Dark”
  - Mapped new palette classes in `PaletteClassTypeConverter`
  - Added `KryptonManager` singletons and cases in `GetPaletteForMode` for `MaterialLight` and `MaterialDark`
  - Mapped toolbar image set for Material (using Microsoft 365 set initially) in `KryptonManager.UpdatePaletteImages`

How to try it

- In your app startup:
  - C#:

    ```csharp
    var manager = new KryptonManager();
    manager.GlobalPaletteMode = PaletteMode.MaterialLight; // or MaterialDark
    ```

- The new themes will appear anywhere the supported theme list is used (e.g., `KryptonThemeComboBox`, `PaletteViewerForm`).

Notes

- This is an MVP: `PaletteMaterialLight/Dark` re-use Microsoft 365 Light/Dark values but select the new Material renderer. The next step is to introduce `PaletteMaterialBase` with Material scheme colors and denser metrics, then pivot variants to inherit from it.
- `PaletteMode` and `PaletteModeStrings.SupportedThemes` order is preserved per the project’s strict rules.

Summary of changes

- Added `RendererMode.Material`, `RenderMaterial`, and `KryptonManager.RenderMaterial` mapping.
- Added `PaletteMode.MaterialLight/Dark`, display strings in `PaletteModeStrings`, and palette factory wiring in `KryptonManager`.
- Introduced `PaletteMaterialLight` and `PaletteMaterialDark` (MVP inheriting 365 variants with Material renderer).
- Mapped palette class conversion in `PaletteClassTypeConverter`.
- Added a focused, actionable TODO list at `Documents/material-theme-todo.md`.

---

PHASE 2 STATUS UPDATE:

- Implemented `PaletteMaterialBase` in `Palette Builtin/Material/Bases/PaletteMaterialBase.cs` deriving from `PaletteMicrosoft365Base`, returning `KryptonManager.RenderMaterial` and applying compact Material metrics:
  - Reduced header/input/bar paddings, smaller adjacent gaps, compact page button padding.
  - Small, uniform button/input border rounding (4f) via `GetBorderRounding`.
- Refactored variants to inherit from `PaletteMaterialBase` while reusing existing 365 Light/Dark schemes and image lists for now:
  - `PaletteMaterialLight` now derives from `PaletteMaterialBase` and passes `PaletteMicrosoft365BlueLightMode_BaseScheme` plus image lists.
  - `PaletteMaterialDark` now derives from `PaletteMaterialBase` and passes `PaletteMicrosoft365BlackDarkMode_BaseScheme` plus image lists.
- Tweaked `RenderMaterial` to flatten ribbon cluster edges and keep toolstrip rendering consistent with solid fills.

---

From MetroFramework’s `MetroForm` (MIT licensed) we can apply patterns for a Material-flat window chrome and resizing behavior. Relevant takeaways:

- Borderless chrome and resizing
  - Handle `WM_NCCALCSIZE` and `WM_NCHITTEST` to remove the standard frame and implement custom resize grips with a fixed thickness.
- DWM integration
  - Toggle non-client rendering and shadows via DWM (`DwmSetWindowAttribute`, `DwmExtendFrameIntoClientArea`) with a fallback shadow when DWM is off.
- Caption/ControlBox drawing
  - Paint a flat caption area and custom Close/Min/Max hit targets; forward move/resize via `WM_SYSCOMMAND` and `HTCAPTION`.
- Min/max bounds
  - Respect `WM_GETMINMAXINFO` for proper sizing when borderless.
- Flicker control
  - Aggressive double-buffering and avoiding redundant repaints.

This is useful to refine `VisualForm`/`KryptonForm` so Material forms have thin borders, flat captions, and consistent shadows.

Reference: [MetroForm.cs (ModernUI)](https://github.com/MassimoLoi/ModernUI/blob/master/MetroFramework/Forms/MetroForm.cs)

What is already implemented
    - Form visuals: flat chrome via zero `FormMain`/`HeaderForm` border width and rounding in `PaletteMaterialBase`.
    - UX: preserved easy resizing for borderless look by expanding the invisible edge hit-band to 6px when `RenderMaterial` is active in `KryptonForm.WindowChromeHitTest(...)`.

Additional reusable knobs already in the toolkit you can use now
    - Shadow tuning on any `KryptonForm` through `ShadowValues` (hooked in `VisualForm`):
      - `EnableShadows`, `Offset`, `ExtraWidth`, `BlurDistance`, `Colour`, `Opacity` control the per-edge shadow windows managed by `ShadowManager`.
      - Example (Material default suggestion):
        - `EnableShadows = true`, `Offset = new Point(2,2)`, `ExtraWidth = 6`, `BlurDistance = 40`, `Colour = Color.FromArgb(0,0,0)`, `Opacity = 40`.
    - Blur (Acrylic-style) channel via `BlurValues` in `VisualForm` if you want soft backdrop effects. It’s already integrated, can be turned on per form.

Why I kept the code changes minimal
    - Your Material family already wires the “apply theme chrome border width” flag, and `KryptonForm`/`VisualForm` contain the full NCA (hit test, paint, syscommands) infrastructure. A Material theme mainly needs:
      - Flat header/frame visuals (done in palette).
      - Comfortable resize with borderless visuals (done in hit test).
      - Optional shadow/blur presets (available via `ShadowValues`/`BlurValues` without core edits).

If you want, I can add small Material defaults
    - Set Material-friendly defaults on forms when `RenderMaterial` is active:
      - Automatically enable subtle shadows (low opacity, small offset).
      - Center caption text for Material.

These would be minimal edits in `KryptonForm` gated by renderer check, keeping other themes unchanged.

MetroForm does full client-area chrome with a custom caption panel and fixed metrics; your Krypton path paints non-client chrome via `VisualForm` using palette metrics. So MetroForm “feels” flatter because it totally replaces the frame and enforces a fixed, tall, solid header, centered title, no icon, and very plain caption buttons. Your current Material theme still inherits 365 colors/metrics for much of that, so it feels familiar rather than distinctly Material.

## Concrete next steps (surgical locations)

- Caption height:
  - In `KryptonForm.CheckViewLayout`, `_headingFixedSize.FixedSize = new Size(windowBorders.Top, windowBorders.Top)`. Provide a Material-specific fixed height (e.g., 44) when `Renderer is RenderMaterial` instead of using `windowBorders.Top`.
- Icon and title:
  - Default `AllowIconDisplay = false` when `Renderer is RenderMaterial`; keep user override.
  - Ensure `FormTitleAlign = Center` (already added).
- Header colors:
  - In `PaletteMaterialBase`, override header back colors:
    - `GetBackColor1/2(HeaderForm)` and/or `HeaderPrimary/Secondary` to real Material tones (light/dark).
- Caption buttons (hover/press):
  - In `PaletteMaterialBase`, tune `PaletteBackStyle.ButtonForm/ButtonFormClose` and related content colors for flatter states; keep outlines subtle.
- Rounding and paddings:
  - Keep `GetBorderRounding` at 4f for controls; set `HeaderButtonPaddingForm` slightly larger, ensure consistent gaps (we already compacted metrics; adjust to your taste).
