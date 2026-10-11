<#
.SYNOPSIS
    Asserts disabled caption-button glyph colours for issue #4413.

.DESCRIPTION
    Office 2010, Office 2013, and Microsoft 365 caption buttons keep an empty close fill.
    Disabled ButtonForm / ButtonFormClose text is theme-specific: light 205 gray, Office 2010 Black
    and Office 2013 Dark Gray 196 gray, Microsoft 365 Black ghost white.

    Exit code 0 on success; non-zero on failure.
    Requires an STA apartment (use powershell -STA). Invoke-AllUnitTests launches include scripts with -STA.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-DisabledCaptionGlyph.ps1
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
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Themes.dll'))

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

function Format-Color {
    param($Color)
    if ($Color.IsEmpty) { return 'empty' }
    return ('#{0:X2}{1:X2}{2:X2}' -f $Color.R, $Color.G, $Color.B)
}

function Assert-Color {
    param($Actual, [System.Drawing.Color]$Expected, [string]$Message)
    $ok = $Actual.ToArgb() -eq $Expected.ToArgb()
    Assert-True $ok "$Message (got $(Format-Color $Actual), expected $(Format-Color $Expected))"
}

Write-UnitTestBanner -Status INFO -Message 'Asserting disabled caption glyph colours (#4413)'

$style = [Krypton.Toolkit.PaletteContentStyle]::ButtonFormClose
$formStyle = [Krypton.Toolkit.PaletteContentStyle]::ButtonForm
$back = [Krypton.Toolkit.PaletteBackStyle]::ButtonFormClose
$disabled = [Krypton.Toolkit.PaletteState]::Disabled
$normal = [Krypton.Toolkit.PaletteState]::Normal
$light = [System.Drawing.Color]::FromArgb(205, 205, 205)
$dark = [System.Drawing.Color]::FromArgb(196, 196, 196)
$ghost = [System.Drawing.Color]::GhostWhite

$cases = @(
    @{ Name = 'Office 2010 Blue'; Type = [Krypton.Toolkit.PaletteOffice2010Blue]; Glyph = $light }
    @{ Name = 'Office 2010 Black'; Type = [Krypton.Toolkit.PaletteOffice2010Black]; Glyph = $dark }
    @{ Name = 'Office 2013 White'; Type = [Krypton.Themes.PaletteOffice2013White]; Glyph = $light }
    @{ Name = 'Office 2013 Dark Gray'; Type = [Krypton.Themes.PaletteOffice2013DarkGray]; Glyph = $dark }
    @{ Name = 'Microsoft 365 Blue'; Type = [Krypton.Toolkit.PaletteMicrosoft365Blue]; Glyph = $light }
    @{ Name = 'Microsoft 365 Black'; Type = [Krypton.Toolkit.PaletteMicrosoft365Black]; Glyph = $ghost }
)

foreach ($case in $cases) {
    $palette = [System.Activator]::CreateInstance($case.Type)
    try {
        $glyph = $palette.GetContentShortTextColor1($style, $disabled)
        Assert-Color $glyph $case.Glyph "$($case.Name) close glyph"
        $formGlyph = $palette.GetContentShortTextColor1($formStyle, $disabled)
        Assert-Color $formGlyph $case.Glyph "$($case.Name) form glyph matches close"
        $longGlyph = $palette.GetContentLongTextColor1($style, $disabled)
        Assert-Color $longGlyph $case.Glyph "$($case.Name) long-text glyph"
        $fill = $palette.GetBackColor1($back, $disabled)
        Assert-True $fill.IsEmpty "$($case.Name) disabled close fill is empty (got $(Format-Color $fill))"
        $idle = $palette.GetBackColor1($back, $normal)
        Assert-True $idle.IsEmpty "$($case.Name) idle close fill is empty (got $(Format-Color $idle))"
    }
    finally {
        if ($palette -is [System.IDisposable]) {
            $palette.Dispose()
        }
    }
}

if ($failed.Count -gt 0) {
    Write-UnitTestBanner -Status FAIL -Message "$($failed.Count) assertion(s) failed"
    exit 1
}

Write-UnitTestBanner -Status PASS -Message 'Disabled caption glyph colours match #4413'
exit 0
