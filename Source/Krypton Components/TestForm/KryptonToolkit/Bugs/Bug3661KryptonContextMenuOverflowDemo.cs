#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive demo for Issue #3661: KryptonContextMenu overflow when item list exceeds screen height.
/// </summary>
public partial class Bug3661KryptonContextMenuOverflowDemo : KryptonForm
{
    private KryptonContextMenu _longListMenu;
    private KryptonContextMenu _mixedListMenu;

    public Bug3661KryptonContextMenuOverflowDemo()
    {
        InitializeComponent();
        _longListMenu = CreateLongListMenu();
        _mixedListMenu = CreateMixedListMenu();
        kbtnLongList.KryptonContextMenu = _longListMenu;
        kbtnMixedList.KryptonContextMenu = _mixedListMenu;
        kbtnAnchorTarget.KryptonContextMenu = _longListMenu;
        HookMenuEvents(_longListMenu);
        HookMenuEvents(_mixedListMenu);
        knumItemCount.ValueChanged += OnItemCountChanged;
        kchkOverflowArrows.CheckedChanged += OnOverflowArrowsChanged;
        UpdateItemCountLabel();
    }

    private void OnOverflowArrowsChanged(object? sender, EventArgs e)
    {
        _longListMenu.OverflowScrollUseArrows = kchkOverflowArrows.Checked;
        _mixedListMenu.OverflowScrollUseArrows = kchkOverflowArrows.Checked;
    }

    private void OnItemCountChanged(object? sender, EventArgs e)
    {
        _longListMenu = CreateLongListMenu();
        _mixedListMenu = CreateMixedListMenu();
        kbtnLongList.KryptonContextMenu = _longListMenu;
        kbtnMixedList.KryptonContextMenu = _mixedListMenu;
        kbtnAnchorTarget.KryptonContextMenu = _longListMenu;
        HookMenuEvents(_longListMenu);
        HookMenuEvents(_mixedListMenu);
        UpdateItemCountLabel();
    }

    private void UpdateItemCountLabel()
    {
        var count = (int)knumItemCount.Value;
        klblItemCount.Values.Text = $"Menu item count: {count}";
    }

    private void kbtnShowMouse_Click(object? sender, EventArgs e) =>
        ShowMenu(kbtnAnchorTarget, false);

    private void kbtnShowScreenTop_Click(object? sender, EventArgs e) =>
        ShowMenuAtScreen(new Point(80, 40), false);

    private void kbtnShowScreenCenter_Click(object? sender, EventArgs e)
    {
        var area = Screen.PrimaryScreen?.WorkingArea ?? Screen.GetWorkingArea(this);
        var pt = new Point(area.Left + (area.Width / 2), area.Top + (area.Height / 2));
        ShowMenuAtScreen(pt, false);
    }

    private void kbtnShowScreenBottom_Click(object? sender, EventArgs e)
    {
        var area = Screen.PrimaryScreen?.WorkingArea ?? Screen.GetWorkingArea(this);
        var pt = new Point(area.Left + 80, area.Bottom - 8);
        ShowMenuAtScreen(pt, true);
    }

    private void kbtnKeyboardOpen_Click(object? sender, EventArgs e) =>
        ShowMenu(kbtnAnchorTarget, true);

    private void ShowMenu(Control target, bool keyboardActivated)
    {
        var screenPt = target.PointToScreen(new Point(target.Width / 2, target.Height));
        _longListMenu.Show(target, new Rectangle(screenPt, Size.Empty), KryptonContextMenuPositionH.Left,
            KryptonContextMenuPositionV.Below, keyboardActivated, true);
    }

    private void ShowMenuAtScreen(Point screenPt, bool alignAbove)
    {
        _longListMenu.Show(kbtnAnchorTarget, new Rectangle(screenPt, Size.Empty),
            KryptonContextMenuPositionH.Left,
            alignAbove ? KryptonContextMenuPositionV.Above : KryptonContextMenuPositionV.Below,
            false, true);
    }

    private KryptonContextMenu CreateLongListMenu()
    {
        var menu = new KryptonContextMenu
        {
            OverflowScrollUseArrows = kchkOverflowArrows.Checked
        };
        var items = new KryptonContextMenuItems();
        menu.Items.Add(items);

        var count = (int)knumItemCount.Value;
        for (var i = 1; i <= count; i++)
        {
            var item = new KryptonContextMenuItem($"Long list item {i}")
            {
                CommandParameter = i
            };
            item.Click += OnMenuItemClick;
            items.Items.Add(item);
        }

        return menu;
    }

    private KryptonContextMenu CreateMixedListMenu()
    {
        var menu = new KryptonContextMenu
        {
            OverflowScrollUseArrows = kchkOverflowArrows.Checked
        };
        var items = new KryptonContextMenuItems();
        menu.Items.Add(items);

        items.Items.Add(new KryptonContextMenuHeading("Mixed content"));

        var count = Math.Max(12, (int)knumItemCount.Value / 2);
        for (var i = 1; i <= count; i++)
        {
            if (i > 1 && (i % 8) == 1)
            {
                items.Items.Add(new KryptonContextMenuSeparator());
            }

            if (i == 5)
            {
                items.Items.Add(new KryptonContextMenuCheckBox("Sample check box"));
            }

            if (i == 10)
            {
                items.Items.Add(new KryptonContextMenuRadioButton("Radio option A"));
                items.Items.Add(new KryptonContextMenuRadioButton("Radio option B"));
            }

            var item = new KryptonContextMenuItem($"Mixed item {i}")
            {
                ExtraText = i % 3 == 0 ? "Extra text column" : string.Empty,
                CommandParameter = $"mixed-{i}"
            };
            item.Click += OnMenuItemClick;
            items.Items.Add(item);
        }

        return menu;
    }

    private void HookMenuEvents(KryptonContextMenu menu)
    {
        menu.Opened += OnMenuOpened;
        menu.Closed += OnMenuClosed;
    }

    private void OnMenuOpened(object? sender, EventArgs e) =>
        klblStatus.Values.Text = "Menu opened — hover Scroll Up/Down, use wheel, or arrow keys";

    private void OnMenuClosed(object? sender, ToolStripDropDownClosedEventArgs e) =>
        klblStatus.Values.Text = $"Menu closed ({e.CloseReason})";

    private void OnMenuItemClick(object? sender, EventArgs e)
    {
        if (sender is KryptonContextMenuItem item)
        {
            klblStatus.Values.Text = $"Clicked: {item.Text} (param: {item.CommandParameter})";
        }
    }
}
