<#
.SYNOPSIS
    Prints RealWindowBorders and caption-strip geometry for NavigatorFormIntegrationDemo.

.DESCRIPTION
    Instantiates the demo briefly, dumps form/strip diagnostics, then closes. Useful when
    debugging non-client coordinate conversion for caption tabs.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\Get-NavigatorCaptionTabProbe.ps1
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

Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Navigator.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Navigator.Utilities.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

[System.Windows.Forms.Application]::EnableVisualStyles()
$formType = $asm.GetType('TestForm.NavigatorFormIntegrationDemo')
$form = [System.Activator]::CreateInstance($formType)

$form.add_Shown({
    $f = $form
    Write-Host "form type       : $($f.GetType().FullName)"
    Write-Host "is KryptonForm  : $($f -is [Krypton.Toolkit.KryptonForm])"
    Write-Host "RealWindowBorders: $($f.RealWindowBorders)"
    Write-Host "Bounds          : $($f.Bounds)"
    Write-Host "clientOrigin    : $($f.PointToScreen([System.Drawing.Point]::Empty))"

    $flags = [System.Reflection.BindingFlags]::NonPublic -bor [System.Reflection.BindingFlags]::Instance
    $integ = $f.GetType().GetField('kryptonNavigatorFormIntegrator1', $flags).GetValue($f)
    $strip = $integ.GetType().GetField('_captionTabs', $flags).GetValue($integ)
    Write-Host "strip           : $($strip.GetType().Name)"
    Write-Host "strip owner     : $($strip.OwningControl)"
    Write-Host "strip rect      : $($strip.ClientRectangle)"
    $f.Close()
})

[System.Windows.Forms.Application]::Run($form)
