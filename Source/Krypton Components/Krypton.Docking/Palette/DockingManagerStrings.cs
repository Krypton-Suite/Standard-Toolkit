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

namespace Krypton.Docking;

/// <summary>
/// Storage for docking manager strings.
/// </summary>
public class DockingManagerStrings : Storage
{
    #region Static Fields

    private const string DEFAULT_TEXT_AUTO_HIDE = "Auto Hide";
    private const string DEFAULT_TEXT_CLOSE = "Close";
    private const string DEFAULT_TEXT_CLOSE_ALL_BUT_THIS = "Close All But This";
    private const string DEFAULT_TEXT_DOCK = "Dock";
    private const string DEFAULT_TEXT_FLOAT = "Float";
    private const string DEFAULT_TEXT_HIDE = "Hide";
    private const string DEFAULT_TEXT_TABBED_DOCUMENT = "Tabbed Document";
    private const string DEFAULT_TEXT_WINDOW_LOCATION = "Window Position";

    #endregion

    #region Instance Fields
    private string _textAutoHide;
    private string _textClose;
    private string _textCloseAllButThis;
    private string _textDock;
    private string _textFloat;
    private string _textHide;
    private string _textTabbedDocument;
    private string _textWindowLocation;
    #endregion

    #region Events
    /// <summary>
    /// Occurs whenever a property has changed value.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the DockingManagerStrings class.
    /// </summary>
    /// <param name="docking">Reference to owning docking manager.</param>
    public DockingManagerStrings(KryptonDockingManager docking)
    {
        // Default values
        _textAutoHide = DEFAULT_TEXT_AUTO_HIDE;
        _textClose = DEFAULT_TEXT_CLOSE;
        _textCloseAllButThis = DEFAULT_TEXT_CLOSE_ALL_BUT_THIS;
        _textDock = DEFAULT_TEXT_DOCK;
        _textFloat = DEFAULT_TEXT_FLOAT;
        _textHide = DEFAULT_TEXT_HIDE;
        _textTabbedDocument = DEFAULT_TEXT_TABBED_DOCUMENT;
        _textWindowLocation = DEFAULT_TEXT_WINDOW_LOCATION;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (_textAutoHide.Equals(DEFAULT_TEXT_AUTO_HIDE) &&
                                       _textClose.Equals(DEFAULT_TEXT_CLOSE) &&
                                       _textDock.Equals(DEFAULT_TEXT_DOCK) &&
                                       _textFloat.Equals(DEFAULT_TEXT_FLOAT) &&
                                       _textHide.Equals(DEFAULT_TEXT_HIDE) &&
                                       _textTabbedDocument.Equals(DEFAULT_TEXT_TABBED_DOCUMENT) &&
                                       _textWindowLocation.Equals(DEFAULT_TEXT_WINDOW_LOCATION));

    #endregion

    #region TextAutoHide
    /// <summary>
    /// Gets and sets the text to use for the auto hide button tooltip.
    /// </summary>
    [Category("Visuals")]
    [Description("Text to use for the auto hide button tooltip.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("Auto Hide")]
    [Localizable(true)]
    public string TextAutoHide
    {
        get => _textAutoHide;

        set 
        {
            if (_textAutoHide != value)
            {
                _textAutoHide = value;
                OnPropertyChanged(nameof(TextAutoHide));
            }
        }
    }

    /// <summary>
    /// Resets the TextAutoHide property to its default value.
    /// </summary>
    public void ResetTextAutoHide() => TextAutoHide = DEFAULT_TEXT_AUTO_HIDE;
    #endregion

    #region TextClose
    /// <summary>
    /// Gets and sets the text to use for the close button tooltip.
    /// </summary>
    [Category("Visuals")]
    [Description("Text to use for the close button tooltip.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("Close")]
    [Localizable(true)]
    public string TextClose
    {
        get => _textClose;

        set 
        {
            if (_textClose != value)
            {
                _textClose = value;
                OnPropertyChanged(nameof(TextClose));
            }
        }
    }

    /// <summary>
    /// Resets the TextClose property to its default value.
    /// </summary>
    public void ResetTextClose() => TextClose = DEFAULT_TEXT_CLOSE;
    #endregion

    #region TextCloseAllButThis
    /// <summary>
    /// Gets and sets the text to use for the 'close all but this' button tooltip.
    /// </summary>
    [Category("Visuals")]
    [Description("Text to use for the 'close all but this' button tooltip.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("Close All But This")]
    [Localizable(true)]
    public string TextCloseAllButThis
    {
        get => _textCloseAllButThis;

        set
        {
            if (_textCloseAllButThis != value)
            {
                _textCloseAllButThis = value;
                OnPropertyChanged(nameof(TextCloseAllButThis));
            }
        }
    }

    /// <summary>
    /// Resets the TextCloseAllButThis property to its default value.
    /// </summary>
    public void ResetTextCloseAllButThis() => TextCloseAllButThis = DEFAULT_TEXT_CLOSE_ALL_BUT_THIS;
    #endregion

    #region TextDock
    /// <summary>
    /// Gets and sets the text to use for the dock menu item.
    /// </summary>
    [Category("Visuals")]
    [Description("Text to use for the dock menu item.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("Dock")]
    [Localizable(true)]
    public string TextDock
    {
        get => _textDock;

        set
        {
            if (_textDock != value)
            {
                _textDock = value;
                OnPropertyChanged(nameof(TextDock));
            }
        }
    }

    /// <summary>
    /// Resets the TextDock property to its default value.
    /// </summary>
    public void ResetTextDock() => TextDock = DEFAULT_TEXT_DOCK;
    #endregion

    #region TextFloat
    /// <summary>
    /// Gets and sets the text to use for the float menu item.
    /// </summary>
    [Category("Visuals")]
    [Description("Text to use for the float menu item.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("Float")]
    [Localizable(true)]
    public string TextFloat
    {
        get => _textFloat;

        set
        {
            if (_textFloat != value)
            {
                _textFloat = value;
                OnPropertyChanged(nameof(TextFloat));
            }
        }
    }

    /// <summary>
    /// Resets the TextFloat property to its default value.
    /// </summary>
    public void ResetTextFloat() => TextFloat = DEFAULT_TEXT_DOCK;
    #endregion

    #region TextHide
    /// <summary>
    /// Gets and sets the text to use for the hide menu item.
    /// </summary>
    [Category("Visuals")]
    [Description("Text to use for the hide menu item.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("Hide")]
    [Localizable(true)]
    public string TextHide
    {
        get => _textHide;

        set
        {
            if (_textHide != value)
            {
                _textHide = value;
                OnPropertyChanged(nameof(TextHide));
            }
        }
    }

    /// <summary>
    /// Resets the TextHide property to its default value.
    /// </summary>
    public void ResetTextHide() => TextHide = DEFAULT_TEXT_DOCK;

    #endregion

    #region TextTabbedDocument
    /// <summary>
    /// Gets and sets the text to use for the tabbed document menu item.
    /// </summary>
    [Category("Visuals")]
    [Description("Text to use for the tabbed document menu item.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("Tabbed Document")]
    [Localizable(true)]
    public string TextTabbedDocument
    {
        get => _textTabbedDocument;

        set
        {
            if (_textTabbedDocument != value)
            {
                _textTabbedDocument = value;
                OnPropertyChanged(nameof(TextTabbedDocument));
            }
        }
    }

    /// <summary>
    /// Resets the TextTabbedDocument property to its default value.
    /// </summary>
    public void ResetTextTabbedDocument() => TextTabbedDocument = DEFAULT_TEXT_TABBED_DOCUMENT;

    #endregion

    #region TextWindowLocation
    /// <summary>
    /// Gets and sets the text to use for the drop-down button tooltip.
    /// </summary>
    [Category("Visuals")]
    [Description("Text to use for the drop-down button tooltip.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("Window Position")]
    [Localizable(true)]
    public string TextWindowLocation
    {
        get => _textWindowLocation;

        set
        {
            if (_textWindowLocation != value)
            {
                _textWindowLocation = value;
                OnPropertyChanged(nameof(TextWindowLocation));
            }
        }
    }

    /// <summary>
    /// Resets the TextWindowLocation property to its default value.
    /// </summary>
    public void ResetTextWindowLocation() => TextWindowLocation = DEFAULT_TEXT_WINDOW_LOCATION;
    #endregion

    #region Protected
    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">Name of the property that has changed.</param>
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}