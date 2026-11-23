<#
.SYNOPSIS
    Hosts the ElevatedClick shield and captures it without launching RunAs.

.DESCRIPTION
    Loads Krypton.Toolkit in-process (STA), shows the ElevatedClick shield scenario
    (same shield as TestForm ButtonsTest), and writes
    Documents/PR/4648-elevated-click-demo.png via Save-UnitTestWindowPng.
    Does not click the button, so the UAC consent dialog is not opened.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-ElevatedClickScreenshot.ps1
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
    $OutputPath = Join-Path $repoRoot 'Documents\PR\4648-elevated-click-demo.png'
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))

[System.Windows.Forms.Application]::EnableVisualStyles()

$form = New-Object Krypton.Toolkit.KryptonForm
$form.Text = 'Buttons Test - ElevatedClick'
$form.ClientSize = New-Object System.Drawing.Size 540, 240
$form.FormBorderStyle = [System.Windows.Forms.FormBorderStyle]::Fixed3D
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point 80, 80

$hint = New-Object Krypton.Toolkit.KryptonLabel
$hint.Location = New-Object System.Drawing.Point 16, 16
$hint.Values.Text = 'The shield overlays the button image. A click uses RunAs.'

$elevated = New-Object Krypton.Toolkit.KryptonButton
$elevated.Location = New-Object System.Drawing.Point 16, 48
$elevated.Size = New-Object System.Drawing.Size 240, 48
$elevated.Values.Text = 'Elevated Click'

$hide = New-Object Krypton.Toolkit.KryptonButton
$hide.Location = New-Object System.Drawing.Point 272, 48
$hide.Size = New-Object System.Drawing.Size 240, 48
$hide.Values.Text = 'Try to hide shield'

$logBox = New-Object Krypton.Toolkit.KryptonTextBox
$logBox.Location = New-Object System.Drawing.Point 16, 108
$logBox.Size = New-Object System.Drawing.Size 496, 112
$logBox.Multiline = $true
$logBox.ReadOnly = $true

function Add-Log([string]$line) {
    $logBox.AppendText($line + [Environment]::NewLine)
}

# A content image is already set, so subscribing places the shield on the overlay.
$elevated.Values.Image = [System.Drawing.SystemIcons]::Application.ToBitmap()
$elevated.add_ElevatedClick({ Add-Log 'ElevatedClick' })
$elevated.add_Click({ Add-Log 'Click' })
$hide.add_Click({
    $elevated.Values.UseAsUACElevationButton = $false
    $elevated.Values.Image = $null
    $elevated.Values.ResetImage()
    $stillShown = $elevated.Values.UseAsUACElevationButton -and ($null -ne $elevated.Values.Image)
    if ($stillShown) {
        Add-Log 'Hide ignored: shield still shown.'
    }
    else {
        Add-Log 'Hide changed the shield.'
    }
})

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
