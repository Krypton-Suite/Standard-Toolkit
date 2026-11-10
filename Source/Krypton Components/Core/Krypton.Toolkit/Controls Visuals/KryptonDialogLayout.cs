#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Sizes Krypton dialogs from their content and the owner monitor.
/// </summary>
/// <remarks>
/// Designer client sizes are 96 DPI pixels. <see cref="AutoScaleMode.Font"/> scales them once,
/// but a fixed border still clips long text and can grow larger than the working area.
/// Call <see cref="EnableResizable"/> for tool dialogs, or <see cref="FitWrappedPrompt"/> /
/// <see cref="FitClientToText"/> for prompts. Neither path multiplies a size that WinForms has
/// already scaled.
/// </remarks>
internal static class KryptonDialogLayout
{
    /// <summary>Largest fraction of the owner monitor width used for a content-sized prompt.</summary>
    public const float MaxWidthFraction = 2f / 3f;

    /// <summary>Largest fraction of the owner monitor height used for a content-sized prompt.</summary>
    public const float MaxHeightFraction = 0.95f;

    private static readonly ConditionalWeakTable<Form, ClampState> _states = new ConditionalWeakTable<Form, ClampState>();

    /// <summary>
    /// Working area of the monitor that will host the dialog.
    /// </summary>
    /// <param name="owner">Owner window, or <see langword="null"/> to use the primary monitor.</param>
    /// <returns>The working area in screen pixels.</returns>
    public static Rectangle GetWorkingArea(IWin32Window? owner)
    {
        if (owner != null)
        {
            try
            {
                if (owner is Control control && !control.IsDisposed && control.IsHandleCreated)
                {
                    return Screen.FromControl(control).WorkingArea;
                }

                if (owner.Handle != IntPtr.Zero)
                {
                    return Screen.FromHandle(owner.Handle).WorkingArea;
                }
            }
            catch (ArgumentException)
            {
                // The owner handle is not a window. Fall through to the primary monitor.
            }
        }

        return (Screen.PrimaryScreen ?? Screen.AllScreens[0]).WorkingArea;
    }

    /// <summary>
    /// Largest client size that still fits on the owner monitor.
    /// </summary>
    /// <param name="form">Dialog being sized.</param>
    /// <param name="owner">Owner window used to choose the monitor.</param>
    /// <param name="useContentFractions">
    /// When <see langword="true"/>, cap width at <see cref="MaxWidthFraction"/> and height at <see cref="MaxHeightFraction"/>.
    /// Tool windows pass <see langword="false"/> so they may use the full working area.
    /// </param>
    /// <returns>Maximum client size in device pixels.</returns>
    public static Size GetMaximumClientSize(Form form, IWin32Window? owner, bool useContentFractions)
    {
        var working = GetWorkingArea(owner ?? form);
        var limitWidth = useContentFractions ? (int)(working.Width * MaxWidthFraction) : working.Width;
        var limitHeight = useContentFractions ? (int)(working.Height * MaxHeightFraction) : working.Height;
        var chromeWidth = Math.Max(0, form.Width - form.ClientSize.Width);
        var chromeHeight = Math.Max(0, form.Height - form.ClientSize.Height);

        return new Size(
            Math.Max(1, limitWidth - chromeWidth),
            Math.Max(1, limitHeight - chromeHeight));
    }

    /// <summary>
    /// Measures wrapped text in device pixels for the font's current size.
    /// </summary>
    /// <param name="text">Text to measure. Null or empty yields <see cref="Size.Empty"/>.</param>
    /// <param name="font">Font used to paint the text.</param>
    /// <param name="maxWidth">Maximum width before wrapping.</param>
    /// <returns>The measured size, including the glyph overhang <see cref="TextRenderer"/> adds.</returns>
    public static Size MeasureWrappedText(string? text, Font font, int maxWidth)
    {
        if (string.IsNullOrEmpty(text))
        {
            return Size.Empty;
        }

        return TextRenderer.MeasureText(
            text,
            font,
            new Size(Math.Max(1, maxWidth), int.MaxValue),
            TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl);
    }

    /// <summary>
    /// Places the visible buttons in a row at the parent's right edge, with equal sizes and
    /// <see cref="SharedStaticConstants.GLOBAL_BUTTON_PADDING"/> between them.
    /// Hidden buttons are skipped, so a designer slot for an optional button does not leave a hole.
    /// </summary>
    /// <param name="panel">Parent that owns the buttons.</param>
    /// <param name="buttonsLeftToRight">Buttons in visual order, left to right.</param>
    public static void PackButtonsRight(Control panel, params Control[] buttonsLeftToRight)
    {
        var gap = SharedStaticConstants.GLOBAL_BUTTON_PADDING;
        var visible = new List<Control>(buttonsLeftToRight.Length);
        var width = 1;
        var height = 1;
        foreach (Control button in buttonsLeftToRight)
        {
            if (!button.Visible)
            {
                continue;
            }

            visible.Add(button);
            var preferred = button.GetPreferredSize(Size.Empty);
            width = Math.Max(width, Math.Max(button.Width, Math.Max(button.MinimumSize.Width, preferred.Width)));
            height = Math.Max(height, Math.Max(button.Height, Math.Max(button.MinimumSize.Height, preferred.Height)));
        }

        if (visible.Count == 0 || panel.ClientSize.Width <= gap)
        {
            return;
        }

        var right = panel.ClientSize.Width - gap;
        for (var index = visible.Count - 1; index >= 0; index--)
        {
            var button = visible[index];
            var vertical = button.Anchor & (AnchorStyles.Top | AnchorStyles.Bottom);
            if (vertical == AnchorStyles.None)
            {
                vertical = AnchorStyles.Top;
            }

            // AutoSize plus a right anchor lets the layout engine close the gap when the parent shrinks.
            button.AutoSize = false;
            button.Anchor = vertical;
            button.SetBounds(right - width, button.Top, width, height);
            button.Anchor = vertical | AnchorStyles.Right;
            right -= width + gap;
        }
    }

    /// <summary>
    /// Assigns a client size and shrinks it so the window stays inside the owner monitor.
    /// </summary>
    /// <param name="form">Dialog to resize.</param>
    /// <param name="desiredClient">Preferred client size in device pixels.</param>
    /// <param name="owner">Owner window used to choose the monitor.</param>
    /// <param name="useContentFractions">See <see cref="GetMaximumClientSize"/>.</param>
    public static void FitClientSize(Form form, Size desiredClient, IWin32Window? owner, bool useContentFractions)
    {
        var maxClient = GetMaximumClientSize(form, owner, useContentFractions);
        form.ClientSize = new Size(
            Math.Max(1, Math.Min(Math.Max(1, desiredClient.Width), maxClient.Width)),
            Math.Max(1, Math.Min(Math.Max(1, desiredClient.Height), maxClient.Height)));

        ShrinkMinimumToWindow(form);
    }

    /// <summary>
    /// Sizes a prompt from a wrap label that lives in an auto-sized layout (input box).
    /// </summary>
    /// <param name="form">Prompt dialog.</param>
    /// <param name="prompt">Label whose text wraps.</param>
    /// <param name="layoutRoot">Auto-sized root (usually the table that holds the label, input, and buttons).</param>
    /// <param name="owner">Owner window used to choose the monitor.</param>
    /// <param name="minimumContentWidth">Smallest content width, already in device pixels (buttons and input).</param>
    public static void FitWrappedPrompt(Form form, Control prompt, Control layoutRoot, IWin32Window? owner, int minimumContentWidth)
    {
        var maxClient = GetMaximumClientSize(form, owner, true);
        var contentWidth = Math.Min(Math.Max(minimumContentWidth, 1), maxClient.Width);
        var innerWidth = Math.Max(1, contentWidth - prompt.Margin.Horizontal);
        Font font = prompt.Font ?? SystemFonts.MessageBoxFont ?? SystemFonts.DefaultFont;
        var measured = MeasureWrappedText(prompt.Text, font, innerWidth);

        prompt.AutoSize = false;
        prompt.MaximumSize = new Size(innerWidth, Math.Max(measured.Height + 8, 1));
        prompt.Size = new Size(innerWidth, Math.Max(1, measured.Height));

        layoutRoot.PerformLayout();
        var preferred = layoutRoot.GetPreferredSize(new Size(contentWidth, 0));
        var stackedHeight = prompt.Margin.Vertical + prompt.Height;
        foreach (Control child in layoutRoot.Controls)
        {
            if (!ReferenceEquals(child, prompt) && child.Visible)
            {
                stackedHeight += child.Height + child.Margin.Vertical;
            }
        }

        stackedHeight += layoutRoot.Padding.Vertical;
        FitClientSize(
            form,
            new Size(
                Math.Max(contentWidth, preferred.Width),
                Math.Max(preferred.Height, stackedHeight)),
            owner,
            true);

        // Measure again at the width the label will actually wrap to. A later layout pass
        // often narrows the control, which adds lines the first measure did not include.
        form.PerformLayout();
        var available = Math.Max(1, form.ClientSize.Width - prompt.Margin.Horizontal - layoutRoot.Padding.Horizontal);
        var fitted = MeasureWrappedText(prompt.Text, font, available);
        prompt.MaximumSize = new Size(available, Math.Max(fitted.Height + 8, 1));
        prompt.MinimumSize = new Size(1, Math.Max(1, fitted.Height));
        prompt.Size = new Size(available, Math.Max(1, fitted.Height));

        var parent = prompt.Parent ?? form;
        var bottom = form.PointToClient(parent.PointToScreen(new Point(0, prompt.Bottom + prompt.Margin.Bottom)));
        var overflow = bottom.Y + layoutRoot.Margin.Bottom - form.ClientSize.Height;
        if (overflow > 0)
        {
            form.ClientSize = new Size(form.ClientSize.Width, form.ClientSize.Height + overflow);
        }
    }

    /// <summary>
    /// Sizes a dialog from wrapped text plus chrome, without forcing the text control's bounds.
    /// Use this when the text control is docked and will follow the client size.
    /// </summary>
    /// <param name="form">Dialog to resize.</param>
    /// <param name="text">Text that must be visible.</param>
    /// <param name="font">Font used to paint the text.</param>
    /// <param name="chrome">Extra client pixels around the text (icon, buttons, padding), in device pixels.</param>
    /// <param name="minimumClient">Smallest client size, in device pixels.</param>
    /// <param name="owner">Owner window used to choose the monitor.</param>
    public static void FitClientToText(Form form, string? text, Font font, Size chrome, Size minimumClient, IWin32Window? owner)
    {
        var maxClient = GetMaximumClientSize(form, owner, true);
        var single = string.IsNullOrEmpty(text) ? Size.Empty : TextRenderer.MeasureText(text, font);
        var width = Math.Min(maxClient.Width, Math.Max(minimumClient.Width, single.Width + chrome.Width));
        var innerWidth = Math.Max(1, width - chrome.Width);
        var wrapped = MeasureWrappedText(text, font, innerWidth);
        var height = Math.Min(maxClient.Height, Math.Max(minimumClient.Height, wrapped.Height + chrome.Height));

        FitClientSize(form, new Size(Math.Max(1, width), Math.Max(1, height)), owner, true);
    }

    /// <summary>
    /// Replaces a fixed border with a resizable one, keeps the design size as the minimum,
    /// and clamps the window to the owner monitor on load, show, and DPI change.
    /// </summary>
    /// <param name="form">Dialog to unlock.</param>
    /// <param name="owner">Owner window used to choose the monitor. The form itself is used when this is null.</param>
    /// <param name="useContentFractions">
    /// When <see langword="true"/>, the window is also capped at the message-box fractions of the working area.
    /// </param>
    public static void EnableResizable(Form form, IWin32Window? owner = null, bool useContentFractions = false)
    {
        ReleaseFixedChrome(form);

        if (form.MinimumSize.IsEmpty)
        {
            // Design-time window size. AutoScaleMode.Font / Dpi scales MinimumSize with the rest of the form.
            form.MinimumSize = form.Size;
        }

        HookClamp(form, owner, useContentFractions);

        if (form.IsHandleCreated)
        {
            ClampToWorkingArea(form, owner ?? form, useContentFractions);
        }
    }

    /// <summary>
    /// Switches <see cref="FormBorderStyle.FixedDialog"/>, <see cref="FormBorderStyle.Fixed3D"/>,
    /// <see cref="FormBorderStyle.FixedSingle"/>, and <see cref="FormBorderStyle.FixedToolWindow"/>
    /// to the matching sizable style and clears a locked <see cref="Form.MaximumSize"/>.
    /// </summary>
    /// <param name="form">Dialog whose chrome should resize.</param>
    public static void ReleaseFixedChrome(Form form)
    {
        if (form.FormBorderStyle == FormBorderStyle.FixedToolWindow)
        {
            form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
        }
        else if (form.FormBorderStyle == FormBorderStyle.FixedDialog
                 || form.FormBorderStyle == FormBorderStyle.Fixed3D
                 || form.FormBorderStyle == FormBorderStyle.FixedSingle)
        {
            form.FormBorderStyle = FormBorderStyle.Sizable;
        }

        form.MaximumSize = Size.Empty;
    }

    /// <summary>
    /// Shrinks the window so it fits the owner monitor, then pulls it back inside the working area.
    /// </summary>
    /// <param name="form">Dialog to clamp.</param>
    /// <param name="owner">Owner window used to choose the monitor.</param>
    /// <param name="useContentFractions">See <see cref="GetMaximumClientSize"/>.</param>
    public static void ClampToWorkingArea(Form form, IWin32Window? owner, bool useContentFractions)
    {
        if (form.IsDisposed || form.Disposing)
        {
            return;
        }

        var anchor = owner ?? (form.IsHandleCreated ? form : null);
        var working = GetWorkingArea(anchor);
        var limitWidth = Math.Max(1, useContentFractions ? (int)(working.Width * MaxWidthFraction) : working.Width);
        var limitHeight = Math.Max(1, useContentFractions ? (int)(working.Height * MaxHeightFraction) : working.Height);
        var width = Math.Min(Math.Max(1, form.Width), limitWidth);
        var height = Math.Min(Math.Max(1, form.Height), limitHeight);

        if (width != form.Width || height != form.Height)
        {
            form.Size = new Size(width, height);
        }

        ShrinkMinimumToWindow(form);

        if (!form.IsHandleCreated)
        {
            return;
        }

        var bounds = form.Bounds;
        var left = Math.Min(Math.Max(bounds.Left, working.Left), Math.Max(working.Left, working.Right - bounds.Width));
        var top = Math.Min(Math.Max(bounds.Top, working.Top), Math.Max(working.Top, working.Bottom - bounds.Height));

        if (left != bounds.Left || top != bounds.Top)
        {
            form.Location = new Point(left, top);
        }
    }

    private static void ShrinkMinimumToWindow(Form form)
    {
        if (form.MinimumSize.IsEmpty)
        {
            return;
        }

        if (form.MinimumSize.Width > form.Width || form.MinimumSize.Height > form.Height)
        {
            form.MinimumSize = new Size(
                Math.Min(form.MinimumSize.Width, Math.Max(1, form.Width)),
                Math.Min(form.MinimumSize.Height, Math.Max(1, form.Height)));
        }
    }

    private static void HookClamp(Form form, IWin32Window? owner, bool useContentFractions)
    {
        if (!_states.TryGetValue(form, out var state))
        {
            state = new ClampState();
            _states.Add(form, state);
        }

        state.Owner = owner;
        state.UseContentFractions = useContentFractions;

        if (state.Hooked)
        {
            return;
        }

        state.Hooked = true;
        form.Load += OnClamp;
        form.Shown += OnClamp;
        form.DpiChanged += OnDpiChanged;
    }

    private static void OnClamp(object? sender, EventArgs e)
    {
        if (sender is Form form)
        {
            ApplyClamp(form);
        }
    }

    private static void OnDpiChanged(object? sender, DpiChangedEventArgs e)
    {
        if (sender is Form form)
        {
            // AutoScaleMode.None does not resize with the monitor. SuggestedRectangle is the
            // size WinForms would have applied. Forms that already scaled are left as they are
            // and only clamped below.
            var suggested = e.SuggestedRectangle.Size;
            if (form.AutoScaleMode == AutoScaleMode.None
                && suggested.Width > 0
                && suggested.Height > 0
                && form.Size != suggested)
            {
                form.Size = suggested;
            }

            ApplyClamp(form);
        }
    }

    private static void ApplyClamp(Form form)
    {
        if (_states.TryGetValue(form, out var state))
        {
            ClampToWorkingArea(form, state.Owner ?? form, state.UseContentFractions);
        }
    }

    private sealed class ClampState
    {
        public IWin32Window? Owner { get; set; }

        public bool UseContentFractions { get; set; }

        public bool Hooked { get; set; }
    }
}
