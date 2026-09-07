<#
.SYNOPSIS
    Asserts KryptonSplitButton defaults: always-on splitter, accessibility role, and designer IsDefault.

.DESCRIPTION
    Loads Debug Krypton.Toolkit and checks the dedicated split-button subclass (#4366).

    Exit code 0 on success; non-zero on failure.
    Requires an STA apartment (use powershell -STA). Invoke-AllUnitTests launches include scripts with -STA.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-KryptonSplitButton.ps1
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
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))

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

Write-UnitTestBanner -Status INFO -Message 'Asserting KryptonSplitButton API'

[System.Windows.Forms.Application]::EnableVisualStyles()

$splitType = [Krypton.Toolkit.KryptonSplitButton]
$button = New-Object Krypton.Toolkit.KryptonSplitButton
try {
    Assert-True ($button -is [Krypton.Toolkit.KryptonDropButton]) 'KryptonSplitButton inherits KryptonDropButton'
    Assert-True (-not ($button -is [Krypton.Toolkit.KryptonButton])) 'KryptonSplitButton is not a KryptonButton'
    Assert-True ($button.Splitter) 'Splitter is true on construct'
    $button.Splitter = $false
    Assert-True ($button.Splitter) 'Splitter stays true when set to false'
    Assert-True $button.Values.IsDefault 'Values.IsDefault is true on construct'

    $mnemonic = $splitType.GetProperty('MnemonicPerformsDropDown', [System.Reflection.BindingFlags]'Instance,NonPublic,Public')
    Assert-True ($null -ne $mnemonic) 'MnemonicPerformsDropDown property exists'
    if ($null -ne $mnemonic) {
        Assert-True (-not [bool]$mnemonic.GetValue($button)) 'MnemonicPerformsDropDown is false'
    }

    $createAcc = $splitType.GetMethod('CreateAccessibilityInstance', [System.Reflection.BindingFlags]'Instance,NonPublic,Public')
    Assert-True ($null -ne $createAcc) 'CreateAccessibilityInstance exists'
    if ($null -ne $createAcc) {
        $acc = $createAcc.Invoke($button, @())
        Assert-True ($acc.Role -eq [System.Windows.Forms.AccessibleRole]::SplitButton) 'Accessible role is SplitButton'
    }
}
finally {
    $button.Dispose()
}

if ($failed.Count -gt 0) {
    Write-UnitTestBanner -Status FAIL -Message "$($failed.Count) assertion(s) failed"
    exit 1
}

Write-UnitTestBanner -Status PASS -Message 'KryptonSplitButton assertions passed'
exit 0
