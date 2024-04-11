#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved. 
 *  
 */
#endregion

namespace TestForm
{
    public partial class ButtonsTest : KryptonForm
    {
        public ButtonsTest()
        {
            InitializeComponent();
        }

        private void kcbtnDropDown_SelectedColorChanged(object sender, ColorEventArgs e)
        {
            kryptonButton3.Values.DropDownArrowColor = e.Color;

            kryptonButton4.Values.DropDownArrowColor = e.Color;

            kryptonButton7.Values.DropDownArrowColor = e.Color;

            kryptonButton8.Values.DropDownArrowColor = e.Color;
        }
    }
}
