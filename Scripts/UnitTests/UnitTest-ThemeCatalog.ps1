<#
.SYNOPSIS
    Asserts #4230 theme catalog: cores, discovery, availability, export/import, sample provider.

.DESCRIPTION
    Loads Debug Krypton.Interop + Krypton.Toolkit, asserts unimplemented extras, then loads
    Krypton.Themes and ThemeProviderSample.

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
$sparkleDark = [Krypton.Toolkit.PaletteMode]::SparkleBlueDarkMode
$vsDark = [Krypton.Toolkit.PaletteMode]::VisualStudio2022Dark

Assert-True ([Krypton.Toolkit.KryptonThemeCatalog]::IsCoreMode($sparkleBlue)) 'SparkleBlue is a core Toolkit palette'

$themesPath = Join-Path $bin 'Krypton.Themes.dll'
Assert-True (Test-Path -LiteralPath $themesPath) 'Krypton.Themes.dll exists in the bin folder'

# Toolkit-directory probe should load sibling Krypton.Themes.dll without an explicit LoadFrom.
[Krypton.Toolkit.KryptonThemeCatalog]::DiscoverThemes()
Assert-True ([Krypton.Toolkit.KryptonThemeCatalog]::IsImplementationAvailable($vsDark)) 'VisualStudio2022Dark is available via Toolkit-directory probe'
Assert-True (-not [Krypton.Toolkit.KryptonThemeCatalog]::IsCoreMode($vsDark)) 'VisualStudio2022Dark is not a core palette'

$coreDescriptors = [Krypton.Toolkit.KryptonThemeCatalog]::GetDescriptors()
Assert-True ($coreDescriptors.Length -ge 14) "GetDescriptors has at least 14 cores ($($coreDescriptors.Length))"

$families = [Krypton.Toolkit.KryptonThemeCatalog]::GetFamilies()
Assert-True ($families.Length -ge 1) 'GetFamilies is not empty'

$missing = [Krypton.Toolkit.KryptonThemeCatalog]::GetUnimplementedBuiltinModes()
Assert-True ($missing.Length -eq 0) ("GetUnimplementedBuiltinModes is empty (count=$($missing.Length))")

$all = [Krypton.Toolkit.ThemeManager]::GetThemesArray($true)
$core = [Krypton.Toolkit.ThemeManager]::GetThemesArray($false)
Assert-True ($all.Length -gt $core.Length) "GetThemesArray(true) ($($all.Length)) lists more names than cores-only ($($core.Length))"

[Krypton.Toolkit.KryptonThemeAvailability]::Reset()
Assert-True ([Krypton.Toolkit.KryptonThemeAvailability]::IsSelectable($sparkleDark)) 'Sparkle extra is selectable after Themes loads'
[Krypton.Toolkit.KryptonThemeAvailability]::SetFamilyEnabled([Krypton.Toolkit.KryptonThemeFamilies]::Sparkle, $false, $true)
Assert-True ([Krypton.Toolkit.KryptonThemeAvailability]::IsSelectable($sparkleBlue)) 'extraOnly Sparkle keeps SparkleBlue selectable'
Assert-True (-not [Krypton.Toolkit.KryptonThemeAvailability]::IsSelectable($sparkleDark)) 'extraOnly Sparkle hides SparkleBlueDarkMode'

$exported = [Krypton.Toolkit.KryptonThemeAvailability]::Export()
[Krypton.Toolkit.KryptonThemeAvailability]::Reset()
Assert-True ([Krypton.Toolkit.KryptonThemeAvailability]::IsSelectable($sparkleDark)) 'Reset restores Sparkle extra selectable'
[Krypton.Toolkit.KryptonThemeAvailability]::Import($exported)
Assert-True (-not [Krypton.Toolkit.KryptonThemeAvailability]::IsSelectable($sparkleDark)) 'Import restores extraOnly Sparkle hide'
[Krypton.Toolkit.KryptonThemeAvailability]::Reset()

$sampleProj = Join-Path $repoRoot 'Source\TestHarnesses\ThemeProviderSample\ThemeProviderSample.csproj'
dotnet build $sampleProj -c $Configuration -f $TargetFramework --nologo | Out-Null
$sampleDll = Join-Path $bin 'ThemeProviderSample.dll'
if (-not (Test-Path -LiteralPath $sampleDll)) {
    $alts = @(
        (Join-Path $repoRoot "Source\TestHarnesses\ThemeProviderSample\bin\$Configuration\$TargetFramework\ThemeProviderSample.dll"),
        (Join-Path $repoRoot "Source\TestHarnesses\ThemeProviderSample\bin\Any CPU\$Configuration\$TargetFramework\ThemeProviderSample.dll")
    )
    foreach ($alt in $alts) {
        if (Test-Path -LiteralPath $alt) {
            Copy-Item -LiteralPath $alt -Destination $sampleDll -Force
            break
        }
    }
}

Assert-True (Test-Path -LiteralPath $sampleDll) 'ThemeProviderSample.dll is available'
[void][System.Reflection.Assembly]::LoadFrom($sampleDll)
[Krypton.Toolkit.KryptonThemeCatalog]::DiscoverThemes()
$sampleLoaded = $false
foreach ($assembly in [AppDomain]::CurrentDomain.GetAssemblies()) {
    if ($assembly.GetName().Name -eq 'ThemeProviderSample') {
        $sampleLoaded = $true
        break
    }
}
Assert-True $sampleLoaded 'ThemeProviderSample assembly is loaded'

if ($failed.Count -gt 0) {
    Write-Host "$($failed.Count) assertion(s) failed." -ForegroundColor Red
    exit 1
}

Write-Host 'UnitTest-ThemeCatalog passed.' -ForegroundColor Green
exit 0
