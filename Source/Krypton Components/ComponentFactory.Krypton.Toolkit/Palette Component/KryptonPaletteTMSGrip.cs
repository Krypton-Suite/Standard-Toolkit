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
    /// Storage for grip entries of the professional color table.
    /// </summary>
    public class KryptonPaletteTMSGrip : KryptonPaletteTMSBase
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteKCTGrip class.
        /// </summary>
        /// <param name="internalKCT">Reference to inherited values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        internal KryptonPaletteTMSGrip(KryptonInternalKCT internalKCT,
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
        public override bool IsDefault => (InternalKCT.InternalGripDark == Color.Empty) &&
                                          (InternalKCT.InternalGripLight == Color.Empty);

        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            GripDark = InternalKCT.GripDark;
            GripLight = InternalKCT.GripLight;
        }
        #endregion

        #region GripDark
        /// <summary>
        /// Gets and sets the color to use for shadow effects on the grip (move handle).
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Color to use for shadow effects on the grip (move handle).")]
        [KryptonDefaultColorAttribute()]
        public Color GripDark
        {
            get => InternalKCT.InternalGripDark;

            set 
            { 
                InternalKCT.InternalGripDark = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the GripDark property to its default value.
        /// </summary>
        public void ResetGripDark()
        {
            GripDark = Color.Empty;
        }
        #endregion

        #region GripLight
        /// <summary>
        /// Gets and sets the color to use for highlight effects on the grip (move handle).
        /// </summary>
        [KryptonPersist(false)]
        [Category("ToolMenuStatus")]
        [Description("Color to use for highlight effects on the grip (move handle).")]
        [KryptonDefaultColorAttribute()]
        public Color GripLight
        {
            get => InternalKCT.InternalGripLight;

            set 
            { 
                InternalKCT.InternalGripLight = value;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// esets the GripLight property to its default value.
        /// </summary>
        public void ResetGripLight()
        {
            GripLight = Color.Empty;
        }
        #endregion
    }
}
