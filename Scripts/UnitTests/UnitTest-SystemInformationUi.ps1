<#
.SYNOPSIS
    Interactive #3176 System Information host: open the dialog and wait for System Summary rows.

.DESCRIPTION
    Hosts KryptonSystemInformation modelessly. WMI may return access-denied rows; a non-empty
    grid (including error/unavailable rows) is treated as success. Not for CI.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-SystemInformationUi.ps1
#>
# UnitTest-CI: exclude
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
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))

$form = [Krypton.Toolkit.Utilities.KryptonSystemInformation]::Show($null)
$sw = [System.Diagnostics.Stopwatch]::StartNew()
while ($sw.Elapsed.TotalSeconds -lt 30 -and -not $form.IsDisposed) {
    [System.Windows.Forms.Application]::DoEvents()
    Start-Sleep -Milliseconds 200
    $grids = $form.Controls.Find('kdgvDetails', $true)
    if ($grids.Count -gt 0 -and $grids[0].RowCount -gt 0) {
        Write-Host "PASS: System Information grid has $($grids[0].RowCount) row(s)" -ForegroundColor Green
        $form.Close()
        exit 0
    }
}

if (-not $form.IsDisposed) {
    $form.Close()
}

Write-Error 'System Information UI did not populate the details grid within 30 seconds (WMI may be blocked).'
exit 1
