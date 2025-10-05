#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace TestForm;

public partial class TextBoxEventTest : KryptonForm
{
    public TextBoxEventTest()
    {
        InitializeComponent();
    }

    private void AddNormalTextBoxEvent(string message) => lbNormalTextBoxEvents.Items.Add(message);

    private void AddKryptonTextBoxEvent(string message) => klbKryptonTextBoxEvents.Items.Add(message);

    private void txtNormalTextBox_Click(object sender, EventArgs e) => AddNormalTextBoxEvent(nameof(txtNormalTextBox_Click));

    private void txtNormalTextBox_DoubleClick(object sender, EventArgs e) => AddNormalTextBoxEvent(nameof(txtNormalTextBox_DoubleClick));

    private void txtNormalTextBox_KeyDown(object sender, KeyEventArgs e) => AddNormalTextBoxEvent(nameof(txtNormalTextBox_KeyDown));

    private void txtNormalTextBox_KeyPress(object sender, KeyPressEventArgs e) => AddNormalTextBoxEvent(nameof(txtNormalTextBox_KeyPress));

    private void txtNormalTextBox_KeyUp(object sender, KeyEventArgs e) => AddNormalTextBoxEvent(nameof(txtNormalTextBox_KeyUp));

    private void txtNormalTextBox_MouseClick(object sender, MouseEventArgs e) => AddNormalTextBoxEvent(nameof(txtNormalTextBox_MouseClick));

    private void txtNormalTextBox_MouseDoubleClick(object sender, MouseEventArgs e) => AddNormalTextBoxEvent(nameof(txtNormalTextBox_MouseDoubleClick));

    private void txtNormalTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) => AddNormalTextBoxEvent(nameof(txtNormalTextBox_PreviewKeyDown));

    private void txtNormalTextBox_Validated(object sender, EventArgs e) => AddNormalTextBoxEvent(nameof(txtNormalTextBox_Validated));

    private void txtNormalTextBox_Validating(object sender, CancelEventArgs e) => AddNormalTextBoxEvent(nameof(txtNormalTextBox_Validating));

    private void ktxtKryptonTextBox_Click(object sender, EventArgs e) => AddKryptonTextBoxEvent(nameof(ktxtKryptonTextBox_Click));

    private void ktxtKryptonTextBox_DoubleClick(object sender, EventArgs e) => AddKryptonTextBoxEvent(nameof(ktxtKryptonTextBox_DoubleClick));

    private void ktxtKryptonTextBox_KeyDown(object sender, KeyEventArgs e) => AddKryptonTextBoxEvent(nameof(ktxtKryptonTextBox_KeyDown));

    private void ktxtKryptonTextBox_KeyPress(object sender, KeyPressEventArgs e) => AddKryptonTextBoxEvent(nameof(ktxtKryptonTextBox_KeyPress));

    private void ktxtKryptonTextBox_KeyUp(object sender, KeyEventArgs e) => AddKryptonTextBoxEvent(nameof(ktxtKryptonTextBox_KeyUp));

    private void ktxtKryptonTextBox_MouseClick(object sender, MouseEventArgs e) => AddKryptonTextBoxEvent(nameof(ktxtKryptonTextBox_MouseClick));

    private void ktxtKryptonTextBox_MouseDoubleClick(object sender, MouseEventArgs e) => AddKryptonTextBoxEvent(nameof(ktxtKryptonTextBox_MouseDoubleClick));

    private void ktxtKryptonTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) => AddKryptonTextBoxEvent(nameof(ktxtKryptonTextBox_PreviewKeyDown));

    private void ktxtKryptonTextBox_Validated(object sender, EventArgs e) => AddKryptonTextBoxEvent(nameof(ktxtKryptonTextBox_Validated));

    private void ktxtKryptonTextBox_Validating(object sender, CancelEventArgs e) => AddKryptonTextBoxEvent(nameof(ktxtKryptonTextBox_Validating));

    private void kbtnClearNormalEvents_Click(object sender, EventArgs e)
    {
        lbNormalTextBoxEvents.Items.Clear();

        //kbtnClearNormalEvents.Enabled = false;
    }

    private void kbtnClearKryptonEvents_Click(object sender, EventArgs e)
    {
        klbKryptonTextBoxEvents.Items.Clear();

        //kbtnClearKryptonEvents.Enabled = false;
    }
}