#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Krypton.Navigator;
using Krypton.Ribbon;
using Krypton.Toolkit;
using Krypton.Utilities;

namespace TestForm;

/// <summary>
/// Test form demonstrating Font Awesome icon support in Krypton Toolkit components.
/// </summary>
public partial class FontAwesomeTest : KryptonForm
{
    public FontAwesomeTest()
    {
        InitializeComponent();
        SetupFontAwesomeDemo();
    }

    private void SetupFontAwesomeDemo()
    {
        // Check if Font Awesome is available
        var isAvailable = FontAwesomeHelper.IsFontAvailable(FontAwesomeStyle.Solid);
        var proAvailable = FontAwesomeHelper.IsFontAvailable(FontAwesomeStyle.Light);

        var statusText = isAvailable
            ? "Font Awesome fonts detected and ready!"
            : "Font Awesome fonts not found. Please install Font Awesome fonts or set FontFilePath.";

        if (proAvailable)
        {
            statusText += " (Pro fonts detected)";
        }

        lblFontStatus.Text = statusText;

        // Configure defaults
        FontAwesomeHelper.DefaultSize = 20;
        FontAwesomeHelper.DefaultColor = Color.Black;

        // Setup button examples
        SetupButtonExamples();

        // Setup navigator examples
        SetupNavigatorExamples();

        // Setup ribbon examples
        SetupRibbonExamples();

        // Setup state examples
        SetupStateExamples();

        // Setup style examples (including Pro styles)
        SetupStyleExamples();

        // Setup icons.json examples
        SetupIconsJsonExamples();

        // Setup Pro style examples
        SetupProStyleExamples();
    }

    private void SetupButtonExamples()
    {
        // Basic icon using enum
        btnHome.Values.SetFontAwesomeIcon(FontAwesomeIcon.Home, size: 24, color: Color.Blue);
        btnHome.Text = "Home";

        // Icon using string name
        btnUser.Values.SetFontAwesomeIcon("user", size: 24, color: Color.Green);
        btnUser.Text = "User";

        // Settings icon
        btnSettings.Values.SetFontAwesomeIcon(FontAwesomeIcon.Settings, size: 24, color: Color.DarkOrange);
        btnSettings.Text = "Settings";

        // Search icon
        btnSearch.Values.SetFontAwesomeIcon(FontAwesomeIcon.Search, size: 24, color: Color.Purple);
        btnSearch.Text = "Search";

        // Save icon
        btnSave.Values.SetFontAwesomeIcon(FontAwesomeIcon.Save, size: 24, color: Color.DarkBlue);
        btnSave.Text = "Save";

        // Download icon
        btnDownload.Values.SetFontAwesomeIcon(FontAwesomeIcon.Download, size: 24, color: Color.DarkGreen);
        btnDownload.Text = "Download";
    }

    private void SetupNavigatorExamples()
    {
        // Create pages with Font Awesome icons
        var page1 = new KryptonPage
        {
            Text = "Home",
            TextTitle = "Home Page"
        };
        page1.SetFontAwesomeIcons(FontAwesomeIcon.Home, smallSize: 16, mediumSize: 24, largeSize: 32, color: Color.Blue);

        var page2 = new KryptonPage
        {
            Text = "User",
            TextTitle = "User Page"
        };
        page2.SetFontAwesomeIcons(FontAwesomeIcon.User, smallSize: 16, mediumSize: 24, largeSize: 32, color: Color.Green);

        var page3 = new KryptonPage
        {
            Text = "Settings",
            TextTitle = "Settings Page"
        };
        page3.SetFontAwesomeIcons(FontAwesomeIcon.Settings, smallSize: 16, mediumSize: 24, largeSize: 32, color: Color.DarkOrange);

        navigator.Pages.Clear();
        navigator.Pages.Add(page1);
        navigator.Pages.Add(page2);
        navigator.Pages.Add(page3);
    }

    private void SetupRibbonExamples()
    {
        // Ribbon examples are demonstrated through the ribbon control if available
        // In a real application, you would use:
        // var ribbonButton = new KryptonRibbonGroupButton();
        // ribbonButton.SetFontAwesomeIcons(FontAwesomeIcon.Print, smallSize: 16, largeSize: 32);
    }

    private void SetupStateExamples()
    {
        // Button with different states
        btnStateDemo.Values.SetFontAwesomeIconStates(
            FontAwesomeIcon.Star,
            normalColor: Color.Blue,
            disabledColor: Color.Gray,
            pressedColor: Color.DarkBlue,
            trackingColor: Color.LightBlue,
            size: 24
        );
        btnStateDemo.Text = "State Demo";
        btnStateDemo.Enabled = true;
    }

    private void SetupStyleExamples()
    {
        // Solid style (default)
        btnSolid.Values.SetFontAwesomeIcon(FontAwesomeIcon.Star, size: 20, color: Color.Blue, style: FontAwesomeStyle.Solid);
        btnSolid.Text = "Solid";

        // Regular style
        btnRegular.Values.SetFontAwesomeIcon(FontAwesomeIcon.Star, size: 20, color: Color.Blue, style: FontAwesomeStyle.Regular);
        btnRegular.Text = "Regular";

        // Brands style (if available)
        btnBrands.Values.SetFontAwesomeIcon(FontAwesomeIcon.Github, size: 20, color: Color.Black, style: FontAwesomeStyle.Brands);
        btnBrands.Text = "Brands";
    }

    private void SetupProStyleExamples()
    {
        // Light style (Pro only)
        if (btnLight != null)
        {
            btnLight.Values.SetFontAwesomeIcon(FontAwesomeIcon.Star, size: 20, color: Color.Blue, style: FontAwesomeStyle.Light);
            btnLight.Text = "Light (Pro)";
            btnLight.Enabled = FontAwesomeHelper.IsFontAvailable(FontAwesomeStyle.Light);
        }

        // Thin style (Pro only)
        if (btnThin != null)
        {
            btnThin.Values.SetFontAwesomeIcon(FontAwesomeIcon.Star, size: 20, color: Color.Blue, style: FontAwesomeStyle.Thin);
            btnThin.Text = "Thin (Pro)";
            btnThin.Enabled = FontAwesomeHelper.IsFontAvailable(FontAwesomeStyle.Thin);
        }

        // Duotone style (Pro only)
        if (btnDuotone != null)
        {
            btnDuotone.Values.SetFontAwesomeIcon(FontAwesomeIcon.Star, size: 20, color: Color.Blue, style: FontAwesomeStyle.Duotone);
            btnDuotone.Text = "Duotone (Pro)";
            btnDuotone.Enabled = FontAwesomeHelper.IsFontAvailable(FontAwesomeStyle.Duotone);
        }
    }

    private void SetupIconsJsonExamples()
    {
        // Try to load icons.json if available
        if (btnLoadIconsJson != null)
        {
            btnLoadIconsJson.Click += BtnLoadIconsJson_Click;
        }

        if (btnShowIconMetadata != null)
        {
            btnShowIconMetadata.Click += BtnShowIconMetadata_Click;
            btnShowIconMetadata.Enabled = FontAwesomeIconMetadataLoader.IsMetadataLoaded;
        }
    }

    private void BtnToggleState_Click(object sender, EventArgs e)
    {
        if (btnStateDemo.Enabled)
        {
            btnStateDemo.Enabled = false;
            btnToggleState.Text = "Enable Button";
        }
        else
        {
            btnStateDemo.Enabled = true;
            btnToggleState.Text = "Disable Button";
        }
    }

    private void BtnClearCache_Click(object sender, EventArgs e)
    {
        FontAwesomeHelper.ClearCache();
        KryptonMessageBox.Show("Font Awesome icon cache cleared.", "Cache Cleared", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
    }

    private void BtnSetFontPath_Click(object sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "Font Files (*.ttf;*.otf)|*.ttf;*.otf|All Files (*.*)|*.*",
            Title = "Select Font Awesome Font File"
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            FontAwesomeHelper.FontFilePath = dialog.FileName;
            FontAwesomeHelper.ClearCache();
            KryptonMessageBox.Show($"Font file path set to:\n{dialog.FileName}\n\nPlease restart the demo to see changes.",
                "Font Path Set", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
        }
    }

    private void BtnDirectRender_Click(object sender, EventArgs e)
    {
        // Demonstrate direct rendering
        var iconBitmap = FontAwesomeHelper.RenderIcon(
            FontAwesomeIcon.Heart,
            size: 64,
            color: Color.Red,
            style: FontAwesomeStyle.Solid
        );

        if (iconBitmap != null)
        {
            using var form = new KryptonForm
            {
                Text = "Direct Rendered Icon",
                Size = new Size(200, 200),
                StartPosition = FormStartPosition.CenterParent
            };

            var pictureBox = new PictureBox
            {
                Image = iconBitmap,
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.CenterImage
            };

            form.Controls.Add(pictureBox);
            form.ShowDialog(this);
            iconBitmap.Dispose();
        }
    }

    private void BtnLoadIconsJson_Click(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
            Title = "Select Font Awesome icons.json File"
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            FontAwesomeIconMetadataLoader.IconsJsonPath = dialog.FileName;
            FontAwesomeIconMetadataLoader.ClearMetadata();

            if (FontAwesomeIconMetadataLoader.LoadMetadata())
            {
                var iconCount = FontAwesomeIconMetadataLoader.GetAvailableIconNames().Count;
                KryptonMessageBox.Show(
                    $"Icons.json loaded successfully!\n\nFound {iconCount} icons.\n\nIcons will now use accurate Unicode mappings from the metadata file.",
                    "Icons.json Loaded",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Information);

                if (btnShowIconMetadata != null)
                {
                    btnShowIconMetadata.Enabled = true;
                }
            }
            else
            {
                KryptonMessageBox.Show(
                    "Failed to load icons.json file. Please check the file format.",
                    "Load Failed",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Warning);
            }
        }
    }

    private void BtnShowIconMetadata_Click(object? sender, EventArgs e)
    {
        if (!FontAwesomeIconMetadataLoader.IsMetadataLoaded)
        {
            KryptonMessageBox.Show(
                "Icons.json metadata not loaded. Please load icons.json first.",
                "Metadata Not Loaded",
                KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Information);
            return;
        }

        var iconNames = FontAwesomeIconMetadataLoader.GetAvailableIconNames();
        var sampleIcons = iconNames.Take(20).ToList();

        var message = $"Icons.json Metadata Loaded\n\n" +
                     $"Total Icons: {iconNames.Count}\n\n" +
                     $"Sample Icons (first 20):\n" +
                     string.Join(", ", sampleIcons);

        KryptonMessageBox.Show(
            message,
            "Icon Metadata",
            KryptonMessageBoxButtons.OK,
            KryptonMessageBoxIcon.Information);
    }

    private void BtnIconPicker_Click(object? sender, EventArgs e)
    {
        // Demonstrate the icon picker dialog
        using var dialog = new Krypton.Utilities.FontAwesomeIconPickerDialog();

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            // Apply the selected icon to a demo button
            if (btnIconPickerDemo != null)
            {
                btnIconPickerDemo.Values.SetFontAwesomeIcon(dialog.SelectedIcon, size: 24, color: Color.Purple);
                btnIconPickerDemo.Text = $"Selected: {dialog.SelectedIcon}";
            }

            KryptonMessageBox.Show(
                $"Icon Picker Selected: {dialog.SelectedIcon}",
                "Icon Selected",
                KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Information);
        }
    }
}

