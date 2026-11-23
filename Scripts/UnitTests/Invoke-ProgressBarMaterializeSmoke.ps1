<#
.SYNOPSIS
    Smoke-checks Materialize ProgressBar scheme slots (#4684).

.DESCRIPTION
    Loads Themes, selects a Materialize palette mode, and asserts ProgressBarValue/High
    are accent-driven (not Green) while Low/Medium stay Red/Orange.

    # UnitTest-CI: exclude
#>
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

$mgr = New-Object Krypton.Toolkit.KryptonManager
[void][Krypton.Toolkit.KryptonThemeCatalog]::DiscoverThemes()

$mode = [Krypton.Toolkit.PaletteMode]::Office2007MaterializeBlue
Write-Host ("Using mode: {0}" -f $mode)
$mgr.GlobalPaletteMode = $mode
$palette = [Krypton.Toolkit.KryptonManager]::CurrentGlobalPalette
$value = $palette.GetSchemeColor([Krypton.Toolkit.SchemeBaseColors]::ProgressBarValue)
$high = $palette.GetSchemeColor([Krypton.Toolkit.SchemeBaseColors]::ProgressBarHigh)
$low = $palette.GetSchemeColor([Krypton.Toolkit.SchemeBaseColors]::ProgressBarLow)
$med = $palette.GetSchemeColor([Krypton.Toolkit.SchemeBaseColors]::ProgressBarMedium)

Write-Host ("ProgressBarValue={0} High={1} Low={2} Medium={3}" -f $value, $high, $low, $med)

if ($value.ToArgb() -eq [System.Drawing.Color]::Green.ToArgb()) {
    throw 'ProgressBarValue still Green for Materialize'
}
if ($low.ToArgb() -ne [System.Drawing.Color]::Red.ToArgb()) {
    throw 'ProgressBarLow expected Red'
}
if ($med.ToArgb() -ne [System.Drawing.Color]::Orange.ToArgb()) {
    throw 'ProgressBarMedium expected Orange'
}
if ($value.ToArgb() -ne $high.ToArgb()) {
    throw 'High should match Value accent'
}

Write-Host 'SMOKE PASS'
