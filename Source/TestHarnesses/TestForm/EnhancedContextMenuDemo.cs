#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Demo for issue #3862: Office 2007+-style <see cref="KryptonEnhancedContextMenu"/>
/// with Mini Toolbar, selection fade, and in-menu galleries.
/// </summary>
public partial class EnhancedContextMenuDemo : KryptonForm
{
    private readonly KryptonEnhancedContextMenu _enhancedMenu = new();
    private readonly KryptonMiniToolbar _selectionToolbar = new();
    private readonly KryptonCommand _cmdBold = new();
    private readonly KryptonCommand _cmdItalic = new();
    private readonly KryptonCommand _cmdUnderline = new();
    private readonly KryptonCommand _cmdCut = new();
    private readonly KryptonCommand _cmdCopy = new();
    private readonly KryptonCommand _cmdPaste = new();
    private readonly KryptonCommand _cmdFontColor = new();
    private readonly KryptonCommand _cmdGrowFont = new();
    private readonly KryptonCommand _cmdShrinkFont = new();
    private readonly KryptonCommand _cmdClearFormatting = new();
    private readonly List<Image> _ownedImages = [];
    private readonly ImageList _styleImages = new();
    private readonly ImageList _miniStyleImages = new();
    private readonly ContextMenuStrip _suppressedStrip = new();
    private RichTextBox? _lastEditor;
    private Font? _previewFont;
    private Color _previewColor;
    private Color _fontColor = Color.FromArgb(192, 57, 43);
    private bool _previewing;

    public EnhancedContextMenuDemo()
    {
        InitializeComponent();
        _suppressedStrip.Opening += (_, e) => e.Cancel = true;
        BuildCommands();
        BuildEnhancedMenu();
        PopulateMiniToolbar(_enhancedMenu.MiniToolbar);
        PopulateMiniToolbar(_selectionToolbar);
        HookEditors();
        LoadSampleText(krtbKrypton.RichTextBox);
        LoadSampleText(rtbNative);
        _lastEditor = krtbKrypton.RichTextBox;
        _selectionToolbar.Attach(krtbKrypton);
        foreach (KryptonMiniToolbarPosition position in Enum.GetValues(typeof(KryptonMiniToolbarPosition)))
        {
            kcmbPosition.Items.Add(position);
        }

        kcmbPosition.SelectedItem = _enhancedMenu.MiniToolbarPosition;
        kpgSettings.SelectedObject = _enhancedMenu;
        kwlblStatus.Text = @"Ready. Right-click either editor, or select text to fade in the Mini Toolbar.";
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _selectionToolbar.Detach();
            _enhancedMenu.Dispose();
            _selectionToolbar.Dispose();
            _cmdBold.Dispose();
            _cmdItalic.Dispose();
            _cmdUnderline.Dispose();
            _cmdCut.Dispose();
            _cmdCopy.Dispose();
            _cmdPaste.Dispose();
            _cmdFontColor.Dispose();
            _cmdGrowFont.Dispose();
            _cmdShrinkFont.Dispose();
            _cmdClearFormatting.Dispose();
            _suppressedStrip.Dispose();
            _styleImages.Dispose();
            _miniStyleImages.Dispose();
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
            foreach (Image image in _ownedImages)
            {
                image.Dispose();
            }

            _ownedImages.Clear();
            components?.Dispose();
        }

        base.Dispose(disposing);
    }

    private void BuildCommands()
    {
        _cmdBold.Text = @"Bold";
        _cmdBold.ImageSmall = CreateLetterGlyph(@"B", FontStyle.Bold);
        _cmdBold.Execute += (_, _) => ToggleStyle(FontStyle.Bold);

        _cmdItalic.Text = @"Italic";
        _cmdItalic.ImageSmall = CreateLetterGlyph(@"I", FontStyle.Italic);
        _cmdItalic.Execute += (_, _) => ToggleStyle(FontStyle.Italic);

        _cmdUnderline.Text = @"Underline";
        _cmdUnderline.ImageSmall = CreateLetterGlyph(@"U", FontStyle.Underline);
        _cmdUnderline.Execute += (_, _) => ToggleStyle(FontStyle.Underline);

        _cmdCut.Text = @"Cut";
        _cmdCut.CommandType = KryptonCommandType.IntegratedToolBarCutCommand;
        _cmdCut.Execute += (_, _) => GetActiveEditor()?.Cut();

        _cmdCopy.Text = @"Copy";
        _cmdCopy.CommandType = KryptonCommandType.IntegratedToolBarCopyCommand;
        _cmdCopy.Execute += (_, _) => GetActiveEditor()?.Copy();

        _cmdPaste.Text = @"Paste";
        _cmdPaste.CommandType = KryptonCommandType.IntegratedToolBarPasteCommand;
        _cmdPaste.Execute += (_, _) => GetActiveEditor()?.Paste();

        _cmdFontColor.Text = string.Empty;
        _cmdFontColor.ImageSmall = CreateFontColorGlyph(_fontColor);
        _cmdFontColor.Execute += (_, _) => ApplySelectionColor(_fontColor);

        _cmdGrowFont.Text = @"Increase font size";
        _cmdGrowFont.ImageSmall = CreateFontStepGlyph(true);
        _cmdGrowFont.Execute += (_, _) => AdjustFontSize(1f);

        _cmdShrinkFont.Text = @"Decrease font size";
        _cmdShrinkFont.ImageSmall = CreateFontStepGlyph(false);
        _cmdShrinkFont.Execute += (_, _) => AdjustFontSize(-1f);

        _cmdClearFormatting.Text = @"Clear formatting";
        _cmdClearFormatting.ImageSmall = CreateClearFormattingGlyph();
        _cmdClearFormatting.Execute += (_, _) => ApplyStylePreset(0);
    }

    private void BuildEnhancedMenu()
    {
        _enhancedMenu.KeepMiniToolbarAfterCommand = kchkKeepToolbar.Checked;
        _enhancedMenu.ShowMiniToolbar = kchkShowMiniToolbar.Checked;

        var clipboard = new KryptonContextMenuItems();
        clipboard.Items.Add(new KryptonContextMenuItem(@"Cut") { KryptonCommand = _cmdCut, ShortcutKeys = Keys.Control | Keys.X });
        clipboard.Items.Add(new KryptonContextMenuItem(@"Copy") { KryptonCommand = _cmdCopy, ShortcutKeys = Keys.Control | Keys.C });
        clipboard.Items.Add(new KryptonContextMenuItem(@"Paste") { KryptonCommand = _cmdPaste, ShortcutKeys = Keys.Control | Keys.V });
        _enhancedMenu.Menu.Items.Add(new KryptonContextMenuHeading(@"Clipboard"));
        _enhancedMenu.Menu.Items.Add(clipboard);

        _styleImages.ImageSize = new Size(56, 32);
        _styleImages.ColorDepth = ColorDepth.Depth32Bit;
        Size previewSize = _styleImages.ImageSize;
        _styleImages.Images.Add(CreateStylePreview(@"Aa", @"Segoe UI", 11f, FontStyle.Regular, Color.Black, previewSize));
        _styleImages.Images.Add(CreateStylePreview(@"Aa", @"Cambria", 14f, FontStyle.Bold, Color.FromArgb(0, 51, 102), previewSize));
        _styleImages.Images.Add(CreateStylePreview(@"Aa", @"Segoe UI", 11f, FontStyle.Italic, Color.FromArgb(102, 102, 102), previewSize));
        _styleImages.Images.Add(CreateStylePreview(@"Aa", @"Segoe UI", 11f, FontStyle.Bold | FontStyle.Italic, Color.Maroon, previewSize));
        _styleImages.Images.Add(CreateStylePreview(@"Aa", @"Consolas", 11f, FontStyle.Regular, Color.Teal, previewSize));

        _miniStyleImages.ImageSize = new Size(20, 16);
        _miniStyleImages.ColorDepth = ColorDepth.Depth32Bit;
        for (var i = 0; i < _styleImages.Images.Count; i++)
        {
            Image source = _styleImages.Images[i];
            _miniStyleImages.Images.Add(CreateMiniStyleSwatch(source));
        }

        var gallery = new KryptonContextMenuGallery
        {
            ImageList = _styleImages,
            ShowItemText = true,
            LineItems = 5,
            AutoClose = true,
            Padding = new Padding(4)
        };
        gallery.Items.Add(new KryptonContextMenuGalleryItem(@"Normal") { ImageIndex = 0, Tag = 0 });
        gallery.Items.Add(new KryptonContextMenuGalleryItem(@"Heading") { ImageIndex = 1, Tag = 1 });
        gallery.Items.Add(new KryptonContextMenuGalleryItem(@"Quote") { ImageIndex = 2, Tag = 2 });
        gallery.Items.Add(new KryptonContextMenuGalleryItem(@"Emphasis") { ImageIndex = 3, Tag = 3 });
        gallery.Items.Add(new KryptonContextMenuGalleryItem(@"Code") { ImageIndex = 4, Tag = 4 });
        gallery.TrackingImage += OnStyleTracking;
        gallery.SelectedIndexChanged += (_, _) =>
        {
            CommitPreview();
            ApplyStylePreset(gallery.SelectedIndex);
            SetStatus($@"Style gallery selected index {gallery.SelectedIndex}.");
        };
        gallery.MoreItems.Add(new KryptonContextMenuItem(@"Clear formatting", (_, _) => ApplyStylePreset(0)));
        _enhancedMenu.Menu.Items.Add(new KryptonContextMenuHeading(@"Styles"));
        _enhancedMenu.Menu.Items.Add(gallery);

        var extra = new KryptonContextMenuItems();
        extra.Items.Add(new KryptonContextMenuItem(@"Select All", (_, _) => GetActiveEditor()?.SelectAll())
        {
            ShortcutKeys = Keys.Control | Keys.A
        });
        _enhancedMenu.Menu.Items.Add(extra);

        _enhancedMenu.Opened += (_, _) => SetStatus(@"Enhanced context menu opened (Mini Toolbar + menu). Click a Mini Toolbar command to keep the bar.");
        _enhancedMenu.Closed += (_, _) => SetStatus(@"Enhanced context menu closed.");
    }

    private void PopulateMiniToolbar(KryptonMiniToolbar toolbar)
    {
        toolbar.Items.Add(new KryptonMiniToolbarButton
        {
            ButtonType = KryptonMiniToolbarButtonType.Check,
            KryptonCommand = _cmdBold,
            Image = _cmdBold.ImageSmall,
            ToolTipText = @"Bold",
            Tag = @"Bold"
        });
        toolbar.Items.Add(new KryptonMiniToolbarButton
        {
            ButtonType = KryptonMiniToolbarButtonType.Check,
            KryptonCommand = _cmdItalic,
            Image = _cmdItalic.ImageSmall,
            ToolTipText = @"Italic",
            Tag = @"Italic"
        });
        toolbar.Items.Add(new KryptonMiniToolbarButton
        {
            ButtonType = KryptonMiniToolbarButtonType.Check,
            KryptonCommand = _cmdUnderline,
            Image = _cmdUnderline.ImageSmall,
            ToolTipText = @"Underline",
            Tag = @"Underline"
        });
        toolbar.Items.Add(new KryptonMiniToolbarSeparator());

        var fontCombo = new KryptonMiniToolbarComboBox
        {
            Width = 112,
            DropDownStyle = ComboBoxStyle.DropDownList,
            ToolTipText = @"Font family",
            Tag = @"Font"
        };
        fontCombo.Items.Add(@"Segoe UI");
        fontCombo.Items.Add(@"Calibri");
        fontCombo.Items.Add(@"Cambria");
        fontCombo.Items.Add(@"Consolas");
        fontCombo.Items.Add(@"Arial");
        fontCombo.Items.Add(@"Times New Roman");
        fontCombo.Items.Add(@"Courier New");
        fontCombo.SelectedIndex = 0;
        fontCombo.SelectedIndexChanged += (_, _) =>
        {
            if (fontCombo.SelectedItem is string family)
            {
                ApplyFontFamily(family);
            }
        };
        toolbar.Items.Add(fontCombo);

        var sizeCombo = new KryptonMiniToolbarComboBox
        {
            Width = 48,
            DropDownStyle = ComboBoxStyle.DropDownList,
            ToolTipText = @"Font size",
            Tag = @"Size"
        };
        foreach (var size in new object[] { "8", "9", "10", "11", "12", "14", "16", "18", "24", "36" })
        {
            sizeCombo.Items.Add(size);
        }

        sizeCombo.SelectedIndex = 4;
        sizeCombo.SelectedIndexChanged += (_, _) =>
        {
            if (sizeCombo.SelectedItem is string text
                && float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var size))
            {
                ApplyFontSize(size);
            }
        };
        toolbar.Items.Add(sizeCombo);
        toolbar.Items.Add(new KryptonMiniToolbarSeparator());

        var colorMenu = new KryptonContextMenu();
        var columns = new KryptonContextMenuColorColumns(ColorScheme.Basic16);
        columns.SelectedColorChanged += (_, e) => ApplySelectionColor(e.Color);
        colorMenu.Items.Add(columns);
        toolbar.Items.Add(new KryptonMiniToolbarSplitButton
        {
            Image = _cmdFontColor.ImageSmall,
            ToolTipText = @"Font colour",
            KryptonCommand = _cmdFontColor,
            KryptonContextMenu = colorMenu,
            Tag = @"Color"
        });
        toolbar.Items.Add(new KryptonMiniToolbarButton
        {
            KryptonCommand = _cmdGrowFont,
            Image = _cmdGrowFont.ImageSmall,
            ToolTipText = @"Increase font size",
            Tag = @"Grow"
        });
        toolbar.Items.Add(new KryptonMiniToolbarButton
        {
            KryptonCommand = _cmdShrinkFont,
            Image = _cmdShrinkFont.ImageSmall,
            ToolTipText = @"Decrease font size",
            Tag = @"Shrink"
        });
        toolbar.Items.Add(new KryptonMiniToolbarButton
        {
            KryptonCommand = _cmdClearFormatting,
            Image = _cmdClearFormatting.ImageSmall,
            ToolTipText = @"Clear formatting",
            Tag = @"Clear"
        });

        var miniGallery = new KryptonMiniToolbarGallery
        {
            ImageList = _miniStyleImages,
            MaxVisibleItems = 5,
            ToolTipText = @"Style gallery",
            Tag = @"Gallery",
            Visible = kchkItemGallery.Checked
        };
        miniGallery.TrackingImage += OnStyleTracking;
        miniGallery.SelectedIndexChanged += (_, _) =>
        {
            CommitPreview();
            ApplyStylePreset(miniGallery.SelectedIndex);
        };
        toolbar.Items.Add(miniGallery);

        toolbar.ItemClick += (_, e) => SetStatus($@"Mini Toolbar item: {e.Item}");
    }

    private void HookEditors()
    {
        krtbKrypton.RichTextBox.ContextMenuStrip = _suppressedStrip;
        rtbNative.ContextMenuStrip = _suppressedStrip;
        krtbKrypton.RichTextBox.MouseUp += OnEditorMouseUp;
        rtbNative.MouseUp += OnEditorMouseUp;
        krtbKrypton.Enter += (_, _) =>
        {
            _lastEditor = krtbKrypton.RichTextBox;
            AttachSelectionToolbar(krtbKrypton);
        };
        rtbNative.Enter += (_, _) =>
        {
            _lastEditor = rtbNative;
            AttachSelectionToolbar(rtbNative);
        };
        krtbKrypton.RichTextBox.SelectionChanged += (_, _) => SyncCommandChecks();
        rtbNative.SelectionChanged += (_, _) => SyncCommandChecks();
    }

    private void OnEditorMouseUp(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right)
        {
            return;
        }

        if (sender is RichTextBox editor)
        {
            _lastEditor = editor;
        }

        _enhancedMenu.KeepMiniToolbarAfterCommand = kchkKeepToolbar.Checked;
        _enhancedMenu.ShowMiniToolbar = kchkShowMiniToolbar.Checked;
        _enhancedMenu.Show(this, Control.MousePosition);
    }

    private void kchkKeepToolbar_CheckedChanged(object? sender, EventArgs e)
    {
        _enhancedMenu.KeepMiniToolbarAfterCommand = kchkKeepToolbar.Checked;
        RefreshSettingsGrid();
    }

    private void kchkShowMiniToolbar_CheckedChanged(object? sender, EventArgs e)
    {
        _enhancedMenu.ShowMiniToolbar = kchkShowMiniToolbar.Checked;
        RefreshSettingsGrid();
    }

    private void kcmbPosition_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kcmbPosition.SelectedItem is KryptonMiniToolbarPosition position)
        {
            _enhancedMenu.MiniToolbarPosition = position;
            RefreshSettingsGrid();
        }
    }

    private void knudIdleOpacity_ValueChanged(object? sender, EventArgs e)
    {
        var opacity = (byte)knudIdleOpacity.Value;
        _enhancedMenu.MiniToolbar.IdleOpacity = opacity;
        _selectionToolbar.IdleOpacity = opacity;
        RefreshSettingsGrid();
    }

    private void knudApproach_ValueChanged(object? sender, EventArgs e)
    {
        var distance = (int)knudApproach.Value;
        _enhancedMenu.MiniToolbar.ApproachDistance = distance;
        _selectionToolbar.ApproachDistance = distance;
        RefreshSettingsGrid();
    }

    private void knudGap_ValueChanged(object? sender, EventArgs e)
    {
        _enhancedMenu.MiniToolbarGap = (int)knudGap.Value;
        RefreshSettingsGrid();
    }

    private void kchkShowShadow_CheckedChanged(object? sender, EventArgs e)
    {
        _enhancedMenu.MiniToolbar.ShowShadow = kchkShowShadow.Checked;
        _selectionToolbar.ShowShadow = kchkShowShadow.Checked;
        RefreshSettingsGrid();
    }

    private void kchkSelectionFade_CheckedChanged(object? sender, EventArgs e)
    {
        if (kchkSelectionFade.Checked)
        {
            Control host = ReferenceEquals(_lastEditor, rtbNative) ? rtbNative : krtbKrypton;
            AttachSelectionToolbar(host);
        }
        else
        {
            _selectionToolbar.Detach();
        }
    }

    private void OnItemVisibilityChanged(object? sender, EventArgs e)
    {
        SetItemVisible(@"Bold", kchkItemBold.Checked);
        SetItemVisible(@"Italic", kchkItemItalic.Checked);
        SetItemVisible(@"Underline", kchkItemUnderline.Checked);
        SetItemVisible(@"Font", kchkItemFont.Checked);
        SetItemVisible(@"Size", kchkItemSize.Checked);
        SetItemVisible(@"Color", kchkItemColor.Checked);
        SetItemVisible(@"Gallery", kchkItemGallery.Checked);
        RefreshSettingsGrid();
    }

    private void AttachSelectionToolbar(Control host)
    {
        if (kchkSelectionFade.Checked)
        {
            _selectionToolbar.Attach(host);
        }
    }

    private void SetItemVisible(string tag, bool visible)
    {
        SetItemVisible(_enhancedMenu.MiniToolbar, tag, visible);
        SetItemVisible(_selectionToolbar, tag, visible);
    }

    private static void SetItemVisible(KryptonMiniToolbar toolbar, string tag, bool visible)
    {
        foreach (KryptonMiniToolbarItemBase item in toolbar.Items)
        {
            if (Equals(item.Tag, tag))
            {
                item.Visible = visible;
            }
        }
    }

    private void RefreshSettingsGrid() => kpgSettings.Refresh();

    private void kbtnClose_Click(object? sender, EventArgs e) => Close();

    private RichTextBox? GetActiveEditor()
    {
        if (krtbKrypton.RichTextBox.Focused)
        {
            return krtbKrypton.RichTextBox;
        }

        if (rtbNative.Focused)
        {
            return rtbNative;
        }

        return _lastEditor;
    }

    private void ToggleStyle(FontStyle style)
    {
        RichTextBox? editor = GetActiveEditor();
        if (editor == null)
        {
            return;
        }

        Font current = editor.SelectionFont ?? editor.Font;
        FontStyle next = current.Style ^ style;
        editor.SelectionFont = new Font(current, next);
        SyncCommandChecks();
        SetStatus($@"Toggled {style} on the current selection.");
    }

    private void ApplyFontFamily(string family)
    {
        RichTextBox? editor = GetActiveEditor();
        if (editor == null)
        {
            return;
        }

        Font current = editor.SelectionFont ?? editor.Font;
        editor.SelectionFont = new Font(family, current.Size, current.Style);
        SetStatus($@"Font family: {family}.");
    }

    private void ApplyFontSize(float size)
    {
        RichTextBox? editor = GetActiveEditor();
        if (editor == null)
        {
            return;
        }

        Font current = editor.SelectionFont ?? editor.Font;
        editor.SelectionFont = new Font(current.FontFamily, size, current.Style);
        SetStatus($@"Font size: {size}.");
    }

    private void AdjustFontSize(float delta)
    {
        RichTextBox? editor = GetActiveEditor();
        if (editor == null)
        {
            return;
        }

        Font current = editor.SelectionFont ?? editor.Font;
        var next = Math.Max(8f, Math.Min(72f, current.Size + delta));
        ApplyFontSize(next);
    }

    private void ApplySelectionColor(Color color)
    {
        RichTextBox? editor = GetActiveEditor();
        if (editor == null)
        {
            return;
        }

        editor.SelectionColor = color;
        _fontColor = color;
        _cmdFontColor.ImageSmall = CreateFontColorGlyph(color);
        foreach (KryptonMiniToolbarItemBase item in _enhancedMenu.MiniToolbar.Items)
        {
            if (Equals(item.Tag, @"Color"))
            {
                item.Image = _cmdFontColor.ImageSmall;
            }
        }

        foreach (KryptonMiniToolbarItemBase item in _selectionToolbar.Items)
        {
            if (Equals(item.Tag, @"Color"))
            {
                item.Image = _cmdFontColor.ImageSmall;
            }
        }

        SetStatus($@"Selection colour: {color.Name}.");
    }

    private void OnStyleTracking(object? sender, ImageSelectEventArgs e)
    {
        if (e.ImageIndex < 0)
        {
            RevertPreview();
            return;
        }

        BeginPreview();
        ApplyStylePreset(e.ImageIndex);
    }

    private void BeginPreview()
    {
        RichTextBox? editor = GetActiveEditor();
        if (editor == null || _previewing)
        {
            return;
        }

        _previewFont = editor.SelectionFont;
        _previewColor = editor.SelectionColor;
        _previewing = true;
    }

    private void RevertPreview()
    {
        RichTextBox? editor = GetActiveEditor();
        if (editor == null || !_previewing)
        {
            return;
        }

        if (_previewFont != null)
        {
            editor.SelectionFont = _previewFont;
        }

        editor.SelectionColor = _previewColor;
        _previewing = false;
    }

    private void CommitPreview() => _previewing = false;

    private void ApplyStylePreset(int index)
    {
        RichTextBox? editor = GetActiveEditor();
        if (editor == null)
        {
            return;
        }

        Font baseFont = editor.SelectionFont ?? editor.Font;
        switch (index)
        {
            case 1:
                editor.SelectionFont = new Font(@"Cambria", 16f, FontStyle.Bold);
                editor.SelectionColor = Color.FromArgb(0, 51, 102);
                break;
            case 2:
                editor.SelectionFont = new Font(baseFont.FontFamily, 11f, FontStyle.Italic);
                editor.SelectionColor = Color.FromArgb(102, 102, 102);
                break;
            case 3:
                editor.SelectionFont = new Font(baseFont.FontFamily, baseFont.Size, FontStyle.Italic | FontStyle.Bold);
                editor.SelectionColor = Color.Maroon;
                break;
            case 4:
                editor.SelectionFont = new Font(@"Consolas", 10f, FontStyle.Regular);
                editor.SelectionColor = Color.Teal;
                break;
            default:
                editor.SelectionFont = new Font(@"Segoe UI", 10f, FontStyle.Regular);
                editor.SelectionColor = Color.Black;
                break;
        }

        SyncCommandChecks();
    }

    private void SyncCommandChecks()
    {
        RichTextBox? editor = GetActiveEditor();
        FontStyle style = (editor?.SelectionFont ?? editor?.Font)?.Style ?? FontStyle.Regular;
        _cmdBold.Checked = style.HasFlag(FontStyle.Bold);
        _cmdItalic.Checked = style.HasFlag(FontStyle.Italic);
        _cmdUnderline.Checked = style.HasFlag(FontStyle.Underline);
    }

    private static void LoadSampleText(RichTextBox editor)
    {
        editor.Clear();
        editor.SelectionFont = new Font(@"Segoe UI", 10f, FontStyle.Regular);
        editor.SelectionColor = Color.Black;
        editor.AppendText(@"Select this paragraph to fade in the Mini Toolbar, then hover the faded bar until it becomes opaque. Click Bold or Italic without moving the caret.");
        editor.AppendText(Environment.NewLine + Environment.NewLine);
        editor.SelectionFont = new Font(@"Cambria", 16f, FontStyle.Bold);
        editor.SelectionColor = Color.FromArgb(0, 51, 102);
        editor.AppendText(@"Heading sample");
        editor.AppendText(Environment.NewLine);
        editor.SelectionFont = new Font(@"Segoe UI", 10f, FontStyle.Regular);
        editor.SelectionColor = Color.Black;
        editor.AppendText(@"Right-click here for the Mini Toolbar paired with Cut / Copy / Paste and the style gallery. Hover a gallery tile for live preview.");
        editor.Select(0, 0);
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        _cmdBold.ImageSmall = CreateLetterGlyph(@"B", FontStyle.Bold);
        _cmdItalic.ImageSmall = CreateLetterGlyph(@"I", FontStyle.Italic);
        _cmdUnderline.ImageSmall = CreateLetterGlyph(@"U", FontStyle.Underline);
        _cmdFontColor.ImageSmall = CreateFontColorGlyph(_fontColor);
        _cmdGrowFont.ImageSmall = CreateFontStepGlyph(true);
        _cmdShrinkFont.ImageSmall = CreateFontStepGlyph(false);
        _cmdClearFormatting.ImageSmall = CreateClearFormattingGlyph();
        foreach (KryptonMiniToolbar toolbar in new[] { _enhancedMenu.MiniToolbar, _selectionToolbar })
        {
            foreach (KryptonMiniToolbarItemBase item in toolbar.Items)
            {
                switch (item.Tag as string)
                {
                    case @"Bold":
                        item.Image = _cmdBold.ImageSmall;
                        break;
                    case @"Italic":
                        item.Image = _cmdItalic.ImageSmall;
                        break;
                    case @"Underline":
                        item.Image = _cmdUnderline.ImageSmall;
                        break;
                    case @"Color":
                        item.Image = _cmdFontColor.ImageSmall;
                        break;
                    case @"Grow":
                        item.Image = _cmdGrowFont.ImageSmall;
                        break;
                    case @"Shrink":
                        item.Image = _cmdShrinkFont.ImageSmall;
                        break;
                    case @"Clear":
                        item.Image = _cmdClearFormatting.ImageSmall;
                        break;
                }
            }
        }
    }

    private Image CreateLetterGlyph(string letter, FontStyle style)
    {
        var bmp = new Bitmap(16, 16);
        using (var g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.Clear(Color.Transparent);
            Color color = GetGlyphColor();
            var drawStyle = style & ~FontStyle.Underline;
            using var font = new Font(@"Segoe UI", 9.75f, drawStyle == FontStyle.Regular ? FontStyle.Bold : drawStyle);
            using var textBrush = new SolidBrush(color);
            using var format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            g.DrawString(letter, font, textBrush, new RectangleF(0, -1, 16, 16), format);
            if (style.HasFlag(FontStyle.Underline))
            {
                using var pen = new Pen(color, 1.5f);
                g.DrawLine(pen, 3, 14, 13, 14);
            }
        }

        _ownedImages.Add(bmp);
        return bmp;
    }

    private Image CreateFontColorGlyph(Color underline)
    {
        var bmp = new Bitmap(16, 16);
        using (var g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.Clear(Color.Transparent);
            using var font = new Font(@"Segoe UI", 9f, FontStyle.Bold);
            using var textBrush = new SolidBrush(GetGlyphColor());
            using var format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Near
            };
            g.DrawString(@"A", font, textBrush, new RectangleF(0, -1, 16, 12), format);
            using var fill = new SolidBrush(underline);
            g.FillRectangle(fill, 2, 13, 12, 3);
        }

        _ownedImages.Add(bmp);
        return bmp;
    }

    private Image CreateFontStepGlyph(bool increase)
    {
        var bmp = new Bitmap(16, 16);
        using (var g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.Clear(Color.Transparent);
            Color color = GetGlyphColor();
            using var font = new Font(@"Segoe UI", increase ? 9f : 7.5f, FontStyle.Bold);
            using var textBrush = new SolidBrush(color);
            using var format = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            };
            g.DrawString(@"A", font, textBrush, new RectangleF(-1, 0, 12, 16), format);
            using var pen = new Pen(color, 1.25f);
            using var fill = new SolidBrush(color);
            Point[] arrow = increase
                ? new[] { new Point(12, 6), new Point(15, 6), new Point(13, 3) }
                : new[] { new Point(12, 9), new Point(15, 9), new Point(13, 12) };
            g.FillPolygon(fill, arrow);
            g.DrawPolygon(pen, arrow);
        }

        _ownedImages.Add(bmp);
        return bmp;
    }

    private Image CreateClearFormattingGlyph()
    {
        var bmp = new Bitmap(16, 16);
        using (var g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.Clear(Color.Transparent);
            Color color = GetGlyphColor();
            using var font = new Font(@"Segoe UI", 8f, FontStyle.Bold);
            using var textBrush = new SolidBrush(color);
            using var format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Near
            };
            g.DrawString(@"A", font, textBrush, new RectangleF(0, 0, 16, 12), format);
            using var eraser = new SolidBrush(GetPaletteBack(PaletteBackStyle.ButtonStandalone, PaletteState.Tracking, Color.FromArgb(232, 80, 120)));
            g.FillRectangle(eraser, 3, 12, 10, 3);
            using var strike = new Pen(GetPaletteBack(PaletteBackStyle.ButtonStandalone, PaletteState.Pressed, Color.FromArgb(192, 57, 43)), 1.5f);
            g.DrawLine(strike, 3, 13, 13, 5);
        }

        _ownedImages.Add(bmp);
        return bmp;
    }

    private static Color GetGlyphColor() =>
        GetPaletteContent(PaletteContentStyle.ButtonLowProfile, PaletteState.Normal, SystemColors.ControlText);

    private static Color GetPaletteContent(PaletteContentStyle style, PaletteState state, Color fallback)
    {
        PaletteBase? palette = KryptonManager.CurrentGlobalPalette;
        Color color = palette?.GetContentShortTextColor1(style, state) ?? Color.Empty;
        return color.IsEmpty ? fallback : color;
    }

    private static Color GetPaletteBack(PaletteBackStyle style, PaletteState state, Color fallback)
    {
        PaletteBase? palette = KryptonManager.CurrentGlobalPalette;
        Color color = palette?.GetBackColor1(style, state) ?? Color.Empty;
        return color.IsEmpty ? fallback : color;
    }

    private static Color GetPaletteBorder(PaletteBorderStyle style, PaletteState state, Color fallback)
    {
        PaletteBase? palette = KryptonManager.CurrentGlobalPalette;
        Color color = palette?.GetBorderColor1(style, state) ?? Color.Empty;
        return color.IsEmpty ? fallback : color;
    }

    private Image CreateStylePreview(string sample, string family, float emSize, FontStyle style, Color color, Size size)
    {
        var bmp = new Bitmap(size.Width, size.Height);
        using (var g = Graphics.FromImage(bmp))
        using (var font = new Font(family, emSize, style))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.Clear(GetPaletteBack(PaletteBackStyle.InputControlStandalone, PaletteState.Normal, Color.White));
            using (var border = new Pen(GetPaletteBorder(PaletteBorderStyle.InputControlStandalone, PaletteState.Normal, Color.FromArgb(196, 196, 196))))
            {
                g.DrawRectangle(border, 0, 0, size.Width - 1, size.Height - 1);
            }

            using var textBrush = new SolidBrush(color);
            using var format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.None
            };
            g.DrawString(sample, font, textBrush, new RectangleF(1, 1, size.Width - 2, size.Height - 2), format);
        }

        _ownedImages.Add(bmp);
        return bmp;
    }

    private Image CreateMiniStyleSwatch(Image source)
    {
        var bmp = new Bitmap(_miniStyleImages.ImageSize.Width, _miniStyleImages.ImageSize.Height);
        using (var g = Graphics.FromImage(bmp))
        {
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.Clear(GetPaletteBack(PaletteBackStyle.InputControlStandalone, PaletteState.Normal, Color.White));
            g.DrawImage(source, 0, 0, bmp.Width, bmp.Height);
        }

        _ownedImages.Add(bmp);
        return bmp;
    }

    private void SetStatus(string message) => kwlblStatus.Text = message;
}
