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

namespace Krypton.Toolkit;

/// <summary>
/// Implement redirected storage for common bread crumb appearance.
/// </summary>
public class PaletteBreadCrumbRedirect : PaletteDoubleMetricRedirect
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteBreadCrumbRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection for bread crumb level.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteBreadCrumbRedirect(PaletteRedirect redirect,
        NeedPaintHandler needPaint)
        : base(redirect, PaletteBackStyle.PanelAlternate, PaletteBorderStyle.ControlClient) =>
        BreadCrumb = new PaletteTripleRedirect(redirect, 
            PaletteBackStyle.ButtonBreadCrumb,
            PaletteBorderStyle.ButtonBreadCrumb,
            PaletteContentStyle.ButtonBreadCrumb, 
            needPaint);

    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => base.IsDefault && BreadCrumb.IsDefault;

    #endregion

    #region BreadCrumb
    /// <summary>
    /// Gets access to the bread crumb appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining bread crumb appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteTripleRedirect BreadCrumb { get; }

    private bool ShouldSerializeBreadCrumb() => !BreadCrumb.IsDefault;

    #endregion  
}