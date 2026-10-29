<#
.SYNOPSIS
    Asserts #4369 KryptonRibbon RibbonTranslations.xml round-trip and Auto Discovery.

.DESCRIPTION
    Loads Debug Krypton.Toolkit / Krypton.Ribbon and runs in-process STA checks:

    1. Export a ribbon with TranslationId keys to XML, mutate captions, import, captions restore.
    2. JSON round-trip of the same overlay.
    3. Auto Discover picks RibbonTranslations.de.xml from a temp directory.

    Requires an STA apartment (use powershell -STA).
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
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Ribbon.dll'))

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

function Assert-Equal {
    param($Expected, $Actual, [string]$Message)
    if (-not [object]::Equals($Expected, $Actual)) {
        $failed.Add("$Message (expected='$Expected' actual='$Actual')")
        Write-Host "FAIL: $Message (expected='$Expected' actual='$Actual')" -ForegroundColor Red
    }
    else {
        Write-Host "PASS: $Message" -ForegroundColor Green
    }
}

function New-TestRibbon {
    $ribbon = New-Object Krypton.Ribbon.KryptonRibbon
    $ribbon.EnableAutoDiscoverTranslations = $false
    $ribbon.TranslationId = 'demoRibbon'
    $tab = New-Object Krypton.Ribbon.KryptonRibbonTab
    $tab.Text = 'Home'
    $tab.TranslationId = 'home'
    $group = New-Object Krypton.Ribbon.KryptonRibbonGroup
    $group.TextLine1 = 'Clipboard'
    $group.TranslationId = 'clipboard'
    $button = New-Object Krypton.Ribbon.KryptonRibbonGroupButton
    $button.TextLine1 = 'Paste'
    $button.TranslationId = 'paste'
    $triple = New-Object Krypton.Ribbon.KryptonRibbonGroupTriple
    [void]$triple.Items.Add($button)
    [void]$group.Items.Add($triple)
    [void]$tab.Groups.Add($group)
    [void]$ribbon.RibbonTabs.Add($tab)
    return @{ Ribbon = $ribbon; Tab = $tab; Group = $group; Button = $button }
}

try {
    [Krypton.Ribbon.KryptonRibbon]::AutoDiscoverTranslations = $false

    $temp = Join-Path $env:TEMP ('KryptonRibbonTranslations-' + [guid]::NewGuid().ToString('N'))
    New-Item -ItemType Directory -Path $temp | Out-Null

    $fixture = New-TestRibbon
    $xmlPath = Join-Path $temp 'RibbonTranslations.xml'
    $options = New-Object Krypton.Ribbon.RibbonTranslationOptions
    $options.IncludeDefaults = $true
    $fixture.Ribbon.ExportTranslationsToXmlFile($xmlPath, $options)
    Assert-True (Test-Path -LiteralPath $xmlPath) 'XML export created a file'

    $xml = [System.IO.File]::ReadAllText($xmlPath)
    Assert-True ($xml.Contains('KryptonRibbonTranslations')) 'XML root is KryptonRibbonTranslations'
    Assert-True ($xml.Contains('TranslationId="home"') -or $xml.Contains('TranslationId="home"')) 'XML contains tab TranslationId'

    $fixture.Tab.Text = 'MUTATED'
    $fixture.Group.TextLine1 = 'MUTATED'
    $fixture.Button.TextLine1 = 'MUTATED'
    $importOptions = New-Object Krypton.Ribbon.RibbonTranslationOptions
    $importOptions.ResetFirst = $true
    $fixture.Ribbon.ImportTranslationsFromXmlFile($xmlPath, $importOptions)
    Assert-Equal 'Home' $fixture.Tab.Text 'XML import restored tab Text'
    Assert-Equal 'Clipboard' $fixture.Group.TextLine1 'XML import restored group TextLine1'
    Assert-Equal 'Paste' $fixture.Button.TextLine1 'XML import restored button TextLine1'

    $jsonPath = Join-Path $temp 'RibbonTranslations.json'
    $fixture.Ribbon.ExportTranslationsToJsonFile($jsonPath, $options)
    $fixture.Tab.Text = 'JSON-MUTATED'
    $fixture.Ribbon.ImportTranslationsFromJsonFile($jsonPath, $importOptions)
    Assert-Equal 'Home' $fixture.Tab.Text 'JSON import restored tab Text'

    $coverage = $fixture.Ribbon.AnalyzeTranslationsFromFile($xmlPath, $options)
    Assert-True ($coverage.Applied.Count -gt 0) 'Analyze reports applied keys'

    $discoverDir = Join-Path $temp 'discover'
    New-Item -ItemType Directory -Path $discoverDir | Out-Null
    $deRibbon = (New-TestRibbon).Ribbon
    $deRibbon.RibbonTabs[0].Text = 'Start'
    $deRibbon.RibbonTabs[0].Groups[0].TextLine1 = 'Zwischenablage'
    $dePath = Join-Path $discoverDir 'RibbonTranslations.de.xml'
    $deRibbon.ExportTranslationsToXmlFile($dePath, $options)

    $probe = New-TestRibbon
    $probe.Ribbon.TranslationsSearchPath = $discoverDir
    $loaded = $probe.Ribbon.TryAutoDiscoverTranslations($discoverDir, (New-Object System.Globalization.CultureInfo 'de'))
    Assert-True $loaded 'Auto Discover found RibbonTranslations.de.xml'
    Assert-Equal 'Start' $probe.Tab.Text 'Auto Discover applied German tab caption'
    Assert-Equal 'Zwischenablage' $probe.Group.TextLine1 'Auto Discover applied German group caption'

    Remove-Item -LiteralPath $temp -Recurse -Force -ErrorAction SilentlyContinue
}
catch {
    $failed.Add($_.Exception.Message)
    Write-Host "FAIL: $($_.Exception.Message)" -ForegroundColor Red
}

if ($failed.Count -gt 0) {
    Write-Host "$($failed.Count) assertion(s) failed." -ForegroundColor Red
    exit 1
}

Write-Host 'All RibbonTranslations assertions passed.' -ForegroundColor Green
exit 0
