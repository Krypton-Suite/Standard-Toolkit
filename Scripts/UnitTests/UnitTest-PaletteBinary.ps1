<#
.SYNOPSIS
    Asserts #2117 KryptonCustomPaletteBase .kpalx XML save/load.

.DESCRIPTION
    Loads Debug Krypton.Toolkit and round-trips a distinctive colour through
    .kpalx XML, legacy XML, compressed-XML .kpal, and native binary .kpal.
    Also checks KPLT magic, FormatFromPath, PaletteCornerRounding persist, Convert XML to .kpalx / .kpal, JSON rejection, and that .kpalx is XML (not KPLT).

    Exit code 0 on success; non-zero on failure.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-PaletteBinary.ps1
#>
# UnitTest-CI: include
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
Register-UnitTestAssemblyResolver -BinDir $bin

Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Interop.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$themesDll = Join-Path $bin 'Krypton.Themes.dll'
if (Test-Path -LiteralPath $themesDll) {
    [void][System.Reflection.Assembly]::LoadFrom($themesDll)
}

$failed = New-Object System.Collections.Generic.List[string]

function Assert-True {
    param([bool]$Condition, [string]$Message)
    if (-not $Condition) {
        $failed.Add($Message)
        Write-Host "FAIL: $Message" -ForegroundColor Red
    }
    else {
        Write-Host "PASS: $Message" -ForegroundColor Green
    }
}

function Format-Color([System.Drawing.Color]$Color) {
    return ('#{0:X2}{1:X2}{2:X2}' -f $Color.R, $Color.G, $Color.B)
}

function Get-Magic([string]$Path) {
    $bytes = [System.IO.File]::ReadAllBytes($Path)
    if ($bytes.Length -lt 4) {
        return ''
    }
    return [System.Text.Encoding]::ASCII.GetString($bytes, 0, 4)
}

Write-UnitTestBanner -Status INFO -Message 'Asserting #2117 .kpalx XML save/load'

Assert-True ([Krypton.Toolkit.KryptonPaletteFile]::FormatFromPath('theme.kpalx') -eq [Krypton.Toolkit.KryptonPaletteFileFormat]::Xml) 'FormatFromPath(.kpalx) is Xml'
Assert-True ([Krypton.Toolkit.KryptonPaletteFile]::FormatFromPath('theme.kpal') -eq [Krypton.Toolkit.KryptonPaletteFileFormat]::PaletteBinary) 'FormatFromPath(.kpal) is PaletteBinary'
Assert-True ([Krypton.Toolkit.KryptonPaletteFile]::FormatFromPath('theme.xml') -eq [Krypton.Toolkit.KryptonPaletteFileFormat]::Xml) 'FormatFromPath(.xml) is Xml'
Assert-True ([Krypton.Toolkit.KryptonPaletteFile]::DialogFilter.StartsWith('Krypton palette files (*.kpalx)')) 'Dialog filter lists .kpalx first'
Assert-True ([Krypton.Toolkit.KryptonPaletteFile]::IsPaletteExtension('theme.kpalx')) 'IsPaletteExtension(.kpalx)'
Assert-True ([Krypton.Toolkit.KryptonPaletteFile]::IsPaletteExtension('.kpal')) 'IsPaletteExtension(.kpal)'
Assert-True (-not [Krypton.Toolkit.KryptonPaletteFile]::IsPaletteExtension('theme.xml')) 'IsPaletteExtension(.xml) is false'
$shellIcon = [Krypton.Toolkit.KryptonPaletteFile]::CreateShellIcon($false)
Assert-True ($null -ne $shellIcon) 'CreateShellIcon returns the Stable Kr tile'
if ($shellIcon) { $shellIcon.Dispose() }

$marker = [System.Drawing.Color]::Lime
$source = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
$source.SetPaletteName('2117-roundtrip')
$source.ToolMenuStatus.StatusStrip.StatusStripGradientBegin = $marker
$corners = [Krypton.Toolkit.PaletteCornerRounding]::new([float]2, [float]3, [float]4, [float]5)
$source.Common.StateCommon.Border.CornerRounding = $corners

$temp = Join-Path ([System.IO.Path]::GetTempPath()) ('krypton-2117-' + [Guid]::NewGuid().ToString('N'))
[void][System.IO.Directory]::CreateDirectory($temp)
$xmlPath = Join-Path $temp 'roundtrip.xml'
$kpalxPath = Join-Path $temp 'roundtrip.kpalx'
$compressedPath = Join-Path $temp 'roundtrip-xml.kpal'
$binaryPath = Join-Path $temp 'roundtrip.kpal'

try {
    [void]$source.Export($xmlPath, $true, $true, [Krypton.Toolkit.KryptonPaletteFileFormat]::Xml)
    [void]$source.Export($kpalxPath, $true, $true)
    [void]$source.Export($compressedPath, $true, $true, [Krypton.Toolkit.KryptonPaletteFileFormat]::PaletteCompressedXml)
    [void]$source.Export($binaryPath, $true, $true)
    $fullPath = Join-Path $temp 'full-defaults.kpalx'
    [void]$source.Export($fullPath, $false, $true)

    Assert-True (Test-Path -LiteralPath $xmlPath) 'XML export created a file'
    Assert-True (Test-Path -LiteralPath $kpalxPath) 'Path-based .kpalx export created a file'
    Assert-True (Test-Path -LiteralPath $compressedPath) 'Compressed-XML .kpal export created a file'
    Assert-True (Test-Path -LiteralPath $binaryPath) 'Path-based native .kpal export created a file'
    Assert-True (Test-Path -LiteralPath $fullPath) 'Export with ignoreDefaults false succeeds (PaletteCornerRounding persist)'

    $xmlLen = (Get-Item -LiteralPath $xmlPath).Length
    $kpalxLen = (Get-Item -LiteralPath $kpalxPath).Length
    $compressedLen = (Get-Item -LiteralPath $compressedPath).Length
    $binaryLen = (Get-Item -LiteralPath $binaryPath).Length
    Assert-True ($xmlLen -gt 0) 'XML file is not empty'
    Assert-True ($kpalxLen -gt 0) '.kpalx file is not empty'
    Assert-True ($compressedLen -gt 0) 'Compressed-XML .kpal is not empty'
    Assert-True ($binaryLen -gt 0) 'Native .kpal is not empty'
    Assert-True ($compressedLen -lt $xmlLen) 'Compressed-XML .kpal is smaller than XML'
    Assert-True ((Get-Magic $binaryPath) -eq 'KPLT') 'Native .kpal starts with KPLT'
    Assert-True ((Get-Magic $compressedPath) -eq 'KPLT') 'Compressed-XML .kpal starts with KPLT'
    Assert-True ((Get-Magic $xmlPath) -ne 'KPLT') 'XML export is not a KPLT container'
    Assert-True ((Get-Magic $kpalxPath) -ne 'KPLT') '.kpalx export is XML, not a KPLT container'

    $fromXml = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    $fromKpalx = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    $fromCompressed = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    $fromBinary = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    [void]$fromXml.Import($xmlPath, $true)
    [void]$fromKpalx.Import($kpalxPath, $true)
    [void]$fromCompressed.Import($compressedPath, $true)
    [void]$fromBinary.Import($binaryPath, $true)

    Assert-True ((Format-Color $fromXml.ColorTable.StatusStripGradientBegin) -eq (Format-Color $marker)) 'XML import restores StatusStripGradientBegin'
    Assert-True ((Format-Color $fromKpalx.ColorTable.StatusStripGradientBegin) -eq (Format-Color $marker)) '.kpalx import restores StatusStripGradientBegin'
    Assert-True ((Format-Color $fromCompressed.ColorTable.StatusStripGradientBegin) -eq (Format-Color $marker)) 'Compressed-XML .kpal import restores StatusStripGradientBegin'
    Assert-True ((Format-Color $fromBinary.ColorTable.StatusStripGradientBegin) -eq (Format-Color $marker)) 'Native .kpal import restores StatusStripGradientBegin'
    Assert-True ($fromBinary.GetPaletteName() -eq '2117-roundtrip') 'Native container restores the palette name'
    Assert-True ($fromXml.GetPaletteName() -eq '2117-roundtrip') 'XML import restores the palette name'
    Assert-True ($fromKpalx.GetPaletteName() -eq '2117-roundtrip') '.kpalx restores the palette name'
    $importedCorners = $fromKpalx.Common.StateCommon.Border.CornerRounding
    Assert-True (($importedCorners.TopLeft -eq 2) -and ($importedCorners.TopRight -eq 3) -and ($importedCorners.BottomRight -eq 4) -and ($importedCorners.BottomLeft -eq 5)) '.kpalx restores PaletteCornerRounding'

    $convertedKpalx = Join-Path $temp 'converted.kpalx'
    $convertedKpal = Join-Path $temp 'converted.kpal'
    $convertedPath = [Krypton.Toolkit.KryptonPaletteFile]::Convert($xmlPath, $convertedKpalx)
    [void][Krypton.Toolkit.KryptonPaletteFile]::Convert($xmlPath, $convertedKpal, [Krypton.Toolkit.KryptonPaletteFileFormat]::PaletteBinary)
    Assert-True (Test-Path -LiteralPath $convertedPath) 'Convert XML → .kpalx created a file'
    Assert-True ((Get-Magic $convertedKpalx) -ne 'KPLT') 'Convert XML → .kpalx writes XML'
    Assert-True ((Get-Magic $convertedKpal) -eq 'KPLT') 'Convert XML → .kpal writes a KPLT container'
    $fromConverted = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    [void]$fromConverted.Import($convertedKpalx, $true)
    Assert-True ((Format-Color $fromConverted.ColorTable.StatusStripGradientBegin) -eq (Format-Color $marker)) 'Convert XML → .kpalx restores StatusStripGradientBegin'
    Assert-True ($fromConverted.GetPaletteName() -eq '2117-roundtrip') 'Convert XML → .kpalx restores the palette name'
    $fromConverted.Dispose()

    $jsonRejected = $false
    try {
        [void][Krypton.Toolkit.KryptonPaletteFile]::Convert((Join-Path $temp 'theme.json'), $convertedKpalx)
    }
    catch {
        $jsonRejected = $_.Exception.GetBaseException().Message -match 'JSON'
    }
    Assert-True $jsonRejected 'Convert rejects JSON as a palette format'

    $macOs = [Krypton.Toolkit.PaletteMode]::MacOSLight
    Assert-True ([Krypton.Toolkit.KryptonThemeCatalog]::IsImplementationAvailable($macOs)) 'Krypton.Themes extra palette MacOSLight is catalogued'
    $extra = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    $extra.BasePaletteMode = $macOs
    $extra.SetPaletteName('macos-extra')
    $extra.ToolMenuStatus.StatusStrip.StatusStripGradientBegin = $marker
    $extraPath = Join-Path $temp 'macos-extra.kpalx'
    [void]$extra.Export($extraPath, $true, $true)
    Assert-True ((Get-Magic $extraPath) -ne 'KPLT') 'Extra-theme .kpalx is XML, not a KPLT container'
    $fromExtra = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    [void]$fromExtra.Import($extraPath, $true)
    Assert-True ((Format-Color $fromExtra.ColorTable.StatusStripGradientBegin) -eq (Format-Color $marker)) 'Themes extra-mode .kpalx import restores StatusStripGradientBegin'
    $extra.Dispose()
    $fromExtra.Dispose()

    $fromXml.Dispose()
    $fromKpalx.Dispose()
    $fromCompressed.Dispose()
    $fromBinary.Dispose()
}
finally {
    $source.Dispose()
    if (Test-Path -LiteralPath $temp) {
        Remove-Item -LiteralPath $temp -Recurse -Force
    }
}

if ($failed.Count -gt 0) {
    Write-Host ("{0} assertion(s) failed." -f $failed.Count) -ForegroundColor Red
    exit 1
}

Write-Host 'All #2117 .kpalx XML save/load assertions passed.' -ForegroundColor Green
exit 0
