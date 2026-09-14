<#
.SYNOPSIS
    Asserts #2103 KryptonForm RTL chrome coordinates (close button side + ScreenToWindow).

.DESCRIPTION
    Shows a KryptonForm in LTR and RTL+RightToLeftLayout, then checks that ScreenToWindow
    uses the physical top-left origin, HitTestCloseButton matches native chrome placement
    (right in LTR, left in RTL layout), and the realized window region includes both
    physical left and right chrome (RTL must not clip the left frame).

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-KryptonFormRtl.ps1
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
Add-Type @"
using System;
using System.Runtime.InteropServices;
public static class KryptonFormRtlNative {
    [DllImport("user32.dll")] public static extern int GetWindowRgn(IntPtr hWnd, IntPtr hRgn);
    [DllImport("gdi32.dll")] public static extern IntPtr CreateRectRgn(int x1, int y1, int x2, int y2);
    [DllImport("gdi32.dll")] public static extern bool DeleteObject(IntPtr hObject);
    [DllImport("gdi32.dll")] public static extern bool PtInRegion(IntPtr hrgn, int x, int y);
    public const int ERROR = 0;
    public const int NULLREGION = 1;
    public const int SIMPLEREGION = 2;
    public const int COMPLEXREGION = 3;
}
"@

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

$screenToWindow = [Krypton.Toolkit.VisualForm].GetMethod(
    'ScreenToWindow',
    [System.Reflection.BindingFlags]'Instance,NonPublic')
Assert-True ($null -ne $screenToWindow) 'VisualForm.ScreenToWindow is available'

function Test-FormRtlChrome {
    param(
        [bool]$RtlLayout,
        [string]$Label
    )

    $form = New-Object Krypton.Toolkit.KryptonForm
    try {
        $form.Text = 'Caption Test ABC'
        $form.ControlBox = $true
        $form.MinimizeBox = $true
        $form.MaximizeBox = $true
        $form.FormBorderStyle = [System.Windows.Forms.FormBorderStyle]::Sizable
        $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
        $form.Location = New-Object System.Drawing.Point 80, 80
        $form.Size = New-Object System.Drawing.Size 480, 280
        if ($RtlLayout) {
            $form.RightToLeft = [System.Windows.Forms.RightToLeft]::Yes
            $form.RightToLeftLayout = $true
        }
        else {
            $form.RightToLeft = [System.Windows.Forms.RightToLeft]::No
            $form.RightToLeftLayout = $false
        }

        $form.Show()
        $form.Activate()
        [System.Windows.Forms.Application]::DoEvents()
        Start-Sleep -Milliseconds 200
        [System.Windows.Forms.Application]::DoEvents()

        $leftScreen = New-Object System.Drawing.Point ($form.Left + 2), ($form.Top + 40)
        $windowPt = $screenToWindow.Invoke($form, @($leftScreen))
        Assert-True ($windowPt.X -lt 40) "$Label ScreenToWindow(physical left) X=$($windowPt.X) is near 0, not mirrored to Width"

        $closeXs = New-Object System.Collections.Generic.List[int]
        for ($scanY = 8; $scanY -le 36; $scanY += 4) {
            for ($scanX = 0; $scanX -lt $form.Width; $scanX += 4) {
                if ($form.HitTestCloseButton((New-Object System.Drawing.Point $scanX, $scanY))) {
                    $closeXs.Add($scanX)
                }
            }
        }
        $closeCount = $closeXs.Count
        Assert-True ($closeCount -gt 0) "$Label Close button has a non-empty hit area (scanned caption)"
        if ($closeCount -gt 0) {
            $avgX = ($closeXs | Measure-Object -Average).Average
            $mid = $form.Width / 2.0
            Write-Host "$Label Close hit X average=$([int]$avgX) width=$($form.Width) samples=$closeCount"
            if ($RtlLayout) {
                Assert-True ($avgX -lt $mid) "$Label Close is on the physical left (avg X=$([int]$avgX))"
            }
            else {
                Assert-True ($avgX -gt $mid) "$Label Close is on the physical right (avg X=$([int]$avgX))"
            }
        }

        $hrgn = [KryptonFormRtlNative]::CreateRectRgn(0, 0, 0, 0)
        try {
            $rgnType = [KryptonFormRtlNative]::GetWindowRgn($form.Handle, $hrgn)
            $leftChrome = ($rgnType -eq [KryptonFormRtlNative]::ERROR) -or [KryptonFormRtlNative]::PtInRegion($hrgn, 0, 12)
            $rightChrome = ($rgnType -eq [KryptonFormRtlNative]::ERROR) -or [KryptonFormRtlNative]::PtInRegion($hrgn, $form.Width - 1, 12)
            Write-Host "$Label GetWindowRgn type=$rgnType leftPt=$leftChrome rightPt=$rightChrome"
            Assert-True $leftChrome "$Label window region includes physical left chrome (x=0)"
            Assert-True $rightChrome "$Label window region includes physical right chrome (x=Width-1)"
        }
        finally {
            if ($hrgn -ne [IntPtr]::Zero) {
                [void][KryptonFormRtlNative]::DeleteObject($hrgn)
            }
        }
    }
    finally {
        if ($form) {
            $form.Close()
            $form.Dispose()
        }
    }
}

[System.Windows.Forms.Application]::EnableVisualStyles()
Test-FormRtlChrome -RtlLayout $false -Label 'LTR'
Test-FormRtlChrome -RtlLayout $true -Label 'RTL+Layout'

if ($failed.Count -gt 0) {
    Write-Host ("{0} assertion(s) failed." -f $failed.Count) -ForegroundColor Red
    exit 1
}

Write-Host 'KryptonForm RTL chrome assertions passed.'
exit 0
