<#
.SYNOPSIS
    Smoke-tests banner/pulse manifest parse.

.DESCRIPTION
    # UnitTest-CI: exclude
#>
[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')
$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration Debug -TargetFramework net472
Register-UnitTestAssemblyResolver -BinDir $bin
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Interop.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Resources.dll'))
[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
$utilities = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.Utilities.dll'))
$parser = $utilities.GetType('Krypton.Toolkit.Utilities.KryptonUpdateManifestParser')
$parse = $parser.GetMethod('Parse', [System.Reflection.BindingFlags]'NonPublic,Static')
$json = '{"version":"9.9.9","severity":"important","packageKind":"installer","packageUrl":"https://example.com/a.exe","sha256":"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa","banner":{"text":"Hi","backColor":"#8B0000","foreColor":"#FFFFFF"},"pulse":{"enable":true,"color":"#FF4500","style":"All","speed":1.5}}'
$catalog = $parse.Invoke($null, @($json))
$package = $catalog.Packages[0]
if ($package.Banner.Text -ne 'Hi') { throw 'banner text mismatch' }
if (-not $package.Pulse.Enable) { throw 'pulse enable mismatch' }
$bad = '{"version":"1.0.0","packageUrl":"https://example.com/a.exe","sha256":"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa","banner":{"imageUrl":"http://example.com/x.png"}}'
try {
    [void]$parse.Invoke($null, @($bad))
    throw 'http imageUrl should fail'
}
catch {
    $message = $_.Exception.Message
    if ($_.Exception.InnerException) {
        $message = $_.Exception.InnerException.Message
    }

    if ($message -notmatch 'https') {
        throw
    }
}

Write-Output 'PASS banner/pulse parse'
