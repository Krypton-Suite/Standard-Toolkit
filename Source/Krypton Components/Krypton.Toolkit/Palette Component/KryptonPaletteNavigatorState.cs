// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.550.0  
// *****************************************************************************

using System.ComponentModel;
using System.Diagnostics;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for an individual navigator states.
    /// </summary>
    public class KryptonPaletteNavigatorState : Storage
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteNavigatorState class.
        /// </summary>
        /// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteNavigatorState(PaletteRedirect redirect,
                                            NeedPaintHandler needPaint)
        {
            Debug.Assert(redirect != null);
            
            // Create the storage objects
            Bar = new KryptonPaletteNavigatorStateBar(redirect, needPaint);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => Bar.IsDefault;

        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            Bar.PopulateFromBase();
        }
        #endregion

        #region Bar
        /// <summary>
        /// Gets access to the navigator bar appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining navigator bar appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteNavigatorStateBar Bar { get; }

        private bool ShouldSerializeStateCommon()
        {
            return !Bar.IsDefault;
        }
        #endregion
    }
}
