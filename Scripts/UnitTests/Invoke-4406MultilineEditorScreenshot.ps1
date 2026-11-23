# UnitTest-CI: exclude
#Requires -Version 5.1
<#
.SYNOPSIS
    Opens the themed multiline text editor from Feature3851 and captures a PNG.

.DESCRIPTION
    Hosts Feature3851RenderingDpiDemo, enables MultilineStringEditor, and invokes the
    text box editor button. A timer captures the modal VisualMultilineStringEditorForm
    with Save-UnitTestWindowPng, then closes it with Cancel so the text box is unchanged.
    Writes Documents/PR/4406-multiline-string-editor.png (or -OutputPath).

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-4406MultilineEditorScreenshot.ps1
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
    $OutputPath = Join-Path $repoRoot 'Documents\PR\4406-multiline-string-editor.png'
}

$outDir = Split-Path -Parent $OutputPath
if (-not (Test-Path -LiteralPath $outDir)) {
    New-Item -ItemType Directory -Path $outDir | Out-Null
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Ribbon.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.Feature3851RenderingDpiDemo')
if (-not $formType) {
    throw 'Type TestForm.Feature3851RenderingDpiDemo was not found in TestForm.exe.'
}

$flags = [System.Reflection.BindingFlags]'Instance,NonPublic'
$form = [System.Activator]::CreateInstance($formType)
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point 80, 80
$form.TopMost = $true
$form.Show()
$form.Activate()
$form.BringToFront()
[void][UnitTestNative]::SetForegroundWindow($form.Handle)
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 400

$toggle = $formType.GetField('_multilineToggle', $flags).GetValue($form)
$toggle.Checked = $true
$textBox = $formType.GetField('_multilineTextBox', $flags).GetValue($form)
$openEditor = $textBox.GetType().GetMethod('OnEditorButtonClicked', $flags)
if (-not $openEditor) {
    throw 'KryptonTextBox.OnEditorButtonClicked was not found.'
}

$script:editorCaptured = $false
$timer = New-Object System.Windows.Forms.Timer
$timer.Interval = 400
$script:editorWaitTicks = 0
$timer.Add_Tick({
    $script:editorWaitTicks++
    foreach ($open in [System.Windows.Forms.Application]::OpenForms) {
        if ($open.GetType().Name -ne 'VisualMultilineStringEditorForm') {
            continue
        }

        # Layout can still be settling on the first tick. Wait for the design width.
        if ($open.ClientSize.Width -lt 400 -and $script:editorWaitTicks -lt 15) {
            return
        }

        $timer.Stop()
        $open.TopMost = $true
        $open.Activate()
        $open.BringToFront()
        [void][UnitTestNative]::SetForegroundWindow($open.Handle)
        [System.Windows.Forms.Application]::DoEvents()
        Save-UnitTestWindowPng -Form $open -Path $OutputPath -SettleMs 300
        $open.DialogResult = [System.Windows.Forms.DialogResult]::Cancel
        $open.Close()
        $script:editorCaptured = $true
        return
    }
})
$timer.Start()

[void]$openEditor.Invoke($textBox, @($null, [System.EventArgs]::Empty))
$timer.Stop()
$timer.Dispose()

if (-not $script:editorCaptured) {
    throw 'The themed multiline editor dialog was not captured.'
}

$form.Close()
$form.Dispose()
