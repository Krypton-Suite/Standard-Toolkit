#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Shared Windows XP visual-style renderer: square corners, hard one-pixel bevels, and vertical linear fills.
/// </summary>
public class RenderWindowsXP : RenderStandard
{
    private readonly Func<Bitmap?> _buttonSprite;

    private static readonly Font XPDefaultFont = new("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point);

    /// <summary>Gets the standard Windows XP user-interface font.</summary>
    /// <remarks>XP themes use Tahoma independently of the host operating system's default UI font.</remarks>
    public static Font DefaultFont => XPDefaultFont;

    /// <summary>
    /// Initialize a new instance of the <see cref="RenderWindowsXP"/> class.
    /// </summary>
    /// <param name="buttonSprite">Factory that returns the XP push-button sprite sheet for ToolStrip buttons.</param>
    protected RenderWindowsXP(Func<Bitmap?> buttonSprite) =>
        _buttonSprite = buttonSprite ?? throw new ArgumentNullException(nameof(buttonSprite));

    /// <inheritdoc />
    public override ToolStripRenderer RenderToolStrip([DisallowNull] PaletteBase? colorPalette)
    {
        if (colorPalette == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(colorPalette));
        }

        return new KryptonWindowsXPRenderer(colorPalette.ColorTable, _buttonSprite);
    }

    /// <inheritdoc />
    public override GraphicsPath GetBackPath([DisallowNull] RenderContext context,
        Rectangle rect,
        [DisallowNull] IPaletteBorder palette,
        VisualOrientation orientation,
        PaletteState state)
    {
        // XP's classic controls use square geometry in every state. Do not delegate
        // to RenderStandard here: its rounded-path logic can leak into XP palettes
        // when a control has a non-zero border rounding value.
        var path = new GraphicsPath();
        path.AddRectangle(rect);
        return path;
    }

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
            ThrowHelper.ThrowArgumentNullException(nameof(context));
        }

        if (path == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(path));
        }

        if (palette == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(palette));
        }

        if (rect.Width <= 0 || rect.Height <= 0 ||
            CommonHelper.IsOverrideState(state) ||
            palette.GetBackColorStyle(state) != PaletteColorStyle.Linear)
        {
            return base.DrawBack(context, rect, path, palette, orientation, state, memento);
        }

        var top = palette.GetBackColor1(state);
        var bottom = palette.GetBackColor2(state);
        if (top == Color.Empty || bottom == Color.Empty)
        {
            return base.DrawBack(context, rect, path, palette, orientation, state, memento);
        }

        var graphics = context.Graphics;
        var oldSmoothing = graphics.SmoothingMode;
        var oldPixel = graphics.PixelOffsetMode;
        using var oldClip = graphics.Clip;

        graphics.SmoothingMode = SmoothingMode.None;
        graphics.PixelOffsetMode = PixelOffsetMode.None;

        try
        {
            graphics.SetClip(path, CombineMode.Intersect);

            // XP's gradient fill is separate from its one-pixel edge treatment.
            // Drawing a generic light/dark bevel here made panels and form surfaces
            // look like buttons. The palette border pass owns the edge and its state.
            using var brush = new LinearGradientBrush(rect, top, bottom, 90f);
            graphics.FillRectangle(brush, rect);
        }
        finally
        {
            graphics.Clip = oldClip;
            graphics.SmoothingMode = oldSmoothing;
            graphics.PixelOffsetMode = oldPixel;
        }

        return memento;
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
            ThrowHelper.ThrowArgumentNullException(nameof(palette));
        }

        if (context == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(context));
        }

        if (rect.Width <= 0 || rect.Height <= 0 ||
            palette.GetBorderWidth(state) <= 0 ||
            palette.GetBorderDraw(state) != InheritBool.True ||
            !CommonHelper.HasABorder(palette.GetBorderDrawBorders(state)))
        {
            return;
        }

        base.DrawBorder(context, rect, palette, orientation, state);
    }
}

/// <summary>
/// Windows XP renderer that 9-slices registered sprite sheets. Luna, Royale, Royale Noir, and Zune derive from it.
/// </summary>
public class RenderWindowsXPSprite : RenderWindowsXP
{
    /// <summary>
    /// Initialize a new instance of the <see cref="RenderWindowsXPSprite"/> class.
    /// </summary>
    /// <param name="buttonSprite">Factory that returns the theme push-button sprite sheet.</param>
    protected RenderWindowsXPSprite(Func<Bitmap?> buttonSprite)
        : base(buttonSprite)
    {
    }

    /// <inheritdoc />
    public override ToolStripRenderer RenderToolStrip([DisallowNull] PaletteBase? colorPalette)
    {
        if (colorPalette == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(colorPalette));
        }

        return new KryptonWindowsXPLunaRenderer(colorPalette.ColorTable);
    }

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
            ThrowHelper.ThrowArgumentNullException(nameof(context));
        }

        if (palette == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(palette));
        }

        Image? backImage = palette.GetBackImage(state);
        if (WindowsXPLunaArt.TryGet(backImage, out WindowsXPLunaSprite sprite) &&
            sprite.Part != WindowsXPLunaPart.FrameLeft &&
            sprite.Part != WindowsXPLunaPart.FrameRight &&
            sprite.Part != WindowsXPLunaPart.FrameBottom)
        {
            if (!sprite.Chroma.IsEmpty)
            {
                Color fill = palette.GetBackColor1(state);
                if (fill.IsEmpty)
                {
                    fill = palette.GetBackColor2(state);
                }

                if (!fill.IsEmpty)
                {
                    using var fillBrush = new SolidBrush(fill);
                    context.Graphics.FillRectangle(fillBrush, rect);
                }
            }

            WindowsXPLunaArt.Draw(context.Graphics, sprite, rect, state);
            return memento;
        }

        return base.DrawBack(context, rect, path, palette, orientation, state, memento);
    }

    /// <inheritdoc />
    public override void DrawBorder(RenderContext context,
        Rectangle rect,
        IPaletteBorder palette,
        VisualOrientation orientation,
        PaletteState state)
    {
        if (context == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(context));
        }

        if (palette == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(palette));
        }

        Image? borderImage = palette.GetBorderImage(state);
        if (WindowsXPLunaArt.TryGet(borderImage, out WindowsXPLunaSprite frame) &&
            frame.Part == WindowsXPLunaPart.FrameLeft)
        {
            WindowsXPLunaArt.DrawFrame(context.Graphics, rect, frame, state);
            return;
        }

        base.DrawBorder(context, rect, palette, orientation, state);
    }

    /// <inheritdoc />
    public override void DrawDropDownButton([DisallowNull] RenderContext context,
        Rectangle displayRect,
        [DisallowNull] IPaletteContent palette,
        PaletteState state,
        VisualOrientation orientation)
    {
        if (TryDrawLunaComboGlyph(context, displayRect, state))
        {
            return;
        }

        base.DrawDropDownButton(context, displayRect, palette, state, orientation);
    }

    /// <inheritdoc />
    public override void DrawInputControlDropDownGlyph([DisallowNull] RenderContext context,
        Rectangle cellRect,
        [DisallowNull] IPaletteContent paletteContent,
        PaletteState state)
    {
        if (TryDrawLunaComboGlyph(context, cellRect, state))
        {
            return;
        }

        base.DrawInputControlDropDownGlyph(context, cellRect, paletteContent, state);
    }

    private static bool TryDrawLunaComboGlyph(RenderContext context, Rectangle bounds, PaletteState state)
    {
        if (context?.Graphics == null || !WindowsXPLunaArt.TryGetTheme(context.Control, out WindowsXPSpriteTheme theme))
        {
            return false;
        }

        WindowsXPLunaArt.DrawComboGlyph(context.Graphics, bounds, theme, state);
        return true;
    }
}

/// <summary>
/// Windows XP Luna renderer. Caption, frame, button, and combo chrome come from the Luna sprite sheets.
/// </summary>
public sealed class RenderWindowsXPLuna : RenderWindowsXPSprite
{
    /// <summary>Initialize a new instance of the <see cref="RenderWindowsXPLuna"/> class.</summary>
    public RenderWindowsXPLuna()
        : base(() => WindowsXPThemeResourceBridge.LunaButton)
    {
    }
}

/// <summary>Windows XP Royale renderer.</summary>
public sealed class RenderWindowsXPRoyale : RenderWindowsXPSprite
{
    /// <summary>Initialize a new instance of the <see cref="RenderWindowsXPRoyale"/> class.</summary>
    public RenderWindowsXPRoyale()
        : base(() => WindowsXPThemeResourceBridge.RoyaleButton)
    {
    }
}

/// <summary>Windows XP Royale Noir renderer.</summary>
public sealed class RenderWindowsXPRoyaleNoir : RenderWindowsXPSprite
{
    /// <summary>Initialize a new instance of the <see cref="RenderWindowsXPRoyaleNoir"/> class.</summary>
    public RenderWindowsXPRoyaleNoir()
        : base(() => WindowsXPThemeResourceBridge.RoyaleNoirButton)
    {
    }
}

/// <summary>Windows XP Zune renderer.</summary>
public sealed class RenderWindowsXPZune : RenderWindowsXPSprite
{
    /// <summary>Initialize a new instance of the <see cref="RenderWindowsXPZune"/> class.</summary>
    public RenderWindowsXPZune()
        : base(() => WindowsXPThemeResourceBridge.ZuneButton)
    {
    }
}

/// <summary>Windows XP Embedded renderer.</summary>
public sealed class RenderWindowsXPEmbedded : RenderWindowsXP
{
    /// <summary>Initialize a new instance of the <see cref="RenderWindowsXPEmbedded"/> class.</summary>
    public RenderWindowsXPEmbedded()
        : base(() => WindowsXPThemeResourceBridge.EmbeddedButton)
    {
    }
}
