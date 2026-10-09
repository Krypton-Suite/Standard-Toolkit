# Unit test scripts

## Dialog DPI layout (`UnitTest-DialogDpiLayout.ps1`)

Issue #4465: fixed chrome becomes sizable, a huge window is clamped to the working area, and a long input prompt grows without clipping the label or collapsing the OK/Cancel gap.

Requires Debug `net472` output (`Bin\Debug\net472\Krypton.Toolkit.dll`).

```cmd
dotnet build "Source\Krypton Components\Krypton.Toolkit\Krypton.Toolkit 2022.csproj" -c Debug -f net472
powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-DialogDpiLayout.ps1
```

`Invoke-DialogDpiScreenshot.ps1` (`exclude`) writes `Documents/PR/4465-dialog-dpi-input.png` and `4465-dialog-dpi-exception.png`.

## Borderless form caption (`UnitTest-BorderlessFormCaption.ps1`)

Issue #2922: after `Show`, a `FormBorderStyle.None` `KryptonForm` must not keep `WS_CAPTION`, and an MDI `Dock.Fill` child must fire `MdiChildActivate` with the MDI client 3D edge already stripped.

Requires Debug `net472` TestForm output.

```cmd
dotnet build "Source\Krypton Components\TestForm\TestForm.csproj" -c Debug -f net472
powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-BorderlessFormCaption.ps1
```

## Designer serialization defaults (`UnitTest-DesignerSerializationDefaults.ps1`)

Fresh Toolbox controls must not show nested `Storage` objects as **Modified** (issue #4325).

Requires Debug `net472` output (`Bin\Debug\net472\Krypton.Toolkit.dll`).

```cmd
dotnet build "Source\Krypton Components\Krypton.Toolkit\Krypton.Toolkit 2022.csproj" -c Debug -f net472
powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-DesignerSerializationDefaults.ps1
```

The script instantiates toolbox controls, walks `TypeDescriptor` content properties, and fails if core drop targets have `IsDefault == false` or unexpected `ShouldSerializeValue == true`.

## ButtonSpec FillHeight (`Probe-ButtonSpecFillHeight.ps1` / `Invoke-ButtonSpecFillHeightScreenshot.ps1`)

Issue #4432: `ButtonSpec.FillHeight` stretches ButtonSpecs to the full host height (default remains centred).

Requires Debug `net472` TestForm output.

```cmd
dotnet build "Source\Krypton Components\TestForm\TestForm.csproj" -c Debug -f net472
powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Probe-ButtonSpecFillHeight.ps1
powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-ButtonSpecFillHeightScreenshot.ps1
## ComboBox GDI+ DrawString screenshot (`Invoke-ComboBoxDrawStringScreenshot.ps1`)

Issue #4424: host `Feature4339ComboBoxSimpleStyleDemo` and write `Documents/PR/4424-combobox-drawstring-v105.png`.

```cmd
dotnet build "Source\Krypton Components\TestForm\TestForm.csproj" -c Debug -f net472
powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-ComboBoxDrawStringScreenshot.ps1
## TextBox InputMode screenshot (`Invoke-TextBoxInputModeScreenshot.ps1`)

Issue #4417: opens `TextBoxInputModeDemo` and writes `Documents/PR/4417-textbox-input-mode-v105-demo.png`.

```cmd
powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-TextBoxInputModeScreenshot.ps1
```

## Disabled caption glyph (`UnitTest-DisabledCaptionGlyph.ps1`)

Issue #4413: disabled Office 2010 / 2013 / Microsoft 365 caption glyphs, with an empty close fill.

```cmd
dotnet build "Source\Krypton Components\TestForm\TestForm.csproj" -c Debug -f net472
powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-DisabledCaptionGlyph.ps1
powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-4413DisabledCaptionScreenshot.ps1
```
