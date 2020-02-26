// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.500.0.0  
// *****************************************************************************

using System.Drawing;
using System.ComponentModel;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for separator entries of the professional color table.
    /// </summary>
    public class KryptonPaletteTMSSeparator : KryptonPaletteTMSBase
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteKCTSeparator class.
        /// </summary>
        /// <param name="internalKCT">Reference to inherited values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        internal KryptonPaletteTMSSeparator(KryptonInternalKCT internalKCT,
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
        public override bool IsDefault => (InternalKCT.InternalSeparatorDark == Color.Empty) &&
                                          (InternalKCT.InternalSeparatorLight == Color.Empty);

        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            SeparatorDark = InternalKCT.SeparatorDark;
            SeparatorLight = InternalKCT.SeparatorLight;
        }
        #endregion

        #region SeparatorDark
        /// <summary>
        /// Gets and sets the color to use for shadow effects on the ToolStripSeparator.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Color to use for shadow effects on the ToolStripSeparator.")]
        [KryptonDefaultColorAttribute()]
        public Color SeparatorDark
        {
            get => InternalKCT.InternalSeparatorDark;

            set 
            { 
                InternalKCT.InternalSeparatorDark = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Resets the SeparatorDark property to its default value.
        /// </summary>
        public void ResetSeparatorDark()
        {
            SeparatorDark = Color.Empty;
        }
        #endregion

        #region SeparatorLight
        /// <summary>
        /// Gets and sets the color to use for highlight effects on the ToolStripSeparator.
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Color to use for highlight effects on the ToolStripSeparator.")]
        [KryptonDefaultColorAttribute()]
        public Color SeparatorLight
        {
            get => InternalKCT.InternalSeparatorLight;

            set 
            { 
                InternalKCT.InternalSeparatorLight = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Resets the SeparatorLight property to its default value.
        /// </summary>
        public void ResetSeparatorLight()
        {
            SeparatorLight = Color.Empty;
        }
        #endregion
    }
}
