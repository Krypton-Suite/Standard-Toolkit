#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public interface IKryptonTaskDialogElementFlowDirection
{
    /// <summary>
    /// Get or sets the flow direction of the control.
    /// </summary>
    public FlowDirection FlowDirection { get; set; }

    /// <summary>
    /// Get or set the height of the element.
    /// </summary>
    public bool ShowFlowDirection { get; set; }
}