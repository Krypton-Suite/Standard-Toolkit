<#
.SYNOPSIS
    Asserts #2383 KryptonWorkspace logical RTL packing (horizontal reverse, vertical unchanged).

.DESCRIPTION
    Builds a KryptonWorkspace with a horizontal root (A | vertical[Top,Bottom] | C),
    then checks LTR left-to-right packing, RTL packing from the right with Top still
    above Bottom, and XML save/load keeping Children order.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-WorkspaceRtlLayout.ps1
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

[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Interop.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Navigator.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Workspace.dll'))

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

function New-RtlTestCell {
    param([string]$UniqueName, [string]$Title)
    $cell = New-Object Krypton.Workspace.KryptonWorkspaceCell
    $cell.UniqueName = $UniqueName
    $cell.StarSize = '50*,50*'
    $page = New-Object Krypton.Navigator.KryptonPage
    $page.Text = $Title
    $page.UniqueName = $UniqueName + 'Page'
    [void]$cell.Pages.Add($page)
    return $cell
}

function Get-RootChildNames {
    param($Workspace)
    $names = New-Object System.Collections.Generic.List[string]
    foreach ($child in $Workspace.Root.Children) {
        if ($child -is [Krypton.Workspace.KryptonWorkspaceCell]) {
            $names.Add($child.UniqueName)
        }
        elseif ($child -is [Krypton.Workspace.KryptonWorkspaceSequence]) {
            $nested = New-Object System.Collections.Generic.List[string]
            foreach ($inner in $child.Children) {
                if ($inner -is [Krypton.Workspace.KryptonWorkspaceCell]) {
                    $nested.Add($inner.UniqueName)
                }
            }
            $names.Add(('[' + ($nested -join ',') + ']'))
        }
    }
    return ($names -join ',')
}

$form = New-Object Krypton.Toolkit.KryptonForm
$ws = $null
try {
    $form.Text = 'Workspace RTL unit test'
    $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $form.Location = New-Object System.Drawing.Point 40, 40
    $form.Size = New-Object System.Drawing.Size 900, 520
    $form.RightToLeft = [System.Windows.Forms.RightToLeft]::No
    $form.RightToLeftLayout = $false

    $ws = New-Object Krypton.Workspace.KryptonWorkspace
    $ws.Dock = [System.Windows.Forms.DockStyle]::Fill
    [void]$form.Controls.Add($ws)

    $cellA = New-RtlTestCell -UniqueName 'RtlCellA' -Title 'A'
    $cellTop = New-RtlTestCell -UniqueName 'RtlCellTop' -Title 'Top'
    $cellBottom = New-RtlTestCell -UniqueName 'RtlCellBottom' -Title 'Bottom'
    $cellC = New-RtlTestCell -UniqueName 'RtlCellC' -Title 'C'

    $stacked = New-Object Krypton.Workspace.KryptonWorkspaceSequence ([System.Windows.Forms.Orientation]::Vertical)
    [void]$stacked.Children.Add($cellTop)
    [void]$stacked.Children.Add($cellBottom)

    $ws.Root.Orientation = [System.Windows.Forms.Orientation]::Horizontal
    [void]$ws.Root.Children.Add($cellA)
    [void]$ws.Root.Children.Add($stacked)
    [void]$ws.Root.Children.Add($cellC)

    $form.Show()
    $form.Activate()
    [System.Windows.Forms.Application]::DoEvents()
    $ws.PerformLayout()
    [System.Windows.Forms.Application]::DoEvents()

    $ltrOrder = Get-RootChildNames -Workspace $ws
    Assert-True ($ltrOrder -eq 'RtlCellA,[RtlCellTop,RtlCellBottom],RtlCellC') "LTR children order is A, [Top,Bottom], C (actual='$ltrOrder')"
    Assert-True ($cellA.Left -lt $cellC.Left) "LTR A is left of C (A.Left=$($cellA.Left) C.Left=$($cellC.Left))"
    Assert-True ($cellTop.Top -lt $cellBottom.Top) "LTR Top is above Bottom (Top.Y=$($cellTop.Top) Bottom.Y=$($cellBottom.Top))"
    Assert-True (-not $ws.RightToLeftLayout) 'Workspace RightToLeftLayout is false in LTR'

    $saved = $ws.SaveLayoutToArray()
    Assert-True ($saved.Length -gt 0) 'SaveLayoutToArray returns bytes'

    $form.RightToLeft = [System.Windows.Forms.RightToLeft]::Yes
    $form.RightToLeftLayout = $true
    [System.Windows.Forms.Application]::DoEvents()
    $ws.PerformLayout()
    [System.Windows.Forms.Application]::DoEvents()

    Assert-True $ws.RightToLeftLayout 'Workspace RightToLeftLayout syncs from the form'
    Assert-True ($ws.RightToLeft -eq [System.Windows.Forms.RightToLeft]::Yes) 'Workspace RightToLeft inherits Yes'
    Assert-True ($cellA.Left -gt $cellC.Left) "RTL A is right of C (A.Left=$($cellA.Left) C.Left=$($cellC.Left))"
    Assert-True ($cellTop.Top -lt $cellBottom.Top) "RTL Top stays above Bottom (Top.Y=$($cellTop.Top) Bottom.Y=$($cellBottom.Top))"

    $rtlOrder = Get-RootChildNames -Workspace $ws
    Assert-True ($rtlOrder -eq 'RtlCellA,[RtlCellTop,RtlCellBottom],RtlCellC') "RTL children order is unchanged (actual='$rtlOrder')"

    $form.RightToLeft = [System.Windows.Forms.RightToLeft]::Yes
    $form.RightToLeftLayout = $false
    [System.Windows.Forms.Application]::DoEvents()
    $ws.PerformLayout()
    [System.Windows.Forms.Application]::DoEvents()
    Assert-True ($cellA.Left -lt $cellC.Left) "RightToLeft alone does not reverse columns (A.Left=$($cellA.Left) C.Left=$($cellC.Left))"

    $form.RightToLeft = [System.Windows.Forms.RightToLeft]::Yes
    $form.RightToLeftLayout = $true
    [System.Windows.Forms.Application]::DoEvents()
    $ws.LoadLayoutFromArray($saved)
    $ws.PerformLayout()
    [System.Windows.Forms.Application]::DoEvents()
    $loadedOrder = Get-RootChildNames -Workspace $ws
    Assert-True ($loadedOrder -eq 'RtlCellA,[RtlCellTop,RtlCellBottom],RtlCellC') "LoadLayoutFromArray keeps Children order (actual='$loadedOrder')"
}
finally {
    if ($form) {
        $form.Close()
        $form.Dispose()
    }
}

if ($failed.Count -gt 0) {
    Write-Host ""
    Write-Host "$($failed.Count) assertion(s) failed." -ForegroundColor Red
    exit 1
}

Write-Host ''
Write-Host 'All Workspace RTL layout assertions passed.' -ForegroundColor Green
exit 0
