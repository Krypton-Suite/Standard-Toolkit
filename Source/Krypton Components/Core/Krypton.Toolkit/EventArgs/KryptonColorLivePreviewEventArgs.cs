#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Data for <see cref="IKryptonColorLivePreview.ColorPreview"/>.
/// </summary>
public sealed class KryptonColorLivePreviewEventArgs : EventArgs
{
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonColorLivePreviewEventArgs"/> class.
    /// </summary>
    /// <param name="action">Stage of the preview.</param>
    /// <param name="color">Colour to paint for this stage.</param>
    /// <param name="committedColor">Colour that was committed when the drop-down opened.</param>
    public KryptonColorLivePreviewEventArgs(KryptonColorLivePreviewAction action, Color color, Color committedColor)
    {
        Action = action;
        Color = color;
        CommittedColor = committedColor;
    }

    /// <summary>
    /// Gets the stage of the preview.
    /// </summary>
    public KryptonColorLivePreviewAction Action { get; }

    /// <summary>
    /// Gets the colour to paint now.
    /// </summary>
    public Color Color { get; }

    /// <summary>
    /// Gets the colour that was committed when the drop-down opened.
    /// </summary>
    public Color CommittedColor { get; }
}