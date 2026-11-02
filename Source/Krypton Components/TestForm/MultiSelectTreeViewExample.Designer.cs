#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class MultiSelectTreeViewExample
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.klblInstructions = new Krypton.Toolkit.KryptonLabel();
            this.klblSelection = new Krypton.Toolkit.KryptonLabel();
            this.kbtnSelectAll = new Krypton.Toolkit.KryptonButton();
            this.kbtnClearSelection = new Krypton.Toolkit.KryptonButton();
            this.kbtnClose = new Krypton.Toolkit.KryptonButton();
            this.kmstvDemo = new Krypton.Toolkit.Utilities.KryptonMultiSelectTreeView();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.klblInstructions);
            this.kryptonPanel1.Controls.Add(this.klblSelection);
            this.kryptonPanel1.Controls.Add(this.kbtnSelectAll);
            this.kryptonPanel1.Controls.Add(this.kbtnClearSelection);
            this.kryptonPanel1.Controls.Add(this.kbtnClose);
            this.kryptonPanel1.Controls.Add(this.kmstvDemo);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(684, 511);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // klblInstructions
            // 
            this.klblInstructions.Location = new System.Drawing.Point(12, 12);
            this.klblInstructions.Name = "klblInstructions";
            this.klblInstructions.Size = new System.Drawing.Size(660, 40);
            this.klblInstructions.TabIndex = 0;
            this.klblInstructions.Values.Text = "Ctrl+click toggles nodes. Shift+click selects a visible range. Drag to rubber-ban" +
    "d select. Check boxes also update SelectedNodes.";
            // 
            // klblSelection
            // 
            this.klblSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.klblSelection.Location = new System.Drawing.Point(12, 404);
            this.klblSelection.Name = "klblSelection";
            this.klblSelection.Size = new System.Drawing.Size(660, 20);
            this.klblSelection.TabIndex = 2;
            this.klblSelection.Values.Text = "Selected: (none)";
            // 
            // kbtnSelectAll
            // 
            this.kbtnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kbtnSelectAll.Location = new System.Drawing.Point(12, 436);
            this.kbtnSelectAll.Name = "kbtnSelectAll";
            this.kbtnSelectAll.Size = new System.Drawing.Size(120, 25);
            this.kbtnSelectAll.TabIndex = 3;
            this.kbtnSelectAll.Values.Text = "Select All";
            this.kbtnSelectAll.Click += new System.EventHandler(this.kbtnSelectAll_Click);
            // 
            // kbtnClearSelection
            // 
            this.kbtnClearSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kbtnClearSelection.Location = new System.Drawing.Point(138, 436);
            this.kbtnClearSelection.Name = "kbtnClearSelection";
            this.kbtnClearSelection.Size = new System.Drawing.Size(120, 25);
            this.kbtnClearSelection.TabIndex = 4;
            this.kbtnClearSelection.Values.Text = "Clear Selection";
            this.kbtnClearSelection.Click += new System.EventHandler(this.kbtnClearSelection_Click);
            // 
            // kbtnClose
            // 
            this.kbtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnClose.Location = new System.Drawing.Point(552, 436);
            this.kbtnClose.Name = "kbtnClose";
            this.kbtnClose.Size = new System.Drawing.Size(120, 25);
            this.kbtnClose.TabIndex = 5;
            this.kbtnClose.Values.Text = "Close";
            this.kbtnClose.Click += new System.EventHandler(this.kbtnClose_Click);
            // 
            // kmstvDemo
            // 
            this.kmstvDemo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kmstvDemo.CheckBoxes = true;
            this.kmstvDemo.FullRowSelect = true;
            this.kmstvDemo.Location = new System.Drawing.Point(12, 58);
            this.kmstvDemo.Name = "kmstvDemo";
            this.kmstvDemo.Size = new System.Drawing.Size(660, 340);
            this.kmstvDemo.TabIndex = 1;
            this.kmstvDemo.SelectedNodesChanged += new System.EventHandler(this.kmstvDemo_SelectedNodesChanged);
            // 
            // MultiSelectTreeViewExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 511);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MultiSelectTreeViewExample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Multi-Select TreeView Example (#3837)";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.Utilities.KryptonMultiSelectTreeView kmstvDemo;
        private Krypton.Toolkit.KryptonLabel klblInstructions;
        private Krypton.Toolkit.KryptonLabel klblSelection;
        private Krypton.Toolkit.KryptonButton kbtnSelectAll;
        private Krypton.Toolkit.KryptonButton kbtnClearSelection;
        private Krypton.Toolkit.KryptonButton kbtnClose;
    }
}
