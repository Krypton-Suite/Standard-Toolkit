#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class KryptonDialogExamples: KryptonForm
{
    public KryptonDialogExamples()
    {
        InitializeComponent();
    }

    private void kbtnColorDialog_Click(object sender, EventArgs e)
    {
        var kcd = new KryptonColorDialog();

        kcd.ShowDialog();
    }

    private void kbtnFontDialog_Click(object sender, EventArgs e)
    {
        var kfd = new KryptonFontDialog();

        kfd.ShowDialog();
    }

    private void kbtnPrintDialog_Click(object sender, EventArgs e)
    {
        var kpd = new KryptonPrintDialog();

        kpd.ShowDialog();
    }

    private void kbtnLongInput_Click(object sender, EventArgs e)
    {
        var data = new KryptonInputBoxData
        {
            Owner = this,
            Caption = "DPI-aware input",
            Prompt = "This prompt is long enough to wrap. The dialog grows with the text and stays inside the working area. The response box and the OK and Cancel buttons stay visible.",
            CueText = "Type a response"
        };

        KryptonInputBox.Show(data);
    }

    private void kbtnExceptionDialog_Click(object sender, EventArgs e)
    {
        KryptonExceptionDialog.Show(new InvalidOperationException("Sample exception used to check dialog DPI layout."), true, true);
    }
}