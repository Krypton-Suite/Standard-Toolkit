<#
.SYNOPSIS
    Asserts #4326: KryptonTreeView.MultiSelect can be set to false independently of CheckBoxes.

.DESCRIPTION
    Loads Debug Krypton.Toolkit binaries and runs an in-process STA check:

    1. A freshly constructed KryptonTreeView reports MultiSelect = false.
    2. TypeDescriptor SetValue(false) after SetValue(true) reads back false.
    3. With CheckBoxes = true, SetValue(false) still reads back false (does not snap to true).
    4. After handle creation on a host form, MultiSelect remains independently settable.

    Exit code 0 on success; non-zero on failure.
    Requires an STA apartment (use powershell -STA). Invoke-AllUnitTests launches include scripts with -STA.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-TreeViewMultiSelect.ps1
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

function Assert-Equal {
    param($Expected, $Actual, [string]$Message)
    if (-not [object]::Equals($Expected, $Actual)) {
        $failed.Add("$Message (expected='$Expected' actual='$Actual')")
        Write-Host "FAIL: $Message (expected='$Expected' actual='$Actual')" -ForegroundColor Red
    }
    else {
        Write-Host "PASS: $Message" -ForegroundColor Green
    }
}

function Get-NetObject {
    param($Value)
    if ($null -eq $Value) { return $null }
    if ($Value -is [System.Management.Automation.PSObject]) {
        return $Value.PSObject.BaseObject
    }
    return $Value
}

function Get-MultiSelectDescriptorValue {
    param($Tree)
    $pd = [System.ComponentModel.TypeDescriptor]::GetProperties($Tree).Find('MultiSelect', $false)
    return [bool]$pd.GetValue($Tree)
}

function Set-MultiSelectDescriptorValue {
    param($Tree, [bool]$Value)
    $pd = [System.ComponentModel.TypeDescriptor]::GetProperties($Tree).Find('MultiSelect', $false)
    $pd.SetValue($Tree, $Value)
}

Write-UnitTestBanner -Status INFO -Message 'Asserting #4326 KryptonTreeView MultiSelect can be set to false'

[System.Windows.Forms.Application]::EnableVisualStyles()
[System.Windows.Forms.Application]::SetCompatibleTextRenderingDefault($false)

$tree = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonTreeView]))
Assert-Equal $false $tree.MultiSelect 'Fresh KryptonTreeView.MultiSelect defaults to false'
Assert-Equal $false $tree.CheckBoxes 'Fresh KryptonTreeView.CheckBoxes defaults to false'
Assert-Equal $false (Get-MultiSelectDescriptorValue -Tree $tree) 'Fresh TypeDescriptor MultiSelect is false'

Set-MultiSelectDescriptorValue -Tree $tree -Value $true
Assert-Equal $true $tree.MultiSelect 'TypeDescriptor can set MultiSelect to true'
Set-MultiSelectDescriptorValue -Tree $tree -Value $false
Assert-Equal $false $tree.MultiSelect 'TypeDescriptor can set MultiSelect back to false'
Assert-Equal $false (Get-MultiSelectDescriptorValue -Tree $tree) 'TypeDescriptor reads MultiSelect false after set'

$tree.CheckBoxes = $true
Assert-Equal $false $tree.MultiSelect 'CheckBoxes true does not force MultiSelect true'
Set-MultiSelectDescriptorValue -Tree $tree -Value $false
Assert-Equal $false $tree.MultiSelect 'MultiSelect stays false when CheckBoxes is true'
Assert-Equal $false (Get-MultiSelectDescriptorValue -Tree $tree) 'TypeDescriptor MultiSelect stays false when CheckBoxes is true'

$form = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonForm]))
$form.Text = 'UnitTest-4326-TreeViewMultiSelect'
$form.ShowInTaskbar = $false
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
$form.Location = New-Object System.Drawing.Point(-32000, -32000)
$form.Size = New-Object System.Drawing.Size(320, 240)

$hosted = Get-NetObject ([System.Activator]::CreateInstance([Krypton.Toolkit.KryptonTreeView]))
$hosted.Dock = [System.Windows.Forms.DockStyle]::Fill
[void]$form.Controls.Add($hosted)
[void]$form.Show()
[System.Windows.Forms.Application]::DoEvents()

Assert-Equal $false $hosted.MultiSelect 'Hosted fresh tree MultiSelect is false after handle creation'
Assert-Equal $false $hosted.CheckBoxes 'Hosted fresh tree CheckBoxes is false after handle creation'
Set-MultiSelectDescriptorValue -Tree $hosted -Value $true
Set-MultiSelectDescriptorValue -Tree $hosted -Value $false
Assert-Equal $false $hosted.MultiSelect 'Hosted tree MultiSelect can be set to false after handle creation'
$hosted.CheckBoxes = $true
Set-MultiSelectDescriptorValue -Tree $hosted -Value $false
Assert-Equal $false $hosted.MultiSelect 'Hosted tree MultiSelect stays false with CheckBoxes after handle creation'

$form.Close()
$form.Dispose()
$tree.Dispose()

if ($failed.Count -gt 0) {
    Write-UnitTestBanner -Status FAIL -Message ("#4326 assertions failed ($($failed.Count))")
    exit 1
}

Write-UnitTestBanner -Status PASS -Message '#4326 KryptonTreeView MultiSelect assertions passed'
exit 0
