#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

[Serializable]
public class KryptonMenuStripPanelExtended : ToolStripPanel
{
    #region Instance Fields

    private KryptonFloatableMenuStrip _floatableMenuStrip;

    private Rectangle _activeRectangle;

    #endregion

    #region Public

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonFloatableMenuStrip KryptonFloatableMenuStrip { get => _floatableMenuStrip; set => _floatableMenuStrip = value; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Rectangle ActiveRectangle => _activeRectangle;

    #endregion

    #region Identity

    public KryptonMenuStripPanelExtended()
    {
        //MenuStrip = FloatableMenuStrip;
    }

    #endregion

    #region Overrides

    /// <inheritdoc />
    protected override void OnCreateControl() => base.OnCreateControl();

    /// <inheritdoc />
    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);

        _activeRectangle = ClientRectangle;

        if (_activeRectangle.Width < 23 || _activeRectangle.Height < 23)
        {
            if (Orientation == Orientation.Horizontal)
            {
                _activeRectangle.Height = 23;
            }
            else
            {
                _activeRectangle.Width = 23;
            }

            switch (base.Dock)
            {
                case DockStyle.Bottom:
                    _activeRectangle.Y -= 23;
                    break;
                case DockStyle.Left:
                    _activeRectangle.X += 23;
                    break;
                case DockStyle.Right:
                    _activeRectangle.X -= 23;
                    break;
            }
        }
    }

    /// <inheritdoc />
    protected override void OnControlAdded(ControlEventArgs e)
    {
        base.OnControlAdded(e);

        if (e.Control is MenuStrip menuStrip)
        {
            if (Orientation == Orientation.Horizontal)
            {
                menuStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            }
            else
            {
                menuStrip.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
            }
        }
    }
    #endregion
}