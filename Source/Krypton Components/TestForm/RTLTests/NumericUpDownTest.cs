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

public partial class NumericUpDownTest : KryptonForm
{
    private KryptonNumericUpDown kryptonNumericUpDown1;
    private KryptonNumericUpDown kryptonNumericUpDown5;
    private KryptonNumericUpDown kryptonNumericUpDown4;
    private KryptonNumericUpDown kryptonNumericUpDown3;
    private KryptonNumericUpDown kryptonNumericUpDown2;
    private KryptonButton kbtnTestUpDown;
    private KryptonButton kbtnResetValues;
    private KryptonLabel klblRtlStatus;
    private KryptonButton kbtnToggleRtl;
    private KryptonPanel kryptonPanel1;

    public NumericUpDownTest()
    {
        InitializeComponent();
        InitializeNumericUpDowns();
    }

    private void InitializeNumericUpDowns()
    {
        // Configure different numeric up/down configurations
        kryptonNumericUpDown1.Minimum = 0;
        kryptonNumericUpDown1.Maximum = 100;
        kryptonNumericUpDown1.Value = 50;
        kryptonNumericUpDown1.Increment = 1;
        kryptonNumericUpDown1.DecimalPlaces = 0;

        kryptonNumericUpDown2.Minimum = 0;
        kryptonNumericUpDown2.Maximum = 1000;
        kryptonNumericUpDown2.Value = 250;
        kryptonNumericUpDown2.Increment = 10;
        kryptonNumericUpDown2.DecimalPlaces = 0;

        kryptonNumericUpDown3.Minimum = 0;
        kryptonNumericUpDown3.Maximum = 100;
        kryptonNumericUpDown3.Value = 25.5m;
        kryptonNumericUpDown3.Increment = 0.5m;
        kryptonNumericUpDown3.DecimalPlaces = 1;

        kryptonNumericUpDown4.Minimum = -100;
        kryptonNumericUpDown4.Maximum = 100;
        kryptonNumericUpDown4.Value = 0;
        kryptonNumericUpDown4.Increment = 5;
        kryptonNumericUpDown4.DecimalPlaces = 0;

        kryptonNumericUpDown5.Minimum = 0;
        kryptonNumericUpDown5.Maximum = 999999;
        kryptonNumericUpDown5.Value = 12345;
        kryptonNumericUpDown5.Increment = 100;
        kryptonNumericUpDown5.DecimalPlaces = 0;

        // Set initial RTL state
        UpdateRtlState();
    }

    private void kbtnToggleRtl_Click(object sender, EventArgs e)
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

    private void kbtnResetValues_Click(object sender, EventArgs e)
    {
        // Reset all numeric up/down values
        kryptonNumericUpDown1.Value = 50;
        kryptonNumericUpDown2.Value = 250;
        kryptonNumericUpDown3.Value = 25.5m;
        kryptonNumericUpDown4.Value = 0;
        kryptonNumericUpDown5.Value = 12345;
    }

    private void kbtnTestUpDown_Click(object sender, EventArgs e)
    {
        // Test the up/down functionality
        kryptonNumericUpDown1.UpButton();
        kryptonNumericUpDown2.DownButton();
        kryptonNumericUpDown3.UpButton();
        kryptonNumericUpDown4.DownButton();
        kryptonNumericUpDown5.UpButton();
    }

    private void InitializeComponent()
    {
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnTestUpDown = new Krypton.Toolkit.KryptonButton();
            this.kbtnResetValues = new Krypton.Toolkit.KryptonButton();
            this.klblRtlStatus = new Krypton.Toolkit.KryptonLabel();
            this.kbtnToggleRtl = new Krypton.Toolkit.KryptonButton();
            this.kryptonNumericUpDown5 = new Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonNumericUpDown4 = new Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonNumericUpDown3 = new Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonNumericUpDown2 = new Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonNumericUpDown1 = new Krypton.Toolkit.KryptonNumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnTestUpDown);
            this.kryptonPanel1.Controls.Add(this.kbtnResetValues);
            this.kryptonPanel1.Controls.Add(this.klblRtlStatus);
            this.kryptonPanel1.Controls.Add(this.kbtnToggleRtl);
            this.kryptonPanel1.Controls.Add(this.kryptonNumericUpDown5);
            this.kryptonPanel1.Controls.Add(this.kryptonNumericUpDown4);
            this.kryptonPanel1.Controls.Add(this.kryptonNumericUpDown3);
            this.kryptonPanel1.Controls.Add(this.kryptonNumericUpDown2);
            this.kryptonPanel1.Controls.Add(this.kryptonNumericUpDown1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(1128, 317);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnTestUpDown
            // 
            this.kbtnTestUpDown.Location = new System.Drawing.Point(601, 124);
            this.kbtnTestUpDown.Name = "kbtnTestUpDown";
            this.kbtnTestUpDown.Size = new System.Drawing.Size(226, 25);
            this.kbtnTestUpDown.TabIndex = 16;
            this.kbtnTestUpDown.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnTestUpDown.Values.Text = "Test Up/Down";
            this.kbtnTestUpDown.Click += new System.EventHandler(this.kbtnTestUpDown_Click);
            // 
            // kbtnResetValues
            // 
            this.kbtnResetValues.Location = new System.Drawing.Point(368, 124);
            this.kbtnResetValues.Name = "kbtnResetValues";
            this.kbtnResetValues.Size = new System.Drawing.Size(226, 25);
            this.kbtnResetValues.TabIndex = 15;
            this.kbtnResetValues.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnResetValues.Values.Text = "Reset Values";
            this.kbtnResetValues.Click += new System.EventHandler(this.kbtnResetValues_Click);
            // 
            // klblRtlStatus
            // 
            this.klblRtlStatus.Location = new System.Drawing.Point(601, 93);
            this.klblRtlStatus.Name = "klblRtlStatus";
            this.klblRtlStatus.Size = new System.Drawing.Size(88, 20);
            this.klblRtlStatus.TabIndex = 14;
            this.klblRtlStatus.Values.Text = "kryptonLabel1";
            // 
            // kbtnToggleRtl
            // 
            this.kbtnToggleRtl.Location = new System.Drawing.Point(368, 93);
            this.kbtnToggleRtl.Name = "kbtnToggleRtl";
            this.kbtnToggleRtl.Size = new System.Drawing.Size(226, 25);
            this.kbtnToggleRtl.TabIndex = 13;
            this.kbtnToggleRtl.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnToggleRtl.Values.Text = "Toggle RTL";
            this.kbtnToggleRtl.Click += new System.EventHandler(this.kbtnToggleRtl_Click);
            // 
            // kryptonNumericUpDown5
            // 
            this.kryptonNumericUpDown5.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.kryptonNumericUpDown5.Location = new System.Drawing.Point(12, 124);
            this.kryptonNumericUpDown5.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.kryptonNumericUpDown5.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.kryptonNumericUpDown5.Name = "kryptonNumericUpDown5";
            this.kryptonNumericUpDown5.Size = new System.Drawing.Size(350, 22);
            this.kryptonNumericUpDown5.TabIndex = 4;
            this.kryptonNumericUpDown5.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // kryptonNumericUpDown4
            // 
            this.kryptonNumericUpDown4.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.kryptonNumericUpDown4.Location = new System.Drawing.Point(12, 96);
            this.kryptonNumericUpDown4.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.kryptonNumericUpDown4.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.kryptonNumericUpDown4.Name = "kryptonNumericUpDown4";
            this.kryptonNumericUpDown4.Size = new System.Drawing.Size(350, 22);
            this.kryptonNumericUpDown4.TabIndex = 3;
            this.kryptonNumericUpDown4.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // kryptonNumericUpDown3
            // 
            this.kryptonNumericUpDown3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.kryptonNumericUpDown3.Location = new System.Drawing.Point(12, 68);
            this.kryptonNumericUpDown3.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.kryptonNumericUpDown3.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.kryptonNumericUpDown3.Name = "kryptonNumericUpDown3";
            this.kryptonNumericUpDown3.Size = new System.Drawing.Size(350, 22);
            this.kryptonNumericUpDown3.TabIndex = 2;
            this.kryptonNumericUpDown3.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // kryptonNumericUpDown2
            // 
            this.kryptonNumericUpDown2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.kryptonNumericUpDown2.Location = new System.Drawing.Point(12, 40);
            this.kryptonNumericUpDown2.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.kryptonNumericUpDown2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.kryptonNumericUpDown2.Name = "kryptonNumericUpDown2";
            this.kryptonNumericUpDown2.Size = new System.Drawing.Size(350, 22);
            this.kryptonNumericUpDown2.TabIndex = 1;
            this.kryptonNumericUpDown2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // kryptonNumericUpDown1
            // 
            this.kryptonNumericUpDown1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.kryptonNumericUpDown1.Location = new System.Drawing.Point(12, 12);
            this.kryptonNumericUpDown1.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.kryptonNumericUpDown1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.kryptonNumericUpDown1.Name = "kryptonNumericUpDown1";
            this.kryptonNumericUpDown1.Size = new System.Drawing.Size(350, 22);
            this.kryptonNumericUpDown1.TabIndex = 0;
            this.kryptonNumericUpDown1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // NumericUpDownTest
            // 
            this.ClientSize = new System.Drawing.Size(1128, 317);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "NumericUpDownTest";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

    }
}