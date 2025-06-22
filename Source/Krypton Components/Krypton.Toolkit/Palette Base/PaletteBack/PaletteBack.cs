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
/// Implement storage for palette background details.
/// </summary>
public class PaletteBack : Storage,
    IPaletteBack
{
    #region Internal Classes
    private class InternalStorage
    {
        public InheritBool BackDraw;
        public PaletteGraphicsHint BackGraphicsHint;
        public Color BackColor1;
        public Color BackColor2;
        public PaletteColorStyle BackColorStyle;
        public PaletteRectangleAlign BackColorAlign;
        public float BackColorAngle;
        public Image? BackImage;
        public PaletteImageStyle BackImageStyle;
        public PaletteRectangleAlign BackImageAlign;

        /// <summary>
        /// Initialize a new instance of the InternalStorage structure.
        /// </summary>
        public InternalStorage()
        {
            // Set to default values
            BackDraw = InheritBool.Inherit;
            BackGraphicsHint = PaletteGraphicsHint.Inherit;
            BackColor1 = GlobalStaticValues.EMPTY_COLOR;
            BackColor2 = GlobalStaticValues.EMPTY_COLOR;
            BackColorStyle = PaletteColorStyle.Inherit;
            BackColorAlign = PaletteRectangleAlign.Inherit;
            BackColorAngle = -1;
            BackImageStyle = PaletteImageStyle.Inherit;
            BackImageAlign = PaletteRectangleAlign.Inherit;
        }

        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        public bool IsDefault => (BackDraw == InheritBool.Inherit) &&
                                 (BackGraphicsHint == PaletteGraphicsHint.Inherit) &&
                                 (BackColor1 == GlobalStaticValues.EMPTY_COLOR) &&
                                 (BackColor2 == GlobalStaticValues.EMPTY_COLOR) &&
                                 (BackColorStyle == PaletteColorStyle.Inherit) &&
                                 (BackColorAlign == PaletteRectangleAlign.Inherit) &&
                                 (BackColorAngle == -1) &&
                                 (BackImage == null) &&
                                 (BackImageStyle == PaletteImageStyle.Inherit) &&
                                 (BackImageAlign == PaletteRectangleAlign.Inherit);
    }
    #endregion

    #region Instance Fields
    private IPaletteBack? _inherit;
    private InternalStorage? _storage;
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
    /// Initialize a new instance of the PaletteBack class.
    /// </summary>
    /// <param name="inherit">Source for inheriting defaulted values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteBack(IPaletteBack? inherit,
        NeedPaintHandler? needPaint)
    {
        // Remember inheritance
        _inherit = inherit;

        // Store the provided paint notification delegate
        NeedPaint = needPaint;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (_storage == null) || _storage.IsDefault;

    #endregion

    #region SetInherit
    /// <summary>
    /// Sets the inheritance parent.
    /// </summary>
    public void SetInherit(IPaletteBack inherit) => _inherit = inherit;
    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    /// <param name="state">Palette state to use when populating.</param>
    public void PopulateFromBase(PaletteState state)
    {
        // Get the values and set into storage
        Draw = GetBackDraw(state);
        GraphicsHint = GetBackGraphicsHint(state);
        Color1 = GetBackColor1(state);
        Color2 = GetBackColor2(state);
        ColorStyle = GetBackColorStyle(state);
        ColorAlign = GetBackColorAlign(state);
        ColorAngle = GetBackColorAngle(state);
        Image = GetBackImage(state);
        ImageStyle = GetBackImageStyle(state);
        ImageAlign = GetBackImageAlign(state);
    }
    #endregion

    #region Draw
    /// <summary>
    /// Gets a value indicating if background should be drawn.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Should background be drawn.")]
    [DefaultValue(InheritBool.Inherit)]
    [RefreshProperties(RefreshProperties.All)]
    public InheritBool Draw
    {
        get => _storage?.BackDraw ?? InheritBool.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.BackDraw != value)
                {
                    _storage.BackDraw = value;
                    OnPropertyChanged(nameof(Draw));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != InheritBool.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        BackDraw = value
                    };
                    OnPropertyChanged(nameof(Draw));
                    PerformNeedPaint();
                }
            }
        }
    }

    /// <summary>
    /// Gets the actual background draw value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>InheritBool value.</returns>
    public InheritBool GetBackDraw(PaletteState state) => Draw != InheritBool.Inherit ? Draw : _inherit!.GetBackDraw(state);
    #endregion

    #region GraphicsHint
    /// <summary>
    /// Gets the graphics hint for drawing the background.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Hint for drawing graphics.")]
    [DefaultValue(PaletteGraphicsHint.Inherit)]
    [RefreshProperties(RefreshProperties.All)]
    public PaletteGraphicsHint GraphicsHint
    {
        get => _storage?.BackGraphicsHint ?? PaletteGraphicsHint.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.BackGraphicsHint != value)
                {
                    _storage.BackGraphicsHint = value;
                    OnPropertyChanged(nameof(GraphicsHint));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != PaletteGraphicsHint.Inherit)
                {
                    _storage = new InternalStorage
                    {
                        BackGraphicsHint = value
                    };
                    OnPropertyChanged(nameof(GraphicsHint));
                    PerformNeedPaint();
                }
            }
        }
    }

    /// <summary>
    /// Gets the actual background graphics hint value.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteGraphicsHint value.</returns>
    public PaletteGraphicsHint GetBackGraphicsHint(PaletteState state) =>
        GraphicsHint != PaletteGraphicsHint.Inherit ? GraphicsHint : _inherit!.GetBackGraphicsHint(state);
    #endregion

    #region Color1
    /// <summary>
    /// Gets and sets the first background color.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Main background color.")]
    [KryptonDefaultColor]
    [RefreshProperties(RefreshProperties.All)]
    public Color Color1
    {
        get => _storage?.BackColor1 ?? GlobalStaticValues.EMPTY_COLOR;

        set
        {
            if (_storage != null)
            {
                if (_storage.BackColor1 != value)
                {
                    _storage.BackColor1 = value;
                    OnPropertyChanged(nameof(Color1));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != GlobalStaticValues.EMPTY_COLOR)
                {
                    _storage = new InternalStorage
                    {
                        BackColor1 = value
                    };
                    OnPropertyChanged(nameof(Color1));
                    PerformNeedPaint();
                }
            }
        }
    }

    /// <summary>
    /// Gets the first background color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetBackColor1(PaletteState state) => Color1 != GlobalStaticValues.EMPTY_COLOR ? Color1 : _inherit!.GetBackColor1(state);
    #endregion

    #region Color2
    /// <summary>
    /// Gets and sets the second background color.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Secondary background color.")]
    [KryptonDefaultColor]
    [RefreshProperties(RefreshProperties.All)]
    public Color Color2
    {
        get => _storage?.BackColor2 ?? GlobalStaticValues.EMPTY_COLOR;

        set
        {
            if (_storage != null)
            {
                if (_storage.BackColor2 != value)
                {
                    _storage.BackColor2 = value;
                    OnPropertyChanged(nameof(Color2));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != GlobalStaticValues.EMPTY_COLOR)
                {
                    _storage = new InternalStorage
                    {
                        BackColor2 = value
                    };
                    OnPropertyChanged(nameof(Color2));
                    PerformNeedPaint();
                }
            }
        }
    }

    /// <summary>
    /// Gets the second back color.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public Color GetBackColor2(PaletteState state) => Color2 != GlobalStaticValues.EMPTY_COLOR ? Color2 : _inherit!.GetBackColor2(state);
    #endregion

    #region ColorStyle
    /// <summary>
    /// Gets and sets the color drawing style.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Background color drawing style.")]
    [DefaultValue(PaletteColorStyle.Inherit)]
    [RefreshProperties(RefreshProperties.All)]
    public PaletteColorStyle ColorStyle
    {
        get => _storage?.BackColorStyle ?? PaletteColorStyle.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.BackColorStyle != value)
                {
                    _storage.BackColorStyle = value;
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
                        BackColorStyle = value
                    };
                    OnPropertyChanged(nameof(ColorStyle));
                    PerformNeedPaint();
                }
            }
        }
    }

    /// <summary>
    /// Gets the color drawing style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public PaletteColorStyle GetBackColorStyle(PaletteState state) => ColorStyle != PaletteColorStyle.Inherit ? ColorStyle : _inherit!.GetBackColorStyle(state);
    #endregion

    #region ColorAlign
    /// <summary>
    /// Gets and set the color alignment.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Background color alignment style.")]
    [DefaultValue(PaletteRectangleAlign.Inherit)]
    [RefreshProperties(RefreshProperties.All)]
    public PaletteRectangleAlign ColorAlign
    {
        get => _storage?.BackColorAlign ?? PaletteRectangleAlign.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.BackColorAlign != value)
                {
                    _storage.BackColorAlign = value;
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
                        BackColorAlign = value
                    };
                    OnPropertyChanged(nameof(ColorAlign));
                    PerformNeedPaint();
                }
            }
        }
    }

    /// <summary>
    /// Gets the color alignment style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color alignment style.</returns>
    public PaletteRectangleAlign GetBackColorAlign(PaletteState state) =>
        ColorAlign != PaletteRectangleAlign.Inherit ? ColorAlign : _inherit!.GetBackColorAlign(state);
    #endregion

    #region ColorAngle
    /// <summary>
    /// Gets and sets the color angle.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Background color angle.")]
    [DefaultValue(-1f)]
    [RefreshProperties(RefreshProperties.All)]
    public float ColorAngle
    {
        get => _storage?.BackColorAngle ?? -1;

        set
        {
            if (_storage != null)
            {
                if (_storage.BackColorAngle != value)
                {
                    _storage.BackColorAngle = value;
                    OnPropertyChanged(nameof(ColorAngle));
                    PerformNeedPaint();
                }
            }
            else
            {
                if (value != -1)
                {
                    _storage = new InternalStorage
                    {
                        BackColorAngle = value
                    };
                    OnPropertyChanged(nameof(ColorAngle));
                    PerformNeedPaint();
                }
            }
        }
    }

    /// <summary>
    /// Gets the color background angle.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Angle used for color drawing.</returns>
    public float GetBackColorAngle(PaletteState state) => ColorAngle != -1 ? ColorAngle : _inherit!.GetBackColorAngle(state);

    #endregion

    #region Image
    /// <summary>
    /// Gets and sets the background image.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Background image.")]
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.All)]
    public Image? Image
    {
        get => _storage?.BackImage;

        set
        {
            if (_storage != null)
            {
                if (_storage.BackImage != value)
                {
                    _storage.BackImage = value;
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
                        BackImage = value
                    };
                    OnPropertyChanged(nameof(Image));
                    PerformNeedPaint();
                }
            }
        }
    }

    /// <summary>
    /// Gets a background image.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image instance.</returns>
    public Image? GetBackImage(PaletteState state) => Image ?? _inherit?.GetBackImage(state);
    #endregion

    #region ImageStyle
    /// <summary>
    /// Gets and sets the background image style.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Background image style.")]
    [DefaultValue(PaletteImageStyle.Inherit)]
    [RefreshProperties(RefreshProperties.All)]
    public PaletteImageStyle ImageStyle
    {
        get => _storage?.BackImageStyle ?? PaletteImageStyle.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.BackImageStyle != value)
                {
                    _storage.BackImageStyle = value;
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
                        BackImageStyle = value
                    };
                    OnPropertyChanged(nameof(ImageStyle));
                    PerformNeedPaint();
                }
            }
        }
    }

    /// <summary>
    /// Gets the background image style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image style value.</returns>
    public PaletteImageStyle GetBackImageStyle(PaletteState state) => ImageStyle != PaletteImageStyle.Inherit
        ? ImageStyle
        : _inherit!.GetBackImageStyle(state);
    #endregion

    #region ImageAlign
    /// <summary>
    /// Gets and set the image alignment.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Background image alignment style.")]
    [DefaultValue(PaletteRectangleAlign.Inherit)]
    [RefreshProperties(RefreshProperties.All)]
    public PaletteRectangleAlign ImageAlign
    {
        get => _storage?.BackImageAlign ?? PaletteRectangleAlign.Inherit;

        set
        {
            if (_storage != null)
            {
                if (_storage.BackImageAlign != value)
                {
                    _storage.BackImageAlign = value;
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
                        BackImageAlign = value
                    };
                    OnPropertyChanged(nameof(ImageAlign));
                    PerformNeedPaint();
                }
            }
        }
    }

    /// <summary>
    /// Gets the image alignment style.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Image alignment style.</returns>
    public PaletteRectangleAlign GetBackImageAlign(PaletteState state) =>
        ImageAlign != PaletteRectangleAlign.Inherit ? ImageAlign : _inherit!.GetBackImageAlign(state);
    #endregion

    #region Protected
    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="property">Name of the property changed.</param>
    protected virtual void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

    #endregion
}