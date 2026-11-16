<#
.SYNOPSIS
    Captures Standard-Toolkit-Demos KryptonProgressbar Examples light/dark gallery PNGs (#4684).

.DESCRIPTION
    Builds the Dev ProgressBar example against this Toolkit tree, hosts Form1 in-process (STA),
    and writes example-light.png / example-dark.png into the Demos example project folder for the
    category README gallery (AGENTS.md Standard-Toolkit-Demos rules).

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-ProgressBarDemosGalleryScreenshot.ps1
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    # Windows PowerShell 5.1 hosts Framework TFMs; net48 needs the preserialized-resources flag.
    [string]$TargetFramework = 'net48'
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$demosRoot = Join-Path (Split-Path $repoRoot -Parent) 'Standard-Toolkit-Demos'
$exampleDir = Join-Path $demosRoot 'Source\Krypton Toolkit Examples\KryptonProgressbar Examples'
$csproj = Join-Path $exampleDir 'KryptonProgressbar Examples 2022.csproj'
if (-not (Test-Path -LiteralPath $csproj)) {
    throw "Demos ProgressBar example not found: $csproj"
}

Write-Host "Building $csproj ($TargetFramework) ..."
dotnet build $csproj -c $Configuration -f $TargetFramework `
    -p:GenerateResourceUsePreserializedResources=true `
    -p:GenerateResourceWarnOnBinaryFormatterUse=false
if ($LASTEXITCODE -ne 0) {
    throw "dotnet build failed for ProgressBar demos example."
}

$bin = Join-Path $demosRoot "Binaries\Krypton Demos\$Configuration\$TargetFramework"
$asmPath = Join-Path $bin 'Krypton ProgressBar Examples.exe'
if (-not (Test-Path -LiteralPath $asmPath)) {
    $asmPath = Join-Path $bin 'Krypton ProgressBar Examples.dll'
}
if (-not (Test-Path -LiteralPath $asmPath)) {
    throw "Built assembly not found under $bin"
}

# Prefer Demos output folder for assembly resolve (copies Toolkit refs beside the example).
Register-UnitTestAssemblyResolver -BinDir $bin
$toolkitBin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework
Register-UnitTestAssemblyResolver -BinDir $toolkitBin
Initialize-UnitTestNativeInput
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms

[void][System.Reflection.Assembly]::LoadFrom((Join-Path $toolkitBin 'Krypton.Shared.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $toolkitBin 'Krypton.Toolkit.dll'))
$themesDll = Join-Path $toolkitBin 'Krypton.Themes.dll'
if (-not (Test-Path -LiteralPath $themesDll)) {
    $themesDll = Join-Path $bin 'Krypton.Themes.dll'
}
if (Test-Path -LiteralPath $themesDll) {
    [void][System.Reflection.Assembly]::LoadFrom($themesDll)
}

$asm = [System.Reflection.Assembly]::LoadFrom($asmPath)
$formType = $asm.GetType('KryptonProgressBarExamples.Form1')
if (-not $formType) {
    throw 'Type KryptonProgressBarExamples.Form1 was not found.'
}

[System.Windows.Forms.Application]::EnableVisualStyles()

$shots = @(
    @{ ThemeText = 'Microsoft 365 - Blue'; Name = 'example-light.png' }
    @{ ThemeText = 'Microsoft 365 - Black (Dark Mode)'; Name = 'example-dark.png' }
)

$flags = [Reflection.BindingFlags]'Instance,NonPublic,Public'
foreach ($shot in $shots) {
    $form = [Activator]::CreateInstance($formType)
    $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $form.Location = New-Object System.Drawing.Point 80, 80
    $form.Show()

    # Form designer stamps Office2007Blue — select the gallery theme via the on-form combo.
    $themeCombo = $formType.GetField('kryptonThemeComboBox1', $flags).GetValue($form)
    $matched = $false
    for ($i = 0; $i -lt $themeCombo.Items.Count; $i++) {
        if ([string]$themeCombo.Items[$i] -eq $shot.ThemeText) {
            $themeCombo.SelectedIndex = $i
            $matched = $true
            break
        }
    }
    if (-not $matched) {
        throw "Theme '$($shot.ThemeText)' was not found in kryptonThemeComboBox1."
    }

    $refresh = $formType.GetMethod('RefreshSchemeReadout', $flags)
    if ($refresh) {
        $refresh.Invoke($form, @())
    }

    $form.TopMost = $true
    $form.Activate()
    $form.BringToFront()
    if ($form.Handle -ne [IntPtr]::Zero) {
        [void][UnitTestNative]::SetForegroundWindow($form.Handle)
    }
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 700

    $path = Join-Path $exampleDir $shot.Name
    Save-UnitTestWindowPng -Form $form -Path $path
    Write-Host "Wrote $path (theme=$($shot.ThemeText))"
    $form.Close()
    $form.Dispose()
}

Write-Host 'Done.'
