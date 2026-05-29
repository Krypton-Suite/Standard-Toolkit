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
/// Draws a quick access toolbar button based on a IQuickAccessToolbarButton source.
/// </summary>
internal class ViewDrawRibbonQATButton : ViewComposite,
    IContentValues
{

    #region Instance Fields
    private readonly Size _viewSize;
    private readonly KryptonRibbon _ribbon;
    private readonly QATButtonToContent _contentProvider;
    private readonly ViewDrawContent _drawContent;
    private IDisposable? _mementoBack;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonQATButton class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="qatButton">Reference to button definition.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ViewDrawRibbonQATButton([DisallowNull] KryptonRibbon ribbon,
        [DisallowNull] IQuickAccessToolbarButton qatButton,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(ribbon != null);
        Debug.Assert(qatButton != null);

        // Remember incoming references
        _ribbon = ribbon!;
        QATButton = qatButton!;

        _viewSize = new Size((int)(22 * FactorDpiX), (int)(22*FactorDpiY));

        // If the source interface comes from a component then allow it to 
        // be selected at design time by clicking on the view instance
        Component = qatButton as Component;

        // Attach a controller to this element for the pressing of the button
        var controller = new QATButtonController(ribbon!, this, needPaint);
        controller.Click += OnClick;
        SourceController = controller;
        KeyController = controller;

        // Create controller for intercepting events to determine tool tip handling
        MouseController = new ToolTipController(_ribbon.TabsArea?.ButtonSpecManager?.ToolTipManager!,
            this, controller);


        // Use a class to convert from ribbon tab to content interface
        _contentProvider = new QATButtonToContent(qatButton!);

        // Create and add the draw content for display inside the button
        _drawContent = new ViewDrawContent(_contentProvider, this, VisualOrientation.Top);
        Add(_drawContent);

        // Need to notice when the ribbon enable state changes
        _ribbon.EnabledChanged += OnRibbonEnableChanged;

        // Set the initial enabled state
        UpdateEnabled();
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonQATButton:{Id}";

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

    #region KeyTipTarget
    /// <summary>
    /// Gets the key tip target for this view.
    /// </summary>
    public IRibbonKeyTipTarget? KeyTipTarget => SourceController as IRibbonKeyTipTarget;

    #endregion

    #region QATButton
    /// <summary>
    /// Gets access to the source button this view represents.
    /// </summary>
    public IQuickAccessToolbarButton QATButton { get; }

    #endregion

    #region Enabled
    /// <summary>
    /// Gets and sets the enabled state of the element.
    /// </summary>
    public override bool Enabled
    {
        get => base.Enabled && _ribbon.Enabled;

        set
        {
            base.Enabled = value;
            UpdateEnabled();
        }
    }
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

        // Let child elements layout in given space
        base.Layout(context);
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore(RenderContext context) 
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // Make sure we reflect the current enabled state
        if (!Enabled && _ribbon.InDesignHelperMode)
        {
            ElementState = PaletteState.Disabled;
        }

        IPaletteBack paletteBack = _ribbon.StateCommon.RibbonQATButton.PaletteBack;
        IPaletteBorder? paletteBorder = _ribbon.StateCommon.RibbonQATButton.PaletteBorder;

        // Do we need to draw the background?
        if (paletteBack.GetBackDraw(State) == InheritBool.True)
        {
            // Get the border path which the background is clipped to drawing within
            using GraphicsPath borderPath = context.Renderer.RenderStandardBorder.GetBackPath(context, ClientRectangle, paletteBorder!, VisualOrientation.Top, State);
            Padding borderPadding = context.Renderer.RenderStandardBorder.GetBorderRawPadding(paletteBorder!, State, VisualOrientation.Top);

            // Apply the padding depending on the orientation
            Rectangle enclosingRect = CommonHelper.ApplyPadding(VisualOrientation.Top, ClientRectangle, borderPadding);

            // Render the background inside the border path
            using var gh = new GraphicsHint(context.Graphics, paletteBorder!.GetBorderGraphicsHint(PaletteState.Normal));
            _mementoBack = context.Renderer.RenderStandardBack.DrawBack(context, enclosingRect, borderPath,
                paletteBack, VisualOrientation.Top,
                State, _mementoBack);
        }

        // Do we need to draw the border?
        if (paletteBorder?.GetBorderDraw(State) == InheritBool.True)
        {
            context.Renderer.RenderStandardBorder.DrawBorder(context, ClientRectangle, paletteBorder, 
                VisualOrientation.Top, State);
        }

        base.RenderBefore(context);
    }
    #endregion

    #region Implementation
    private void OnRibbonEnableChanged(object? sender, EventArgs e) => UpdateEnabled();

    private void UpdateEnabled() =>
        // Content is only enabled if the QAT button is enabled
        // and the owning ribbon control is also enabled
        _drawContent.Enabled = base.Enabled && _ribbon.Enabled;

    private void OnClick(object? sender, MouseEventArgs e)
    {
        // We do not operate the qat button at design time
        if (!_ribbon.InDesignMode)
        {
            Form? ownerForm = _ribbon.FindForm();

            // Ensure the form we are inside is active
            ownerForm?.Activate();

            // Inform quick access toolbar button it has been clicked
            QATButton.PerformClick();
        }
    }
    #endregion

    #region IContentValues

    private Image? _cachedImage;
    /// <summary>
    /// Gets the image used for the ribbon tab.
    /// </summary>
    /// <param name="state">Tab state.</param>
    /// <returns>Image.</returns>
    public Image? GetImage(PaletteState state)
    {
        if (_cachedImage == null)
        {
            var sourceImage = QATButton.GetImage();
            var currentWidth = sourceImage!.Width * FactorDpiX;
            var currentHeight = sourceImage.Height * FactorDpiY;
            /*if ((int)currentHeight == sourceImage.Height)
            {
                // Need to work around the image drawing off the bottom of the form title bar when scaling @ 100%
                currentHeight -= 2; // Has to be even to ensure that horizontal lines are still drawn.
            }
            */

            _cachedImage = CommonHelper.ScaleImageForSizedDisplay(sourceImage, currentWidth, currentHeight, false);
        }

        return _cachedImage;
    }

    /// <summary>
    /// Gets the image color that should be interpreted as transparent.
    /// </summary>
    /// <param name="state">Tab state.</param>
    /// <returns>Transparent Color.</returns>
    public Color GetImageTransparentColor(PaletteState state) => Color.Empty;

    /// <summary>
    /// Gets the short text used as the main ribbon title.
    /// </summary>
    /// <returns>Title string.</returns>
    public string GetShortText() => string.Empty;

    /// <summary>
    /// Gets the long text used as the secondary ribbon title.
    /// </summary>
    /// <returns>Title string.</returns>
    public string GetLongText() => string.Empty;

    #endregion
}