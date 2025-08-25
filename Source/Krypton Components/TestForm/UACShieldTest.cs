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
public partial class UACShieldTest : KryptonForm
{
    public UACShieldTest()
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
            Text = "Light Theme Shield",
            Location = new Point(200, 260),
            Size = new Size(150, 40)
        };
        btnLightTheme.Values.UseSystemShieldIcon = true;
        btnLightTheme.Values.UACShieldIconSize = UACShieldIconSize.Medium;
        btnLightTheme.Values.ShieldIconThemeMode = ShieldIconThemeMode.Light;
        Controls.Add(btnLightTheme);

        // Test 11: Force dark theme
        var btnDarkTheme = new KryptonButton
        {
            Text = "Dark Theme Shield",
            Location = new Point(380, 20),
            Size = new Size(150, 40)
        };
        btnDarkTheme.Values.UseSystemShieldIcon = true;
        btnDarkTheme.Values.UACShieldIconSize = UACShieldIconSize.Medium;
        btnDarkTheme.Values.ShieldIconThemeMode = ShieldIconThemeMode.Dark;
        Controls.Add(btnDarkTheme);

        // Test 12: System theme mode
        var btnSystemTheme = new KryptonButton
        {
            Text = "System Theme Shield",
            Location = new Point(380, 80),
            Size = new Size(150, 40)
        };
        btnSystemTheme.Values.UseSystemShieldIcon = true;
        btnSystemTheme.Values.UACShieldIconSize = UACShieldIconSize.Medium;
        btnSystemTheme.Values.ShieldIconThemeMode = ShieldIconThemeMode.System;
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

        // Test 16: Refresh button to test dynamic updates
        var btnRefresh = new KryptonButton
        {
            Text = "Refresh All Shields",
            Location = new Point(380, 140),
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
            var shieldIcon = UacShieldIconHelper.GetShieldIcon();
            if (shieldIcon != null)
            {
                return $"System Shield Icon Available: Yes\nIcon Size: {shieldIcon.Size.Width}x{shieldIcon.Size.Height}";
            }
            else
            {
                return "System Shield Icon Available: No\nFalling back to SystemIcons.Shield";
            }
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
            // Check if we're on Windows 10 or later for dark mode support
            if (OSUtilities.IsWindowsTen || OSUtilities.IsAtLeastWindowsEleven)
            {
                // Try to detect the current theme using registry
                try
                {
                    using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
                    {
                        if (key != null)
                        {
                            var value = key.GetValue("AppsUseLightTheme");
                            if (value != null)
                            {
                                return (int)value == 0 ? "Dark" : "Light";
                            }
                        }
                    }
                }
                catch
                {
                    // Registry access failed, try alternative method
                }

                // Alternative: Check if we can detect dark mode from system colors
                var backColor = SystemColors.Window;
                var brightness = (backColor.R * 299 + backColor.G * 587 + backColor.B * 114) / 1000;
                return brightness > 128 ? "Light" : "Dark";
            }
            
            return "Classic (No theme support)";
        }
        catch
        {
            return "Unknown";
        }
    }
}
