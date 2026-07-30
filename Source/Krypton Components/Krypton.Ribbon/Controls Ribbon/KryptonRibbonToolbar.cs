#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Ribbon control configured for toolbar-style layout without tab headers.
/// </summary>
/// <remarks>
/// Same control surface as <see cref="KryptonRibbon"/> with <see cref="KryptonRibbon.ShowTabHeaders"/>
/// defaulting to <c>false</c>. Prefer a single tab; groups of the selected tab remain visible.
/// Caption chrome, application button/tab, QAT, and button specs are unchanged — hide those with
/// their existing properties when a pure groups-only band is required.
/// </remarks>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonRibbonToolbar), "ToolboxBitmaps.KryptonRibbonToolbar.bmp")]
[DefaultEvent(nameof(SelectedTabChanged))]
[DefaultProperty(nameof(RibbonTabs))]
[Designer(typeof(KryptonRibbonDesigner))]
[DesignerCategory(@"code")]
[Description(@"Ribbon control without tab headers, for toolbar-style layouts.")]
[Docking(DockingBehavior.Never)]
public class KryptonRibbonToolbar : KryptonRibbon
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRibbonToolbar"/> class.
    /// </summary>
    public KryptonRibbonToolbar() =>
        // Base ctor builds the view with headers visible; hide after the view tree exists.
        ShowTabHeaders = false;
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets a value indicating whether ribbon tab headers are visible.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>false</c> for this control. Override uses <c>new</c> so designer
    /// serialization treats <c>false</c> as the default (base type defaults to <c>true</c>).
    /// </remarks>
    [Category(@"Values")]
    [Description(@"Shows or hides the ribbon tab headers. When false, the ribbon acts as a toolbar for the selected tab.")]
    [DefaultValue(false)]
    public new bool ShowTabHeaders
    {
        get => base.ShowTabHeaders;
        set => base.ShowTabHeaders = value;
    }

    /// <summary>
    /// Resets the ShowTabHeaders property to its default value.
    /// </summary>
    public new void ResetShowTabHeaders() => ShowTabHeaders = false;
    #endregion
}
