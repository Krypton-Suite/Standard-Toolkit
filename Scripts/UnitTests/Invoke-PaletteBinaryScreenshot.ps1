<#
.SYNOPSIS
    Opens PaletteBinaryDemo and captures a PNG of the #2117 demo (bulk folder upgrade button).

.DESCRIPTION
    Loads TestForm assemblies in-process (STA), shows PaletteBinaryDemo, captures the window,
    and writes Documents/PR/2117-bulk-xml-upgrade-demo.png (or -OutputPath).

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-PaletteBinaryScreenshot.ps1
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
    $OutputPath = Join-Path $repoRoot 'Documents\PR\2117-bulk-xml-upgrade-demo.png'
}

$outDir = Split-Path -Parent $OutputPath
if (-not (Test-Path -LiteralPath $outDir)) {
    New-Item -ItemType Directory -Path $outDir | Out-Null
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))
$themesDll = Join-Path $bin 'Krypton.Themes.dll'
if (Test-Path -LiteralPath $themesDll) {
    [void][System.Reflection.Assembly]::LoadFrom($themesDll)
}
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.PaletteBinaryDemo')
if (-not $formType) {
    throw 'Type TestForm.PaletteBinaryDemo was not found in TestForm.exe.'
}

$form = [System.Activator]::CreateInstance($formType)
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point 80, 80
$form.Show()
$form.Activate()
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 700
[System.Windows.Forms.Application]::DoEvents()

$bounds = $form.Bounds
$bmp = New-Object System.Drawing.Bitmap $bounds.Width, $bounds.Height
$g = [System.Drawing.Graphics]::FromImage($bmp)
$g.CopyFromScreen($bounds.Location, [System.Drawing.Point]::Empty, $bounds.Size)
$g.Dispose()
$bmp.Save($OutputPath, [System.Drawing.Imaging.ImageFormat]::Png)
$bmp.Dispose()
$form.Close()
$form.Dispose()
Write-Host "Wrote $OutputPath"
