<#
.SYNOPSIS
    Asserts #2117 KryptonCustomPaletteBase .kpalx XML save/load.

.DESCRIPTION
    Loads Debug Krypton.Toolkit and round-trips a distinctive colour through
    .kpalx XML, legacy XML, compressed-XML .kpal, and native binary .kpal.
    Also checks KPLT magic, FormatFromPath, PaletteCornerRounding persist, Convert, packs, directory packs, JSON rejection, and Utilities FromDirectory scan.

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
$utilitiesDll = Join-Path $bin 'Krypton.Toolkit.Utilities.dll'
if (Test-Path -LiteralPath $utilitiesDll) {
    [void][System.Reflection.Assembly]::LoadFrom($utilitiesDll)
}
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

    $orange = [System.Drawing.Color]::Orange
    $packLime = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    $packLime.SetPaletteName('Pack-Lime')
    $packLime.ToolMenuStatus.StatusStrip.StatusStripGradientBegin = $marker
    $packOrange = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    $packOrange.SetPaletteName('Pack-Orange')
    $packOrange.ToolMenuStatus.StatusStrip.StatusStripGradientBegin = $orange
    $packList = New-Object 'System.Collections.Generic.List[Krypton.Toolkit.KryptonCustomPaletteBase]'
    [void]$packList.Add($packLime)
    [void]$packList.Add($packOrange)
    $packPath = Join-Path $temp 'themes.kpal'
    [void][Krypton.Toolkit.KryptonPaletteFile]::ExportPack($packPath, $packList, $true, '2117-pack')
    Assert-True ((Get-Magic $packPath) -eq 'KPLT') 'Pack .kpal starts with KPLT'
    Assert-True ([Krypton.Toolkit.KryptonPaletteFile]::IsPack($packPath)) 'IsPack is true for a multi-theme .kpal'
    Assert-True (-not [Krypton.Toolkit.KryptonPaletteFile]::IsPack($binaryPath)) 'IsPack is false for a single-theme .kpal'
    $packNames = [Krypton.Toolkit.KryptonPaletteFile]::GetThemeNames($packPath)
    Assert-True ($packNames.Length -eq 2) 'GetThemeNames returns two pack themes'
    Assert-True (($packNames[0] -eq 'Pack-Lime') -and ($packNames[1] -eq 'Pack-Orange')) 'GetThemeNames preserves pack order'
    $fromPack = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    [void]$fromPack.Import($packPath, 'Pack-Orange', $true)
    Assert-True ((Format-Color $fromPack.ColorTable.StatusStripGradientBegin) -eq (Format-Color $orange)) 'Import named pack theme restores Pack-Orange'
    Assert-True ($fromPack.GetPaletteName() -eq 'Pack-Orange') 'Import named pack theme sets the palette name'
    $packUnnamedThrow = $false
    try {
        [void]$fromPack.Import($packPath, $true)
    }
    catch {
        $packUnnamedThrow = $_.Exception.GetBaseException().Message -match 'multiple themes'
    }
    Assert-True $packUnnamedThrow 'Import without a name throws for a multi-theme .kpal'
    $packXmlRejected = $false
    try {
        [void][Krypton.Toolkit.KryptonPaletteFile]::ExportPack((Join-Path $temp 'themes.kpalx'), $packList)
    }
    catch {
        $packXmlRejected = $_.Exception.GetBaseException().Message -match '\.kpal'
    }
    Assert-True $packXmlRejected 'ExportPack rejects a .kpalx destination'

    $emptyThumbs = [Krypton.Toolkit.KryptonPaletteFile]::GetThemeThumbnails($packPath)
    Assert-True ($emptyThumbs.Length -eq 2) 'GetThemeThumbnails matches GetThemeNames length when no catalog is present'
    Assert-True (($null -eq $emptyThumbs[0]) -and ($null -eq $emptyThumbs[1])) 'GetThemeThumbnails is empty when palettes have no Thumbnail'

    $thumb = New-Object System.Drawing.Bitmap 8,8
    $thumb.SetPixel(0, 0, [System.Drawing.Color]::Red)
    $packLime.Thumbnail = $thumb
    $thumbPack = Join-Path $temp 'thumbs.kpal'
    [void][Krypton.Toolkit.KryptonPaletteFile]::ExportPack($thumbPack, $packList, $true, '2117-thumbs')
    Assert-True ((Get-Magic $thumbPack) -eq 'KPLT') 'Thumbnail pack still starts with KPLT'
    $thumbNames = [Krypton.Toolkit.KryptonPaletteFile]::GetThemeNames($thumbPack)
    Assert-True (($thumbNames[0] -eq 'Pack-Lime') -and ($thumbNames[1] -eq 'Pack-Orange')) 'Thumbnail pack GetThemeNames is unchanged'
    $packThumbs = [Krypton.Toolkit.KryptonPaletteFile]::GetThemeThumbnails($thumbPack)
    Assert-True ($packThumbs.Length -eq 2) 'GetThemeThumbnails returns two slots for a two-theme pack'
    Assert-True (($null -ne $packThumbs[0]) -and ($packThumbs[0].Width -eq 8)) 'Pack catalog returns the Pack-Lime thumbnail'
    Assert-True ($null -eq $packThumbs[1]) 'Pack catalog has no image for a theme without Thumbnail'
    if ($packThumbs[0]) { $packThumbs[0].Dispose() }
    $fromThumb = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    [void]$fromThumb.Import($thumbPack, 'Pack-Lime', $true)
    Assert-True (($null -ne $fromThumb.Thumbnail) -and ($fromThumb.Thumbnail.Width -eq 8)) 'Imported pack theme restores Thumbnail from persist'
    $xmlThumbPath = Join-Path $temp 'thumb.kpalx'
    [void]$packLime.Export($xmlThumbPath, $true, $true)
    $fromXmlThumb = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    [void]$fromXmlThumb.Import($xmlThumbPath, $true)
    Assert-True (($null -ne $fromXmlThumb.Thumbnail) -and ($fromXmlThumb.Thumbnail.Width -eq 8)) '.kpalx import restores Thumbnail'
    $fromThumb.Dispose()
    $fromXmlThumb.Dispose()
    $thumb.Dispose()

    $treeRoot = Join-Path $temp 'Palettes'
    $officeDir = Join-Path $treeRoot 'Office Themes\2013'
    $otherDir = Join-Path $treeRoot 'Other'
    New-Item -ItemType Directory -Path $officeDir -Force | Out-Null
    New-Item -ItemType Directory -Path $otherDir -Force | Out-Null
    $access = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    $access.SetPaletteName('Access 2013')
    $access.ToolMenuStatus.StatusStrip.StatusStripGradientBegin = $marker
    [void]$access.Export((Join-Path $officeDir 'Access 2013.xml'), $true, $true)
    $hazel = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    $hazel.SetPaletteName('Hazel')
    $hazel.ToolMenuStatus.StatusStrip.StatusStripGradientBegin = $orange
    [void]$hazel.Export((Join-Path $otherDir 'Hazel.xml'), $true, $true)
    $dirPack = Join-Path $temp 'palettes.kpal'
    [void][Krypton.Toolkit.KryptonPaletteFile]::ExportPackFromDirectory($dirPack, $treeRoot)
    Assert-True ([Krypton.Toolkit.KryptonPaletteFile]::IsPack($dirPack)) 'ExportPackFromDirectory writes a kind-2 pack'
    Assert-True ([Krypton.Toolkit.KryptonPaletteFile]::IsPackThemePath('Office Themes/2013/Access 2013')) 'IsPackThemePath is true for a / path'
    $dirNames = [Krypton.Toolkit.KryptonPaletteFile]::GetThemeNames($dirPack)
    Assert-True ($dirNames -contains 'Office Themes/2013/Access 2013') 'Directory pack names include Office Themes/2013/Access 2013'
    Assert-True ($dirNames -contains 'Other/Hazel') 'Directory pack names include Other/Hazel'
    $fromDirPack = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    [void]$fromDirPack.Import($dirPack, 'Office Themes/2013/Access 2013', $true)
    Assert-True ((Format-Color $fromDirPack.ColorTable.StatusStripGradientBegin) -eq (Format-Color $marker)) 'Import path-named pack theme restores Access 2013'
    Assert-True ($fromDirPack.GetPaletteName() -eq 'Office Themes/2013/Access 2013') 'Import path-named pack theme sets the palette name'

    $utilitiesLoaded = [bool]([AppDomain]::CurrentDomain.GetAssemblies() | Where-Object { $_.GetName().Name -eq 'Krypton.Toolkit.Utilities' })
    Assert-True $utilitiesLoaded 'Krypton.Toolkit.Utilities is loaded for file-selector scan'
    if ($utilitiesLoaded) {
        $scanned = [Krypton.Toolkit.Utilities.KryptonPaletteFileThemeItem]::FromDirectory($temp)
        Assert-True ($scanned.Length -ge 2) 'FromDirectory finds palette files in the temp folder'
        $packItems = @($scanned | Where-Object { $_.IsPack })
        Assert-True ($packItems.Length -ge 2) 'FromDirectory expands a .kpal pack into named items'
        $scannedTree = [Krypton.Toolkit.Utilities.KryptonPaletteFileThemeItem]::FromDirectory($treeRoot, $true)
        Assert-True ($scannedTree.Length -eq 2) 'FromDirectory with SearchSubdirectories finds nested XML palettes'
        $accessItem = @($scannedTree | Where-Object { $_.TreePath -eq 'Office Themes/2013/Access 2013' })
        Assert-True ($accessItem.Length -eq 1) 'FromDirectory TreePath preserves Office Themes/2013/Access 2013'
        $packOnly = Join-Path $temp 'pack-only'
        New-Item -ItemType Directory -Path $packOnly -Force | Out-Null
        Copy-Item -LiteralPath $dirPack -Destination (Join-Path $packOnly 'palettes.kpal')
        $fromPackScan = [Krypton.Toolkit.Utilities.KryptonPaletteFileThemeItem]::FromDirectory($packOnly)
        $packAccess = @($fromPackScan | Where-Object { $_.ThemeName -eq 'Office Themes/2013/Access 2013' })
        Assert-True ($packAccess.Length -eq 1) 'FromDirectory rebuilds path-named pack themes'
        Assert-True ($packAccess[0].TreePath -eq 'Office Themes/2013/Access 2013') 'Pack theme TreePath matches the stored / path'
    }

    $access.Dispose()
    $hazel.Dispose()
    $fromDirPack.Dispose()

    $packLime.Dispose()
    $packOrange.Dispose()
    $fromPack.Dispose()

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
