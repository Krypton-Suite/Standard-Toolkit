<#
.SYNOPSIS
    Asserts #4270 / #4269 KryptonLog secret redaction before file storage.

.DESCRIPTION
    Loads Debug Krypton.Toolkit.Utilities and writes a structured event with a {Password}
    property to a temp rolling file (sync). Asserts the secret is stored as *** and that a
    non-secret property is left intact.

    Exit code 0 on success; non-zero on failure.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-KryptonLogProtect.ps1
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

[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Interop.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))

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

Write-UnitTestBanner -Status INFO -Message 'Asserting #4270 / #4269 KryptonLog secret redaction'

$workDir = Join-Path ([System.IO.Path]::GetTempPath()) ('krypton-4270-' + [guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $workDir | Out-Null
$logPath = Join-Path $workDir 'protect.log'
$xmlPath = Join-Path $workDir 'protect.xml'
$secret = 'should-not-appear-in-log'

try {
    $xml = @"
<kryptonLog minimumLevel="Trace" async="false">
  <writeTo>
    <file path="$logPath" rollOnDate="false" />
  </writeTo>
</kryptonLog>
"@
    $utf8Bom = New-Object System.Text.UTF8Encoding $true
    [System.IO.File]::WriteAllText($xmlPath, $xml, $utf8Bom)

    [Krypton.Toolkit.Utilities.KryptonLog]::ConfigureFromXml($xmlPath)
    $logger = [Krypton.Toolkit.Utilities.KryptonLog]::ForContext('UnitTest.4270')
    $level = [Krypton.Toolkit.Utilities.KryptonLogLevel]::Information
    $logger.Write($level, 'Secret probe {Password} {Number}', [object[]]@($secret, 42))
    $logger.Write($level, 'Safe probe {UserName}', [object[]]@('alice'))
    [Krypton.Toolkit.Utilities.KryptonLog]::CloseAndFlush()

    Assert-True (Test-Path -LiteralPath $logPath) 'Rolling log file was created'
    $text = [System.IO.File]::ReadAllText($logPath)
    Assert-True ($text.IndexOf('***') -ge 0) 'Password property stored as ***'
    Assert-True ($text.IndexOf($secret) -lt 0) 'Password value is not stored in clear text'
    Assert-True ($text.IndexOf('42') -ge 0) 'Non-secret Number property is stored'
    Assert-True ($text.IndexOf('alice') -ge 0) 'Non-secret UserName property is stored'
}
finally {
    try { [Krypton.Toolkit.Utilities.KryptonLog]::CloseAndFlush() } catch { }
    if (Test-Path -LiteralPath $workDir) {
        Remove-Item -LiteralPath $workDir -Recurse -Force -ErrorAction SilentlyContinue
    }
}

if ($failed.Count -gt 0) {
    Write-Host ("{0} assertion(s) failed." -f $failed.Count) -ForegroundColor Red
    exit 1
}

Write-Host 'All #4270 / #4269 KryptonLog protect assertions passed.' -ForegroundColor Green
exit 0
