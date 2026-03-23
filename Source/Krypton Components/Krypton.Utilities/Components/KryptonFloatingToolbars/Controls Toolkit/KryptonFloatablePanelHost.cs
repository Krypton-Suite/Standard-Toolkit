#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

public class KryptonFloatablePanelHost : ToolStripPanel
{
    #region Instance Fields

    private KryptonFloatableMenuStrip? _floatableMenuStrip;

    private KryptonFloatableToolStrip? _floatableToolStrip;

    private Rectangle _activeArea;

    #endregion

    #region Public

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonFloatableMenuStrip? KryptonFloatableMenuStrip { get => _floatableMenuStrip; set => _floatableMenuStrip = value; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonFloatableToolStrip? KryptonFloatableToolStrip { get => _floatableToolStrip; set => _floatableToolStrip = value; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Rectangle ActiveArea => _activeArea;

    #endregion

    #region Overrides

    /// <inheritdoc />
    protected override void OnControlAdded(ControlEventArgs e)
    {
        if (KryptonFloatableMenuStrip != null)
        {
            if (e.Control is MenuStrip ms)
            {
                if (Orientation == Orientation.Horizontal)
                {
                    ms.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
                }
                else
                {
                    ms.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
                }
            }
        }
        else if (KryptonFloatableToolStrip != null)
        {
            if (e.Control is ToolStrip ts)
            {
                if (Orientation == Orientation.Horizontal)
                {
                    ts.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
                }
                else
                {
                    ts.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
                }
            }
        }

        base.OnControlAdded(e);
    }

    /// <inheritdoc />
    protected override void OnSizeChanged(EventArgs e)
    {
        _activeArea = ClientRectangle;

        if (_activeArea.Width < 23 || _activeArea.Height < 23)
        {
            if (Orientation == Orientation.Horizontal)
            {
                _activeArea.Height = 23;
            }
            else
            {
                _activeArea.Width = 23;
            }

            switch (base.Dock)
            {
                case DockStyle.Bottom:
                    _activeArea.Y -= 23;
                    break;
                case DockStyle.Left:
                    _activeArea.X += 23;
                    break;
                case DockStyle.Right:
                    _activeArea.X -= 23;
                    break;
            }
        }

        base.OnSizeChanged(e);
    }

    #endregion
}