#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

using Krypton.Toolkit;
using Krypton.Navigator;
using Krypton.Workspace;
using Krypton.Ribbon;

namespace ExtensibilityDesignerTest;

/// <summary>
/// Test form for validating WinForms Designer Extensibility SDK implementation.
/// This form contains Krypton controls with the new extensibility designers.
/// </summary>
public partial class ExtensibilityDesignerTestForm : KryptonForm
{
    /// <summary>
    /// Initializes a new instance of the ExtensibilityDesignerTestForm class.
    /// </summary>
    public ExtensibilityDesignerTestForm()
    {
        InitializeComponent();
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonTextBox1 = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonCheckBox1 = new Krypton.Toolkit.KryptonCheckBox();
            this.kryptonRadioButton1 = new Krypton.Toolkit.KryptonRadioButton();
            this.kryptonComboBox1 = new Krypton.Toolkit.KryptonComboBox();
            this.kryptonListBox1 = new Krypton.Toolkit.KryptonListBox();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonGroupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonMaskedTextBox1 = new Krypton.Toolkit.KryptonMaskedTextBox();
            this.kryptonRichTextBox1 = new Krypton.Toolkit.KryptonRichTextBox();
            this.kryptonNumericUpDown1 = new Krypton.Toolkit.KryptonNumericUpDown();
            this.kryptonNavigator1 = new Krypton.Navigator.KryptonNavigator();
            this.kryptonWorkspace1 = new Krypton.Workspace.KryptonWorkspace();
            this.kryptonRibbon1 = new Krypton.Ribbon.KryptonRibbon();
            this.kryptonGallery1 = new Krypton.Ribbon.KryptonGallery();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspace1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonRibbon1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(10, 116);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(103, 26);
            this.kryptonButton1.TabIndex = 0;
            this.kryptonButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton1.Values.Text = "Test Button";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(10, 52);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(65, 20);
            this.kryptonLabel1.TabIndex = 1;
            this.kryptonLabel1.Values.Text = "Test Label";
            // 
            // kryptonTextBox1
            // 
            this.kryptonTextBox1.Location = new System.Drawing.Point(10, 87);
            this.kryptonTextBox1.Name = "kryptonTextBox1";
            this.kryptonTextBox1.Size = new System.Drawing.Size(171, 23);
            this.kryptonTextBox1.TabIndex = 2;
            this.kryptonTextBox1.Text = "Test TextBox";
            // 
            // kryptonCheckBox1
            // 
            this.kryptonCheckBox1.Location = new System.Drawing.Point(10, 121);
            this.kryptonCheckBox1.Name = "kryptonCheckBox1";
            this.kryptonCheckBox1.Size = new System.Drawing.Size(102, 20);
            this.kryptonCheckBox1.TabIndex = 3;
            this.kryptonCheckBox1.Values.Text = "Test CheckBox";
            // 
            // kryptonRadioButton1
            // 
            this.kryptonRadioButton1.Location = new System.Drawing.Point(10, 156);
            this.kryptonRadioButton1.Name = "kryptonRadioButton1";
            this.kryptonRadioButton1.Size = new System.Drawing.Size(116, 20);
            this.kryptonRadioButton1.TabIndex = 4;
            this.kryptonRadioButton1.Values.Text = "Test RadioButton";
            // 
            // kryptonComboBox1
            // 
            this.kryptonComboBox1.DropDownWidth = 171;
            this.kryptonComboBox1.Location = new System.Drawing.Point(10, 191);
            this.kryptonComboBox1.Name = "kryptonComboBox1";
            this.kryptonComboBox1.Size = new System.Drawing.Size(171, 22);
            this.kryptonComboBox1.TabIndex = 5;
            this.kryptonComboBox1.Text = "Test ComboBox";
            // 
            // kryptonListBox1
            // 
            this.kryptonListBox1.Location = new System.Drawing.Point(10, 225);
            this.kryptonListBox1.Name = "kryptonListBox1";
            this.kryptonListBox1.Size = new System.Drawing.Size(171, 87);
            this.kryptonListBox1.TabIndex = 6;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Location = new System.Drawing.Point(197, 10);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(171, 87);
            this.kryptonPanel1.TabIndex = 7;
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Location = new System.Drawing.Point(197, 113);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(171, 87);
            this.kryptonGroupBox1.TabIndex = 8;
            this.kryptonGroupBox1.Values.Heading = "Test GroupBox";
            // 
            // kryptonMaskedTextBox1
            // 
            this.kryptonMaskedTextBox1.Location = new System.Drawing.Point(10, 329);
            this.kryptonMaskedTextBox1.Name = "kryptonMaskedTextBox1";
            this.kryptonMaskedTextBox1.Size = new System.Drawing.Size(171, 23);
            this.kryptonMaskedTextBox1.TabIndex = 9;
            this.kryptonMaskedTextBox1.Text = "Test MaskedTextBox";
            // 
            // kryptonRichTextBox1
            // 
            this.kryptonRichTextBox1.Location = new System.Drawing.Point(10, 364);
            this.kryptonRichTextBox1.Name = "kryptonRichTextBox1";
            this.kryptonRichTextBox1.Size = new System.Drawing.Size(171, 87);
            this.kryptonRichTextBox1.TabIndex = 10;
            this.kryptonRichTextBox1.Text = "Test RichTextBox";
            // 
            // kryptonNumericUpDown1
            // 
            this.kryptonNumericUpDown1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.kryptonNumericUpDown1.Location = new System.Drawing.Point(10, 468);
            this.kryptonNumericUpDown1.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.kryptonNumericUpDown1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.kryptonNumericUpDown1.Name = "kryptonNumericUpDown1";
            this.kryptonNumericUpDown1.Size = new System.Drawing.Size(171, 22);
            this.kryptonNumericUpDown1.TabIndex = 11;
            this.kryptonNumericUpDown1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // kryptonNavigator1
            // 
            this.kryptonNavigator1.ControlKryptonFormFeatures = false;
            this.kryptonNavigator1.Location = new System.Drawing.Point(214, 10);
            this.kryptonNavigator1.NavigatorMode = Krypton.Navigator.NavigatorMode.BarTabGroup;
            this.kryptonNavigator1.Owner = null;
            this.kryptonNavigator1.PageBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kryptonNavigator1.Size = new System.Drawing.Size(200, 200);
            this.kryptonNavigator1.TabIndex = 13;
            // 
            // kryptonWorkspace1
            // 
            this.kryptonWorkspace1.ActivePage = null;
            this.kryptonWorkspace1.CompactFlags = ((Krypton.Workspace.CompactFlags)((((Krypton.Workspace.CompactFlags.RemoveEmptyCells | Krypton.Workspace.CompactFlags.RemoveEmptySequences) 
            | Krypton.Workspace.CompactFlags.PromoteLeafs) 
            | Krypton.Workspace.CompactFlags.AtLeastOneVisibleCell)));
            this.kryptonWorkspace1.ContainerBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kryptonWorkspace1.Location = new System.Drawing.Point(214, 217);
            this.kryptonWorkspace1.Name = "kryptonWorkspace1";
            // 
            // 
            // 
            this.kryptonWorkspace1.Root.UniqueName = "3d475759ebda4612977efd1ffbfaa9dc";
            this.kryptonWorkspace1.SeparatorStyle = Krypton.Toolkit.SeparatorStyle.LowProfile;
            this.kryptonWorkspace1.Size = new System.Drawing.Size(171, 173);
            this.kryptonWorkspace1.SplitterWidth = 5;
            this.kryptonWorkspace1.TabIndex = 14;
            this.kryptonWorkspace1.TabStop = true;
            // 
            // kryptonRibbon1
            // 
            this.kryptonRibbon1.Name = "kryptonRibbon1";
            this.kryptonRibbon1.SelectedTab = null;
            this.kryptonRibbon1.Size = new System.Drawing.Size(393, 114);
            this.kryptonRibbon1.TabIndex = 15;
            // 
            // kryptonGallery1
            // 
            this.kryptonGallery1.Location = new System.Drawing.Point(386, 10);
            this.kryptonGallery1.Name = "kryptonGallery1";
            this.kryptonGallery1.Size = new System.Drawing.Size(171, 173);
            this.kryptonGallery1.TabIndex = 16;
            // 
            // ExtensibilityDesignerTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 480);
            this.CloseBox = false;
            this.Controls.Add(this.kryptonGallery1);
            this.Controls.Add(this.kryptonRibbon1);
            this.Controls.Add(this.kryptonWorkspace1);
            this.Controls.Add(this.kryptonNavigator1);
            this.Controls.Add(this.kryptonNumericUpDown1);
            this.Controls.Add(this.kryptonRichTextBox1);
            this.Controls.Add(this.kryptonMaskedTextBox1);
            this.Controls.Add(this.kryptonGroupBox1);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.kryptonListBox1);
            this.Controls.Add(this.kryptonComboBox1);
            this.Controls.Add(this.kryptonRadioButton1);
            this.Controls.Add(this.kryptonCheckBox1);
            this.Controls.Add(this.kryptonTextBox1);
            this.Controls.Add(this.kryptonLabel1);
            this.Controls.Add(this.kryptonButton1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "ExtensibilityDesignerTestForm";
            this.Text = "Extensibility Designer Test";
            this.Controls.SetChildIndex(this.kryptonButton1, 0);
            this.Controls.SetChildIndex(this.kryptonLabel1, 0);
            this.Controls.SetChildIndex(this.kryptonTextBox1, 0);
            this.Controls.SetChildIndex(this.kryptonCheckBox1, 0);
            this.Controls.SetChildIndex(this.kryptonRadioButton1, 0);
            this.Controls.SetChildIndex(this.kryptonComboBox1, 0);
            this.Controls.SetChildIndex(this.kryptonListBox1, 0);
            this.Controls.SetChildIndex(this.kryptonPanel1, 0);
            this.Controls.SetChildIndex(this.kryptonGroupBox1, 0);
            this.Controls.SetChildIndex(this.kryptonMaskedTextBox1, 0);
            this.Controls.SetChildIndex(this.kryptonRichTextBox1, 0);
            this.Controls.SetChildIndex(this.kryptonNumericUpDown1, 0);
            this.Controls.SetChildIndex(this.kryptonNavigator1, 0);
            this.Controls.SetChildIndex(this.kryptonWorkspace1, 0);
            this.Controls.SetChildIndex(this.kryptonRibbon1, 0);
            this.Controls.SetChildIndex(this.kryptonGallery1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonWorkspace1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonRibbon1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    #region Private Fields
    private KryptonButton kryptonButton1;
    private KryptonLabel kryptonLabel1;
    private KryptonTextBox kryptonTextBox1;
    private KryptonCheckBox kryptonCheckBox1;
    private KryptonRadioButton kryptonRadioButton1;
    private KryptonComboBox kryptonComboBox1;
    private KryptonListBox kryptonListBox1;
    private KryptonPanel kryptonPanel1;
    private KryptonGroupBox kryptonGroupBox1;
    private KryptonMaskedTextBox kryptonMaskedTextBox1;
    private KryptonRichTextBox kryptonRichTextBox1;
    private KryptonNumericUpDown kryptonNumericUpDown1;
    private KryptonNavigator kryptonNavigator1;
    private KryptonWorkspace kryptonWorkspace1;
    private KryptonRibbon kryptonRibbon1;
    private KryptonGallery kryptonGallery1;
    #endregion
}
