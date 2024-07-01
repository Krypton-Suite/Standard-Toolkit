#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <inheritdoc />
    [ToolboxItem(false), ToolboxBitmap(typeof(FlowLayoutPanel)), Description("A Kryptonised version of the FlowLayoutPanel. Handles the layout of its components, and arranges them in the format of a table automatically.")]
    internal class KryptonFlowLayoutPanel : FlowLayoutPanel
    {
        #region Instance Fields

        private KryptonPanel _backgroundPanel;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonFlowLayoutPanel" /> class.</summary>
        public KryptonFlowLayoutPanel()
        {
            // ToDo: To be fleshed out in V100
        }

        #endregion
    }
}