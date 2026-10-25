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
/// Layout a scroller button with appropriate separator space around it.
/// </summary>
internal class ViewLayoutRibbonScroller : ViewComposite
{
    #region Static Fields

    private const int SCROLLER_LENGTH = 12;
    private const int GAP_LENGTH = 2;

    #endregion

    #region Instance Fields
    private VisualOrientation _orientation;
    private readonly ViewDrawRibbonScrollButton _button;
    private readonly ViewLayoutRibbonSeparator _separator;
    private readonly bool _insetForTabs;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when the button has been clicked.
    /// </summary>
    public event EventHandler? Click;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutRibbonScroller class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="orientation">Scroller orientation.</param>
    /// <param name="insetForTabs">Should scoller be inset for use in tabs area.</param>
    /// <param name="needPaintDelegate">Delegate for notifying paint/layout requests.</param>
    public ViewLayoutRibbonScroller(KryptonRibbon ribbon,
        VisualOrientation orientation,
        bool insetForTabs,
        NeedPaintHandler needPaintDelegate)
    {
        // Cache provided values
        _orientation = orientation;
        _insetForTabs = insetForTabs;

        // Create the button and the separator
        _button = new ViewDrawRibbonScrollButton(ribbon, orientation);
        _separator = new ViewLayoutRibbonSeparator(GAP_LENGTH, true);

        // Create button controller for clicking the button
        var rbc = new RepeatButtonController(ribbon, _button, needPaintDelegate);
        rbc.Click += OnButtonClick;
        _button.MouseController = rbc;

        // Add as child elements
        Add(_button);
        Add(_separator);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutRibbonScroller:{Id}";

    #endregion

    #region Orientation
    /// <summary>
    /// Gets and sets the visual orientation of the scroller button.
    /// </summary>
    public VisualOrientation Orientation
    {
        get => _orientation;

        set 
        { 
            _orientation = value;
            _button.Orientation = value;
        }
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context) =>
        // Always return the same minimum size
        new Size(SCROLLER_LENGTH + GAP_LENGTH, SCROLLER_LENGTH + GAP_LENGTH);

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;

        // Layout depends on orientation
        switch (Orientation)
        {
            case VisualOrientation.Top:
                context.DisplayRectangle = new Rectangle(ClientLocation.X, ClientRectangle.Bottom - GAP_LENGTH, ClientWidth, GAP_LENGTH);
                _separator.Layout(context);
                context.DisplayRectangle = new Rectangle(ClientLocation.X, ClientLocation.Y, ClientWidth, ClientHeight - GAP_LENGTH);
                _button.Layout(context);
                break;
            case VisualOrientation.Bottom:
                context.DisplayRectangle = new Rectangle(ClientLocation.X, ClientRectangle.Y, ClientWidth, GAP_LENGTH);
                _separator.Layout(context);
                context.DisplayRectangle = new Rectangle(ClientLocation.X, ClientLocation.Y + GAP_LENGTH, ClientWidth, ClientHeight - GAP_LENGTH);
                _button.Layout(context);
                break;
            case VisualOrientation.Left:
                if (_insetForTabs)
                {
                    ClientRectangle = AdjustRectForTabs(ClientRectangle);
                }

                context.DisplayRectangle = new Rectangle(ClientRectangle.Right - GAP_LENGTH, ClientLocation.Y, GAP_LENGTH, ClientHeight);
                _separator.Layout(context);
                context.DisplayRectangle = new Rectangle(ClientLocation.X, ClientLocation.Y, ClientWidth - GAP_LENGTH, ClientHeight);
                _button.Layout(context);
                break;
            case VisualOrientation.Right:
                if (_insetForTabs)
                {
                    ClientRectangle = AdjustRectForTabs(ClientRectangle);
                }

                context.DisplayRectangle = new Rectangle(ClientLocation.X, ClientLocation.Y, GAP_LENGTH, ClientHeight);
                _separator.Layout(context);
                context.DisplayRectangle = new Rectangle(ClientLocation.X + GAP_LENGTH, ClientLocation.Y, ClientWidth - GAP_LENGTH, ClientHeight);
                _button.Layout(context);
                break;
        }

        // Put back the original display rectangle
        context.DisplayRectangle = ClientRectangle;
    }
    #endregion
        
    #region Implementation
    private Rectangle AdjustRectForTabs(Rectangle rect)
    {
        rect.Y++;
        rect.Height -= 3;
        return rect;
    }

    private void OnButtonClick(object? sender, MouseEventArgs e) => Click?.Invoke(this, EventArgs.Empty);
    #endregion
}