#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// ToolStrip/MenuStrip renderer for Windows XP visual styles.
/// </summary>
public class KryptonWindowsXPRenderer : KryptonProfessionalRenderer
{
    private readonly Func<Bitmap?> _buttonSprite;

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonWindowsXPRenderer"/> class.
    /// </summary>
    /// <param name="kct">Source of palette values.</param>
    /// <param name="buttonSprite">Factory for the XP push-button sprite sheet.</param>
    public KryptonWindowsXPRenderer([DisallowNull] KryptonColorTable kct, Func<Bitmap?> buttonSprite)
        : base(kct) =>
        _buttonSprite = buttonSprite ?? throw new ArgumentNullException(nameof(buttonSprite));

    /// <inheritdoc />
    protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
    {
        var bounds = new Rectangle(Point.Empty, e.ToolStrip.Size);
        if (bounds.Width <= 0 || bounds.Height <= 0)
        {
            return;
        }

        // Classic XP drop-down menus are a flat white/cream surface. Menu bars
        // and toolbars use their own restrained vertical gradient.
        if (e.ToolStrip is ContextMenuStrip or ToolStripDropDownMenu)
        {
            using var menuBrush = new SolidBrush(KCT.ContextMenuBack);
            e.Graphics.FillRectangle(menuBrush, bounds);
            return;
        }

        var begin = e.ToolStrip is MenuStrip
            ? KCT.MenuStripGradientBegin
            : KCT.ToolStripGradientBegin;
        var end = e.ToolStrip is MenuStrip
            ? KCT.MenuStripGradientEnd
            : KCT.ToolStripGradientEnd;
        if (begin == end || bounds.Height == 1)
        {
            using var brush = new SolidBrush(begin);
            e.Graphics.FillRectangle(brush, bounds);
            return;
        }

        using var gradient = new LinearGradientBrush(bounds, begin, end, LinearGradientMode.Vertical);
        e.Graphics.FillRectangle(gradient, bounds);
    }

    /// <inheritdoc />
    protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
    {
        if (e.Item is not ToolStripMenuItem menuItem)
        {
            base.OnRenderMenuItemBackground(e);
            return;
        }

        var bounds = new Rectangle(Point.Empty, e.Item.Size);
        if (menuItem.Selected)
        {
            using var brush = new SolidBrush(KCT.MenuItemSelected);
            using var pen = new Pen(KCT.MenuItemBorder);
            e.Graphics.FillRectangle(brush, bounds);
            if (bounds.Width > 1 && bounds.Height > 1)
            {
                e.Graphics.DrawRectangle(pen, 0, 0, bounds.Width - 1, bounds.Height - 1);
            }

            return;
        }

        if (e.ToolStrip is ContextMenuStrip or ToolStripDropDownMenu)
        {
            using var brush = new SolidBrush(KCT.ContextMenuBack);
            e.Graphics.FillRectangle(brush, bounds);
            return;
        }

        // MenuStrip items are painted by the strip background until tracked.
        if (e.ToolStrip is MenuStrip)
        {
            return;
        }

        base.OnRenderMenuItemBackground(e);
    }

    /// <inheritdoc />
    protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
    {
        if (e.ToolStrip is ContextMenuStrip or ToolStripDropDownMenu)
        {
            var bounds = new Rectangle(Point.Empty, e.ToolStrip.Size);
            if (bounds.Width > 1 && bounds.Height > 1)
            {
                using var pen = new Pen(KCT.ContextMenuBorder);
                e.Graphics.DrawRectangle(pen, 0, 0, bounds.Width - 1, bounds.Height - 1);
            }

            return;
        }

        var stripBounds = new Rectangle(Point.Empty, e.ToolStrip.Size);
        if (stripBounds.Width > 1 && stripBounds.Height > 1)
        {
            using var pen = new Pen(KCT.ToolStripBorder);
            e.Graphics.DrawRectangle(pen, 0, 0, stripBounds.Width - 1, stripBounds.Height - 1);
        }
    }

    /// <inheritdoc />
    protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
    {
        var bounds = new Rectangle(Point.Empty, e.Item.Size);
        if (bounds.Width <= 0 || bounds.Height <= 0)
        {
            return;
        }

        using var darkPen = new Pen(KCT.SeparatorDark);
        using var lightPen = new Pen(KCT.SeparatorLight);

        if (e.Vertical)
        {
            var x = bounds.Left + bounds.Width / 2;
            e.Graphics.DrawLine(darkPen, x, bounds.Top + 2, x, bounds.Bottom - 3);
            e.Graphics.DrawLine(lightPen, x + 1, bounds.Top + 2, x + 1, bounds.Bottom - 3);
            return;
        }

        var inset = e.ToolStrip is ContextMenuStrip or ToolStripDropDownMenu ? 28 : 3;
        var left = bounds.Left + inset;
        var right = bounds.Right - 4;
        var y = bounds.Top + bounds.Height / 2;
        if (right >= left)
        {
            e.Graphics.DrawLine(darkPen, left, y, right, y);
            if (y + 1 < bounds.Bottom)
            {
                e.Graphics.DrawLine(lightPen, left, y + 1, right, y + 1);
            }
        }
    }

    /// <inheritdoc />
    protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
    {
        if (e.Item is ToolStripButton button && e.Item.Width > 0 && e.Item.Height > 0)
        {
            var state = !button.Enabled
                ? 4
                : button.Pressed
                    ? 2
                    : button.Selected
                        ? 1
                        : button.Checked
                            ? 3
                            : 0;
            var sprite = _buttonSprite();
            if (sprite != null)
            {
                WindowsXPThemeResourceBridge.DrawSpriteSlice(e.Graphics, sprite,
                    new Rectangle(Point.Empty, e.Item.Size), state, 5);
                return;
            }
        }

        base.OnRenderButtonBackground(e);
    }
}

/// <summary>
/// ToolStrip and MenuStrip renderer for Windows XP sprite themes.
/// Menu-bar tracking and toolbar buttons use the theme push-button sheet.
/// </summary>
public sealed class KryptonWindowsXPLunaRenderer : KryptonWindowsXPRenderer
{
    /// <summary>Initialize a new instance of the <see cref="KryptonWindowsXPLunaRenderer"/> class.</summary>
    /// <param name="kct">Source of palette values.</param>
    public KryptonWindowsXPLunaRenderer([DisallowNull] KryptonColorTable kct)
        : base(kct, () => WindowsXPThemeResourceBridge.LunaButton)
    {
    }

    /// <inheritdoc />
    protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
    {
        if (e.Item is ToolStripMenuItem menuItem &&
            menuItem.Selected &&
            e.ToolStrip is MenuStrip &&
            WindowsXPLunaArt.TryGetTheme(e.ToolStrip, out WindowsXPSpriteTheme theme) &&
            WindowsXPLunaArt.TryGet(WindowsXPLunaArt.Sheet(theme, WindowsXPLunaPart.Button), out WindowsXPLunaSprite button))
        {
            var faceState = menuItem.Pressed ? PaletteState.Pressed : PaletteState.Normal;
            WindowsXPLunaArt.Draw(e.Graphics, button, new Rectangle(Point.Empty, e.Item.Size), faceState);
            return;
        }

        base.OnRenderMenuItemBackground(e);
    }

    /// <inheritdoc />
    protected override bool TryApplyItemTextColor(ToolStripItemTextRenderEventArgs e)
    {
        if (e?.Item is ToolStripMenuItem menuItem &&
            menuItem.Selected &&
            menuItem.Enabled &&
            e.ToolStrip is not MenuStrip &&
            KCT is KryptonWindowsXPLunaColorTable luna)
        {
            e.TextColor = luna.HighlightText;
            return true;
        }

        return false;
    }

    /// <inheritdoc />
    protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
    {
        if (e.Item is ToolStripButton button &&
            button.Width > 0 &&
            button.Height > 0 &&
            WindowsXPLunaArt.TryGetTheme(e.ToolStrip, out WindowsXPSpriteTheme theme) &&
            WindowsXPLunaArt.TryGet(WindowsXPLunaArt.Sheet(theme, WindowsXPLunaPart.Button), out WindowsXPLunaSprite buttonSprite))
        {
            var paletteState = !button.Enabled
                ? PaletteState.Disabled
                : button.Pressed
                    ? PaletteState.Pressed
                    : button.Selected
                        ? PaletteState.Tracking
                        : button.Checked
                            ? PaletteState.CheckedNormal
                            : PaletteState.Normal;
            WindowsXPLunaArt.Draw(e.Graphics, buttonSprite, new Rectangle(Point.Empty, e.Item.Size), paletteState);
            return;
        }

        base.OnRenderButtonBackground(e);
    }
}
