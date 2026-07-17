# Verifies packable Krypton module .nupkg files include Krypton.Interop.dll under lib/<tfm>/.
# Dot-source this script, then call Test-KryptonInteropInPackages after msbuild Pack.
#
# Parameters:
#   -Configuration          Release, Canary, Nightly, etc. (searches artifacts/packages and Bin/Packages)
#   -PackageSearchPaths     Additional glob paths for .nupkg files
#   -SkipIfInteropProjectMissing  Exit 0 when Krypton.Interop.csproj is absent (LTS branches without Interop)
#   -MinimumMajorVersion    Exit 0 when the evaluated toolkit major version is below this value (V110+ Interop)

function Get-KryptonToolkitMajorVersion {
    param(
        [string]$Configuration = 'Release',
        [string]$RepoRoot = ''
    )

    if ([string]::IsNullOrWhiteSpace($RepoRoot)) {
        $RepoRoot = if ($null -ne $env:GITHUB_WORKSPACE -and $env:GITHUB_WORKSPACE -ne '') {
            $env:GITHUB_WORKSPACE
        }
        else {
            (Get-Location).Path
        }
    }

    if ([string]::IsNullOrWhiteSpace($Configuration)) {
        $Configuration = 'Release'
    }

    $proj = Join-Path $RepoRoot 'Source/Krypton Components/Krypton.Toolkit/Krypton.Toolkit 2022.csproj'
    if (-not (Test-Path -LiteralPath $proj)) {
        Write-Error "Krypton.Toolkit project not found at '$proj'."
    }

    $versionProperties = @('LibraryVersion', 'AssemblyVersion', 'Version')
    foreach ($property in $versionProperties) {
        $evaluated = (& dotnet msbuild $proj -getProperty:$property -p:Configuration=$Configuration -nologo -v:q).Trim()
        if ($evaluated -match '^(\d+)\.') {
            return [int]$Matches[1]
        }
    }

    Write-Error "Could not resolve Krypton toolkit major version from MSBuild (tried: $($versionProperties -join ', '))."
}

function Test-PackageRequiresKryptonInterop {
    param(
        [Parameter(Mandatory = $true)]
        [string]$PackageFileName
    )

    if ($PackageFileName -match '\.snupkg$') {
        return $false
    }

    $packageId = [System.IO.Path]::GetFileNameWithoutExtension($PackageFileName)
    $roots = @(
        'Krypton.Toolkit',
        'Krypton.Ribbon',
        'Krypton.Navigator',
        'Krypton.Docking',
        'Krypton.Workspace',
        'Krypton.Standard.Toolkit'
    )

    foreach ($root in $roots) {
        if ($packageId.Equals($root, [System.StringComparison]::OrdinalIgnoreCase)) {
            return $true
        }

        if ($packageId.StartsWith("$root.", [System.StringComparison]::OrdinalIgnoreCase)) {
            return $true
        }
    }

    return $false
}

function Test-NupkgContainsKryptonInterop {
    param(
        [Parameter(Mandatory = $true)]
        [System.IO.FileInfo]$Package
    )

    Add-Type -AssemblyName System.IO.Compression.FileSystem

    $zip = [System.IO.Compression.ZipFile]::OpenRead($Package.FullName)
    try {
        $interopEntries = @(
            $zip.Entries |
                Where-Object { $_.FullName -match '(?i)^lib/[^/]+/Krypton\.Interop\.dll$' }
        )

        return $interopEntries.Count -gt 0
    }
    finally {
        $zip.Dispose()
    }
}

function Test-NupkgDeclaresKryptonInteropDependency {
    # Krypton.Interop is never published to nuget.org; a nuspec <dependency id="Krypton.Interop"> makes
    # consumer restore fail with NU1101 (#3908), so packages must bundle the DLL instead of depending on it.
    param(
        [Parameter(Mandatory = $true)]
        [System.IO.FileInfo]$Package
    )

    Add-Type -AssemblyName System.IO.Compression.FileSystem

    $zip = [System.IO.Compression.ZipFile]::OpenRead($Package.FullName)
    try {
        $nuspecEntry = $zip.Entries | Where-Object { $_.FullName -match '(?i)^[^/]+\.nuspec$' } | Select-Object -First 1
        if ($null -eq $nuspecEntry) {
            return $false
        }

        $reader = New-Object System.IO.StreamReader($nuspecEntry.Open())
        try {
            [xml]$nuspec = $reader.ReadToEnd()
        }
        finally {
            $reader.Dispose()
        }

        $dependencies = @($nuspec.GetElementsByTagName('dependency') | Where-Object { $_.id -eq 'Krypton.Interop' })
        return $dependencies.Count -gt 0
    }
    finally {
        $zip.Dispose()
    }
}

function Test-KryptonInteropInPackages {
    param(
        [string]$Configuration = '',
        [string[]]$PackageSearchPaths = @(),
        [switch]$SkipIfInteropProjectMissing,
        [int]$MinimumMajorVersion = 0
    )

    $ErrorActionPreference = 'Stop'

    $repoRoot = if ($null -ne $env:GITHUB_WORKSPACE -and $env:GITHUB_WORKSPACE -ne '') {
        $env:GITHUB_WORKSPACE
    }
    else {
        (Get-Location).Path
    }

    $msbuildConfiguration = if ([string]::IsNullOrWhiteSpace($Configuration)) { 'Release' } else { $Configuration }

    if ($MinimumMajorVersion -gt 0) {
        $major = Get-KryptonToolkitMajorVersion -Configuration $msbuildConfiguration -RepoRoot $repoRoot
        if ($major -lt $MinimumMajorVersion) {
            Write-Host "::notice:: Major version $major is below $MinimumMajorVersion; skipping Krypton.Interop NuGet package verification."
            return
        }
    }

    $interopProject = Join-Path $repoRoot 'Source/Krypton Components/Krypton.Interop/Krypton.Interop.csproj'
    if ($SkipIfInteropProjectMissing -and -not (Test-Path -LiteralPath $interopProject)) {
        Write-Host '::notice:: Krypton.Interop project not present; skipping NuGet package verification.'
        return
    }

    if (-not (Test-Path -LiteralPath $interopProject)) {
        Write-Error "Krypton.Interop project not found at '$interopProject'."
    }

    $searchPaths = [System.Collections.Generic.List[string]]::new()
    if (-not [string]::IsNullOrWhiteSpace($Configuration)) {
        $searchPaths.Add("artifacts/packages/$Configuration/*.nupkg")
        $searchPaths.Add("Bin/Packages/$Configuration/*.nupkg")
        $searchPaths.Add("Artefacts/Packages/$Configuration/*.nupkg")
    }

    foreach ($path in $PackageSearchPaths) {
        if (-not [string]::IsNullOrWhiteSpace($path)) {
            $searchPaths.Add($path)
        }
    }

    if ($searchPaths.Count -eq 0) {
        $searchPaths.Add('artifacts/packages/*/*.nupkg')
        $searchPaths.Add('Bin/Packages/*/*.nupkg')
        $searchPaths.Add('Artefacts/Packages/*/*.nupkg')
    }

    $packages = @()
    foreach ($pattern in $searchPaths) {
        $packages += Get-ChildItem -Path $pattern -ErrorAction SilentlyContinue
    }

    $packages = @($packages | Where-Object { $_ -ne $null } | Sort-Object FullName -Unique)

    if ($packages.Count -eq 0) {
        Write-Error "No .nupkg files found. Searched: $($searchPaths -join '; ')"
    }

    $requiredPackages = @($packages | Where-Object { Test-PackageRequiresKryptonInterop -PackageFileName $_.Name })
    if ($requiredPackages.Count -eq 0) {
        Write-Error "Found $($packages.Count) .nupkg file(s), but none match packable Krypton module package IDs."
    }

    $failures = [System.Collections.Generic.List[string]]::new()
    foreach ($package in $requiredPackages) {
        if (Test-NupkgDeclaresKryptonInteropDependency -Package $package) {
            $failures.Add($package.Name)
            Write-Host "::error file=$($package.FullName)::$($package.Name) declares a nuspec dependency on Krypton.Interop, which is not published to nuget.org (NU1101, #3908)"
            continue
        }

        if (Test-NupkgContainsKryptonInterop -Package $package) {
            Write-Host "OK: $($package.Name) contains lib/*/Krypton.Interop.dll and no Krypton.Interop dependency"
            continue
        }

        $failures.Add($package.Name)
        Write-Host "::error file=$($package.FullName)::Missing lib/*/Krypton.Interop.dll in $($package.Name)"
    }

    if ($failures.Count -gt 0) {
        throw "Krypton.Interop.dll verification failed for $($failures.Count) package(s): $($failures -join ', ')"
    }

    Write-Host "Verified Krypton.Interop.dll in $($requiredPackages.Count) Krypton module package(s)."
}
