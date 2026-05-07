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
/// Draws a radio button centered in the correct location.
/// </summary>
internal class ViewDrawRibbonGroupRadioButtonImage : ViewComposite
{
    #region Instance Fields
    private readonly Size _smallSize;// = new Size(16, 16);
    private readonly Size _largeSize;// = new Size(32, 32);
    private readonly KryptonRibbonGroupRadioButton _ribbonRadioButton;
    private readonly ViewDrawRadioButton _drawRadioButton;
    private readonly bool _large;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonGroupRadioButtonImage class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="ribbonRadioButton">Reference to ribbon group radio button definition.</param>
    /// <param name="large">Show the large image.</param>
    public ViewDrawRibbonGroupRadioButtonImage([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] KryptonRibbonGroupRadioButton? ribbonRadioButton,
        bool large)
    {
        Debug.Assert(ribbonRadioButton is not null);

        // Remember incoming parameters
        _ribbonRadioButton = ribbonRadioButton ?? throw new ArgumentNullException(nameof(ribbonRadioButton));
        _large = large;

        // Use redirector to get the radio button images and redirect to parent palette
        var redirectImages = new PaletteRedirectRadioButton(ribbon.GetRedirector(), ribbon.StateCommon.RibbonImages.RadioButton);

        // Create drawing element
        _drawRadioButton = new ViewDrawRadioButton(redirectImages);

        // Add as only child
        Add(_drawRadioButton);
        _smallSize = new Size((int)(16 * FactorDpiX), (int)(16 * FactorDpiY));
        _largeSize = new Size((int)(32 * FactorDpiX), (int)(32 * FactorDpiY));
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonGroupRadioButtonImage:{Id}";

    #endregion

    #region Enabled
    /// <summary>
    /// Gets and sets the enabled state of the element.
    /// </summary>
    public override bool Enabled
    {
        get => _drawRadioButton.Enabled;
        set => _drawRadioButton.Enabled = value;
    }
    #endregion

    #region Checked
    /// <summary>
    /// Gets and sets the checked state of the radio button.
    /// </summary>
    public bool Checked
    {
        get => _drawRadioButton.CheckState;
        set => _drawRadioButton.CheckState = value;
    }
    #endregion

    #region Tracking
    /// <summary>
    /// Gets and sets the tracking state of the radio button.
    /// </summary>
    public bool Tracking
    {
        get => _drawRadioButton.Tracking;
        set => _drawRadioButton.Tracking = value;
    }
    #endregion

    #region Pressed
    /// <summary>
    /// Gets and sets the pressed state of the radio button.
    /// </summary>
    public bool Pressed
    {
        get => _drawRadioButton.Pressed;
        set => _drawRadioButton.Pressed = value;
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context) => _large ? _largeSize : _smallSize;

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout(ViewLayoutContext context)
    {
        // Take on all the provided area
        ClientRectangle = context.DisplayRectangle;

        // Get the size of the radio button when it is drawn
        var radioButtonRect = new Rectangle(Point.Empty, _drawRadioButton.GetPreferredSize(context));

        // Decide on correct position within our rectangle
        if (_large)
        {
            // Place horizontal centered at the bottom of area
            radioButtonRect.X = ClientLocation.X + (ClientWidth - radioButtonRect.Width) / 2;
            radioButtonRect.Y = ClientRectangle.Bottom - radioButtonRect.Height;
        }
        else
        {
            // Place vertically centered at the right of area
            radioButtonRect.X = ClientRectangle.Right - radioButtonRect.Width;
            radioButtonRect.Y = ClientLocation.Y + (ClientHeight - radioButtonRect.Height) / 2;
        }

        // Layout the radio button draw element
        context.DisplayRectangle = radioButtonRect;
        _drawRadioButton.Layout(context);
        context.DisplayRectangle = ClientRectangle;
    }
    #endregion
}