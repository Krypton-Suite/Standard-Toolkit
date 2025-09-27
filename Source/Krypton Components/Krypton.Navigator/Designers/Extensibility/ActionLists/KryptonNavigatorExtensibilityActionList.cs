#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Navigator;

/// <summary>
/// Action list for KryptonNavigator using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonNavigatorExtensibilityActionList : KryptonNavigatorExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonNavigator _navigator;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonNavigatorExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list.</param>
    public KryptonNavigatorExtensibilityActionList(KryptonNavigatorExtensibilityDesigner owner)
        : base(owner)
    {
        _navigator = (owner.Component as KryptonNavigator)!;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the navigator mode.
    /// </summary>
    [Category("Appearance")]
    [Description("Navigator mode.")]
    [DefaultValue(NavigatorMode.BarTabGroup)]
    public NavigatorMode NavigatorMode
    {
        get => _navigator.NavigatorMode;
        set => SetPropertyValue(nameof(NavigatorMode), value);
    }

    /// <summary>
    /// Gets or sets the page back style.
    /// </summary>
    [Category("Appearance")]
    [Description("Page background style.")]
    [DefaultValue(PaletteBackStyle.PanelClient)]
    public PaletteBackStyle PageBackStyle
    {
        get => _navigator.PageBackStyle;
        set => SetPropertyValue(nameof(PageBackStyle), value);
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
        items.Add(new DesignerActionHeaderItem("Appearance"));
        items.Add(new DesignerActionPropertyItem(nameof(NavigatorMode), "Navigator Mode", "Appearance", "Navigator mode."));
        items.Add(new DesignerActionPropertyItem(nameof(PageBackStyle), "Page Back Style", "Appearance", "Page background style."));

        return items;
    }
    #endregion
}
