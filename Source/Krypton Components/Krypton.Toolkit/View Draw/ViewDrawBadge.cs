#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

using Timer = System.Windows.Forms.Timer;
using GraphicsPath = System.Drawing.Drawing2D.GraphicsPath;
using ColorMatrix = System.Drawing.Imaging.ColorMatrix;
using ImageAttributes = System.Drawing.Imaging.ImageAttributes;
using ColorMatrixFlag = System.Drawing.Imaging.ColorMatrixFlag;
using ColorAdjustType = System.Drawing.Imaging.ColorAdjustType;

namespace Krypton.Toolkit;

/// <summary>
/// View element that can draw a badge.
/// </summary>
public class ViewDrawBadge : ViewLeaf
{
    #region Instance Fields
    private readonly BadgeValues _badgeValues;
    private readonly Control _control;
    private Timer? _animationTimer;
    private float _animationOpacity = 1.0f;
    private float _animationScale = 1.0f;
    private bool _animationDirection = true; // true = increasing, false = decreasing
    private const int DEFAULT_BADGE_SIZE = 18;
    private const int BADGE_MIN_SIZE = 16;
    private const int BADGE_OFFSET = 3;
    private const int ANIMATION_INTERVAL = 50; // ms between animation frames
    private const float FADE_MIN_OPACITY = 0.3f;
    private const float FADE_MAX_OPACITY = 1.0f;
    private const float PULSE_MIN_SCALE = 0.85f;
    private const float PULSE_MAX_SCALE = 1.0f;
    private const float PULSE_MIN_OPACITY = 0.6f;
    private const float PULSE_MAX_OPACITY = 1.0f;
    private const float ANIMATION_STEP = 0.05f;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawBadge class.
    /// </summary>
    /// <param name="badgeValues">Source for badge values.</param>
    /// <param name="control">Control instance for DPI awareness.</param>
    public ViewDrawBadge(BadgeValues badgeValues, Control control)
    {
        _badgeValues = badgeValues;
        _control = control;
        _animationOpacity = 1.0f;
        _animationScale = 1.0f;
        _animationDirection = true;

        // Setup animation timer if needed
        UpdateAnimationTimer();
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawBadge:{Id}";

    /// <summary>
    /// Release unmanaged and optionally managed resources.
    /// </summary>
    /// <param name="disposing">Called from Dispose method.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Stop and dispose animation timer
            if (_animationTimer != null)
            {
                _animationTimer.Stop();
                _animationTimer.Tick -= OnAnimationTick;
                _animationTimer.Dispose();
                _animationTimer = null;
            }
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Badge has no preferred size - it's positioned absolutely
        return Size.Empty;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Update animation timer if needed
        UpdateAnimationTimer();

        // Only layout if badge is visible and has content (text or image)
        // Note: If AutoShowHideBadge is enabled, visibility is automatically managed in the Text/Image property setters
        if (!_badgeValues.BadgeContentValues.Visible || (string.IsNullOrEmpty(_badgeValues.BadgeContentValues.Text) && _badgeValues.BadgeContentValues.BadgeImage == null))
        {
            ClientRectangle = Rectangle.Empty;
            return;
        }

        // Calculate badge size based on text
        Size badgeSize = CalculateBadgeSize(context!);

        // Calculate position based on BadgePosition enum relative to parent's ClientRectangle
        // Use the display rectangle which should be the button's client rectangle
        Rectangle parentRect = context!.DisplayRectangle;
        Point badgeLocation = CalculateBadgeLocation(parentRect, badgeSize);

        // Set the client rectangle for the badge (relative to parent)
        ClientRectangle = new Rectangle(badgeLocation, badgeSize);
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform a render of the elements.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void Render([DisallowNull] RenderContext context)
    {
        Debug.Assert(context != null);

        // Only render if visible and has content (text or image)
        if (!Visible || !_badgeValues.BadgeContentValues.Visible || (string.IsNullOrEmpty(_badgeValues.BadgeContentValues.Text) && _badgeValues.BadgeContentValues.BadgeImage == null))
        {
            return;
        }

        RenderBadge(context!);
    }
    #endregion

    #region Implementation
    private Size CalculateBadgeSize(ViewLayoutContext context)
    {
        // If shape is Circle and BadgeDiameter is specified, use it
        if (_badgeValues.BadgeContentValues.Shape == BadgeShape.Circle && _badgeValues.BadgeContentValues.BadgeDiameter > 0)
        {
            return new Size(_badgeValues.BadgeContentValues.BadgeDiameter, _badgeValues.BadgeContentValues.BadgeDiameter);
        }

        // If image is provided, use image size with padding
        if (_badgeValues.BadgeContentValues.BadgeImage != null)
        {
            int padding = _badgeValues.BadgeContentValues.BadgeImagePadding; // Padding around image
            // For images, use a reasonable badge size based on the image
            int imageMax = Math.Max(_badgeValues.BadgeContentValues.BadgeImage.Width, _badgeValues.BadgeContentValues.BadgeImage.Height);
            int size = Math.Max(BADGE_MIN_SIZE, Math.Min(imageMax + padding, 32)); // Cap at 32px for reasonable badge size
            return new Size(size, size);
        }

        // Otherwise calculate based on text
        string text = GetDisplayText();

        if (string.IsNullOrEmpty(text))
        {
            return new Size(DEFAULT_BADGE_SIZE, DEFAULT_BADGE_SIZE);
        }

        // Use the graphics from the context to measure text
        using var g = context.Control?.CreateGraphics();
        if (g == null)
        {
            return new Size(DEFAULT_BADGE_SIZE, DEFAULT_BADGE_SIZE);
        }

        // Use the badge font or default font for measurement
        // Only dispose fonts we create ourselves, not fonts from BadgeValues
        Font measureFont = _badgeValues.BadgeContentValues.Font ?? new Font(KryptonManager.CurrentGlobalPalette.BaseFont.FontFamily, 7.5f, FontStyle.Bold, GraphicsUnit.Point);
        bool createdFont = _badgeValues.BadgeContentValues.Font == null;

        try
        {
            SizeF textSize = g.MeasureString(text, measureFont);

            // For non-circle shapes, we might want different sizing
            int padding = _badgeValues.BadgeContentValues.Shape == BadgeShape.Circle ? 8 : 6;
            int diameter = Math.Max(BADGE_MIN_SIZE, (int)Math.Max(textSize.Width, textSize.Height) + padding);
            return new Size(diameter, diameter);
        }
        finally
        {
            // Only dispose if we created the font
            if (createdFont)
            {
                measureFont.Dispose();
            }
        }

    }

    private Point CalculateBadgeLocation(Rectangle parentRect, Size badgeSize)
    {
        int offset = BADGE_OFFSET;
        Point location;

        switch (_badgeValues.BadgeContentValues.Position)
        {
            case BadgePosition.TopRight:
                location = new Point(parentRect.Right - badgeSize.Width - offset, parentRect.Top + offset);
                break;
            case BadgePosition.TopLeft:
                location = new Point(parentRect.Left + offset, parentRect.Top + offset);
                break;
            case BadgePosition.BottomRight:
                location = new Point(parentRect.Right - badgeSize.Width - offset, parentRect.Bottom - badgeSize.Height - offset);
                break;
            case BadgePosition.BottomLeft:
                location = new Point(parentRect.Left + offset, parentRect.Bottom - badgeSize.Height - offset);
                break;
            default:
                location = new Point(parentRect.Right - badgeSize.Width - offset, parentRect.Top + offset);
                break;
        }

        return location;
    }

    private void RenderBadge(RenderContext context)
    {
        Rectangle badgeRect = ClientRectangle;

        if (badgeRect.IsEmpty || badgeRect.Width <= 0 || badgeRect.Height <= 0)
        {
            return;
        }

        Graphics g = context.Graphics;

        // Enable anti-aliasing for smoother rendering
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = TextRenderingHint.AntiAlias;

        // Apply animation scale if pulsing
        Rectangle drawRect = badgeRect;
        if (_badgeValues.BadgeContentValues.Animation == BadgeAnimation.Pulse && _animationScale != 1.0f)
        {
            int scaledWidth = (int)(badgeRect.Width * _animationScale);
            int scaledHeight = (int)(badgeRect.Height * _animationScale);
            int offsetX = (badgeRect.Width - scaledWidth) / 2;
            int offsetY = (badgeRect.Height - scaledHeight) / 2;
            drawRect = new Rectangle(badgeRect.X + offsetX, badgeRect.Y + offsetY, scaledWidth, scaledHeight);
        }

        // Calculate opacity based on animation
        float opacity = GetAnimationOpacity();

        // Draw image if provided, otherwise draw text
        if (_badgeValues.BadgeContentValues.BadgeImage != null)
        {
            // Calculate opacity for image
            ColorMatrix? colorMatrix = null;
            ImageAttributes? imageAttributes = null;
            if (opacity < 1.0f)
            {
                colorMatrix = new ColorMatrix(new float[][]
                {
                    [1, 0, 0, 0, 0],
                    [0, 1, 0, 0, 0],
                    [0, 0, 1, 0, 0],
                    [0, 0, 0, opacity, 0],
                    [0, 0, 0, 0, 1]
                });
                imageAttributes = new ImageAttributes();
                imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            }

            // Scale image to fit within badge with padding
            int padding = _badgeValues.BadgeContentValues.BadgeImagePadding;
            int maxImageSize = Math.Min(drawRect.Width, drawRect.Height) - padding;

            // Calculate scaled size maintaining aspect ratio
            int imageWidth = _badgeValues.BadgeContentValues.BadgeImage.Width;
            int imageHeight = _badgeValues.BadgeContentValues.BadgeImage.Height;
            float scale = Math.Min((float)maxImageSize / imageWidth, (float)maxImageSize / imageHeight);
            int scaledWidth = (int)(imageWidth * scale);
            int scaledHeight = (int)(imageHeight * scale);

            // Center the image in the badge
            int imageX = drawRect.Left + (drawRect.Width - scaledWidth) / 2;
            int imageY = drawRect.Top + (drawRect.Height - scaledHeight) / 2;
            Rectangle imageRect = new Rectangle(imageX, imageY, scaledWidth, scaledHeight);

            // Draw the image with high quality interpolation
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            if (imageAttributes != null)
            {
                g.DrawImage(_badgeValues.BadgeContentValues.BadgeImage, imageRect, 0, 0, imageWidth, imageHeight, GraphicsUnit.Pixel, imageAttributes);
                imageAttributes.Dispose();
                colorMatrix = null;
            }
            else
            {
                g.DrawImage(_badgeValues.BadgeContentValues.BadgeImage, imageRect);
            }

            g.InterpolationMode = InterpolationMode.Default;
            g.PixelOffsetMode = PixelOffsetMode.Default;

            // Draw border for image badges if specified
            DrawBadgeBorder(g, drawRect, opacity);
        }
        else
        {
            // Draw the badge background for text badges (no background for image badges)
            Color badgeColor = _badgeValues.BadgeColorValues.BadgeColor;
            if (opacity < 1.0f)
            {
                badgeColor = Color.FromArgb((int)(opacity * 255), badgeColor.R, badgeColor.G, badgeColor.B);
            }

            using (var badgeBrush = new SolidBrush(badgeColor))
            {
                switch (_badgeValues.BadgeContentValues.Shape)
                {
                    case BadgeShape.Circle:
                        g.FillEllipse(badgeBrush, drawRect);
                        break;
                    case BadgeShape.Square:
                        g.FillRectangle(badgeBrush, drawRect);
                        break;
                    case BadgeShape.RoundedRectangle:
                        int radius = Math.Min(drawRect.Width, drawRect.Height) / 4;
                        FillRoundedRectangle(g, badgeBrush, drawRect, radius);
                        break;
                }
            }

            // Draw border if specified
            DrawBadgeBorder(g, drawRect, opacity);

            // Draw the badge text
            string text = GetDisplayText();
            if (!string.IsNullOrEmpty(text))
            {
                // Only dispose fonts we create ourselves, not fonts from BadgeValues
                Font textFont = _badgeValues.BadgeContentValues.Font ?? new Font(KryptonManager.CurrentGlobalPalette.BaseFont.FontFamily, 7.5f, FontStyle.Bold, GraphicsUnit.Point);
                bool createdFont = _badgeValues.BadgeContentValues.Font == null;
                Color textColor = _badgeValues.BadgeColorValues.TextColor;
                if (opacity < 1.0f)
                {
                    textColor = Color.FromArgb((int)(opacity * 255), textColor.R, textColor.G, textColor.B);
                }

                try
                {
                    using (var textBrush = new SolidBrush(textColor))
                    using (var stringFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center,
                        FormatFlags = StringFormatFlags.NoWrap
                    })
                    {
                        g.DrawString(text, textFont, textBrush, drawRect, stringFormat);
                    }
                }
                finally
                {
                    // Only dispose if we created the font
                    if (createdFont)
                    {
                        textFont.Dispose();
                    }
                }
            }
        }
    }

    private void FillRoundedRectangle(Graphics g, Brush brush, Rectangle rect, int radius)
    {
        using (var path = new GraphicsPath())
        {
            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseAllFigures();
            g.FillPath(brush, path);
        }
    }

    private void DrawRoundedRectangle(Graphics g, Pen pen, Rectangle rect, int radius)
    {
        using (var path = new GraphicsPath())
        {
            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseAllFigures();
            g.DrawPath(pen, path);
        }
    }

    private string GetDisplayText()
    {
        string text = _badgeValues.BadgeContentValues.Text ?? string.Empty;

        // Check for overflow if enabled (OverflowNumber > 0)
        if (_badgeValues.BadgeContentValues.MaxBadgeValue > 0 && !string.IsNullOrEmpty(text))
        {
            // Try to parse the text as an integer
            if (int.TryParse(text, out int numericValue))
            {
                // If the value exceeds the overflow threshold, return overflow text
                if (numericValue > _badgeValues.BadgeContentValues.MaxBadgeValue)
                {
                    return _badgeValues.BadgeOverflowValues.OverflowText;
                }
            }
        }

        return text;
    }

    private void DrawBadgeBorder(Graphics g, Rectangle drawRect, float opacity)
    {
        if (_badgeValues.BadgeBorderValues.BadgeBorderSize > 0 && _badgeValues.BadgeColorValues.BadgeBorderColor != Color.Empty)
        {
            Color borderColor = _badgeValues.BadgeColorValues.BadgeBorderColor;
            if (opacity < 1.0f)
            {
                borderColor = Color.FromArgb((int)(opacity * 255), borderColor.R, borderColor.G, borderColor.B);
            }

            // Adjust rectangle to account for pen width (pen draws centered on edge)
            int halfBorder = _badgeValues.BadgeBorderValues.BadgeBorderSize / 2;
            Rectangle borderRect = new Rectangle(
                drawRect.X + halfBorder,
                drawRect.Y + halfBorder,
                drawRect.Width - _badgeValues.BadgeBorderValues.BadgeBorderSize,
                drawRect.Height - _badgeValues.BadgeBorderValues.BadgeBorderSize);

            // If bevel is enabled, draw border with bevel effect
            if (_badgeValues.BadgeBorderValues.BadgeBorderBevel != BadgeBevelType.None)
            {
                DrawBevelBorder(g, borderRect, borderColor, opacity);
            }
            else
            {
                // Standard border without bevel
                using (var borderPen = new Pen(borderColor, _badgeValues.BadgeBorderValues.BadgeBorderSize))
                {
                    switch (_badgeValues.BadgeContentValues.Shape)
                    {
                        case BadgeShape.Circle:
                            g.DrawEllipse(borderPen, borderRect);
                            break;
                        case BadgeShape.Square:
                            g.DrawRectangle(borderPen, borderRect);
                            break;
                        case BadgeShape.RoundedRectangle:
                            int borderRadius = Math.Min(borderRect.Width, borderRect.Height) / 4;
                            DrawRoundedRectangle(g, borderPen, borderRect, borderRadius);
                            break;
                    }
                }
            }
        }
    }

    private void DrawBevelBorder(Graphics g, Rectangle borderRect, Color baseColor, float opacity)
    {
        // Create lighter and darker colors for bevel effect
        Color lightColor = ControlPaint.Light(baseColor);
        Color darkColor = ControlPaint.Dark(baseColor);

        if (opacity < 1.0f)
        {
            lightColor = Color.FromArgb((int)(opacity * 255), lightColor.R, lightColor.G, lightColor.B);
            darkColor = Color.FromArgb((int)(opacity * 255), darkColor.R, darkColor.G, darkColor.B);
        }

        int borderSize = _badgeValues.BadgeBorderValues.BadgeBorderSize;

        // For Raised bevel: light top/left, dark bottom/right
        // For Inset bevel: dark top/left, light bottom/right (reversed)
        Pen topLeftPen;
        Pen bottomRightPen;

        if (_badgeValues.BadgeBorderValues.BadgeBorderBevel == BadgeBevelType.Raised)
        {
            topLeftPen = new Pen(lightColor, borderSize);
            bottomRightPen = new Pen(darkColor, borderSize);
        }
        else // Inset
        {
            topLeftPen = new Pen(darkColor, borderSize);
            bottomRightPen = new Pen(lightColor, borderSize);
        }

        using (topLeftPen)
        using (bottomRightPen)
        {
            switch (_badgeValues.BadgeContentValues.Shape)
            {
                case BadgeShape.Circle:
                    DrawBevelCircle(g, borderRect, topLeftPen, bottomRightPen);
                    break;
                case BadgeShape.Square:
                    DrawBevelSquare(g, borderRect, topLeftPen, bottomRightPen);
                    break;
                case BadgeShape.RoundedRectangle:
                    int borderRadius = Math.Min(borderRect.Width, borderRect.Height) / 4;
                    DrawBevelRoundedRectangle(g, borderRect, borderRadius, topLeftPen, bottomRightPen);
                    break;
            }
        }
    }

    private void DrawBevelCircle(Graphics g, Rectangle rect, Pen lightPen, Pen darkPen)
    {
        // For circle, draw top-left half with light color, bottom-right half with dark color
        // Top-left arc (180 degrees from 135 to 315)
        using (var path = new GraphicsPath())
        {
            path.AddArc(rect.X, rect.Y, rect.Width, rect.Height, 135, 180);
            g.DrawPath(lightPen, path);
        }

        // Bottom-right arc (180 degrees from 315 to 135, wrapping around)
        using (var path = new GraphicsPath())
        {
            path.AddArc(rect.X, rect.Y, rect.Width, rect.Height, 315, 180);
            g.DrawPath(darkPen, path);
        }
    }

    private void DrawBevelSquare(Graphics g, Rectangle rect, Pen lightPen, Pen darkPen)
    {
        int borderSize = _badgeValues.BadgeBorderValues.BadgeBorderSize;
        int halfBorder = borderSize / 2;

        // Top edge (light)
        g.DrawLine(lightPen, rect.Left + halfBorder, rect.Top + halfBorder, rect.Right - halfBorder, rect.Top + halfBorder);

        // Left edge (light)
        g.DrawLine(lightPen, rect.Left + halfBorder, rect.Top + halfBorder, rect.Left + halfBorder, rect.Bottom - halfBorder);

        // Bottom edge (dark)
        g.DrawLine(darkPen, rect.Left + halfBorder, rect.Bottom - halfBorder, rect.Right - halfBorder, rect.Bottom - halfBorder);

        // Right edge (dark)
        g.DrawLine(darkPen, rect.Right - halfBorder, rect.Top + halfBorder, rect.Right - halfBorder, rect.Bottom - halfBorder);
    }

    private void DrawBevelRoundedRectangle(Graphics g, Rectangle rect, int radius, Pen lightPen, Pen darkPen)
    {
        int borderSize = _badgeValues.BadgeBorderValues.BadgeBorderSize;
        int halfBorder = borderSize / 2;

        // Top-left arc (light)
        using (var path = new GraphicsPath())
        {
            path.AddArc(rect.X + halfBorder, rect.Y + halfBorder, radius * 2, radius * 2, 180, 90);
            g.DrawPath(lightPen, path);
        }

        // Top edge (light)
        g.DrawLine(lightPen, rect.Left + radius + halfBorder, rect.Top + halfBorder, rect.Right - radius - halfBorder, rect.Top + halfBorder);

        // Top-right arc - split: top half light, right half dark
        using (var path = new GraphicsPath())
        {
            path.AddArc(rect.Right - radius * 2 - halfBorder, rect.Y + halfBorder, radius * 2, radius * 2, 270, 45);
            g.DrawPath(lightPen, path);
        }
        using (var path = new GraphicsPath())
        {
            path.AddArc(rect.Right - radius * 2 - halfBorder, rect.Y + halfBorder, radius * 2, radius * 2, 315, 45);
            g.DrawPath(darkPen, path);
        }

        // Right edge (dark)
        g.DrawLine(darkPen, rect.Right - halfBorder, rect.Top + radius + halfBorder, rect.Right - halfBorder, rect.Bottom - radius - halfBorder);

        // Bottom-right arc (dark)
        using (var path = new GraphicsPath())
        {
            path.AddArc(rect.Right - radius * 2 - halfBorder, rect.Bottom - radius * 2 - halfBorder, radius * 2, radius * 2, 0, 90);
            g.DrawPath(darkPen, path);
        }

        // Bottom edge (dark)
        g.DrawLine(darkPen, rect.Left + radius + halfBorder, rect.Bottom - halfBorder, rect.Right - radius - halfBorder, rect.Bottom - halfBorder);

        // Bottom-left arc - split: bottom half dark, left half light
        using (var path = new GraphicsPath())
        {
            path.AddArc(rect.X + halfBorder, rect.Bottom - radius * 2 - halfBorder, radius * 2, radius * 2, 90, 45);
            g.DrawPath(darkPen, path);
        }
        using (var path = new GraphicsPath())
        {
            path.AddArc(rect.X + halfBorder, rect.Bottom - radius * 2 - halfBorder, radius * 2, radius * 2, 135, 45);
            g.DrawPath(lightPen, path);
        }

        // Left edge (light)
        g.DrawLine(lightPen, rect.Left + halfBorder, rect.Top + radius + halfBorder, rect.Left + halfBorder, rect.Bottom - radius - halfBorder);
    }

    private float GetAnimationOpacity()
    {
        return _badgeValues.BadgeContentValues.Animation switch
        {
            BadgeAnimation.FadeInOut => _animationOpacity,
            BadgeAnimation.Pulse => _animationOpacity,
            _ => 1.0f
        };
    }

    private void UpdateAnimationTimer()
    {
        // Stop existing timer
        if (_animationTimer != null)
        {
            _animationTimer.Stop();
            _animationTimer.Tick -= OnAnimationTick;
            _animationTimer.Dispose();
            _animationTimer = null;
        }

        // Start new timer if animation is enabled
        if (_badgeValues.BadgeContentValues.Animation != BadgeAnimation.None && _badgeValues.BadgeContentValues.Visible)
        {
            _animationTimer = new Timer
            {
                Interval = ANIMATION_INTERVAL
            };
            _animationTimer.Tick += OnAnimationTick;

            // Reset animation state
            _animationOpacity = _badgeValues.BadgeContentValues.Animation == BadgeAnimation.FadeInOut ? FADE_MIN_OPACITY : PULSE_MAX_OPACITY;
            _animationScale = PULSE_MAX_SCALE;
            _animationDirection = true;

            _animationTimer.Start();
        }
    }

    private void OnAnimationTick(object? sender, EventArgs e)
    {
        if (_badgeValues.BadgeContentValues.Animation == BadgeAnimation.None || !_badgeValues.BadgeContentValues.Visible)
        {
            UpdateAnimationTimer();
            return;
        }

        bool needsUpdate = false;

        switch (_badgeValues.BadgeContentValues.Animation)
        {
            case BadgeAnimation.FadeInOut:
                if (_animationDirection)
                {
                    _animationOpacity += ANIMATION_STEP;
                    if (_animationOpacity >= FADE_MAX_OPACITY)
                    {
                        _animationOpacity = FADE_MAX_OPACITY;
                        _animationDirection = false;
                    }
                }
                else
                {
                    _animationOpacity -= ANIMATION_STEP;
                    if (_animationOpacity <= FADE_MIN_OPACITY)
                    {
                        _animationOpacity = FADE_MIN_OPACITY;
                        _animationDirection = true;
                    }
                }
                needsUpdate = true;
                break;

            case BadgeAnimation.Pulse:
                if (_animationDirection)
                {
                    _animationScale -= ANIMATION_STEP * 0.15f;
                    _animationOpacity -= ANIMATION_STEP * 0.4f;
                    if (_animationScale <= PULSE_MIN_SCALE)
                    {
                        _animationScale = PULSE_MIN_SCALE;
                        _animationOpacity = PULSE_MIN_OPACITY;
                        _animationDirection = false;
                    }
                }
                else
                {
                    _animationScale += ANIMATION_STEP * 0.15f;
                    _animationOpacity += ANIMATION_STEP * 0.4f;
                    if (_animationScale >= PULSE_MAX_SCALE)
                    {
                        _animationScale = PULSE_MAX_SCALE;
                        _animationOpacity = PULSE_MAX_OPACITY;
                        _animationDirection = true;
                    }
                }
                needsUpdate = true;
                break;
        }

        if (needsUpdate && _control != null && !_control.IsDisposed && _control.IsHandleCreated)
        {
            _control.Invalidate();
        }
    }
    #endregion
}