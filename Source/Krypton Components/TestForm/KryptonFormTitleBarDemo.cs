#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive interactive demo for <see cref="KryptonFormTitleBar"/>.
///
/// Demonstrates:
///   - Adding button specs to the title bar at runtime
///   - Removing individual or all buttons
///   - Toggle (checked) button behaviour
///   - Drop-down context menu on a title bar button
///   - KryptonCommand binding — enabled/checked sync
///   - Visible / Enabled property toggling per button
///   - Attaching and detaching the entire TitleBar component
///   - Theme switching and RTL layout
///   - Event logging (Click, ButtonSpecInserted, ButtonSpecRemoved)
/// </summary>
public partial class KryptonFormTitleBarDemo : KryptonForm
{
    #region Instance Fields

    private readonly KryptonFormTitleBar _titleBar;
    private readonly KryptonCommand _saveCommand;

    // Individual specs kept as fields for per-button control
    private ButtonSpecAny? _specHome;
    private ButtonSpecAny? _specSave;
    private ButtonSpecAny? _specPin;
    private ButtonSpecAny? _specDrop;

    #endregion

    #region Identity

    public KryptonFormTitleBarDemo()
    {
        InitializeComponent();

        _titleBar = new KryptonFormTitleBar();
        _saveCommand = new KryptonCommand
        {
            Text = "Save",
            Enabled = true,
            Checked = false
        };

        WireEvents();
        BuildInitialTitleBarButtons();

        TitleBar = _titleBar;
        LogEvent("Demo started — KryptonFormTitleBar attached.");
    }

    #endregion

    #region Setup

    private void WireEvents()
    {
        _titleBar.ButtonSpecs.Inserted += (s, e) => LogEvent($"ButtonSpecInserted: '{e.ButtonSpec.ToolTipTitle}' at index {e.Index}");
        _titleBar.ButtonSpecs.Removed += (s, e) => LogEvent($"ButtonSpecRemoved: '{e.ButtonSpec.ToolTipTitle}' at index {e.Index}");
        _saveCommand.Execute += (s, e) => LogEvent("KryptonCommand.Execute fired (Save)");
    }

    private void BuildInitialTitleBarButtons()
    {
        // --- Home button (push) ---
        _specHome = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Generic,
            Image = CreateGlyphBitmap(Color.SteelBlue, '\u2302'),     // ⌂
            ToolTipTitle = "Home",
            ToolTipBody = "Navigate to the start screen (push button demo).",
            Enabled = ButtonEnabled.True
        };
        _specHome.Click += OnHomeClicked;

        // --- Save button (driven by KryptonCommand) ---
        _specSave = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Generic,
            Image = CreateGlyphBitmap(Color.SeaGreen, '\u2666'),    // ♦
            ToolTipTitle = "Save",
            ToolTipBody = "Save the current document (KryptonCommand demo).",
            KryptonCommand = _saveCommand
        };
        _specSave.Click += OnSaveClicked;

        // --- Pin toggle button ---
        _specPin = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Generic,
            Image = CreateGlyphBitmap(Color.OrangeRed, '\u2605'),     // ★
            ToolTipTitle = "Pin window",
            ToolTipBody = "Keep this window on top (toggle button demo).",
            Checked = ButtonCheckState.Unchecked
        };
        _specPin.Click += OnPinClicked;

        // --- Drop-down button ---
        var dropMenu = new KryptonContextMenu();
        var items = new KryptonContextMenuItems();
        var item1 = new KryptonContextMenuItem("Option A");
        var item2 = new KryptonContextMenuItem("Option B");
        var item3 = new KryptonContextMenuItem("Option C");
        item1.Click += (s, e) => LogEvent("Drop-down: Option A selected");
        item2.Click += (s, e) => LogEvent("Drop-down: Option B selected");
        item3.Click += (s, e) => LogEvent("Drop-down: Option C selected");
        items.Items.AddRange([item1, item2, item3]);
        dropMenu.Items.Add(items);

        _specDrop = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Generic,
            Image = CreateGlyphBitmap(Color.SlateBlue, '\u2630'), // ☰
            ToolTipTitle = "Options",
            ToolTipBody = "Show option menu (drop-down button demo).",
            ShowDrop = true,
            KryptonContextMenu = dropMenu
        };

        _titleBar.ButtonSpecs.AddRange([_specHome, _specSave, _specPin, _specDrop]);
    }

    #endregion

    #region Title-bar button handlers

    private void OnHomeClicked(object? sender, EventArgs e)
    {
        LogEvent("Home clicked — navigating to start.");
        klblStatus.Text = "Home pressed.";
    }

    private void OnSaveClicked(object? sender, EventArgs e)
    {
        LogEvent("Save clicked via KryptonCommand.");
        klblStatus.Text = "Document saved (simulated).";
    }

    private void OnPinClicked(object? sender, EventArgs e)
    {
        if (_specPin == null) return;
        bool pinned = _specPin.Checked == ButtonCheckState.Checked;
        TopMost = pinned;
        klblStatus.Text = pinned ? "Window pinned on top." : "Window unpinned.";
        LogEvent($"Pin toggled — TopMost = {pinned}");
    }

    #endregion

    #region Panel button handlers

    private void kbtnInsertStandardItems_Click(object sender, EventArgs e)
    {
        _titleBar.InsertStandardItems();
        LogEvent("Insert Standard Items — added File, Edit, Tools, Help (menus) + New, Open, Save, Cut, Copy, Paste, Print, etc. (icons).");
    }

    private void kbtnAddButton_Click(object sender, EventArgs e)
    {
        var spec = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Generic,
            Image = CreateGlyphBitmap(Color.DarkGoldenrod, '\u2605'), // ★
            ToolTipTitle = $"Extra #{_titleBar.ButtonSpecs.Count + 1}",
            Enabled = ButtonEnabled.True
        };
        spec.Click += (s, ev) => LogEvent($"Extra button clicked: '{((ButtonSpecAny)s!).ToolTipTitle}'");
        _titleBar.ButtonSpecs.Add(spec);
    }

    private void kbtnRemoveLast_Click(object sender, EventArgs e)
    {
        int count = _titleBar.ButtonSpecs.Count;
        if (count == 0)
        {
            LogEvent("No buttons to remove.");
            return;
        }
        _titleBar.ButtonSpecs.RemoveAt(count - 1);
    }

    private void kbtnClearAll_Click(object sender, EventArgs e)
    {
        _titleBar.ButtonSpecs.Clear();
        _specHome = _specSave = _specPin = _specDrop = null;
        LogEvent("All title bar buttons cleared.");
    }

    private void kbtnRebuild_Click(object sender, EventArgs e)
    {
        _titleBar.ButtonSpecs.Clear();
        _specHome = _specSave = _specPin = _specDrop = null;
        BuildInitialTitleBarButtons();
        LogEvent("Title bar buttons rebuilt.");
    }

    private void kbtnToggleHomeVisible_Click(object sender, EventArgs e)
    {
        if (_specHome == null) { LogEvent("Home spec not present."); return; }
        _specHome.Visible = !_specHome.Visible;
        LogEvent($"Home button Visible = {_specHome.Visible}");
    }

    private void kbtnToggleHomeEnabled_Click(object sender, EventArgs e)
    {
        if (_specHome == null) { LogEvent("Home spec not present."); return; }
        _specHome.Enabled = _specHome.Enabled == ButtonEnabled.True
            ? ButtonEnabled.False
            : ButtonEnabled.True;
        LogEvent($"Home button Enabled = {_specHome.Enabled}");
    }

    private void kbtnToggleSaveCommand_Click(object sender, EventArgs e)
    {
        _saveCommand.Enabled = !_saveCommand.Enabled;
        LogEvent($"KryptonCommand.Enabled = {_saveCommand.Enabled} → Save button reflects this automatically.");
    }

    private void kbtnDetachTitleBar_Click(object sender, EventArgs e)
    {
        if (TitleBar != null)
        {
            TitleBar = null;
            kbtnDetachTitleBar.Values.Text = "Attach TitleBar";
            LogEvent("TitleBar detached — caption reverted to standard.");
        }
        else
        {
            TitleBar = _titleBar;
            kbtnDetachTitleBar.Values.Text = "Detach TitleBar";
            LogEvent("TitleBar re-attached.");
        }
    }

    private void kbtnToggleRtl_Click(object sender, EventArgs e)
    {
        bool newRtl = RightToLeftLayout == false;
        RightToLeft = newRtl ? RightToLeft.Yes : RightToLeft.No;
        RightToLeftLayout = newRtl;
        LogEvent($"RightToLeftLayout = {newRtl}  (buttons mirror automatically)");
    }

    private void kbtnClearLog_Click(object sender, EventArgs e)
    {
        klbLog.Items.Clear();
    }

    private void kthemeCombo_SelectedPaletteChanged(object sender, EventArgs e)
    {
        LogEvent($"Theme changed — title bar buttons repaint automatically.");
    }

    #endregion

    #region Helpers

    private void LogEvent(string message)
    {
        string entry = $"[{DateTime.Now:HH:mm:ss.fff}]  {message}";
        klbLog.Items.Insert(0, entry);
        klblStatus.Text = message;
    }

    /// <summary>
    /// Renders a Unicode glyph character onto a 16x16 bitmap for use as a button image.
    /// This avoids any embedded resource dependency and works identically at runtime and
    /// in the designer.
    /// </summary>
    private static Bitmap CreateGlyphBitmap(Color colour, char glyph)
    {
        var bmp = new Bitmap(16, 16);
        using var g = Graphics.FromImage(bmp);
        g.Clear(Color.Transparent);
        using var brush = new SolidBrush(colour);
        using var font = new Font("Segoe UI", 9f, FontStyle.Bold, GraphicsUnit.Point);
        string text = glyph.ToString();
        var size = g.MeasureString(text, font);
        g.DrawString(text, font, brush,
            (16 - size.Width) / 2f,
            (16 - size.Height) / 2f);
        return bmp;
    }

    #endregion

    #region Form events

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _saveCommand.Dispose();
        base.OnFormClosed(e);
    }

    #endregion

    private void kbtnExit_Click(object sender, EventArgs e)
    {
        Close();
    }
}
