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
    /// Settings associated with context menus.
    /// </summary>
    public class KryptonPaletteContextMenu : Storage
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteContextMenu class.
        /// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        internal KryptonPaletteContextMenu(PaletteRedirect redirect,
                                           NeedPaintHandler needPaint)
        {
            // Create the storage objects
            StateCommon = new PaletteContextMenuRedirect(redirect, needPaint);
            StateNormal = new PaletteContextMenuItemState(StateCommon);
            StateDisabled = new PaletteContextMenuItemState(StateCommon);
            StateHighlight = new PaletteContextMenuItemStateHighlight(StateCommon);
            StateChecked = new PaletteContextMenuItemStateChecked(StateCommon);
        }
        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public void SetRedirector(PaletteRedirect redirect)
        {
            StateCommon.SetRedirector(redirect);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        public override bool IsDefault => StateCommon.IsDefault &&
                                          StateNormal.IsDefault &&
                                          StateDisabled.IsDefault &&
                                          StateHighlight.IsDefault &&
                                          StateChecked.IsDefault;

        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="common">Reference to common settings.</param>
        public void PopulateFromBase(KryptonPaletteCommon common)
        {
            // Populate only the designated styles
            StateCommon.PopulateFromBase(common, PaletteState.Normal);
            StateDisabled.PopulateFromBase(common, PaletteState.Disabled);
            StateNormal.PopulateFromBase(common, PaletteState.Normal);
            StateHighlight.PopulateFromBase(common, PaletteState.Tracking);
            StateChecked.PopulateFromBase(common, PaletteState.CheckedNormal);
        }
        #endregion

        #region StateCommon
        /// <summary>
        /// Gets access to the common appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContextMenuRedirect StateCommon { get; }

        private bool ShouldSerializeStateCommon()
        {
            return !StateCommon.IsDefault;
        }
        #endregion

        #region StateDisabled
        /// <summary>
        /// Gets access to the disabled appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining disabled appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContextMenuItemState StateDisabled { get; }

        private bool ShouldSerializeStateDisabled()
        {
            return !StateDisabled.IsDefault;
        }
        #endregion

        #region StateNormal
        /// <summary>
        /// Gets access to the normal appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining normal appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContextMenuItemState StateNormal { get; }

        private bool ShouldSerializeStateNormal()
        {
            return !StateNormal.IsDefault;
        }
        #endregion

        #region StateHighlight
        /// <summary>
        /// Gets access to the highlight appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining highlight appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContextMenuItemStateHighlight StateHighlight { get; }

        private bool ShouldSerializeStateHighlight()
        {
            return !StateHighlight.IsDefault;
        }
        #endregion

        #region StateChecked
        /// <summary>
        /// Gets access to the checked appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining checked appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContextMenuItemStateChecked StateChecked { get; }

        private bool ShouldSerializeStateChecked()
        {
            return !StateChecked.IsDefault;
        }
        #endregion
    }
}
