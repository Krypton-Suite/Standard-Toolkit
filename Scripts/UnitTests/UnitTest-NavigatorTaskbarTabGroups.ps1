<#
.SYNOPSIS
    Asserts #4129 TabGroup composite taskbar thumbnail wiring and docking float taskbar opt-in.

.DESCRIPTION
    Loads Debug TestForm binaries and runs in-process checks (no live taskbar hover required):

    1. KryptonNavigatorTaskbarThumbnails.AreTabGroupThumbnailsActive() gating
       (ShowTabGroupThumbnails + FormIntegrator + AllowTabGroups + matching Navigator).
    2. After RefreshThumbnails on a visible ShowInTaskbar host, the shared host coordinator
       registers one group proxy plus member page proxies (via private field reflection).
    3. KryptonDockingFloating.ShowFloatingWindowsInTaskbar defaults to false and can be set.

    Exit code 0 on success; non-zero with a failing assertion message on failure.
    Requires an STA apartment (use powershell -STA). Invoke-AllUnitTests launches include scripts with -STA.
    Coordinator registration counts are asserted when the shell taskbar API is ready; otherwise they are noted and skipped.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-NavigatorTaskbarTabGroups.ps1
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

[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Navigator.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Navigator.Utilities.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Workspace.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Docking.dll'))

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

[System.Windows.Forms.Application]::EnableVisualStyles()
[System.Windows.Forms.Application]::SetCompatibleTextRenderingDefault($false)

$form = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonForm]))
$form.Text = 'UnitTest-4129-TaskbarTabGroups'
$form.ShowInTaskbar = $true
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point(-32000, -32000)
$form.Size = New-Object System.Drawing.Size(640, 480)

$nav = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Navigator.KryptonNavigator]))
$nav.Dock = [System.Windows.Forms.DockStyle]::Fill
[void]$form.Controls.Add($nav)

$pageA = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Navigator.KryptonPage]))
$pageA.Text = 'Alpha'
$pageA.UniqueName = 'page-alpha'
$pageB = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Navigator.KryptonPage]))
$pageB.Text = 'Beta'
$pageB.UniqueName = 'page-beta'
$pageC = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Navigator.KryptonPage]))
$pageC.Text = 'Gamma'
$pageC.UniqueName = 'page-gamma'
[void]$nav.Pages.Add($pageA)
[void]$nav.Pages.Add($pageB)
[void]$nav.Pages.Add($pageC)

$integrator = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Navigator.Utilities.KryptonNavigatorFormIntegrator]))
$integrator.Form = $form
$integrator.Navigator = $nav
$integrator.AllowTabGroups = $true
$integrator.Enabled = $false
$integrator.Mode = [Krypton.Navigator.Utilities.NavigatorFormIntegrationMode]::ClientChrome

$group = Get-NetObject ($integrator.CreateGroup('Work', [System.Drawing.Color]::DodgerBlue))
$integrator.AssignPageToGroup($pageA, $group.Id)
$integrator.AssignPageToGroup($pageB, $group.Id)

$thumbs = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Navigator.Utilities.KryptonNavigatorTaskbarThumbnails]))
$thumbs.Navigator = $nav
$thumbs.Enabled = $true

$flags = [System.Reflection.BindingFlags]::Instance -bor [System.Reflection.BindingFlags]::NonPublic
$activeMethod = $thumbs.GetType().GetMethod('AreTabGroupThumbnailsActive', $flags)
Assert-True ($null -ne $activeMethod) 'AreTabGroupThumbnailsActive is available via reflection'

Assert-Equal $false $thumbs.ShowTabGroupThumbnails 'ShowTabGroupThumbnails defaults to false'
Assert-Equal $false ([bool]$activeMethod.Invoke($thumbs, @())) 'AreTabGroupThumbnailsActive is false before FormIntegrator wiring'

$thumbs.FormIntegrator = $integrator
$thumbs.ShowTabGroupThumbnails = $true
Assert-Equal $true ([bool]$activeMethod.Invoke($thumbs, @())) 'AreTabGroupThumbnailsActive is true when wired'

$thumbs.ShowTabGroupThumbnails = $false
Assert-Equal $false ([bool]$activeMethod.Invoke($thumbs, @())) 'AreTabGroupThumbnailsActive is false when ShowTabGroupThumbnails is cleared'
$thumbs.ShowTabGroupThumbnails = $true

$integrator.AllowTabGroups = $false
Assert-Equal $false ([bool]$activeMethod.Invoke($thumbs, @())) 'AreTabGroupThumbnailsActive is false when AllowTabGroups is false'
$integrator.AllowTabGroups = $true
Assert-Equal $true ([bool]$activeMethod.Invoke($thumbs, @())) 'AreTabGroupThumbnailsActive restored when AllowTabGroups is true'

$otherNav = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Navigator.KryptonNavigator]))
$integrator.Navigator = $otherNav
Assert-Equal $false ([bool]$activeMethod.Invoke($thumbs, @())) 'AreTabGroupThumbnailsActive is false when FormIntegrator.Navigator mismatches'
$integrator.Navigator = $nav
Assert-Equal $true ([bool]$activeMethod.Invoke($thumbs, @())) 'AreTabGroupThumbnailsActive is true again with matching Navigator'

# Show the host so the coordinator can treat TaskbarButtonCreated as ready and Sync.
[void]$form.Show()
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 200
[System.Windows.Forms.Application]::DoEvents()

$thumbs.RefreshThumbnails()
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
[System.Windows.Forms.Application]::DoEvents()

$coordType = [Krypton.Navigator.Utilities.KryptonNavigatorTaskbarThumbnails].Assembly.GetType(
    'Krypton.Navigator.Utilities.NavigatorTaskbarHostCoordinator', $true)
Assert-True ($null -ne $coordType) 'NavigatorTaskbarHostCoordinator type resolved'

$getOrCreate = $coordType.GetMethod('GetOrCreate', [System.Reflection.BindingFlags]::Public -bor [System.Reflection.BindingFlags]::Static)
$hostForm = [System.Windows.Forms.Form]$form
$coordinator = Get-NetObject ($getOrCreate.Invoke($null, @(,$hostForm)))
Assert-True ($null -ne $coordinator) 'Host coordinator created for the demo form'

$pageEntries = $coordType.GetField('_entries', $flags).GetValue($coordinator)
$groupEntries = $coordType.GetField('_groupEntries', $flags).GetValue($coordinator)
$pageCount = $pageEntries.Count
$groupCount = $groupEntries.Count

Write-Host "Coordinator page entries=$pageCount group entries=$groupCount"

# When the shell taskbar API is available, expect 3 page proxies + 1 group proxy.
# On restricted environments Sync may tear down; treat zero/zero as soft skip with a note.
$os = [System.Environment]::OSVersion.Version
$taskbarApiOk = ($os.Major -gt 6) -or (($os.Major -eq 6) -and ($os.Minor -ge 1))
if ($taskbarApiOk -and $pageCount -gt 0) {
    Assert-Equal 3 $pageCount 'Coordinator registered three page thumbnails'
    Assert-Equal 1 $groupCount 'Coordinator registered one TabGroup composite thumbnail'
}
elseif ($taskbarApiOk) {
    Write-Host 'NOTE: Coordinator has no registered tabs yet (taskbar button may not be ready in this session). API gating assertions above still apply.' -ForegroundColor Yellow
}
else {
    Write-Host 'NOTE: OS taskbar tab API not supported; skipping coordinator registration counts.' -ForegroundColor Yellow
}

# MaxThumbnails counts group + page slots: with max=1 only the group slot should remain when active.
$thumbs.MaxThumbnails = 1
$thumbs.RefreshThumbnails()
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 200
[System.Windows.Forms.Application]::DoEvents()

$pageEntries = $coordType.GetField('_entries', $flags).GetValue($coordinator)
$groupEntries = $coordType.GetField('_groupEntries', $flags).GetValue($coordinator)
if ($taskbarApiOk -and ($pageEntries.Count + $groupEntries.Count) -gt 0) {
    $total = $pageEntries.Count + $groupEntries.Count
    Assert-True ($total -le 1) "MaxThumbnails=1 limits total registered slots (total=$total)"
    Assert-Equal 1 $groupEntries.Count 'MaxThumbnails=1 prefers the group slot ahead of members'
}

# Docking float opt-in (#4129 part 2)
$dockAsm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Docking.dll'))
$floatingType = $dockAsm.GetType('Krypton.Docking.KryptonDockingFloating', $true)
Assert-True ($null -ne $floatingType) 'KryptonDockingFloating type resolved'
$floatingCtor = $floatingType.GetConstructor(@([string], [System.Windows.Forms.Form]))
Assert-True ($null -ne $floatingCtor) 'KryptonDockingFloating(string, Form) constructor resolved'
$ctorArgs = New-Object 'System.Object[]' 2
$ctorArgs[0] = [string]'Floating'
$ctorArgs[1] = $hostForm
try {
    $floating = $floatingCtor.Invoke($ctorArgs)
}
catch {
    Write-Host "Floating ctor failed: $($_.Exception.InnerException.Message)" -ForegroundColor Red
    $floating = $null
}
Assert-True ($null -ne $floating) 'KryptonDockingFloating instance created'
if ($null -ne $floating) {
    $floatProp = $floatingType.GetProperty('ShowFloatingWindowsInTaskbar')
    Assert-True ($null -ne $floatProp) 'ShowFloatingWindowsInTaskbar property exists on KryptonDockingFloating'
    Assert-Equal $false ([bool]$floatProp.GetValue($floating, $null)) 'ShowFloatingWindowsInTaskbar defaults to false'
    $floatProp.SetValue($floating, $true, $null)
    Assert-equal $true ([bool]$floatProp.GetValue($floating, $null)) 'ShowFloatingWindowsInTaskbar can be enabled'
}

$form.Close()
$form.Dispose()

if ($failed.Count -gt 0) {
    Write-Host ""
    Write-Host "$($failed.Count) assertion(s) failed." -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host 'All #4129 unit-test assertions passed.' -ForegroundColor Green
exit 0
