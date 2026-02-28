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
/// </remarks>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonFormTitleBar), "ToolboxBitmaps.KryptonMenuBar.bmp")]
[DefaultEvent(nameof(ButtonSpecs))]
[DefaultProperty(nameof(ButtonSpecs))]
[Designer(typeof(KryptonFormTitleBarDesigner))]
[DesignerCategory(@"code")]
[Description(@"Hosts button-spec items inside the KryptonForm title bar.")]
public class KryptonFormTitleBar : Component
{
    #region Instance Fields

    private bool _showDropArrow;

    private FormTitleBarValues _values;

    private KryptonForm? _ownerForm;

    #endregion

    #region Events

    /// <summary>Raised when the <see cref="ButtonSpecs"/> collection changes.</summary>
    internal event EventHandler<ButtonSpecEventArgs>? ButtonSpecInserted;

    /// <summary>Raised when the <see cref="ButtonSpecs"/> collection changes.</summary>
    internal event EventHandler<ButtonSpecEventArgs>? ButtonSpecRemoved;

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

        // When button specs are added or removed, raise the corresponding events to notify the owner form to update the title bar display
        ButtonSpecs.Inserted += (s, e) => ButtonSpecInserted?.Invoke(s, e);

        // When button specs are added or removed, raise the corresponding events to notify the owner form to update the title bar display
        ButtonSpecs.Removed += (s, e) => ButtonSpecRemoved?.Invoke(s, e);
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
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public FormTitleBarButtonSpecCollection ButtonSpecs { get; }

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
        var tb = KryptonManager.Strings.ToolBarStrings;
        var fb = KryptonManager.Strings.TitleBarStrings;

        var fileItems = new KryptonContextMenuItems();
        fileItems.Items.Add(new KryptonContextMenuItem(tb.New));
        fileItems.Items.Add(new KryptonContextMenuItem(tb.Open));
        fileItems.Items.Add(new KryptonContextMenuItem(tb.Save));
        fileItems.Items.Add(new KryptonContextMenuItem(tb.SaveAs));
        fileItems.Items.Add(new KryptonContextMenuItem(tb.SaveAll));
        fileItems.Items.Add(new KryptonContextMenuSeparator());
        fileItems.Items.Add(new KryptonContextMenuItem(tb.Print));
        fileItems.Items.Add(new KryptonContextMenuItem(tb.PrintPreview));
        fileItems.Items.Add(new KryptonContextMenuSeparator());
        fileItems.Items.Add(new KryptonContextMenuItem(fb.Exit));
        var fileMenu = new KryptonContextMenu();
        fileMenu.Items.Add(fileItems);

        var editItems = new KryptonContextMenuItems();
        editItems.Items.Add(new KryptonContextMenuItem(tb.Undo));
        editItems.Items.Add(new KryptonContextMenuItem(tb.Redo));
        editItems.Items.Add(new KryptonContextMenuSeparator());
        editItems.Items.Add(new KryptonContextMenuItem(tb.Cut));
        editItems.Items.Add(new KryptonContextMenuItem(tb.Copy));
        editItems.Items.Add(new KryptonContextMenuItem(tb.Paste));
        editItems.Items.Add(new KryptonContextMenuSeparator());
        editItems.Items.Add(new KryptonContextMenuItem(fb.SelectAll));
        var editMenu = new KryptonContextMenu();
        editMenu.Items.Add(editItems);

        var toolsItems = new KryptonContextMenuItems();
        toolsItems.Items.Add(new KryptonContextMenuItem(fb.Customize));
        toolsItems.Items.Add(new KryptonContextMenuItem(fb.Options));
        var toolsMenu = new KryptonContextMenu();
        toolsMenu.Items.Add(toolsItems);

        var helpItems = new KryptonContextMenuItems();
        helpItems.Items.Add(new KryptonContextMenuItem(fb.Contents));
        helpItems.Items.Add(new KryptonContextMenuItem(fb.Index));
        helpItems.Items.Add(new KryptonContextMenuSeparator());
        helpItems.Items.Add(new KryptonContextMenuItem(fb.About));
        var helpMenu = new KryptonContextMenu();
        helpMenu.Items.Add(helpItems);

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

    internal void SetOwnerForm(KryptonForm? form) => _ownerForm = form;

    /// <summary>
    /// Called when Values (ButtonVisibility or ButtonAlignment) change. Syncs existing ButtonSpecs to match.
    /// </summary>
    internal void OnValuesChanged()
    {
        SyncButtonSpecsFromValues();
    }

    #endregion

    #region Implementation

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
        if (disposing && _ownerForm != null)
        {
            _ownerForm.TitleBar = null;
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
