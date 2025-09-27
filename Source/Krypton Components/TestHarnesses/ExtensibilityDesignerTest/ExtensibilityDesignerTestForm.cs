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
        kryptonButton1 = new KryptonButton();
        kryptonLabel1 = new KryptonLabel();
        kryptonTextBox1 = new KryptonTextBox();
        kryptonCheckBox1 = new KryptonCheckBox();
        kryptonRadioButton1 = new KryptonRadioButton();
        kryptonComboBox1 = new KryptonComboBox();
        kryptonListBox1 = new KryptonListBox();
        kryptonPanel1 = new KryptonPanel();
        kryptonGroupBox1 = new KryptonGroupBox();
        kryptonMaskedTextBox1 = new KryptonMaskedTextBox();
        kryptonRichTextBox1 = new KryptonRichTextBox();
        kryptonNumericUpDown1 = new KryptonNumericUpDown();
        kryptonNavigator1 = new KryptonNavigator();
        kryptonWorkspace1 = new KryptonWorkspace();
        kryptonRibbon1 = new KryptonRibbon();
        kryptonGallery1 = new KryptonGallery();
        ((System.ComponentModel.ISupportInitialize)kryptonComboBox1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)kryptonPanel1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)kryptonGroupBox1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)kryptonGroupBox1.Panel).BeginInit();
        ((System.ComponentModel.ISupportInitialize)kryptonNavigator1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)kryptonWorkspace1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)kryptonRibbon1).BeginInit();
        SuspendLayout();
        // 
        // kryptonButton1
        // 
        kryptonButton1.Location = new Point(12, 134);
        kryptonButton1.Name = "kryptonButton1";
        kryptonButton1.Size = new Size(120, 30);
        kryptonButton1.TabIndex = 0;
        kryptonButton1.Values.DropDownArrowColor = Color.Empty;
        kryptonButton1.Values.Text = "Test Button";
        // 
        // kryptonLabel1
        // 
        kryptonLabel1.Location = new Point(12, 60);
        kryptonLabel1.Name = "kryptonLabel1";
        kryptonLabel1.Size = new Size(65, 20);
        kryptonLabel1.TabIndex = 1;
        kryptonLabel1.Values.Text = "Test Label";
        // 
        // kryptonTextBox1
        // 
        kryptonTextBox1.Location = new Point(12, 100);
        kryptonTextBox1.Name = "kryptonTextBox1";
        kryptonTextBox1.Size = new Size(200, 23);
        kryptonTextBox1.TabIndex = 2;
        kryptonTextBox1.Text = "Test TextBox";
        // 
        // kryptonCheckBox1
        // 
        kryptonCheckBox1.Location = new Point(12, 140);
        kryptonCheckBox1.Name = "kryptonCheckBox1";
        kryptonCheckBox1.Size = new Size(102, 20);
        kryptonCheckBox1.TabIndex = 3;
        kryptonCheckBox1.Values.Text = "Test CheckBox";
        // 
        // kryptonRadioButton1
        // 
        kryptonRadioButton1.Location = new Point(12, 180);
        kryptonRadioButton1.Name = "kryptonRadioButton1";
        kryptonRadioButton1.Size = new Size(116, 20);
        kryptonRadioButton1.TabIndex = 4;
        kryptonRadioButton1.Values.Text = "Test RadioButton";
        // 
        // kryptonComboBox1
        // 
        kryptonComboBox1.Location = new Point(12, 220);
        kryptonComboBox1.Name = "kryptonComboBox1";
        kryptonComboBox1.Size = new Size(200, 22);
        kryptonComboBox1.TabIndex = 5;
        kryptonComboBox1.Text = "Test ComboBox";
        // 
        // kryptonListBox1
        // 
        kryptonListBox1.Location = new Point(12, 260);
        kryptonListBox1.Name = "kryptonListBox1";
        kryptonListBox1.Size = new Size(200, 100);
        kryptonListBox1.TabIndex = 6;
        // 
        // kryptonPanel1
        // 
        kryptonPanel1.Location = new Point(230, 12);
        kryptonPanel1.Name = "kryptonPanel1";
        kryptonPanel1.Size = new Size(200, 100);
        kryptonPanel1.TabIndex = 7;
        // 
        // kryptonGroupBox1
        // 
        kryptonGroupBox1.Location = new Point(230, 130);
        kryptonGroupBox1.Size = new Size(200, 100);
        kryptonGroupBox1.TabIndex = 8;
        kryptonGroupBox1.Values.Heading = "Test GroupBox";
        // 
        // kryptonMaskedTextBox1
        // 
        kryptonMaskedTextBox1.Location = new Point(12, 380);
        kryptonMaskedTextBox1.Name = "kryptonMaskedTextBox1";
        kryptonMaskedTextBox1.Size = new Size(200, 23);
        kryptonMaskedTextBox1.TabIndex = 9;
        kryptonMaskedTextBox1.Text = "Test MaskedTextBox";
        // 
        // kryptonRichTextBox1
        // 
        kryptonRichTextBox1.Location = new Point(12, 420);
        kryptonRichTextBox1.Name = "kryptonRichTextBox1";
        kryptonRichTextBox1.Size = new Size(200, 100);
        kryptonRichTextBox1.TabIndex = 10;
        kryptonRichTextBox1.Text = "Test RichTextBox";
        // 
        // kryptonNumericUpDown1
        // 
        kryptonNumericUpDown1.Increment = new decimal(new int[] { 1, 0, 0, 0 });
        kryptonNumericUpDown1.Location = new Point(12, 540);
        kryptonNumericUpDown1.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
        kryptonNumericUpDown1.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
        kryptonNumericUpDown1.Name = "kryptonNumericUpDown1";
        kryptonNumericUpDown1.Size = new Size(200, 22);
        kryptonNumericUpDown1.TabIndex = 11;
        kryptonNumericUpDown1.Value = new decimal(new int[] { 0, 0, 0, 0 });
        // 
        // kryptonNavigator1
        // 
        kryptonNavigator1.ControlKryptonFormFeatures = false;
        kryptonNavigator1.Location = new Point(250, 12);
        kryptonNavigator1.NavigatorMode = NavigatorMode.BarTabGroup;
        kryptonNavigator1.Owner = null;
        kryptonNavigator1.PageBackStyle = PaletteBackStyle.PanelClient;
        kryptonNavigator1.Size = new Size(200, 200);
        kryptonNavigator1.TabIndex = 13;
        // 
        // kryptonWorkspace1
        // 
        kryptonWorkspace1.ActivePage = null;
        kryptonWorkspace1.CompactFlags = CompactFlags.RemoveEmptyCells | CompactFlags.RemoveEmptySequences | CompactFlags.PromoteLeafs | CompactFlags.AtLeastOneVisibleCell;
        kryptonWorkspace1.ContainerBackStyle = PaletteBackStyle.PanelClient;
        kryptonWorkspace1.Location = new Point(250, 250);
        kryptonWorkspace1.Name = "kryptonWorkspace1";
        // 
        // 
        // 
        kryptonWorkspace1.Root.UniqueName = "3d475759ebda4612977efd1ffbfaa9dc";
        kryptonWorkspace1.SeparatorStyle = SeparatorStyle.LowProfile;
        kryptonWorkspace1.Size = new Size(200, 200);
        kryptonWorkspace1.SplitterWidth = 5;
        kryptonWorkspace1.TabIndex = 14;
        kryptonWorkspace1.TabStop = true;
        // 
        // kryptonRibbon1
        // 
        kryptonRibbon1.Name = "kryptonRibbon1";
        kryptonRibbon1.SelectedTab = null;
        kryptonRibbon1.Size = new Size(454, 114);
        kryptonRibbon1.TabIndex = 15;
        // 
        // kryptonGallery1
        // 
        kryptonGallery1.Location = new Point(450, 12);
        kryptonGallery1.Name = "kryptonGallery1";
        kryptonGallery1.Size = new Size(200, 200);
        kryptonGallery1.TabIndex = 16;
        // 
        // ExtensibilityDesignerTestForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(454, 568);
        CloseBox = false;
        Controls.Add(kryptonGallery1);
        Controls.Add(kryptonRibbon1);
        Controls.Add(kryptonWorkspace1);
        Controls.Add(kryptonNavigator1);
        Controls.Add(kryptonNumericUpDown1);
        Controls.Add(kryptonRichTextBox1);
        Controls.Add(kryptonMaskedTextBox1);
        Controls.Add(kryptonGroupBox1);
        Controls.Add(kryptonPanel1);
        Controls.Add(kryptonListBox1);
        Controls.Add(kryptonComboBox1);
        Controls.Add(kryptonRadioButton1);
        Controls.Add(kryptonCheckBox1);
        Controls.Add(kryptonTextBox1);
        Controls.Add(kryptonLabel1);
        Controls.Add(kryptonButton1);
        Location = new Point(0, 0);
        Name = "ExtensibilityDesignerTestForm";
        Text = "Extensibility Designer Test";
        Controls.SetChildIndex(kryptonButton1, 0);
        Controls.SetChildIndex(kryptonLabel1, 0);
        Controls.SetChildIndex(kryptonTextBox1, 0);
        Controls.SetChildIndex(kryptonCheckBox1, 0);
        Controls.SetChildIndex(kryptonRadioButton1, 0);
        Controls.SetChildIndex(kryptonComboBox1, 0);
        Controls.SetChildIndex(kryptonListBox1, 0);
        Controls.SetChildIndex(kryptonPanel1, 0);
        Controls.SetChildIndex(kryptonGroupBox1, 0);
        Controls.SetChildIndex(kryptonMaskedTextBox1, 0);
        Controls.SetChildIndex(kryptonRichTextBox1, 0);
        Controls.SetChildIndex(kryptonNumericUpDown1, 0);
        Controls.SetChildIndex(kryptonNavigator1, 0);
        Controls.SetChildIndex(kryptonWorkspace1, 0);
        Controls.SetChildIndex(kryptonRibbon1, 0);
        Controls.SetChildIndex(kryptonGallery1, 0);
        ((System.ComponentModel.ISupportInitialize)kryptonComboBox1).EndInit();
        ((System.ComponentModel.ISupportInitialize)kryptonPanel1).EndInit();
        ((System.ComponentModel.ISupportInitialize)kryptonGroupBox1.Panel).EndInit();
        ((System.ComponentModel.ISupportInitialize)kryptonGroupBox1).EndInit();
        ((System.ComponentModel.ISupportInitialize)kryptonNavigator1).EndInit();
        ((System.ComponentModel.ISupportInitialize)kryptonWorkspace1).EndInit();
        ((System.ComponentModel.ISupportInitialize)kryptonRibbon1).EndInit();
        ResumeLayout(false);
        PerformLayout();
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
