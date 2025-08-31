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
    private KryptonForm _form;
    private bool _disposed;
    private KryptonTaskDialogCommonButtonTypes _commonButtons;
    private TableLayoutPanel _tableLayoutPanel;
    private KryptonButton _btnOk;
    private KryptonButton _btnYes;
    private KryptonButton _btnNo;
    private KryptonButton _btnCancel;
    private KryptonButton _btnRetry;
    private KryptonButton _btnAbort;
    private KryptonButton _btnIgnore;
    #endregion

    #region Identity
    public KryptonTaskDialogElementCommonButtons(KryptonForm kryptonForm) 
    {
        // Standard button is 90 x 25 on creation
        _disposed = false;
        _form = kryptonForm;

        Panel.Height = 26 + KryptonTaskDialog.Defaults.PanelTop + KryptonTaskDialog.Defaults.PanelBottom;
        Panel.Width = KryptonTaskDialog.Defaults.ClientWidth;

        // A panel to arrange the buttons
        _tableLayoutPanel = new()
        {
            Height = 26,
            Width = KryptonTaskDialog.Defaults.ClientWidth - KryptonTaskDialog.Defaults.PanelTop - KryptonTaskDialog.Defaults.PanelLeft,
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            Padding = new Padding(0),
            Margin = new Padding(0),
            Top = KryptonTaskDialog.Defaults.PanelTop,
            Left = KryptonTaskDialog.Defaults.PanelLeft,
            BackColor = Color.Transparent
        };
        _tableLayoutPanel.ColumnStyles.Clear();
        _tableLayoutPanel.RowStyles.Clear();

        _tableLayoutPanel.RowCount = 1;
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 26f));

        _tableLayoutPanel.ColumnCount = 8;
        
        // The first column and style will act as a filler and push the buttons to the right as there are buttons visible.
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        // Add the column styles for each button
        for (int i = 0; i < _tableLayoutPanel.ColumnCount - 1; i++)
        {
            _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        }

        // Finally add the table layout panel to the panel
        Panel.Controls.Add(_tableLayoutPanel);

        CreateButtons();
        
        // Initial setting, OK Cancel
        Buttons = KryptonTaskDialogCommonButtonTypes.OK | KryptonTaskDialogCommonButtonTypes.Cancel;
        AcceptButton = KryptonTaskDialogCommonButtonTypes.OK;
        CancelButton = KryptonTaskDialogCommonButtonTypes.Cancel;
    }
    #endregion

    #region Public
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
    private void CreateButtons()
    {
        // Create and add the buttons to the table layout panel.
        _btnOk = CreateButton("OK", DialogResult.OK, 1);
        _btnYes = CreateButton("Yes", DialogResult.Yes, 2);
        _btnNo = CreateButton("No", DialogResult.No, 3);
        _btnCancel = CreateButton("Cancel", DialogResult.Cancel, 4);
        _btnRetry = CreateButton("Retry", DialogResult.Retry, 5);
        _btnAbort = CreateButton("Abort", DialogResult.Abort, 6);
        _btnIgnore = CreateButton("Ignore", DialogResult.Ignore, 6);
    }

    private KryptonButton CreateButton(string text, DialogResult dialogResult, int index)
    {
        var button = new KryptonButton()
        {
            Text = text,
            DialogResult = dialogResult,
            Padding = new Padding(0),
            Margin = new Padding(5, 0, 5, 0),
            Height = 24,
            Width = 75
        };
        button.Click += ButtonClick;

        _tableLayoutPanel.Controls.Add(button, index, 0);
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

    #region Protected
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
