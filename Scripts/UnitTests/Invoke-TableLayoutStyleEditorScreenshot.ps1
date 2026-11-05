# UnitTest-CI: exclude
#Requires -Version 5.1
<#
.SYNOPSIS
    Opens the themed Column and Row Styles dialog and captures a PNG.

.DESCRIPTION
    Checks that KryptonTableLayoutPanel ColumnStyles and RowStyles use
    KryptonDesignerTableLayoutStyleEditor, that insert and delete keep a child
    in the same visual cell, that Cancel restores the grid, and that OK keeps
    an added column. Writes Documents/PR/table-layout-style-editor.png
    (or -OutputPath) with Save-UnitTestWindowPng.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-TableLayoutStyleEditorScreenshot.ps1
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,
    [string]$OutputPath
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
if (-not $OutputPath) {
    $OutputPath = Join-Path $repoRoot 'Documents\PR\table-layout-style-editor.png'
}

$outDir = Split-Path -Parent $OutputPath
if (-not (Test-Path -LiteralPath $outDir)) {
    New-Item -ItemType Directory -Path $outDir | Out-Null
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))

[System.Windows.Forms.Application]::EnableVisualStyles()

$panel = New-Object Krypton.Toolkit.KryptonTableLayoutPanel
# ColumnCount does not create style objects until layout, so add them explicitly.
[void]$panel.ColumnStyles.Add((New-Object System.Windows.Forms.ColumnStyle([System.Windows.Forms.SizeType]::Absolute, 120)))
[void]$panel.ColumnStyles.Add((New-Object System.Windows.Forms.ColumnStyle([System.Windows.Forms.SizeType]::Percent, 50)))
[void]$panel.ColumnStyles.Add((New-Object System.Windows.Forms.ColumnStyle([System.Windows.Forms.SizeType]::AutoSize, 0)))
[void]$panel.RowStyles.Add((New-Object System.Windows.Forms.RowStyle([System.Windows.Forms.SizeType]::Absolute, 40)))
[void]$panel.RowStyles.Add((New-Object System.Windows.Forms.RowStyle([System.Windows.Forms.SizeType]::Percent, 100)))
$panel.ColumnCount = 3
$panel.RowCount = 2

$cell = New-Object Krypton.Toolkit.KryptonButton
$cell.Values.Text = 'Cell'
$panel.Controls.Add($cell, 1, 0)

$properties = [System.ComponentModel.TypeDescriptor]::GetProperties($panel)
$editorType = [System.Drawing.Design.UITypeEditor]
foreach ($name in @('ColumnStyles', 'RowStyles')) {
    $editor = $properties[$name].GetEditor($editorType)
    if ($null -eq $editor -or $editor.GetType().Name -ne 'KryptonDesignerTableLayoutStyleEditor') {
        throw "$name did not return KryptonDesignerTableLayoutStyleEditor."
    }
}

$flags = [System.Reflection.BindingFlags]'Static,NonPublic'
$editType = [Krypton.Toolkit.KryptonTableLayoutPanel].Assembly.GetType('Krypton.Toolkit.KryptonTableLayoutStyleEdit')
if ($null -eq $editType) {
    throw 'KryptonTableLayoutStyleEdit was not found.'
}

$insert = $editType.GetMethod('Insert', $flags)
$remove = $editType.GetMethod('RemoveAt', $flags)
[void]$insert.Invoke($null, @([System.Windows.Forms.TableLayoutPanel]$panel, [bool]$true, [int]1))
if ($panel.ColumnCount -ne 4) {
    throw "Insert should grow to 4 columns, got $($panel.ColumnCount)."
}

if ($panel.GetColumn($cell) -ne 2) {
    throw "Insert should shift the cell from column 1 to 2, got $($panel.GetColumn($cell))."
}

[void]$remove.Invoke($null, @([System.Windows.Forms.TableLayoutPanel]$panel, [bool]$true, [int]1))
if ($panel.ColumnCount -ne 3 -or $panel.GetColumn($cell) -ne 1) {
    throw "Delete should restore 3 columns and put the cell back in column 1. Count=$($panel.ColumnCount) Column=$($panel.GetColumn($cell))."
}

$owner = New-Object Krypton.Toolkit.KryptonForm
$owner.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$owner.Location = New-Object System.Drawing.Point 80, 80
$owner.ClientSize = New-Object System.Drawing.Size 420, 180
$owner.Text = 'Column and Row Styles host'
$owner.TopMost = $true
$owner.Show()
$owner.Activate()
[void][UnitTestNative]::SetForegroundWindow($owner.Handle)
[System.Windows.Forms.Application]::DoEvents()

function Find-NamedButton([System.Windows.Forms.Control]$root, [string]$text) {
    foreach ($child in $root.Controls) {
        if ($child.GetType().Name -eq 'KryptonButton' -and $child.Values.Text -eq $text) {
            return $child
        }

        $nested = Find-NamedButton $child $text
        if ($null -ne $nested) {
            return $nested
        }
    }

    return $null
}

function Show-StyleDialog([scriptblock]$onDialog) {
    $script:dialogAction = $onDialog
    $script:dialogTicks = 0
    $script:dialogDone = $false
    $timer = New-Object System.Windows.Forms.Timer
    $timer.Interval = 300
    $timer.Add_Tick({
        $script:dialogTicks++
        foreach ($open in [System.Windows.Forms.Application]::OpenForms) {
            if ($open.GetType().Name -ne 'VisualTableLayoutStyleEditorForm') {
                continue
            }

            if ($open.ClientSize.Width -lt 400 -and $script:dialogTicks -lt 15) {
                return
            }

            $timer.Stop()
            $open.TopMost = $true
            $open.Activate()
            $open.BringToFront()
            [void][UnitTestNative]::SetForegroundWindow($open.Handle)
            [System.Windows.Forms.Application]::DoEvents()
            & $script:dialogAction $open
            $script:dialogDone = $true
            return
        }

        if ($script:dialogTicks -gt 40) {
            $timer.Stop()
        }
    })
    $timer.Start()
    $result = [Krypton.Toolkit.KryptonDesignerTableLayoutStyleEditor]::ShowDialog($owner, $panel, $true)
    $timer.Stop()
    $timer.Dispose()
    if (-not $script:dialogDone) {
        throw 'The Column and Row Styles dialog did not appear.'
    }

    return $result
}

$cancel = Show-StyleDialog {
    param($dialog)
    Save-UnitTestWindowPng -Form $dialog -Path $OutputPath -SettleMs 300
    $dialog.DialogResult = [System.Windows.Forms.DialogResult]::Cancel
    $dialog.Close()
}

if ($cancel -ne [System.Windows.Forms.DialogResult]::Cancel) {
    throw "Cancel path returned $cancel."
}

if ($panel.ColumnCount -ne 3 -or $panel.GetColumn($cell) -ne 1 -or $panel.ColumnStyles[0].Width -ne 120) {
    throw "Cancel should restore the grid. Count=$($panel.ColumnCount) Column=$($panel.GetColumn($cell)) Width=$($panel.ColumnStyles[0].Width)."
}

$ok = Show-StyleDialog {
    param($dialog)
    $add = Find-NamedButton $dialog 'Add'
    if ($null -eq $add) {
        throw 'The Add button was not found.'
    }

    $add.PerformClick()
    $dialog.DialogResult = [System.Windows.Forms.DialogResult]::OK
    $dialog.Close()
}

if ($ok -ne [System.Windows.Forms.DialogResult]::OK -or $panel.ColumnCount -ne 4 -or $panel.GetColumn($cell) -ne 1) {
    throw "OK Add should leave 4 columns and the cell in column 1. Result=$ok Count=$($panel.ColumnCount) Column=$($panel.GetColumn($cell))."
}

$owner.Close()
$owner.Dispose()
$panel.Dispose()
