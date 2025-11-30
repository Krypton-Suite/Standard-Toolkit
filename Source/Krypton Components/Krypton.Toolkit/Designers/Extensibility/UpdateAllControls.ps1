# PowerShell script to update all Krypton controls to use hybrid designer approach
# This script adds conditional compilation directives to switch between simple and extensibility designers

param(
    [string]$Path = "Source/Krypton Components/Krypton.Toolkit/Controls Toolkit",
    [switch]$WhatIf = $false
)

Write-Host "Updating Krypton controls to use hybrid designer approach..." -ForegroundColor Green
Write-Host "Path: $Path" -ForegroundColor Yellow
Write-Host "WhatIf: $WhatIf" -ForegroundColor Yellow
Write-Host ""

# Get all control files
$controlFiles = Get-ChildItem -Path $Path -Filter "Krypton*.cs" -Recurse

$updatedCount = 0
$skippedCount = 0
$errorCount = 0

foreach ($file in $controlFiles) {
    try {
        Write-Host "Processing: $($file.Name)" -ForegroundColor Cyan
        
        $content = Get-Content $file.FullName -Raw
        
        # Check if file has Designer attribute
        if ($content -match '\[Designer\(typeof\(([^)]+)\)\)\]') {
            $currentDesigner = $matches[1]
            $controlName = [System.IO.Path]::GetFileNameWithoutExtension($file.Name)
            
            # Skip if already has hybrid approach
            if ($content -match '#if NET8_0_OR_GREATER') {
                Write-Host "  Already has hybrid approach - skipping" -ForegroundColor Yellow
                $skippedCount++
                continue
            }
            
            # Create simple designer name
            $simpleDesigner = "${controlName}SimpleDesigner"
            
            # Replace with hybrid approach
            $newContent = $content -replace '\[Designer\(typeof\([^)]+\)\)\]', @"
#if NET8_0_OR_GREATER
[Designer(typeof($simpleDesigner))]
#else
[Designer(typeof($currentDesigner))]
#endif
"@
            
            if ($WhatIf) {
                Write-Host "  Would update to use hybrid approach:" -ForegroundColor Green
                Write-Host "    .NET 8+: $simpleDesigner" -ForegroundColor Green
                Write-Host "    .NET Framework: $currentDesigner" -ForegroundColor Green
            } else {
                Set-Content -Path $file.FullName -Value $newContent -NoNewline
                Write-Host "  Updated to use hybrid approach" -ForegroundColor Green
            }
            
            $updatedCount++
        } else {
            Write-Host "  No Designer attribute found - skipping" -ForegroundColor Yellow
            $skippedCount++
        }
    }
    catch {
        Write-Host "  Error processing file: $($_.Exception.Message)" -ForegroundColor Red
        $errorCount++
    }
}

Write-Host ""
Write-Host "Summary:" -ForegroundColor Green
Write-Host "  Updated: $updatedCount" -ForegroundColor Green
Write-Host "  Skipped: $skippedCount" -ForegroundColor Yellow
Write-Host "  Errors: $errorCount" -ForegroundColor Red

if ($WhatIf) {
    Write-Host ""
    Write-Host "This was a dry run. Use without -WhatIf to apply changes." -ForegroundColor Yellow
}

Write-Host ""
Write-Host "Next steps:" -ForegroundColor Green
Write-Host "1. Create simple designers for all controls" -ForegroundColor White
Write-Host "2. Test compilation" -ForegroundColor White
Write-Host "3. Test designer functionality" -ForegroundColor White
