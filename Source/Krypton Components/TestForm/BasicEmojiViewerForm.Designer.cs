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
            this.kcbEmojis = new Krypton.Toolkit.KryptonComboBox();
            this.klvEmojis = new Krypton.Toolkit.KryptonListView();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcbEmojis)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.klblEmojis);
            this.kryptonPanel1.Controls.Add(this.klbEmojis);
            this.kryptonPanel1.Controls.Add(this.ktxtSearch);
            this.kryptonPanel1.Controls.Add(this.kcbEmojis);
            this.kryptonPanel1.Controls.Add(this.klvEmojis);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(558, 350);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // klblEmojis
            // 
            this.klblEmojis.Location = new System.Drawing.Point(13, 308);
            this.klblEmojis.Name = "klblEmojis";
            this.klblEmojis.Size = new System.Drawing.Size(88, 20);
            this.klblEmojis.TabIndex = 2;
            this.klblEmojis.TabStop = false;
            this.klblEmojis.Values.Text = "kryptonLabel1";
            // 
            // klbEmojis
            // 
            this.klbEmojis.Location = new System.Drawing.Point(13, 43);
            this.klbEmojis.Name = "klbEmojis";
            this.klbEmojis.Size = new System.Drawing.Size(250, 258);
            this.klbEmojis.TabIndex = 1;
            this.klbEmojis.SelectedIndexChanged += new System.EventHandler(this.klbEmojis_SelectedIndexChanged);
            // 
            // ktxtSearch
            // 
            this.ktxtSearch.CueHint.CueHintText = "Search...";
            this.ktxtSearch.Location = new System.Drawing.Point(13, 14);
            this.ktxtSearch.Name = "ktxtSearch";
            this.ktxtSearch.Size = new System.Drawing.Size(250, 23);
            this.ktxtSearch.TabIndex = 0;
            this.ktxtSearch.TextChanged += new System.EventHandler(this.ktxtSearch_TextChanged);
            // 
            // kcbEmojis
            // 
            this.kcbEmojis.DropDownWidth = 250;
            this.kcbEmojis.Location = new System.Drawing.Point(280, 15);
            this.kcbEmojis.Name = "kcbEmojis";
            this.kcbEmojis.Size = new System.Drawing.Size(250, 22);
            this.kcbEmojis.TabIndex = 2;
            // 
            // klvEmojis
            // 
            this.klvEmojis.HideSelection = false;
            this.klvEmojis.Location = new System.Drawing.Point(280, 43);
            this.klvEmojis.Name = "klvEmojis";
            this.klvEmojis.Size = new System.Drawing.Size(250, 258);
            this.klvEmojis.TabIndex = 3;
            this.klvEmojis.View = System.Windows.Forms.View.Details;
            // 
            // BasicEmojiViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 350);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "BasicEmojiViewerForm";
            this.Text = "EmojiViewerForm";
            this.Load += new System.EventHandler(this.EmojiViewerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcbEmojis)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel kryptonPanel1;
        private KryptonTextBox ktxtSearch;
        private KryptonListBox klbEmojis;
        private KryptonLabel klblEmojis;
        private KryptonComboBox kcbEmojis;
        private KryptonListView klvEmojis;
    }
}