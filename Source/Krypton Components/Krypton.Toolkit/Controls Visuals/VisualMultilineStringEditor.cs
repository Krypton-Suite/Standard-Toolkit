#region BSD License
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

// ReSharper disable UnusedMember.Local

namespace Krypton.Toolkit;

/// <summary>
/// Multiline String Editor Window.
/// </summary>
internal sealed class MultilineStringEditor1 : KryptonForm //Form
{
    #region Instance Members
    private bool _saveChanges = true;
    private readonly KryptonTextBox _textBox;
    private readonly KryptonTextBox _owner;
    private VisualStyleRenderer _sizeGripRenderer;
    #endregion

    #region Identity
    /// <summary>
    /// Initializes a new instance of the MultilineStringEditor class.
    /// </summary>
    /// <param name="owner"></param>
    public MultilineStringEditor1(KryptonTextBox owner)
    {
        SetInheritedControlOverride();
        SuspendLayout();
        _textBox = new KryptonTextBox { Dock = DockStyle.Fill, Multiline = true };
        _textBox.StateCommon.Border.Draw = InheritBool.False;
        _textBox.KeyDown += OnKeyDownTextBox;
        AutoScaleDimensions = new SizeF(6F, 13F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(284, 262);
        ControlBox = false;
        Controls.Add(_textBox);
        FormBorderStyle = FormBorderStyle.None;
        BackColor = Color.White;
        Padding = new Padding(1, 1, 1, 16);
        MaximizeBox = false;
        MinimizeBox = false;
        MinimumSize = new Size(100, 20);
        AutoSize = false;
        DoubleBuffered = true;
        ResizeRedraw = true;
        ShowIcon = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.Manual;
        _owner = owner;
        ResumeLayout(false);
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Shows the multiline string editor.
    /// </summary>
    public void ShowEditor()
    {
        Location = _owner.PointToScreen(Point.Empty);
        _textBox.Text = _owner.Text;
        Show();
    }
    #endregion

    #region Protected Override
    /// <summary>
    /// Closes the multiline string editor.
    /// </summary>
    /// <param name="e">
    /// Event arguments.
    /// </param>
    protected override void OnDeactivate(EventArgs e)
    {
        base.OnDeactivate(e);
        CloseEditor();
    }

    /// <summary>
    /// Raises the Paint event.
    /// </summary>
    /// <param name="e">
    /// A PaintEventArgs that contains the event data.
    /// </param>
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        // Paint the sizing grip.
        using (var gripImage = new Bitmap(0x10, 0x10))
        {
            using (Graphics g = Graphics.FromImage(gripImage))
            {
                if (Application.RenderWithVisualStyles)
                {
                    _sizeGripRenderer ??= new VisualStyleRenderer(VisualStyleElement.Status.Gripper.Normal);

                    _sizeGripRenderer.DrawBackground(g, new Rectangle(0, 0, 0x10, 0x10));
                }
                else
                {
                    ControlPaint.DrawSizeGrip(g, BackColor, 0, 0, 0x10, 0x10);
                }
            }
            e.Graphics.DrawImage(gripImage, ClientSize.Width - 0x10, ClientSize.Height - 0x10 + 1, 0x10, 0x10);
        }
        ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
    }

    /// <summary>
    /// Processes Windows messages.
    /// </summary>
    /// <param name="m">
    /// The Windows Message to process.
    /// </param>
    protected override void WndProc(ref Message m)
    {
        var handled = false;
        switch (m.Msg)
        {
            case PI.WM_.NCHITTEST:
                handled = OnNcHitTest(ref m);
                break;
            case PI.WM_.GETMINMAXINFO:
                handled = OnGetMinMaxInfo(ref m);
                break;
        }

        if (!handled)
        {
            base.WndProc(ref m);
        }
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Closes the editor form.
    /// </summary>
    private void CloseEditor()
    {
        if (_saveChanges)
        {
            _owner.Text = _textBox.Text;
        }

        Close();
    }

    /// <summary>
    /// Occurs when a key is pressed while the control has focus.
    /// </summary>
    /// <param name="sender">The control.</param>
    /// <param name="e">The event arguments.</param>
    private void OnKeyDownTextBox(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            _saveChanges = false;
            CloseEditor();
        }
    }
    /// <summary>
    /// Occurs when the MinMaxInfo needs to be retrieved by the operating system.
    /// </summary>
    /// <param name="m">
    /// The window message.
    /// </param>
    /// <returns>
    /// true if the message was handled; otherwise false.
    /// </returns>
    private bool OnGetMinMaxInfo(ref Message m)
    {
        var minMax = (PI.MINMAXINFO)Marshal.PtrToStructure(m.LParam, typeof(PI.MINMAXINFO))!;
        if (!MaximumSize.IsEmpty)
        {
            minMax.ptMaxTrackSize.X = MaximumSize.Width;
            minMax.ptMaxTrackSize.Y = MaximumSize.Height;
        }

        minMax.ptMinTrackSize.X = MinimumSize.Width;
        minMax.ptMinTrackSize.Y = MinimumSize.Height;
        Marshal.StructureToPtr(minMax, m.LParam, false);
        return true;
    }

    /// <summary>
    /// Occurs when the operating system needs to determine what part of the window corresponds
    /// to a particular screen coordinate.
    /// </summary>
    /// <param name="m">
    /// The window message.
    /// </param>
    /// <returns>
    /// true if the message was handled; otherwise false.
    /// </returns>
    private bool OnNcHitTest(ref Message m)
    {
        Point clientLocation = PointToClient(Cursor.Position);
        var gripBounds = new GripBounds(ClientRectangle);
        if (gripBounds.BottomRight.Contains(clientLocation))
        {
            m.Result = (IntPtr)PI.HT.BOTTOMRIGHT;
        }
        else if (gripBounds.Bottom.Contains(clientLocation))
        {
            m.Result = (IntPtr)PI.HT.BOTTOM;
        }
        else if (gripBounds.Right.Contains(clientLocation))
        {
            m.Result = (IntPtr)PI.HT.RIGHT;
        }

        return m.Result != IntPtr.Zero;
    }
    #endregion

    #region Internal
    // ReSharper disable IdentifierTypo
    // ReSharper disable InconsistentNaming

    private struct GripBounds
    {
        private const int GripSize = 6;
        private const int CornerGripSize = GripSize << 1;

        public GripBounds(Rectangle clientRectangle) => ClientRectangle = clientRectangle;

        private Rectangle ClientRectangle
        {
            get;
            //set { clientRectangle = value; }
        }

        public Rectangle Bottom
        {
            get
            {
                Rectangle rect = ClientRectangle;
                rect.Y = rect.Bottom - GripSize + 1;
                rect.Height = GripSize;
                return rect;
            }
        }

        public Rectangle BottomRight
        {
            get
            {
                Rectangle rect = ClientRectangle;
                rect.Y = rect.Bottom - CornerGripSize + 1;
                rect.Height = CornerGripSize;
                rect.X = rect.Width - CornerGripSize + 1;
                rect.Width = CornerGripSize;
                return rect;
            }
        }

        public Rectangle Top
        {
            get
            {
                Rectangle rect = ClientRectangle;
                rect.Height = GripSize;
                return rect;
            }
        }

        public Rectangle TopRight
        {
            get
            {
                Rectangle rect = ClientRectangle;
                rect.Height = CornerGripSize;
                rect.X = rect.Width - CornerGripSize + 1;
                rect.Width = CornerGripSize;
                return rect;
            }
        }

        public Rectangle Left
        {
            get
            {
                Rectangle rect = ClientRectangle;
                rect.Width = GripSize;
                return rect;
            }
        }

        public Rectangle BottomLeft
        {
            get
            {
                Rectangle rect = ClientRectangle;
                rect.Width = CornerGripSize;
                rect.Y = rect.Height - CornerGripSize + 1;
                rect.Height = CornerGripSize;
                return rect;
            }
        }

        public Rectangle Right
        {
            get
            {
                Rectangle rect = ClientRectangle;
                rect.X = rect.Right - GripSize + 1;
                rect.Width = GripSize;
                return rect;
            }
        }

        public Rectangle TopLeft
        {
            get
            {
                Rectangle rect = ClientRectangle;
                rect.Width = CornerGripSize;
                rect.Height = CornerGripSize;
                return rect;
            }
        }
    }
    // ReSharper restore IdentifierTypo
    // ReSharper restore InconsistentNaming
    #endregion
}