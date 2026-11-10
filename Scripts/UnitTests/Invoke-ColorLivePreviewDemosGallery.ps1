<#
.SYNOPSIS
    Captures the Standard-Toolkit-Demos colour-button live preview in light and dark themes.

.DESCRIPTION
    Hosts Krypton ColorButton Examples (net48) and writes live-preview-light.png and
    live-preview-dark.png into that example folder.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-ColorLivePreviewDemosGallery.ps1
#>
[CmdletBinding()]
param(
    [string]$DemosRoot,
    [string]$Configuration = 'Debug',
    [string]$BinDir
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
if (-not $DemosRoot) {
    $DemosRoot = Join-Path (Split-Path -Parent $repoRoot) 'Standard-Toolkit-Demos'
}

if ($BinDir) {
    $bin = $BinDir
} else {
    $bin = Join-Path $DemosRoot "Binaries\Krypton Demos\$Configuration"
}
$exe = Join-Path $bin 'Krypton ColorButton Examples.exe'
if (-not $BinDir -and -not (Test-Path -LiteralPath $exe)) {
    $bin = Join-Path $bin 'net48'
    $exe = Join-Path $bin 'Krypton ColorButton Examples.exe'
}
if (-not (Test-Path -LiteralPath $exe)) {
    throw "Demos colour-button example was not found: $exe"
}

$outDir = Join-Path $DemosRoot 'Source\Krypton Toolkit Examples\KryptonColorButton Examples'
Register-UnitTestAssemblyResolver -BinDir $bin
Initialize-UnitTestNativeInput
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms

$toolkitBin = Join-Path $repoRoot "Bin\$Configuration\net48"
$toolkit = Join-Path $toolkitBin 'Krypton.Toolkit.dll'
if (-not (Test-Path -LiteralPath $toolkit)) {
    $toolkit = Join-Path $bin 'Krypton.Toolkit.dll'
}
[void][System.Reflection.Assembly]::LoadFrom($toolkit)
$themes = Join-Path $toolkitBin 'Krypton.Themes.dll'
if (Test-Path -LiteralPath $themes) {
    [void][System.Reflection.Assembly]::LoadFrom($themes)
}

$asm = [System.Reflection.Assembly]::LoadFrom($exe)
[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('KryptonColorButtonExamples.Form1')
if (-not $formType) {
    throw 'Type KryptonColorButtonExamples.Form1 was not found.'
}

$shots = @(
    @{ Mode = [Krypton.Toolkit.PaletteMode]::Microsoft365Blue; Name = 'live-preview-light.png' }
    @{ Mode = [Krypton.Toolkit.PaletteMode]::Microsoft365BlackDarkMode; Name = 'live-preview-dark.png' }
)

$mgr = New-Object Krypton.Toolkit.KryptonManager
foreach ($shot in $shots) {
    Write-Host "Showing $($shot.Mode)"
    $mgr.GlobalPaletteMode = $shot.Mode
    [System.Windows.Forms.Application]::DoEvents()

    $form = [Activator]::CreateInstance($formType)
    $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $form.Location = New-Object System.Drawing.Point 60, 40
    $form.Show()
    $form.TopMost = $true
    $form.Activate()
    $form.BringToFront()
    if ($form.Handle -ne [IntPtr]::Zero) {
        [void][UnitTestNative]::SetForegroundWindow($form.Handle)
    }
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 500

    $path = Join-Path $outDir $shot.Name
    Save-UnitTestWindowPng -Form $form -Path $path
    Write-Host "Wrote $path (mode=$($shot.Mode))"
    $form.Hide()
    $form.Dispose()
    [System.Windows.Forms.Application]::DoEvents()
}

Write-Host 'Done.'
