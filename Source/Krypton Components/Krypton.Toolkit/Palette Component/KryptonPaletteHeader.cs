﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for palette header states.
    /// </summary>
    public class KryptonPaletteHeader : Storage
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteHeader class.
        /// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="backStyle">Background style.</param>
        /// <param name="borderStyle">Border style.</param>
        /// <param name="contentStyle">Content style.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteHeader(PaletteRedirect redirect,
                                    PaletteBackStyle backStyle,
                                    PaletteBorderStyle borderStyle,
                                    PaletteContentStyle contentStyle,
                                    NeedPaintHandler needPaint) 
        {
            // Create the storage objects
            StateCommon = new PaletteHeaderRedirect(redirect, backStyle, borderStyle, contentStyle, needPaint);
            StateDisabled = new PaletteTripleMetric(StateCommon, needPaint);
            StateNormal = new PaletteTripleMetric(StateCommon, needPaint);
        }
        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public void SetRedirector(PaletteRedirect redirect) => StateCommon.SetRedirector(redirect);

        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => StateCommon.IsDefault &&
                                          StateDisabled.IsDefault &&
                                          StateNormal.IsDefault;

        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            StateDisabled.PopulateFromBase(PaletteState.Disabled);
            StateNormal.PopulateFromBase(PaletteState.Normal);
        }
        #endregion

        #region StateCommon
        /// <summary>
        /// Gets access to the common header appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining common header appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteHeaderRedirect StateCommon { get; }

        private bool ShouldSerializeStateCommon() => !StateCommon.IsDefault;

        #endregion
    
        #region StateDisabled
        /// <summary>
        /// Gets access to the disabled header appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining disabled header appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleMetric StateDisabled { get; }

        private bool ShouldSerializeStateDisabled() => !StateDisabled.IsDefault;

        #endregion

        #region StateNormal
        /// <summary>
        /// Gets access to the normal header appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category(@"Visuals")]
        [Description(@"Overrides for defining normal header appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleMetric StateNormal { get; }

        private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

        #endregion
    }
}
