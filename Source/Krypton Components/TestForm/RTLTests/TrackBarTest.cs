#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class TrackBarTest : KryptonForm
{
    public TrackBarTest()
    {
        AddRtlToggleButton();
        AddTrackBars();
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
        Controls.Add(btnToggleRtl);
    }

    private void AddTrackBars()
    {
        // Horizontal TrackBar - Small
        var lblHorizontalSmall = new Krypton.Toolkit.KryptonLabel
        {
            Text = "Horizontal TrackBar (Small):",
            Location = new System.Drawing.Point(20, 20),
            Size = new System.Drawing.Size(200, 20)
        };
        Controls.Add(lblHorizontalSmall);

        var trackBarHorizontalSmall = new Krypton.Toolkit.KryptonTrackBar
        {
            Name = "trackBarHorizontalSmall",
            Location = new System.Drawing.Point(20, 50),
            Size = new System.Drawing.Size(300, 35),
            Orientation = System.Windows.Forms.Orientation.Horizontal,
            TrackBarSize = Krypton.Toolkit.PaletteTrackBarSize.Small,
            Minimum = 0,
            Maximum = 100,
            Value = 50,
            TickStyle = System.Windows.Forms.TickStyle.BottomRight,
            TickFrequency = 10
        };
        Controls.Add(trackBarHorizontalSmall);

        // Horizontal TrackBar - Medium
        var lblHorizontalMedium = new Krypton.Toolkit.KryptonLabel
        {
            Text = "Horizontal TrackBar (Medium):",
            Location = new System.Drawing.Point(20, 100),
            Size = new System.Drawing.Size(200, 20)
        };
        Controls.Add(lblHorizontalMedium);

        var trackBarHorizontalMedium = new Krypton.Toolkit.KryptonTrackBar
        {
            Name = "trackBarHorizontalMedium",
            Location = new System.Drawing.Point(20, 130),
            Size = new System.Drawing.Size(300, 35),
            Orientation = System.Windows.Forms.Orientation.Horizontal,
            TrackBarSize = Krypton.Toolkit.PaletteTrackBarSize.Medium,
            Minimum = 0,
            Maximum = 100,
            Value = 75,
            TickStyle = System.Windows.Forms.TickStyle.Both,
            TickFrequency = 20
        };
        Controls.Add(trackBarHorizontalMedium);

        // Horizontal TrackBar - Large
        var lblHorizontalLarge = new Krypton.Toolkit.KryptonLabel
        {
            Text = "Horizontal TrackBar (Large):",
            Location = new System.Drawing.Point(20, 180),
            Size = new System.Drawing.Size(200, 20)
        };
        Controls.Add(lblHorizontalLarge);

        var trackBarHorizontalLarge = new Krypton.Toolkit.KryptonTrackBar
        {
            Name = "trackBarHorizontalLarge",
            Location = new System.Drawing.Point(20, 210),
            Size = new System.Drawing.Size(300, 35),
            Orientation = System.Windows.Forms.Orientation.Horizontal,
            TrackBarSize = Krypton.Toolkit.PaletteTrackBarSize.Large,
            Minimum = 0,
            Maximum = 100,
            Value = 25,
            TickStyle = System.Windows.Forms.TickStyle.TopLeft,
            TickFrequency = 25
        };
        Controls.Add(trackBarHorizontalLarge);

        // Vertical TrackBar - Small
        var lblVerticalSmall = new Krypton.Toolkit.KryptonLabel
        {
            Text = "Vertical TrackBar (Small):",
            Location = new System.Drawing.Point(350, 20),
            Size = new System.Drawing.Size(200, 20)
        };
        Controls.Add(lblVerticalSmall);

        var trackBarVerticalSmall = new Krypton.Toolkit.KryptonTrackBar
        {
            Name = "trackBarVerticalSmall",
            Location = new System.Drawing.Point(350, 50),
            Size = new System.Drawing.Size(35, 200),
            Orientation = System.Windows.Forms.Orientation.Vertical,
            TrackBarSize = Krypton.Toolkit.PaletteTrackBarSize.Small,
            Minimum = 0,
            Maximum = 100,
            Value = 30,
            TickStyle = System.Windows.Forms.TickStyle.BottomRight,
            TickFrequency = 10
        };
        Controls.Add(trackBarVerticalSmall);

        // Vertical TrackBar - Medium
        var lblVerticalMedium = new Krypton.Toolkit.KryptonLabel
        {
            Text = "Vertical TrackBar (Medium):",
            Location = new System.Drawing.Point(400, 20),
            Size = new System.Drawing.Size(200, 20)
        };
        Controls.Add(lblVerticalMedium);

        var trackBarVerticalMedium = new Krypton.Toolkit.KryptonTrackBar
        {
            Name = "trackBarVerticalMedium",
            Location = new System.Drawing.Point(400, 50),
            Size = new System.Drawing.Size(35, 200),
            Orientation = System.Windows.Forms.Orientation.Vertical,
            TrackBarSize = Krypton.Toolkit.PaletteTrackBarSize.Medium,
            Minimum = 0,
            Maximum = 100,
            Value = 60,
            TickStyle = System.Windows.Forms.TickStyle.Both,
            TickFrequency = 20
        };
        Controls.Add(trackBarVerticalMedium);

        // Vertical TrackBar - Large
        var lblVerticalLarge = new Krypton.Toolkit.KryptonLabel
        {
            Text = "Vertical TrackBar (Large):",
            Location = new System.Drawing.Point(450, 20),
            Size = new System.Drawing.Size(200, 20)
        };
        Controls.Add(lblVerticalLarge);

        var trackBarVerticalLarge = new Krypton.Toolkit.KryptonTrackBar
        {
            Name = "trackBarVerticalLarge",
            Location = new System.Drawing.Point(450, 50),
            Size = new System.Drawing.Size(35, 200),
            Orientation = System.Windows.Forms.Orientation.Vertical,
            TrackBarSize = Krypton.Toolkit.PaletteTrackBarSize.Large,
            Minimum = 0,
            Maximum = 100,
            Value = 90,
            TickStyle = System.Windows.Forms.TickStyle.TopLeft,
            TickFrequency = 25
        };
        Controls.Add(trackBarVerticalLarge);

        // Status Label
        var lblStatus = new Krypton.Toolkit.KryptonLabel
        {
            Name = "lblStatus",
            Text = $"RTL Mode: {(RightToLeft == RightToLeft.Yes ? "Enabled" : "Disabled")} - Test KryptonTrackBar RTL Support",
            Location = new System.Drawing.Point(20, 270),
            Size = new System.Drawing.Size(500, 20),
            Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left
        };
        Controls.Add(lblStatus);

        // Value Display Labels
        var lblValue1 = new Krypton.Toolkit.KryptonLabel
        {
            Name = "lblValue1",
            Text = "Value: 50",
            Location = new System.Drawing.Point(330, 50),
            Size = new System.Drawing.Size(100, 20)
        };
        Controls.Add(lblValue1);

        var lblValue2 = new Krypton.Toolkit.KryptonLabel
        {
            Name = "lblValue2",
            Text = "Value: 75",
            Location = new System.Drawing.Point(330, 130),
            Size = new System.Drawing.Size(100, 20)
        };
        Controls.Add(lblValue2);

        var lblValue3 = new Krypton.Toolkit.KryptonLabel
        {
            Name = "lblValue3",
            Text = "Value: 25",
            Location = new System.Drawing.Point(330, 210),
            Size = new System.Drawing.Size(100, 20)
        };
        Controls.Add(lblValue3);

        // Wire up value change events
        trackBarHorizontalSmall.ValueChanged += (s, e) => lblValue1.Text = $"Value: {trackBarHorizontalSmall.Value}";
        trackBarHorizontalMedium.ValueChanged += (s, e) => lblValue2.Text = $"Value: {trackBarHorizontalMedium.Value}";
        trackBarHorizontalLarge.ValueChanged += (s, e) => lblValue3.Text = $"Value: {trackBarHorizontalLarge.Value}";
    }

    private void BtnToggleRtl_Click(object sender, EventArgs e)
    {
        // Toggle RTL settings for the form and all track bars
        RightToLeft = RightToLeft == RightToLeft.Yes ? RightToLeft.No : RightToLeft.Yes;

        // Update all track bars
        foreach (Control control in Controls)
        {
            if (control is Krypton.Toolkit.KryptonTrackBar trackBar)
            {
                trackBar.RightToLeft = RightToLeft;
            }
        }

        // Update status label
        var lblStatus = Controls["lblStatus"] as Krypton.Toolkit.KryptonLabel;
        if (lblStatus != null)
            lblStatus.Text = $"RTL Mode: {(RightToLeft == RightToLeft.Yes ? "Enabled" : "Disabled")} - Test KryptonTrackBar RTL Support";

        // Update button text
        var btn = Controls["btnToggleRtl"] as Krypton.Toolkit.KryptonButton;
        if (btn != null)
            btn.Text = $"RTL: {(RightToLeft == RightToLeft.Yes ? "ON" : "OFF")}";
    }
} 