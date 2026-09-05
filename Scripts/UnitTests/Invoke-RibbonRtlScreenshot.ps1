<#
.SYNOPSIS
    Hosts RibbonRtlDemo and captures LTR / RTL PNGs for issue #2382.

.DESCRIPTION
    Loads TestForm assemblies in-process (STA), shows RibbonRtlDemo, captures the
    window in LTR then after enabling RightToLeft + RightToLeftLayout, and writes
    Documents/PR/2382-ribbon-rtl-ltr.png and 2382-ribbon-rtl-rtl.png (or -OutputDir).

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-RibbonRtlScreenshot.ps1
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
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Ribbon.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.RibbonRtlDemo')
if (-not $formType) {
    throw 'Type TestForm.RibbonRtlDemo was not found in TestForm.exe.'
}

function Save-FormCapture {
    param(
        [System.Windows.Forms.Form]$Form,
        [string]$Path
    )

    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 400
    [System.Windows.Forms.Application]::DoEvents()

    $bounds = $Form.Bounds
    $bmp = New-Object System.Drawing.Bitmap $bounds.Width, $bounds.Height
    $g = [System.Drawing.Graphics]::FromImage($bmp)
    $g.CopyFromScreen($bounds.Location, [System.Drawing.Point]::Empty, $bounds.Size)
    $g.Dispose()
    $bmp.Save($Path, [System.Drawing.Imaging.ImageFormat]::Png)
    $bmp.Dispose()
    Write-Host "Wrote $Path"
}

$form = [System.Activator]::CreateInstance($formType)
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point 80, 80
$form.TopMost = $true
$form.Show()
$form.Activate()
$form.BringToFront()
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 700

Save-FormCapture -Form $form -Path (Join-Path $OutputDir '2382-ribbon-rtl-ltr.png')

$chkField = $formType.GetField('_chkRtl', [System.Reflection.BindingFlags]'Instance,NonPublic')
if (-not $chkField) {
    throw 'Field _chkRtl was not found on RibbonRtlDemo.'
}
$chk = $chkField.GetValue($form)
$chk.Checked = $true
$form.PerformLayout()
$form.Refresh()
Save-FormCapture -Form $form -Path (Join-Path $OutputDir '2382-ribbon-rtl-rtl.png')

$orbField = $formType.GetField('_chkOrb', [System.Reflection.BindingFlags]'Instance,NonPublic')
if (-not $orbField) {
    throw 'Field _chkOrb was not found on RibbonRtlDemo.'
}
$orb = $orbField.GetValue($form)
$orb.Checked = $true
$form.PerformLayout()
$form.Refresh()
Save-FormCapture -Form $form -Path (Join-Path $OutputDir '2382-ribbon-rtl-orb.png')

$form.Close()
$form.Dispose()
