#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class KryptonSVGPictureBoxDemo
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
            this.kpnlTop = new Krypton.Toolkit.KryptonPanel();
            this.klblInstructions = new Krypton.Toolkit.KryptonLabel();
            this.klblSizeMode = new Krypton.Toolkit.KryptonLabel();
            this.kcmbSizeMode = new Krypton.Toolkit.KryptonComboBox();
            this.kbtnSample = new Krypton.Toolkit.KryptonButton();
            this.kbtnLoad = new Krypton.Toolkit.KryptonButton();
            this.kbtnInvalid = new Krypton.Toolkit.KryptonButton();
            this.kbtnApply = new Krypton.Toolkit.KryptonButton();
            this.ktxtSvg = new Krypton.Toolkit.KryptonTextBox();
            this.klblStatus = new Krypton.Toolkit.KryptonLabel();
            this.ksvg = new Krypton.Drawing.KryptonSVGPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlTop)).BeginInit();
            this.kpnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbSizeMode)).BeginInit();
            this.SuspendLayout();
            //
            // kpnlTop
            //
            this.kpnlTop.Controls.Add(this.klblInstructions);
            this.kpnlTop.Controls.Add(this.klblSizeMode);
            this.kpnlTop.Controls.Add(this.kcmbSizeMode);
            this.kpnlTop.Controls.Add(this.kbtnSample);
            this.kpnlTop.Controls.Add(this.kbtnLoad);
            this.kpnlTop.Controls.Add(this.kbtnInvalid);
            this.kpnlTop.Controls.Add(this.kbtnApply);
            this.kpnlTop.Controls.Add(this.ktxtSvg);
            this.kpnlTop.Controls.Add(this.klblStatus);
            this.kpnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.kpnlTop.Location = new System.Drawing.Point(0, 0);
            this.kpnlTop.Name = "kpnlTop";
            this.kpnlTop.Size = new System.Drawing.Size(900, 188);
            this.kpnlTop.TabIndex = 0;
            //
            // klblInstructions
            //
            this.klblInstructions.Location = new System.Drawing.Point(12, 12);
            this.klblInstructions.Name = "klblInstructions";
            this.klblInstructions.Size = new System.Drawing.Size(876, 40);
            this.klblInstructions.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
            this.klblInstructions.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.klblInstructions.TabIndex = 0;
            this.klblInstructions.Values.Text = "Issue #4657: Apply markup, load a file, or send invalid SVG. Try each SizeMode, resize to re-rasterize, and hover for the tooltip.";
            //
            // klblSizeMode
            //
            this.klblSizeMode.Location = new System.Drawing.Point(12, 60);
            this.klblSizeMode.Name = "klblSizeMode";
            this.klblSizeMode.Size = new System.Drawing.Size(72, 20);
            this.klblSizeMode.TabIndex = 1;
            this.klblSizeMode.Values.Text = "Size mode";
            //
            // kcmbSizeMode
            //
            this.kcmbSizeMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbSizeMode.DropDownWidth = 160;
            this.kcmbSizeMode.Location = new System.Drawing.Point(96, 56);
            this.kcmbSizeMode.Name = "kcmbSizeMode";
            this.kcmbSizeMode.Size = new System.Drawing.Size(160, 22);
            this.kcmbSizeMode.TabIndex = 2;
            this.kcmbSizeMode.SelectedIndexChanged += new System.EventHandler(this.OnSizeModeChanged);
            //
            // kbtnSample
            //
            this.kbtnSample.Location = new System.Drawing.Point(272, 54);
            this.kbtnSample.Name = "kbtnSample";
            this.kbtnSample.Size = new System.Drawing.Size(110, 26);
            this.kbtnSample.TabIndex = 3;
            this.kbtnSample.Values.Text = "Sample";
            this.kbtnSample.Click += new System.EventHandler(this.OnSampleClick);
            //
            // kbtnLoad
            //
            this.kbtnLoad.Location = new System.Drawing.Point(388, 54);
            this.kbtnLoad.Name = "kbtnLoad";
            this.kbtnLoad.Size = new System.Drawing.Size(110, 26);
            this.kbtnLoad.TabIndex = 4;
            this.kbtnLoad.Values.Text = "Load file";
            this.kbtnLoad.Click += new System.EventHandler(this.OnLoadClick);
            //
            // kbtnInvalid
            //
            this.kbtnInvalid.Location = new System.Drawing.Point(504, 54);
            this.kbtnInvalid.Name = "kbtnInvalid";
            this.kbtnInvalid.Size = new System.Drawing.Size(110, 26);
            this.kbtnInvalid.TabIndex = 5;
            this.kbtnInvalid.Values.Text = "Invalid SVG";
            this.kbtnInvalid.Click += new System.EventHandler(this.OnInvalidClick);
            //
            // kbtnApply
            //
            this.kbtnApply.Location = new System.Drawing.Point(620, 54);
            this.kbtnApply.Name = "kbtnApply";
            this.kbtnApply.Size = new System.Drawing.Size(110, 26);
            this.kbtnApply.TabIndex = 6;
            this.kbtnApply.Values.Text = "Apply markup";
            this.kbtnApply.Click += new System.EventHandler(this.OnApplyClick);
            //
            // ktxtSvg
            //
            this.ktxtSvg.Location = new System.Drawing.Point(12, 90);
            this.ktxtSvg.Multiline = true;
            this.ktxtSvg.Name = "ktxtSvg";
            this.ktxtSvg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ktxtSvg.Size = new System.Drawing.Size(876, 52);
            this.ktxtSvg.TabIndex = 7;
            //
            // klblStatus
            //
            this.klblStatus.Location = new System.Drawing.Point(12, 150);
            this.klblStatus.Name = "klblStatus";
            this.klblStatus.Size = new System.Drawing.Size(876, 24);
            this.klblStatus.TabIndex = 8;
            this.klblStatus.Values.Text = "Status";
            //
            // ksvg
            //
            this.ksvg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ksvg.Location = new System.Drawing.Point(0, 188);
            this.ksvg.Name = "ksvg";
            this.ksvg.Size = new System.Drawing.Size(900, 452);
            this.ksvg.TabIndex = 1;
            this.ksvg.Resize += new System.EventHandler(this.OnPictureResize);
            //
            // KryptonSVGPictureBoxDemo
            //
            this.ClientSize = new System.Drawing.Size(900, 640);
            this.Controls.Add(this.ksvg);
            this.Controls.Add(this.kpnlTop);
            this.Name = "KryptonSVGPictureBoxDemo";
            this.Text = "Krypton SVG Picture Box (#4657)";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlTop)).EndInit();
            this.kpnlTop.ResumeLayout(false);
            this.kpnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbSizeMode)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kpnlTop;
        private Krypton.Toolkit.KryptonLabel klblInstructions;
        private Krypton.Toolkit.KryptonLabel klblSizeMode;
        private Krypton.Toolkit.KryptonComboBox kcmbSizeMode;
        private Krypton.Toolkit.KryptonButton kbtnSample;
        private Krypton.Toolkit.KryptonButton kbtnLoad;
        private Krypton.Toolkit.KryptonButton kbtnInvalid;
        private Krypton.Toolkit.KryptonButton kbtnApply;
        private Krypton.Toolkit.KryptonTextBox ktxtSvg;
        private Krypton.Toolkit.KryptonLabel klblStatus;
        private Krypton.Drawing.KryptonSVGPictureBox ksvg;
    }
}
