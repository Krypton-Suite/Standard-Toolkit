<#
.SYNOPSIS
    Hosts KryptonVuMeterDemo and captures stills plus a short needle GIF.

.DESCRIPTION
    Loads the Debug TestForm build in-process (STA), shows the VU meter demo,
    and writes Documents/PR/4655-vu-meter-*.png and .gif via PrintWindow.
    Local PR assets only; do not commit them.

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-VuMeterScreenshot.ps1
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
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Gauges.dll'))
$testFormPath = Join-Path $bin 'TestForm.dll'
if (-not (Test-Path -LiteralPath $testFormPath)) {
    $testFormPath = Join-Path $bin 'TestForm.exe'
}

$asm = [System.Reflection.Assembly]::LoadFrom($testFormPath)

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.KryptonVuMeterDemo')
if (-not $formType) {
    throw 'Type TestForm.KryptonVuMeterDemo was not found in TestForm.exe.'
}

$flags = [System.Reflection.BindingFlags]'Instance,NonPublic'
$form = [System.Activator]::CreateInstance($formType)
try {
    $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $form.Location = New-Object System.Drawing.Point 40, 40
    $form.TopMost = $true
    $form.Show()
    $form.Activate()
    $form.BringToFront()
    [void][UnitTestNative]::SetForegroundWindow($form.Handle)
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 400

    $analog = $formType.GetField('kvumAnalog', $flags).GetValue($form)
    $segments = $formType.GetField('kvumSegments', $flags).GetValue($form)
    $analog.Values.AttackMilliseconds = 0
    $analog.Values.ReleaseMilliseconds = 0
    $segments.Values.AttackMilliseconds = 0
    $segments.Values.ReleaseMilliseconds = 0
    $analog.Value = -6
    $segments.Value = -6
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 200
    [System.Windows.Forms.Application]::DoEvents()
    Save-UnitTestWindowPng -Form $form -Path (Join-Path $OutputDirectory '4655-vu-meter-mid.png') -SettleMs 200

    $peak = $formType.GetField('kchkPeak', $flags).GetValue($form)
    $peak.Checked = $true
    $analog.Value = 2
    $segments.Value = 2
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 80
    $analog.Value = -8
    $segments.Value = -8
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 200
    [System.Windows.Forms.Application]::DoEvents()
    Save-UnitTestWindowPng -Form $form -Path (Join-Path $OutputDirectory '4655-vu-meter-overload.png') -SettleMs 200

    $frameDir = Join-Path $OutputDirectory '4655-vu-meter-frames'
    New-Item -ItemType Directory -Force -Path $frameDir | Out-Null
    $analog.Values.AttackMilliseconds = 0
    $analog.Values.ReleaseMilliseconds = 0
    $segments.Values.AttackMilliseconds = 0
    $segments.Values.ReleaseMilliseconds = 0
    $levels = @(-20, -14, -8, -4, -1, 0, 1, 2, 3, 1, -2, -10, -20)
    $framePaths = New-Object System.Collections.Generic.List[string]
    for ($i = 0; $i -lt $levels.Count; $i++) {
        $analog.Value = $levels[$i]
        $segments.Value = $levels[$i]
        [System.Windows.Forms.Application]::DoEvents()
        Start-Sleep -Milliseconds 60
        $framePath = Join-Path $frameDir ('frame-{0:00}.png' -f $i)
        Save-UnitTestWindowPng -Form $form -Path $framePath -SettleMs 40
        $framePaths.Add($framePath)
    }

    Add-Type -AssemblyName PresentationCore
    $encoder = New-Object System.Windows.Media.Imaging.GifBitmapEncoder
    foreach ($framePath in $framePaths) {
        $stream = [System.IO.File]::OpenRead($framePath)
        try {
            $decoder = [System.Windows.Media.Imaging.BitmapDecoder]::Create(
                $stream,
                [System.Windows.Media.Imaging.BitmapCreateOptions]::PreservePixelFormat,
                [System.Windows.Media.Imaging.BitmapCacheOption]::OnLoad)
            $cached = New-Object System.Windows.Media.Imaging.CachedBitmap $decoder.Frames[0],
                ([System.Windows.Media.Imaging.BitmapCreateOptions]::None),
                ([System.Windows.Media.Imaging.BitmapCacheOption]::OnLoad)
            $metadata = New-Object System.Windows.Media.Imaging.BitmapMetadata 'gif'
            $metadata.SetQuery('/grctlext/Delay', [uint16]20)
            $encoder.Frames.Add([System.Windows.Media.Imaging.BitmapFrame]::Create($cached, $null, $metadata, $null))
        }
        finally {
            $stream.Dispose()
        }
    }

    $gifPath = Join-Path $OutputDirectory '4655-vu-meter.gif'
    $output = [System.IO.File]::Create($gifPath)
    try {
        $encoder.Save($output)
    }
    finally {
        $output.Dispose()
    }

    Remove-Item -LiteralPath $frameDir -Recurse -Force
    Write-Output $gifPath
}
finally {
    if ($form -ne $null) {
        $form.Close()
        $form.Dispose()
    }
}
