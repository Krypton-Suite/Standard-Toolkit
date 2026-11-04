<#
.SYNOPSIS
    Captures the updater prompt, notes, remind-later dialog, and progress footer.

.DESCRIPTION
    Loads the Debug net472 utilities build in-process (STA) and writes
    Documents/PR/4669-application-updater-prompt.png,
    4669-application-updater-notes.png,
    4669-application-updater-postpone.png, and
    4669-application-updater-details.png with PrintWindow.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-ApplicationUpdaterWindowScreenshot.ps1
#>
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
$pr = Join-Path $repoRoot 'Documents\PR'

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Interop.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Resources.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$utilities = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))
[System.Windows.Forms.Application]::EnableVisualStyles()

$flags = [System.Reflection.BindingFlags]'Instance,Public,NonPublic'
$packageType = $utilities.GetType('Krypton.Toolkit.Utilities.KryptonUpdatePackage')
$severityType = $utilities.GetType('Krypton.Toolkit.Utilities.KryptonUpdateSeverity')
$kindType = $utilities.GetType('Krypton.Toolkit.Utilities.KryptonUpdatePackageKind')
$stringsType = $utilities.GetType('Krypton.Toolkit.Utilities.KryptonApplicationUpdaterStrings')
$strings = [System.Activator]::CreateInstance($stringsType)
$important = [Enum]::Parse($severityType, 'Important')
$recommended = [Enum]::Parse($severityType, 'Recommended')
$installer = [Enum]::Parse($kindType, 'Installer')
$hash = 'a' * 64
$ctor = $packageType.GetConstructors() | Where-Object { $_.GetParameters().Count -ge 9 } | Select-Object -First 1

function New-Package([string]$version, $severity, [string]$notes) {
    $released = [Nullable[datetime]]([datetime]::SpecifyKind([datetime]'2026-11-03T12:00:00', [DateTimeKind]::Utc))
    $invokeArgs = @(
            [version]$version,
            $severity,
            $installer,
            'https://example.com/setup-9.9.9.exe',
            $hash,
            $null,
            $notes,
            $null,
            $released)
    while ($invokeArgs.Count -lt $ctor.GetParameters().Count) {
        $invokeArgs += [Type]::Missing
    }

    return $ctor.Invoke($invokeArgs)
}

$newer = New-Package '9.9.9' $important 'Newest local package. Severity is Important.'
$older = New-Package '1.5.0' $recommended 'Earlier local package. Severity is Recommended.'
$listType = [System.Collections.Generic.List``1].MakeGenericType($packageType)
$notes = [System.Activator]::CreateInstance($listType)
[void]$notes.Add($newer)
[void]$notes.Add($older)

function Show-Shot($form, [string]$path) {
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
    Save-UnitTestWindowPng -Form $form -Path $path -SettleMs 200
    $form.Close()
    $form.Dispose()
    Write-Output "Wrote $path"
}

$promptType = $utilities.GetType('Krypton.Toolkit.Utilities.VisualApplicationUpdatePromptForm')
$prompt = [System.Activator]::CreateInstance($promptType)
[void]$promptType.GetMethod('Bind', $flags).Invoke($prompt, @($newer, [version]'1.0.0', $notes, $strings, $true, $true, 7))
Show-Shot $prompt (Join-Path $pr '4669-application-updater-prompt.png')

$notesType = $utilities.GetType('Krypton.Toolkit.Utilities.VisualApplicationUpdateNotesForm')
$notesForm = [System.Activator]::CreateInstance($notesType)
[void]$notesType.GetMethod('Bind', $flags).Invoke($notesForm, @($newer, [version]'1.0.0', $notes, $strings, $true, $true, $true, 7, $null))
Show-Shot $notesForm (Join-Path $pr '4669-application-updater-notes.png')

$postponeType = $utilities.GetType('Krypton.Toolkit.Utilities.VisualApplicationUpdatePostponeForm')
$postpone = [System.Activator]::CreateInstance($postponeType)
[void]$postponeType.GetMethod('Bind', $flags).Invoke($postpone, @($strings, 7, 365, $true))
Show-Shot $postpone (Join-Path $pr '4669-application-updater-postpone.png')

$progressType = $utilities.GetType('Krypton.Toolkit.Utilities.VisualApplicationUpdateProgressForm')
$progress = [System.Activator]::CreateInstance($progressType)
[void]$progressType.GetMethod('Bind', $flags).Invoke($progress, @($strings))
[void]$progressType.GetMethod('SetFileName', $flags).Invoke($progress, @('setup-9.9.9.exe'))
$progress.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$progress.Location = New-Object System.Drawing.Point 80, 80
$progress.TopMost = $true
$progress.Show()
$progress.Activate()
$progress.BringToFront()
[void][UnitTestNative]::SetForegroundWindow($progress.Handle)
[System.Windows.Forms.Application]::DoEvents()
$progressArgs = [object[]]::new(2)
$progressArgs[0] = [int64]4420000
$progressArgs[1] = [Nullable[int64]]10000000
[void]$progressType.GetMethod('SetProgress', $flags).Invoke($progress, $progressArgs)
Start-Sleep -Milliseconds 400
$progressArgs[0] = [int64]4500000
[void]$progressType.GetMethod('SetProgress', $flags).Invoke($progress, $progressArgs)
[void]$progressType.GetMethod('OnDetails', $flags).Invoke($progress, @($null, [System.EventArgs]::Empty))
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
[System.Windows.Forms.Application]::DoEvents()
$detailsPath = Join-Path $pr '4669-application-updater-details.png'
Save-UnitTestWindowPng -Form $progress -Path $detailsPath -SettleMs 200
$progress.Close()
$progress.Dispose()
Write-Output "Wrote $detailsPath"
