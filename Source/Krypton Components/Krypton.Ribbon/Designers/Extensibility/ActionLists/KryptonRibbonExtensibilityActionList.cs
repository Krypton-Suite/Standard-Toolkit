#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Action list for KryptonRibbon using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonRibbonExtensibilityActionList : KryptonRibbonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRibbonExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list.</param>
    public KryptonRibbonExtensibilityActionList(KryptonRibbonExtensibilityDesigner owner)
        : base(owner)
    {
        _ribbon = (owner.Component as KryptonRibbon)!;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets whether the ribbon is minimized mode.
    /// </summary>
    [Category("Behavior")]
    [Description("Is the ribbon in minimized mode.")]
    [DefaultValue(false)]
    public bool MinimizedMode
    {
        get => _ribbon.MinimizedMode;
        set => SetPropertyValue(nameof(MinimizedMode), value);
    }
    #endregion

    #region Public Overrides
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var items = new DesignerActionItemCollection();

        // Add the action items
        items.Add(new DesignerActionHeaderItem("Behavior"));
        items.Add(new DesignerActionPropertyItem(nameof(MinimizedMode), "Minimized Mode", "Behavior", "Is the ribbon in minimized mode."));

        return items;
    }
    #endregion
}
