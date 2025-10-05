#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
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
            this.btnDockTopRight = new Krypton.Toolkit.ButtonSpecAny();
            this.btnRestoreSize = new Krypton.Toolkit.ButtonSpecAny();
            this.SuspendLayout();
            // 
            // kbtnExit
            // 
            this.kbtnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kbtnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnExit.Location = new System.Drawing.Point(12, 490);
            this.kbtnExit.Margin = new System.Windows.Forms.Padding(2);
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
            this.kryptonManager1.ToolkitStrings.MessageBoxStrings.LessDetails = "L&ess Details...";
            this.kryptonManager1.ToolkitStrings.MessageBoxStrings.MoreDetails = "&More Details...";
            // 
            // tlpMain
            // 
            this.tlpMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Location = new System.Drawing.Point(12, 51);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Size = new System.Drawing.Size(526, 428);
            this.tlpMain.TabIndex = 2;
            // 
            // tbFilter
            // 
            this.tbFilter.ButtonSpecs.Add(this.btnClearFilter);
            this.tbFilter.Location = new System.Drawing.Point(12, 12);
            this.tbFilter.MinimumSize = new System.Drawing.Size(526, 30);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(526, 30);
            this.tbFilter.StateCommon.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.tbFilter.TabIndex = 0;
            // 
            // btnClearFilter
            // 
            this.btnClearFilter.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Close;
            this.btnClearFilter.UniqueName = "149f662cfafb4c229f6a7c701553bf5d";
            // 
            // kryptonThemeListBox1
            // 
            this.kryptonThemeListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.kryptonThemeListBox1.DefaultPalette = Krypton.Toolkit.PaletteMode.Microsoft365Blue;
            this.kryptonThemeListBox1.Location = new System.Drawing.Point(559, 12);
            this.kryptonThemeListBox1.Name = "kryptonThemeListBox1";
            this.kryptonThemeListBox1.Size = new System.Drawing.Size(313, 520);
            this.kryptonThemeListBox1.TabIndex = 17;
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ButtonSpecs.Add(this.btnRestoreSize);
            this.ButtonSpecs.Add(this.btnDockTopRight);
            this.CancelButton = this.kbtnExit;
            this.ClientSize = new System.Drawing.Size(900, 540);
            this.Controls.Add(this.kryptonThemeListBox1);
            this.Controls.Add(this.tbFilter);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.kbtnExit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "StartScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Krypton.Toolkit.KryptonButton kbtnExit;
        private KryptonManager kryptonManager1;
        private TableLayoutPanel tlpMain;
        private KryptonTextBox tbFilter;
        private ButtonSpecAny btnClearFilter;
        private KryptonThemeListBox kryptonThemeListBox1;
        private ButtonSpecAny btnDockTopRight;
        private ButtonSpecAny btnRestoreSize;
    }
}