// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2020. All rights reserved. (https://github.com/Wagnerp/Krypton-Toolkit-Suite-NET-Core)
//  Version 5.500.0.0  www.ComponentFactory.com
// *****************************************************************************

using System.Drawing;
using System.ComponentModel;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Storage for rafting entries of the professional color table.
    /// </summary>
    public class KryptonPaletteTMSRafting : KryptonPaletteTMSBase
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteKCTRafting class.
        /// </summary>
        /// <param name="internalKCT">Reference to inherited values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        internal KryptonPaletteTMSRafting(KryptonInternalKCT internalKCT,
                                          NeedPaintHandler needPaint)
            : base(internalKCT, needPaint)
        {
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (InternalKCT.InternalRaftingContainerGradientBegin == Color.Empty) &&
                                          (InternalKCT.InternalRaftingContainerGradientEnd == Color.Empty);

        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            RaftingContainerGradientBegin = InternalKCT.RaftingContainerGradientBegin;
            RaftingContainerGradientEnd = InternalKCT.RaftingContainerGradientEnd;
        }
        #endregion

        #region RaftingContainerGradientBegin
        /// <summary>
        /// Gets and sets the starting color of the gradient used in the ToolStripContainer.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Starting color of the gradient used in the ToolStripContainer.")]
        [KryptonDefaultColorAttribute()]
        public Color RaftingContainerGradientBegin
        {
            get => InternalKCT.InternalRaftingContainerGradientBegin;

            set 
            { 
                InternalKCT.InternalRaftingContainerGradientBegin = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the RaftingContainerGradientBegin property to its default value.
        /// </summary>
        public void ResetRaftingContainerGradientBegin()
        {
            RaftingContainerGradientBegin = Color.Empty;
        }
        #endregion

        #region RaftingContainerGradientEnd
        /// <summary>
        /// Gets and sets the ending color of the gradient used in the ToolStripContainer.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Ending color of the gradient used in the ToolStripContainer.")]
        [KryptonDefaultColorAttribute()]
        public Color RaftingContainerGradientEnd
        {
            get => InternalKCT.InternalRaftingContainerGradientEnd;

            set 
            { 
                InternalKCT.InternalRaftingContainerGradientEnd = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the RaftingContainerGradientEnd property to its default value.
        /// </summary>
        public void ResetRaftingContainerGradientEnd()
        {
            RaftingContainerGradientEnd = Color.Empty;
        }
        #endregion
    }
}
