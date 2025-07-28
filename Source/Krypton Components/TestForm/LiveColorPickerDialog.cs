using System;
using System.Drawing;
using System.Windows.Forms;

namespace TestForm
{
    /// <summary>
    /// Wrapper around Cyotek ColorPickerDialog that adds a bottom checkbox for enabling live theme updates and
    /// fires <see cref="LiveColorChanged"/> whenever the user changes the colour while the checkbox is checked.
    /// </summary>
    internal sealed class LiveColorPickerDialog : Cyotek.Windows.Forms.ColorPickerDialog
    {
        private readonly CheckBox _chkLive;
        private readonly FlowLayoutPanel _bottomPanel;
        private readonly Button _btnReset;
        private Color _initialColor;
        private bool _panelAdded;

        public LiveColorPickerDialog()
        {
            // Create checkbox – will be added once handle created so we know size of dialog.
            _chkLive = new CheckBox
            {
                AutoSize = true,
                Text = "Live theme updates?",
                Padding = new Padding(0, 0, 20, 0),
                Checked = false
            };

            _btnReset = new Button
            {
                Text = "Reset",
                AutoSize = true,
                Margin = new Padding(0, -3, 0, 0) // up 3px, left shift will be handled by checkbox margin
            };
            _btnReset.Click += (_, __) => { Color = _initialColor; };

            // Reduce right margin of checkbox to pull Reset button leftwards (~40px)
            _chkLive.Margin = new Padding(0, 0, -40, 0);

            _bottomPanel = new FlowLayoutPanel
            {
                AutoSize = true,
                Dock = DockStyle.Bottom,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10, 6, 10, 6)
            };
            _bottomPanel.Controls.Add(_chkLive);
            _bottomPanel.Controls.Add(_btnReset);

            _pollTimer = new Timer { Interval = 150, Enabled = true };
            _pollTimer.Tick += (_, __) => OnColorChangedInternal();
        }

        /// <summary>
        /// Raised when the user changes the colour and live-update is enabled.
        /// </summary>
        public event EventHandler<LiveColorChangedEventArgs>? LiveColorChanged;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (!_panelAdded)
            {
                _panelAdded = true;
                _initialColor = Color;

                Controls.Add(_bottomPanel);
                _bottomPanel.BringToFront();
                // Ensure dialog is tall enough for the new bottom panel
                Height += _bottomPanel.PreferredSize.Height;
            }
        }

        private readonly Timer _pollTimer;

        private void OnColorChangedInternal()
        {
            if (_chkLive.Checked)
            {
                if (Color != _lastReportedColor)
                {
                    _lastReportedColor = Color;
                    LiveColorChanged?.Invoke(this, new LiveColorChangedEventArgs(Color));
                }
            }
        }

        private Color _lastReportedColor;

        internal sealed class LiveColorChangedEventArgs : EventArgs
        {
            public LiveColorChangedEventArgs(Color color) => Color = color;

            public Color Color { get; }
        }
    }
}