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
            Height = 88,
            LabelStyle = LabelStyle.NormalPanel,
            Text = "Issue #3890: compare knob styles. Chevron: adjust KnobChevronGlyphSize (0.2-1) and EnableKnobGradient for optional knob fill gradient. Set OnColor/OffColor for track/knob colours.",
            TextAlign = ContentAlignment.MiddleLeft
        };

        FlowLayoutPanel previewPanel = new FlowLayoutPanel
        {
            AutoScroll = true,
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            WrapContents = true,
            BackColor = Color.Transparent
        };

        kryptonPanel4.Controls.Add(previewPanel);
        kryptonPanel4.Controls.Add(instructionsLabel);
        previewPanel.Controls.Add(CreateOrientationPreview());

        foreach (ToggleSwitchKnobStyle style in Enum.GetValues(typeof(ToggleSwitchKnobStyle)))
        {
            previewPanel.Controls.Add(CreateKnobStylePreview(style));
        }

        kryptonPropertyGrid1.SelectedObject = ktsTest;
    }

    private Control CreateOrientationPreview()
    {
        KryptonPanel previewContainer = new KryptonPanel
        {
            Margin = new Padding(8),
            Padding = new Padding(8),
            Size = new Size(320, 280)
        };

        KryptonWrapLabel orientationLabel = new KryptonWrapLabel
        {
            AutoSize = false,
            Dock = DockStyle.Top,
            Height = 32,
            LabelStyle = LabelStyle.BoldControl,
            Text = "Orientation",
            TextAlign = ContentAlignment.MiddleCenter
        };

        TableLayoutPanel layoutPanel = new TableLayoutPanel
        {
            ColumnCount = 2,
            Dock = DockStyle.Fill,
            Padding = new Padding(4)
        };
        layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
        layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

        layoutPanel.Controls.Add(CreateOrientationSample("Horizontal", ToggleSwitchOrientation.Horizontal, new Size(140, 44)), 0, 0);
        layoutPanel.Controls.Add(CreateOrientationSample("Vertical", ToggleSwitchOrientation.Vertical, new Size(44, 140)), 1, 0);

        previewContainer.Controls.Add(layoutPanel);
        previewContainer.Controls.Add(orientationLabel);

        return previewContainer;
    }

    private Control CreateOrientationSample(string caption, ToggleSwitchOrientation orientation, Size size)
    {
        KryptonPanel samplePanel = new KryptonPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(8)
        };

        KryptonWrapLabel captionLabel = new KryptonWrapLabel
        {
            AutoSize = false,
            Dock = DockStyle.Top,
            Height = 24,
            LabelStyle = LabelStyle.NormalControl,
            Text = caption,
            TextAlign = ContentAlignment.MiddleCenter
        };

        KryptonToggleSwitch toggleSwitch = new KryptonToggleSwitch
        {
            Anchor = AnchorStyles.None,
            Location = new Point(48, 40),
            Size = size
        };
        toggleSwitch.ToggleSwitchValues.Orientation = orientation;
        toggleSwitch.ToggleSwitchValues.ShowText = true;
        toggleSwitch.ToggleSwitchValues.ShowTrackIcons = orientation == ToggleSwitchOrientation.Vertical;
        toggleSwitch.ToggleSwitchValues.KnobStyle = ToggleSwitchKnobStyle.Metallic;
        toggleSwitch.ToggleSwitchValues.OffColor = Color.FromArgb(255, 214, 88, 52);

        samplePanel.Controls.Add(toggleSwitch);
        samplePanel.Controls.Add(captionLabel);

        return samplePanel;
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
        toggleSwitch.ToggleSwitchValues.EnableKnobGradient = style == ToggleSwitchKnobStyle.Gradient
            || style == ToggleSwitchKnobStyle.Pill
            || style == ToggleSwitchKnobStyle.Chevron;
        toggleSwitch.ToggleSwitchValues.AnimateGradientEffect = style == ToggleSwitchKnobStyle.Gradient;
        toggleSwitch.ToggleSwitchValues.EnableKnobPulse = style == ToggleSwitchKnobStyle.Classic;
        toggleSwitch.ToggleSwitchValues.ShowText = style != ToggleSwitchKnobStyle.ThinTrack
            && style != ToggleSwitchKnobStyle.Metallic;
        toggleSwitch.ToggleSwitchValues.ShowTrackIcons = style == ToggleSwitchKnobStyle.Metallic;
        if (style == ToggleSwitchKnobStyle.Metallic)
        {
            toggleSwitch.ToggleSwitchValues.OffColor = Color.FromArgb(255, 214, 88, 52);
        }

        previewContainer.Controls.Add(toggleSwitch);
        previewContainer.Controls.Add(styleLabel);

        toggleSwitch.Click += (s, e) =>
        {
            kryptonPropertyGrid1.SelectedObject = toggleSwitch;
        };

        return previewContainer;
    }

    private void UpdateStatusText()
    {
        kryptonWrapLabel1.Text = $@"Selected switch: Checked = {ktsTest.ToggleSwitchValues.Checked}, Orientation = {ktsTest.ToggleSwitchValues.Orientation}, KnobStyle = {ktsTest.ToggleSwitchValues.KnobStyle}, EnableKnobPulse = {ktsTest.ToggleSwitchValues.EnableKnobPulse}";
    }

    private void kbtnCancel_Click(object sender, EventArgs e)
    {
        Hide();
    }
}