# Shared guard for workflows that publish Krypton.Standard.Toolkit aggregate packages (.nupkg only).
# Dot-source this script, then call Test-StandardToolkitNupkgPushAllowed before dotnet nuget push.
#
# Minimum size (default 10 MiB):
#   - GitHub Actions: set repository variable STANDARD_TOOLKIT_MIN_NUPKG_MB (integer megabytes).
#     Workflows pass it as env STANDARD_TOOLKIT_MIN_NUPKG_MB.
#   - Optional env STANDARD_TOOLKIT_MIN_NUPKG_BYTES overrides when set to a positive integer (bytes).

function Get-StandardToolkitMinNupkgBytes {
    $bytesRaw = $env:STANDARD_TOOLKIT_MIN_NUPKG_BYTES
    if (-not [string]::IsNullOrWhiteSpace($bytesRaw)) {
        $b = [long]0
        if ([long]::TryParse($bytesRaw.Trim(), [ref]$b) -and $b -gt 0) {
            return $b
        }
    }

    $mbRaw = $env:STANDARD_TOOLKIT_MIN_NUPKG_MB
    if (-not [string]::IsNullOrWhiteSpace($mbRaw)) {
        $m = [long]0
        if ([long]::TryParse($mbRaw.Trim(), [ref]$m) -and $m -gt 0) {
            return $m * [long](1024 * 1024)
        }
    }

    return [long](10 * 1024 * 1024)
}

function Test-StandardToolkitNupkgPushAllowed {
    param(
        [Parameter(Mandatory = $true)]
        [System.IO.FileInfo]$Package
    )

    $minBytes = Get-StandardToolkitMinNupkgBytes

    if (-not $Package.Name.StartsWith('Krypton.Standard.Toolkit.', [System.StringComparison]::OrdinalIgnoreCase)) {
        return $true
    }

    if ($Package.Length -ge $minBytes) {
        return $true
    }

    $mb = [math]::Round($Package.Length / (1024 * 1024), 2)
    $minMb = [math]::Round($minBytes / (1024 * 1024), 2)
    Write-Host "::error::Refusing NuGet push: Krypton.Standard.Toolkit aggregate package '$($Package.Name)' is $mb MiB (minimum $minMb MiB). Package appears incomplete."
    return $false
}
