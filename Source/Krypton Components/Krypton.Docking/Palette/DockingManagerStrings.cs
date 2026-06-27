#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Docking;

/// <summary>
/// Localized UI strings for docking context menus, tooltips, and related visuals.
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
    /// Raised after a string property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initializes localized string defaults for the owning docking manager.
    /// </summary>
    /// <param name="docking">Owning docking manager.</param>
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
    /// Indicates whether every localized string still matches its built-in default.
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
    /// Localized tooltip text for the auto-hide button.
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
    /// Restores the built-in default auto-hide tooltip text.
    /// </summary>
    public void ResetTextAutoHide() => TextAutoHide = DEFAULT_TEXT_AUTO_HIDE;
    #endregion

    #region TextClose
    /// <summary>
    /// Localized tooltip text for the close button.
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
    /// Restores the built-in default close tooltip text.
    /// </summary>
    public void ResetTextClose() => TextClose = DEFAULT_TEXT_CLOSE;
    #endregion

    #region TextCloseAllButThis
    /// <summary>
    /// Localized tooltip text for the close-all-but-this button.
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
    /// Restores the built-in default close-all-but-this tooltip text.
    /// </summary>
    public void ResetTextCloseAllButThis() => TextCloseAllButThis = DEFAULT_TEXT_CLOSE_ALL_BUT_THIS;
    #endregion

    #region TextDock
    /// <summary>
    /// Localized caption for the dock context-menu item.
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
    /// Restores the built-in default dock menu caption.
    /// </summary>
    public void ResetTextDock() => TextDock = DEFAULT_TEXT_DOCK;
    #endregion

    #region TextFloat
    /// <summary>
    /// Localized caption for the float context-menu item.
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
    /// Restores the built-in default float menu caption.
    /// </summary>
    public void ResetTextFloat() => TextFloat = DEFAULT_TEXT_DOCK;
    #endregion

    #region TextHide
    /// <summary>
    /// Localized caption for the hide context-menu item.
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
    /// Restores the built-in default hide menu caption.
    /// </summary>
    public void ResetTextHide() => TextHide = DEFAULT_TEXT_DOCK;

    #endregion

    #region TextTabbedDocument
    /// <summary>
    /// Localized caption for the tabbed-document context-menu item.
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
    /// Restores the built-in default tabbed-document menu caption.
    /// </summary>
    public void ResetTextTabbedDocument() => TextTabbedDocument = DEFAULT_TEXT_TABBED_DOCUMENT;

    #endregion

    #region TextWindowLocation
    /// <summary>
    /// Localized tooltip text for the window-position drop-down button.
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
    /// Restores the built-in default window-position tooltip text.
    /// </summary>
    public void ResetTextWindowLocation() => TextWindowLocation = DEFAULT_TEXT_WINDOW_LOCATION;
    #endregion

    #region Protected
    /// <summary>
    /// Raises <see cref="PropertyChanged"/> for <paramref name="propertyName"/>.
    /// </summary>
    /// <param name="propertyName">Name of the property whose value changed.</param>
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}
