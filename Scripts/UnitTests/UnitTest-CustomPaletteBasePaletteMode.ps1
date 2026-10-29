<#
.SYNOPSIS
    Asserts #1870 KryptonCustomPaletteBase.BasePaletteMode updates the colour table.

.DESCRIPTION
    Loads Debug Krypton.Toolkit and checks that switching BasePaletteMode to
    Office 2010 Silver inherits that theme's ColorTable, that a ToolMenuStatus
    override wins, and that assigning a builtin palette to BasePalette keeps
    the matching PaletteMode instead of Custom.

    Exit code 0 on success; non-zero on failure.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-CustomPaletteBasePaletteMode.ps1
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

Write-UnitTestBanner -Status INFO -Message 'Asserting #1870 custom palette BasePaletteMode colour table'

$palette = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
$blue = [Krypton.Toolkit.KryptonManager]::GetPaletteForMode([Krypton.Toolkit.PaletteMode]::Microsoft365Blue)
$silver = [Krypton.Toolkit.KryptonManager]::GetPaletteForMode([Krypton.Toolkit.PaletteMode]::Office2010Silver)
$office2010Blue = [Krypton.Toolkit.KryptonManager]::GetPaletteForMode([Krypton.Toolkit.PaletteMode]::Office2010Blue)

Assert-True ($palette.BasePaletteMode -eq [Krypton.Toolkit.PaletteMode]::Microsoft365Blue) 'Default BasePaletteMode is Microsoft 365 Blue'
Assert-True ((Format-Color $palette.ColorTable.ToolStripGradientBegin) -eq (Format-Color $blue.ColorTable.ToolStripGradientBegin)) 'Default ColorTable matches Microsoft 365 Blue'

$palette.BasePaletteMode = [Krypton.Toolkit.PaletteMode]::Office2010Silver
Assert-True ($palette.BasePaletteMode -eq [Krypton.Toolkit.PaletteMode]::Office2010Silver) 'BasePaletteMode stores Office 2010 Silver'
Assert-True ($palette.BasePalette.GetType().FullName -eq $silver.GetType().FullName) 'BasePalette instance is Office 2010 Silver'
Assert-True ((Format-Color $palette.ColorTable.ToolStripGradientBegin) -eq (Format-Color $silver.ColorTable.ToolStripGradientBegin)) 'ColorTable ToolStripGradientBegin inherits Office 2010 Silver'
Assert-True ((Format-Color $palette.ColorTable.StatusStripGradientBegin) -eq (Format-Color $silver.ColorTable.StatusStripGradientBegin)) 'ColorTable StatusStripGradientBegin inherits Office 2010 Silver'
Assert-True ((Format-Color $palette.ColorTable.ToolStripGradientBegin) -ne (Format-Color $blue.ColorTable.ToolStripGradientBegin)) 'Office 2010 Silver ColorTable differs from Microsoft 365 Blue'

$overrideColor = [System.Drawing.Color]::Lime
$palette.ToolMenuStatus.StatusStrip.StatusStripGradientBegin = $overrideColor
Assert-True ((Format-Color $palette.ColorTable.StatusStripGradientBegin) -eq (Format-Color $overrideColor)) 'ToolMenuStatus override wins over the inherited ColorTable'

$palette.ToolMenuStatus.StatusStrip.ResetStatusStripGradientBegin()
Assert-True ((Format-Color $palette.ColorTable.StatusStripGradientBegin) -eq (Format-Color $silver.ColorTable.StatusStripGradientBegin)) 'Resetting the override restores the inherited ColorTable'

$palette.BasePalette = $office2010Blue
Assert-True ($palette.BasePaletteMode -eq [Krypton.Toolkit.PaletteMode]::Office2010Blue) 'Assigning a builtin BasePalette keeps the catalog mode (not Custom)'
Assert-True ((Format-Color $palette.ColorTable.ToolStripGradientBegin) -eq (Format-Color $office2010Blue.ColorTable.ToolStripGradientBegin)) 'ColorTable follows the assigned builtin BasePalette'

$shouldSerialize = $palette.GetType().GetMethod('ShouldSerializeBasePalette', [System.Reflection.BindingFlags]'Instance, NonPublic')
Assert-True ($null -ne $shouldSerialize) 'ShouldSerializeBasePalette is present'
Assert-True (-not [bool]$shouldSerialize.Invoke($palette, @())) 'Builtin BasePalette is not designer-serialized'

$other = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
$palette.BasePalette = $other
Assert-True ($palette.BasePaletteMode -eq [Krypton.Toolkit.PaletteMode]::Custom) 'Assigning another custom palette sets BasePaletteMode to Custom'
Assert-True ([bool]$shouldSerialize.Invoke($palette, @())) 'Custom BasePalette is designer-serialized'

$palette.Dispose()
$other.Dispose()

if ($failed.Count -gt 0) {
    Write-Host ("{0} assertion(s) failed." -f $failed.Count) -ForegroundColor Red
    exit 1
}

Write-Host 'All #1870 custom palette BasePaletteMode assertions passed.' -ForegroundColor Green
exit 0
