<#
.SYNOPSIS
    Asserts #925 caption-tab tear-out and remerge without mouse synthesis.

.DESCRIPTION
    Loads Debug binaries and runs in-process STA checks:

    1. AllowTearOut=false rejects TryTearOutPages.
    2. TryTearOutPages moves the last page into a new CaptionIntegrated host.
    3. NavigatorCaptionDragPageNotify.PageDragEnd over the source drop rect remerges
       the torn page (TryRemergeAtPoint, not DragManager overlays).
    4. CloseEmptySourceWindowAfterLastTabMoved closes the empty torn window.

    Does not host NavigatorFormIntegrationDemo or send mouse input (CI-safe).
    Interactive caption drags still use Start-NavigatorFormIntegrationHost.ps1 and
    Invoke-CaptionTabDrag.ps1. Requires an STA apartment (Invoke-AllUnitTests uses -STA).

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-NavigatorCaptionTabRemerge.ps1
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

$failed = New-Object System.Collections.Generic.List[string]
$instanceFlags = [System.Reflection.BindingFlags]::Instance -bor [System.Reflection.BindingFlags]::NonPublic

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
    param(
        [Parameter(Mandatory = $false)]
        [AllowNull()]
        [object]$Value
    )
    if ($null -eq $Value) { return $null }
    if ($Value -is [System.Management.Automation.PSObject]) {
        return $Value.PSObject.BaseObject
    }
    return $Value
}

function Invoke-NetMethod {
    param(
        $Method,
        $Target,
        [System.Collections.IList]$ArgumentList
    )
    $count = 0
    if ($null -ne $ArgumentList) {
        $count = $ArgumentList.Count
    }
    $invokeArgs = New-Object 'System.Object[]' $count
    for ($i = 0; $i -lt $count; $i++) {
        $item = $ArgumentList[$i]
        if ($item -is [System.Management.Automation.PSObject]) {
            $item = $item.PSObject.BaseObject
        }
        $invokeArgs[$i] = $item
    }
    $targetObject = $Target
    if ($targetObject -is [System.Management.Automation.PSObject]) {
        $targetObject = $targetObject.PSObject.BaseObject
    }
    return $Method.Invoke($targetObject, $invokeArgs)
}

function Get-OpenKryptonForms {
    $list = New-Object System.Collections.Generic.List[System.Windows.Forms.Form]
    foreach ($open in [System.Windows.Forms.Application]::OpenForms) {
        $form = Get-NetObject -Value $open
        if ($form -is [Krypton.Toolkit.KryptonForm] -and -not $form.IsDisposed) {
            $list.Add($form)
        }
    }
    return $list
}

function Invoke-UnitTestDoEvents {
    param([int]$Times = 3)
    for ($i = 0; $i -lt $Times; $i++) {
        [System.Windows.Forms.Application]::DoEvents()
        Start-Sleep -Milliseconds 50
    }
}

Write-UnitTestBanner -Status INFO -Message 'Asserting #925 caption-tab tear-out and remerge'

[System.Windows.Forms.Application]::EnableVisualStyles()
[System.Windows.Forms.Application]::SetCompatibleTextRenderingDefault($false)

$form = $null
$integrator = $null
try {
    $form = Get-NetObject -Value (New-Object Krypton.Toolkit.KryptonForm)
    $form.Text = 'UnitTest-925-CaptionTabRemerge'
    $form.ShowInTaskbar = $false
    $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $form.Location = New-Object System.Drawing.Point(-32000, -32000)
    $form.Size = New-Object System.Drawing.Size(900, 600)

    $nav = Get-NetObject -Value (New-Object Krypton.Navigator.KryptonNavigator)
    $nav.Dock = [System.Windows.Forms.DockStyle]::Fill
    $nav.AllowPageDrag = $true
    [void]$form.Controls.Add($nav)

    foreach ($spec in @(
            @{ Text = 'Home'; UniqueName = 'page-home' },
            @{ Text = 'Docs'; UniqueName = 'page-docs' },
            @{ Text = 'Settings'; UniqueName = 'page-settings' }
        )) {
        $page = Get-NetObject -Value (New-Object Krypton.Navigator.KryptonPage)
        $page.Text = $spec.Text
        $page.UniqueName = $spec.UniqueName
        [void]$nav.Pages.Add($page)
    }

    $integrator = Get-NetObject -Value (New-Object Krypton.Navigator.Utilities.KryptonNavigatorFormIntegrator)
    $integrator.Form = $form
    $integrator.Navigator = $nav
    $integrator.Mode = [Krypton.Navigator.Utilities.NavigatorFormIntegrationMode]::CaptionIntegrated
    $integrator.AllowTearOut = $true
    $integrator.CloseEmptySourceWindowAfterLastTabMoved = $true
    $integrator.Enabled = $true

    [void]$form.Show()
    Invoke-UnitTestDoEvents

    Assert-True $integrator.IsIntegrated 'CaptionIntegrated integrator applied'
    Assert-Equal 3 $nav.Pages.Count 'Source navigator starts with three pages'

    $tryTearOut = $integrator.GetType().GetMethod('TryTearOutPages', $instanceFlags)
    Assert-True ($null -ne $tryTearOut) 'TryTearOutPages is available via reflection'
    $containsDrop = $integrator.GetType().GetMethod('ContainsDropTarget', $instanceFlags)
    Assert-True ($null -ne $containsDrop) 'ContainsDropTarget is available via reflection'
    $getRegistered = $integrator.GetType().GetMethod('GetRegisteredIntegrators', $instanceFlags)
    Assert-True ($null -ne $getRegistered) 'GetRegisteredIntegrators is available via reflection'

    $settings = Get-NetObject -Value $nav.Pages['page-settings']
    Assert-True ($null -ne $settings) 'Settings page resolved by UniqueName'
    $nav.SelectedPage = $settings

    $integrator.AllowTearOut = $false
    $blockedPages = New-Object Krypton.Navigator.KryptonPageCollection
    [void]$blockedPages.Add($settings)
    $blockedArgs = New-Object 'System.Collections.Generic.List[object]'
    [void]$blockedArgs.Add($nav)
    [void]$blockedArgs.Add($blockedPages)
    [void]$blockedArgs.Add((New-Object System.Drawing.Point 400, 300))
    $blocked = [bool](Invoke-NetMethod -Method $tryTearOut -Target $integrator -ArgumentList $blockedArgs)
    Assert-Equal $false $blocked 'TryTearOutPages returns false when AllowTearOut is false'
    Assert-Equal 3 $nav.Pages.Count 'AllowTearOut=false does not move pages'
    Assert-Equal 1 @(Get-OpenKryptonForms).Count 'AllowTearOut=false does not create a second window'
    $integrator.AllowTearOut = $true

    $dragPages = New-Object Krypton.Navigator.KryptonPageCollection
    [void]$dragPages.Add($settings)
    $tearArgs = New-Object 'System.Collections.Generic.List[object]'
    [void]$tearArgs.Add($nav)
    [void]$tearArgs.Add($dragPages)
    [void]$tearArgs.Add((New-Object System.Drawing.Point 400, 300))
    $torn = [bool](Invoke-NetMethod -Method $tryTearOut -Target $integrator -ArgumentList $tearArgs)
    Assert-True $torn 'TryTearOutPages returns true for Settings'
    if ($nav.Pages.Contains($settings)) {
        [void]$nav.Pages.Remove($settings)
    }
    Invoke-UnitTestDoEvents

    Assert-Equal 2 $nav.Pages.Count 'Source navigator has two pages after tear-out'
    Assert-True (-not $nav.Pages.Contains($settings)) 'Settings is no longer on the source navigator'

    $openAfterTear = @(Get-OpenKryptonForms)
    Assert-Equal 2 $openAfterTear.Count 'Tear-out created a second KryptonForm'

    $tornForm = $null
    foreach ($candidate in $openAfterTear) {
        if (-not [object]::ReferenceEquals($candidate, $form)) {
            $tornForm = $candidate
            break
        }
    }
    Assert-True ($null -ne $tornForm) 'Torn KryptonForm resolved'

    $tornIntegrator = $null
    $tornNav = $null
    foreach ($item in $getRegistered.Invoke($integrator, [object[]]@())) {
        $reg = Get-NetObject -Value $item
        $regNav = Get-NetObject -Value $reg.Navigator
        if ($null -ne $regNav -and -not [object]::ReferenceEquals($regNav, $nav)) {
            $tornIntegrator = $reg
            $tornNav = $regNav
            break
        }
    }
    Assert-True ($null -ne $tornIntegrator) 'Torn integrator is registered'
    Assert-True ($null -ne $tornNav) 'Torn navigator resolved'
    Assert-Equal 1 $tornNav.Pages.Count 'Torn navigator hosts the Settings page'
    Assert-Equal 'page-settings' $tornNav.Pages[0].UniqueName 'Torn page UniqueName is Settings'

    $notify = Get-NetObject -Value ($tornIntegrator.GetType().GetField('_captionDragNotify', $instanceFlags).GetValue($tornIntegrator))
    Assert-True ($null -ne $notify) 'Torn CaptionIntegrated host has NavigatorCaptionDragPageNotify'

    $remergePages = New-Object Krypton.Navigator.KryptonPageCollection
    [void]$remergePages.Add((Get-NetObject -Value $tornNav.Pages[0]))
    $notifyType = $notify.GetType()
    $notifyType.GetField('_sourceNavigator', $instanceFlags).SetValue($notify, $tornNav)
    $notifyType.GetField('_draggingPages', $instanceFlags).SetValue($notify, $remergePages)
    $notifyType.GetField('_sourceForm', $instanceFlags).SetValue($notify, $tornForm)

    $clientCenter = New-Object System.Drawing.Point ([int]($form.ClientSize.Width / 2), [int]($form.ClientSize.Height / 2))
    $dropPoint = $form.PointToScreen($clientCenter)
    $dropArgs = New-Object 'System.Collections.Generic.List[object]'
    [void]$dropArgs.Add($dropPoint)
    if (-not [bool](Invoke-NetMethod -Method $containsDrop -Target $integrator -ArgumentList $dropArgs)) {
        $dropPoint = New-Object System.Drawing.Point ([int]($form.Bounds.X + 80), [int]($form.Bounds.Y + 40))
        $dropArgs = New-Object 'System.Collections.Generic.List[object]'
        [void]$dropArgs.Add($dropPoint)
    }
    Assert-True ([bool](Invoke-NetMethod -Method $containsDrop -Target $integrator -ArgumentList $dropArgs)) 'Source integrator drop rect contains the remerge point'

    $pointArgs = New-Object Krypton.Toolkit.PointEventArgs $dropPoint
    $endArgs = New-Object 'System.Collections.Generic.List[object]'
    [void]$endArgs.Add($tornNav)
    [void]$endArgs.Add($pointArgs)
    $dropped = [bool](Invoke-NetMethod -Method $notifyType.GetMethod('PageDragEnd') -Target $notify -ArgumentList $endArgs)
    Assert-True $dropped 'PageDragEnd remerges Settings onto the source window'
    foreach ($page in @($remergePages)) {
        if ($tornNav.Pages.Contains($page)) {
            [void]$tornNav.Pages.Remove($page)
        }
    }
    Invoke-UnitTestDoEvents -Times 8

    Assert-Equal 3 $nav.Pages.Count 'Source navigator has three pages after remerge'
    Assert-True $nav.Pages.Contains($settings) 'Settings is back on the source navigator'
    Assert-True ($tornForm.IsDisposed -or -not $tornForm.Visible) 'Empty torn window closed after last tab moved'

    $openAfterRemerge = @(Get-OpenKryptonForms)
    Assert-Equal 1 $openAfterRemerge.Count 'Remerge leaves a single KryptonForm'
}
catch {
    $failed.Add("Exception: $($_.Exception.Message)")
    Write-Host "Exception: $($_.Exception)" -ForegroundColor Red
}
finally {
    if ($integrator -and -not $integrator.IsDisposed) {
        $integrator.Dispose()
    }
    foreach ($open in @([System.Windows.Forms.Application]::OpenForms)) {
        $candidate = Get-NetObject -Value $open
        if ($candidate -and -not $candidate.IsDisposed) {
            try { $candidate.Close(); $candidate.Dispose() } catch { }
        }
    }
}

if ($failed.Count -gt 0) {
    Write-Host ""
    Write-Host "$($failed.Count) assertion(s) failed." -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host 'All #925 caption-tab remerge assertions passed.' -ForegroundColor Green
exit 0
