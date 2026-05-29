#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Navigator;

/// <summary>
/// Implement storage for other navigator appearance states.
/// </summary>
public class PaletteNavigatorOther : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteNavigatorOther class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteNavigatorOther(PaletteNavigatorRedirect? redirect,
        NeedPaintHandler needPaint)
    {
        // Create the palette storage
        CheckButton = new PaletteTriple(redirect!.CheckButton, needPaint);
        OverflowButton = new PaletteTriple(redirect.OverflowButton, needPaint);
        MiniButton = new PaletteTriple(redirect.MiniButton, needPaint);
        Tab = new PaletteTabTriple(redirect.Tab, needPaint);
        RibbonTab = new PaletteRibbonTabContent(redirect.RibbonTab.TabDraw, redirect.RibbonTab.TabDraw, redirect.RibbonTab.Content, needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (CheckButton.IsDefault &&
                                       OverflowButton.IsDefault &&
                                       MiniButton.IsDefault &&
                                       Tab.IsDefault &&
                                       RibbonTab.IsDefault);

    #endregion

    #region SetInherit
    /// <summary>
    /// Sets the inheritance parent.
    /// </summary>
    /// <param name="inheritNavigator">Source for inheriting.</param>
    public virtual void SetInherit(PaletteNavigator inheritNavigator)
    {
        CheckButton.SetInherit(inheritNavigator.CheckButton);
        OverflowButton.SetInherit(inheritNavigator.OverflowButton);
        MiniButton.SetInherit(inheritNavigator.MiniButton);
        Tab.SetInherit(inheritNavigator.Tab);
        RibbonTab.SetInherit(inheritNavigator.RibbonTab.TabDraw, inheritNavigator.RibbonTab.TabDraw, inheritNavigator.RibbonTab.Content);
    }
    #endregion

    #region CheckButton
    /// <summary>
    /// Gets access to the check button appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining check button appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple CheckButton { get; }

    private bool ShouldSerializeCheckButton() => !CheckButton.IsDefault;

    #endregion

    #region OverflowButton
    /// <summary>
    /// Gets access to the outlook overflow button appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining outlook overflow button appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple OverflowButton { get; }

    private bool ShouldSerializeOverflowButton() => !OverflowButton.IsDefault;

    #endregion

    #region MiniButton
    /// <summary>
    /// Gets access to the outlook mini button appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining outlook mini button appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTriple MiniButton { get; }

    private bool ShouldSerializeMiniButton() => !MiniButton.IsDefault;

    #endregion

    #region Tab
    /// <summary>
    /// Gets access to the tab appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining tab appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTabTriple Tab { get; }

    private bool ShouldSerializeTab() => !Tab.IsDefault;

    #endregion

    #region RibbonTab
    /// <summary>
    /// Gets access to the ribbon tab appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon tab appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteRibbonTabContent RibbonTab { get; }

    private bool ShouldSerializeRibbonTab() => !RibbonTab.IsDefault;

    #endregion
}