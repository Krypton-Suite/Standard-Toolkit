namespace TestForm
{
    partial class RibbonDetachableTest
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
            this.SuspendLayout();
            // 
            // RibbonDetachableTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 520);
            this.CloseBox = false;
            this.Name = "RibbonDetachableTest";
            this.Text = "Detachable Ribbons Test - Issue #595";
            this.ResumeLayout(false);

        }

        #endregion

        #region Designer Fields

        private Krypton.Ribbon.KryptonRibbon? _ribbon;
        private Krypton.Toolkit.KryptonButton? _btnDetach;
        private Krypton.Toolkit.KryptonButton? _btnReattach;
        private Krypton.Toolkit.KryptonLabel? _lblStatus;
        private Krypton.Toolkit.ButtonSpecAny? _detachButton;
        private Krypton.Toolkit.ButtonSpecAny? _reattachButton;

        #endregion
    }
}