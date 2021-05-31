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


namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for palette track bar states.
    /// </summary>
    public class KryptonPaletteTrackBar : Storage
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteTrackbar class.
        /// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteTrackBar(PaletteRedirect redirect,
                                      NeedPaintHandler needPaint) 
        {
            // Create the storage objects
            StateCommon = new PaletteTrackBarRedirect(redirect, needPaint);
            OverrideFocus = new PaletteTrackBarRedirect(redirect, needPaint);
            StateDisabled = new PaletteTrackBarStates(StateCommon, needPaint);
            StateNormal = new PaletteTrackBarStates(StateCommon, needPaint);
            StateTracking = new PaletteTrackBarPositionStates(StateCommon, needPaint);
            StatePressed = new PaletteTrackBarPositionStates(StateCommon, needPaint);
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
            OverrideFocus.SetRedirector(redirect);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => StateCommon.IsDefault &&
                                          OverrideFocus.IsDefault &&
                                          StateDisabled.IsDefault &&
                                          StateNormal.IsDefault &&
                                          StateTracking.IsDefault &&
                                          StatePressed.IsDefault;

        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            // Populate only the designated styles
            OverrideFocus.PopulateFromBase(PaletteState.FocusOverride);
            StateDisabled.PopulateFromBase(PaletteState.Disabled);
            StateNormal.PopulateFromBase(PaletteState.Normal);
            StateTracking.PopulateFromBase(PaletteState.Tracking);
            StatePressed.PopulateFromBase(PaletteState.Pressed);
        }
        #endregion

        #region StateCommon
        /// <summary>
        /// Gets access to the common track bar appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common track bar appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTrackBarRedirect StateCommon { get; }

        private bool ShouldSerializeStateCommon()
        {
            return !StateCommon.IsDefault;
        }
        #endregion
    
        #region StateDisabled
        /// <summary>
        /// Gets access to the disabled track bar appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining disabled track bar appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTrackBarStates StateDisabled { get; }

        private bool ShouldSerializeStateDisabled()
        {
            return !StateDisabled.IsDefault;
        }
        #endregion

        #region StateNormal
        /// <summary>
        /// Gets access to the normal track bar appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining normal track bar appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTrackBarStates StateNormal { get; }

        private bool ShouldSerializeStateNormal()
        {
            return !StateNormal.IsDefault;
        }
        #endregion

        #region StateTracking
        /// <summary>
        /// Gets access to the tracking track bar appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining tracking track bar appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTrackBarPositionStates StateTracking { get; }

        private bool ShouldSerializeStateTracking()
        {
            return !StateTracking.IsDefault;
        }
        #endregion

        #region StatePressed
        /// <summary>
        /// Gets access to the pressed track bar appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining pressed track bar appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTrackBarPositionStates StatePressed { get; }

        private bool ShouldSerializeStatePressed()
        {
            return !StatePressed.IsDefault;
        }
        #endregion

        #region OverrideFocus
        /// <summary>
        /// Gets access to the track bar appearance when it has focus.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining track bar appearance when it has focus.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTrackBarRedirect OverrideFocus { get; }

        private bool ShouldSerializeOverrideFocus()
        {
            return !OverrideFocus.IsDefault;
        }
        #endregion    
    }
}
