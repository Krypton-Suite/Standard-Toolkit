#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class CheckBoxTest : KryptonForm
{
    public CheckBoxTest()
    {
        AddRtlToggleButton();
        AddCheckBoxes();
    }

    private void AddRtlToggleButton()
    {
        var btnToggleRtl = new Krypton.Toolkit.KryptonButton
        {
            Name = "btnToggleRtl",
            Text = $"RTL: {(RightToLeft == RightToLeft.Yes ? "ON" : "OFF")}",
            Location = new System.Drawing.Point(13, 530),
            Size = new System.Drawing.Size(120, 30),
            Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left
        };
        btnToggleRtl.Click += BtnToggleRtl_Click;
        Controls.Add(btnToggleRtl);
    }

    private void AddCheckBoxes()
    {
        // Basic CheckBox - Left Position
        var lblBasicLeft = new Krypton.Toolkit.KryptonLabel
        {
            Text = "Basic CheckBox (Left Position):",
            Location = new System.Drawing.Point(20, 20),
            Size = new System.Drawing.Size(200, 20)
        };
        Controls.Add(lblBasicLeft);

        var checkBoxBasicLeft = new Krypton.Toolkit.KryptonCheckBox
        {
            Name = "checkBoxBasicLeft",
            Text = "This is a basic check box with text on the right",
            Location = new System.Drawing.Point(20, 50),
            Size = new System.Drawing.Size(300, 25),
            CheckPosition = Krypton.Toolkit.VisualOrientation.Left,
            Checked = true
        };
        Controls.Add(checkBoxBasicLeft);

        // Basic CheckBox - Right Position
        var lblBasicRight = new Krypton.Toolkit.KryptonLabel
        {
            Text = "Basic CheckBox (Right Position):",
            Location = new System.Drawing.Point(20, 90),
            Size = new System.Drawing.Size(200, 20)
        };
        Controls.Add(lblBasicRight);

        var checkBoxBasicRight = new Krypton.Toolkit.KryptonCheckBox
        {
            Name = "checkBoxBasicRight",
            Text = "This is a basic check box with text on the left",
            Location = new System.Drawing.Point(20, 120),
            Size = new System.Drawing.Size(300, 25),
            CheckPosition = Krypton.Toolkit.VisualOrientation.Right,
            Checked = false
        };
        Controls.Add(checkBoxBasicRight);

        // Three State CheckBox
        var lblThreeState = new Krypton.Toolkit.KryptonLabel
        {
            Text = "Three State CheckBox:",
            Location = new System.Drawing.Point(20, 160),
            Size = new System.Drawing.Size(200, 20)
        };
        Controls.Add(lblThreeState);

        var checkBoxThreeState = new Krypton.Toolkit.KryptonCheckBox
        {
            Name = "checkBoxThreeState",
            Text = "This is a three state check box (Unchecked/Checked/Indeterminate)",
            Location = new System.Drawing.Point(20, 190),
            Size = new System.Drawing.Size(350, 25),
            CheckPosition = Krypton.Toolkit.VisualOrientation.Left,
            ThreeState = true,
            CheckState = System.Windows.Forms.CheckState.Indeterminate
        };
        Controls.Add(checkBoxThreeState);

        // Disabled CheckBox
        var lblDisabled = new Krypton.Toolkit.KryptonLabel
        {
            Text = "Disabled CheckBox:",
            Location = new System.Drawing.Point(20, 230),
            Size = new System.Drawing.Size(200, 20)
        };
        Controls.Add(lblDisabled);

        var checkBoxDisabled = new Krypton.Toolkit.KryptonCheckBox
        {
            Name = "checkBoxDisabled",
            Text = "This is a disabled check box",
            Location = new System.Drawing.Point(20, 260),
            Size = new System.Drawing.Size(300, 25),
            CheckPosition = Krypton.Toolkit.VisualOrientation.Left,
            Checked = true,
            Enabled = false
        };
        Controls.Add(checkBoxDisabled);

        // Long Text CheckBox
        var lblLongText = new Krypton.Toolkit.KryptonLabel
        {
            Text = "Long Text CheckBox:",
            Location = new System.Drawing.Point(20, 300),
            Size = new System.Drawing.Size(200, 20)
        };
        Controls.Add(lblLongText);

        var checkBoxLongText = new Krypton.Toolkit.KryptonCheckBox
        {
            Name = "checkBoxLongText",
            Text = "This is a check box with very long text that should wrap properly in RTL mode and demonstrate how the layout handles text flow and positioning",
            Location = new System.Drawing.Point(20, 330),
            Size = new System.Drawing.Size(400, 50),
            CheckPosition = Krypton.Toolkit.VisualOrientation.Left,
            Checked = false
        };
        Controls.Add(checkBoxLongText);

        // Top Position CheckBox
        var lblTopPosition = new Krypton.Toolkit.KryptonLabel
        {
            Text = "CheckBox (Top Position):",
            Location = new System.Drawing.Point(450, 20),
            Size = new System.Drawing.Size(200, 20)
        };
        Controls.Add(lblTopPosition);

        var checkBoxTopPosition = new Krypton.Toolkit.KryptonCheckBox
        {
            Name = "checkBoxTopPosition",
            Text = "Check box with check mark on top",
            Location = new System.Drawing.Point(450, 50),
            Size = new System.Drawing.Size(200, 50),
            CheckPosition = Krypton.Toolkit.VisualOrientation.Top,
            Checked = true
        };
        Controls.Add(checkBoxTopPosition);

        // Bottom Position CheckBox
        var lblBottomPosition = new Krypton.Toolkit.KryptonLabel
        {
            Text = "CheckBox (Bottom Position):",
            Location = new System.Drawing.Point(450, 120),
            Size = new System.Drawing.Size(200, 20)
        };
        Controls.Add(lblBottomPosition);

        var checkBoxBottomPosition = new Krypton.Toolkit.KryptonCheckBox
        {
            Name = "checkBoxBottomPosition",
            Text = "Check box with check mark on bottom",
            Location = new System.Drawing.Point(450, 150),
            Size = new System.Drawing.Size(200, 50),
            CheckPosition = Krypton.Toolkit.VisualOrientation.Bottom,
            Checked = false
        };
        Controls.Add(checkBoxBottomPosition);

        // Status Label
        var lblStatus = new Krypton.Toolkit.KryptonLabel
        {
            Name = "lblStatus",
            Text = $"RTL Mode: {(RightToLeft == RightToLeft.Yes ? "Enabled" : "Disabled")} - Test KryptonCheckBox RTL Support",
            Location = new System.Drawing.Point(20, 400),
            Size = new System.Drawing.Size(500, 20),
            Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left
        };
        Controls.Add(lblStatus);

        // Value Display Labels
        var lblValue1 = new Krypton.Toolkit.KryptonLabel
        {
            Name = "lblValue1",
            Text = "State: Checked",
            Location = new System.Drawing.Point(330, 50),
            Size = new System.Drawing.Size(100, 20)
        };
        Controls.Add(lblValue1);

        var lblValue2 = new Krypton.Toolkit.KryptonLabel
        {
            Name = "lblValue2",
            Text = "State: Unchecked",
            Location = new System.Drawing.Point(330, 120),
            Size = new System.Drawing.Size(100, 20)
        };
        Controls.Add(lblValue2);

        var lblValue3 = new Krypton.Toolkit.KryptonLabel
        {
            Name = "lblValue3",
            Text = "State: Indeterminate",
            Location = new System.Drawing.Point(380, 190),
            Size = new System.Drawing.Size(120, 20)
        };
        Controls.Add(lblValue3);

        // Wire up value change events
        checkBoxBasicLeft.CheckedChanged += (s, e) => lblValue1.Text = $"State: {(checkBoxBasicLeft.Checked ? "Checked" : "Unchecked")}";
        checkBoxBasicRight.CheckedChanged += (s, e) => lblValue2.Text = $"State: {(checkBoxBasicRight.Checked ? "Checked" : "Unchecked")}";
        checkBoxThreeState.CheckStateChanged += (s, e) => lblValue3.Text = $"State: {checkBoxThreeState.CheckState}";
    }

    private void BtnToggleRtl_Click(object sender, EventArgs e)
    {
        // Toggle RTL settings for the form and all check boxes
        RightToLeft = RightToLeft == RightToLeft.Yes ? RightToLeft.No : RightToLeft.Yes;

        // Update all check boxes
        foreach (Control control in Controls)
        {
            if (control is Krypton.Toolkit.KryptonCheckBox checkBox)
            {
                checkBox.RightToLeft = RightToLeft;
            }
        }

        // Update status label
        var lblStatus = Controls["lblStatus"] as Krypton.Toolkit.KryptonLabel;
        if (lblStatus != null)
            lblStatus.Text = $"RTL Mode: {(RightToLeft == RightToLeft.Yes ? "Enabled" : "Disabled")} - Test KryptonCheckBox RTL Support";

        // Update button text
        var btn = Controls["btnToggleRtl"] as Krypton.Toolkit.KryptonButton;
        if (btn != null)
            btn.Text = $"RTL: {(RightToLeft == RightToLeft.Yes ? "ON" : "OFF")}";
    }
} 