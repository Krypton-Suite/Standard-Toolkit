#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace TestForm;

/// <summary>
/// Test form to demonstrate UAC Shield functionality
/// </summary>
public partial class UACShieldTestOld : KryptonForm
{
    public UACShieldTestOld()
    {
        InitializeComponent();
        InitializeUACShieldTests();
    }

    private void InitializeUACShieldTests()
    {
        // Test 1: Basic UAC Shield with system icon
        var btnSystemShield = new KryptonButton
        {
            Text = "System UAC Shield",
            Location = new Point(20, 20),
            Size = new Size(150, 40)
        };
        btnSystemShield.Values.UseSystemShieldIcon = true;
        btnSystemShield.Values.UACShieldIconSize = UACShieldIconSize.Small;
        Controls.Add(btnSystemShield);

        // Test 2: UAC Shield with different sizes
        var btnLargeShield = new KryptonButton
        {
            Text = "Large UAC Shield",
            Location = new Point(20, 80),
            Size = new Size(150, 40)
        };
        btnLargeShield.Values.UseSystemShieldIcon = true;
        btnLargeShield.Values.UACShieldIconSize = UACShieldIconSize.Large;
        Controls.Add(btnLargeShield);

        // Test 3: Legacy UAC elevation button
        var btnLegacyUAC = new KryptonButton
        {
            Text = "Legacy UAC Button",
            Location = new Point(20, 140),
            Size = new Size(150, 40)
        };
        btnLegacyUAC.Values.UseAsUACElevationButton = true;
        btnLegacyUAC.Values.UACShieldIconSize = UACShieldIconSize.Medium;
        Controls.Add(btnLegacyUAC);

        // Test 4: Extra small shield
        var btnExtraSmall = new KryptonButton
        {
            Text = "Extra Small Shield",
            Location = new Point(20, 200),
            Size = new Size(150, 40)
        };
        btnExtraSmall.Values.UseSystemShieldIcon = true;
        btnExtraSmall.Values.UACShieldIconSize = UACShieldIconSize.ExtraSmall;
        Controls.Add(btnExtraSmall);

        // Test 5: Extra large shield
        var btnExtraLarge = new KryptonButton
        {
            Text = "Extra Large Shield",
            Location = new Point(20, 260),
            Size = new Size(150, 40)
        };
        btnExtraLarge.Values.UseSystemShieldIcon = true;
        btnExtraLarge.Values.UACShieldIconSize = UACShieldIconSize.ExtraLarge;
        Controls.Add(btnExtraLarge);

        // Test 6: Dynamic size change
        var btnDynamic = new KryptonButton
        {
            Text = "Dynamic Shield",
            Location = new Point(200, 20),
            Size = new Size(150, 40)
        };
        btnDynamic.Values.UseSystemShieldIcon = true;
        btnDynamic.Values.UACShieldIconSize = UACShieldIconSize.Small;
        btnDynamic.Click += (sender, e) =>
        {
            // Cycle through different sizes
            var currentSize = btnDynamic.Values.UACShieldIconSize;
            btnDynamic.Values.UACShieldIconSize = currentSize switch
            {
                UACShieldIconSize.ExtraSmall => UACShieldIconSize.Small,
                UACShieldIconSize.Small => UACShieldIconSize.Medium,
                UACShieldIconSize.Medium => UACShieldIconSize.Large,
                UACShieldIconSize.Large => UACShieldIconSize.ExtraLarge,
                UACShieldIconSize.ExtraLarge => UACShieldIconSize.ExtraSmall,
                _ => UACShieldIconSize.ExtraSmall
            };
        };
        Controls.Add(btnDynamic);

        // Test 7: Toggle between system and legacy
        var btnToggle = new KryptonButton
        {
            Text = "Toggle Shield Type",
            Location = new Point(200, 80),
            Size = new Size(150, 40)
        };
        btnToggle.Values.UseSystemShieldIcon = true;
        btnToggle.Values.UACShieldIconSize = UACShieldIconSize.Medium;
        btnToggle.Click += (sender, e) =>
        {
            if (btnToggle.Values.UseSystemShieldIcon)
            {
                btnToggle.Values.UseSystemShieldIcon = false;
                btnToggle.Values.UseAsUACElevationButton = true;
                btnToggle.Text = "Legacy UAC";
            }
            else
            {
                btnToggle.Values.UseAsUACElevationButton = false;
                btnToggle.Values.UseSystemShieldIcon = true;
                btnToggle.Text = "System Shield";
            }
        };
        Controls.Add(btnToggle);

        // Test 8: Clear shield
        var btnClear = new KryptonButton
        {
            Text = "Clear Shield",
            Location = new Point(200, 140),
            Size = new Size(150, 40)
        };
        btnClear.Click += (sender, e) =>
        {
            btnClear.Values.UseSystemShieldIcon = false;
            btnClear.Values.UseAsUACElevationButton = false;
            btnClear.Text = "Shield Cleared";
        };
        Controls.Add(btnClear);

        // Test 9: Theme-aware shield
        var btnThemeAware = new KryptonButton
        {
            Text = "Theme Aware Shield",
            Location = new Point(200, 200),
            Size = new Size(150, 40)
        };
        btnThemeAware.Values.UseSystemShieldIcon = true;
        btnThemeAware.Values.UACShieldIconSize = UACShieldIconSize.Medium;
        btnThemeAware.Values.UseThemeAwareShieldIcon = true;
        Controls.Add(btnThemeAware);

        // Test 10: Force light theme
        var btnLightTheme = new KryptonButton
        {
            Text = "Modern Theme Shield",
            Location = new Point(200, 260),
            Size = new Size(150, 40)
        };
        btnLightTheme.Values.UseSystemShieldIcon = true;
        btnLightTheme.Values.UACShieldIconSize = UACShieldIconSize.Medium;
        // Theme-aware approach is now automatic
        Controls.Add(btnLightTheme);

        // Test 11: Mid-modern theme
        var btnDarkTheme = new KryptonButton
        {
            Text = "Mid-Modern Theme Shield",
            Location = new Point(380, 20),
            Size = new Size(150, 40)
        };
        btnDarkTheme.Values.UseSystemShieldIcon = true;
        btnDarkTheme.Values.UACShieldIconSize = UACShieldIconSize.Medium;
        // Theme-aware approach is now automatic
        Controls.Add(btnDarkTheme);

        // Test 12: Legacy theme
        var btnSystemTheme = new KryptonButton
        {
            Text = "Legacy Theme Shield",
            Location = new Point(380, 80),
            Size = new Size(150, 40)
        };
        btnSystemTheme.Values.UseSystemShieldIcon = true;
        btnSystemTheme.Values.UACShieldIconSize = UACShieldIconSize.Medium;
        // Theme-aware approach is now automatic
        Controls.Add(btnSystemTheme);

        // Test 13: Show current OS info
        var lblOSInfo = new KryptonLabel
        {
            Text = $"Current OS: {GetOSInfo()}",
            Location = new Point(20, 320),
            Size = new Size(400, 30)
        };
        Controls.Add(lblOSInfo);

        // Test 14: Show current shield icon info
        var lblShieldInfo = new KryptonLabel
        {
            Text = "Shield Icon Information:",
            Location = new Point(20, 360),
            Size = new Size(400, 30)
        };
        Controls.Add(lblShieldInfo);

        var lblShieldDetails = new KryptonLabel
        {
            Text = GetShieldIconInfo(),
            Location = new Point(20, 390),
            Size = new Size(400, 60),
            AutoSize = false
        };
        Controls.Add(lblShieldDetails);

        // Test 15: Show DPI and theme info
        var lblDPIInfo = new KryptonLabel
        {
            Text = $"DPI Scale: {GetDPIScale()}, Theme: {GetCurrentTheme()}",
            Location = new Point(20, 460),
            Size = new Size(400, 30)
        };
        Controls.Add(lblDPIInfo);

        // Test 16: Theme-aware shield icon testing
        var btnThemeTest = new KryptonButton
        {
            Text = "Test Theme Icons",
            Location = new Point(380, 140),
            Size = new Size(150, 40)
        };
        btnThemeTest.Click += (sender, e) =>
        {
            // Test different theme modes to show theme-aware icon selection
            var modernIcon = UacShieldIconHelper.GetThemeAwareShieldIcon(PaletteMode.Office2013White);
            var legacyIcon = UacShieldIconHelper.GetThemeAwareShieldIcon(PaletteMode.Office2007Blue);
            
            // Show the difference in a message box
            var message = $"Theme-aware UAC Shield Icons:\n\n" +
                         $"Modern Theme (Office 2019): {(modernIcon != null ? "Available" : "Not Available")}\n" +
                         $"Legacy Theme (Office 2007): {(legacyIcon != null ? "Available" : "Not Available")}\n\n" +
                         $"Current Global Theme: {KryptonManager.CurrentGlobalPaletteMode}";
            
            KryptonMessageBox.Show(message, "Theme-Aware UAC Shield Test", 
                KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
        };
        Controls.Add(btnThemeTest);

        // Test 17: Refresh button to test dynamic updates
        var btnRefresh = new KryptonButton
        {
            Text = "Refresh All Shields",
            Location = new Point(380, 200),
            Size = new Size(150, 40)
        };
        btnRefresh.Click += (sender, e) =>
        {
            // Force refresh of all shield icons
            foreach (Control control in Controls)
            {
                if (control is KryptonButton btn && btn.Values.UseSystemShieldIcon)
                {
                    btn.PerformLayout();
                }
            }
            
            // Update info labels
            lblShieldDetails.Text = GetShieldIconInfo();
            lblDPIInfo.Text = $"DPI Scale: {GetDPIScale()}, Theme: {GetCurrentTheme()}";
        };
        Controls.Add(btnRefresh);
    }

    private string GetOSInfo()
    {
        if (OSUtilities.IsAtLeastWindowsEleven)
            return "Windows 11";
        else if (OSUtilities.IsWindowsTen)
            return "Windows 10";
        else if (OSUtilities.IsWindowsEightPointOne)
            return "Windows 8.1";
        else if (OSUtilities.IsWindowsEight)
            return "Windows 8";
        else if (OSUtilities.IsWindowsSeven)
            return "Windows 7";
        else
            return "Unknown Windows Version";
    }

    private string GetShieldIconInfo()
    {
        try
        {
            var currentTheme = KryptonManager.CurrentGlobalPaletteMode;
            var systemShieldIcon = UacShieldIconHelper.GetShieldIcon();
            var themeAwareIcon = UacShieldIconHelper.GetThemeAwareShieldIcon(currentTheme);
            
            var info = $"Current Theme: {currentTheme}\n";
            info += $"System Shield Icon: {(systemShieldIcon != null ? "Available" : "Not Available")}\n";
            info += $"Theme-Aware Icon: {(themeAwareIcon != null ? "Available" : "Not Available")}";
            
            if (systemShieldIcon != null)
            {
                info += $"\nSystem Icon Size: {systemShieldIcon.Size.Width}x{systemShieldIcon.Size.Height}";
            }
            
            if (themeAwareIcon != null)
            {
                info += $"\nTheme Icon Size: {themeAwareIcon.Size.Width}x{themeAwareIcon.Size.Height}";
            }
            
            return info;
        }
        catch (Exception ex)
        {
            return $"Error getting shield icon: {ex.Message}";
        }
    }

    private string GetDPIScale()
    {
        try
        {
            using (var graphics = CreateGraphics())
            {
                float dpiX = graphics.DpiX;
                float dpiY = graphics.DpiY;
                float scale = dpiX / 96.0f; // 96 DPI is the standard
                return $"{scale:F2}x ({dpiX:F0}x{dpiY:F0})";
            }
        }
        catch
        {
            return "Unknown";
        }
    }

    private string GetCurrentTheme()
    {
        try
        {
            var currentTheme = KryptonManager.CurrentGlobalPaletteMode;
            var themeDescription = currentTheme switch
            {
                PaletteMode.MaterialDark or PaletteMode.MaterialLight or
                PaletteMode.MaterialDarkRipple or PaletteMode.MaterialLightRipple => "Modern (Windows 11 Style)",
                PaletteMode.Office2010Blue or PaletteMode.Office2010Silver or
                PaletteMode.Office2010White or PaletteMode.Office2010Black or
                PaletteMode.Office2013White or PaletteMode.Microsoft365Black or
                PaletteMode.Microsoft365Blue or PaletteMode.Microsoft365Silver => "Mid-Modern (Windows 10 Style)",
                PaletteMode.Office2007Blue or PaletteMode.Office2007Silver or
                PaletteMode.Office2007White or PaletteMode.Office2007Black or
                PaletteMode.ProfessionalOffice2003 or PaletteMode.ProfessionalSystem => "Legacy (Windows 7/8 Style)",
                _ => "Unknown"
            };
            
            return $"{currentTheme} ({themeDescription})";
        }
        catch
        {
            return "Unknown";
        }
    }
}
