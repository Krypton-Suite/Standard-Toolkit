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
/// Demo for <see cref="KryptonRadialMenu"/> (issue #4172): native items, nested submenus,
/// slider/colour/font editors, <see cref="KryptonCommand"/> binding, and context-menu import.
/// </summary>
public partial class RadialMenuDemo : KryptonForm
{
    private readonly KryptonRadialMenu _radialMenu = new();
    private readonly KryptonRadialMenu _importedMenu = new();
    private readonly KryptonContextMenu _sourceContextMenu = new();
    private readonly KryptonCommand _cutCommand = new();
    private readonly KryptonCommand _copyCommand = new();
    private readonly List<Image> _ownedImages = [];

    public RadialMenuDemo()
    {
        InitializeComponent();
        BuildMenus();
        PopulateAnimationCombo();
        PopulateDisplayStyleCombo();
        PopulateImageSizeCombo();
        kchkShowShadow.Checked = true;
        kchkShowCheckedGlyph.Checked = true;
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        foreach (var image in _ownedImages)
        {
            image.Dispose();
        }

        _ownedImages.Clear();
        base.OnFormClosed(e);
    }

    private void PopulateAnimationCombo()
    {
        kcmbAnimation.Items.Clear();
        foreach (KryptonRadialMenuAnimationStyle style in Enum.GetValues(typeof(KryptonRadialMenuAnimationStyle)))
        {
            kcmbAnimation.Items.Add(style);
        }

        kcmbAnimation.SelectedItem = KryptonRadialMenuAnimationStyle.Sweep;
        ApplyAnimationStyle(KryptonRadialMenuAnimationStyle.Sweep);
    }

    private void PopulateDisplayStyleCombo()
    {
        kcmbDisplayStyle.Items.Clear();
        foreach (KryptonRadialMenuDisplayStyle style in Enum.GetValues(typeof(KryptonRadialMenuDisplayStyle)))
        {
            kcmbDisplayStyle.Items.Add(style);
        }

        kcmbDisplayStyle.SelectedItem = KryptonRadialMenuDisplayStyle.ImageAboveText;
    }

    private void PopulateImageSizeCombo()
    {
        kcmbImageSize.Items.Clear();
        foreach (var size in new[] { 16, 20, 24, 32 })
        {
            kcmbImageSize.Items.Add(size);
        }

        kcmbImageSize.SelectedItem = 24;
    }

    private void ApplyAnimationStyle(KryptonRadialMenuAnimationStyle style)
    {
        _radialMenu.AnimationStyle = style;
        _importedMenu.AnimationStyle = style;
    }

    private Image CreateSliceIcon(Color fill, string glyph)
    {
        var bmp = new Bitmap(32, 32);
        using (var g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);
            using (var brush = new SolidBrush(fill))
            {
                g.FillEllipse(brush, 2, 2, 28, 28);
            }

            using var font = new Font(@"Segoe UI", 11f, FontStyle.Bold);
            using var textBrush = new SolidBrush(Color.White);
            var format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            g.DrawString(glyph, font, textBrush, new RectangleF(0, 0, 32, 32), format);
            format.Dispose();
        }

        _ownedImages.Add(bmp);
        return bmp;
    }

    private void BuildMenus()
    {
        _cutCommand.Text = @"Cut";
        _cutCommand.ImageSmall = CreateSliceIcon(Color.FromArgb(192, 57, 43), @"✂");
        _cutCommand.Execute += (_, _) => AppendLog(@"Command: Cut");
        _copyCommand.Text = @"Copy";
        _copyCommand.ImageSmall = CreateSliceIcon(Color.FromArgb(41, 128, 185), @"❐");
        _copyCommand.Execute += (_, _) => AppendLog(@"Command: Copy");

        var edit = new KryptonRadialMenuItem(@"Edit")
        {
            Image = CreateSliceIcon(Color.FromArgb(52, 73, 94), @"✎"),
            Items =
            {
                new KryptonRadialMenuItem(@"Cut") { KryptonCommand = _cutCommand },
                new KryptonRadialMenuItem(@"Copy") { KryptonCommand = _copyCommand },
                new KryptonRadialMenuItem(@"Paste", (_, _) => AppendLog(@"Paste clicked"))
                {
                    Image = CreateSliceIcon(Color.FromArgb(39, 174, 96), @"📋")
                }
            }
        };

        _radialMenu.Items.Add(edit);
        _radialMenu.Items.Add(new KryptonRadialMenuItem(@"Bold", (_, _) => AppendLog(@"Bold"))
        {
            Image = CreateSliceIcon(Color.FromArgb(142, 68, 173), @"B"),
            CheckOnClick = true,
            Checked = true,
            ToolTipText = @"Toggle bold formatting"
        });
        _radialMenu.Items.Add(new KryptonRadialMenuSliderItem
        {
            Text = @"Opacity",
            Image = CreateSliceIcon(Color.FromArgb(230, 126, 34), @"◐"),
            Minimum = 0,
            Maximum = 100,
            Value = 75,
            ToolTipText = @"Drag the ring to change opacity"
        });
        _radialMenu.Items.Add(new KryptonRadialMenuColorPaletteItem(ColorScheme.Basic16)
        {
            Text = @"Fill",
            Image = CreateSliceIcon(Color.FromArgb(26, 188, 156), @"■"),
            ToolTipText = @"Open the colour palette ring"
        });
        _radialMenu.Items.Add(new KryptonRadialMenuFontListItem
        {
            Text = @"Font",
            Image = CreateSliceIcon(Color.FromArgb(52, 152, 219), @"A"),
            ToolTipText = @"Open the font list ring"
        });
        _radialMenu.Items.Add(new KryptonRadialMenuTextItem
        {
            Label = @"Note",
            Text = @"Hello",
            Image = CreateSliceIcon(Color.FromArgb(155, 89, 182), @"T"),
            ToolTipText = @"Open the text editor ring"
        });
        _radialMenu.Items.Add(new KryptonRadialMenuCalendarItem
        {
            Text = @"Date",
            SelectedDate = DateTime.Today,
            Image = CreateSliceIcon(Color.FromArgb(22, 160, 133), @"D"),
            ToolTipText = @"Open the calendar editor ring"
        });
        _radialMenu.Items.Add(new KryptonRadialMenuItem(@"Disabled")
        {
            Image = CreateSliceIcon(Color.FromArgb(127, 140, 141), @"∅"),
            Enabled = false
        });

        // Demonstrate overflow paging when more than six sectors are present.
        _radialMenu.MaxVisibleItems = 6;

        edit.ToolTipValues.EnableToolTips = true;
        edit.ToolTipValues.Heading = @"Edit";
        edit.ToolTipValues.Description = @"Opens cut / copy / paste commands";
        foreach (KryptonRadialMenuItemBase child in edit.Items)
        {
            if (child is KryptonRadialMenuItem leaf && string.IsNullOrEmpty(leaf.ToolTipText))
            {
                leaf.ToolTipText = leaf.Text;
            }
        }

        // Context menu used as the import source for the bridge (live-synced).
        var items = new KryptonContextMenuItems();
        items.Items.Add(new KryptonContextMenuItem(@"Open", CreateSliceIcon(Color.FromArgb(41, 128, 185), @"O"), (_, _) => AppendLog(@"Imported: Open")));
        items.Items.Add(new KryptonContextMenuItem(@"Save", CreateSliceIcon(Color.FromArgb(39, 174, 96), @"S"), (_, _) => AppendLog(@"Imported: Save")));
        var docsLink = new KryptonContextMenuLinkLabel(@"Docs");
        docsLink.Click += (_, _) => AppendLog(@"Imported: LinkLabel");
        items.Items.Add(docsLink);
        var nested = new KryptonContextMenuItem(@"More", CreateSliceIcon(Color.FromArgb(142, 68, 173), @"…"), null);
        nested.Items.Add(new KryptonContextMenuItem(@"Properties", CreateSliceIcon(Color.FromArgb(230, 126, 34), @"P"), (_, _) => AppendLog(@"Imported: Properties")));
        items.Items.Add(nested);
        items.Items.Add(new KryptonContextMenuSeparator());
        items.Items.Add(new KryptonContextMenuTextBox { Text = @"Sample text box value" });
        var combo = new KryptonContextMenuComboBox();
        combo.Items.Add(@"Alpha");
        combo.Items.Add(@"Beta");
        combo.Items.Add(@"Gamma");
        combo.SelectedIndex = 1;
        items.Items.Add(combo);
        items.Items.Add(new KryptonContextMenuProgressBar { Minimum = 0, Maximum = 100, Value = 42 });
        items.Items.Add(new KryptonContextMenuMonthCalendar());
        items.Items.Add(new KryptonContextMenuColorColumns(ColorScheme.Basic16));
        _sourceContextMenu.Items.Add(items);

        _importedMenu.ImportFrom(_sourceContextMenu, liveSync: true);
        _importedMenu.ItemClick += (_, e) => AppendLog($@"Imported ItemClick: {e.Item}");

        _radialMenu.ItemClick += (_, e) => AppendLog($@"ItemClick: {e.Item}");
        _radialMenu.CenterButtonClick += (_, _) => AppendLog(@"Centre button (close)");
        if (_radialMenu.Items[2] is KryptonRadialMenuSliderItem slider)
        {
            slider.ValueChanged += (_, _) => AppendLog($@"Slider: {slider.Value}");
        }

        if (_radialMenu.Items[3] is KryptonRadialMenuColorPaletteItem colors)
        {
            colors.SelectedColorChanged += (_, e) => AppendLog($@"Color: {e.Color}");
        }

        if (_radialMenu.Items[4] is KryptonRadialMenuFontListItem fonts)
        {
            fonts.SelectedFontChanged += (_, _) => AppendLog($@"Font: {fonts.SelectedFont?.Name}");
        }

        if (_radialMenu.Items[5] is KryptonRadialMenuTextItem note)
        {
            note.TextChanged += (_, _) => AppendLog($@"Text: {note.Text}");
        }

        if (_radialMenu.Items[6] is KryptonRadialMenuCalendarItem date)
        {
            date.SelectedDateChanged += (_, _) => AppendLog($@"Date: {date.SelectedDate:d}");
        }
    }

    private void kpnlSurface_MouseUp(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right)
        {
            return;
        }

        if (krdoImported.Checked && kchkPreferRadial.Checked)
        {
            KryptonRadialMenuPresenter.PreferRadialContextMenus = true;
            KryptonRadialMenuPresenter.Show(_sourceContextMenu, this, e.Location);
            return;
        }

        var menu = krdoNative.Checked ? _radialMenu : _importedMenu;
        menu.Show(this, e.Location);
    }

    private void kbtnShowAtCursor_Click(object? sender, EventArgs e)
    {
        if (krdoImported.Checked && kchkPreferRadial.Checked)
        {
            KryptonRadialMenuPresenter.PreferRadialContextMenus = true;
            KryptonRadialMenuPresenter.Show(_sourceContextMenu, this, PointToScreen(Point.Empty));
            return;
        }

        var menu = krdoNative.Checked ? _radialMenu : _importedMenu;
        menu.Show(this);
    }

    private void kchkAllowMove_CheckedChanged(object? sender, EventArgs e)
    {
        _radialMenu.AllowMove = kchkAllowMove.Checked;
        _importedMenu.AllowMove = kchkAllowMove.Checked;
    }

    private void kcmbAnimation_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kcmbAnimation.SelectedItem is KryptonRadialMenuAnimationStyle style)
        {
            ApplyAnimationStyle(style);
        }
    }

    private void kcmbDisplayStyle_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kcmbDisplayStyle.SelectedItem is KryptonRadialMenuDisplayStyle style)
        {
            _radialMenu.DisplayStyle = style;
            _importedMenu.DisplayStyle = style;
        }
    }

    private void kcmbImageSize_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (kcmbImageSize.SelectedItem is int size)
        {
            _radialMenu.ItemImageSize = size;
            _importedMenu.ItemImageSize = size;
        }
    }

    private void kchkShowShadow_CheckedChanged(object? sender, EventArgs e)
    {
        _radialMenu.ShowShadow = kchkShowShadow.Checked;
        _importedMenu.ShowShadow = kchkShowShadow.Checked;
    }

    private void kchkShowCheckedGlyph_CheckedChanged(object? sender, EventArgs e)
    {
        _radialMenu.ShowCheckedGlyph = kchkShowCheckedGlyph.Checked;
        _importedMenu.ShowCheckedGlyph = kchkShowCheckedGlyph.Checked;
    }

    private void kchkPreferRadial_CheckedChanged(object? sender, EventArgs e)
    {
        KryptonRadialMenuPresenter.PreferRadialContextMenus = kchkPreferRadial.Checked;
    }

    private void AppendLog(string message)
    {
        var line = $"{DateTime.Now:HH:mm:ss}  {message}";
        if (string.IsNullOrEmpty(ktxtLog.Text))
        {
            ktxtLog.Text = line;
        }
        else
        {
            ktxtLog.Text = line + Environment.NewLine + ktxtLog.Text;
        }
    }
}
