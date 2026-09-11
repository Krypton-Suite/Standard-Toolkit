<#
.SYNOPSIS
    Hosts WorkspaceRtlDemo and captures LTR / RTL PNGs for issue #2383.

.DESCRIPTION
    Loads TestForm assemblies in-process (STA), shows WorkspaceRtlDemo, captures the
    window in LTR then after enabling RightToLeft + RightToLeftLayout, and writes
    Documents/PR/2383-workspace-rtl-ltr.png and 2383-workspace-rtl-rtl.png (or -OutputDir).

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-WorkspaceRtlScreenshot.ps1
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
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Navigator.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Workspace.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.WorkspaceRtlDemo')
if (-not $formType) {
    throw 'Type TestForm.WorkspaceRtlDemo was not found in TestForm.exe.'
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

Save-FormCapture -Form $form -Path (Join-Path $OutputDir '2383-workspace-rtl-ltr.png')

$chkField = $formType.GetField('chkRtl', [System.Reflection.BindingFlags]'Instance,NonPublic,Public')
if (-not $chkField) {
    throw 'Field chkRtl was not found on WorkspaceRtlDemo.'
}
$chk = $chkField.GetValue($form)
$chk.Checked = $true
$form.PerformLayout()
$form.Refresh()
Save-FormCapture -Form $form -Path (Join-Path $OutputDir '2383-workspace-rtl-rtl.png')

$form.Close()
$form.Dispose()
