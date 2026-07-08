# Verifies packable Krypton module .nupkg files include Krypton.Interop.dll under lib/<tfm>/.
# Dot-source this script, then call Test-KryptonInteropInPackages after msbuild Pack.
#
# Parameters:
#   -Configuration          Release, Canary, Nightly, etc. (searches artifacts/packages and Bin/Packages)
#   -PackageSearchPaths     Additional glob paths for .nupkg files
#   -SkipIfInteropProjectMissing  Exit 0 when Krypton.Interop.csproj is absent (LTS branches without Interop)

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

function Test-KryptonInteropInPackages {
    param(
        [string]$Configuration = '',
        [string[]]$PackageSearchPaths = @(),
        [switch]$SkipIfInteropProjectMissing
    )

    $ErrorActionPreference = 'Stop'

    $repoRoot = if ($null -ne $env:GITHUB_WORKSPACE -and $env:GITHUB_WORKSPACE -ne '') {
        $env:GITHUB_WORKSPACE
    }
    else {
        (Get-Location).Path
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
        if (Test-NupkgContainsKryptonInterop -Package $package) {
            Write-Host "OK: $($package.Name) contains lib/*/Krypton.Interop.dll"
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
