#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Navigator;
using Krypton.Ribbon;

namespace TestForm;

/// <summary>
/// Reproduces issue #4557 and checks #4559: a fresh ribbon notification bar, and the navigator bar,
/// stay blank in the property grid until a value changes.
/// </summary>
public sealed class Bug4557NotificationBarModifiedDemo : KryptonForm
{
    private readonly KryptonRibbon _ribbon;
    private readonly KryptonNavigator _navigator;
    private readonly KryptonPropertyGrid _propertyGrid;
    private readonly KryptonWrapLabel _lblStatus;

    public Bug4557NotificationBarModifiedDemo()
    {
        Text = @"Bug #4557 - Notification bar Modified";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(980, 780);
        MinimumSize = new Size(820, 640);

        var info = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 72,
            Text =
                @"Issue #4557 / #4559: a fresh ribbon NotificationBar is blank. Ribbon state colours stay blank too." + Environment.NewLine +
                @"The navigator Bar row below is blank until you change it. Change title makes NotificationBar show Modified."
        };

        _lblStatus = new KryptonWrapLabel
        {
            Dock = DockStyle.Bottom,
            AutoSize = false,
            Height = 36,
            Text = @"Status"
        };

        _ribbon = new KryptonRibbon
        {
            Dock = DockStyle.Top
        };
        _navigator = new KryptonNavigator
        {
            Dock = DockStyle.Top
        };

        var buttons = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 44,
            Padding = new Padding(8, 6, 8, 0),
            WrapContents = false
        };

        var changeTitle = new KryptonButton { Text = @"Change title", AutoSize = true };
        changeTitle.Click += (_, _) =>
        {
            _ribbon.NotificationBar.Title = @"UPDATES AVAILABLE";
            RefreshView();
        };
        var reset = new KryptonButton { Text = @"Reset", AutoSize = true };
        reset.Click += (_, _) =>
        {
            _ribbon.ResetNotificationBar();
            RefreshView();
        };
        buttons.Controls.Add(changeTitle);
        buttons.Controls.Add(reset);

        _propertyGrid = new KryptonPropertyGrid
        {
            Dock = DockStyle.Fill,
            HelpVisible = true,
            ToolbarVisible = true,
            PropertySort = PropertySort.Categorized,
            SelectedObject = _ribbon
        };
        _propertyGrid.PropertyValueChanged += (_, _) => RefreshView();

        var showNavigator = new KryptonButton { Text = @"Navigator", AutoSize = false, Size = new Size(130, 28) };
        showNavigator.Click += (_, _) =>
        {
            _propertyGrid.SelectedObject = _navigator;
            RefreshView();
        };
        var showRibbon = new KryptonButton { Text = @"Ribbon", AutoSize = true };
        showRibbon.Click += (_, _) =>
        {
            _propertyGrid.SelectedObject = _ribbon;
            RefreshView();
        };
        buttons.Controls.Add(showNavigator);
        buttons.Controls.Add(showRibbon);

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 5
        };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 116));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 72));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.Controls.Add(buttons, 0, 0);
        layout.Controls.Add(_ribbon, 0, 1);
        layout.Controls.Add(_navigator, 0, 2);
        layout.Controls.Add(_lblStatus, 0, 3);
        layout.Controls.Add(_propertyGrid, 0, 4);
        buttons.Dock = DockStyle.Fill;
        _ribbon.Dock = DockStyle.Fill;
        _navigator.Dock = DockStyle.Fill;
        _lblStatus.Dock = DockStyle.Fill;
        _lblStatus.Height = 48;

        Controls.Add(layout);
        Controls.Add(info);

        Shown += (_, _) => RefreshView();
    }

    private void RefreshView()
    {
        string shown = _ribbon.NotificationBar.ToString();
        string quoted = shown.Length == 0 ? @"""""" : @"""" + shown + @"""";
        string barShown = _navigator.Bar.ToString();
        string barQuoted = barShown.Length == 0 ? @"""""" : @"""" + barShown + @"""";
        _lblStatus.Text = @"NotificationBar shows " + quoted + @". IsDefault = " + _ribbon.NotificationBar.IsDefault
                          + @". Navigator Bar shows " + barQuoted + @".";
        _propertyGrid.PropertyGrid.Refresh();
        if (ReferenceEquals(_propertyGrid.SelectedObject, _ribbon))
        {
            SelectNotificationBar();
        }
    }

    private void SelectNotificationBar()
    {
        PropertyGrid grid = _propertyGrid.PropertyGrid;
        GridItem? root = grid.SelectedGridItem;
        while (root != null && root.Parent != null)
        {
            root = root.Parent;
        }

        if (root == null)
        {
            return;
        }

        foreach (GridItem category in root.GridItems)
        {
            bool appearance = category.Label == @"Appearance";
            category.Expanded = appearance;
            if (!appearance)
            {
                continue;
            }

            foreach (GridItem item in category.GridItems)
            {
                if (item.PropertyDescriptor != null && item.PropertyDescriptor.Name == @"NotificationBar")
                {
                    item.Select();
                    return;
                }
            }
        }
    }
}
