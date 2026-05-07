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
/// Implement storage for a ribbon focus palette. 
/// </summary>
public class PaletteRibbonFocus : PaletteMetricRedirect
{
    #region Instance Fields
    private readonly PaletteRibbonDouble _ribbonTab;
    private readonly PaletteRibbonDoubleInheritRedirect _ribbonTabInherit;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteRibbonFocus class.
    /// </summary>
    /// <param name="redirect">Inheritence redirection instance.</param>
    /// <param name="needPaint">Paint delegate.</param>
    public PaletteRibbonFocus([DisallowNull] PaletteRedirect redirect,
        [DisallowNull] NeedPaintHandler? needPaint)
        : base(redirect)
    {
        Debug.Assert(redirect is not null);

        if (redirect is null)
        {
            throw new ArgumentNullException(nameof(redirect));
        }

        // Store the provided paint notification delegate
        NeedPaint = needPaint ?? throw new ArgumentNullException(nameof(needPaint));

        // Create the redirection instances
        _ribbonTabInherit = new PaletteRibbonDoubleInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonTab, PaletteRibbonTextStyle.RibbonTab);

        // Create storage that maps onto the inherit instances
        _ribbonTab = new PaletteRibbonDouble(_ribbonTabInherit, _ribbonTabInherit, needPaint);
    }
    #endregion

    #region SetRedirector
    /// <summary>
    /// Update the redirector with new reference.
    /// </summary>
    /// <param name="redirect">Target redirector.</param>
    public override void SetRedirector(PaletteRedirect redirect)
    {
        base.SetRedirector(redirect);
        _ribbonTabInherit.SetRedirector(redirect);
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

    #region Protected
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