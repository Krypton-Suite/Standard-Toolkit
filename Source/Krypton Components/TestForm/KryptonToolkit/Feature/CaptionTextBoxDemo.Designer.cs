#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

partial class CaptionTextBoxDemo
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

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this._manager = new Krypton.Toolkit.KryptonManager(this.components);
        this._ribbon = new Krypton.Ribbon.KryptonRibbon();
        this._instructions = new Krypton.Toolkit.KryptonTextBox();
        this._editorChoice = new Krypton.Toolkit.KryptonComboBox();
        this._edgeChoice = new Krypton.Toolkit.KryptonComboBox();
        this._themeChoice = new Krypton.Toolkit.KryptonComboBox();
        this._log = new Krypton.Toolkit.KryptonTextBox();
        this._options = new Krypton.Toolkit.KryptonPanel();
        this._editorLabel = new Krypton.Toolkit.KryptonLabel();
        this._edgeLabel = new Krypton.Toolkit.KryptonLabel();
        this._themeLabel = new Krypton.Toolkit.KryptonLabel();
        ((System.ComponentModel.ISupportInitialize)this._editorChoice).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this._edgeChoice).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this._themeChoice).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this._options).BeginInit();
        this._options.SuspendLayout();
        this.SuspendLayout();
        //
        // _ribbon
        //
        this._ribbon.Dock = System.Windows.Forms.DockStyle.Top;
        this._ribbon.Name = "_ribbon";
        this._ribbon.Size = new System.Drawing.Size(960, 115);
        this._ribbon.TabIndex = 0;
        //
        // _instructions
        //
        this._instructions.Dock = System.Windows.Forms.DockStyle.Top;
        this._instructions.Location = new System.Drawing.Point(0, 0);
        this._instructions.Multiline = true;
        this._instructions.Name = "_instructions";
        this._instructions.ReadOnly = true;
        this._instructions.Size = new System.Drawing.Size(960, 88);
        this._instructions.TabIndex = 1;
        this._instructions.Text = "Search, text box, button, and combo are in the caption together. Each has its own edge. Search moves the text box with it. Drag the caption outside the controls, resize, and maximize. Type in the search box to open suggestions.";
        //
        // _options
        //
        this._options.Controls.Add(this._themeChoice);
        this._options.Controls.Add(this._themeLabel);
        this._options.Controls.Add(this._edgeChoice);
        this._options.Controls.Add(this._edgeLabel);
        this._options.Controls.Add(this._editorChoice);
        this._options.Controls.Add(this._editorLabel);
        this._options.Dock = System.Windows.Forms.DockStyle.Top;
        this._options.Location = new System.Drawing.Point(0, 88);
        this._options.Name = "_options";
        this._options.Size = new System.Drawing.Size(960, 44);
        this._options.TabIndex = 2;
        //
        // _editorLabel
        //
        this._editorLabel.Location = new System.Drawing.Point(12, 10);
        this._editorLabel.Name = "_editorLabel";
        this._editorLabel.Size = new System.Drawing.Size(49, 20);
        this._editorLabel.TabIndex = 0;
        this._editorLabel.Values.Text = "Editor";
        //
        // _editorChoice
        //
        this._editorChoice.DropDownWidth = 160;
        this._editorChoice.Location = new System.Drawing.Point(68, 8);
        this._editorChoice.Name = "_editorChoice";
        this._editorChoice.Size = new System.Drawing.Size(160, 22);
        this._editorChoice.TabIndex = 1;
        this._editorChoice.SelectedIndexChanged += new System.EventHandler(this.OnEditorChoiceChanged);
        //
        // _edgeLabel
        //
        this._edgeLabel.Location = new System.Drawing.Point(248, 10);
        this._edgeLabel.Name = "_edgeLabel";
        this._edgeLabel.Size = new System.Drawing.Size(36, 20);
        this._edgeLabel.TabIndex = 2;
        this._edgeLabel.Values.Text = "Edge";
        //
        // _edgeChoice
        //
        this._edgeChoice.DropDownWidth = 120;
        this._edgeChoice.Location = new System.Drawing.Point(292, 8);
        this._edgeChoice.Name = "_edgeChoice";
        this._edgeChoice.Size = new System.Drawing.Size(120, 22);
        this._edgeChoice.TabIndex = 3;
        this._edgeChoice.SelectedIndexChanged += new System.EventHandler(this.OnEdgeChoiceChanged);
        //
        // _themeLabel
        //
        this._themeLabel.Location = new System.Drawing.Point(432, 10);
        this._themeLabel.Name = "_themeLabel";
        this._themeLabel.Size = new System.Drawing.Size(46, 20);
        this._themeLabel.TabIndex = 4;
        this._themeLabel.Values.Text = "Theme";
        //
        // _themeChoice
        //
        this._themeChoice.DropDownWidth = 220;
        this._themeChoice.Location = new System.Drawing.Point(484, 8);
        this._themeChoice.Name = "_themeChoice";
        this._themeChoice.Size = new System.Drawing.Size(220, 22);
        this._themeChoice.TabIndex = 5;
        this._themeChoice.SelectedIndexChanged += new System.EventHandler(this.OnThemeChoiceChanged);
        //
        // _log
        //
        this._log.Dock = System.Windows.Forms.DockStyle.Fill;
        this._log.Location = new System.Drawing.Point(0, 132);
        this._log.Multiline = true;
        this._log.Name = "_log";
        this._log.ReadOnly = true;
        this._log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this._log.Size = new System.Drawing.Size(960, 360);
        this._log.TabIndex = 3;
        //
        // CaptionTextBoxDemo
        //
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(960, 560);
        this.Controls.Add(this._log);
        this.Controls.Add(this._options);
        this.Controls.Add(this._instructions);
        this.Controls.Add(this._ribbon);
        this.Name = "CaptionTextBoxDemo";
        this.Text = "Caption text box (#4550)";
        ((System.ComponentModel.ISupportInitialize)this._editorChoice).EndInit();
        ((System.ComponentModel.ISupportInitialize)this._edgeChoice).EndInit();
        ((System.ComponentModel.ISupportInitialize)this._themeChoice).EndInit();
        ((System.ComponentModel.ISupportInitialize)this._options).EndInit();
        this._options.ResumeLayout(false);
        this._options.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private Krypton.Toolkit.KryptonManager _manager;
    private Krypton.Ribbon.KryptonRibbon _ribbon;
    private Krypton.Toolkit.KryptonTextBox _instructions;
    private Krypton.Toolkit.KryptonPanel _options;
    private Krypton.Toolkit.KryptonLabel _editorLabel;
    private Krypton.Toolkit.KryptonComboBox _editorChoice;
    private Krypton.Toolkit.KryptonLabel _edgeLabel;
    private Krypton.Toolkit.KryptonComboBox _edgeChoice;
    private Krypton.Toolkit.KryptonLabel _themeLabel;
    private Krypton.Toolkit.KryptonComboBox _themeChoice;
    private Krypton.Toolkit.KryptonTextBox _log;
}
