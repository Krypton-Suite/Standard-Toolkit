using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton.Toolkit
{
    public class KryptonToggleSwitch : Control
    {
        #region Instance Fields

        private bool _checked;
        private bool _isTracking;
        private bool _isPressed;

        private int _knobSize;
        private int _padding;

        private RectangleF _knob;

        private IPaletteTriple _stateCommon;
        private IPaletteTriple _stateDisabled;
        private IPaletteTriple _stateNormal;
        private IPaletteTriple _stateTracking;
        private IPaletteTriple _statePressed;

        #endregion

        #region Events

        [Description("Occurs when the value of the Checked property changes.")]
        public event EventHandler CheckedChanged;

        #endregion

        #region Public Properties

        [Category("Visuals")]
        [Description("Defines the common appearance settings.")]
        public IPaletteTriple StateCommon
        {
            get => _stateCommon;
            set
            {
                _stateCommon = value; 
                Invalidate();
            }
        }

        [Category("Visuals")]
        [Description("Defines the disabled appearance settings.")]
        public IPaletteTriple StateDisabled
        {
            get => _stateDisabled;
            set
            {
                _stateDisabled = value;
                Invalidate();
            }
        }

        [Category("Visuals")]
        [Description("Defines the normal appearance settings.")]
        public IPaletteTriple StateNormal
        {
            get => _stateNormal;
            set
            {
                _stateNormal = value;
                Invalidate();
            }
        }

        [Category("Visuals")]
        [Description("Defines the pressed appearance settings.")]
        public IPaletteTriple StatePressed
        {
            get => _statePressed;
            set
            {
                _statePressed = value;
                Invalidate();
            }
        }

        [Category("Visuals")]
        [Description("Defines the tracking appearance settings.")]
        public IPaletteTriple StateTracking
        {
            get => _stateTracking;
            set
            {
                _stateTracking = value;
                Invalidate();
            }
        }

        [Category("Behavior")]
        [Description("Indicates whether the toggle switch is checked.")]
        public bool Checked
        {
            get => _checked;
            set
            {
                if (_checked != value)
                {
                    _checked = value;
                    CheckedChanged.Invoke(this, EventArgs.Empty);
                    Invalidate();
                }
            }
        }


        #endregion

        #region Identity

        public KryptonToggleSwitch()
        {
            DoubleBuffered = true;
            _knobSize = 20;
            _padding = 4;
            Size = new Size(50, _knobSize + _padding * 2);

            // Initialize PaletteRedirect with a default context
            PaletteRedirect redirector = new PaletteRedirect(KryptonManager.CurrentGlobalPalette);

            // Default state configuration
            StateCommon = new PaletteTripleRedirect(redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone);
            StateDisabled = new PaletteTripleRedirect(redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone);
            StateNormal = new PaletteTripleRedirect(redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone);
            StatePressed = new PaletteTripleRedirect(redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone);
            StateTracking = new PaletteTripleRedirect(redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone);
        }

        #endregion

        #region Protected Overrides

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            IPaletteTriple state = GetCurrentState();

            // Background
            using (SolidBrush backgroundBrush = new SolidBrush(state.PaletteBack.GetBackColor1(PaletteState.Normal)))
            {
                e.Graphics.FillRectangle(backgroundBrush, ClientRectangle);
            }

            // Border
            using (Pen borderPen = new Pen(state.PaletteBorder!.GetBorderColor1(PaletteState.Normal), state.PaletteBorder.GetBorderWidth(PaletteState.Normal)))
            {
                e.Graphics.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);
            }

            // Knob
            _knob = GetKnobRectangle();
            using (SolidBrush knobBrush = new SolidBrush(state.PaletteBack.GetBackColor2(PaletteState.Normal)))
            {
                e.Graphics.FillEllipse(knobBrush, _knob);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isTracking = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isTracking = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                _isPressed = true;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (_isPressed)
            {
                _isPressed = false;
                Checked = !Checked;
            }
            Invalidate();
        }

        #endregion

        #region Private Methods

        private IPaletteTriple GetCurrentState()
        {
            if (!Enabled)
            {
                return StateDisabled;
            }

            if (_isPressed)
            {
                return StatePressed;
            }

            if (_isTracking)
            {
                return StateTracking;
            }

            return StateNormal;
        }

        private RectangleF GetKnobRectangle()
        {
            float x = Checked
                ? Width - _knobSize - _padding
                : _padding;

            float y = (Height - _knobSize) / 2f;

            return new RectangleF(x, y, _knobSize, _knobSize);
        }


        #endregion
    }
}
