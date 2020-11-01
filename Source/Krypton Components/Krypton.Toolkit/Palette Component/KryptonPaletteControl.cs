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

namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for palette control states.
    /// </summary>
    public class KryptonPaletteControl : KryptonPaletteDouble3
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteControl class.
        /// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="backStyle">Background style.</param>
        /// <param name="borderStyle">Border style.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteControl(PaletteRedirect redirect,
                                     PaletteBackStyle backStyle,
                                     PaletteBorderStyle borderStyle,
                                     NeedPaintHandler needPaint) 
            : base(redirect, backStyle, borderStyle, needPaint)
        {
        }
        #endregion

        #region StateCommon
        /// <summary>
        /// Gets access to the common control appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common control appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDoubleRedirect StateCommon => _stateCommon;

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }
        #endregion
    
        #region StateDisabled
        /// <summary>
        /// Gets access to the disabled control appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining disabled control appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDouble StateDisabled => _stateDisabled;

        private bool ShouldSerializeStateDisabled()
        {
            return !_stateDisabled.IsDefault;
        }
        #endregion

        #region StateNormal
        /// <summary>
        /// Gets access to the normal control appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining normal control appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDouble StateNormal => _stateNormal;

        private bool ShouldSerializeStateNormal()
        {
            return !_stateNormal.IsDefault;
        }
        #endregion
    }
}
