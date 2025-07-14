#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class SplitContainerTest : KryptonForm
{
    public SplitContainerTest()
    {
        AddRtlToggleButton();
        AddSplitContainers();
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

    private void AddSplitContainers()
    {
        var panel = new Krypton.Toolkit.KryptonPanel
        {
            Location = new System.Drawing.Point(13, 50),
            Size = new System.Drawing.Size(600, 400),
            BorderStyle = BorderStyle.FixedSingle
        };

        // Group 1: Vertical Split Container
        var lblGroup1 = new Label
        {
            Text = "Vertical Split Container:",
            Location = new System.Drawing.Point(10, 10),
            Size = new System.Drawing.Size(150, 20),
            Font = new Font(Font.FontFamily, 9, FontStyle.Bold)
        };
        panel.Controls.Add(lblGroup1);

        var splitContainer1 = new Krypton.Toolkit.KryptonSplitContainer
        {
            Location = new System.Drawing.Point(10, 35),
            Size = new System.Drawing.Size(280, 150),
            Orientation = Orientation.Vertical,
            SplitterDistance = 100
        };

        // Add content to panels
        var panel1Label1 = new Label
        {
            Text = "Left Panel",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.LightBlue
        };
        splitContainer1.Panel1.Controls.Add(panel1Label1);

        var panel2Label1 = new Label
        {
            Text = "Right Panel",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.LightGreen
        };
        splitContainer1.Panel2.Controls.Add(panel2Label1);

        panel.Controls.Add(splitContainer1);

        // Group 2: Horizontal Split Container
        var lblGroup2 = new Label
        {
            Text = "Horizontal Split Container:",
            Location = new System.Drawing.Point(310, 10),
            Size = new System.Drawing.Size(150, 20),
            Font = new Font(Font.FontFamily, 9, FontStyle.Bold)
        };
        panel.Controls.Add(lblGroup2);

        var splitContainer2 = new Krypton.Toolkit.KryptonSplitContainer
        {
            Location = new System.Drawing.Point(310, 35),
            Size = new System.Drawing.Size(280, 150),
            Orientation = Orientation.Horizontal,
            SplitterDistance = 75
        };

        // Add content to panels
        var panel1Label2 = new Label
        {
            Text = "Top Panel",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.LightCoral
        };
        splitContainer2.Panel1.Controls.Add(panel1Label2);

        var panel2Label2 = new Label
        {
            Text = "Bottom Panel",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.LightYellow
        };
        splitContainer2.Panel2.Controls.Add(panel2Label2);

        panel.Controls.Add(splitContainer2);

        // Group 3: Fixed Panel Split Container
        var lblGroup3 = new Label
        {
            Text = "Fixed Panel Split Container:",
            Location = new System.Drawing.Point(10, 200),
            Size = new System.Drawing.Size(150, 20),
            Font = new Font(Font.FontFamily, 9, FontStyle.Bold)
        };
        panel.Controls.Add(lblGroup3);

        var splitContainer3 = new Krypton.Toolkit.KryptonSplitContainer
        {
            Location = new System.Drawing.Point(10, 225),
            Size = new System.Drawing.Size(280, 150),
            Orientation = Orientation.Vertical,
            FixedPanel = FixedPanel.Panel1,
            SplitterDistance = 80
        };

        // Add content to panels
        var panel1Label3 = new Label
        {
            Text = "Fixed Panel",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.LightSteelBlue
        };
        splitContainer3.Panel1.Controls.Add(panel1Label3);

        var panel2Label3 = new Label
        {
            Text = "Resizable Panel",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.LightPink
        };
        splitContainer3.Panel2.Controls.Add(panel2Label3);

        panel.Controls.Add(splitContainer3);

        // Group 4: Nested Split Containers
        var lblGroup4 = new Label
        {
            Text = "Nested Split Containers:",
            Location = new System.Drawing.Point(310, 200),
            Size = new System.Drawing.Size(150, 20),
            Font = new Font(Font.FontFamily, 9, FontStyle.Bold)
        };
        panel.Controls.Add(lblGroup4);

        var outerSplit = new Krypton.Toolkit.KryptonSplitContainer
        {
            Location = new System.Drawing.Point(310, 225),
            Size = new System.Drawing.Size(280, 150),
            Orientation = Orientation.Vertical,
            SplitterDistance = 140
        };

        var innerSplit = new Krypton.Toolkit.KryptonSplitContainer
        {
            Dock = DockStyle.Fill,
            Orientation = Orientation.Horizontal,
            SplitterDistance = 75
        };

        // Add content to nested panels
        var outerPanel1Label = new Label
        {
            Text = "Outer Left",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.LightSalmon
        };
        outerSplit.Panel1.Controls.Add(outerPanel1Label);

        var innerPanel1Label = new Label
        {
            Text = "Inner Top",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.LightSkyBlue
        };
        innerSplit.Panel1.Controls.Add(innerPanel1Label);

        var innerPanel2Label = new Label
        {
            Text = "Inner Bottom",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.LightGoldenrodYellow
        };
        innerSplit.Panel2.Controls.Add(innerPanel2Label);

        outerSplit.Panel2.Controls.Add(innerSplit);
        panel.Controls.Add(outerSplit);

        Controls.Add(panel);

        // Add status label
        var lblStatus = new Label
        {
            Text = "Toggle RTL to test split container panel ordering and splitter positioning",
            Location = new System.Drawing.Point(13, 470),
            Size = new System.Drawing.Size(400, 20),
            Font = new Font(Font.FontFamily, 8),
            ForeColor = Color.Gray
        };
        Controls.Add(lblStatus);
    }
} 