#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Shared helpers for Office-style logical RTL packing in ribbon view layouts.
/// </summary>
/// <remarks>
/// Uses the same two-flag contract as <see cref="KryptonForm"/>: both
/// <see cref="Control.RightToLeft"/> and a bool layout flag must be on.
/// <see cref="KryptonRibbon"/> inherits <see cref="VisualSimple"/>, so it exposes
/// its own layout flag and copies it from the parent form. Does not mirror glyphs.
/// </remarks>
internal static class RibbonRtlLayout
{
    /// <summary>
    /// Gets whether the ribbon should pack and dock as right-to-left.
    /// </summary>
    /// <param name="ribbon">Owning ribbon.</param>
    /// <returns>True when both RTL flags are set.</returns>
    public static bool IsRtl(KryptonRibbon? ribbon) =>
        ribbon != null && CommonHelper.IsRightToLeftLayout(ribbon);

    /// <summary>
    /// Gets whether the layout context's control should pack as right-to-left.
    /// </summary>
    /// <param name="context">Layout context.</param>
    /// <returns>True when both RTL flags are set on the context control.</returns>
    public static bool IsRtl(ViewLayoutContext? context) =>
        context != null && context.IsRightToLeftLayout;

    /// <summary>
    /// Gets the running X for the first packed item.
    /// </summary>
    /// <param name="client">Container rectangle.</param>
    /// <param name="isRtl">True to pack from the right edge.</param>
    /// <returns>Left edge for LTR; right edge for RTL.</returns>
    public static int StartX(Rectangle client, bool isRtl) => isRtl ? client.Right : client.X;

    /// <summary>
    /// Gets the start-side window border width (left in LTR, right in RTL).
    /// </summary>
    /// <param name="borders">Window borders.</param>
    /// <param name="isRtl">True when RTL layout is active.</param>
    /// <returns>Border thickness on the reading-order start edge.</returns>
    public static int StartBorderWidth(Padding borders, bool isRtl) => isRtl ? borders.Right : borders.Left;

    /// <summary>
    /// Places the next packed item and advances the running X.
    /// </summary>
    /// <param name="x">Running X; updated to the next slot.</param>
    /// <param name="y">Top of the item.</param>
    /// <param name="width">Item width.</param>
    /// <param name="height">Item height.</param>
    /// <param name="isRtl">True to pack toward the left.</param>
    /// <param name="gap">Extra gap after the item along the packing direction.</param>
    /// <returns>Rectangle for the item.</returns>
    public static Rectangle NextItem(ref int x, int y, int width, int height, bool isRtl, int gap = 0)
    {
        Rectangle rect;
        if (isRtl)
        {
            x -= width;
            rect = new Rectangle(x, y, width, height);
            x -= gap;
        }
        else
        {
            rect = new Rectangle(x, y, width, height);
            x += width + gap;
        }

        return rect;
    }

    /// <summary>
    /// Gets the client rectangle covering packed items after a sequential walk.
    /// </summary>
    /// <param name="origin">Original client rectangle before packing.</param>
    /// <param name="x">Running X after packing.</param>
    /// <param name="isRtl">True when packing was from the right.</param>
    /// <returns>Tight rectangle over the packed children.</returns>
    public static Rectangle PackedBounds(Rectangle origin, int x, bool isRtl)
    {
        var usedWidth = isRtl ? origin.Right - x : x - origin.X;
        var locX = isRtl ? x : origin.X;
        return new Rectangle(locX, origin.Y, usedWidth, origin.Height);
    }

    /// <summary>
    /// Remainder rectangle on the far side of packed items (spare caption / unused strip).
    /// </summary>
    /// <param name="origin">Original full client rectangle.</param>
    /// <param name="x">Running X after packing.</param>
    /// <param name="isRtl">True when packing was from the right.</param>
    /// <returns>Empty if there is no remainder; otherwise the unused strip.</returns>
    public static Rectangle FarRemainder(Rectangle origin, int x, bool isRtl)
    {
        if (isRtl)
        {
            return x > origin.Left
                ? new Rectangle(origin.Left, origin.Y, x - origin.Left, origin.Height)
                : Rectangle.Empty;
        }

        return x < origin.Right
            ? new Rectangle(x, origin.Y, origin.Right - x, origin.Height)
            : Rectangle.Empty;
    }

    /// <summary>
    /// Remaps Left/Right key data so existing LTR switch arms follow reading order under RTL.
    /// </summary>
    /// <param name="keyData">Original key data.</param>
    /// <param name="isRtl">True when RTL layout is active.</param>
    /// <returns>Swapped Left/Right when RTL; otherwise unchanged.</returns>
    public static Keys HorizontalKey(Keys keyData, bool isRtl)
    {
        if (!isRtl)
        {
            return keyData;
        }

        var code = keyData & Keys.KeyCode;
        if (code == Keys.Left)
        {
            return (keyData & ~Keys.KeyCode) | Keys.Right;
        }

        return code == Keys.Right
            ? (keyData & ~Keys.KeyCode) | Keys.Left
            : keyData;
    }

    /// <summary>
    /// Copies <see cref="Control.RightToLeft"/> and layout mirroring onto a popup or hosted control.
    /// </summary>
    /// <param name="control">Target control.</param>
    /// <param name="ribbon">Source ribbon.</param>
    public static void ApplyTo(Control? control, KryptonRibbon? ribbon)
    {
        if (control == null || ribbon == null)
        {
            return;
        }

        control.RightToLeft = ribbon.RightToLeft;
        var layout = ribbon.RightToLeftLayout;
        switch (control)
        {
            case VisualSimpleBase visual:
                visual.RightToLeftLayout = layout;
                break;
            case VisualPopup popup:
                popup.RightToLeftLayout = layout;
                break;
            case Form form:
                form.RightToLeftLayout = layout;
                break;
            case KryptonRibbon otherRibbon:
                otherRibbon.RightToLeftLayout = layout;
                break;
        }
    }
}
