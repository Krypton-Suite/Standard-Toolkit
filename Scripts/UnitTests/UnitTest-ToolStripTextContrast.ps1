<#
.SYNOPSIS
    Asserts #4373 ToolStrip item text contrasts with the strip background on every catalog theme.

.DESCRIPTION
    Loads Debug Krypton.Interop + Krypton.Toolkit + Krypton.Themes and checks
    ColorTable.ToolStripText against ToolStripGradientBegin (WCAG AA 4.5:1).
    Also asserts the themes from the issue screenshot (Office White, Office 2007 Black,
    Visual Studio 2010 variations) no longer use an unreadable historic alias.

    Exit code 0 on success; non-zero on failure.
    Requires an STA apartment (use powershell -STA). Invoke-AllUnitTests launches include scripts with -STA.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-ToolStripTextContrast.ps1
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
    if ($Color.IsEmpty) {
        return '(empty)'
    }
    return ('#{0:X2}{1:X2}{2:X2}' -f $Color.R, $Color.G, $Color.B)
}

Write-UnitTestBanner -Status INFO -Message 'Asserting #4373 ToolStrip text contrast'

$minimum = [Krypton.Toolkit.CommonHelper]::ReadableContrastRatio
$allModes = @([enum]::GetValues([Krypton.Toolkit.PaletteMode]))

foreach ($mode in $allModes) {
    if ($mode -eq [Krypton.Toolkit.PaletteMode]::Global -or $mode -eq [Krypton.Toolkit.PaletteMode]::Custom) {
        continue
    }

    $palette = [Krypton.Toolkit.KryptonManager]::GetPaletteForMode($mode)
    if ($null -eq $palette -or $null -eq $palette.ColorTable) {
        $failed.Add("No ColorTable for $mode")
        Write-Host "FAIL: No ColorTable for $mode" -ForegroundColor Red
        continue
    }

    $text = $palette.ColorTable.ToolStripText
    $back = $palette.ColorTable.ToolStripGradientBegin
    $ratio = [Krypton.Toolkit.CommonHelper]::ColorContrastRatio($text, $back)
    $ok = [Krypton.Toolkit.CommonHelper]::HasReadableContrast($text, $back)
    $message = "$mode ToolStripText=$(Format-Color $text) on Begin=$(Format-Color $back) contrast=$([math]::Round($ratio, 2)):1"
    Assert-True $ok $message
}

$reported = @(
    [Krypton.Toolkit.PaletteMode]::Office2010White,
    [Krypton.Toolkit.PaletteMode]::Office2013White,
    [Krypton.Toolkit.PaletteMode]::Office2007Black,
    [Krypton.Toolkit.PaletteMode]::Office2010Black,
    [Krypton.Toolkit.PaletteMode]::VisualStudio2010Render2010,
    [Krypton.Toolkit.PaletteMode]::VisualStudio2010Render2013,
    [Krypton.Toolkit.PaletteMode]::VisualStudio2010Render365
)

foreach ($mode in $reported) {
    $palette = [Krypton.Toolkit.KryptonManager]::GetPaletteForMode($mode)
    $text = $palette.ColorTable.ToolStripText
    $back = $palette.ColorTable.ToolStripGradientBegin
    $ratio = [Krypton.Toolkit.CommonHelper]::ColorContrastRatio($text, $back)
    Assert-True ($ratio -ge $minimum) "Reported theme $mode remains at or above $minimum :1 ($([math]::Round($ratio, 2)):1)"
}

$white = [Krypton.Toolkit.KryptonManager]::GetPaletteForMode([Krypton.Toolkit.PaletteMode]::Office2010White)
Assert-True ($white.ColorTable.ToolStripText.R -lt 200) 'Office 2010 White ToolStripText is dark, not white-on-white'

$black = [Krypton.Toolkit.KryptonManager]::GetPaletteForMode([Krypton.Toolkit.PaletteMode]::Office2007Black)
Assert-True ([Krypton.Toolkit.CommonHelper]::ColorRelativeLuminance($black.ColorTable.ToolStripText) -gt 0.5) 'Office 2007 Black ToolStripText is light on the dark strip'

if ($failed.Count -gt 0) {
    Write-Host ("`n{0} assertion(s) failed." -f $failed.Count) -ForegroundColor Red
    exit 1
}

Write-Host "`nAll #4373 ToolStrip text contrast assertions passed." -ForegroundColor Green
exit 0
