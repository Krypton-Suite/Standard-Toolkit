#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides a toolbar-style area inside the <see cref="KryptonForm"/> title bar (caption area).
/// </summary>
/// <remarks>
/// <para>
/// Add <see cref="ButtonSpecAny"/> items to <see cref="ButtonSpecs"/> to display icon buttons on
/// the <em>left</em> side of the title bar, after the form icon and before the title text.
/// </para>
/// <para>
/// Assign an instance of this component to <see cref="KryptonForm.TitleBar"/> to activate the
/// integration.  The mechanism mirrors the approach used by <c>KryptonRibbon</c> when it injects
/// its Quick Access Toolbar into the custom chrome caption area.
/// </para>
/// <para>
/// Optionally assign a <see cref="MenuStrip"/> (or <see cref="KryptonMenuStrip"/>) to
/// <see cref="MenuStrip"/> to show that strip's top-level items as caption buttons. Generated
/// specs are not serialized; keep the strip on the form (hidden by default) as
/// <see cref="Form.MainMenuStrip"/> so shortcuts still work. Do not also call
/// <see cref="InsertStandardItems"/> for the same File/Edit tree.
/// </para>
/// </remarks>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonFormTitleBar), "ToolboxBitmaps.KryptonApplicationBarMenu.bmp")]
[DefaultEvent(nameof(ButtonSpecs))]
[DefaultProperty(nameof(ButtonSpecs))]
[Designer(typeof(KryptonFormTitleBarDesigner))]
[DesignerCategory(@"code")]
[Description(@"Hosts button-spec items inside the KryptonForm title bar.")]
public class KryptonFormTitleBar : Component
{
    #region Instance Fields

    private bool _showDropArrow;
    private bool _hideSourceMenuStrip = true;
    private bool _assignedMainMenuStrip;
    private bool _sourceHiddenByUs;
    private bool _sourceWasVisible = true;
    private MenuStrip? _menuStrip;
    private FormTitleBarValues _values;
    private KryptonForm? _ownerForm;

    #endregion

    #region Events

    /// <summary>Raised when the <see cref="ButtonSpecs"/> collection changes.</summary>
    internal event EventHandler<ButtonSpecEventArgs>? ButtonSpecInserted;

    /// <summary>Raised when the <see cref="ButtonSpecs"/> collection changes.</summary>
    internal event EventHandler<ButtonSpecEventArgs>? ButtonSpecRemoved;

    /// <summary>Raised when the generated <see cref="MenuStripButtonSpecs"/> collection changes.</summary>
    internal event EventHandler<ButtonSpecEventArgs>? MenuStripButtonSpecInserted;

    /// <summary>Raised when the generated <see cref="MenuStripButtonSpecs"/> collection changes.</summary>
    internal event EventHandler<ButtonSpecEventArgs>? MenuStripButtonSpecRemoved;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonFormTitleBar"/> class.
    /// </summary>
    public KryptonFormTitleBar()
    {
        _values = new FormTitleBarValues(this);

        // Create the collection of button specifications and wire events so that changes to the collection can be reflected in the title bar
        ButtonSpecs = new FormTitleBarButtonSpecCollection(this);
        MenuStripButtonSpecs = new FormTitleBarButtonSpecCollection(this);

        // When button specs are added or removed, raise the corresponding events to notify the owner form to update the title bar display
        ButtonSpecs.Inserted += (s, e) => ButtonSpecInserted?.Invoke(s, e);
        ButtonSpecs.Removed += (s, e) => ButtonSpecRemoved?.Invoke(s, e);
        MenuStripButtonSpecs.Inserted += (s, e) => MenuStripButtonSpecInserted?.Invoke(s, e);
        MenuStripButtonSpecs.Removed += (s, e) => MenuStripButtonSpecRemoved?.Invoke(s, e);
    }

    #endregion

    #region Public

    /// <summary>
    /// Should a drop arrow be shown on buttons that have a dropdown menu.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Should a drop arrow be shown on buttons that have a dropdown menu?")]
    [DefaultValue(false)]
    public bool ShowDropArrow
    {
        get => _showDropArrow;
        set
        {
            if (_showDropArrow != value)
            {
                _showDropArrow = value;

                foreach (var buttonSpec in ButtonSpecs)
                {
                    buttonSpec.ShowDrop = value;
                }

                if (_menuStrip != null)
                {
                    RebuildMenuStripButtonSpecs();
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets a <see cref="MenuStrip"/> (including <see cref="KryptonMenuStrip"/>) whose top-level
    /// items are shown as caption buttons. Generated specs are not serialized; assign the strip in the designer.
    /// Clicks forward to the original <see cref="ToolStripMenuItem"/> handlers.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Optional MenuStrip whose items are shown in the form caption.")]
    [DefaultValue(null)]
    public MenuStrip? MenuStrip
    {
        get => _menuStrip;
        set
        {
            if (ReferenceEquals(_menuStrip, value))
            {
                return;
            }

            UnhookMenuStrip();
            RestoreSourceMenuStrip();
            _menuStrip = value;
            HookMenuStrip();
            ApplySourceMenuStripVisibility();
            RebuildMenuStripButtonSpecs();
        }
    }

    /// <summary>
    /// Gets or sets whether <see cref="MenuStrip"/> is hidden when assigned so the caption is the only menu chrome.
    /// The strip should remain <see cref="Form.MainMenuStrip"/> (set automatically if that property is empty)
    /// so ToolStrip shortcuts keep working.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Hide the source MenuStrip when it is shown in the title bar.")]
    [DefaultValue(true)]
    public bool HideSourceMenuStrip
    {
        get => _hideSourceMenuStrip;
        set
        {
            if (_hideSourceMenuStrip != value)
            {
                _hideSourceMenuStrip = value;
                ApplySourceMenuStripVisibility();
            }
        }
    }

    [Category(@"Visuals")]
    [Description(@"Storage for form title bar related values.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public FormTitleBarValues Values => _values;

    public bool ShouldSerializeValues() => !_values.IsDefault;

    public void ResetValues() => _values.Reset();

    /// <summary>
    /// Gets the collection of button specifications displayed in the title bar.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Collection of button specifications shown in the title bar.")]
    [Editor(typeof(KryptonDesignerButtonSpecAnyCollectionEditor), typeof(UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public FormTitleBarButtonSpecCollection ButtonSpecs { get; }

    /// <summary>
    /// Caption specs generated from <see cref="MenuStrip"/>. Not designer-serialized.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal FormTitleBarButtonSpecCollection MenuStripButtonSpecs { get; }

    /// <summary>
    /// Copies top-level items from <paramref name="menuStrip"/> into <see cref="ButtonSpecs"/>.
    /// Prefer <see cref="MenuStrip"/> for a live bind that is not duplicated in the designer file.
    /// </summary>
    /// <param name="menuStrip">Source strip.</param>
    /// <param name="hideSource">When <c>true</c>, hides <paramref name="menuStrip"/> after the copy.</param>
    public void ImportFrom(MenuStrip menuStrip, bool hideSource = true)
    {
        if (menuStrip is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(menuStrip));
        }

        ButtonSpecs.AddRange(CreateButtonSpecsFrom(menuStrip, ShowDropArrow));
        if (hideSource)
        {
            menuStrip.Visible = false;
        }
    }

    /// <summary>
    /// Creates caption button specs from a <see cref="MenuStrip"/> without assigning <see cref="MenuStrip"/>.
    /// </summary>
    /// <param name="menuStrip">Source strip.</param>
    /// <param name="showDropArrow">Whether drop-down caption buttons show a drop glyph.</param>
    /// <returns>Newly created specs.</returns>
    public static ButtonSpecAny[] CreateButtonSpecsFrom(MenuStrip menuStrip, bool showDropArrow = false) =>
        KryptonMenuStripTitleBarConverter.CreateButtonSpecs(menuStrip, showDropArrow);

    /// <summary>
    /// Gets the <see cref="KryptonForm"/> this component is currently attached to, or <c>null</c>.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonForm? OwnerForm => _ownerForm;

    /// <summary>
    /// Inserts a standard set of button specifications into the title bar, similar to the
    /// WinForms MenuStrip "Insert Standard Items" option.
    /// </summary>
    /// <remarks>
    /// Adds top-level menu dropdowns (File, Edit, Tools, Help) each with sub-items, followed
    /// by flat icon buttons for quick access: New, Open, Save, Save As, Save All, Cut, Copy,
    /// Paste, Undo, Redo, Page Setup, Print Preview, Print, and Quick Print. Wire
    /// <see cref="E:Krypton.Toolkit.ButtonSpecAny.Click"/> or the menu item <see cref="E:Krypton.Toolkit.KryptonContextMenuItem.Click"/>
    /// events, or bind <see cref="ButtonSpecAny.KryptonCommand"/>, to implement the actions.
    /// </remarks>
    public void InsertStandardItems()
    {
        ButtonSpecs.AddRange(CreateStandardMenuButtonSpecs(ShowDropArrow));
        ButtonSpecs.AddRange(CreateStandardToolbarButtonSpecs(this));
    }

    /// <summary>
    /// Creates the top-level menu button specifications (File, Edit, Tools, Help) with dropdowns.
    /// Uses <see cref="KryptonManager.Strings"/> for localizable text.
    /// </summary>
    /// <param name="showDropArrow">Whether to show a drop arrow on menu buttons.</param>
    internal static ButtonSpecAny[] CreateStandardMenuButtonSpecs(bool showDropArrow)
    {
        var fb = KryptonManager.Strings.TitleBarStrings;

        var fileMenu = KryptonStandardMenuFactory.CreateFileContextMenu();
        var editMenu = KryptonStandardMenuFactory.CreateEditContextMenu();
        var toolsMenu = KryptonStandardMenuFactory.CreateToolsContextMenu();
        var helpMenu = KryptonStandardMenuFactory.CreateHelpContextMenu();

        var fileBtn = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Generic,
            Text = fb.File,
            AllowInheritText = false,
            ShowDrop = showDropArrow,
            KryptonContextMenu = fileMenu,
            ToolTipTitle = fb.File
        };
        var editBtn = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Generic,
            Text = fb.Edit,
            AllowInheritText = false,
            ShowDrop = showDropArrow,
            KryptonContextMenu = editMenu,
            ToolTipTitle = fb.Edit
        };
        var toolsBtn = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Generic,
            Text = fb.Tools,
            AllowInheritText = false,
            ShowDrop = showDropArrow,
            KryptonContextMenu = toolsMenu,
            ToolTipTitle = fb.Tools
        };
        var helpBtn = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Generic,
            Text = fb.Help,
            AllowInheritText = false,
            ShowDrop = showDropArrow,
            KryptonContextMenu = helpMenu,
            ToolTipTitle = fb.Help
        };

        return [fileBtn, editBtn, toolsBtn, helpBtn];
    }

    /// <summary>
    /// Creates the flat toolbar button specifications (New, Open, Save, etc.).
    /// Uses <see cref="KryptonManager.Strings"/> for localizable text and the given title bar's Values for visibility and alignment.
    /// </summary>
    /// <param name="titleBar">The title bar instance whose Values (ButtonVisibility, ButtonAlignment) are used.</param>
    internal static ButtonSpecAny[] CreateStandardToolbarButtonSpecs(KryptonFormTitleBar titleBar)
    {
        var tb = KryptonManager.Strings.ToolBarStrings;
        var v = titleBar.Values.ButtonVisibility;
        var a = titleBar.Values.ButtonAlignment;
        var newBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.New, ToolTipTitle = tb.New, Visible = v.ShowNewButton, Edge = a.NewButtonAlignment };
        var openBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.Open, ToolTipTitle = tb.Open, Visible = v.ShowOpenButton, Edge = a.OpenButtonAlignment };
        var saveBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.Save, ToolTipTitle = tb.Save, Visible = v.ShowSaveButton, Edge = a.SaveButtonAlignment };
        var saveAsBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.SaveAs, ToolTipTitle = tb.SaveAs, Visible = v.ShowSaveAsButton, Edge = a.SaveAsButtonAlignment };
        var saveAllBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.SaveAll, ToolTipTitle = tb.SaveAll, Visible = v.ShowSaveAllButton, Edge = a.SaveAllButtonAlignment };
        var cutBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.Cut, ToolTipTitle = tb.Cut, Visible = v.ShowCutButton, Edge = a.CutButtonAlignment };
        var copyBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.Copy, ToolTipTitle = tb.Copy, Visible = v.ShowCopyButton, Edge = a.CopyButtonAlignment };
        var pasteBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.Paste, ToolTipTitle = tb.Paste, Visible = v.ShowPasteButton, Edge = a.PasteButtonAlignment };
        var undoBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.Undo, ToolTipTitle = tb.Undo, Visible = v.ShowUndoButton, Edge = a.UndoButtonAlignment };
        var redoBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.Redo, ToolTipTitle = tb.Redo, Visible = v.ShowRedoButton, Edge = a.RedoButtonAlignment };
        var pageSetupBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.PageSetup, ToolTipTitle = tb.PageSetup, Visible = v.ShowPageSetupButton, Edge = a.PageSetupButtonAlignment };
        var printPreviewBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.PrintPreview, ToolTipTitle = tb.PrintPreview, Visible = v.ShowPrintPreviewButton, Edge = a.PrintPreviewButtonAlignment };
        var printBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.Print, ToolTipTitle = tb.Print, Visible = v.ShowPrintButton, Edge = a.PrintButtonAlignment };
        var quickPrintBtn = new ButtonSpecAny { Type = PaletteButtonSpecStyle.QuickPrint, ToolTipTitle = tb.QuickPrint, Visible = v.ShowQuickPrintButton, Edge = a.QuickPrintButtonAlignment };

        return
        [
            newBtn,
            openBtn,
            saveBtn,
            saveAsBtn,
            saveAllBtn,
            cutBtn,
            copyBtn,
            pasteBtn,
            undoBtn,
            redoBtn,
            pageSetupBtn,
            printPreviewBtn,
            printBtn,
            quickPrintBtn
        ];
    }

    /// <summary>
    /// Creates the complete standard set of button specifications (menus + toolbar).
    /// Used by the designer when inserting via the "Insert Standard Items" verb.
    /// </summary>
    internal static ButtonSpecAny[] CreateStandardButtonSpecs()
    {
        // Create a temporary title bar instance to determine the current ShowDropArrow setting for menu buttons
        KryptonFormTitleBar tb = new KryptonFormTitleBar();

        // Combine menu and toolbar button specs into a single array
        var list = new List<ButtonSpecAny>();

        // Menu buttons should be added first to appear on the left side of the title bar, followed by toolbar buttons
        list.AddRange(CreateStandardMenuButtonSpecs(tb._showDropArrow));

        // Toolbar buttons are added after menu buttons to appear to the right of them in the title bar
        list.AddRange(CreateStandardToolbarButtonSpecs(tb));

        // Return the combined array of button specs
        return list.ToArray();
    }

    #endregion

    #region Internal

    internal void SetOwnerForm(KryptonForm? form)
    {
        _ownerForm = form;
        ApplySourceMenuStripVisibility();
    }

    /// <summary>
    /// Processes shortcuts on user <see cref="ButtonSpecs"/> context menus (not MenuStrip-generated specs).
    /// </summary>
    /// <param name="keyData">Key data.</param>
    /// <returns><c>true</c> if a title-bar menu shortcut was handled.</returns>
    internal bool ProcessButtonSpecShortcuts(Keys keyData)
    {
        foreach (ButtonSpecAny spec in ButtonSpecs)
        {
            if (spec.KryptonContextMenu?.ProcessShortcut(keyData) == true)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Called when Values (ButtonVisibility or ButtonAlignment) change. Syncs existing ButtonSpecs to match.
    /// </summary>
    internal void OnValuesChanged()
    {
        SyncButtonSpecsFromValues();
    }

    #endregion

    #region Implementation

    private void HookMenuStrip()
    {
        if (_menuStrip == null)
        {
            return;
        }

        _menuStrip.ItemAdded += OnMenuStripStructureChanged;
        _menuStrip.ItemRemoved += OnMenuStripStructureChanged;
        _menuStrip.Disposed += OnMenuStripDisposed;
    }

    private void UnhookMenuStrip()
    {
        if (_menuStrip == null)
        {
            return;
        }

        _menuStrip.ItemAdded -= OnMenuStripStructureChanged;
        _menuStrip.ItemRemoved -= OnMenuStripStructureChanged;
        _menuStrip.Disposed -= OnMenuStripDisposed;
    }

    private void OnMenuStripDisposed(object? sender, EventArgs e) => MenuStrip = null;

    private void OnMenuStripStructureChanged(object? sender, ToolStripItemEventArgs e) =>
        RebuildMenuStripButtonSpecs();

    private void RebuildMenuStripButtonSpecs()
    {
        var previous = new List<ButtonSpecAny>();
        foreach (ButtonSpecAny spec in MenuStripButtonSpecs)
        {
            previous.Add(spec);
        }

        MenuStripButtonSpecs.Clear();
        foreach (ButtonSpecAny spec in previous)
        {
            spec.KryptonContextMenu?.Dispose();
        }

        if (_menuStrip == null)
        {
            return;
        }

        MenuStripButtonSpecs.AddRange(CreateButtonSpecsFrom(_menuStrip, ShowDropArrow));
    }

    private void ApplySourceMenuStripVisibility()
    {
        if (_menuStrip == null || _menuStrip.IsDisposed)
        {
            return;
        }

        if (_hideSourceMenuStrip)
        {
            if (!_sourceHiddenByUs)
            {
                _sourceWasVisible = _menuStrip.Visible;
                _sourceHiddenByUs = true;
            }

            _menuStrip.Visible = false;
            if (_ownerForm is { MainMenuStrip: null })
            {
                _ownerForm.MainMenuStrip = _menuStrip;
                _assignedMainMenuStrip = true;
            }
        }
        else
        {
            RestoreSourceMenuStrip();
        }
    }

    private void RestoreSourceMenuStrip()
    {
        if (_menuStrip == null || _menuStrip.IsDisposed)
        {
            _assignedMainMenuStrip = false;
            _sourceHiddenByUs = false;
            return;
        }

        if (_sourceHiddenByUs)
        {
            _menuStrip.Visible = _sourceWasVisible;
            _sourceHiddenByUs = false;
        }

        if (_assignedMainMenuStrip && _ownerForm != null && ReferenceEquals(_ownerForm.MainMenuStrip, _menuStrip))
        {
            _ownerForm.MainMenuStrip = null;
        }

        _assignedMainMenuStrip = false;
    }

    private void SyncButtonSpecsFromValues()
    {
        if (_values is null || ButtonSpecs is null)
        {
            return;
        }

        var v = _values.ButtonVisibility;
        var a = _values.ButtonAlignment;

        foreach (ButtonSpecAny spec in ButtonSpecs)
        {
            switch (spec.Type)
            {
                case PaletteButtonSpecStyle.New:
                    spec.Visible = v.ShowNewButton;
                    spec.Edge = a.NewButtonAlignment;
                    break;
                case PaletteButtonSpecStyle.Open:
                    spec.Visible = v.ShowOpenButton;
                    spec.Edge = a.OpenButtonAlignment;
                    break;
                case PaletteButtonSpecStyle.Save:
                    spec.Visible = v.ShowSaveButton;
                    spec.Edge = a.SaveButtonAlignment;
                    break;
                case PaletteButtonSpecStyle.SaveAs:
                    spec.Visible = v.ShowSaveAsButton;
                    spec.Edge = a.SaveAsButtonAlignment;
                    break;
                case PaletteButtonSpecStyle.SaveAll:
                    spec.Visible = v.ShowSaveAllButton;
                    spec.Edge = a.SaveAllButtonAlignment;
                    break;
                case PaletteButtonSpecStyle.Cut:
                    spec.Visible = v.ShowCutButton;
                    spec.Edge = a.CutButtonAlignment;
                    break;
                case PaletteButtonSpecStyle.Copy:
                    spec.Visible = v.ShowCopyButton;
                    spec.Edge = a.CopyButtonAlignment;
                    break;
                case PaletteButtonSpecStyle.Paste:
                    spec.Visible = v.ShowPasteButton;
                    spec.Edge = a.PasteButtonAlignment;
                    break;
                case PaletteButtonSpecStyle.Undo:
                    spec.Visible = v.ShowUndoButton;
                    spec.Edge = a.UndoButtonAlignment;
                    break;
                case PaletteButtonSpecStyle.Redo:
                    spec.Visible = v.ShowRedoButton;
                    spec.Edge = a.RedoButtonAlignment;
                    break;
                case PaletteButtonSpecStyle.PageSetup:
                    spec.Visible = v.ShowPageSetupButton;
                    spec.Edge = a.PageSetupButtonAlignment;
                    break;
                case PaletteButtonSpecStyle.PrintPreview:
                    spec.Visible = v.ShowPrintPreviewButton;
                    spec.Edge = a.PrintPreviewButtonAlignment;
                    break;
                case PaletteButtonSpecStyle.Print:
                    spec.Visible = v.ShowPrintButton;
                    spec.Edge = a.PrintButtonAlignment;
                    break;
                case PaletteButtonSpecStyle.QuickPrint:
                    spec.Visible = v.ShowQuickPrintButton;
                    spec.Edge = a.QuickPrintButtonAlignment;
                    break;
            }
        }
    }

    #endregion

    #region Protected

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            UnhookMenuStrip();
            RestoreSourceMenuStrip();
            foreach (ButtonSpecAny spec in MenuStripButtonSpecs)
            {
                spec.KryptonContextMenu?.Dispose();
            }

            MenuStripButtonSpecs.Clear();
            if (_ownerForm != null)
            {
                _ownerForm.TitleBar = null;
            }
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Nested Types

    /// <summary>
    /// Typed collection of <see cref="ButtonSpecAny"/> items for the title bar.
    /// </summary>
    public class FormTitleBarButtonSpecCollection : ButtonSpecCollection<ButtonSpecAny>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormTitleBarButtonSpecCollection"/> class.
        /// </summary>
        public FormTitleBarButtonSpecCollection(KryptonFormTitleBar owner)
            : base(owner)
        {
        }
    }

    #endregion
}
