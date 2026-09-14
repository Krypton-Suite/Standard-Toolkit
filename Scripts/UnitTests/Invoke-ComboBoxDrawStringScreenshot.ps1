<#
.SYNOPSIS
    Opens Feature4339ComboBoxSimpleStyleDemo in DropDownList and captures a PNG for #4424.

.DESCRIPTION
    Loads TestForm assemblies in-process (STA), shows the combo demo with DropDownList
    style (edit-strip GDI+ DrawString path), captures the window, and writes
    Documents/PR/4424-combobox-drawstring-dropdownlist.png.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-ComboBoxDrawStringScreenshot.ps1
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
    $OutputPath = Join-Path $repoRoot 'Documents\PR\4424-combobox-drawstring-dropdownlist.png'
}

$outDir = Split-Path -Parent $OutputPath
if (-not (Test-Path -LiteralPath $outDir)) {
    New-Item -ItemType Directory -Path $outDir | Out-Null
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.Feature4339ComboBoxSimpleStyleDemo')
if (-not $formType) {
    throw 'Type TestForm.Feature4339ComboBoxSimpleStyleDemo was not found in TestForm.exe.'
}

$form = [System.Activator]::CreateInstance($formType)
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point 40, 40
$form.TopMost = $true
$form.Show()
$form.Activate()
$form.BringToFront()
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 900
[System.Windows.Forms.Application]::DoEvents()

# Bottom-right DropDownList already exercises the edit-strip GDI+ DrawString path (#4424).
# Do not flip the Dock=Fill Simple combo to DropDownList — that leaves a misleading tall chrome.

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
