#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Ribbon;
using Krypton.Toolkit;

namespace TestForm;

/// <summary>
/// Demonstrates Office-style logical RTL for <see cref="KryptonRibbon"/> (Issue #2382).
/// </summary>
public partial class RibbonRtlDemo : KryptonForm
{
    private KryptonRibbon? _ribbon;
    private KryptonCheckBox? _chkRtl;
    private KryptonCheckBox? _chkQatBelow;
    private KryptonCheckBox? _chkOrb;
    private KryptonLabel? _lblStatus;
    private ImageList? _galleryImages;
    private readonly KryptonManager _manager = new();
    private PaletteMode _savedPaletteMode;

    public RibbonRtlDemo()
    {
        InitializeComponent();
        _savedPaletteMode = _manager.GlobalPaletteMode;
        BuildUi();
        UpdateStatus();
    }

    private void BuildUi()
    {
        _ribbon = CreateRibbon();
        ShowNotification();

        var instructions = new KryptonLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 88,
            LabelStyle = LabelStyle.NormalControl,
            Values =
            {
                Text = "Issue #2382 — Toggle RightToLeft + RightToLeftLayout on this form. " +
                       "The ribbon syncs layout from the form. Tabs, groups, QAT, clusters, gallery items, " +
                       "and the File app button pack from the start (right) edge. Text and glyphs stay readable. " +
                       "Press Alt for key tips; Left/Right follow visual start/end. Use QAT below to move the toolbar."
            }
        };

        var optionsPanel = new KryptonPanel
        {
            Dock = DockStyle.Top,
            Height = 40,
            PanelBackStyle = PaletteBackStyle.PanelClient
        };

        _chkRtl = new KryptonCheckBox
        {
            Text = "RightToLeft + RightToLeftLayout",
            Checked = false,
            Location = new Point(12, 10),
            AutoSize = true
        };
        _chkRtl.CheckedChanged += OnRtlCheckedChanged;

        _chkQatBelow = new KryptonCheckBox
        {
            Text = "QAT below ribbon",
            Checked = false,
            Location = new Point(280, 10),
            AutoSize = true
        };
        _chkQatBelow.CheckedChanged += OnQatBelowCheckedChanged;

        _chkOrb = new KryptonCheckBox
        {
            Text = "Office 2007 orb",
            Checked = false,
            Location = new Point(440, 10),
            AutoSize = true
        };
        _chkOrb.CheckedChanged += OnOrbCheckedChanged;

        optionsPanel.Controls.Add(_chkRtl);
        optionsPanel.Controls.Add(_chkQatBelow);
        optionsPanel.Controls.Add(_chkOrb);

        _lblStatus = new KryptonLabel
        {
            Dock = DockStyle.Bottom,
            AutoSize = false,
            Height = 28,
            LabelStyle = LabelStyle.NormalControl
        };

        var filler = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            PanelBackStyle = PaletteBackStyle.PanelClient
        };

        var body = new KryptonLabel
        {
            Dock = DockStyle.Fill,
            LabelStyle = LabelStyle.NormalControl,
            Values =
            {
                Text = "Client area. Switch RTL and confirm:\r\n" +
                       "• Home is the rightmost tab, Clipboard is the rightmost group.\r\n" +
                       "• QAT sits after the File button or orb on the start (right) edge when above.\r\n" +
                       "• Office 2007 orb is on the start edge; QAT tucks under it from the far side.\r\n" +
                       "• Dialog launcher is on the far (left) side of the group title.\r\n" +
                       "• Gallery items pack from the start; scroll buttons sit on the far edge.\r\n" +
                       "• Notification close sits on the far edge. Hosted editors inherit RightToLeft."
            }
        };
        filler.Controls.Add(body);

        Controls.Add(filler);
        Controls.Add(_lblStatus);
        Controls.Add(_ribbon);
        Controls.Add(optionsPanel);
        Controls.Add(instructions);
    }

    private KryptonRibbon CreateRibbon()
    {
        var ribbon = new KryptonRibbon
        {
            Dock = DockStyle.Top,
            QATLocation = QATLocation.Above
        };
        ribbon.RibbonFileAppButton.AppButtonVisible = true;

        var homeTab = new KryptonRibbonTab { Text = @"Home" };

        var clipboardGroup = new KryptonRibbonGroup
        {
            TextLine1 = @"Clipboard",
            DialogBoxLauncher = true
        };
        var triple = new KryptonRibbonGroupTriple();
        triple.Items?.Add(new KryptonRibbonGroupButton { TextLine1 = @"Paste" });
        triple.Items?.Add(new KryptonRibbonGroupButton { TextLine1 = @"Cut" });
        triple.Items?.Add(new KryptonRibbonGroupButton { TextLine1 = @"Copy" });
        clipboardGroup.Items.Add(triple);
        homeTab.Groups.Add(clipboardGroup);

        var fontGroup = new KryptonRibbonGroup { TextLine1 = @"Font" };
        var cluster = new KryptonRibbonGroupCluster();
        cluster.Items.Add(new KryptonRibbonGroupClusterButton { TextLine = @"B" });
        cluster.Items.Add(new KryptonRibbonGroupClusterButton { TextLine = @"I" });
        cluster.Items.Add(new KryptonRibbonGroupClusterButton { TextLine = @"U" });
        var fontLines = new KryptonRibbonGroupLines();
        fontLines.Items?.Add(cluster);
        fontGroup.Items.Add(fontLines);
        homeTab.Groups.Add(fontGroup);

        var editGroup = new KryptonRibbonGroup { TextLine1 = @"Edit" };
        var editTriple = new KryptonRibbonGroupTriple();
        editTriple.Items?.Add(new KryptonRibbonGroupTextBox { Text = @"Sample" });
        editTriple.Items?.Add(new KryptonRibbonGroupCheckBox { TextLine1 = @"Wrap" });
        editTriple.Items?.Add(new KryptonRibbonGroupButton { TextLine1 = @"Find" });
        editGroup.Items.Add(editTriple);
        homeTab.Groups.Add(editGroup);

        ribbon.RibbonTabs.Add(homeTab);

        var insertTab = new KryptonRibbonTab { Text = @"Insert" };
        var insertGroup = new KryptonRibbonGroup { TextLine1 = @"Illustrations" };
        var insertTriple = new KryptonRibbonGroupTriple();
        insertTriple.Items?.Add(new KryptonRibbonGroupButton { TextLine1 = @"Picture" });
        insertTriple.Items?.Add(new KryptonRibbonGroupButton { TextLine1 = @"Shape" });
        insertGroup.Items.Add(insertTriple);
        insertTab.Groups.Add(insertGroup);

        var galleryGroup = new KryptonRibbonGroup { TextLine1 = @"Styles" };
        _galleryImages = CreateGalleryImages();
        var gallery = new KryptonRibbonGroupGallery
        {
            ImageList = _galleryImages,
            DropButtonItemWidth = 4,
            TextLine1 = @"Styles"
        };
        galleryGroup.Items.Add(gallery);
        insertTab.Groups.Add(galleryGroup);
        ribbon.RibbonTabs.Add(insertTab);

        var qatSave = new KryptonRibbonQATButton { Text = @"Save" };
        var qatUndo = new KryptonRibbonQATButton { Text = @"Undo" };
        ribbon.QATButtons.Add(qatSave);
        ribbon.QATButtons.Add(qatUndo);

        ribbon.SelectedTab = homeTab;
        return ribbon;
    }

    private static ImageList CreateGalleryImages()
    {
        var images = new ImageList
        {
            ImageSize = new Size(24, 24),
            ColorDepth = ColorDepth.Depth32Bit
        };

        var colors = new[]
        {
            Color.FromArgb(192, 57, 43),
            Color.FromArgb(41, 128, 185),
            Color.FromArgb(39, 174, 96),
            Color.FromArgb(241, 196, 15),
            Color.FromArgb(142, 68, 173),
            Color.FromArgb(22, 160, 133)
        };

        for (var i = 0; i < colors.Length; i++)
        {
            var bmp = new Bitmap(24, 24);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(colors[i]);
                using var brush = new SolidBrush(Color.White);
                g.DrawString((i + 1).ToString(), SystemFonts.DefaultFont, brush, 6, 4);
            }

            images.Images.Add(bmp);
        }

        return images;
    }

    private void ShowNotification()
    {
        if (_ribbon is null)
        {
            return;
        }

        _ribbon.NotificationBar.Type = RibbonNotificationBarType.Information;
        _ribbon.NotificationBar.Text = @"RTL demo — close sits on the far edge when RightToLeftLayout is on.";
        _ribbon.NotificationBar.ShowCloseButton = true;
        _ribbon.NotificationBar.ShowActionButtons = false;
        _ribbon.NotificationBar.Visible = true;
    }

    private void OnRtlCheckedChanged(object? sender, EventArgs e)
    {
        var rtl = _chkRtl is { Checked: true };
        RightToLeft = rtl ? RightToLeft.Yes : RightToLeft.No;
        RightToLeftLayout = rtl;
        UpdateStatus();
    }

    private void OnQatBelowCheckedChanged(object? sender, EventArgs e)
    {
        if (_ribbon is null)
        {
            return;
        }

        _ribbon.QATLocation = _chkQatBelow is { Checked: true }
            ? QATLocation.Below
            : QATLocation.Above;
        UpdateStatus();
    }

    private void OnOrbCheckedChanged(object? sender, EventArgs e)
    {
        if (_chkOrb is { Checked: true })
        {
            _savedPaletteMode = _manager.GlobalPaletteMode;
            _manager.GlobalPaletteMode = PaletteMode.Office2007Blue;
        }
        else if (_savedPaletteMode != PaletteMode.Office2007Blue)
        {
            _manager.GlobalPaletteMode = _savedPaletteMode;
        }
        else
        {
            _manager.GlobalPaletteMode = ThemeManager.DefaultGlobalPalette;
        }

        UpdateStatus();
    }

    private void UpdateStatus()
    {
        if (_lblStatus is null || _ribbon is null)
        {
            return;
        }

        var formRtl = RightToLeftLayout && RightToLeft == RightToLeft.Yes;
        _lblStatus.Values.Text =
            $"Form RTL: {(formRtl ? "Yes" : "No")}  |  Ribbon.RightToLeft={_ribbon.RightToLeft}  |  Ribbon.RightToLeftLayout={_ribbon.RightToLeftLayout}  |  QAT={_ribbon.QATLocation}  |  Shape={_ribbon.StateCommon.RibbonGeneral.GetRibbonShape()}";
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        if (_chkOrb is { Checked: true })
        {
            _manager.GlobalPaletteMode = _savedPaletteMode;
        }

        _manager.Dispose();

        _galleryImages?.Dispose();
        _galleryImages = null;
        base.OnFormClosed(e);
    }
}
