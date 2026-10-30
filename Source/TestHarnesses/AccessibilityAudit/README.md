# Accessibility audit

Scores WCAG contrast for every built-in theme and smoke-tests the accessible object of every public Krypton control that has a parameterless constructor.

`PaletteMode.Custom` and `PaletteMode.ProfessionalSystem` are not contrast-scored. Disabled palette states are reported and do not fail the run. The ten controls with a custom accessible object must report their expected role even when other misses are listed in the baseline. Text-entry controls report `Text`, combo box reports `ComboBox`, lists report `List`, the link label reports `Link`, and the up-down controls report `SpinButton` (the role of the inner control on .NET 10).

The audit sets `KRYPTON_HEADLESS=1`. That tells `DebugTools.NotImplemented` to skip its modal dialog and return `ArgumentOutOfRangeException`, which the audit records as a skipped palette pair.

From the repository root:

```cmd
dotnet build "Source/TestHarnesses/AccessibilityAudit/AccessibilityAudit.csproj" -c Debug -f net10.0-windows -m:1 -p:BuildInParallel=false
Source\TestHarnesses\AccessibilityAudit\bin\Debug\net10.0-windows\AccessibilityAudit.exe --baseline Scripts\Accessibility\baseline.json --report accessibility-report.md
```

Rewrite the checked-in baseline from the current results:

```cmd
Source\TestHarnesses\AccessibilityAudit\bin\Debug\net10.0-windows\AccessibilityAudit.exe --baseline Scripts\Accessibility\baseline.json --report accessibility-report.md --update-baseline
```

The process exits 0 when every current failure key is already in `Scripts/Accessibility/baseline.json`. It exits 1 when a new key appears, or when one of the ten provider controls reports the wrong role. Keys that now pass are listed in the report as stale and do not fail the run.

## DPI audit

The same executable scores `Control.Scale(1.5, 1.5)` for every constructible control. A control that had a positive width and height and is still below 1.2x on either axis is `scale-ignored`. A throw is `scale-threw`. GitHub-hosted runners are 96 DPI, so this checks that `Scale` is honored. It does not require a high-DPI monitor.

`KryptonManager.GetDpiFactor()` must match the host form `DeviceDpi / 96`, and `ScaleValueByDpi(16)` must match that factor. Those checks fail the run even when size misses are listed in the baseline.

```cmd
Source\TestHarnesses\AccessibilityAudit\bin\Debug\net10.0-windows\AccessibilityAudit.exe --suite dpi --baseline Scripts\DpiAudit\baseline.json --report dpi-report.md
Source\TestHarnesses\AccessibilityAudit\bin\Debug\net10.0-windows\AccessibilityAudit.exe --suite dpi --baseline Scripts\DpiAudit\baseline.json --report dpi-report.md --update-baseline
```

## Touchscreen audit

The runner's lack of a digitizer is not a failure. `IsTouchscreenAvailable()` must match `GetSystemMetrics(SM_DIGITIZER)` with the ready bit set, and `GetMaximumTouchContacts()` must not be negative. With automatic detection on, `UseTouchscreenSupport` follows that detection result. Turning detection off must not leave the timer running in a way that throws. Setting `UseTouchscreenSupport` raises `GlobalTouchscreenSupportChanged`. With support on and the scale set to 1.5, `TouchscreenScaleFactor` is 1.5 and `GetCombinedScaleFactorX()` is the DPI factor times 1.5. Turning support off returns the touch factor to 1.

Per control, font scaling is off so preferred-size measurement applies the control scale. `GetPreferredSize(Size.Empty)` is measured with touch off and again with touch on at 1.5. If both sizes are non-empty and the touch size is below 1.2x on either axis, the key is `touch-size-unchanged`. Empty preferred sizes are skipped. Construction throws use `construct-threw`.

```cmd
Source\TestHarnesses\AccessibilityAudit\bin\Debug\net10.0-windows\AccessibilityAudit.exe --suite touch --baseline Scripts\TouchAudit\baseline.json --report touch-report.md
Source\TestHarnesses\AccessibilityAudit\bin\Debug\net10.0-windows\AccessibilityAudit.exe --suite touch --baseline Scripts\TouchAudit\baseline.json --report touch-report.md --update-baseline
```
