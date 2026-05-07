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
/// Implement storage but remove accesss to the non image properties.
/// </summary>
public class PaletteContentJustImage : PaletteContent
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteContentJustImage class.
    /// </summary>
    /// <param name="inherit">Source for inheriting defaulted values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteContentJustImage(IPaletteContent inherit,
        NeedPaintHandler? needPaint)
        : base(inherit, needPaint)
    {
    }
    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">Palette state to use when populating.</param>
    public override void PopulateFromBase(PaletteState state)
    {
        // Get the values and set into storage
        Draw = GetContentDraw(state);
        Image!.ImageH = GetContentImageH(state);
        Image.ImageV = GetContentImageV(state);
        Image.Effect = GetContentImageEffect(state);
        Image.ImageColorMap = GetContentImageColorMap(state);
        Image.ImageColorTo = GetContentImageColorTo(state);
        Padding = GetBorderContentPadding(null, state);
    }
    #endregion

    #region DrawFocus
    /// <summary>
    /// Gets a value indicating if content should be drawn with focus indication.
    /// </summary>
    [KryptonPersist(false)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override InheritBool DrawFocus
    {
        get => base.DrawFocus;
        set => base.DrawFocus = value;
    }
    #endregion

    #region ShortText
    /// <summary>
    /// Gets access to the short text palette details.
    /// </summary>
    [KryptonPersist]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override PaletteContentText ShortText => base.ShortText;

    #endregion

    #region LongText
    /// <summary>
    /// Gets access to the long text palette details.
    /// </summary>
    [KryptonPersist]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override PaletteContentText LongText => base.LongText;

    #endregion

    #region AdjacentGap
    /// <summary>
    /// Gets the padding between adjacent content items.
    /// </summary>
    [KryptonPersist(false)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override int AdjacentGap
    {
        get => base.AdjacentGap;
        set => base.AdjacentGap = value;
    }
    #endregion
}