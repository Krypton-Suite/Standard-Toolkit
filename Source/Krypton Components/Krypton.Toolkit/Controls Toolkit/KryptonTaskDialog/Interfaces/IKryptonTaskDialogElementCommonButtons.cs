#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public interface IKryptonTaskDialogElementCommonButtons
{
    /// <summary>
    /// A bitmask defining the buttons to display on the TaksDialog.
    /// </summary>
    public KryptonTaskDialogCommonButtonTypes Buttons { get; set; }

    /// <summary>
    /// The assgined button will be triggered when the user presses the enter key.
    /// </summary>
    public KryptonTaskDialogCommonButtonTypes AcceptButton { get; set; }

    /// <summary>
    /// The assgined button will be triggered when the user presses the escape key.
    /// </summary>
    public KryptonTaskDialogCommonButtonTypes CancelButton { get; set; }


}
