#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & tobitege et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

using Timer = System.Windows.Forms.Timer;

/// <summary>
/// Provides a user interface for indicating that a control on a form has an error associated with it.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(ErrorProvider))]
[DefaultEvent(nameof(SetError))]
[DefaultProperty(nameof(BlinkStyle))]
[DesignerCategory(@"code")]
[Description(@"Provides a user interface for indicating that a control on a form has an error associated with it.")]
[ProvideProperty("Error", typeof(Control))]
[ProvideProperty("ChangeBorderColorOnError", typeof(Control))]
public class KryptonErrorProvider : Component, IExtenderProvider
{
    #region Instance Fields
    
    private ErrorProvider? _errorProvider;
    private ContainerControl? _containerControl;
    private KryptonErrorBlinkStyle _blinkStyle;
    private KryptonErrorIconAlignment _iconAlignment;
    private int _iconPadding;
    private Icon? _icon;
    private Icon? _resizedIcon;
    private Size _iconSize;
    private PaletteBase? _palette;
    private PaletteMode _paletteMode;
    private bool _changeBorderColor;
    private readonly Dictionary<Control, bool> _changeBorderColorOnControl;
    private ToolTipValues? _toolTipValues;
    private VisualPopupToolTip? _visualPopupToolTip;
    private PaletteRedirect? _redirector;
    private IRenderer? _renderer;
    private Control? _currentHoverControl;
    private Timer? _toolTipTimer;
    private readonly Dictionary<Control, string> _errorMessages;
    private const int DEFAULT_ICON_SIZE = 16;
    
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
        _iconSize = new Size(DEFAULT_ICON_SIZE, DEFAULT_ICON_SIZE);
        _paletteMode = PaletteMode.Global;
        _palette = KryptonManager.CurrentGlobalPalette;
        _changeBorderColor = true;
        _changeBorderColorOnControl = new Dictionary<Control, bool>();
        _errorMessages = new Dictionary<Control, string>();

        // Create the underlying ErrorProvider
        _errorProvider = new ErrorProvider
        {
            BlinkStyle = (ErrorBlinkStyle)_blinkStyle
        };

        // Set default Krypton-themed icon
        UpdateIcon();

        // Initialize tooltip system
        InitializeToolTipSystem();

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

            // Clean up tooltip system
            CleanupToolTipSystem();

            // Unhook container control events
            UnhookContainerControlEvents();

            // Dispose the underlying ErrorProvider
            _errorProvider?.Dispose();
            _errorProvider = null;
            // Only dispose if we own the icon (not a SystemIcons shared instance)
            if (_icon != null && !IsSystemIcon(_icon))
            {
                _icon.Dispose();
            }
            _icon = null;
            if (_resizedIcon != null && !IsSystemIcon(_resizedIcon))
            {
                _resizedIcon.Dispose();
            }
            _resizedIcon = null;
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
    /// Gets or sets the size of the error icon.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"The size of the error icon in pixels (width and height).")]
    [DefaultValue(typeof(Size), "16, 16")]
    public Size IconSize
    {
        get => _iconSize;

        set
        {
            if (_iconSize != value)
            {
                _iconSize = value;
                // Recreate resized icon if we have an icon set
                if (_icon != null)
                {
                    UpdateResizedIcon();
                }
            }
        }
    }

    private bool ShouldSerializeIconSize() => _iconSize.Width != DEFAULT_ICON_SIZE || _iconSize.Height != DEFAULT_ICON_SIZE;

    private void ResetIconSize() => IconSize = new Size(DEFAULT_ICON_SIZE, DEFAULT_ICON_SIZE);

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
                if (_resizedIcon != null && !IsSystemIcon(_resizedIcon))
                {
                    _resizedIcon.Dispose();
                }
                _icon = value;
                _resizedIcon = null;
                // Create resized icon if needed and assign to ErrorProvider
                if (_errorProvider != null && value != null)
                {
                    UpdateResizedIcon();
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the border color of Krypton controls should be changed based on the icon type.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Indicates whether the border color of Krypton controls should be changed based on the icon type (red for error, yellow for warning, blue for information).")]
    [DefaultValue(true)]
    public bool ChangeBorderColor
    {
        get => _changeBorderColor;
        set => _changeBorderColor = value;
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
                UnhookContainerControlEvents();
                _containerControl = value;
                if (_errorProvider != null)
                {
                    _errorProvider.ContainerControl = value;
                }
                HookContainerControlEvents();
            }
        }
    }

    /// <summary>
    /// Gets the tooltip values.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Tooltip appearance and behavior settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ToolTipValues ToolTipValues
    {
        get
        {
            _toolTipValues ??= new ToolTipValues(null, () => 96F / 96F);
            return _toolTipValues;
        }
    }

    private bool ShouldSerializeToolTipValues() => _toolTipValues != null && !_toolTipValues.IsDefault;

    private void ResetToolTipValues() => _toolTipValues?.Reset();

    /// <summary>
    /// Gets the underlying ErrorProvider instance.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ErrorProvider? ErrorProvider => _errorProvider;

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
            // Store actual error message for Krypton tooltips
            if (string.IsNullOrEmpty(value))
            {
                _errorMessages.Remove(control);
            }
            else
            {
                _errorMessages[control] = value;
            }
            // Set empty tooltip text to disable standard tooltip (we'll use Krypton tooltips)
            _errorProvider.SetError(control, string.IsNullOrEmpty(value) ? string.Empty : " ");

            if (ShouldChangeBorderColor(control) && control != null)
            {
                if (string.IsNullOrEmpty(value))
                {
                    // Clear border color when error is removed
                    ErrorProviderBorderHelper.ClearBorderColor(control);
                }
                else
                {
                    // Set border color based on icon type
                    var iconType = ErrorProviderBorderHelper.GetIconType(_icon);
                    ErrorProviderBorderHelper.SetBorderColor(control, iconType);
                }
            }
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
    public void Clear()
    {
        if (_changeBorderColor && _errorProvider != null && _containerControl != null)
        {
            // Clear border colors on all controls recursively
            ClearBorderColorsRecursive(_containerControl);
        }

        // Clear control-specific settings
        _changeBorderColorOnControl.Clear();
        _errorMessages.Clear();

        _errorProvider?.Clear();
    }

    private void ClearBorderColorsRecursive(Control parent)
    {
        foreach (Control control in parent.Controls)
        {
            // Clear border color if this control has an error
            if (_errorMessages.TryGetValue(control, out string? errorText) && !string.IsNullOrEmpty(errorText))
            {
                ErrorProviderBorderHelper.ClearBorderColor(control);
            }

            // Recurse into child controls
            if (control.HasChildren)
            {
                ClearBorderColorsRecursive(control);
            }
        }
    }

    #region IExtenderProvider

    /// <summary>
    /// Specifies whether this object can provide its extender properties to the specified object.
    /// </summary>
    /// <param name="extendee">The Object to receive the extender properties.</param>
    /// <returns>true if this object can provide extender properties to the specified object; otherwise, false.</returns>
    public bool CanExtend(object extendee) => _errorProvider?.CanExtend(extendee) ?? false;

    /// <summary>
    /// Gets the error description string for the specified control (extender property).
    /// </summary>
    /// <param name="control">The control to get the error description string for.</param>
    /// <returns>The error description string for the specified control.</returns>
    [ExtenderProvidedProperty]
    [Category(@"Validation")]
    [Description(@"Gets or sets the error description string for this control.")]
    [DefaultValue("")]
    [Localizable(true)]
    public string GetError(Control control) => _errorMessages.TryGetValue(control, out string? value) ? value : string.Empty;

    /// <summary>
    /// Sets the error description string for the specified control (extender property).
    /// </summary>
    /// <param name="control">The control to set the error description string for.</param>
    /// <param name="value">The error description string, or null or empty string to remove the error.</param>
    public void SetError(Control control, string value)
    {
        // Store actual error message for Krypton tooltips
        if (string.IsNullOrEmpty(value))
        {
            _errorMessages.Remove(control);
        }
        else
        {
            _errorMessages[control] = value;
        }
        // Set empty tooltip text to disable standard tooltip (we'll use Krypton tooltips)
        _errorProvider?.SetError(control, string.IsNullOrEmpty(value) ? string.Empty : " ");

        if (ShouldChangeBorderColor(control) && control != null)
        {
            if (string.IsNullOrEmpty(value))
            {
                // Clear border color when error is removed
                ErrorProviderBorderHelper.ClearBorderColor(control);
            }
            else
            {
                // Set border color based on icon type
                var iconType = ErrorProviderBorderHelper.GetIconType(_icon);
                ErrorProviderBorderHelper.SetBorderColor(control, iconType);
            }
        }
    }

    /// <summary>
    /// Gets a value indicating whether the border color should be changed for the specified control when an error is set.
    /// </summary>
    /// <param name="control">The control to check.</param>
    /// <returns>True if border color should be changed; otherwise, false.</returns>
    [ExtenderProvidedProperty]
    [Category(@"Validation")]
    [Description(@"Gets or sets a value indicating whether the border color of this control should be changed when an error is set.")]
    [DefaultValue(true)]
    public bool GetChangeBorderColorOnError(Control control)
    {
        if (control == null)
        {
            return true;
        }

        return _changeBorderColorOnControl.TryGetValue(control, out bool value) ? value : _changeBorderColor;
    }

    /// <summary>
    /// Sets a value indicating whether the border color should be changed for the specified control when an error is set.
    /// </summary>
    /// <param name="control">The control to set the value for.</param>
    /// <param name="value">True to change border color; otherwise, false.</param>
    public void SetChangeBorderColorOnError(Control control, bool value)
    {
        if (control == null)
        {
            return;
        }

        if (value == _changeBorderColor)
        {
            // If value matches the global setting, remove the control-specific override
            _changeBorderColorOnControl.Remove(control);
        }
        else
        {
            // Store control-specific setting
            _changeBorderColorOnControl[control] = value;
        }

        // Update border color if control currently has an error
        if (_errorMessages.TryGetValue(control, out string? errorText) && !string.IsNullOrEmpty(errorText))
        {
            if (ShouldChangeBorderColor(control))
            {
                var iconType = ErrorProviderBorderHelper.GetIconType(_icon);
                ErrorProviderBorderHelper.SetBorderColor(control, iconType);
            }
            else
            {
                ErrorProviderBorderHelper.ClearBorderColor(control);
            }
        }
    }

    private bool ShouldChangeBorderColor(Control control)
    {
        if (!_changeBorderColor)
        {
            return false;
        }

        if (control == null)
        {
            return false;
        }

        // Check if there's a control-specific override
        if (_changeBorderColorOnControl.TryGetValue(control, out bool value))
        {
            return value;
        }

        // Use global setting
        return _changeBorderColor;
    }
    
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
            UpdateResizedIcon();
        }
    }

    private void UpdateResizedIcon()
    {
        if (_icon == null || _errorProvider == null)
        {
            return;
        }

        // Dispose previous resized icon if we own it
        if (_resizedIcon != null && !IsSystemIcon(_resizedIcon))
        {
            _resizedIcon.Dispose();
            _resizedIcon = null;
        }

        // Check if icon needs resizing
        if (_icon.Size == _iconSize)
        {
            // Icon is already the correct size, use it directly
            _errorProvider.Icon = _icon;
        }
        else
        {
            // Create a resized version of the icon
            try
            {
                using (Bitmap sourceBitmap = _icon.ToBitmap())
                {
                    Bitmap? resizedBitmap = GraphicsExtensions.ScaleImage(sourceBitmap, _iconSize);
                    if (resizedBitmap != null)
                    {
                        // Create icon from resized bitmap and clone it to own the handle
                        IntPtr hIcon = resizedBitmap.GetHicon();
                        try
                        {
                            Icon tempIcon = Icon.FromHandle(hIcon);
                            // Clone the icon to create an owned copy
                            _resizedIcon = (Icon)tempIcon.Clone();
                        }
                        finally
                        {
                            // Destroy the temporary handle
                            ImageNativeMethods.DestroyIcon(hIcon);
                        }
                        resizedBitmap.Dispose();
                        _errorProvider.Icon = _resizedIcon;
                    }
                    else
                    {
                        // Fallback to original icon if resize fails
                        _errorProvider.Icon = _icon;
                    }
                }
            }
            catch
            {
                // Fallback to original icon if resize fails
                _errorProvider.Icon = _icon;
            }
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
            UpdateToolTipSystem();
        }
    }

    private void InitializeToolTipSystem()
    {
        if (_palette != null)
        {
            _redirector = new PaletteRedirect(_palette);
            _renderer = _palette.GetRenderer();
        }
    }

    private void UpdateToolTipSystem()
    {
        if (_palette != null)
        {
            _redirector = new PaletteRedirect(_palette);
            _renderer = _palette.GetRenderer();
        }
    }

    private void CleanupToolTipSystem()
    {
        HideToolTip();
        _toolTipTimer?.Stop();
        _toolTipTimer?.Dispose();
        _toolTipTimer = null;
        _visualPopupToolTip?.Dispose();
        _visualPopupToolTip = null;
    }

    private void HookContainerControlEvents()
    {
        if (_containerControl != null)
        {
            _containerControl.MouseMove += OnContainerControlMouseMove;
            _containerControl.MouseLeave += OnContainerControlMouseLeave;
        }
    }

    private void UnhookContainerControlEvents()
    {
        if (_containerControl != null)
        {
            _containerControl.MouseMove -= OnContainerControlMouseMove;
            _containerControl.MouseLeave -= OnContainerControlMouseLeave;
        }
    }

    private void OnContainerControlMouseMove(object? sender, MouseEventArgs e)
    {
        if (_containerControl == null || _errorProvider == null || _palette == null)
        {
            return;
        }

        // Check all controls with errors to see if mouse is over their error icon
        Control? hoverControl = null;
        foreach (var kvp in _errorMessages)
        {
            Control control = kvp.Key;
            if (control != null && control.Visible && !string.IsNullOrEmpty(kvp.Value))
            {
                Rectangle iconBounds = GetErrorIconBounds(control);
                if (!iconBounds.IsEmpty && iconBounds.Contains(e.Location))
                {
                    hoverControl = control;
                    break;
                }
            }
        }

        if (hoverControl != null)
        {
            if (_currentHoverControl != hoverControl)
            {
                _currentHoverControl = hoverControl;
                if (_errorMessages.TryGetValue(hoverControl, out string? errorText))
                {
                    StartToolTipTimer(hoverControl, errorText, e.Location);
                }
            }
        }
        else
        {
            // Mouse not over error icon
            if (_currentHoverControl != null)
            {
                _currentHoverControl = null;
                HideToolTip();
            }
        }
    }

    private void OnContainerControlMouseLeave(object? sender, EventArgs e)
    {
        _currentHoverControl = null;
        HideToolTip();
    }


    private Rectangle GetErrorIconBounds(Control control)
    {
        if (_errorProvider == null || control == null || !control.Visible || _containerControl == null)
        {
            return Rectangle.Empty;
        }

        // Get control bounds relative to its parent
        Rectangle controlBounds = control.Bounds;
        ErrorIconAlignment alignment = _errorProvider.GetIconAlignment(control);
        int padding = _errorProvider.GetIconPadding(control);
        int iconWidth = _iconSize.Width;
        int iconHeight = _iconSize.Height;

        int iconX, iconY;

        switch (alignment)
        {
            case ErrorIconAlignment.TopLeft:
                iconX = controlBounds.Left - iconWidth - padding;
                iconY = controlBounds.Top;
                break;
            case ErrorIconAlignment.TopRight:
                iconX = controlBounds.Right + padding;
                iconY = controlBounds.Top;
                break;
            case ErrorIconAlignment.MiddleLeft:
                iconX = controlBounds.Left - iconWidth - padding;
                iconY = controlBounds.Top + (controlBounds.Height - iconHeight) / 2;
                break;
            case ErrorIconAlignment.MiddleRight:
                iconX = controlBounds.Right + padding;
                iconY = controlBounds.Top + (controlBounds.Height - iconHeight) / 2;
                break;
            case ErrorIconAlignment.BottomLeft:
                iconX = controlBounds.Left - iconWidth - padding;
                iconY = controlBounds.Bottom - iconHeight;
                break;
            case ErrorIconAlignment.BottomRight:
                iconX = controlBounds.Right + padding;
                iconY = controlBounds.Bottom - iconHeight;
                break;
            default:
                iconX = controlBounds.Right + padding;
                iconY = controlBounds.Top + (controlBounds.Height - iconHeight) / 2;
                break;
        }

        // Convert icon position from control's parent coordinates to container coordinates
        Point iconPointInParent = new Point(iconX, iconY);
        Point screenPoint = control.Parent?.PointToScreen(iconPointInParent) ?? control.PointToScreen(iconPointInParent);
        Point containerPoint = _containerControl.PointToClient(screenPoint);
        return new Rectangle(containerPoint.X, containerPoint.Y, iconWidth, iconHeight);
    }

    private void StartToolTipTimer(Control control, string errorText, Point mouseLocation)
    {
        HideToolTip();

        _toolTipTimer?.Stop();
        _toolTipTimer?.Dispose();

        _toolTipTimer = new System.Windows.Forms.Timer
        {
            Interval = ToolTipValues.ShowIntervalDelay
        };

        _toolTipTimer.Tick += (sender, e) =>
        {
            _toolTipTimer?.Stop();
            ShowToolTip(control, errorText, mouseLocation);
        };

        _toolTipTimer.Start();
    }

    private void ShowToolTip(Control control, string errorText, Point mouseLocation)
    {
        if (_redirector == null || _renderer == null || _palette == null || _containerControl == null)
        {
            return;
        }

        HideToolTip();

        // Create tooltip content values
        var toolTipContent = new ErrorToolTipContent(errorText);

        // Create tooltip popup
        _visualPopupToolTip = new VisualPopupToolTip(
            _redirector,
            toolTipContent,
            _renderer,
            PaletteBackStyle.ControlToolTip,
            PaletteBorderStyle.ControlToolTip,
            CommonHelper.ContentStyleFromLabelStyle(ToolTipValues.ToolTipStyle),
            ToolTipValues.ToolTipShadow);

        _visualPopupToolTip.Disposed += OnVisualPopupToolTipDisposed;

        // Calculate icon position in screen coordinates
        Rectangle iconBounds = GetErrorIconBounds(control);
        if (!iconBounds.IsEmpty && _containerControl != null)
        {
            Point screenPoint = _containerControl.PointToScreen(new Point(iconBounds.X + _iconSize.Width / 2, iconBounds.Y + _iconSize.Height / 2));
            _visualPopupToolTip.ShowCalculatingSize(screenPoint);
        }
    }

    private void HideToolTip()
    {
        _visualPopupToolTip?.Dispose();
        _visualPopupToolTip = null;
    }

    private void OnVisualPopupToolTipDisposed(object? sender, EventArgs e)
    {
        if (sender is VisualPopupToolTip popupToolTip)
        {
            popupToolTip.Disposed -= OnVisualPopupToolTipDisposed;
        }
    }

    private class ErrorToolTipContent : IContentValues
    {
        private readonly string _errorText;

        public ErrorToolTipContent(string errorText)
        {
            _errorText = errorText;
        }

        public bool HasContent => !string.IsNullOrEmpty(_errorText);

        public Image? GetImage(PaletteState state) => null;

        public Color GetImageTransparentColor(PaletteState state) => Color.Empty;

        public string GetShortText() => _errorText;

        public string GetLongText() => string.Empty;

        public Image? GetOverlayImage(PaletteState state) => null;

        public Color GetOverlayImageTransparentColor(PaletteState state) => Color.Empty;

        public OverlayImagePosition GetOverlayImagePosition(PaletteState state) => OverlayImagePosition.TopRight;

        public OverlayImageScaleMode GetOverlayImageScaleMode(PaletteState state) => OverlayImageScaleMode.None;

        public float GetOverlayImageScaleFactor(PaletteState state) => 0.5f;

        public Size GetOverlayImageFixedSize(PaletteState state) => new Size(16, 16);

        public string GetDescription() => string.Empty;
    }

    #endregion
}