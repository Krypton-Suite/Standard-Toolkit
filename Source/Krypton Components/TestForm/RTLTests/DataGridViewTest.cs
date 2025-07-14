#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

using System.Collections.Generic;

namespace Krypton.Toolkit.Suite.Core.Standard.Toolkit.TestForm;

public partial class DataGridViewTest : KryptonForm
{
    private KryptonDataGridView kryptonDataGridView1;
    private KryptonButton kbtnToggleRTL;
    private KryptonLabel kryptonLabel1;
    private KryptonPanel kryptonPanel1;

    public DataGridViewTest()
    {
        InitializeComponent();
        InitializeDataGridView();
    }

    private void InitializeComponent()
    {
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnToggleRTL = new Krypton.Toolkit.KryptonButton();
            this.kryptonDataGridView1 = new Krypton.Toolkit.KryptonDataGridView();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.kbtnToggleRTL);
            this.kryptonPanel1.Controls.Add(this.kryptonDataGridView1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(975, 623);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnToggleRTL
            // 
            this.kbtnToggleRTL.Location = new System.Drawing.Point(13, 557);
            this.kbtnToggleRTL.Name = "kbtnToggleRTL";
            this.kbtnToggleRTL.Size = new System.Drawing.Size(226, 25);
            this.kbtnToggleRTL.TabIndex = 1;
            this.kbtnToggleRTL.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnToggleRTL.Values.Text = "Toggle RTL";
            this.kbtnToggleRTL.Click += new System.EventHandler(this.kbtnToggleRTL_Click);
            // 
            // kryptonDataGridView1
            // 
            this.kryptonDataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.kryptonDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kryptonDataGridView1.Location = new System.Drawing.Point(12, 12);
            this.kryptonDataGridView1.Name = "kryptonDataGridView1";
            this.kryptonDataGridView1.Size = new System.Drawing.Size(949, 538);
            this.kryptonDataGridView1.TabIndex = 0;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(246, 557);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(88, 20);
            this.kryptonLabel1.TabIndex = 2;
            this.kryptonLabel1.Values.Text = "kryptonLabel1";
            // 
            // DataGridViewTest
            // 
            this.ClientSize = new System.Drawing.Size(975, 623);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "DataGridViewTest";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridView1)).EndInit();
            this.ResumeLayout(false);

    }

    private void InitializeDataGridView()
    {
        // Create sample data
        var data = new List<TestData>
        {
            new TestData { Id = 1, Name = "Item 1", Description = "First item", Category = "Category A", Value = 100.50m },
            new TestData { Id = 2, Name = "Item 2", Description = "Second item", Category = "Category B", Value = 250.75m },
            new TestData { Id = 3, Name = "Item 3", Description = "Third item", Category = "Category A", Value = 75.25m },
            new TestData { Id = 4, Name = "Item 4", Description = "Fourth item", Category = "Category C", Value = 300.00m },
            new TestData { Id = 5, Name = "Item 5", Description = "Fifth item", Category = "Category B", Value = 125.80m }
        };

        // Bind data to DataGridView
        kryptonDataGridView1.DataSource = data;

        // Configure columns for better RTL testing
        kryptonDataGridView1.Columns[0].HeaderText = "ID";
        kryptonDataGridView1.Columns[1].HeaderText = "Name";
        kryptonDataGridView1.Columns[2].HeaderText = "Description";
        kryptonDataGridView1.Columns[3].HeaderText = "Category";
        kryptonDataGridView1.Columns[4].HeaderText = "Value";

        // Set column widths
        kryptonDataGridView1.Columns[0].Width = 50;
        kryptonDataGridView1.Columns[1].Width = 120;
        kryptonDataGridView1.Columns[2].Width = 200;
        kryptonDataGridView1.Columns[3].Width = 100;
        kryptonDataGridView1.Columns[4].Width = 80;

        // Enable sorting
        kryptonDataGridView1.AllowUserToAddRows = false;
        kryptonDataGridView1.AllowUserToDeleteRows = false;
        kryptonDataGridView1.ReadOnly = true;
        kryptonDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    }

    private void kbtnToggleRTL_Click(object sender, EventArgs e)
    {
        // Toggle RTL settings
        RightToLeft = RightToLeft == RightToLeft.Yes ? RightToLeft.No : RightToLeft.Yes;
        RightToLeftLayout = !RightToLeftLayout;

        // Update display
        UpdateRtlDisplay();
    }

    private void UpdateRtlDisplay()
    {
        // Update button text
        kbtnToggleRTL.Text = $"RTL: {(RightToLeft == RightToLeft.Yes ? "ON" : "OFF")}";
        
        // Update label to show RTL state
        kryptonLabel1.Values.Text = $"DataGridView RTL Test - Mode: {(RightToLeft == RightToLeft.Yes ? "RTL" : "LTR")}";
        
        // Update form title
        Text = $"DataGridView RTL Test - {(RightToLeft == RightToLeft.Yes ? "RTL" : "LTR")} Mode";
    }

    public class TestData
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Value { get; set; }
    }
}