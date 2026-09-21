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

$themesPath = Join-Path $bin 'Krypton.Themes.dll'
$themesBackup = Join-Path $bin 'Krypton.Themes.dll.unittest-backup'
$themesHiddenForFallback = $false
if (Test-Path -LiteralPath $themesPath) {
    Move-Item -LiteralPath $themesPath -Destination $themesBackup -Force
    $themesHiddenForFallback = $true
}

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

$allModes = @([enum]::GetValues([Krypton.Toolkit.PaletteMode]))
Assert-True ([int][Krypton.Toolkit.PaletteMode]::Global -eq -1) 'PaletteMode.Global is -1'
$custom = [Krypton.Toolkit.PaletteMode]::Custom
$maxValue = ($allModes | ForEach-Object { [int]$_ } | Measure-Object -Maximum).Maximum
Assert-True ([int]$custom -eq $maxValue) 'PaletteMode.Custom has the highest integer (remains last)'

$map = [Krypton.Toolkit.PaletteModeStrings]::SupportedThemesMap
Assert-True ($map.Count -eq ($allModes.Length - 1)) "SupportedThemesMap count ($($map.Count)) equals enum length minus Global ($($allModes.Length - 1))"

$mapValues = @($map.Values)
Assert-True ($mapValues[$mapValues.Length - 1] -eq $custom) 'SupportedThemesMap lists Custom last'

$missingFromMap = @()
foreach ($mode in $allModes) {
    if ($mode -eq [Krypton.Toolkit.PaletteMode]::Global) {
        continue
    }

    $found = $false
    foreach ($mapped in $mapValues) {
        if ($mapped -eq $mode) {
            $found = $true
            break
        }
    }

    if (-not $found) {
        $missingFromMap += $mode
    }
}
Assert-True ($missingFromMap.Length -eq 0) ("Every PaletteMode except Global is in SupportedThemesMap (missing: $($missingFromMap -join ', '))")

$sparkleBlue = [Krypton.Toolkit.PaletteMode]::SparkleBlue
$sparkleDark = [Krypton.Toolkit.PaletteMode]::SparkleBlueDarkMode
$vsDark = [Krypton.Toolkit.PaletteMode]::VisualStudio2022Dark

Assert-True ([Krypton.Toolkit.KryptonThemeCatalog]::IsCoreMode($sparkleBlue)) 'SparkleBlue is a core Toolkit palette'
Assert-True ([Krypton.Toolkit.KryptonThemeCatalog]::CorePaletteCount -eq 14) 'CorePaletteCount is 14 after core registration'

Assert-True ([Krypton.Toolkit.KryptonThemeCatalog]::ShowMissingThemeWarningDialog) 'ShowMissingThemeWarningDialog is true by default (opt-out)'
Assert-True ([Krypton.Toolkit.KryptonManager]::ShowMissingThemeWarningDialog) 'KryptonManager.ShowMissingThemeWarningDialog forwards to KryptonThemeCatalog'
Assert-True ([Krypton.Toolkit.KryptonManager]::Strings.MiscellaneousThemeStrings.ThemeFallbackWarningTitle -eq 'Theme Fallback Warning') 'ThemeFallbackWarningTitle has default value'
Assert-True ([Krypton.Toolkit.KryptonManager]::Strings.MiscellaneousThemeStrings.ThemeFallbackWarningMessage.Length -gt 0) 'ThemeFallbackWarningMessage has default template'

# Missing-theme fallback when Krypton.Themes.dll is absent (Toolkit-only scenario).
$fallbackState = New-Object PSObject -Property @{
    Fired     = $false
    Requested = $null
    Reason    = $null
}
$fallbackHandler = {
    param($sender, $e)
    $script:fallbackState.Fired = $true
    $script:fallbackState.Requested = $e.RequestedMode
    $script:fallbackState.Reason = $e.Reason
    $e.Handled = $true # Suppress warning dialog during automated test
}
[Krypton.Toolkit.KryptonThemeCatalog]::add_MissingThemeFallback($fallbackHandler)
$fallbackPalette = [Krypton.Toolkit.KryptonThemeCatalog]::GetPalette($vsDark)
Assert-True $fallbackState.Fired 'MissingThemeFallback fires when extra mode requested without Themes'
Assert-True ($fallbackState.Requested -eq $vsDark) 'MissingThemeFallback reports requested mode'
Assert-True ($fallbackState.Reason.Length -gt 0) 'MissingThemeFallback provides a descriptive reason'
Assert-True ($fallbackPalette.GetType().Name -eq 'PaletteMicrosoft365Blue') 'Missing extra falls back to Microsoft 365 Blue palette type'
[Krypton.Toolkit.KryptonThemeCatalog]::remove_MissingThemeFallback($fallbackHandler)

if ($themesHiddenForFallback) {
    Move-Item -LiteralPath $themesBackup -Destination $themesPath -Force
    $themesHiddenForFallback = $false
    [void][System.Reflection.Assembly]::LoadFrom($themesPath)
}

Assert-True (Test-Path -LiteralPath $themesPath) 'Krypton.Themes.dll exists in the bin folder'

# Toolkit-directory probe should load sibling Krypton.Themes.dll without an explicit LoadFrom.
[Krypton.Toolkit.KryptonThemeCatalog]::DiscoverThemes()
Assert-True ([Krypton.Toolkit.KryptonThemeCatalog]::IsImplementationAvailable($vsDark)) 'VisualStudio2022Dark is available via Toolkit-directory probe'
Assert-True (-not [Krypton.Toolkit.KryptonThemeCatalog]::IsCoreMode($vsDark)) 'VisualStudio2022Dark is not a core palette'

$descriptors = [Krypton.Toolkit.KryptonThemeCatalog]::GetDescriptors()
$coreCount = @($descriptors | Where-Object { $_.IsCore }).Count
Assert-True ($coreCount -eq [Krypton.Toolkit.KryptonThemeCatalog]::CorePaletteCount) "Core descriptor count is $([Krypton.Toolkit.KryptonThemeCatalog]::CorePaletteCount) (actual=$coreCount)"

$families = [Krypton.Toolkit.KryptonThemeCatalog]::GetFamilies()
Assert-True ($families.Length -ge 1) 'GetFamilies is not empty'

$missing = [Krypton.Toolkit.KryptonThemeCatalog]::GetUnimplementedBuiltinModes()
Assert-True ($missing.Length -eq 0) ("GetUnimplementedBuiltinModes is empty (count=$($missing.Length))")

$materialize = [Krypton.Toolkit.PaletteMode]::Office2007MaterializeBlue
Assert-True ([Krypton.Toolkit.KryptonThemeCatalog]::IsImplementationAvailable($materialize)) 'Office2007MaterializeBlue is available via Themes'
Assert-True (-not [Krypton.Toolkit.KryptonThemeCatalog]::IsCoreMode($materialize)) 'Office2007MaterializeBlue is not a core palette'
Assert-True ($families -contains [Krypton.Toolkit.KryptonThemeFamilies]::Materialize) 'Materialize family is registered'

$materializeDesc = $null
$gotMaterialize = [Krypton.Toolkit.KryptonThemeCatalog]::TryGetDescriptor($materialize, [ref]$materializeDesc)
Assert-True $gotMaterialize 'TryGetDescriptor returns Office2007MaterializeBlue'
Assert-True ($materializeDesc.Family -eq [Krypton.Toolkit.KryptonThemeFamilies]::Materialize) 'Office2007MaterializeBlue family is Materialize'
Assert-True ($materializeDesc.ChromeKind -eq [Krypton.Toolkit.KryptonThemeChromeKind]::Office2007) 'Office2007MaterializeBlue chrome is Office2007'
Assert-True ([Krypton.Toolkit.KryptonThemeChrome]::GetChromeKind($sparkleBlue) -eq [Krypton.Toolkit.KryptonThemeChromeKind]::Sparkle) 'SparkleBlue chrome is Sparkle'
$vs2010_2007 = [Krypton.Toolkit.PaletteMode]::VisualStudio2010Render2007
Assert-True ([Krypton.Toolkit.KryptonThemeChrome]::GetChromeKind($vs2010_2007) -eq [Krypton.Toolkit.KryptonThemeChromeKind]::Office2007) 'VS2010 Render2007 chrome is Office2007'
Assert-True ([Krypton.Toolkit.KryptonThemeChrome]::GetShieldIconStyle($vs2010_2007) -eq [Krypton.Toolkit.KryptonThemeShieldIconStyle]::Windows7) 'VS2010 Render2007 shield is Windows7'

$deuteranopia = [Krypton.Toolkit.PaletteMode]::Deuteranopia
Assert-True ($families -contains [Krypton.Toolkit.KryptonThemeFamilies]::Accessibility) 'Accessibility family is registered'
$accessDesc = $null
Assert-True ([Krypton.Toolkit.KryptonThemeCatalog]::TryGetDescriptor($deuteranopia, [ref]$accessDesc)) 'TryGetDescriptor returns Deuteranopia'
Assert-True ($accessDesc.Family -eq [Krypton.Toolkit.KryptonThemeFamilies]::Accessibility) 'Deuteranopia family is Accessibility'

$limeGreen = [Krypton.Toolkit.PaletteMode]::Office2007LimeGreen
Assert-True ($families -contains [Krypton.Toolkit.KryptonThemeFamilies]::LimeGreen) 'LimeGreen family is registered'
$limeDesc = $null
Assert-True ([Krypton.Toolkit.KryptonThemeCatalog]::TryGetDescriptor($limeGreen, [ref]$limeDesc)) 'TryGetDescriptor returns Office2007LimeGreen'
Assert-True ($limeDesc.Family -eq [Krypton.Toolkit.KryptonThemeFamilies]::LimeGreen) 'Office2007LimeGreen family is LimeGreen'

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
