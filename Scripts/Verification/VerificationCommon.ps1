# Shared helpers for Scripts/Verification/*.ps1
# Dot-source from a verification script after setting $script:RepoRoot if needed.

function Get-VerificationRepoRoot {
    param([string]$StartPath = $PSScriptRoot)
    $dir = Resolve-Path -LiteralPath $StartPath
    while ($dir) {
        if (Test-Path -LiteralPath (Join-Path $dir 'AGENTS.md')) {
            return [string]$dir
        }
        $parent = Split-Path -Parent $dir
        if (-not $parent -or $parent -eq $dir) {
            break
        }
        $dir = $parent
    }
    throw "Could not locate repository root from '$StartPath'."
}

function Get-VerificationBinDir {
    param(
        [string]$RepoRoot,
        [string]$Configuration = 'Debug',
        [string]$TargetFramework = 'net472',
        [string]$BinDir
    )

    if ($BinDir) {
        return (Resolve-Path -LiteralPath $BinDir).Path
    }

    $path = Join-Path $RepoRoot "Bin\$Configuration\$TargetFramework"
    if (-not (Test-Path -LiteralPath $path)) {
        throw "Bin directory not found: $path. Build TestForm first."
    }
    return (Resolve-Path -LiteralPath $path).Path
}

function Register-VerificationAssemblyResolver {
    param([string]$BinDir)

    $script:VerificationBinDir = $BinDir
    $script:VerificationResolving = @{}

    [System.AppDomain]::CurrentDomain.add_AssemblyResolve({
        param($sender, $e)
        $name = ($e.Name -split ',')[0]
        foreach ($a in [System.AppDomain]::CurrentDomain.GetAssemblies()) {
            if ($a.GetName().Name -eq $name) {
                return $a
            }
        }
        if ($script:VerificationResolving.ContainsKey($name)) {
            return $null
        }
        $script:VerificationResolving[$name] = $true
        foreach ($ext in '.dll', '.exe') {
            $candidate = Join-Path $script:VerificationBinDir ($name + $ext)
            if (Test-Path -LiteralPath $candidate) {
                return [System.Reflection.Assembly]::LoadFile($candidate)
            }
        }
        return $null
    })
}

function Initialize-VerificationNativeInput {
    Add-Type @"
using System;
using System.Runtime.InteropServices;
public static class VerificationNative
{
    [DllImport("user32.dll")] public static extern bool SetProcessDPIAware();
    [DllImport("user32.dll")] public static extern bool SetCursorPos(int x, int y);
    [DllImport("user32.dll")] public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, IntPtr dwExtraInfo);
    [DllImport("user32.dll")] public static extern bool SetForegroundWindow(IntPtr h);
    public const uint LEFTDOWN = 0x0002;
    public const uint LEFTUP = 0x0004;
    public const uint RIGHTDOWN = 0x0008;
    public const uint RIGHTUP = 0x0010;
}
"@
    [void][VerificationNative]::SetProcessDPIAware()
}

function Invoke-VerificationDrag {
    param(
        [int]$FromX,
        [int]$FromY,
        [int]$ToX,
        [int]$ToY,
        [int]$Steps = 24,
        [int]$StepDelayMs = 50
    )

    Write-Host "drag from $FromX,$FromY to $ToX,$ToY"
    [VerificationNative]::SetCursorPos($FromX, $FromY)
    Start-Sleep -Milliseconds 300
    [VerificationNative]::mouse_event([VerificationNative]::LEFTDOWN, 0, 0, 0, [IntPtr]::Zero)
    Start-Sleep -Milliseconds 250
    for ($i = 1; $i -le $Steps; $i++) {
        $x = [int]($FromX + ($ToX - $FromX) * $i / $Steps)
        $y = [int]($FromY + ($ToY - $FromY) * $i / $Steps)
        [VerificationNative]::SetCursorPos($x, $y)
        Start-Sleep -Milliseconds $StepDelayMs
    }
    Start-Sleep -Milliseconds 400
    [VerificationNative]::mouse_event([VerificationNative]::LEFTUP, 0, 0, 0, [IntPtr]::Zero)
    Start-Sleep -Milliseconds 1200
}
