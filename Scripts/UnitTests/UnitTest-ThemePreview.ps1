<#
.SYNOPSIS
    Asserts #3870 theme preview generation and Thumbnail persist.

.DESCRIPTION
    Loads Debug Krypton.Toolkit and checks KryptonThemePreview.Create size,
    AssignGeneratedThumbnail, and .kthemex round-trip of Thumbnail.

    Exit code 0 on success; non-zero on failure.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-ThemePreview.ps1
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

Write-UnitTestBanner -Status INFO -Message 'Asserting #3870 theme previews'

$palette = [Krypton.Toolkit.KryptonManager]::GetPaletteForMode([Krypton.Toolkit.PaletteMode]::Microsoft365Blue)
$preview = [Krypton.Toolkit.KryptonThemePreview]::Create($palette, 64)
Assert-True (($preview.Width -eq 64) -and ($preview.Height -eq 64)) 'Create(palette, 64) is 64x64'
$preview.Dispose()

$custom = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
$custom.BasePaletteMode = [Krypton.Toolkit.PaletteMode]::Microsoft365Blue
$custom.SetPaletteName('3870-test')
[Krypton.Toolkit.KryptonThemePreview]::AssignGeneratedThumbnail($custom)
Assert-True ($null -ne $custom.Thumbnail) 'AssignGeneratedThumbnail sets Thumbnail'
Assert-True (($custom.Thumbnail.Width -eq 64) -and ($custom.Thumbnail.Height -eq 64)) 'Assigned Thumbnail is 64x64'

$temp = Join-Path ([System.IO.Path]::GetTempPath()) ('krypton-3870-' + [guid]::NewGuid().ToString('N'))
[void][System.IO.Directory]::CreateDirectory($temp)
try {
    $path = Join-Path $temp 'preview.kthemex'
    [void]$custom.Export($path, $true, $true)
    $imported = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
    [void]$imported.Import($path, $true)
    Assert-True ($null -ne $imported.Thumbnail) '.kthemex import restores Thumbnail'
    Assert-True (($imported.Thumbnail.Width -eq 64) -and ($imported.Thumbnail.Height -eq 64)) 'Imported Thumbnail is 64x64'
    $icon = [Krypton.Toolkit.KryptonPaletteFile]::CreateThemeIcon($imported.Thumbnail, 32)
    Assert-True (($icon.Width -eq 32) -and ($icon.Height -eq 32)) 'CreateThemeIcon composites the stored preview at 32x32'
    $icon.Dispose()
    $imported.Dispose()
}
finally {
    $custom.Dispose()
    if (Test-Path -LiteralPath $temp) {
        Remove-Item -LiteralPath $temp -Recurse -Force
    }
}

$none = [Krypton.Toolkit.KryptonThemePreview]::Resolve('Microsoft 365 - Blue', $null, $false)
Assert-True ($null -eq $none) 'Resolve builtin without generateWhenMissing returns null'
$generated = [Krypton.Toolkit.KryptonThemePreview]::Resolve('Microsoft 365 - Blue', $null, $true)
Assert-True ($null -ne $generated) 'Resolve builtin with generateWhenMissing returns a mock-up'
if ($generated) { $generated.Dispose() }

if ($failed.Count -gt 0) {
    Write-Host "$($failed.Count) assertion(s) failed." -ForegroundColor Red
    exit 1
}

Write-Host 'UnitTest-ThemePreview passed.' -ForegroundColor Green
exit 0
