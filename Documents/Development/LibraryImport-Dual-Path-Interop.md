# LibraryImport Dual-Path Interop (Developer Guide)

## Overview

Issue [#3874](https://github.com/Krypton-Suite/Standard-Toolkit/issues/3874) migrates eligible Win32 P/Invoke declarations from runtime `[DllImport]` marshalling to source-generated `[LibraryImport]` on modern target frameworks, while retaining `[DllImport]` for .NET Framework (`net472` / `net48` / `net481`).

This builds on [#3855](https://github.com/Krypton-Suite/Standard-Toolkit/issues/3855) / [#3860](https://github.com/Krypton-Suite/Standard-Toolkit/pull/3860), which moved shared interop into the internal `Krypton.Interop` assembly (`PI` / `Libraries`).

## Architecture

```text
Callers (Toolkit / Ribbon / Utilities / TestForm / ...)
        |
        v
   Krypton.Interop  (namespace still Krypton.Toolkit for PI)
        |
        +-- #if NET8_0_OR_GREATER
        |       [LibraryImport] static partial ...
        +-- #else
                [DllImport] static extern ...
```

- Prefer declaring new shared APIs on `PI` in `PlatformInvoke.cs` (or a sibling partial file).
- Call sites should consume `PI` instead of duplicating local DllImport/LibraryImport blocks.
- Facades such as `ImageNativeMethods` and `PlatformEvents` may remain for historical call shapes, but should forward to `PI`.
- `AllowUnsafeBlocks` is enabled on `Krypton.Interop` and `TestForm` because the LibraryImport source generator (and stock-icon fixed buffers) may emit or require unsafe code.

## Dual-path convention

Gate modern TFMs with `#if NET8_0_OR_GREATER` (matches supported modern TFMs; LibraryImport requires .NET 7+).

### Attribute mapping

| DllImport | LibraryImport |
|-----------|---------------|
| `extern` method | `static partial` method on a `partial` type |
| `CharSet.Unicode` / `Auto` (Windows) | `StringMarshalling.Utf16` (prefer explicit `*W` entry points) |
| `SetLastError = true` | `SetLastError = true` |
| Implicit `bool` | Explicit `[MarshalAs(UnmanagedType.Bool)]` / `[return: MarshalAs(...)]` |
| `[In]` / `[Out]` on by-ref | Use `in` / `ref` / `out` keywords; drop `[In]`/`[Out]` attrs |

## What stays on DllImport

- `StringBuilder` buffer parameters
- `HandleRef` (prefer rewrite callers to `IntPtr`)
- Arrays (`IntPtr[]`, `byte[]`, ...) — e.g. `PI.ExtractIconEx`
- `BestFitMapping` / `ThrowOnUnmappableChar`
- Managed classes / complex layouts (`CHOOSEFONT`, `MONITORINFO`, `PAINTSTRUCT`, `SHFILEINFO`, `DTTOPTS`, `SafeModuleHandle`, COM `[ComImport]`, `PRINTDLG_32` / `PRINTDLG_64`, etc.)

`KryptonPrintDialog` keeps local `PrintDlg_*` DllImports because the dialog structures are managed classes.

## Consolidation status

Routed to `PI` (no local import):

- `ImageNativeMethods` (facade), `StockIconHelper` (`SHSTOCKICONINFO` + `SHGetStockIconInfo`)
- `LocalWindowsHook` / `LocalCbtHook`
- `KryptonAlternateCommandLinkButton` note/shield messages
- `PlatformEvents` (forwards; uses `PI.RECT` for window rects while preserving historical LTRB `Rectangle` layout)
- TestForm `Program` AppUserModelID / DPI aware

Still local by design:

- `KryptonPrintDialog` PrintDlg class layouts
- TestForm `PropertyGridTest` GDI stress helpers (demo-only)
- Remaining `StringBuilder`/`array`/`HandleRef` APIs inside `PI` itself

## CsWin32 (optional, new APIs only)

`Krypton.Interop` references `Microsoft.Windows.CsWin32` and `NativeMethods.txt`.

- Do **not** regenerate the entire `PI` surface with CsWin32.
- Add API names to `NativeMethods.txt` only for genuinely new Win32 needs, then decide whether to dual-path a thin wrapper into `PI` for Framework.
- Prefer `>= 0.3.287` when multi-targeting `net472` (IComIID emission on downlevel TFMs).

## Conversion helper

`Scripts/Tools/Convert-DllImportToLibraryImport.ps1` converts eligible declarations in a file and skips unsupported signatures. After running it:

1. Ensure containing types are `partial`.
2. Build `net472` and at least one `net8.0-windows` (or newer) TFM.
3. Fix remaining `SYSLIB105x` / `CS0227` (unsafe) issues manually.

## Validation

- Build Interop / Toolkit / Toolkit.Utilities for `net472` and `net8.0-windows`.
- Smoke TestForm: DPI / AppUserModelID startup, stock icons, hooks/CBT shell dialogs, floating toolbars, extended message box positioning, print dialog.
