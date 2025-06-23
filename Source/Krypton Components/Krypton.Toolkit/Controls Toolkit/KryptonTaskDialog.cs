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
/// Represents a task dialog for presenting different options to the user.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonTaskDialog), "ToolboxBitmaps.KryptonTaskDialog.bmp")]
[DefaultEvent(nameof(PropertyChanged))]
[DesignerCategory(@"code")]
[Description(@"Displays a task dialog for presenting different options to the user.")]
public class KryptonTaskDialog : Component, INotifyPropertyChanged
{
    #region Instance Fields
    private VisualTaskDialogForm? _taskDialog;
    private string _windowTitle;
    private string _mainInstruction;
    private string _content;
    private Image? _customIcon;
    private KryptonMessageBoxIcon _icon;
    private KryptonTaskDialogCommand? _defaultRadioButton;
    private TaskDialogButtons _commonButtons;
    private TaskDialogButtons _defaultButton;
    private KryptonMessageBoxIcon _footerIcon;
    private Image? _customFooterIcon;
    private string _footerText;
    private string _footerHyperlink;
    private string _checkboxText;
    private bool _checkboxState;
    private bool _allowDialogClose;
    private bool _useNativeOSIcons;
    private string _textExtra;

    #endregion

    #region Events
    /// <summary>
    /// Occurs when the users clicks the footer hyperlink.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the users clicks the footer hyperlink.")]
    public event EventHandler? FooterHyperlinkClicked;

    /// <summary>
    /// Occurs when a property has changed value.
    /// </summary>
    [Category(@"Property Changed")]
    [Description(@"Occurs when the value of property has changed.")]
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion

    #region Identity
    /// <summary>
    ///  Initialize a new instance of the KryptonTaskDialog class.
    /// </summary>
    public KryptonTaskDialog()
    {
        RadioButtons = [];
        CommandButtons = [];
        _commonButtons = TaskDialogButtons.OK;
        _textExtra = @"Ctrl+C to copy";
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_taskDialog != null)
            {
                _taskDialog.Dispose();
                _taskDialog = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the caption of the window.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Caption of the window.")]
    [DefaultValue("")]
    [Localizable(true)]
    [Bindable(true)]
    public string WindowTitle
    {
        get => _windowTitle;

        set
        {
            if (_windowTitle != value)
            {
                _windowTitle = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(WindowTitle)));
            }
        }
    }

    /// <summary>
    /// Gets and sets the principal text.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Principal text.")]
    [DefaultValue("")]
    [Localizable(true)]
    [Bindable(true)]
    public string MainInstruction
    {
        get => _mainInstruction;

        set
        {
            if (_mainInstruction != value)
            {
                _mainInstruction = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(MainInstruction)));
            }
        }
    }

    /// <summary>
    /// Gets and sets the extra text.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Extra text.")]
    [DefaultValue("")]
    [Localizable(true)]
    [Bindable(true)]
    public string Content
    {
        get => _content;

        set
        {
            if (_content != value)
            {
                _content = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Content)));
            }
        }
    }

    /// <summary>
    /// Gets and sets the predefined icon.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Predefined icon.")]
    [DefaultValue(KryptonMessageBoxIcon.None)]
    public KryptonMessageBoxIcon Icon
    {
        get => _icon;

        set
        {
            if (_icon != value)
            {
                _icon = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Icon)));
            }
        }
    }

    /// <summary>
    /// Gets and sets the custom icon.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Custom icon.")]
    [DefaultValue(null)]
    public Image? CustomIcon
    {
        get => _customIcon;

        set
        {
            if (_customIcon != value)
            {
                _customIcon = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CustomIcon)));
            }
        }
    }

    /// <summary>
    /// Gets access to the collection of radio button definitions.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Collection of radio button definitions.")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [RefreshProperties(RefreshProperties.All)]
    [Browsable(true)]
    public KryptonTaskDialogCommandCollection RadioButtons { get; }

    /// <summary>
    /// Gets access to the collection of command button definitions.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Collection of command button definitions.")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [RefreshProperties(RefreshProperties.All)]
    [Browsable(true)]
    public KryptonTaskDialogCommandCollection CommandButtons { get; }

    /// <summary>
    /// Gets and sets the common dialog buttons.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Common dialog buttons.")]
    [DefaultValue(TaskDialogButtons.OK)]
    public TaskDialogButtons CommonButtons
    {
        get => _commonButtons;

        set
        {
            if (_commonButtons != value)
            {
                _commonButtons = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CommonButtons)));
            }
        }
    }

    /// <summary>
    /// Gets and sets the default radio button.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Default radio button.")]
    [DefaultValue(TaskDialogButtons.None)]
    public KryptonTaskDialogCommand? DefaultRadioButton
    {
        get => _defaultRadioButton;

        set
        {
            if (_defaultRadioButton != value)
            {
                _defaultRadioButton = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(DefaultRadioButton)));
            }
        }
    }

    /// <summary>
    /// Gets and sets the default common button.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Default Common button.")]
    [DefaultValue(TaskDialogButtons.None)]
    public TaskDialogButtons DefaultButton
    {
        get => _defaultButton;

        set
        {
            if (_defaultButton != value)
            {
                _defaultButton = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(DefaultButton)));
            }
        }
    }

    /// <summary>
    /// Gets and sets the predefined footer icon.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Predefined footer icon.")]
    [DefaultValue(KryptonMessageBoxIcon.None)]
    public KryptonMessageBoxIcon FooterIcon
    {
        get => _footerIcon;

        set
        {
            if (_footerIcon != value)
            {
                _footerIcon = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(FooterIcon)));
            }
        }
    }

    /// <summary>
    /// Gets and sets the custom footer icon.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Custom footer icon.")]
    [DefaultValue(null)]
    public Image? CustomFooterIcon
    {
        get => _customFooterIcon;

        set
        {
            if (_customFooterIcon != value)
            {
                _customFooterIcon = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CustomFooterIcon)));
            }
        }
    }

    /// <summary>
    /// Gets and sets the footer text.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Footer text.")]
    [DefaultValue("")]
    [Localizable(true)]
    [Bindable(true)]
    public string FooterText
    {
        get => _footerText;

        set
        {
            if (_footerText != value)
            {
                _footerText = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(FooterText)));
            }
        }
    }

    /// <summary>
    /// Gets and sets the footer hyperlink.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Footer hyperlink.")]
    [DefaultValue("")]
    [Localizable(true)]
    [Bindable(true)]
    public string FooterHyperlink
    {
        get => _footerHyperlink;

        set
        {
            if (_footerHyperlink != value)
            {
                _footerHyperlink = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(FooterHyperlink)));
            }
        }
    }

    /// <summary>
    /// Gets and sets the Checkbox text.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Checkbox text.")]
    [DefaultValue("")]
    [Localizable(true)]
    [Bindable(true)]
    public string CheckboxText
    {
        get => _checkboxText;

        set
        {
            if (_checkboxText != value)
            {
                _checkboxText = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CheckboxText)));
            }
        }
    }

    /// <summary>
    /// Gets and sets the Checkbox text.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Checkbox state.")]
    [DefaultValue(false)]
    [Localizable(true)]
    [Bindable(true)]
    public bool CheckboxState
    {
        get => _checkboxState;

        set
        {
            if (_checkboxState != value)
            {
                _checkboxState = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CheckboxState)));
            }
        }
    }

    /// <summary>
    /// Gets and sets if the window can be closed.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Can the user close the window.")]
    [DefaultValue(false)]
    public bool AllowDialogClose
    {
        get => _allowDialogClose;

        set
        {
            if (_allowDialogClose != value)
            {
                _allowDialogClose = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(AllowDialogClose)));
            }
        }
    }

    /// <summary>Gets or sets a value indicating whether [use native os icons].</summary>
    /// <value><c>true</c> if [use native os icons]; otherwise, <c>false</c>.</value>
    [Category(@"Appearance")]
    [Description(@"Use the native OS icons.")]
    [DefaultValue(true)]
    public bool UseNativeOSIcons
    {
        get => _useNativeOSIcons;

        set
        {
            if (_useNativeOSIcons != value)
            {
                _useNativeOSIcons = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(UseNativeOSIcons)));
            }
        }
    }

    /// <summary>
    /// Gets and sets user-defined data associated with the object.
    /// </summary>
    [Category(@"Data")]
    [Description(@"User-defined data associated with the object.")]
    [TypeConverter(typeof(StringConverter))]
    [Bindable(true)]
    public object? Tag { get; set; }

    /// <summary>
    /// Allows user to override the default "Ctrl+c to copy" in window caption
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"'ExtraText' in Caption of the window.")]
    [DefaultValue("")]
    [Localizable(true)]
    [Bindable(true)]
    public string TextExtra
    {
        get => _textExtra;
        set
        {
            if (_textExtra != value)
            {
                _textExtra = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(TextExtra)));
            }
        }
    }

    private void ResetTag() => Tag = null;

    private bool ShouldSerializeTag() => Tag != null;

    /// <summary>
    /// Shows the task dialog as a modal dialog box with the currently active window set as its owner.
    /// </summary>
    /// <returns>One of the DialogResult values.</returns>
    public DialogResult ShowDialog() => ShowDialog(Control.FromHandle(PI.GetActiveWindow())!);

    /// <summary>
    /// Shows the form as a modal dialog box with the specified owner.
    /// </summary>
    /// <param name="owner">Any object that implements IWin32Window that represents the top-level window that will own the modal dialog box.</param>
    /// <returns>One of the DialogResult values.</returns>
    public DialogResult ShowDialog(IWin32Window owner)
    {
        // Remove any existing task dialog that is showing
        _taskDialog?.Dispose();

        // Create visual form to show our defined task properties
        _taskDialog = new VisualTaskDialogForm(this)
        {
            StartPosition = owner == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent
        };


        // Return result of showing the task dialog
        return _taskDialog.ShowDialog(owner);
    }

    /// <summary>
    /// Show a task dialog using the specified values as content.
    /// </summary>
    /// <param name="windowTitle">Caption of the window.</param>
    /// <param name="mainInstruction">Principal text.</param>
    /// <param name="content">Extra text.</param>
    /// <param name="icon">Predefined icon.</param>
    /// <param name="commonButtons">Common buttons.</param>
    /// <param name="useNativeOSIcons">Use the OS set of icons.</param>
    /// <returns>One of the DialogResult values.</returns>
    public static DialogResult Show(string windowTitle,
        string mainInstruction,
        string content,
        KryptonMessageBoxIcon icon,
        TaskDialogButtons commonButtons,
        bool? useNativeOSIcons)
    {
        // Create a temporary task dialog for storing definition whilst showing
        using var taskDialog = new KryptonTaskDialog();
        // Store incoming values
        taskDialog.WindowTitle = windowTitle;
        taskDialog.MainInstruction = mainInstruction;
        taskDialog.Content = content;
        taskDialog.Icon = icon;
        taskDialog.CommonButtons = commonButtons;
        taskDialog.UseNativeOSIcons = useNativeOSIcons ?? true;

        // Show as a modal dialog
        return taskDialog.ShowDialog();
    }
    #endregion

    #region Protected
    /// <summary>
    /// Raises the PropertyFooterHyperlinkClickedChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing the event data.</param>
    protected virtual void OnFooterHyperlinkClicked(EventArgs e) => FooterHyperlinkClicked?.Invoke(this, e);

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="e">A PropertyChangedEventArgs containing the event data.</param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);

    #endregion

    #region Internal
    internal void RaiseFooterHyperlinkClicked() => OnFooterHyperlinkClicked(EventArgs.Empty);

    #endregion
}