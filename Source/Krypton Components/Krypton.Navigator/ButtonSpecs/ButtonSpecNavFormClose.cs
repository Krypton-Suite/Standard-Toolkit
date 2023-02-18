﻿#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Navigator
{
    public class ButtonSpecNavFormClose : ButtonSpecNavFixed
    {
        #region Instance Fields

        private bool _enabled = true;

        private KryptonNavigator _navigator;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="ButtonSpecNavFormClose" /> class.</summary>
        /// <param name="navigator">The navigator.</param>
        public ButtonSpecNavFormClose(KryptonNavigator navigator) : base(navigator, PaletteButtonSpecStyle.FormClose)
        {
            _navigator = navigator;
        }

        #endregion

        #region Implementation

        /// <summary>
        /// Form Close Button Enabled: This will also Disable the System Menu `Close` BUT NOT the `Alt+F4` key action
        /// </summary>
        [Category(@"Appearance")]
        [DefaultValue(true)]
        [Description("Form Close Button Enabled: This will also Disable the System Menu `Close` BUT NOT the `Alt+F4` key action")]
        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    IntPtr hSystemMenu = PI.GetSystemMenu(_navigator.Owner!.Handle, false);
                    if (hSystemMenu != IntPtr.Zero)
                    {
                        PI.EnableMenuItem(hSystemMenu, PI.SC_.CLOSE, _enabled ? PI.MF_.ENABLED : PI.MF_.DISABLED);
                    }
                }
            }
        }

        #endregion

        #region ButtonSpecNavFixed Implementation

        public override bool GetVisible(PaletteBase? palette)
        {
            // We do not show if the custom chrome is combined with composition,
            // in which case the form buttons are handled by the composition
            if (_navigator.Owner!.ApplyComposition && _navigator.Owner.ApplyCustomChrome)
            {
                return false;
            }

            // Have all buttons been turned off?
            return _navigator.Owner.ControlBox && _navigator.Owner.CloseBox;
        }

        public override ButtonCheckState GetChecked(PaletteBase palette) => ButtonCheckState.NotCheckButton;

        public override ButtonEnabled GetEnabled(PaletteBase palette) => _navigator.Owner!.CloseBox && Enabled ? ButtonEnabled.True : ButtonEnabled.False;

        #endregion

        #region Protected Overrides

        protected override void OnClick(EventArgs e)
        {
            if (GetViewEnabled())
            {
                if (!_navigator.Owner.InertForm)
                {
                    MouseEventArgs mea = (MouseEventArgs)e;

                    if (GetView().ClientRectangle.Contains(mea.Location))
                    {
                        PropertyInfo? propertyInfo = typeof(Form).GetProperty(nameof(CloseReason),
                            BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.NonPublic);

                        propertyInfo.SetValue(_navigator.Owner, CloseReason.UserClosing, null);

                        Point screenPosition = Control.MousePosition;

                        IntPtr lParam = (IntPtr)(PI.MAKELOWORD(screenPosition.X) | PI.MAKEHIWORD(screenPosition.Y));

                        // Note: Do I need to make 'SC_' public?
                        //? Navigator.Owner.SendSysCommand(PI.SC_.CLOSE, lParam);

                        base.OnClick(e);
                    }
                }
            }
        }

        #endregion
    }
}