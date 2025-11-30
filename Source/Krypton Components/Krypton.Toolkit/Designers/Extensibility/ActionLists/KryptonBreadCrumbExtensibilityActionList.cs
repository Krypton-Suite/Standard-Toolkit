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
/// Action list for the KryptonBreadCrumb control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonBreadCrumbExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonBreadCrumb? _breadCrumb;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonBreadCrumbExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonBreadCrumbExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _breadCrumb = (KryptonBreadCrumb?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the palette to be applied.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _breadCrumb?.PaletteMode ?? PaletteMode.Global;
        set => SetPropertyValue(_breadCrumb!, nameof(PaletteMode), PaletteMode, value, v => _breadCrumb!.PaletteMode = v);
    }

    /// <summary>
    /// Gets and sets whether button spec tooltips are allowed.
    /// </summary>
    public bool AllowButtonSpecToolTips
    {
        get => _breadCrumb?.AllowButtonSpecToolTips ?? true;
        set => SetPropertyValue(_breadCrumb!, nameof(AllowButtonSpecToolTips), AllowButtonSpecToolTips, value, v => _breadCrumb!.AllowButtonSpecToolTips = v);
    }

    /// <summary>
    /// Gets and sets the breadcrumb selected item.
    /// </summary>
    public KryptonBreadCrumbItem? SelectedItem
    {
        get => _breadCrumb?.SelectedItem;
        set => SetPropertyValue(_breadCrumb!, nameof(SelectedItem), SelectedItem, value, v => _breadCrumb!.SelectedItem = v);
    }

    /// <summary>
    /// Gets and sets whether the breadcrumb is enabled.
    /// </summary>
    public bool Enabled
    {
        get => _breadCrumb?.Enabled ?? true;
        set => SetPropertyValue(_breadCrumb!, nameof(Enabled), Enabled, value, v => _breadCrumb!.Enabled = v);
    }

    /// <summary>
    /// Gets and sets whether the breadcrumb is visible.
    /// </summary>
    public bool Visible
    {
        get => _breadCrumb?.Visible ?? true;
        set => SetPropertyValue(_breadCrumb!, nameof(Visible), Visible, value, v => _breadCrumb!.Visible = v);
    }

    /// <summary>
    /// Gets and sets the breadcrumb button style.
    /// </summary>
    public ButtonStyle CrumbButtonStyle
    {
        get => _breadCrumb?.CrumbButtonStyle ?? ButtonStyle.BreadCrumb;
        set => SetPropertyValue(_breadCrumb!, nameof(CrumbButtonStyle), CrumbButtonStyle, value, v => _breadCrumb!.CrumbButtonStyle = v);
    }
    #endregion

    #region Public Override
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a control instance at design time
        if (_breadCrumb != null)
        {
            // Add the list of BreadCrumb specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(CrumbButtonStyle), @"Button Style", @"Appearance", @"Breadcrumb button style"));
            actions.Add(new DesignerActionHeaderItem(@"Data"));
            actions.Add(new DesignerActionPropertyItem(nameof(SelectedItem), @"Selected Item", @"Data", @"Selected breadcrumb item"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(AllowButtonSpecToolTips), @"Allow Button Spec ToolTips", @"Behavior", @"Allow button spec tooltips"));
            actions.Add(new DesignerActionPropertyItem(nameof(Enabled), @"Enabled", @"Behavior", @"Breadcrumb enabled"));
            actions.Add(new DesignerActionPropertyItem(nameof(Visible), @"Visible", @"Behavior", @"Breadcrumb visible"));
        }

        return actions;
    }
    #endregion
}
