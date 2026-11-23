# Prints the path to VsixPublisher.exe from an installed Visual Studio instance.
# Used by .github/workflows/templates-release.yml to submit the stable VSIX
# to the Visual Studio Marketplace.

$ErrorActionPreference = 'Stop'

function Find-Publisher([string] $InstallationPath) {
    if ([string]::IsNullOrWhiteSpace($InstallationPath)) {
        return $null
    }

    $candidate = Join-Path $InstallationPath 'VSSDK\VisualStudioIntegration\Tools\Bin\VsixPublisher.exe'
    if (Test-Path -LiteralPath $candidate) {
        return $candidate
    }

    return $null
}

$vswhere = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'
if (-not (Test-Path -LiteralPath $vswhere)) {
    throw "vswhere.exe was not found. Visual Studio Installer is required to locate VsixPublisher.exe."
}

$found = Find-Publisher ((& $vswhere -latest -products * -property installationPath | Select-Object -First 1))
if (-not $found) {
    $installs = & $vswhere -all -products * -property installationPath
    foreach ($install in $installs) {
        $found = Find-Publisher $install
        if ($found) {
            break
        }
    }
}

if (-not $found) {
    throw "VsixPublisher.exe was not found. Install the Visual Studio SDK (extension development workload)."
}

Write-Output $found
