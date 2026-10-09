<#
.SYNOPSIS
    Captures the #4465 input and exception dialogs.

.DESCRIPTION
    Hosts VisualInputBoxForm and VisualExceptionDialogForm from the Debug net472 toolkit
    and writes Documents/PR/4465-dialog-dpi-input.png and 4465-dialog-dpi-exception.png.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-DialogDpiScreenshot.ps1
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[System.Windows.Forms.Application]::EnableVisualStyles()

function Show-CapturedForm($form, $path) {
    $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $form.Location = New-Object System.Drawing.Point 80, 80
    $form.TopMost = $true
    $form.Show()
    $form.Activate()
    $form.Refresh()
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 700
    Save-UnitTestWindowPng -Form $form -Path $path -InflateX 1 -InflateY 1 -SettleMs 500
    $form.Close()
    $form.Dispose()
    Write-Output "Wrote $path"
}

$outDir = Join-Path $repoRoot 'Documents\PR'
$data = New-Object Krypton.Toolkit.KryptonInputBoxData
$data.Caption = 'DPI-aware input'
$data.Prompt = 'This prompt is long enough to wrap. The dialog grows with the text and stays inside the working area. The response box and the OK and Cancel buttons stay visible.'
$data.CueText = 'Type a response'
$inputForm = New-Object Krypton.Toolkit.VisualInputBoxForm $data
Show-CapturedForm $inputForm (Join-Path $outDir '4465-dialog-dpi-input.png')

$exception = New-Object System.InvalidOperationException 'Sample exception used to check dialog DPI layout.'
$exceptionForm = New-Object Krypton.Toolkit.VisualExceptionDialogForm @(
    [Nullable[bool]]$true,
    [Nullable[bool]]$true,
    [Nullable[System.Drawing.Color]]$null,
    [System.Exception]$exception)
Show-CapturedForm $exceptionForm (Join-Path $outDir '4465-dialog-dpi-exception.png')
Write-Output 'Wrote 4465 dialog screenshots.'
