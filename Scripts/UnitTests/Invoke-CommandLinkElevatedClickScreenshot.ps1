<#
.SYNOPSIS
    Hosts the command-link ElevatedClick shield and captures it without launching RunAs.

.DESCRIPTION
    Loads Krypton.Toolkit.Utilities in-process (STA), shows the ElevatedClick command link
    (UAC shield on the arrow, left slot empty), and writes
    Documents/PR/4648-command-link-elevated-click-demo.png via Save-UnitTestWindowPng.
    Does not click the button, so the UAC consent dialog is not opened.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-CommandLinkElevatedClickScreenshot.ps1
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
    $OutputPath = Join-Path $repoRoot 'Documents\PR\4648-command-link-elevated-click-demo.png'
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))

[System.Windows.Forms.Application]::EnableVisualStyles()

$form = New-Object Krypton.Toolkit.KryptonForm
$form.Text = 'Command Link - ElevatedClick'
$form.ClientSize = New-Object System.Drawing.Size 560, 220
$form.FormBorderStyle = [System.Windows.Forms.FormBorderStyle]::Fixed3D
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point 80, 80

$hint = New-Object Krypton.Toolkit.KryptonLabel
$hint.Location = New-Object System.Drawing.Point 16, 16
$hint.Values.Text = 'The shield overlays the arrow. A click uses RunAs.'

$elevated = New-Object Krypton.Toolkit.Utilities.KryptonCommandLinkButton
$elevated.Location = New-Object System.Drawing.Point 16, 48
$elevated.Size = New-Object System.Drawing.Size 360, 90
$elevated.CommandLinkTextValues.Heading = 'Elevated Click'
$elevated.CommandLinkTextValues.Description = 'The arrow keeps the shield.'

$hide = New-Object Krypton.Toolkit.KryptonButton
$hide.Location = New-Object System.Drawing.Point 388, 48
$hide.Size = New-Object System.Drawing.Size 156, 28
$hide.Values.Text = 'Try to hide shield'

$logBox = New-Object Krypton.Toolkit.KryptonTextBox
$logBox.Location = New-Object System.Drawing.Point 16, 148
$logBox.Size = New-Object System.Drawing.Size 528, 56
$logBox.Multiline = $true
$logBox.ReadOnly = $true

# Subscribing forces the left-hand shield and the arrow overlay. Do not click.
$elevated.add_ElevatedClick({ })

$form.Controls.Add($hint)
$form.Controls.Add($elevated)
$form.Controls.Add($hide)
$form.Controls.Add($logBox)

$form.TopMost = $true
$form.Show()
$form.Activate()
$form.BringToFront()
[void][UnitTestNative]::SetForegroundWindow($form.Handle)
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 400

Save-UnitTestWindowPng -Form $form -Path $OutputPath -SettleMs 200

$form.Close()
$form.Dispose()
Write-Output "Wrote $OutputPath"
