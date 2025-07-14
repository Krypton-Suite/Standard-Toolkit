#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class PropertyGridTest : KryptonForm
{
    public PropertyGridTest()
    {
        InitializeComponent();
        AddRtlToggleButton();
    }

    private void AddRtlToggleButton()
    {
        var btnToggleRtl = new Krypton.Toolkit.KryptonButton
        {
            Name = "btnToggleRtl",
            Text = $"RTL: {(RightToLeft == RightToLeft.Yes ? "ON" : "OFF")}",
            Location = new System.Drawing.Point(13, 530),
            Size = new System.Drawing.Size(120, 30),
            Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left
        };
        btnToggleRtl.Click += BtnToggleRtl_Click;
        kryptonPanel1.Controls.Add(btnToggleRtl);
    }

    private void BtnToggleRtl_Click(object sender, EventArgs e)
    {
        // Toggle RTL settings for the form and property grid
        RightToLeft = RightToLeft == RightToLeft.Yes ? RightToLeft.No : RightToLeft.Yes;
        RightToLeftLayout = !RightToLeftLayout;
        kpgExample.RightToLeft = RightToLeft;
        kpgExample.RightToLeftLayout = RightToLeftLayout;
        // Update button text
        var btn = Controls["btnToggleRtl"] as Krypton.Toolkit.KryptonButton;
        if (btn != null)
            btn.Text = $"RTL: {(RightToLeft == RightToLeft.Yes ? "ON" : "OFF")}";
    }
}