#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
///
/// </summary>
public class KryptonProfessionalRenderer : ToolStripProfessionalRenderer
{
    #region Identity
    /// <summary>
    /// Initialise a new instance of the KryptonProfessionalRenderer class.
    /// </summary>
    /// <param name="kct">Source for text colors.</param>
    public KryptonProfessionalRenderer([DisallowNull] KryptonColorTable kct)
        : base(kct)
    {
        Debug.Assert(kct is not null);
        KCT = kct ?? throw new ArgumentNullException(nameof(kct));
    }
    #endregion

    #region KCT
    /// <summary>
    /// Gets access to the KryptonColorTable instance.
    /// </summary>
    public KryptonColorTable KCT { get; }

    #endregion

    #region OnRenderItemText
    /// <summary>
    /// Raises the RenderItemText event.
    /// </summary>
    /// <param name="e">A ToolStripItemTextRenderEventArgs that contains the event data.</param>
    protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
    {
        if (e == null)
        {
            return;
        }

        if (IsContextMenuToolStrip(e.ToolStrip))
        {
            e.TextColor = e.Item.Enabled ? KCT.ContextMenuItemText : KCT.ContextMenuItemDisabledText;
        }

        base.OnRenderItemText(e);
    }
    #endregion

    #region OnRenderToolStripBackground
    /// <summary>
    /// Raises the RenderToolStripBackground event.
    /// </summary>
    /// <param name="e">An ToolStripRenderEventArgs containing the event data.</param>
    protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
    {
        if (e == null)
        {
            return;
        }

        if (IsContextMenuToolStrip(e.ToolStrip))
        {
            using (var backBrush = new SolidBrush(KCT.ContextMenuBack))
            {
                e.Graphics.FillRectangle(backBrush, e.AffectedBounds);
            }

            return;
        }

        base.OnRenderToolStripBackground(e);
    }
    #endregion

    #region OnRenderImageMargin
    /// <summary>
    /// Raises the RenderImageMargin event.
    /// </summary>
    /// <param name="e">An ToolStripRenderEventArgs containing the event data.</param>
    protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
    {
        if (e == null)
        {
            return;
        }

        if (IsContextMenuToolStrip(e.ToolStrip))
        {
            using (var backBrush = new SolidBrush(KCT.ContextMenuImageColumnBack))
            {
                e.Graphics.FillRectangle(backBrush, e.AffectedBounds);
            }

            return;
        }

        base.OnRenderImageMargin(e);
    }
    #endregion

    #region OnRenderMenuItemBackground
    /// <summary>
    /// Raises the RenderMenuItemBackground event.
    /// </summary>
    /// <param name="e">An ToolStripItemRenderEventArgs containing the event data.</param>
    protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
    {
        if (e == null)
        {
            return;
        }

        if (TryRenderMenuItemOverride(e))
        {
            return;
        }

        base.OnRenderMenuItemBackground(e);
    }
    #endregion

    #region OnRenderItemImage
    /// <summary>
    /// Raises the RenderItemImage event.
    /// </summary>
    /// <param name="e">An ToolStripItemImageRenderEventArgs containing the event data.</param>
    protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
    {
        // Is this a min/restore/close pendant button
        if (e.Item.GetType().ToString() == "System.Windows.Forms.MdiControlStrip+ControlBoxMenuItem")
        {
            // Get access to the owning form of the mdi control strip
            if (e.ToolStrip!.Parent!.TopLevelControl is Form f)
            {
                // Get the mdi control strip instance
                PropertyInfo? piMCS = typeof(Form).GetProperty(@"MdiControlStrip", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField)!;
                if (piMCS != null)
                {
                    var mcs = piMCS.GetValue(f, null);
                    if (mcs != null)
                    {
                        // Get the min/restore/close internal menu items
                        Type mcsType = mcs.GetType();
                        FieldInfo? fiM = mcsType.GetField("minimize", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField)!;
                        FieldInfo? fiR = mcsType.GetField("restore", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField)!;
                        FieldInfo? fiC = mcsType.GetField("close", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField)!;
                        if ((fiM != null) && (fiR != null) && (fiC != null))
                        {
#pragma warning disable IDE0019 // Use pattern matching
                            var m = fiM.GetValue(mcs) as ToolStripMenuItem;
                            var r = fiR.GetValue(mcs) as ToolStripMenuItem;
                            var c = fiC.GetValue(mcs) as ToolStripMenuItem;
#pragma warning restore IDE0019 // Use pattern matching
                            if ((m != null) && (r != null) && (c != null))
                            {
                                // Compare the event provided image with the internal cached ones to discover the type of pendant button we are drawing
                                var specStyle = PaletteButtonSpecStyle.Generic;
                                if (m.Image == e.Image)
                                {
                                    specStyle = PaletteButtonSpecStyle.PendantMin;
                                }
                                else if (r.Image == e.Image)
                                {
                                    specStyle = PaletteButtonSpecStyle.PendantRestore;
                                }
                                else if (c.Image == e.Image)
                                {
                                    specStyle = PaletteButtonSpecStyle.PendantClose;
                                }

                                // A match, means we have a known pendant button
                                if (specStyle != PaletteButtonSpecStyle.Generic)
                                {
                                    // Grab the palette pendant details needed for drawing
                                    Image? paletteImage = KCT.Palette.GetButtonSpecImage(specStyle, PaletteState.Normal);
                                    Color transparentColor = KCT.Palette.GetButtonSpecImageTransparentColor(specStyle);

                                    // Finally we actually have an image to draw!
                                    if (paletteImage != null)
                                    {
                                        using var attribs = new ImageAttributes();
                                        // Setup mapping to make required color transparent
                                        var remap = new ColorMap
                                        {
                                            OldColor = transparentColor,
                                            NewColor = Color.Transparent
                                        };
                                        attribs.SetRemapTable([remap]);

                                        // Phew, actually draw the darn thing
                                        e.Graphics.DrawImage(paletteImage, e.ImageRectangle,
                                            0, 0, e.Image!.Width, e.Image.Height,
                                            GraphicsUnit.Pixel, attribs);

                                        // Do not let base class draw system defined image
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        base.OnRenderItemImage(e);
    }
    #endregion

    #region StatusStrip Overrides Helper
    /// <summary>
    /// Resolves per-control StatusStrip background overrides (Color1/Color2/Style/Angle) if present.
    /// Returns true if an override was applied and the caller should skip default painting.
    /// </summary>
    /// <param name="e">Render event args.</param>
    /// <param name="graphics">Graphics to draw on.</param>
    /// <returns>True if painted using per-control overrides; otherwise false.</returns>
    protected bool TryRenderStatusStripOverride(ToolStripRenderEventArgs e, Graphics graphics)
    {
        if (e.ToolStrip is not StatusStrip statusStrip)
        {
            return false;
        }

        // Only KryptonStatusStrip exposes PaletteBack overrides
        if (statusStrip is not KryptonStatusStrip kryptonStatus)
        {
            return false;
        }

        // Determine state palette to use
        PaletteBack state = kryptonStatus.Enabled ? kryptonStatus.StateNormal : kryptonStatus.StateDisabled;

        // Consider overrides present if any state has non-default values, or Draw is explicitly disabled
        bool hasAnyOverride = !kryptonStatus.StateCommon.IsDefault ||
                              !kryptonStatus.StateNormal.IsDefault ||
                              !kryptonStatus.StateDisabled.IsDefault ||
                              state.Draw == InheritBool.False;
        if (state.Draw == InheritBool.False)
        {
            // Explicitly suppress painting and skip default renderer
            return true;
        }

        if (!hasAnyOverride)
        {
            return false;
        }

        // Establish drawing rect: skip top 2px to respect border line drawing (as existing renderers do)
        RectangleF backRect = new RectangleF(0, 1.5f, e.ToolStrip.Width, e.ToolStrip.Height - 2);
        if (!(backRect.Width > 0 && backRect.Height > 0))
        {
            return false;
        }

        // Resolve effective settings using inheritance (so StateCommon overrides are honored)
        var effectiveStyle = state.GetBackColorStyle(PaletteState.Normal);
        var angle = state.GetBackColorAngle(PaletteState.Normal);
        var color1 = state.GetBackColor1(PaletteState.Normal);
        var color2 = state.GetBackColor2(PaletteState.Normal);

        // Default style when the user provides one color only: Solid
        if (effectiveStyle == PaletteColorStyle.Inherit)
        {
            effectiveStyle = (color2 == GlobalStaticValues.EMPTY_COLOR || color2.IsEmpty)
                ? PaletteColorStyle.Solid
                : PaletteColorStyle.Linear;
        }

        if (Math.Abs(angle - (-1f)) <= float.Epsilon)
        {
            angle = 90f;
        }

        // Paint according to style
        Rectangle rect = Rectangle.Truncate(backRect);

        switch (effectiveStyle)
        {
            case PaletteColorStyle.Solid:
            {
                using var brush = new SolidBrush((color1 == GlobalStaticValues.EMPTY_COLOR || color1.IsEmpty)
                    ? KCT.StatusStripGradientEnd : color1);
                graphics.FillRectangle(brush, rect);
                break;
            }
            default:
            {
                Color a = (color1 == GlobalStaticValues.EMPTY_COLOR || color1.IsEmpty) ? KCT.StatusStripGradientBegin : color1;
                Color b = (color2 == GlobalStaticValues.EMPTY_COLOR || color2.IsEmpty) ? KCT.StatusStripGradientEnd : color2;
                using var brush = new LinearGradientBrush(rect, a, b, angle);
                graphics.FillRectangle(brush, rect);
                break;
            }
        }

        return true;
    }
    #endregion

    #region MenuItem Per-Item Override Helper
    /// <summary>
    /// If the menu item has any per-item or color table overrides,
    /// draw its background/border here and return true to signal the caller to skip default painting.
    /// </summary>
    /// <param name="e">Item render event args.</param>
    /// <returns>True if the item was painted using overrides; otherwise false.</returns>
    protected bool TryRenderMenuItemOverride(ToolStripItemRenderEventArgs e)
    {
        if (TryRenderMenuItemPaletteOverride(e))
        {
            return true;
        }

        return TryRenderMenuItemColorTableOverride(e);
    }

    private bool TryRenderMenuItemPaletteOverride(ToolStripItemRenderEventArgs e)
    {
        var ktmi = e.Item as KryptonToolStripMenuItem;
        if (ktmi == null)
        {
            return TryRenderContextMenuItemBackground(e);
        }

        // Resolve which highlight palette to use based on state
        PaletteDoubleMetric highlight;
        if (!ktmi.Enabled)
        {
            highlight = ktmi.StateDisabled.ItemHighlight;
        }
        else if (ktmi.Pressed || ktmi.Selected)
        {
            // Checked does not expose its own highlight; use StateHighlight
            highlight = ktmi.StateHighlight.ItemHighlight;
        }
        else
        {
            highlight = ktmi.StateNormal.ItemHighlight;
        }

        bool hasBack = !highlight.IsDefault && !highlight.Back.IsDefault;
        bool hasBorder = !highlight.IsDefault && !highlight.Border.IsDefault;
        if (!hasBack && !hasBorder)
        {
            return TryRenderContextMenuItemBackground(e);
        }

        Rectangle rect = e.Item.ContentRectangle;
        if (hasBack || hasBorder)
        {
            // Use Color1 so border-only overrides still preserve a complete item background.
            var state = !ktmi.Enabled ? PaletteState.Disabled : (ktmi.Pressed || ktmi.Selected ? PaletteState.Tracking : PaletteState.Normal);
            Color c1 = highlight.Back.GetBackColor1(state);
            using var brush = new SolidBrush(c1);
            e.Graphics.FillRectangle(brush, rect);
        }
        if (hasBorder)
        {
            var state = !ktmi.Enabled ? PaletteState.Disabled : (ktmi.Pressed || ktmi.Selected ? PaletteState.Tracking : PaletteState.Normal);
            Color b1 = highlight.Border.GetBorderColor1(state);
            using var pen = new Pen(b1);
            rect.Width -= 1; rect.Height -= 1;
            e.Graphics.DrawRectangle(pen, rect);
        }

        return true;
    }

    private bool TryRenderMenuItemColorTableOverride(ToolStripItemRenderEventArgs e)
    {
        var internalKCT = KCT as KryptonInternalKCT;
        if (internalKCT == null || e.ToolStrip == null)
        {
            return false;
        }

        if (!(e.ToolStrip is MenuStrip || e.ToolStrip is ContextMenuStrip || e.ToolStrip is ToolStripDropDownMenu))
        {
            return false;
        }

        if (!e.Item.Enabled)
        {
            return false;
        }

        bool pressedMenuItem = e.Item.Pressed && e.ToolStrip is MenuStrip;
        bool selectedMenuItem = e.Item.Selected;
        if (!pressedMenuItem && !selectedMenuItem)
        {
            return false;
        }

        bool hasBorder = HasMenuItemOverrideColor(internalKCT.InternalMenuItemBorder);
        bool hasBack;
        Color color1;
        Color color2;
        Color colorMiddle;
        bool useMiddle;

        if (pressedMenuItem)
        {
            hasBack = HasMenuItemOverrideColor(internalKCT.InternalMenuItemPressedGradientBegin) ||
                      HasMenuItemOverrideColor(internalKCT.InternalMenuItemPressedGradientMiddle) ||
                      HasMenuItemOverrideColor(internalKCT.InternalMenuItemPressedGradientEnd);
            color1 = ResolveMenuItemOverrideColor(internalKCT.InternalMenuItemPressedGradientBegin, KCT.MenuItemPressedGradientBegin);
            color2 = ResolveMenuItemOverrideColor(internalKCT.InternalMenuItemPressedGradientEnd, KCT.MenuItemPressedGradientEnd);
            colorMiddle = ResolveMenuItemOverrideColor(internalKCT.InternalMenuItemPressedGradientMiddle, KCT.MenuItemPressedGradientMiddle);
            useMiddle = HasMenuItemOverrideColor(internalKCT.InternalMenuItemPressedGradientMiddle);
        }
        else
        {
            bool isDropDownMenuItem = !(e.ToolStrip is MenuStrip);
            bool hasSelected = isDropDownMenuItem && HasMenuItemOverrideColor(internalKCT.InternalMenuItemSelected);
            bool hasSelectedGradient = HasMenuItemOverrideColor(internalKCT.InternalMenuItemSelectedGradientBegin) ||
                                       HasMenuItemOverrideColor(internalKCT.InternalMenuItemSelectedGradientEnd);
            bool useSelectedSolid = hasSelected || (isDropDownMenuItem && !hasSelectedGradient);
            hasBack = hasSelected || hasSelectedGradient;
            color1 = useSelectedSolid
                ? ResolveMenuItemOverrideColor(internalKCT.InternalMenuItemSelected, KCT.MenuItemSelected)
                : ResolveMenuItemOverrideColor(internalKCT.InternalMenuItemSelectedGradientBegin, KCT.MenuItemSelectedGradientBegin);
            color2 = useSelectedSolid
                ? color1
                : ResolveMenuItemOverrideColor(internalKCT.InternalMenuItemSelectedGradientEnd, KCT.MenuItemSelectedGradientEnd);
            colorMiddle = GlobalStaticValues.EMPTY_COLOR;
            useMiddle = false;
        }

        if (!hasBack && !hasBorder)
        {
            return false;
        }

        Rectangle rect = new Rectangle(Point.Empty, e.Item.Bounds.Size);
        if (e.ToolStrip is ContextMenuStrip || e.ToolStrip is ToolStripDropDownMenu)
        {
            rect.X = 2;
            rect.Width -= 3;
        }

        if (rect.Width <= 0 || rect.Height <= 0)
        {
            return false;
        }

        DrawMenuItemOverrideBackground(e.Graphics, rect, color1, color2, colorMiddle, useMiddle);

        Color border = ResolveMenuItemOverrideColor(internalKCT.InternalMenuItemBorder, KCT.MenuItemBorder);
        DrawMenuItemOverrideBorder(e.Graphics, rect, border);

        return true;
    }

    private static bool HasMenuItemOverrideColor(Color color) => color != GlobalStaticValues.EMPTY_COLOR && !color.IsEmpty;

    private static Color ResolveMenuItemOverrideColor(Color color, Color fallback) => HasMenuItemOverrideColor(color) ? color : fallback;

    private static void DrawMenuItemOverrideBackground(Graphics graphics,
        Rectangle rect,
        Color color1,
        Color color2,
        Color colorMiddle,
        bool useMiddle)
    {
        if (color1 == color2 && !useMiddle)
        {
            using (var brush = new SolidBrush(color1))
            {
                graphics.FillRectangle(brush, rect);
            }
        }
        else
        {
            using (var brush = new LinearGradientBrush(rect, color1, color2, 90f))
            {
                if (useMiddle)
                {
                    brush.InterpolationColors = new ColorBlend
                    {
                        Colors = new[] { color1, colorMiddle, color2 },
                        Positions = new[] { 0f, 0.5f, 1f }
                    };
                }

                graphics.FillRectangle(brush, rect);
            }
        }
    }

    private static void DrawMenuItemOverrideBorder(Graphics graphics, Rectangle rect, Color border)
    {
        rect.Width -= 1;
        rect.Height -= 1;

        using (var pen = new Pen(border))
        {
            graphics.DrawRectangle(pen, rect);
        }
    }
    #endregion

    #region ContextMenu Helper
    /// <summary>
    /// Determines if the tool strip is being used as a context menu.
    /// </summary>
    /// <param name="toolStrip">Tool strip to test.</param>
    /// <returns>True if the tool strip is a context menu; otherwise false.</returns>
    protected static bool IsContextMenuToolStrip(ToolStrip? toolStrip) =>
        toolStrip is ContextMenuStrip || toolStrip is ToolStripDropDownMenu;

    private bool TryRenderContextMenuItemBackground(ToolStripItemRenderEventArgs e)
    {
        if (!IsContextMenuToolStrip(e.ToolStrip) || !e.Item.Enabled || !e.Item.Selected)
        {
            return false;
        }

        Rectangle backRect = new Rectangle(2, 0, e.Item.Bounds.Width - 3, e.Item.Bounds.Height);
        if ((backRect.Width <= 0) || (backRect.Height <= 0))
        {
            return false;
        }

        Color back1 = KCT.ContextMenuItemHighlightBack1;
        Color back2 = KCT.ContextMenuItemHighlightBack2;
        if (back1 == back2)
        {
            using (var backBrush = new SolidBrush(back1))
            {
                e.Graphics.FillRectangle(backBrush, backRect);
            }
        }
        else
        {
            using (var backBrush = new LinearGradientBrush(backRect, back1, back2, 90f))
            {
                e.Graphics.FillRectangle(backBrush, backRect);
            }
        }

        using (var borderPen = new Pen(KCT.ContextMenuItemHighlightBorder))
        {
            backRect.Width--;
            backRect.Height--;
            e.Graphics.DrawRectangle(borderPen, backRect);
        }

        return true;
    }
    #endregion

    #region OnRenderToolStripBorder
    /// <summary>
    /// Raises the RenderToolStripBorder event.
    /// </summary>
    /// <param name="e">An ToolStripRenderEventArgs containing the event data.</param>
    protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
    {
        if (e == null)
        {
            return;
        }

        if (IsContextMenuToolStrip(e.ToolStrip))
        {
            using (var borderPen = new Pen(KCT.ContextMenuBorder))
            {
                Rectangle borderRect = e.AffectedBounds;
                borderRect.Width--;
                borderRect.Height--;
                e.Graphics.DrawRectangle(borderPen, borderRect);
            }

            return;
        }

        // D0 not draw the annoying status strip single line that is not needed
        if (e.ToolStrip is not StatusStrip)
        {
            base.OnRenderToolStripBorder(e);
        }
    }
    #endregion
}
