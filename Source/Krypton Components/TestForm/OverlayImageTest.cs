#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive test form demonstrating overlay image functionality on KryptonButton and KryptonLabel.
/// </summary>
public partial class OverlayImageTest : KryptonForm
{
    public OverlayImageTest()
    {
        InitializeComponent();
        InitializeOverlayImages();
    }

    private void InitializeOverlayImages()
    {
        // Create sample images for main and overlay
        Image mainImage = CreateSampleMainImage();
        Image overlayImage = CreateSampleOverlayImage();

        // Setup position examples
        SetupPositionExamples(mainImage, overlayImage);

        // Setup scaling examples
        SetupScalingExamples(mainImage, overlayImage);

        // Setup interactive examples
        SetupInteractiveExamples(mainImage, overlayImage);

        // Setup label examples
        SetupLabelExamples(mainImage, overlayImage);

        // Setup property grid
        propertyGrid.SelectedObject = btnInteractive;
    }

    private Image CreateSampleMainImage()
    {
        // Create a 64x64 main image with a gradient background
        var bitmap = new Bitmap(64, 64);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw gradient background
            using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                new Rectangle(0, 0, 64, 64),
                Color.LightBlue,
                Color.LightSteelBlue,
                45f))
            {
                g.FillRectangle(brush, 0, 0, 64, 64);
            }

            // Draw a simple icon shape
            using (var pen = new Pen(Color.DarkBlue, 3))
            {
                g.DrawEllipse(pen, 12, 12, 40, 40);
                g.DrawLine(pen, 32, 20, 32, 44);
                g.DrawLine(pen, 20, 32, 44, 32);
            }
        }
        return bitmap;
    }

    private Image CreateSampleOverlayImage()
    {
        // Create a 24x24 overlay image (badge/indicator style)
        var bitmap = new Bitmap(24, 24);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw circular badge with red background
            using (var brush = new SolidBrush(Color.Red))
            {
                g.FillEllipse(brush, 0, 0, 24, 24);
            }

            // Draw white border
            using (var pen = new Pen(Color.White, 2))
            {
                g.DrawEllipse(pen, 1, 1, 22, 22);
            }

            // Draw exclamation mark (centered)
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            using (var font = new Font("Arial", 14, FontStyle.Bold))
            using (var brush = new SolidBrush(Color.White))
            using (var sf = new StringFormat(StringFormat.GenericTypographic)
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.NoClip,
                Trimming = StringTrimming.None
            })
            {
                var rect = new RectangleF(0, 0, 24, 24);
                g.DrawString("!", font, brush, rect, sf);
            }
        }
        return bitmap;
    }

    private Image CreateCustomOverlayImage(Color color, string? text = null)
    {
        // Create a custom overlay image with specified color
        var bitmap = new Bitmap(20, 20);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw circular badge
            using (var brush = new SolidBrush(color))
            {
                g.FillEllipse(brush, 0, 0, 20, 20);
            }

            // Draw white border
            using (var pen = new Pen(Color.White, 1.5f))
            {
                g.DrawEllipse(pen, 0.75f, 0.75f, 18.5f, 18.5f);
            }

            // Draw text if provided (centered)
            if (!string.IsNullOrEmpty(text))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                using (var font = new Font("Arial", 10, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.White))
                using (var sf = new StringFormat(StringFormat.GenericTypographic)
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.NoClip,
                    Trimming = StringTrimming.None
                })
                {
                    var rect = new RectangleF(0, 0, 20, 20);
                    g.DrawString(text, font, brush, rect, sf);
                }
            }
        }
        return bitmap;
    }

    private void SetupPositionExamples(Image mainImage, Image overlayImage)
    {
        // Top Left
        btnTopLeft.Values.Image = mainImage;
        btnTopLeft.Values.OverlayImage.Image = overlayImage;
        btnTopLeft.Values.OverlayImage.Position = OverlayImagePosition.TopLeft;
        btnTopLeft.Values.OverlayImage.ScaleMode = OverlayImageScaleMode.None;
        btnTopLeft.Text = "Top Left";

        // Top Right
        btnTopRight.Values.Image = mainImage;
        btnTopRight.Values.OverlayImage.Image = overlayImage;
        btnTopRight.Values.OverlayImage.Position = OverlayImagePosition.TopRight;
        btnTopRight.Values.OverlayImage.ScaleMode = OverlayImageScaleMode.None;
        btnTopRight.Text = "Top Right";

        // Bottom Left
        btnBottomLeft.Values.Image = mainImage;
        btnBottomLeft.Values.OverlayImage.Image = overlayImage;
        btnBottomLeft.Values.OverlayImage.Position = OverlayImagePosition.BottomLeft;
        btnBottomLeft.Values.OverlayImage.ScaleMode = OverlayImageScaleMode.None;
        btnBottomLeft.Text = "Bottom Left";

        // Bottom Right
        btnBottomRight.Values.Image = mainImage;
        btnBottomRight.Values.OverlayImage.Image = overlayImage;
        btnBottomRight.Values.OverlayImage.Position = OverlayImagePosition.BottomRight;
        btnBottomRight.Values.OverlayImage.ScaleMode = OverlayImageScaleMode.None;
        btnBottomRight.Text = "Bottom Right";
    }

    private void SetupScalingExamples(Image mainImage, Image overlayImage)
    {
        // None (actual size)
        btnScaleNone.Values.Image = mainImage;
        btnScaleNone.Values.OverlayImage.Image = overlayImage;
        btnScaleNone.Values.OverlayImage.Position = OverlayImagePosition.TopRight;
        btnScaleNone.Values.OverlayImage.ScaleMode = OverlayImageScaleMode.None;
        btnScaleNone.Text = "No Scaling";

        // Percentage (30% of main image)
        btnScalePercentage.Values.Image = mainImage;
        btnScalePercentage.Values.OverlayImage.Image = overlayImage;
        btnScalePercentage.Values.OverlayImage.Position = OverlayImagePosition.TopRight;
        btnScalePercentage.Values.OverlayImage.ScaleMode = OverlayImageScaleMode.Percentage;
        btnScalePercentage.Values.OverlayImage.ScaleFactor = 0.3f;
        btnScalePercentage.Text = "30% Scale";

        // Fixed Size (16x16)
        btnScaleFixed.Values.Image = mainImage;
        btnScaleFixed.Values.OverlayImage.Image = overlayImage;
        btnScaleFixed.Values.OverlayImage.Position = OverlayImagePosition.TopRight;
        btnScaleFixed.Values.OverlayImage.ScaleMode = OverlayImageScaleMode.FixedSize;
        btnScaleFixed.Values.OverlayImage.FixedSize = new Size(16, 16);
        btnScaleFixed.Text = "Fixed 16x16";

        // Proportional to Main (50% of smaller dimension)
        btnScaleProportional.Values.Image = mainImage;
        btnScaleProportional.Values.OverlayImage.Image = overlayImage;
        btnScaleProportional.Values.OverlayImage.Position = OverlayImagePosition.TopRight;
        btnScaleProportional.Values.OverlayImage.ScaleMode = OverlayImageScaleMode.ProportionalToMain;
        btnScaleProportional.Values.OverlayImage.ScaleFactor = 0.5f;
        btnScaleProportional.Text = "50% Proportional";
    }

    private void SetupInteractiveExamples(Image mainImage, Image overlayImage)
    {
        // Interactive button for property grid
        btnInteractive.Values.Image = mainImage;
        btnInteractive.Values.OverlayImage.Image = overlayImage;
        btnInteractive.Values.OverlayImage.Position = OverlayImagePosition.TopRight;
        btnInteractive.Values.OverlayImage.ScaleMode = OverlayImageScaleMode.Percentage;
        btnInteractive.Values.OverlayImage.ScaleFactor = 0.4f;
        btnInteractive.Text = "Interactive (Select for Property Grid)";

        // Different overlay colors
        btnRedOverlay.Values.Image = mainImage;
        btnRedOverlay.Values.OverlayImage.Image = CreateCustomOverlayImage(Color.Red, "!");
        btnRedOverlay.Values.OverlayImage.Position = OverlayImagePosition.TopRight;
        btnRedOverlay.Text = "Red Overlay";

        btnGreenOverlay.Values.Image = mainImage;
        btnGreenOverlay.Values.OverlayImage.Image = CreateCustomOverlayImage(Color.Green, "✓");
        btnGreenOverlay.Values.OverlayImage.Position = OverlayImagePosition.TopRight;
        btnGreenOverlay.Text = "Green Overlay";

        btnBlueOverlay.Values.Image = mainImage;
        btnBlueOverlay.Values.OverlayImage.Image = CreateCustomOverlayImage(Color.Blue, "i");
        btnBlueOverlay.Values.OverlayImage.Position = OverlayImagePosition.TopRight;
        btnBlueOverlay.Text = "Blue Overlay";

        // Notification counter example
        btnNotification.Values.Image = mainImage;
        btnNotification.Values.OverlayImage.Image = CreateCustomOverlayImage(Color.Orange, "5");
        btnNotification.Values.OverlayImage.Position = OverlayImagePosition.TopRight;
        btnNotification.Values.OverlayImage.ScaleMode = OverlayImageScaleMode.FixedSize;
        btnNotification.Values.OverlayImage.FixedSize = new Size(24, 24);
        btnNotification.Text = "Notifications (5)";
    }

    private void SetupLabelExamples(Image mainImage, Image overlayImage)
    {
        // Label with overlay
        lblWithOverlay.Values.Image = mainImage;
        lblWithOverlay.Values.OverlayImage.Image = overlayImage;
        lblWithOverlay.Values.OverlayImage.Position = OverlayImagePosition.TopRight;
        lblWithOverlay.Values.OverlayImage.ScaleMode = OverlayImageScaleMode.Percentage;
        lblWithOverlay.Values.OverlayImage.ScaleFactor = 0.35f;
        lblWithOverlay.Text = "Label with Overlay";

        // Label with different positions
        lblTopLeft.Values.Image = mainImage;
        lblTopLeft.Values.OverlayImage.Image = CreateCustomOverlayImage(Color.Purple);
        lblTopLeft.Values.OverlayImage.Position = OverlayImagePosition.TopLeft;
        lblTopLeft.Text = "Top Left";

        lblBottomRight.Values.Image = mainImage;
        lblBottomRight.Values.OverlayImage.Image = CreateCustomOverlayImage(Color.Teal);
        lblBottomRight.Values.OverlayImage.Position = OverlayImagePosition.BottomRight;
        lblBottomRight.Text = "Bottom Right";
    }

    private void BtnCyclePosition_Click(object? sender, EventArgs e)
    {
        var currentPosition = btnInteractive.Values.OverlayImage.Position;
        var nextPosition = currentPosition switch
        {
            OverlayImagePosition.TopLeft => OverlayImagePosition.TopRight,
            OverlayImagePosition.TopRight => OverlayImagePosition.BottomLeft,
            OverlayImagePosition.BottomLeft => OverlayImagePosition.BottomRight,
            OverlayImagePosition.BottomRight => OverlayImagePosition.TopLeft,
            _ => OverlayImagePosition.TopRight
        };

        btnInteractive.Values.OverlayImage.Position = nextPosition;
        lblPositionStatus.Text = $"Position: {nextPosition}";
        btnInteractive.Invalidate();
    }

    private void BtnCycleScaleMode_Click(object? sender, EventArgs e)
    {
        var currentMode = btnInteractive.Values.OverlayImage.ScaleMode;
        var nextMode = currentMode switch
        {
            OverlayImageScaleMode.None => OverlayImageScaleMode.Percentage,
            OverlayImageScaleMode.Percentage => OverlayImageScaleMode.FixedSize,
            OverlayImageScaleMode.FixedSize => OverlayImageScaleMode.ProportionalToMain,
            OverlayImageScaleMode.ProportionalToMain => OverlayImageScaleMode.None,
            _ => OverlayImageScaleMode.None
        };

        btnInteractive.Values.OverlayImage.ScaleMode = nextMode;
        lblScaleModeStatus.Text = $"Scale Mode: {nextMode}";
        btnInteractive.Invalidate();
    }

    private void BtnToggleOverlay_Click(object? sender, EventArgs e)
    {
        bool isVisible = btnInteractive.Values.OverlayImage.Image != null;
        if (isVisible)
        {
            btnInteractive.Values.OverlayImage.Image = null;
            lblToggleStatus.Text = "Overlay: Hidden";
        }
        else
        {
            btnInteractive.Values.OverlayImage.Image = CreateSampleOverlayImage();
            lblToggleStatus.Text = "Overlay: Visible";
        }
        btnInteractive.Invalidate();
    }

    private void BtnUpdateScaleFactor_Click(object? sender, EventArgs e)
    {
        if (float.TryParse(txtScaleFactor.Text, out float factor))
        {
            factor = Math.Max(0.1f, Math.Min(2.0f, factor));
            btnInteractive.Values.OverlayImage.ScaleFactor = factor;
            btnInteractive.Values.OverlayImage.ScaleMode = OverlayImageScaleMode.Percentage;
            lblScaleModeStatus.Text = $"Scale Factor: {factor:P0}";
            btnInteractive.Invalidate();
        }
    }
}
