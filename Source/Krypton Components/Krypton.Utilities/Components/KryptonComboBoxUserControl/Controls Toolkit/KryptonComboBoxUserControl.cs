#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Provides a ComboBox-style input control whose drop-down portion hosts an arbitrary
/// <see cref="Control"/> (typically a developer-supplied <see cref="UserControl"/>).
/// Implements feature request
/// <a href="https://github.com/Krypton-Suite/Standard-Toolkit/issues/3443">#3443</a>.
/// </summary>
/// <remarks>
/// The control derives from <see cref="KryptonTextBox"/> and adds a drop button on its right
/// edge. When the user clicks the button (or presses F4 / Alt+Down) the supplied
/// <see cref="DropContent"/> is shown as a Krypton-styled popup anchored to the editor.
/// Drop content can implement <see cref="IKryptonDropDownUserControl"/> to participate in
/// sizing, lifecycle callbacks and value commit/cancel signalling.
/// </remarks>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonComboBox), "ToolboxBitmaps.KryptonComboBox.bmp")]
[DefaultEvent(nameof(ValueCommitted))]
[DefaultProperty(nameof(Text))]
[DefaultBindingProperty(nameof(Text))]
[Designer(typeof(KryptonComboBoxUserControlDesigner))]
[DesignerCategory(@"code")]
[Description(@"A ComboBox-style control whose drop-down hosts any UserControl.")]
public class KryptonComboBoxUserControl : KryptonTextBox
{
    #region Static Fields

    /// <summary>Default popup width used when no preferred size is supplied.</summary>
    private const int DefaultDropDownWidth = 200;
    /// <summary>Default popup height used when no preferred size is supplied.</summary>
    private const int DefaultDropDownHeight = 200;

    #endregion

    #region Instance Fields

    private readonly ButtonSpecAny _dropButton;
    private VisualKryptonDropDownPopup? _popup;
    private Control? _dropContent;
    private object? _selectedValue;
    private LeftRightAlignment _dropDownAlign = LeftRightAlignment.Left;
    private int _dropDownWidth = DefaultDropDownWidth;
    private int _dropDownHeight = DefaultDropDownHeight;
    private Size _minDropDownSize;
    private Size _maxDropDownSize;
    private bool _dropDownResizable;
    private bool _readOnlyEditor;
    private bool _autoOpenOnType;
    private int _minFilterLength = 1;
    private bool _suspendAutoOpen;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the drop-down is about to be shown. Set <c>Cancel</c> to suppress the popup.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the drop-down is about to be shown.")]
    public event EventHandler<KryptonDropDownOpeningEventArgs>? DropDownOpening;

    /// <summary>
    /// Occurs after the drop-down has been shown.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs after the drop-down has been shown.")]
    public event EventHandler? DropDownOpened;

    /// <summary>
    /// Occurs when the drop-down has closed (either by commit, cancel, or click outside).
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Occurs when the drop-down has closed.")]
    public event EventHandler? DropDownClosed;

    /// <summary>
    /// Occurs when the drop-down content commits a value.
    /// </summary>
    [Category(@"Action")]
    [Description(@"Occurs when the drop-down content commits a value.")]
    public event EventHandler<KryptonDropDownCommitEventArgs>? ValueCommitted;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonComboBoxUserControl"/> class.
    /// </summary>
    public KryptonComboBoxUserControl()
    {
        // Disable native auto-complete by default; consumers can opt in via the inherited properties
        base.AutoCompleteMode = AutoCompleteMode.None;
        base.AutoCompleteSource = AutoCompleteSource.None;

        AllowButtonSpecToolTips = true;

        _dropButton = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.DropDown,
            Style = PaletteButtonStyle.ButtonSpec,
            Edge = PaletteRelativeEdgeAlign.Far
        };
        _dropButton.Click += OnDropButtonClick;
        ButtonSpecs.Add(_dropButton);

        KeyDown += OnEditorKeyDown;
        TextChanged += OnEditorTextChanged;
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            KeyDown -= OnEditorKeyDown;
            TextChanged -= OnEditorTextChanged;
            _dropButton.Click -= OnDropButtonClick;

            CloseDropDown();
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the control to display in the drop-down portion. May be any
    /// <see cref="Control"/>; controls implementing <see cref="IKryptonDropDownUserControl"/>
    /// participate in sizing, lifecycle and commit/cancel signalling.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"The control to display in the drop-down portion.")]
    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Editor(typeof(KryptonDropContentEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public Control? DropContent
    {
        get => _dropContent;
        set
        {
            if (_dropContent == value)
            {
                return;
            }

            // Closing while the popup is open is the cleanest behaviour
            if (IsDroppedDown)
            {
                CloseDropDown();
            }

            _dropContent = value;
        }
    }

    /// <summary>
    /// Gets or sets the horizontal alignment of the drop-down relative to the editor.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Horizontal alignment of the drop-down relative to the editor.")]
    [DefaultValue(LeftRightAlignment.Left)]
    public LeftRightAlignment DropDownAlign
    {
        get => _dropDownAlign;
        set => _dropDownAlign = value;
    }

    /// <summary>
    /// Gets or sets the desired width of the drop-down popup. Ignored when the
    /// <see cref="DropContent"/> implements <see cref="IKryptonDropDownUserControl"/> and
    /// returns a non-empty preferred size.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Desired width of the drop-down popup.")]
    [DefaultValue(DefaultDropDownWidth)]
    public int DropDownWidth
    {
        get => _dropDownWidth;
        set => _dropDownWidth = Math.Max(1, value);
    }

    /// <summary>
    /// Gets or sets the desired height of the drop-down popup. Ignored when the
    /// <see cref="DropContent"/> implements <see cref="IKryptonDropDownUserControl"/> and
    /// returns a non-empty preferred size.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Desired height of the drop-down popup.")]
    [DefaultValue(DefaultDropDownHeight)]
    public int DropDownHeight
    {
        get => _dropDownHeight;
        set => _dropDownHeight = Math.Max(1, value);
    }

    /// <summary>
    /// Gets or sets the minimum size of the drop-down popup.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Minimum size of the drop-down popup. Use Size.Empty to disable.")]
    public Size MinDropDownSize
    {
        get => _minDropDownSize;
        set => _minDropDownSize = value;
    }

    private bool ShouldSerializeMinDropDownSize() => !_minDropDownSize.IsEmpty;
    private void ResetMinDropDownSize() => _minDropDownSize = Size.Empty;

    /// <summary>
    /// Gets or sets the maximum size of the drop-down popup.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Maximum size of the drop-down popup. Use Size.Empty to disable.")]
    public Size MaxDropDownSize
    {
        get => _maxDropDownSize;
        set => _maxDropDownSize = value;
    }

    private bool ShouldSerializeMaxDropDownSize() => !_maxDropDownSize.IsEmpty;
    private void ResetMaxDropDownSize() => _maxDropDownSize = Size.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the user can resize the drop-down popup at runtime.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Whether the user can resize the drop-down popup at runtime.")]
    [DefaultValue(false)]
    public bool DropDownResizable
    {
        get => _dropDownResizable;
        set => _dropDownResizable = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the editor is read-only (mimicking
    /// <c>ComboBoxStyle.DropDownList</c> on the standard <see cref="KryptonComboBox"/>).
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"When true the editor cannot be typed into; selection happens through the drop-down only.")]
    [DefaultValue(false)]
    public bool ReadOnlyEditor
    {
        get => _readOnlyEditor;
        set
        {
            _readOnlyEditor = value;
            ReadOnly = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the drop-down should automatically open as the
    /// user types in the editor (filter-as-you-type). The host opens the popup without stealing
    /// focus and forwards typed text to the drop content via
    /// <see cref="IKryptonDropDownFilterable.ApplyFilter"/>.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"When true, typing in the editor automatically opens the drop-down and forwards the text to a filterable drop content.")]
    [DefaultValue(false)]
    public bool AutoOpenOnType
    {
        get => _autoOpenOnType;
        set => _autoOpenOnType = value;
    }

    /// <summary>
    /// Gets or sets the minimum number of characters that must be typed before
    /// <see cref="AutoOpenOnType"/> opens the drop-down.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Minimum number of characters before AutoOpenOnType triggers the drop-down.")]
    [DefaultValue(1)]
    public int MinFilterLength
    {
        get => _minFilterLength;
        set => _minFilterLength = Math.Max(0, value);
    }

    /// <summary>
    /// Gets the value most recently committed by the drop-down content.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object? SelectedValue => _selectedValue;

    /// <summary>
    /// Gets a value indicating whether the drop-down is currently shown.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsDroppedDown => _popup is { IsHandleCreated: true, IsDisposed: false };

    /// <summary>
    /// Gets access to the drop button specification, allowing further customisation
    /// (e.g. a custom image or tooltip).
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ButtonSpecAny DropButton => _dropButton;

    #endregion

    #region Public Methods

    /// <summary>
    /// Opens the drop-down. Has no effect when no <see cref="DropContent"/> is assigned or when
    /// the drop-down is already open.
    /// </summary>
    public void ShowDropDown() => ShowDropDown(retainEditorFocus: false);

    /// <summary>
    /// Opens the drop-down. Has no effect when no <see cref="DropContent"/> is assigned or when
    /// the drop-down is already open.
    /// </summary>
    /// <param name="retainEditorFocus">
    /// When <see langword="true"/> focus is forced back to the editor after the popup is shown
    /// (useful for filter-as-you-type scenarios where the user needs to continue typing).
    /// </param>
    public void ShowDropDown(bool retainEditorFocus)
    {
        if (_dropContent == null || IsDroppedDown || DesignMode || !IsHandleCreated)
        {
            return;
        }

        var openingArgs = new KryptonDropDownOpeningEventArgs(_dropContent);
        OnDropDownOpening(openingArgs);
        if (openingArgs.Cancel)
        {
            return;
        }

        Size popupSize = ResolvePopupSize();

        IRenderer? renderer = null;
        try
        {
            renderer = KryptonManager.CurrentGlobalPalette.GetRenderer();
        }
        catch
        {
            // Fall back to default renderer if palette cannot supply one
        }

        _popup = new VisualKryptonDropDownPopup(this, _dropContent, renderer,
            _dropDownResizable, _minDropDownSize, _maxDropDownSize);
        _popup.Disposed += OnPopupDisposed;
        _popup.ValueCommitted += OnPopupValueCommitted;

        Rectangle anchor = RectangleToScreen(ClientRectangle);
        _popup.ShowAnchored(anchor, popupSize, _dropDownAlign);

        OnDropDownOpened(EventArgs.Empty);

        if (retainEditorFocus)
        {
            // Pull focus back to the editor after the contract's OnDropDownOpened may have moved it
            BeginInvoke(new Action(() =>
            {
                if (!IsDisposed && CanFocus && !Focused)
                {
                    Focus();
                    SelectionStart = Text.Length;
                    SelectionLength = 0;
                }
            }));
        }
    }

    /// <summary>
    /// Closes the drop-down if it is currently shown.
    /// </summary>
    public void CloseDropDown()
    {
        if (_popup == null)
        {
            return;
        }

        if (_popup is { IsDisposed: false, IsHandleCreated: true })
        {
            VisualPopupManager.Singleton.EndPopupTracking(_popup);
        }

        _popup = null;
    }

    #endregion

    #region Protected Overrides

    /// <summary>
    /// Raises the <see cref="DropDownOpening"/> event.
    /// </summary>
    /// <param name="e">A <see cref="KryptonDropDownOpeningEventArgs"/> that contains the event data.</param>
    protected virtual void OnDropDownOpening(KryptonDropDownOpeningEventArgs e) => DropDownOpening?.Invoke(this, e);

    /// <summary>
    /// Raises the <see cref="DropDownOpened"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    protected virtual void OnDropDownOpened(EventArgs e) => DropDownOpened?.Invoke(this, e);

    /// <summary>
    /// Raises the <see cref="DropDownClosed"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    protected virtual void OnDropDownClosed(EventArgs e) => DropDownClosed?.Invoke(this, e);

    /// <summary>
    /// Raises the <see cref="ValueCommitted"/> event.
    /// </summary>
    /// <param name="e">A <see cref="KryptonDropDownCommitEventArgs"/> that contains the event data.</param>
    protected virtual void OnValueCommitted(KryptonDropDownCommitEventArgs e) => ValueCommitted?.Invoke(this, e);

    #endregion

    #region Implementation

    private Size ResolvePopupSize()
    {
        Size proposed = new Size(_dropDownWidth, _dropDownHeight);

        if (_dropContent is IKryptonDropDownUserControl contract)
        {
            Size preferred = contract.GetPreferredDropSize(proposed);
            if (!preferred.IsEmpty)
            {
                proposed = preferred;
            }
        }

        // Width should be at least the editor width so the popup never appears narrower than its anchor
        if (proposed.Width < Width)
        {
            proposed = new Size(Width, proposed.Height);
        }

        return proposed;
    }

    private void OnDropButtonClick(object? sender, EventArgs e)
    {
        if (IsDroppedDown)
        {
            CloseDropDown();
        }
        else
        {
            ShowDropDown();
        }
    }

    private void OnEditorKeyDown(object? sender, KeyEventArgs e)
    {
        // F4 toggles, Alt+Down opens, Alt+Up closes - matches WinForms ComboBox conventions
        if (e.KeyCode == Keys.F4 && !e.Alt && !e.Control && !e.Shift)
        {
            if (IsDroppedDown)
            {
                CloseDropDown();
            }
            else
            {
                ShowDropDown();
            }
            e.Handled = true;
            e.SuppressKeyPress = true;
            return;
        }

        if (e.Alt && e.KeyCode == Keys.Down)
        {
            if (!IsDroppedDown)
            {
                ShowDropDown();
            }
            e.Handled = true;
            e.SuppressKeyPress = true;
            return;
        }

        if (e.Alt && e.KeyCode == Keys.Up)
        {
            if (IsDroppedDown)
            {
                CloseDropDown();
            }
            e.Handled = true;
            e.SuppressKeyPress = true;
            return;
        }

        if (e.KeyCode == Keys.Escape && IsDroppedDown)
        {
            CloseDropDown();
            e.Handled = true;
            e.SuppressKeyPress = true;
            return;
        }

        // Forward Up/Down/Enter to a filterable drop content while the popup is open and the
        // editor still has focus (filter-as-you-type pattern).
        if (IsDroppedDown && _dropContent is IKryptonDropDownFilterable filterable)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    filterable.NavigateSelection(1);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    return;
                case Keys.Up:
                    filterable.NavigateSelection(-1);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    return;
                case Keys.Enter:
                    if (filterable.CommitSelection())
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    return;
            }
        }
    }

    private void OnEditorTextChanged(object? sender, EventArgs e)
    {
        if (!_autoOpenOnType || _suspendAutoOpen || DesignMode)
        {
            return;
        }

        if (Text.Length < _minFilterLength)
        {
            // Below the threshold, close the popup if it was opened by us
            if (IsDroppedDown)
            {
                CloseDropDown();
            }
            return;
        }

        if (!IsDroppedDown)
        {
            ShowDropDown(retainEditorFocus: true);
        }

        if (_dropContent is IKryptonDropDownFilterable filterable)
        {
            bool anyMatch = filterable.ApplyFilter(Text);
            if (!anyMatch && IsDroppedDown)
            {
                CloseDropDown();
            }
        }
    }

    private void OnPopupValueCommitted(object? sender, KryptonDropDownCommitEventArgs e)
    {
        _selectedValue = e.Value;

        if (e.DisplayText != null)
        {
            // Suppress auto-open-on-type while we programmatically push the committed text into
            // the editor, otherwise the popup would immediately try to reopen with the new text.
            _suspendAutoOpen = true;
            try
            {
                Text = e.DisplayText;
                SelectionStart = Text.Length;
                SelectionLength = 0;
            }
            finally
            {
                _suspendAutoOpen = false;
            }
        }

        OnValueCommitted(e);
    }

    private void OnPopupDisposed(object? sender, EventArgs e)
    {
        if (_popup != null)
        {
            _popup.Disposed -= OnPopupDisposed;
            _popup.ValueCommitted -= OnPopupValueCommitted;
            _popup = null;
        }

        OnDropDownClosed(EventArgs.Empty);
    }

    #endregion
}
