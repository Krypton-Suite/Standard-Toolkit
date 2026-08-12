#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class AsyncFormMethodsTest : KryptonForm
{
    public AsyncFormMethodsTest()
    {
        InitializeComponent();
#if !NET9_0_OR_GREATER
        kbtnShowDialogAsync.Enabled = false;
        kbtnMessageBoxShowAsync.Enabled = false;
        kbtnTaskDialogShowDialogAsync.Enabled = false;
        klblResult.Text = "Async form APIs require net9.0-windows or newer.";
#endif
    }

#if NET9_0_OR_GREATER
    private async void kbtnShowDialogAsync_Click(object sender, EventArgs e)
    {
        using var dialog = new KryptonForm
        {
            Text = "ShowDialogAsync",
            StartPosition = FormStartPosition.CenterParent,
            ClientSize = new Size(320, 120),
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MaximizeBox = false,
            MinimizeBox = false,
            ShowInTaskbar = false
        };

        var ok = new KryptonButton
        {
            DialogResult = DialogResult.OK,
            Location = new Point(120, 40),
            Size = new Size(80, 30)
        };
        ok.Values.Text = "OK";
        dialog.Controls.Add(ok);
        dialog.AcceptButton = ok;

        var result = await dialog.ShowDialogAsync(this).ConfigureAwait(true);
        klblResult.Text = $"KryptonForm.ShowDialogAsync => {result}";
    }

    private async void kbtnMessageBoxShowAsync_Click(object sender, EventArgs e)
    {
        var result = await KryptonMessageBox.ShowAsync(this, "Async message box test.", "ShowAsync",
            KryptonMessageBoxButtons.OKCancel, KryptonMessageBoxIcon.Information).ConfigureAwait(true);
        klblResult.Text = $"KryptonMessageBox.ShowAsync => {result}";
    }

    private async void kbtnTaskDialogShowDialogAsync_Click(object sender, EventArgs e)
    {
        using var taskDialog = new KryptonTaskDialog();
        taskDialog.Heading.Text = "Async Task Dialog";
        taskDialog.Content.Text = "Shown via ShowDialogAsync.";
        taskDialog.FooterBar.CommonButtons.Buttons =
            KryptonTaskDialogCommonButtonTypes.OK | KryptonTaskDialogCommonButtonTypes.Cancel;
        taskDialog.FooterBar.CommonButtons.AcceptButton = KryptonTaskDialogCommonButtonTypes.OK;
        taskDialog.FooterBar.CommonButtons.CancelButton = KryptonTaskDialogCommonButtonTypes.Cancel;
        taskDialog.FooterBar.Visible = true;

        var result = await taskDialog.ShowDialogAsync(this).ConfigureAwait(true);
        klblResult.Text = $"KryptonTaskDialog.ShowDialogAsync => {result}";
    }
#else
    private void kbtnShowDialogAsync_Click(object sender, EventArgs e)
    {
    }

    private void kbtnMessageBoxShowAsync_Click(object sender, EventArgs e)
    {
    }

    private void kbtnTaskDialogShowDialogAsync_Click(object sender, EventArgs e)
    {
    }
#endif
}
