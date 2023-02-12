#region BSD License
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

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="ButtonSpecNavFormClose" /> class.</summary>
        /// <param name="navigator">The navigator.</param>
        public ButtonSpecNavFormClose(KryptonNavigator navigator) : base(navigator, PaletteButtonSpecStyle.FormClose)
        {
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
                    IntPtr hSystemMenu = PI.GetSystemMenu(Navigator.Owner!.Handle, false);
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
            if (Navigator.Owner!.ApplyComposition && Navigator.Owner.ApplyCustomChrome)
            {
                return false;
            }

            // Have all buttons been turned off?
            return Navigator.Owner.ControlBox && Navigator.Owner.CloseBox;
        }

        public override ButtonCheckState GetChecked(PaletteBase palette) => ButtonCheckState.NotCheckButton;

        public override ButtonEnabled GetEnabled(PaletteBase palette) => Navigator.Owner!.CloseBox && Enabled ? ButtonEnabled.True : ButtonEnabled.False;

        #endregion
    }
}