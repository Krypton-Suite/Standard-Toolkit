#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace TestForm;

/// <summary>
/// Test form to verify that KryptonTextBox Validating event fires only once per focus change.
/// Tests fix for issue #2801: https://github.com/Krypton-Suite/Standard-Toolkit/issues/2801
/// </summary>
public partial class KryptonTextBoxValidatingTest : KryptonForm
{
    private int _textBox1ValidatingCount;
    private int _textBox2ValidatingCount;

    public KryptonTextBoxValidatingTest()
    {
        InitializeComponent();
        ResetCounts();
        UpdateCountLabels();
    }

    private void ResetCounts()
    {
        _textBox1ValidatingCount = 0;
        _textBox2ValidatingCount = 0;
    }

    private void UpdateCountLabels()
    {
        klblTextBox1Count.Values.Text = $"Validating events: {_textBox1ValidatingCount}";
        klblTextBox2Count.Values.Text = $"Validating events: {_textBox2ValidatingCount}";
    }

    private void ktxtTextBox1_Validating(object sender, CancelEventArgs e)
    {
        _textBox1ValidatingCount++;
        UpdateCountLabels();

        // Show message box to visually confirm the event (as described in the bug report)
        MessageBox.Show("TextBox1 Validating event fired", "Validation Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void ktxtTextBox2_Validating(object sender, CancelEventArgs e)
    {
        _textBox2ValidatingCount++;
        UpdateCountLabels();

        // Show message box to visually confirm the event (as described in the bug report)
        MessageBox.Show("TextBox2 Validating event fired", "Validation Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void kbtnResetCounts_Click(object sender, EventArgs e)
    {
        ResetCounts();
        UpdateCountLabels();
    }

    private void kbtnTestInstructions_Click(object sender, EventArgs e)
    {
        string instructions = @"Test Instructions:

1. Click in TextBox1, then press Tab or click in TextBox2
   → You should see ONE message box, not two

2. Click in TextBox2, then press Tab or click in TextBox1
   → You should see ONE message box, not two

3. Check the event count labels - each should increment by 1 per focus change

Expected Behavior:
- Validating event should fire ONCE per control when it loses focus
- Each textbox should show exactly 1 message box per focus change
- Event counts should match the number of focus changes

Bug #2801:
- Before fix: Each textbox raised Validating TWICE, so 4 message boxes total
- After fix: Each textbox raises Validating ONCE, so 2 message boxes total";

        MessageBox.Show(instructions, "Test Instructions", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
