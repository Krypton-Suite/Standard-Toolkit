<#
.SYNOPSIS
    Hosts the update manifest editor and captures a filled package in light and dark themes.

.DESCRIPTION
    Loads the Debug net472 UpdateManifestEditor build in-process (STA), fills one sample package,
    applies Microsoft365Blue and Microsoft365BlackDarkMode, and writes
    Documents/PR/4669-update-manifest-editor-light.png and *-dark.png with PrintWindow.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-UpdateManifestEditorScreenshot.ps1
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

$samplePath = Join-Path ([System.IO.Path]::GetTempPath()) 'krypton-update-sample.txt'
[System.IO.File]::WriteAllText($samplePath, 'krypton update sample')
$hash = [System.BitConverter]::ToString([System.Security.Cryptography.SHA256]::Create().ComputeHash([System.IO.File]::ReadAllBytes($samplePath))).Replace('-', '').ToLowerInvariant()

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Shared.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Resources.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$themesDll = Join-Path $bin 'Krypton.Themes.dll'
if (-not (Test-Path -LiteralPath $themesDll)) {
    throw "Missing $themesDll - build Krypton.Themes Debug $TargetFramework first."
}
[void][System.Reflection.Assembly]::LoadFrom($themesDll)
$editorPath = Join-Path $bin 'UpdateManifestEditor.exe'
$asm = [System.Reflection.Assembly]::LoadFrom($editorPath)

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('UpdateManifestEditor.MainForm')
if (-not $formType) {
    throw 'Type UpdateManifestEditor.MainForm was not found.'
}

$flags = [System.Reflection.BindingFlags]'Instance,NonPublic'
$manager = New-Object Krypton.Toolkit.KryptonManager
$themes = @(
    @{ Mode = [Krypton.Toolkit.PaletteMode]::Microsoft365Blue; Suffix = 'light' },
    @{ Mode = [Krypton.Toolkit.PaletteMode]::Microsoft365BlackDarkMode; Suffix = 'dark' }
)

foreach ($theme in $themes) {
    $manager.GlobalPaletteMode = $theme.Mode
    $form = [System.Activator]::CreateInstance($formType)
    $packages = $formType.GetField('_packages', $flags).GetValue($form)
    $package = $packages[0]
    $package.Version = '9.9.9'
    $package.Severity = 'important'
    $package.PackageKind = 'installer'
    $package.PackageFile = $samplePath
    $package.PackageUrl = 'https://example.com/setup.exe'
    $package.Sha256 = $hash
    $package.InstallerArguments = '/quiet'
    $package.ReleaseNotes = 'Sample package for the manifest editor.'
    $package.ReleasedUtc = '2026-11-03T12:00:00.0000000Z'
    $package.MaximumPostponeDays = '14'
    $package.AllowPostpone = 'false'
    $package.AllowSkip = 'true'
    $package.AllowResume = 'true'
    $package.AllowRetry = 'false'
    [void]$formType.GetMethod('ShowSelection', $flags).Invoke($form, @())
    [void]$formType.GetMethod('RefreshListItem', $flags).Invoke($form, @())
    [void]$formType.GetMethod('UpdatePreview', $flags).Invoke($form, @())

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

    $outputPath = Join-Path $OutputDirectory "4669-update-manifest-editor-$($theme.Suffix).png"
    Save-UnitTestWindowPng -Form $form -Path $outputPath
    $form.Close()
    $form.Dispose()
    Write-Output "Wrote $outputPath"
}
