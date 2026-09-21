#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class PaletteCollectionEditorDemo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
            this.kwlblInfo = new Krypton.Toolkit.KryptonWrapLabel();
            this.kbtnCreateSample = new Krypton.Toolkit.KryptonButton();
            this.kbtnEditCollection = new Krypton.Toolkit.KryptonButton();
            this.kbtnEditEmpty = new Krypton.Toolkit.KryptonButton();
            this.kwlblStatus = new Krypton.Toolkit.KryptonWrapLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            this.SuspendLayout();
            //
            // kpnlMain
            //
            this.kpnlMain.Controls.Add(this.kwlblStatus);
            this.kpnlMain.Controls.Add(this.kbtnEditEmpty);
            this.kpnlMain.Controls.Add(this.kbtnEditCollection);
            this.kpnlMain.Controls.Add(this.kbtnCreateSample);
            this.kpnlMain.Controls.Add(this.kwlblInfo);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Padding = new System.Windows.Forms.Padding(12);
            this.kpnlMain.Size = new System.Drawing.Size(640, 280);
            this.kpnlMain.TabIndex = 0;
            //
            // kwlblInfo
            //
            this.kwlblInfo.Location = new System.Drawing.Point(16, 16);
            this.kwlblInfo.Name = "kwlblInfo";
            this.kwlblInfo.Size = new System.Drawing.Size(608, 88);
            this.kwlblInfo.Text = "Issue #2117: KryptonPaletteCollectionEditor (Krypton.Toolkit.Utilities) adds .kthemex files to a .ktheme collection and removes named themes. Create a sample collection (two themes plus a spare .kthemex), then open the editor. Add Collection - Violet.kthemex, then Remove it. The last theme cannot be removed.";
            //
            // kbtnCreateSample
            //
            this.kbtnCreateSample.Location = new System.Drawing.Point(16, 112);
            this.kbtnCreateSample.Name = "kbtnCreateSample";
            this.kbtnCreateSample.Size = new System.Drawing.Size(180, 32);
            this.kbtnCreateSample.TabIndex = 0;
            this.kbtnCreateSample.Values.Text = "Create sample collection";
            //
            // kbtnEditCollection
            //
            this.kbtnEditCollection.Enabled = false;
            this.kbtnEditCollection.Location = new System.Drawing.Point(202, 112);
            this.kbtnEditCollection.Name = "kbtnEditCollection";
            this.kbtnEditCollection.Size = new System.Drawing.Size(180, 32);
            this.kbtnEditCollection.TabIndex = 1;
            this.kbtnEditCollection.Values.Text = "Edit sample collection...";
            //
            // kbtnEditEmpty
            //
            this.kbtnEditEmpty.Location = new System.Drawing.Point(388, 112);
            this.kbtnEditEmpty.Name = "kbtnEditEmpty";
            this.kbtnEditEmpty.Size = new System.Drawing.Size(180, 32);
            this.kbtnEditEmpty.TabIndex = 2;
            this.kbtnEditEmpty.Values.Text = "Open empty editor...";
            //
            // kwlblStatus
            //
            this.kwlblStatus.Location = new System.Drawing.Point(16, 156);
            this.kwlblStatus.Name = "kwlblStatus";
            this.kwlblStatus.Size = new System.Drawing.Size(608, 96);
            this.kwlblStatus.Text = "Create a sample collection, then open the editor.";
            //
            // PaletteCollectionEditorDemo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 280);
            this.Controls.Add(this.kpnlMain);
            this.MinimumSize = new System.Drawing.Size(560, 240);
            this.Name = "PaletteCollectionEditorDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Feature #2117 - Palette collection editor";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            this.kpnlMain.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kpnlMain;
        private Krypton.Toolkit.KryptonWrapLabel kwlblInfo;
        private Krypton.Toolkit.KryptonButton kbtnCreateSample;
        private Krypton.Toolkit.KryptonButton kbtnEditCollection;
        private Krypton.Toolkit.KryptonButton kbtnEditEmpty;
        private Krypton.Toolkit.KryptonWrapLabel kwlblStatus;
    }
}
