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

namespace Krypton.Toolkit.Suite.Core.Standard.Toolkit.TestForm;

public partial class DomainUpDownTest : KryptonForm
{
    private KryptonDomainUpDown kryptonDomainUpDown5;
    private KryptonDomainUpDown kryptonDomainUpDown4;
    private KryptonDomainUpDown kryptonDomainUpDown3;
    private KryptonDomainUpDown kryptonDomainUpDown2;
    private KryptonDomainUpDown kryptonDomainUpDown1;
    private KryptonButton kbtnResetValues;
    private KryptonLabel klblRtlStatus;
    private KryptonButton kbtnToggleRtl;
    private KryptonButton kbtnAddItems;
    private KryptonButton kbtnTestUpDown;
    private KryptonPanel kryptonPanel1;

    public DomainUpDownTest()
    {
        InitializeComponent();
        InitializeDomainUpDowns();
    }

    private void InitializeDomainUpDowns()
    {
        // Configure different domain up/down configurations
        kryptonDomainUpDown1.Items.Clear();
        kryptonDomainUpDown1.Items.AddRange(new object[] { "Red", "Green", "Blue", "Yellow", "Purple", "Orange" });
        kryptonDomainUpDown1.SelectedIndex = 0;

        kryptonDomainUpDown2.Items.Clear();
        kryptonDomainUpDown2.Items.AddRange(new object[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" });
        kryptonDomainUpDown2.SelectedIndex = 0;

        kryptonDomainUpDown3.Items.Clear();
        kryptonDomainUpDown3.Items.AddRange(new object[] { "January", "February", "March", "April", "May", "June", 
                                                           "July", "August", "September", "October", "November", "December" });
        kryptonDomainUpDown3.SelectedIndex = 0;

        kryptonDomainUpDown4.Items.Clear();
        kryptonDomainUpDown4.Items.AddRange(new object[] { "Small", "Medium", "Large", "Extra Large" });
        kryptonDomainUpDown4.SelectedIndex = 1;

        kryptonDomainUpDown5.Items.Clear();
        kryptonDomainUpDown5.Items.AddRange(new object[] { "Low", "Medium", "High", "Critical" });
        kryptonDomainUpDown5.SelectedIndex = 0;

        // Set initial RTL state
        UpdateRtlState();
    }

    private void kbtnToggleRtl_Click(object? sender, EventArgs e)
    {

    }

    private void UpdateRtlState()
    {
        bool isRtl = RightToLeft == RightToLeft.Yes;
        kbtnToggleRtl.Text = isRtl ? "Switch to LTR" : "Switch to RTL";
        
        // Update labels
        klblRtlStatus.Text = isRtl ? "RTL Mode Active" : "LTR Mode Active";
        klblRtlStatus.StateCommon.ShortText.Color1 = isRtl ? Color.Green : Color.Blue;
    }

    private void kbtnResetValues_Click(object? sender, EventArgs e)
    {
        // Reset all domain up/down values
        kryptonDomainUpDown1.SelectedIndex = 0;
        kryptonDomainUpDown2.SelectedIndex = 0;
        kryptonDomainUpDown3.SelectedIndex = 0;
        kryptonDomainUpDown4.SelectedIndex = 1;
        kryptonDomainUpDown5.SelectedIndex = 0;
    }

    private void kbtnTestUpDown_Click(object? sender, EventArgs e)
    {
        // Test the up/down functionality
        kryptonDomainUpDown1.UpButton();
        kryptonDomainUpDown2.DownButton();
        kryptonDomainUpDown3.UpButton();
        kryptonDomainUpDown4.DownButton();
        kryptonDomainUpDown5.UpButton();
    }

    private void kbtnAddItems_Click(object? sender, EventArgs e)
    {
        // Add some additional items to test with
        kryptonDomainUpDown1.Items.Add("Cyan");
        kryptonDomainUpDown1.Items.Add("Magenta");
        
        kryptonDomainUpDown2.Items.Add("Holiday");
        kryptonDomainUpDown2.Items.Add("Weekend");
        
        kryptonDomainUpDown3.Items.Add("Leap Year");
        kryptonDomainUpDown3.Items.Add("Season");
    }

    private void InitializeComponent()
    {
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnTestUpDown = new Krypton.Toolkit.KryptonButton();
            this.kbtnAddItems = new Krypton.Toolkit.KryptonButton();
            this.kbtnResetValues = new Krypton.Toolkit.KryptonButton();
            this.klblRtlStatus = new Krypton.Toolkit.KryptonLabel();
            this.kbtnToggleRtl = new Krypton.Toolkit.KryptonButton();
            this.kryptonDomainUpDown5 = new Krypton.Toolkit.KryptonDomainUpDown();
            this.kryptonDomainUpDown4 = new Krypton.Toolkit.KryptonDomainUpDown();
            this.kryptonDomainUpDown3 = new Krypton.Toolkit.KryptonDomainUpDown();
            this.kryptonDomainUpDown2 = new Krypton.Toolkit.KryptonDomainUpDown();
            this.kryptonDomainUpDown1 = new Krypton.Toolkit.KryptonDomainUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnTestUpDown);
            this.kryptonPanel1.Controls.Add(this.kbtnAddItems);
            this.kryptonPanel1.Controls.Add(this.kbtnResetValues);
            this.kryptonPanel1.Controls.Add(this.klblRtlStatus);
            this.kryptonPanel1.Controls.Add(this.kbtnToggleRtl);
            this.kryptonPanel1.Controls.Add(this.kryptonDomainUpDown5);
            this.kryptonPanel1.Controls.Add(this.kryptonDomainUpDown4);
            this.kryptonPanel1.Controls.Add(this.kryptonDomainUpDown3);
            this.kryptonPanel1.Controls.Add(this.kryptonDomainUpDown2);
            this.kryptonPanel1.Controls.Add(this.kryptonDomainUpDown1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(1101, 158);
            this.kryptonPanel1.TabIndex = 1;
            // 
            // kbtnTestUpDown
            // 
            this.kbtnTestUpDown.Location = new System.Drawing.Point(840, 124);
            this.kbtnTestUpDown.Name = "kbtnTestUpDown";
            this.kbtnTestUpDown.Size = new System.Drawing.Size(226, 25);
            this.kbtnTestUpDown.TabIndex = 12;
            this.kbtnTestUpDown.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnTestUpDown.Values.Text = "Test Up/Down";
            this.kbtnTestUpDown.Click += new System.EventHandler(this.kbtnTestUpDown_Click);
            // 
            // kbtnAddItems
            // 
            this.kbtnAddItems.Location = new System.Drawing.Point(608, 124);
            this.kbtnAddItems.Name = "kbtnAddItems";
            this.kbtnAddItems.Size = new System.Drawing.Size(226, 25);
            this.kbtnAddItems.TabIndex = 11;
            this.kbtnAddItems.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnAddItems.Values.Text = "Add Items";
            this.kbtnAddItems.Click += new System.EventHandler(this.kbtnAddItems_Click);
            // 
            // kbtnResetValues
            // 
            this.kbtnResetValues.Location = new System.Drawing.Point(375, 124);
            this.kbtnResetValues.Name = "kbtnResetValues";
            this.kbtnResetValues.Size = new System.Drawing.Size(226, 25);
            this.kbtnResetValues.TabIndex = 10;
            this.kbtnResetValues.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnResetValues.Values.Text = "Reset Values";
            this.kbtnResetValues.Click += new System.EventHandler(this.kbtnResetValues_Click);
            // 
            // klblRtlStatus
            // 
            this.klblRtlStatus.Location = new System.Drawing.Point(608, 93);
            this.klblRtlStatus.Name = "klblRtlStatus";
            this.klblRtlStatus.Size = new System.Drawing.Size(88, 20);
            this.klblRtlStatus.TabIndex = 9;
            this.klblRtlStatus.Values.Text = "kryptonLabel1";
            // 
            // kbtnToggleRtl
            // 
            this.kbtnToggleRtl.Location = new System.Drawing.Point(375, 93);
            this.kbtnToggleRtl.Name = "kbtnToggleRtl";
            this.kbtnToggleRtl.Size = new System.Drawing.Size(226, 25);
            this.kbtnToggleRtl.TabIndex = 8;
            this.kbtnToggleRtl.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnToggleRtl.Values.Text = "Toggle RTL";
            this.kbtnToggleRtl.Click += new System.EventHandler(this.kbtnToggleRtl_Click);
            // 
            // kryptonDomainUpDown5
            // 
            this.kryptonDomainUpDown5.Location = new System.Drawing.Point(12, 124);
            this.kryptonDomainUpDown5.Name = "kryptonDomainUpDown5";
            this.kryptonDomainUpDown5.Size = new System.Drawing.Size(357, 22);
            this.kryptonDomainUpDown5.TabIndex = 4;
            this.kryptonDomainUpDown5.Text = "kryptonDomainUpDown5";
            // 
            // kryptonDomainUpDown4
            // 
            this.kryptonDomainUpDown4.Location = new System.Drawing.Point(12, 96);
            this.kryptonDomainUpDown4.Name = "kryptonDomainUpDown4";
            this.kryptonDomainUpDown4.Size = new System.Drawing.Size(357, 22);
            this.kryptonDomainUpDown4.TabIndex = 3;
            this.kryptonDomainUpDown4.Text = "kryptonDomainUpDown4";
            // 
            // kryptonDomainUpDown3
            // 
            this.kryptonDomainUpDown3.Location = new System.Drawing.Point(12, 68);
            this.kryptonDomainUpDown3.Name = "kryptonDomainUpDown3";
            this.kryptonDomainUpDown3.Size = new System.Drawing.Size(357, 22);
            this.kryptonDomainUpDown3.TabIndex = 2;
            this.kryptonDomainUpDown3.Text = "kryptonDomainUpDown3";
            // 
            // kryptonDomainUpDown2
            // 
            this.kryptonDomainUpDown2.Location = new System.Drawing.Point(12, 40);
            this.kryptonDomainUpDown2.Name = "kryptonDomainUpDown2";
            this.kryptonDomainUpDown2.Size = new System.Drawing.Size(357, 22);
            this.kryptonDomainUpDown2.TabIndex = 1;
            this.kryptonDomainUpDown2.Text = "kryptonDomainUpDown2";
            // 
            // kryptonDomainUpDown1
            // 
            this.kryptonDomainUpDown1.Location = new System.Drawing.Point(12, 12);
            this.kryptonDomainUpDown1.Name = "kryptonDomainUpDown1";
            this.kryptonDomainUpDown1.Size = new System.Drawing.Size(357, 22);
            this.kryptonDomainUpDown1.TabIndex = 0;
            this.kryptonDomainUpDown1.Text = "kryptonDomainUpDown1";
            // 
            // DomainUpDownTest
            // 
            this.ClientSize = new System.Drawing.Size(1101, 158);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "DomainUpDownTest";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

    }
}