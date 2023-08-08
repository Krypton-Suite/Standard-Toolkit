namespace TestForm
{
    partial class Form5
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form5));
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonProgressBar1 = new Krypton.Toolkit.KryptonProgressBar();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.kryptonPropertyGrid1 = new Krypton.Toolkit.KryptonPropertyGrid();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.kryptonManager1 = new Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonCustomPaletteBase1 = new Krypton.Toolkit.KryptonCustomPaletteBase(this.components);
            this.buttonSpecAny1 = new Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecAny2 = new Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecAny3 = new Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecAny4 = new Krypton.Toolkit.ButtonSpecAny();
            this.kryptonCommand1 = new Krypton.Toolkit.KryptonCommand();
            this.kryptonStatusStrip1 = new Krypton.Toolkit.KryptonStatusStrip();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonProgressBar1);
            this.kryptonPanel1.Controls.Add(this.kryptonButton1);
            this.kryptonPanel1.Controls.Add(this.kryptonPropertyGrid1);
            this.kryptonPanel1.Controls.Add(this.kryptonThemeComboBox1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(800, 474);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonProgressBar1
            // 
            this.kryptonProgressBar1.Location = new System.Drawing.Point(364, 45);
            this.kryptonProgressBar1.Name = "kryptonProgressBar1";
            this.kryptonProgressBar1.Size = new System.Drawing.Size(356, 26);
            this.kryptonProgressBar1.StateCommon.Back.Color1 = System.Drawing.Color.Green;
            this.kryptonProgressBar1.StateDisabled.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kryptonProgressBar1.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.OneNote;
            this.kryptonProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.kryptonProgressBar1.TabIndex = 3;
            this.kryptonProgressBar1.Value = 50;
            this.kryptonProgressBar1.Values.Text = "";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(364, 13);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(90, 25);
            this.kryptonButton1.TabIndex = 2;
            this.kryptonButton1.Values.Text = "kryptonButton1";
            // 
            // kryptonPropertyGrid1
            // 
            this.kryptonPropertyGrid1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.kryptonPropertyGrid1.CategoryForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.kryptonPropertyGrid1.CommandsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.kryptonPropertyGrid1.CommandsForeColor = System.Drawing.Color.Black;
            this.kryptonPropertyGrid1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonPropertyGrid1.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.kryptonPropertyGrid1.HelpForeColor = System.Drawing.Color.Black;
            this.kryptonPropertyGrid1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(196)))), ((int)(((byte)(216)))));
            this.kryptonPropertyGrid1.Location = new System.Drawing.Point(13, 41);
            this.kryptonPropertyGrid1.Name = "kryptonPropertyGrid1";
            this.kryptonPropertyGrid1.Size = new System.Drawing.Size(344, 397);
            this.kryptonPropertyGrid1.TabIndex = 1;
            this.kryptonPropertyGrid1.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.kryptonPropertyGrid1.ViewForeColor = System.Drawing.Color.Black;
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.DisplayMember = "Key";
            this.kryptonThemeComboBox1.DropDownWidth = 344;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(13, 13);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(344, 21);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 0;
            this.kryptonThemeComboBox1.ValueMember = "Value";
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.CustomPalette = this.kryptonCustomPaletteBase1;
            // 
            // kryptonCustomPaletteBase1
            // 
            this.kryptonCustomPaletteBase1.BaseFont = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonCustomPaletteBase1.BaseFontSize = 9F;
            this.kryptonCustomPaletteBase1.BasePaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonCustomPaletteBase1.BasePaletteType = Krypton.Toolkit.BasePaletteType.Custom;
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonAlternate.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonAlternate.OverrideDefault.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonAlternate.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(88)))), ((int)(((byte)(110)))));
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonAlternate.OverrideFocus.Border.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonAlternate.OverrideFocus.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonAlternate.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonAlternate.StateCommon.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonAlternate.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(94)))), ((int)(((byte)(94)))));
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonAlternate.StateCommon.Border.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonAlternate.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonAlternate.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonAlternate.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonAlternate.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonAlternate.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonAlternate.StatePressed.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonAlternate.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(69)))), ((int)(((byte)(69)))));
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonCommon.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonCommon.OverrideDefault.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonCommon.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(88)))), ((int)(((byte)(110)))));
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonCommon.OverrideFocus.Border.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonCommon.OverrideFocus.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonCommon.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonCommon.StateCommon.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonCommon.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(94)))), ((int)(((byte)(94)))));
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonCommon.StateCommon.Border.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonCommon.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonCommon.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonCommon.StateCommon.Content.ShortText.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonCommon.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonCommon.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonCommon.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonCommon.StatePressed.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonCommon.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.kryptonCustomPaletteBase1.ButtonStyles.ButtonCommon.StateTracking.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.Common.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.Common.StateCommon.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.Common.StateDisabled.Back.Color1 = System.Drawing.Color.Silver;
            this.kryptonCustomPaletteBase1.Common.StateDisabled.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.ContextMenu.StateCommon.Heading.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ContextMenu.StateCommon.Heading.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.ContextMenu.StateCommon.Heading.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(94)))), ((int)(((byte)(94)))));
            this.kryptonCustomPaletteBase1.ContextMenu.StateCommon.Heading.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonCustomPaletteBase1.ContextMenu.StateCommon.Heading.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.kryptonCustomPaletteBase1.ContextMenu.StateCommon.ItemImageColumn.Back.Draw = Krypton.Toolkit.InheritBool.False;
            this.kryptonCustomPaletteBase1.ContextMenu.StateCommon.ItemImageColumn.Border.Draw = Krypton.Toolkit.InheritBool.False;
            this.kryptonCustomPaletteBase1.ContextMenu.StateCommon.ItemImageColumn.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonCustomPaletteBase1.ContextMenu.StateCommon.Separator.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ContextMenu.StateCommon.Separator.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.ContextMenu.StateCommon.Separator.Border.Draw = Krypton.Toolkit.InheritBool.False;
            this.kryptonCustomPaletteBase1.ContextMenu.StateCommon.Separator.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonCustomPaletteBase1.ContextMenu.StateHighlight.ItemHighlight.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(88)))), ((int)(((byte)(110)))));
            this.kryptonCustomPaletteBase1.ContextMenu.StateHighlight.ItemHighlight.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.ContextMenu.StateHighlight.ItemHighlight.Border.Draw = Krypton.Toolkit.InheritBool.False;
            this.kryptonCustomPaletteBase1.ContextMenu.StateHighlight.ItemHighlight.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonCustomPaletteBase1.ContextMenu.StateHighlight.ItemSplit.Back.Draw = Krypton.Toolkit.InheritBool.False;
            this.kryptonCustomPaletteBase1.ContextMenu.StateHighlight.ItemSplit.Border.Draw = Krypton.Toolkit.InheritBool.False;
            this.kryptonCustomPaletteBase1.ContextMenu.StateHighlight.ItemSplit.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonCustomPaletteBase1.ControlStyles.ControlClient.StateCommon.Back.Color1 = System.Drawing.Color.DimGray;
            this.kryptonCustomPaletteBase1.ControlStyles.ControlClient.StateCommon.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.ControlStyles.ControlCommon.StateCommon.Back.Color1 = System.Drawing.Color.DimGray;
            this.kryptonCustomPaletteBase1.ControlStyles.ControlCommon.StateCommon.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.HeaderStyles.HeaderForm.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(94)))), ((int)(((byte)(94)))));
            this.kryptonCustomPaletteBase1.HeaderStyles.HeaderForm.StateCommon.Border.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.HeaderStyles.HeaderForm.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonCustomPaletteBase1.HeaderStyles.HeaderForm.StateCommon.Border.Width = 1;
            this.kryptonCustomPaletteBase1.HeaderStyles.HeaderPrimary.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(88)))), ((int)(((byte)(110)))));
            this.kryptonCustomPaletteBase1.HeaderStyles.HeaderPrimary.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(94)))), ((int)(((byte)(94)))));
            this.kryptonCustomPaletteBase1.HeaderStyles.HeaderPrimary.StateCommon.Border.Draw = Krypton.Toolkit.InheritBool.True;
            this.kryptonCustomPaletteBase1.HeaderStyles.HeaderPrimary.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)(((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonCustomPaletteBase1.HeaderStyles.HeaderPrimary.StateCommon.Border.Width = 1;
            this.kryptonCustomPaletteBase1.HeaderStyles.HeaderPrimary.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.kryptonCustomPaletteBase1.HeaderStyles.HeaderPrimary.StateCommon.Content.ShortText.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.HeaderStyles.HeaderPrimary.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.kryptonCustomPaletteBase1.InputControlStyles.InputControlCommon.StateActive.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(88)))), ((int)(((byte)(110)))));
            this.kryptonCustomPaletteBase1.InputControlStyles.InputControlCommon.StateActive.Border.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.InputControlStyles.InputControlCommon.StateActive.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonCustomPaletteBase1.InputControlStyles.InputControlCommon.StateActive.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.kryptonCustomPaletteBase1.InputControlStyles.InputControlCommon.StateActive.Content.ShortText.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.InputControlStyles.InputControlCommon.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.InputControlStyles.InputControlCommon.StateCommon.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.InputControlStyles.InputControlCommon.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.kryptonCustomPaletteBase1.InputControlStyles.InputControlCommon.StateCommon.Content.ShortText.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.InputControlStyles.InputControlCustom1.StateCommon.Border.Draw = Krypton.Toolkit.InheritBool.False;
            this.kryptonCustomPaletteBase1.InputControlStyles.InputControlCustom1.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonCustomPaletteBase1.InputControlStyles.InputControlStandalone.StateActive.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.InputControlStyles.InputControlStandalone.StateActive.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.InputControlStyles.InputControlStandalone.StateActive.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.kryptonCustomPaletteBase1.InputControlStyles.InputControlStandalone.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.InputControlStyles.InputControlStandalone.StateCommon.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.InputControlStyles.InputControlStandalone.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.kryptonCustomPaletteBase1.LabelStyles.LabelCaptionPanel.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.kryptonCustomPaletteBase1.LabelStyles.LabelCaptionPanel.StateCommon.ShortText.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.LabelStyles.LabelCommon.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.kryptonCustomPaletteBase1.LabelStyles.LabelCommon.StateCommon.ShortText.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.LabelStyles.LabelKeyTip.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.kryptonCustomPaletteBase1.LabelStyles.LabelKeyTip.StateCommon.ShortText.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.LabelStyles.LabelNormalControl.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.kryptonCustomPaletteBase1.LabelStyles.LabelNormalControl.StateCommon.ShortText.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.LabelStyles.LabelNormalPanel.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.kryptonCustomPaletteBase1.LabelStyles.LabelNormalPanel.StateCommon.ShortText.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.LabelStyles.LabelSuperTip.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.kryptonCustomPaletteBase1.LabelStyles.LabelSuperTip.StateCommon.ShortText.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.LabelStyles.LabelSuperTip.StateNormal.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.kryptonCustomPaletteBase1.LabelStyles.LabelSuperTip.StateNormal.ShortText.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.LabelStyles.LabelTitleControl.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.kryptonCustomPaletteBase1.LabelStyles.LabelTitleControl.StateCommon.ShortText.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.LabelStyles.LabelTitlePanel.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.kryptonCustomPaletteBase1.LabelStyles.LabelTitlePanel.StateCommon.ShortText.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.LabelStyles.LabelToolTip.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.kryptonCustomPaletteBase1.LabelStyles.LabelToolTip.StateCommon.ShortText.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.PanelStyles.PanelAlternate.StateCommon.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.PanelStyles.PanelAlternate.StateCommon.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.PanelStyles.PanelClient.StateCommon.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.PanelStyles.PanelClient.StateCommon.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.PanelStyles.PanelCommon.StateCommon.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.PanelStyles.PanelCommon.StateCommon.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.PanelStyles.PanelCustom1.StateCommon.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.PanelStyles.PanelCustom1.StateCommon.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.OverrideFocus.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.OverrideFocus.Border.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.OverrideFocus.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StateCommon.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StateCommon.Border.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StateCommon.Content.ShortText.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StatePressed.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StatePressed.Border.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StatePressed.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StateSelected.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(88)))), ((int)(((byte)(110)))));
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StateSelected.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StateSelected.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StateSelected.Border.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StateSelected.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StateTracking.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StateTracking.Border.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StateTracking.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonCustomPaletteBase1.TabStyles.TabCommon.StateTracking.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.kryptonCustomPaletteBase1.ThemeName = "";
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.ButtonCheckedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.ButtonCheckedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.ButtonCheckedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.ButtonCheckedHighlight = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.ButtonCheckedHighlightBorder = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.ButtonPressedBorder = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.ButtonPressedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.ButtonPressedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.ButtonPressedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.ButtonPressedHighlight = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.ButtonPressedHighlightBorder = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.ButtonSelectedBorder = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(88)))), ((int)(((byte)(110)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.ButtonSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(88)))), ((int)(((byte)(110)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.ButtonSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(88)))), ((int)(((byte)(110)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.ButtonSelectedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(88)))), ((int)(((byte)(110)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.ButtonSelectedHighlight = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(88)))), ((int)(((byte)(110)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.ButtonSelectedHighlightBorder = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(88)))), ((int)(((byte)(110)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.CheckBackground = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.CheckPressedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.CheckSelectedBackground = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(88)))), ((int)(((byte)(110)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.OverflowButtonGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.OverflowButtonGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Button.OverflowButtonGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Grip.GripDark = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Grip.GripLight = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Menu.ImageMarginGradientBegin = System.Drawing.Color.Transparent;
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Menu.ImageMarginGradientEnd = System.Drawing.Color.Transparent;
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Menu.ImageMarginGradientMiddle = System.Drawing.Color.Transparent;
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Menu.ImageMarginRevealedGradientBegin = System.Drawing.Color.Transparent;
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Menu.ImageMarginRevealedGradientEnd = System.Drawing.Color.Transparent;
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Menu.ImageMarginRevealedGradientMiddle = System.Drawing.Color.Transparent;
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Menu.MenuBorder = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Menu.MenuItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Menu.MenuItemPressedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Menu.MenuItemPressedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Menu.MenuItemPressedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Menu.MenuItemSelected = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(88)))), ((int)(((byte)(110)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Menu.MenuItemSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(88)))), ((int)(((byte)(110)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Menu.MenuItemSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(88)))), ((int)(((byte)(110)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Menu.MenuItemText = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.MenuStrip.MenuStripGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.MenuStrip.MenuStripGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.MenuStrip.MenuStripText = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Rafting.RaftingContainerGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Rafting.RaftingContainerGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Separator.SeparatorDark = System.Drawing.Color.Transparent;
            this.kryptonCustomPaletteBase1.ToolMenuStatus.Separator.SeparatorLight = System.Drawing.Color.Transparent;
            this.kryptonCustomPaletteBase1.ToolMenuStatus.StatusStrip.StatusStripGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.StatusStrip.StatusStripGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.StatusStrip.StatusStripText = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(88)))), ((int)(((byte)(110)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.ToolStrip.ToolStripBorder = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.ToolStrip.ToolStripContentPanelGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.ToolStrip.ToolStripContentPanelGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.ToolStrip.ToolStripDropDownBackground = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.ToolStrip.ToolStripFont = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonCustomPaletteBase1.ToolMenuStatus.ToolStrip.ToolStripGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.ToolStrip.ToolStripGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.ToolStrip.ToolStripGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.ToolStrip.ToolStripPanelGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.ToolStrip.ToolStripPanelGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.ToolStrip.ToolStripText = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.kryptonCustomPaletteBase1.ToolMenuStatus.UseRoundedEdges = Krypton.Toolkit.InheritBool.False;
            this.kryptonCustomPaletteBase1.UseKryptonFileDialogs = true;
            // 
            // buttonSpecAny1
            // 
            this.buttonSpecAny1.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Previous;
            this.buttonSpecAny1.UniqueName = "4cb7c96cbdba4cfebc468a3149d6cc4c";
            // 
            // buttonSpecAny2
            // 
            this.buttonSpecAny2.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Next;
            this.buttonSpecAny2.UniqueName = "229ede385c5b4d98ab13f3464707d87d";
            // 
            // buttonSpecAny3
            // 
            this.buttonSpecAny3.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Context;
            this.buttonSpecAny3.UniqueName = "0943ca091e624724a0d729320faa85a5";
            // 
            // buttonSpecAny4
            // 
            this.buttonSpecAny4.Enabled = Krypton.Toolkit.ButtonEnabled.True;
            this.buttonSpecAny4.KryptonCommand = this.kryptonCommand1;
            this.buttonSpecAny4.UniqueName = "abced5c74f5f4da6a610c39b43791594";
            // 
            // kryptonCommand1
            // 
            this.kryptonCommand1.AssignedButtonSpec = this.buttonSpecAny4;
            this.kryptonCommand1.CommandType = Krypton.Toolkit.KryptonCommandType.IntegratedToolBarNewCommand;
            this.kryptonCommand1.ImageSmall = ((System.Drawing.Image)(resources.GetObject("kryptonCommand1.ImageSmall")));
            this.kryptonCommand1.Text = "kryptonCommand1";
            // 
            // kryptonStatusStrip1
            // 
            this.kryptonStatusStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.kryptonStatusStrip1.Location = new System.Drawing.Point(0, 452);
            this.kryptonStatusStrip1.Name = "kryptonStatusStrip1";
            this.kryptonStatusStrip1.ProgressBars = null;
            this.kryptonStatusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.kryptonStatusStrip1.Size = new System.Drawing.Size(800, 22);
            this.kryptonStatusStrip1.TabIndex = 1;
            this.kryptonStatusStrip1.Text = "kryptonStatusStrip1";
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonSpecs.Add(this.buttonSpecAny1);
            this.ButtonSpecs.Add(this.buttonSpecAny2);
            this.ButtonSpecs.Add(this.buttonSpecAny3);
            this.ButtonSpecs.Add(this.buttonSpecAny4);
            this.ClientSize = new System.Drawing.Size(800, 474);
            this.Controls.Add(this.kryptonStatusStrip1);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "Form5";
            this.Text = "Form5";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Toolkit.KryptonPropertyGrid kryptonPropertyGrid1;
        private Krypton.Toolkit.KryptonManager kryptonManager1;
        private Krypton.Toolkit.ButtonSpecAny buttonSpecAny1;
        private Krypton.Toolkit.ButtonSpecAny buttonSpecAny2;
        private Krypton.Toolkit.ButtonSpecAny buttonSpecAny3;
        private Krypton.Toolkit.ButtonSpecAny buttonSpecAny4;
        private Krypton.Toolkit.KryptonCommand kryptonCommand1;
        private Krypton.Toolkit.KryptonButton kryptonButton1;
        private Krypton.Toolkit.KryptonProgressBar kryptonProgressBar1;
        private Krypton.Toolkit.KryptonStatusStrip kryptonStatusStrip1;
        private Krypton.Toolkit.KryptonCustomPaletteBase kryptonCustomPaletteBase1;
    }
}