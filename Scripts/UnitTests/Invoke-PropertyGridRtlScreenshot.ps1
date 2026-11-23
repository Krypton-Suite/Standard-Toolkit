<#
.SYNOPSIS
    Hosts ToolkitRtlGalleryDemo on the PropertyGrid tab and captures Dual RTL.

.DESCRIPTION
    Loads Debug TestForm in-process (STA), shows ToolkitRtlGalleryDemo with Dual RTL,
    selects the PropertyGrid page, and writes Documents/PR/2379-toolkit-rtl-propertygrid.png.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-PropertyGridRtlScreenshot.ps1
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
    $OutputPath = Join-Path $repoRoot 'Documents\PR\2379-toolkit-rtl-propertygrid.png'
}

$outDir = Split-Path -Parent $OutputPath
if (-not (Test-Path -LiteralPath $outDir)) {
    New-Item -ItemType Directory -Path $outDir | Out-Null
}

Register-UnitTestAssemblyResolver -BinDir $bin
Initialize-UnitTestNativeInput
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms

Get-ChildItem -LiteralPath $bin -Filter '*.dll' | ForEach-Object {
    try {
        [void][System.Reflection.Assembly]::LoadFrom($_.FullName)
    }
    catch {
        # Some satellite / native binaries are not managed assemblies.
    }
}

$testFormPath = Join-Path $bin 'TestForm.dll'
if (-not (Test-Path -LiteralPath $testFormPath)) {
    $testFormPath = Join-Path $bin 'TestForm.exe'
}
if (-not (Test-Path -LiteralPath $testFormPath)) {
    throw "TestForm was not found in $bin."
}

$asm = [System.Reflection.Assembly]::LoadFrom($testFormPath)
[System.Windows.Forms.Application]::EnableVisualStyles()
if ([System.Windows.Forms.Application]::RenderWithVisualStyles) { }

$formType = $asm.GetType('TestForm.ToolkitRtlGalleryDemo')
if (-not $formType) {
    throw 'Type TestForm.ToolkitRtlGalleryDemo was not found.'
}

$form = [System.Activator]::CreateInstance($formType)
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point 80, 80
$form.TopMost = $true
$form.Show()
$form.Activate()
$form.BringToFront()
[void][UnitTestNative]::SetForegroundWindow($form.Handle)
[System.Windows.Forms.Application]::DoEvents()

$navField = $formType.GetField('kryptonNavigator', [System.Reflection.BindingFlags]'Instance,NonPublic')
$nav = $navField.GetValue($form)
foreach ($page in $nav.Pages) {
    if ($page.Text -eq 'PropertyGrid') {
        $nav.SelectedPage = $page
        break
    }
}

Write-Host "Hosted $($form.Text); selected $($nav.SelectedPage.Text)"
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 1200
[System.Windows.Forms.Application]::DoEvents()

$bounds = $form.Bounds
$bmp = New-Object System.Drawing.Bitmap $bounds.Width, $bounds.Height
$g = [System.Drawing.Graphics]::FromImage($bmp)
try {
    $g.CopyFromScreen($bounds.Location, [System.Drawing.Point]::Empty, $bounds.Size)
    $bmp.Save($OutputPath, [System.Drawing.Imaging.ImageFormat]::Png)
}
finally {
    $g.Dispose()
    $bmp.Dispose()
    $form.Close()
    $form.Dispose()
}

Write-Host "Wrote $OutputPath"
