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
/// Set of calculated values for use when laying out view elements.
/// </summary>
internal class CalculatedValues
{
    #region Static Fields

    private const int FONT_HEIGHT_EXTRA = 2; // Always need to add 2 pixels for it to draw
    private const int DIALOG_MIN_HEIGHT = 14; // Button image = 8 + 2 content gap + 2 for button border + 2 for outside gap
    private const int GROUP_LINE_CONTENT_MIN = 18; // Small image = 16 + 2 content gap
    private const int GROUP_LINE_CONTENT_EXTRA = 4; // 2 content gap + 2 for button border
    private const int GROUP_INSIDE_BOTTOM_GAP = 1; // 1 pixel between last group line and the group title
    private const int GROUP_TOP_BORDER = 2; // 2 pixel border for top edge of a group
    private const int GROUPS_TOP_GAP = 3; // Space between top of a group and inside edge of borders area
    private const int GROUPS_BOTTOM_GAP = 2; // Space between bottom of group and bottom of the borders area
    private const int TABS_TOP_GAP = 5; // 4 padding at top of tab text and 1 extra for the bottom
    private const int KEYTIP_HOFFSET = 16; // Horizontal distance to offset keytips for group items
    private const int KEYTIP_VOFFSET_LINE2 = 1; // Vertical distance to offset keytips on group line 2
    private const int KEYTIP_VOFFSET_LINE4 = 8; // Vertical distance to offset keytips on group line 4
    private const int KEYTIP_VOFFSET_LINE5 = 8; // Vertical distance to offset keytips on group line 5

    #endregion

    #region Instance Fields
    private PaletteRibbonShape _lastShape;
    private readonly KryptonRibbon _ribbon;
    private int _groupHeightModifier;
    private int _groupsHeightModifier;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the CalculatedValues class.
    /// </summary>
    /// <param name="ribbon">Source control instance.</param>
    public CalculatedValues([DisallowNull] KryptonRibbon? ribbon)
    {
        Debug.Assert(ribbon is not null);
        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));

        _lastShape = PaletteRibbonShape.Inherit;
    }
    #endregion

    #region Recalc
    /// <summary>
    /// Recalculate all the values.
    /// </summary>
    public void Recalculate()
    {
        // Do we need to update the shape dependant values?
        if (_lastShape != _ribbon.RibbonShape)
        {
            _lastShape = _ribbon.RibbonShape;
            switch (_lastShape)
            {
                default:
                case PaletteRibbonShape.Office2007:
                    _groupHeightModifier = 0;
                    _groupsHeightModifier = 0;
                    break;
                case PaletteRibbonShape.Office2013:
                case PaletteRibbonShape.Microsoft365:
                case PaletteRibbonShape.VisualStudio:
                case PaletteRibbonShape.Office2010:
                case PaletteRibbonShape.VisualStudio2010:
                    _groupHeightModifier = -3;
                    _groupsHeightModifier = -3;
                    break;
            }
        }

        // Get the font used by the ribbon
        Font font = _ribbon.StateCommon.RibbonGeneral.GetRibbonTextFont(PaletteState.Normal);

        // Cache common font height related values
        RawFontHeight = font.Height;

        DrawFontHeight = RawFontHeight + FONT_HEIGHT_EXTRA;

        // Height of all tabs in the tabs area
        TabHeight = DrawFontHeight + TABS_TOP_GAP;

        // Find the height of the group title area (must be minimum size to show the dialog launcher button)
        GroupTitleHeight = Math.Max(DrawFontHeight, DIALOG_MIN_HEIGHT);

        // Get the height needed for showing the content of a group line
        GroupLineContentHeight = Math.Max(DrawFontHeight, GROUP_LINE_CONTENT_MIN);

        // Group line height must be the content plus spacing gap and then border
        GroupLineHeight = GroupLineContentHeight + GROUP_LINE_CONTENT_EXTRA;

        // Group inside height is 3 group lines plus space at bottom of the lines
        GroupTripleHeight = GroupLineHeight * 3;

        // The gap between lines is one of the lines divide by a gap above, between and below lines
        GroupLineGapHeight = GroupLineHeight / 3;

        // Group height is the inside plus title area at bottom and the top border
        GroupHeight = GroupTripleHeight + GROUP_INSIDE_BOTTOM_GAP + GroupTitleHeight + GROUP_TOP_BORDER;

        // Size of the groups area (not including the top pixel that is placed in the tabs
        // area is the height of a group plus the bottom and top gaps).
        GroupsHeight = GroupHeight + GROUPS_BOTTOM_GAP + GROUPS_TOP_GAP;

        // Apply shape specific modifiers
        GroupHeight += _groupHeightModifier;
        GroupsHeight += _groupsHeightModifier;
    }
    #endregion

    #region RawFontHeight
    /// <summary>
    /// Gets the raw height of the ribbon font.
    /// </summary>
    public int RawFontHeight { get; private set; }

    #endregion

    #region DrawFontHeight
    /// <summary>
    /// Gets the drawing height of the ribbon font.
    /// </summary>
    public int DrawFontHeight { get; private set; }

    #endregion

    #region TabHeight
    /// <summary>
    /// Gets the drawing height of a tab.
    /// </summary>
    public int TabHeight { get; private set; }

    #endregion

    #region GroupTitleHeight
    /// <summary>
    /// Gets the drawing height of the ribbon font.
    /// </summary>
    public int GroupTitleHeight { get; private set; }

    #endregion

    #region GroupLineContentHeight
    /// <summary>
    /// Gets the drawing height of the content for a group line.
    /// </summary>
    public int GroupLineContentHeight { get; private set; }

    #endregion

    #region GroupLineHeight
    /// <summary>
    /// Gets the drawing height of one of the three group lines.
    /// </summary>
    public int GroupLineHeight { get; private set; }

    #endregion

    #region GroupLineGapHeight
    /// <summary>
    /// Gets the spacing height between two group lines.
    /// </summary>
    public int GroupLineGapHeight { get; private set; }

    #endregion

    #region GroupTripleHeight
    /// <summary>
    /// Gets the height of the triple height item.
    /// </summary>
    public int GroupTripleHeight { get; private set; }

    #endregion

    #region GroupHeight
    /// <summary>
    /// Gets the drawing height of a group.
    /// </summary>
    public int GroupHeight { get; private set; }

    #endregion

    #region GroupsHeight
    /// <summary>
    /// Gets the drawing height of the entire groups area not including top pixel line.
    /// </summary>
    public int GroupsHeight { get; private set; }

    #endregion

    #region KeyTipRectToPoint
    /// <summary>
    /// Find the correct screen point for a key tip given a rectangle and its group line.
    /// </summary>
    /// <param name="viewRect">Screen rectangle of the view element.</param>
    /// <param name="groupLine">Group line the view is positioned on.</param>
    /// <returns>Screen point that is the center of the key tip.</returns>
    public Point KeyTipRectToPoint(Rectangle viewRect, int groupLine)
    {
        Point screenPt;

        switch (groupLine)
        {
            case 1:
                screenPt = new Point(viewRect.Left + KEYTIP_HOFFSET, viewRect.Top);
                break;

            case 2:
                screenPt = new Point(viewRect.Left + KEYTIP_HOFFSET, viewRect.Top + (viewRect.Height / 2) + KEYTIP_VOFFSET_LINE2);
                break;

            case 3:
                screenPt = new Point(viewRect.Left + KEYTIP_HOFFSET, viewRect.Bottom);
                break;

            case 4:
                screenPt = new Point(viewRect.Left + KEYTIP_HOFFSET, viewRect.Top - KEYTIP_VOFFSET_LINE4);
                break;

            case 5:
                screenPt = new Point(viewRect.Left + KEYTIP_HOFFSET, viewRect.Bottom + KEYTIP_VOFFSET_LINE5);
                break;

            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(groupLine.ToString());
                screenPt = new Point(viewRect.X, viewRect.Y);
                break;
        }

        return screenPt;
    }
    #endregion
}