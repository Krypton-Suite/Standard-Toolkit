# UnitTest-CI: exclude
<#
.SYNOPSIS
    Captures CustomRendererDemo with the Luna renderer and the stripe renderer.
.DESCRIPTION
    Hosts CustomRendererDemo from Debug TestForm and writes two PrintWindow PNGs
    under Documents/PR/.
.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Invoke-CustomRendererScreenshot.ps1
#>
#Requires -Version 5.1
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')
$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir
Register-UnitTestAssemblyResolver -BinDir $bin
Initialize-UnitTestNativeInput

foreach ($name in @('Krypton.Interop.dll', 'Krypton.Resources.dll', 'Krypton.Toolkit.dll', 'Krypton.Themes.dll')) {
    $path = Join-Path $bin $name
    if (-not (Test-Path -LiteralPath $path)) {
        throw "Missing $path - build TestForm Debug $TargetFramework first."
    }
    [void][Reflection.Assembly]::LoadFrom($path)
}

$testFormPath = Join-Path $bin 'TestForm.exe'
if (-not (Test-Path -LiteralPath $testFormPath)) {
    throw "Missing $testFormPath - build TestForm Debug $TargetFramework first."
}

[void][Reflection.Assembly]::LoadFrom($testFormPath)
$demoType = [Type]::GetType('TestForm.CustomRendererDemo, TestForm', $true)
$outDir = Join-Path $repoRoot 'Documents\PR'
New-Item -ItemType Directory -Force -Path $outDir | Out-Null

[System.Windows.Forms.Application]::EnableVisualStyles()

function Show-Demo {
    $form = [Activator]::CreateInstance($demoType)
    $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
    $form.Location = New-Object System.Drawing.Point(80, 80)
    $form.TopMost = $true
    $form.Show()
    $form.Activate()
    $form.BringToFront()
    [void][UnitTestNative]::SetForegroundWindow($form.Handle)
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 400
    return $form
}

$luna = Show-Demo
$luna.UseLunaRenderer()
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 200
Save-UnitTestWindowPng -Form $luna -Path (Join-Path $outDir 'luna-custom-renderer-luna.png')
$luna.Close()
$luna.Dispose()

$stripe = Show-Demo
$stripe.UseStripeRenderer()
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 200
Save-UnitTestWindowPng -Form $stripe -Path (Join-Path $outDir 'luna-custom-renderer-stripe.png')
$stripe.Close()
$stripe.Dispose()