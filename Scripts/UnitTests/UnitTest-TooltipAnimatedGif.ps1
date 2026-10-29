<#
.SYNOPSIS
    Shows a themed tooltip with a multi-frame GIF and checks that two captures differ.

.DESCRIPTION
    Hosts VisualPopupToolTip with ToolTipValues.Image set to a four-colour GIF.
    PrintWindow captures a few hundred milliseconds apart must not match, which
    means ImageAnimator advanced the frame. Pass -GifPath to also write an animated GIF.

    Exit 0 on success. Marker: exclude (needs an interactive desktop).

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-TooltipAnimatedGif.ps1
#>
# UnitTest-CI: exclude
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,
    [string]$GifPath
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
Register-UnitTestAssemblyResolver -BinDir $bin

[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Interop.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))

function New-PropertyItem {
    $flags = [System.Reflection.BindingFlags]'Instance,NonPublic'
    $ctor = [System.Drawing.Imaging.PropertyItem].GetConstructor($flags, $null, [Type[]]@(), $null)
    if ($null -eq $ctor) {
        throw 'PropertyItem has no constructor.'
    }

    return $ctor.Invoke(@())
}

function New-SolidBitmap {
    param([System.Drawing.Color]$Color)
    $bitmap = New-Object System.Drawing.Bitmap 48, 48, ([System.Drawing.Imaging.PixelFormat]::Format32bppArgb)
    $graphics = [System.Drawing.Graphics]::FromImage($bitmap)
    try {
        $graphics.Clear($Color)
    }
    finally {
        $graphics.Dispose()
    }

    return $bitmap
}

function New-ColorCycleGif {
    $colors = @(
        [System.Drawing.Color]::Firebrick,
        [System.Drawing.Color]::DarkOrange,
        [System.Drawing.Color]::SeaGreen,
        [System.Drawing.Color]::RoyalBlue
    )
    $frames = New-Object System.Collections.Generic.List[System.Drawing.Bitmap]
    try {
        foreach ($color in $colors) {
            $frame = New-SolidBitmap -Color $color
            $delay = New-PropertyItem
            $delay.Id = 0x5100
            $delay.Type = 4
            $delay.Len = 4
            $delay.Value = [BitConverter]::GetBytes([int]15)
            $frame.SetPropertyItem($delay)
            $frames.Add($frame)
        }

        $loop = New-PropertyItem
        $loop.Id = 0x5101
        $loop.Type = 3
        $loop.Len = 2
        $loop.Value = [byte[]](0, 0)
        $frames[0].SetPropertyItem($loop)

        $encoder = [System.Drawing.Imaging.ImageCodecInfo]::GetImageEncoders() |
            Where-Object { $_.FormatID -eq [System.Drawing.Imaging.ImageFormat]::Gif.Guid } |
            Select-Object -First 1
        $script:GifStream = New-Object System.IO.MemoryStream
        $first = New-Object System.Drawing.Imaging.EncoderParameters 1
        $first.Param[0] = New-Object System.Drawing.Imaging.EncoderParameter (
            [System.Drawing.Imaging.Encoder]::SaveFlag,
            [long][System.Drawing.Imaging.EncoderValue]::MultiFrame)
        $frames[0].Save($script:GifStream, $encoder, $first)
        $first.Dispose()

        $next = New-Object System.Drawing.Imaging.EncoderParameters 1
        $next.Param[0] = New-Object System.Drawing.Imaging.EncoderParameter (
            [System.Drawing.Imaging.Encoder]::SaveFlag,
            [long][System.Drawing.Imaging.EncoderValue]::FrameDimensionTime)
        for ($i = 1; $i -lt $frames.Count; $i++) {
            $frames[0].SaveAdd($frames[$i], $next)
        }
        $next.Dispose()

        $flush = New-Object System.Drawing.Imaging.EncoderParameters 1
        $flush.Param[0] = New-Object System.Drawing.Imaging.EncoderParameter (
            [System.Drawing.Imaging.Encoder]::SaveFlag,
            [long][System.Drawing.Imaging.EncoderValue]::Flush)
        $frames[0].SaveAdd($flush)
        $flush.Dispose()

        $script:GifStream.Position = 0
        return [System.Drawing.Image]::FromStream($script:GifStream)
    }
    finally {
        foreach ($frame in $frames) {
            $frame.Dispose()
        }
    }
}

function Wait-Ui {
    param([int]$Milliseconds)
    $deadline = [DateTime]::UtcNow.AddMilliseconds($Milliseconds)
    while ([DateTime]::UtcNow -lt $deadline) {
        [System.Windows.Forms.Application]::DoEvents()
        Start-Sleep -Milliseconds 15
    }
}

function Test-BitmapsDiffer {
    param(
        [System.Drawing.Bitmap]$Left,
        [System.Drawing.Bitmap]$Right
    )

    if ($Left.Width -ne $Right.Width -or $Left.Height -ne $Right.Height) {
        return $true
    }

    for ($y = 0; $y -lt $Left.Height; $y += 2) {
        for ($x = 0; $x -lt $Left.Width; $x += 2) {
            if ($Left.GetPixel($x, $y).ToArgb() -ne $Right.GetPixel($x, $y).ToArgb()) {
                return $true
            }
        }
    }

    return $false
}

$gif = New-ColorCycleGif
if (-not [System.Drawing.ImageAnimator]::CanAnimate($gif)) {
    throw 'Generated GIF is not animated.'
}

$palette = [Krypton.Toolkit.KryptonManager]::CurrentGlobalPalette
$redirector = New-Object Krypton.Toolkit.PaletteRedirect $palette
$renderer = $palette.GetRenderer()
$dpi = [Krypton.Toolkit.HeaderValuesBase+GetDpiFactor]{ return 1.0 }
$values = New-Object Krypton.Toolkit.ToolTipValues ($null, $dpi)
$values.Heading = 'Animated GIF'
$values.Description = 'Frame colours advance while the tip is open.'
$values.Image = $gif

$popup = New-Object Krypton.Toolkit.VisualPopupToolTip ($redirector, $values, $renderer, $false)
$tempDir = Join-Path ([System.IO.Path]::GetTempPath()) 'krypton-4379'
New-Item -ItemType Directory -Path $tempDir -Force | Out-Null
$firstPath = Join-Path $tempDir 'frame-a.png'
$secondPath = Join-Path $tempDir 'frame-b.png'
$captured = New-Object System.Collections.Generic.List[System.Drawing.Bitmap]

try {
    $popup.ShowCalculatingSize((New-Object System.Drawing.Point 160, 120))
    Initialize-UnitTestNativeInput
    [void][UnitTestNative]::SetCursorPos($popup.Left + 20, $popup.Top + 20)
    Wait-Ui -Milliseconds 200
    if ($popup.IsDisposed -or -not $popup.IsHandleCreated) {
        throw 'Tooltip popup closed before the first capture.'
    }

    Save-UnitTestWindowPng -Handle $popup.Handle -Path $firstPath -SettleMs 50
    $captured.Add([System.Drawing.Bitmap]::FromFile($firstPath))

    if ($GifPath) {
        Add-Type -AssemblyName PresentationCore
        $encoder = New-Object System.Windows.Media.Imaging.GifBitmapEncoder
        foreach ($step in 0..5) {
            $framePath = Join-Path $tempDir ("gif-{0}.png" -f $step)
            Wait-Ui -Milliseconds 180
            Save-UnitTestWindowPng -Handle $popup.Handle -Path $framePath -SettleMs 20
            $bmp = [System.Drawing.Bitmap]::FromFile($framePath)
            $captured.Add($bmp)
            $ms = New-Object System.IO.MemoryStream
            $bmp.Save($ms, [System.Drawing.Imaging.ImageFormat]::Bmp)
            $ms.Position = 0
            $decoder = New-Object System.Windows.Media.Imaging.BmpBitmapDecoder (
                $ms,
                [System.Windows.Media.Imaging.BitmapCreateOptions]::PreservePixelFormat,
                [System.Windows.Media.Imaging.BitmapCacheOption]::OnLoad)
            $metadata = New-Object System.Windows.Media.Imaging.BitmapMetadata 'gif'
            $metadata.SetQuery('/grctlext/Delay', [uint16]20)
            $encoder.Frames.Add([System.Windows.Media.Imaging.BitmapFrame]::Create($decoder.Frames[0], $null, $metadata, $null))
            $ms.Dispose()
        }

        $gifDir = Split-Path -Parent $GifPath
        if ($gifDir -and -not (Test-Path -LiteralPath $gifDir)) {
            New-Item -ItemType Directory -Path $gifDir | Out-Null
        }

        $fs = [System.IO.File]::Open($GifPath, [System.IO.FileMode]::Create)
        try {
            $encoder.Save($fs)
        }
        finally {
            $fs.Dispose()
        }

        Write-Host "Wrote $GifPath"
    }
    else {
        Wait-Ui -Milliseconds 400
    }

    Save-UnitTestWindowPng -Handle $popup.Handle -Path $secondPath -SettleMs 50
    $second = [System.Drawing.Bitmap]::FromFile($secondPath)
    $captured.Add($second)
    if (-not (Test-BitmapsDiffer -Left $captured[0] -Right $second)) {
        throw 'Tooltip captures match; the GIF frame did not advance.'
    }

    Write-Host 'PASS: tooltip GIF frames differ.'
}
finally {
    foreach ($bitmap in $captured) {
        $bitmap.Dispose()
    }

    if ($null -ne $popup -and -not $popup.IsDisposed) {
        $popup.Dispose()
    }

    $gif.Dispose()
    if ($null -ne $script:GifStream) {
        $script:GifStream.Dispose()
    }
}
