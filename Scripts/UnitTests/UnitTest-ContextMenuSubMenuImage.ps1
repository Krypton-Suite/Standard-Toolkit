<#
.SYNOPSIS
    Asserts #4252 Light Gray palettes return a context-menu submenu image without throwing.

.DESCRIPTION
    Loads Debug Krypton.Interop + Krypton.Toolkit + Krypton.Themes and calls
    GetContextMenuSubMenuImage on every catalog palette. Office 2007 / 2010 /
    Microsoft 365 Light Gray must return a non-null image (those modes used to
    throw NotImplementedException).

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-ContextMenuSubMenuImage.ps1
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

Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms

[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Interop.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Themes.dll'))
[Krypton.Toolkit.KryptonThemeCatalog]::DiscoverThemes()

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

$lightGrayModes = @(
    [Krypton.Toolkit.PaletteMode]::Office2007LightGray,
    [Krypton.Toolkit.PaletteMode]::Office2010LightGray,
    [Krypton.Toolkit.PaletteMode]::Microsoft365LightGray
)

foreach ($mode in $lightGrayModes) {
    $palette = $null
    $image = $null
    $threw = $false
    $errorText = $null
    try {
        $palette = [Krypton.Toolkit.KryptonManager]::GetPaletteForMode($mode)
        $image = $palette.GetContextMenuSubMenuImage()
    }
    catch {
        $threw = $true
        $errorText = $_.Exception.GetType().FullName + ': ' + $_.Exception.Message
    }

    Assert-True (-not $threw) "$mode GetContextMenuSubMenuImage does not throw$(if ($errorText) { " ($errorText)" })"
    Assert-True ($null -ne $palette) "$mode GetPaletteForMode returns a palette"
    Assert-True ($null -ne $image) "$mode GetContextMenuSubMenuImage returns an image"
}

$allModes = @([enum]::GetValues([Krypton.Toolkit.PaletteMode]))
foreach ($mode in $allModes) {
    if ($mode -eq [Krypton.Toolkit.PaletteMode]::Global -or $mode -eq [Krypton.Toolkit.PaletteMode]::Custom) {
        continue
    }

    try {
        $palette = [Krypton.Toolkit.KryptonManager]::GetPaletteForMode($mode)
        $null = $palette.GetContextMenuSubMenuImage()
    }
    catch {
        Assert-True $false "$mode GetContextMenuSubMenuImage threw $($_.Exception.GetType().Name): $($_.Exception.Message)"
    }
}

if ($failed.Count -gt 0) {
    Write-Error ("Context-menu submenu image checks failed:`n" + ($failed -join "`n"))
    exit 1
}

Write-Host 'UnitTest-ContextMenuSubMenuImage passed.' -ForegroundColor Green
exit 0
