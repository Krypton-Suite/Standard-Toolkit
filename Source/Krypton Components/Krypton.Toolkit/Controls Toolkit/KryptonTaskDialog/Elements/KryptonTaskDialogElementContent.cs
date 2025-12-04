#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public partial class KryptonTaskDialogElementContent : KryptonTaskDialogElementBase,
    IKryptonTaskDialogElementContent,
    IKryptonTaskDialogElementForeColor
{
    #region Fields
    // default text format flags
    private const TextFormatFlags textFormatFlags = TextFormatFlags.WordBreak | TextFormatFlags.NoPadding | TextFormatFlags.ExpandTabs;

    private KryptonWrapLabel _textBox;
    private TableLayoutPanel _tlp;
    private KryptonPictureBox _pictureBox;
    private bool _disposed;
    #endregion

    #region Identity
    public KryptonTaskDialogElementContent(KryptonTaskDialogDefaults taskDialogDefaults)         
        : base(taskDialogDefaults)
    {
        _disposed = false;

        Panel.Height = 120;
        Panel.Padding = Defaults.PanelPadding1;

        ContentImage = new ContentImageStorage();
        ContentImage.PositionedLeft = true;
        ContentImage.PropertyChanged += OnContentImagePropertyChanged;

        _pictureBox = new();
        _tlp = new();
        _textBox = new();

        SetupPanel();

        LayoutDirty = true;
        OnSizeChanged();
    }
    #endregion

    #region Private
    private void SetupTableLayoutPanel()
    {
        _tlp.SetDoubleBuffered(true);
        _tlp.Left = Defaults.PanelLeft;
        _tlp.Top = Defaults.PanelTop;
        _tlp.AutoSize = true;
        _tlp.Margin = Defaults.PanelPadding1;
        _tlp.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _tlp.MaximumSize = Defaults.TLP.StdMaxSize;
        _tlp.MinimumSize = Defaults.TLP.StdMinSize;
        
        // Partition the tlp.
        _tlp.RowStyles.Clear();
        _tlp.ColumnStyles.Clear();
        
        _tlp.RowCount = 1;
        _tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        
        _tlp.ColumnCount = 3;
        _tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
    }

    private void SetupControls()
    {
        _pictureBox.Visible = false;
        _pictureBox.Margin = new Padding(0, 0, Defaults.ComponentSpace, 0);
        _pictureBox.Padding = Defaults.NullPadding;
        _pictureBox.BorderStyle = BorderStyle.None;
        _pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
        _pictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;

        _textBox.AutoSize = false;
        _textBox.Height = 100;
        _textBox.Padding = new Padding(0);
        _textBox.Margin = new Padding(0, 0, 0, 0);
        _textBox.Location = new Point(10, 10);
        _textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    }

    private void SetupPanel()
    {
        SetupControls();
        SetupTableLayoutPanel();
            
        // Put it together
        _tlp.Controls.Add(_textBox, 1, 0);
        _tlp.Controls.Add(_pictureBox, 0, 0);
        Panel.Controls.Add(_tlp);
    }

    private void OnContentImagePropertyChanged(ContentImageStorageProperties property)
    {
        if (property is ContentImageStorageProperties.Image or ContentImageStorageProperties.Size)
        {
            if (ContentImage.Image is not null)
            {
                // If the user has set width or height to zero the raw image size is used, whatever that may be.
                _pictureBox.Size = (ContentImage.Size.Width == 0 || ContentImage.Size.Height == 0)
                    ? ContentImage.Image.Size
                    : ContentImage.Size;

                _pictureBox.Image = new Bitmap(ContentImage.Image, _pictureBox.Size);
            }
        }
        else if (property == ContentImageStorageProperties.Visible)
        {
            _pictureBox.Visible = ContentImage.Image is not null && ContentImage.Visible;
        }
        else if (property == ContentImageStorageProperties.Position)
        {
            int columnIndex = ContentImage.PositionedLeft ? 0 : 2;

            _tlp.Controls.Remove(_pictureBox);
            _tlp.Controls.Add(_pictureBox, columnIndex, 0);
        }
        else
        {
            throw new ArgumentOutOfRangeException($"Unknown ContentImageStorageProperties member: {property}");
        }

        LayoutDirty = true;
        OnSizeChanged();
    }
    #endregion

    #region Protected/Internal
    /// <inheritdoc/>
    protected override void OnSizeChanged(bool performLayout = false)
    {
        // Updates / changes are deferred if the element is not visible or until PerformLayout is called
        if (LayoutDirty && (Visible || performLayout))
        {
            Font font = _textBox.StateCommon.Font
                ?? Palette.GetContentShortTextFont(PaletteContentStyle.LabelNormalControl, PaletteState.Normal)
                ?? KryptonManager.CurrentGlobalPalette.BaseFont;

            _textBox.Width = _pictureBox.GetDesiredVisibility()
                ? _tlp.MaximumSize.Width - _pictureBox.Width - _pictureBox.Margin.Horizontal
                : _tlp.MaximumSize.Width;

            _textBox.Height = TextRenderer.MeasureText(_textBox.Text, font, new SizeF(_textBox.Width, float.MaxValue).ToSize(), textFormatFlags).Height;

            Panel.Height = _tlp.Height + Defaults.PanelTop + Defaults.PanelBottom;

            // Tell everybody about it when visible.
            base.OnSizeChanged(performLayout);

            // Done
            LayoutDirty = false;
        }
    }

    protected override void OnPalettePaint(object? sender, PaletteLayoutEventArgs e)
    {
        base.OnPalettePaint(sender, e);

        // Flag dirty, and if visible call OnSizeChanged,
        // otherwise leave it deferred for a call from PerformLayout.
        LayoutDirty = true;
        if (Visible)
        {
            OnSizeChanged();
        }
    }

    /// <inheritdoc/>
    internal override void PerformLayout()
    {
        base.PerformLayout();
        OnSizeChanged(true);
    }

    /// <inheritdoc/>
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
        if (!_disposed && disposing)
        {
            ContentImage.PropertyChanged -= OnContentImagePropertyChanged;
        
            _disposed = true;
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Public
    /// <summary>
    /// Image to display together with the content.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonTaskDialogElementContent.ContentImageStorage ContentImage { get; }

    /// <inheritdoc/>
    public string Text 
    {
        get => _textBox.Text;

        set
        {
            if (_textBox.Text != value)
            {
                _textBox.Text = CommonHelper.NormalizeLineBreaks(value) + Environment.NewLine;
                LayoutDirty = true;
                OnSizeChanged();
            }
        }
    }

    public Color ForeColor 
    {
        get => _textBox.StateCommon.TextColor;
        set => _textBox.StateCommon.TextColor = value;
    }
    #endregion
}