#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Used internally by KryptonTaskDialogElementCommandLinkButtons.
/// </summary>
public partial class KryptonTaskDialogElementCommandLinkButtons
{
    /// <summary>
    /// The standard FlowLayoutPanel does not provide access to the DoubleBuffered property.
    /// </summary>
    internal class FlowLayoutPanelDoubleBuffered : System.Windows.Forms.FlowLayoutPanel
    {
        public FlowLayoutPanelDoubleBuffered() : base()
        {
            DoubleBuffered = true;
        }

        /// <summary>
        /// Get or set double buffered drawing to prevent flickering,
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool DoubleBuffered
        {
            get => base.DoubleBuffered;
            set => base.DoubleBuffered = value;
        }
    }
}