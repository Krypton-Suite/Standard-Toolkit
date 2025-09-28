# PowerShell script to create simple designers for all Krypton controls
# This script generates basic simple designer files based on a template

param(
    [string]$OutputPath = "Source/Krypton Components/Krypton.Toolkit/Designers/Extensibility/Controls",
    [switch]$WhatIf = $false
)

Write-Host "Creating simple designers for Krypton controls..." -ForegroundColor Green
Write-Host "Output Path: $OutputPath" -ForegroundColor Yellow
Write-Host "WhatIf: $WhatIf" -ForegroundColor Yellow
Write-Host ""

# List of controls that need simple designers (from build errors)
$controls = @(
    "KryptonDataGridViewMaskedTextBoxColumn",
    "KryptonDataGridViewNumericUpDownColumn", 
    "KryptonNumericUpDown",
    "KryptonDataGridViewTextBoxColumn",
    "KryptonDataGridViewTextBoxEditingControl",
    "KryptonDateTimePicker",
    "KryptonThemeComboBox",
    "KryptonCheckButton",
    "KryptonDropButton",
    "KryptonBorderEdge",
    "KryptonBreadCrumb",
    "KryptonThemeListBox",
    "KryptonContextMenu",
    "KryptonDomainUpDown",
    "KryptonBreadCrumbItem",
    "KryptonTrackBar",
    "KryptonDataGridViewComboBoxColumn",
    "KryptonCheckSet",
    "KryptonTreeView",
    "KryptonDataGridViewDateTimePickerColumn",
    "KryptonColorButton",
    "KryptonDataGridViewDomainUpDownColumn",
    "KryptonHeader",
    "KryptonHeaderGroup",
    "KryptonGroup",
    "KryptonCommand",
    "KryptonWebBrowser",
    "KryptonWrapLabel",
    "KryptonGroupPanel",
    "KryptonPoweredByButton",
    "KryptonLinkLabel",
    "KryptonCustomPaletteBase",
    "KryptonSplitContainer",
    "KryptonSplitterPanel",
    "KryptonLinkWrapLabel",
    "KryptonPropertyGrid",
    "KryptonListView",
    "KryptonRichTextBox",
    "KryptonScrollBar",
    "KryptonSeparator",
    "KryptonManager",
    "KryptonMaskedTextBox",
    "KryptonMonthCalendar"
)

# Template for simple designer
$template = @'
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
/// Simplified designer for {0} optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class {0}SimpleDesigner : ControlDesigner
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
                actionLists.Add(new {0}SimpleActionList(Component));
            }}

            return actionLists;
        }}
    }}
    #endregion
}}

/// <summary>
/// Simplified action list for {0} optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class {0}SimpleActionList : DesignerActionList
{{
    #region Instance Fields
    private readonly {0} _{1};
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the {0}SimpleActionList class.
    /// </summary>
    /// <param name="component">The component to create actions for.</param>
    public {0}SimpleActionList(IComponent component)
        : base(component)
    {{
        _{1} = ({0})component;
    }}
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    [Category("Visuals")]
    [Description("Palette applied to drawing")]
    public PaletteMode PaletteMode
    {{
        get => _{1}.PaletteMode;
        set
        {{
            if (_{1}.PaletteMode != value)
            {{
                _{1}.PaletteMode = value;
                NotifyPropertyChanged("PaletteMode", value);
            }}
        }}
    }}
    #endregion

    #region Private Methods
    /// <summary>
    /// Notify that a property has changed.
    /// </summary>
    /// <param name="propertyName">Name of the property that changed.</param>
    /// <param name="value">New value of the property.</param>
    private void NotifyPropertyChanged(string propertyName, object? value)
    {{
        var changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
        if (changeService != null)
        {{
            var propertyDescriptor = TypeDescriptor.GetProperties(_{1})[propertyName];
            changeService.OnComponentChanged(_{1}, propertyDescriptor, null, value);
        }}
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

        if (_{1} != null)
        {{
            // Visuals
            actions.Add(new DesignerActionHeaderItem("Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), "Palette", "Visuals", "Palette applied to drawing"));
        }}

        return actions;
    }}
    #endregion
}}
'@

$createdCount = 0
$skippedCount = 0
$errorCount = 0

foreach ($control in $controls) {
    try {
        $fileName = "${control}SimpleDesigner.cs"
        $filePath = Join-Path $OutputPath $fileName
        
        # Check if file already exists
        if (Test-Path $filePath) {
            Write-Host "  $fileName already exists - skipping" -ForegroundColor Yellow
            $skippedCount++
            continue
        }
        
        # Generate field name (lowercase first letter)
        $fieldName = $control.Substring(0,1).ToLower() + $control.Substring(1)
        
        # Generate content
        $content = $template -f $control, $fieldName
        
        if ($WhatIf) {
            Write-Host "  Would create: $fileName" -ForegroundColor Green
        } else {
            Set-Content -Path $filePath -Value $content -NoNewline
            Write-Host "  Created: $fileName" -ForegroundColor Green
        }
        
        $createdCount++
    }
    catch {
        Write-Host "  Error creating $control`: $($_.Exception.Message)" -ForegroundColor Red
        $errorCount++
    }
}

Write-Host ""
Write-Host "Summary:" -ForegroundColor Green
Write-Host "  Created: $createdCount" -ForegroundColor Green
Write-Host "  Skipped: $skippedCount" -ForegroundColor Yellow
Write-Host "  Errors: $errorCount" -ForegroundColor Red

if ($WhatIf) {
    Write-Host ""
    Write-Host "This was a dry run. Use without -WhatIf to create files." -ForegroundColor Yellow
}

Write-Host ""
Write-Host "Next steps:" -ForegroundColor Green
Write-Host "1. Test compilation" -ForegroundColor White
Write-Host "2. Add specific properties to each designer as needed" -ForegroundColor White
Write-Host "3. Test designer functionality" -ForegroundColor White
