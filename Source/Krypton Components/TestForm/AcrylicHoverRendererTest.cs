#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

using System.Drawing;

using Krypton.Toolkit;

namespace TestForm;

/// <summary>
/// Comprehensive test form for Acrylic Hover Renderer feature.
/// Demonstrates all configuration options and visual effects.
/// </summary>
public partial class AcrylicHoverRendererTest : KryptonForm
{
    public AcrylicHoverRendererTest()
    {
        InitializeComponent();
        InitializeSettings();
        SetupEventHandlers();
    }

    private void InitializeSettings()
    {
        var settings = KryptonManager.AcrylicTrackingValuesStatic;

        // Set initial values
        trackBarIntensity.Value = (int)(settings.Intensity * 100);
        labelIntensityValue.Text = settings.Intensity.ToString("F2");

        comboBoxQuality.SelectedIndex = (int)settings.TrackingQuality;

        checkBoxEnabled.Checked = settings.Enabled;
        checkBoxAnimation.Checked = settings.EnableAnimation;

        trackBarAnimationDuration.Value = settings.AnimationDuration;
        labelAnimationDurationValue.Text = settings.AnimationDuration.ToString();

        // Initialize color pickers
        if (settings.LightColor.HasValue)
        {
            panelLightColor.BackColor = settings.LightColor.Value;
            checkBoxCustomLightColor.Checked = true;
        }

        if (settings.DarkColor.HasValue)
        {
            panelDarkColor.BackColor = settings.DarkColor.Value;
            checkBoxCustomDarkColor.Checked = true;
        }

        UpdatePreviewInfo();
    }

    private void SetupEventHandlers()
    {
        // Enable/Disable
        checkBoxEnabled.CheckedChanged += (s, e) =>
        {
            KryptonManager.AcrylicTrackingValuesStatic.Enabled = checkBoxEnabled.Checked;
            InvalidateControls();
            UpdatePreviewInfo();
        };

        // Intensity
        trackBarIntensity.ValueChanged += (s, e) =>
        {
            float intensity = trackBarIntensity.Value / 100f;
            KryptonManager.AcrylicTrackingValuesStatic.Intensity = intensity;
            labelIntensityValue.Text = intensity.ToString("F2");
            InvalidateControls();
            UpdatePreviewInfo();
        };

        // Quality
        comboBoxQuality.SelectedIndexChanged += (s, e) =>
        {
            if (comboBoxQuality.SelectedIndex >= 0)
            {
                KryptonManager.AcrylicTrackingValuesStatic.TrackingQuality = (AcrylicTrackingQuality)comboBoxQuality.SelectedIndex;
                InvalidateControls();
                UpdatePreviewInfo();
            }
        };

        // Animation
        checkBoxAnimation.CheckedChanged += (s, e) =>
        {
            KryptonManager.AcrylicTrackingValuesStatic.EnableAnimation = checkBoxAnimation.Checked;
            trackBarAnimationDuration.Enabled = checkBoxAnimation.Checked;
            labelAnimationDuration.Enabled = checkBoxAnimation.Checked;
            labelAnimationDurationValue.Enabled = checkBoxAnimation.Checked;
            UpdatePreviewInfo();
        };

        trackBarAnimationDuration.ValueChanged += (s, e) =>
        {
            KryptonManager.AcrylicTrackingValuesStatic.AnimationDuration = trackBarAnimationDuration.Value;
            labelAnimationDurationValue.Text = trackBarAnimationDuration.Value.ToString();
            UpdatePreviewInfo();
        };

        // Custom Colors
        checkBoxCustomLightColor.CheckedChanged += (s, e) =>
        {
            if (checkBoxCustomLightColor.Checked)
            {
                KryptonManager.AcrylicTrackingValuesStatic.LightColor = panelLightColor.BackColor;
            }
            else
            {
                KryptonManager.AcrylicTrackingValuesStatic.LightColor = null;
            }
            panelLightColor.Enabled = checkBoxCustomLightColor.Checked;
            InvalidateControls();
            UpdatePreviewInfo();
        };

        checkBoxCustomDarkColor.CheckedChanged += (s, e) =>
        {
            if (checkBoxCustomDarkColor.Checked)
            {
                KryptonManager.AcrylicTrackingValuesStatic.DarkColor = panelDarkColor.BackColor;
            }
            else
            {
                KryptonManager.AcrylicTrackingValuesStatic.DarkColor = null;
            }
            panelDarkColor.Enabled = checkBoxCustomDarkColor.Checked;
            InvalidateControls();
            UpdatePreviewInfo();
        };

        // Color picker buttons
        buttonPickLightColor.Click += (s, e) =>
        {
            using var dialog = new ColorDialog
            {
                Color = panelLightColor.BackColor,
                FullOpen = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                panelLightColor.BackColor = dialog.Color;
                KryptonManager.AcrylicTrackingValuesStatic.LightColor = dialog.Color;
                checkBoxCustomLightColor.Checked = true;
                InvalidateControls();
                UpdatePreviewInfo();
            }
        };

        buttonPickDarkColor.Click += (s, e) =>
        {
            using var dialog = new ColorDialog
            {
                Color = panelDarkColor.BackColor,
                FullOpen = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                panelDarkColor.BackColor = dialog.Color;
                KryptonManager.AcrylicTrackingValuesStatic.DarkColor = dialog.Color;
                checkBoxCustomDarkColor.Checked = true;
                InvalidateControls();
                UpdatePreviewInfo();
            }
        };

        // Reset button
        buttonReset.Click += (s, e) =>
        {
            KryptonManager.AcrylicTrackingValuesStatic.Reset();
            InitializeSettings();
            InvalidateControls();
        };

        // Preset buttons
        buttonPresetHighQuality.Click += (s, e) => ApplyPreset(PresetType.HighQuality);
        buttonPresetBalanced.Click += (s, e) => ApplyPreset(PresetType.Balanced);
        buttonPresetPerformance.Click += (s, e) => ApplyPreset(PresetType.Performance);
        buttonPresetSubtle.Click += (s, e) => ApplyPreset(PresetType.Subtle);
        buttonPresetIntense.Click += (s, e) => ApplyPreset(PresetType.Intense);
    }

    private void InvalidateControls()
    {
        // Invalidate all demo controls to show updated effects
        kryptonButton1.Invalidate();
        kryptonButton2.Invalidate();
        kryptonButton3.Invalidate();
        kryptonButton4.Invalidate();
        kryptonButton5.Invalidate();
        kryptonButton6.Invalidate();
        kryptonButton7.Invalidate();
        kryptonButton8.Invalidate();
        kryptonButton9.Invalidate();
        kryptonButton10.Invalidate();
        kryptonButton11.Invalidate();
        kryptonButton12.Invalidate();

        kryptonCheckButton1.Invalidate();
        kryptonCheckButton2.Invalidate();
        kryptonCheckButton3.Invalidate();

        kryptonRadioButton1.Invalidate();
        kryptonRadioButton2.Invalidate();
        kryptonRadioButton3.Invalidate();

        kryptonLinkLabel1.Invalidate();
        kryptonLinkLabel2.Invalidate();
        kryptonLinkLabel3.Invalidate();

        kryptonLabel1.Invalidate();
        kryptonLabel2.Invalidate();
        kryptonLabel3.Invalidate();

        kryptonHeader1.Invalidate();
        kryptonHeader2.Invalidate();
        kryptonHeader3.Invalidate();
    }

    private void UpdatePreviewInfo()
    {
        var settings = KryptonManager.AcrylicTrackingValuesStatic;

        labelPreviewInfo.Text = $@"Acrylic Hover Settings:
Enabled: {settings.Enabled}
Intensity: {settings.Intensity:F2}
Quality: {settings.TrackingQuality}
Light Color: {(settings.LightColor.HasValue ? settings.LightColor.Value.ToString() : "Auto")}
Dark Color: {(settings.DarkColor.HasValue ? settings.DarkColor.Value.ToString() : "Auto")}
Animation: {settings.EnableAnimation}
Duration: {settings.AnimationDuration}ms";
    }

    private enum PresetType
    {
        HighQuality,
        Balanced,
        Performance,
        Subtle,
        Intense
    }

    private void ApplyPreset(PresetType preset)
    {
        var settings = KryptonManager.AcrylicTrackingValuesStatic;

        switch (preset)
        {
            case PresetType.HighQuality:
                settings.Enabled = true;
                settings.Intensity = 1.0f;
                settings.TrackingQuality = AcrylicTrackingQuality.HighQuality;
                settings.LightColor = null;
                settings.DarkColor = null;
                settings.EnableAnimation = false;
                break;

            case PresetType.Balanced:
                settings.Enabled = true;
                settings.Intensity = 1.0f;
                settings.TrackingQuality = AcrylicTrackingQuality.Balanced;
                settings.LightColor = null;
                settings.DarkColor = null;
                settings.EnableAnimation = false;
                break;

            case PresetType.Performance:
                settings.Enabled = true;
                settings.Intensity = 0.8f;
                settings.TrackingQuality = AcrylicTrackingQuality.Performance;
                settings.LightColor = null;
                settings.DarkColor = null;
                settings.EnableAnimation = false;
                break;

            case PresetType.Subtle:
                settings.Enabled = true;
                settings.Intensity = 0.5f;
                settings.TrackingQuality = AcrylicTrackingQuality.Balanced;
                settings.LightColor = null;
                settings.DarkColor = null;
                settings.EnableAnimation = false;
                break;

            case PresetType.Intense:
                settings.Enabled = true;
                settings.Intensity = 1.8f;
                settings.TrackingQuality = AcrylicTrackingQuality.HighQuality;
                settings.LightColor = null;
                settings.DarkColor = null;
                settings.EnableAnimation = false;
                break;
        }

        InitializeSettings();
        InvalidateControls();
    }
}
