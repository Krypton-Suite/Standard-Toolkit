#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Shared helpers for Toolkit logical RTL layout.
/// </summary>
/// <remarks>
/// Uses the same two-flag contract as <see cref="KryptonForm"/>: both
/// <see cref="Control.RightToLeft"/> and a bool layout flag must be on.
/// Does not mirror glyphs. Docker chrome should either rely on
/// <see cref="ViewLayoutDocker.CalculateDock"/> / <see cref="ViewDrawDocker.CalculateDock"/>
/// or set <c>IgnoreRightToLeftLayout</c> and assign docks itself — never both.
/// </remarks>
internal static class ToolkitRtlLayout
{
    /// <summary>
    /// Gets whether the control should pack and dock as right-to-left.
    /// </summary>
    /// <param name="control">Owning control.</param>
    /// <returns>True when both RTL flags are set.</returns>
    public static bool IsRtl(Control? control) => CommonHelper.IsRightToLeftLayout(control);

    /// <summary>
    /// Gets whether the layout context's control should pack as right-to-left.
    /// </summary>
    /// <param name="context">Layout context.</param>
    /// <returns>True when both RTL flags are set on the context control.</returns>
    public static bool IsRtl(ViewLayoutContext? context) =>
        context != null && context.IsRightToLeftLayout;

    /// <summary>
    /// Mirrors a horizontal dock style for RTL layout.
    /// </summary>
    /// <param name="dock">Incoming dock style.</param>
    /// <returns>Left and Right swapped; other values unchanged.</returns>
    public static ViewDockStyle MirrorDock(ViewDockStyle dock) => dock switch
    {
        ViewDockStyle.Left => ViewDockStyle.Right,
        ViewDockStyle.Right => ViewDockStyle.Left,
        _ => dock
    };

    /// <summary>
    /// Returns a new bitmap that is a horizontal mirror of <paramref name="source"/>.
    /// </summary>
    /// <param name="source">Image to flip. Not disposed.</param>
    /// <returns>A new bitmap, or <see langword="null"/> when <paramref name="source"/> is null.</returns>
    public static Image? FlipHorizontal(Image? source)
    {
        if (source == null)
        {
            return null;
        }

        var flipped = new Bitmap(source);
        flipped.RotateFlip(RotateFlipType.RotateNoneFlipX);
        return flipped;
    }

    /// <summary>
    /// Maps Near/Far content alignment to an absolute <see cref="System.Drawing.ContentAlignment"/> for WinForms text.
    /// </summary>
    /// <param name="align">Relative alignment.</param>
    /// <param name="rtl">True when both RTL layout flags are set.</param>
    /// <returns>MiddleLeft/Center/Right with Near and Far swapped when <paramref name="rtl"/> is true.</returns>
    public static System.Drawing.ContentAlignment MapNearFar(PaletteRelativeAlign align, bool rtl) => align switch
    {
        PaletteRelativeAlign.Center => System.Drawing.ContentAlignment.MiddleCenter,
        PaletteRelativeAlign.Far => rtl ? System.Drawing.ContentAlignment.MiddleLeft : System.Drawing.ContentAlignment.MiddleRight,
        _ => rtl ? System.Drawing.ContentAlignment.MiddleRight : System.Drawing.ContentAlignment.MiddleLeft
    };

    /// <summary>
    /// Copies <see cref="Control.RightToLeft"/> and layout mirroring onto a hosted or popup control.
    /// </summary>
    /// <param name="source">Control that owns the RTL flags.</param>
    /// <param name="target">Control that should match the source.</param>
    public static void ApplyTo(Control? source, Control? target)
    {
        if (source == null || target == null)
        {
            return;
        }

        target.RightToLeft = source.RightToLeft;
        var layout = CommonHelper.GetRightToLeftLayout(source);
        switch (target)
        {
            case VisualControlBase visual:
                visual.RightToLeftLayout = layout;
                break;
            case VisualPanel panel:
                panel.RightToLeftLayout = layout;
                break;
            case VisualContainerControlBase container:
                container.RightToLeftLayout = layout;
                break;
            case VisualPopup popup:
                popup.RightToLeftLayout = layout;
                break;
            case Form form:
                form.RightToLeftLayout = layout;
                break;
        }
    }
}
