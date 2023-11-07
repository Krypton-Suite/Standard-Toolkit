﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Show a wait dialog during long operations.
    /// </summary>
    [ToolboxItem(false)]
    public partial class ModalWaitDialog : KryptonForm, IMessageFilter
    {
        #region Static Fields
        private const int DELAY_SHOWING = 500;
        private const int DELAY_SPIN = 75;
        private const int SPIN_ANGLE = 20;
        private static readonly Bitmap _hourGlass = GenericImageResources.HourGlass;
        #endregion

        #region Instance Fields

        private readonly bool _showProgressBar;
        private bool _startTimestamped;
        private DateTime _startTimestamp;
        private DateTime _spinTimestamp;
        private float _spinAngle;
        private int _progressBarValue;

        #endregion

        #region Public

        public int ProgressBarValue { get => _progressBarValue; set { _progressBarValue = value; UpdateProgressBarValue(value); } }

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ModalWaitDialog class. 
        /// </summary>
        public ModalWaitDialog(Image? image, string? text, bool? showProgressBar, int? progressBarValue)
        {
            InitializeComponent();

            // Remove redraw flicker by using double buffering
            SetStyle(ControlStyles.DoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint, true);

            pbxImage.Image = image ?? GenericImageResources.HourGlass;

            kwlStatus.Text = text ?? @"Please wait for operation to complete...";

            _showProgressBar = showProgressBar ?? false;

            _progressBarValue = progressBarValue ?? 0;

            if (_showProgressBar)
            {
                kpbProgress.Visible = true;

                kpbProgress.Value = _progressBarValue;

                Size = new Size(312, 125);
            }
            else
            {
                kpbProgress.Visible = false;

                kpbProgress.Value = 0;

                Size = new Size(312, 105);
            }

            // Hook into dispatch of windows messages
            Application.AddMessageFilter(this);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the OnPaint event.
        /// </summary>
        /// <param name="e">A PaintEventArgs containing event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Let base class perform standard painting
            base.OnPaint(e);

            // Start drawing offset by 16 pixels from each edge
            e.Graphics.TranslateTransform(32, 32);

            // Number of degrees to rotate the image around
            e.Graphics.RotateTransform(_spinAngle);

            // Perform actual draw of the image
            e.Graphics.DrawImage(_hourGlass,
                                 -16, -16,
                                 _hourGlass.Width,
                                 _hourGlass.Height);

            // Must return the graphics instance in same state provided
            e.Graphics.ResetTransform();
        }
        #endregion

        #region Public
        /// <summary>
        /// Called periodically to update the wait dialog.
        /// </summary>
        public void UpdateDialog()
        {
            // Remember the first time the update is called
            if (!_startTimestamped)
            {
                _startTimestamped = true;
                _startTimestamp = DateTime.Now;
            }
            else
            {
                // If the dialog has not been shown yet
                if (!Visible)
                {
                    // Has initial delay expired?
                    if (DateTime.Now.Subtract(_startTimestamp).TotalMilliseconds > DELAY_SHOWING)
                    {
                        // Make this dialog visible to the user
                        Show();

                        // Start the spin timing
                        _spinTimestamp = DateTime.Now;
                    }
                }
                else
                {
                    // Has the spin delay expired?
                    if (DateTime.Now.Subtract(_spinTimestamp).TotalMilliseconds > DELAY_SPIN)
                    {
                        // Increase the spin angle by one notch
                        _spinAngle = (_spinAngle + SPIN_ANGLE) % 360;

                        // Request the spin image be redrawn
                        Invalidate();

                        // Start the next spin timing
                        _spinTimestamp = DateTime.Now;
                    }
                }
            }

            // Let any repainting or processing events be dispatched
            Application.DoEvents();
        }

        /// <summary>
        /// Process windows messages before they are dispatched.
        /// </summary>
        /// <param name="m">Message to process.</param>
        /// <returns>True to suppress message dispatch; false otherwise.</returns>
        public bool PreFilterMessage(ref Message m)
        {
            // Prevent mouse messages from activating any application windows
            if (m.Msg is >= 0x0200 and <= 0x0209 or >= 0x00A0 and <= 0x00A9)
            {
                // Discover target control for message
                if (FromHandle(m.HWnd) != null)
                {
                    // Find the form that the control is inside
                    Form? f = FromHandle(m.HWnd).FindForm();

                    // If the message is for this dialog then let it be dispatched
                    if ((f != null) && (f == this))
                    {
                        return false;
                    }
                }

                // Eat message to prevent dispatch
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Implementation

        private void UpdateProgressBarValue(int value) => kpbProgress.Value = value;

        #endregion
    }
}
