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
/// Layout area for the application button.
/// </summary>
internal class ViewLayoutRibbonAppButton : ViewLayoutDocker
{
    #region Static Fields

    private const int APPBUTTON_WIDTH = 39;
    private const int APPBUTTON_GAP = 4;

    #endregion

    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private readonly ViewLayoutRibbonSeparator _separator;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutRibbonAppButton class.
    /// </summary>
    /// <param name="ribbon">Owning control instance.</param>
    /// <param name="bottomHalf">Scroller orientation.</param>
    public ViewLayoutRibbonAppButton([DisallowNull] KryptonRibbon? ribbon,
        bool bottomHalf)
    {
        Debug.Assert(ribbon is not null);
        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));

        AppButton = new ViewDrawRibbonAppButton(ribbon, bottomHalf);
        _separator = new ViewLayoutRibbonSeparator(APPBUTTON_GAP, true);

        // Dock it against the appropriate edge
        Add(AppButton, bottomHalf ? ViewDockStyle.Top : ViewDockStyle.Bottom);

        // Place a separator between edge of control and start of the app button
        Add(_separator, ViewDockStyle.Left);

        // Use filler placeholder to force size to that required
        Add(new ViewLayoutRibbonSeparator(APPBUTTON_WIDTH, APPBUTTON_GAP, true), ViewDockStyle.Fill);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutRibbonAppButton:{Id}";

    #endregion

    #region OwnerForm
    /// <summary>
    /// Gets and sets the owning form instance.
    /// </summary>
    public KryptonForm? OwnerForm { get; set; }

    #endregion

    #region Visible
    /// <summary>
    /// Gets and sets the visible state of the element.
    /// </summary>
    public override bool Visible
    {
        get 
        {
            if (OwnerForm == null)
            {
                return base.Visible;
            }
            else
            {
                return _ribbon.Visible && base.Visible;
            }
        }

        set => base.Visible = value;
    }
    #endregion

    #region AppButton
    /// <summary>
    /// Gets the view element that represents the button.
    /// </summary>
    public ViewDrawRibbonAppButton AppButton { get; }

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        UpdateSeparatorSize();
        return base.GetPreferredSize(context);
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout(ViewLayoutContext context)
    {
        UpdateSeparatorSize();
        base.Layout(context);
    }
    #endregion

    #region Implementation
    private void UpdateSeparatorSize()
    {
        var separatorSize = new Size(APPBUTTON_GAP, APPBUTTON_GAP);

        // Do we need to add on extra sizing to the separator?
        if (OwnerForm != null)
        {
            // Get the actual owning window border settings
            Padding borders = OwnerForm.RealWindowBorders;

            // Add the left border side to the sizing
            separatorSize.Width += borders.Left;
        }

        _separator.SeparatorSize = separatorSize;
    }
    #endregion
}