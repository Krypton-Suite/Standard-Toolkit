#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) & Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using ImageEncoder = System.Drawing.Imaging.Encoder;

/// <summary>
/// Manual demo for <see cref="Krypton.Toolkit.KryptonToolTip"/> — themed tooltips on standard WinForms and Krypton controls (Issues #3380, #4192, and #4379).
/// </summary>
public partial class KryptonToolTipTest : KryptonForm
{
    private int _interactiveClicks;

    public KryptonToolTipTest()
    {
        InitializeComponent();
        kryptonToolTip1.ContainerControl = this;
        kryptonToolTip1.ToolTipValues.ToolTipStyle = LabelStyle.SuperTip;
        kryptonToolTip1.SetToolTip(btnStandardWinFormsButton,
            "Standard WinForms button",
            "This is a themed Krypton tooltip on a plain System.Windows.Forms.Button via KryptonToolTip.");
        kryptonToolTip1.SetToolTip(kbtnSample,
            "Krypton theme",
            "KryptonButton can already show built-in tips; here the same wrapper shows a tooltip for comparison.");
        kryptonToolTip1.SetToolTip(pnlHoverRegion,
            "Panel region",
            "Hover anywhere on this panel surface to verify hit-testing hooks on composite controls.");
        kryptonToolTip1.SetLinkToolTip(kbtnLinkTip,
            "Issue #4192",
            "Open the GitHub feature request",
            "https://github.com/Krypton-Suite/Standard-Toolkit/issues/4192");

        var host = new TableLayoutPanel
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            ColumnCount = 1,
            Padding = new Padding(2),
            BackColor = Color.Transparent
        };
        var caption = new KryptonLabel { AutoSize = true };
        caption.Values.Text = "Any Control: click to count.";
        var action = new KryptonButton
        {
            AutoSize = true,
            MinimumSize = new Size(120, 28)
        };
        action.Values.Text = "Increment";
        action.Click += (_, _) =>
        {
            _interactiveClicks++;
            klblInteractiveStatus.Values.Text = $"Interactive clicks: {_interactiveClicks}";
        };
        host.Controls.Add(caption);
        host.Controls.Add(action);
        kryptonToolTip1.SetToolTip(kbtnInteractiveTip, "Hosted controls", host, ownsContent: true);
        kryptonToolTip1.LinkClicked += (_, e) =>
        {
            klblInteractiveStatus.Values.Text = $"Link clicked: {e.Url}";
        };

        kryptonToolTip1.SetToolTip(kbtnHtmlTip, "HTML fragment",
            Krypton.Toolkit.Utilities.KryptonHtmlToolTipContent.Create("See <a href=\"https://github.com/Krypton-Suite/Standard-Toolkit/issues/4192\">issue 4192</a> for hosted HTML."),
            ownsContent: true);

        kbtnSample.ToolTipValues.EnableToolTips = false;

        _animatedGif = CreateColorCycleGif();
        _staticImage = CreateSolidImage(Color.SlateGray);
        kryptonToolTip1.SetToolTip(kbtnAnimatedGif, "Animated GIF", "Four colours advance while this tip is open.");
        kryptonToolTip1.SetKryptonToolTipImage(kbtnAnimatedGif, _animatedGif);
        kryptonToolTip1.SetToolTip(kbtnStaticImage, "Static image", "A single-frame bitmap stays on the first colour.");
        kryptonToolTip1.SetKryptonToolTipImage(kbtnStaticImage, _staticImage);

        klblInstructions.Values.Text =
            "Hover the controls below. SuperTip text uses show / auto-close delays (about 5 seconds). "
            + "Move onto Hyperlink / Hosted / HTML popups to click. Click away to close. "
            + "LinkClicked updates the status line. "
            + "Animated GIF cycles red, orange, green, and blue. Static image does not.";
    }

    /// <inheritdoc />
    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _animatedGif?.Dispose();
        _staticImage?.Dispose();
        _gifStream?.Dispose();
        base.OnFormClosed(e);
    }

    /// <summary>
    /// Builds a four-frame GIF. The stream must stay open for the life of the image.
    /// </summary>
    private Image CreateColorCycleGif()
    {
        Color[] colors =
        {
            Color.Firebrick,
            Color.DarkOrange,
            Color.SeaGreen,
            Color.RoyalBlue
        };

        var frames = new Bitmap[colors.Length];
        try
        {
            for (int i = 0; i < colors.Length; i++)
            {
                frames[i] = CreateSolidImage(colors[i]);
                SetGifFrameDelay(frames[i], 20);
            }

            SetGifLoopCount(frames[0]);

            ImageCodecInfo encoder = ImageCodecInfo.GetImageEncoders()
                .First(codec => codec.FormatID == ImageFormat.Gif.Guid);
            _gifStream = new MemoryStream();
            using (var firstFrame = new EncoderParameters(1))
            {
                firstFrame.Param[0] = new EncoderParameter(ImageEncoder.SaveFlag, new[] { (long)EncoderValue.MultiFrame });
                frames[0].Save(_gifStream, encoder, firstFrame);
            }

            using (var nextFrame = new EncoderParameters(1))
            {
                nextFrame.Param[0] = new EncoderParameter(ImageEncoder.SaveFlag, new[] { (long)EncoderValue.FrameDimensionTime });
                for (int i = 1; i < frames.Length; i++)
                {
                    frames[0].SaveAdd(frames[i], nextFrame);
                }
            }

            using (var flush = new EncoderParameters(1))
            {
                flush.Param[0] = new EncoderParameter(ImageEncoder.SaveFlag, new[] { (long)EncoderValue.Flush });
                frames[0].SaveAdd(flush);
            }

            _gifStream.Position = 0;
            return Image.FromStream(_gifStream);
        }
        finally
        {
            for (int i = 0; i < frames.Length; i++)
            {
                frames[i]?.Dispose();
            }
        }
    }

    private static Bitmap CreateSolidImage(Color color)
    {
        var bitmap = new Bitmap(48, 48, PixelFormat.Format32bppArgb);
        using (Graphics graphics = Graphics.FromImage(bitmap))
        using (var brush = new SolidBrush(color))
        {
            graphics.Clear(color);
            graphics.FillRectangle(brush, 8, 8, 32, 32);
        }

        return bitmap;
    }

    private static void SetGifFrameDelay(Image frame, short delayCentiseconds)
    {
        PropertyItem item = CreatePropertyItem();
        item.Id = 0x5100;
        item.Type = 4;
        item.Len = 4;
        item.Value = BitConverter.GetBytes((int)delayCentiseconds);
        frame.SetPropertyItem(item);
    }

    private static void SetGifLoopCount(Image frame)
    {
        PropertyItem item = CreatePropertyItem();
        item.Id = 0x5101;
        item.Type = 3;
        item.Len = 2;
        item.Value = new byte[] { 0, 0 };
        frame.SetPropertyItem(item);
    }

    private static PropertyItem CreatePropertyItem()
    {
        ConstructorInfo? ctor = typeof(PropertyItem).GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            Type.EmptyTypes,
            null);
        if (ctor is null)
        {
            throw new InvalidOperationException("PropertyItem has no constructor.");
        }

        return (PropertyItem)ctor.Invoke(null);
    }

    private MemoryStream? _gifStream;

    private Image? _animatedGif;

    private Image? _staticImage;
}
