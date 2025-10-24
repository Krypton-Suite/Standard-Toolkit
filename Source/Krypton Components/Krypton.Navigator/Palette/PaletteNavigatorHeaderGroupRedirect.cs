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
/// Redirect storage for Navigator HeaderGroup states.
/// </summary>
public class PaletteNavigatorHeaderGroupRedirect : PaletteHeaderGroupRedirect
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteNavigatorHeaderGroupRedirect class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteNavigatorHeaderGroupRedirect(PaletteRedirect redirect,
        NeedPaintHandler needPaint)
        : this(redirect, redirect, redirect, redirect, redirect, needPaint)
    {
    }

    /// <summary>
    /// Initialize a new instance of the PaletteNavigatorHeaderGroupRedirect class.
    /// </summary>
    /// <param name="redirectHeaderGroup">inheritance redirection for header group.</param>
    /// <param name="redirectHeaderPrimary">inheritance redirection for primary header.</param>
    /// <param name="redirectHeaderSecondary">inheritance redirection for secondary header.</param>
    /// <param name="redirectHeaderBar">inheritance redirection for bar header.</param>
    /// <param name="redirectHeaderOverflow">inheritance redirection for overflow header.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteNavigatorHeaderGroupRedirect(PaletteRedirect redirectHeaderGroup,
        PaletteRedirect redirectHeaderPrimary,
        PaletteRedirect redirectHeaderSecondary,
        [DisallowNull] PaletteRedirect redirectHeaderBar,
        [DisallowNull] PaletteRedirect redirectHeaderOverflow,
        NeedPaintHandler needPaint)
        : base(redirectHeaderGroup, redirectHeaderPrimary,
            redirectHeaderSecondary, needPaint)
    {
        Debug.Assert(redirectHeaderBar is not null);
        Debug.Assert(redirectHeaderOverflow is not null);

        if (redirectHeaderBar is null)
        {
            throw new ArgumentNullException(nameof(redirectHeaderBar));
        }

        if (redirectHeaderOverflow is null)
        {
            throw new ArgumentNullException(nameof(redirectHeaderOverflow));
        }

        // Create the palette storage
        HeaderBar = new PaletteHeaderPaddingRedirect(redirectHeaderBar, PaletteBackStyle.HeaderSecondary, PaletteBorderStyle.HeaderSecondary, PaletteContentStyle.HeaderSecondary, needPaint);
        HeaderOverflow = new PaletteHeaderPaddingRedirect(redirectHeaderOverflow, PaletteBackStyle.ButtonNavigatorStack, PaletteBorderStyle.HeaderSecondary, PaletteContentStyle.HeaderSecondary, needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (base.IsDefault &&
                                       HeaderBar.IsDefault &&
                                       HeaderOverflow.IsDefault);

    #endregion

    #region HeaderBar
    /// <summary>
    /// Gets access to the bar header appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining bar header appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteHeaderPaddingRedirect HeaderBar { get; }

    private bool ShouldSerializeHeaderBar() => !HeaderBar.IsDefault;

    #endregion

    #region HeaderOverflow
    /// <summary>
    /// Gets access to the overlow header appearance entries.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Overrides for defining overflow header appearance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteHeaderPaddingRedirect HeaderOverflow { get; }

    private bool ShouldSerializeHeaderOverflow() => !HeaderOverflow.IsDefault;

    #endregion
}