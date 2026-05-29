#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// DOS/RetroUI-style renderer: blocky 8px 2D button shadow and flat fills.
/// </summary>
/// <seealso cref="RenderOffice2010" />
public sealed class RenderRetro : RenderOffice2010
{
    #region IRenderer Overrides
    /// <inheritdoc />
    public override ToolStripRenderer RenderToolStrip([DisallowNull] PaletteBase? colorPalette)
    {
        if (colorPalette == null)
        {
            throw new ArgumentNullException(nameof(colorPalette));
        }

        return new KryptonRetroRenderer(colorPalette.ColorTable);
    }
    #endregion

    #region RenderStandardBorder
    /// <inheritdoc />
    public override GraphicsPath GetBackPath([DisallowNull] RenderContext context,
        Rectangle rect,
        [DisallowNull] IPaletteBorder palette,
        VisualOrientation orientation,
        PaletteState state)
    {
        if (rect.Width <= 0 || rect.Height <= 0)
        {
            return base.GetBackPath(context, rect, palette, orientation, state);
        }

        if (palette.GetBorderDraw(state) != InheritBool.True
            || !CommonHelper.HasABorder(palette.GetBorderDrawBorders(state))
            || palette.GetBorderWidth(state) <= 0)
        {
            var path = new GraphicsPath();
            path.AddRectangle(rect);
            return path;
        }

        return base.GetBackPath(context, rect, palette, orientation, state);
    }

    /// <inheritdoc />
    public override void DrawBorder(RenderContext context,
        Rectangle rect,
        IPaletteBorder palette,
        VisualOrientation orientation,
        PaletteState state)
    {
        if (palette == null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        if (palette.GetBorderWidth(state) <= 0
            || palette.GetBorderDraw(state) != InheritBool.True
            || !CommonHelper.HasABorder(palette.GetBorderDrawBorders(state)))
        {
            return;
        }

        base.DrawBorder(context, rect, palette, orientation, state);
    }
    #endregion

    #region RenderStandard
    /// <inheritdoc />
    public override IDisposable? DrawBack(RenderContext context,
        Rectangle rect,
        GraphicsPath path,
        IPaletteBack palette,
        VisualOrientation orientation,
        PaletteState state,
        IDisposable? memento)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (path == null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        if (palette == null)
        {
            throw new ArgumentNullException(nameof(palette));
        }

        if (rect.Width <= 0 || rect.Height <= 0)
        {
            return base.DrawBack(context, rect, path, palette, orientation, state, memento);
        }

        if (RetroRenderHelper.IsRetroProgressValueBack(palette, state, out Color progressValueColor))
        {
            DrawRetroProgressValueBack(context, rect, path, progressValueColor);
            return memento;
        }

        if (!RetroRenderHelper.IsRetroButtonBack(palette, state))
        {
            return base.DrawBack(context, rect, path, palette, orientation, state, memento);
        }

        DrawRetroButtonBack(context, rect, path, palette, state);
        return memento;
    }
    #endregion

    #region Implementation
    private static void DrawRetroButtonBack(RenderContext context,
        Rectangle rect,
        GraphicsPath path,
        IPaletteBack palette,
        PaletteState state)
    {
        var g = context.Graphics;
        var oldSmoothing = g.SmoothingMode;
        var oldPixel = g.PixelOffsetMode;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;

        try
        {
            int shadow = RetroRenderHelper.GetButtonShadowSize(rect);
            bool pressed = state is PaletteState.Pressed or PaletteState.CheckedPressed;
            Color face = palette.GetBackColor1(state);
            Color frame = RetroRenderHelper.GetButtonFrameColor(KryptonManager.CurrentGlobalPalette);

            var region = g.Clip;
            g.SetClip(path, System.Drawing.Drawing2D.CombineMode.Intersect);

            Color chrome = RetroRenderHelper.GetChromeBackgroundColor(KryptonManager.CurrentGlobalPalette);
            using (var chromeBrush = new SolidBrush(chrome))
            {
                g.FillRectangle(chromeBrush, rect);
            }

            if (!pressed && rect.Width > shadow && rect.Height > shadow)
            {
                using var shadowBrush = new SolidBrush(frame);
                g.FillRectangle(shadowBrush, rect.X + shadow, rect.Y + shadow, rect.Width - shadow, rect.Height - shadow);
            }

            int faceX = pressed ? rect.X + shadow : rect.X;
            int faceY = pressed ? rect.Y + shadow : rect.Y;
            int faceW = Math.Max(0, rect.Width - shadow);
            int faceH = Math.Max(0, rect.Height - shadow);

            if (faceW > 0 && faceH > 0)
            {
                using var faceBrush = new SolidBrush(face);
                g.FillRectangle(faceBrush, faceX, faceY, faceW, faceH);

                using var borderPen = new Pen(frame, 1f);
                int right = faceX + faceW - 1;
                int bottom = faceY + faceH - 1;
                g.DrawLine(borderPen, faceX, faceY, right, faceY);
                g.DrawLine(borderPen, faceX, faceY, faceX, bottom);
                g.DrawLine(borderPen, right, faceY, right, bottom);
                g.DrawLine(borderPen, faceX, bottom, right, bottom);
            }

            g.Clip = region;
        }
        finally
        {
            g.SmoothingMode = oldSmoothing;
            g.PixelOffsetMode = oldPixel;
        }
    }

    private static void DrawRetroProgressValueBack(RenderContext context,
        Rectangle rect,
        GraphicsPath path,
        Color valueColor)
    {
        var g = context.Graphics;
        var oldSmoothing = g.SmoothingMode;
        var oldPixel = g.PixelOffsetMode;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;

        try
        {
            var region = g.Clip;
            g.SetClip(path, System.Drawing.Drawing2D.CombineMode.Intersect);

            using (var valueBrush = new SolidBrush(valueColor))
            {
                g.FillRectangle(valueBrush, rect);
            }

            using (var borderPen = new Pen(Color.Black, 1f))
            {
                g.DrawRectangle(borderPen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            }

            g.Clip = region;
        }
        finally
        {
            g.SmoothingMode = oldSmoothing;
            g.PixelOffsetMode = oldPixel;
        }
    }
    #endregion
}

/// <summary>
/// Shared Retro button metrics and detection for palette/renderer.
/// </summary>
internal static class RetroRenderHelper
{
    private const int DefaultButtonShadowSize = 8;

    private const int MaxButtonShadowSize = 32;

    private static int _buttonShadowSize = DefaultButtonShadowSize;

    internal static int ButtonShadowSize => _buttonShadowSize;

    internal static int ButtonShadowVerticalPaddingCompensation => _buttonShadowSize;// / 2;

    internal static int SetButtonShadowSize(int value)
    {
        int clamped = Math.Max(0, Math.Min(MaxButtonShadowSize, value));
        _buttonShadowSize = clamped;
        return clamped;
    }

    internal static void ResetButtonShadowSize() => _buttonShadowSize = DefaultButtonShadowSize;

    internal static bool IsRetroPalette(PaletteBase? palette) => palette is PaletteRetroBase;

    internal static PaletteRetroBase? AsRetroPalette(PaletteBase? palette) => palette as PaletteRetroBase;

    internal static Color GetChromeBackgroundColor(PaletteBase? palette) =>
        AsRetroPalette(palette)?.ChromeBackgroundColor ?? Color.FromArgb(0, 160, 160);

    internal static Color GetButtonFrameColor(PaletteBase? palette) =>
        AsRetroPalette(palette)?.RetroButtonFrameColor ?? Color.Black;

    internal static bool UsesRetroPushButtonChrome(PaletteBackStyle style) =>
        !IsChromeAdjacentButtonBackStyle(style) && IsRetroPushButtonBackStyle(style);

    internal static bool UsesRetroPushButtonChrome(PaletteBorderStyle style) =>
        !IsChromeAdjacentButtonBorderStyle(style) && IsRetroPushButtonBorderStyle(style);

    internal static bool UsesRetroPushButtonChrome(PaletteContentStyle style) =>
        !IsChromeAdjacentButtonContentStyle(style) && IsRetroPushButtonContentStyle(style);

    internal static bool IsRetroButtonBack(IPaletteBack palette, PaletteState state)
    {
        if (CommonHelper.IsOverrideState(state))
        {
            return false;
        }

        if (palette.GetBackColorStyle(state) != PaletteColorStyle.Solid)
        {
            return false;
        }

        if (IsChromeAdjacentButtonBack(palette))
        {
            return false;
        }

        Color c = palette.GetBackColor1(state);
        return IsRetroButtonFaceColor(c, AsRetroPalette(KryptonManager.CurrentGlobalPalette));
    }

    internal static bool IsRetroProgressValueBack(IPaletteBack palette, PaletteState state, out Color valueColor)
    {
        valueColor = GlobalStaticVariables.EMPTY_COLOR;

        if (CommonHelper.IsOverrideState(state)
            || palette.GetBackColorStyle(state) != PaletteColorStyle.GlassNormalFull)
        {
            return false;
        }

        PaletteRetroBase? retro = AsRetroPalette(KryptonManager.CurrentGlobalPalette);
        if (retro == null)
        {
            return false;
        }

        Color c = palette.GetBackColor1(state);
        if (c.ToArgb() != Color.Green.ToArgb()
            && c.ToArgb() != retro.ProgressValueColor.ToArgb())
        {
            return false;
        }

        valueColor = retro.ProgressValueColor;
        return true;
    }

    internal static bool IsRetroButtonFaceColor(Color color, PaletteRetroBase? retro)
    {
        if (retro == null)
        {
            return false;
        }

        return color.ToArgb() == retro.ButtonFaceColor.ToArgb()
               || color.ToArgb() == retro.ButtonDisabledColor.ToArgb();
    }

    internal static int GetButtonShadowSize(Rectangle rect)
    {
        if (_buttonShadowSize <= 0)
        {
            return 0;
        }

        return Math.Min(_buttonShadowSize, Math.Max(1, Math.Min(rect.Width, rect.Height) / 3));
    }

    internal static bool IsRetroButtonContentStyle(PaletteContentStyle style, PaletteState state)
    {
        if (CommonHelper.IsOverrideStateExclude(state, PaletteState.NormalDefaultOverride))
        {
            return false;
        }

        return UsesRetroPushButtonChrome(style);
    }

    private static bool IsChromeAdjacentButtonBackStyle(PaletteBackStyle style) =>
        style switch
        {
            PaletteBackStyle.ButtonForm or PaletteBackStyle.ButtonFormClose or PaletteBackStyle.ButtonInputControl
                or PaletteBackStyle.ButtonButtonSpec or PaletteBackStyle.ButtonCalendarDay
                or PaletteBackStyle.ButtonListItem => true,
            _ => false
        };

    private static bool IsChromeAdjacentButtonBack(IPaletteBack palette) =>
        palette is PaletteBackToPalette back && IsChromeAdjacentButtonBackStyle(back.BackStyle);

    private static bool IsChromeAdjacentButtonBorderStyle(PaletteBorderStyle style) =>
        style switch
        {
            PaletteBorderStyle.ButtonForm or PaletteBorderStyle.ButtonFormClose or PaletteBorderStyle.ButtonInputControl
                or PaletteBorderStyle.ButtonButtonSpec or PaletteBorderStyle.ButtonCalendarDay
                or PaletteBorderStyle.ButtonListItem => true,
            _ => false
        };

    private static bool IsChromeAdjacentButtonContentStyle(PaletteContentStyle style) =>
        style switch
        {
            PaletteContentStyle.ButtonForm or PaletteContentStyle.ButtonFormClose or PaletteContentStyle.ButtonInputControl
                or PaletteContentStyle.ButtonButtonSpec or PaletteContentStyle.ButtonCalendarDay
                or PaletteContentStyle.ButtonListItem => true,
            _ => false
        };

    private static bool IsRetroPushButtonBackStyle(PaletteBackStyle style) =>
        style switch
        {
            PaletteBackStyle.ButtonStandalone or PaletteBackStyle.ButtonAlternate or PaletteBackStyle.ButtonLowProfile
                or PaletteBackStyle.ButtonBreadCrumb or PaletteBackStyle.ButtonCommand or PaletteBackStyle.ButtonCluster
                or PaletteBackStyle.ButtonCustom1 or PaletteBackStyle.ButtonCustom2 or PaletteBackStyle.ButtonCustom3 => true,
            _ => false
        };

    private static bool IsRetroPushButtonBorderStyle(PaletteBorderStyle style) =>
        style switch
        {
            PaletteBorderStyle.ButtonStandalone or PaletteBorderStyle.ButtonAlternate or PaletteBorderStyle.ButtonLowProfile
                or PaletteBorderStyle.ButtonBreadCrumb or PaletteBorderStyle.ButtonCommand or PaletteBorderStyle.ButtonCluster
                or PaletteBorderStyle.ButtonCustom1 or PaletteBorderStyle.ButtonCustom2 or PaletteBorderStyle.ButtonCustom3 => true,
            _ => false
        };

    private static bool IsRetroPushButtonContentStyle(PaletteContentStyle style) =>
        style switch
        {
            PaletteContentStyle.ButtonStandalone or PaletteContentStyle.ButtonAlternate or PaletteContentStyle.ButtonLowProfile
                or PaletteContentStyle.ButtonBreadCrumb or PaletteContentStyle.ButtonCommand or PaletteContentStyle.ButtonCluster
                or PaletteContentStyle.ButtonCustom1 or PaletteContentStyle.ButtonCustom2 or PaletteContentStyle.ButtonCustom3 => true,
            _ => false
        };

    internal static bool IsRetroButtonPressedState(PaletteState state) =>
        state is PaletteState.Pressed or PaletteState.CheckedPressed;

    internal static Padding AdjustRetroButtonContentPadding(Padding padding, PaletteState state)
    {
        int top = padding.Top;
        int left = padding.Left;
        int compensate = ButtonShadowVerticalPaddingCompensation;
        if (compensate > 0)
        {
            top = Math.Max(0, top - compensate);
        }

        if (IsRetroButtonPressedState(state) && ButtonShadowSize > 0)
        {
            top += ButtonShadowSize;
            left += ButtonShadowSize;
        }

        return new Padding(left, top, padding.Right, padding.Bottom);
    }
}
