#region BSD License
/*
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Implement storage for palette content text details.
/// </summary>
public class PaletteContentText : Storage
{
    #region Internal Classes
    private class InternalStorage
    {
        public Font? ContentTextFont;
        public PaletteTextHint ContentTextHint;
        public PaletteTextTrim ContentTextTrim;
        public PaletteTextHotkeyPrefix ContentTextPrefix;
        public PaletteRelativeAlign ContentTextH;
        public PaletteRelativeAlign ContentTextV;
        public PaletteRelativeAlign ContentTextMultiLineH;
        public InheritBool ContentTextMultiLine;
        public Color ContentTextColor1;
        public Color ContentTextColor2;
        public PaletteColorStyle ContentTextColorStyle;
        public PaletteRectangleAlign ContentTextColorAlign;
        public float ContentTextColorAngle;
        public Image? ContentTextImage;
        public PaletteImageStyle ContentTextImageStyle;
        public PaletteRectangleAlign ContentTextImageAlign;

        /// <summary>
        /// Initialize a new instance of the InternalStorage structure.
        /// </summary>
        public InternalStorage()
        {
            // Set to default values
            ContentTextHint = PaletteTextHint.Inherit;
            ContentTextTrim = PaletteTextTrim.Inherit;
            ContentTextPrefix = PaletteTextHotkeyPrefix.Inherit;
            ContentTextH = PaletteRelativeAlign.Inherit;
            ContentTextV = PaletteRelativeAlign.Inherit;
            ContentTextMultiLineH = PaletteRelativeAlign.Inherit;
            ContentTextMultiLine = InheritBool.Inherit;
            ContentTextColor1 = SharedStaticVariables.EMPTY_COLOR;
            ContentTextColor2 = SharedStaticVariables.EMPTY_COLOR;
            ContentTextColorStyle = PaletteColorStyle.Inherit;
            ContentTextColorAlign = PaletteRectangleAlign.Inherit;
            ContentTextColorAngle = -1;
            ContentTextImageStyle = PaletteImageStyle.Inherit;
            ContentTextImageAlign = PaletteRectangleAlign.Inherit;
        }

        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        public bool IsDefault => (ContentTextFont == null) &&
                                 (ContentTextHint == PaletteTextHint.Inherit) &&
                                 (ContentTextTrim == PaletteTextTrim.Inherit) &&
                                 (ContentTextPrefix == PaletteTextHotkeyPrefix.Inherit) &&
                                 (ContentTextH == PaletteRelativeAlign.Inherit) &&
                                 (ContentTextV == PaletteRelativeAlign.Inherit) &&
                                 (ContentTextMultiLineH == PaletteRelativeAlign.Inherit) &&
                                 (ContentTextMultiLine == InheritBool.Inherit) &&
                                 (ContentTextColor1 == SharedStaticVariables.EMPTY_COLOR) &&
                                 (ContentTextColor2 == SharedStaticVariables.EMPTY_COLOR) &&
                                 (ContentTextColorStyle == PaletteColorStyle.Inherit) &&
                                 (ContentTextColorAlign == PaletteRectangleAlign.Inherit) &&
                                 (ContentTextColorAngle == -1) &&
                                 (ContentTextImage == null) &&
                                 (ContentTextImageStyle == PaletteImageStyle.Inherit) &&
                                 (ContentTextImageAlign == PaletteRectangleAlign.Inherit);
    }
    #endregion

    #region Instance Fields
    private InternalStorage? _storage;
    private Font? _factoryFont;
    private PaletteRelativeAlign _factoryTextH = PaletteRelativeAlign.Inherit;
    private PaletteRelativeAlign _factoryTextV = PaletteRelativeAlign.Inherit;
    private PaletteRelativeAlign _factoryMultiLineH = PaletteRelativeAlign.Inherit;
    private InheritBool _factoryMultiLine = InheritBool.Inherit;
    private PaletteTextTrim _factoryTrim = PaletteTextTrim.Inherit;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when a property has changed value.
    /// </summary>
    [Browsable(false)]  // SKC: Probably a special case for not exposing this event in the designer....
    [EditorBrowsable(EditorBrowsableState.Never)]
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteContentText class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteContentText(NeedPaintHandler? needPaint) =>
        // Store the provided paint notification delegate
        NeedPaint = needPaint;

    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault
    {
        get
        {
            if (_storage == null)
            {
                return (_factoryFont == null)
                       && (_factoryTextH == PaletteRelativeAlign.Inherit)
                       && (_factoryTextV == PaletteRelativeAlign.Inherit)
                       && (_factoryMultiLineH == PaletteRelativeAlign.Inherit)
                       && (_factoryMultiLine == InheritBool.Inherit)
                       && (_factoryTrim == PaletteTextTrim.Inherit);
            }

            return (_storage.ContentTextFont == _factoryFont)
                   && (_storage.ContentTextHint == PaletteTextHint.Inherit)
                   && (_storage.ContentTextTrim == _factoryTrim)
                   && (_storage.ContentTextPrefix == PaletteTextHotkeyPrefix.Inherit)
                   && (_storage.ContentTextH == _factoryTextH)
                   && (_storage.ContentTextV == _factoryTextV)
                   && (_storage.ContentTextMultiLineH == _factoryMultiLineH)
                   && (_storage.ContentTextMultiLine == _factoryMultiLine)
                   && (_storage.ContentTextColor1 == SharedStaticVariables.EMPTY_COLOR)
                   && (_storage.ContentTextColor2 == SharedStaticVariables.EMPTY_COLOR)
                   && (_storage.ContentTextColorStyle == PaletteColorStyle.Inherit)
                   && (_storage.ContentTextColorAlign == PaletteRectangleAlign.Inherit)
                   && (_storage.ContentTextColorAngle == -1)
                   && (_storage.ContentTextImage == null)
                   && (_storage.ContentTextImageStyle == PaletteImageStyle.Inherit)
                   && (_storage.ContentTextImageAlign == PaletteRectangleAlign.Inherit);
        }
    }

    /// <summary>
    /// Treats <paramref name="font"/> as the unset designer default for <see cref="Font"/>.
    /// </summary>
    internal void SetDefaultFont(Font? font)
    {
        _factoryFont = font;
        Font = font;
    }

    /// <summary>
    /// Treats <paramref name="value"/> as the unset designer default for <see cref="TextH"/>.
    /// </summary>
    internal void SetDefaultTextH(PaletteRelativeAlign value)
    {
        _factoryTextH = value;
        TextH = value;
    }

    /// <summary>
    /// Treats <paramref name="value"/> as the unset designer default for <see cref="TextV"/>.
    /// </summary>
    internal void SetDefaultTextV(PaletteRelativeAlign value)
    {
        _factoryTextV = value;
        TextV = value;
    }

    /// <summary>
    /// Treats <paramref name="value"/> as the unset designer default for <see cref="MultiLineH"/>.
    /// </summary>
    internal void SetDefaultMultiLineH(PaletteRelativeAlign value)
    {
        _factoryMultiLineH = value;
        MultiLineH = value;
    }

    /// <summary>
    /// Treats <paramref name="value"/> as the unset designer default for <see cref="MultiLine"/>.
    /// </summary>
    internal void SetDefaultMultiLine(InheritBool value)
    {
        _factoryMultiLine = value;
        MultiLine = value;
    }

    /// <summary>
    /// Treats <paramref name="value"/> as the unset designer default for <see cref="Trim"/>.
    /// </summary>
    internal void SetDefaultTrim(PaletteTextTrim value)
    {
        _factoryTrim = value;
        Trim = value;
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
        get => _storage?.ContentTextFont;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextFont != value)
                {
                    _storage.ContentTextFont = value;
                    OnPropertyChanged(nameof(Font));
                    PerformNeedPaint(true);
                }
            }
            else
            {
                if (value != null)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextFont = value
                    };
                    OnPropertyChanged(nameof(Font));
                    PerformNeedPaint(true);
                }
            }
        }
    }

    private bool ShouldSerializeFont() => Font != _factoryFont;
    private void ResetFont() => Font = _factoryFont;

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
        get => _storage?.ContentTextHint ?? PaletteTextHint.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextHint != value)
                {
                    _storage.ContentTextHint = value;
                    OnPropertyChanged(nameof(Hint));
                    PerformNeedPaint(true);
                }
            }
            else
            {
                if (value != PaletteTextHint.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextHint = value
                    };
                    OnPropertyChanged(nameof(Hint));
                    PerformNeedPaint(true);
                }
            }
        }
    }
    #endregion

    #region Trim
    /// <summary>
    /// Gets the text trimming for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Text trimming style for the content text.")]
    [DefaultValue(PaletteTextTrim.Inherit)]
    [RefreshProperties(RefreshProperties.All)]
    public virtual PaletteTextTrim Trim
    {
        get => _storage?.ContentTextTrim ?? PaletteTextTrim.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextTrim != value)
                {
                    _storage.ContentTextTrim = value;
                    OnPropertyChanged(nameof(Trim));
                    PerformNeedPaint(true);
                }
            }
            else
            {
                if (value != PaletteTextTrim.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextTrim = value
                    };
                    OnPropertyChanged(nameof(Trim));
                    PerformNeedPaint(true);
                }
            }
        }
    }

    private bool ShouldSerializeTrim() => Trim != _factoryTrim;
    private void ResetTrim() => Trim = _factoryTrim;

    #endregion

    #region Prefix
    /// <summary>
    /// Gets the drawing used for prefix characters.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"How to draw prefix characters for the content text.")]
    [DefaultValue(PaletteTextHotkeyPrefix.Inherit)]
    [RefreshProperties(RefreshProperties.All)]
    public virtual PaletteTextHotkeyPrefix Prefix
    {
        get => _storage?.ContentTextPrefix ?? PaletteTextHotkeyPrefix.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextPrefix != value)
                {
                    _storage.ContentTextPrefix = value;
                    OnPropertyChanged(nameof(Prefix));
                    PerformNeedPaint(true);
                }
            }
            else
            {
                if (value != PaletteTextHotkeyPrefix.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextPrefix = value
                    };
                    OnPropertyChanged(nameof(Prefix));
                    PerformNeedPaint(true);
                }
            }
        }
    }
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
        get => _storage?.ContentTextH ?? PaletteRelativeAlign.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextH != value)
                {
                    _storage.ContentTextH = value;
                    OnPropertyChanged(nameof(TextH));
                    PerformNeedPaint(true);
                }
            }
            else
            {
                if (value != PaletteRelativeAlign.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextH = value
                    };
                    OnPropertyChanged(nameof(TextH));
                    PerformNeedPaint(true);
                }
            }
        }
    }

    private bool ShouldSerializeTextH() => TextH != _factoryTextH;
    private void ResetTextH() => TextH = _factoryTextH;

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
        get => _storage?.ContentTextV ?? PaletteRelativeAlign.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextV != value)
                {
                    _storage.ContentTextV = value;
                    OnPropertyChanged(nameof(TextV));
                    PerformNeedPaint(true);
                }
            }
            else
            {
                if (value != PaletteRelativeAlign.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextV = value
                    };
                    OnPropertyChanged(nameof(TextV));
                    PerformNeedPaint(true);
                }
            }
        }
    }

    private bool ShouldSerializeTextV() => TextV != _factoryTextV;
    private void ResetTextV() => TextV = _factoryTextV;

    #endregion

    #region MultiLineH
    /// <summary>
    /// Gets the relative horizontal alignment of multiline content text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Relative horizontal alignment of multiline content text.")]
    [DefaultValue(PaletteRelativeAlign.Inherit)]
    [RefreshProperties(RefreshProperties.All)]
    public virtual PaletteRelativeAlign MultiLineH
    {
        get => _storage?.ContentTextMultiLineH ?? PaletteRelativeAlign.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextMultiLineH != value)
                {
                    _storage.ContentTextMultiLineH = value;
                    OnPropertyChanged(nameof(MultiLineH));
                    PerformNeedPaint(true);
                }
            }
            else
            {
                if (value != PaletteRelativeAlign.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextMultiLineH = value
                    };
                    OnPropertyChanged(nameof(MultiLineH));
                    PerformNeedPaint(true);
                }
            }
        }
    }

    private bool ShouldSerializeMultiLineH() => MultiLineH != _factoryMultiLineH;
    private void ResetMultiLineH() => MultiLineH = _factoryMultiLineH;

    #endregion

    #region MultiLine
    /// <summary>
    /// Gets the flag indicating if multiline text is allowed.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Flag indicating if multiline text is allowed..")]
    [DefaultValue(InheritBool.Inherit)]
    [RefreshProperties(RefreshProperties.All)]
    public virtual InheritBool MultiLine
    {
        get => _storage?.ContentTextMultiLine ?? InheritBool.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextMultiLine != value)
                {
                    _storage.ContentTextMultiLine = value;
                    OnPropertyChanged(nameof(MultiLine));
                    PerformNeedPaint(true);
                }
            }
            else
            {
                if (value != InheritBool.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextMultiLine = value
                    };
                    OnPropertyChanged(nameof(MultiLine));
                    PerformNeedPaint(true);
                }
            }
        }
    }

    private bool ShouldSerializeMultiLine() => MultiLine != _factoryMultiLine;
    private void ResetMultiLine() => MultiLine = _factoryMultiLine;

    #endregion

    #region Color1
    /// <summary>
    /// Gets and sets the first color for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Main color for the text.")]
    [KryptonDefaultColor]
    [RefreshProperties(RefreshProperties.All)]
    public virtual Color Color1
    {
        get => _storage?.ContentTextColor1 ?? SharedStaticVariables.EMPTY_COLOR;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextColor1 != value)
                {
                    _storage.ContentTextColor1 = value;
                    OnPropertyChanged(nameof(Color1));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != SharedStaticVariables.EMPTY_COLOR)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextColor1 = value
                    };
                    OnPropertyChanged(nameof(Color1));
                    PerformNeedPaint();
                }
            }
        }
    }
    #endregion

    #region Color2
    /// <summary>
    /// Gets and sets the second color for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Secondary color for the text.")]
    [KryptonDefaultColor]
    [RefreshProperties(RefreshProperties.All)]
    public virtual Color Color2
    {
        get => _storage?.ContentTextColor2 ?? SharedStaticVariables.EMPTY_COLOR;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextColor2 != value)
                {
                    _storage.ContentTextColor2 = value;
                    OnPropertyChanged(nameof(Color2));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != SharedStaticVariables.EMPTY_COLOR)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextColor2 = value
                    };
                    OnPropertyChanged(nameof(Color2));
                    PerformNeedPaint();
                }
            }
        }
    }
    #endregion

    #region ColorStyle
    /// <summary>
    /// Gets and sets the color drawing style for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Color drawing style for the text.")]
    [DefaultValue(PaletteColorStyle.Inherit)]
    [RefreshProperties(RefreshProperties.All)]
    public virtual PaletteColorStyle ColorStyle
    {
        get => _storage?.ContentTextColorStyle ?? PaletteColorStyle.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextColorStyle != value)
                {
                    _storage.ContentTextColorStyle = value;
                    OnPropertyChanged(nameof(ColorStyle));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != PaletteColorStyle.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextColorStyle = value
                    };
                    OnPropertyChanged(nameof(ColorStyle));
                    PerformNeedPaint();
                }
            }
        }
    }
    #endregion

    #region ColorAlign
    /// <summary>
    /// Gets and set the color alignment for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Color alignment style for the text.")]
    [DefaultValue(PaletteRectangleAlign.Inherit)]
    [RefreshProperties(RefreshProperties.All)]
    public virtual PaletteRectangleAlign ColorAlign
    {
        get => _storage?.ContentTextColorAlign ?? PaletteRectangleAlign.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextColorAlign != value)
                {
                    _storage.ContentTextColorAlign = value;
                    OnPropertyChanged(nameof(ColorAlign));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != PaletteRectangleAlign.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextColorAlign = value
                    };
                    OnPropertyChanged(nameof(ColorAlign));
                    PerformNeedPaint();
                }
            }
        }
    }
    #endregion

    #region ColorAngle
    /// <summary>
    /// Gets and sets the color angle for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Color angle for the text.")]
    [DefaultValue(-1f)]
    [RefreshProperties(RefreshProperties.All)]
    public virtual float ColorAngle
    {
        get => _storage?.ContentTextColorAngle ?? -1f;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextColorAngle != value)
                {
                    _storage.ContentTextColorAngle = value;
                    OnPropertyChanged(nameof(ColorAngle));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != -1f)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextColorAngle = value
                    };
                    OnPropertyChanged(nameof(ColorAngle));
                    PerformNeedPaint();
                }
            }
        }
    }
    #endregion

    #region Image
    /// <summary>
    /// Gets and sets the image for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Image for the text.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    [Editor(typeof(KryptonDesignerImageEditor), typeof(UITypeEditor))]
    public virtual Image? Image
    {
        get => _storage?.ContentTextImage;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextImage != value)
                {
                    _storage.ContentTextImage = value;
                    OnPropertyChanged(nameof(Image));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != null)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextImage = value
                    };
                    OnPropertyChanged(nameof(Image));
                    PerformNeedPaint();
                }
            }
        }
    }
    #endregion

    #region ImageStyle
    /// <summary>
    /// Gets and sets the image style for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Image style for the text.")]
    [DefaultValue(PaletteImageStyle.Inherit)]
    [RefreshProperties(RefreshProperties.All)]
    public virtual PaletteImageStyle ImageStyle
    {
        get => _storage?.ContentTextImageStyle ?? PaletteImageStyle.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextImageStyle != value)
                {
                    _storage.ContentTextImageStyle = value;
                    OnPropertyChanged(nameof(ImageStyle));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != PaletteImageStyle.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextImageStyle = value
                    };
                    OnPropertyChanged(nameof(ImageStyle));
                    PerformNeedPaint();
                }
            }
        }
    }
    #endregion

    #region ImageAlign
    /// <summary>
    /// Gets and set the image alignment for the text.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Image alignment style for the text.")]
    [DefaultValue(PaletteRectangleAlign.Inherit)]
    [RefreshProperties(RefreshProperties.All)]
    public virtual PaletteRectangleAlign ImageAlign
    {
        get => _storage?.ContentTextImageAlign ?? PaletteRectangleAlign.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.ContentTextImageAlign != value)
                {
                    _storage.ContentTextImageAlign = value;
                    OnPropertyChanged(nameof(ImageAlign));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != PaletteRectangleAlign.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        ContentTextImageAlign = value
                    };
                    OnPropertyChanged(nameof(ImageAlign));
                    PerformNeedPaint();
                }
            }
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="property">Name of the property changed.</param>
    protected virtual void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

    #endregion
}
