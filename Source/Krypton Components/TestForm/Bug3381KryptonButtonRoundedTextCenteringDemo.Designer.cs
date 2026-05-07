#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace TestForm
{
    partial class Bug3381KryptonButtonRoundedTextCenteringDemo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support — do not modify the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            _lblIntro = new KryptonWrapLabel();
            _root = new TableLayoutPanel();
            SuspendLayout();
            _root.SuspendLayout();
            //
            // _lblIntro
            //
            _lblIntro.Dock = DockStyle.Top;
            _lblIntro.AutoSize = false;
            _lblIntro.Padding = new Padding(0, 0, 0, 8);
            _lblIntro.Height = 120;
            _lblIntro.Text =
                @"GitHub issue #3381: With large StateCommon.Border.Rounding compared to button height, short text should stay visually centered (horizontal and vertical) inside the pill shape." + Environment.NewLine +
                @"What to check: Cyrillic + large font on a wide short bar; tall narrow capsule; low rounding baseline; then use the live controls to sweep rounding, vertical alignment, font size, and button height. Resize the form — layout should track the border path, not the uncapped palette rounding value.";
            //
            // _root
            //
            _root.Dock = DockStyle.Fill;
            _root.Padding = new Padding(12, 0, 12, 12);
            _root.AutoSize = false;
            _root.ColumnCount = 1;
            _root.Margin = new Padding(0);
            _root.Name = "tableLayoutRoot";

            int row = 0;
            void AddRow(RowStyle style, Control c)
            {
                _root.RowStyles.Add(style);
                _root.Controls.Add(c, 0, row++);
            }

            AddRow(new RowStyle(SizeType.AutoSize), NewSectionHeader(@"1) Wide × short — matches reporter setup (large rounding vs height)"));
            var btnWideIssueShape = CreatePillButton(@"Начать", 40f, 100f);
            AddRow(new RowStyle(SizeType.Absolute, 102), NewFillPanel(102, btnWideIssueShape));

            AddRow(new RowStyle(SizeType.AutoSize), NewSectionHeader(@"2) Two equal-height bars — different font sizes (metrics stress)"));
            var tlpPair = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0)
            };
            tlpPair.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            tlpPair.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            tlpPair.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            var btnSideBySideLeft = CreatePillButton(@"Start — 22pt", 22f, 100f);
            var btnSideBySideRight = CreatePillButton(@"Начать — 40pt", 40f, 100f);
            tlpPair.Controls.Add(NewFillPanel(86, btnSideBySideLeft), 0, 0);
            tlpPair.Controls.Add(NewFillPanel(86, btnSideBySideRight), 1, 0);
            AddRow(new RowStyle(SizeType.Absolute, 86), tlpPair);

            AddRow(new RowStyle(SizeType.AutoSize), NewSectionHeader(@"3) Tall narrow capsule (rounding clamped by width)"));
            var btnTallNarrow = CreatePillButton(@"OK", 28f, 72f);
            var tlpTall = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Height = 200,
                ColumnCount = 3,
                RowCount = 1,
                Margin = new Padding(0)
            };
            tlpTall.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            tlpTall.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140f));
            tlpTall.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            tlpTall.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            tlpTall.Controls.Add(new Panel { Dock = DockStyle.Fill, Margin = new Padding(0) }, 0, 0);
            tlpTall.Controls.Add(btnTallNarrow, 1, 0);
            btnTallNarrow.Dock = DockStyle.Fill;
            tlpTall.Controls.Add(new Panel { Dock = DockStyle.Fill, Margin = new Padding(0) }, 2, 0);
            AddRow(new RowStyle(SizeType.Absolute, 200), tlpTall);

            AddRow(new RowStyle(SizeType.AutoSize), NewSectionHeader(@"4) Low rounding baseline (rounding ≈ 4)"));
            var btnLowRounding = CreatePillButton(@"Centered reference", 18f, 4f);
            AddRow(new RowStyle(SizeType.Absolute, 52), NewFillPanel(52, btnLowRounding));

            AddRow(new RowStyle(SizeType.AutoSize), NewSectionHeader(@"5) Live — rounding, TextV, font size, button height"));
            _btnLive = CreatePillButton(@"Live preview", 36f, 100f);
            var liveHost = BuildLiveSectionPanel();
            AddRow(new RowStyle(SizeType.Percent, 100f), liveHost);

            var body = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0) };
            body.Controls.Add(_root);
            body.Controls.Add(_lblIntro);

            Controls.Add(body);
            _lblIntro.BringToFront();
            //
            // Bug3381KryptonButtonRoundedTextCenteringDemo
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(980, 760);
            MinimumSize = new Size(720, 620);
            Name = "Bug3381KryptonButtonRoundedTextCenteringDemo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = @"Bug #3381 — KryptonButton rounded corners + text centering";
            _root.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Panel BuildLiveSectionPanel()
        {
            var wrap = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0) };

            _trackRounding = new TrackBar
            {
                Minimum = 0,
                Maximum = 120,
                TickFrequency = 10,
                SmallChange = 2,
                LargeChange = 10,
                Value = 100,
                Width = 360,
                Height = 42,
                Margin = new Padding(12, 4, 0, 0)
            };
            _lblRoundingValue = new Label
            {
                AutoSize = true,
                Text = @"Rounding: 100",
                Margin = new Padding(0, 10, 0, 0)
            };
            _trackRounding.ValueChanged += OnTrackRoundingValueChanged;

            _cmbTextV = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 200,
                Margin = new Padding(12, 4, 0, 0)
            };
            _cmbTextV.Items.AddRange(new object[] { @"TextV: Center", @"TextV: Near (top)", @"TextV: Far (bottom)" });
            _cmbTextV.SelectedIndex = 0;
            _cmbTextV.SelectedIndexChanged += OnCmbTextVSelectedIndexChanged;

            _nudFontSize = new NumericUpDown
            {
                Minimum = 10,
                Maximum = 96,
                Value = 36,
                Width = 72,
                Margin = new Padding(8, 4, 0, 0)
            };
            _nudFontSize.ValueChanged += OnNudFontSizeValueChanged;

            _nudLiveHeight = new NumericUpDown
            {
                Minimum = 48,
                Maximum = 220,
                Value = 96,
                Width = 72,
                Margin = new Padding(8, 4, 0, 0)
            };
            _nudLiveHeight.ValueChanged += OnNudLiveHeightValueChanged;

            var topFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Padding = new Padding(0, 0, 0, 8)
            };
            topFlow.Controls.Add(_lblRoundingValue);
            topFlow.Controls.Add(_trackRounding);
            topFlow.Controls.Add(_cmbTextV);
            topFlow.Controls.Add(new Label { AutoSize = true, Text = @"Font (pt):", Margin = new Padding(12, 10, 0, 0) });
            topFlow.Controls.Add(_nudFontSize);
            topFlow.Controls.Add(new Label { AutoSize = true, Text = @"Btn height:", Margin = new Padding(12, 10, 0, 0) });
            topFlow.Controls.Add(_nudLiveHeight);

            _pLiveButtonHost = new Panel
            {
                Dock = DockStyle.Top,
                Height = 96,
                Margin = new Padding(0)
            };
            _pLiveButtonHost.Controls.Add(_btnLive);
            _btnLive.Dock = DockStyle.Fill;

            wrap.Controls.Add(_pLiveButtonHost);
            wrap.Controls.Add(topFlow);

            return wrap;
        }

        #endregion

        private KryptonWrapLabel _lblIntro;
        private TableLayoutPanel _root;
        private KryptonButton _btnLive;
        private TrackBar _trackRounding;
        private Label _lblRoundingValue;
        private ComboBox _cmbTextV;
        private NumericUpDown _nudFontSize;
        private NumericUpDown _nudLiveHeight;
        private Panel _pLiveButtonHost;
    }
}
