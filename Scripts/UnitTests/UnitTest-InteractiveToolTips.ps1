<#
.SYNOPSIS
    Asserts #4192 interactive tooltip API surface.

.DESCRIPTION
    Reflection smoke check against Debug binaries:

    - KryptonToolTip hosted SetToolTip / SetLinkToolTip / LinkClicked
    - ToolTipValues.HostedContent
    - KryptonNotifyIcon.ShowPopupTip
    - KryptonHtmlToolTipContent.Create

    Exit 0 on success. Marker: include.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-InteractiveToolTips.ps1
#>
# UnitTest-CI: include
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

[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Interop.dll'))
$toolkit = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$utilities = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))

$failed = New-Object System.Collections.Generic.List[string]

function Assert-True {
    param([bool]$Condition, [string]$Message)
    if (-not $Condition) {
        $failed.Add($Message)
        Write-Host "FAIL: $Message" -ForegroundColor Red
    }
    else {
        Write-Host "PASS: $Message" -ForegroundColor Green
    }
}

$tipType = $toolkit.GetType('Krypton.Toolkit.KryptonToolTip', $true)
Assert-True ($null -ne $tipType.GetMethod('SetLinkToolTip')) 'KryptonToolTip.SetLinkToolTip exists'
Assert-True ($null -ne $tipType.GetEvent('LinkClicked')) 'KryptonToolTip.LinkClicked exists'
Assert-True ($null -ne $tipType.GetProperty('EnableInteractiveKeyboard')) 'EnableInteractiveKeyboard exists'
Assert-True ($null -ne $tipType.GetProperty('UseCloseTimerForInteractive')) 'UseCloseTimerForInteractive exists'
Assert-True ($null -ne $tipType.GetProperty('DismissInteractiveOnTargetMouseDown')) 'DismissInteractiveOnTargetMouseDown exists'
Assert-True ($null -ne $tipType.GetMethod('GetKryptonToolTipContent')) 'GetKryptonToolTipContent extender exists'

$valuesType = $toolkit.GetType('Krypton.Toolkit.ToolTipValues', $true)
Assert-True ($null -ne $valuesType.GetProperty('HostedContent')) 'ToolTipValues.HostedContent exists'

$notifyType = $toolkit.GetType('Krypton.Toolkit.KryptonNotifyIcon', $true)
Assert-True ($null -ne $notifyType.GetMethod('ShowPopupTip')) 'KryptonNotifyIcon.ShowPopupTip exists'
Assert-True ($null -ne $notifyType.GetProperty('KryptonContextMenu')) 'KryptonNotifyIcon.KryptonContextMenu exists'

$htmlType = $utilities.GetType('Krypton.Toolkit.Utilities.KryptonHtmlToolTipContent', $true)
Assert-True ($null -ne $htmlType) 'KryptonHtmlToolTipContent type exists'
$create = $htmlType.GetMethod('Create')
Assert-True ($null -ne $create) 'KryptonHtmlToolTipContent.Create exists'
$html = $create.Invoke($null, @('Hello <a href="https://example.com">link</a>'))
Assert-True ($null -ne $html) 'Create returns a control'
Assert-True ($html.Controls.Count -ge 2) 'HTML fragment produced text and link children'

if ($failed.Count -gt 0) {
    Write-Host "$($failed.Count) assertion(s) failed." -ForegroundColor Red
    exit 1
}

Write-Host 'UnitTest-InteractiveToolTips passed.' -ForegroundColor Green
exit 0
