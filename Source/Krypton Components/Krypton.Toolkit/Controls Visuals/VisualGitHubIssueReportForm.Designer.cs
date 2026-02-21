namespace Krypton.Toolkit
{
    partial class VisualGitHubIssueReportForm
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
            this.pnlMain = new Krypton.Toolkit.KryptonPanel();
            this.pnlScroll = new System.Windows.Forms.Panel();
            this.tlpContent = new System.Windows.Forms.TableLayoutPanel();
            this.kwlblSummary = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbSummary = new Krypton.Toolkit.KryptonTextBox();
            this.kwlblDescription = new Krypton.Toolkit.KryptonWrapLabel();
            this.krtbDescription = new Krypton.Toolkit.KryptonRichTextBox();
            this.kwlblStepsToReproduce = new Krypton.Toolkit.KryptonWrapLabel();
            this.krtbStepsToReproduce = new Krypton.Toolkit.KryptonRichTextBox();
            this.kwlblExpectedBehavior = new Krypton.Toolkit.KryptonWrapLabel();
            this.krtbExpectedBehavior = new Krypton.Toolkit.KryptonRichTextBox();
            this.kwlblActualBehavior = new Krypton.Toolkit.KryptonWrapLabel();
            this.krtbActualBehavior = new Krypton.Toolkit.KryptonRichTextBox();
            this.kwlblOs = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbOs = new Krypton.Toolkit.KryptonTextBox();
            this.kwlblOsVersion = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbOsVersion = new Krypton.Toolkit.KryptonTextBox();
            this.kwlblFrameworkVersion = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbFrameworkVersion = new Krypton.Toolkit.KryptonTextBox();
            this.kwlblToolkitVersion = new Krypton.Toolkit.KryptonWrapLabel();
            this.ktbToolkitVersion = new Krypton.Toolkit.KryptonTextBox();
            this.kwlblAdditionalInfo = new Krypton.Toolkit.KryptonWrapLabel();
            this.krtbAdditionalInfo = new Krypton.Toolkit.KryptonRichTextBox();
            this.kwlblAreasAffected = new Krypton.Toolkit.KryptonWrapLabel();
            this.kcmbAreasAffected = new Krypton.Toolkit.KryptonComboBox();
            this.pnlButtons = new Krypton.Toolkit.KryptonPanel();
            this.kbtnCreate = new Krypton.Toolkit.KryptonButton();
            this.kbtnCancel = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlScroll.SuspendLayout();
            this.tlpContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbAreasAffected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButtons)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlScroll);
            this.pnlMain.Controls.Add(this.pnlButtons);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(584, 461);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlScroll
            // 
            this.pnlScroll.AutoScroll = true;
            this.pnlScroll.BackColor = System.Drawing.Color.Transparent;
            this.pnlScroll.Controls.Add(this.tlpContent);
            this.pnlScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScroll.Location = new System.Drawing.Point(0, 0);
            this.pnlScroll.Name = "pnlScroll";
            this.pnlScroll.Padding = new System.Windows.Forms.Padding(12);
            this.pnlScroll.Size = new System.Drawing.Size(584, 406);
            this.pnlScroll.TabIndex = 0;
            // 
            // tlpContent
            // 
            this.tlpContent.ColumnCount = 2;
            this.tlpContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tlpContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContent.Controls.Add(this.kwlblSummary, 0, 0);
            this.tlpContent.Controls.Add(this.ktbSummary, 1, 0);
            this.tlpContent.Controls.Add(this.kwlblDescription, 0, 1);
            this.tlpContent.Controls.Add(this.krtbDescription, 1, 1);
            this.tlpContent.Controls.Add(this.kwlblStepsToReproduce, 0, 2);
            this.tlpContent.Controls.Add(this.krtbStepsToReproduce, 1, 2);
            this.tlpContent.Controls.Add(this.kwlblExpectedBehavior, 0, 3);
            this.tlpContent.Controls.Add(this.krtbExpectedBehavior, 1, 3);
            this.tlpContent.Controls.Add(this.kwlblActualBehavior, 0, 4);
            this.tlpContent.Controls.Add(this.krtbActualBehavior, 1, 4);
            this.tlpContent.Controls.Add(this.kwlblOs, 0, 5);
            this.tlpContent.Controls.Add(this.ktbOs, 1, 5);
            this.tlpContent.Controls.Add(this.kwlblOsVersion, 0, 6);
            this.tlpContent.Controls.Add(this.ktbOsVersion, 1, 6);
            this.tlpContent.Controls.Add(this.kwlblFrameworkVersion, 0, 7);
            this.tlpContent.Controls.Add(this.ktbFrameworkVersion, 1, 7);
            this.tlpContent.Controls.Add(this.kwlblToolkitVersion, 0, 8);
            this.tlpContent.Controls.Add(this.ktbToolkitVersion, 1, 8);
            this.tlpContent.Controls.Add(this.kwlblAdditionalInfo, 0, 9);
            this.tlpContent.Controls.Add(this.krtbAdditionalInfo, 1, 9);
            this.tlpContent.Controls.Add(this.kwlblAreasAffected, 0, 10);
            this.tlpContent.Controls.Add(this.kcmbAreasAffected, 1, 10);
            this.tlpContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpContent.Location = new System.Drawing.Point(12, 12);
            this.tlpContent.Name = "tlpContent";
            this.tlpContent.RowCount = 11;
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpContent.Size = new System.Drawing.Size(543, 448);
            this.tlpContent.TabIndex = 0;
            // 
            // kwlblSummary
            // 
            this.kwlblSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblSummary.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kwlblSummary.Location = new System.Drawing.Point(0, 0);
            this.kwlblSummary.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblSummary.Name = "kwlblSummary";
            this.kwlblSummary.Padding = new System.Windows.Forms.Padding(5);
            this.kwlblSummary.Size = new System.Drawing.Size(132, 28);
            this.kwlblSummary.Text = "Summary:";
            this.kwlblSummary.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ktbSummary
            // 
            this.ktbSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ktbSummary.Location = new System.Drawing.Point(140, 2);
            this.ktbSummary.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.ktbSummary.Name = "ktbSummary";
            this.ktbSummary.Size = new System.Drawing.Size(403, 23);
            this.ktbSummary.TabIndex = 0;
            // 
            // kwlblDescription
            // 
            this.kwlblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblDescription.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kwlblDescription.Location = new System.Drawing.Point(0, 28);
            this.kwlblDescription.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblDescription.Name = "kwlblDescription";
            this.kwlblDescription.Padding = new System.Windows.Forms.Padding(5);
            this.kwlblDescription.Size = new System.Drawing.Size(132, 56);
            this.kwlblDescription.Text = "Description:";
            this.kwlblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // krtbDescription
            // 
            this.krtbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.krtbDescription.Location = new System.Drawing.Point(140, 30);
            this.krtbDescription.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.krtbDescription.Name = "krtbDescription";
            this.krtbDescription.Size = new System.Drawing.Size(403, 52);
            this.krtbDescription.TabIndex = 1;
            this.krtbDescription.Text = "";
            // 
            // kwlblStepsToReproduce
            // 
            this.kwlblStepsToReproduce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblStepsToReproduce.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kwlblStepsToReproduce.Location = new System.Drawing.Point(0, 84);
            this.kwlblStepsToReproduce.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblStepsToReproduce.Name = "kwlblStepsToReproduce";
            this.kwlblStepsToReproduce.Padding = new System.Windows.Forms.Padding(5);
            this.kwlblStepsToReproduce.Size = new System.Drawing.Size(132, 56);
            this.kwlblStepsToReproduce.Text = "Steps to Reproduce:";
            this.kwlblStepsToReproduce.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // krtbStepsToReproduce
            // 
            this.krtbStepsToReproduce.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.krtbStepsToReproduce.Location = new System.Drawing.Point(140, 86);
            this.krtbStepsToReproduce.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.krtbStepsToReproduce.Name = "krtbStepsToReproduce";
            this.krtbStepsToReproduce.Size = new System.Drawing.Size(403, 52);
            this.krtbStepsToReproduce.TabIndex = 2;
            this.krtbStepsToReproduce.Text = "";
            // 
            // kwlblExpectedBehavior
            // 
            this.kwlblExpectedBehavior.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblExpectedBehavior.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kwlblExpectedBehavior.Location = new System.Drawing.Point(0, 140);
            this.kwlblExpectedBehavior.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblExpectedBehavior.Name = "kwlblExpectedBehavior";
            this.kwlblExpectedBehavior.Padding = new System.Windows.Forms.Padding(5);
            this.kwlblExpectedBehavior.Size = new System.Drawing.Size(132, 56);
            this.kwlblExpectedBehavior.Text = "Expected Behavior:";
            this.kwlblExpectedBehavior.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // krtbExpectedBehavior
            // 
            this.krtbExpectedBehavior.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.krtbExpectedBehavior.Location = new System.Drawing.Point(140, 142);
            this.krtbExpectedBehavior.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.krtbExpectedBehavior.Name = "krtbExpectedBehavior";
            this.krtbExpectedBehavior.Size = new System.Drawing.Size(403, 52);
            this.krtbExpectedBehavior.TabIndex = 3;
            this.krtbExpectedBehavior.Text = "";
            // 
            // kwlblActualBehavior
            // 
            this.kwlblActualBehavior.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblActualBehavior.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kwlblActualBehavior.Location = new System.Drawing.Point(0, 196);
            this.kwlblActualBehavior.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblActualBehavior.Name = "kwlblActualBehavior";
            this.kwlblActualBehavior.Padding = new System.Windows.Forms.Padding(5);
            this.kwlblActualBehavior.Size = new System.Drawing.Size(132, 56);
            this.kwlblActualBehavior.Text = "Actual Behavior:";
            this.kwlblActualBehavior.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // krtbActualBehavior
            // 
            this.krtbActualBehavior.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.krtbActualBehavior.Location = new System.Drawing.Point(140, 198);
            this.krtbActualBehavior.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.krtbActualBehavior.Name = "krtbActualBehavior";
            this.krtbActualBehavior.Size = new System.Drawing.Size(403, 52);
            this.krtbActualBehavior.TabIndex = 4;
            this.krtbActualBehavior.Text = "";
            // 
            // kwlblOs
            // 
            this.kwlblOs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblOs.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kwlblOs.Location = new System.Drawing.Point(0, 252);
            this.kwlblOs.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblOs.Name = "kwlblOs";
            this.kwlblOs.Padding = new System.Windows.Forms.Padding(5);
            this.kwlblOs.Size = new System.Drawing.Size(132, 28);
            this.kwlblOs.Text = "Operating System:";
            this.kwlblOs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ktbOs
            // 
            this.ktbOs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ktbOs.Location = new System.Drawing.Point(140, 254);
            this.ktbOs.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.ktbOs.Name = "ktbOs";
            this.ktbOs.Size = new System.Drawing.Size(403, 23);
            this.ktbOs.TabIndex = 5;
            // 
            // kwlblOsVersion
            // 
            this.kwlblOsVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblOsVersion.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kwlblOsVersion.Location = new System.Drawing.Point(0, 280);
            this.kwlblOsVersion.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblOsVersion.Name = "kwlblOsVersion";
            this.kwlblOsVersion.Padding = new System.Windows.Forms.Padding(5);
            this.kwlblOsVersion.Size = new System.Drawing.Size(132, 28);
            this.kwlblOsVersion.Text = "OS Version:";
            this.kwlblOsVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ktbOsVersion
            // 
            this.ktbOsVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ktbOsVersion.Location = new System.Drawing.Point(140, 282);
            this.ktbOsVersion.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.ktbOsVersion.Name = "ktbOsVersion";
            this.ktbOsVersion.Size = new System.Drawing.Size(403, 23);
            this.ktbOsVersion.TabIndex = 6;
            // 
            // kwlblFrameworkVersion
            // 
            this.kwlblFrameworkVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblFrameworkVersion.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kwlblFrameworkVersion.Location = new System.Drawing.Point(0, 308);
            this.kwlblFrameworkVersion.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblFrameworkVersion.Name = "kwlblFrameworkVersion";
            this.kwlblFrameworkVersion.Padding = new System.Windows.Forms.Padding(5);
            this.kwlblFrameworkVersion.Size = new System.Drawing.Size(132, 28);
            this.kwlblFrameworkVersion.Text = "Framework/.NET:";
            this.kwlblFrameworkVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ktbFrameworkVersion
            // 
            this.ktbFrameworkVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ktbFrameworkVersion.Location = new System.Drawing.Point(140, 310);
            this.ktbFrameworkVersion.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.ktbFrameworkVersion.Name = "ktbFrameworkVersion";
            this.ktbFrameworkVersion.Size = new System.Drawing.Size(403, 23);
            this.ktbFrameworkVersion.TabIndex = 7;
            // 
            // kwlblToolkitVersion
            // 
            this.kwlblToolkitVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblToolkitVersion.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kwlblToolkitVersion.Location = new System.Drawing.Point(0, 336);
            this.kwlblToolkitVersion.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblToolkitVersion.Name = "kwlblToolkitVersion";
            this.kwlblToolkitVersion.Padding = new System.Windows.Forms.Padding(5);
            this.kwlblToolkitVersion.Size = new System.Drawing.Size(132, 28);
            this.kwlblToolkitVersion.Text = "Toolkit Version:";
            this.kwlblToolkitVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ktbToolkitVersion
            // 
            this.ktbToolkitVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ktbToolkitVersion.Location = new System.Drawing.Point(140, 338);
            this.ktbToolkitVersion.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.ktbToolkitVersion.Name = "ktbToolkitVersion";
            this.ktbToolkitVersion.Size = new System.Drawing.Size(403, 23);
            this.ktbToolkitVersion.TabIndex = 8;
            // 
            // kwlblAdditionalInfo
            // 
            this.kwlblAdditionalInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblAdditionalInfo.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kwlblAdditionalInfo.Location = new System.Drawing.Point(0, 364);
            this.kwlblAdditionalInfo.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblAdditionalInfo.Name = "kwlblAdditionalInfo";
            this.kwlblAdditionalInfo.Padding = new System.Windows.Forms.Padding(5);
            this.kwlblAdditionalInfo.Size = new System.Drawing.Size(132, 56);
            this.kwlblAdditionalInfo.Text = "Additional Information:";
            this.kwlblAdditionalInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // krtbAdditionalInfo
            // 
            this.krtbAdditionalInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.krtbAdditionalInfo.Location = new System.Drawing.Point(140, 366);
            this.krtbAdditionalInfo.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.krtbAdditionalInfo.Name = "krtbAdditionalInfo";
            this.krtbAdditionalInfo.Size = new System.Drawing.Size(403, 52);
            this.krtbAdditionalInfo.TabIndex = 9;
            this.krtbAdditionalInfo.Text = "";
            // 
            // kwlblAreasAffected
            // 
            this.kwlblAreasAffected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kwlblAreasAffected.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kwlblAreasAffected.Location = new System.Drawing.Point(0, 420);
            this.kwlblAreasAffected.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.kwlblAreasAffected.Name = "kwlblAreasAffected";
            this.kwlblAreasAffected.Padding = new System.Windows.Forms.Padding(5);
            this.kwlblAreasAffected.Size = new System.Drawing.Size(132, 28);
            this.kwlblAreasAffected.Text = "Areas Affected:";
            this.kwlblAreasAffected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kcmbAreasAffected
            // 
            this.kcmbAreasAffected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.kcmbAreasAffected.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kcmbAreasAffected.DropDownWidth = 250;
            this.kcmbAreasAffected.Items.AddRange(new object[] {
            "Docking",
            "Navigator",
            "Ribbon",
            "Toolkit",
            "Workspace"});
            this.kcmbAreasAffected.Location = new System.Drawing.Point(140, 423);
            this.kcmbAreasAffected.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.kcmbAreasAffected.Name = "kcmbAreasAffected";
            this.kcmbAreasAffected.Size = new System.Drawing.Size(403, 22);
            this.kcmbAreasAffected.TabIndex = 10;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.kbtnCreate);
            this.pnlButtons.Controls.Add(this.kbtnCancel);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 406);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.pnlButtons.Size = new System.Drawing.Size(584, 55);
            this.pnlButtons.TabIndex = 1;
            // 
            // kbtnCreate
            // 
            this.kbtnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnCreate.Location = new System.Drawing.Point(333, 15);
            this.kbtnCreate.Name = "kbtnCreate";
            this.kbtnCreate.Size = new System.Drawing.Size(120, 28);
            this.kbtnCreate.TabIndex = 0;
            this.kbtnCreate.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCreate.Values.Text = "Create on GitHub";
            this.kbtnCreate.Click += new System.EventHandler(this.kbtnCreate_Click);
            // 
            // kbtnCancel
            // 
            this.kbtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.kbtnCancel.Location = new System.Drawing.Point(459, 15);
            this.kbtnCancel.Name = "kbtnCancel";
            this.kbtnCancel.Size = new System.Drawing.Size(113, 28);
            this.kbtnCancel.TabIndex = 1;
            this.kbtnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kbtnCancel.Values.Text = "Cancel";
            this.kbtnCancel.Click += new System.EventHandler(this.kbtnCancel_Click);
            // 
            // VisualGitHubIssueReportForm
            // 
            this.AcceptButton = this.kbtnCreate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.kbtnCancel;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "VisualGitHubIssueReportForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Bug Report on GitHub";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlScroll.ResumeLayout(false);
            this.tlpContent.ResumeLayout(false);
            this.tlpContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kcmbAreasAffected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButtons)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel pnlMain;
        private System.Windows.Forms.Panel pnlScroll;
        private System.Windows.Forms.TableLayoutPanel tlpContent;
        private Krypton.Toolkit.KryptonWrapLabel kwlblSummary;
        private Krypton.Toolkit.KryptonTextBox ktbSummary;
        private Krypton.Toolkit.KryptonWrapLabel kwlblDescription;
        private Krypton.Toolkit.KryptonRichTextBox krtbDescription;
        private Krypton.Toolkit.KryptonWrapLabel kwlblStepsToReproduce;
        private Krypton.Toolkit.KryptonRichTextBox krtbStepsToReproduce;
        private Krypton.Toolkit.KryptonWrapLabel kwlblExpectedBehavior;
        private Krypton.Toolkit.KryptonRichTextBox krtbExpectedBehavior;
        private Krypton.Toolkit.KryptonWrapLabel kwlblActualBehavior;
        private Krypton.Toolkit.KryptonRichTextBox krtbActualBehavior;
        private Krypton.Toolkit.KryptonWrapLabel kwlblOs;
        private Krypton.Toolkit.KryptonTextBox ktbOs;
        private Krypton.Toolkit.KryptonWrapLabel kwlblOsVersion;
        private Krypton.Toolkit.KryptonTextBox ktbOsVersion;
        private Krypton.Toolkit.KryptonWrapLabel kwlblFrameworkVersion;
        private Krypton.Toolkit.KryptonTextBox ktbFrameworkVersion;
        private Krypton.Toolkit.KryptonWrapLabel kwlblToolkitVersion;
        private Krypton.Toolkit.KryptonTextBox ktbToolkitVersion;
        private Krypton.Toolkit.KryptonWrapLabel kwlblAdditionalInfo;
        private Krypton.Toolkit.KryptonRichTextBox krtbAdditionalInfo;
        private Krypton.Toolkit.KryptonWrapLabel kwlblAreasAffected;
        private Krypton.Toolkit.KryptonComboBox kcmbAreasAffected;
        private Krypton.Toolkit.KryptonPanel pnlButtons;
        private Krypton.Toolkit.KryptonButton kbtnCreate;
        private Krypton.Toolkit.KryptonButton kbtnCancel;
    }
}