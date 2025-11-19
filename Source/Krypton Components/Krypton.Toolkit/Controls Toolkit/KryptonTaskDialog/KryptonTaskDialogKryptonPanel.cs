#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Internally used by KryptonTaskDialogElementBase
/// </summary>
[Browsable(false)]
[ToolboxItem(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public class KryptonTaskDialogKryptonPanel : KryptonPanel
{
    #region Fields
    private (int FontMinValue, int FontMaxValue, int Adjust75, int Adjust50, int Adjust25) _separatorValues;
    private KryptonTaskDialogDefaults _taskDialogDefaults;
    private Bitmap _separatorImage;
    private PaletteBase _palette;
    private bool _disposed;
    #endregion

    #region Identity
    public KryptonTaskDialogKryptonPanel(KryptonTaskDialogDefaults taskDialogDefaults)
    {
        _disposed = false;
        _taskDialogDefaults = taskDialogDefaults;

        // initialise only, the real assigment will be done from KryptonTaskDialogElementBase
        _separatorImage = new Bitmap(1,1);
        ShowSeparator = false;

        _separatorValues.FontMinValue = 50;
        _separatorValues.FontMaxValue = 255 - 50;
        _separatorValues.Adjust75 = 75;
        _separatorValues.Adjust50 = 50;
        _separatorValues.Adjust25 = 25;

        // Subscribe to theme changes
        _palette = KryptonManager.CurrentGlobalPalette;
        KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;
        // Set the initial separator image
        UpdateSeparatorImage();
    }
    #endregion

    #region Override
    /// <inheritdoc/>
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (ShowSeparator)
        {
            e.Graphics.DrawImage(_separatorImage, _taskDialogDefaults.PanelLeft, 0);
        }
    }
    #endregion

    #region Public
    /// <summary>
    /// Displays a separator line at the top of the element.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowSeparator
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;
                Invalidate(true);
            }
        }
    }
    #endregion

    #region Private
    private void UpdateSeparatorImage()
    {
        (Color color1, Color color2) = GetSeparatorColors();

        int width = _taskDialogDefaults.ClientWidth - _taskDialogDefaults.PanelLeft - _taskDialogDefaults.PanelRight;
        Rectangle rectangle       = new(0, 0, width, 2);
        Rectangle rectangleTop    = new(0, 0, width, 1);
        Rectangle rectangleBottom = new(0, 1, width, 1);

        using Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height);
        using Brush brush1 = new SolidBrush(color1);
        using Brush brush2 = new SolidBrush(color2);
        using Graphics graphics = Graphics.FromImage(bitmap);

        graphics.FillRectangle(brush1, rectangleTop);
        graphics.FillRectangle(brush2, rectangleBottom);

        _separatorImage = bitmap.Clone(rectangle, bitmap.PixelFormat);
        Invalidate();
    }

    private (Color, Color) GetSeparatorColors()
    {
        Color color1;
        Color color2;

        // If BackColor1 has been set use this for the separator, otherwise fall back to theme colours.
        Color tmp = this.StateCommon.Color1 != Color.Empty
            ? this.StateCommon.Color1
            : _palette.GetBackColor1(PaletteBackStyle.PanelClient, PaletteState.Normal);

        if (tmp.R > _separatorValues.FontMaxValue && tmp.G > _separatorValues.FontMaxValue && tmp.B > _separatorValues.FontMaxValue)
        {
            // If the colour reaches an upper bound it can't be made lighter.
            color1 = Color.FromArgb(tmp.R - _separatorValues.Adjust75, tmp.G - _separatorValues.Adjust75, tmp.B - _separatorValues.Adjust75);
            color2 = Color.White;
        }
        else if (tmp.R < _separatorValues.FontMinValue && tmp.G < _separatorValues.FontMinValue && tmp.B < _separatorValues.FontMinValue)
        {
            // If the colour reaches a lower bound, it can't be made darker.
            color2 = ControlPaint.Light(tmp);
            color1 = ControlPaint.Light(Color.LightSlateGray);
        }
        else if (tmp.R > 150 && tmp.G > 150 && tmp.B > 150)
        {
            // Colours that are somewhere in the middle need different handling
            int r = Math.Min(tmp.R - _separatorValues.Adjust50, 255);
            int g = Math.Min(tmp.G - _separatorValues.Adjust50, 255);
            int b = Math.Min(tmp.B - _separatorValues.Adjust50, 255);

            color1 = Color.FromArgb(r, g, b);
            color2 = Color.LightGray;
        }
        else
        {
            color1 = ControlPaint.Dark(tmp);
            color2 = ControlPaint.Light(tmp);
        }

        return (color1, color2);
    }

    private void OnGlobalPaletteChanged(object? sender, EventArgs e)
    {
        _palette = KryptonManager.CurrentGlobalPalette;
        UpdateSeparatorImage();
    }
    #endregion

    #region IDipose
    protected override void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
            _disposed = true;
        }

        base.Dispose(disposing);
    }
    #endregion
}