using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    public partial class KryptonToggleSwitch : UserControl
    {
        private bool _isChecked;

        public event EventHandler CheckedChanged;

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnCheckedChanged(EventArgs.Empty);
                    AnimateToggle();
                }
            }
        }

        public KryptonToggleSwitch()
        {
            InitializeComponent();

            UpdateToggleState();
        }

        protected virtual void OnCheckedChanged(EventArgs e)
        {
            CheckedChanged?.Invoke(this, e);
        }

        private async void AnimateToggle()
        {
            int start = _knob.Location.X;
            int end = IsChecked ? Width - _knob.Width - 4 : 2;

            // Update background and labels
            _onLabel.Visible = IsChecked;
            _offLabel.Visible = !IsChecked;
            //_background.StateCommon.Color1 = IsChecked ? System.Drawing.Color.MediumSeaGreen : System.Drawing.Color.LightGray;

            // Smooth animation for the knob
            for (int i = 0; i <= 10; i++)
            {
                int step = start + (end - start) * i / 10;
                _knob.Location = new System.Drawing.Point(step, 2);
                await Task.Delay(10);
            }
        }

        private void ToggleSwitch_Click(object sender, EventArgs e)
        {
            IsChecked = !IsChecked;
        }

        private void UpdateToggleState()
        {
            // Set the initial state
            _onLabel.Visible = IsChecked;
            _offLabel.Visible = !IsChecked;
            //_background.StateCommon.Back.Color1 = IsChecked ? System.Drawing.Color.MediumSeaGreen : System.Drawing.Color.LightGray;
            _knob.Location = new System.Drawing.Point(IsChecked ? Width - _knob.Width - 4 : 2, 2);
        }

        private void _knob_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.FillEllipse(System.Drawing.Brushes.White, new System.Drawing.Rectangle(0, 0, this._knob.Width, this._knob.Height));
        }

        private void KryptonToggleSwitch_Click(object sender, EventArgs e)
        {
            IsChecked = !IsChecked;
        }
    }
}
