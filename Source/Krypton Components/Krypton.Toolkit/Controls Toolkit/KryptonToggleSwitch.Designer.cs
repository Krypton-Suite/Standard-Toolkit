namespace Krypton.Toolkit
{
    partial class KryptonToggleSwitch
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._background = new Krypton.Toolkit.KryptonPanel();
            this._onLabel = new Krypton.Toolkit.KryptonLabel();
            this._offLabel = new Krypton.Toolkit.KryptonLabel();
            this._knob = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._background)).BeginInit();
            this._background.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._knob)).BeginInit();
            this.SuspendLayout();
            // 
            // _background
            // 
            this._background.Controls.Add(this._onLabel);
            this._background.Controls.Add(this._offLabel);
            this._background.Controls.Add(this._knob);
            this._background.Dock = System.Windows.Forms.DockStyle.Fill;
            this._background.Location = new System.Drawing.Point(0, 0);
            this._background.Name = "_background";
            this._background.Size = new System.Drawing.Size(100, 40);
            this._background.StateCommon.Color1 = System.Drawing.Color.LightGray;
            this._background.StateCommon.Color2 = System.Drawing.Color.LightGray;
            this._background.TabIndex = 0;
            // 
            // _onLabel
            // 
            this._onLabel.Location = new System.Drawing.Point(5, 10);
            this._onLabel.Name = "_onLabel";
            this._onLabel.Size = new System.Drawing.Size(30, 22);
            this._onLabel.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this._onLabel.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._onLabel.TabIndex = 0;
            this._onLabel.Values.Text = "On";
            this._onLabel.Visible = false;
            // 
            // _offLabel
            // 
            this._offLabel.Location = new System.Drawing.Point(70, 10);
            this._offLabel.Name = "_offLabel";
            this._offLabel.Size = new System.Drawing.Size(32, 22);
            this._offLabel.StateCommon.ShortText.Color1 = System.Drawing.Color.Gray;
            this._offLabel.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._offLabel.TabIndex = 1;
            this._offLabel.Values.Text = "Off";
            // 
            // _knob
            // 
            this._knob.BackColor = System.Drawing.Color.White;
            this._knob.Location = new System.Drawing.Point(2, 2);
            this._knob.Name = "_knob";
            this._knob.Size = new System.Drawing.Size(36, 36);
            this._knob.TabIndex = 2;
            this._knob.TabStop = false;
            this._knob.Paint += (sender, args) =>
            {
                args.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                args.Graphics.FillEllipse(System.Drawing.Brushes.White, new System.Drawing.Rectangle(0, 0, this._knob.Width, this._knob.Height));
            };
            // 
            // KryptonToggleSwitch
            // 
            this.Controls.Add(this._background);
            this.Name = "KryptonToggleSwitch";
            this.Size = new System.Drawing.Size(100, 40);
            this.Click += new System.EventHandler(this.KryptonToggleSwitch_Click);
            ((System.ComponentModel.ISupportInitialize)(this._background)).EndInit();
            this._background.ResumeLayout(false);
            this._background.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._knob)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel _background;
        private Krypton.Toolkit.KryptonLabel _onLabel;
        private Krypton.Toolkit.KryptonLabel _offLabel;
        private System.Windows.Forms.PictureBox _knob;
    }
}
