# PowerShell script to fix PaletteMode property errors in simple designers
# This script removes PaletteMode properties from controls that don't have them

param(
    [string]$ControlsPath = "Source/Krypton Components/Krypton.Toolkit/Designers/Extensibility/Controls",
    [switch]$WhatIf = $false
)

Write-Host "Fixing PaletteMode property errors in simple designers..." -ForegroundColor Green
Write-Host "Controls Path: $ControlsPath" -ForegroundColor Yellow
Write-Host "WhatIf: $WhatIf" -ForegroundColor Yellow
Write-Host ""

# List of controls that don't have PaletteMode property (from build errors)
$controlsWithoutPaletteMode = @(
    "KryptonBreadCrumbItem",
    "KryptonCheckSet", 
    "KryptonCommand",
    "KryptonCustomPaletteBase",
    "KryptonDataGridViewComboBoxColumn",
    "KryptonDataGridViewDateTimePickerColumn",
    "KryptonDataGridViewDomainUpDownColumn",
    "KryptonDataGridViewNumericUpDownColumn",
    "KryptonDataGridViewTextBoxColumn",
    "KryptonDataGridViewMaskedTextBoxColumn",
    "KryptonManager",
    "KryptonScrollBar",
    "KryptonProgressBar",
    "KryptonWebBrowser"
)

$fixedCount = 0
$skippedCount = 0
$errorCount = 0

foreach ($control in $controlsWithoutPaletteMode) {
    try {
        $fileName = "${control}SimpleDesigner.cs"
        $filePath = Join-Path $ControlsPath $fileName
        
        # Check if file exists
        if (-not (Test-Path $filePath)) {
            Write-Host "  $fileName not found - skipping" -ForegroundColor Yellow
            $skippedCount++
            continue
        }
        
        # Read file content
        $content = Get-Content $filePath -Raw
        
        # Remove PaletteMode property and related code
        $newContent = $content -replace `
            '(?s)\s*/// <summary>\s*/// Gets and sets the palette mode\.\s*/// </summary>\s*\[Category\("Visuals"\)\]\s*\[Description\("Palette applied to drawing"\)\]\s*public PaletteMode PaletteMode\s*\{\s*get => _[^;]+;\s*set\s*\{\s*if \(_[^)]+\)\s*\{\s*_[^;]+;\s*NotifyPropertyChanged\("PaletteMode", value\);\s*\}\s*\}\s*\}', ''
        
        # Remove PaletteMode from GetSortedActionItems
        $newContent = $newContent -replace `
            '(?s)\s*// Visuals\s*actions\.Add\(new DesignerActionHeaderItem\("Visuals"\)\);\s*actions\.Add\(new DesignerActionPropertyItem\(nameof\(PaletteMode\), "Palette", "Visuals", "Palette applied to drawing"\)\);', ''
        
        # If no changes were made, try a different approach
        if ($newContent -eq $content) {
            # Remove just the PaletteMode property block
            $newContent = $content -replace `
                '(?s)\s*/// <summary>\s*/// Gets and sets the palette mode\.\s*/// </summary>\s*\[Category\("Visuals"\)\]\s*\[Description\("Palette applied to drawing"\)\]\s*public PaletteMode PaletteMode\s*\{[^}]+\}', ''
            
            # Remove PaletteMode action item
            $newContent = $newContent -replace `
                '\s*actions\.Add\(new DesignerActionPropertyItem\(nameof\(PaletteMode\), "Palette", "Visuals", "Palette applied to drawing"\)\);', ''
        }
        
        # If still no changes, create a minimal version
        if ($newContent -eq $content) {
            $fieldName = $control.Substring(0,1).ToLower() + $control.Substring(1)
            
            $minimalContent = @"
#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Simplified designer for $control optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class ${control}SimpleDesigner : ControlDesigner
{{
    #region Public Overrides
    /// <summary>
    /// Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {{
        get
        {{
            var actionLists = new DesignerActionListCollection();
            
            if (Component != null)
            {{
                actionLists.Add(new ${control}SimpleActionList(Component));
            }}

            return actionLists;
        }}
    }}
    #endregion
}}

/// <summary>
/// Simplified action list for $control optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class ${control}SimpleActionList : DesignerActionList
{{
    #region Instance Fields
    private readonly $control _${fieldName};
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ${control}SimpleActionList class.
    /// </summary>
    /// <param name="component">The component to create actions for.</param>
    public ${control}SimpleActionList(IComponent component)
        : base(component)
    {{
        _${fieldName} = ($control)component;
    }}
    #endregion

    #region Public Override
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {{
        var actions = new DesignerActionItemCollection();

        if (_${fieldName} != null)
        {{
            // No specific properties for this control
            actions.Add(new DesignerActionHeaderItem("General"));
        }}

        return actions;
    }}
    #endregion
}}
"@
            $newContent = $minimalContent
        }
        
        if ($WhatIf) {
            Write-Host "  Would fix: $fileName" -ForegroundColor Green
        } else {
            Set-Content -Path $filePath -Value $newContent -NoNewline
            Write-Host "  Fixed: $fileName" -ForegroundColor Green
        }
        
        $fixedCount++
    }
    catch {
        Write-Host "  Error fixing $control`: $($_.Exception.Message)" -ForegroundColor Red
        $errorCount++
    }
}

Write-Host ""
Write-Host "Summary:" -ForegroundColor Green
Write-Host "  Fixed: $fixedCount" -ForegroundColor Green
Write-Host "  Skipped: $skippedCount" -ForegroundColor Yellow
Write-Host "  Errors: $errorCount" -ForegroundColor Red

if ($WhatIf) {
    Write-Host ""
    Write-Host "This was a dry run. Use without -WhatIf to apply changes." -ForegroundColor Yellow
}

Write-Host ""
Write-Host "Next steps:" -ForegroundColor Green
Write-Host "1. Test compilation" -ForegroundColor White
Write-Host "2. Add specific properties to each designer as needed" -ForegroundColor White
Write-Host "3. Test designer functionality" -ForegroundColor White
