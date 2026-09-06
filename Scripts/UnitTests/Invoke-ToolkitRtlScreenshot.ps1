<#
.SYNOPSIS
    Hosts RTLControlsTest and captures LTR / RTL PNGs for issue #2379.

.DESCRIPTION
    Launches Debug TestForm with --demo RTLControlsTest (process host, not LoadFrom).
    Prefers TestForm.exe; falls back to `dotnet TestForm.dll` when Application Control
    blocks the exe. Captures LTR, then relaunches with --rtl (both flags) and captures RTL.
    Writes Documents/PR/2379-toolkit-rtl-ltr.png and 2379-toolkit-rtl-rtl.png (or -OutputDir).

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-ToolkitRtlScreenshot.ps1
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
Initialize-UnitTestNativeInput

if (-not ('ToolkitRtlCaptureNative' -as [type])) {
    Add-Type @"
using System;
using System.Runtime.InteropServices;
public static class ToolkitRtlCaptureNative
{
    [DllImport("user32.dll")] public static extern bool SetWindowPos(IntPtr hWnd, IntPtr h, int X, int Y, int cx, int cy, uint flags);
    public const uint SWP_NOSIZE = 0x0001;
    public const uint SWP_NOZORDER = 0x0004;
}
"@
}

function Get-ToolkitRtlHost {
    param([string]$BinDir)

    $exe = Join-Path $BinDir 'TestForm.exe'
    $dll = Join-Path $BinDir 'TestForm.dll'
    if (Test-Path -LiteralPath $exe) {
        try {
            $probe = Start-Process -FilePath $exe -ArgumentList @('--help') -WorkingDirectory $BinDir -PassThru -WindowStyle Hidden -ErrorAction Stop
            Start-Sleep -Milliseconds 200
            if ($probe -and -not $probe.HasExited) {
                $probe.Kill()
            }
            return @{ FilePath = $exe; WorkingDirectory = $BinDir }
        }
        catch {
            # WDAC / Application Control often blocks the net472 exe; dotnet is trusted.
        }
    }

    $dotnetBins = @(
        $BinDir,
        (Join-Path (Split-Path -Parent $BinDir) 'net8.0-windows')
    )
    foreach ($candidate in $dotnetBins) {
        $candidateDll = Join-Path $candidate 'TestForm.dll'
        if (Test-Path -LiteralPath $candidateDll) {
            return @{ FilePath = 'dotnet'; ArgumentsPrefix = @($candidateDll); WorkingDirectory = $candidate }
        }
    }

    throw "TestForm.exe/dll not found or blocked in $BinDir. Build TestForm first."
}

function Wait-AutomationWindow {
    param(
        [string]$Name,
        [int]$TimeoutMs = 25000
    )

    $root = [System.Windows.Automation.AutomationElement]::RootElement
    $cond = New-Object System.Windows.Automation.PropertyCondition (
        [System.Windows.Automation.AutomationElement]::NameProperty, $Name)
    $sw = [System.Diagnostics.Stopwatch]::StartNew()
    while ($sw.ElapsedMilliseconds -lt $TimeoutMs) {
        $el = $root.FindFirst([System.Windows.Automation.TreeScope]::Children, $cond)
        if ($el) {
            $rect = $el.Current.BoundingRectangle
            if ($rect.Width -ge 400 -and $rect.Height -ge 300) {
                return $el
            }
        }
        Start-Sleep -Milliseconds 200
    }

    throw "Window '$Name' was not found within ${TimeoutMs}ms."
}

function Save-ElementShot {
    param(
        [System.Windows.Automation.AutomationElement]$Window,
        [string]$Path
    )

    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 500
    [System.Windows.Forms.Application]::DoEvents()

    $rect = $Window.Current.BoundingRectangle
    $bounds = [System.Drawing.Rectangle]::new([int]$rect.X, [int]$rect.Y, [int]$rect.Width, [int]$rect.Height)
    $bmp = New-Object System.Drawing.Bitmap $bounds.Width, $bounds.Height
    $g = [System.Drawing.Graphics]::FromImage($bmp)
    try {
        $g.CopyFromScreen($bounds.Location, [System.Drawing.Point]::Empty, $bounds.Size)
        $bmp.Save($Path, [System.Drawing.Imaging.ImageFormat]::Png)
    }
    finally {
        $g.Dispose()
        $bmp.Dispose()
    }

    Write-Host "Wrote $Path"
}

function Invoke-ToolkitRtlCapture {
    param(
        [hashtable]$HostInfo,
        [string[]]$ExtraArgs,
        [string]$OutputPath
    )

    $argList = @()
    if ($HostInfo.ArgumentsPrefix) {
        $argList += $HostInfo.ArgumentsPrefix
    }
    $argList += @('--demo', 'RTLControlsTest')
    if ($ExtraArgs) {
        $argList += $ExtraArgs
    }

    $proc = Start-Process -FilePath $HostInfo.FilePath -ArgumentList $argList -WorkingDirectory $HostInfo.WorkingDirectory -PassThru
    try {
        $window = Wait-AutomationWindow -Name 'Toolkit RTL Test (#2379)'
        $hwnd = [IntPtr]$window.Current.NativeWindowHandle
        [void][ToolkitRtlCaptureNative]::SetWindowPos($hwnd, [IntPtr]::Zero, 40, 40, 0, 0, 0x0005)
        [void][UnitTestNative]::SetForegroundWindow($hwnd)
        Start-Sleep -Milliseconds 700
        $window = Wait-AutomationWindow -Name 'Toolkit RTL Test (#2379)'
        Save-ElementShot -Window $window -Path $OutputPath
    }
    finally {
        if ($proc -and -not $proc.HasExited) {
            $proc.Kill()
            $proc.WaitForExit(5000) | Out-Null
        }
    }
}

$hostInfo = Get-ToolkitRtlHost -BinDir $bin
Invoke-ToolkitRtlCapture -HostInfo $hostInfo -OutputPath (Join-Path $OutputDir '2379-toolkit-rtl-ltr.png')
Invoke-ToolkitRtlCapture -HostInfo $hostInfo -ExtraArgs @('--rtl') -OutputPath (Join-Path $OutputDir '2379-toolkit-rtl-rtl.png')
Write-Host "Captured #2379 screenshots under $OutputDir"
