#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved. 
 *  
 */
#endregion

namespace TestForm
{
    partial class DataGridViewTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonDataGridView1 = new Krypton.Toolkit.KryptonDataGridView();
            this.dataSet = new System.Data.DataSet();
            this.dtTestData = new System.Data.DataTable();
            this.dataDateTime = new System.Data.DataColumn();
            this.dataComboBox = new System.Data.DataColumn();
            this.dataTextBox = new System.Data.DataColumn();
            this.dataMaskedTextBox = new System.Data.DataColumn();
            this.dataDomainUpDown = new System.Data.DataColumn();
            this.dataNumericUpDown = new System.Data.DataColumn();
            this.dataButton = new System.Data.DataColumn();
            this.dataCheckBox = new System.Data.DataColumn();
            this.kryptonContextMenu1 = new Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuItems1 = new Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuHeading1 = new Krypton.Toolkit.KryptonContextMenuHeading();
            this.kryptonContextMenuSeparator1 = new Krypton.Toolkit.KryptonContextMenuSeparator();
            this.kryptonContextMenuCheckBox1 = new Krypton.Toolkit.KryptonContextMenuCheckBox();
            this.kryptonContextMenuCheckButton1 = new Krypton.Toolkit.KryptonContextMenuCheckButton();
            this.kryptonContextMenuRadioButton1 = new Krypton.Toolkit.KryptonContextMenuRadioButton();
            this.kryptonContextMenuLinkLabel1 = new Krypton.Toolkit.KryptonContextMenuLinkLabel();
            this.kryptonContextMenuColorColumns1 = new Krypton.Toolkit.KryptonContextMenuColorColumns();
            this.kryptonContextMenuImageSelect1 = new Krypton.Toolkit.KryptonContextMenuImageSelect();
            this.kryptonContextMenuMonthCalendar1 = new Krypton.Toolkit.KryptonContextMenuMonthCalendar();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.buttonClearCellColors = new Krypton.Toolkit.KryptonButton();
            this.buttonRandomCellColors = new Krypton.Toolkit.KryptonButton();
            this.kcmbGridStyle = new Krypton.Toolkit.KryptonComboBox();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.kryptonPropertyGrid1 = new Krypton.Toolkit.KryptonPropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTestData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbGridStyle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonBorderEdge1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 461);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(1234, 50);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.BorderStyle = Krypton.Toolkit.PaletteBorderStyle.HeaderSecondary;
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(1234, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.kryptonDataGridView1);
            this.kryptonPanel2.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel2.Controls.Add(this.kryptonButton1);
            this.kryptonPanel2.Controls.Add(this.buttonClearCellColors);
            this.kryptonPanel2.Controls.Add(this.buttonRandomCellColors);
            this.kryptonPanel2.Controls.Add(this.kcmbGridStyle);
            this.kryptonPanel2.Controls.Add(this.kryptonThemeComboBox1);
            this.kryptonPanel2.Controls.Add(this.kryptonPropertyGrid1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(1234, 461);
            this.kryptonPanel2.TabIndex = 1;
            // 
            // kryptonDataGridView1
            // 
            this.kryptonDataGridView1.AutoGenerateColumns = false;
            this.kryptonDataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.kryptonDataGridView1.ColumnHeadersHeight = 36;
            this.kryptonDataGridView1.DataMember = "TestData";
            this.kryptonDataGridView1.DataSource = this.dataSet;
            this.kryptonDataGridView1.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.kryptonDataGridView1.KryptonContextMenu = this.kryptonContextMenu1;
            this.kryptonDataGridView1.Location = new System.Drawing.Point(13, 12);
            this.kryptonDataGridView1.Name = "kryptonDataGridView1";
            this.kryptonDataGridView1.RowHeadersWidth = 51;
            this.kryptonDataGridView1.Size = new System.Drawing.Size(870, 252);
            this.kryptonDataGridView1.TabIndex = 2;
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "NewDataSet";
            this.dataSet.Tables.AddRange(new System.Data.DataTable[] {
            this.dtTestData});
            // 
            // dtTestData
            // 
            this.dtTestData.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataDateTime,
            this.dataComboBox,
            this.dataTextBox,
            this.dataMaskedTextBox,
            this.dataDomainUpDown,
            this.dataNumericUpDown,
            this.dataButton,
            this.dataCheckBox});
            this.dtTestData.TableName = "TestData";
            // 
            // dataDateTime
            // 
            this.dataDateTime.Caption = "DateTime";
            this.dataDateTime.ColumnName = "DateTime";
            this.dataDateTime.DataType = typeof(System.DateTime);
            // 
            // dataComboBox
            // 
            this.dataComboBox.Caption = "ComboBox";
            this.dataComboBox.ColumnName = "ComboBox";
            // 
            // dataTextBox
            // 
            this.dataTextBox.Caption = "TextBox";
            this.dataTextBox.ColumnName = "TextBox";
            // 
            // dataMaskedTextBox
            // 
            this.dataMaskedTextBox.Caption = "MaskedTextBox";
            this.dataMaskedTextBox.ColumnName = "MaskedTextBox";
            // 
            // dataDomainUpDown
            // 
            this.dataDomainUpDown.Caption = "DomainUpDown";
            this.dataDomainUpDown.ColumnName = "DomainUpDown";
            // 
            // dataNumericUpDown
            // 
            this.dataNumericUpDown.Caption = "NumericUpDown";
            this.dataNumericUpDown.ColumnName = "NumericUpDown";
            this.dataNumericUpDown.DataType = typeof(decimal);
            // 
            // dataButton
            // 
            this.dataButton.Caption = "Button";
            this.dataButton.ColumnName = "Button";
            // 
            // dataCheckBox
            // 
            this.dataCheckBox.Caption = "CheckBox";
            this.dataCheckBox.ColumnName = "CheckBox";
            this.dataCheckBox.DataType = typeof(bool);
            // 
            // kryptonContextMenu1
            // 
            this.kryptonContextMenu1.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItems1,
            this.kryptonContextMenuHeading1,
            this.kryptonContextMenuSeparator1,
            this.kryptonContextMenuCheckBox1,
            this.kryptonContextMenuCheckButton1,
            this.kryptonContextMenuRadioButton1,
            this.kryptonContextMenuLinkLabel1,
            this.kryptonContextMenuColorColumns1,
            this.kryptonContextMenuImageSelect1,
            this.kryptonContextMenuMonthCalendar1});
            // 
            // kryptonContextMenuHeading1
            // 
            this.kryptonContextMenuHeading1.ExtraText = "";
            // 
            // kryptonContextMenuCheckBox1
            // 
            this.kryptonContextMenuCheckBox1.ExtraText = "";
            // 
            // kryptonContextMenuCheckButton1
            // 
            this.kryptonContextMenuCheckButton1.Text = "CheckButton";
            // 
            // kryptonContextMenuRadioButton1
            // 
            this.kryptonContextMenuRadioButton1.ExtraText = "";
            this.kryptonContextMenuRadioButton1.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            // 
            // kryptonContextMenuLinkLabel1
            // 
            this.kryptonContextMenuLinkLabel1.ExtraText = "";
            this.kryptonContextMenuLinkLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.NormalControl;
            // 
            // kryptonContextMenuColorColumns1
            // 
            this.kryptonContextMenuColorColumns1.SelectedColor = System.Drawing.Color.Empty;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(342, 272);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(263, 24);
            this.kryptonLabel1.TabIndex = 8;
            this.kryptonLabel1.Values.Text = "Right click grid for Krypton Context Menu";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(11, 395);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(324, 29);
            this.kryptonButton1.TabIndex = 7;
            this.kryptonButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton1.Values.Text = "&Open Programatic Populate";
            // 
            // buttonClearCellColors
            // 
            this.buttonClearCellColors.Location = new System.Drawing.Point(11, 360);
            this.buttonClearCellColors.Name = "buttonClearCellColors";
            this.buttonClearCellColors.Size = new System.Drawing.Size(324, 29);
            this.buttonClearCellColors.TabIndex = 6;
            this.buttonClearCellColors.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonClearCellColors.Values.Text = "Clear Cell Colors";
            // 
            // buttonRandomCellColors
            // 
            this.buttonRandomCellColors.Location = new System.Drawing.Point(12, 326);
            this.buttonRandomCellColors.Name = "buttonRandomCellColors";
            this.buttonRandomCellColors.Size = new System.Drawing.Size(324, 29);
            this.buttonRandomCellColors.TabIndex = 5;
            this.buttonRandomCellColors.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.buttonRandomCellColors.Values.Text = "Random Cell Colors";
            // 
            // kcmbGridStyle
            // 
            this.kcmbGridStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbGridStyle.DropDownWidth = 323;
            this.kcmbGridStyle.IntegralHeight = false;
            this.kcmbGridStyle.Location = new System.Drawing.Point(13, 299);
            this.kcmbGridStyle.Name = "kcmbGridStyle";
            this.kcmbGridStyle.Size = new System.Drawing.Size(323, 24);
            this.kcmbGridStyle.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kcmbGridStyle.TabIndex = 4;
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.DefaultPalette = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonThemeComboBox1.DropDownWidth = 323;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(13, 271);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(323, 24);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 3;
            // 
            // kryptonPropertyGrid1
            // 
            this.kryptonPropertyGrid1.BackColor = System.Drawing.SystemColors.Window;
            this.kryptonPropertyGrid1.CategoryForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.kryptonPropertyGrid1.CommandsBackColor = System.Drawing.SystemColors.Window;
            this.kryptonPropertyGrid1.CommandsForeColor = System.Drawing.SystemColors.ControlText;
            this.kryptonPropertyGrid1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.kryptonPropertyGrid1.HelpBackColor = System.Drawing.SystemColors.Window;
            this.kryptonPropertyGrid1.HelpForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.kryptonPropertyGrid1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.kryptonPropertyGrid1.Location = new System.Drawing.Point(888, 12);
            this.kryptonPropertyGrid1.Name = "kryptonPropertyGrid1";
            this.kryptonPropertyGrid1.Size = new System.Drawing.Size(334, 429);
            this.kryptonPropertyGrid1.TabIndex = 2;
            this.kryptonPropertyGrid1.ViewForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // DataGridViewTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 511);
            this.Controls.Add(this.kryptonPanel2);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "DataGridViewTest";
            this.Text = "DataGridViewTest";
            this.Load += new System.EventHandler(this.DataGridViewTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTestData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbGridStyle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private Krypton.Toolkit.KryptonPropertyGrid kryptonPropertyGrid1;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Toolkit.KryptonComboBox kcmbGridStyle;
        private Krypton.Toolkit.KryptonButton kryptonButton1;
        private Krypton.Toolkit.KryptonButton buttonClearCellColors;
        private Krypton.Toolkit.KryptonButton buttonRandomCellColors;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private System.Data.DataSet dataSet;
        private System.Data.DataTable dtTestData;
        private System.Data.DataColumn dataDateTime;
        private System.Data.DataColumn dataComboBox;
        private System.Data.DataColumn dataTextBox;
        private System.Data.DataColumn dataMaskedTextBox;
        private System.Data.DataColumn dataDomainUpDown;
        private System.Data.DataColumn dataNumericUpDown;
        private System.Data.DataColumn dataButton;
        private System.Data.DataColumn dataCheckBox;
        private Krypton.Toolkit.KryptonContextMenu kryptonContextMenu1;
        private Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems1;
        private Krypton.Toolkit.KryptonContextMenuHeading kryptonContextMenuHeading1;
        private Krypton.Toolkit.KryptonContextMenuSeparator kryptonContextMenuSeparator1;
        private Krypton.Toolkit.KryptonContextMenuCheckBox kryptonContextMenuCheckBox1;
        private Krypton.Toolkit.KryptonContextMenuCheckButton kryptonContextMenuCheckButton1;
        private Krypton.Toolkit.KryptonContextMenuRadioButton kryptonContextMenuRadioButton1;
        private Krypton.Toolkit.KryptonContextMenuLinkLabel kryptonContextMenuLinkLabel1;
        private Krypton.Toolkit.KryptonContextMenuColorColumns kryptonContextMenuColorColumns1;
        private Krypton.Toolkit.KryptonContextMenuImageSelect kryptonContextMenuImageSelect1;
        private Krypton.Toolkit.KryptonContextMenuMonthCalendar kryptonContextMenuMonthCalendar1;
        private Krypton.Toolkit.KryptonDataGridView kryptonDataGridView1;
    }
}