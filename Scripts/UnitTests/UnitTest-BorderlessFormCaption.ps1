<#
.SYNOPSIS
    Asserts #2922 borderless KryptonForm does not keep WS_CAPTION after show.

.DESCRIPTION
    Shows a top-level FormBorderStyle.None KryptonForm and an MDI Dock.Fill child
    (the remaining #2922 case). After Show, WS_CAPTION must be clear, client height
    must match window height (no system caption band), and MdiChildActivate must fire.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-BorderlessFormCaption.ps1
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
public static class BorderlessCaptionNative {
    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtrW")]
    public static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);
    [DllImport("user32.dll", EntryPoint = "GetWindowLongW")]
    public static extern int GetWindowLong32(IntPtr hWnd, int nIndex);
    public const int GWL_STYLE = -16;
    public const int GWL_EXSTYLE = -20;
    public const uint WS_CAPTION = 0x00C00000;
    public const uint WS_EX_CLIENTEDGE = 0x00000200;
    public static uint GetStyle(IntPtr hWnd) {
        if (IntPtr.Size == 8) {
            return unchecked((uint)GetWindowLongPtr64(hWnd, GWL_STYLE).ToInt64());
        }
        return unchecked((uint)GetWindowLong32(hWnd, GWL_STYLE));
    }
    public static uint GetExStyle(IntPtr hWnd) {
        if (IntPtr.Size == 8) {
            return unchecked((uint)GetWindowLongPtr64(hWnd, GWL_EXSTYLE).ToInt64());
        }
        return unchecked((uint)GetWindowLong32(hWnd, GWL_EXSTYLE));
    }
}
"@

[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$testFormAsm = Join-Path $bin 'TestForm.exe'
if (-not (Test-Path -LiteralPath $testFormAsm)) {
    throw "TestForm.exe not found in $bin. Build TestForm first."
}
[void][System.Reflection.Assembly]::LoadFrom($testFormAsm)

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

function Assert-NoSystemCaption {
    param(
        [System.Windows.Forms.Form]$Form,
        [string]$Label
    )

    $style = [BorderlessCaptionNative]::GetStyle($Form.Handle)
    $hasCaption = ($style -band [BorderlessCaptionNative]::WS_CAPTION) -ne 0
    Assert-True (-not $hasCaption) "$Label WS_CAPTION is clear (style=0x$('{0:X8}' -f $style))"
    Assert-True ($Form.FormBorderStyle -eq [System.Windows.Forms.FormBorderStyle]::None) "$Label FormBorderStyle is None"
    Assert-True ($Form.Height -eq $Form.ClientSize.Height) "$Label window height equals client height (no caption band); Height=$($Form.Height) Client=$($Form.ClientSize.Height)"
    $ex = [BorderlessCaptionNative]::GetExStyle($Form.Handle)
    Assert-True (($ex -band [BorderlessCaptionNative]::WS_EX_CLIENTEDGE) -eq 0) "$Label WS_EX_CLIENTEDGE is clear (ex=0x$('{0:X8}' -f $ex))"
    $borders = $Form.RealWindowBorders
    Assert-True (($borders.Left -eq 0) -and ($borders.Top -eq 0) -and ($borders.Right -eq 0) -and ($borders.Bottom -eq 0)) "$Label RealWindowBorders is empty ($borders)"
}

[System.Windows.Forms.Application]::EnableVisualStyles()

$top = New-Object Krypton.Toolkit.KryptonForm
try {
    $top.FormBorderStyle = [System.Windows.Forms.FormBorderStyle]::None
    $top.Text = 'Borderless top-level #2922'
    $top.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $top.Location = New-Object System.Drawing.Point 40, 40
    $top.Size = New-Object System.Drawing.Size 400, 200
    $top.Show()
    [System.Windows.Forms.Application]::DoEvents()
    Assert-NoSystemCaption -Form $top -Label 'Top-level'
}
finally {
    $top.Close()
    $top.Dispose()
}

$hostType = [TestForm.BorderlessMdiHostDemo]
$hostForm = [System.Activator]::CreateInstance($hostType)
try {
    $hostForm.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $hostForm.Location = New-Object System.Drawing.Point 40, 40
    $hostForm.Show()
    $hostForm.Activate()
    [System.Windows.Forms.Application]::DoEvents()

    $mdiClient = $null
    foreach ($c in $hostForm.Controls) {
        if ($c -is [System.Windows.Forms.MdiClient]) { $mdiClient = $c; break }
    }
    Assert-True ($null -ne $mdiClient) 'Host has an MdiClient'
    if ($null -ne $mdiClient) {
        $mdiExBefore = [BorderlessCaptionNative]::GetExStyle($mdiClient.Handle)
        Assert-True (($mdiExBefore -band [BorderlessCaptionNative]::WS_EX_CLIENTEDGE) -eq 0) "MdiClient WS_EX_CLIENTEDGE stripped before child create (ex=0x$('{0:X8}' -f $mdiExBefore))"
    }

    $child = $hostForm.OpenDockFillChild()
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 50
    [System.Windows.Forms.Application]::DoEvents()

    Assert-True ($child.IsMdiChild) 'Dock.Fill child IsMdiChild'
    Assert-True ($child.Dock -eq [System.Windows.Forms.DockStyle]::Fill) 'Child Dock is Fill'
    Assert-NoSystemCaption -Form $child -Label 'MDI Dock.Fill child'
    Assert-True ($hostForm.MdiChildActivateCount -ge 1) "MdiChildActivate fired (count=$($hostForm.MdiChildActivateCount))"

    $mdiClient = $null
    foreach ($c in $hostForm.Controls) {
        if ($c -is [System.Windows.Forms.MdiClient]) { $mdiClient = $c; break }
    }
    Assert-True ($null -ne $mdiClient) 'Host has an MdiClient'
    if ($null -ne $mdiClient) {
        $mdiEx = [BorderlessCaptionNative]::GetExStyle($mdiClient.Handle)
        Assert-True (($mdiEx -band [BorderlessCaptionNative]::WS_EX_CLIENTEDGE) -eq 0) "MdiClient WS_EX_CLIENTEDGE stripped while Dock.Fill None child is visible (ex=0x$('{0:X8}' -f $mdiEx))"
    }
}
finally {
    $hostForm.Close()
    $hostForm.Dispose()
}

if ($failed.Count -gt 0) {
    Write-Host "$($failed.Count) assertion(s) failed." -ForegroundColor Red
    exit 1
}

Write-Host 'All #2922 borderless caption assertions passed.' -ForegroundColor Green
exit 0
