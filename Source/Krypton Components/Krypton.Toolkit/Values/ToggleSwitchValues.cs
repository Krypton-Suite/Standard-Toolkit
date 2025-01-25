#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ToggleSwitchValues : NullContentValues
    {
        #region Identity

        public ToggleSwitchValues()
        {
            Reset();
        }

        #endregion

        #region Public

        public void Reset()
        {
            UseGradientOnKnob = false;

            GradientDirection = LinearGradientMode.ForwardDiagonal;

            ShowText = false;
        }

        public bool ShowText { get; set; }

        public bool UseGradientOnKnob { get; set; }

        public LinearGradientMode GradientDirection { get; set; }

        #endregion
    }
}
