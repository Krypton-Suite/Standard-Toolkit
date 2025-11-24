#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Defines state and events for a single command.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonCommand), "ToolboxBitmaps.KryptonCommand.bmp")]
[DefaultEvent("Click")]
[DefaultProperty(nameof(Text))]
[DesignerCategory(@"code")]
[Designer(typeof(KryptonCommandDesigner))]
[Description(@"Defines state and events for a single command.")]
public class KryptonCommand : Component, IKryptonCommand, INotifyPropertyChanged
{
    #region Instance Fields

    private bool _enabled;
    private bool _checked;
    private ButtonSpec? _assignedButtonSpec;
    private CheckState _checkState;
    private string _text;
    private string _extraText;
    private string _textLine1;
    private string _textLine2;
    private Image? _imageSmall;
    private Image? _imageLarge;
    private Color _imageTransparentColor;
    private KryptonCommandType _commandType;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the command needs executing.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the command needs executing.")]
    public event EventHandler? Execute;

    /// <summary>
    /// Occurs when a property has changed value.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of property has changed.")]
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCommand class.
    /// </summary>
    public KryptonCommand()
    {
        _enabled = true;
        _checked = false;
        _checkState = CheckState.Unchecked;
        _text = string.Empty;
        _extraText = string.Empty;
        _textLine1 = string.Empty;
        _textLine2 = string.Empty;
        _imageSmall = null;
        _imageLarge = null;
        _imageTransparentColor = GlobalStaticValues.EMPTY_COLOR;
        _commandType = KryptonCommandType.General;
        _assignedButtonSpec = null;
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the enabled state of the command.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Indicates whether the command is enabled.")]
    [DefaultValue(true)]
    public bool Enabled
    {
        get => _enabled;

        set
        {
            if (_enabled != value)
            {
                _enabled = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Enabled)));
            }
        }
    }

    /// <summary>
    /// Gets and sets the checked state of the command.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Indicates whether the command is in the checked state.")]
    [DefaultValue(false)]
    public bool Checked
    {
        get => _checked;

        set
        {
            if (_checked != value)
            {
                // Store new values
                _checked = value;
                _checkState = _checked ? CheckState.Checked : CheckState.Unchecked;

                // Generate events
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Checked)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CheckState)));
            }
        }
    }

    [DefaultValue(null)]
    public ButtonSpec? AssignedButtonSpec { get => _assignedButtonSpec; set => _assignedButtonSpec = value; }

    /// <summary>
    /// Gets and sets the check state of the command.
    /// </summary>
    [Bindable(true)]
    [Category(@"Behavior")]
    [Description(@"Indicates the checked state of the command.")]
    //[DefaultValue(CheckState.Unchecked)]
    [DefaultValue(typeof(CheckState), "Unchecked")]

    public CheckState CheckState
    {
        get => _checkState;

        set
        {
            if (_checkState != value)
            {
                // Store new values
                _checkState = value;
                var newChecked = _checkState != CheckState.Unchecked;
                var checkedChanged = _checked != newChecked;
                _checked = newChecked;

                // Generate events
                if (checkedChanged)
                {
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Checked)));
                }

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CheckState)));
            }
        }
    }

    /// <summary>
    /// Gets and sets the command text.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Command text.")]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    public string Text
    {
        get => _text;

        set
        {
            if (_text != value)
            {
                _text = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Text)));
            }
        }
    }

    private void ResetText() => Text = string.Empty;

    private bool ShouldSerializeText() => !string.IsNullOrEmpty(Text);

    /// <summary>
    /// Gets and sets the command extra text.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Command extra text.")]
    [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
    public string ExtraText
    {
        get => _extraText;

        set
        {
            if (_extraText != value)
            {
                _extraText = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ExtraText)));
            }
        }
    }

    private void ResetExtraText() => ExtraText = string.Empty;

    private bool ShouldSerializeExtraText() => !string.IsNullOrEmpty(ExtraText);

    /// <summary>
    /// Gets and sets the command text line 1 for use in KryptonRibbon.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Command text line 1 for use in KryptonRibbon.")]
    public string TextLine1
    {
        get => _textLine1;

        set
        {
            if (_textLine1 != value)
            {
                _textLine1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(TextLine1)));
            }
        }
    }

    private void ResetTextLine1() => TextLine1 = string.Empty;

    private bool ShouldSerializeTextLine1() => !string.IsNullOrEmpty(TextLine1);

    /// <summary>
    /// Gets and sets the command text line 2 for use in KryptonRibbon.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Command text line 2 for use in KryptonRibbon.")]
    public string TextLine2
    {
        get => _textLine2;

        set
        {
            if (_textLine2 != value)
            {
                _textLine2 = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(TextLine2)));
            }
        }
    }

    private void ResetTextLine2() => TextLine2 = string.Empty;

    private bool ShouldSerializeTextLine2() => !string.IsNullOrEmpty(TextLine2);

    /// <summary>
    /// Gets and sets the command small image.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Command small image.")]
    public Image? ImageSmall
    {
        get => _imageSmall;

        set
        {
            if (_imageSmall != value)
            {
                _imageSmall = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ImageSmall)));
            }
        }
    }

    private void ResetImageSmall() => ImageSmall = null;

    private bool ShouldSerializeImageSmall() => ImageSmall != null;

    /// <summary>
    /// Gets and sets the command large image.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Command large image.")]
    public Image? ImageLarge
    {
        get => _imageLarge;

        set
        {
            if (_imageLarge != value)
            {
                _imageLarge = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ImageLarge)));
            }
        }
    }

    private void ResetImageLarge() => ImageLarge = null;

    private bool ShouldSerializeImageLarge() => ImageLarge != null;

    /// <summary>
    /// Gets and sets the command image transparent color.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Command image transparent color.")]
    [KryptonDefaultColor]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ImageTransparentColor
    {
        get => _imageTransparentColor;

        set
        {
            if (_imageTransparentColor != value)
            {
                _imageTransparentColor = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ImageTransparentColor)));
            }
        }
    }

    /// <summary>
    /// Gets and sets user-defined data associated with the object.
    /// </summary>
    [Category(@"Data")]
    [Description(@"User-defined data associated with the object.")]
    [TypeConverter(typeof(StringConverter))]
    [DefaultValue(null)]
    public object? Tag { get; set; }

    /// <summary>Gets or sets the type of the krypton command.</summary>
    /// <value>The type of the krypton command.</value>
    [Category(@"Data")]
    [Description(@"Changes the KryptonCommand, depending on its intended use.")]
    [DefaultValue(KryptonCommandType.General)]
    public KryptonCommandType CommandType
    {
        get => _commandType;

        set
        {
            _commandType = value;

            UpdateCommandType(value);
        }
    }

    /// <summary>
    /// Generates a Execute event for a button.
    /// </summary>
    public void PerformExecute() => OnExecute(EventArgs.Empty);

    // Allow specifying the originating sender so shared commands can identify the source control
    public void PerformExecute(object? sender)
        => Execute?.Invoke(sender ?? this, EventArgs.Empty);

    #endregion

    #region Protected
    /// <summary>
    /// Raises the Execute event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnExecute(EventArgs e) => Execute?.Invoke(this, e);

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="e">A PropertyChangedEventArgs containing the event data.</param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);

    #endregion

    #region Extended Implementation

    private void UpdateCommandType(KryptonCommandType commandType)
    {
        switch (commandType)
        {
            case KryptonCommandType.General:
                break;
            case KryptonCommandType.HelpCommand:
                SwitchToHelpCommand(KryptonManager.CurrentGlobalPaletteMode);
                break;
            case KryptonCommandType.IntegratedToolBarCopyCommand:
                SwitchToCopyCommand(KryptonManager.CurrentGlobalPaletteMode);
                break;
            case KryptonCommandType.IntegratedToolBarCutCommand:
                SwitchToCutCommand(KryptonManager.CurrentGlobalPaletteMode);
                break;
            case KryptonCommandType.IntegratedToolBarNewCommand:
                SwitchToNewCommand(KryptonManager.CurrentGlobalPaletteMode);
                break;
            case KryptonCommandType.IntegratedToolBarOpenCommand:
                SwitchToOpenCommand(KryptonManager.CurrentGlobalPaletteMode);
                break;
            case KryptonCommandType.IntegratedToolBarPageSetupCommand:
                SwitchToPageSetupCommand(KryptonManager.CurrentGlobalPaletteMode);
                break;
            case KryptonCommandType.IntegratedToolBarPasteCommand:
                SwitchToPasteCommand(KryptonManager.CurrentGlobalPaletteMode);
                break;
            case KryptonCommandType.IntegratedToolBarPrintCommand:
                SwitchToPrintCommand(KryptonManager.CurrentGlobalPaletteMode);
                break;
            case KryptonCommandType.IntegratedToolBarPrintPreviewCommand:
                SwitchToPrintPreviewCommand(KryptonManager.CurrentGlobalPaletteMode);
                break;
            case KryptonCommandType.IntegratedToolBarQuickPrintCommand:
                SwitchToQuickPrintCommand(KryptonManager.CurrentGlobalPaletteMode);
                break;
            case KryptonCommandType.IntegratedToolBarRedoCommand:
                SwitchToRedoCommand(KryptonManager.CurrentGlobalPaletteMode);
                break;
            case KryptonCommandType.IntegratedToolBarSaveAllCommand:
                SwitchToSaveAllCommand(KryptonManager.CurrentGlobalPaletteMode);
                break;
            case KryptonCommandType.IntegratedToolBarSaveAsCommand:
                SwitchToSaveAsCommand(KryptonManager.CurrentGlobalPaletteMode);
                break;
            case KryptonCommandType.IntegratedToolBarSaveCommand:
                SwitchToSaveCommand(KryptonManager.CurrentGlobalPaletteMode);
                break;
            case KryptonCommandType.IntegratedToolBarUndoCommand:
                SwitchToUndoCommand(KryptonManager.CurrentGlobalPaletteMode);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(commandType), commandType, null);
        }
    }

    private void SwitchToUndoCommand(PaletteMode paletteMode)
    {
        switch (paletteMode)
        {
            case PaletteMode.Global:
                break;
            case PaletteMode.ProfessionalSystem:
                UpdateImage(SystemToolbarImageResources.SystemToolbarUndoNormal);
                break;
            case PaletteMode.ProfessionalOffice2003:
                UpdateImage(Office2003ToolbarImageResources.Office2003ToolbarUndoNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2007DarkGray:
            case PaletteMode.Office2007Blue:
            case PaletteMode.Office2007BlueDarkMode:
            case PaletteMode.Office2007BlueLightMode:
            case PaletteMode.Office2007Silver:
            case PaletteMode.Office2007SilverDarkMode:
            case PaletteMode.Office2007SilverLightMode:
            case PaletteMode.Office2007White:
            case PaletteMode.Office2007Black:
            case PaletteMode.Office2007BlackDarkMode:
                UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarUndoNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2010DarkGray:
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
            case PaletteMode.SparkleBlue:
            case PaletteMode.SparkleBlueDarkMode:
            case PaletteMode.SparkleBlueLightMode:
            case PaletteMode.SparkleOrange:
            case PaletteMode.SparkleOrangeDarkMode:
            case PaletteMode.SparkleOrangeLightMode:
            case PaletteMode.SparklePurple:
            case PaletteMode.SparklePurpleDarkMode:
            case PaletteMode.SparklePurpleLightMode:
            case PaletteMode.Custom:
                UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarUndoNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2013DarkGray:
            // case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                UpdateImage(Office2013ToolbarImageResources.Office2013ToolbarUndoNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Microsoft365DarkGray:
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
                UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarUndoNormal);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(paletteMode), paletteMode, null);
        }
    }

    private void SwitchToSaveCommand(PaletteMode paletteMode)
    {
        switch (paletteMode)
        {
            case PaletteMode.Global:
                break;
            case PaletteMode.ProfessionalSystem:
                UpdateImage(SystemToolbarImageResources.SystemToolbarSaveNormal);
                break;
            case PaletteMode.ProfessionalOffice2003:
                UpdateImage(Office2003ToolbarImageResources.Office2003ToolbarSaveNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2007DarkGray:
            case PaletteMode.Office2007Blue:
            case PaletteMode.Office2007BlueDarkMode:
            case PaletteMode.Office2007BlueLightMode:
            case PaletteMode.Office2007Silver:
            case PaletteMode.Office2007SilverDarkMode:
            case PaletteMode.Office2007SilverLightMode:
            case PaletteMode.Office2007White:
            case PaletteMode.Office2007Black:
            case PaletteMode.Office2007BlackDarkMode:
                UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarSaveNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2010DarkGray:
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
            case PaletteMode.SparkleBlue:
            case PaletteMode.SparkleBlueDarkMode:
            case PaletteMode.SparkleBlueLightMode:
            case PaletteMode.SparkleOrange:
            case PaletteMode.SparkleOrangeDarkMode:
            case PaletteMode.SparkleOrangeLightMode:
            case PaletteMode.SparklePurple:
            case PaletteMode.SparklePurpleDarkMode:
            case PaletteMode.SparklePurpleLightMode:
            case PaletteMode.Custom:
                UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarSaveNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2013DarkGray:
            // case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                UpdateImage(Office2013ToolbarImageResources.Office2013ToolbarSaveNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Microsoft365DarkGray:
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
                UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarSaveNormal);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(paletteMode), paletteMode, null);
        }
    }

    private void SwitchToSaveAsCommand(PaletteMode paletteMode)
    {
        switch (paletteMode)
        {
            case PaletteMode.Global:
                break;
            case PaletteMode.ProfessionalSystem:
                UpdateImage(SystemToolbarImageResources.SystemToolbarSaveNormal);
                break;
            case PaletteMode.ProfessionalOffice2003:
                UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarSaveAsNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2007DarkGray:
            case PaletteMode.Office2007Blue:
            case PaletteMode.Office2007BlueDarkMode:
            case PaletteMode.Office2007BlueLightMode:
            case PaletteMode.Office2007Silver:
            case PaletteMode.Office2007SilverDarkMode:
            case PaletteMode.Office2007SilverLightMode:
            case PaletteMode.Office2007White:
            case PaletteMode.Office2007Black:
            case PaletteMode.Office2007BlackDarkMode:
                UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarSaveAsNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2010DarkGray:
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
            case PaletteMode.SparkleBlue:
            case PaletteMode.SparkleBlueDarkMode:
            case PaletteMode.SparkleBlueLightMode:
            case PaletteMode.SparkleOrange:
            case PaletteMode.SparkleOrangeDarkMode:
            case PaletteMode.SparkleOrangeLightMode:
            case PaletteMode.SparklePurple:
            case PaletteMode.SparklePurpleDarkMode:
            case PaletteMode.SparklePurpleLightMode:
            case PaletteMode.Custom:
                UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarSaveAsNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2013DarkGray:
            // case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                UpdateImage(Office2013ToolbarImageResources.Office2013ToolbarSaveAsNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Microsoft365DarkGray:
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
                UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarSaveAsNormal);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(paletteMode), paletteMode, null);
        }
    }

    private void SwitchToSaveAllCommand(PaletteMode paletteMode)
    {
        switch (paletteMode)
        {
            case PaletteMode.Global:
                break;
            case PaletteMode.ProfessionalSystem:
                UpdateImage(SystemToolbarImageResources.SystemToolbarSaveAllNormal);
                break;
            case PaletteMode.ProfessionalOffice2003:
                UpdateImage(Office2003ToolbarImageResources.Office2003ToolbarSaveAllNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2007DarkGray:
            case PaletteMode.Office2007Blue:
            case PaletteMode.Office2007BlueDarkMode:
            case PaletteMode.Office2007BlueLightMode:
            case PaletteMode.Office2007Silver:
            case PaletteMode.Office2007SilverDarkMode:
            case PaletteMode.Office2007SilverLightMode:
            case PaletteMode.Office2007White:
            case PaletteMode.Office2007Black:
            case PaletteMode.Office2007BlackDarkMode:
                UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarSaveAllNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2010DarkGray:
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
            case PaletteMode.SparkleBlue:
            case PaletteMode.SparkleBlueDarkMode:
            case PaletteMode.SparkleBlueLightMode:
            case PaletteMode.SparkleOrange:
            case PaletteMode.SparkleOrangeDarkMode:
            case PaletteMode.SparkleOrangeLightMode:
            case PaletteMode.SparklePurple:
            case PaletteMode.SparklePurpleDarkMode:
            case PaletteMode.SparklePurpleLightMode:
            case PaletteMode.Custom:
                UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarSaveAllNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2013DarkGray:
            // case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                UpdateImage(Office2013ToolbarImageResources.Office2013ToolbarSaveAllNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Microsoft365DarkGray:
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
                UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarSaveAllNormal);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(paletteMode), paletteMode, null);
        }
    }

    private void SwitchToRedoCommand(PaletteMode paletteMode)
    {
        switch (paletteMode)
        {
            case PaletteMode.Global:
                break;
            case PaletteMode.ProfessionalSystem:
                UpdateImage(SystemToolbarImageResources.SystemToolbarRedoNormal);
                break;
            case PaletteMode.ProfessionalOffice2003:
                UpdateImage(Office2003ToolbarImageResources.Office2003ToolbarRedoNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2007DarkGray:
            case PaletteMode.Office2007Blue:
            case PaletteMode.Office2007BlueDarkMode:
            case PaletteMode.Office2007BlueLightMode:
            case PaletteMode.Office2007Silver:
            case PaletteMode.Office2007SilverDarkMode:
            case PaletteMode.Office2007SilverLightMode:
            case PaletteMode.Office2007White:
            case PaletteMode.Office2007Black:
            case PaletteMode.Office2007BlackDarkMode:
                UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarRedoNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2010DarkGray:
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
            case PaletteMode.SparkleBlue:
            case PaletteMode.SparkleBlueDarkMode:
            case PaletteMode.SparkleBlueLightMode:
            case PaletteMode.SparkleOrange:
            case PaletteMode.SparkleOrangeDarkMode:
            case PaletteMode.SparkleOrangeLightMode:
            case PaletteMode.SparklePurple:
            case PaletteMode.SparklePurpleDarkMode:
            case PaletteMode.SparklePurpleLightMode:
            case PaletteMode.Custom:
                UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarRedoNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2013DarkGray:
            // case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                UpdateImage(Office2013ToolbarImageResources.Office2013ToolbarRedoNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Microsoft365DarkGray:
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
                UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarRedoNormal);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(paletteMode), paletteMode, null);
        }
    }

    private void SwitchToQuickPrintCommand(PaletteMode paletteMode)
    {
        switch (paletteMode)
        {
            case PaletteMode.Global:
                break;
            case PaletteMode.ProfessionalSystem:
            case PaletteMode.ProfessionalOffice2003:
                UpdateImage(GenericToolbarImageResources.GenericQuickPrint);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2007DarkGray:
            case PaletteMode.Office2007Blue:
            case PaletteMode.Office2007BlueDarkMode:
            case PaletteMode.Office2007BlueLightMode:
            case PaletteMode.Office2007Silver:
            case PaletteMode.Office2007SilverDarkMode:
            case PaletteMode.Office2007SilverLightMode:
            case PaletteMode.Office2007White:
            case PaletteMode.Office2007Black:
            case PaletteMode.Office2007BlackDarkMode:
                UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2010DarkGray:
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
            case PaletteMode.SparkleBlue:
            case PaletteMode.SparkleBlueDarkMode:
            case PaletteMode.SparkleBlueLightMode:
            case PaletteMode.SparkleOrange:
            case PaletteMode.SparkleOrangeDarkMode:
            case PaletteMode.SparkleOrangeLightMode:
            case PaletteMode.SparklePurple:
            case PaletteMode.SparklePurpleDarkMode:
            case PaletteMode.SparklePurpleLightMode:
            case PaletteMode.Custom:
                UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2013DarkGray:
            // case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                UpdateImage(Office2013ToolbarImageResources.Office2013ToolbarQuickPrintNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Microsoft365DarkGray:
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
                UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(paletteMode), paletteMode, null);
        }
    }

    private void SwitchToPrintPreviewCommand(PaletteMode paletteMode)
    {
        switch (paletteMode)
        {
            case PaletteMode.Global:
                break;
            case PaletteMode.ProfessionalSystem:
                UpdateImage(SystemToolbarImageResources.SystemToolbarPrintPreviewNormal);
                break;
            case PaletteMode.ProfessionalOffice2003:
                UpdateImage(Office2003ToolbarImageResources.Office2003ToolbarPrintPreviewNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2007DarkGray:
            case PaletteMode.Office2007Blue:
            case PaletteMode.Office2007BlueDarkMode:
            case PaletteMode.Office2007BlueLightMode:
            case PaletteMode.Office2007Silver:
            case PaletteMode.Office2007SilverDarkMode:
            case PaletteMode.Office2007SilverLightMode:
            case PaletteMode.Office2007White:
            case PaletteMode.Office2007Black:
            case PaletteMode.Office2007BlackDarkMode:
                UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarPrintPreviewNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2010DarkGray:
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
            case PaletteMode.SparkleBlue:
            case PaletteMode.SparkleBlueDarkMode:
            case PaletteMode.SparkleBlueLightMode:
            case PaletteMode.SparkleOrange:
            case PaletteMode.SparkleOrangeDarkMode:
            case PaletteMode.SparkleOrangeLightMode:
            case PaletteMode.SparklePurple:
            case PaletteMode.SparklePurpleDarkMode:
            case PaletteMode.SparklePurpleLightMode:
            case PaletteMode.Custom:
                UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarPrintPreviewNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2013DarkGray:
            // case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                UpdateImage(Office2013ToolbarImageResources.Office2013ToolbarPrintPreviewNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Microsoft365DarkGray:
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
                UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarPrintPreviewNormal);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(paletteMode), paletteMode, null);
        }
    }

    private void SwitchToPrintCommand(PaletteMode paletteMode)
    {
        switch (paletteMode)
        {
            case PaletteMode.Global:
                break;
            case PaletteMode.ProfessionalSystem:
                UpdateImage(SystemToolbarImageResources.SystemToolbarPrintNormal);
                break;
            case PaletteMode.ProfessionalOffice2003:
                UpdateImage(Office2003ToolbarImageResources.Office2003ToolbarPrintNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2007DarkGray:
            case PaletteMode.Office2007Blue:
            case PaletteMode.Office2007BlueDarkMode:
            case PaletteMode.Office2007BlueLightMode:
            case PaletteMode.Office2007Silver:
            case PaletteMode.Office2007SilverDarkMode:
            case PaletteMode.Office2007SilverLightMode:
            case PaletteMode.Office2007White:
            case PaletteMode.Office2007Black:
            case PaletteMode.Office2007BlackDarkMode:
                UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarPrintNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2010DarkGray:
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
            case PaletteMode.SparkleBlue:
            case PaletteMode.SparkleBlueDarkMode:
            case PaletteMode.SparkleBlueLightMode:
            case PaletteMode.SparkleOrange:
            case PaletteMode.SparkleOrangeDarkMode:
            case PaletteMode.SparkleOrangeLightMode:
            case PaletteMode.SparklePurple:
            case PaletteMode.SparklePurpleDarkMode:
            case PaletteMode.SparklePurpleLightMode:
            case PaletteMode.Custom:
                UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarPrintNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2013DarkGray:
            // case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                UpdateImage(Office2013ToolbarImageResources.Office2013ToolbarPrintNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Microsoft365DarkGray:
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
                UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarPrintNormal);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(paletteMode), paletteMode, null);
        }
    }

    private void SwitchToPasteCommand(PaletteMode paletteMode)
    {
        switch (paletteMode)
        {
            case PaletteMode.Global:
                break;
            case PaletteMode.ProfessionalSystem:
                UpdateImage(SystemToolbarImageResources.SystemToolbarPasteNormal);
                break;
            case PaletteMode.ProfessionalOffice2003:
                UpdateImage(Office2003ToolbarImageResources.Office2003ToolbarPasteNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2007DarkGray:
            case PaletteMode.Office2007Blue:
            case PaletteMode.Office2007BlueDarkMode:
            case PaletteMode.Office2007BlueLightMode:
            case PaletteMode.Office2007Silver:
            case PaletteMode.Office2007SilverDarkMode:
            case PaletteMode.Office2007SilverLightMode:
            case PaletteMode.Office2007White:
            case PaletteMode.Office2007Black:
            case PaletteMode.Office2007BlackDarkMode:
                UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarPasteNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2010DarkGray:
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
            case PaletteMode.SparkleBlue:
            case PaletteMode.SparkleBlueDarkMode:
            case PaletteMode.SparkleBlueLightMode:
            case PaletteMode.SparkleOrange:
            case PaletteMode.SparkleOrangeDarkMode:
            case PaletteMode.SparkleOrangeLightMode:
            case PaletteMode.SparklePurple:
            case PaletteMode.SparklePurpleDarkMode:
            case PaletteMode.SparklePurpleLightMode:
            case PaletteMode.Custom:
                UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarPasteNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2013DarkGray:
            // case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                UpdateImage(Office2013ToolbarImageResources.Office2013ToolbarPasteNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Microsoft365DarkGray:
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
                UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarPasteNormal);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(paletteMode), paletteMode, null);
        }
    }

    private void SwitchToPageSetupCommand(PaletteMode paletteMode)
    {
        switch (paletteMode)
        {
            case PaletteMode.Global:
                break;
            case PaletteMode.ProfessionalSystem:
                UpdateImage(SystemToolbarImageResources.SystemToolbarPageSetupNormal);
                break;
            case PaletteMode.ProfessionalOffice2003:
                UpdateImage(Office2003ToolbarImageResources.Office2003ToolbarPageSetupNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2007DarkGray:
            case PaletteMode.Office2007Blue:
            case PaletteMode.Office2007BlueDarkMode:
            case PaletteMode.Office2007BlueLightMode:
            case PaletteMode.Office2007Silver:
            case PaletteMode.Office2007SilverDarkMode:
            case PaletteMode.Office2007SilverLightMode:
            case PaletteMode.Office2007White:
            case PaletteMode.Office2007Black:
            case PaletteMode.Office2007BlackDarkMode:
                UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarPageSetupNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2010DarkGray:
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
            case PaletteMode.SparkleBlue:
            case PaletteMode.SparkleBlueDarkMode:
            case PaletteMode.SparkleBlueLightMode:
            case PaletteMode.SparkleOrange:
            case PaletteMode.SparkleOrangeDarkMode:
            case PaletteMode.SparkleOrangeLightMode:
            case PaletteMode.SparklePurple:
            case PaletteMode.SparklePurpleDarkMode:
            case PaletteMode.SparklePurpleLightMode:
            case PaletteMode.Custom:
                UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarPageSetupNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2013DarkGray:
            // case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                UpdateImage(Office2013ToolbarImageResources.Office2013ToolbarPageSetupNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Microsoft365DarkGray:
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
                UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarPageSetupNormal);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(paletteMode), paletteMode, null);
        }
    }

    private void SwitchToOpenCommand(PaletteMode paletteMode)
    {
        switch (paletteMode)
        {
            case PaletteMode.Global:
                break;
            case PaletteMode.ProfessionalSystem:
                UpdateImage(SystemToolbarImageResources.SystemToolbarOpenNormal);
                break;
            case PaletteMode.ProfessionalOffice2003:
                UpdateImage(Office2003ToolbarImageResources.Office2003ToolbarOpenNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2007DarkGray:
            case PaletteMode.Office2007Blue:
            case PaletteMode.Office2007BlueDarkMode:
            case PaletteMode.Office2007BlueLightMode:
            case PaletteMode.Office2007Silver:
            case PaletteMode.Office2007SilverDarkMode:
            case PaletteMode.Office2007SilverLightMode:
            case PaletteMode.Office2007White:
            case PaletteMode.Office2007Black:
            case PaletteMode.Office2007BlackDarkMode:
                UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarOpenNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2010DarkGray:
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
            case PaletteMode.SparkleBlue:
            case PaletteMode.SparkleBlueDarkMode:
            case PaletteMode.SparkleBlueLightMode:
            case PaletteMode.SparkleOrange:
            case PaletteMode.SparkleOrangeDarkMode:
            case PaletteMode.SparkleOrangeLightMode:
            case PaletteMode.SparklePurple:
            case PaletteMode.SparklePurpleDarkMode:
            case PaletteMode.SparklePurpleLightMode:
            case PaletteMode.Custom:
                UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarOpenNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2013DarkGray:
            // case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                UpdateImage(Office2013ToolbarImageResources.Office2013ToolbarOpenNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Microsoft365DarkGray:
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
                UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarOpenNormal);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(paletteMode), paletteMode, null);
        }
    }

    private void SwitchToNewCommand(PaletteMode paletteMode)
    {
        switch (paletteMode)
        {
            case PaletteMode.Global:
                break;
            case PaletteMode.ProfessionalSystem:
                UpdateImage(SystemToolbarImageResources.SystemToolbarNewNormal);
                break;
            case PaletteMode.ProfessionalOffice2003:
                UpdateImage(Office2003ToolbarImageResources.Office2003ToolbarNewNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2007DarkGray:
            case PaletteMode.Office2007Blue:
            case PaletteMode.Office2007BlueDarkMode:
            case PaletteMode.Office2007BlueLightMode:
            case PaletteMode.Office2007Silver:
            case PaletteMode.Office2007SilverDarkMode:
            case PaletteMode.Office2007SilverLightMode:
            case PaletteMode.Office2007White:
            case PaletteMode.Office2007Black:
            case PaletteMode.Office2007BlackDarkMode:
                UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarNewNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2010DarkGray:
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
            case PaletteMode.SparkleBlue:
            case PaletteMode.SparkleBlueDarkMode:
            case PaletteMode.SparkleBlueLightMode:
            case PaletteMode.SparkleOrange:
            case PaletteMode.SparkleOrangeDarkMode:
            case PaletteMode.SparkleOrangeLightMode:
            case PaletteMode.SparklePurple:
            case PaletteMode.SparklePurpleDarkMode:
            case PaletteMode.SparklePurpleLightMode:
            case PaletteMode.Custom:
                UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarNewNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2013DarkGray:
            // case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                UpdateImage(Office2013ToolbarImageResources.Office2013ToolbarNewNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Microsoft365DarkGray:
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
                UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarNewNormal);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(paletteMode), paletteMode, null);
        }
    }

    private void SwitchToCutCommand(PaletteMode paletteMode)
    {
        switch (paletteMode)
        {
            case PaletteMode.Global:
                break;
            case PaletteMode.ProfessionalSystem:
                UpdateImage(SystemToolbarImageResources.SystemToolbarCutNormal);
                break;
            case PaletteMode.ProfessionalOffice2003:
                UpdateImage(Office2003ToolbarImageResources.Office2003ToolbarCutNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2007DarkGray:
            case PaletteMode.Office2007Blue:
            case PaletteMode.Office2007BlueDarkMode:
            case PaletteMode.Office2007BlueLightMode:
            case PaletteMode.Office2007Silver:
            case PaletteMode.Office2007SilverDarkMode:
            case PaletteMode.Office2007SilverLightMode:
            case PaletteMode.Office2007White:
            case PaletteMode.Office2007Black:
            case PaletteMode.Office2007BlackDarkMode:
                UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarCutNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2010DarkGray:
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
            case PaletteMode.SparkleBlue:
            case PaletteMode.SparkleBlueDarkMode:
            case PaletteMode.SparkleBlueLightMode:
            case PaletteMode.SparkleOrange:
            case PaletteMode.SparkleOrangeDarkMode:
            case PaletteMode.SparkleOrangeLightMode:
            case PaletteMode.SparklePurple:
            case PaletteMode.SparklePurpleDarkMode:
            case PaletteMode.SparklePurpleLightMode:
            case PaletteMode.Custom:
                UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarCutNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2013DarkGray:
            // case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                UpdateImage(Office2013ToolbarImageResources.Office2013ToolbarCutNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Microsoft365DarkGray:
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
                UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarCutNormal);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(paletteMode), paletteMode, null);
        }
    }

    private void SwitchToCopyCommand(PaletteMode paletteMode)
    {
        switch (paletteMode)
        {
            case PaletteMode.Global:
                break;
            case PaletteMode.ProfessionalSystem:
                UpdateImage(SystemToolbarImageResources.SystemToolbarCopyNormal);
                break;
            case PaletteMode.ProfessionalOffice2003:
                UpdateImage(Office2003ToolbarImageResources.Office2003ToolbarCopyNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2007DarkGray:
            case PaletteMode.Office2007Blue:
            case PaletteMode.Office2007BlueDarkMode:
            case PaletteMode.Office2007BlueLightMode:
            case PaletteMode.Office2007Silver:
            case PaletteMode.Office2007SilverDarkMode:
            case PaletteMode.Office2007SilverLightMode:
            case PaletteMode.Office2007White:
            case PaletteMode.Office2007Black:
            case PaletteMode.Office2007BlackDarkMode:
                UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarCopyNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2010DarkGray:
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
            case PaletteMode.SparkleBlue:
            case PaletteMode.SparkleBlueDarkMode:
            case PaletteMode.SparkleBlueLightMode:
            case PaletteMode.SparkleOrange:
            case PaletteMode.SparkleOrangeDarkMode:
            case PaletteMode.SparkleOrangeLightMode:
            case PaletteMode.SparklePurple:
            case PaletteMode.SparklePurpleDarkMode:
            case PaletteMode.SparklePurpleLightMode:
            case PaletteMode.Custom:
                UpdateImage(Office2010ToolbarImageResources.Office2010ToolbarCopyNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2013DarkGray:
            // case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                UpdateImage(Office2013ToolbarImageResources.Office2013ToolbarCopyNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Microsoft365DarkGray:
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
                UpdateImage(Office2019ToolbarImageResources.Office2019ToolbarCopyNormal);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(paletteMode), paletteMode, null);
        }
    }

    private void SwitchToHelpCommand(PaletteMode paletteMode)
    {
        switch (paletteMode)
        {
            case PaletteMode.Global:
                break;
            case PaletteMode.ProfessionalSystem:
                UpdateImage(ProfessionalControlBoxResources.ProfessionalHelpIconNormal);
                break;
            case PaletteMode.ProfessionalOffice2003:
                UpdateImage(Office2003ControlBoxResources.Office2003HelpIconNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2007DarkGray:
            case PaletteMode.Office2007Blue:
            case PaletteMode.Office2007BlueDarkMode:
            case PaletteMode.Office2007BlueLightMode:
            case PaletteMode.Office2007Silver:
            case PaletteMode.Office2007SilverDarkMode:
            case PaletteMode.Office2007SilverLightMode:
            case PaletteMode.Office2007White:
            case PaletteMode.Office2007Black:
            case PaletteMode.Office2007BlackDarkMode:
                UpdateImage(Office2007ToolbarImageResources.Office2007ToolbarHelpNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2010DarkGray:
            case PaletteMode.Office2010Blue:
            case PaletteMode.Office2010BlueDarkMode:
            case PaletteMode.Office2010BlueLightMode:
            case PaletteMode.Office2010Silver:
            case PaletteMode.Office2010SilverDarkMode:
            case PaletteMode.Office2010SilverLightMode:
            case PaletteMode.Office2010White:
            case PaletteMode.Office2010Black:
            case PaletteMode.Office2010BlackDarkMode:
            case PaletteMode.SparkleBlue:
            case PaletteMode.SparkleBlueDarkMode:
            case PaletteMode.SparkleBlueLightMode:
            case PaletteMode.SparkleOrange:
            case PaletteMode.SparkleOrangeDarkMode:
            case PaletteMode.SparkleOrangeLightMode:
            case PaletteMode.SparklePurple:
            case PaletteMode.SparklePurpleDarkMode:
            case PaletteMode.SparklePurpleLightMode:
            case PaletteMode.Custom:
                UpdateImage(Office2010ControlBoxResources.Office2010HelpIconNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Office2013DarkGray:
            // case PaletteMode.Office2013LightGray:
            case PaletteMode.Office2013White:
                UpdateImage(Office2013ControlBoxResources.Office2013HelpNormal);
                break;
            // TODO: Re-enable this once completed
            // case PaletteMode.Microsoft365DarkGray:
            case PaletteMode.Microsoft365Black:
            case PaletteMode.Microsoft365BlackDarkMode:
            case PaletteMode.Microsoft365Blue:
            case PaletteMode.Microsoft365BlueDarkMode:
            case PaletteMode.Microsoft365BlueLightMode:
            case PaletteMode.Microsoft365Silver:
            case PaletteMode.Microsoft365SilverDarkMode:
            case PaletteMode.Microsoft365SilverLightMode:
            case PaletteMode.Microsoft365White:
                UpdateImage(Microsoft365ControlBoxResources.Microsoft365HelpIconNormal);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(paletteMode), paletteMode, null);
        }
    }

    /// <summary>Updates the image.</summary>
    /// <param name="helpImage">The help image.</param>
    private void UpdateImage(Image? helpImage) => ImageSmall = helpImage;

    /// <summary>Sets the text.</summary>
    /// <param name="value">The value.</param>
    private void SetText(string value) => Text = value;

    #endregion
}

/// <summary>
/// Manages a collection of KryptonCommand instances.
/// </summary>
public class KryptonCommandCollection : TypedCollection<KryptonCommand>
{
    #region Public
    /// <summary>
    /// Gets the item with the provided name.
    /// </summary>
    /// <param name="name">Name to find.</param>
    /// <returns>Item with matching name.</returns>
    public override KryptonCommand? this[string name]
    {
        get
        {
            if (!string.IsNullOrEmpty(name))
            {
                foreach (KryptonCommand item in this)
                {
                    var text = item.Text;
                    if (!string.IsNullOrEmpty(text) && (text == name))
                    {
                        return item;
                    }

                    text = item.ExtraText;
                    if (!string.IsNullOrEmpty(text) && (text == name))
                    {
                        return item;
                    }
                }
            }

            return null;
        }
    }
    #endregion
}