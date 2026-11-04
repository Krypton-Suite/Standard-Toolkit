<#
.SYNOPSIS
    Captures the updater progress window after an interrupted download.

.DESCRIPTION
    Loads the Debug net472 utilities build in-process (STA), shows the internal
    progress form with Resume, Restart, and Close, and writes
    Documents/PR/4669-application-updater-recovery.png with PrintWindow.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-ApplicationUpdaterRecoveryScreenshot.ps1
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
    $OutputPath = Join-Path $repoRoot 'Documents\PR\4669-application-updater-recovery.png'
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Interop.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Resources.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$utilities = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $utilities.GetType('Krypton.Toolkit.Utilities.VisualApplicationUpdateProgressForm')
$stringsType = $utilities.GetType('Krypton.Toolkit.Utilities.KryptonApplicationUpdaterStrings')
if (-not $formType -or -not $stringsType) {
    throw 'The updater progress form was not found in Krypton.Toolkit.Utilities.dll.'
}

$flags = [System.Reflection.BindingFlags]'Instance,Public,NonPublic'
$strings = [System.Activator]::CreateInstance($stringsType)
$form = [System.Activator]::CreateInstance($formType)
[void]$formType.GetMethod('Bind', $flags).Invoke($form, @($strings))
$progressArgs = [object[]]::new(2)
$progressArgs[0] = [int64]45
$progressArgs[1] = [Nullable[int64]]100
[void]$formType.GetMethod('SetProgress', $flags).Invoke($form, $progressArgs)
[void]$formType.GetMethod('ShowRecovery', $flags).Invoke($form, @($strings.DownloadInterrupted, $true, $true, $strings))

$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point 80, 80
$form.TopMost = $true
$form.Show()
$form.Activate()
$form.BringToFront()
[void][UnitTestNative]::SetForegroundWindow($form.Handle)
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 500
[System.Windows.Forms.Application]::DoEvents()

Save-UnitTestWindowPng -Form $form -Path $OutputPath
$form.Close()
$form.Dispose()
Write-Output "Wrote $OutputPath"
