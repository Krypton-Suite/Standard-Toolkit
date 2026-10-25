#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Implement storage for a ribbon state.
/// </summary>
public class PaletteRibbonJustTab : Storage
{
    #region Instance Fields
    private readonly PaletteRibbonDouble _ribbonTab;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRibbonJustTab class.
    /// </summary>
    /// <param name="inherit">Source for inheriting values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteRibbonJustTab([DisallowNull] PaletteRibbonRedirect inherit,
        [DisallowNull] NeedPaintHandler needPaint)
    {
        Debug.Assert(inherit is not null);

        if (inherit is null)
        {
            throw new ArgumentNullException(nameof(inherit));
        }

        // Store the provided paint notification delegate
        NeedPaint = needPaint ?? throw new ArgumentNullException(nameof(needPaint));

        // Create storage that maps onto the inherit instances
        _ribbonTab = new PaletteRibbonDouble(inherit.RibbonTab, inherit.RibbonTab, needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => RibbonTab.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">The palette state to populate with.</param>
    public virtual void PopulateFromBase(PaletteState state) => _ribbonTab.PopulateFromBase(state);
    #endregion

    #region SetInherit
    /// <summary>
    /// Sets the inheritance parent.
    /// </summary>
    public virtual void SetInherit(PaletteRibbonRedirect inherit) => _ribbonTab.SetInherit(inherit.RibbonTab, inherit.RibbonTab);
    #endregion

    #region RibbonTab
    /// <summary>
    /// Gets access to the ribbon tab palette details.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining ribbon tab appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual PaletteRibbonDouble RibbonTab => _ribbonTab;

    private bool ShouldSerializeRibbonTab() => !_ribbonTab.IsDefault;

    #endregion

    #region Implementation
    /// <summary>
    /// Handle a change event from palette source.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="needLayout">True if a layout is also needed.</param>
    protected void OnNeedPaint(object? sender, bool needLayout) =>
        // Pass request from child to our own handler
        PerformNeedPaint(needLayout);
    #endregion
}