#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Demo for <see cref="KryptonBlinkingToolStripStatusLabel"/>: hard/soft/visibility blink modes,
/// timing options, colour targets, and side-by-side comparison with a normal status label.
/// </summary>
public partial class BlinkingStatusStripLabelDemo : KryptonForm
{
    private bool _syncing;

    public BlinkingStatusStripLabelDemo()
    {
        InitializeComponent();
        WireEvents();
        SyncUiFromLabel();
        UpdateStatus(@"Ready. Use Start / Stop or BlinkEnabled to exercise the blinking status label.");
    }

    private void WireEvents()
    {
        kbtnStart.Click += (_, _) =>
        {
            ApplyUiToLabel();
            _blinkingLabel.StartBlink();
            UpdateStatus(@"StartBlink() called.");
        };

        kbtnStartDuration.Click += (_, _) =>
        {
            ApplyUiToLabel();
            int duration = (int)knudSessionDuration.Value;
            _blinkingLabel.StartBlink(duration);
            UpdateStatus($@"StartBlink({duration}) called.");
        };

        kbtnStop.Click += (_, _) =>
        {
            _blinkingLabel.StopBlink();
            UpdateStatus(@"StopBlink() called.");
        };

        kchkBlinkEnabled.CheckedChanged += (_, _) =>
        {
            if (_syncing)
            {
                return;
            }

            ApplyUiToLabel();
            _blinkingLabel.BlinkValues.BlinkEnabled = kchkBlinkEnabled.Checked;
            UpdateStatus($@"BlinkValues.BlinkEnabled = {_blinkingLabel.BlinkValues.BlinkEnabled}");
        };

        kcmbMode.SelectedIndexChanged += (_, _) => ApplyUiToLabel();
        kcmbTarget.SelectedIndexChanged += (_, _) => ApplyUiToLabel();
        kcmbVisibilityStyle.SelectedIndexChanged += (_, _) => ApplyUiToLabel();

        knudInterval.ValueChanged += (_, _) => ApplyUiToLabel();
        knudSoftCycle.ValueChanged += (_, _) => ApplyUiToLabel();
        knudSoftTick.ValueChanged += (_, _) => ApplyUiToLabel();
        knudDuration.ValueChanged += (_, _) => ApplyUiToLabel();
        knudMaxCount.ValueChanged += (_, _) => ApplyUiToLabel();

        kchkUseBlinkTextColor.CheckedChanged += (_, _) => ApplyUiToLabel();
        kchkPauseOnMouseOver.CheckedChanged += (_, _) => ApplyUiToLabel();
        kchkRestoreOnStop.CheckedChanged += (_, _) => ApplyUiToLabel();
        kchkBlinkOnlyWhenVisible.CheckedChanged += (_, _) => ApplyUiToLabel();

        kbtnColorOne.SelectedColorChanged += (_, _) => ApplyUiToLabel();
        kbtnColorTwo.SelectedColorChanged += (_, _) => ApplyUiToLabel();
        kbtnTextColor.SelectedColorChanged += (_, _) => ApplyUiToLabel();

        _blinkingLabel.BlinkStarted += (_, _) =>
        {
            _syncing = true;
            kchkBlinkEnabled.Checked = true;
            _syncing = false;
            UpdateStatus(@"BlinkStarted");
        };

        _blinkingLabel.BlinkStopped += (_, _) =>
        {
            _syncing = true;
            kchkBlinkEnabled.Checked = false;
            _syncing = false;
            UpdateStatus(@"BlinkStopped");
        };

        _blinkingLabel.BlinkTick += (_, _) =>
        {
            BlinkingStatusLabelValues blink = _blinkingLabel.BlinkValues;
            klblRuntime.Values.Text =
                $@"IsBlinking={_blinkingLabel.IsBlinking}  Mode={blink.BlinkMode}  Target={blink.BlinkTarget}";
        };
    }

    private void SyncUiFromLabel()
    {
        _syncing = true;

        BlinkingStatusLabelValues blink = _blinkingLabel.BlinkValues;

        kcmbMode.SelectedItem = blink.BlinkMode.ToString();
        kcmbTarget.SelectedItem = blink.BlinkTarget.ToString();
        kcmbVisibilityStyle.SelectedItem = blink.VisibilityStyle.ToString();

        knudInterval.Value = ClampNud(knudInterval, blink.BlinkInterval);
        knudSoftCycle.Value = ClampNud(knudSoftCycle, blink.SoftBlinkCycleInterval);
        knudSoftTick.Value = ClampNud(knudSoftTick, blink.SoftBlinkTickInterval);
        knudDuration.Value = ClampNud(knudDuration, blink.BlinkDuration);
        knudMaxCount.Value = ClampNud(knudMaxCount, blink.MaxBlinkCount);
        knudSessionDuration.Value = 3000;

        kbtnColorOne.SelectedColor = blink.BlinkColorOne;
        kbtnColorTwo.SelectedColor = blink.BlinkColorTwo;
        kbtnTextColor.SelectedColor = blink.BlinkTextColor;

        kchkUseBlinkTextColor.Checked = blink.UseBlinkTextColor;
        kchkPauseOnMouseOver.Checked = blink.PauseOnMouseOver;
        kchkRestoreOnStop.Checked = blink.RestoreAppearanceOnStop;
        kchkBlinkOnlyWhenVisible.Checked = blink.BlinkOnlyWhenVisible;
        kchkBlinkEnabled.Checked = blink.BlinkEnabled;

        _syncing = false;
    }

    private void ApplyUiToLabel()
    {
        if (_syncing)
        {
            return;
        }

        BlinkingStatusLabelValues blink = _blinkingLabel.BlinkValues;

        if (Enum.TryParse(kcmbMode.Text, out BlinkMode mode))
        {
            blink.BlinkMode = mode;
        }

        if (Enum.TryParse(kcmbTarget.Text, out BlinkTarget target))
        {
            blink.BlinkTarget = target;
        }

        if (Enum.TryParse(kcmbVisibilityStyle.Text, out VisibilityStyle style))
        {
            blink.VisibilityStyle = style;
        }

        blink.BlinkInterval = (int)knudInterval.Value;
        blink.SoftBlinkCycleInterval = (int)knudSoftCycle.Value;
        blink.SoftBlinkTickInterval = (int)knudSoftTick.Value;
        blink.BlinkDuration = (int)knudDuration.Value;
        blink.MaxBlinkCount = (int)knudMaxCount.Value;

        blink.BlinkColorOne = kbtnColorOne.SelectedColor;
        blink.BlinkColorTwo = kbtnColorTwo.SelectedColor;
        blink.BlinkTextColor = kbtnTextColor.SelectedColor;

        blink.UseBlinkTextColor = kchkUseBlinkTextColor.Checked;
        blink.PauseOnMouseOver = kchkPauseOnMouseOver.Checked;
        blink.RestoreAppearanceOnStop = kchkRestoreOnStop.Checked;
        blink.BlinkOnlyWhenVisible = kchkBlinkOnlyWhenVisible.Checked;
    }

    private static decimal ClampNud(KryptonNumericUpDown nud, int value)
    {
        if (value < nud.Minimum)
        {
            return nud.Minimum;
        }

        if (value > nud.Maximum)
        {
            return nud.Maximum;
        }

        return value;
    }

    private void UpdateStatus(string message)
    {
        klblStatus.Values.Text = message;
        _normalLabel.Text = $@"Normal label | {message}";
    }
}
