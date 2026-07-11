namespace TestForm;

public partial class ToggleSwitchTest : KryptonForm
{
    public ToggleSwitchTest()
    {
        InitializeComponent();
        InitializeKnobStylePreviews();
    }

    private void ToggleSwitchTest_Load(object sender, EventArgs e)
    {
        UpdateStatusText();
    }

    private void ktsTest_CheckedChanged(object sender, EventArgs e)
    {
        UpdateStatusText();
    }

    private void InitializeKnobStylePreviews()
    {
        kryptonPanel4.Controls.Clear();

        KryptonWrapLabel instructionsLabel = new KryptonWrapLabel
        {
            AutoSize = false,
            Dock = DockStyle.Top,
            Height = 72,
            LabelStyle = LabelStyle.NormalPanel,
            Text = "Issue #3890: compare knob styles. Set ToggleSwitchValues.OnColor/OffColor (default red/green) — with OnlyShowColorOnKnob=true they apply to the knob even when UseThemeColors is enabled. Enable EnableKnobGradient or choose the Gradient style for gradient fills. Enable EnableKnobPulse on the selected switch to preview the optional pulsing knob animation.",
            TextAlign = ContentAlignment.MiddleLeft
        };

        FlowLayoutPanel previewPanel = new FlowLayoutPanel
        {
            AutoScroll = true,
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            WrapContents = true
        };

        kryptonPanel4.Controls.Add(previewPanel);
        kryptonPanel4.Controls.Add(instructionsLabel);

        foreach (ToggleSwitchKnobStyle style in Enum.GetValues(typeof(ToggleSwitchKnobStyle)))
        {
            previewPanel.Controls.Add(CreateKnobStylePreview(style));
        }

        kryptonPropertyGrid1.SelectedObject = ktsTest;
    }

    private Control CreateKnobStylePreview(ToggleSwitchKnobStyle style)
    {
        KryptonPanel previewContainer = new KryptonPanel
        {
            Margin = new Padding(8),
            Padding = new Padding(8),
            Size = new Size(172, 112)
        };

        KryptonWrapLabel styleLabel = new KryptonWrapLabel
        {
            AutoSize = false,
            Dock = DockStyle.Top,
            Height = 32,
            LabelStyle = LabelStyle.BoldControl,
            Text = style.ToString(),
            TextAlign = ContentAlignment.MiddleCenter
        };

        KryptonToggleSwitch toggleSwitch = style == ToggleSwitchKnobStyle.Classic ? ktsTest : new KryptonToggleSwitch();
        toggleSwitch.Anchor = AnchorStyles.None;
        toggleSwitch.Location = new Point(16, 52);
        toggleSwitch.Size = new Size(140, 44);
        toggleSwitch.ToggleSwitchValues.KnobStyle = style;
        toggleSwitch.ToggleSwitchValues.OnlyShowColorOnKnob = true;
        toggleSwitch.ToggleSwitchValues.EnableKnobGradient = style == ToggleSwitchKnobStyle.Gradient;
        toggleSwitch.ToggleSwitchValues.AnimateGradientEffect = style == ToggleSwitchKnobStyle.Gradient;
        toggleSwitch.ToggleSwitchValues.EnableKnobPulse = style == ToggleSwitchKnobStyle.Classic;

        previewContainer.Controls.Add(toggleSwitch);
        previewContainer.Controls.Add(styleLabel);

        return previewContainer;
    }

    private void UpdateStatusText()
    {
        kryptonWrapLabel1.Text = $@"Selected switch: Checked = {ktsTest.ToggleSwitchValues.Checked}, KnobStyle = {ktsTest.ToggleSwitchValues.KnobStyle}, EnableKnobPulse = {ktsTest.ToggleSwitchValues.EnableKnobPulse}";
    }
}