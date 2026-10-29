<#
.SYNOPSIS
    Asserts an application still starts when Krypton.Resources.dll is missing.

.DESCRIPTION
    Moves Krypton.Resources.dll aside in the Debug output folder, loads Krypton.Toolkit from that
    folder, and checks that control text can be set, palette schema strings still resolve, and image
    accessors draw fallback glyphs instead of throwing. The DLL is moved back when the script exits.

.EXAMPLE
    powershell -NoProfile -ExecutionPolicy Bypass -STA -File .\Scripts\UnitTests\UnitTest-ResourcesFallback.ps1
#>
# UnitTest-CI: include
[CmdletBinding()]
param(
    [string]$Configuration = 'Debug',
    [string]$TargetFramework = 'net472',
    [string]$BinDir,
    [string]$ScreenshotPath
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'UnitTestCommon.ps1')

$repoRoot = Get-UnitTestRepoRoot
$bin = Get-UnitTestBinDir -RepoRoot $repoRoot -Configuration $Configuration -TargetFramework $TargetFramework -BinDir $BinDir

Add-Type -AssemblyName System.Drawing
Add-Type -AssemblyName System.Windows.Forms

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

function Test-FallbackGlyph {
    param([System.Drawing.Bitmap]$Bitmap)
    for ($y = 0; $y -lt $Bitmap.Height; $y++) {
        for ($x = 0; $x -lt $Bitmap.Width; $x++) {
            $color = $Bitmap.GetPixel($x, $y)
            $magenta = $color.R -gt 200 -and $color.B -gt 200 -and $color.G -lt 80
            if ($color.A -gt 200 -and -not $magenta) {
                return $true
            }
        }
    }

    return $false
}

$resourcesPath = Join-Path $bin 'Krypton.Resources.dll'
$heldPath = Join-Path $bin 'Krypton.Resources.dll.held'
if ((Test-Path -LiteralPath $heldPath) -and -not (Test-Path -LiteralPath $resourcesPath)) {
    Move-Item -LiteralPath $heldPath -Destination $resourcesPath
}
if (-not (Test-Path -LiteralPath $resourcesPath)) {
    throw "Expected $resourcesPath so the test can move it aside."
}

Move-Item -LiteralPath $resourcesPath -Destination $heldPath
try {
    Assert-True (-not (Test-Path -LiteralPath $resourcesPath)) 'Krypton.Resources.dll is not beside Krypton.Toolkit.dll'

    $toolkit = [System.Reflection.Assembly]::LoadFrom((Join-Path $bin 'Krypton.Toolkit.dll'))
    Assert-True ($null -ne $toolkit) 'Krypton.Toolkit loads without Krypton.Resources.dll'

    $labelType = $toolkit.GetType('Krypton.Toolkit.KryptonLabel', $true)
    try {
        $label = [Activator]::CreateInstance($labelType)
    }
    catch {
        $detail = $_.Exception
        while ($null -ne $detail) {
            Write-Host $detail.GetType().FullName
            Write-Host $detail.Message
            $detail = $detail.InnerException
        }
        throw
    }
    $label.Text = 'Still visible'
    Assert-True ($label.Text -eq 'Still visible') 'Control text stays visible without Krypton.Resources.dll'
    $label.Dispose()

    $resources = [System.AppDomain]::CurrentDomain.GetAssemblies() |
        Where-Object { $_.GetName().Name -eq 'Krypton.Resources' } |
        Select-Object -First 1
    Assert-True ($null -ne $resources) 'Placeholder Krypton.Resources assembly is loaded'

    $fallbackType = $resources.GetType('Krypton.Toolkit.ResourceFiles.KryptonResourceFallback', $false)
    Assert-True ($null -ne $fallbackType) 'Loaded resource assembly is the placeholder'
    if ($null -ne $fallbackType) {
        $isPlaceholder = $fallbackType.GetProperty('IsPlaceholder', [System.Reflection.BindingFlags]'NonPublic,Public,Static').GetValue($null)
        Assert-True ($isPlaceholder -eq $true) 'Placeholder flag is set'
    }

    $schemaType = $resources.GetType('Krypton.Toolkit.ResourceFiles.PaletteSchemas.PaletteSchemaResources', $true)
    $schemaProp = $schemaType.GetProperty('CurrentSupportedPaletteSchema', [System.Reflection.BindingFlags]'NonPublic,Static')
    $schema = [string]$schemaProp.GetValue($null)
    Assert-True (-not [string]::IsNullOrWhiteSpace($schema) -and $schema.Contains('KryptonPalette')) 'Palette schema string still resolves'

    $gridType = $resources.GetType('Krypton.Toolkit.ResourceFiles.OutlookGrid.Strings.OutlookGridStringResources', $true)
    $todayProp = $gridType.GetProperty('TODAY', [System.Reflection.BindingFlags]'NonPublic,Static')
    $today = [string]$todayProp.GetValue($null)
    Assert-True (-not [string]::IsNullOrWhiteSpace($today)) 'Outlook grid string still resolves'

    $imageType = $resources.GetType('Krypton.Toolkit.ResourceFiles.Generic.GenericImageResources', $true)
    $imageProp = $imageType.GetProperty('ButtonImageSmall', [System.Reflection.BindingFlags]'NonPublic,Static')
    if ($null -eq $imageProp) {
        $props = $imageType.GetProperties([System.Reflection.BindingFlags]'NonPublic,Static') |
            Where-Object { $_.PropertyType.FullName -eq 'System.Drawing.Bitmap' }
        $imageProp = $props | Select-Object -First 1
    }
    Assert-True ($null -ne $imageProp) 'Placeholder image property exists'
    if ($null -ne $imageProp) {
        $bitmap = [System.Drawing.Bitmap]$imageProp.GetValue($null)
        Assert-True ($null -ne $bitmap -and $bitmap.Width -gt 0 -and $bitmap.Height -gt 0) 'Missing resource DLL returns a placeholder image instead of throwing'
        Assert-True (Test-FallbackGlyph $bitmap) 'Placeholder image draws a glyph instead of a blank bitmap'
    }

    $checkType = $resources.GetType('Krypton.Toolkit.ResourceFiles.CheckBoxes.CheckBoxStripResources', $true)
    $checkProp = $checkType.GetProperty('CheckBoxStrip2010Blue', [System.Reflection.BindingFlags]'NonPublic,Static')
    $checkBitmap = [System.Drawing.Bitmap]$checkProp.GetValue($null)
    Assert-True ($null -ne $checkBitmap -and $checkBitmap.Width -eq 156 -and $checkBitmap.Height -eq 13) 'Checkbox strip keeps the original strip size'
    Assert-True (Test-FallbackGlyph $checkBitmap) 'Checkbox strip draws a fallback glyph'

    if (-not [string]::IsNullOrWhiteSpace($ScreenshotPath) -and $failed.Count -eq 0) {
        $formType = $toolkit.GetType('Krypton.Toolkit.KryptonForm', $true)
        $form = [Activator]::CreateInstance($formType)
        $form.Text = 'Missing Krypton.Resources.dll'
        $form.StartPosition = [System.Windows.Forms.FormStartPosition]::Manual
        $form.Location = New-Object System.Drawing.Point 80, 80
        $form.ClientSize = New-Object System.Drawing.Size 720, 220
        $shotLabel = [Activator]::CreateInstance($labelType)
        $shotLabel.Text = 'Control text stays visible. Missing theme images use drawn fallbacks.'
        $shotLabel.Dock = [System.Windows.Forms.DockStyle]::Fill
        $shotLabel.StateCommon.ShortText.Color1 = [System.Drawing.Color]::Black
        $labelStyle = $toolkit.GetType('Krypton.Toolkit.LabelStyle', $true)
        $shotLabel.LabelStyle = [Enum]::Parse($labelStyle, 'NormalControl')
        $form.Controls.Add($shotLabel)
        $form.Show()
        $form.TopMost = $true
        $form.Activate()
        $form.BringToFront()
        for ($i = 0; $i -lt 30; $i++) {
            [System.Windows.Forms.Application]::DoEvents()
            Start-Sleep -Milliseconds 50
        }
        $form.Refresh()
        [System.Windows.Forms.Application]::DoEvents()
        # PrintWindow returns an empty bitmap for this layered form. A foreground copy includes the caption text.
        $bounds = $form.Bounds
        $drawn = New-Object System.Drawing.Bitmap $bounds.Width, $bounds.Height
        $graphics = [System.Drawing.Graphics]::FromImage($drawn)
        try {
            $graphics.CopyFromScreen($bounds.Location, [System.Drawing.Point]::Empty, $bounds.Size)
            $outDir = Split-Path -Parent $ScreenshotPath
            if ($outDir -and -not (Test-Path -LiteralPath $outDir)) {
                New-Item -ItemType Directory -Path $outDir | Out-Null
            }

            $drawn.Save($ScreenshotPath, [System.Drawing.Imaging.ImageFormat]::Png)
        }
        finally {
            $graphics.Dispose()
            $drawn.Dispose()
        }
        $form.Dispose()
    }
}
finally {
    if ((Test-Path -LiteralPath $heldPath) -and -not (Test-Path -LiteralPath $resourcesPath)) {
        Move-Item -LiteralPath $heldPath -Destination $resourcesPath
    }
}

if ($failed.Count -gt 0) {
    Write-Error ("Missing Krypton.Resources checks failed:`n" + ($failed -join "`n"))
    exit 1
}

exit 0
