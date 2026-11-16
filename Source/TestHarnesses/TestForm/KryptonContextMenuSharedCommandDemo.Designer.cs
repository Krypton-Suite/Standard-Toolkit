#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class KryptonContextMenuSharedCommandDemo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                _sharedCommand.Dispose();
                _contextMenu.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.kryptonPanelMain = new Krypton.Toolkit.KryptonPanel();
            this.klblInstruction = new Krypton.Toolkit.KryptonWrapLabel();
            this.kbtnShowMenu = new Krypton.Toolkit.KryptonButton();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).BeginInit();
            this.kryptonPanelMain.SuspendLayout();
            this.SuspendLayout();
            //
            // kryptonPanelMain
            //
            this.kryptonPanelMain.Controls.Add(this.klblStatus);
            this.kryptonPanelMain.Controls.Add(this.kbtnShowMenu);
            this.kryptonPanelMain.Controls.Add(this.klblInstruction);
            this.kryptonPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanelMain.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanelMain.Name = "kryptonPanelMain";
            this.kryptonPanelMain.Padding = new System.Windows.Forms.Padding(16);
            this.kryptonPanelMain.Size = new System.Drawing.Size(784, 361);
            this.kryptonPanelMain.TabIndex = 0;
            //
            // klblInstruction
            //
            this.klblInstruction.AutoSize = false;
            this.klblInstruction.Dock = System.Windows.Forms.DockStyle.Top;
            this.klblInstruction.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.klblInstruction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.klblInstruction.LabelStyle = Krypton.Toolkit.LabelStyle.NormalPanel;
            this.klblInstruction.Location = new System.Drawing.Point(16, 16);
            this.klblInstruction.Name = "klblInstruction";
            this.klblInstruction.Size = new System.Drawing.Size(752, 120);
            this.klblInstruction.Text = "Issue #3891: multiple KryptonContextMenu items can share one KryptonCommand.\r\n\r\n1. Right-click Show Menu or use the drop-down arrow.\r\n2. Choose any item — each passes CommandParameter to the same Execute handler.\r\n3. The status line shows the originating item type and parameter.\r\n\r\nLeave the shared command Text empty so each item keeps its own caption.";
            //
            // kbtnShowMenu
            //
            this.kbtnShowMenu.Location = new System.Drawing.Point(19, 152);
            this.kbtnShowMenu.Name = "kbtnShowMenu";
            this.kbtnShowMenu.Size = new System.Drawing.Size(220, 28);
            this.kbtnShowMenu.TabIndex = 1;
            this.kbtnShowMenu.Values.Text = "Show Menu";
            //
            // klblStatus
            //
            this.klblStatus.Location = new System.Drawing.Point(19, 196);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(720, 24);
            this.klblStatus.TabIndex = 2;
            this.klblStatus.Values.Text = "Waiting for a menu selection...";
            //
            // KryptonContextMenuSharedCommandDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.kryptonPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(640, 320);
            this.Name = "KryptonContextMenuSharedCommandDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Issue #3891 - Shared KryptonContextMenu Command";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelMain)).EndInit();
            this.kryptonPanelMain.ResumeLayout(false);
            this.kryptonPanelMain.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanelMain;
        private Krypton.Toolkit.KryptonWrapLabel klblInstruction;
        private Krypton.Toolkit.KryptonButton kbtnShowMenu;
        private Krypton.Toolkit.KryptonLabel klblStatus;
    }
}
