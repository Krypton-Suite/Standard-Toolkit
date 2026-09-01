<#
.SYNOPSIS
    Asserts #4253 QAT overflow chevrons paint for Office 2007 and Office 2010 at 96/144/192 DPI.

.DESCRIPTION
    Loads Debug Krypton.Interop + Krypton.Toolkit and draws DrawRibbonOverflow into bitmaps
    whose resolution is 96, 144, and 192 DPI. Both the two-tone (Office 2007) and unified-colour
    (Office 2010) paths must produce opaque pixels, and the painted width must grow with DPI.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-RibbonOverflowGlyph.ps1
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

function Get-OpaqueMetrics {
    param([System.Drawing.Bitmap]$Bitmap)
    $minX = $Bitmap.Width
    $maxX = -1
    $count = 0
    for ($y = 0; $y -lt $Bitmap.Height; $y++) {
        for ($x = 0; $x -lt $Bitmap.Width; $x++) {
            if ($Bitmap.GetPixel($x, $y).A -gt 0) {
                $count++
                if ($x -lt $minX) { $minX = $x }
                if ($x -gt $maxX) { $maxX = $x }
            }
        }
    }
    $width = 0
    if ($maxX -ge 0) {
        $width = $maxX - $minX + 1
    }
    return @{ Count = $count; Width = $width }
}

function Invoke-DrawOverflow {
    param(
        [float]$Dpi,
        [Krypton.Toolkit.PaletteRibbonShape]$Shape,
        [Krypton.Toolkit.IPaletteRibbonGeneral]$PaletteGeneral,
        [Krypton.Toolkit.IRenderer]$Renderer
    )

    $form = New-Object System.Windows.Forms.Form
    $bmp = New-Object System.Drawing.Bitmap 64, 32, ([System.Drawing.Imaging.PixelFormat]::Format32bppArgb)
    $bmp.SetResolution($Dpi, $Dpi)
    $g = [System.Drawing.Graphics]::FromImage($bmp)
    try {
        Assert-True ([math]::Abs($g.DpiX - $Dpi) -lt 0.01) ("Graphics.DpiX is {0} for {1} DPI {2}" -f $g.DpiX, $Dpi, $Shape)
        $rect = New-Object System.Drawing.Rectangle 4, 8, 24, 16
        $ctx = New-Object Krypton.Toolkit.RenderContext($form, $g, $rect, $Renderer)
        $Renderer.RenderGlyph.DrawRibbonOverflow($Shape, $ctx, $rect, $PaletteGeneral, [Krypton.Toolkit.PaletteState]::Normal)
        $ctx.Dispose()
    }
    finally {
        $g.Dispose()
        $form.Dispose()
    }
    return $bmp
}

$renderer = [Krypton.Toolkit.KryptonManager]::RenderStandard
Assert-True ($null -ne $renderer) 'RenderStandard is available'

$palette2007 = [Krypton.Toolkit.KryptonManager]::PaletteOffice2007Blue
$redirect = New-Object Krypton.Toolkit.PaletteRedirect($palette2007)
$general = New-Object Krypton.Toolkit.PaletteRibbonGeneralInheritRedirect($redirect)

$shapes = @(
    [Krypton.Toolkit.PaletteRibbonShape]::Office2007,
    [Krypton.Toolkit.PaletteRibbonShape]::Office2010
)

$dpiList = @(96, 144, 192)

foreach ($shape in $shapes) {
    $widthAt96 = $null
    $widthAt192 = $null
    foreach ($dpi in $dpiList) {
        $bmp = Invoke-DrawOverflow -Dpi $dpi -Shape $shape -PaletteGeneral $general -Renderer $renderer
        try {
            $metrics = Get-OpaqueMetrics -Bitmap $bmp
            Assert-True ($metrics.Count -gt 0) ("{0} overflow glyph at {1} DPI has opaque pixels" -f $shape, $dpi)
            Assert-True ($metrics.Width -gt 0) ("{0} overflow glyph at {1} DPI has a painted width" -f $shape, $dpi)
            if ($dpi -eq 96) { $widthAt96 = $metrics.Width }
            if ($dpi -eq 192) { $widthAt192 = $metrics.Width }
        }
        finally {
            $bmp.Dispose()
        }
    }
    Assert-True ($null -ne $widthAt96 -and $null -ne $widthAt192 -and $widthAt192 -gt $widthAt96) (
        "{0} overflow glyph painted width grows from 96 DPI ({1}px) to 192 DPI ({2}px)" -f $shape, $widthAt96, $widthAt192
    )
}

if ($failed.Count -gt 0) {
    Write-Error ("Ribbon overflow glyph checks failed:`n" + ($failed -join "`n"))
    exit 1
}

exit 0
