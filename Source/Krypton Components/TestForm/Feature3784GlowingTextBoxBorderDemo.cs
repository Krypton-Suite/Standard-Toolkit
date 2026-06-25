#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit;

namespace TestForm;

/// <summary>
/// Comprehensive manual demo for GitHub issue #3784: optional glowing borders and cue hint shimmer.
/// </summary>
public partial class Feature3784GlowingTextBoxBorderDemo : KryptonForm
{
    private bool _syncingSettings;

    public Feature3784GlowingTextBoxBorderDemo()
    {
        InitializeComponent();
        WireEvents();
        ApplyFactoryDefaults();
        SyncSettingsFromTarget();
    }

    private void WireEvents()
    {
        kbtnApply.Click += (_, _) => ApplySettingsFromPanel();
        kbtnReset.Click += (_, _) =>
        {
            ApplyFactoryDefaults();
            SetStatus(@"Defaults restored.");
        };
        kbtnOpenFormGlow.Click += (_, _) => OpenFormGlowDemo();
        kcmbTarget.SelectedIndexChanged += (_, _) => SyncSettingsFromTarget();
        kchkCueAnimate.CheckedChanged += (_, _) => knudCueSpeed.Enabled = kchkCueAnimate.Checked;

        foreach (Control control in GetSampleControls())
        {
            control.GotFocus += (_, _) => SyncSettingsFromFocusedControl(control);
        }
    }

    private IEnumerable<Control> GetSampleControls() => new Control[]
    {
        ktxtAnimatedGlow,
        ktxtStaticGlow,
        kmtxtPhone,
        kcmbGlow,
        krtbGlow,
        knudQuantity,
        kdudPriority,
        kdtpDue,
        kcalcBudget,
        kbtnGlow
    };

    private IEnumerable<GlowTarget> GetGlowTargets()
    {
        yield return new GlowTarget("Hero TextBox", ktxtAnimatedGlow, () => ktxtAnimatedGlow.GlowingBorderValues, () => ktxtAnimatedGlow.CueHint);
        yield return new GlowTarget("Static TextBox", ktxtStaticGlow, () => ktxtStaticGlow.GlowingBorderValues, () => ktxtStaticGlow.CueHint);
        yield return new GlowTarget("MaskedTextBox", kmtxtPhone, () => kmtxtPhone.GlowingBorderValues, null);
        yield return new GlowTarget("ComboBox", kcmbGlow, () => kcmbGlow.GlowingBorderValues, () => kcmbGlow.CueHint);
        yield return new GlowTarget("RichTextBox", krtbGlow, () => krtbGlow.GlowingBorderValues, () => krtbGlow.CueHint);
        yield return new GlowTarget("NumericUpDown", knudQuantity, () => knudQuantity.GlowingBorderValues, null);
        yield return new GlowTarget("DomainUpDown", kdudPriority, () => kdudPriority.GlowingBorderValues, null);
        yield return new GlowTarget("DateTimePicker", kdtpDue, () => kdtpDue.GlowingBorderValues, null);
        yield return new GlowTarget("CalcInput", kcalcBudget, () => kcalcBudget.GlowingBorderValues, null);
        yield return new GlowTarget("Button", kbtnGlow, () => kbtnGlow.GlowingBorderValues, null);
    }

    private IEnumerable<GlowTarget> GetSelectedTargets()
    {
        if (kcmbTarget.SelectedIndex <= 0)
        {
            return GetGlowTargets().ToArray();
        }

        string name = kcmbTarget.SelectedItem?.ToString() ?? string.Empty;
        return GetGlowTargets().Where(t => t.Name == name);
    }

    private void ApplyFactoryDefaults()
    {
        ApplyGlowDefaults(ktxtAnimatedGlow.GlowingBorderValues, true, true, InputGlowingBorderShowWhen.Focused, InputGlowingBorderStyle.All, 1.5f);
        ApplyCueDefaults(ktxtAnimatedGlow.CueHint, true, 0.75f, "Describe the app or website or idea that you want to build");

        ApplyGlowDefaults(ktxtStaticGlow.GlowingBorderValues, true, false, InputGlowingBorderShowWhen.Focused, InputGlowingBorderStyle.Bottom, 1f);
        ApplyCueDefaults(ktxtStaticGlow.CueHint, false, 1f, "Static bottom glow while focused");

        ApplyGlowDefaults(kmtxtPhone.GlowingBorderValues, true, true, InputGlowingBorderShowWhen.Focused, InputGlowingBorderStyle.Bottom, 1.2f);

        ApplyGlowDefaults(kcmbGlow.GlowingBorderValues, true, true, InputGlowingBorderShowWhen.Focused, InputGlowingBorderStyle.Bottom, 0.5f);
        ApplyCueDefaults(kcmbGlow.CueHint, false, 1f, "Choose an option…");

        ApplyGlowDefaults(krtbGlow.GlowingBorderValues, true, true, InputGlowingBorderShowWhen.Focused, InputGlowingBorderStyle.Bottom, 1f);
        krtbGlow.Text = "Rich text with glowing border";

        ApplyGlowDefaults(knudQuantity.GlowingBorderValues, true, true, InputGlowingBorderShowWhen.Focused, InputGlowingBorderStyle.Bottom, 1f);
        knudQuantity.Value = 3;

        ApplyGlowDefaults(kdudPriority.GlowingBorderValues, true, true, InputGlowingBorderShowWhen.Focused, InputGlowingBorderStyle.All, 1f);
        kdudPriority.SelectedIndex = 1;

        ApplyGlowDefaults(kdtpDue.GlowingBorderValues, true, true, InputGlowingBorderShowWhen.Focused, InputGlowingBorderStyle.Bottom, 0.8f);

        ApplyGlowDefaults(kcalcBudget.GlowingBorderValues, true, true, InputGlowingBorderShowWhen.Focused, InputGlowingBorderStyle.Bottom, 1f);
        kcalcBudget.Value = 1250.50m;

        ApplyGlowDefaults(kbtnGlow.GlowingBorderValues, true, true, InputGlowingBorderShowWhen.Active, InputGlowingBorderStyle.All, 1f);

        SyncSettingsFromTarget();
    }

    private static void ApplyGlowDefaults(InputGlowingBorderValues values,
        bool enable,
        bool animate,
        InputGlowingBorderShowWhen showWhen,
        InputGlowingBorderStyle style,
        float speed)
    {
        values.Enable = enable;
        values.Animate = animate;
        values.ShowWhen = showWhen;
        values.Style = style;
        values.AnimationSpeed = speed;
    }

    private static void ApplyCueDefaults(PaletteCueHintText cue, bool animate, float speed, string text)
    {
        cue.CueHintText = text;
        cue.Animate = animate;
        cue.AnimationSpeed = speed;
    }

    private void SyncSettingsFromFocusedControl(Control control)
    {
        GlowTarget? target = GetGlowTargets().FirstOrDefault(t => ReferenceEquals(t.Control, control));
        if (target == null)
        {
            return;
        }

        int index = kcmbTarget.Items.IndexOf(target.Name);
        if (index >= 0 && kcmbTarget.SelectedIndex != index)
        {
            kcmbTarget.SelectedIndex = index;
        }
        else
        {
            SyncSettingsFromTarget();
        }
    }

    private void SyncSettingsFromTarget()
    {
        GlowTarget target = kcmbTarget.SelectedIndex <= 0
            ? GetGlowTargets().First()
            : GetSelectedTargets().First();

        InputGlowingBorderValues values = target.GetGlow();

        _syncingSettings = true;
        try
        {
            kchkEnable.Checked = values.Enable;
            kchkAnimate.Checked = values.Animate;
            kcmbShowWhen.SelectedIndex = (int)values.ShowWhen;
            kcmbStyle.SelectedIndex = values.Style == InputGlowingBorderStyle.All ? 1 : 0;
            knudAnimationSpeed.Value = (decimal)values.AnimationSpeed;

            PaletteCueHintText? cue = target.GetCueHint?.Invoke();
            if (cue != null)
            {
                kchkCueAnimate.Enabled = true;
                knudCueSpeed.Enabled = cue.Animate;
                kchkCueAnimate.Checked = cue.Animate;
                knudCueSpeed.Value = (decimal)cue.AnimationSpeed;
            }
            else
            {
                kchkCueAnimate.Enabled = false;
                knudCueSpeed.Enabled = false;
                kchkCueAnimate.Checked = false;
            }
        }
        finally
        {
            _syncingSettings = false;
        }
    }

    private void ApplySettingsFromPanel()
    {
        if (_syncingSettings)
        {
            return;
        }

        var showWhen = (InputGlowingBorderShowWhen)kcmbShowWhen.SelectedIndex;
        var style = kcmbStyle.SelectedIndex == 1 ? InputGlowingBorderStyle.All : InputGlowingBorderStyle.Bottom;
        float speed = (float)knudAnimationSpeed.Value;
        float cueSpeed = (float)knudCueSpeed.Value;

        foreach (GlowTarget target in GetSelectedTargets())
        {
            InputGlowingBorderValues glow = target.GetGlow();
            glow.Enable = kchkEnable.Checked;
            glow.Animate = kchkAnimate.Checked;
            glow.ShowWhen = showWhen;
            glow.Style = style;
            glow.AnimationSpeed = speed;

            PaletteCueHintText? cue = target.GetCueHint?.Invoke();
            if (cue != null)
            {
                cue.Animate = kchkCueAnimate.Checked;
                cue.AnimationSpeed = cueSpeed;
            }
        }

        string targetName = kcmbTarget.SelectedIndex <= 0 ? "all controls" : kcmbTarget.Text;
        SetStatus($@"Applied glow settings to {targetName}.");
    }

    private void OpenFormGlowDemo()
    {
        var form = new KryptonForm
        {
            Text = @"KryptonForm — glowing chrome",
            StartPosition = FormStartPosition.CenterParent,
            ClientSize = new Size(520, 220),
            MinimumSize = new Size(400, 180)
        };
        form.GlowingBorderValues.Enable = true;
        form.GlowingBorderValues.Animate = true;
        form.GlowingBorderValues.ShowWhen = InputGlowingBorderShowWhen.Active;
        form.GlowingBorderValues.Style = InputGlowingBorderStyle.All;
        form.GlowingBorderValues.AnimationSpeed = 1f;

        var panel = new KryptonPanel { Dock = DockStyle.Fill, Padding = new Padding(16) };
        var label = new KryptonWrapLabel
        {
            Dock = DockStyle.Fill,
            Text =
                @"This child KryptonForm uses GlowingBorderValues on the form chrome. " +
                @"ShowWhen is Active — glow tracks the foreground window. " +
                @"Compare with the input samples on the parent demo."
        };
        panel.Controls.Add(label);
        form.Controls.Add(panel);
        form.Show(this);
        SetStatus(@"Opened KryptonForm glow demo.");
    }

    private void SetStatus(string message) => kwlblStatus.Text = message;

    private sealed class GlowTarget
    {
        public GlowTarget(string name,
            Control control,
            Func<InputGlowingBorderValues> getGlow,
            Func<PaletteCueHintText>? getCueHint)
        {
            Name = name;
            Control = control;
            GetGlow = getGlow;
            GetCueHint = getCueHint;
        }

        public string Name { get; }
        public Control Control { get; }
        public Func<InputGlowingBorderValues> GetGlow { get; }
        public Func<PaletteCueHintText>? GetCueHint { get; }
    }
}
