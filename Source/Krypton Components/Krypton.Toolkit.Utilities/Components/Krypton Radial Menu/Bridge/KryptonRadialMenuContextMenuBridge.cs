#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Projects supported <see cref="KryptonContextMenuItemBase"/> types into radial menu items.
/// </summary>
internal static class KryptonRadialMenuContextMenuBridge
{
    /// <summary>
    /// Converts a context-menu item collection into radial items (supported types only).
    /// </summary>
    /// <param name="source">Source collection.</param>
    /// <returns>Converted radial items.</returns>
    public static IEnumerable<KryptonRadialMenuItemBase> ConvertItems(KryptonContextMenuCollection? source)
    {
        if (source == null)
        {
            yield break;
        }

        foreach (KryptonContextMenuItemBase entry in source)
        {
            foreach (var converted in ConvertItem(entry))
            {
                yield return converted;
            }
        }
    }

    private static IEnumerable<KryptonRadialMenuItemBase> ConvertItem(KryptonContextMenuItemBase entry)
    {
        switch (entry)
        {
            case KryptonContextMenuItems itemsGroup:
                foreach (KryptonContextMenuItemBase child in itemsGroup.Items)
                {
                    foreach (var converted in ConvertItem(child))
                    {
                        yield return converted;
                    }
                }
                break;

            case KryptonContextMenuItem item:
                yield return ConvertCommandItem(item);
                break;

            case KryptonContextMenuLinkLabel linkLabel:
                yield return ConvertLinkLabel(linkLabel);
                break;

            case KryptonContextMenuCheckBox checkBox:
                yield return ConvertCheckStyle(checkBox.Text, checkBox.Checked, checkBox.Enabled, checkBox, (_, _) =>
                {
                    checkBox.Checked = !checkBox.Checked;
                });
                break;

            case KryptonContextMenuCheckButton checkButton:
                yield return ConvertCheckStyle(checkButton.Text, checkButton.Checked, checkButton.Enabled, checkButton, (_, _) =>
                {
                    checkButton.Checked = !checkButton.Checked;
                });
                break;

            case KryptonContextMenuRadioButton radioButton:
                yield return ConvertCheckStyle(radioButton.Text, radioButton.Checked, radioButton.Enabled, radioButton, (_, _) =>
                {
                    radioButton.Checked = true;
                });
                break;

            case KryptonContextMenuColorColumns colorColumns:
                yield return ConvertColorColumns(colorColumns);
                break;

            case KryptonContextMenuImageSelect imageSelect:
                yield return ConvertImageSelect(imageSelect);
                break;

            case KryptonContextMenuTextBox textBox:
                yield return ConvertTextBox(textBox);
                break;

            case KryptonContextMenuComboBox comboBox:
                yield return ConvertComboBox(comboBox);
                break;

            case KryptonContextMenuProgressBar progressBar:
                yield return ConvertProgressBar(progressBar);
                break;

            case KryptonContextMenuMonthCalendar monthCalendar:
                yield return ConvertMonthCalendar(monthCalendar);
                break;

            // Heading / Separator: no radial equivalent.
            default:
                yield break;
        }
    }

    private static KryptonRadialMenuItem ConvertCommandItem(KryptonContextMenuItem source)
    {
        var item = new KryptonRadialMenuItem
        {
            Text = source.Text,
            Image = ResolveContextMenuImage(source),
            ImageTransparentColor = ResolveContextMenuImageTransparentColor(source),
            Enabled = source.Enabled,
            Visible = source.Visible,
            Checked = source.Checked,
            CheckOnClick = false,
            LargeKryptonCommandImage = source.LargeKryptonCommandImage,
            // Command executes via source.PerformClick to avoid double-invocation.
            KryptonCommand = null,
            Tag = source,
            AutoClose = source.Items.Count == 0
        };

        foreach (var child in ConvertItems(source.Items))
        {
            item.Items.Add(child);
        }

        item.Click += (_, _) =>
        {
            try
            {
                source.PerformClick();
            }
            catch
            {
                // Source may have been disposed; radial consumers still receive ItemClick.
            }
        };

        CopyToolTips(source, item);
        return item;
    }

    private static KryptonRadialMenuItem ConvertLinkLabel(KryptonContextMenuLinkLabel source)
    {
        var item = new KryptonRadialMenuItem
        {
            Text = string.IsNullOrEmpty(source.Text) ? @"Link" : source.Text,
            Image = source.Image,
            ImageTransparentColor = source.ImageTransparentColor,
            Enabled = source.Visible,
            Visible = source.Visible,
            Tag = source,
            AutoClose = source.AutoClose
        };
        item.Click += (_, _) =>
        {
            try
            {
                source.PerformClick();
            }
            catch
            {
                // Ignore disposed source.
            }
        };
        CopyToolTips(source, item);
        return item;
    }

    private static KryptonRadialMenuItem ConvertImageSelect(KryptonContextMenuImageSelect source)
    {
        var item = new KryptonRadialMenuItem
        {
            Text = @"Images",
            Enabled = source.Visible,
            Visible = source.Visible,
            Tag = source,
            AutoClose = false
        };

        var list = source.ImageList;
        if (list is { Images.Count: > 0 })
        {
            var start = Math.Max(0, source.ImageIndexStart);
            var end = source.ImageIndexEnd < 0 ? list.Images.Count - 1 : Math.Min(source.ImageIndexEnd, list.Images.Count - 1);
            for (var index = start; index <= end; index++)
            {
                var imageIndex = index;
                var child = new KryptonRadialMenuItem($@"#{imageIndex}")
                {
                    Image = list.Images[imageIndex],
                    Tag = source,
                    AutoClose = source.AutoClose
                };
                child.Click += (_, _) =>
                {
                    try
                    {
                        source.SelectedIndex = imageIndex;
                    }
                    catch
                    {
                        // Ignore disposed source.
                    }
                };
                item.Items.Add(child);
            }
        }

        CopyToolTips(source, item);
        return item;
    }

    private static KryptonRadialMenuTextItem ConvertTextBox(KryptonContextMenuTextBox source)
    {
        var item = new KryptonRadialMenuTextItem
        {
            Label = @"Text",
            Text = source.Text,
            Enabled = source.Enabled,
            Visible = source.Visible,
            Tag = source,
            ToolTipText = source.Text
        };
        item.TextChanged += (_, _) =>
        {
            try
            {
                source.Text = item.Text;
            }
            catch
            {
                // Ignore disposed source.
            }
        };
        CopyToolTips(source, item);
        return item;
    }

    private static KryptonRadialMenuItem ConvertComboBox(KryptonContextMenuComboBox source)
    {
        var item = new KryptonRadialMenuItem(string.IsNullOrEmpty(source.Text) ? @"Combo" : Truncate(source.Text, 18))
        {
            Enabled = source.Enabled,
            Visible = source.Visible,
            Tag = source,
            AutoClose = false
        };

        for (var i = 0; i < source.Items.Count; i++)
        {
            var index = i;
            var entry = source.Items[i];
            var label = entry?.ToString() ?? $@"Item {index}";
            var child = new KryptonRadialMenuItem(Truncate(label, 18))
            {
                Tag = source,
                AutoClose = true
            };
            child.Click += (_, _) =>
            {
                try
                {
                    source.SelectedIndex = index;
                }
                catch
                {
                    // Ignore disposed source.
                }
            };
            item.Items.Add(child);
        }

        CopyToolTips(source, item);
        return item;
    }

    private static KryptonRadialMenuItem ConvertProgressBar(KryptonContextMenuProgressBar source)
    {
        var item = new KryptonRadialMenuItem($@"{source.Value}/{source.Maximum}")
        {
            Enabled = false,
            Visible = source.Visible,
            Tag = source,
            AutoClose = false,
            ToolTipText = $@"Progress {source.Value} of {source.Maximum}"
        };
        CopyToolTips(source, item);
        return item;
    }

    private static KryptonRadialMenuCalendarItem ConvertMonthCalendar(KryptonContextMenuMonthCalendar source)
    {
        var item = new KryptonRadialMenuCalendarItem
        {
            Text = @"Date",
            SelectedDate = source.SelectionStart.Date,
            Enabled = source.Enabled,
            Visible = source.Visible,
            Tag = source,
            ToolTipText = source.SelectionStart.ToShortDateString()
        };
        item.SelectedDateChanged += (_, _) =>
        {
            try
            {
                source.SelectionStart = item.SelectedDate;
                source.SelectionEnd = item.SelectedDate;
            }
            catch
            {
                // Ignore disposed source.
            }
        };
        CopyToolTips(source, item);
        return item;
    }

    private static KryptonRadialMenuItem ConvertCheckStyle(
        string text,
        bool isChecked,
        bool enabled,
        object source,
        EventHandler clickHandler)
    {
        var item = new KryptonRadialMenuItem
        {
            Text = text,
            Checked = isChecked,
            CheckOnClick = true,
            Enabled = enabled,
            Tag = source,
            AutoClose = true
        };
        item.Click += clickHandler;
        if (source is KryptonContextMenuItemBase menuItem)
        {
            CopyToolTips(menuItem, item);
        }

        return item;
    }

    private static KryptonRadialMenuColorPaletteItem ConvertColorColumns(KryptonContextMenuColorColumns source)
    {
        var item = new KryptonRadialMenuColorPaletteItem(source.ColorScheme)
        {
            Text = @"Colors",
            Enabled = source.Visible,
            Visible = source.Visible,
            SelectedColor = source.SelectedColor,
            Tag = source
        };

        item.SelectedColorChanged += (_, e) =>
        {
            try
            {
                source.SelectedColor = e.Color;
            }
            catch
            {
                // Ignore disposed source.
            }
        };

        CopyToolTips(source, item);
        return item;
    }

    private static void CopyToolTips(KryptonContextMenuItemBase source, KryptonRadialMenuItemBase target)
    {
        var from = source.ToolTipValues;
        if (from is { EnableToolTips: false, IsDefault: true })
        {
            return;
        }

        var to = target.ToolTipValues;
        to.EnableToolTips = from.EnableToolTips;
        to.Heading = from.Heading;
        to.Description = from.Description;
        to.Image = from.Image;
        to.ToolTipStyle = from.ToolTipStyle;
        to.ToolTipShadow = from.ToolTipShadow;
        to.ShowIntervalDelay = from.ShowIntervalDelay;
        to.CloseIntervalDelay = from.CloseIntervalDelay;
    }

    private static Image? ResolveContextMenuImage(KryptonContextMenuItem source)
    {
        if (source.Image != null)
        {
            return source.Image;
        }

        var command = source.KryptonCommand;
        if (command == null)
        {
            return null;
        }

        return source.LargeKryptonCommandImage ? command.ImageLarge : command.ImageSmall;
    }

    private static Color ResolveContextMenuImageTransparentColor(KryptonContextMenuItem source)
    {
        if (source.Image != null || source.KryptonCommand == null)
        {
            return source.ImageTransparentColor;
        }

        return source.KryptonCommand.ImageTransparentColor;
    }

    private static string Truncate(string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
        {
            return value;
        }

        return value.Substring(0, maxLength - 1) + @"…";
    }
}
