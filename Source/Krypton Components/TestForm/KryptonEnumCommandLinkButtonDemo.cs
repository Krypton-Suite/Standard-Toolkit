#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive demonstration of the <see cref="Krypton.Toolkit.Utilities.KryptonEnumCommandLinkButton"/>
/// control (issue #3838). The command-link variant renders each enum value as a bold heading plus a
/// descriptive sub-text and cycles through the values on click.
/// </summary>
/// <remarks>
/// The demo exercises: attribute-driven text (heading = field name, description = <see cref="DescriptionAttribute"/>),
/// provider-driven text (<c>HeadingProvider</c> / <c>DescriptionProvider</c>), per-value icons
/// (<c>ImageProvider</c>), wrap/clamp, humanised names, sort order, excluded values, reverse-on-right-click,
/// keyboard and mouse-wheel cycling, a cancelable <c>SelectedValueChanging</c> veto, data binding of
/// <c>SelectedValue</c>, programmatic cycling, and a synced equivalent radio-button group.
/// </remarks>
public partial class KryptonEnumCommandLinkButtonDemo : KryptonForm
{
    #region Sample Enums

    /// <summary>Attribute-driven enum: the field name becomes the heading, the description the sub-text.</summary>
    public enum UpdateChannel
    {
        [Description("Fully tested builds. Recommended for everyone.")]
        Stable,

        [Description("Early access with new features that are mostly stable.")]
        Beta,

        [Description("Daily builds. Expect rough edges and occasional breakage.")]
        Nightly,

        [Description("Bleeding-edge experimental builds for testing only.")]
        Canary
    }

    /// <summary>Provider-driven enum: no attributes, so the demo supplies heading/description in code.</summary>
    public enum ConnectionSpeed
    {
        VeryFast,
        Fast,
        Moderate,
        Slow
    }

    #endregion

    #region Instance Fields

    private readonly Dictionary<UpdateChannel, Image> _channelImages;
    private readonly Dictionary<ConnectionSpeed, Image> _speedImages;
    private bool _syncingRadios;

    #endregion

    public KryptonEnumCommandLinkButtonDemo()
    {
        InitializeComponent();

        _channelImages = new Dictionary<UpdateChannel, Image>
        {
            [UpdateChannel.Stable] = CreateDot(Color.ForestGreen),
            [UpdateChannel.Beta] = CreateDot(Color.SteelBlue),
            [UpdateChannel.Nightly] = CreateDot(Color.MediumPurple),
            [UpdateChannel.Canary] = CreateDot(Color.Orange)
        };

        _speedImages = new Dictionary<ConnectionSpeed, Image>
        {
            [ConnectionSpeed.VeryFast] = CreateDot(Color.ForestGreen),
            [ConnectionSpeed.Fast] = CreateDot(Color.YellowGreen),
            [ConnectionSpeed.Moderate] = CreateDot(Color.Orange),
            [ConnectionSpeed.Slow] = CreateDot(Color.Firebrick)
        };

        SetupDemo();
    }

    private void SetupDemo()
    {
        SetupPrimaryButton();
        SetupSecondaryButton();

        // Sort-order combo (applies to the primary button).
        kcmbSortOrder.Items.Add(Krypton.Toolkit.Utilities.EnumButtonSortOrder.Declaration);
        kcmbSortOrder.Items.Add(Krypton.Toolkit.Utilities.EnumButtonSortOrder.Value);
        kcmbSortOrder.Items.Add(Krypton.Toolkit.Utilities.EnumButtonSortOrder.Alphabetical);
        kcmbSortOrder.SelectedItem = Krypton.Toolkit.Utilities.EnumButtonSortOrder.Declaration;

        // One-way data binding: the label follows the primary SelectedValue via SelectedValueChanged.
        klblBoundValue.DataBindings.Add(@"Text", kclbPrimary, nameof(Krypton.Toolkit.Utilities.KryptonEnumCommandLinkButton.SelectedValue), true, DataSourceUpdateMode.Never);

        WireOptionHandlers();
        WireRadioSync();
        WireProgrammaticButtons();

        UpdateReadouts();
        SyncRadios();

        AddLog(@"KryptonEnumCommandLinkButton demo initialised.");
        AddLog(@"Click a command link to cycle forwards; arrow keys and the mouse wheel also cycle.");
        AddLog($@"Primary initial value: {kclbPrimary.SelectedValue} - heading ""{kclbPrimary.SelectedHeadingText}""");
        AddLog($@"Secondary (providers) initial value: {kclbSecondary.SelectedValue} - heading ""{kclbSecondary.SelectedHeadingText}""");
    }

    private void SetupPrimaryButton()
    {
        // Attribute-driven: heading = field name, description = [Description]; plus a per-value icon.
        kclbPrimary.EnumType = typeof(UpdateChannel);
        kclbPrimary.ImageProvider = value => _channelImages[(UpdateChannel)value];

        kclbPrimary.SelectedValueChanging += (_, e) =>
        {
            if (kchkVetoNightly.Checked && e.ProposedValue is UpdateChannel.Nightly)
            {
                e.Cancel = true;
                AddLog(@"Vetoed change to 'Nightly' (SelectedValueChanging).");
            }
        };

        kclbPrimary.EnumValueChanged += (_, e) =>
        {
            AddLog($@"Primary -> {e.Value} (heading ""{e.DisplayText}"")");
            UpdateReadouts();
        };

        kclbPrimary.SelectedValueChanged += (_, _) => SyncRadios();
    }

    private void SetupSecondaryButton()
    {
        // Provider-driven: no [Description] on ConnectionSpeed, so supply heading/description/image in code.
        kclbSecondary.EnumType = typeof(ConnectionSpeed);

        kclbSecondary.HeadingProvider = value => (ConnectionSpeed)value switch
        {
            ConnectionSpeed.VeryFast => @"Very fast connection",
            ConnectionSpeed.Fast => @"Fast connection",
            ConnectionSpeed.Moderate => @"Moderate connection",
            ConnectionSpeed.Slow => @"Slow connection",
            _ => value.ToString() ?? string.Empty
        };

        kclbSecondary.DescriptionProvider = value => (ConnectionSpeed)value switch
        {
            ConnectionSpeed.VeryFast => @"Downloads complete almost instantly (100 Mbps+).",
            ConnectionSpeed.Fast => @"Comfortable for streaming and large downloads (25-100 Mbps).",
            ConnectionSpeed.Moderate => @"Fine for browsing; larger downloads take a while (5-25 Mbps).",
            ConnectionSpeed.Slow => @"Basic browsing only; downloads are slow (< 5 Mbps).",
            _ => string.Empty
        };

        kclbSecondary.ImageProvider = value => _speedImages[(ConnectionSpeed)value];

        kclbSecondary.EnumValueChanged += (_, e) => AddLog($@"Secondary -> {e.Value} (heading ""{e.DisplayText}"")");
    }

    private void WireOptionHandlers()
    {
        kchkWrapAround.CheckedChanged += (_, _) =>
        {
            kclbPrimary.WrapAround = kchkWrapAround.Checked;
            AddLog($@"Primary WrapAround = {kchkWrapAround.Checked}");
        };

        kchkUseDescription.CheckedChanged += (_, _) =>
        {
            kclbPrimary.UseDescriptionAttribute = kchkUseDescription.Checked;
            UpdateReadouts();
            AddLog($@"Primary UseDescriptionAttribute = {kchkUseDescription.Checked}");
        };

        kchkHumanize.CheckedChanged += (_, _) =>
        {
            kclbPrimary.HumanizeNames = kchkHumanize.Checked;
            UpdateReadouts();
            AddLog($@"Primary HumanizeNames = {kchkHumanize.Checked}");
        };

        kchkReverseRightClick.CheckedChanged += (_, _) =>
        {
            kclbPrimary.ReverseOnRightClick = kchkReverseRightClick.Checked;
            kclbSecondary.ReverseOnRightClick = kchkReverseRightClick.Checked;
            AddLog($@"ReverseOnRightClick = {kchkReverseRightClick.Checked} (both buttons)");
        };

        kchkKeyboard.CheckedChanged += (_, _) =>
        {
            kclbPrimary.AllowKeyboardCycling = kchkKeyboard.Checked;
            kclbSecondary.AllowKeyboardCycling = kchkKeyboard.Checked;
            AddLog($@"AllowKeyboardCycling = {kchkKeyboard.Checked} (both buttons)");
        };

        kchkMouseWheel.CheckedChanged += (_, _) =>
        {
            kclbPrimary.AllowMouseWheelCycling = kchkMouseWheel.Checked;
            kclbSecondary.AllowMouseWheelCycling = kchkMouseWheel.Checked;
            AddLog($@"AllowMouseWheelCycling = {kchkMouseWheel.Checked} (both buttons)");
        };

        kchkExcludeCanary.CheckedChanged += (_, _) =>
        {
            kclbPrimary.ExcludedValues = kchkExcludeCanary.Checked
                ? new object[] { UpdateChannel.Canary }
                : null;
            krbCanary.Enabled = !kchkExcludeCanary.Checked;
            UpdateReadouts();
            SyncRadios();
            AddLog($@"Exclude 'Canary' = {kchkExcludeCanary.Checked}");
        };

        kcmbSortOrder.SelectedIndexChanged += (_, _) =>
        {
            if (kcmbSortOrder.SelectedItem is Krypton.Toolkit.Utilities.EnumButtonSortOrder sortOrder)
            {
                kclbPrimary.SortOrder = sortOrder;
                UpdateReadouts();
                AddLog($@"Primary SortOrder = {sortOrder}");
            }
        };
    }

    private void WireRadioSync()
    {
        krbStable.CheckedChanged += (_, _) => OnRadioChecked(krbStable, UpdateChannel.Stable);
        krbBeta.CheckedChanged += (_, _) => OnRadioChecked(krbBeta, UpdateChannel.Beta);
        krbNightly.CheckedChanged += (_, _) => OnRadioChecked(krbNightly, UpdateChannel.Nightly);
        krbCanary.CheckedChanged += (_, _) => OnRadioChecked(krbCanary, UpdateChannel.Canary);
    }

    private void WireProgrammaticButtons()
    {
        kbtnPrev.Click += (_, _) => kclbPrimary.CyclePrevious();
        kbtnNext.Click += (_, _) => kclbPrimary.CycleNext();
        kbtnSelectNightly.Click += (_, _) => kclbPrimary.SetSelectedValue(UpdateChannel.Nightly);
        kbtnReset.Click += (_, _) => kclbPrimary.SetSelectedValue(UpdateChannel.Stable);
        kbtnClearLog.Click += (_, _) => ktxtLog.Text = string.Empty;
    }

    private void OnRadioChecked(KryptonRadioButton radio, UpdateChannel value)
    {
        if (_syncingRadios || !radio.Checked)
        {
            return;
        }

        kclbPrimary.SelectedValue = value;
    }

    private void SyncRadios()
    {
        // Guard against re-entrancy: setting Checked raises CheckedChanged which would set the value again.
        _syncingRadios = true;
        try
        {
            switch (kclbPrimary.GetSelectedValue<UpdateChannel>())
            {
                case UpdateChannel.Stable:
                    krbStable.Checked = true;
                    break;
                case UpdateChannel.Beta:
                    krbBeta.Checked = true;
                    break;
                case UpdateChannel.Nightly:
                    krbNightly.Checked = true;
                    break;
                case UpdateChannel.Canary:
                    krbCanary.Checked = true;
                    break;
            }
        }
        finally
        {
            _syncingRadios = false;
        }
    }

    private void UpdateReadouts()
    {
        klblHeadingValue.Text = kclbPrimary.SelectedHeadingText;
        klblDescValue.Text = kclbPrimary.SelectedDescriptionText;
    }

    private static Image CreateDot(Color color, int size = 28)
    {
        var bitmap = new Bitmap(size, size);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        graphics.Clear(Color.Transparent);

        var inset = 3;
        var diameter = size - (inset * 2) - 1;

        using var brush = new SolidBrush(color);
        graphics.FillEllipse(brush, inset, inset, diameter, diameter);

        using var pen = new Pen(Color.FromArgb(96, Color.Black));
        graphics.DrawEllipse(pen, inset, inset, diameter, diameter);

        return bitmap;
    }

    private void AddLog(string message)
    {
        if (InvokeRequired)
        {
            Invoke(new Action(() => AddLog(message)));
            return;
        }

        var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
        ktxtLog.AppendText($"[{timestamp}] {message}{Environment.NewLine}");
        ktxtLog.SelectionStart = ktxtLog.Text.Length;
        ktxtLog.ScrollToCaret();
    }
}
