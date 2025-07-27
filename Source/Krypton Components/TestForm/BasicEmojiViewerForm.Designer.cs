namespace TestForm
{
    partial class BasicEmojiViewerForm
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
            this.klblEmojis = new Krypton.Toolkit.KryptonLabel();
            this.klbEmojis = new Krypton.Toolkit.KryptonListBox();
            this.ktxtSearch = new Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.klblEmojis);
            this.kryptonPanel1.Controls.Add(this.klbEmojis);
            this.kryptonPanel1.Controls.Add(this.ktxtSearch);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(329, 332);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // klblEmojis
            // 
            this.klblEmojis.Location = new System.Drawing.Point(13, 308);
            this.klblEmojis.Name = "klblEmojis";
            this.klblEmojis.Size = new System.Drawing.Size(88, 20);
            this.klblEmojis.TabIndex = 2;
            this.klblEmojis.Values.Text = "kryptonLabel1";
            // 
            // klbEmojis
            // 
            this.klbEmojis.Location = new System.Drawing.Point(13, 43);
            this.klbEmojis.Name = "klbEmojis";
            this.klbEmojis.Size = new System.Drawing.Size(289, 258);
            this.klbEmojis.TabIndex = 1;
            this.klbEmojis.SelectedIndexChanged += new System.EventHandler(this.klbEmojis_SelectedIndexChanged);
            // 
            // ktxtSearch
            // 
            this.ktxtSearch.CueHint.CueHintText = "Search...";
            this.ktxtSearch.Location = new System.Drawing.Point(13, 13);
            this.ktxtSearch.Name = "ktxtSearch";
            this.ktxtSearch.Size = new System.Drawing.Size(289, 23);
            this.ktxtSearch.TabIndex = 0;
            this.ktxtSearch.TextChanged += new System.EventHandler(this.ktxtSearch_TextChanged);
            // 
            // BasicEmojiViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 332);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "BasicEmojiViewerForm";
            this.Text = "EmojiViewerForm";
            this.Load += new System.EventHandler(this.EmojiViewerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonTextBox ktxtSearch;
        private KryptonListBox klbEmojis;
        private KryptonLabel klblEmojis;
    }
}