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
/// Draw today's date as a button.
/// </summary>
public class ViewDrawToday : ViewDrawButton,
    IContentValues
{
    #region Events
    /// <summary>
    /// Occurs when the today button is clicked.
    /// </summary>
    public event EventHandler? Click;
    #endregion

    #region Instance Fields
    private readonly IKryptonMonthCalendar _calendar;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawToday class.
    /// </summary>
    /// <param name="calendar">Provider of month calendar values.</param>
    /// <param name="paletteDisabled">Palette source for the disabled state.</param>
    /// <param name="paletteNormal">Palette source for the normal state.</param>
    /// <param name="paletteTracking">Palette source for the tracking state.</param>
    /// <param name="palettePressed">Palette source for the pressed state.</param>
    /// <param name="needPaintHandler">Delegate for requested repainting.</param>
    public ViewDrawToday(IKryptonMonthCalendar calendar,
        IPaletteTriple paletteDisabled,
        IPaletteTriple paletteNormal,
        IPaletteTriple paletteTracking,
        IPaletteTriple palettePressed,
        NeedPaintHandler needPaintHandler)
        : base(paletteDisabled, paletteNormal, paletteTracking, palettePressed,
            paletteNormal, paletteTracking, palettePressed, null!,
            null, VisualOrientation.Top, false)
    {
        _calendar = calendar;

        // We provide the content values for display
        ButtonValues = this;

        // Define a controller so the button can be clicked
        var controller = new ButtonController(this, needPaintHandler);
        controller.Click += OnClick;
        MouseController = controller;
        SourceController = controller;
        KeyController = controller;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewDrawToday:{Id}";

    #endregion

    #region IContentValues
    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    public Image? GetImage(PaletteState state) => null;

    /// <summary>
    /// Gets the image color that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Color value.</returns>
    public Color GetImageTransparentColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the content short text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetShortText() =>
        $"{KryptonManager.Strings.GeneralStrings.Today} {_calendar.TodayDate.ToString(_calendar.TodayFormat)}";

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetLongText() => string.Empty;

    #endregion

    #region Implementation
    private void OnClick(object? sender, MouseEventArgs e) => Click?.Invoke(this, EventArgs.Empty);

    #endregion

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);
        base.Layout(context!);
    }
}