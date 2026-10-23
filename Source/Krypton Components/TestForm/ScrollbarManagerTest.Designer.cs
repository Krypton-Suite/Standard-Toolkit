namespace TestForm
{
    partial class ScrollbarManagerTest
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
                _containerManager?.Dispose();
                _textBoxManager?.Dispose();
                _richTextBoxManager?.Dispose();
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
            this.kpnlMain = new Krypton.Toolkit.KryptonPanel();
            this.kpgMain = new Krypton.Toolkit.KryptonGroupBox();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.kpgContainer = new Krypton.Toolkit.KryptonGroupBox();
            this.panelContainer = new Krypton.Toolkit.KryptonPanel();
            this.btnAddContentContainer = new Krypton.Toolkit.KryptonButton();
            this.btnClearContentContainer = new Krypton.Toolkit.KryptonButton();
            this.btnToggleContainerManager = new Krypton.Toolkit.KryptonButton();
            this.lblContainerExample = new Krypton.Toolkit.KryptonLabel();
            this.lblContainerStatus = new Krypton.Toolkit.KryptonLabel();
            this.kpgTextBox = new Krypton.Toolkit.KryptonGroupBox();
            this.textBoxNative = new Krypton.Toolkit.KryptonTextBox();
            this.btnToggleTextBoxManager = new Krypton.Toolkit.KryptonButton();
            this.lblTextBoxExample = new Krypton.Toolkit.KryptonLabel();
            this.lblTextBoxStatus = new Krypton.Toolkit.KryptonLabel();
            this.kpgRichTextBox = new Krypton.Toolkit.KryptonGroupBox();
            this.richTextBoxNative = new Krypton.Toolkit.KryptonRichTextBox();
            this.btnToggleRichTextBoxManager = new Krypton.Toolkit.KryptonButton();
            this.lblRichTextBoxExample = new Krypton.Toolkit.KryptonLabel();
            this.lblRichTextBoxStatus = new Krypton.Toolkit.KryptonLabel();
            this.kpgDynamic = new Krypton.Toolkit.KryptonGroupBox();
            this.panelDynamic = new Krypton.Toolkit.KryptonPanel();
            this.btnAddDynamicContent = new Krypton.Toolkit.KryptonButton();
            this.btnRemoveDynamicContent = new Krypton.Toolkit.KryptonButton();
            this.lblDynamicExample = new Krypton.Toolkit.KryptonLabel();
            this.lblDynamicStatus = new Krypton.Toolkit.KryptonLabel();
            this.kpgProperties = new Krypton.Toolkit.KryptonGroupBox();
            this.propertyGrid = new Krypton.Toolkit.KryptonPropertyGrid();
            this.lblPropertiesExample = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).BeginInit();
            this.kpnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpgMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgMain.Panel)).BeginInit();
            this.kpgMain.Panel.SuspendLayout();
            this.kpgMain.SuspendLayout();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpgContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgContainer.Panel)).BeginInit();
            this.kpgContainer.Panel.SuspendLayout();
            this.kpgContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgTextBox.Panel)).BeginInit();
            this.kpgTextBox.Panel.SuspendLayout();
            this.kpgTextBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpgRichTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgRichTextBox.Panel)).BeginInit();
            this.kpgRichTextBox.Panel.SuspendLayout();
            this.kpgRichTextBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpgDynamic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgDynamic.Panel)).BeginInit();
            this.kpgDynamic.Panel.SuspendLayout();
            this.kpgDynamic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelDynamic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgProperties.Panel)).BeginInit();
            this.kpgProperties.Panel.SuspendLayout();
            this.kpgProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // kpnlMain
            // 
            this.kpnlMain.Controls.Add(this.kpgMain);
            this.kpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpnlMain.Location = new System.Drawing.Point(0, 0);
            this.kpnlMain.Name = "kpnlMain";
            this.kpnlMain.Size = new System.Drawing.Size(1029, 693);
            this.kpnlMain.TabIndex = 0;
            // 
            // kpgMain
            // 
            this.kpgMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpgMain.Location = new System.Drawing.Point(0, 0);
            // 
            // kpgMain.Panel
            // 
            this.kpgMain.Panel.Controls.Add(this.tlpMain);
            this.kpgMain.Size = new System.Drawing.Size(1029, 693);
            this.kpgMain.TabIndex = 0;
            this.kpgMain.Values.Heading = "KryptonScrollbarManager Demo";
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.kpgContainer, 0, 0);
            this.tlpMain.Controls.Add(this.kpgTextBox, 1, 0);
            this.tlpMain.Controls.Add(this.kpgRichTextBox, 0, 1);
            this.tlpMain.Controls.Add(this.kpgDynamic, 1, 1);
            this.tlpMain.Controls.Add(this.kpgProperties, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpMain.Size = new System.Drawing.Size(1025, 669);
            this.tlpMain.TabIndex = 0;
            // 
            // kpgContainer
            // 
            this.kpgContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpgContainer.Location = new System.Drawing.Point(3, 3);
            // 
            // kpgContainer.Panel
            // 
            this.kpgContainer.Panel.Controls.Add(this.panelContainer);
            this.kpgContainer.Panel.Controls.Add(this.btnAddContentContainer);
            this.kpgContainer.Panel.Controls.Add(this.btnClearContentContainer);
            this.kpgContainer.Panel.Controls.Add(this.btnToggleContainerManager);
            this.kpgContainer.Panel.Controls.Add(this.lblContainerExample);
            this.kpgContainer.Panel.Controls.Add(this.lblContainerStatus);
            this.kpgContainer.Size = new System.Drawing.Size(506, 217);
            this.kpgContainer.TabIndex = 0;
            this.kpgContainer.Values.Heading = "Container Mode";
            // 
            // panelContainer
            // 
            this.panelContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContainer.Location = new System.Drawing.Point(9, 52);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(489, 120);
            this.panelContainer.TabIndex = 0;
            // 
            // btnAddContentContainer
            // 
            this.btnAddContentContainer.Location = new System.Drawing.Point(9, 191);
            this.btnAddContentContainer.Name = "btnAddContentContainer";
            this.btnAddContentContainer.Size = new System.Drawing.Size(86, 22);
            this.btnAddContentContainer.TabIndex = 1;
            this.btnAddContentContainer.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnAddContentContainer.Values.Text = "Add Content";
            // 
            // btnClearContentContainer
            // 
            this.btnClearContentContainer.Location = new System.Drawing.Point(103, 191);
            this.btnClearContentContainer.Name = "btnClearContentContainer";
            this.btnClearContentContainer.Size = new System.Drawing.Size(86, 22);
            this.btnClearContentContainer.TabIndex = 2;
            this.btnClearContentContainer.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnClearContentContainer.Values.Text = "Clear";
            // 
            // btnToggleContainerManager
            // 
            this.btnToggleContainerManager.Location = new System.Drawing.Point(197, 191);
            this.btnToggleContainerManager.Name = "btnToggleContainerManager";
            this.btnToggleContainerManager.Size = new System.Drawing.Size(103, 22);
            this.btnToggleContainerManager.TabIndex = 3;
            this.btnToggleContainerManager.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnToggleContainerManager.Values.Text = "Toggle Manager";
            // 
            // lblContainerExample
            // 
            this.lblContainerExample.Location = new System.Drawing.Point(9, 9);
            this.lblContainerExample.Name = "lblContainerExample";
            this.lblContainerExample.Size = new System.Drawing.Size(171, 20);
            this.lblContainerExample.TabIndex = 4;
            this.lblContainerExample.Values.Text = "Container Mode Example";
            // 
            // lblContainerStatus
            // 
            this.lblContainerStatus.Location = new System.Drawing.Point(9, 30);
            this.lblContainerStatus.Name = "lblContainerStatus";
            this.lblContainerStatus.Size = new System.Drawing.Size(171, 20);
            this.lblContainerStatus.TabIndex = 5;
            this.lblContainerStatus.Values.Text = "Status: Ready";
            // 
            // kpgTextBox
            // 
            this.kpgTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpgTextBox.Location = new System.Drawing.Point(515, 3);
            // 
            // kpgTextBox.Panel
            // 
            this.kpgTextBox.Panel.Controls.Add(this.textBoxNative);
            this.kpgTextBox.Panel.Controls.Add(this.btnToggleTextBoxManager);
            this.kpgTextBox.Panel.Controls.Add(this.lblTextBoxExample);
            this.kpgTextBox.Panel.Controls.Add(this.lblTextBoxStatus);
            this.kpgTextBox.Size = new System.Drawing.Size(507, 217);
            this.kpgTextBox.TabIndex = 1;
            this.kpgTextBox.Values.Heading = "Native Wrapper - TextBox";
            // 
            // textBoxNative
            // 
            this.textBoxNative.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNative.Location = new System.Drawing.Point(15, 52);
            this.textBoxNative.Multiline = true;
            this.textBoxNative.Name = "textBoxNative";
            this.textBoxNative.Size = new System.Drawing.Size(490, 120);
            this.textBoxNative.TabIndex = 0;
            this.textBoxNative.UseKryptonScrollbars = true;
            // 
            // btnToggleTextBoxManager
            // 
            this.btnToggleTextBoxManager.Location = new System.Drawing.Point(9, 191);
            this.btnToggleTextBoxManager.Name = "btnToggleTextBoxManager";
            this.btnToggleTextBoxManager.Size = new System.Drawing.Size(103, 22);
            this.btnToggleTextBoxManager.TabIndex = 1;
            this.btnToggleTextBoxManager.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnToggleTextBoxManager.Values.Text = "Toggle Manager";
            // 
            // lblTextBoxExample
            // 
            this.lblTextBoxExample.Location = new System.Drawing.Point(9, 9);
            this.lblTextBoxExample.Name = "lblTextBoxExample";
            this.lblTextBoxExample.Size = new System.Drawing.Size(181, 20);
            this.lblTextBoxExample.TabIndex = 2;
            this.lblTextBoxExample.Values.Text = "Native Wrapper Mode Example";
            // 
            // lblTextBoxStatus
            // 
            this.lblTextBoxStatus.Location = new System.Drawing.Point(9, 30);
            this.lblTextBoxStatus.Name = "lblTextBoxStatus";
            this.lblTextBoxStatus.Size = new System.Drawing.Size(171, 20);
            this.lblTextBoxStatus.TabIndex = 3;
            this.lblTextBoxStatus.Values.Text = "Status: Ready";
            // 
            // kpgRichTextBox
            // 
            this.kpgRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpgRichTextBox.Location = new System.Drawing.Point(3, 226);
            // 
            // kpgRichTextBox.Panel
            // 
            this.kpgRichTextBox.Panel.Controls.Add(this.richTextBoxNative);
            this.kpgRichTextBox.Panel.Controls.Add(this.btnToggleRichTextBoxManager);
            this.kpgRichTextBox.Panel.Controls.Add(this.lblRichTextBoxExample);
            this.kpgRichTextBox.Panel.Controls.Add(this.lblRichTextBoxStatus);
            this.kpgRichTextBox.Size = new System.Drawing.Size(506, 217);
            this.kpgRichTextBox.TabIndex = 2;
            this.kpgRichTextBox.Values.Heading = "Native Wrapper - RichTextBox";
            // 
            // richTextBoxNative
            // 
            this.richTextBoxNative.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxNative.Location = new System.Drawing.Point(9, 52);
            this.richTextBoxNative.Name = "richTextBoxNative";
            this.richTextBoxNative.Size = new System.Drawing.Size(489, 120);
            this.richTextBoxNative.TabIndex = 0;
            this.richTextBoxNative.Text = "";
            this.richTextBoxNative.UseKryptonScrollbars = true;
            // 
            // btnToggleRichTextBoxManager
            // 
            this.btnToggleRichTextBoxManager.Location = new System.Drawing.Point(9, 191);
            this.btnToggleRichTextBoxManager.Name = "btnToggleRichTextBoxManager";
            this.btnToggleRichTextBoxManager.Size = new System.Drawing.Size(103, 22);
            this.btnToggleRichTextBoxManager.TabIndex = 1;
            this.btnToggleRichTextBoxManager.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnToggleRichTextBoxManager.Values.Text = "Toggle Manager";
            // 
            // lblRichTextBoxExample
            // 
            this.lblRichTextBoxExample.Location = new System.Drawing.Point(9, 9);
            this.lblRichTextBoxExample.Name = "lblRichTextBoxExample";
            this.lblRichTextBoxExample.Size = new System.Drawing.Size(181, 20);
            this.lblRichTextBoxExample.TabIndex = 2;
            this.lblRichTextBoxExample.Values.Text = "Native Wrapper Mode Example";
            // 
            // lblRichTextBoxStatus
            // 
            this.lblRichTextBoxStatus.Location = new System.Drawing.Point(9, 30);
            this.lblRichTextBoxStatus.Name = "lblRichTextBoxStatus";
            this.lblRichTextBoxStatus.Size = new System.Drawing.Size(171, 20);
            this.lblRichTextBoxStatus.TabIndex = 3;
            this.lblRichTextBoxStatus.Values.Text = "Status: Ready";
            // 
            // kpgDynamic
            // 
            this.kpgDynamic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpgDynamic.Location = new System.Drawing.Point(515, 226);
            // 
            // kpgDynamic.Panel
            // 
            this.kpgDynamic.Panel.Controls.Add(this.panelDynamic);
            this.kpgDynamic.Panel.Controls.Add(this.btnAddDynamicContent);
            this.kpgDynamic.Panel.Controls.Add(this.btnRemoveDynamicContent);
            this.kpgDynamic.Panel.Controls.Add(this.lblDynamicExample);
            this.kpgDynamic.Panel.Controls.Add(this.lblDynamicStatus);
            this.kpgDynamic.Size = new System.Drawing.Size(507, 217);
            this.kpgDynamic.TabIndex = 3;
            this.kpgDynamic.Values.Heading = "Dynamic Content";
            // 
            // panelDynamic
            // 
            this.panelDynamic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDynamic.Location = new System.Drawing.Point(9, 52);
            this.panelDynamic.Name = "panelDynamic";
            this.panelDynamic.Size = new System.Drawing.Size(490, 120);
            this.panelDynamic.TabIndex = 0;
            // 
            // btnAddDynamicContent
            // 
            this.btnAddDynamicContent.Location = new System.Drawing.Point(9, 191);
            this.btnAddDynamicContent.Name = "btnAddDynamicContent";
            this.btnAddDynamicContent.Size = new System.Drawing.Size(86, 22);
            this.btnAddDynamicContent.TabIndex = 1;
            this.btnAddDynamicContent.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnAddDynamicContent.Values.Text = "Add Control";
            // 
            // btnRemoveDynamicContent
            // 
            this.btnRemoveDynamicContent.Location = new System.Drawing.Point(103, 191);
            this.btnRemoveDynamicContent.Name = "btnRemoveDynamicContent";
            this.btnRemoveDynamicContent.Size = new System.Drawing.Size(86, 22);
            this.btnRemoveDynamicContent.TabIndex = 2;
            this.btnRemoveDynamicContent.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnRemoveDynamicContent.Values.Text = "Remove Last";
            // 
            // lblDynamicExample
            // 
            this.lblDynamicExample.Location = new System.Drawing.Point(9, 9);
            this.lblDynamicExample.Name = "lblDynamicExample";
            this.lblDynamicExample.Size = new System.Drawing.Size(171, 20);
            this.lblDynamicExample.TabIndex = 3;
            this.lblDynamicExample.Values.Text = "Dynamic Content Example";
            // 
            // lblDynamicStatus
            // 
            this.lblDynamicStatus.Location = new System.Drawing.Point(9, 30);
            this.lblDynamicStatus.Name = "lblDynamicStatus";
            this.lblDynamicStatus.Size = new System.Drawing.Size(171, 20);
            this.lblDynamicStatus.TabIndex = 4;
            this.lblDynamicStatus.Values.Text = "Status: Ready";
            // 
            // kpgProperties
            // 
            this.kpgProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpgProperties.Location = new System.Drawing.Point(3, 449);
            // 
            // kpgProperties.Panel
            // 
            this.kpgProperties.Panel.Controls.Add(this.propertyGrid);
            this.kpgProperties.Panel.Controls.Add(this.lblPropertiesExample);
            this.kpgProperties.Size = new System.Drawing.Size(506, 217);
            this.kpgProperties.TabIndex = 4;
            this.kpgProperties.Values.Heading = "Scrollbar Properties";
            // 
            // propertyGrid
            // 
            this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid.Location = new System.Drawing.Point(9, 52);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Padding = new System.Windows.Forms.Padding(1);
            this.propertyGrid.SelectedObject = this.kpgDynamic;
            this.propertyGrid.Size = new System.Drawing.Size(489, 153);
            this.propertyGrid.TabIndex = 0;
            // 
            // lblPropertiesExample
            // 
            this.lblPropertiesExample.Location = new System.Drawing.Point(9, 9);
            this.lblPropertiesExample.Name = "lblPropertiesExample";
            this.lblPropertiesExample.Size = new System.Drawing.Size(171, 20);
            this.lblPropertiesExample.TabIndex = 1;
            this.lblPropertiesExample.Values.Text = "Properties Example";
            // 
            // ScrollbarManagerTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 693);
            this.Controls.Add(this.kpnlMain);
            this.Name = "ScrollbarManagerTest";
            this.Text = "KryptonScrollbarManager Demo";
            ((System.ComponentModel.ISupportInitialize)(this.kpnlMain)).EndInit();
            this.kpnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpgMain.Panel)).EndInit();
            this.kpgMain.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpgMain)).EndInit();
            this.kpgMain.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpgContainer.Panel)).EndInit();
            this.kpgContainer.Panel.ResumeLayout(false);
            this.kpgContainer.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpgContainer)).EndInit();
            this.kpgContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelContainer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgTextBox.Panel)).EndInit();
            this.kpgTextBox.Panel.ResumeLayout(false);
            this.kpgTextBox.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpgTextBox)).EndInit();
            this.kpgTextBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpgRichTextBox.Panel)).EndInit();
            this.kpgRichTextBox.Panel.ResumeLayout(false);
            this.kpgRichTextBox.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpgRichTextBox)).EndInit();
            this.kpgRichTextBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kpgDynamic.Panel)).EndInit();
            this.kpgDynamic.Panel.ResumeLayout(false);
            this.kpgDynamic.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpgDynamic)).EndInit();
            this.kpgDynamic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelDynamic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kpgProperties.Panel)).EndInit();
            this.kpgProperties.Panel.ResumeLayout(false);
            this.kpgProperties.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kpgProperties)).EndInit();
            this.kpgProperties.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kpnlMain;
        private Krypton.Toolkit.KryptonGroupBox kpgMain;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private Krypton.Toolkit.KryptonGroupBox kpgContainer;
        private Krypton.Toolkit.KryptonPanel panelContainer;
        private Krypton.Toolkit.KryptonButton btnAddContentContainer;
        private Krypton.Toolkit.KryptonButton btnClearContentContainer;
        private Krypton.Toolkit.KryptonButton btnToggleContainerManager;
        private Krypton.Toolkit.KryptonLabel lblContainerExample;
        private Krypton.Toolkit.KryptonLabel lblContainerStatus;
        private Krypton.Toolkit.KryptonGroupBox kpgTextBox;
        private Krypton.Toolkit.KryptonTextBox textBoxNative;
        private Krypton.Toolkit.KryptonButton btnToggleTextBoxManager;
        private Krypton.Toolkit.KryptonLabel lblTextBoxExample;
        private Krypton.Toolkit.KryptonLabel lblTextBoxStatus;
        private Krypton.Toolkit.KryptonGroupBox kpgRichTextBox;
        private Krypton.Toolkit.KryptonRichTextBox richTextBoxNative;
        private Krypton.Toolkit.KryptonButton btnToggleRichTextBoxManager;
        private Krypton.Toolkit.KryptonLabel lblRichTextBoxExample;
        private Krypton.Toolkit.KryptonLabel lblRichTextBoxStatus;
        private Krypton.Toolkit.KryptonGroupBox kpgDynamic;
        private Krypton.Toolkit.KryptonPanel panelDynamic;
        private Krypton.Toolkit.KryptonButton btnAddDynamicContent;
        private Krypton.Toolkit.KryptonButton btnRemoveDynamicContent;
        private Krypton.Toolkit.KryptonLabel lblDynamicExample;
        private Krypton.Toolkit.KryptonLabel lblDynamicStatus;
        private Krypton.Toolkit.KryptonGroupBox kpgProperties;
        private Krypton.Toolkit.KryptonPropertyGrid propertyGrid;
        private Krypton.Toolkit.KryptonLabel lblPropertiesExample;
    }
}