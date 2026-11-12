<#
.SYNOPSIS
    Hosts TimelineDemo and captures the step and date timelines.

.DESCRIPTION
    Loads the Debug TestForm build in-process (STA), shows TimelineDemo, and writes
    Documents/PR/4218-timeline-default.png (horizontal alternate steps plus the date chart),
    Documents/PR/4218-timeline-style-colours.png (custom ItemStyles colours),
    Documents/PR/4218-timeline-vertical.png (vertical compact steps), and
    Documents/PR/4218-timeline-vis.png (vis.js stacked boxes).
    Capture uses Save-UnitTestWindowPng (PrintWindow).

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-TimelineScreenshot.ps1
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,
    [string]$OutputDirectory
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
if (-not $OutputDirectory) {
    $OutputDirectory = Join-Path $repoRoot 'Documents\PR'
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Charting.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.TimelineDemo')
if (-not $formType) {
    throw 'Type TestForm.TimelineDemo was not found in TestForm.exe.'
}

$flags = [System.Reflection.BindingFlags]'Instance,NonPublic'
$form = [System.Activator]::CreateInstance($formType)
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point 80, 80
$form.TopMost = $true
$form.Show()
$form.Activate()
$form.BringToFront()
[void][UnitTestNative]::SetForegroundWindow($form.Handle)
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 600
[System.Windows.Forms.Application]::DoEvents()

$defaultPath = Join-Path $OutputDirectory '4218-timeline-default.png'
Save-UnitTestWindowPng -Form $form -Path $defaultPath

$custom = $formType.GetField('kchkCustomColours', $flags).GetValue($form)
$custom.Checked = $true
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
[System.Windows.Forms.Application]::DoEvents()
$coloursPath = Join-Path $OutputDirectory '4218-timeline-style-colours.png'
Save-UnitTestWindowPng -Form $form -Path $coloursPath
$custom.Checked = $false
[System.Windows.Forms.Application]::DoEvents()

$orientation = $formType.GetField('cmbOrientation', $flags).GetValue($form)
$layout = $formType.GetField('cmbLayout', $flags).GetValue($form)
$orientation.SelectedItem = [System.Windows.Forms.Orientation]::Vertical
$layoutType = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Charting.dll')).GetType('Krypton.Charting.TimelineLayout')
$layout.SelectedItem = [System.Enum]::Parse($layoutType, 'Compact')
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 400
[System.Windows.Forms.Application]::DoEvents()

$verticalPath = Join-Path $OutputDirectory '4218-timeline-vertical.png'
Save-UnitTestWindowPng -Form $form -Path $verticalPath

$orientation.SelectedItem = [System.Windows.Forms.Orientation]::Horizontal
$layout.SelectedItem = [System.Enum]::Parse($layoutType, 'Alternate')
$dateStyle = $formType.GetField('cmbDateStyle', $flags).GetValue($form)
$styleType = $layoutType.Assembly.GetType('Krypton.Charting.DateTimelineStyle')
$dateStyle.SelectedItem = [System.Enum]::Parse($styleType, 'Vis')
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 400
[System.Windows.Forms.Application]::DoEvents()

$visPath = Join-Path $OutputDirectory '4218-timeline-vis.png'
Save-UnitTestWindowPng -Form $form -Path $visPath

$dateStyle.SelectedItem = [System.Enum]::Parse($styleType, 'Chart')
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
[System.Windows.Forms.Application]::DoEvents()

$date = $formType.GetField('dateTimeline', $flags).GetValue($form)
$draw = $date.GetType().GetField('_draw', $flags).GetValue($date)
$marks = $draw.GetType().GetField('_marks', $flags).GetValue($draw)
$bar = $marks.GetValue(0).Bar
$x = [int]($bar.X + ($bar.Width / 2))
$y = [int]($bar.Y + ($bar.Height / 2))
function Get-InstanceMethod([type]$type, [string]$name) {
    $lookup = [System.Reflection.BindingFlags]'Instance,NonPublic,Public,DeclaredOnly'
    while ($null -ne $type) {
        $found = $type.GetMethod($name, $lookup)
        if ($null -ne $found) {
            return $found
        }

        $type = $type.BaseType
    }

    throw "Missing $name"
}

$down = Get-InstanceMethod $date.GetType() 'OnMouseDown'
$move = Get-InstanceMethod $date.GetType() 'OnMouseMove'
$up = Get-InstanceMethod $date.GetType() 'OnMouseUp'
$downArgs = New-Object System.Windows.Forms.MouseEventArgs[] 1
$moveArgs = New-Object System.Windows.Forms.MouseEventArgs[] 1
$upArgs = New-Object System.Windows.Forms.MouseEventArgs[] 1
$downArgs[0] = New-Object System.Windows.Forms.MouseEventArgs ([System.Windows.Forms.MouseButtons]::Left), 1, $x, $y, 0
$moveArgs[0] = New-Object System.Windows.Forms.MouseEventArgs ([System.Windows.Forms.MouseButtons]::Left), 1, ($x + 140), ($y + 55), 0
$upArgs[0] = New-Object System.Windows.Forms.MouseEventArgs ([System.Windows.Forms.MouseButtons]::Left), 1, ($x + 140), ($y + 55), 0
[void]$down.Invoke($date, $downArgs)
[void]$move.Invoke($date, $moveArgs)
[System.Windows.Forms.Application]::DoEvents()
[void]$up.Invoke($date, $upArgs)
[System.Windows.Forms.Application]::DoEvents()

$dragPath = Join-Path $OutputDirectory '4218-timeline-drag.png'
Save-UnitTestWindowPng -Form $form -Path $dragPath

$form.Close()
$form.Dispose()
Write-Output $defaultPath
Write-Output $coloursPath
Write-Output $verticalPath
Write-Output $visPath
Write-Output $dragPath
