#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class RadioButtonTest : KryptonForm
{
    public RadioButtonTest()
    {
        AddRtlToggleButton();
        AddRadioButtons();
    }

    private void AddRtlToggleButton()
    {
        var btnToggleRtl = new Krypton.Toolkit.KryptonButton
        {
            Name = "btnToggleRtl",
            Text = $"RTL: {(RightToLeft == RightToLeft.Yes ? "ON" : "OFF")}",
            Location = new System.Drawing.Point(13, 13),
            Size = new System.Drawing.Size(100, 30)
        };
        btnToggleRtl.Click += (sender, e) =>
        {
            RightToLeft = RightToLeft == RightToLeft.Yes ? RightToLeft.No : RightToLeft.Yes;
            btnToggleRtl.Text = $"RTL: {(RightToLeft == RightToLeft.Yes ? "ON" : "OFF")}";
        };
        Controls.Add(btnToggleRtl);
    }

    private void AddRadioButtons()
    {
        var panel = new Krypton.Toolkit.KryptonPanel
        {
            Location = new System.Drawing.Point(13, 50),
            Size = new System.Drawing.Size(400, 300),
            BorderStyle = BorderStyle.FixedSingle
        };

        // Group 1: Standard radio buttons
        var lblGroup1 = new Label
        {
            Text = "Standard Radio Buttons:",
            Location = new System.Drawing.Point(10, 10),
            Size = new System.Drawing.Size(150, 20),
            Font = new Font(Font.FontFamily, 9, FontStyle.Bold)
        };
        panel.Controls.Add(lblGroup1);

        var radio1 = new Krypton.Toolkit.KryptonRadioButton
        {
            Text = "Option 1",
            Location = new System.Drawing.Point(10, 35),
            Size = new System.Drawing.Size(100, 25),
            Checked = true
        };
        panel.Controls.Add(radio1);

        var radio2 = new Krypton.Toolkit.KryptonRadioButton
        {
            Text = "Option 2",
            Location = new System.Drawing.Point(10, 60),
            Size = new System.Drawing.Size(100, 25)
        };
        panel.Controls.Add(radio2);

        var radio3 = new Krypton.Toolkit.KryptonRadioButton
        {
            Text = "Option 3",
            Location = new System.Drawing.Point(10, 85),
            Size = new System.Drawing.Size(100, 25)
        };
        panel.Controls.Add(radio3);

        // Group 2: Different check positions
        var lblGroup2 = new Label
        {
            Text = "Different Check Positions:",
            Location = new System.Drawing.Point(200, 10),
            Size = new System.Drawing.Size(150, 20),
            Font = new Font(Font.FontFamily, 9, FontStyle.Bold)
        };
        panel.Controls.Add(lblGroup2);

        var radioRight = new Krypton.Toolkit.KryptonRadioButton
        {
            Text = "Check on Right",
            Location = new System.Drawing.Point(200, 35),
            Size = new System.Drawing.Size(120, 25),
            CheckPosition = Krypton.Toolkit.VisualOrientation.Right
        };
        panel.Controls.Add(radioRight);

        var radioTop = new Krypton.Toolkit.KryptonRadioButton
        {
            Text = "Check on Top",
            Location = new System.Drawing.Point(200, 60),
            Size = new System.Drawing.Size(120, 25),
            CheckPosition = Krypton.Toolkit.VisualOrientation.Top
        };
        panel.Controls.Add(radioTop);

        var radioBottom = new Krypton.Toolkit.KryptonRadioButton
        {
            Text = "Check on Bottom",
            Location = new System.Drawing.Point(200, 85),
            Size = new System.Drawing.Size(120, 25),
            CheckPosition = Krypton.Toolkit.VisualOrientation.Bottom
        };
        panel.Controls.Add(radioBottom);

        // Group 3: Different orientations
        var lblGroup3 = new Label
        {
            Text = "Different Orientations:",
            Location = new System.Drawing.Point(10, 130),
            Size = new System.Drawing.Size(150, 20),
            Font = new Font(Font.FontFamily, 9, FontStyle.Bold)
        };
        panel.Controls.Add(lblGroup3);

        var radioLeft = new Krypton.Toolkit.KryptonRadioButton
        {
            Text = "Left Orientation",
            Location = new System.Drawing.Point(10, 155),
            Size = new System.Drawing.Size(120, 25),
            Orientation = Krypton.Toolkit.VisualOrientation.Left
        };
        panel.Controls.Add(radioLeft);

        var radioTopOrient = new Krypton.Toolkit.KryptonRadioButton
        {
            Text = "Top Orientation",
            Location = new System.Drawing.Point(10, 180),
            Size = new System.Drawing.Size(120, 25),
            Orientation = Krypton.Toolkit.VisualOrientation.Top
        };
        panel.Controls.Add(radioTopOrient);

        var radioRightOrient = new Krypton.Toolkit.KryptonRadioButton
        {
            Text = "Right Orientation",
            Location = new System.Drawing.Point(10, 205),
            Size = new System.Drawing.Size(120, 25),
            Orientation = Krypton.Toolkit.VisualOrientation.Right
        };
        panel.Controls.Add(radioRightOrient);

        var radioBottomOrient = new Krypton.Toolkit.KryptonRadioButton
        {
            Text = "Bottom Orientation",
            Location = new System.Drawing.Point(10, 230),
            Size = new System.Drawing.Size(120, 25),
            Orientation = Krypton.Toolkit.VisualOrientation.Bottom
        };
        panel.Controls.Add(radioBottomOrient);

        // Group 4: Complex combinations
        var lblGroup4 = new Label
        {
            Text = "Complex Combinations:",
            Location = new System.Drawing.Point(200, 130),
            Size = new System.Drawing.Size(150, 20),
            Font = new Font(Font.FontFamily, 9, FontStyle.Bold)
        };
        panel.Controls.Add(lblGroup4);

        var radioComplex1 = new Krypton.Toolkit.KryptonRadioButton
        {
            Text = "Right Check + Left Orient",
            Location = new System.Drawing.Point(200, 155),
            Size = new System.Drawing.Size(150, 25),
            CheckPosition = Krypton.Toolkit.VisualOrientation.Right,
            Orientation = Krypton.Toolkit.VisualOrientation.Left
        };
        panel.Controls.Add(radioComplex1);

        var radioComplex2 = new Krypton.Toolkit.KryptonRadioButton
        {
            Text = "Top Check + Bottom Orient",
            Location = new System.Drawing.Point(200, 180),
            Size = new System.Drawing.Size(150, 25),
            CheckPosition = Krypton.Toolkit.VisualOrientation.Top,
            Orientation = Krypton.Toolkit.VisualOrientation.Bottom
        };
        panel.Controls.Add(radioComplex2);

        var radioComplex3 = new Krypton.Toolkit.KryptonRadioButton
        {
            Text = "Bottom Check + Right Orient",
            Location = new System.Drawing.Point(200, 205),
            Size = new System.Drawing.Size(150, 25),
            CheckPosition = Krypton.Toolkit.VisualOrientation.Bottom,
            Orientation = Krypton.Toolkit.VisualOrientation.Right
        };
        panel.Controls.Add(radioComplex3);

        Controls.Add(panel);

        // Add status label
        var lblStatus = new Label
        {
            Text = "Toggle RTL to test radio button positioning and layout",
            Location = new System.Drawing.Point(13, 370),
            Size = new System.Drawing.Size(400, 20),
            Font = new Font(Font.FontFamily, 8),
            ForeColor = Color.Gray
        };
        Controls.Add(lblStatus);
    }
} 