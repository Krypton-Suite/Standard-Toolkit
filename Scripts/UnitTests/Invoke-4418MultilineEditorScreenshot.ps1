# UnitTest-CI: exclude
#Requires -Version 5.1
<#
.SYNOPSIS
    Hosts a MultilineStringEditor text box and captures the themed editor dialog.

.DESCRIPTION
    Loads Debug Krypton.Toolkit in-process (STA). Shows a text box with the editor
    button, opens the modal text editor through OnEditorButtonClicked, captures both
    windows with Save-UnitTestWindowPng, cancels the dialog, and checks that the
    text box text is unchanged.

    Writes Documents/PR/4418-multiline-editor-host.png and
    Documents/PR/4418-multiline-editor-dialog.png.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-4418MultilineEditorScreenshot.ps1
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
Initialize-UnitTestNativeInput
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Interop.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$resourcesDll = Join-Path $bin 'Krypton.Resources.dll'
if (Test-Path -LiteralPath $resourcesDll) {
    [void][System.Reflection.Assembly]::LoadFrom($resourcesDll)
}

[System.Windows.Forms.Application]::EnableVisualStyles()
[System.Windows.Forms.Application]::SetCompatibleTextRenderingDefault($false)

$sampleText = "Line one`r`nLine two"
$working = [System.Windows.Forms.Screen]::PrimaryScreen.WorkingArea
$hostForm = New-Object Krypton.Toolkit.KryptonForm
$textBox = New-Object Krypton.Toolkit.KryptonTextBox
try {
    $hostForm.Text = 'Multiline string editor (#4418)'
    $hostForm.ClientSize = New-Object System.Drawing.Size 520, 220
    $hostForm.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $hostForm.Location = New-Object System.Drawing.Point ($working.Left + 80), ($working.Top + 80)

    $label = New-Object Krypton.Toolkit.KryptonWrapLabel
    $label.Dock = [System.Windows.Forms.DockStyle]::Top
    $label.LabelStyle = [Krypton.Toolkit.LabelStyle]::NormalPanel
    $label.Text = 'The editor button opens the themed text editor. OK writes the text back; Cancel and Escape leave it unchanged.'

    $textBox.Multiline = $true
    $textBox.Dock = [System.Windows.Forms.DockStyle]::Fill
    $textBox.Text = $sampleText
    $textBox.MultilineStringEditor = $true

    $hostForm.Controls.Add($textBox)
    $hostForm.Controls.Add($label)
    $hostForm.Show()
    $hostForm.TopMost = $true
    $hostForm.Activate()
    $hostForm.BringToFront()
    [void][UnitTestNative]::SetForegroundWindow($hostForm.Handle)
    [System.Windows.Forms.Application]::DoEvents()

    $hostPng = Join-Path $OutputDir '4418-multiline-editor-host.png'
    Save-UnitTestWindowPng -Form $hostForm -Path $hostPng -SettleMs 200
    Write-Host "Wrote $hostPng"

    $script:capturedEditor = $false
    $script:captureError = $null
    $dialogPng = Join-Path $OutputDir '4418-multiline-editor-dialog.png'
    $timer = New-Object System.Windows.Forms.Timer
    $timer.Interval = 150
    $timer.Add_Tick({
        if ($script:capturedEditor) {
            return
        }

        try {
            # Index the collection. foreach over OpenForms yields a wrapper whose
            # DialogResult property is not visible to Windows PowerShell.
            $openForms = [System.Windows.Forms.Application]::OpenForms
            for ($i = 0; $i -lt $openForms.Count; $i++) {
                $openForm = $openForms[$i]
                if ($openForm.GetType().FullName -ne 'Krypton.Toolkit.VisualMultilineStringEditorForm') {
                    continue
                }

                # Load restores the designer client size after a brief shrink. Wait for that layout.
                if ($openForm.ClientSize.Height -lt 200) {
                    continue
                }

                $script:capturedEditor = $true
                $timer.Stop()
                $openForm.TopMost = $true
                $openForm.Activate()
                $openForm.BringToFront()
                [void][UnitTestNative]::SetForegroundWindow($openForm.Handle)
                [System.Windows.Forms.Application]::DoEvents()
                Save-UnitTestWindowPng -Form $openForm -Path $dialogPng -SettleMs 200
                Write-Host "Wrote $dialogPng"
                $dialogResult = [System.Windows.Forms.Form].GetProperty('DialogResult')
                $dialogResult.SetValue($openForm, [System.Windows.Forms.DialogResult]::Cancel, $null)
                return
            }
        }
        catch {
            $script:captureError = $_
            $script:capturedEditor = $true
            $timer.Stop()
        }
    })

    $timer.Start()
    $flags = [System.Reflection.BindingFlags]'Instance,NonPublic'
    $method = $textBox.GetType().GetMethod('OnEditorButtonClicked', $flags)
    if (-not $method) {
        throw 'OnEditorButtonClicked was not found.'
    }

    $method.Invoke($textBox, @($null, [System.EventArgs]::Empty))
    $timer.Stop()
    $timer.Dispose()

    if ($script:captureError) {
        throw $script:captureError
    }

    if (-not $script:capturedEditor) {
        throw 'The multiline text editor dialog did not open.'
    }

    if ($textBox.Text -ne $sampleText) {
        throw 'Cancel changed the text box text.'
    }
}
finally {
    if (-not $hostForm.IsDisposed) {
        $hostForm.Close()
        $hostForm.Dispose()
    }
}
