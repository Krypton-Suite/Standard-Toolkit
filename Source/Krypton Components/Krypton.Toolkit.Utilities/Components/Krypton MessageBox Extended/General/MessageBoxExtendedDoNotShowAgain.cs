#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Session-lifetime 'Do not show again' helpers for <see cref="KryptonMessageBoxExtended"/>.
/// </summary>
internal static class MessageBoxExtendedDoNotShowAgain
{
    private static readonly Dictionary<string, DialogResult> _suppressed =
        new Dictionary<string, DialogResult>(StringComparer.Ordinal);

    /// <summary>
    /// Gets whether the optional checkbox should be shown for <paramref name="data"/>.
    /// </summary>
    internal static bool ResolveShow(KryptonMessageBoxExtendedData data) =>
        data.ShowDoNotShowAgainOption || !string.IsNullOrEmpty(data.CheckBoxText);

    /// <summary>
    /// Resolves checkbox caption text. Custom <see cref="KryptonMessageBoxExtendedData.CheckBoxText"/> wins;
    /// otherwise the localizable <see cref="CustomToolkitStrings.DoNotShowAgain"/> string is used when the option is shown.
    /// </summary>
    internal static string ResolveText(bool show, string? customText)
    {
        if (!string.IsNullOrEmpty(customText))
        {
            return customText ?? string.Empty;
        }

        string caption = KryptonManager.Strings.CustomStrings.DoNotShowAgain ?? string.Empty;
        return show ? caption : string.Empty;
    }

    /// <summary>
    /// Resolves checkbox caption text from <paramref name="data"/>.
    /// </summary>
    internal static string ResolveText(KryptonMessageBoxExtendedData data) =>
        ResolveText(ResolveShow(data), data.CheckBoxText);

    /// <summary>
    /// Tries to return a previously stored result for <paramref name="data"/>'s suppression key.
    /// </summary>
    internal static bool TrySkip(KryptonMessageBoxExtendedData data, out DialogResult result)
    {
        result = DialogResult.None;
        return data.ShowDoNotShowAgainOption && TryGet(data.DoNotShowAgainKey, out result);
    }

    /// <summary>
    /// Stores <paramref name="result"/> when the option is enabled, the box was checked, and a key is set.
    /// </summary>
    internal static void RememberIfChecked(KryptonMessageBoxExtendedData data, bool isChecked, DialogResult result)
    {
        if (data.ShowDoNotShowAgainOption && isChecked)
        {
            Remember(data.DoNotShowAgainKey, result);
        }
    }

    /// <summary>
    /// Places the optional checkbox on the left of the button bar (after Copy when that is visible)
    /// and returns the widened button-area width.
    /// </summary>
    internal static int LayoutInButtonBar(
        KryptonCheckBox checkBox,
        bool show,
        Control copyButton,
        bool copyEnabled,
        int padding,
        int buttonHeight,
        int buttonsAreaWidth)
    {
        if (!show)
        {
            checkBox.Visible = false;
            return buttonsAreaWidth;
        }

        checkBox.Visible = true;
        checkBox.AutoSize = true;
        Size checkSize = checkBox.GetPreferredSize(Size.Empty);
        int x = copyEnabled ? copyButton.Right + padding : padding;
        int y = padding + Math.Max(0, (buttonHeight - checkSize.Height) / 2);
        checkBox.Location = new Point(x, y);
        checkBox.Size = checkSize;
        return buttonsAreaWidth + checkSize.Width + padding * 2;
    }

    internal static bool TryGet(string? key, out DialogResult result)
    {
        result = DialogResult.None;
        string storedKey = key ?? string.Empty;
        if (storedKey.Length == 0)
        {
            return false;
        }

        lock (_suppressed)
        {
            return _suppressed.TryGetValue(storedKey, out result);
        }
    }

    internal static void Remember(string? key, DialogResult result)
    {
        string storedKey = key ?? string.Empty;
        if (storedKey.Length == 0)
        {
            return;
        }

        lock (_suppressed)
        {
            _suppressed[storedKey] = result;
        }
    }

    internal static void Reset(string? key)
    {
        string storedKey = key ?? string.Empty;
        lock (_suppressed)
        {
            if (storedKey.Length == 0)
            {
                _suppressed.Clear();
            }
            else
            {
                _suppressed.Remove(storedKey);
            }
        }
    }
}
