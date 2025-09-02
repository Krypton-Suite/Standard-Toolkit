#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm;

public partial class ButtonsTest : KryptonForm
{
    public ButtonsTest()
    {
        InitializeComponent();
        kcbColorScheme.SelectedItem = "OfficeThemes";
        kcbSortMode.Enabled = false;
    }

    private void kcbtnDropDown_SelectedColorChanged(object sender, ColorEventArgs e)
    {
        kryptonButton3.Values.DropDownArrowColor = e.Color;

        kryptonButton4.Values.DropDownArrowColor = e.Color;

        kryptonButton7.Values.DropDownArrowColor = e.Color;

        kryptonButton8.Values.DropDownArrowColor = e.Color;
    }

    private void kbtnButtonStyles_Click(object sender, EventArgs e)
    {
        new ButtonStyleExamples().Show();
    }

    private void kryptonCommand1_Execute(object sender, EventArgs e)
    {
        var typeName = sender.GetType().FullName;
        var item = sender as KryptonContextMenuItem;
        var paramText = item?.CommandParameter is string s ? s : "<no param>";

        KryptonMessageBox.Show($"Command executed by:\nType: {typeName}\nParam: {paramText}", "Context Item Called");
    }

    private void kcbSortMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        var text = kcbSortMode.SelectedItem?.ToString();
        if (!Enum.TryParse<Krypton.Toolkit.ThemeColorSortMode>(text, true, out var mode))
        {
            mode = Krypton.Toolkit.ThemeColorSortMode.OKLCH;
        }
        kcbtnDropDown.ThemeColorSortMode = mode;
    }

    private void kcbColorScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        var text = kcbColorScheme.SelectedItem?.ToString();
        if (!Enum.TryParse<Krypton.Toolkit.ColorScheme>(text, true, out var scheme))
        {
            scheme = Krypton.Toolkit.ColorScheme.OfficeThemes;
        }

        kcbtnDropDown.SchemeThemes = scheme;
        kcbSortMode.Enabled = scheme == Krypton.Toolkit.ColorScheme.PaletteColors;
    }

    private void kbtnTestOSSpecificUAC_Click(object sender, EventArgs e)
    {
        TestOSSpecificUACShields();
    }

    private void TestOSSpecificUACShields()
    {
        // Show current OS detection
        string osInfo = $"Current OS Detection:\n" +
                       $"Windows 7: {OSUtilities.IsWindowsSeven}\n" +
                       $"Windows 8: {OSUtilities.IsWindowsEight}\n" +
                       $"Windows 8.1: {OSUtilities.IsWindowsEightPointOne}\n" +
                       $"Windows 10: {OSUtilities.IsWindowsTen}\n" +
                       $"Windows 11: {OSUtilities.IsWindowsEleven}\n" +
                       $"At Least Windows 11: {OSUtilities.IsAtLeastWindowsEleven}";

        KryptonMessageBox.Show(osInfo, "OS Detection Test", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);

        // Test imageres.dll extraction and available sizes
        try
        {
            var imageresIcon = UACShieldHelper.GetUACShieldIcon(UACShieldIconSize.Small);
            if (imageresIcon != null)
            {
                var availableSizes = UACShieldHelper.GetAvailableImageresSizes();
                string sizeInfo = availableSizes.Length > 0 
                    ? $"Available sizes in imageres.dll: {string.Join(", ", availableSizes.Select(s => $"{s.Width}x{s.Height}"))}"
                    : "No sizes available in imageres.dll";
                
                KryptonMessageBox.Show($"Successfully extracted UAC shield from imageres.dll!\n\n{sizeInfo}", "Imageres.dll Test", 
                    KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
            }
            else
            {
                KryptonMessageBox.Show("Failed to extract from imageres.dll, using fallback resources.", "Imageres.dll Test", 
                    KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
            }
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show($"Error testing imageres.dll: {ex.Message}", "Imageres.dll Test", 
                KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
        }

        // Test different shield sizes including custom sizes
        var testForm = new KryptonForm
        {
            Text = "UAC Shield Test (Imageres.dll + All Sizes)",
            Size = new Size(800, 600),
            StartPosition = FormStartPosition.CenterParent
        };

        var panel = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(10)
        };

        // Predefined sizes
        var predefinedButtons = new[]
        {
            new { Text = "Tiny (8x8)", Size = UACShieldIconSize.Tiny },
            new { Text = "Extra Small (16x16)", Size = UACShieldIconSize.ExtraSmall },
            new { Text = "Small (24x24)", Size = UACShieldIconSize.Small },
            new { Text = "Medium Small (32x32)", Size = UACShieldIconSize.MediumSmall },
            new { Text = "Medium (48x48)", Size = UACShieldIconSize.Medium },
            new { Text = "Medium Large (64x64)", Size = UACShieldIconSize.MediumLarge },
            new { Text = "Large (96x96)", Size = UACShieldIconSize.Large },
            new { Text = "Extra Large (128x128)", Size = UACShieldIconSize.ExtraLarge },
            new { Text = "Huge (192x192)", Size = UACShieldIconSize.Huge },
            new { Text = "Maximum (256x256)", Size = UACShieldIconSize.Maximum }
        };

        // Create two columns for predefined sizes
        for (int i = 0; i < predefinedButtons.Length; i++)
        {
            int column = i / 5; // 5 buttons per column
            int row = i % 5;
            
            var button = new KryptonButton
            {
                Text = predefinedButtons[i].Text,
                Location = new Point(10 + column * 260, 10 + row * 35),
                Size = new Size(250, 30),
                Values = { UseAsUACElevationButton = true, UACShieldIconSize = predefinedButtons[i].Size }
            };
            panel.Controls.Add(button);
        }

        // Custom sizes
        var customSizes = new[] { new Size(48, 48), new Size(96, 96), new Size(192, 192) };
        var customLabels = new[] { "Custom 48x48", "Custom 96x96", "Custom 192x192" };

        for (int i = 0; i < customSizes.Length; i++)
        {
            var button = new KryptonButton
            {
                Text = customLabels[i],
                Location = new Point(530, 10 + i * 35),
                Size = new Size(250, 30),
                Values = { UseAsUACElevationButton = true, CustomUACShieldSize = customSizes[i] }
            };
            panel.Controls.Add(button);
        }

        // Add labels explaining the test
        var label1 = new KryptonLabel
        {
            Text = "Left columns: All predefined sizes using UACShieldIconSize enum",
            Location = new Point(10, 190),
            Size = new Size(520, 20)
        };
        panel.Controls.Add(label1);

        var label2 = new KryptonLabel
        {
            Text = "Right column: Custom sizes using CustomUACShieldSize property",
            Location = new Point(530, 190),
            Size = new Size(250, 20)
        };
        panel.Controls.Add(label2);

        var label3 = new KryptonLabel
        {
            Text = "All buttons try to extract exact sizes from imageres.dll first, then fall back to scaled local resources.",
            Location = new Point(10, 220),
            Size = new Size(770, 40),
            StateCommon = { Content = { LongText = { MultiLine = true } } }
        };
        panel.Controls.Add(label3);

        testForm.Controls.Add(panel);
        testForm.ShowDialog(this);
    }
}