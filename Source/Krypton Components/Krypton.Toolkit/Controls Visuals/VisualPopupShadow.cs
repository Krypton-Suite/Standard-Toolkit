﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Displays a shadow effect for a visual popup.
    /// </summary>
    public class VisualPopupShadow : Form
    {
        #region Static Fields

        private const int SHADOW_SIZE = 3;
        private static readonly Brush[] _brushes;
        #endregion

        #region Instance Fields
        private GraphicsPath? _path1;
        private GraphicsPath? _path2;
        private GraphicsPath? _path3;
        #endregion

        #region Identity
        static VisualPopupShadow()
        {
            _brushes = new Brush[SHADOW_SIZE];
            for (var i = 0; i < SHADOW_SIZE; i++)
            {
                var shade = i * 70;
                _brushes[i] = new SolidBrush(Color.FromArgb(shade, shade, shade));
            }
        }

        /// <summary>
        /// Initialize a new instance of the VisualPopupShadow class. 
        /// </summary>
        public VisualPopupShadow()
        {
            // Update form properties so we do not have a border and do not show
            // in the task bar. We draw the background in Magenta and set that as
            // the transparency key so it is a see through window.
            StartPosition = FormStartPosition.Manual;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            TransparencyKey = Color.Magenta;
            Opacity = 0.18f;
        }

        /// <summary>
        /// Disposing of instance resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ClearPaths();
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Show the popup using the provided rectangle as the screen rect.
        /// </summary>
        /// <param name="screenRect">Screen rectangle for showing the popup.</param>
        public virtual void Show(Rectangle screenRect)
        {
            // Offset by the width/height of the shadow
            screenRect.X += SHADOW_SIZE;
            screenRect.Y += SHADOW_SIZE;

            // Update the screen position
            Location = screenRect.Location;
            ClientSize = screenRect.Size;

            // Show the window without activating it (i.e. do not take focus)
            PI.ShowWindow(Handle, PI.ShowWindowCommands.SW_SHOWNOACTIVATE);
        }

        /// <summary>
        /// Define the drawing paths for the shadow.
        /// </summary>
        /// <param name="path1">Outer path.</param>
        /// <param name="path2">Middle path.</param>
        /// <param name="path3">Inside path.</param>
        public void DefinePaths(GraphicsPath path1,
                                GraphicsPath path2,
                                GraphicsPath path3)
        {
            // Dispose of existing paths
            ClearPaths();

            // Use the new paths
            _path1 = path1;
            _path2 = path2;
            _path3 = path3;

            // Redraw with the new paths
            Invalidate();
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets the creation parameters.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Parent = IntPtr.Zero;
                cp.Style |= unchecked((int)PI.WS_.POPUP);
                cp.ExStyle |= unchecked((int)(PI.WS_EX_.TOPMOST + PI.WS_EX_.TOOLWINDOW));
                return cp;
            }
        }

        /// <summary>
        /// Raises the PaintBackground event.
        /// </summary>
        /// <param name="pevent">A PaintEventArgs containing the event data.</param>
        protected override void OnPaintBackground(PaintEventArgs pevent) =>
            // Magenta is the transparent color
            pevent.Graphics.FillRectangle(Brushes.Magenta, pevent.ClipRectangle);

        /// <summary>
        /// Raises the Paint event.
        /// </summary>
        /// <param name="e">A PaintEventArgs containing the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if ((_path1 != null) && (_path2 != null) && (_path3 != null))
            {
                DrawPaths(e.Graphics);
            }
            else
            {
                DrawShadow(e.Graphics, ClientRectangle);
            }
        }
        #endregion

        #region Implementation
        private void ClearPaths()
        {
            if (_path1 != null)
            {
                _path1.Dispose();
                _path1 = null;
            }

            if (_path2 != null)
            {
                _path2.Dispose();
                _path2 = null;
            }

            if (_path3 != null)
            {
                _path3.Dispose();
                _path3 = null;
            }
        }

        private void DrawPaths(Graphics g)
        {
            g.FillPath(_brushes[2], _path1!);
            g.FillPath(_brushes[1], _path2!);
            g.FillPath(_brushes[0], _path3!);
        }

        private void DrawShadow(Graphics g, Rectangle area)
        {
            using GraphicsPath outside1 = CommonHelper.RoundedRectanglePath(area, 6);
            area.Inflate(-1, -1);
            g.FillPath(_brushes[2], outside1);
            using GraphicsPath outside2 = CommonHelper.RoundedRectanglePath(area, 6);
            g.FillPath(_brushes[1], outside2);
            area.Inflate(-1, -1);
            using GraphicsPath outside3 = CommonHelper.RoundedRectanglePath(area, 6);
            g.FillPath(_brushes[0], outside3);
        }
        #endregion
    }
}
