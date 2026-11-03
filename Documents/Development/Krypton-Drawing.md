# Krypton.Drawing

`Krypton.Drawing` owns `KryptonSVGPictureBox`. The assembly is `IsPackable=false`. `Krypton.Standard.Toolkit` packs `Krypton.Drawing.dll` into every `lib` folder. `Krypton.Toolkit` does not reference Drawing.

SVG is rasterized with Windows Direct2D (`ID2D1SvgDocument`). That API arrived in Windows 10 version 1703 (build 15063). Windows 7, 8, 8.1, and earlier Windows 10 builds set `SvgError` and paint nothing. The package does not ship Svg.Skia, SkiaSharp, HarfBuzzSharp, ExCSS, or their native binaries.

## Architecture

```
KryptonSVGPictureBox
  KryptonSvgDocuments.TryOpen
    KryptonSvgIntrinsicSize     viewBox, else absolute width/height
    KryptonSvgDirect2D          one process-wide ID2D1DeviceContext5
      d3d11.dll  D3D11CreateDevice (hardware, then WARP), BGRA support
      d2d1.dll   ID2D1Factory1.CreateDevice, then ID2D1Device5.CreateDeviceContext
      shlwapi.dll SHCreateMemStream for the SVG bytes
```

P/Invoke stays in `Krypton.Drawing`. Direct2D is not added to `Krypton.Interop` or `Krypton.Toolkit`. There is no SharpDX or Vortice package.

The device context is created once, on the UI thread, the first time markup is opened. `SetDpi(96, 96)` makes one DIP one bitmap pixel. The control already chooses the pixel size. `CreateSvgDocument` uses that pixel size as the viewport, so Stretch, Zoom, Normal, CenterImage, and AutoSize stay in `KryptonSVGPictureBox`.

Readback is a Direct2D target bitmap, copied into a `CPU_READ | CANNOT_DRAW` bitmap, then mapped into a `Format32bppPArgb` `Bitmap`. Direct2D output is premultiplied. A 1x1 CPU-readable bitmap is the capability probe. If that probe fails, every later open reports that Direct2D SVG is not available.

COM objects for each document are released after the parse check and after each draw. The shared device context lives for the process.

## Public API

| Member | Behavior |
| --- | --- |
| `Svg` | Markup. Empty clears the picture. A failed parse keeps the text, clears the picture, and sets `SvgError`. The setter does not throw. |
| `SizeMode` | `Normal` and `CenterImage` use the intrinsic size. `StretchImage` fills the client. `Zoom` fits the aspect. `AutoSize` sets `ClientSize` to the intrinsic size. |
| `LoadFromFile` / `Load` | UTF-8 file or stream. `Load` leaves the stream open. |
| `SvgError` | `null` when the markup is empty or valid. Hidden from the property grid. |
| `ToolTipValues` | Krypton tooltip. `ToolTipManager` is the same pattern as other Krypton controls. |

Intrinsic size comes from the root `viewBox`, otherwise from absolute `width` and `height` (`px`, `in` = 96, `cm`, `mm`, `pt`, `pc`). Percentages are rejected. Missing or non-positive bounds set `SvgError` to "The SVG document has no drawable bounds." A failed `CreateSvgDocument` sets "The SVG document could not be parsed." A failed draw sets "The SVG document could not be drawn."

The raster pixel size is the logical draw size times `DeviceDpi / 96`, and each edge is capped at 8192. The cached bitmap is rebuilt when the markup, size mode, logical size, or DPI changes. The control does not play SMIL animation.

The parameterless constructor does not parse SVG and does not create the Direct2D device.

## Packaging

`Krypton.Standard.Toolkit` copies `Krypton.Drawing.dll` (and the XML doc, and the PDB when symbols are included) into `lib\{tfm}\`. `{tfm}` for modern Windows targets is `_KryptonPackageLibFolder` (`netX.0-windows7.0`).

There is no `native\win-*` Skia folder and no `runtimes` entry for Skia. `NetFramework.SystemResourcesExtensions.targets` still copies the preserialized-resource closure for .NET Framework consumers. It does not copy Skia natives.

## Edge cases

- Windows 7, 8, 8.1, and Windows 10 before version 1703: `SvgError` is "Direct2D SVG is not available on this version of Windows." and the control paints nothing. `net472`, `net48`, and `net481` still compile the Direct2D path, because those TFMs also run on Windows 10 and later.
- The availability test is the Direct2D interface, not `Environment.OSVersion`.
- A bad document does not throw from the setter or from `OnPaint`.
- `KryptonSVGPictureBox` does not derive from `PictureBox`, so the designer does not serialize a raster `Image` in place of the markup.

## Validation

- TestForm: `KryptonSVGPictureBoxDemo`, registered as "4657 SVG Picture Box".
- Standard-Toolkit-Demos: `Krypton SVG Picture Box Example` on branch `alpha-4657-krypton-svg-picture-box`.
- Screenshot: `Scripts/UnitTests/Invoke-SvgPictureBoxScreenshot.ps1` (`UnitTest-CI: exclude`).
- Designer defaults: `Scripts/UnitTests/UnitTest-DesignerSerializationDefaults.ps1`. A fresh drop must not show Modified storage.
