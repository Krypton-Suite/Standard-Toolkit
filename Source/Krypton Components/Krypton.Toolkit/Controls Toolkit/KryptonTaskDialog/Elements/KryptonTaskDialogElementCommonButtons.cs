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

public class KryptonTaskDialogElementCommonButtons : KryptonTaskDialogElementBase,
    IKryptonTaskDialogElementCommonButtons
{
    #region Fields
    private bool _disposed;
    private KryptonForm _form;
    private TableLayoutPanel _tlp;
    // Button sizing
    private Font _font;
    private string _dummyText;
    private Size _buttonSize;
    // Buttons
    private List<KryptonButton> _buttons;
    private KryptonTaskDialogCommonButtonTypes _commonButtons;
    private KryptonButton _btnOk;
    private KryptonButton _btnYes;
    private KryptonButton _btnNo;
    private KryptonButton _btnCancel;
    private KryptonButton _btnRetry;
    private KryptonButton _btnAbort;
    private KryptonButton _btnIgnore;
    #endregion

    #region Identity
    public KryptonTaskDialogElementCommonButtons(KryptonTaskDialogDefaults taskDialogDefaults, KryptonForm kryptonForm) 
        : base(taskDialogDefaults)
    {
        _disposed = false;
        _form = kryptonForm;
        _buttons = [];
        RoundedCorners = false;

        Panel.Width = Defaults.ClientWidth;
        Panel.Padding = Defaults.PanelPadding1;
        
        SetupTableLayoutPanel();
        Panel.Controls.Add(_tlp);

        // Button defaults
        CreateButtons();
        Buttons = KryptonTaskDialogCommonButtonTypes.OK | KryptonTaskDialogCommonButtonTypes.Cancel;
        AcceptButton = KryptonTaskDialogCommonButtonTypes.OK;
        CancelButton = KryptonTaskDialogCommonButtonTypes.Cancel;

        // Text sample to measure the string length
        _dummyText = new string('B', _buttons.Max( b => b.Text.Length) + 1);
        UpdateButtonSize();

        // Request a layout before display
        LayoutDirty = true;
        PerformLayout();
    }
    #endregion

    private void UpdateButtonSize()
    {
        // Obtain the current font.
        _font = _btnOk!.StateCommon.Content.GetContentShortTextFont(PaletteState.Normal) ?? KryptonManager.CurrentGlobalPalette.BaseFont;
        _buttonSize = AccurateText.MeasureString(Panel.CreateGraphics(), Panel.RightToLeft, _dummyText, _font, 
            PaletteTextTrim.Character, PaletteRelativeAlign.Center, PaletteTextHotkeyPrefix.None, TextRenderingHint.AntiAlias, false).Size;

        // KButton does always restrict itself to the MinimumSize
        _buttonSize.Width = Math.Max(_buttonSize.Width, 75);
        _buttonSize.Height = Math.Max(_buttonSize.Height, 24);
        _buttons.ForEach(b => b.ClientSize = _buttonSize);
    }

    #region Protected/Internal
    /// <inheritdoc/>
    protected override void OnSizeChanged(bool performLayout = false)
    {
        // Updates / changes are deferred if the element is not visible or until PerformLayout is called
        if (LayoutDirty && (Visible || performLayout))
        {
            // Button size is set through UpdateButtonSize() called from the constructor and OnPalettePaint.
            Panel.Height = _btnOk.Height + Defaults.PanelTop + Defaults.PanelBottom;

            // Done
            LayoutDirty = false;

            // Tell everybody about it when visible.
            base.OnSizeChanged(performLayout);
        }
    }

    /// <inheritdoc/>
    protected override void OnPalettePaint(object? sender, PaletteLayoutEventArgs e)
    {
        base.OnPalettePaint(sender, e);

        // Update button size on change.
        // Flag dirty, and if visible call OnSizeChanged, otherwise leave it deferred for a call from PerformLayout.
        UpdateButtonSize();
        LayoutDirty = true;
        OnSizeChanged();
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
            if (base.Visible != value)
            {
                base.Visible = value;
                OnSizeChanged();
            }
        }
    }
    #endregion

    #region Public
    /// <summary>
    /// Rounds the button corners
    /// </summary>
    public bool RoundedCorners 
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;

                int rounding = value ? 10 : -1;
                _buttons.ForEach(b => b.StateCommon.Border.Rounding = rounding);
                LayoutDirty = true;
                OnSizeChanged();
            }
        }
    }

    /// <inheritdoc/>
    public KryptonTaskDialogCommonButtonTypes Buttons 
    {
        get => _commonButtons;
        set
        {
            if (_commonButtons != value)
            {
                _commonButtons = value;
                OnCommonButtonsChanged();
            }
        }
    }

    /// <inheritdoc/>
    public KryptonTaskDialogCommonButtonTypes AcceptButton 
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;
                OnCommonButtonsChanged();
            }
        }
    }

    /// <inheritdoc/>
    public KryptonTaskDialogCommonButtonTypes CancelButton 
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;
                OnCommonButtonsChanged();
            }
        }
    }
    #endregion

    #region Private
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
        _tlp.ColumnStyles.Clear();
        _tlp.RowStyles.Clear();

        _tlp.RowCount = 1;
        _tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        _tlp.ColumnCount = 8;

        // The first column and style will act as a filler and push the buttons to the right as there are buttons visible.
        _tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        // Add the column styles for each button
        for (int i = 0; i < _tlp.ColumnCount - 1; i++)
        {
            _tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        }

    }
    private void CreateButtons()
    {
        // Create and add the buttons to the table layout panel.
        _btnOk = CreateButton("OK", DialogResult.OK, 1);
        _btnYes = CreateButton("Yes", DialogResult.Yes, 2);
        _btnNo = CreateButton("No", DialogResult.No, 3);
        _btnCancel = CreateButton("Cancel", DialogResult.Cancel, 4);
        _btnRetry = CreateButton("Retry", DialogResult.Retry, 5);
        _btnAbort = CreateButton("Abort", DialogResult.Abort, 6);
        _btnIgnore = CreateButton("Ignore", DialogResult.Ignore, 7);

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

    private void OnCommonButtonsChanged()
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

    private void SetButton( KryptonButton button, KryptonTaskDialogCommonButtonTypes buttonType)
    {
        button.Visible = (_commonButtons & buttonType) == buttonType;

        // If AcceptButton and CancelButton are the same, Accept wins.
        if (buttonType == AcceptButton)
        {
            _form.AcceptButton = button;
            button.Select();
        }
        else if (buttonType == CancelButton)
        {
            _form.CancelButton = button;
        }
    }
    #endregion

    #region IDispose
    protected override void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            // Remove all connections to the outer world
            _btnOk.Click -= ButtonClick;
            _btnYes.Click -= ButtonClick;
            _btnNo.Click -= ButtonClick;
            _btnCancel.Click -= ButtonClick;
            _btnRetry.Click -= ButtonClick;
            _btnAbort.Click -= ButtonClick;
            _btnIgnore.Click -= ButtonClick;

            _form.AcceptButton = null;
            _form.CancelButton = null;
            _form = null!;

            _disposed = true;
        }

        base.Dispose(disposing);
    }
    #endregion
}
