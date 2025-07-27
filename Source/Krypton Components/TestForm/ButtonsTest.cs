#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

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

    private void kbtnButtonStyles_Click(object sender, EventArgs e)
    {
        new ButtonStyleExamples().Show();
    }

    private void kryptonCommand1_Execute(object sender, EventArgs e)
    {
        var typeName = sender.GetType().FullName;
        var item = sender as KryptonContextMenuItem;
        var paramText = item?.CommandParameter is string s ? s : "<no param>";

        KryptonMessageBox.Show($"Command executed by:\nType: {typeName}\nParam: {paramText}", "Context Item Called");
    }
}