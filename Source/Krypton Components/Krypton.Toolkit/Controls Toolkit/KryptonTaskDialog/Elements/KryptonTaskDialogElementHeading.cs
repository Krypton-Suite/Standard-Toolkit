#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
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
    private KryptonWrapLabel _headingText;
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
        _height = 48;
        Panel.Height = _height + Defaults.PanelTop + Defaults.PanelBottom;
        Panel.Width = Defaults.ClientWidth;

        _iconController = new();
        _pictureBox = new();
        _headingText = new();
        _tlp = new();

        SetupPanel();

        IconType = KryptonTaskDialogIconType.None;
        TextAlignmentHorizontal = PaletteRelativeAlign.Near;
    }
    #endregion

    #region Public
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
        get => field;

        set
        {
            if (field != value)
            {
                field = value;
                UpdateTextAlignmentHorizontal();
            }
        }
    }

    /// <inheritdoc/>
    public Color ForeColor 
    {
        get => _headingText.StateCommon.TextColor;
        set => _headingText.StateCommon.TextColor = value;
    }
    #endregion

    #region Public override
    /// <inheritdoc/>
    public override Color BackColor1
    {
        get => base.BackColor1;

        set
        {
            if (base.BackColor1 != value)
            {
                base.BackColor1 = value;
                _headingText.Invalidate();
            }
        }
    }

    /// <inheritdoc/>
    public override Color BackColor2 
    {
        get => base.BackColor2;
        
        set
        {
            if (base.BackColor2 != value)
            {
                base.BackColor2 = value;
                _headingText.Invalidate();
            }
        }
    }
    #endregion

    #region Private
    private void UpdateTextAlignmentHorizontal()
    {
        _headingText.TextAlign = TextAlignmentHorizontal switch
        {
            PaletteRelativeAlign.Near => System.Drawing.ContentAlignment.MiddleLeft,
            PaletteRelativeAlign.Center => System.Drawing.ContentAlignment.MiddleCenter,
            PaletteRelativeAlign.Far => System.Drawing.ContentAlignment.MiddleRight,
            _ => System.Drawing.ContentAlignment.MiddleLeft
        };
    }

    private void SetupControls()
    {
        _pictureBox.Size = new Size( _height, _height );
        _pictureBox.Padding = new( 0 );
        _pictureBox.Margin = new( 0, 0, Defaults.ComponentSpace, 0 );
        _pictureBox.BorderStyle = BorderStyle.None;
        _pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
        _pictureBox.Visible = false;

        _headingText.AutoSize = false;
        _headingText.Margin = Defaults.NullMargin;
        _headingText.Dock = DockStyle.Fill;

        // theme based 
        //_headingText.StateCommon.ShortText.Font = new Font(KryptonManager.CurrentGlobalPalette.BaseFont.FontFamily, 20f, FontStyle.Bold);
        //_headingText.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;

        _headingText.StateCommon.Font = new Font( KryptonManager.CurrentGlobalPalette.BaseFont.FontFamily, 20f, FontStyle.Bold );
        _headingText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
    }

    private void SetupPanel()
    {
        SetupTableLayoutPanel();
        SetupControls();

        _tlp.Controls.Add(_pictureBox, 0, 0);
        _tlp.Controls.Add(_headingText, 1, 0);
        
        Panel.Controls.Add(_tlp);
    }

    private void SetupTableLayoutPanel()
    {
        _tlp.SetDoubleBuffered(true);
        _tlp.AutoSize        = false;
        _tlp.Height          = _height;
        _tlp.Width           = Defaults.ClientWidth - Defaults.PanelLeft - Defaults.PanelRight;
        
        _tlp.MinimumSize     = 
        _tlp.MaximumSize     = new Size(_tlp.Width, _height);

        _tlp.Top             = Defaults.PanelTop;
        _tlp.Left            = Defaults.PanelLeft;
        _tlp.Padding         = Defaults.NullPadding;
        _tlp.Margin          = Defaults.NullMargin;          
        
        _tlp.RowCount        = 1;
        _tlp.ColumnCount     = 2;
        _tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
        _tlp.BackColor       = Color.Transparent;

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