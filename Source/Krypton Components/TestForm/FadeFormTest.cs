#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2024 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace TestForm;

public partial class FadeFormTest : KryptonForm
{
    public FadeFormTest()
    {
        InitializeComponent();
        cbtnShowImage.Checked = true;

        FadeValues.FadingEnabled = true;
        FadeValues.FadeIn = true;
        FadeValues.FadeOut = true;
        FadeValues.FadeSpeed = FadeSpeedChoice.Normal;

        FadeInCompleted += (_, _) => lblOpacity.Text = $"Fade in complete ({Opacity:0.00})";
        FadeOutCompleted += (_, _) => lblOpacity.Text = $"Fade out complete ({Opacity:0.00})";
    }

    private void btnFadeOut_Click(object sender, EventArgs e) => FadeOut();

    private void btnFadeIn_Click(object sender, EventArgs e) => FadeIn();

    private void btnFadeOutAndClose_Click(object sender, EventArgs e) => FadeOutAndClose();

    private void btnOpenChild_Click(object sender, EventArgs e)
    {
        KryptonForm child = new KryptonForm
        {
            Text = @"Faded child form",
            Size = new Size(420, 240),
            StartPosition = FormStartPosition.CenterParent
        };
        child.FadeValues.FadingEnabled = true;
        child.FadeValues.FadeSpeed = FadeValues.FadeSpeed;
        child.FadeValues.CustomFadeSpeed = FadeValues.CustomFadeSpeed;

        KryptonLabel label = new KryptonLabel
        {
            Dock = DockStyle.Fill,
            Values =
            {
                Text = @"This KryptonForm uses FadeValues.FadingEnabled. Close it to fade out."
            }
        };
        child.Controls.Add(label);
        child.Show(this);
    }

    private void cbtnShowImage_Click(object sender, EventArgs e) => kryptonPictureBox1.Visible = cbtnShowImage.Checked;
}