<#
.SYNOPSIS
    Opens RadialMenuDemo, shows the native radial menu, and captures a PNG.

.DESCRIPTION
    Loads TestForm assemblies in-process (STA), shows RadialMenuDemo, invokes the native
    KryptonRadialMenu at the form centre, captures the window (plus popup padding via
    Save-UnitTestWindowPng), and writes Documents/PR/4172-radial-menu-native.png
    (or -OutputPath).

    # UnitTest-CI: exclude

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-RadialMenuScreenshot.ps1
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
    $OutputPath = Join-Path $repoRoot 'Documents\PR\4172-radial-menu-native.png'
}

Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
Initialize-UnitTestNativeInput
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.RadialMenuDemo')
if (-not $formType) {
    throw 'Type TestForm.RadialMenuDemo was not found in TestForm.exe.'
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
Start-Sleep -Milliseconds 500

$radialField = $formType.GetField('_radialMenu', [System.Reflection.BindingFlags]'Instance,NonPublic')
$radial = $radialField.GetValue($form)
$showMethod = $radial.GetType().GetMethod('Show', [type[]]@([object], [System.Drawing.Point]))
$clientCentre = New-Object System.Drawing.Point ([int]($form.ClientSize.Width / 2), [int]($form.ClientSize.Height / 2))
$screenPt = $form.PointToScreen($clientCentre)
[void]$showMethod.Invoke($radial, @($form, $screenPt))
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 700
[System.Windows.Forms.Application]::DoEvents()

# Inflate so the radial popup centred on the form is included (screen blit after TopMost).
Save-UnitTestWindowPng -Form $form -Path $OutputPath -InflateX 180 -InflateY 180 -SettleMs 200

$form.Close()
$form.Dispose()
