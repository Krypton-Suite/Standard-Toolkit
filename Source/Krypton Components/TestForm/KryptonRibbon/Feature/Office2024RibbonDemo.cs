#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Ribbon;

namespace TestForm;

/// <summary>
/// Issue #4496: Office 2024 underline tabs, flat groups, and a text File tab.
/// Microsoft 365 Blue is included so the previous folder-tab chrome can be compared.
/// </summary>
public partial class Office2024RibbonDemo : KryptonForm
{
    private bool _syncingBevel;
    private bool _syncingContextTitles;
    private bool _syncingPills;
    private bool _syncingGlow;
    private bool _syncingCompact;
    private bool _syncingArrowLocation;
    private bool _syncingBevelSize;
    private bool _syncingGap;

    public Office2024RibbonDemo()
    {
        InitializeComponent();
        ApplyTheme(PaletteMode.Office2024Black);
    }

    private void OnBevelCheckedChanged(object? sender, EventArgs e)
    {
        if (!_syncingBevel)
        {
            SetGroupAreaBevel(_bevel.Checked);
        }
    }

    private void OnContextTitlesCheckedChanged(object? sender, EventArgs e)
    {
        if (!_syncingContextTitles)
        {
            SetShowContextTitles(_contextTitles.Checked);
        }
    }

    private void OnPillsCheckedChanged(object? sender, EventArgs e)
    {
        if (!_syncingPills)
        {
            SetTabMarker(_pills.Checked ? PaletteRibbonTabMarker.Pill : PaletteRibbonTabMarker.Line);
        }
    }

    private void OnGlowCheckedChanged(object? sender, EventArgs e)
    {
        if (!_syncingGlow)
        {
            SetTabMarkerGlow(_glow.Checked);
        }
    }

    private void OnCompactCheckedChanged(object? sender, EventArgs e)
    {
        if (!_syncingCompact)
        {
            SetCompactMode(_compact.Checked);
        }
    }

    private void OnArrowLocationChanged(object? sender, EventArgs e)
    {
        if (!_syncingArrowLocation && _arrowLocation.SelectedIndex >= 0)
        {
            SetCompactButtonLocation(LocationFromIndex(_arrowLocation.SelectedIndex));
        }
    }

    private void OnBevelSizeChanged(object? sender, EventArgs e)
    {
        if (!_syncingBevelSize)
        {
            SetGroupAreaBevelSize((int)_bevelSize.Value);
        }
    }

    private void OnGapChanged(object? sender, EventArgs e)
    {
        if (!_syncingGap)
        {
            SetGroupAreaGap((int)_gap.Value);
        }
    }

    private void OnCompactModeChanged(object? sender, EventArgs e)
    {
        if (_compact.Checked == _ribbon.CompactMode)
        {
            _status.Text = StatusText();
            return;
        }

        _syncingCompact = true;
        _compact.Checked = _ribbon.CompactMode;
        _syncingCompact = false;
        _status.Text = StatusText();
    }

    /// <summary>
    /// Opens the File backstage the same way the File tab does.
    /// </summary>
    public void ShowFileBackstage()
    {
        System.Reflection.MethodInfo? method = typeof(KryptonRibbon).GetMethod(
            @"TryToggleBackstageView",
            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        method?.Invoke(_ribbon, null);
    }

    /// <summary>
    /// Applies a builtin palette to the form and ribbon and reports the ribbon shape.
    /// </summary>
    /// <param name="mode">Palette mode to apply.</param>
    public void ApplyTheme(PaletteMode mode)
    {
        _manager.GlobalPaletteMode = mode;
        PaletteMode = mode;
        _ribbon.PaletteMode = mode;
        _status.Text = StatusText();
    }

    /// <summary>
    /// Turns the Office 2024 group-area bevel on or off.
    /// </summary>
    /// <param name="enabled">True to draw the bevel.</param>
    public void SetGroupAreaBevel(bool enabled)
    {
        _ribbon.StateCommon.RibbonGeneral.GroupAreaBevelEdges = enabled;
        if (_bevel.Checked != enabled)
        {
            _syncingBevel = true;
            _bevel.Checked = enabled;
            _syncingBevel = false;
        }

        _status.Text = StatusText();
    }

    /// <summary>
    /// Shows or hides Office 2024 contextual tab titles in the title bar.
    /// </summary>
    /// <param name="visible">True to draw the context title in the caption.</param>
    public void SetShowContextTitles(bool visible)
    {
        _ribbon.StateCommon.RibbonGeneral.ShowContextTitles = visible;
        if (_contextTitles.Checked != visible)
        {
            _syncingContextTitles = true;
            _contextTitles.Checked = visible;
            _syncingContextTitles = false;
        }

        _status.Text = StatusText();
    }

    /// <summary>
    /// Lays the ribbon groups out as one command row, or restores the classic three-line band.
    /// </summary>
    /// <param name="compact">True for the single-row compact ribbon.</param>
    public void SetCompactMode(bool compact)
    {
        _ribbon.CompactMode = compact;
        if (_compact.Checked != compact)
        {
            _syncingCompact = true;
            _compact.Checked = compact;
            _syncingCompact = false;
        }

        _status.Text = StatusText();
    }

    /// <summary>
    /// Moves the compact-mode arrow under the groups, to the end of the groups, or onto the tab row.
    /// </summary>
    /// <param name="location">Placement of the arrow.</param>
    public void SetCompactButtonLocation(CompactButtonLocation location)
    {
        _ribbon.CompactButtonLocation = location;
        int index = location == CompactButtonLocation.GroupsTrailing
            ? 1
            : location == CompactButtonLocation.Tabs
                ? 2
                : 0;
        if (_arrowLocation.SelectedIndex != index)
        {
            _syncingArrowLocation = true;
            _arrowLocation.SelectedIndex = index;
            _syncingArrowLocation = false;
        }

        _status.Text = StatusText();
    }

    private static CompactButtonLocation LocationFromIndex(int index)
    {
        if (index == 1)
        {
            return CompactButtonLocation.GroupsTrailing;
        }

        if (index == 2)
        {
            return CompactButtonLocation.Tabs;
        }

        return CompactButtonLocation.BelowGroups;
    }

    /// <summary>
    /// Turns the compact-mode height animation on or off.
    /// </summary>
    /// <param name="animate">True to ease the groups area when compact mode changes.</param>
    public void SetAnimateCompactMode(bool animate) => _ribbon.AnimateCompactMode = animate;

    /// <summary>
    /// Selects the Home tab so the clipboard and font groups can be checked.
    /// </summary>
    public void SelectHomeTab()
    {
        if (_ribbon.RibbonTabs.Count > 0)
        {
            _ribbon.SelectedTab = _ribbon.RibbonTabs[0];
            _status.Text = StatusText();
        }
    }

    /// <summary>
    /// Sets the size at which the ribbon hides, so a narrow compact row can be checked.
    /// </summary>
    /// <param name="width">Minimum width before the ribbon hides.</param>
    /// <param name="height">Minimum height before the ribbon hides.</param>
    public void SetHideRibbonSize(int width, int height) => _ribbon.HideRibbonSize = new Size(width, height);

    /// <summary>
    /// Sets Office 2024 selected and hover tabs to a straight line or a pill-shaped line.
    /// </summary>
    /// <param name="marker">Straight line, or a pill-shaped line, under the label.</param>
    public void SetTabMarker(PaletteRibbonTabMarker marker)
    {
        _ribbon.StateCommon.RibbonGeneral.TabMarker = marker;
        var pills = marker == PaletteRibbonTabMarker.Pill;
        if (_pills.Checked != pills)
        {
            _syncingPills = true;
            _pills.Checked = pills;
            _syncingPills = false;
        }

        _status.Text = StatusText();
    }

    /// <summary>
    /// Turns the soft halo behind Office 2024 tab lines on or off.
    /// </summary>
    /// <param name="enabled">True to draw the halo.</param>
    public void SetTabMarkerGlow(bool enabled)
    {
        _ribbon.StateCommon.RibbonGeneral.TabMarkerGlow = enabled;
        if (_glow.Checked != enabled)
        {
            _syncingGlow = true;
            _glow.Checked = enabled;
            _syncingGlow = false;
        }

        _status.Text = StatusText();
    }

    /// <summary>
    /// Selects the first contextual tab so its coloured label and underline can be seen.
    /// </summary>
    public void SelectContextualTab()
    {
        foreach (KryptonRibbonTab tab in _ribbon.RibbonTabs)
        {
            if (!string.IsNullOrEmpty(tab.ContextName))
            {
                _ribbon.SelectedTab = tab;
                return;
            }
        }
    }

    /// <summary>
    /// Sets the visible width of the Office 2024 group-area bevel.
    /// </summary>
    /// <param name="size">Width in pixels at 96 DPI.</param>
    public void SetGroupAreaBevelSize(int size)
    {
        _ribbon.StateCommon.RibbonGeneral.GroupAreaBevelSize = size;
        if ((int)_bevelSize.Value != _ribbon.StateCommon.RibbonGeneral.GroupAreaBevelSize)
        {
            _syncingBevelSize = true;
            _bevelSize.Value = _ribbon.StateCommon.RibbonGeneral.GroupAreaBevelSize;
            _syncingBevelSize = false;
        }

        _status.Text = StatusText();
    }

    /// <summary>
    /// Sets the Office 2024 gap between group cards. Zero keeps one continuous card.
    /// </summary>
    /// <param name="gap">Gap in pixels at 96 DPI.</param>
    public void SetGroupAreaGap(int gap)
    {
        _ribbon.StateCommon.RibbonGeneral.GroupAreaGap = gap;
        if ((int)_gap.Value != gap)
        {
            _syncingGap = true;
            _gap.Value = gap;
            _syncingGap = false;
        }

        _status.Text = StatusText();
    }

    private string StatusText() =>
        $@"Palette: {_ribbon.PaletteMode}. Ribbon shape: {_ribbon.StateCommon.RibbonGeneral.GetRibbonShape()}. Compact: {_ribbon.CompactMode}. Bevel: {_ribbon.StateCommon.RibbonGeneral.GroupAreaBevelEdges}. Bevel size: {_ribbon.StateCommon.RibbonGeneral.GroupAreaBevelSize}. Gap: {_ribbon.StateCommon.RibbonGeneral.GroupAreaGap}. Context titles: {_ribbon.StateCommon.RibbonGeneral.ShowContextTitles}. Tab marker: {_ribbon.StateCommon.RibbonGeneral.TabMarker}. Glow: {_ribbon.StateCommon.RibbonGeneral.TabMarkerGlow}.";

    private KryptonButton CreateThemeButton(string text, PaletteMode mode)
    {
        var button = new KryptonButton
        {
            Text = text,
            AutoSize = true,
            MinimumSize = new Size(140, 28),
            Tag = mode
        };
        button.Click += OnThemeClick;
        return button;
    }

    private void OnThemeClick(object? sender, EventArgs e)
    {
        if (sender is KryptonButton button && button.Tag is PaletteMode mode)
        {
            ApplyTheme(mode);
        }
    }

    private static KryptonRibbonGroupButton Command(string text, string keyTip, Color color)
    {
        var image = new Bitmap(16, 16);
        using (var graphics = Graphics.FromImage(image))
        using (var brush = new SolidBrush(color))
        {
            graphics.FillRectangle(brush, 0, 0, 16, 16);
        }

        return new KryptonRibbonGroupButton
        {
            TextLine1 = text,
            KeyTip = keyTip,
            ImageSmall = image,
            ImageLarge = (Image)image.Clone()
        };
    }
}
