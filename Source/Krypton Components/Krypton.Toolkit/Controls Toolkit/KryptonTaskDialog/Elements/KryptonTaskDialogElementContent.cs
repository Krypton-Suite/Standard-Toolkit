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

public class KryptonTaskDialogElementContent : KryptonTaskDialogElementBase,
    IKryptonTaskDialogElementContent,
    IKryptonTaskDialogElementForeColor,
    IKryptonTaskDialogElementEventSizeChanged
{
    #region Fields
    // Content text
    KryptonWrapLabel _textControl;
    // Ttextbox width
    int _textBoxWidth;
    #endregion

    #region Events
    /// <summary>
    /// Subscribers will be notified when size of the element has changed.
    /// </summary>
    public event Action SizeChanged;
    #endregion 

    public KryptonTaskDialogElementContent()
    {
        Panel.Height = 120;
        _textBoxWidth = KryptonTaskDialog.Defaults.ClientWidth - KryptonTaskDialog.Defaults.PanelLeft - KryptonTaskDialog.Defaults.PanelRight;

        _textControl = new()
        {
            AutoSize = false,
            Width = _textBoxWidth,
            Height = 0,
            Padding = new Padding(0),
            Margin = new Padding(0),
            Location = new Point(10, 10),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
        };

        Panel.Controls.Add(_textControl);
    }

    private void OnSizeChanged()
    {
        Font font = _textControl.StateCommon.Font ?? KryptonManager.CurrentGlobalPalette.BaseFont;

        using Graphics g = Panel.CreateGraphics();
        var factorDpiY = g.DpiY / 96f;

        StringFormat sf = new();
        sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.NoClip;

        SizeF sizef = g.MeasureString(_textControl.Text, font, new SizeF(_textBoxWidth, float.MaxValue), sf);
        int height = (int)Math.Floor((double)(sizef.Height * factorDpiY));

        // Controls seem to need a little help here to stay within the correct bounds
        Panel.Height = height + KryptonTaskDialog.Defaults.PanelTop + KryptonTaskDialog.Defaults.PanelBottom;
        _textControl.Width = _textBoxWidth;
        _textControl.Height = ((int)Math.Floor(((double)sizef.Height) * factorDpiY));

        // Tell everybody about it
        SizeChanged?.Invoke();
    }

    /// <inheritdoc/>
    public string Text 
    {
        get => _textControl.Text;
        set
        {
            if (value is not null && _textControl.Text != value)
            {
                _textControl.Text = CommonHelper.NormalizeLineBreaks(value);
                OnSizeChanged();
            }
        }
    }

    public Color ForeColor 
    {
        get => _textControl.StateCommon.TextColor;
        set => _textControl.StateCommon.TextColor = value;
    }

    /// <summary>
    /// Not implemented
    /// </summary>
    /// <returns>String.Empty</returns>
    public override string ToString()
    {
        return string.Empty;
    }
}