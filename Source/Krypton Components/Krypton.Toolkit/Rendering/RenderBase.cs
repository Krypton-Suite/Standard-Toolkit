#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides base class for rendering implementations.
/// </summary>
[ToolboxItem(false)]
public abstract class RenderBase : Component,
    IRenderer,
    IRenderBorder,
    IRenderBack,
    IRenderContent,
    IRenderTabBorder,
    IRenderRibbon,
    IRenderGlyph
{
    #region IRenderer
    /// <summary>
    /// Gets the standard border renderer.
    /// </summary>
    public IRenderBorder RenderStandardBorder
    {
        [DebuggerStepThrough]
        get => this;
    }

    /// <summary>
    /// Gets the standard background renderer.
    /// </summary>
    public IRenderBack RenderStandardBack
    {
        [DebuggerStepThrough]
        get => this;
    }

    /// <summary>
    /// Gets the standard content renderer.
    /// </summary>
    public IRenderContent RenderStandardContent
    {
        [DebuggerStepThrough]
        get => this;
    }

    /// <summary>
    /// Gets the tab border renderer.
    /// </summary>
    public IRenderTabBorder RenderTabBorder
    {
        [DebuggerStepThrough]
        get => this;
    }

    /// <summary>
    /// Gets the ribbon renderer.
    /// </summary>
    public IRenderRibbon RenderRibbon
    {
        [DebuggerStepThrough]
        get => this;
    }

    /// <summary>
    /// Gets the glyph renderer.
    /// </summary>
    public IRenderGlyph RenderGlyph
    {
        [DebuggerStepThrough]
        get => this;
    }

    /// <summary>
    /// Gets a renderer for drawing the toolstrips.
    /// </summary>
    /// <param name="colorPalette">Color palette to use when rendering toolstrip.</param>
    public abstract ToolStripRenderer RenderToolStrip([DisallowNull] PaletteBase? colorPalette);
    #endregion

    #region RenderStandardBorder
    /// <summary>
    /// Gets the raw padding used per edge of the border.
    /// </summary>
    /// <param name="palette">Palette used for drawing.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <param name="orientation">Visual orientation of the border.</param>
    /// <returns>Padding structure detailing all four edges.</returns>
    public abstract Padding GetBorderRawPadding(IPaletteBorder palette,
        PaletteState state,
        VisualOrientation orientation);

    /// <summary>
    /// Gets the padding used to position display elements completely inside border drawing.
    /// </summary>
    /// <param name="palette">Palette used for drawing.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <param name="orientation">Visual orientation of the border.</param>
    /// <returns>Padding structure detailing all four edges.</returns>
    public virtual Padding GetBorderDisplayPadding(IPaletteBorder? palette,
        PaletteState state,
        VisualOrientation orientation) =>
        GetBorderDisplayPadding(palette, state, orientation, Size.Empty);

    /// <summary>
    /// Gets the padding used to position display elements completely inside border drawing.
    /// </summary>
    /// <param name="palette">Palette used for drawing.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <param name="orientation">Visual orientation of the border.</param>
    /// <param name="borderOuterSize">Outer size of the bordered area; use <see cref="Size.Empty"/> for legacy behaviour.</param>
    /// <returns>Padding structure detailing all four edges.</returns>
    public abstract Padding GetBorderDisplayPadding(IPaletteBorder? palette,
        PaletteState state,
        VisualOrientation orientation,
        Size borderOuterSize);

    /// <summary>
    /// Generate a graphics path that is the outside edge of the border.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Target rectangle.</param>
    /// <param name="palette">Palette used for drawing.</param>
    /// <param name="orientation">Visual orientation of the border.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <returns>GraphicsPath instance.</returns>
    public abstract GraphicsPath GetOutsideBorderPath(RenderContext context,
        Rectangle rect,
        IPaletteBorder? palette,
        VisualOrientation orientation,
        PaletteState state);

    /// <summary>
    /// Generate a graphics path that is in the middle of the border.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Target rectangle.</param>
    /// <param name="palette">Palette used for drawing.</param>
    /// <param name="orientation">Visual orientation of the border.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <returns>GraphicsPath instance.</returns>
    public abstract GraphicsPath GetBorderPath(RenderContext context,
        Rectangle rect,
        IPaletteBorder palette,
        VisualOrientation orientation,
        PaletteState state);

    /// <summary>
    /// Generate a graphics path that encloses the border and is used when rendering a background to ensure the background does not draw over the border area.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Target rectangle.</param>
    /// <param name="palette">Palette used for drawing.</param>
    /// <param name="orientation">Visual orientation of the border.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <returns>GraphicsPath instance.</returns>
    public abstract GraphicsPath GetBackPath(RenderContext context,
        Rectangle rect,
        IPaletteBorder palette,
        VisualOrientation orientation,
        PaletteState state);

    /// <summary>
    /// Draw border on the inside edge of the specified rectangle.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Target rectangle.</param>
    /// <param name="palette">Palette used for drawing.</param>
    /// <param name="orientation">Visual orientation of the border.</param>
    /// <param name="state">State associated with rendering.</param>
    public abstract void DrawBorder(RenderContext context,
        Rectangle rect,
        IPaletteBorder palette,
        VisualOrientation orientation,
        PaletteState state);
    #endregion

    #region RenderStandardBack
    /// <summary>
    /// Draw background to fill the specified path.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Target rectangle that encloses path.</param>
    /// <param name="path">Graphics path.</param>
    /// <param name="palette">Palette used for drawing.</param>
    /// <param name="orientation">Visual orientation of the background.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <param name="memento">Cache used for drawing.</param>
    public abstract IDisposable? DrawBack(RenderContext context,
        Rectangle rect,
        GraphicsPath path,
        IPaletteBack palette,
        VisualOrientation orientation,
        PaletteState state,
        IDisposable? memento);
    #endregion

    #region RenderStandardContent

    /// <summary>
    /// Get the preferred size for drawing the content.
    /// </summary>
    /// <param name="context">Layout context.</param>
    /// <param name="palette">Content palette details.</param>
    /// <param name="values">Content values.</param>
    /// <param name="orientation">Visual orientation of the content.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <returns>Preferred size.</returns>
    public abstract Size GetContentPreferredSize(ViewLayoutContext context,
        IPaletteContent palette,
        IContentValues values,
        VisualOrientation orientation,
        PaletteState state);

    /// <summary>
    /// Perform layout calculations on the provided content.
    /// </summary>
    /// <param name="context">Layout context.</param>
    /// <param name="availableRect">Space available for laying out.</param>
    /// <param name="palette">Content palette details.</param>
    /// <param name="values">Content values.</param>
    /// <param name="orientation">Visual orientation of the content.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <returns>Memento with cached information.</returns>
    public abstract IDisposable LayoutContent(ViewLayoutContext context,
        Rectangle availableRect,
        IPaletteContent palette,
        IContentValues values,
        VisualOrientation orientation,
        PaletteState state);

    /// <summary>
    /// Perform draw of content using provided memento.
    /// </summary>
    /// <param name="context">Render context.</param>
    /// <param name="displayRect">Display area available for drawing.</param>
    /// <param name="palette">Content palette details.</param>
    /// <param name="memento">Cached values from layout call.</param>
    /// <param name="orientation">Visual orientation of the content.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <param name="allowFocusRect">Allow drawing of focus rectangle.</param>
    public abstract void DrawContent(RenderContext context,
        Rectangle displayRect,
        IPaletteContent palette,
        IDisposable memento,
        VisualOrientation orientation,
        PaletteState state,
        bool allowFocusRect);

    /// <summary>
    /// Request the calculated display of the image.
    /// </summary>
    /// <param name="memento">Cached values from layout call.</param>
    /// <returns>True if the image is being Displayed; otherwise false.</returns>
    public abstract bool GetContentImageDisplayed(IDisposable? memento);

    /// <summary>
    /// Request the calculated position of the content image.
    /// </summary>
    /// <param name="memento">Cached values from layout call.</param>
    /// <returns>Display rectangle for the image content.</returns>
    public abstract Rectangle GetContentImageRectangle(IDisposable? memento);

    /// <summary>
    /// Request the calculated display of the short text.
    /// </summary>
    /// <param name="memento">Cached values from layout call.</param>
    /// <returns>True if the short text is being Displayed; otherwise false.</returns>
    public abstract bool GetContentShortTextDisplayed(IDisposable? memento);

    /// <summary>
    /// Request the calculated position of the content short text.
    /// </summary>
    /// <param name="memento">Cached values from layout call.</param>
    /// <returns>Display rectangle for the image content.</returns>
    public abstract Rectangle GetContentShortTextRectangle(IDisposable? memento);

    /// <summary>
    /// Request the calculated display of the long text.
    /// </summary>
    /// <param name="memento">Cached values from layout call.</param>
    /// <returns>True if the long text is being Displayed; otherwise false.</returns>
    public abstract bool GetContentLongTextDisplayed(IDisposable? memento);

    /// <summary>
    /// Request the calculated position of the content long text.
    /// </summary>
    /// <param name="memento">Cached values from layout call.</param>
    /// <returns>Display rectangle for the image content.</returns>
    public abstract Rectangle GetContentLongTextRectangle(IDisposable? memento);
    #endregion

    #region RenderTabBorder
    /// <summary>
    /// Gets if the tabs should be drawn from left to right for z-ordering.
    /// </summary>
    /// <param name="tabBorderStyle">Style of tab border.</param>
    /// <returns>True for left to right, otherwise draw right to left.</returns>
    public abstract bool GetTabBorderLeftDrawing(TabBorderStyle tabBorderStyle);

    /// <summary>
    /// Gets the spacing used to separate each tab border instance.
    /// </summary>
    /// <param name="tabBorderStyle">Style of tab border.</param>
    /// <returns>Number of pixels to space instances.</returns>
    public abstract int GetTabBorderSpacingGap(TabBorderStyle tabBorderStyle);

    /// <summary>
    /// Gets the padding used to position display elements completely inside border drawing.
    /// </summary>
    /// <param name="context">Layout context.</param>
    /// <param name="palette">Palette used for drawing.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <param name="orientation">Visual orientation of the border.</param>
    /// <param name="tabBorderStyle">Style of tab border.</param>
    /// <returns>Padding structure detailing all four edges.</returns>
    public abstract Padding GetTabBorderDisplayPadding(ViewLayoutContext context,
        IPaletteBorder palette,
        PaletteState state,
        VisualOrientation orientation,
        TabBorderStyle tabBorderStyle);

    /// <summary>
    /// Generate a graphics path that encloses the border itself.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Target rectangle.</param>
    /// <param name="palette">Palette used for drawing.</param>
    /// <param name="orientation">Visual orientation of the border.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <param name="tabBorderStyle">Style of tab border.</param>
    /// <returns>GraphicsPath instance.</returns>
    public abstract GraphicsPath GetTabBorderPath(RenderContext context,
        Rectangle rect,
        IPaletteBorder palette,
        VisualOrientation orientation,
        PaletteState state,
        TabBorderStyle tabBorderStyle);

    /// <summary>
    /// Generate a graphics path that encloses the border and is used when rendering a background to ensure the background does not draw over the border area.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Target rectangle.</param>
    /// <param name="palette">Palette used for drawing.</param>
    /// <param name="orientation">Visual orientation of the border.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <param name="tabBorderStyle">Style of tab border.</param>
    /// <returns>GraphicsPath instance.</returns>
    public abstract GraphicsPath GetTabBackPath(RenderContext context,
        Rectangle rect,
        IPaletteBorder palette,
        VisualOrientation orientation,
        PaletteState state,
        TabBorderStyle tabBorderStyle);

    /// <summary>
    /// Draw border on the inside edge of the specified rectangle.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Target rectangle.</param>
    /// <param name="palette">Palette used for drawing.</param>
    /// <param name="orientation">Visual orientation of the border.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <param name="tabBorderStyle">Style of tab border.</param>
    public abstract void DrawTabBorder(RenderContext context,
        Rectangle rect,
        IPaletteBorder palette,
        VisualOrientation orientation,
        PaletteState state,
        TabBorderStyle tabBorderStyle);
    #endregion

    #region RenderRibbon

    /// <summary>
    /// Draw the background of a ribbon element.
    /// </summary>
    /// <param name="shape">Ribbon shape.</param>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Target rectangle.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <param name="palette">Palette used for sourcing settings.</param>
    /// <param name="orientation">Orientation for drawing.</param>
    /// <param name="memento">Cached values to use when drawing.</param>
    public abstract IDisposable? DrawRibbonBack(PaletteRibbonShape shape,
        RenderContext context,
        Rectangle rect,
        PaletteState state,
        IPaletteRibbonBack palette,
        VisualOrientation orientation,
        IDisposable? memento);

    /// <summary>
    /// Draw a context ribbon tab title.
    /// </summary>
    /// <param name="shape">Ribbon shape.</param>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Target rectangle.</param>
    /// <param name="paletteGeneral">Palette used for general ribbon settings.</param>
    /// <param name="paletteBack">Palette used for background ribbon settings.</param>
    /// <param name="memento">Cached values to use when drawing.</param>
    public abstract IDisposable? DrawRibbonTabContextTitle(PaletteRibbonShape shape,
        RenderContext context,
        Rectangle rect,
        IPaletteRibbonGeneral paletteGeneral,
        IPaletteRibbonBack paletteBack,
        IDisposable? memento);

    /// <summary>
    /// Draw the application button.
    /// </summary>
    /// <param name="shape">Ribbon shape.</param>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Target rectangle.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <param name="palette">Palette used for sourcing settings.</param>
    /// <param name="memento">Cached storage for drawing objects.</param>
    public abstract IDisposable? DrawRibbonApplicationButton(PaletteRibbonShape shape,
        RenderContext context,
        Rectangle rect,
        PaletteState state,
        IPaletteRibbonBack palette,
        IDisposable? memento);

    /// <summary>
    /// Draw the "File application tab"
    /// </summary>
    /// <param name="shape">Ribbon shape.</param>
    /// <param name="context">Rendering context.</param>
    /// <param name="rect">Target rectangle.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <param name="palette">Palette used for sourcing settings.</param>
    /// <param name="memento">Cached values to use when drawing.</param>
    public abstract IDisposable? DrawRibbonFileApplicationTab(PaletteRibbonShape shape,
        RenderContext context,
        Rectangle rect,
        PaletteState state,
        IPaletteRibbonFileAppTab palette,
        IDisposable? memento);

    /// <summary>
    /// Perform drawing of a ribbon cluster edge.
    /// </summary>
    /// <param name="shape">Ribbon shape.</param>
    /// <param name="context">Render context.</param>
    /// <param name="displayRect">Display area available for drawing.</param>
    /// <param name="paletteBack">Palette used for recovering drawing details.</param>
    /// <param name="state">State associated with rendering.</param>
    public abstract void DrawRibbonClusterEdge(PaletteRibbonShape shape,
        RenderContext context,
        Rectangle displayRect,
        IPaletteBack paletteBack,
        PaletteState state);
    #endregion

    #region RenderGlyph
    /// <summary>
    /// Perform drawing of a separator glyph.
    /// </summary>
    /// <param name="context">Render context.</param>
    /// <param name="displayRect">Display area available for drawing.</param>
    /// <param name="paletteBack">Background palette details.</param>
    /// <param name="paletteBorder">Border palette details.</param>
    /// <param name="orientation">Visual orientation of the content.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <param name="canMove">Can the separator be moved.</param>
    public abstract void DrawSeparator(RenderContext context,
        Rectangle displayRect,
        IPaletteBack paletteBack,
        IPaletteBorder paletteBorder,
        Orientation orientation,
        PaletteState state,
        bool canMove);

    /// <summary>
    /// Calculate the requested display size for the check box.
    /// </summary>
    /// <param name="context">Render context.</param>
    /// <param name="palette">Palette for sourcing display values.</param>
    /// <param name="enabled">Should check box be Displayed as enabled.</param>
    /// <param name="checkState">The checked state of the check box.</param>
    /// <param name="tracking">Should check box be Displayed as hot tracking.</param>
    /// <param name="pressed">Should check box be Displayed as pressed.</param>
    public abstract Size GetCheckBoxPreferredSize(ViewLayoutContext context,
        PaletteBase palette,
        bool enabled,
        CheckState checkState,
        bool tracking,
        bool pressed);

    /// <summary>
    /// Perform drawing of a check box.
    /// </summary>
    /// <param name="context">Render context.</param>
    /// <param name="displayRect">Display area available for drawing.</param>
    /// <param name="palette">Palette for sourcing display values.</param>
    /// <param name="enabled">Should check box be Displayed as enabled.</param>
    /// <param name="checkState">The checked state of the check box.</param>
    /// <param name="tracking">Should check box be Displayed as hot tracking.</param>
    /// <param name="pressed">Should check box be Displayed as pressed.</param>
    public abstract void DrawCheckBox(RenderContext context,
        Rectangle displayRect,
        PaletteBase palette,
        bool enabled,
        CheckState checkState,
        bool tracking,
        bool pressed);

    /// <summary>
    /// Calculate the requested display size for the radio button.
    /// </summary>
    /// <param name="context">Render context.</param>
    /// <param name="palette">Palette for sourcing display values.</param>
    /// <param name="enabled">Should check box be Displayed as enabled.</param>
    /// <param name="checkState">Checked state of the radio button.</param>
    /// <param name="tracking">Should check box be Displayed as hot tracking.</param>
    /// <param name="pressed">Should check box be Displayed as pressed.</param>
    public abstract Size GetRadioButtonPreferredSize(ViewLayoutContext context,
        PaletteBase palette,
        bool enabled,
        bool checkState,
        bool tracking,
        bool pressed);
    /// <summary>
    /// Perform drawing of a radio button.
    /// </summary>
    /// <param name="context">Render context.</param>
    /// <param name="displayRect">Display area available for drawing.</param>
    /// <param name="palette">Palette for sourcing display values.</param>
    /// <param name="enabled">Should radio button be Displayed as enabled.</param>
    /// <param name="checkState">Checked state of the radio button.</param>
    /// <param name="tracking">Should radio button be Displayed as hot tracking.</param>
    /// <param name="pressed">Should radio button be Displayed as pressed.</param>
    public abstract void DrawRadioButton(RenderContext context,
        Rectangle displayRect,
        PaletteBase palette,
        bool enabled,
        bool checkState,
        bool tracking,
        bool pressed);

    /// <summary>
    /// Calculate the requested display size for the drop-down button.
    /// </summary>
    /// <param name="context">Render context.</param>
    /// <param name="palette">Palette for sourcing display values.</param>
    /// <param name="state">State for which image size is needed.</param>
    /// <param name="orientation">How to orientate the image.</param>
    public abstract Size GetDropDownButtonPreferredSize(ViewLayoutContext context,
        IPaletteContent palette,
        PaletteState state,
        VisualOrientation orientation);

    /// <summary>
    /// Perform drawing of a drop-down button.
    /// </summary>
    /// <param name="context">Render context.</param>
    /// <param name="displayRect">Display area available for drawing.</param>
    /// <param name="palette">Palette for sourcing display values.</param>
    /// <param name="state">State for which image size is needed.</param>
    /// <param name="orientation">How to orientate the image.</param>
    public abstract void DrawDropDownButton(RenderContext context,
        Rectangle displayRect,
        IPaletteContent palette,
        PaletteState state,
        VisualOrientation orientation);

    /// <summary>
    /// Draw a numeric up button image appropriate for a input control.
    /// </summary>
    /// <param name="context">Render context.</param>
    /// <param name="cellRect">Available drawing rectangle space.</param>
    /// <param name="paletteContent">Content palette for getting colors.</param>
    /// <param name="state">State associated with rendering.</param>
    public abstract void DrawInputControlNumericUpGlyph(RenderContext context,
        Rectangle cellRect,
        IPaletteContent? paletteContent,
        PaletteState state);

    /// <summary>
    /// Draw a numeric down button image appropriate for a input control.
    /// </summary>
    /// <param name="context">Render context.</param>
    /// <param name="cellRect">Available drawing rectangle space.</param>
    /// <param name="paletteContent">Content palette for getting colors.</param>
    /// <param name="state">State associated with rendering.</param>
    public abstract void DrawInputControlNumericDownGlyph(RenderContext context,
        Rectangle cellRect,
        IPaletteContent? paletteContent,
        PaletteState state);

    /// <summary>
    /// Draw a drop-down grid appropriate for a input control.
    /// </summary>
    /// <param name="context">Render context.</param>
    /// <param name="cellRect">Available drawing rectangle space.</param>
    /// <param name="paletteContent">Content palette for getting colors.</param>
    /// <param name="state">State associated with rendering.</param>
    public abstract void DrawInputControlDropDownGlyph(RenderContext context,
        Rectangle cellRect,
        IPaletteContent paletteContent,
        PaletteState state);

    /// <summary>
    /// Perform drawing of a ribbon dialog box launcher glyph.
    /// </summary>
    /// <param name="shape">Ribbon shape.</param>
    /// <param name="context">Render context.</param>
    /// <param name="displayRect">Display area available for drawing.</param>
    /// <param name="paletteGeneral">General ribbon palette details.</param>
    /// <param name="state">State associated with rendering.</param>
    public abstract void DrawRibbonDialogBoxLauncher(PaletteRibbonShape shape,
        RenderContext context,
        Rectangle displayRect,
        IPaletteRibbonGeneral paletteGeneral,
        PaletteState state);

    /// <summary>
    /// Perform drawing of a ribbon drop arrow glyph.
    /// </summary>
    /// <param name="shape">Ribbon shape.</param>
    /// <param name="context">Render context.</param>
    /// <param name="displayRect">Display area available for drawing.</param>
    /// <param name="paletteGeneral">General ribbon palette details.</param>
    /// <param name="state">State associated with rendering.</param>
    public abstract void DrawRibbonDropArrow(PaletteRibbonShape shape,
        RenderContext context,
        Rectangle displayRect,
        IPaletteRibbonGeneral paletteGeneral,
        PaletteState state);

    /// <summary>
    /// Perform drawing of a ribbon context arrow glyph.
    /// </summary>
    /// <param name="shape">Ribbon shape.</param>
    /// <param name="context">Render context.</param>
    /// <param name="displayRect">Display area available for drawing.</param>
    /// <param name="paletteGeneral">General ribbon palette details.</param>
    /// <param name="state">State associated with rendering.</param>
    public abstract void DrawRibbonContextArrow(PaletteRibbonShape shape,
        RenderContext context,
        Rectangle displayRect,
        IPaletteRibbonGeneral paletteGeneral,
        PaletteState state);

    /// <summary>
    /// Perform drawing of a ribbon overflow image.
    /// </summary>
    /// <param name="shape">Ribbon shape.</param>
    /// <param name="context">Render context.</param>
    /// <param name="displayRect">Display area available for drawing.</param>
    /// <param name="paletteGeneral">General ribbon palette details.</param>
    /// <param name="state">State associated with rendering.</param>
    public abstract void DrawRibbonOverflow(PaletteRibbonShape shape,
        RenderContext context,
        Rectangle displayRect,
        IPaletteRibbonGeneral paletteGeneral,
        PaletteState state);

    /// <summary>
    /// Perform drawing of a ribbon group separator.
    /// </summary>
    /// <param name="shape">Ribbon shape.</param>
    /// <param name="context">Render context.</param>
    /// <param name="displayRect">Display area available for drawing.</param>
    /// <param name="paletteGeneral">General ribbon palette details.</param>
    /// <param name="state">State associated with rendering.</param>
    public abstract void DrawRibbonGroupSeparator(PaletteRibbonShape shape,
        RenderContext context,
        Rectangle displayRect,
        IPaletteRibbonGeneral paletteGeneral,
        PaletteState state);

    /// <summary>
    /// Draw a grid sorting direction glyph.
    /// </summary>
    /// <param name="context">Render context.</param>
    /// <param name="sortOrder">Sorting order of the glyph.</param>
    /// <param name="cellRect">Available drawing rectangle space.</param>
    /// <param name="paletteContent">Palette to use for sourcing values.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <param name="rtl">Should be drawn from right to left.</param>
    /// <returns>Remainder space left over for other drawing.</returns>
    public abstract Rectangle DrawGridSortGlyph(RenderContext context,
        SortOrder sortOrder,
        Rectangle cellRect,
        IPaletteContent paletteContent,
        PaletteState state,
        bool rtl);

    /// <summary>
    /// Draw a grid row glyph.
    /// </summary>
    /// <param name="context">Render context.</param>
    /// <param name="rowGlyph">Row glyph.</param>
    /// <param name="cellRect">Available drawing rectangle space.</param>
    /// <param name="paletteContent">Palette to use for sourcing values.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <param name="rtl">Should be drawn from right to left.</param>
    /// <returns>Remainder space left over for other drawing.</returns>
    public abstract Rectangle DrawGridRowGlyph(RenderContext context,
        GridRowGlyph rowGlyph,
        Rectangle cellRect,
        IPaletteContent? paletteContent,
        PaletteState state,
        bool rtl);

    /// <summary>
    /// Draw a grid error glyph.
    /// </summary>
    /// <param name="context">Render context.</param>
    /// <param name="cellRect">Available drawing rectangle space.</param>
    /// <param name="state">State associated with rendering.</param>
    /// <param name="rtl">Should be drawn from right to left.</param>
    /// <returns>Remainder space left over for other drawing.</returns>
    public abstract Rectangle DrawGridErrorGlyph(RenderContext context,
        Rectangle cellRect,
        PaletteState state,
        bool rtl);

    /// <summary>
    /// Draw a solid area glyph suitable for a drag drop area.
    /// </summary>
    /// <param name="context">Render context.</param>
    /// <param name="drawRect">Drawing rectangle space.</param>
    /// <param name="dragDropPalette">Palette source of drawing values.</param>
    public abstract void DrawDragDropSolidGlyph(RenderContext context,
        Rectangle drawRect,
        IPaletteDragDrop dragDropPalette);

    /// <summary>
    /// Measure the drag and drop docking glyphs.
    /// </summary>
    /// <param name="dragData">Set of drag docking data.</param>
    /// <param name="dragDropPalette">Palette source of drawing values.</param>
    /// <param name="feedback">Feedback requested.</param>
    public abstract void MeasureDragDropDockingGlyph(RenderDragDockingData dragData,
        IPaletteDragDrop dragDropPalette,
        PaletteDragFeedback feedback);

    /// <summary>
    /// Draw a solid area glyph suitable for a drag drop area.
    /// </summary>
    /// <param name="context">Render context.</param>
    /// <param name="dragData">Set of drag docking data.</param>
    /// <param name="dragDropPalette">Palette source of drawing values.</param>
    /// <param name="feedback">Feedback requested.</param>
    public abstract void DrawDragDropDockingGlyph(RenderContext context,
        RenderDragDockingData dragData,
        IPaletteDragDrop dragDropPalette,
        PaletteDragFeedback feedback);

    /// <summary>
    /// Draw the track bar ticks glyph.
    /// </summary>
    /// <param name="context">Render context.</param>
    /// <param name="state">Element state.</param>
    /// <param name="elementPalette">Source of palette colors.</param>
    /// <param name="drawRect">Drawing rectangle that should contain ticks.</param>
    /// <param name="orientation">Orientation of the drawing area.</param>
    /// <param name="topRight">Drawing on the topRight or the bottomLeft.</param>
    /// <param name="positionSize">Size of the position indicator.</param>
    /// <param name="minimum">First value.</param>
    /// <param name="maximum">Last value.</param>
    /// <param name="frequency">How often ticks are drawn.</param>
    public abstract void DrawTrackTicksGlyph(RenderContext context,
        PaletteState state,
        IPaletteElementColor elementPalette,
        Rectangle drawRect,
        Orientation orientation,
        bool topRight,
        Size positionSize,
        int minimum,
        int maximum,
        int frequency);

    /// <summary>
    /// Draw the track bar track glyph.
    /// </summary>
    /// <param name="context">Render context.</param>
    /// <param name="state">Element state.</param>
    /// <param name="elementPalette">Source of palette colors.</param>
    /// <param name="drawRect">Drawing rectangle that should contain the track.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="volumeControl">Drawing as a volume control or standard slider.</param>
    public abstract void DrawTrackGlyph(RenderContext context,
        PaletteState state,
        IPaletteElementColor elementPalette,
        Rectangle drawRect,
        Orientation orientation,
        bool volumeControl);

    /// <summary>
    /// Draw the track bar position glyph.
    /// </summary>
    /// <param name="context">Render context.</param>
    /// <param name="state">Element state.</param>
    /// <param name="elementPalette">Source of palette colors.</param>
    /// <param name="drawRect">Drawing rectangle that should contain the track.</param>
    /// <param name="orientation">Drawing orientation.</param>
    /// <param name="tickStyle">Tick marks that surround the position.</param>
    public abstract void DrawTrackPositionGlyph(RenderContext context,
        PaletteState state,
        IPaletteElementColor elementPalette,
        Rectangle drawRect,
        Orientation orientation,
        TickStyle tickStyle);
    #endregion

    #region EvalTransparentPaint
    /// <summary>
    /// Evaluate if transparent painting is needed for background palette.
    /// </summary>
    /// <param name="paletteBack">Background palette to test.</param>
    /// <param name="state">Element state associated with palette.</param>
    /// <returns>True if transparent painting required.</returns>
    public abstract bool EvalTransparentPaint(IPaletteBack paletteBack,
        PaletteState state);

    /// <summary>
    /// Evaluate if transparent painting is needed for background or border palettes.
    /// </summary>
    /// <param name="paletteBack">Background palette to test.</param>
    /// <param name="paletteBorder">Background palette to test.</param>
    /// <param name="state">Element state associated with palette.</param>
    /// <returns>True if transparent painting required.</returns>
    public abstract bool EvalTransparentPaint(IPaletteBack paletteBack,
        IPaletteBorder? paletteBorder,
        PaletteState state);
    #endregion

    #region DrawIconHelper

    /// <summary>
    /// Helper routine to draw an image taking into account various properties.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="icon">Icon to be drawn.</param>
    /// <param name="iconRect">Destination rectangle.</param>
    /// <param name="orientation">Visual orientation.</param>
    /// <exception cref="ArgumentNullException"></exception>
    protected static void DrawIconHelper([DisallowNull] ViewContext context,
        Icon icon,
        Rectangle iconRect,
        VisualOrientation orientation)
    {
        Debug.Assert(context != null);

        // Validate reference parameter
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // Finally, just draw the icon and let the transforms do the rest
        context.Graphics.DrawIcon(icon, iconRect);
    }
    #endregion

    #region DrawImageHelper

    /// <summary>
    /// Helper routine to draw an image taking into account various properties.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <param name="image">Image to be drawn.</param>
    /// <param name="remapTransparent">Color that should become transparent.</param>
    /// <param name="imageRect">Destination rectangle.</param>
    /// <param name="orientation">Visual orientation.</param>
    /// <param name="effect">Drawing effect.</param>
    /// <param name="remapColor">Image color to remap.</param>
    /// <param name="remapNew">New color for remap.</param>
    /// <exception cref="ArgumentNullException"></exception>
    protected static void DrawImageHelper([DisallowNull] ViewContext context,
        Image? image,
        Color remapTransparent,
        Rectangle imageRect,
        VisualOrientation orientation,
        PaletteImageEffect effect,
        Color remapColor,
        Color remapNew)
    {
        Debug.Assert(context != null);

        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (image == null)
        {
            return;
        }

        PaletteImageDrawing.Draw(
            context.Graphics,
            image,
            imageRect,
            orientation,
            effect,
            remapTransparent,
            remapColor,
            remapNew);
    }
    #endregion
}