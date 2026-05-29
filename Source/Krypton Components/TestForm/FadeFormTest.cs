#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved. 
 *  
 */
#endregion

using System.Threading;

namespace TestForm;

public partial class FadeFormTest : KryptonForm
{
    public FadeFormTest()
    {
        InitializeComponent();
        cbtnShowImage.Checked = true;
    }

    private void FadeFormTest_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = false;
    }

    private void btnFadeOut_Click(object sender, EventArgs e)
    {

        KryptonForm owner = this;
        double fraction = (double)nudOpacityFraction.Value / 100;
        int delay = (int)nudSleepDelay.Value;

        while (owner.Opacity > 0.0)
        {
            Thread.Sleep(delay);

            owner.Opacity -= fraction;
            lblOpacity.Text = owner.Opacity.ToString();
            owner.Refresh();
        }

        MessageBox.Show("Done");

        owner.Opacity = 1;

    }

    private void btnFadeIn_Click(object sender, EventArgs e)
    {

        KryptonForm owner = this;
        owner.Opacity = 0;

        double fraction = (double)nudOpacityFraction.Value / 100;
        int delay = (int)nudSleepDelay.Value;

        while (owner.Opacity < 1)
        {
            Thread.Sleep(delay);

            owner.Opacity += fraction;
            lblOpacity.Text = owner.Opacity.ToString();
            owner.Refresh();

        }
        MessageBox.Show("Done1");

        owner.Opacity = 1;
    }

    private void cbtnShowImage_Click(object sender, EventArgs e)
    {
        kryptonPictureBox1.Visible = cbtnShowImage.Checked;
    }
}