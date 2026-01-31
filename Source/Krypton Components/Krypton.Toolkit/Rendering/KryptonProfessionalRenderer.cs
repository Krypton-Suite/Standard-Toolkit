#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2017 - 2026. All rights reserved.
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
    /// If the item is a KryptonToolStripMenuItem and it has any per-item palette overrides,
    /// draw its background/border here and return true to signal the caller to skip default painting.
    /// </summary>
    /// <param name="e">Item render event args.</param>
    /// <returns>True if the item was painted using per-item overrides; otherwise false.</returns>
    protected bool TryRenderMenuItemOverride(ToolStripItemRenderEventArgs e)
    {
        if (e.Item is not KryptonToolStripMenuItem ktmi)
        {
            return false;
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
            return false;
        }

        Rectangle rect = e.Item.ContentRectangle;
        if (hasBack)
        {
            // Use a simple fill based on Color1; this is sufficient for per-item testing without full renderer path
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
    #endregion

    #region OnRenderToolStripBorder
    /// <summary>
    /// Raises the RenderToolStripBorder event.
    /// </summary>
    /// <param name="e">An ToolStripRenderEventArgs containing the event data.</param>
    protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
    {
        // D0 not draw the annoying status strip single line that is not needed
        if (e.ToolStrip is not StatusStrip)
        {
            base.OnRenderToolStripBorder(e);
        }
    }
    #endregion
}