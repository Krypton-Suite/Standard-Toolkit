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

public partial class DateTimePickerTest : KryptonForm
{
    private KryptonDateTimePicker kryptonDateTimePicker1;
    private KryptonDateTimePicker kryptonDateTimePicker5;
    private KryptonDateTimePicker kryptonDateTimePicker4;
    private KryptonDateTimePicker kryptonDateTimePicker3;
    private KryptonDateTimePicker kryptonDateTimePicker2;
    private KryptonLabel klblRtlStatus;
    private KryptonButton kbtnToggleRtl;
    private KryptonButton kbtnResetValues;
    private KryptonPanel kryptonPanel1;

    public DateTimePickerTest()
    {
        InitializeComponent();
        InitializeDateTimePickers();
    }

    private void InitializeComponent()
    {
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kbtnResetValues = new Krypton.Toolkit.KryptonButton();
            this.klblRtlStatus = new Krypton.Toolkit.KryptonLabel();
            this.kbtnToggleRtl = new Krypton.Toolkit.KryptonButton();
            this.kryptonDateTimePicker5 = new Krypton.Toolkit.KryptonDateTimePicker();
            this.kryptonDateTimePicker4 = new Krypton.Toolkit.KryptonDateTimePicker();
            this.kryptonDateTimePicker3 = new Krypton.Toolkit.KryptonDateTimePicker();
            this.kryptonDateTimePicker2 = new Krypton.Toolkit.KryptonDateTimePicker();
            this.kryptonDateTimePicker1 = new Krypton.Toolkit.KryptonDateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnResetValues);
            this.kryptonPanel1.Controls.Add(this.klblRtlStatus);
            this.kryptonPanel1.Controls.Add(this.kbtnToggleRtl);
            this.kryptonPanel1.Controls.Add(this.kryptonDateTimePicker5);
            this.kryptonPanel1.Controls.Add(this.kryptonDateTimePicker4);
            this.kryptonPanel1.Controls.Add(this.kryptonDateTimePicker3);
            this.kryptonPanel1.Controls.Add(this.kryptonDateTimePicker2);
            this.kryptonPanel1.Controls.Add(this.kryptonDateTimePicker1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(1104, 141);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnResetValues
            // 
            this.kbtnResetValues.Location = new System.Drawing.Point(363, 98);
            this.kbtnResetValues.Name = "kbtnResetValues";
            this.kbtnResetValues.Size = new System.Drawing.Size(226, 25);
            this.kbtnResetValues.TabIndex = 7;
            this.kbtnResetValues.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnResetValues.Values.Text = "Reset Values";
            this.kbtnResetValues.Click += new System.EventHandler(this.kbtnResetValues_Click);
            // 
            // klblRtlStatus
            // 
            this.klblRtlStatus.Location = new System.Drawing.Point(596, 67);
            this.klblRtlStatus.Name = "klblRtlStatus";
            this.klblRtlStatus.Size = new System.Drawing.Size(88, 20);
            this.klblRtlStatus.TabIndex = 6;
            this.klblRtlStatus.Values.Text = "kryptonLabel1";
            // 
            // kbtnToggleRtl
            // 
            this.kbtnToggleRtl.Location = new System.Drawing.Point(363, 67);
            this.kbtnToggleRtl.Name = "kbtnToggleRtl";
            this.kbtnToggleRtl.Size = new System.Drawing.Size(226, 25);
            this.kbtnToggleRtl.TabIndex = 5;
            this.kbtnToggleRtl.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnToggleRtl.Values.Text = "Toggle RTL";
            this.kbtnToggleRtl.Click += new System.EventHandler(this.kbtnToggleRtl_Click);
            // 
            // kryptonDateTimePicker5
            // 
            this.kryptonDateTimePicker5.Location = new System.Drawing.Point(13, 121);
            this.kryptonDateTimePicker5.Name = "kryptonDateTimePicker5";
            this.kryptonDateTimePicker5.Size = new System.Drawing.Size(344, 21);
            this.kryptonDateTimePicker5.TabIndex = 4;
            // 
            // kryptonDateTimePicker4
            // 
            this.kryptonDateTimePicker4.Location = new System.Drawing.Point(13, 94);
            this.kryptonDateTimePicker4.Name = "kryptonDateTimePicker4";
            this.kryptonDateTimePicker4.Size = new System.Drawing.Size(344, 21);
            this.kryptonDateTimePicker4.TabIndex = 3;
            // 
            // kryptonDateTimePicker3
            // 
            this.kryptonDateTimePicker3.Location = new System.Drawing.Point(13, 67);
            this.kryptonDateTimePicker3.Name = "kryptonDateTimePicker3";
            this.kryptonDateTimePicker3.Size = new System.Drawing.Size(344, 21);
            this.kryptonDateTimePicker3.TabIndex = 2;
            // 
            // kryptonDateTimePicker2
            // 
            this.kryptonDateTimePicker2.Location = new System.Drawing.Point(13, 40);
            this.kryptonDateTimePicker2.Name = "kryptonDateTimePicker2";
            this.kryptonDateTimePicker2.Size = new System.Drawing.Size(344, 21);
            this.kryptonDateTimePicker2.TabIndex = 1;
            // 
            // kryptonDateTimePicker1
            // 
            this.kryptonDateTimePicker1.Location = new System.Drawing.Point(13, 13);
            this.kryptonDateTimePicker1.Name = "kryptonDateTimePicker1";
            this.kryptonDateTimePicker1.Size = new System.Drawing.Size(344, 21);
            this.kryptonDateTimePicker1.TabIndex = 0;
            // 
            // DateTimePickerTest
            // 
            this.ClientSize = new System.Drawing.Size(1104, 141);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "DateTimePickerTest";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

    }

    private void InitializeDateTimePickers()
    {
        // Configure different date picker formats and styles
        kryptonDateTimePicker1.Format = DateTimePickerFormat.Long;
        kryptonDateTimePicker1.ShowCheckBox = true;
        kryptonDateTimePicker1.Checked = true;
        kryptonDateTimePicker1.Value = DateTime.Now;

        kryptonDateTimePicker2.Format = DateTimePickerFormat.Short;
        kryptonDateTimePicker2.ShowUpDown = true;
        kryptonDateTimePicker2.Value = DateTime.Now.AddDays(1);

        kryptonDateTimePicker3.Format = DateTimePickerFormat.Time;
        kryptonDateTimePicker3.ShowUpDown = true;
        kryptonDateTimePicker3.Value = DateTime.Now;

        kryptonDateTimePicker4.Format = DateTimePickerFormat.Custom;
        kryptonDateTimePicker4.CustomFormat = "dd/MM/yyyy HH:mm";
        kryptonDateTimePicker4.Value = DateTime.Now.AddDays(-1);

        kryptonDateTimePicker5.Format = DateTimePickerFormat.Long;
        kryptonDateTimePicker5.ShowCheckBox = true;
        kryptonDateTimePicker5.Checked = false;
        kryptonDateTimePicker5.Value = DateTime.Now.AddMonths(1);

        // Set initial RTL state
        UpdateRtlState();
    }

    private void kbtnToggleRtl_Click(object sender, EventArgs e)
    {
        // Toggle RTL state
        RightToLeft = RightToLeft == RightToLeft.Yes ? RightToLeft.No : RightToLeft.Yes;
        RightToLeftLayout = RightToLeft == RightToLeft.Yes;
        
        UpdateRtlState();
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
        // Reset all date picker values
        kryptonDateTimePicker1.Value = DateTime.Now;
        kryptonDateTimePicker2.Value = DateTime.Now.AddDays(1);
        kryptonDateTimePicker3.Value = DateTime.Now;
        kryptonDateTimePicker4.Value = DateTime.Now.AddDays(-1);
        kryptonDateTimePicker5.Value = DateTime.Now.AddMonths(1);
    }
}