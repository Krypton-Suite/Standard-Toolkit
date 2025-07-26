#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provide accurate text measuring and drawing capability.
/// </summary>
public class AccurateText : GlobalId
{
    #region Static Fields

    private const int GLOW_EXTRA_WIDTH = 14;
    private const int GLOW_EXTRA_HEIGHT = 3;

    // DO NOT USE THIS CACHING, because when the Theme Font is changed and there is a global update, 
    // the whole thing gets locked up in resizing......
    //private static TimedCache<(string text, Font font, StringFormatFlags formatFlags, TextRenderingHint hint), AccurateTextMemento> _cache 
    //    = new(TimeSpan.FromMinutes(10));

    #endregion

    #region Public Static Methods

    /// <summary>
    /// Pixel accurate measure of the specified string when drawn with the specified Font object.
    /// </summary>
    /// <param name="g">Graphics instance used to measure text.</param>
    /// <param name="rtl">Right to left setting for control.</param>
    /// <param name="text">String to measure.</param>
    /// <param name="font">Font object that defines the text format of the string.</param>
    /// <param name="trim">How to trim excess text.</param>
    /// <param name="align">How to align multi-line text.</param>
    /// <param name="prefix">How to process prefix characters.</param>
    /// <param name="hint">Rendering hint.</param>
    /// <param name="disposeFont">Dispose of font when finished with it.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>A memento used to draw the text.</returns>
    public static AccurateTextMemento MeasureString([DisallowNull] Graphics g,
        RightToLeft rtl,
        [DisallowNull] string text,
        [DisallowNull] Font? font,
        PaletteTextTrim trim,
        PaletteRelativeAlign align,
        PaletteTextHotkeyPrefix prefix,
        TextRenderingHint hint,
        bool disposeFont)
    {
        Debug.Assert(g != null);
        Debug.Assert(text != null);
        Debug.Assert(font != null);

        if (g == null)
        {
            throw new ArgumentNullException(nameof(g));
        }

        if (text == null)
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (font == null)
        {
            throw new ArgumentNullException(nameof(font));
        }

        // An empty string cannot be drawn, so uses the empty memento
        if (text.Length == 0)
        {
            return AccurateTextMemento.Empty;
        }

        // Create the format object used when measuring and drawing
        var format = new StringFormat { FormatFlags = StringFormatFlags.NoClip };

        // Ensure that text reflects reversed RTL setting
        if (rtl == RightToLeft.Yes)
        {
            format.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
        }

        // How do we position text horizontally?
        switch (align)
        {
            case PaletteRelativeAlign.Near:
                format.Alignment = (rtl == RightToLeft.Yes) ? StringAlignment.Far : StringAlignment.Near;
                break;

            case PaletteRelativeAlign.Center:
                format.Alignment = StringAlignment.Center;
                break;

            case PaletteRelativeAlign.Far:
                format.Alignment = (rtl == RightToLeft.Yes) ? StringAlignment.Near : StringAlignment.Far;
                break;

            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(align.ToString());
                break;
        }

        // Do we need to trim text that is too big?
        switch (trim)
        {
            case PaletteTextTrim.Character:
                format.Trimming = StringTrimming.Character;
                break;

            case PaletteTextTrim.EllipsisCharacter:
                format.Trimming = StringTrimming.EllipsisCharacter;
                break;

            case PaletteTextTrim.EllipsisPath:
                format.Trimming = StringTrimming.EllipsisPath;
                break;

            case PaletteTextTrim.EllipsisWord:
                format.Trimming = StringTrimming.EllipsisWord;
                break;

            case PaletteTextTrim.Word:
                format.Trimming = StringTrimming.Word;
                break;

            case PaletteTextTrim.Hide:
                format.Trimming = StringTrimming.None;
                break;

            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(trim.ToString());
                break;
        }

        // Setup the correct prefix processing
        switch (prefix)
        {
            case PaletteTextHotkeyPrefix.None:
                format.HotkeyPrefix = HotkeyPrefix.None;
                break;

            case PaletteTextHotkeyPrefix.Hide:
                format.HotkeyPrefix = HotkeyPrefix.Hide;
                break;

            case PaletteTextHotkeyPrefix.Show:
                format.HotkeyPrefix = HotkeyPrefix.Show;
                break;

            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(prefix.ToString());
                break;
        }

        // Optimisation: Lookup key before performing expensive / slow GDI functions
        //var key = (text, font, format.FormatFlags, hint);
        var memento = //_cache.GetOrCreate(key,
            () =>
            {

                // Replace tab characters with a fixed four spaces
                text = text.Replace("\t", @"    ");

                // Perform actual measure of the text
                using var graphicsHint = new GraphicsTextHint(g, hint);
                var textSize = SizeF.Empty;

                try
                {
                    // Declare a proposed size with dimensions set to the maximum integer value.
                    var proposedSize = new Size(int.MaxValue, int.MaxValue);
                    textSize = g.MeasureString(text, font, proposedSize, format);
                }
                catch
                {
                    // ignored
                }

                return new AccurateTextMemento(text, font, textSize, format, hint, disposeFont);
            };
        //);
        // Return a memento with drawing details
        return memento.Invoke();
    }

    /// <summary>
    /// Pixel accurate drawing of the requested text memento information.
    /// </summary>
    /// <param name="g">Graphics object used for drawing.</param>
    /// <param name="brush">Brush for drawing text with.</param>
    /// <param name="rect">Rectangle to draw text inside.</param>
    /// <param name="rtl">Right to left setting for control.</param>
    /// <param name="orientation">Orientation for drawing text.</param>
    /// <param name="state">State of the source element.</param>
    /// <param name="memento">Memento containing text context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>True if draw succeeded; False is draw produced an error.</returns>
    public static bool DrawString([DisallowNull] Graphics g,
        Brush brush,
        Rectangle rect,
        RightToLeft rtl,
        VisualOrientation orientation,
        PaletteState state,
        [DisallowNull] AccurateTextMemento memento)
    {
        Debug.Assert(g != null);
        Debug.Assert(memento != null);

        // Cannot draw with a null graphics instance
        if (g == null)
        {
            throw new ArgumentNullException(nameof(g));
        }

        // Cannot draw with a null memento instance
        if (memento == null)
        {
            throw new ArgumentNullException(nameof(memento));
        }

        var ret = true;

        // Is there a valid place to be drawn into
        if (rect is { Width: > 0, Height: > 0 })
        {
            // Does the memento contain something to draw?
            if (!memento.IsEmpty)
            {
                var translateX = 0;
                var translateY = 0;
                var rotation = 0f;

                // Perform any transformations needed for orientation
                switch (orientation)
                {
                    case VisualOrientation.Bottom:
                        // Translate to opposite side of origin, so the rotate can 
                        // then bring it back to original position but mirror image
                        translateX = (rect.X * 2) + rect.Width;
                        translateY = (rect.Y * 2) + rect.Height;
                        rotation = 180f;
                        break;

                    case VisualOrientation.Left:
                        // Invert the dimensions of the rectangle for drawing upwards
                        rect = rect with { Width = rect.Height, Height = rect.Width };
                        // Translate back from a quarter left turn to the original place 
                        translateX = rect.X - rect.Y - 1;
                        translateY = rect.X + rect.Y + rect.Width;
                        rotation = 270;
                        break;

                    case VisualOrientation.Right:
                        // Invert the dimensions of the rectangle for drawing upwards
                        rect = rect with { Width = rect.Height, Height = rect.Width };

                        // Translate back from a quarter right turn to the original place 
                        translateX = rect.X + rect.Y + rect.Height + 1;
                        translateY = -(rect.X - rect.Y);
                        rotation = 90f;
                        break;
                }

                // Apply the transforms if we have any to apply
                if ((translateX != 0) || (translateY != 0))
                {
                    g.TranslateTransform(translateX, translateY);
                }

                if (rotation != 0f)
                {
                    g.RotateTransform(rotation);
                }

                try
                {
                    // Support for unicode surrogates is only available when drawing horizontally.
                    if (orientation == VisualOrientation.Top)
                    {
                        // Only a brush is provided, so we have to get the color from it since
                        // DrawText only works with solid colors.
                        Color color = brush is SolidBrush b
                            ? b.Color
                            : brush is LinearGradientBrush l
                                ? l.LinearColors[0]
                                : KryptonManager.CurrentGlobalPalette.GetContentShortTextColor1(PaletteContentStyle.LabelNormalControl, PaletteState.Normal);

                        // Convert from StringFormat to TextFormatFlags
                        var tff = StringFormatToFlags(memento.Format);

                        // End line ellipsis don't work well with DrawText and tend to cut off words when not needed
                        // DrawString seems to do this better
                        tff &= ~(TextFormatFlags.EndEllipsis | TextFormatFlags.WordEllipsis | TextFormatFlags.PathEllipsis);
                        
                        // Whatever happens, NoClipping is on
                        tff |= TextFormatFlags.NoClipping;

                        TextRenderer.DrawText(g, memento.Text, memento.Font!, rect, color, tff);
                    }
                    else
                    {
                        g.DrawString(memento.Text, memento.Font!, brush, rect, memento.Format);
                    }
                }
                catch
                {
                    // Ignore any error from the DrawString, usually because the display settings
                    // have changed causing Fonts to be invalid. Our controls will notice the change
                    // and refresh the fonts but sometimes the draw happens before the fonts are
                    // regenerated. Just ignore message and everything will sort itself out. Trust me!
                    ret = false;
                }
                finally
                {
                    // Remove the applied transforms
                    if (rotation != 0f)
                    {
                        g.RotateTransform(-rotation);
                    }

                    if ((translateX != 0) || (translateY != 0))
                    {
                        g.TranslateTransform(-translateX, -translateY);
                    }
                }
            }
        }

        return ret;
    }

    private static Color ContrastColor(Color color)
    {
        //  Counting the perceptive luminance - human eye favours green colour... 
        var a = 1
                - (((0.299 * color.R)
                    + ((0.587 * color.G) + (0.114 * color.B)))
                   / 255);
        var d = a < 0.5 ? 0 : 255;

        //  dark colours - white font
        return Color.FromArgb(d, d, d);
    }
    #endregion

    #region Implementation
    private static StringFormat FlagsToStringFormat(TextFormatFlags flags)
    {
        var sf = new StringFormat();

        // Translation table: http://msdn.microsoft.com/msdnmag/issues/06/03/TextRendering/default.aspx?fig=true#fig4

        // Horizontal Alignment
        if ((flags & TextFormatFlags.HorizontalCenter) == TextFormatFlags.HorizontalCenter)
        {
            sf.Alignment = StringAlignment.Center;
        }
        else if ((flags & TextFormatFlags.Right) == TextFormatFlags.Right)
        {
            sf.Alignment = StringAlignment.Far;
        }
        else
        {
            sf.Alignment = StringAlignment.Near;
        }

        // Vertical Alignment
        if ((flags & TextFormatFlags.Bottom) == TextFormatFlags.Bottom)
        {
            sf.LineAlignment = StringAlignment.Far;
        }
        else if ((flags & TextFormatFlags.VerticalCenter) == TextFormatFlags.VerticalCenter)
        {
            sf.LineAlignment = StringAlignment.Center;
        }
        else
        {
            sf.LineAlignment = StringAlignment.Near;
        }

        // Ellipsis
        if ((flags & TextFormatFlags.EndEllipsis) == TextFormatFlags.EndEllipsis)
        {
            sf.Trimming = StringTrimming.EllipsisCharacter;
        }
        else if ((flags & TextFormatFlags.PathEllipsis) == TextFormatFlags.PathEllipsis)
        {
            sf.Trimming = StringTrimming.EllipsisPath;
        }
        else if ((flags & TextFormatFlags.WordEllipsis) == TextFormatFlags.WordEllipsis)
        {
            sf.Trimming = StringTrimming.EllipsisWord;
        }
        else
        {
            sf.Trimming = StringTrimming.Character;
        }

        // Hotkey Prefix
        if ((flags & TextFormatFlags.NoPrefix) == TextFormatFlags.NoPrefix)
        {
            sf.HotkeyPrefix = HotkeyPrefix.None;
        }
        else if ((flags & TextFormatFlags.HidePrefix) == TextFormatFlags.HidePrefix)
        {
            sf.HotkeyPrefix = HotkeyPrefix.Hide;
        }
        else
        {
            sf.HotkeyPrefix = HotkeyPrefix.Show;
        }

        // Text Padding
        if ((flags & TextFormatFlags.NoPadding) == TextFormatFlags.NoPadding)
        {
            sf.FormatFlags |= StringFormatFlags.FitBlackBox;
        }

        // Text Wrapping
        if ((flags & TextFormatFlags.SingleLine) == TextFormatFlags.SingleLine)
        {
            sf.FormatFlags |= StringFormatFlags.NoWrap;
        }
        else if ((flags & TextFormatFlags.TextBoxControl) == TextFormatFlags.TextBoxControl)
        {
            sf.FormatFlags |= StringFormatFlags.LineLimit;
        }

        // Other Flags
        //if ((flags & TextFormatFlags.RightToLeft) == TextFormatFlags.RightToLeft)
        //        sf.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
        if ((flags & TextFormatFlags.NoClipping) == TextFormatFlags.NoClipping)
        {
            sf.FormatFlags |= StringFormatFlags.NoClip;
        }

        return sf;
    }

    private static TextFormatFlags StringFormatToFlags(StringFormat sf)
    {
        var flags = new TextFormatFlags();

        // Link is dead
        // Translation table: http://msdn.microsoft.com/msdnmag/issues/06/03/TextRendering/default.aspx?fig=true#fig4

        flags = sf.Alignment switch
        {
            // Horizontal Alignment
            StringAlignment.Center => TextFormatFlags.HorizontalCenter,
            StringAlignment.Far => TextFormatFlags.Right,
            _ => TextFormatFlags.Left
        };

        flags |= sf.LineAlignment switch
        {
            // Vertical Alignment
            StringAlignment.Far => TextFormatFlags.Bottom,
            StringAlignment.Center => TextFormatFlags.VerticalCenter,
            _ => TextFormatFlags.Top
        };

        switch (sf.Trimming)
        {
            // Ellipsis
            case StringTrimming.EllipsisCharacter:
                flags |= TextFormatFlags.EndEllipsis;
                break;
            case StringTrimming.EllipsisPath:
                flags |= TextFormatFlags.PathEllipsis;
                break;
            case StringTrimming.EllipsisWord:
                flags |= TextFormatFlags.WordEllipsis;
                break;
        }

        switch (sf.HotkeyPrefix)
        {
            // Hotkey Prefix
            case HotkeyPrefix.None:
                flags |= TextFormatFlags.NoPrefix;
                break;
            case HotkeyPrefix.Hide:
                flags |= TextFormatFlags.HidePrefix;
                break;
        }

        switch (sf.FormatFlags)
        {
            // Text Padding
            case StringFormatFlags.FitBlackBox:
                flags |= TextFormatFlags.NoPadding;
                break;
            // Text Wrapping
            case StringFormatFlags.NoWrap:
                flags |= TextFormatFlags.SingleLine;
                break;
            case StringFormatFlags.LineLimit:
                flags |= TextFormatFlags.TextBoxControl;
                break;
            // Other Flags
            case StringFormatFlags.DirectionRightToLeft:
                flags |= TextFormatFlags.RightToLeft;
                break;
            case StringFormatFlags.NoClip:
                flags |= TextFormatFlags.NoClipping;
                break;
        }

        return flags;
    }
    #endregion
}