#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public interface IKryptonTaskDialogElementEventSizeChanged
{
    /// <summary>
    /// Event and corresponding action that is used to notify subscribers that the size of the element has changed.
    /// </summary>
    public event Action SizeChanged;
}
