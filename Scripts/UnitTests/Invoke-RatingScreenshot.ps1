<#
.SYNOPSIS
    Hosts KryptonRatingDemo and writes default / half-precision PNGs plus a short hover GIF.

.DESCRIPTION
    Launches Debug TestForm.exe with --demo KryptonRatingDemo (does not LoadFrom the exe).
    Captures the window, switches Precision to Half, captures again, then writes a short
    hover-preview GIF by moving the mouse along the main rating strip.
    Output lands under Documents/PR/ (local PR assets; do not commit).

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-RatingScreenshot.ps1
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,
    [string]$OutputDir
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
if (-not $OutputDir) {
    $OutputDir = Join-Path $repoRoot 'Documents\PR'
}
if (-not (Test-Path -LiteralPath $OutputDir)) {
    New-Item -ItemType Directory -Path $OutputDir | Out-Null
}

Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName UIAutomationClient
Add-Type -AssemblyName UIAutomationTypes
Add-Type -AssemblyName PresentationCore
Initialize-UnitTestNativeInput

if (-not ("RatingCaptureNative" -as [type])) {
    Add-Type @"
using System;
using System.Runtime.InteropServices;
public static class RatingCaptureNative
{
    [DllImport("user32.dll")] public static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);
    [DllImport("user32.dll")] public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
    public struct RECT { public int Left; public int Top; public int Right; public int Bottom; }
}
"@
}

$exe = Join-Path $bin 'TestForm.exe'
if (-not (Test-Path -LiteralPath $exe)) {
    throw "TestForm.exe not found in $bin. Build TestForm first."
}

function Wait-ProcessWindow {
    param(
        [System.Diagnostics.Process]$Process,
        [int]$TimeoutMs = 25000
    )

    $sw = [System.Diagnostics.Stopwatch]::StartNew()
    while ($sw.ElapsedMilliseconds -lt $TimeoutMs) {
        $Process.Refresh()
        if ($Process.MainWindowHandle -ne [IntPtr]::Zero) {
            $el = [System.Windows.Automation.AutomationElement]::FromHandle($Process.MainWindowHandle)
            if ($el) {
                $rect = $el.Current.BoundingRectangle
                if ($rect.Width -ge 600 -and $rect.Height -ge 400) {
                    return $el
                }
            }
        }
        Start-Sleep -Milliseconds 200
    }

    throw "TestForm window was not found within ${TimeoutMs}ms."
}

function Find-NamedElement {
    param(
        [System.Windows.Automation.AutomationElement]$Root,
        [string]$Name
    )

    $cond = New-Object System.Windows.Automation.PropertyCondition (
        [System.Windows.Automation.AutomationElement]::NameProperty, $Name)
    return $Root.FindFirst([System.Windows.Automation.TreeScope]::Descendants, $cond)
}

function Save-WindowPrint {
    param(
        [System.IntPtr]$Handle,
        [string]$Path
    )

    Start-Sleep -Milliseconds 350
    $bmp = Get-WindowBitmap -Handle $Handle
    try {
        $bmp.Save($Path, [System.Drawing.Imaging.ImageFormat]::Png)
    }
    finally {
        $bmp.Dispose()
    }

    Write-Host "Wrote $Path"
}

function Get-WindowBitmap {
    param([System.IntPtr]$Handle)

    $rect = New-Object RatingCaptureNative+RECT
    if (-not [RatingCaptureNative]::GetWindowRect($Handle, [ref]$rect)) {
        throw 'GetWindowRect failed.'
    }

    $width = $rect.Right - $rect.Left
    $height = $rect.Bottom - $rect.Top
    if ($width -le 0 -or $height -le 0) {
        throw "Invalid window size ${width}x${height}."
    }

    $bmp = New-Object System.Drawing.Bitmap $width, $height
    $g = [System.Drawing.Graphics]::FromImage($bmp)
    $hdc = $g.GetHdc()
    try {
        [void][RatingCaptureNative]::PrintWindow($Handle, $hdc, 2)
    }
    finally {
        $g.ReleaseHdc($hdc)
        $g.Dispose()
    }

    return $bmp
}

$proc = Start-Process -FilePath $exe -ArgumentList @('--demo', 'KryptonRatingDemo') -WorkingDirectory $bin -PassThru
$frames = New-Object System.Collections.Generic.List[System.Drawing.Bitmap]
try {
    $window = Wait-ProcessWindow -Process $proc
    [void][UnitTestNative]::SetForegroundWindow([IntPtr]$proc.MainWindowHandle)
    Start-Sleep -Milliseconds 700

    $defaultPath = Join-Path $OutputDir '3928-krypton-rating-default.png'
    Save-WindowPrint -Handle $proc.MainWindowHandle -Path $defaultPath

    $hwnd = $proc.MainWindowHandle
    $winRect = New-Object RatingCaptureNative+RECT
    [void][RatingCaptureNative]::GetWindowRect($hwnd, [ref]$winRect)

    # Precision combo sits under the header on the left; Alt+Down then Half.
    $precisionX = $winRect.Left + 160
    $precisionY = $winRect.Top + 130
    [void][UnitTestNative]::SetCursorPos($precisionX, $precisionY)
    [UnitTestNative]::mouse_event([UnitTestNative]::LEFTDOWN, 0, 0, 0, [IntPtr]::Zero)
    [UnitTestNative]::mouse_event([UnitTestNative]::LEFTUP, 0, 0, 0, [IntPtr]::Zero)
    Start-Sleep -Milliseconds 250
    [System.Windows.Forms.SendKeys]::SendWait('{DOWN}')
    Start-Sleep -Milliseconds 150
    [System.Windows.Forms.SendKeys]::SendWait('{ENTER}')
    Start-Sleep -Milliseconds 400

    $halfPath = Join-Path $OutputDir '3928-krypton-rating-half.png'
    $window = Wait-ProcessWindow -Process $proc
    Save-WindowPrint -Handle $proc.MainWindowHandle -Path $halfPath

    $gifPath = Join-Path $OutputDir '3928-krypton-rating-hover.gif'
    try {
        $starY = $winRect.Top + 280
        $starLeft = $winRect.Left + 40
        $encoder = New-Object System.Windows.Media.Imaging.GifBitmapEncoder
        foreach ($step in 0..6) {
            $x = [int]($starLeft + ($step * 20))
            [void][UnitTestNative]::SetCursorPos($x, $starY)
            Start-Sleep -Milliseconds 180

            $bmp = Get-WindowBitmap -Handle $hwnd
            $frames.Add($bmp)

            $ms = New-Object System.IO.MemoryStream
            $bmp.Save($ms, [System.Drawing.Imaging.ImageFormat]::Bmp)
            $ms.Position = 0
            $decoder = New-Object System.Windows.Media.Imaging.BmpBitmapDecoder (
                $ms,
                [System.Windows.Media.Imaging.BitmapCreateOptions]::PreservePixelFormat,
                [System.Windows.Media.Imaging.BitmapCacheOption]::OnLoad)
            $encoder.Frames.Add([System.Windows.Media.Imaging.BitmapFrame]::Create($decoder.Frames[0]))
            $ms.Dispose()
        }

        $fs = [System.IO.File]::Open($gifPath, [System.IO.FileMode]::Create)
        $encoder.Save($fs)
        $fs.Dispose()
        Write-Host "Wrote $gifPath"
    }
    catch {
        Write-Warning "GIF encode failed: $($_.Exception.Message). PNG stills were written."
    }
}
finally {
    foreach ($frame in $frames) {
        $frame.Dispose()
    }
    if ($proc -and -not $proc.HasExited) {
        $proc.Kill()
        $proc.WaitForExit(5000) | Out-Null
    }
}
