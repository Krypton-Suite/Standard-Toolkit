<#
.SYNOPSIS
    Asserts #4370 toolkit translation catalog coverage and Merge Missing.

.DESCRIPTION
    Exports a full ToolkitTranslations.xml, drops one live key, adds a retired key,
    and checks Analyze / Merge Missing / tolerant import / strict import / CSV report.

    Requires an STA apartment (use powershell -STA).
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

[Krypton.Toolkit.KryptonManager]::AutoDiscoverTranslations = $false

$temp = Join-Path ([System.IO.Path]::GetTempPath()) ("krypton-coverage-" + [guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $temp | Out-Null
try {
    $xmlPath = Join-Path $temp 'ToolkitTranslations.xml'
    [Krypton.Toolkit.KryptonManager]::Strings.ExportToXmlFile($xmlPath, $true)

    $doc = New-Object System.Xml.XmlDocument
    $doc.Load($xmlPath)
    $ok = $doc.SelectSingleNode('/KryptonTranslations/CommonStrings/General/OK')
    $cancel = $doc.SelectSingleNode('/KryptonTranslations/CommonStrings/General/Cancel')
    Assert-True ($null -ne $ok) 'Export contains CommonStrings.General.OK'
    Assert-True ($null -ne $cancel) 'Export contains CommonStrings.General.Cancel'
    if ($null -eq $ok -or $null -eq $cancel) {
        throw 'Cannot continue without canonical OK/Cancel nodes.'
    }

    [void]$ok.ParentNode.RemoveChild($ok)
    $cancel.SetAttribute('Value', 'Abbrechen')
    $retired = $doc.CreateElement('RetiredCatalogKey')
    $retired.SetAttribute('Value', 'gone')
    [void]$doc.DocumentElement.AppendChild($retired)
    $doc.Save($xmlPath)

    $staleCopy = Join-Path $temp 'ToolkitTranslations-stale.xml'
    Copy-Item $xmlPath $staleCopy

    $coverage = [Krypton.Toolkit.KryptonManager]::AnalyzeTranslationsFromFile($xmlPath)
    Assert-True ($coverage.MissingInFile.Contains('CommonStrings.General.OK')) 'MissingInFile contains CommonStrings.General.OK'
    Assert-True ($coverage.ExtraInFile.Contains('RetiredCatalogKey')) 'ExtraInFile contains RetiredCatalogKey'

    $report = Join-Path $temp 'coverage.csv'
    $coverage.ExportReport($report)
    $csv = [System.IO.File]::ReadAllText($report)
    Assert-True ($csv.Contains('Missing,CommonStrings,CommonStrings.General.OK')) 'CSV report groups the missing key by section'

    $merged = [Krypton.Toolkit.KryptonManager]::MergeMissingTranslationsToFile($xmlPath, $true)
    Assert-True (-not $merged.HasMissing) 'Merge Missing clears missing keys'

    $after = New-Object System.Xml.XmlDocument
    $after.Load($xmlPath)
    $cancelAfter = $after.SelectSingleNode('/KryptonTranslations/CommonStrings/General/Cancel')
    $okAfter = $after.SelectSingleNode('/KryptonTranslations/CommonStrings/General/OK')
    Assert-True ($null -ne $okAfter) 'Merged file restores OK'
    Assert-True ($null -ne $cancelAfter -and $cancelAfter.GetAttribute('Value') -eq 'Abbrechen') 'Merged file keeps the translated Cancel value'

    $tolerant = $true
    try {
        [Krypton.Toolkit.KryptonManager]::Strings.ImportFromXmlFile($staleCopy, $true, $false, $true, $false)
    }
    catch {
        $tolerant = $false
    }
    Assert-True $tolerant 'Tolerant import of a stale file does not throw'

    $strictThrew = $false
    try {
        [Krypton.Toolkit.KryptonManager]::Strings.ImportFromXmlFile($staleCopy, $true, $false, $true, $true)
    }
    catch {
        $strictThrew = $true
    }
    Assert-True $strictThrew 'Strict import throws when the catalog has drifted'

    $jsonPath = Join-Path $temp 'ToolkitTranslations.json'
    [Krypton.Toolkit.KryptonManager]::Strings.ExportToJsonFile($jsonPath, $true)
    $jsonCoverage = [Krypton.Toolkit.KryptonManager]::AnalyzeTranslationsFromFile($jsonPath)
    Assert-True (-not $jsonCoverage.HasMissing) 'Full JSON export has no missing keys'

    $autoPath = Join-Path $temp 'ToolkitTranslations-auto.xml'
    Copy-Item $xmlPath $autoPath
    $autoDoc = New-Object System.Xml.XmlDocument
    $autoDoc.Load($autoPath)
    $autoDoc.DocumentElement.SetAttribute('Culture', 'de-DE')
    $autoOk = $autoDoc.SelectSingleNode('/KryptonTranslations/CommonStrings/General/OK')
    Assert-True ($null -ne $autoOk) 'Auto-translate sample still has OK before it is removed'
    if ($null -ne $autoOk) {
        $englishOk = $autoOk.GetAttribute('Value')
        [void]$autoOk.ParentNode.RemoveChild($autoOk)
        $autoDoc.Save($autoPath)

        Add-Type -ReferencedAssemblies (Join-Path $bin 'Krypton.Toolkit.dll') -TypeDefinition @"
public sealed class PrefixTranslator : Krypton.Toolkit.IKryptonStringTranslator
{
    public string Translate(string text, System.Globalization.CultureInfo source, System.Globalization.CultureInfo target, string keyPath)
    {
        return "DE:" + text;
    }
}
"@
        [Krypton.Toolkit.KryptonStringTranslation]::AutoTranslateMissingStrings = $true
        [Krypton.Toolkit.KryptonStringTranslation]::Translator = New-Object PrefixTranslator
        [void][Krypton.Toolkit.KryptonManager]::MergeMissingTranslationsToFile($autoPath, $true)
        $autoAfter = New-Object System.Xml.XmlDocument
        $autoAfter.Load($autoPath)
        $translatedOk = $autoAfter.SelectSingleNode('/KryptonTranslations/CommonStrings/General/OK')
        Assert-True ($null -ne $translatedOk -and $translatedOk.GetAttribute('Value') -eq ("DE:" + $englishOk)) 'Auto-translate fills only the missing OK key'
        $keptCancel = $autoAfter.SelectSingleNode('/KryptonTranslations/CommonStrings/General/Cancel')
        Assert-True ($null -ne $keptCancel -and $keptCancel.GetAttribute('Value') -eq 'Abbrechen') 'Auto-translate leaves an existing translation unchanged'
    }
}
finally {
    [Krypton.Toolkit.KryptonStringTranslation]::AutoTranslateMissingStrings = $false
    [Krypton.Toolkit.KryptonStringTranslation]::Translator = $null
    if (Test-Path $temp) {
        Remove-Item $temp -Recurse -Force
    }
}

if ($failed.Count -gt 0) {
    Write-Host "$($failed.Count) assertion(s) failed." -ForegroundColor Red
    exit 1
}

Write-Host 'Toolkit translations coverage checks passed.'
exit 0
