<#
.SYNOPSIS
    Asserts #4264 command-link default arrow resolution (shell32 + Windows 7 embedded fallback).

.DESCRIPTION
    Loads Debug binaries and checks that CommandLinkArrowHelper.GetDefaultArrowImage returns a
    32x32 image with opaque pixels, and that the Windows 7 embedded resource is packaged.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-CommandLinkArrow.ps1
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

function Test-HasOpaquePixels {
    param([System.Drawing.Bitmap]$Bitmap)
    $opaque = 0
    for ($y = 0; $y -lt $Bitmap.Height; $y++) {
        for ($x = 0; $x -lt $Bitmap.Width; $x++) {
            if ($Bitmap.GetPixel($x, $y).A -gt 0) {
                $opaque++
            }
        }
    }
    return $opaque -gt 0
}

$helper = [Krypton.Toolkit.CommandLinkArrowHelper]
Assert-True ($null -ne $helper) 'CommandLinkArrowHelper type loads'

$arrow = [Krypton.Toolkit.CommandLinkArrowHelper]::GetDefaultArrowImage()
Assert-True ($null -ne $arrow) 'GetDefaultArrowImage returns an image'
Assert-True ($arrow.Width -eq 32 -and $arrow.Height -eq 32) 'Default arrow is 32x32'
if ($arrow -is [System.Drawing.Bitmap]) {
    Assert-True (Test-HasOpaquePixels -Bitmap $arrow) 'Default arrow has opaque pixels'
}
else {
    $bmp = New-Object System.Drawing.Bitmap $arrow
    try {
        Assert-True (Test-HasOpaquePixels -Bitmap $bmp) 'Default arrow has opaque pixels'
    }
    finally {
        $bmp.Dispose()
    }
}

$resourceType = [Krypton.Toolkit.CommandLinkArrowHelper].Assembly.GetType('Krypton.Toolkit.ResourceFiles.CommandLink.CommandLinkImageResources', $true)
$win7Prop = $resourceType.GetProperty('Windows_7_CommandLink_Arrow', [System.Reflection.BindingFlags]'NonPublic,Static')
Assert-True ($null -ne $win7Prop) 'Windows_7_CommandLink_Arrow resource property exists'
$win7 = [System.Drawing.Bitmap]$win7Prop.GetValue($null)
Assert-True ($null -ne $win7) 'Windows 7 embedded arrow loads'
Assert-True ($win7.Width -eq 32 -and $win7.Height -eq 32) 'Windows 7 embedded arrow is 32x32'
Assert-True (Test-HasOpaquePixels -Bitmap $win7) 'Windows 7 embedded arrow has opaque pixels'

if ($failed.Count -gt 0) {
    Write-Error ("Command-link arrow checks failed:`n" + ($failed -join "`n"))
    exit 1
}

exit 0
