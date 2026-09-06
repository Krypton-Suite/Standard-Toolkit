#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Maps <see cref="MenuStrip"/> / <see cref="KryptonMenuStrip"/> items onto title-bar
/// <see cref="ButtonSpecAny"/> instances with <see cref="KryptonContextMenu"/> drop-downs.
/// </summary>
internal static class KryptonMenuStripTitleBarConverter
{
    /// <summary>
    /// Creates caption button specs for each top-level menu item on <paramref name="menuStrip"/>.
    /// Clicks are forwarded to the source <see cref="ToolStripMenuItem"/>.
    /// </summary>
    /// <param name="menuStrip">Source strip. Must not be <c>null</c>.</param>
    /// <param name="showDropArrow">Whether drop-down caption buttons show a drop glyph.</param>
    /// <returns>Newly created specs. Top-level separators and host items (combo, text box) are skipped.</returns>
    public static ButtonSpecAny[] CreateButtonSpecs(MenuStrip menuStrip, bool showDropArrow)
    {
        if (menuStrip is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(menuStrip));
        }

        var specs = new List<ButtonSpecAny>();
        foreach (ToolStripItem item in menuStrip.Items)
        {
            if (item is ToolStripMenuItem menuItem)
            {
                specs.Add(CreateTopLevelSpec(menuItem, showDropArrow));
            }
        }

        return specs.ToArray();
    }

    private static ButtonSpecAny CreateTopLevelSpec(ToolStripMenuItem menuItem, bool showDropArrow)
    {
        var spec = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Generic,
            Text = menuItem.Text,
            AllowInheritText = false,
            Image = menuItem.Image,
            ImageTransparentColor = menuItem.ImageTransparentColor,
            ToolTipTitle = string.IsNullOrEmpty(menuItem.ToolTipText) ? menuItem.Text : menuItem.ToolTipText,
            Enabled = menuItem.Enabled ? ButtonEnabled.True : ButtonEnabled.False,
            Visible = menuItem.Available
        };

        if (HasConvertibleDropDown(menuItem))
        {
            spec.ShowDrop = showDropArrow;
            spec.KryptonContextMenu = CreateContextMenu(menuItem);
        }
        else
        {
            spec.Click += (_, _) => PerformSourceClick(menuItem);
        }

        return spec;
    }

    private static KryptonContextMenu CreateContextMenu(ToolStripMenuItem menuItem)
    {
        var menu = new KryptonContextMenu();
        var group = ConvertDropDownItems(menuItem.DropDownItems);
        if (group.Items.Count > 0)
        {
            menu.Items.Add(group);
        }

        return menu;
    }

    private static KryptonContextMenuItems ConvertDropDownItems(ToolStripItemCollection items)
    {
        var group = new KryptonContextMenuItems();
        foreach (ToolStripItem item in items)
        {
            switch (item)
            {
                case ToolStripSeparator _:
                    group.Items.Add(new KryptonContextMenuSeparator());
                    break;
                case ToolStripMenuItem menuItem:
                    group.Items.Add(ConvertMenuItem(menuItem));
                    break;
            }
        }

        return group;
    }

    private static KryptonContextMenuItem ConvertMenuItem(ToolStripMenuItem source)
    {
        var dest = new KryptonContextMenuItem
        {
            Text = source.Text,
            Image = source.Image,
            ImageTransparentColor = source.ImageTransparentColor,
            Enabled = source.Enabled,
            Visible = source.Available,
            ShortcutKeys = source.ShortcutKeys,
            ShowShortcutKeys = source.ShowShortcutKeys,
            ShortcutKeyDisplayString = source.ShortcutKeyDisplayString,
            CheckOnClick = source.CheckOnClick,
            Checked = source.Checked,
            CheckState = source.CheckState
        };

        dest.Click += (_, _) =>
        {
            PerformSourceClick(source);
            dest.Checked = source.Checked;
            dest.CheckState = source.CheckState;
            dest.Enabled = source.Enabled;
        };

        if (HasConvertibleDropDown(source))
        {
            dest.Items.Add(ConvertDropDownItems(source.DropDownItems));
        }

        return dest;
    }

    private static bool HasConvertibleDropDown(ToolStripMenuItem item)
    {
        foreach (ToolStripItem child in item.DropDownItems)
        {
            if (child is ToolStripMenuItem || child is ToolStripSeparator)
            {
                return true;
            }
        }

        return false;
    }

    private static void PerformSourceClick(ToolStripMenuItem source)
    {
        if (!source.IsDisposed)
        {
            source.PerformClick();
        }
    }
}
