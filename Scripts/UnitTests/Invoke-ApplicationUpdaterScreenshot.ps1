<#
.SYNOPSIS
    Hosts ApplicationUpdaterDemo, captures the update prompt, and applies a local zip.

.DESCRIPTION
    Loads the Debug TestForm build in-process (STA). Check now opens the Important
    installer prompt and the script writes Documents/PR/4669-application-updater-prompt.png.
    It then opens Release notes at version 9.9.9 and writes
    Documents/PR/4669-application-updater-notes.png. It switches back to 1.0.0,
    Automatic, and an Optional archive, and waits for the embedded helper to extract
    readme.txt into the demo temp folder.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-ApplicationUpdaterScreenshot.ps1
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
    $OutputPath = Join-Path $repoRoot 'Documents\PR\4669-application-updater-prompt.png'
}

$notesPath = Join-Path $repoRoot 'Documents\PR\4669-application-updater-notes.png'

$demoRoot = Join-Path ([System.IO.Path]::GetTempPath()) 'KryptonApplicationUpdaterDemo'
if (Test-Path -LiteralPath $demoRoot) {
    Remove-Item -LiteralPath $demoRoot -Recurse -Force
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))
$testFormPath = Join-Path $bin 'TestForm.dll'
if (-not (Test-Path -LiteralPath $testFormPath)) {
    $testFormPath = Join-Path $bin 'TestForm.exe'
}

$asm = [System.Reflection.Assembly]::LoadFrom($testFormPath)

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.ApplicationUpdaterDemo')
if (-not $formType) {
    throw 'Type TestForm.ApplicationUpdaterDemo was not found in TestForm.exe.'
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
Start-Sleep -Milliseconds 400
[System.Windows.Forms.Application]::DoEvents()

$shot = $false
$timer = New-Object System.Windows.Forms.Timer
$timer.Interval = 300
$timer.Add_Tick({
    if ($shot) {
        return
    }

    foreach ($open in [System.Windows.Forms.Application]::OpenForms) {
        if ($open.Text -eq 'Update available') {
            $script:shot = $true
            $timer.Stop()
            Save-UnitTestWindowPng -Form $open -Path $OutputPath -SettleMs 200
            $open.Close()
            return
        }
    }
})
$timer.Start()

$checkNow = $formType.GetField('btnCheckNow', $flags).GetValue($form)
$form.BeginInvoke([Action] { $checkNow.PerformClick() }) | Out-Null

$deadline = [DateTime]::UtcNow.AddSeconds(20)
while (-not $shot -and [DateTime]::UtcNow -lt $deadline) {
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 50
}

$timer.Stop()
$timer.Dispose()
if (-not $shot) {
    $form.Close()
    $form.Dispose()
    throw 'The update prompt did not open.'
}

$enabledDeadline = [DateTime]::UtcNow.AddSeconds(15)
while (-not $checkNow.Enabled -and [DateTime]::UtcNow -lt $enabledDeadline) {
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 50
}

$updater = $formType.GetField('_updater', $flags).GetValue($form)
$updater.CurrentVersion = '9.9.9'
$btnNotes = $formType.GetField('btnReleaseNotes', $flags).GetValue($form)
$notesShot = $false
$notesTimer = New-Object System.Windows.Forms.Timer
$notesTimer.Interval = 300
$notesTimer.Add_Tick({
    if ($notesShot) {
        return
    }

    foreach ($open in [System.Windows.Forms.Application]::OpenForms) {
        if ($open.Text -eq 'Release notes') {
            $script:notesShot = $true
            $notesTimer.Stop()
            Save-UnitTestWindowPng -Form $open -Path $notesPath -SettleMs 200
            $open.Close()
            return
        }
    }
})
$notesTimer.Start()
$form.BeginInvoke([Action] { $btnNotes.PerformClick() }) | Out-Null

$notesDeadline = [DateTime]::UtcNow.AddSeconds(20)
while (-not $notesShot -and [DateTime]::UtcNow -lt $notesDeadline) {
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 50
}

$notesTimer.Stop()
$notesTimer.Dispose()
if (-not $notesShot) {
    $form.Close()
    $form.Dispose()
    throw 'The release notes window did not open.'
}

$notesEnabled = [DateTime]::UtcNow.AddSeconds(15)
while (-not $btnNotes.Enabled -and [DateTime]::UtcNow -lt $notesEnabled) {
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 50
}

$updater.CurrentVersion = '1.0.0'

$cmbMode = $formType.GetField('cmbMode', $flags).GetValue($form)
$cmbSeverity = $formType.GetField('cmbSeverity', $flags).GetValue($form)
$cmbKind = $formType.GetField('cmbKind', $flags).GetValue($form)
$utilities = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))
$cmbMode.SelectedItem = [Enum]::Parse($utilities.GetType('Krypton.Toolkit.Utilities.KryptonUpdateMode'), 'Automatic')
$cmbSeverity.SelectedItem = [Enum]::Parse($utilities.GetType('Krypton.Toolkit.Utilities.KryptonUpdateSeverity'), 'Optional')
$cmbKind.SelectedItem = [Enum]::Parse($utilities.GetType('Krypton.Toolkit.Utilities.KryptonUpdatePackageKind'), 'Archive')
[System.Windows.Forms.Application]::DoEvents()
$form.BeginInvoke([Action] { $checkNow.PerformClick() }) | Out-Null

$extracted = Join-Path $demoRoot 'install\readme.txt'
$extractDeadline = [DateTime]::UtcNow.AddSeconds(30)
while (-not (Test-Path -LiteralPath $extracted) -and [DateTime]::UtcNow -lt $extractDeadline) {
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 100
}

$form.Close()
$form.Dispose()

if (-not (Test-Path -LiteralPath $extracted)) {
    throw "The update helper did not extract $extracted."
}

Write-Output "Captured $OutputPath"
Write-Output "Captured $notesPath"
Write-Output "Extracted $extracted"
