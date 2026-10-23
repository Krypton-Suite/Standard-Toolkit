#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Draws a dialog box launcher button for a group.
/// </summary>
internal class ViewDrawRibbonGroupDialogButton : ViewLeaf
{
    #region Instance Fields
    // Button is 8 for context image, 4 for context padding and 2 for border drawing
    private readonly Size _viewSize; // = new(14, 14);
    // Inflate size to convert from view size to content size
    private readonly Size _contentSize; // = new(-3, -3);
    private readonly KryptonRibbon _ribbon;
    private readonly KryptonRibbonGroup _ribbonGroup;
    private IDisposable? _mementoBack;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonGroupDialogButton class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="ribbonGroup">Reference to ribbon group this represents.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ViewDrawRibbonGroupDialogButton([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] KryptonRibbonGroup? ribbonGroup,
        NeedPaintHandler? needPaint)
    {
        Debug.Assert(ribbon is not null);
        Debug.Assert(ribbonGroup is not null);

        // Remember incoming references
        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
        _ribbonGroup = ribbonGroup ?? throw new ArgumentNullException(nameof(ribbonGroup));

        // Attach a controller to this element for the pressing of the button
        var controller = new DialogLauncherButtonController(ribbon, this, needPaint!);
        controller.Click += OnClick;
        MouseController = controller;
        SourceController = controller;
        KeyController = controller;
        // Button is 8 for context image, 4 for context padding and 2 for border drawing
        _viewSize = new Size((int)(14 * FactorDpiX), (int)(14 * FactorDpiY));
        // Inflate size to convert from view size to content size
        _contentSize = new Size((int)(-3 * FactorDpiX), (int)(-3 * FactorDpiY));
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonGroupButton:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_mementoBack != null)
            {
                _mementoBack.Dispose();
                _mementoBack = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region DialogButtonController
    /// <summary>
    /// Gets access to the controller used for the button.
    /// </summary>
    public DialogLauncherButtonController? DialogButtonController => SourceController as DialogLauncherButtonController;

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context) => _viewSize;

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore([DisallowNull] RenderContext context)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        IPaletteBack paletteBack = _ribbon.StateCommon.RibbonGroupDialogButton.PaletteBack;
        IPaletteBorder paletteBorder = _ribbon.StateCommon.RibbonGroupDialogButton.PaletteBorder!;
        IPaletteRibbonGeneral paletteGeneral = _ribbon.StateCommon.RibbonGeneral;

        // Do we need to draw the background?
        if (paletteBack.GetBackDraw(State) == InheritBool.True)
        {
            // Get the border path which the background is clipped to drawing within
            using GraphicsPath borderPath = context.Renderer.RenderStandardBorder.GetBackPath(context, ClientRectangle, paletteBorder, VisualOrientation.Top, State);
            Padding borderPadding = context.Renderer.RenderStandardBorder.GetBorderRawPadding(paletteBorder, State, VisualOrientation.Top);

            // Apply the padding depending on the orientation
            Rectangle enclosingRect = CommonHelper.ApplyPadding(VisualOrientation.Top, ClientRectangle, borderPadding);

            // Render the background inside the border path
            using var gh = new GraphicsHint(context.Graphics, paletteBorder.GetBorderGraphicsHint(PaletteState.Normal));
            _mementoBack = context.Renderer.RenderStandardBack.DrawBack(context, enclosingRect, borderPath,
                paletteBack, VisualOrientation.Top,
                State, _mementoBack);
        }

        // Do we need to draw the border?
        if (paletteBorder.GetBorderDraw(State) == InheritBool.True)
        {
            context.Renderer.RenderStandardBorder.DrawBorder(context, ClientRectangle, paletteBorder,
                VisualOrientation.Top, State);
        }

        // Find the content area inside the button rectangle
        Rectangle contentRect = ClientRectangle;
        contentRect.Inflate(_contentSize);

        // Draw the dialog box launcher glyph in the center
        context.Renderer.RenderGlyph.DrawRibbonDialogBoxLauncher(_ribbon.RibbonShape, context, contentRect, paletteGeneral, State);
    }
    #endregion

    #region Implementation
    private void OnClick(object? sender, MouseEventArgs e)
    {
        // We do not operate the dialog launcher at design time
        if (!_ribbon.InDesignMode)
        {
            // Fire the group defined event that indicates dialog box launcher button pressed
            _ribbonGroup.OnDialogBoxLauncherClick(EventArgs.Empty);
        }
    }
    #endregion
}