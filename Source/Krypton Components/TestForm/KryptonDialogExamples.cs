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
}