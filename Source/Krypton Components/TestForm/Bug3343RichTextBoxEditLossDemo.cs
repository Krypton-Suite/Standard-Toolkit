#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Manual repro for Issue #3343: KryptonRichTextBox losing typed text when the mouse leaves the control
/// (palette repaint restored stale RTF). Type a short line, move the pointer out without clicking elsewhere;
/// text and character counts should stay the same. KryptonTextBox is shown for comparison.
/// </summary>
public partial class Bug3343RichTextBoxEditLossDemo : KryptonForm
{
    public Bug3343RichTextBoxEditLossDemo()
    {
        InitializeComponent();
    }

    private void Bug3343RichTextBoxEditLossDemo_Load(object? sender, EventArgs e)
    {
        UpdateStatusLines();
    }

    private void krtbDemo_TextChanged(object? sender, EventArgs e) => UpdateStatusLines();

    private void ktbxReference_TextChanged(object? sender, EventArgs e) => UpdateStatusLines();

    private void krtbDemo_TrackMouseLeave(object? sender, EventArgs e)
    {
        klblAfterLeave.Values.Text =
            $"After TrackMouseLeave on KryptonRichTextBox: TextLength={krtbDemo.TextLength}. " +
            "That count should match what you typed; if it shrinks or clears when you only moved the mouse out, the bug is present.";
        UpdateStatusLines();
    }

    private void UpdateStatusLines()
    {
        klblLive.Values.Text =
            $"Live: KryptonRichTextBox TextLength={krtbDemo.TextLength}, KryptonTextBox TextLength={ktbxReference.TextLength}.";
    }
}
