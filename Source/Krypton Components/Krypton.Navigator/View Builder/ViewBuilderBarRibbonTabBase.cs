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

namespace Krypton.Navigator;

/// <summary>
/// Base class for implementation of 'Bar - RibbonTab' modes.
/// </summary>
internal abstract class ViewBuilderBarRibbonTabBase : ViewBuilderBarItemBase
{
    #region Protected
    /// <summary>
    /// Create a new check item with initial settings.
    /// </summary>
    /// <param name="page">Page for which the check button is to be created.</param>
    /// <param name="orientation">Initial orientation of the check button.</param>
    protected override INavCheckItem CreateCheckItem(KryptonPage? page,
        VisualOrientation orientation)
    {
        // Create a check button view element
        var ribbonTab = new ViewDrawNavRibbonTab(Navigator, page!);

        // Convert the button orientation to the appropriate visual orientations
        VisualOrientation orientBackBorder = ConvertButtonBorderBackOrientation();
        VisualOrientation orientContent = ConvertButtonContentOrientation();

        // Set the correct initial orientation of the button
        ribbonTab.SetOrientation(orientBackBorder, orientContent);

        return ribbonTab;
    }

    /// <summary>
    /// Gets the visual orientation of the check button.
    /// </summary>
    /// <returns>Visual orientation.</returns>
    protected override VisualOrientation ConvertButtonBorderBackOrientation()
    {
        switch (Navigator.Bar.BarOrientation)
        {
            case VisualOrientation.Top:
                return VisualOrientation.Top;

            case VisualOrientation.Bottom:
                return VisualOrientation.Bottom;

            case VisualOrientation.Left:
                if (CommonHelper.GetRightToLeftLayout(Navigator) &&
                    (Navigator.RightToLeft == RightToLeft.Yes))
                {
                    return VisualOrientation.Right;
                }
                else
                {
                    return VisualOrientation.Left;
                }

            case VisualOrientation.Right:
                if (CommonHelper.GetRightToLeftLayout(Navigator) &&
                    (Navigator.RightToLeft == RightToLeft.Yes))
                {
                    return VisualOrientation.Left;
                }
                else
                {
                    return VisualOrientation.Right;
                }

            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(Navigator.Bar.BarOrientation.ToString());
                return VisualOrientation.Top;
        }
    }

    /// <summary>
    /// Gets the visual orientation of the check buttons content.
    /// </summary>
    /// <returns>Visual orientation.</returns>
    protected override VisualOrientation ConvertButtonContentOrientation() => ResolveButtonContentOrientation(Navigator.Bar.BarOrientation);

    #endregion
}