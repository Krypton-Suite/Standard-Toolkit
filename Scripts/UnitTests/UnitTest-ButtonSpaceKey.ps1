<#
.SYNOPSIS
    Asserts #4147: Space activates a focused KryptonButton when ActiveView is null (no mouse hover).

.DESCRIPTION
    Loads Debug Krypton.Toolkit binaries and runs an in-process STA check:

    1. Host a KryptonButton on an off-screen form.
    2. Clear ViewManager.ActiveView (simulates mouse not over the control).
    3. Send Space KeyDown/KeyUp through ViewManager.
    4. Assert Click fires; repeat with ActiveView set to the button root (hover path).

    Exit code 0 on success; non-zero on failure.
    Requires an STA apartment (use powershell -STA). Invoke-AllUnitTests launches include scripts with -STA.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-ButtonSpaceKey.ps1
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

function Assert-Equal {
    param($Expected, $Actual, [string]$Message)
    if (-not [object]::Equals($Expected, $Actual)) {
        $failed.Add("$Message (expected='$Expected' actual='$Actual')")
        Write-Host "FAIL: $Message (expected='$Expected' actual='$Actual')" -ForegroundColor Red
    }
    else {
        Write-Host "PASS: $Message" -ForegroundColor Green
    }
}

function Get-NetObject {
    param($Value)
    if ($null -eq $Value) { return $null }
    if ($Value -is [System.Management.Automation.PSObject]) {
        return $Value.PSObject.BaseObject
    }
    return $Value
}

function Invoke-ButtonSpaceClick {
    param(
        $ViewManager,
        [bool]$WithActiveView
    )

    $vm = Get-NetObject $ViewManager
    if ($WithActiveView) {
        $vm.ActiveView = $vm.Root
    }
    else {
        $vm.ActiveView = $null
    }

    $down = New-Object System.Windows.Forms.KeyEventArgs ([System.Windows.Forms.Keys]::Space)
    $up = New-Object System.Windows.Forms.KeyEventArgs ([System.Windows.Forms.Keys]::Space)
    $vm.KeyDown($down)
    $vm.KeyUp($up)
}

Write-UnitTestBanner -Status INFO -Message 'Asserting #4147 KryptonButton Space without mouse hover'

[System.Windows.Forms.Application]::EnableVisualStyles()
[System.Windows.Forms.Application]::SetCompatibleTextRenderingDefault($false)

$form = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonForm]))
$form.Text = 'UnitTest-4147-ButtonSpaceKey'
$form.ShowInTaskbar = $false
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point(-32000, -32000)
$form.Size = New-Object System.Drawing.Size(320, 160)

$button = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonButton]))
$button.Text = 'Space target'
$button.Dock = [System.Windows.Forms.DockStyle]::Top
$button.Height = 40
[void]$form.Controls.Add($button)

$script:clickCount = 0
$handler = [System.EventHandler]{ param($s, $e) $script:clickCount++ }
$button.add_Click($handler)

[void]$form.Show()
[System.Windows.Forms.Application]::DoEvents()
[void]$button.Focus()
[System.Windows.Forms.Application]::DoEvents()

$vm = Get-NetObject $button.ViewManager
Assert-True ($null -ne $vm) 'KryptonButton.ViewManager is available'
Assert-True ($null -ne $vm.Root) 'ViewManager.Root is available'
Assert-True ($null -ne $vm.Root.KeyController) 'View root exposes child KeyController (pulsing-border decorator)'

$script:clickCount = 0
Invoke-ButtonSpaceClick -ViewManager $vm -WithActiveView:$false
Assert-Equal 1 $script:clickCount 'Space KeyDown/KeyUp clicks when ActiveView is null (no hover)'

$script:clickCount = 0
Invoke-ButtonSpaceClick -ViewManager $vm -WithActiveView:$true
Assert-Equal 1 $script:clickCount 'Space KeyDown/KeyUp clicks when ActiveView is the button root (hover path)'

$button.remove_Click($handler)
$form.Close()
$form.Dispose()

if ($failed.Count -gt 0) {
    Write-UnitTestBanner -Status FAIL -Message ("#4147 assertions failed ($($failed.Count))")
    exit 1
}

Write-UnitTestBanner -Status PASS -Message '#4147 KryptonButton Space key assertions passed'
exit 0
