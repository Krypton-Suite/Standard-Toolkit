#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public class KryptonTaskDialogElementContentTest : KryptonTaskDialogElementBase,
    IKryptonTaskDialogElementText,
    IKryptonTaskDialogElementDescription,
    IKryptonTaskDialogElementForeColor,
    IKryptonTaskDialogElementEventSizeChanged
{
    #region Fields
    // Content text
    KryptonWrapLabel _description;
    KryptonWrapLabel _textControl;
    // Ttextbox width
    int _tlpWidth;
    TableLayoutPanel _tlp;
    bool _disposed = false;
    #endregion

    public KryptonTaskDialogElementContentTest(KryptonTaskDialogDefaults taskDialogDefaults) : base(taskDialogDefaults)
    {
        Panel.Height = 120;
        Panel.Width = Defaults.ClientWidth;
        _tlpWidth = Defaults.ClientWidth - Defaults.PanelLeft - Defaults.PanelRight;

        _tlp = new()
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Padding = new Padding(0, 0, 0, 0),
            Margin = new Padding(0),
            Size = new Size(_tlpWidth, 0),
            Location = new Point(Defaults.PanelLeft, Defaults.PanelTop)
        };
        _tlp.SetDoubleBuffered(true);
        _tlp.RowStyles.Clear();
        _tlp.ColumnStyles.Clear();

        _tlp.ColumnCount = 1;
        _tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

        _tlp.RowCount = 2;
        _tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        _textControl = new()
        {
            AutoSize = true,
            Padding = new Padding(0),
            Margin = new Padding(0, 0, 0, 5),
        };

        _description = new()
        {
            AutoSize = true,
            //AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Padding = new Padding(0),
            Margin = new Padding(0, 0, 0, 5),
        };

        // Description is on by default
        ShowDescription = true;


        Panel.Controls.Add(_tlp);
        //Panel.Controls.Add(_description);
        //Panel.Controls.Add(_textBox);

        _tlp.Controls.Add(_description, 0, 0);
        _tlp.Controls.Add(_textControl, 0, 1);

        OnSizeChanged();
    }

    protected override void OnPalettePaint(object? sender, PaletteLayoutEventArgs e)
    {
        base.OnPalettePaint(sender, e);
        OnSizeChanged();
    }

    internal override void PerformLayout()
    {
        base.PerformLayout();
        OnSizeChanged(true);
    }

    protected override void OnSizeChanged(bool performLayout = false)
    {
        if (Visible || performLayout)
        {
            using Graphics g = _textControl.CreateGraphics();

            StringFormat sf = new();
            sf.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap | StringFormatFlags.MeasureTrailingSpaces;

            Font font = Palette.GetContentShortTextFont(PaletteContentStyle.LabelNormalControl, PaletteState.Normal)!;

            var sizeTC = g.MeasureString(_textControl.Text, font, int.MaxValue, sf);
            var sizeDS = g.MeasureString(_description.Text, font, int.MaxValue, sf);

            int height = Math.Max(((int)Math.Ceiling((double)sizeTC.Height)), ((int)Math.Ceiling((double)sizeDS.Height)));
            _textControl.Height = height;
            _description.Height = ShowDescription ? height : 0;

            _tlp.Refresh();

            Panel.Height = Defaults.PanelTop + Defaults.PanelBottom;
            Panel.Height += height + _textControl.Margin.Top + _textControl.Margin.Bottom;
            Panel.Height += _description.GetDesiredVisibility() ? height + _description.Margin.Top + _description.Margin.Bottom : 0;

            //// Tell everybody about it
            base.OnSizeChanged(performLayout);
        }
    }

    /// <inheritdoc/>
    public string Text 
    {
        get => _textControl.Text;

        set
        {
            if (_textControl.Text != value)
            {
                _textControl.Text = CommonHelper.NormalizeLineBreaks(value);
                OnSizeChanged();
            }
        }
    }

    public Color ForeColor 
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;
                _textControl.StateCommon.TextColor = value;
                _description.StateCommon.TextColor = value;
            }
        }
    }
    
    public string Description 
    {        
        get => _description.Text;
        set
        {
            _description.Text = value;
            OnSizeChanged();
        }
    }
    
    public bool ShowDescription 
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;
                _description.Visible = value;
                OnSizeChanged();
            }
        }
    }

    public override bool Visible 
    {
        get => base.Visible; 
        set
        {
            base.Visible = value;
            OnSizeChanged();
        }
    }

    protected override void Dispose(bool disposing)
    {
        if(!_disposed && disposing)
        {
            _disposed = false;
        }

        base.Dispose(disposing);
    }
}