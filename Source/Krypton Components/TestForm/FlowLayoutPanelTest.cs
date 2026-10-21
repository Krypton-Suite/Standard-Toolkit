#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace TestForm;

public partial class FlowLayoutPanelTest : KryptonForm
{
    private int _controlCounter;
    private KryptonContextMenu? _contextMenu;

    public FlowLayoutPanelTest()
    {
        InitializeComponent();
        InitializeComboBoxes();
        InitializeContextMenu();
        SetupAdditionalPanels();
        UpdateUIState();
    }

    private void InitializeComboBoxes()
    {
        // Populate FlowDirection combo
        kcmbFlowDirection.Items.Clear();
        kcmbFlowDirection.Items.Add("LeftToRight");
        kcmbFlowDirection.Items.Add("TopDown");
        kcmbFlowDirection.Items.Add("RightToLeft");
        kcmbFlowDirection.Items.Add("BottomUp");
        kcmbFlowDirection.SelectedIndex = 0; // "LeftToRight"

        // Populate PanelBackStyle combo
        kcmbPanelBackStyle.Items.Clear();
        kcmbPanelBackStyle.Items.Add("PanelClient");
        kcmbPanelBackStyle.Items.Add("PanelAlternate");
        kcmbPanelBackStyle.Items.Add("PanelPrimary");
        kcmbPanelBackStyle.Items.Add("PanelSecondary");
        kcmbPanelBackStyle.SelectedIndex = 0; // "PanelClient"

        // Populate PaletteMode combo
        kcmbPaletteMode.Items.Clear();
        kcmbPaletteMode.Items.Add("Global");
        kcmbPaletteMode.Items.Add("Professional System");
        kcmbPaletteMode.Items.Add("Office 2003");
        kcmbPaletteMode.Items.Add("Office 2007");
        kcmbPaletteMode.Items.Add("Office 2010");
        kcmbPaletteMode.Items.Add("Office 2013");
        kcmbPaletteMode.Items.Add("Sparkle Blue");
        kcmbPaletteMode.Items.Add("Sparkle Orange");
        kcmbPaletteMode.Items.Add("Sparkle Purple");
        kcmbPaletteMode.SelectedIndex = 0; // "Global"
    }

    private void InitializeContextMenu()
    {
        _contextMenu = new KryptonContextMenu();
        var items = new KryptonContextMenuItems();

        var addButtonItem = new KryptonContextMenuItem("Add Button");
        addButtonItem.Click += (s, e) => kbtnAddButton_Click(s!, e);
        items.Items.Add(addButtonItem);

        var addLabelItem = new KryptonContextMenuItem("Add Label");
        addLabelItem.Click += (s, e) => kbtnAddLabel_Click(s!, e);
        items.Items.Add(addLabelItem);

        var addTextBoxItem = new KryptonContextMenuItem("Add TextBox");
        addTextBoxItem.Click += (s, e) => kbtnAddTextBox_Click(s!, e);
        items.Items.Add(addTextBoxItem);

        items.Items.Add(new KryptonContextMenuSeparator());

        var clearItem = new KryptonContextMenuItem("Clear All");
        clearItem.Click += (s, e) => kbtnClear_Click(s!, e);
        items.Items.Add(clearItem);

        _contextMenu.Items.Add(items);
        kflpDemo.KryptonContextMenu = _contextMenu;
    }

    private void SetupAdditionalPanels()
    {
        // Add some initial sample controls to demonstrate flow layout
        for (int i = 0; i < 3; i++)
        {
            var button = new KryptonButton
            {
                Text = $"Sample {++_controlCounter}",
                Size = new Size(90, 30),
                Margin = new Padding(5)
            };
            button.Click += (s, e) =>
            {
                AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] {button.Text} clicked");
            };
            kflpDemo.Controls.Add(button);
        }
    }

    private void UpdateUIState()
    {
        if (kflpDemo != null)
        {
            klblFlowDirectionStatus.Text = $"Flow Direction: {kflpDemo.FlowDirection}";
            klblWrapContents.Text = $"Wrap Contents: {kflpDemo.WrapContents}";
            klblAutoSize.Text = $"Auto Size: {kflpDemo.AutoSize}";
            klblControlCount.Text = $"Control Count: {kflpDemo.Controls.Count}";
            klblPanelBackStyleStatus.Text = $"Panel Back Style: {kflpDemo.PanelBackStyle}";
            kchkEnabled.Checked = kflpDemo.Enabled;
        }
    }

    private void kbtnAddButton_Click(object sender, EventArgs e)
    {
        var button = new KryptonButton
        {
            Text = $"Button {++_controlCounter}",
            Size = new Size(100, 30),
            Margin = new Padding(5)
        };
        button.Click += (s, e) =>
        {
            AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] {button.Text} clicked");
        };

        kflpDemo.Controls.Add(button);
        UpdateUIState();
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Added {button.Text}");
    }

    private void kbtnAddCheckBox_Click(object sender, EventArgs e)
    {
        var checkBox = new KryptonCheckBox
        {
            Text = $"CheckBox {++_controlCounter}",
            Size = new Size(120, 25),
            Margin = new Padding(5),
            Checked = _controlCounter % 2 == 0
        };
        checkBox.CheckedChanged += (s, e) =>
        {
            AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] {checkBox.Text} checked: {checkBox.Checked}");
        };

        kflpDemo.Controls.Add(checkBox);
        UpdateUIState();
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Added {checkBox.Text}");
    }

    private void kbtnAddRadioButton_Click(object sender, EventArgs e)
    {
        var radioButton = new KryptonRadioButton
        {
            Text = $"Radio {++_controlCounter}",
            Size = new Size(100, 25),
            Margin = new Padding(5)
        };
        radioButton.CheckedChanged += (s, e) =>
        {
            if (radioButton.Checked)
            {
                AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] {radioButton.Text} selected");
            }
        };

        kflpDemo.Controls.Add(radioButton);
        UpdateUIState();
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Added {radioButton.Text}");
    }

    private void kbtnAddComboBox_Click(object sender, EventArgs e)
    {
        var comboBox = new KryptonComboBox
        {
            Text = $"ComboBox {++_controlCounter}",
            Size = new Size(120, 25),
            Margin = new Padding(5)
        };
        comboBox.Items.AddRange(new[] { "Item 1", "Item 2", "Item 3", "Item 4" });
        comboBox.SelectedIndexChanged += (s, e) =>
        {
            AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] {comboBox.Text} selection changed: {comboBox.SelectedItem}");
        };

        kflpDemo.Controls.Add(comboBox);
        UpdateUIState();
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Added {comboBox.Text}");
    }

    private void kbtnAddLabel_Click(object sender, EventArgs e)
    {
        var label = new KryptonLabel
        {
            Text = $"Label {++_controlCounter}",
            Size = new Size(80, 25),
            Margin = new Padding(5),
            LabelStyle = LabelStyle.NormalControl
        };

        kflpDemo.Controls.Add(label);
        UpdateUIState();
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Added {label.Text}");
    }

    private void kbtnAddTextBox_Click(object sender, EventArgs e)
    {
        var textBox = new KryptonTextBox
        {
            Text = $"TextBox {++_controlCounter}",
            Size = new Size(120, 25),
            Margin = new Padding(5)
        };

        kflpDemo.Controls.Add(textBox);
        UpdateUIState();
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Added {textBox.Text}");
    }

    private void kbtnClear_Click(object sender, EventArgs e)
    {
        kflpDemo.Controls.Clear();
        _controlCounter = 0;
        UpdateUIState();
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Cleared all controls");
    }

    private void kbtnRemoveLast_Click(object sender, EventArgs e)
    {
        if (kflpDemo.Controls.Count > 0)
        {
            var lastControl = kflpDemo.Controls[kflpDemo.Controls.Count - 1];
            kflpDemo.Controls.Remove(lastControl);
            UpdateUIState();
            AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Removed {lastControl.Name ?? lastControl.GetType().Name}");
        }
    }

    private void kchkWrapContents_CheckedChanged(object sender, EventArgs e)
    {
        if (kflpDemo != null)
        {
            kflpDemo.WrapContents = kchkWrapContents.Checked;
            UpdateUIState();
            AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Wrap Contents: {kflpDemo.WrapContents}");
        }
    }

    private void kchkAutoSize_CheckedChanged(object sender, EventArgs e)
    {
        if (kflpDemo != null)
        {
            kflpDemo.AutoSize = kchkAutoSize.Checked;
            UpdateUIState();
            AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Auto Size: {kflpDemo.AutoSize}");
        }
    }

    private void kcmbFlowDirection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (kflpDemo == null)
        {
            return;
        }

        var selected = kcmbFlowDirection.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(selected))
        {
            return;
        }

        kflpDemo.FlowDirection = selected switch
        {
            "LeftToRight" => FlowDirection.LeftToRight,
            "TopDown" => FlowDirection.TopDown,
            "RightToLeft" => FlowDirection.RightToLeft,
            "BottomUp" => FlowDirection.BottomUp,
            _ => FlowDirection.LeftToRight
        };

        UpdateUIState();
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Flow Direction: {kflpDemo.FlowDirection}");
    }

    private void kcmbPanelBackStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (kflpDemo == null)
        {
            return;
        }

        var selected = kcmbPanelBackStyle.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(selected))
        {
            return;
        }

        kflpDemo.PanelBackStyle = selected switch
        {
            "PanelClient" => PaletteBackStyle.PanelClient,
            "PanelAlternate" => PaletteBackStyle.PanelAlternate,
            _ => PaletteBackStyle.PanelClient
        };

        UpdateUIState();
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Panel Back Style: {kflpDemo.PanelBackStyle}");
    }

    private void kcmbPaletteMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (kflpDemo == null)
        {
            return;
        }

        var selected = kcmbPaletteMode.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(selected))
        {
            return;
        }

        kflpDemo.PaletteMode = selected switch
        {
            "Global" => PaletteMode.Global,
            "Professional System" => PaletteMode.ProfessionalSystem,
            "Office 2003" => PaletteMode.ProfessionalOffice2003,
            "Office 2007" => PaletteMode.Office2007Blue,
            "Office 2010" => PaletteMode.Office2010Blue,
            "Office 2013" => PaletteMode.Office2013White,
            "Sparkle Blue" => PaletteMode.SparkleBlue,
            "Sparkle Orange" => PaletteMode.SparkleOrange,
            "Sparkle Purple" => PaletteMode.SparklePurple,
            _ => PaletteMode.Global
        };

        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Palette Mode: {kflpDemo.PaletteMode}");
    }

    private void knudPadding_ValueChanged(object sender, EventArgs e)
    {
        if (kflpDemo != null)
        {
            var padding = (int)knudPadding.Value;
            kflpDemo.Padding = new Padding(padding);
            AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Padding: {padding}");
        }
    }

    private void AddEventToList(string message)
    {
        if (klstEvents.InvokeRequired)
        {
            klstEvents.Invoke(new Action(() => AddEventToList(message)));
            return;
        }

        klstEvents.Items.Add(message);

        if (klstEvents.Items.Count > 0)
        {
            klstEvents.SelectedIndex = klstEvents.Items.Count - 1;
        }

        if (klstEvents.Items.Count > 500)
        {
            klstEvents.Items.RemoveAt(0);
        }
    }

    private void kbtnClearEvents_Click(object sender, EventArgs e)
    {
        klstEvents.Items.Clear();
    }

    private void kbtnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void kbtnAddSampleControls_Click(object sender, EventArgs e)
    {
        // Add a mix of controls to demonstrate flow layout
        for (int i = 0; i < 5; i++)
        {
            var button = new KryptonButton
            {
                Text = $"Sample {++_controlCounter}",
                Size = new Size(90, 30),
                Margin = new Padding(3)
            };
            kflpDemo.Controls.Add(button);
        }

        for (int i = 0; i < 3; i++)
        {
            var label = new KryptonLabel
            {
                Text = $"Label {++_controlCounter}",
                Size = new Size(70, 25),
                Margin = new Padding(3),
                LabelStyle = LabelStyle.NormalControl
            };
            kflpDemo.Controls.Add(label);
        }

        UpdateUIState();
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Added 8 sample controls");
    }

    private void kbtnAddMixedControls_Click(object sender, EventArgs e)
    {
        // Add a comprehensive mix of different control types
        var button = new KryptonButton
        {
            Text = $"Button {++_controlCounter}",
            Size = new Size(100, 30),
            Margin = new Padding(5)
        };
        kflpDemo.Controls.Add(button);

        var checkBox = new KryptonCheckBox
        {
            Text = $"Check {++_controlCounter}",
            Size = new Size(100, 25),
            Margin = new Padding(5)
        };
        kflpDemo.Controls.Add(checkBox);

        var radioButton = new KryptonRadioButton
        {
            Text = $"Radio {++_controlCounter}",
            Size = new Size(100, 25),
            Margin = new Padding(5)
        };
        kflpDemo.Controls.Add(radioButton);

        var textBox = new KryptonTextBox
        {
            Text = $"TextBox {++_controlCounter}",
            Size = new Size(120, 25),
            Margin = new Padding(5)
        };
        kflpDemo.Controls.Add(textBox);

        var comboBox = new KryptonComboBox
        {
            Text = $"Combo {++_controlCounter}",
            Size = new Size(120, 25),
            Margin = new Padding(5)
        };
        comboBox.Items.AddRange(new[] { "Option 1", "Option 2", "Option 3" });
        kflpDemo.Controls.Add(comboBox);

        var label = new KryptonLabel
        {
            Text = $"Label {++_controlCounter}",
            Size = new Size(80, 25),
            Margin = new Padding(5),
            LabelStyle = LabelStyle.NormalControl
        };
        kflpDemo.Controls.Add(label);

        UpdateUIState();
        AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Added 6 mixed controls");
    }

    private void kchkEnabled_CheckedChanged(object sender, EventArgs e)
    {
        if (kflpDemo != null)
        {
            kflpDemo.Enabled = kchkEnabled.Checked;
            UpdateUIState();
            AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Panel Enabled: {kflpDemo.Enabled}");
        }
    }

    private void knudMargin_ValueChanged(object sender, EventArgs e)
    {
        if (kflpDemo != null)
        {
            var margin = (int)knudMargin.Value;
            // Update all existing controls' margins
            foreach (Control control in kflpDemo.Controls)
            {
                control.Margin = new Padding(margin);
            }
            AddEventToList($"[{DateTime.Now:HH:mm:ss.fff}] Control Margin: {margin}");
        }
    }
}
