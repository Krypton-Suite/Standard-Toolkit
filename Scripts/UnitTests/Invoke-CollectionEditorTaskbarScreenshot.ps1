<#
.SYNOPSIS
    Captures the #4547 collection-editor taskbar demo and the editor dialog.

.DESCRIPTION
    Hosts Bug4547CollectionEditorTaskbarDemo, opens the ButtonSpec collection editor,
    and writes two PNGs with Save-UnitTestWindowPng (PrintWindow of each HWND).

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-CollectionEditorTaskbarScreenshot.ps1
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,
    [string]$HostOutputPath,
    [string]$EditorOutputPath
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
if (-not $HostOutputPath) {
    $HostOutputPath = Join-Path $repoRoot 'Documents\PR\4547-collection-editor-taskbar-host.png'
}
if (-not $EditorOutputPath) {
    $EditorOutputPath = Join-Path $repoRoot 'Documents\PR\4547-collection-editor-taskbar-editor.png'
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Interop.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Resources.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

function Test-PngMostlyBlack {
    param([string]$Path)
    $bmp = [System.Drawing.Bitmap]::FromFile($Path)
    try {
        $hits = 0
        foreach ($pt in @(
                (New-Object System.Drawing.Point 8, 8),
                (New-Object System.Drawing.Point ([int]($bmp.Width / 2)), ([int]($bmp.Height / 2))),
                (New-Object System.Drawing.Point ($bmp.Width - 8), ($bmp.Height - 8)))) {
            $c = $bmp.GetPixel($pt.X, $pt.Y)
            if ($c.R -gt 8 -or $c.G -gt 8 -or $c.B -gt 8) { $hits++ }
        }
        return $hits -eq 0
    }
    finally {
        $bmp.Dispose()
    }
}

function Save-CollectionEditorPng {
    param($Form, [string]$Path)
    Save-UnitTestWindowPng -Form $Form -Path $Path -SettleMs 500
    if (-not (Test-PngMostlyBlack $Path)) { return }

    # PW_RENDERFULLCONTENT can return an empty bitmap for this chrome. WM_PRINT is next.
    $hwnd = $Form.Handle
    $rect = New-Object UnitTestCaptureNative+RECT
    [void][UnitTestCaptureNative]::GetWindowRect($hwnd, [ref]$rect)
    $width = $rect.Right - $rect.Left
    $height = $rect.Bottom - $rect.Top
    $bmp = New-Object System.Drawing.Bitmap $width, $height
    $g = [System.Drawing.Graphics]::FromImage($bmp)
    $hdc = $g.GetHdc()
    try {
        [void][UnitTestCaptureNative]::PrintWindow($hwnd, $hdc, 0)
    }
    finally {
        $g.ReleaseHdc($hdc)
        $g.Dispose()
    }
    $bmp.Save($Path, [System.Drawing.Imaging.ImageFormat]::Png)
    $bmp.Dispose()
    if (-not (Test-PngMostlyBlack $Path)) { return }

    $Form.TopMost = $true
    $Form.Activate()
    $Form.BringToFront()
    [void][UnitTestNative]::SetForegroundWindow($Form.Handle)
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 300
    $bounds = $Form.Bounds
    $shot = New-Object System.Drawing.Bitmap $bounds.Width, $bounds.Height
    $sg = [System.Drawing.Graphics]::FromImage($shot)
    try {
        $sg.CopyFromScreen($bounds.Location, [System.Drawing.Point]::Empty, $bounds.Size)
        $shot.Save($Path, [System.Drawing.Imaging.ImageFormat]::Png)
    }
    finally {
        $sg.Dispose()
        $shot.Dispose()
    }
}

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.Bug4547CollectionEditorTaskbarDemo')
if (-not $formType) {
    throw 'Type TestForm.Bug4547CollectionEditorTaskbarDemo was not found in TestForm.exe.'
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
Start-Sleep -Milliseconds 400

$editorPath = $EditorOutputPath
$captureState = @{ Captured = $false }
$timer = New-Object System.Windows.Forms.Timer
$timer.Interval = 700
$timer.Add_Tick({
    $timer.Stop()
    foreach ($open in [System.Windows.Forms.Application]::OpenForms) {
        if ($open.GetType().Name -eq 'VisualStandardCollectionForm') {
            Save-CollectionEditorPng -Form $open -Path $editorPath
            $captureState.Captured = $true
            $open.Close()
            break
        }
    }
}.GetNewClosure())
$timer.Start()
$form.OpenCollectionEditor()
$timer.Dispose()

if (-not $captureState.Captured) {
    throw 'The ButtonSpec collection editor did not open in time to capture.'
}

[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 300
Save-CollectionEditorPng -Form $form -Path $HostOutputPath

$form.Close()
$form.Dispose()
Write-Host "Wrote $HostOutputPath"
Write-Host "Wrote $EditorOutputPath"
