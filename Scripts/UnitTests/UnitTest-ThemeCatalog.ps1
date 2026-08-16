<#
.SYNOPSIS
    Asserts #4230 theme catalog: core Sparkle, extra Themes discovery, unimplemented modes.

.DESCRIPTION
    Loads Debug Krypton.Interop + Krypton.Toolkit, then Krypton.Themes from Bin.

    1. SparkleBlue is a core mode.
    2. After loading Themes.dll, VisualStudio2022Dark is available.
    3. GetUnimplementedBuiltinModes is empty when Themes is loaded.
    4. GetThemesArray(false) is shorter than GetThemesArray(true).

    Exit code 0 on success; non-zero on failure.
    Requires an STA apartment (use powershell -STA). Invoke-AllUnitTests launches include scripts with -STA.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-ThemeCatalog.ps1
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

$sparkleBlue = [Krypton.Toolkit.PaletteMode]::SparkleBlue
Assert-True ([Krypton.Toolkit.KryptonThemeCatalog]::IsCoreMode($sparkleBlue)) 'SparkleBlue is a core Toolkit palette'

$themesPath = Join-Path $bin 'Krypton.Themes.dll'
Assert-True (Test-Path -LiteralPath $themesPath) 'Krypton.Themes.dll exists in the bin folder'

[void][System.Reflection.Assembly]::LoadFrom($themesPath)
[Krypton.Toolkit.KryptonThemeCatalog]::DiscoverThemes()

$vsDark = [Krypton.Toolkit.PaletteMode]::VisualStudio2022Dark
Assert-True ([Krypton.Toolkit.KryptonThemeCatalog]::IsImplementationAvailable($vsDark)) 'VisualStudio2022Dark is available after loading Themes'
Assert-True (-not [Krypton.Toolkit.KryptonThemeCatalog]::IsCoreMode($vsDark)) 'VisualStudio2022Dark is not a core palette'

$missing = [Krypton.Toolkit.KryptonThemeCatalog]::GetUnimplementedBuiltinModes()
Assert-True ($missing.Length -eq 0) ("GetUnimplementedBuiltinModes is empty (count=$($missing.Length))")

$all = [Krypton.Toolkit.ThemeManager]::GetThemesArray($true)
$core = [Krypton.Toolkit.ThemeManager]::GetThemesArray($false)
Assert-True ($all.Length -gt $core.Length) "GetThemesArray(true) ($($all.Length)) lists more names than cores-only ($($core.Length))"

if ($failed.Count -gt 0) {
    Write-Host "$($failed.Count) assertion(s) failed." -ForegroundColor Red
    exit 1
}

Write-Host 'UnitTest-ThemeCatalog passed.' -ForegroundColor Green
exit 0
