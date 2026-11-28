<#
.SYNOPSIS
    Hosts KryptonColorButtonDemo and writes light/dark PNGs for issue #4691.

.DESCRIPTION
    Loads TestForm assemblies in-process (STA) and shows the custom-colour demo,
    including the live-preview section, under Microsoft 365 Blue and Black Dark Mode.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-ColorLivePreviewScreenshot.ps1 -TargetFramework net8.0-windows
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,
    [string]$OutputDir
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
if (-not $OutputDir) {
    $OutputDir = Join-Path $repoRoot 'Documents\PR'
}
if (-not (Test-Path -LiteralPath $OutputDir)) {
    New-Item -ItemType Directory -Path $OutputDir | Out-Null
}

Register-UnitTestAssemblyResolver -BinDir $bin
Initialize-UnitTestNativeInput
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Shared.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$themesDll = Join-Path $bin 'Krypton.Themes.dll'
if (Test-Path -LiteralPath $themesDll) {
    [void][System.Reflection.Assembly]::LoadFrom($themesDll)
}
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))
$testFormPath = Join-Path $bin 'TestForm.dll'
if (-not (Test-Path -LiteralPath $testFormPath)) {
    $testFormPath = Join-Path $bin 'TestForm.exe'
}
if (-not (Test-Path -LiteralPath $testFormPath)) {
    throw "TestForm was not found in $bin."
}
$asm = [System.Reflection.Assembly]::LoadFrom($testFormPath)

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.KryptonColorButtonDemo')
if (-not $formType) {
    throw 'Type TestForm.KryptonColorButtonDemo was not found in TestForm.'
}

$shots = @(
    @{ Mode = [Krypton.Toolkit.PaletteMode]::Microsoft365Blue; Name = '4691-color-live-preview-default-light.png' }
    @{ Mode = [Krypton.Toolkit.PaletteMode]::Microsoft365BlackDarkMode; Name = '4691-color-live-preview-default-dark.png' }
)

$mgr = New-Object Krypton.Toolkit.KryptonManager
foreach ($shot in $shots) {
    $mgr.GlobalPaletteMode = $shot.Mode
    [System.Windows.Forms.Application]::DoEvents()

    $form = [Activator]::CreateInstance($formType)
    $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $form.Location = New-Object System.Drawing.Point 80, 40
    $form.Show()
    $form.TopMost = $true
    $form.Activate()
    $form.BringToFront()
    if ($form.Handle -ne [IntPtr]::Zero) {
        [void][UnitTestNative]::SetForegroundWindow($form.Handle)
    }
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 500

    $path = Join-Path $OutputDir $shot.Name
    Save-UnitTestWindowPng -Form $form -Path $path
    Write-Host "Wrote $path (mode=$($shot.Mode))"
    $form.Close()
    $form.Dispose()
}

Write-Host 'Done.'
