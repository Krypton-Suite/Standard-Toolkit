<#
.SYNOPSIS
    Captures the updater progress window after an interrupted download in light and dark themes.

.DESCRIPTION
    Loads the Debug net472 utilities build in-process (STA), shows the internal progress form
    with Resume, Restart, and Close under Microsoft365Blue and Microsoft365BlackDarkMode, and
    writes Documents/PR/4669-application-updater-recovery-light.png and *-dark.png with PrintWindow.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-ApplicationUpdaterRecoveryScreenshot.ps1
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
if (-not (Test-Path -LiteralPath $OutputDirectory)) {
    New-Item -ItemType Directory -Path $OutputDirectory | Out-Null
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Interop.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Resources.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$themesDll = Join-Path $bin 'Krypton.Themes.dll'
if (-not (Test-Path -LiteralPath $themesDll)) {
    throw "Missing $themesDll - build Krypton.Themes Debug $TargetFramework first."
}
[void][System.Reflection.Assembly]::LoadFrom($themesDll)
$utilities = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $utilities.GetType('Krypton.Toolkit.Utilities.VisualApplicationUpdateProgressForm')
$stringsType = $utilities.GetType('Krypton.Toolkit.Utilities.KryptonApplicationUpdaterStrings')
if (-not $formType -or -not $stringsType) {
    throw 'The updater progress form was not found in Krypton.Toolkit.Utilities.dll.'
}

$flags = [System.Reflection.BindingFlags]'Instance,Public,NonPublic'
$manager = New-Object Krypton.Toolkit.KryptonManager
$themes = @(
    @{ Mode = [Krypton.Toolkit.PaletteMode]::Microsoft365Blue; Suffix = 'light' },
    @{ Mode = [Krypton.Toolkit.PaletteMode]::Microsoft365BlackDarkMode; Suffix = 'dark' }
)

foreach ($theme in $themes) {
    $manager.GlobalPaletteMode = $theme.Mode
    $strings = [System.Activator]::CreateInstance($stringsType)
    $form = [System.Activator]::CreateInstance($formType)
    $progressDisplayType = $utilities.GetType('Krypton.Toolkit.Utilities.KryptonUpdateProgressDisplay')
    $progressBarMode = [Enum]::Parse($progressDisplayType, 'ProgressBar')
    [void]$formType.GetMethod('Bind', $flags).Invoke($form, @($strings, $null, $progressBarMode))
    $progressArgs = [object[]]::new(2)
    $progressArgs[0] = [int64]45
    $progressArgs[1] = [Nullable[int64]]100
    [void]$formType.GetMethod('SetProgress', $flags).Invoke($form, $progressArgs)
    # ShowRecovery returns Task; start the recovery UI without awaiting the choice.
    $null = $formType.GetMethod('ShowRecovery', $flags).Invoke($form, @($strings.DownloadInterrupted, $true, $true, $strings))
    [System.Windows.Forms.Application]::DoEvents()

    $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $form.Location = New-Object System.Drawing.Point 80, 80
    $form.TopMost = $true
    $form.Show()
    $form.Refresh()
    $form.Activate()
    $form.BringToFront()
    [void][UnitTestNative]::SetForegroundWindow($form.Handle)
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 500
    [System.Windows.Forms.Application]::DoEvents()

    $outputPath = Join-Path $OutputDirectory "4669-application-updater-recovery-$($theme.Suffix).png"
    Save-UnitTestWindowPng -Form $form -Path $outputPath
    $form.Close()
    $form.Dispose()
    Write-Output "Wrote $outputPath"
}
