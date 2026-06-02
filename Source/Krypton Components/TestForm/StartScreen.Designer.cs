#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit;

namespace TestForm
{
    partial class StartScreen
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartScreen));
            this.kbtnExit = new Krypton.Toolkit.KryptonButton();
            this.kryptonManager1 = new Krypton.Toolkit.KryptonManager(this.components);
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tbFilter = new Krypton.Toolkit.KryptonTextBox();
            this.btnClearFilter = new Krypton.Toolkit.ButtonSpecAny();
            this.kryptonThemeListBox1 = new Krypton.Toolkit.KryptonThemeListBox();
            this.rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this.leftLayout = new System.Windows.Forms.TableLayoutPanel();
            this.btnDockTopRight = new Krypton.Toolkit.ButtonSpecAny();
            this.btnRestoreSize = new Krypton.Toolkit.ButtonSpecAny();
            this.rootLayout.SuspendLayout();
            this.leftLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // kbtnExit
            // 
            this.kbtnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kbtnExit.Location = new System.Drawing.Point(0, 679);
            this.kbtnExit.Margin = new System.Windows.Forms.Padding(0);
            this.kbtnExit.MinimumSize = new System.Drawing.Size(0, 30);
            this.kbtnExit.Name = "kbtnExit";
            this.kbtnExit.Size = new System.Drawing.Size(526, 30);
            this.kbtnExit.TabIndex = 15;
            this.kbtnExit.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnExit.Values.Text = "Exit";
            this.kbtnExit.Click += new System.EventHandler(this.kbtnExit_Click);
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.BaseFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonManager1.GlobalUseKryptonScrollbars = true;
            this.kryptonManager1.ToolkitStrings.MessageBoxStrings.LessDetails = "L&ess Details...";
            this.kryptonManager1.ToolkitStrings.MessageBoxStrings.MoreDetails = "&More Details...";
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 39);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Size = new System.Drawing.Size(526, 629);
            this.tlpMain.TabIndex = 2;
            // 
            // tbFilter
            // 
            this.tbFilter.ButtonSpecs.Add(this.btnClearFilter);
            this.tbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbFilter.Location = new System.Drawing.Point(0, 0);
            this.tbFilter.Margin = new System.Windows.Forms.Padding(0);
            this.tbFilter.MinimumSize = new System.Drawing.Size(0, 30);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(526, 30);
            this.tbFilter.StateCommon.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.tbFilter.TabIndex = 0;
            this.tbFilter.UseKryptonScrollbars = true;
            // 
            // btnClearFilter
            // 
            this.btnClearFilter.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Close;
            this.btnClearFilter.UniqueName = "149f662cfafb4c229f6a7c701553bf5d";
            // 
            // kryptonThemeListBox1
            // 
            this.kryptonThemeListBox1.DefaultPalette = Krypton.Toolkit.PaletteMode.Microsoft365Blue;
            this.kryptonThemeListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonThemeListBox1.Location = new System.Drawing.Point(551, 12);
            this.kryptonThemeListBox1.Margin = new System.Windows.Forms.Padding(0);
            this.kryptonThemeListBox1.Name = "kryptonThemeListBox1";
            this.kryptonThemeListBox1.Size = new System.Drawing.Size(313, 709);
            this.kryptonThemeListBox1.TabIndex = 17;
            this.kryptonThemeListBox1.UseKryptonScrollbars = true;
            // 
            // rootLayout
            // 
            this.rootLayout.BackColor = System.Drawing.Color.Transparent;
            this.rootLayout.ColumnCount = 3;
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 313F));
            this.rootLayout.Controls.Add(this.leftLayout, 0, 0);
            this.rootLayout.Controls.Add(this.kryptonThemeListBox1, 2, 0);
            this.rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootLayout.Location = new System.Drawing.Point(0, 0);
            this.rootLayout.Margin = new System.Windows.Forms.Padding(0);
            this.rootLayout.Name = "rootLayout";
            this.rootLayout.Padding = new System.Windows.Forms.Padding(12, 12, 12, 8);
            this.rootLayout.RowCount = 1;
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.Size = new System.Drawing.Size(876, 729);
            this.rootLayout.TabIndex = 18;
            // 
            // leftLayout
            // 
            this.leftLayout.BackColor = System.Drawing.Color.Transparent;
            this.leftLayout.ColumnCount = 1;
            this.leftLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.leftLayout.Controls.Add(this.tbFilter, 0, 0);
            this.leftLayout.Controls.Add(this.tlpMain, 0, 2);
            this.leftLayout.Controls.Add(this.kbtnExit, 0, 4);
            this.leftLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftLayout.Location = new System.Drawing.Point(12, 12);
            this.leftLayout.Margin = new System.Windows.Forms.Padding(0);
            this.leftLayout.Name = "leftLayout";
            this.leftLayout.RowCount = 5;
            this.leftLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.leftLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.leftLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.leftLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.leftLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.leftLayout.Size = new System.Drawing.Size(526, 709);
            this.leftLayout.TabIndex = 18;
            // 
            // btnDockTopRight
            // 
            this.btnDockTopRight.Style = Krypton.Toolkit.PaletteButtonStyle.Standalone;
            this.btnDockTopRight.ToolTipBody = "Dock Top Right";
            this.btnDockTopRight.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Next;
            this.btnDockTopRight.UniqueName = "9a67484dbca74819a23989ebf85abc7d";
            // 
            // btnRestoreSize
            // 
            this.btnRestoreSize.ToolTipBody = "Restore Default Size";
            this.btnRestoreSize.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Undo;
            this.btnRestoreSize.UniqueName = "9a3ac5e81b8c43e3808e9ee37eb66bd7";
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ButtonSpecs.Add(this.btnRestoreSize);
            this.ButtonSpecs.Add(this.btnDockTopRight);
            this.CancelButton = this.kbtnExit;
            this.ClientSize = new System.Drawing.Size(876, 729);
            this.Controls.Add(this.rootLayout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "StartScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome";
            this.rootLayout.ResumeLayout(false);
            this.leftLayout.ResumeLayout(false);
            this.leftLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Krypton.Toolkit.KryptonButton kbtnExit;
        private KryptonManager kryptonManager1;
        private TableLayoutPanel tlpMain;
        private KryptonTextBox tbFilter;
        private ButtonSpecAny btnClearFilter;
        private KryptonThemeListBox kryptonThemeListBox1;
        private TableLayoutPanel rootLayout;
        private TableLayoutPanel leftLayout;
        private ButtonSpecAny btnDockTopRight;
        private ButtonSpecAny btnRestoreSize;
    }
}