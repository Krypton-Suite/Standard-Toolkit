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
/// Implement storage for data grid view palette content details.
/// </summary>
public class PaletteDataGridViewContentCommon : PaletteDataGridViewContentStates
{
    #region Instance Fields
    private Padding _padding;
    private Font? _font;
    private PaletteRelativeAlign _textH;
    private PaletteRelativeAlign _textV;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteDataGridViewContentCommon class.
    /// </summary>
    /// <param name="inherit">Source for inheriting defaulted values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteDataGridViewContentCommon(IPaletteContent inherit,
        NeedPaintHandler needPaint)
        : base(inherit, needPaint)
    {
        // Default the initial values
        _padding = CommonHelper.InheritPadding;
        _textH = PaletteRelativeAlign.Inherit;
        _textV = PaletteRelativeAlign.Inherit;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => base.IsDefault &&
                                      Padding.Equals(CommonHelper.InheritPadding) &&
                                      (Font == null) &&
                                      (TextH == PaletteRelativeAlign.Inherit) &&
                                      (TextV == PaletteRelativeAlign.Inherit);

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">Palette state to use when populating.</param>
    public override void PopulateFromBase(PaletteState state)
    {
        base.PopulateFromBase(state);
        Font = GetContentShortTextFont(state);
        TextH = GetContentShortTextH(state);
        TextV = GetContentShortTextV(state);
        Padding = GetBorderContentPadding(null, state);
    }
    #endregion

    #region Font
    /// <summary>
    /// Gets the font for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Font for drawing the content text.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public virtual Font? Font
    {
        get => _font;

        set
        {
            if (value != _font)
            {
                _font = value;
                OnSyncPropertyChanged(EventArgs.Empty);
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets the actual content short text font value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Font value.</returns>
    public override Font? GetContentShortTextFont(PaletteState state) => _font ?? Inherit.GetContentShortTextFont(state);

    #endregion

    #region TextH
    /// <summary>
    /// Gets the horizontal relative alignment of the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Relative horizontal alignment of content text.")]
    [DefaultValue(PaletteRelativeAlign.Inherit)]
    [RefreshProperties(RefreshProperties.All)]
    public virtual PaletteRelativeAlign TextH
    {
        get => _textH;

        set
        {
            if (value != _textH)
            {
                _textH = value;
                OnSyncPropertyChanged(EventArgs.Empty);
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets the actual content short text horizontal alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextH(PaletteState state) => _textH != PaletteRelativeAlign.Inherit ? _textH : Inherit.GetContentShortTextH(state);

    #endregion

    #region TextV
    /// <summary>
    /// Gets the vertical relative alignment of the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Relative vertical alignment of content text.")]
    [DefaultValue(PaletteRelativeAlign.Inherit)]
    [RefreshProperties(RefreshProperties.All)]
    public virtual PaletteRelativeAlign TextV
    {
        get => _textV;

        set
        {
            if (value != _textV)
            {
                _textV = value;
                OnSyncPropertyChanged(EventArgs.Empty);
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets the actual content short text vertical alignment value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>RelativeAlignment value.</returns>
    public override PaletteRelativeAlign GetContentShortTextV(PaletteState state) => _textV != PaletteRelativeAlign.Inherit ? _textV : Inherit.GetContentShortTextV(state);

    #endregion

    #region Padding
    /// <summary>
    /// Gets the padding between the border and content drawing.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Padding between the border and content drawing.")]
    [DefaultValue(typeof(Padding), "-1,-1,-1,-1")]
    [RefreshProperties(RefreshProperties.All)]
    public Padding Padding
    {
        get => _padding;

        set
        {
            if (!value.Equals(_padding))
            {
                _padding = value;
                OnSyncPropertyChanged(EventArgs.Empty);
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Reset the Padding to the default value.
    /// </summary>
    public void ResetPadding() => Padding = CommonHelper.InheritPadding;

    /// <summary>
    /// Gets the actual padding between the border and content drawing.
    /// </summary>
    /// <param name="owningForm"></param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Padding value.</returns>
    public override Padding GetBorderContentPadding(KryptonForm? owningForm, PaletteState state)
    {
        // Initialize the padding from inherited values
        Padding paddingInherit = Inherit.GetBorderContentPadding(owningForm, state);
        Padding paddingThis = Padding;

        // Override with specified values
        if (paddingThis.Left != -1)
        {
            paddingInherit.Left = paddingThis.Left;
        }

        if (paddingThis.Right != -1)
        {
            paddingInherit.Right = paddingThis.Right;
        }

        if (paddingThis.Top != -1)
        {
            paddingInherit.Top = paddingThis.Top;
        }

        if (paddingThis.Bottom != -1)
        {
            paddingInherit.Bottom = paddingThis.Bottom;
        }

        return paddingInherit;
    }
    #endregion
}