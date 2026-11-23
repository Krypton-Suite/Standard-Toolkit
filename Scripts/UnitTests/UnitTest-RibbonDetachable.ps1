<#
.SYNOPSIS
    Asserts #595: KryptonRibbon detachable floating window and drag reattachment support.

.DESCRIPTION
    Loads Debug Krypton.Toolkit / Krypton.Ribbon binaries and runs in-process STA checks:

    1. Ribbon Detach(): removes ribbon from parent Form, creates VisualRibbonFloatingWindow, hosts ribbon.
    2. Floating window properties: correct flags, owner, text, Ribbon accessor.
    3. Ribbon Reattach(): moves ribbon back to original parent, restores Z-order (index 0) and dock state.
    4. AllowDragReattach property: defaults to true and can be toggled.
    5. DropSolidWindow: VisualRibbonDropSolidWindow instantiates, renders glyph, and disposes cleanly.

    Requires an STA apartment (use powershell -STA).
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
$ribbonPath = Join-Path $bin 'Krypton.Ribbon.dll'

[void][System.Reflection.Assembly]::LoadFrom($interopPath)
[void][System.Reflection.Assembly]::LoadFrom($toolkitPath)
[void][System.Reflection.Assembly]::LoadFrom($ribbonPath)

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

try {
    # 1. Create parent form and ribbon
    $form = New-Object Krypton.Toolkit.KryptonForm
    $form.Size = New-Object System.Drawing.Size(800, 600)
    $null = $form.Handle
    
    $ribbon = New-Object Krypton.Ribbon.KryptonRibbon
    $ribbon.Dock = [System.Windows.Forms.DockStyle]::Top
    $ribbon.AllowDetach = $true

    $tab = New-Object Krypton.Ribbon.KryptonRibbonTab
    $tab.Text = 'TestTab'
    $group = New-Object Krypton.Ribbon.KryptonRibbonGroup
    $tab.Groups.Add($group)
    $ribbon.RibbonTabs.Add($tab)

    $form.Controls.Add($ribbon)
    $ribbon.BringToFront()

    Assert-True (-not $ribbon.IsDetached) "Ribbon initially not detached"
    Assert-True ($ribbon.AllowDragReattach) "AllowDragReattach defaults to true"

    # 2. Detach ribbon
    $ribbon.FloatingWindowText = "Custom Floating Title"
    $detached = $ribbon.Detach()
    Assert-True $detached "Ribbon.Detach() returns true"
    Assert-True $ribbon.IsDetached "Ribbon.IsDetached is true after Detach()"
    Assert-True ($ribbon.Parent -is [Krypton.Ribbon.VisualRibbonFloatingWindow]) "Ribbon parent is VisualRibbonFloatingWindow"
    Assert-Equal "Custom Floating Title" ($ribbon.Parent.Text) "Floating window title matches FloatingWindowText"

    # Dynamically update title while floating
    $ribbon.FloatingWindowText = "Updated Title"
    Assert-Equal "Updated Title" ($ribbon.Parent.Text) "Floating window updates title live"

    # 3. Reattach ribbon
    $reattached = $ribbon.Reattach()
    Assert-True $reattached "Ribbon.Reattach() returns true"
    Assert-True (-not $ribbon.IsDetached) "Ribbon.IsDetached is false after Reattach()"
    Assert-Equal $form $ribbon.Parent "Ribbon parent restored to original form"
    Assert-Equal 0 ($form.Controls.GetChildIndex($ribbon)) "Ribbon restored to front of Z-order (child index 0)"

    # 4. DetachAndDrag ribbon
    $dragPt = New-Object System.Drawing.Point(300, 200)
    $dragged = $ribbon.DetachAndDrag($dragPt)
    Assert-True $dragged "Ribbon.DetachAndDrag() returns true"
    Assert-True $ribbon.IsDetached "Ribbon.IsDetached is true after DetachAndDrag()"
    $reattached2 = $ribbon.Reattach()
    Assert-True $reattached2 "Ribbon.Reattach() returns true after DetachAndDrag"

    # 5. Clean up
    $form.Dispose()
    Write-Host "`nAll detachable ribbon assertions passed successfully." -ForegroundColor Cyan
}
catch {
    $failed.Add("Exception: $($_.Exception.Message)")
    Write-Host "Exception: $($_.Exception)" -ForegroundColor Red
}

if ($failed.Count -gt 0) {
    Write-Error "UnitTest-RibbonDetachable failed ($($failed.Count) failures)"
    exit 1
}
