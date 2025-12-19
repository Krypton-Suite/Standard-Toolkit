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
/// KryptonTaskDialogElementFreeWheeler2 exposes a TableLayoutPanel with full access to it's properties.<br/>
/// Here you can host a choice of controls within KryptonTaskDialog.<br/>
/// </summary>
public class KryptonTaskDialogElementFreeWheeler2 : KryptonTaskDialogElementSingleLineControlBase,
    IKryptonTaskDialogElementHeight
{
    #region Fields
    private TableLayoutPanel _tlpExposed;
    #endregion

    #region Identity
    public KryptonTaskDialogElementFreeWheeler2(KryptonTaskDialogDefaults taskDialogDefaults) : base(taskDialogDefaults, 1)
    {
        _tlpExposed = new();
        SetupPanel();
    }
    #endregion

    #region Private
    private void SetupPanel()
    {
        _tlp.SetDoubleBuffered(true);
        _tlp.Controls.Add(_tlpExposed, 0, 0);
        _tlp.Padding = Defaults.NullPadding;
        _tlp.Margin = new Padding(Defaults.PanelLeft, Defaults.PanelTop, Defaults.PanelRight, Defaults.PanelBottom);
        _tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
        _tlp.Dock = DockStyle.Fill;

        _tlpExposed.SetDoubleBuffered(true);
        _tlpExposed.Padding = Defaults.NullPadding;
        _tlpExposed.Margin = Defaults.NullMargin;
        _tlpExposed.Dock = DockStyle.Fill;

        _tlpExposed.RowCount = 1;
        _tlpExposed.ColumnCount = 1;

        _tlpExposed.RowStyles.Clear();
        _tlpExposed.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        _tlpExposed.ColumnStyles.Clear();
        _tlpExposed.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
    }
    #endregion

    #region Public
    /// <summary>
    /// The internal TableLayoutPanel to add your controls to.
    /// </summary>
    public TableLayoutPanel TableLayoutPanel => _tlpExposed;

    /// <inheritdoc/>
    public int ElementHeight 
    { 
        get => Panel.Height; 
        set
        {
            if (Panel.Height != value)
            {
                Panel.Height = value;
                LayoutDirty = true;
                OnSizeChanged();
            }
        }
    }
    #endregion

}