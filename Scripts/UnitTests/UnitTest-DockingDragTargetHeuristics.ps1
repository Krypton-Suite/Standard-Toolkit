<#
.SYNOPSIS
    Asserts #3858: docking drag-target first-match priority and DragViewController Escape cancel.

.DESCRIPTION
    Loads Debug Krypton.Toolkit / Krypton.Navigator binaries and runs in-process STA checks:

    1. DragViewController: Escape KeyUp after capture returns false (not a Contains hover probe)
       and raises DragQuit when a drag was in progress.
    2. DragViewController: LostFocus while captured (not dragging) clears capture without error.
    3. DragFeedbackSolid.FindTarget: overlapping HotRect targets favour the first target in
       list order (control-edge before nested cell); reverse the list and first-match follows.
    4. DragFeedbackDocking: unused FindTarget helper is gone; Feedback API remains.

    Does not call DragFeedback.Start (DropSolidWindow / docking indicators) to stay CI-safe.
    Requires an STA apartment (use powershell -STA). Invoke-AllUnitTests launches include scripts with -STA.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-DockingDragTargetHeuristics.ps1
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

$interopPath = Join-Path $bin 'Krypton.Interop.dll'
$toolkitPath = Join-Path $bin 'Krypton.Toolkit.dll'
$navigatorPath = Join-Path $bin 'Krypton.Navigator.dll'

[void][System.Reflection.Assembly]::LoadFrom($interopPath)
[void][System.Reflection.Assembly]::LoadFrom($toolkitPath)
[void][System.Reflection.Assembly]::LoadFrom($navigatorPath)

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

# Concrete DragTarget for first-match asserts (Add-Type needs already-loaded assemblies).
$drawingAsm = [System.Drawing.Point].Assembly.Location
$formsAsm = [System.Windows.Forms.Form].Assembly.Location
Add-Type -ReferencedAssemblies @($navigatorPath, $toolkitPath, $interopPath, $drawingAsm, $formsAsm) -TypeDefinition @'
using System;
using System.Drawing;
using Krypton.Navigator;
using Krypton.Toolkit;

public sealed class UnitTest3858DragTarget : DragTarget
{
    public string Tag { get; private set; }

    public UnitTest3858DragTarget(string tag, Rectangle screenRect, Rectangle hotRect, Rectangle drawRect, DragTargetHint hint)
        : base(screenRect, hotRect, drawRect, hint, KryptonPageFlags.All)
    {
        Tag = tag;
    }

    public override bool PerformDrop(Point screenPt, PageDragEndData dragEndData)
    {
        return false;
    }
}
'@

Write-UnitTestBanner -Status INFO -Message 'Asserting #3858 drag-target heuristics and DragViewController cancel'

[System.Windows.Forms.Application]::EnableVisualStyles()
[System.Windows.Forms.Application]::SetCompatibleTextRenderingDefault($false)

$form = New-Object System.Windows.Forms.Form
$form.Text = 'UnitTest-3858-DockingDragTargetHeuristics'
$form.ShowInTaskbar = $false
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point(-32000, -32000)
$form.Size = New-Object System.Drawing.Size(400, 300)
[void]$form.Show()
[System.Windows.Forms.Application]::DoEvents()

# ----- DragViewController: Escape after capture -----
# Use a hosted KryptonButton view so OwningControl / ClientRectangle are already wired.
$button = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonButton]))
$button.Text = 'Drag target'
$button.Dock = [System.Windows.Forms.DockStyle]::Top
$button.Height = 40
[void]$form.Controls.Add($button)
[System.Windows.Forms.Application]::DoEvents()

$vm = Get-NetObject $button.ViewManager
$root = $vm.Root
if ($root -is [System.Management.Automation.PSObject]) {
    $root = $root.PSObject.BaseObject
}
Assert-True ($null -ne $root) 'KryptonButton view root is available'
Assert-True ($null -ne $root.OwningControl) 'View root OwningControl is set'

$ctor = [Krypton.Navigator.DragViewController].GetConstructor([type[]]@([Krypton.Toolkit.ViewBase]))
Assert-True ($null -ne $ctor) 'DragViewController(ViewBase) constructor is available'
# ViewBase is enumerable of children; use unary comma so PowerShell does not unroll it.
$controller = Get-NetObject ($ctor.Invoke([object[]](, $root)))
$script:dragQuitCount = 0
$quitHandler = [System.EventHandler]{ param($s, $e) $script:dragQuitCount++ }
$controller.add_DragQuit($quitHandler)

$startHandler = [System.EventHandler[Krypton.Toolkit.DragStartEventCancelArgs]]{
    param($s, $e)
    $e.Cancel = $false
}
$controller.add_DragStart($startHandler)

$hostControl = Get-NetObject $root.OwningControl
[void]$controller.MouseDown($hostControl, (New-Object System.Drawing.Point(10, 10)), [System.Windows.Forms.MouseButtons]::Left)

# Move outside SystemInformation.DragSize to start dragging.
$dragSize = [System.Windows.Forms.SystemInformation]::DragSize
$movePt = New-Object System.Drawing.Point((10 + $dragSize.Width + 8), (10 + $dragSize.Height + 8))
$controller.MouseMove($hostControl, $movePt)
[System.Windows.Forms.Application]::DoEvents()

$escape = New-Object System.Windows.Forms.KeyEventArgs ([System.Windows.Forms.Keys]::Escape)
$escapeResult = [bool]$controller.KeyUp($hostControl, $escape)
Assert-True ($escapeResult -eq $false) 'Escape KeyUp returns false after releasing capture (not ClientRectangle.Contains)'
Assert-Equal 1 $script:dragQuitCount 'Escape during drag raises DragQuit once'

$controller.remove_DragQuit($quitHandler)
$controller.remove_DragStart($startHandler)

# ----- DragViewController: LostFocus while captured, not dragging -----
$controller2 = Get-NetObject ($ctor.Invoke([object[]](, $root)))
$controller2.AllowDragging = $false
[void]$controller2.MouseDown($hostControl, (New-Object System.Drawing.Point(5, 5)), [System.Windows.Forms.MouseButtons]::Left)
$lostFocusOk = $true
try {
    $controller2.LostFocus($hostControl)
}
catch {
    $lostFocusOk = $false
}
Assert-True $lostFocusOk 'LostFocus while captured (not dragging) does not throw'

# ----- DragFeedbackSolid: first overlapping HotRect wins (no DropSolidWindow) -----
# Avoid DragFeedback*.Start — DropSolidWindow/ShowWindow can AV in headless CI.
# Drive protected FindTarget via reflection after setting DragTargets.
$pages = New-Object Krypton.Navigator.KryptonPageCollection
if ($pages -is [System.Management.Automation.PSObject]) {
    $pages = $pages.PSObject.BaseObject
}
Assert-True ($null -ne $pages) 'KryptonPageCollection constructed'
$pageCtor = [Krypton.Navigator.PageDragEndData].GetConstructor([type[]]@(
        [object],
        [Krypton.Navigator.KryptonPageCollection]))
Assert-True ($null -ne $pageCtor) 'PageDragEndData(object, KryptonPageCollection) constructor is available'
$dragData = $pageCtor.Invoke([object[]]@([object]'unit-test-3858', $pages))
if ($dragData -is [System.Management.Automation.PSObject]) {
    $dragData = $dragData.PSObject.BaseObject
}
Assert-True ($null -ne $dragData) 'PageDragEndData constructed'

$edgeRect = New-Object System.Drawing.Rectangle(100, 100, 200, 200)
$cellRect = New-Object System.Drawing.Rectangle(150, 150, 80, 80)
$overlapPt = New-Object System.Drawing.Point(160, 160)

$edgeTarget = New-Object UnitTest3858DragTarget('edge', $edgeRect, $edgeRect, $edgeRect, [Krypton.Navigator.DragTargetHint]::EdgeLeft)
$cellTarget = New-Object UnitTest3858DragTarget('cell', $cellRect, $cellRect, $cellRect, [Krypton.Navigator.DragTargetHint]::Transfer)
Assert-True ($null -ne $edgeTarget) 'Edge UnitTest3858DragTarget constructed'
Assert-True ($null -ne $cellTarget) 'Cell UnitTest3858DragTarget constructed'

$targets = New-Object Krypton.Navigator.DragTargetList
if ($targets -is [System.Management.Automation.PSObject]) {
    $targets = $targets.PSObject.BaseObject
}
[void]$targets.Add($edgeTarget)
[void]$targets.Add($cellTarget)
Assert-Equal 2 $targets.Count 'DragTargetList contains two overlapping targets'

$solidFeedback = New-Object Krypton.Navigator.DragFeedbackSolid
if ($solidFeedback -is [System.Management.Automation.PSObject]) {
    $solidFeedback = $solidFeedback.PSObject.BaseObject
}
Assert-True ($null -ne $solidFeedback) 'DragFeedbackSolid constructed'

$dragTargetsProp = [Krypton.Navigator.DragFeedback].GetProperty(
    'DragTargets',
    [System.Reflection.BindingFlags]'Instance, NonPublic, Public')
$pageDataProp = [Krypton.Navigator.DragFeedback].GetProperty(
    'PageDragEndData',
    [System.Reflection.BindingFlags]'Instance, NonPublic, Public')
Assert-True ($null -ne $dragTargetsProp) 'DragFeedback.DragTargets property is available'
Assert-True ($null -ne $pageDataProp) 'DragFeedback.PageDragEndData property is available'
$dragTargetsProp.GetSetMethod($true).Invoke($solidFeedback, [object[]](, $targets))
$pageDataProp.GetSetMethod($true).Invoke($solidFeedback, [object[]](, $dragData))

$findTargetSolid = [Krypton.Navigator.DragFeedbackSolid].GetMethod(
    'FindTarget',
    [System.Reflection.BindingFlags]'Instance, NonPublic, Public')
Assert-True ($null -ne $findTargetSolid) 'DragFeedbackSolid.FindTarget is available'
$matched = $findTargetSolid.Invoke($solidFeedback, [object[]]@($overlapPt, $dragData))
if ($matched -is [System.Management.Automation.PSObject]) {
    $matched = $matched.PSObject.BaseObject
}
Assert-True ($null -ne $matched) 'DragFeedbackSolid.FindTarget returns a match for overlapping hot rects'
Assert-Equal 'edge' $matched.Tag 'DragFeedbackSolid prefers first target (control-edge before nested cell)'

# Reverse order: first match should now be the cell target.
$targetsReversed = New-Object Krypton.Navigator.DragTargetList
if ($targetsReversed -is [System.Management.Automation.PSObject]) {
    $targetsReversed = $targetsReversed.PSObject.BaseObject
}
[void]$targetsReversed.Add($cellTarget)
[void]$targetsReversed.Add($edgeTarget)
$dragTargetsProp.GetSetMethod($true).Invoke($solidFeedback, [object[]](, $targetsReversed))
$matchedReversed = $findTargetSolid.Invoke($solidFeedback, [object[]]@($overlapPt, $dragData))
if ($matchedReversed -is [System.Management.Automation.PSObject]) {
    $matchedReversed = $matchedReversed.PSObject.BaseObject
}
Assert-True ($null -ne $matchedReversed) 'DragFeedbackSolid.FindTarget returns a match for reversed list'
Assert-Equal 'cell' $matchedReversed.Tag 'DragFeedbackSolid first-match follows list order (not last/nested)'
$solidFeedback.Dispose()

# ----- DragFeedbackDocking: dead FindTarget removed; Feedback API present -----
$dockingType = [Krypton.Navigator.DragFeedbackDocking]
$findTargetDocking = $dockingType.GetMethod(
    'FindTarget',
    [System.Reflection.BindingFlags]'Instance, NonPublic, Public, DeclaredOnly')
Assert-True ($null -eq $findTargetDocking) 'DragFeedbackDocking no longer declares unused FindTarget'

$feedbackMethod = $dockingType.GetMethod('Feedback', [type[]]@([System.Drawing.Point], [Krypton.Navigator.DragTarget]))
Assert-True ($null -ne $feedbackMethod) 'DragFeedbackDocking.Feedback(Point, DragTarget) is present'

$dockingFeedback = New-Object Krypton.Navigator.DragFeedbackDocking ([Krypton.Toolkit.PaletteDragFeedback]::Square)
if ($dockingFeedback -is [System.Management.Automation.PSObject]) {
    $dockingFeedback = $dockingFeedback.PSObject.BaseObject
}
Assert-True ($null -ne $dockingFeedback) 'DragFeedbackDocking constructs for Square feedback'
$dockingFeedback.Dispose()

$form.Close()
$form.Dispose()

if ($failed.Count -gt 0) {
    Write-UnitTestBanner -Status FAIL -Message ("#3858 assertions failed ($($failed.Count))")
    exit 1
}

Write-UnitTestBanner -Status PASS -Message '#3858 docking drag-target heuristic assertions passed'
exit 0
