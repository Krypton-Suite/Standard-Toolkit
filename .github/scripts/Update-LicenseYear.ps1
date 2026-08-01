#requires -Version 7.0
<#
.SYNOPSIS
    Updates the end year in Krypton "Modifications by Peter Wagner" license headers.

.DESCRIPTION
    Scans source and template files for license year ranges on lines containing
    "Modifications by Peter Wagner" and sets the end year to the target year when
    it is behind. Preserves UTF-8 BOM and existing line endings (CRLF/LF).

.PARAMETER Year
    The calendar year to use as the license end year. Defaults to the current year.

.PARAMETER DryRun
    Report files that would be updated without writing changes.
#>
param(
    [int]$Year = (Get-Date).Year,
    [switch]$DryRun
)

$ErrorActionPreference = 'Stop'

$repoRoot = if ($env:GITHUB_WORKSPACE) { $env:GITHUB_WORKSPACE } else { (Get-Location).Path }
$pattern = '(Modifications by Peter Wagner[^\r\n]*?)(\d{4})(\s*-\s*)(\d{4})'
$extensions = [System.Collections.Generic.HashSet[string]]::new(
    [string[]]@('.cs', '.licenseheader', '.yml', '.yaml', '.md'),
    [StringComparer]::OrdinalIgnoreCase
)

function Get-FileEncoding([byte[]]$Bytes) {
    if ($Bytes.Length -ge 3 -and $Bytes[0] -eq 0xEF -and $Bytes[1] -eq 0xBB -and $Bytes[2] -eq 0xBF) {
        return [System.Text.UTF8Encoding]::new($true), 3
    }

    return [System.Text.UTF8Encoding]::new($false), 0
}

function Test-ExcludedPath([string]$FullName) {
    return $FullName -match '[\\/]bin[\\/]|[\\/]obj[\\/]|[\\/]Artefacts[\\/]|[\\/]\.git[\\/]'
}

$updatedFiles = 0

Get-ChildItem -Path $repoRoot -Recurse -File | ForEach-Object {
    $file = $_

    if (Test-ExcludedPath $file.FullName) {
        return
    }

    if (-not ($extensions.Contains($file.Extension) -or $file.Name -eq '.editorconfig')) {
        return
    }

    $bytes = [System.IO.File]::ReadAllBytes($file.FullName)
    if ($bytes.Length -eq 0) {
        return
    }

    $encoding, $bomSkip = Get-FileEncoding $bytes
    $content = $encoding.GetString($bytes, $bomSkip, $bytes.Length - $bomSkip)

    $newContent = [regex]::Replace($content, $pattern, {
        param($Match)

        $startYear = [int]$Match.Groups[2].Value
        $separator = $Match.Groups[3].Value
        $endYear = [int]$Match.Groups[4].Value

        if ($endYear -ge $Year) {
            return $Match.Value
        }

        return "$($Match.Groups[1].Value)$startYear$separator$Year"
    })

    if ($newContent -eq $content) {
        return
    }

    $updatedFiles++
    $relativePath = $file.FullName.Substring($repoRoot.Length).TrimStart('\', '/')

    if ($DryRun) {
        Write-Host "Would update: $relativePath"
        return
    }

    $newBytes = $encoding.GetBytes($newContent)
    if ($encoding.GetPreamble().Length -gt 0) {
        [System.IO.File]::WriteAllBytes($file.FullName, $encoding.GetPreamble() + $newBytes)
    }
    else {
        [System.IO.File]::WriteAllBytes($file.FullName, $newBytes)
    }

    Write-Host "Updated: $relativePath"
}

if ($DryRun) {
    Write-Host "Dry run: $updatedFiles file(s) would be updated to end year $Year."
}
else {
    Write-Host "Updated $updatedFiles file(s) to end year $Year."
}

Write-Output $updatedFiles
