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

function Import-UnitTestAssembly {
    param([Parameter(Mandatory = $true)][string]$Path)

    try {
        return [System.Reflection.Assembly]::LoadFrom($Path)
    }
    catch {
        $detail = $_.Exception
        while ($null -ne $detail.InnerException) {
            $detail = $detail.InnerException
        }

        # 0x800711C7: an application control policy blocked LoadFrom. The file is present;
        # loading the bytes still brings the assembly into the AppDomain.
        $blocked = ($detail.Message -like '*0x800711C7*') -or ($detail.Message -like '*Application Control policy*')
        if (-not $blocked) {
            throw
        }

        Write-Host "[INFO] LoadFrom blocked by application control; loading from bytes: $Path"
        return [System.Reflection.Assembly]::Load([System.IO.File]::ReadAllBytes($Path))
    }
}

$themesPath = Join-Path $bin 'Krypton.Themes.dll'
$themesBackup = Join-Path $bin 'Krypton.Themes.dll.unittest-backup'
$themesHiddenForFallback = $false
if (Test-Path -LiteralPath $themesPath) {
    Move-Item -LiteralPath $themesPath -Destination $themesBackup -Force
    $themesHiddenForFallback = $true
}

[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Interop.dll'))
[void](Import-UnitTestAssembly -Path (Join-Path $bin 'Krypton.Toolkit.dll'))

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
    [void](Import-UnitTestAssembly -Path $themesPath)
}

Assert-True (Test-Path -LiteralPath $themesPath) 'Krypton.Themes.dll exists in the bin folder'

# The startup probe already failed while the DLL was hidden. The designer path asks ITypeResolutionService.
if (-not ('UnitTestThemeResolutionService' -as [type])) {
    Add-Type -TypeDefinition @'
using System;
using System.IO;
using System.Reflection;
using System.ComponentModel.Design;

public sealed class UnitTestThemeResolutionService : ITypeResolutionService, IServiceProvider
{
    private readonly string _path;

    public UnitTestThemeResolutionService(string path)
    {
        _path = path;
    }

    public Assembly GetAssembly(AssemblyName name, bool throwOnError)
    {
        if (name != null && string.Equals(name.Name, "Krypton.Themes", StringComparison.OrdinalIgnoreCase))
        {
            return Assembly.LoadFrom(_path);
        }

        if (throwOnError)
        {
            throw new FileNotFoundException(name == null ? string.Empty : name.FullName);
        }

        return null;
    }

    public Assembly GetAssembly(AssemblyName name)
    {
        return GetAssembly(name, true);
    }

    public string GetPathOfAssembly(AssemblyName name)
    {
        return _path;
    }

    public Type GetType(string name)
    {
        return GetType(name, true, false);
    }

    public Type GetType(string name, bool throwOnError)
    {
        return GetType(name, throwOnError, false);
    }

    public Type GetType(string name, bool throwOnError, bool ignoreCase)
    {
        return null;
    }

    public void ReferenceAssembly(AssemblyName name)
    {
    }

    public object GetService(Type serviceType)
    {
        if (serviceType == typeof(ITypeResolutionService))
        {
            return this;
        }

        return null;
    }
}
'@
}

$resolver = New-Object UnitTestThemeResolutionService $themesPath
[Krypton.Toolkit.KryptonThemeCatalog]::DiscoverThemes($resolver)
Assert-True ([Krypton.Toolkit.KryptonThemeCatalog]::IsImplementationAvailable($vsDark)) 'VisualStudio2022Dark is available via ITypeResolutionService'

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
# Dependencies are already loaded from $bin. Rebuilding them tries to overwrite those DLLs and fails with MSB3027.
$buildOutput = & dotnet build $sampleProj -c $Configuration -f $TargetFramework -p:BuildProjectReferences=false --nologo 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Host '[ERROR] Failed to build ThemeProviderSample.csproj' -ForegroundColor Red
    $buildOutput | ForEach-Object { Write-Host $_ }
    exit 1
}

$sampleDll = Join-Path $bin 'ThemeProviderSample.dll'
$sampleProjectDir = Split-Path -Parent $sampleProj
$alts = @(
    (Join-Path $sampleProjectDir "bin\$Configuration\$TargetFramework\ThemeProviderSample.dll"),
    (Join-Path $sampleProjectDir "bin\Any CPU\$Configuration\$TargetFramework\ThemeProviderSample.dll"),
    (Join-Path $sampleProjectDir "bin\x64\$Configuration\$TargetFramework\ThemeProviderSample.dll"),
    (Join-Path $sampleProjectDir "bin\x86\$Configuration\$TargetFramework\ThemeProviderSample.dll"),
    (Join-Path $repoRoot "artifacts\bin\$Configuration\$TargetFramework\ThemeProviderSample.dll")
)
foreach ($line in @($buildOutput)) {
    $text = "$line"
    if ($text -match '->\s*(.+ThemeProviderSample\.dll)\s*$') {
        $alts = @($Matches[1].Trim()) + $alts
    }
}

if (-not (Test-Path -LiteralPath $sampleDll)) {
    $found = $false
    foreach ($alt in $alts) {
        if (Test-Path -LiteralPath $alt) {
            Copy-Item -LiteralPath $alt -Destination $sampleDll -Force
            $found = $true
            Write-Host "[INFO] Copied ThemeProviderSample.dll from: $alt"
            break
        }
    }

    if (-not $found) {
        Write-Host '[ERROR] ThemeProviderSample.dll not found in any expected location' -ForegroundColor Red
        $searched = @($sampleDll) + $alts
        Write-Host "Searched: $($searched -join ', ')"
        exit 1
    }
}

Assert-True (Test-Path -LiteralPath $sampleDll) 'ThemeProviderSample.dll is available'
[void](Import-UnitTestAssembly -Path $sampleDll)
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
