#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive demonstration of KryptonHScrollBar and KryptonVScrollBar controls.
/// </summary>
public partial class ScrollBarTest : KryptonForm
{
    #region Instance Fields

    private readonly Timer _animationTimer;
    private bool _isAnimating;
    private int _animationTarget;
    private int _animationStart;
    private int _animationStep;

    #endregion

    #region Identity

    public ScrollBarTest()
    {
        InitializeComponent();

        // Initialize animation timer
        _animationTimer = new Timer { Interval = 16 }; // ~60 FPS
        _animationTimer.Tick += AnimationTimer_Tick;

        InitializeDemo();
    }

    #endregion

    #region Implementation

    private void InitializeDemo()
    {
        // Example 1: Basic horizontal scrollbar
        SetupBasicHorizontalScrollBar();

        // Example 2: Basic vertical scrollbar
        SetupBasicVerticalScrollBar();

        // Example 3: Scrolling content panel
        SetupScrollingContentPanel();

        // Example 4: Synchronized scrollbars
        SetupSynchronizedScrollbars();

        // Example 5: Themed scrollbars
        SetupThemedScrollbars();

        // Example 6: Programmatic control
        SetupProgrammaticControl();

        // Example 7: Event logging
        SetupEventLogging();
    }

    private void SetupBasicHorizontalScrollBar()
    {
        khsbBasic.Minimum = 0;
        khsbBasic.Maximum = 100;
        khsbBasic.SmallChange = 1;
        khsbBasic.LargeChange = 10;
        khsbBasic.Value = 0;

        khsbBasic.Scroll += (sender, e) =>
        {
            klblBasicValue.Text = $"Value: {e.NewValue}";
            UpdateBasicScrollInfo(e);
        };
    }

    private void SetupBasicVerticalScrollBar()
    {
        kvsbBasic.Minimum = 0;
        kvsbBasic.Maximum = 100;
        kvsbBasic.SmallChange = 1;
        kvsbBasic.LargeChange = 10;
        kvsbBasic.Value = 0;

        kvsbBasic.Scroll += (sender, e) =>
        {
            klblBasicVValue.Text = $"Value: {e.NewValue}";
            UpdateBasicVScrollInfo(e);
        };
    }

    private void SetupScrollingContentPanel()
    {
        // Set up content panel dimensions
        int contentWidth = 800;
        int contentHeight = 600;
        int visibleWidth = kpnlScrollContent.Width;
        int visibleHeight = kpnlScrollContent.Height;

        // Configure horizontal scrollbar
        khsbContent.Minimum = 0;
        khsbContent.Maximum = Math.Max(0, contentWidth - visibleWidth);
        khsbContent.LargeChange = visibleWidth;
        khsbContent.SmallChange = 20;

        // Configure vertical scrollbar
        kvsbContent.Minimum = 0;
        kvsbContent.Maximum = Math.Max(0, contentHeight - visibleHeight);
        kvsbContent.LargeChange = visibleHeight;
        kvsbContent.SmallChange = 20;

        // Handle horizontal scrolling
        khsbContent.Scroll += (sender, e) =>
        {
            kpnlScrollContent.Left = -e.NewValue;
            klblContentHValue.Text = $"H: {e.NewValue}";
        };

        // Handle vertical scrolling
        kvsbContent.Scroll += (sender, e) =>
        {
            kpnlScrollContent.Top = -e.NewValue;
            klblContentVValue.Text = $"V: {e.NewValue}";
        };

        // Create content
        CreateScrollContent();
    }

    private void CreateScrollContent()
    {
        // Add some visual content to the scrollable panel
        for (int i = 0; i < 20; i++)
        {
            var label = new KryptonLabel
            {
                Text = $"Content Item {i + 1}",
                Location = new Point(10 + (i % 4) * 200, 10 + (i / 4) * 50),
                Size = new Size(180, 40)
            };
            kpnlScrollContent.Controls.Add(label);
        }
    }

    private void SetupSynchronizedScrollbars()
    {
        // Synchronize horizontal scrollbars
        khsbSync1.Minimum = 0;
        khsbSync1.Maximum = 1000;
        khsbSync1.LargeChange = 100;
        khsbSync1.SmallChange = 10;

        khsbSync2.Minimum = 0;
        khsbSync2.Maximum = 1000;
        khsbSync2.LargeChange = 100;
        khsbSync2.SmallChange = 10;

        // Synchronize values
        khsbSync1.Scroll += (sender, e) =>
        {
            if (khsbSync2.Value != e.NewValue)
            {
                khsbSync2.Value = e.NewValue;
            }
            klblSyncValue.Text = $"Synchronized Value: {e.NewValue}";
        };

        khsbSync2.Scroll += (sender, e) =>
        {
            if (khsbSync1.Value != e.NewValue)
            {
                khsbSync1.Value = e.NewValue;
            }
            klblSyncValue.Text = $"Synchronized Value: {e.NewValue}";
        };
    }

    private void SetupThemedScrollbars()
    {
        // Custom theme for horizontal scrollbar
        khsbThemed.StateCommon.Back.Color1 = Color.FromArgb(245, 245, 245);
        khsbThemed.StateCommon.Border.Color1 = Color.FromArgb(180, 180, 180);

        khsbThemed.StateActive.Back.Color1 = Color.FromArgb(230, 240, 255);
        khsbThemed.StateActive.Border.Color1 = Color.FromArgb(0, 120, 215);

        khsbThemed.BorderColor = Color.FromArgb(0, 120, 215);
        khsbThemed.DisabledBorderColor = Color.Gray;

        // Custom theme for vertical scrollbar
        kvsbThemed.StateCommon.Back.Color1 = Color.FromArgb(245, 245, 245);
        kvsbThemed.StateCommon.Border.Color1 = Color.FromArgb(180, 180, 180);

        kvsbThemed.StateActive.Back.Color1 = Color.FromArgb(230, 240, 255);
        kvsbThemed.StateActive.Border.Color1 = Color.FromArgb(0, 120, 215);

        kvsbThemed.BorderColor = Color.FromArgb(0, 120, 215);
        kvsbThemed.DisabledBorderColor = Color.Gray;

        // Configure values
        khsbThemed.Minimum = 0;
        khsbThemed.Maximum = 100;
        khsbThemed.Value = 50;

        kvsbThemed.Minimum = 0;
        kvsbThemed.Maximum = 100;
        kvsbThemed.Value = 50;
    }

    private void SetupProgrammaticControl()
    {
        // Configure scrollbar
        khsbProgrammatic.Minimum = 0;
        khsbProgrammatic.Maximum = 1000;
        khsbProgrammatic.LargeChange = 100;
        khsbProgrammatic.SmallChange = 10;
        khsbProgrammatic.Value = 0;

        // Update label
        UpdateProgrammaticLabel();
    }

    private void SetupEventLogging()
    {
        // Log all scroll events
        khsbEventLog.Scroll += (sender, e) =>
        {
            string eventInfo = $"[{DateTime.Now:HH:mm:ss.fff}] Type: {e.Type}, Old: {e.OldValue}, New: {e.NewValue}";
            ktxtEventLog.AppendText(eventInfo + Environment.NewLine);
            ktxtEventLog.SelectionStart = ktxtEventLog.Text.Length;
            ktxtEventLog.ScrollToCaret();
        };

        khsbEventLog.Minimum = 0;
        khsbEventLog.Maximum = 100;
        khsbEventLog.SmallChange = 1;
        khsbEventLog.LargeChange = 10;
    }

    private void UpdateBasicScrollInfo(ScrollEventArgs e)
    {
        string info = $"Type: {e.Type}";
        if (e.OldValue >= 0)
        {
            info += $", Old: {e.OldValue}";
        }
        klblBasicInfo.Text = info;
    }

    private void UpdateBasicVScrollInfo(ScrollEventArgs e)
    {
        string info = $"Type: {e.Type}";
        if (e.OldValue >= 0)
        {
            info += $", Old: {e.OldValue}";
        }
        klblBasicVInfo.Text = info;
    }

    private void UpdateProgrammaticLabel()
    {
        klblProgrammaticValue.Text = $"Value: {khsbProgrammatic.Value} / {khsbProgrammatic.Maximum}";
    }

    #endregion

    #region Event Handlers

    private void kbtnSetValue_Click(object sender, EventArgs e)
    {
        if (int.TryParse(ktxtValue.Text, out int value))
        {
            khsbProgrammatic.BeginUpdate();
            try
            {
                khsbProgrammatic.Value = Math.Max(khsbProgrammatic.Minimum,
                    Math.Min(khsbProgrammatic.Maximum, value));
            }
            finally
            {
                khsbProgrammatic.EndUpdate();
            }
            UpdateProgrammaticLabel();
        }
    }

    private void kbtnSetRange_Click(object sender, EventArgs e)
    {
        if (int.TryParse(ktxtMinimum.Text, out int min) &&
            int.TryParse(ktxtMaximum.Text, out int max) &&
            min < max)
        {
            khsbProgrammatic.BeginUpdate();
            try
            {
                khsbProgrammatic.Minimum = min;
                khsbProgrammatic.Maximum = max;
                khsbProgrammatic.Value = Math.Max(min, Math.Min(max, khsbProgrammatic.Value));
            }
            finally
            {
                khsbProgrammatic.EndUpdate();
            }
            UpdateProgrammaticLabel();
        }
    }

    private void kbtnAnimate_Click(object sender, EventArgs e)
    {
        if (_isAnimating)
        {
            _animationTimer.Stop();
            _isAnimating = false;
            kbtnAnimate.Text = "Animate to 500";
        }
        else
        {
            _animationStart = khsbProgrammatic.Value;
            _animationTarget = 500;
            _animationStep = _animationTarget > _animationStart ? 5 : -5;
            _isAnimating = true;
            _animationTimer.Start();
            kbtnAnimate.Text = "Stop Animation";
        }
    }

    private void AnimationTimer_Tick(object? sender, EventArgs e)
    {
        int currentValue = khsbProgrammatic.Value;

        if ((_animationStep > 0 && currentValue >= _animationTarget) ||
            (_animationStep < 0 && currentValue <= _animationTarget))
        {
            khsbProgrammatic.Value = _animationTarget;
            _animationTimer.Stop();
            _isAnimating = false;
            kbtnAnimate.Text = "Animate to 500";
            UpdateProgrammaticLabel();
        }
        else
        {
            khsbProgrammatic.Value = currentValue + _animationStep;
            UpdateProgrammaticLabel();
        }
    }

    private void kbtnClearLog_Click(object sender, EventArgs e)
    {
        ktxtEventLog.Clear();
    }

    private void kchkEnableThemed_CheckedChanged(object sender, EventArgs e)
    {
        khsbThemed.Enabled = kchkEnableThemed.Checked;
        kvsbThemed.Enabled = kchkEnableThemed.Checked;
    }

    private void kbtnResetContent_Click(object sender, EventArgs e)
    {
        khsbContent.Value = 0;
        kvsbContent.Value = 0;
        kpnlScrollContent.Location = new Point(0, 0);
    }

    private void ScrollBarTest_Resize(object sender, EventArgs e)
    {
        // Update scrollbar maximums when form resizes
        if (kpnlScrollContent != null && khsbContent != null && kvsbContent != null)
        {
            int contentWidth = 800;
            int contentHeight = 600;
            int visibleWidth = kpnlScrollContent.Width;
            int visibleHeight = kpnlScrollContent.Height;

            khsbContent.Maximum = Math.Max(0, contentWidth - visibleWidth);
            khsbContent.LargeChange = visibleWidth;

            kvsbContent.Maximum = Math.Max(0, contentHeight - visibleHeight);
            kvsbContent.LargeChange = visibleHeight;
        }
    }

    #endregion

    private void khsbProgrammatic_Scroll(object sender, ScrollEventArgs e)
    {
        SetupSynchronizedScrollbars();
    }
}
