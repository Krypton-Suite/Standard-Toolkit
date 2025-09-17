#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 * © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public class KryptonTaskDialogElementHeading : KryptonTaskDialogElementBase,
   IKryptonTaskDialogElementIconType,
   IKryptonTaskDialogElementTextAlignmentHorizontal,
   IKryptonTaskDialogElementForeColor,
   IKryptonTaskDialogElementText
{
    #region Fields
    private KryptonPictureBox _pictureBox;
    private KryptonLabel _headingText;
    private KryptonTaskDialogIconController _iconController;
    private TableLayoutPanel _tlp;
    private int _height;
    private bool _disposed;
    #endregion

    #region Identity
    public KryptonTaskDialogElementHeading(KryptonTaskDialogDefaults taskDialogDefaults) 
        : base(taskDialogDefaults)
    {
        _disposed = false;
        _height         = 48;
        Panel.Height    = _height + Defaults.PanelTop + Defaults.PanelBottom;
        Panel.Width     = Defaults.ClientWidth;
        _iconController = new();
        IconType        = KryptonTaskDialogIconType.None;

        SetupPanel();
    }
    #endregion

    #region Public properties
    /// <inheritdoc/>
    public string Text 
    {
        get => _headingText.Text;
        set => _headingText.Text = value;
    }

    /// <inheritdoc/>
    public KryptonTaskDialogIconType IconType 
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;
                UpdateHeaderIcon();
            }
        }
    }

    /// <inheritdoc/>
    public PaletteRelativeAlign TextAlignmentHorizontal 
    {
        get => _headingText.StateCommon.ShortText.TextH;
        set => _headingText.StateCommon.ShortText.TextH = value;
    }

    /// <inheritdoc/>
    public Color ForeColor 
    {
        get => _headingText.StateCommon.ShortText.Color1;
        set => _headingText.StateCommon.ShortText.Color1 = value;
    }
    #endregion

    #region Public override
    /// <inheritdoc/>
    public override Color BackColor1
    {
        get => base.BackColor1;

        set
        {
            base.BackColor1 = value;
            _headingText.Invalidate();
        }
    }

    /// <inheritdoc/>
    public override Color BackColor2 
    {
        get => base.BackColor2;
        
        set
        {
            base.BackColor2 = value;
            _headingText.Invalidate();
        }
    }
    #endregion

    #region Private
    private void SetupPanel()
    {
        SetupTableLayoutPanel();

        _pictureBox             = new();
        _pictureBox.Size        = new Size(_height, _height);
        _pictureBox.Padding     = new(0);
        _pictureBox.Margin      = new(0, 0, Defaults.ComponentSpace, 0);
        _pictureBox.BorderStyle = BorderStyle.None;
        _pictureBox.SizeMode    = PictureBoxSizeMode.CenterImage;
        _pictureBox.Anchor      = AnchorStyles.Top | AnchorStyles.Left;

        _headingText = new();
        _headingText.AutoSize = false;
        _headingText.Margin = new(0);
        _headingText.Dock = DockStyle.Fill;

        // theme based 
        _headingText.StateCommon.ShortText.Font = new Font(KryptonManager.CurrentGlobalPalette.BaseFont.FontFamily, 20f, FontStyle.Bold);
        _headingText.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;

        _tlp.Controls.Add(_pictureBox, 0, 0);
        _tlp.Controls.Add(_headingText, 1, 0);
        
        Panel.Controls.Add(_tlp);
    }

    private void SetupTableLayoutPanel()
    {
        _tlp = new()
        {
            AutoSize        = false,
            Height          = _height,
            Width           = Defaults.ClientWidth - Defaults.PanelLeft - Defaults.PanelRight,
            Top             = Defaults.PanelTop,
            Left            = Defaults.PanelLeft,
            Padding         = new Padding(0),
            RowCount        = 1,
            ColumnCount     = 2,
            CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
        };

        _tlp.RowStyles.Clear();
        _tlp.ColumnStyles.Clear();

        _tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, _height));
        _tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

    }

    private void UpdateHeaderIcon()
    {
        if (IconType != KryptonTaskDialogIconType.None)
        {
            _pictureBox.Image = _iconController.GetImage(IconType, _pictureBox.Size.Height);
            _pictureBox.Visible = true;
        }
        else
        {
            _pictureBox.Image = null;
            _pictureBox.Visible = false;
        }
    }
    #endregion

    #region IDispose
    protected override void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _iconController.Dispose();

            _disposed = true;
        }

        base.Dispose(disposing);
    }
    #endregion
}