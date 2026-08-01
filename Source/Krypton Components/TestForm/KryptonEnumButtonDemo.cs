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
/// Comprehensive demonstration of the <see cref="Krypton.Toolkit.Utilities.KryptonEnumButton"/> and
/// <see cref="Krypton.Toolkit.Utilities.KryptonEnumCommandLinkButton"/> controls (issue #3838),
/// including cycling, wrap/clamp, description text, humanised names, sort order, keyboard / mouse-wheel
/// cycling, per-value images, a cancelable changing event, data binding, and a side-by-side comparison
/// with equivalent radio buttons.
/// </summary>
public partial class KryptonEnumButtonDemo : KryptonForm
{
    #region Sample Enums

    /// <summary>A plain enum with no <see cref="DescriptionAttribute"/> - the field names are shown.</summary>
    public enum TrafficLight
    {
        Red,
        Amber,
        Green
    }

    /// <summary>An enum decorated with <see cref="DescriptionAttribute"/> - the friendly text is shown.</summary>
    public enum PizzaSize
    {
        [Description("Small (9\")")]
        Small,

        [Description("Medium (11\")")]
        Medium,

        [Description("Large (13\")")]
        Large,

        [Description("Extra Large (15\")")]
        ExtraLarge
    }

    /// <summary>An enum used to demonstrate clamping when wrap-around is disabled.</summary>
    public enum PagerStep
    {
        [Description("First")]
        First,

        [Description("Previous")]
        Previous,

        [Description("Next")]
        Next,

        [Description("Last")]
        Last
    }

    /// <summary>An enum whose <see cref="DescriptionAttribute"/> feeds the command-link description line.</summary>
    public enum BackupMode
    {
        [Description("Backs up your files automatically on a schedule.")]
        Automatic,

        [Description("You choose exactly what and when to back up.")]
        Manual,

        [Description("No backups are taken. You are on your own!")]
        Disabled
    }

    #endregion

    #region Instance Fields

    private readonly Dictionary<TrafficLight, Image> _trafficImages;
    private readonly Dictionary<BackupMode, Image> _backupImages;
    private bool _syncingRadios;

    #endregion

    public KryptonEnumButtonDemo()
    {
        InitializeComponent();

        _trafficImages = new Dictionary<TrafficLight, Image>
        {
            [TrafficLight.Red] = CreateDot(Color.Firebrick),
            [TrafficLight.Amber] = CreateDot(Color.Orange),
            [TrafficLight.Green] = CreateDot(Color.ForestGreen)
        };

        _backupImages = new Dictionary<BackupMode, Image>
        {
            [BackupMode.Automatic] = CreateDot(Color.ForestGreen, 28),
            [BackupMode.Manual] = CreateDot(Color.Orange, 28),
            [BackupMode.Disabled] = CreateDot(Color.Firebrick, 28)
        };

        SetupDemo();
    }

    private void SetupDemo()
    {
        // Assign the enum types in code (the primary usage pattern).
        kbtnTrafficLight.EnumType = typeof(TrafficLight);
        kbtnPizzaSize.EnumType = typeof(PizzaSize);
        kbtnPager.EnumType = typeof(PagerStep);
        kbtnCommandLink.EnumType = typeof(BackupMode);

        // The pager demonstrates clamp (no wrap) behaviour.
        kbtnPager.WrapAround = false;

        // Per-value images: a coloured dot for each traffic-light value and each backup mode.
        kbtnTrafficLight.ImageProvider = value => _trafficImages[(TrafficLight)value];
        kbtnCommandLink.ImageProvider = value => _backupImages[(BackupMode)value];

        // Populate the sort-order combo (applied to the pizza button).
        kcmbSortOrder.Items.Add(Krypton.Toolkit.Utilities.EnumButtonSortOrder.Declaration);
        kcmbSortOrder.Items.Add(Krypton.Toolkit.Utilities.EnumButtonSortOrder.Value);
        kcmbSortOrder.Items.Add(Krypton.Toolkit.Utilities.EnumButtonSortOrder.Alphabetical);
        kcmbSortOrder.SelectedItem = Krypton.Toolkit.Utilities.EnumButtonSortOrder.Declaration;

        // One-way data binding: the label follows the traffic-light SelectedValue via SelectedValueChanged.
        klblBoundValue.DataBindings.Add(@"Text", kbtnTrafficLight, nameof(Krypton.Toolkit.Utilities.KryptonEnumButton.SelectedValue), true, DataSourceUpdateMode.Never);

        WireValueLogging();
        WireOptionHandlers();
        WireRadioSync();
        WireProgrammaticButtons();

        SyncRadios();

        AddLog(@"KryptonEnumButton demo initialised.");
        AddLog(@"Click to cycle forwards; arrow keys and the mouse wheel also cycle. Right-click cycles back when enabled.");
        AddLog($@"Traffic Light initial value: {kbtnTrafficLight.SelectedValue} (""{kbtnTrafficLight.SelectedDisplayText}"")");
        AddLog($@"Backup Mode (command link) initial value: {kbtnCommandLink.SelectedValue} (""{kbtnCommandLink.SelectedHeadingText}"")");
        AddLog(@"Tip: uncheck 'Use Description' and check 'Humanize names' to see PizzaSize.ExtraLarge become 'Extra Large'.");
    }

    private void WireValueLogging()
    {
        kbtnTrafficLight.EnumValueChanged += (_, e) => AddLog($@"Traffic Light -> {e.Value} (""{e.DisplayText}"")");
        kbtnPizzaSize.EnumValueChanged += (_, e) => AddLog($@"Pizza Size -> {e.Value} (""{e.DisplayText}"")");
        kbtnPager.EnumValueChanged += (_, e) => AddLog($@"Pager -> {e.Value} (""{e.DisplayText}"")");
        kbtnCommandLink.EnumValueChanged += (_, e) => AddLog($@"Backup Mode -> {e.Value} (""{e.DisplayText}"")");

        // Cancelable changing event: optionally veto selecting the "Disabled" backup mode.
        kbtnCommandLink.SelectedValueChanging += (_, e) =>
        {
            if (kchkVetoDisabled.Checked && e.ProposedValue is BackupMode.Disabled)
            {
                e.Cancel = true;
                AddLog(@"Vetoed change to Backup Mode 'Disabled' (SelectedValueChanging).");
            }
        };
    }

    private void WireOptionHandlers()
    {
        kchkWrapAround.CheckedChanged += (_, _) =>
        {
            kbtnTrafficLight.WrapAround = kchkWrapAround.Checked;
            kbtnPizzaSize.WrapAround = kchkWrapAround.Checked;
            AddLog($@"WrapAround = {kchkWrapAround.Checked} (Traffic Light and Pizza Size)");
        };

        kchkUseDescription.CheckedChanged += (_, _) =>
        {
            kbtnPizzaSize.UseDescriptionAttribute = kchkUseDescription.Checked;
            kbtnCommandLink.UseDescriptionAttribute = kchkUseDescription.Checked;
            AddLog($@"UseDescriptionAttribute = {kchkUseDescription.Checked} (Pizza size + Backup mode)");
        };

        kchkReverseRightClick.CheckedChanged += (_, _) =>
        {
            kbtnTrafficLight.ReverseOnRightClick = kchkReverseRightClick.Checked;
            kbtnPizzaSize.ReverseOnRightClick = kchkReverseRightClick.Checked;
            kbtnPager.ReverseOnRightClick = kchkReverseRightClick.Checked;
            kbtnCommandLink.ReverseOnRightClick = kchkReverseRightClick.Checked;
            AddLog($@"ReverseOnRightClick = {kchkReverseRightClick.Checked} (all buttons)");
        };

        kchkHumanize.CheckedChanged += (_, _) =>
        {
            kbtnTrafficLight.HumanizeNames = kchkHumanize.Checked;
            kbtnPizzaSize.HumanizeNames = kchkHumanize.Checked;
            AddLog($@"HumanizeNames = {kchkHumanize.Checked} (Traffic Light + Pizza size)");
        };

        kchkKeyboard.CheckedChanged += (_, _) =>
        {
            kbtnTrafficLight.AllowKeyboardCycling = kchkKeyboard.Checked;
            kbtnPizzaSize.AllowKeyboardCycling = kchkKeyboard.Checked;
            kbtnPager.AllowKeyboardCycling = kchkKeyboard.Checked;
            kbtnCommandLink.AllowKeyboardCycling = kchkKeyboard.Checked;
            AddLog($@"AllowKeyboardCycling = {kchkKeyboard.Checked} (all buttons)");
        };

        kchkMouseWheel.CheckedChanged += (_, _) =>
        {
            kbtnTrafficLight.AllowMouseWheelCycling = kchkMouseWheel.Checked;
            kbtnPizzaSize.AllowMouseWheelCycling = kchkMouseWheel.Checked;
            kbtnPager.AllowMouseWheelCycling = kchkMouseWheel.Checked;
            kbtnCommandLink.AllowMouseWheelCycling = kchkMouseWheel.Checked;
            AddLog($@"AllowMouseWheelCycling = {kchkMouseWheel.Checked} (all buttons)");
        };

        kcmbSortOrder.SelectedIndexChanged += (_, _) =>
        {
            if (kcmbSortOrder.SelectedItem is Krypton.Toolkit.Utilities.EnumButtonSortOrder sortOrder)
            {
                kbtnPizzaSize.SortOrder = sortOrder;
                AddLog($@"Pizza size SortOrder = {sortOrder}");
            }
        };
    }

    private void WireRadioSync()
    {
        // Two-way synchronisation between the enum button and the equivalent radio-button group.
        krbRed.CheckedChanged += (_, _) => OnRadioChecked(krbRed, TrafficLight.Red);
        krbAmber.CheckedChanged += (_, _) => OnRadioChecked(krbAmber, TrafficLight.Amber);
        krbGreen.CheckedChanged += (_, _) => OnRadioChecked(krbGreen, TrafficLight.Green);

        kbtnTrafficLight.SelectedValueChanged += (_, _) => SyncRadios();
    }

    private void WireProgrammaticButtons()
    {
        kbtnCycleNext.Click += (_, _) => kbtnTrafficLight.CycleNext();
        kbtnCyclePrevious.Click += (_, _) => kbtnTrafficLight.CyclePrevious();
        kbtnSelectGreen.Click += (_, _) => kbtnTrafficLight.SetSelectedValue(TrafficLight.Green);
        kbtnClearLog.Click += (_, _) => ktxtLog.Text = string.Empty;
    }

    private void OnRadioChecked(KryptonRadioButton radio, TrafficLight value)
    {
        if (_syncingRadios || !radio.Checked)
        {
            return;
        }

        kbtnTrafficLight.SelectedValue = value;
    }

    private void SyncRadios()
    {
        // Guard against re-entrancy: setting Checked raises CheckedChanged which would set the value again.
        _syncingRadios = true;
        try
        {
            switch (kbtnTrafficLight.GetSelectedValue<TrafficLight>())
            {
                case TrafficLight.Red:
                    krbRed.Checked = true;
                    break;
                case TrafficLight.Amber:
                    krbAmber.Checked = true;
                    break;
                case TrafficLight.Green:
                    krbGreen.Checked = true;
                    break;
            }
        }
        finally
        {
            _syncingRadios = false;
        }
    }

    private static Image CreateDot(Color color, int size = 16)
    {
        var bitmap = new Bitmap(size, size);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        graphics.Clear(Color.Transparent);

        var inset = size >= 24 ? 3 : 1;
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
