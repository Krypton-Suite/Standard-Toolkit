#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public partial class KryptonTaskDialogElementFooterBar : KryptonTaskDialogElementBase,
    IKryptonTaskDialogElementForeColor,
    IKryptonTaskDialogElementRoundedCorners
{
    #region Fields
    // default text format flags
    private const TextFormatFlags textFormatFlags = TextFormatFlags.WordBreak | TextFormatFlags.NoPadding | TextFormatFlags.ExpandTabs;

    // incoming expander instance
    private KryptonTaskDialogElementContent _expander;
    // Panel to layout the controls
    private TableLayoutPanel _tlp;
    // Dialog form
    private KryptonForm _form;
    // Expander button
    private KryptonButton _expanderButton;
    // Expander text
    private KryptonLabel _expanderText;
    // Image / Icon controller
    private KryptonTaskDialogIconController _iconController;
    // Footnote picturebox,
    private PictureBox _footNotePictureBox;
    // Footnote label
    private KryptonWrapLabel _footNoteText;
    // Disposal
    protected bool _disposed;
    // Margin on the right to evenly space controls
    private Padding _spacerPadding;
    // Button sizing
    private Font _font;
    private string _dummyText;
    private Size _buttonSize;
    // Buttons
    private List<KryptonButton> _buttons;
    private KryptonButton _btnOk;
    private KryptonButton _btnYes;
    private KryptonButton _btnNo;
    private KryptonButton _btnCancel;
    private KryptonButton _btnRetry;
    private KryptonButton _btnAbort;
    private KryptonButton _btnIgnore;

    #endregion

    #region Identity
    public KryptonTaskDialogElementFooterBar(KryptonTaskDialogDefaults taskDialogDefaults, KryptonTaskDialogElementContent expander, KryptonForm kryptonForm) 
        : base(taskDialogDefaults)
    {
        _spacerPadding = new Padding(0, 0, Defaults.ComponentSpace, 0);

        _form = kryptonForm;
        _buttons = [];

        _iconController = new();
        _expander = expander;
        _expanderButton = new();
        _expanderText = new();
        _footNotePictureBox = new();
        _footNoteText = new();
        _disposed = false;

        CommonButtons = new KryptonTaskDialogElementFooterBarCommonButtonProperties();
        CommonButtons.PropertyChanged += OnCommonButtonsPropertyChanged;
        RoundedCorners = false;

        // default values
        Footer = new KryptonTaskDialogElementFooterBarFooterProperties();
        Footer.PropertyChanged += OnFooterBarPropertyChanged;
        Footer.ExpanderExpandedText = "Expand";
        Footer.ExpanderCollapsedText = "Collapse";

        // FootNote default IconType
        Footer.IconType = KryptonTaskDialogIconType.None;

        // init 
        SetupPanel();

        // Button defaults
        CommonButtons.Buttons = KryptonTaskDialogCommonButtonTypes.OK | KryptonTaskDialogCommonButtonTypes.Cancel;
        CommonButtons.AcceptButton = KryptonTaskDialogCommonButtonTypes.OK;
        CommonButtons.CancelButton = KryptonTaskDialogCommonButtonTypes.Cancel;

        // Text sample to measure the string length
        _dummyText = new string('B', _buttons.Max(b => b.Text.Length) + 1);

        // Set the state
        UpdateExpanderEnabledState();
        UpdateExpanderImage();
        UpdateFootNoteIcon();
        UpdateForeColor();
        UpdateButtonSize();

        // Visible by default
        Visible = true;

        // Update at the next request
        LayoutDirty = true;
        PerformLayout();
    }
    #endregion

    #region Public
    /// <summary>
    /// Common Buttons properties
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonTaskDialogElementFooterBarCommonButtonProperties CommonButtons { get; }

    /// <summary>
    /// Footer properties
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KryptonTaskDialogElementFooterBarFooterProperties Footer { get; }

    /// <inheritdoc/>
    public virtual Color ForeColor 
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;
                UpdateForeColor();
            }
        }
    }
    #endregion

    #region Private CommonButtons
    private void UpdateButtonSize()
    {
        // Obtain the current font.
        _font = _btnOk.StateCommon.Content.GetContentShortTextFont(PaletteState.Normal) ?? KryptonManager.CurrentGlobalPalette.BaseFont;
        _buttonSize = AccurateText.MeasureString(Panel.CreateGraphics(), Panel.RightToLeft, _dummyText, _font, PaletteTextTrim.Character, 
            PaletteRelativeAlign.Center, PaletteTextHotkeyPrefix.None, TextRenderingHint.AntiAlias, false).Size;

        // KButton does always restrict itself to the MinimumSize
        _buttonSize.Width = Math.Max(_buttonSize.Width, 75);
        _buttonSize.Height = Math.Max(_buttonSize.Height, 24);
        _buttons.ForEach(b => b.ClientSize = _buttonSize);
    }

    private void CreateButtons()
    {
        // Create and add the buttons to the table layout panel.
        _btnOk     = CreateButton("OK",     DialogResult.OK,        4);
        _btnYes    = CreateButton("Yes",    DialogResult.Yes,       5);
        _btnNo     = CreateButton("No",     DialogResult.No,        6);
        _btnCancel = CreateButton("Cancel", DialogResult.Cancel,    7);
        _btnRetry  = CreateButton("Retry",  DialogResult.Retry,     8);
        _btnAbort  = CreateButton("Abort",  DialogResult.Abort,     9);
        _btnIgnore = CreateButton("Ignore", DialogResult.Ignore,   10);

        // Add them all to the list
        _buttons.Add(_btnOk);
        _buttons.Add(_btnYes);
        _buttons.Add(_btnNo);
        _buttons.Add(_btnCancel);
        _buttons.Add(_btnRetry);
        _buttons.Add(_btnAbort);
        _buttons.Add(_btnIgnore);
    }

    private KryptonButton CreateButton(string text, DialogResult dialogResult, int index)
    {
        var button = new KryptonButton()
        {
            AutoSize = false,
            Text = text,
            DialogResult = dialogResult,
            Padding = Defaults.NullPadding,
            Margin = new Padding(5, 0, 0, 0),
            MinimumSize = new Size(75, 24)
        };
        button.StateCommon.Content.ShortText.TextH = PaletteRelativeAlign.Center;
        button.StateCommon.Content.ShortText.TextV = PaletteRelativeAlign.Center;
        button.Click += ButtonClick;
        _tlp.Controls.Add(button, index, 0);

        return button;
    }

    private void ButtonClick(object? sender, EventArgs e)
    {
        if (sender is KryptonButton button)
        {
            _form.DialogResult = button.DialogResult;
            _form.Hide();
        }
    }

    internal void OnCommonButtonsChanged()
    {
        // Reset the accept and cancel buttons on the form
        _form.AcceptButton = null;
        _form.CancelButton = null;

        // Update button's state
        SetButton(_btnOk, KryptonTaskDialogCommonButtonTypes.OK);
        SetButton(_btnYes, KryptonTaskDialogCommonButtonTypes.Yes);
        SetButton(_btnNo, KryptonTaskDialogCommonButtonTypes.No);
        SetButton(_btnCancel, KryptonTaskDialogCommonButtonTypes.Cancel);
        SetButton(_btnRetry, KryptonTaskDialogCommonButtonTypes.Retry);
        SetButton(_btnAbort, KryptonTaskDialogCommonButtonTypes.Abort);
        SetButton(_btnIgnore, KryptonTaskDialogCommonButtonTypes.Ignore);
    }

    private void SetButton(KryptonButton button, KryptonTaskDialogCommonButtonTypes buttonType)
    {
        button.Visible = (CommonButtons.Buttons & buttonType) == buttonType;

        // If AcceptButton and CancelButton are the same, Accept wins.
        if (buttonType == CommonButtons.AcceptButton)
        {
            _form.AcceptButton = button;
            button.Select();
        }
        else if (buttonType == CommonButtons.CancelButton)
        {
            _form.CancelButton = button;
        }
    }
    #endregion

    #region Private
    private void OnFooterBarPropertyChanged(FooterBarProperties property)
    {
        if (property == FooterBarProperties.FootNoteText)
        {
            UpdateFootNoteText();
        }
        else if (property is  FooterBarProperties.ExpanderExpandedText or FooterBarProperties.ExpanderCollapsedText)
        {
            UpdateExpanderText();
        }
        else if (property == FooterBarProperties.EnableExpanderControls)
        {
            UpdateExpanderEnabledState();
        }
        else if (property == FooterBarProperties.IconType)
        {
            UpdateFootNoteIcon();
        }
        else
        {
            throw new ArgumentOutOfRangeException($"Unknown FooterBarProperties member: {property}");
        }

        LayoutDirty = true;
        OnSizeChanged();
    }

    private void OnCommonButtonsPropertyChanged(CommonButtonsProperties property)
    {
        if (property is CommonButtonsProperties.Buttons or CommonButtonsProperties.AcceptButton or CommonButtonsProperties.CancelButton)
        {
            OnCommonButtonsChanged();
        }
        else
        {
            throw new ArgumentOutOfRangeException($"Unknown CommonButtonsProperties member: {property}");
        }

        LayoutDirty = true;
        OnSizeChanged();
    }

    private void UpdateFootNoteText()
    {
        _footNoteText.Text = Footer.FootNoteText;
        _footNoteText.Invalidate();
    }

    private void UpdateFootNoteIcon()
    {
        if (Footer.IconType != KryptonTaskDialogIconType.None)
        {
            _footNotePictureBox.Image = _iconController.GetImage(Footer.IconType, _btnOk.Height);
            _footNotePictureBox.Visible = true;
        }
        else
        {
            _footNotePictureBox.Visible = false;
            _footNotePictureBox.Image = null;
        }
    }

    private void UpdateForeColor()
    {
        _expanderText.StateCommon.ShortText.Color1 = ForeColor;
        _footNoteText.StateCommon.TextColor = ForeColor;

        _expanderText.Invalidate();
        _footNoteText.Invalidate();
    }

    private void UpdateExpanderImage()
    {
        if (Footer.EnableExpanderControls)
        {
            _expanderButton.StateCommon.Back.Image = _expander.Visible
                ? _iconController.GetImage(KryptonTaskDialogIconType.ArrowGrayUp, _btnOk.Height)
                : _iconController.GetImage(KryptonTaskDialogIconType.ArrowGrayDown, _btnOk.Height);

            _expanderButton.Invalidate();
        }
    }

    private void UpdateExpanderText()
    {
        if (Footer.EnableExpanderControls)
        {
            _expanderText.Text = _expander.Visible
                ? Footer.ExpanderCollapsedText
                : Footer.ExpanderExpandedText;

            _expanderText.Invalidate();
        }
    }

    private void UpdateExpanderEnabledState()
    {
        _expanderButton.Visible = Footer.EnableExpanderControls;
        _expanderText.Visible = Footer.EnableExpanderControls;

        UpdateExpanderText();
        UpdateExpanderImage();
    }

    private void OnExpanderButtonClick(object? sender, EventArgs e)
    {
        // briefly detach from the event
        WireExpanderVisibleChanged(false);

        // change mode
        _expander.Visible = !_expander.Visible;

        // change the button icon & text
        UpdateExpanderImage();
        UpdateExpanderText();

        // attach the event again
        WireExpanderVisibleChanged(true);
    }

    private void OnExpanderVisibleChanged()
    {
        UpdateExpanderEnabledState();
    }

    private void WireExpanderVisibleChanged(bool wireEvent)
    {
        if (wireEvent)
        {
            _expander.VisibleChanged += OnExpanderVisibleChanged;
        }
        else
        {
            _expander.VisibleChanged -= OnExpanderVisibleChanged;
        }
    }

    private void SetupTableLayoutPanel()
    {
        _tlp = new()
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Width = Defaults.TLP.StdMaxSize.Width,
            Padding = Defaults.NullPadding,
            Margin = Defaults.NullMargin,
            BackColor = Color.Transparent,
            CellBorderStyle = TableLayoutPanelCellBorderStyle.None
        };
        _tlp.SetDoubleBuffered(true);

        // 1 row
        _tlp.RowCount = 1;
        _tlp.ColumnStyles.Clear();
        _tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        // 11 columns, 4 for the footer and for the buttons 7
        _tlp.ColumnCount = 11;
        _tlp.RowStyles.Clear();
        // expander button
        _tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        // expander text
        _tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        // footnote icon
        _tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        // footnote text
        _tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        // 7 CommonButtons
        for (int i = 0; i < _tlp.ColumnCount - 1; i++)
        {
            _tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        }

        // Add the controls to the layout
        _tlp.Controls.Add(_expanderButton, 0, 0);
        _tlp.Controls.Add(_expanderText, 1, 0);
        _tlp.Controls.Add(_footNotePictureBox, 2, 0);
        _tlp.Controls.Add(_footNoteText, 3, 0);
    }

    private void SetupControls()
    {
        // configure controls
        _expanderButton.AutoSize = false;
        _expanderButton.Margin = _spacerPadding;
        _expanderButton.StateCommon.Back.ImageStyle = PaletteImageStyle.CenterMiddle;
        _expanderButton.StateCommon.Back.Color1 = Color.WhiteSmoke;
        _expanderButton.Text = string.Empty;
        _expanderButton.Click += OnExpanderButtonClick;

        _expanderText.AutoSize = true;
        _expanderText.Margin = _spacerPadding;
        _expanderText.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;
        _expanderText.StateCommon.ShortText.TextH = PaletteRelativeAlign.Near;

        _footNotePictureBox.Margin = _spacerPadding;
        _footNotePictureBox.Padding = Defaults.NullPadding;
        _footNotePictureBox.SizeMode = PictureBoxSizeMode.CenterImage;

        _footNoteText.AutoSize = true;
        _footNoteText.Dock = DockStyle.Fill;
        _footNoteText.Margin = Defaults.NullPadding;
        _footNoteText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

        // If the expander element changes visibility we need to react to that.
        WireExpanderVisibleChanged(true);
    }

    private void SetupPanel()
    {
        // Panel height, and padding
        Panel.Width = Defaults.ClientWidth;
        Panel.Padding = Defaults.PanelPadding1;
        
        SetupTableLayoutPanel();
        SetupControls();
        CreateButtons();
        Panel.Controls.Add(_tlp);

        // If the expander element changes visibility we need to react to that.
        WireExpanderVisibleChanged(true);
    }
    #endregion

    #region Overrides
    /// <inheritdoc/>
    protected override void OnSizeChanged(bool performLayout = false)
    {
        // Updates / changes are deferred if the element is not visible or until PerformLayout is called
        if (LayoutDirty && (Visible || performLayout))
        {
            // First set the size to zero so the images don't interfere with the tlp height
            _footNotePictureBox.Width = 0;
            _footNotePictureBox.Height = 0;
            _expanderButton.Width = 0;
            _expanderButton.Height = 0;

            // update common buttons
            UpdateButtonSize();

            // update the images first to make them of the same size.
            UpdateFootNoteIcon();
            UpdateExpanderImage();

            // and make'm visible, use _btnOk.Height for sizing.
            _footNotePictureBox.Width = _btnOk.Height;
            _footNotePictureBox.Height = _btnOk.Height;
            _expanderButton.Width = _btnOk.Height;
            _expanderButton.Height = _btnOk.Height;

            // Update FootNoteText size
            Font font = _footNoteText.StateCommon.Font
                ?? Palette.GetContentShortTextFont(PaletteContentStyle.LabelNormalControl, PaletteState.Normal)
                ?? KryptonManager.CurrentGlobalPalette.BaseFont;

            int height = TextRenderer.MeasureText(_footNoteText.Text, font, new SizeF(_footNoteText.Width, float.MaxValue).ToSize(), textFormatFlags).Height;

            // Controls seem to need a little help here to stay within the correct bounds
            _footNoteText.Height = height;

            // Size the panel. Add an extra PanelBottom to give it some more space at the border
            Panel.Height = Math.Max(_tlp.Height, height) + Defaults.PanelTop + Defaults.PanelBottom;

            // Tell everybody about it when visible.
            base.OnSizeChanged(performLayout);
            
            // Done
            LayoutDirty = false;
        }
    }

    /// <inheritdoc/>
    internal override void PerformLayout()
    {
        base.PerformLayout();
        OnSizeChanged(true);
    }

    /// <inheritdoc/>
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

        base.OnPalettePaint(sender, e);
    }

    /// <inheritdoc/>
    public override Color BackColor1 
    {
        get => base.BackColor1;
        set
        {
            base.BackColor1 = value;

            // labels need a little help
            _expanderText.Invalidate();
            _footNoteText.Invalidate();
        }
    }

    /// <inheritdoc/>
    public override Color BackColor2 
    {
        get => base.BackColor2;
        set
        {
            base.BackColor2 = value;

            // labels need a little help
            _expanderText.Invalidate();
            _footNoteText.Invalidate();
        }
    }

    /// <summary>
    /// Rounds the button corners.
    /// </summary>
    public bool RoundedCorners
    {
        get => field;

        set
        { 
            if (field != value)
            {
                field = value;
                int rounding = Defaults.GetCornerRouding(value);
                _buttons.ForEach(b => b.StateCommon.Border.Rounding = rounding);
                LayoutDirty = true;
                OnSizeChanged();
            }
        }
    }
    #endregion

    #region IDispose
    protected override void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            CommonButtons.PropertyChanged -= OnCommonButtonsPropertyChanged;
            Footer.PropertyChanged -= OnFooterBarPropertyChanged;
            _expanderButton.Click -= OnExpanderButtonClick;
            WireExpanderVisibleChanged(false);

            _iconController.Dispose();

            _disposed = true;
        }

        base.Dispose(disposing);
    }
    #endregion
}
