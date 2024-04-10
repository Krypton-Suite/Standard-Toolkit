#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved. 
 *  
 */
#endregion

namespace TestForm
{
    public partial class FormBorderTest : KryptonForm
    {
        public FormBorderTest()
        {
            InitializeComponent();
        }

        private void kbtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ChangeBorderStyle(FormBorderStyle borderStyle) => FormBorderStyle = borderStyle;

        private void FormBorderTest_Load(object sender, EventArgs e)
        {
            foreach (var value in Enum.GetValues(typeof(FormBorderStyle)))
            {
                kcmbBorderStyle.Items.Add(value);
            }

            kcmbBorderStyle.SelectedIndex = 4;
        }

        private void kcmbBorderStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeBorderStyle((FormBorderStyle)Enum.Parse(typeof(FormBorderStyle), kcmbBorderStyle.Text));
        }
    }
}
