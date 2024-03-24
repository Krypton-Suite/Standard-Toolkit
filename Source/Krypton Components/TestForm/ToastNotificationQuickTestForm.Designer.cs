namespace TestForm
{
    partial class ToastNotificationQuickTestForm
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
            this.kbtnBasicNotificationCheckState = new Krypton.Toolkit.KryptonButton();
            this.kbtnBasicNotificationChecked = new Krypton.Toolkit.KryptonButton();
            this.kbtnBasicNotification = new Krypton.Toolkit.KryptonButton();
            this.kbtnBasicNotificationWithProgressBarCheckState = new Krypton.Toolkit.KryptonButton();
            this.kbtnBasicNotificationWithProgressBarChecked = new Krypton.Toolkit.KryptonButton();
            this.kbtnBasicNotificationWithProgressBar = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kbtnBasicNotificationWithProgressBarCheckState);
            this.kryptonPanel1.Controls.Add(this.kbtnBasicNotificationWithProgressBarChecked);
            this.kryptonPanel1.Controls.Add(this.kbtnBasicNotificationWithProgressBar);
            this.kryptonPanel1.Controls.Add(this.kbtnBasicNotificationCheckState);
            this.kryptonPanel1.Controls.Add(this.kbtnBasicNotificationChecked);
            this.kryptonPanel1.Controls.Add(this.kbtnBasicNotification);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(1083, 470);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kbtnBasicNotificationCheckState
            // 
            this.kbtnBasicNotificationCheckState.Location = new System.Drawing.Point(520, 13);
            this.kbtnBasicNotificationCheckState.Name = "kbtnBasicNotificationCheckState";
            this.kbtnBasicNotificationCheckState.Size = new System.Drawing.Size(288, 25);
            this.kbtnBasicNotificationCheckState.TabIndex = 2;
            this.kbtnBasicNotificationCheckState.Values.Text = "Basic Notification (CheckState)";
            this.kbtnBasicNotificationCheckState.Click += new System.EventHandler(this.kbtnBasicNotificationCheckState_Click);
            // 
            // kbtnBasicNotificationChecked
            // 
            this.kbtnBasicNotificationChecked.Location = new System.Drawing.Point(241, 13);
            this.kbtnBasicNotificationChecked.Name = "kbtnBasicNotificationChecked";
            this.kbtnBasicNotificationChecked.Size = new System.Drawing.Size(273, 25);
            this.kbtnBasicNotificationChecked.TabIndex = 1;
            this.kbtnBasicNotificationChecked.Values.Text = "Basic Notification (Checked)";
            this.kbtnBasicNotificationChecked.Click += new System.EventHandler(this.kbtnBasicNotificationChecked_Click);
            // 
            // kbtnBasicNotification
            // 
            this.kbtnBasicNotification.Location = new System.Drawing.Point(13, 13);
            this.kbtnBasicNotification.Name = "kbtnBasicNotification";
            this.kbtnBasicNotification.Size = new System.Drawing.Size(222, 25);
            this.kbtnBasicNotification.TabIndex = 0;
            this.kbtnBasicNotification.Values.Text = "Basic Notification";
            this.kbtnBasicNotification.Click += new System.EventHandler(this.kbtnBasicNotification_Click);
            // 
            // kbtnBasicNotificationWithProgressBarCheckState
            // 
            this.kbtnBasicNotificationWithProgressBarCheckState.Location = new System.Drawing.Point(520, 44);
            this.kbtnBasicNotificationWithProgressBarCheckState.Name = "kbtnBasicNotificationWithProgressBarCheckState";
            this.kbtnBasicNotificationWithProgressBarCheckState.Size = new System.Drawing.Size(288, 25);
            this.kbtnBasicNotificationWithProgressBarCheckState.TabIndex = 5;
            this.kbtnBasicNotificationWithProgressBarCheckState.Values.Text = "Basic Notification with ProgressBar (CheckState)";
            this.kbtnBasicNotificationWithProgressBarCheckState.Click += new System.EventHandler(this.kbtnBasicNotificationWithProgressBarCheckState_Click);
            // 
            // kbtnBasicNotificationWithProgressBarChecked
            // 
            this.kbtnBasicNotificationWithProgressBarChecked.Location = new System.Drawing.Point(241, 44);
            this.kbtnBasicNotificationWithProgressBarChecked.Name = "kbtnBasicNotificationWithProgressBarChecked";
            this.kbtnBasicNotificationWithProgressBarChecked.Size = new System.Drawing.Size(273, 25);
            this.kbtnBasicNotificationWithProgressBarChecked.TabIndex = 4;
            this.kbtnBasicNotificationWithProgressBarChecked.Values.Text = "Basic Notification with ProgressBar (Checked)";
            this.kbtnBasicNotificationWithProgressBarChecked.Click += new System.EventHandler(this.kbtnBasicNotificationWithProgressBarChecked_Click);
            // 
            // kbtnBasicNotificationWithProgressBar
            // 
            this.kbtnBasicNotificationWithProgressBar.Location = new System.Drawing.Point(13, 44);
            this.kbtnBasicNotificationWithProgressBar.Name = "kbtnBasicNotificationWithProgressBar";
            this.kbtnBasicNotificationWithProgressBar.Size = new System.Drawing.Size(222, 25);
            this.kbtnBasicNotificationWithProgressBar.TabIndex = 3;
            this.kbtnBasicNotificationWithProgressBar.Values.Text = "Basic Notification with ProgressBar";
            this.kbtnBasicNotificationWithProgressBar.Click += new System.EventHandler(this.kbtnBasicNotificationWithProgressBar_Click);
            // 
            // ToastNotificationQuickTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 470);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "ToastNotificationQuickTestForm";
            this.Text = "ToastNotificationQuickTestForm";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonButton kbtnBasicNotification;
        private Krypton.Toolkit.KryptonButton kbtnBasicNotificationCheckState;
        private Krypton.Toolkit.KryptonButton kbtnBasicNotificationChecked;
        private Krypton.Toolkit.KryptonButton kbtnBasicNotificationWithProgressBarCheckState;
        private Krypton.Toolkit.KryptonButton kbtnBasicNotificationWithProgressBarChecked;
        private Krypton.Toolkit.KryptonButton kbtnBasicNotificationWithProgressBar;
    }
}