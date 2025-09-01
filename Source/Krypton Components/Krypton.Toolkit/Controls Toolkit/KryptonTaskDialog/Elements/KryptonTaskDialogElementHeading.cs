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
    #endregion

    #region Identity
    public KryptonTaskDialogElementHeading() 
    {
        Panel.Height = 48 + KryptonTaskDialog.Defaults.PanelTop + KryptonTaskDialog.Defaults.PanelBottom;
        Panel.Width = KryptonTaskDialog.Defaults.ClientWidth;

        _iconController = new();
        IconType = KryptonTaskDialogIconType.None;

        _pictureBox = new();
        _pictureBox.Size = new Size(48, 48);
        _pictureBox.Padding = new(0);
        _pictureBox.Margin = new(0);
        _pictureBox.BorderStyle = BorderStyle.None;
        _pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
        _pictureBox.Location = new Point(KryptonTaskDialog.Defaults.PanelLeft, KryptonTaskDialog.Defaults.PanelTop);
        _pictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;

        _headingText = new();
        _headingText.AutoSize = false;
        _headingText.Width = Panel.Width - KryptonTaskDialog.Defaults.PanelLeft - _pictureBox.Width - KryptonTaskDialog.Defaults.PanelRight - KryptonTaskDialog.Defaults.ComponentSpace;
        _headingText.Height = 48;
        _headingText.Left = KryptonTaskDialog.Defaults.PanelLeft + _pictureBox.Width + KryptonTaskDialog.Defaults.ComponentSpace;
        _headingText.Top = KryptonTaskDialog.Defaults.PanelTop;
        _headingText.StateCommon.ShortText.Font = new Font(KryptonManager.CurrentGlobalPalette.BaseFont.FontFamily, 20f, FontStyle.Bold);
        _headingText.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;

        Panel.Controls.Add(_pictureBox);
        Panel.Controls.Add(_headingText);

        Bitmap b = new Bitmap(48, 48);
        using var g = Graphics.FromImage(b);
        using Brush brush = new SolidBrush(Color.Red);
        g.FillRectangle(brush, 0, 0, 48, 48);

        _pictureBox.Image = b;
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
    public KryptonTaskDialogIconType IconType {
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

    /// <summary>
    /// Not implemented
    /// </summary>
    /// <returns>String.Empty</returns>
    public override string ToString()
    {
        return string.Empty;
    }
    #endregion

    #region Private
    private void UpdateHeaderIcon()
    {
        _pictureBox.Image = IconType != KryptonTaskDialogIconType.None
            ? _iconController.GetImage(IconType, _pictureBox.Size.Height)
            : null;
    }
    #endregion
}