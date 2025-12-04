#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public class KryptonTaskDialogElementComboBox : KryptonTaskDialogElementSingleLineControlBase,
    IKryptonTaskDialogElementDescription,
    IKryptonTaskDialogElementRoundedCorners
{
    #region Enum
    // Used internally only
    public enum InternalComboBoxStyle
    {
        DropDown = 1,
        DropDownList = 2
    }
    #endregion

    #region Fields
    private KryptonWrapLabel _description;
    private KryptonComboBox _comboBox;
    private bool _disposed;
    #endregion

    #region Events
    /// <summary>
    /// Subscribe to be notifed when the user selects an item from the combo-box.
    /// </summary>
    public event Action<object?> SelectedItemChanged;
    #endregion

    #region Identity
    public KryptonTaskDialogElementComboBox(KryptonTaskDialogDefaults taskDialogDefaults) 
        : base(taskDialogDefaults, 2)
    {
        _disposed = false;
        _description = new();
        _comboBox = new();

        SetupPanel();
        ShowDescription = true;

        // Event is active and will invoke this.SelectedItemChanged to which the user can subscribe.
        _comboBox.SelectedIndexChanged += OnSelectedIndexChanged;

        // We use this to trigger an update in panel size after a global font change has happened.
        // Ths guarantees that the resize has been completed. OnPalettePaint does not do this.
        _comboBox.SizeChanged += OnComboBoxSizeChanged;

        // Done
        LayoutDirty = true;
    }
    #endregion

    public bool RoundedCorners
    {
        get => field;
        set
        {
            field = value;
            _comboBox.StateCommon.ComboBox.Border.Rounding = Defaults.GetCornerRouding(value);
        }
    }
    #region Override
    /// <inheritdoc/>
    protected override void OnSizeChanged(bool performLayout = false)
    {
        if (LayoutDirty && (Visible || performLayout))
        {
            int height = ShowDescription
                ? _description.Height + _description.Margin.Bottom :
                0;

            Panel.Height = Defaults.PanelTop + Defaults.PanelBottom + _comboBox.Height + height;

            base.OnSizeChanged(performLayout);

            LayoutDirty = false;
        }
    }

    /// <inheritdoc/>
    internal override void PerformLayout()
    {
        base.PerformLayout();
        OnSizeChanged(true);
    }
    #endregion

    #region Public
    /// <summary>
    /// Index of the currently selected item.<br/>
    /// Return -1 if no item has been selected.
    /// </summary>
    public int SelectedIndex => _comboBox.SelectedIndex;

    /// <summary>
    /// Currently selected item.<br/>
    /// Returns null if no item has been selected.
    /// </summary>
    public object? SelectedItem => _comboBox.SelectedItem;

    /// <summary>
    /// Combobox items collection.
    /// </summary>
    [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    public ComboBox.ObjectCollection Items => _comboBox.Items;

    /// <summary>
    /// Width of the combo box
    /// </summary>
    [DefaultValue(250)]
    public int ComboxWidth 
    {
        get => _comboBox.Width;

        set
        {
            if (_comboBox.Width != value)
            {
                // Only set the value, calling OnSizeChanged is not necessary.
                _comboBox.Width = value;
            }
        }
    }

    /// <summary>
    /// Width of the drop down list.
    /// </summary>
    public int DropDownWidth 
    {
        get => _comboBox.DropDownWidth;
        set => _comboBox.DropDownWidth = value;
    }

    /// <summary>
    /// Combobox drop down style.<br/>
    /// Note: KryptonCombox does not support the simple drop down style.
    /// </summary>
    public InternalComboBoxStyle DropDownStyle 
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;

                _comboBox.DropDownStyle = value == InternalComboBoxStyle.DropDownList
                    ? ComboBoxStyle.DropDownList
                    : ComboBoxStyle.DropDown;
            }
        }
    }


    /// <inheritdoc/>
    public string Description
    {
        get => _description.Text;
        set => _description.Text = value;
    }

    /// <inheritdoc/>
    public bool ShowDescription 
    {
        get => field;

        set
        {
            if ( field != value)
            {
                field = value;
                _description.Visible = value;
                LayoutDirty = true;
                OnSizeChanged();
            }
        }
    }

    /// <inheritdoc/>
    public override Color ForeColor 
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;
                _description.StateCommon.TextColor = value;
            }
        }
    }
    #endregion

    #region Private
    private void SetupControls()
    {
        _description.AutoSize = true;
        _description.Padding = Defaults.NullPadding;
        _description.Margin = new( 0, 0, 0, Defaults.ComponentSpace );

        _comboBox.AutoSize = true;
        _comboBox.Padding = Defaults.NullPadding;
        _comboBox.Margin = Defaults.NullMargin;
        _comboBox.Width = 250;
        DropDownStyle = InternalComboBoxStyle.DropDownList;
    }

    private void SetupPanel()
    {
        SetupControls();

        // add the controls
        _tlp.Controls.Add(_description, 0, 0);
        _tlp.Controls.Add(_comboBox, 0, 1);

        // if IKryptonTaskDialogElementDescription is implemented the first row needs to be AutoSize
        _tlp.RowStyles[0].SizeType = SizeType.AutoSize;
    }

    // This triggers global font(size) changes and will request panel resize.
    private void OnComboBoxSizeChanged(object? sender, EventArgs e)
    {
        LayoutDirty = true;
        OnSizeChanged();
    }

    private void OnSelectedIndexChanged(object? sender, EventArgs e)
    {
        // Only fire if the dialog is visible
        if (Visible)
        {
            SelectedItemChanged?.Invoke(_comboBox.SelectedItem);
        }
    }
    #endregion

    #region IDispose
    protected override void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _comboBox.SelectedIndexChanged -= OnSelectedIndexChanged;
            _comboBox.SizeChanged -= OnComboBoxSizeChanged;

            _disposed = true;
        }

        base.Dispose(disposing);
    }
    #endregion
}

