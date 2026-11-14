#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 */
#endregion

namespace TestForm;

/// <summary>
/// Shows <see cref="KryptonCustomPaletteBase.BaseRenderer"/> replacing the Luna renderer.
/// </summary>
public sealed class CustomRendererDemo : KryptonForm
{
    private readonly KryptonManager _manager = new();
    private readonly KryptonCustomPaletteBase _palette = new();
    private readonly DemoStripeRenderer _stripeRenderer = new();
    private readonly KryptonLabel _status;
    private readonly PaletteMode _savedPaletteMode;
    private readonly KryptonCustomPaletteBase? _savedCustomPalette;

    /// <summary>Initialize a new instance of the <see cref="CustomRendererDemo"/> class.</summary>
    public CustomRendererDemo()
    {
        Text = @"Custom renderer";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(640, 420);

        _savedPaletteMode = KryptonManager.CurrentGlobalPaletteMode;
        _savedCustomPalette = _manager.GlobalCustomPalette;

        var instructions = new KryptonLabel
        {
            Location = new Point(16, 16),
            Size = new Size(600, 72)
        };
        instructions.StateCommon.ShortText.MultiLine = InheritBool.True;
        instructions.Values.Text =
            @"BaseRenderer replaces the palette renderer. Luna Blue is the base palette. " +
            @"Use Luna renderer keeps the Luna caption, button, and progress sprites. " +
            @"Use stripe renderer paints standalone buttons with orange bands, and the progress bar returns to the standard bar.";

        var useLuna = new KryptonButton
        {
            Location = new Point(16, 100),
            Size = new Size(180, 28),
            Text = @"Use Luna renderer"
        };
        useLuna.Click += (_, _) => UseLunaRenderer();

        var useStripe = new KryptonButton
        {
            Location = new Point(208, 100),
            Size = new Size(180, 28),
            Text = @"Use stripe renderer"
        };
        useStripe.Click += (_, _) => UseStripeRenderer();

        var sample = new KryptonButton
        {
            Location = new Point(16, 148),
            Size = new Size(160, 32),
            Text = @"Sample button"
        };

        var check = new KryptonCheckBox
        {
            Location = new Point(192, 154),
            Text = @"Sample check"
        };

        var progress = new KryptonProgressBar
        {
            Location = new Point(16, 200),
            Size = new Size(360, 23),
            Value = 55
        };

        _status = new KryptonLabel
        {
            Location = new Point(16, 240),
            Size = new Size(600, 40)
        };

        Controls.Add(instructions);
        Controls.Add(useLuna);
        Controls.Add(useStripe);
        Controls.Add(sample);
        Controls.Add(check);
        Controls.Add(progress);
        Controls.Add(_status);

        _palette.BasePaletteMode = PaletteMode.WindowsXPLunaBlue;
        _manager.GlobalCustomPalette = _palette;
        UseLunaRenderer();
        FormClosed += OnFormClosed;
    }

    /// <summary>Clears <see cref="KryptonCustomPaletteBase.BaseRenderer"/> so Luna's renderer is used.</summary>
    public void UseLunaRenderer()
    {
        _palette.BaseRenderer = null;
        _status.Values.Text = @"Renderer: Luna (RenderWindowsXPLuna). Progress uses the Luna track and chunks.";
    }

    /// <summary>Assigns the demo stripe renderer, which replaces Luna sprite painting.</summary>
    public void UseStripeRenderer()
    {
        _palette.BaseRenderer = _stripeRenderer;
        _status.Values.Text = @"Renderer: stripe. Sample button is banded. Progress uses the standard bar.";
    }

    private void OnFormClosed(object? sender, FormClosedEventArgs e)
    {
        if (_savedCustomPalette != null)
        {
            _manager.GlobalCustomPalette = _savedCustomPalette;
        }
        else
        {
            _manager.GlobalCustomPalette = null;
            if (_savedPaletteMode != PaletteMode.Custom)
            {
                _manager.GlobalPaletteMode = _savedPaletteMode;
            }
        }

        _palette.Dispose();
        _stripeRenderer.Dispose();
    }

    private sealed class DemoStripeRenderer : RenderWindowsXP
    {
        public DemoStripeRenderer()
            : base(() => null)
        {
        }

        public override IDisposable? DrawBack(RenderContext context,
            Rectangle rect,
            System.Drawing.Drawing2D.GraphicsPath path,
            IPaletteBack palette,
            VisualOrientation orientation,
            PaletteState state,
            IDisposable? memento)
        {
            if (context?.Control is KryptonButton && context.Graphics != null && path != null &&
                rect.Width > 0 && rect.Height > 0)
            {
                var graphics = context.Graphics;
                var oldClip = graphics.Clip;
                graphics.SetClip(path, System.Drawing.Drawing2D.CombineMode.Intersect);
                try
                {
                    int band = Math.Max(4, rect.Height / 3);
                    using var orange = new SolidBrush(Color.DarkOrange);
                    using var white = new SolidBrush(Color.White);
                    for (int y = rect.Top; y < rect.Bottom; y += band * 2)
                    {
                        graphics.FillRectangle(orange, rect.Left, y, rect.Width, band);
                        graphics.FillRectangle(white, rect.Left, y + band, rect.Width, band);
                    }
                }
                finally
                {
                    graphics.Clip = oldClip;
                }

                return memento;
            }

            return base.DrawBack(context!, rect, path!, palette!, orientation, state, memento);
        }
    }
}