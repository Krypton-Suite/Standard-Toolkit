#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class KryptonTaskDialogDemoForm : KryptonForm
{
    private KryptonTaskDialog _ktd;
    private int _dialogWidth = 750;

    public KryptonTaskDialogDemoForm()
    {
        InitializeComponent();

        _ktd = new(_dialogWidth);
        propGridKtd.SelectedObject = _ktd;
    }

    private void btnShowDialog_Click(object sender, EventArgs e)
    {
        _ktd.ShowDialog();
    }

    private void btnShowDialogOwner_Click(object sender, EventArgs e)
    {
        _ktd.ShowDialog(this);
    }

    private void btnShow_Click(object sender, EventArgs e)
    {
        _ktd.Show();
    }

    private void btnShowOwner_Click(object sender, EventArgs e)
    {
        _ktd.Show(this);
    }

    private void btnModelessProgressDialog1_Click(object sender, EventArgs e)
    {
        ModelessProgressDialog1();
    }

    private void btnMessageBox1_Click(object sender, EventArgs e)
    {
        MessageBox1("Bring on the message.", 650);
    }

    private void btnMessageBox2_Click(object sender, EventArgs e)
    {
        MessageBox2("Overheating Detected.",
            "Your system is overheating.\nIf the system is not shutdown at once damage can occur.",
            KryptonTaskDialogIconType.PowerOff,
            KryptonTaskDialogCommonButtonTypes.OK | KryptonTaskDialogCommonButtonTypes.Abort,
            KryptonTaskDialogCommonButtonTypes.OK,
            KryptonTaskDialogCommonButtonTypes.Abort,
            700);
    }

    private void FreeWheeler2DataGridView_Click(object sender, EventArgs e)
    {
        FreeWheeler1DataGridView(800);
    }

    private void btnFreeWheeler1CheckSet_Click(object sender, EventArgs e)
    {
        FreeWheeler2CheckedSetButtons();
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
        _ktd = new(_dialogWidth);
    }

    private void btnContent1_Click(object sender, EventArgs e)
    {
        KryptonTaskDialogDemoFormContent();
    }
}
