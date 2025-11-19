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
/// View element that can draw a content
/// </summary>
public class ViewDrawContent : ViewLeaf
{
    #region Static Fields
    private static PropertyInfo? _pi;
    #endregion

    #region Instance Fields
    internal IPaletteContent? _paletteContent;
    private IDisposable? _memento;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawContent class.
    /// </summary>
    /// <param name="paletteContent">Palette source for the content.</param>
    /// <param name="values">Reference to actual content values.</param>
    /// <param name="orientation">Visual orientation of the content.</param>
    public ViewDrawContent(IPaletteContent? paletteContent, 
        IContentValues? values,
        VisualOrientation orientation)
    {
        // Cache the starting values
        _paletteContent = paletteContent;
        Values = values;
        Orientation = orientation;

        // Default other state
        TestForFocusCues = false;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawContent:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Dispose of old memento first
            if (_memento != null)
            {
                _memento.Dispose();
                _memento = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region TestForFocusCues
    /// <summary>
    /// Gets and sets the use of focus cues for deciding if focus rects are allowed.
    /// </summary>
    public bool TestForFocusCues { get; set; }

    #endregion

    #region Values
    /// <summary>
    /// Gets and sets the source for values.
    /// </summary>
    public IContentValues? Values { get; set; }

    #endregion

    #region Orientation
    /// <summary>
    /// Gets and sets the visual orientation.
    /// </summary>
    public VisualOrientation Orientation
    {
        [DebuggerStepThrough]
        get;
        set;
    }

    #endregion

    #region UseMnemonic
    /// <summary>
    /// Gets and sets the use of mnemonics.
    /// </summary>
    public bool UseMnemonic
    {
        [DebuggerStepThrough]
        get;
        set;
    }

    #endregion

    #region SetPalette
    /// <summary>
    /// Update the source palette for drawing.
    /// </summary>
    /// <param name="paletteContent">Palette source for the content.</param>
    public void SetPalette([DisallowNull] IPaletteContent paletteContent)
    {
        Debug.Assert(paletteContent != null);

        // Use newly provided palette
        _paletteContent = paletteContent;
    }
    #endregion

    #region GetPalette
    /// <summary>
    /// Gets the source palette used for drawing.
    /// </summary>
    /// <returns>Palette source for the content.</returns>
    public IPaletteContent? GetPalette() => _paletteContent;

    #endregion

    #region IsImageDisplayed

    /// <summary>
    /// Get a value indicating if the content image is being displayed.
    /// </summary>
    /// <param name="context">ViewLayoutContext context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public bool IsImageDisplayed([DisallowNull] ViewContext context)
    {
        Debug.Assert(context is not null);

        // Validate incoming reference
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        var isDisplayed = false;

        // If we have some content to investigate
        if (_paletteContent?.GetContentDraw(State) == InheritBool.True)
        {
            isDisplayed = context.Renderer.RenderStandardContent.GetContentImageDisplayed(_memento!);
        }

        return isDisplayed;
    }
    #endregion

    #region ImageRectangle

    /// <summary>
    /// Get a value indicating if the content image is being displayed.
    /// </summary>
    /// <param name="context">ViewLayoutContext context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public Rectangle ImageRectangle([DisallowNull] ViewContext context)
    {
        Debug.Assert(context is not null);

        // Validate incoming reference
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        var imageRect = Rectangle.Empty;

        // If we have some content to investigate
        if (_paletteContent!.GetContentDraw(State) == InheritBool.True)
        {
            imageRect = context.Renderer.RenderStandardContent.GetContentImageRectangle(_memento!);
        }

        return imageRect;
    }
    #endregion

    #region ShortTextRect

    /// <summary>
    /// Gets the short text drawing rectangle.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Rectangle of short text drawing.</returns>
    public Rectangle ShortTextRect([DisallowNull] ViewContext context)
    {
        Debug.Assert(context != null);

        // Validate incoming reference
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        var textRect = Rectangle.Empty;

        // If we have some content to investigate
        if (_paletteContent!.GetContentDraw(State) == InheritBool.True)
        {
            textRect = context.Renderer.RenderStandardContent.GetContentShortTextRectangle(_memento!);
        }

        return textRect;
    }
    #endregion

    #region LongTextRect

    /// <summary>
    /// Gets the short text drawing rectangle.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Rectangle of short text drawing.</returns>
    public Rectangle LongTextRect([DisallowNull] ViewContext context)
    {
        Debug.Assert(context is not null);

        // Validate incoming reference
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        var textRect = Rectangle.Empty;

        // If we have some content to investigate
        if (_paletteContent!.GetContentDraw(State) == InheritBool.True)
        {
            textRect = context.Renderer.RenderStandardContent.GetContentLongTextRectangle(_memento!);
        }

        return textRect;
    }
    #endregion

    #region Layout

    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context is not null);

        // Validate incoming reference
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // By default we take up no space at all
        var preferredSize = Size.Empty;

        // If we have some content to encompass
        if (_paletteContent?.GetContentDraw(State) == InheritBool.True)
        {
            // Ask the renderer for the contents preferred size
            preferredSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context,
                _paletteContent,
                Values!,
                Orientation,
                State);
        }

        return preferredSize;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context is not null);

        // Validate incoming reference
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // We take on all the available display area
        ClientRectangle = context.DisplayRectangle;

        // Do we need to draw the content?
        if (_paletteContent!.GetContentDraw(State) == InheritBool.True)
        {
            // Dispose of old memento first
            if (_memento != null)
            {
                _memento.Dispose();
                _memento = null;
            }

            // Ask the renderer to perform any internal laying out calculations and 
            // store the returned memento ready for whenever a draw is required
            _memento = context.Renderer.RenderStandardContent.LayoutContent(context,
                ClientRectangle,
                _paletteContent,
                Values!,
                Orientation,
                State);
        }
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore([DisallowNull] RenderContext context) 
    {
        Debug.Assert(context is not null);

        // Validate incoming reference
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // Do we need to draw the content?
        if (_paletteContent!.GetContentDraw(State) == InheritBool.True)
        {
            var allowFocusRect = !TestForFocusCues || ShowFocusCues(context.Control!);

            // Draw using memento returned from render layout
            context.Renderer.RenderStandardContent.DrawContent(context,
                ClientRectangle,
                _paletteContent,
                _memento!,
                Orientation,
                State,
                allowFocusRect);
        }
    }
    #endregion

    #region Implementation
    private bool ShowFocusCues(Control c)
    {
        if (_pi == null)
        {
            _pi = typeof(Control).GetProperty(nameof(ShowFocusCues), BindingFlags.Instance |
                                                                     BindingFlags.GetProperty |
                                                                     BindingFlags.NonPublic);
        }

        return (bool)_pi!.GetValue(c, null)!;
    }
    #endregion
}