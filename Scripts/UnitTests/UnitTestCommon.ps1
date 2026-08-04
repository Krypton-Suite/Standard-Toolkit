# Shared helpers for Scripts/UnitTest/*.ps1
# Dot-source from a unit-test script after setting $script:RepoRoot if needed.

function Get-UnitTestRepoRoot {
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

function Get-UnitTestBinDir {
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

function Register-UnitTestAssemblyResolver {
    param([string]$BinDir)

    $script:UnitTestBinDir = $BinDir
    $script:UnitTestResolving = @{}

    [System.AppDomain]::CurrentDomain.add_AssemblyResolve({
        param($sender, $e)
        $name = ($e.Name -split ',')[0]
        foreach ($a in [System.AppDomain]::CurrentDomain.GetAssemblies()) {
            if ($a.GetName().Name -eq $name) {
                return $a
            }
        }
        if ($script:UnitTestResolving.ContainsKey($name)) {
            return $null
        }
        $script:UnitTestResolving[$name] = $true
        foreach ($ext in '.dll', '.exe') {
            $candidate = Join-Path $script:UnitTestBinDir ($name + $ext)
            if (Test-Path -LiteralPath $candidate) {
                return [System.Reflection.Assembly]::LoadFile($candidate)
            }
        }
        return $null
    })
}

function Initialize-UnitTestNativeInput {
    Add-Type @"
using System;
using System.Runtime.InteropServices;
public static class UnitTestNative
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
    [void][UnitTestNative]::SetProcessDPIAware()
}

function Invoke-UnitTestDrag {
    param(
        [int]$FromX,
        [int]$FromY,
        [int]$ToX,
        [int]$ToY,
        [int]$Steps = 24,
        [int]$StepDelayMs = 50
    )

    Write-Host "drag from $FromX,$FromY to $ToX,$ToY"
    [UnitTestNative]::SetCursorPos($FromX, $FromY)
    Start-Sleep -Milliseconds 300
    [UnitTestNative]::mouse_event([UnitTestNative]::LEFTDOWN, 0, 0, 0, [IntPtr]::Zero)
    Start-Sleep -Milliseconds 250
    for ($i = 1; $i -le $Steps; $i++) {
        $x = [int]($FromX + ($ToX - $FromX) * $i / $Steps)
        $y = [int]($FromY + ($ToY - $FromY) * $i / $Steps)
        [UnitTestNative]::SetCursorPos($x, $y)
        Start-Sleep -Milliseconds $StepDelayMs
    }
    Start-Sleep -Milliseconds 400
    [UnitTestNative]::mouse_event([UnitTestNative]::LEFTUP, 0, 0, 0, [IntPtr]::Zero)
    Start-Sleep -Milliseconds 1200
}
