#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public class KryptonTaskDialogElementFreeWheeler : KryptonTaskDialogElementSingleLineControlBase,
    IKryptonTaskDialogElementHeight
{
    #region Fields
    private FlowLayoutPanel _flp;
    #endregion

    #region Identity
    public KryptonTaskDialogElementFreeWheeler(KryptonTaskDialogDefaults taskDialogDefaults) : base(taskDialogDefaults, 1)
    {
        _flp = new();
        SetupPanel();
    }
    #endregion

    #region Private
    private void SetupPanel()
    {
        _tlp.SetDoubleBuffered(true);
        _tlp.Controls.Add(_flp, 0, 0);
        _tlp.Padding = Defaults.NullPadding;
        _tlp.Margin = new Padding(Defaults.PanelLeft, Defaults.PanelTop, Defaults.PanelRight, Defaults.PanelBottom);
        _tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
        _tlp.Dock = DockStyle.Fill;

        _flp.SetDoubleBuffered(true);
        _flp.Padding = Defaults.NullPadding;
        _flp.Margin = Defaults.NullMargin;
        _tlp.Dock = DockStyle.Fill;
        _flp.Size = _tlp.Size;
        _flp.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
    }
    #endregion
    #region Public
    /// <summary>
    /// The internal FlowLayoutPanel to add your controls to.
    /// </summary>
    public FlowLayoutPanel FlowLayoutPanel => _flp;

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