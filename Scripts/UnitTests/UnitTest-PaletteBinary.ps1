<#
.SYNOPSIS
    Asserts #2117 KryptonCustomPaletteBase .kpalx XML save/load.

.DESCRIPTION
    Loads Debug Krypton.Toolkit and round-trips a distinctive colour through
    .kpalx XML, legacy XML, compressed-XML .kpal, and native binary .kpal.
    Also checks KPLT magic, FormatFromPath, PaletteCornerRounding persist, Convert,
    UpgradeXmlToKpalx / ConvertFile (file and KryptonCustomPaletteBase), UpgradeXmlToKpalxFromDirectory, packs, directory packs, JSON rejection, and Utilities FromDirectory scan.

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
Assert-True ([Krypton.Toolkit.KryptonPaletteFile]::IsLegacyXmlExtension('theme.xml')) 'IsLegacyXmlExtension(.xml)'
Assert-True ([Krypton.Toolkit.KryptonPaletteFile]::IsLegacyXmlExtension('xml')) 'IsLegacyXmlExtension(xml)'
Assert-True (-not [Krypton.Toolkit.KryptonPaletteFile]::IsLegacyXmlExtension('theme.kpalx')) 'IsLegacyXmlExtension(.kpalx) is false'
$silentPromptKpalx = [Krypton.Toolkit.KryptonPaletteFile]::PromptLegacyXmlUpgrade('theme.kpalx', $true)
Assert-True ($silentPromptKpalx -eq 'theme.kpalx') 'PromptLegacyXmlUpgrade silent leaves .kpalx unchanged'
$themeStrings = [Krypton.Toolkit.KryptonManager]::Strings.MiscellaneousThemeStrings
Assert-True ($themeStrings.LegacyXmlUpgradeTitle.Length -gt 0) 'LegacyXmlUpgradeTitle has a default'
Assert-True ($themeStrings.LegacyXmlUpgradeMessage.Contains('{0}')) 'LegacyXmlUpgradeMessage includes the file-name placeholder'
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

    $legacyXml = Join-Path $temp 'legacy-upgrade.xml'
    Copy-Item -LiteralPath $xmlPath -Destination $legacyXml
    $upgradedPath = [Krypton.Toolkit.KryptonPaletteFile]::UpgradeXmlToKpalx($legacyXml)
    Assert-True (Test-Path -LiteralPath $upgradedPath) 'UpgradeXmlToKpalx created a .kpalx beside the source'
    Assert-True ($upgradedPath.EndsWith('.kpalx')) 'UpgradeXmlToKpalx destination uses .kpalx'
    Assert-True (Test-Path -LiteralPath $legacyXml) 'UpgradeXmlToKpalx leaves the source .xml in place'
    $silentPromptXml = [Krypton.Toolkit.KryptonPaletteFile]::PromptLegacyXmlUpgrade($legacyXml, $true)
    Assert-True ($silentPromptXml -eq $legacyXml) 'PromptLegacyXmlUpgrade silent returns the .xml path without rewriting'
    Assert-True ((Get-Magic $upgradedPath) -ne 'KPLT') 'UpgradeXmlToKpalx writes XML, not a KPLT container'
    $fromUpgraded = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    [void]$fromUpgraded.Import($upgradedPath, $true)
    Assert-True ((Format-Color $fromUpgraded.ColorTable.StatusStripGradientBegin) -eq (Format-Color $marker)) 'UpgradeXmlToKpalx restores StatusStripGradientBegin'
    Assert-True ($fromUpgraded.GetPaletteName() -eq '2117-roundtrip') 'UpgradeXmlToKpalx restores the palette name'
    $fromUpgraded.Dispose()

    function Get-PaletteXmlVersion([string]$Path) {
        $text = [System.IO.File]::ReadAllText($Path)
        $m = [regex]::Match($text, 'KryptonPalette\s+[^>]*Version="(\d+)"')
        if (-not $m.Success) {
            $m = [regex]::Match($text, "KryptonPalette\s+[^>]*Version='(\d+)'")
        }
        if ($m.Success) { return [int]$m.Groups[1].Value }
        return 0
    }

    $currentSchema = [Krypton.Interop.SharedStaticConstants]::CURRENT_SUPPORTED_PALETTE_VERSION
    Assert-True ((Get-PaletteXmlVersion $xmlPath) -eq $currentSchema) 'Fresh XML export uses the current schema version'

    $schema21Xml = Join-Path $temp 'schema-21.xml'
    $schema21Text = [System.IO.File]::ReadAllText($xmlPath) -replace "Version=`"$currentSchema`"", 'Version="21"' -replace "Version='$currentSchema'", "Version='21'"
    [System.IO.File]::WriteAllText($schema21Xml, $schema21Text)
    Assert-True ((Get-PaletteXmlVersion $schema21Xml) -eq 21) 'Downgraded test file reports schema 21'

    $schema21ImportThrew = $false
    try {
        $directV21 = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
        [void]$directV21.Import($schema21Xml, $true)
        $directV21.Dispose()
    }
    catch {
        $schema21ImportThrew = $_.Exception.GetBaseException().Message -match 'number is incompatible'
    }
    Assert-True $schema21ImportThrew 'Silent Import of schema 21 throws without upgrading'

    $schema21Kpalx = [Krypton.Toolkit.KryptonPaletteFile]::UpgradeXmlToKpalx($schema21Xml)
    Assert-True ((Get-PaletteXmlVersion $schema21Kpalx) -eq $currentSchema) 'UpgradeXmlToKpalx raises schema 21 to the current version'
    $fromSchema21 = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    [void]$fromSchema21.Import($schema21Kpalx, $true)
    Assert-True ((Format-Color $fromSchema21.ColorTable.StatusStripGradientBegin) -eq (Format-Color $marker)) 'UpgradeXmlToKpalx of schema 21 restores StatusStripGradientBegin'
    Assert-True ($fromSchema21.GetPaletteName() -eq '2117-roundtrip') 'UpgradeXmlToKpalx of schema 21 restores the palette name'
    $fromSchema21.Dispose()

    $schema21Stream = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    $fs21 = [System.IO.File]::OpenRead($schema21Xml)
    try {
        $schema21Stream.ImportWithUpgrade($fs21)
    }
    finally {
        $fs21.Dispose()
    }
    Assert-True ((Format-Color $schema21Stream.ColorTable.StatusStripGradientBegin) -eq (Format-Color $marker)) 'ImportWithUpgrade of schema 21 XML restores StatusStripGradientBegin'
    $schema21Stream.Dispose()

    $schema20Xml = Join-Path $temp 'schema-20.xml'
    $schema20Text = [System.IO.File]::ReadAllText($xmlPath) -replace "Version=`"$currentSchema`"", 'Version="20"' -replace "Version='$currentSchema'", "Version='20'"
    [System.IO.File]::WriteAllText($schema20Xml, $schema20Text)
    $schema20Kpalx = [Krypton.Toolkit.KryptonPaletteFile]::Convert($schema20Xml, (Join-Path $temp 'schema-20.kpalx'))
    Assert-True ((Get-PaletteXmlVersion $schema20Kpalx) -eq $currentSchema) 'Convert raises schema 20 to the current version'
    $fromSchema20 = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    [void]$fromSchema20.Import($schema20Kpalx, $true)
    Assert-True ((Format-Color $fromSchema20.ColorTable.StatusStripGradientBegin) -eq (Format-Color $marker)) 'Convert of schema 20 restores StatusStripGradientBegin'
    $fromSchema20.Dispose()

    $bulkRoot = Join-Path $temp 'bulk-xml'
    $bulkNested = Join-Path $bulkRoot 'nested'
    [void][System.IO.Directory]::CreateDirectory($bulkNested)
    Copy-Item -LiteralPath $xmlPath -Destination (Join-Path $bulkRoot 'root.xml')
    Copy-Item -LiteralPath $xmlPath -Destination (Join-Path $bulkNested 'child.xml')
    [System.IO.File]::WriteAllText((Join-Path $bulkRoot 'not-a-palette.xml'), '<root>not a palette</root>')
    $bulk = [Krypton.Toolkit.KryptonPaletteFile]::UpgradeXmlToKpalxFromDirectory($bulkRoot)
    Assert-True ($bulk.ConvertedCount -eq 2) 'UpgradeXmlToKpalxFromDirectory converts two palettes including nested'
    Assert-True ($bulk.SkippedCount -eq 1) 'UpgradeXmlToKpalxFromDirectory skips non-palette XML'
    Assert-True ($bulk.ErrorCount -eq 0) 'UpgradeXmlToKpalxFromDirectory has no errors for valid palettes'
    Assert-True (Test-Path -LiteralPath (Join-Path $bulkRoot 'root.kpalx')) 'Bulk convert wrote root.kpalx beside the source'
    Assert-True (Test-Path -LiteralPath (Join-Path $bulkNested 'child.kpalx')) 'Bulk convert wrote nested child.kpalx'
    Assert-True (Test-Path -LiteralPath (Join-Path $bulkRoot 'root.xml')) 'Bulk convert leaves source XML in place'
    $topOnly = [Krypton.Toolkit.KryptonPaletteFile]::UpgradeXmlToKpalxFromDirectory($bulkRoot, $false)
    Assert-True ($topOnly.ConvertedCount -eq 1) 'UpgradeXmlToKpalxFromDirectory without subdirs converts only the top folder'
    Assert-True ($topOnly.SkippedCount -eq 1) 'Top-only bulk still skips non-palette XML'
    $missingDirThrew = $false
    try {
        [void][Krypton.Toolkit.KryptonPaletteFile]::UpgradeXmlToKpalxFromDirectory((Join-Path $temp 'does-not-exist'))
    }
    catch {
        $missingDirThrew = $_.Exception.GetBaseException().Message -match 'does not exist'
    }
    Assert-True $missingDirThrew 'UpgradeXmlToKpalxFromDirectory throws when the folder is missing'
    $instanceBulk = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    $instanceBulkResult = $instanceBulk.UpgradeXmlToKpalxFromDirectory($bulkRoot, $true, $true)
    Assert-True ($instanceBulkResult.ConvertedCount -eq 2) 'KryptonCustomPaletteBase.UpgradeXmlToKpalxFromDirectory converts the folder'
    $instanceBulk.Dispose()

    $upgradeRejectedKpalx = $false
    try {
        [void][Krypton.Toolkit.KryptonPaletteFile]::UpgradeXmlToKpalx($kpalxPath)
    }
    catch {
        $upgradeRejectedKpalx = $_.Exception.GetBaseException().Message -match 'legacy'
    }
    Assert-True $upgradeRejectedKpalx 'UpgradeXmlToKpalx rejects a .kpalx source'

    $upgradeRejectedDest = $false
    try {
        [void][Krypton.Toolkit.KryptonPaletteFile]::UpgradeXmlToKpalx($legacyXml, $binaryPath)
    }
    catch {
        $upgradeRejectedDest = $_.Exception.GetBaseException().Message -match '\.kpalx'
    }
    Assert-True $upgradeRejectedDest 'UpgradeXmlToKpalx rejects a non-.kpalx destination'

    $legacyXmlInstance = Join-Path $temp 'legacy-upgrade-instance.xml'
    Copy-Item -LiteralPath $xmlPath -Destination $legacyXmlInstance
    $instanceUpgrade = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    $instanceUpgradePath = $instanceUpgrade.UpgradeXmlToKpalx($legacyXmlInstance)
    Assert-True (Test-Path -LiteralPath $instanceUpgradePath) 'KryptonCustomPaletteBase.UpgradeXmlToKpalx created a .kpalx'
    Assert-True ((Format-Color $instanceUpgrade.ColorTable.StatusStripGradientBegin) -eq (Format-Color $marker)) 'KryptonCustomPaletteBase.UpgradeXmlToKpalx imports StatusStripGradientBegin'
    Assert-True ($instanceUpgrade.GetPaletteName() -eq '2117-roundtrip') 'KryptonCustomPaletteBase.UpgradeXmlToKpalx imports the palette name'
    $instanceUpgrade.Dispose()

    $convertedInstancePath = Join-Path $temp 'converted-instance.kpalx'
    $instanceConvert = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    [void]$instanceConvert.ConvertFile($xmlPath, $convertedInstancePath)
    Assert-True (Test-Path -LiteralPath $convertedInstancePath) 'KryptonCustomPaletteBase.ConvertFile created a .kpalx'
    Assert-True ((Format-Color $instanceConvert.ColorTable.StatusStripGradientBegin) -eq (Format-Color $marker)) 'KryptonCustomPaletteBase.ConvertFile imports StatusStripGradientBegin'
    $instanceConvert.Dispose()

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

    Assert-True ([Krypton.Toolkit.KryptonPaletteFile]::GetPackName($packPath) -eq '2117-pack') 'GetPackName returns the pack header name'
    Assert-True ([Krypton.Toolkit.KryptonPaletteFile]::GetPackName($binaryPath) -eq '') 'GetPackName is empty for a single-theme .kpal'
    [void][Krypton.Toolkit.KryptonPaletteFile]::SetPackName($packPath, '2117-pack-renamed')
    Assert-True ([Krypton.Toolkit.KryptonPaletteFile]::GetPackName($packPath) -eq '2117-pack-renamed') 'SetPackName rewrites the pack header name'
    [void][Krypton.Toolkit.KryptonPaletteFile]::SetPackName($packPath, '2117-pack')

    $violet = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    $violet.SetPaletteName('Pack-Violet')
    $violet.ToolMenuStatus.StatusStrip.StatusStripGradientBegin = [System.Drawing.Color]::BlueViolet
    $violetPath = Join-Path $temp 'Pack-Violet.kpalx'
    [void]$violet.Export($violetPath, $true, $true)
    $editPack = Join-Path $temp 'edit.kpal'
    Copy-Item -LiteralPath $packPath -Destination $editPack
    [void][Krypton.Toolkit.KryptonPaletteFile]::AddToPack($editPack, $violetPath)
    $afterAdd = [Krypton.Toolkit.KryptonPaletteFile]::GetThemeNames($editPack)
    Assert-True ($afterAdd.Length -eq 3) 'AddToPack adds a .kpalx theme'
    Assert-True ($afterAdd -contains 'Pack-Violet') 'AddToPack stores Pack-Violet'
    $fromAdded = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    [void]$fromAdded.Import($editPack, 'Pack-Violet', $true)
    Assert-True ((Format-Color $fromAdded.ColorTable.StatusStripGradientBegin) -eq (Format-Color ([System.Drawing.Color]::BlueViolet))) 'AddToPack .kpalx payload is restored'
    $dupThrow = $false
    try {
        [void][Krypton.Toolkit.KryptonPaletteFile]::AddToPack($editPack, $violetPath)
    }
    catch {
        $dupThrow = $_.Exception.GetBaseException().Message -match 'Duplicate'
    }
    Assert-True $dupThrow 'AddToPack throws on duplicate name unless replaceExisting'
    [void][Krypton.Toolkit.KryptonPaletteFile]::AddToPack($editPack, $violet, $true)
    Assert-True (([Krypton.Toolkit.KryptonPaletteFile]::GetThemeNames($editPack)).Length -eq 3) 'AddToPack replaceExisting keeps count'
    [void][Krypton.Toolkit.KryptonPaletteFile]::RemoveFromPack($editPack, 'Pack-Violet')
    Assert-True (([Krypton.Toolkit.KryptonPaletteFile]::GetThemeNames($editPack)).Length -eq 2) 'RemoveFromPack drops Pack-Violet'
    [void][Krypton.Toolkit.KryptonPaletteFile]::RemoveFromPack($editPack, 'Pack-Lime')
    $lastThrow = $false
    try {
        [void][Krypton.Toolkit.KryptonPaletteFile]::RemoveFromPack($editPack, 'Pack-Orange')
    }
    catch {
        $lastThrow = $_.Exception.GetBaseException().Message -match 'cannot be empty'
    }
    Assert-True $lastThrow 'RemoveFromPack throws when removing the last theme'
    Assert-True (([Krypton.Toolkit.KryptonPaletteFile]::GetThemeNames($editPack)).Length -eq 1) 'The last theme remains in the pack'
    Assert-True (Test-Path -LiteralPath $editPack) 'RemoveFromPack does not delete the pack file'

    $promotePath = Join-Path $temp 'promote.kpal'
    Copy-Item -LiteralPath $binaryPath -Destination $promotePath
    Assert-True (-not [Krypton.Toolkit.KryptonPaletteFile]::IsPack($promotePath)) 'Copied single-theme .kpal is not a pack'
    [void][Krypton.Toolkit.KryptonPaletteFile]::AddToPack($promotePath, $violetPath)
    Assert-True ([Krypton.Toolkit.KryptonPaletteFile]::IsPack($promotePath)) 'AddToPack promotes a single-theme .kpal to a pack'
    Assert-True (([Krypton.Toolkit.KryptonPaletteFile]::GetThemeNames($promotePath)).Length -eq 2) 'Promoted pack has the original theme plus the added .kpalx'

    $bogusXml = Join-Path $temp 'notes.xml'
    Set-Content -LiteralPath $bogusXml -Value '<root>not a palette</root>'
    $nonPaletteThrow = $false
    try {
        [void][Krypton.Toolkit.KryptonPaletteFile]::AddToPack($packPath, $bogusXml)
    }
    catch {
        $nonPaletteThrow = $true
    }
    Assert-True $nonPaletteThrow 'AddToPack rejects non-palette XML'
    Assert-True (([Krypton.Toolkit.KryptonPaletteFile]::GetThemeNames($packPath)).Length -eq 2) 'Failed AddToPack leaves the original pack unchanged'
    $fromAdded.Dispose()
    $violet.Dispose()

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
