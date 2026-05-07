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
/// Positions a separator to take up space without drawing.
/// </summary>
public class ViewLayoutMenuSepGap : ViewLayoutSeparator
{
    #region Instance Fields
    private readonly PaletteContextMenuRedirect _stateCommon;
    private readonly bool _standardStyle;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutMenuSepGap class.
    /// </summary>
    /// <param name="stateCommon">Source of palette values.</param>
    /// <param name="standardStyle">Draw items with standard or alternate style.</param>
    public ViewLayoutMenuSepGap(PaletteContextMenuRedirect stateCommon,
        bool standardStyle)
        : base(0)
    {
        _stateCommon = stateCommon;
        _standardStyle = standardStyle;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutMenuSepGap:{Id}";

    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // Grab the padding used for the text/extra content of a menu item
        Padding paddingText = _standardStyle
            ? _stateCommon.ItemTextStandard.GetBorderContentPadding(context.Control as KryptonForm, PaletteState.Normal)
            : _stateCommon.ItemTextAlternate.GetBorderContentPadding(context.Control as KryptonForm, PaletteState.Normal);

        // Get padding needed for the left edge of the item highlight
        Padding paddingHighlight = context.Renderer.RenderStandardBorder.GetBorderDisplayPadding(_stateCommon.ItemHighlight?.Border!, PaletteState.Normal, VisualOrientation.Top);

        // Our separator size is the left padding values added together
        SeparatorSize = new Size(paddingHighlight.Left + paddingText.Left, 0);

        return base.GetPreferredSize(context);
    }
    #endregion
}