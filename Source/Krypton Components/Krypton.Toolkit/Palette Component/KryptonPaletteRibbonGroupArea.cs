#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

using System.ComponentModel;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for palette ribbon group area states.
    /// </summary>
    public class KryptonPaletteRibbonGroupArea : Storage
    {
        #region Instance Fields
        private readonly PaletteRibbonBackInheritRedirect _stateInherit;

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteRibbonGroupArea class.
        /// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteRibbonGroupArea(PaletteRedirect redirect,
                                             NeedPaintHandler needPaint) 
        {
            // Create the storage objects
            _stateInherit = new PaletteRibbonBackInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonGroupArea);
            StateCommon = new PaletteRibbonBack(_stateInherit, needPaint);
            StateCheckedNormal = new PaletteRibbonBack(StateCommon, needPaint);
            StateContextCheckedNormal = new PaletteRibbonBack(StateCommon, needPaint);
        }
        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public void SetRedirector(PaletteRedirect redirect)
        {
            _stateInherit.SetRedirector(redirect);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => StateCommon.IsDefault &&
                                          StateCheckedNormal.IsDefault &&
                                          StateContextCheckedNormal.IsDefault;

        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            // Populate only the designated styles
            StateCheckedNormal.PopulateFromBase(PaletteState.CheckedNormal);
            StateContextCheckedNormal.PopulateFromBase(PaletteState.ContextCheckedNormal);
        }
        #endregion

        #region StateCommon
        /// <summary>
        /// Gets access to the common ribbon application button appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common ribbon application button appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonBack StateCommon { get; }

        private bool ShouldSerializeStateCommon()
        {
            return !StateCommon.IsDefault;
        }
        #endregion

        #region StateCheckedNormal
        /// <summary>
        /// Gets access to the checked ribbon group area appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining checked ribbon group area appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonBack StateCheckedNormal { get; }

        private bool ShouldSerializeStateCheckedNormal()
        {
            return !StateCheckedNormal.IsDefault;
        }
        #endregion

        #region StateContextCheckedNormal
        /// <summary>
        /// Gets access to the context checked ribbon group area appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining context checked ribbon group area appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonBack StateContextCheckedNormal { get; }

        private bool ShouldSerializeStateContextCheckedNormal()
        {
            return !StateContextCheckedNormal.IsDefault;
        }
        #endregion
    }
}
