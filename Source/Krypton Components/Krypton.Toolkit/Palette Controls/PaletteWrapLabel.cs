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
/// Provide wrap label state storage.
/// </summary>
public class PaletteWrapLabel : Storage
{
    #region Instance Fields
    private Font? _font;
    private Color _textColor;
    private PaletteTextHint _hint;
    private readonly KryptonWrapLabel _wrapLabel;
    private readonly KryptonLinkWrapLabel _linkWrapLabel;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteWrapLabel class.
    /// </summary>
    /// <param name="wrapLabel">Reference to owning control.</param>
    public PaletteWrapLabel(KryptonWrapLabel wrapLabel)
    {
        _wrapLabel = wrapLabel;
        _font = null;
        _textColor = GlobalStaticValues.EMPTY_COLOR;
        _hint = PaletteTextHint.Inherit;
    }

    /// <summary>Initializes a new instance of the <see cref="PaletteWrapLabel" /> class.</summary>
    /// <param name="linkWrapLabel">The link wrap label.</param>
    public PaletteWrapLabel(KryptonLinkWrapLabel linkWrapLabel)
    {
        _linkWrapLabel = linkWrapLabel;
        _font = null;
        _textColor = GlobalStaticValues.EMPTY_COLOR;
        _hint = PaletteTextHint.Inherit;
    }

    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (_font == null) &&
                                      (_textColor == GlobalStaticValues.EMPTY_COLOR) &&
                                      (_hint == PaletteTextHint.Inherit);

    #endregion

    #region Font
    /// <summary>
    /// Gets the font for label text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Font for drawing the label text.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public virtual Font? Font
    {
        get => _font;

        set
        {
            _font = value;
            _wrapLabel.PerformLayout();
            _wrapLabel.Invalidate();
        }
    }
    #endregion

    #region TextColor
    /// <summary>
    /// Gets and sets the color for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Color for the text.")]
    [KryptonDefaultColor]
    [RefreshProperties(RefreshProperties.All)]
    public virtual Color TextColor
    {
        get => _textColor;

        set
        {
            _textColor = value;
            _wrapLabel.Invalidate();
        }
    }
    #endregion

    #region Hint
    /// <summary>
    /// Gets the text rendering hint for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Text rendering hint for the content text.")]
    [DefaultValue(PaletteTextHint.Inherit)]
    [RefreshProperties(RefreshProperties.All)]
    public virtual PaletteTextHint Hint
    {
        get => _hint;

        set
        {
            _hint = value;
            _wrapLabel.Invalidate();
        }
    }
    #endregion
}