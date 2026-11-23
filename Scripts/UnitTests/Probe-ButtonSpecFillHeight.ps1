<#
.SYNOPSIS
    Probes ButtonSpec client rectangles for Bug4432 FillHeight.

# UnitTest-CI: exclude
#>
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472'
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework
Register-UnitTestAssemblyResolver -BinDir $bin
Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Interop.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$asm = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'TestForm.exe'))

function Find-KryptonTextBox($control) {
    if ($control -is [Krypton.Toolkit.KryptonTextBox]) {
        return $control
    }
    foreach ($child in $control.Controls) {
        $found = Find-KryptonTextBox $child
        if ($null -ne $found) {
            return $found
        }
    }
    return $null
}

$form = [System.Activator]::CreateInstance($asm.GetType('TestForm.Bug4432ButtonSpecFillHeightDemo'))
$form.Show()
[System.Windows.Forms.Application]::DoEvents()
Start-Sleep -Milliseconds 500
[System.Windows.Forms.Application]::DoEvents()

$tb = Find-KryptonTextBox $form
if ($null -eq $tb) {
    throw 'KryptonTextBox not found'
}

$flags = [System.Reflection.BindingFlags]'Instance,NonPublic,Public'
$mgrField = $tb.GetType().GetField('_buttonManager', $flags)
$mgr = $mgrField.GetValue($tb)
$getRect = $mgr.GetType().GetMethod('GetButtonRectangle', [type[]]@([Krypton.Toolkit.ButtonSpec]))

Write-Output "TextBox Height=$($tb.Height)"
foreach ($spec in $tb.ButtonSpecs) {
    $r = $getRect.Invoke($mgr, @([Krypton.Toolkit.ButtonSpec]$spec))
    Write-Output ("FillHeight={0} Type={1} Rect={2}" -f $spec.FillHeight, $spec.Type, $r)
}

$form.Close()
$form.Dispose()
