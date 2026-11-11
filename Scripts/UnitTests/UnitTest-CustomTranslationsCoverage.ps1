<#
.SYNOPSIS
    Asserts #4370 custom-string catalog coverage and Merge Missing.

.DESCRIPTION
    Registers a small typed string set, exports CustomTranslations.xml, drops one
    property, adds a retired property, and checks Analyze / Merge Missing / strict import.

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

[void][System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Interop.dll'))
$toolkit = Join-Path $bin 'Krypton.Toolkit.dll'
$utilities = Join-Path $bin 'Krypton.Toolkit.Utilities.dll'
[void][System.Reflection.Assembly]::LoadFrom($toolkit)
[void][System.Reflection.Assembly]::LoadFrom($utilities)

Add-Type -ReferencedAssemblies @($toolkit, $utilities) -TypeDefinition @"
public sealed class CoverageProbeStrings : Krypton.Toolkit.GlobalId, Krypton.Toolkit.Utilities.IKryptonCustomStringSet
{
    public CoverageProbeStrings() { Reset(); }

    [System.ComponentModel.Localizable(true)]
    [System.ComponentModel.DefaultValue("Hello")]
    public string Greeting { get; set; }

    [System.ComponentModel.Localizable(true)]
    [System.ComponentModel.DefaultValue("Goodbye")]
    public string Farewell { get; set; }

    public bool IsDefault
    {
        get { return Greeting == "Hello" && Farewell == "Goodbye"; }
    }

    public void Reset()
    {
        Greeting = "Hello";
        Farewell = "Goodbye";
    }
}
"@

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

$probe = New-Object CoverageProbeStrings
[Krypton.Toolkit.Utilities.KryptonCustomStrings]::ResetValues()
[Krypton.Toolkit.Utilities.KryptonCustomStrings]::RegisterStringSet('Probe', $probe)
[Krypton.Toolkit.Utilities.KryptonCustomStrings]::Set('Note', 'Keep me')

$temp = Join-Path ([System.IO.Path]::GetTempPath()) ("krypton-custom-coverage-" + [guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $temp | Out-Null
try {
    $xmlPath = Join-Path $temp 'CustomTranslations.xml'
    [Krypton.Toolkit.Utilities.KryptonCustomStrings]::ExportToXmlFile($xmlPath, $true)

    $doc = New-Object System.Xml.XmlDocument
    $doc.Load($xmlPath)
    Assert-True ($null -ne $doc.DocumentElement.GetAttribute('ToolkitVersion') -and $doc.DocumentElement.GetAttribute('ToolkitVersion').Length -gt 0) 'Export stamps ToolkitVersion'

    $greeting = $doc.SelectSingleNode("/KryptonCustomTranslations/StringSets/StringSet[@Name='Probe']/Greeting")
    $farewell = $doc.SelectSingleNode("/KryptonCustomTranslations/StringSets/StringSet[@Name='Probe']/Farewell")
    Assert-True ($null -ne $greeting) 'Export contains StringSets.Probe.Greeting'
    Assert-True ($null -ne $farewell) 'Export contains StringSets.Probe.Farewell'
    if ($null -eq $greeting -or $null -eq $farewell) {
        throw 'Cannot continue without Probe greeting/farewell nodes.'
    }

    [void]$greeting.ParentNode.RemoveChild($greeting)
    $farewell.SetAttribute('Value', 'Adieu')
    $retired = $doc.CreateElement('Retired')
    $retired.SetAttribute('Value', 'gone')
    [void]$farewell.ParentNode.AppendChild($retired)
    $doc.Save($xmlPath)

    $coverage = [Krypton.Toolkit.Utilities.KryptonCustomStrings]::AnalyzeTranslationsFromFile($xmlPath)
    Assert-True ($coverage.MissingInFile.Contains('StringSets.Probe.Greeting')) 'MissingInFile contains StringSets.Probe.Greeting'
    Assert-True ($coverage.ExtraInFile.Contains('StringSets.Probe.Retired')) 'ExtraInFile contains StringSets.Probe.Retired'
    Assert-True ($coverage.Applied.Contains('Values.Note')) 'Applied contains Values.Note'

    $merged = [Krypton.Toolkit.Utilities.KryptonCustomStrings]::MergeMissingTranslationsToFile($xmlPath, $true)
    Assert-True (-not $merged.MissingInFile.Contains('StringSets.Probe.Greeting')) 'Merge Missing restores Greeting'

    $after = New-Object System.Xml.XmlDocument
    $after.Load($xmlPath)
    $greetingAfter = $after.SelectSingleNode("/KryptonCustomTranslations/StringSets/StringSet[@Name='Probe']/Greeting")
    $farewellAfter = $after.SelectSingleNode("/KryptonCustomTranslations/StringSets/StringSet[@Name='Probe']/Farewell")
    Assert-True ($null -ne $greetingAfter -and $greetingAfter.GetAttribute('Value') -eq 'Hello') 'Merged Greeting is the English placeholder'
    Assert-True ($null -ne $farewellAfter -and $farewellAfter.GetAttribute('Value') -eq 'Adieu') 'Merged Farewell keeps the translation'

    $strictThrew = $false
    $stale = Join-Path $temp 'stale.xml'
    $doc.Save($stale)
    try {
        [Krypton.Toolkit.Utilities.KryptonCustomStrings]::ImportFromXmlFile($stale, $true, $true)
    }
    catch {
        $strictThrew = $true
    }
    Assert-True $strictThrew 'Strict custom import throws on catalog drift'
}
finally {
    [void][Krypton.Toolkit.Utilities.KryptonCustomStrings]::UnregisterStringSet('Probe')
    [Krypton.Toolkit.Utilities.KryptonCustomStrings]::ResetValues()
    if (Test-Path $temp) {
        Remove-Item $temp -Recurse -Force
    }
}

if ($failed.Count -gt 0) {
    Write-Host "$($failed.Count) assertion(s) failed." -ForegroundColor Red
    exit 1
}

Write-Host 'Custom translations coverage checks passed.'
exit 0
