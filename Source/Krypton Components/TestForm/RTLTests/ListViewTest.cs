#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class ListViewTest : KryptonForm
{
    public ListViewTest()
    {
        AddRtlToggleButton();
        AddListViews();
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

    private void AddListViews()
    {
        var panel = new Krypton.Toolkit.KryptonPanel
        {
            Location = new System.Drawing.Point(13, 50),
            Size = new System.Drawing.Size(600, 400),
            BorderStyle = BorderStyle.FixedSingle
        };

        // Group 1: Details View with Columns
        var lblGroup1 = new Label
        {
            Text = "Details View (Columns):",
            Location = new System.Drawing.Point(10, 10),
            Size = new System.Drawing.Size(150, 20),
            Font = new Font(Font.FontFamily, 9, FontStyle.Bold)
        };
        panel.Controls.Add(lblGroup1);

        var listView1 = new Krypton.Toolkit.KryptonListView
        {
            Location = new System.Drawing.Point(10, 35),
            Size = new System.Drawing.Size(280, 150),
            View = View.Details,
            FullRowSelect = true,
            GridLines = true
        };

        // Add columns
        listView1.Columns.Add("Name", 100);
        listView1.Columns.Add("Type", 80);
        listView1.Columns.Add("Size", 80);
        listView1.Columns.Add("Date", 100);

        // Add items
        listView1.Items.Add(new ListViewItem(new[] { "Document1.txt", "Text File", "1.2 KB", "2024-01-15" }));
        listView1.Items.Add(new ListViewItem(new[] { "Image.jpg", "Image", "2.5 MB", "2024-01-14" }));
        listView1.Items.Add(new ListViewItem(new[] { "Report.pdf", "PDF", "850 KB", "2024-01-13" }));
        listView1.Items.Add(new ListViewItem(new[] { "Data.xlsx", "Spreadsheet", "1.8 MB", "2024-01-12" }));
        listView1.Items.Add(new ListViewItem(new[] { "Code.cs", "Source Code", "15 KB", "2024-01-11" }));

        panel.Controls.Add(listView1);

        // Group 2: List View
        var lblGroup2 = new Label
        {
            Text = "List View:",
            Location = new System.Drawing.Point(310, 10),
            Size = new System.Drawing.Size(150, 20),
            Font = new Font(Font.FontFamily, 9, FontStyle.Bold)
        };
        panel.Controls.Add(lblGroup2);

        var listView2 = new Krypton.Toolkit.KryptonListView
        {
            Location = new System.Drawing.Point(310, 35),
            Size = new System.Drawing.Size(150, 150),
            View = View.List
        };

        listView2.Items.Add("Item 1");
        listView2.Items.Add("Item 2");
        listView2.Items.Add("Item 3");
        listView2.Items.Add("Item 4");
        listView2.Items.Add("Item 5");

        panel.Controls.Add(listView2);

        // Group 3: Large Icons View
        var lblGroup3 = new Label
        {
            Text = "Large Icons View:",
            Location = new System.Drawing.Point(10, 200),
            Size = new System.Drawing.Size(150, 20),
            Font = new Font(Font.FontFamily, 9, FontStyle.Bold)
        };
        panel.Controls.Add(lblGroup3);

        var listView3 = new Krypton.Toolkit.KryptonListView
        {
            Location = new System.Drawing.Point(10, 225),
            Size = new System.Drawing.Size(200, 150),
            View = View.LargeIcon
        };

        listView3.Items.Add("File 1");
        listView3.Items.Add("File 2");
        listView3.Items.Add("File 3");

        panel.Controls.Add(listView3);

        // Group 4: Small Icons View
        var lblGroup4 = new Label
        {
            Text = "Small Icons View:",
            Location = new System.Drawing.Point(230, 200),
            Size = new System.Drawing.Size(150, 20),
            Font = new Font(Font.FontFamily, 9, FontStyle.Bold)
        };
        panel.Controls.Add(lblGroup4);

        var listView4 = new Krypton.Toolkit.KryptonListView
        {
            Location = new System.Drawing.Point(230, 225),
            Size = new System.Drawing.Size(200, 150),
            View = View.SmallIcon
        };

        listView4.Items.Add("Item A");
        listView4.Items.Add("Item B");
        listView4.Items.Add("Item C");
        listView4.Items.Add("Item D");
        listView4.Items.Add("Item E");
        listView4.Items.Add("Item F");

        panel.Controls.Add(listView4);

        // Group 5: Tile View
        var lblGroup5 = new Label
        {
            Text = "Tile View:",
            Location = new System.Drawing.Point(450, 200),
            Size = new System.Drawing.Size(150, 20),
            Font = new Font(Font.FontFamily, 9, FontStyle.Bold)
        };
        panel.Controls.Add(lblGroup5);

        var listView5 = new Krypton.Toolkit.KryptonListView
        {
            Location = new System.Drawing.Point(450, 225),
            Size = new System.Drawing.Size(140, 150),
            View = View.Tile
        };

        listView5.Items.Add("Tile Item 1");
        listView5.Items.Add("Tile Item 2");
        listView5.Items.Add("Tile Item 3");

        panel.Controls.Add(listView5);

        Controls.Add(panel);

        // Add status label
        var lblStatus = new Label
        {
            Text = "Toggle RTL to test ListView layout and column ordering",
            Location = new System.Drawing.Point(13, 470),
            Size = new System.Drawing.Size(400, 20),
            Font = new Font(Font.FontFamily, 8),
            ForeColor = Color.Gray
        };
        Controls.Add(lblStatus);
    }
} 