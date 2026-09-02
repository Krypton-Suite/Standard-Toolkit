<#
.SYNOPSIS
    Opens the palette pack editor and captures a PNG of the #2117 add/remove UI.

.DESCRIPTION
    Loads Toolkit assemblies in-process (STA), builds a two-theme sample .kpal pack,
    shows VisualKryptonPalettePackEditorForm, captures the window, and writes
    Documents/PR/2117-pack-editor-demo.png (or -OutputPath).

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-PalettePackEditorScreenshot.ps1
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
    $OutputPath = Join-Path $repoRoot 'Documents\PR\2117-pack-editor-demo.png'
}

$outDir = Split-Path -Parent $OutputPath
if (-not (Test-Path -LiteralPath $outDir)) {
    New-Item -ItemType Directory -Path $outDir | Out-Null
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$utilities = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))

$temp = Join-Path ([System.IO.Path]::GetTempPath()) ('kpal-pack-editor-' + [Guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $temp | Out-Null
$lime = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
$lime.SetPaletteName('Pack-Lime')
$orange = New-Object Krypton.Toolkit.KryptonCustomPaletteBase
$orange.SetPaletteName('Pack-Orange')
$packList = New-Object 'System.Collections.Generic.List[Krypton.Toolkit.KryptonCustomPaletteBase]'
[void]$packList.Add($lime)
[void]$packList.Add($orange)
$packPath = Join-Path $temp 'sample-pack.kpal'
[void][Krypton.Toolkit.KryptonPaletteFile]::ExportPack($packPath, $packList, $true, '2117-pack-editor')
$lime.Dispose()
$orange.Dispose()

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $utilities.GetType('Krypton.Toolkit.Utilities.VisualKryptonPalettePackEditorForm')
if (-not $formType) {
    throw 'Type VisualKryptonPalettePackEditorForm was not found in Krypton.Toolkit.Utilities.dll.'
}

$ctor = $formType.GetConstructor(
    [System.Reflection.BindingFlags]'Instance,NonPublic',
    $null,
    [type[]]@([string]),
    $null)
if (-not $ctor) {
    throw 'VisualKryptonPalettePackEditorForm(string) constructor was not found.'
}

$form = $ctor.Invoke((,[string]$packPath))
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$working = [System.Windows.Forms.Screen]::PrimaryScreen.WorkingArea
$form.Location = New-Object System.Drawing.Point ($working.Left + 80), ($working.Top + 80)
$form.TopMost = $true
$form.Show()
$form.Activate()
$form.BringToFront()
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 900
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
