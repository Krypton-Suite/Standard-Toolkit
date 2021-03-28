#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  Version 6.0.0  
 *
 */
#endregion

#if NETFRAMEWORK // https://docs.microsoft.com/en-us/dotnet/standard/frameworks#how-to-specify-target-frameworks
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Displays byte arrays in hexadecimal, ANSI, and Unicode formats.
    /// </summary>
    /// <remarks>
    /// This is based off <see cref="System.ComponentModel.Design.ByteViewer"/> with a couple
    /// of cosmetic changes to make it look at least a little less Win3.11-ish.
    /// </remarks>
    [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    public class KryptonByteViewer : TableLayoutPanel
    {
        #region Private Classes
        /// <summary>
        /// VScrollBar is a simple wrapper for the ancient WINAPI scrollbar from the early
        /// 90s which has the annoying property of blinking like a cursor when selected. I don't
        /// know why they thought this was a good idea because it looks ridiculous and there is
        /// not even a way to turn it off, so we need to work around it as so often with WinForms.
        /// </summary>
        class VScrollBarEx : VScrollBar
        {
            public void _OnMouseWheel(MouseEventArgs e)
            {
                base.OnMouseWheel(e);
            }

            public void _OnKeyDown(KeyEventArgs e)
            {
                base.OnKeyDown(e);
            }
        }
        #endregion

        #region Private Constants
        private const int DEFAULT_COLUMN_COUNT = 16;
        private const int DEFAULT_ROW_COUNT = 25;
        private const int COLUMN_COUNT = 16;
        private const int BORDER_GAP = 2;
        private const int INSET_GAP = 3;
        private const int CELL_HEIGHT = 21;
        private const int CELL_WIDTH = 25;
        private const int CHAR_WIDTH = 8;
        private const int ADDRESS_WIDTH = 69;
        private const int HEX_WIDTH = 400;
        private const int DUMP_WIDTH = 128;
        private const int HEX_DUMP_GAP = 5;
        private const int ADDRESS_START_X = 5;
        private const int CLIENT_START_Y = 5;
        private const int LINE_START_Y = 7;
        private const int HEX_START_X = 74;
        private const int DUMP_START_X = 479;
        private const int SCROLLBAR_START_X = 612;
        #endregion

        #region Static Fields
        private static readonly Font ADDRESS_FONT = new Font("Microsoft Sans Serif", 8f);
        private static readonly Font HEXDUMP_FONT = new Font("Consolas", 9.75f);
        #endregion

        #region Instance Fields
        private readonly int SCROLLBAR_HEIGHT = SystemInformation.HorizontalScrollBarHeight;
        private readonly int SCROLLBAR_WIDTH = SystemInformation.VerticalScrollBarWidth;

        private VScrollBarEx _scrollBar;
        private TextBox _edit;
        private int _columnCount = 16;
        private int _rowCount = 25;
        private byte[] _dataBuf;
        private int _startLine;
        private int _displayLinesCount;
        private int _linesCount;
        private DisplayMode _displayMode;
        #endregion

        #region Identity
        /// <summary>
        /// Initializes a new instance of the ByteViewer class.
        /// </summary>
        public KryptonByteViewer()
        {
            SuspendLayout();
            CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            ColumnCount = 1;
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            RowCount = 1;
            RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            InitUI();
            ResumeLayout();
            _displayMode = DisplayMode.Hexdump;
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, value: true);
            SetBytes(new byte[] { });
        }
        #endregion

        #region Private

        private int CellToIndex(int column, int row)
        {
            return row * _columnCount + column;
        }

        private byte[] ComposeLineBuffer(int startLine, int line)
        {
            int num = startLine * _columnCount;
            byte[] array = (num + (line + 1) * _columnCount <= _dataBuf.Length) ? new byte[_columnCount] : new byte[_dataBuf.Length % _columnCount];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = _dataBuf[num + CellToIndex(i, line)];
            }
            return array;
        }

        private void DrawAddress(Graphics g, int startLine, int line)
        {
            string s = ((startLine + line) * _columnCount).ToString("X8", CultureInfo.InvariantCulture);
            Brush brush = new SolidBrush(ForeColor);
            try
            {
                g.DrawString(s, ADDRESS_FONT, brush, 5f, 7 + line * 21);
            }
            finally
            {
                brush?.Dispose();
            }
        }

        private void DrawClient(Graphics g)
        {
            using (Brush brush = new SolidBrush(SystemColors.ControlLightLight))
            {
                g.FillRectangle(brush, new Rectangle(74, 5, 538, _rowCount * 21));
            }
            using (Pen pen = new Pen(SystemColors.ControlDark))
            {
                g.DrawRectangle(pen, new Rectangle(74, 5, 537, _rowCount * 21 - 1));
                g.DrawLine(pen, 474, 5, 474, 5 + _rowCount * 21 - 1);
            }
        }

        private static bool CharIsPrintable(char c)
        {
            UnicodeCategory unicodeCategory = char.GetUnicodeCategory(c);
            switch (unicodeCategory)
            {
                case UnicodeCategory.Control:
                    return unicodeCategory == UnicodeCategory.OtherNotAssigned;
                default:
                    return true;
            }
        }

        private void DrawDump(Graphics g, byte[] lineBuffer, int line)
        {
            StringBuilder stringBuilder = new StringBuilder(lineBuffer.Length);
            for (int i = 0; i < lineBuffer.Length; i++)
            {
                char c = Convert.ToChar(lineBuffer[i]);
                if (CharIsPrintable(c))
                {
                    stringBuilder.Append(c);
                }
                else
                {
                    stringBuilder.Append('.');
                }
            }
            Brush brush = new SolidBrush(ForeColor);
            try
            {
                g.DrawString(stringBuilder.ToString(), HEXDUMP_FONT, brush, 479f, 7 + line * 21);
            }
            finally
            {
                brush?.Dispose();
            }
        }

        private void DrawHex(Graphics g, byte[] lineBuffer, int line)
        {
            StringBuilder stringBuilder = new StringBuilder(lineBuffer.Length * 3 + 1);
            for (int i = 0; i < lineBuffer.Length; i++)
            {
                stringBuilder.Append(lineBuffer[i].ToString("X2", CultureInfo.InvariantCulture));
                stringBuilder.Append(" ");
                if (i == _columnCount / 2 - 1)
                {
                    stringBuilder.Append(" ");
                }
            }

            Brush brush = new SolidBrush(ForeColor);
            try
            {
                g.DrawString(stringBuilder.ToString(), HEXDUMP_FONT, brush, 76f, 7 + line * 21);
            }
            finally
            {
                brush?.Dispose();
            }
        }

        private void DrawLines(Graphics g, int startLine, int linesCount)
        {
            for (int i = 0; i < linesCount; i++)
            {
                byte[] lineBuffer = ComposeLineBuffer(startLine, i);
                DrawAddress(g, startLine, i);
                DrawHex(g, lineBuffer, i);
                DrawDump(g, lineBuffer, i);
            }
        }

        private void InitAnsi()
        {
            int num = _dataBuf.Length;
            char[] array = new char[num + 1];
            num = MultiByteToWideChar(0, 0, _dataBuf, num, array, num);
            array[num] = '\0';
            for (int i = 0; i < num; i++)
            {
                if (array[i] == '\0')
                {
                    array[i] = '\v';
                }
            }
            _edit.Text = new string(array);
        }

        private void InitUnicode()
        {
            char[] array = new char[_dataBuf.Length / 2 + 1];
            Encoding.Unicode.GetChars(_dataBuf, 0, _dataBuf.Length, array, 0);
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == '\0')
                {
                    array[i] = '\v';
                }
            }
            array[array.Length - 1] = '\0';
            _edit.Text = new string(array);
        }

        private void InitUI()
        {
            Size = new Size(612 + SCROLLBAR_WIDTH + 2 + 3, 10 + _rowCount * 21);
            _scrollBar = new VScrollBarEx();
            _scrollBar.ValueChanged += ScrollChanged;
            _scrollBar.TabStop = false;
            _scrollBar.Dock = DockStyle.Right;
            _scrollBar.Visible = false;
            _edit = new TextBox()
            {
                AutoSize = false,
                BorderStyle = BorderStyle.None,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Both,
                AcceptsTab = true,
                AcceptsReturn = true,
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                WordWrap = true,
                Visible = false,
                Font = HEXDUMP_FONT
            };
            Controls.Add(_scrollBar, 0, 0);
            Controls.Add(_edit, 0, 0);
        }

        private void InitState()
        {
            _linesCount = (_dataBuf.Length + _columnCount - 1) / _columnCount;
            _startLine = 0;
            if (_linesCount > _rowCount)
            {
                _displayLinesCount = _rowCount;
                _scrollBar.Hide();
                _scrollBar.Maximum = _linesCount - 1;
                _scrollBar.LargeChange = _rowCount;
                _scrollBar.Show();
                _scrollBar.Enabled = true;
            }
            else
            {
                _displayLinesCount = _linesCount;
                _scrollBar.Hide();
                _scrollBar.Maximum = _rowCount;
                _scrollBar.LargeChange = _rowCount;
                _scrollBar.Show();
                _scrollBar.Enabled = false;
            }
            //            scrollBar.Select();
            // Select the panel instead so we can forward its Key and Mousewheel events
            // to the scrollbar.
            Select();
            Invalidate();
        }

        private static bool IsEnumValid(Enum enumValue, int value, int minValue, int maxValue)
        {
            return value >= minValue && value <= maxValue;
        }
        #endregion

        #region Protected Overrides
        /// <param name="e">
        /// A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.
        /// </param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            //         scrollBar.Select();
            if (_scrollBar.Enabled)
                _scrollBar._OnKeyDown(e);
        }

        /// <summary>
        /// Raises the MouseWheel event.
        /// </summary>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (_scrollBar.Enabled)
                _scrollBar._OnMouseWheel(e);
        }

        /// <summary>
        /// Paints the background of the panel.
        /// </summary>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            var palette = KryptonManager.CurrentGlobalPalette;
            if (palette == null)
                return;
            // We use the background color of KryptonPanels.
            var backColor = palette.GetBackColor1(PaletteBackStyle.PanelClient,
                PaletteState.Normal);
            using (var backBrush = new SolidBrush(backColor))
                e.Graphics.FillRectangle(backBrush, e.ClipRectangle);
        }

        /// <param name="e">
        /// A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.
        /// </param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            switch (_displayMode)
            {
                case DisplayMode.Hexdump:
                    SuspendLayout();
                    _edit.Hide();
                    _scrollBar.Show();
                    ResumeLayout();
                    DrawClient(graphics);
                    DrawLines(graphics, _startLine, _displayLinesCount);
                    break;
                case DisplayMode.Ansi:
                    _edit.Invalidate();
                    break;
                case DisplayMode.Unicode:
                    _edit.Invalidate();
                    break;
            }
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            int num = (ClientSize.Height - 10) / 21;
            if (num >= 0 && num != _rowCount)
            {
                _rowCount = num;
                if (Dock == DockStyle.None)
                {
                    Size = new Size(612 + SCROLLBAR_WIDTH + 2 + 3, 10 + _rowCount * 21);
                }
                if (_scrollBar != null)
                {
                    if (_linesCount > _rowCount)
                    {
                        _scrollBar.Hide();
                        _scrollBar.Maximum = _linesCount - 1;
                        _scrollBar.LargeChange = _rowCount;
                        _scrollBar.Show();
                        _scrollBar.Enabled = true;
                        //             scrollBar.Select();
                        Select();
                    }
                    else
                    {
                        _scrollBar.Enabled = false;
                    }
                }
                _displayLinesCount = ((_startLine + _rowCount < _linesCount) ? _rowCount : (_linesCount - _startLine));
            }
        }
        #endregion

        #region Protected Virtual
        /// <summary>Handles the <see cref="E:System.Windows.Forms.ScrollBar.ValueChanged" /> event on the <see cref="T:System.ComponentModel.Design.ByteViewer" /> control's <see cref="T:System.Windows.Forms.ScrollBar" />.</summary>
        /// <param name="source">The source of the event. </param>
        /// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
        protected virtual void ScrollChanged(object source, EventArgs e)
        {
            _startLine = _scrollBar.Value;
            Invalidate();
        }
        #endregion

        #region Public Virtual
        /// <summary>Gets the bytes in the buffer.</summary>
        /// <returns>The unsigned byte array reference.</returns>
        public virtual byte[] GetBytes()
        {
            return _dataBuf;
        }

        /// <summary>Gets the display mode for the control.</summary>
        /// <returns>The display mode that this control uses. The returned value is defined in <see cref="T:System.ComponentModel.Design.DisplayMode" />.</returns>
        public virtual DisplayMode GetDisplayMode()
        {
            return _displayMode;
        }

        /// <summary>Writes the raw data from the data buffer to a file.</summary>
        /// <param name="path">The file path to save to. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.ArgumentException">
        ///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
        /// <exception cref="T:System.IO.IOException">The file write failed. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The access requested is not permitted by the operating system for the specified <paramref name="path" />, such as when access is Write or ReadWrite and the file or directory is set for read-only access. </exception>
        public virtual void SaveToFile(string path)
        {
            if (_dataBuf != null)
            {
                FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                try
                {
                    fileStream.Write(_dataBuf, 0, _dataBuf.Length);
                    fileStream.Close();
                }
                catch
                {
                    fileStream.Close();
                    throw;
                }
            }
        }

        /// <summary>Sets the byte array to display in the viewer.</summary>
        /// <param name="bytes">The byte array to display. </param>
        /// <exception cref="T:System.ArgumentNullException">The specified byte array is null. </exception>
        public virtual void SetBytes(byte[] bytes)
        {
            if (_dataBuf != null)
            {
                _dataBuf = null;
            }
            _dataBuf = bytes ?? throw new ArgumentNullException("bytes");
            InitState();
            SetDisplayMode(_displayMode);
        }

        /// <summary>Sets the current display mode.</summary>
        /// <param name="mode">The display mode to set. </param>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified display mode is not from the <see cref="T:System.ComponentModel.Design.DisplayMode" /> enumeration. </exception>
        public virtual void SetDisplayMode(DisplayMode mode)
        {
            if (!IsEnumValid(mode, (int)mode, 1, 4))
            {
                throw new InvalidEnumArgumentException("mode", (int)mode, typeof(DisplayMode));
            }
            _displayMode = mode;
            switch (_displayMode)
            {
                case DisplayMode.Ansi:
                    InitAnsi();
                    SuspendLayout();
                    _edit.Show();
                    _scrollBar.Hide();
                    ResumeLayout();
                    Invalidate();
                    break;
                case DisplayMode.Unicode:
                    InitUnicode();
                    SuspendLayout();
                    _edit.Show();
                    _scrollBar.Hide();
                    ResumeLayout();
                    Invalidate();
                    break;
                // Auto detection doesn't really work well, so just default to hexdump mode.
                case DisplayMode.Auto:
                case DisplayMode.Hexdump:
                    SuspendLayout();
                    _edit.Hide();
                    if (_linesCount > _rowCount)
                    {
                        if (!_scrollBar.Visible)
                        {
                            _scrollBar.Show();
                            ResumeLayout();
                            _scrollBar.Invalidate();
                            //               scrollBar.Select();
                            Select();

                        }
                        else
                        {
                            ResumeLayout();
                        }
                    }
                    else
                    {
                        ResumeLayout();
                    }
                    break;
            }
        }

        /// <summary>Sets the file to display in the viewer.</summary>
        /// <param name="path">The file path to load from. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="path" /> is null. </exception>
        /// <exception cref="T:System.ArgumentException">
        ///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
        /// <exception cref="T:System.IO.IOException">The file load failed. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The access requested is not permitted by the operating system for the specified <paramref name="path" />, such as when access is Write or ReadWrite and the file or directory is set for read-only access. </exception>
        public virtual void SetFile(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
            try
            {
                int num = (int)fileStream.Length;
                byte[] array = new byte[num + 1];
                fileStream.Read(array, 0, num);
                SetBytes(array);
                fileStream.Close();
            }
            catch
            {
                fileStream.Close();
                throw;
            }
        }

        /// <summary>Sets the current line for the <see cref="F:System.ComponentModel.Design.DisplayMode.Hexdump" /> view.</summary>
        /// <param name="line">The current line to display from. </param>
        public virtual void SetStartLine(int line)
        {
            if (line < 0 || line >= _linesCount || line > _dataBuf.Length / _columnCount)
            {
                _startLine = 0;
            }
            else
            {
                _startLine = line;
            }
        }
        #endregion

        #region Internal
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern int MultiByteToWideChar(int codePage, int dwFlags, byte[] lpMultiByteStr,
            int cchMultiByte, char[] lpWideCharStr, int cchWideChar);
        #endregion
    }
}
#endif // NETFRAMEWORK 