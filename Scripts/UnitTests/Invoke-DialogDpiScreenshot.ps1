<#
.SYNOPSIS
    Captures the long input prompt and the resizable exception dialog for issue 4465.

.DESCRIPTION
    Hosts the dialogs in-process and writes PNGs with Save-UnitTestWindowPng (PrintWindow).

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-DialogDpiScreenshot.ps1
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,
    [string]$OutputDirectory
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
if (-not $OutputDirectory) {
    $OutputDirectory = Join-Path $repoRoot 'Documents\PR'
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput

$toolkit = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))

function Show-CapturedForm([System.Windows.Forms.Form]$Form, [string]$Path) {
    $Form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $Form.Location = New-Object System.Drawing.Point 80, 80
    $Form.TopMost = $true
    $Form.Show()
    $Form.Activate()
    [System.Windows.Forms.Application]::DoEvents()
    $Form.Refresh()
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 700
    # PrintWindow returns a black bitmap for these Krypton frames in this session.
    # A one-pixel inflate switches Save-UnitTestWindowPng to a TopMost screen blit.
    Save-UnitTestWindowPng -Form $Form -Path $Path -InflateX 1 -InflateY 1 -SettleMs 500
    $Form.Close()
    $Form.Dispose()
}

$data = New-Object Krypton.Toolkit.KryptonInputBoxData
$data.Caption = 'DPI-aware input'
$data.Prompt = 'This prompt is long enough to wrap. The dialog grows with the text and stays inside the working area. ' +
    'The response box and the OK and Cancel buttons stay visible.'
$data.CueText = 'Type a response'
$inputForm = New-Object Krypton.Toolkit.VisualInputBoxForm $data
Show-CapturedForm $inputForm (Join-Path $OutputDirectory '4465-dialog-dpi-input.png')

$exceptionType = $toolkit.GetType('Krypton.Toolkit.VisualExceptionDialogForm')
$ctor = $exceptionType.GetConstructors() | Where-Object { $_.GetParameters().Count -ge 4 } | Select-Object -First 1
$exception = New-Object System.InvalidOperationException 'Sample exception used to show the resizable, DPI-clamped exception dialog.'
$invokeArgs = [object[]]@(
    [Nullable[bool]]$true,
    [Nullable[bool]]$true,
    [Nullable[System.Drawing.Color]]$null,
    [System.Exception]$exception,
    $null
)
$exceptionForm = [System.Windows.Forms.Form]$ctor.Invoke($invokeArgs)
Show-CapturedForm $exceptionForm (Join-Path $OutputDirectory '4465-dialog-dpi-exception.png')

Write-Output 'Wrote 4465 dialog screenshots.'
