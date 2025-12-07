#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & tobitege et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides a user interface for indicating that a control on a form has an error associated with it.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(ErrorProvider))]
[DefaultEvent(nameof(SetError))]
[DefaultProperty(nameof(BlinkStyle))]
[DesignerCategory(@"code")]
[Description(@"Provides a user interface for indicating that a control on a form has an error associated with it.")]
public class KryptonErrorProvider : Component, IExtenderProvider
{
    #region Instance Fields
    
    private ErrorProvider? _errorProvider;
    private ContainerControl? _containerControl;
    private KryptonErrorBlinkStyle _blinkStyle;
    private KryptonErrorIconAlignment _iconAlignment;
    private int _iconPadding;
    private Icon? _icon;
    private PaletteBase? _palette;
    private PaletteMode _paletteMode;
    
    #endregion

    #region Identity
    
    /// <summary>
    /// Initialize a new instance of the KryptonErrorProvider class.
    /// </summary>
    public KryptonErrorProvider()
    {
        _blinkStyle = KryptonErrorBlinkStyle.BlinkIfDifferentError;
        _iconAlignment = KryptonErrorIconAlignment.MiddleRight;
        _iconPadding = 0;
        _paletteMode = PaletteMode.Global;
        _palette = KryptonManager.CurrentGlobalPalette;

        // Create the underlying ErrorProvider
        _errorProvider = new ErrorProvider
        {
            BlinkStyle = (ErrorBlinkStyle)_blinkStyle
        };

        // Set default Krypton-themed icon
        UpdateIcon();

        // Hook into global palette changes
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
    }

    /// <summary>
    /// Initialize a new instance of the KryptonErrorProvider class with a container.
    /// </summary>
    /// <param name="container">The IContainer that contains this component.</param>
    public KryptonErrorProvider(IContainer container)
        : this()
    {
        container?.Add(this);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Unhook from events
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;

            // Dispose the underlying ErrorProvider
            _errorProvider?.Dispose();
            _errorProvider = null;
            // Only dispose if we own the icon (not a SystemIcons shared instance)
            if (_icon != null && !IsSystemIcon(_icon))
            {
                _icon.Dispose();
            }
            _icon = null;
            _palette = null;
        }

        base.Dispose(disposing);
    }
    
    #endregion

    #region Public

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the palette mode.")]
    [DefaultValue(PaletteMode.Global)]
    public PaletteMode PaletteMode
    {
        get => _paletteMode;

        set
        {
            if (_paletteMode != value)
            {
                // Action depends on new value
                switch (value)
                {
                    case PaletteMode.Custom:
                        // Do nothing, you must have a palette to set
                        break;
                    default:
                        // Use the one of the built in palettes
                        _paletteMode = value;
                        _palette = KryptonManager.GetPaletteForMode(_paletteMode);
                        UpdateIcon();
                        break;
                }
            }
        }
    }

    private bool ShouldSerializePaletteMode() => PaletteMode != PaletteMode.Global;

    private void ResetPaletteMode() => PaletteMode = PaletteMode.Global;

    /// <summary>
    /// Gets and sets the custom palette.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Sets the custom palette to be used.")]
    [DefaultValue(null)]
    public PaletteBase? Palette
    {
        get => _paletteMode == PaletteMode.Custom ? _palette : null;

        set
        {
            // Only interested in changes of value
            if (_palette != value)
            {
                // Remember new palette
                _palette = value;

                // If no custom palette provided, then must be using a built in palette
                if (value == null)
                {
                    _paletteMode = PaletteMode.Global;
                    _palette = KryptonManager.CurrentGlobalPalette;
                }
                else
                {
                    // No longer using a built in palette
                    _paletteMode = PaletteMode.Custom;
                }

                UpdateIcon();
            }
        }
    }

    private bool ShouldSerializePalette() => PaletteMode == PaletteMode.Custom && _palette != null;

    private void ResetPalette()
    {
        PaletteMode = PaletteMode.Global;
        _palette = null;
    }

    /// <summary>
    /// Gets or sets a value indicating when the error icon blinks.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates when the error icon blinks.")]
    [DefaultValue(KryptonErrorBlinkStyle.BlinkIfDifferentError)]
    public KryptonErrorBlinkStyle BlinkStyle
    {
        get => _blinkStyle;

        set
        {
            if (_blinkStyle != value)
            {
                _blinkStyle = value;
                if (_errorProvider != null)
                {
                    _errorProvider.BlinkStyle = (ErrorBlinkStyle)value;
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets the alignment of the error icon.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Indicates the alignment of the error icon relative to the control.")]
    [DefaultValue(KryptonErrorIconAlignment.MiddleRight)]
    public KryptonErrorIconAlignment IconAlignment
    {
        get => _iconAlignment;

        set
        {
            if (_iconAlignment != value)
            {
                _iconAlignment = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the amount of extra space to leave between the icon and the control.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The amount of extra space to leave between the icon and the control.")]
    [DefaultValue(0)]
    public int IconPadding
    {
        get => _iconPadding;

        set
        {
            if (_iconPadding != value)
            {
                _iconPadding = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the Icon to display next to a control when an error description string has been set.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The Icon to display next to a control when an error description string has been set.")]
    [DefaultValue(null)]
    public Icon? Icon
    {
        get => _icon;

        set
        {
            if (_icon != value)
            {
                // Only dispose if we own the icon (not a SystemIcons shared instance)
                if (_icon != null && !IsSystemIcon(_icon))
                {
                    _icon.Dispose();
                }
                _icon = value;
                // Only assign to ErrorProvider if value is not null (ErrorProvider.Icon is non-nullable)
                if (_errorProvider != null && value != null)
                {
                    _errorProvider.Icon = value;
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets the ContainerControl that this ErrorProvider is bound to.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The ContainerControl that this ErrorProvider is bound to.")]
    [DefaultValue(null)]
    public ContainerControl? ContainerControl
    {
        get => _containerControl;

        set
        {
            if (_containerControl != value)
            {
                _containerControl = value;
                if (_errorProvider != null)
                {
                    _errorProvider.ContainerControl = value;
                }
            }
        }
    }

    /// <summary>
    /// Gets the underlying ErrorProvider instance.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ErrorProvider? ErrorProvider => _errorProvider;

    /// <summary>
    /// Sets the error description string for the specified control.
    /// </summary>
    /// <param name="control">The control to set the error description string for.</param>
    /// <param name="value">The error description string, or null or empty string to remove the error.</param>
    public void SetError(Control control, string value) => _errorProvider?.SetError(control, value);

    /// <summary>
    /// Gets the error description string for the specified control.
    /// </summary>
    /// <param name="control">The control to get the error description string for.</param>
    /// <returns>The error description string for the specified control.</returns>
    public string GetError(Control control) => _errorProvider?.GetError(control) ?? string.Empty;

    /// <summary>
    /// Sets the error description string for the specified control at the specified location.
    /// </summary>
    /// <param name="control">The control to set the error description string for.</param>
    /// <param name="value">The error description string, or null or empty string to remove the error.</param>
    /// <param name="alignment">The alignment of the error icon relative to the control.</param>
    public void SetError(Control control, string value, KryptonErrorIconAlignment alignment)
    {
        if (_errorProvider != null)
        {
            _errorProvider.SetIconAlignment(control, ConvertIconAlignment(alignment));
            _errorProvider.SetError(control, value);
        }
    }

    /// <summary>
    /// Sets the Error, ErrorText, and ErrorIconAlignment for the specified control to the specified values at design time.
    /// </summary>
    /// <param name="control">The control to set the error description string for.</param>
    /// <param name="alignment">The alignment of the error icon relative to the control.</param>
    public void SetIconAlignment(Control control, KryptonErrorIconAlignment alignment)
    {
        if (_errorProvider != null)
        {
            _errorProvider.SetIconAlignment(control, ConvertIconAlignment(alignment));
        }
    }

    /// <summary>
    /// Gets the location where the error icon should be placed relative to the control.
    /// </summary>
    /// <param name="control">The control to check for the icon location.</param>
    /// <returns>One of the KryptonErrorIconAlignment values. The default icon alignment is MiddleRight.</returns>
    public KryptonErrorIconAlignment GetIconAlignment(Control control)
    {
        if (_errorProvider != null)
        {
            return ConvertIconAlignment(_errorProvider.GetIconAlignment(control));
        }

        return KryptonErrorIconAlignment.MiddleRight;
    }

    /// <summary>
    /// Sets the padding between the control and the error icon.
    /// </summary>
    /// <param name="control">The control to set the padding for.</param>
    /// <param name="value">The number of pixels to add between the control and the icon.</param>
    public void SetIconPadding(Control control, int value) => _errorProvider?.SetIconPadding(control, value);

    /// <summary>
    /// Gets the amount of extra space to leave between the specified control and the error icon.
    /// </summary>
    /// <param name="control">The control to check for the padding value.</param>
    /// <returns>The number of pixels between the control and the error icon.</returns>
    public int GetIconPadding(Control control) => _errorProvider?.GetIconPadding(control) ?? 0;

    /// <summary>
    /// Clears all errors associated with this component.
    /// </summary>
    public void Clear() => _errorProvider?.Clear();

    #region IExtenderProvider

    /// <summary>
    /// Specifies whether this object can provide its extender properties to the specified object.
    /// </summary>
    /// <param name="extendee">The Object to receive the extender properties.</param>
    /// <returns>true if this object can provide extender properties to the specified object; otherwise, false.</returns>
    public bool CanExtend(object extendee) => _errorProvider?.CanExtend(extendee) ?? false;
    
    #endregion
    
    #endregion

    #region Implementation
    
    private void UpdateIcon()
    {
        // For now, use the standard error icon
        // In the future, this could be customized based on the palette
        if (_icon == null)
        {
            // Use the standard system error icon
            _icon = SystemIcons.Error;
        }

        if (_errorProvider != null)
        {
            _errorProvider.Icon = _icon;
        }
    }

    private static ErrorIconAlignment ConvertIconAlignment(KryptonErrorIconAlignment alignment) => alignment switch
    {
        KryptonErrorIconAlignment.TopLeft => ErrorIconAlignment.TopLeft,
        KryptonErrorIconAlignment.TopRight => ErrorIconAlignment.TopRight,
        KryptonErrorIconAlignment.MiddleLeft => ErrorIconAlignment.MiddleLeft,
        KryptonErrorIconAlignment.MiddleRight => ErrorIconAlignment.MiddleRight,
        KryptonErrorIconAlignment.BottomLeft => ErrorIconAlignment.BottomLeft,
        KryptonErrorIconAlignment.BottomRight => ErrorIconAlignment.BottomRight,
        _ => ErrorIconAlignment.MiddleRight
    };

    private static KryptonErrorIconAlignment ConvertIconAlignment(ErrorIconAlignment alignment) => alignment switch
    {
        ErrorIconAlignment.TopLeft => KryptonErrorIconAlignment.TopLeft,
        ErrorIconAlignment.TopRight => KryptonErrorIconAlignment.TopRight,
        ErrorIconAlignment.MiddleLeft => KryptonErrorIconAlignment.MiddleLeft,
        ErrorIconAlignment.MiddleRight => KryptonErrorIconAlignment.MiddleRight,
        ErrorIconAlignment.BottomLeft => KryptonErrorIconAlignment.BottomLeft,
        ErrorIconAlignment.BottomRight => KryptonErrorIconAlignment.BottomRight,
        _ => KryptonErrorIconAlignment.MiddleRight
    };

    /// <summary>
    /// Determines if the specified icon is a SystemIcons shared instance that must not be disposed.
    /// </summary>
    /// <param name="icon">The icon to check.</param>
    /// <returns>True if the icon is a SystemIcons instance; otherwise, false.</returns>
    private static bool IsSystemIcon(Icon icon)
    {
        if (icon == null)
        {
            return false;
        }

        // Check if the icon reference matches any of the SystemIcons properties
        // SystemIcons properties return shared static instances that must not be disposed
        return ReferenceEquals(icon, SystemIcons.Application) ||
               ReferenceEquals(icon, SystemIcons.Asterisk) ||
               ReferenceEquals(icon, SystemIcons.Error) ||
               ReferenceEquals(icon, SystemIcons.Exclamation) ||
               ReferenceEquals(icon, SystemIcons.Hand) ||
               ReferenceEquals(icon, SystemIcons.Information) ||
               ReferenceEquals(icon, SystemIcons.Question) ||
               ReferenceEquals(icon, SystemIcons.Shield) ||
               ReferenceEquals(icon, SystemIcons.Warning) ||
               ReferenceEquals(icon, SystemIcons.WinLogo);
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        // Only update if we're using the global palette
        if (_paletteMode == PaletteMode.Global)
        {
            _palette = KryptonManager.CurrentGlobalPalette;
            UpdateIcon();
        }
    }

    #endregion
}