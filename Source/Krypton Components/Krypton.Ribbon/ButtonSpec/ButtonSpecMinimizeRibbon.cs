using System;
using System.ComponentModel;
using System.Diagnostics;
using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Implementation for the minimize ribbon button.
    /// </summary>
    public class ButtonSpecMinimizeRibbon : ButtonSpec
    {
        #region Instance Fields
        private readonly KryptonRibbon _ribbon;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecMinimizeRibbon class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        public ButtonSpecMinimizeRibbon(KryptonRibbon ribbon)
        {
            Debug.Assert(ribbon != null);
            _ribbon = ribbon;

            // Fix the type
            ProtectedType = PaletteButtonSpecStyle.RibbonMinimize;
        }         
        #endregion

        #region AllowComponent
        /// <summary>
        /// Gets a value indicating if the component is allowed to be selected at design time.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool AllowComponent => false;

        #endregion

        #region ButtonSpecStype
        /// <summary>
        /// Gets and sets the actual type of the button.
        /// </summary>
        public PaletteButtonSpecStyle ButtonSpecType
        {
            get => ProtectedType;
            set => ProtectedType = value;
        }
        #endregion

        #region IButtonSpecValues
        /// <summary>
        /// Gets the button visible value.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button visibiliy.</returns>
        public override bool GetVisible(IPalette palette)
        {
            return _ribbon.ShowMinimizeButton && !_ribbon.MinimizedMode;
        }

        /// <summary>
        /// Gets the button enabled state.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button enabled state.</returns>
        public override ButtonEnabled GetEnabled(IPalette palette)
        {
            return ButtonEnabled.True;
        }

        /// <summary>
        /// Gets the button checked state.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button checked state.</returns>
        public override ButtonCheckState GetChecked(IPalette palette)
        {
            // Close button is never shown as checked
            return ButtonCheckState.NotCheckButton;
        }

        /// <summary>
        /// Gets the button style.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button style.</returns>
        public override ButtonStyle GetStyle(IPalette palette)
        {
            return ButtonStyle.ButtonSpec;
        }
        #endregion    

        #region Protected Overrides
        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            // Only if associated view is enabled to we perform an action
            if (GetViewEnabled())
            {
                if (!_ribbon.InDesignMode)
                {
                    _ribbon.MinimizedMode = true;
                }
            }
        }
        #endregion
    }
}
