// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System;
using System.Windows.Forms;
using System.Diagnostics;
using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    /// <summary>
    /// Implementation for the minimize button for mdi child form.
    /// </summary>
    public class ButtonSpecMdiChildMin : ButtonSpecMdiChildFixed
    {
        #region Instance Fields
        private readonly KryptonRibbon _ribbon;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecMdiChildMin class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        public ButtonSpecMdiChildMin(KryptonRibbon ribbon)
            : base(PaletteButtonSpecStyle.PendantMin)
        {
            Debug.Assert(ribbon != null);
            _ribbon = ribbon;
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
            // Cannot be seen if not attached to an mdi child window and cannot be seen
            // if the window is not maximized and so needing the pendant buttons
            if (MdiChild == null || MdiChild.IsDisposed || !MdiChild.IsHandleCreated || MdiChild.Disposing || !CommonHelper.IsFormMaximized(MdiChild))
            {
                return false;
            }

            // The minimize button is never present on tool windows
            switch (MdiChild.FormBorderStyle)
            {
                case FormBorderStyle.FixedToolWindow:
                case FormBorderStyle.SizableToolWindow:
                    return false;
            }

            // Have all buttons been turned off?
            if (!MdiChild.ControlBox)
            {
                return false;
            }

            // Has the minimize/maximize buttons been turned off?
            return MdiChild.MinimizeBox || MdiChild.MaximizeBox;
        }

        /// <summary>
        /// Gets the button enabled state.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button enabled state.</returns>
        public override ButtonEnabled GetEnabled(IPalette palette)
        {
            // Cannot be enabled if not attached to an mdi child window
            if (MdiChild == null || MdiChild.IsDisposed || !MdiChild.IsHandleCreated || MdiChild.Disposing)
            {
                return ButtonEnabled.False;
            }

            // Has the minimize button been turned off?
            return !MdiChild.MinimizeBox ? ButtonEnabled.False : ButtonEnabled.True;
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
                    MdiChild.WindowState = FormWindowState.Minimized;

                    // Let base class fire any other attached events
                    base.OnClick(e);
                }
            }
        }
        #endregion
    }
}
