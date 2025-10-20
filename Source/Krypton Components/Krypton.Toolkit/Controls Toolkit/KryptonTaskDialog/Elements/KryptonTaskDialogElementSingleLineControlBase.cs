#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Base class that has boilerplate code for an element that hosts a control that occupies a single line.<br/>
/// Above the control there will be a label for a descriptive text
/// </summary>
public abstract class KryptonTaskDialogElementSingleLineControlBase : KryptonTaskDialogElementBase,
    IKryptonTaskDialogElementForeColor
{
    #region Fields
    protected TableLayoutPanel _tlp;
    #endregion

    #region Identity
    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="taskDialogDefaults">Dialog defaults.</param>
    /// <param name="rowCount">The number of rows the internal layout panel will have.</param>
    public KryptonTaskDialogElementSingleLineControlBase(KryptonTaskDialogDefaults taskDialogDefaults, int rowCount) 
        : base(taskDialogDefaults)
    {
        Panel.Width = Defaults.ClientWidth;
        // the padding will force the layout table to the correct margins when docked.
        Panel.Padding = Defaults.PanelPadding1;

        // A panel to arrange the controls
        _tlp = new();
        SetupTableLayoutPanel(rowCount);

        Panel.Controls.Add(_tlp);
    }
    #endregion

    #region Private
    private void SetupTableLayoutPanel(int rowCount)
    {
        _tlp.SetDoubleBuffered(true);
        _tlp.Dock = DockStyle.Top;
        _tlp.AutoSize = true;
        _tlp.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _tlp.MinimumSize = Defaults.TLP.StdMinSize;
        _tlp.MaximumSize = Defaults.TLP.StdMaxSize;
        _tlp.Padding = Defaults.NullPadding;
        _tlp.Margin = Defaults.NullMargin;
        _tlp.BackColor = Color.Transparent;

        _tlp.ColumnStyles.Clear();
        _tlp.RowStyles.Clear();

        for (int i = 0; i < rowCount; i++)
        {
            _tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        }

        _tlp.ColumnCount = 1;
        _tlp.RowStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
    }
    #endregion

    #region Public virtual
    /// <inheritdoc/>
    public virtual Color ForeColor { get; set; }
    #endregion
}
