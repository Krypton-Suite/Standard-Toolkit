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

/// <summary>
/// Base class that has boilerplate code for an element that hosts a control that occupies a single line.<br/>
/// Above the control there will be a label for a descriptive text
/// </summary>
public abstract class KryptonTaskDialogElementSingleLineControlBase : KryptonTaskDialogElementBase,
    IKryptonTaskDialogElementForeColor
{
    #region Fields
    protected Padding _nullPadding = new(0);
    protected Padding _nullMargin = new(0);
    protected TableLayoutPanel _tlp;
    #endregion

    #region Identity
    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="rowCount">The number of rows the internal layout panel will have.</param>
    /// <param name="taskDialogDefaults">Dialog defaults.</param>
    public KryptonTaskDialogElementSingleLineControlBase(KryptonTaskDialogDefaults taskDialogDefaults, int rowCount) 
        : base(taskDialogDefaults)
    {
        Panel.Width = Defaults.ClientWidth;
        // the padding will force the layout table to the correct margins when docked.
        Panel.Padding = Defaults.PanelPadding1;

        // A panel to arrange the controls
        _tlp = new()
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            MinimumSize = Defaults.TLP.StdMinSize,
            MaximumSize = Defaults.TLP.StdMaxSize,
            Padding = Defaults.NullPadding,
            Margin = Defaults.NullMargin,
            BackColor = Color.Transparent
        };

        _tlp.ColumnStyles.Clear();
        _tlp.RowStyles.Clear();

        _tlp.RowCount = rowCount;
        for ( int i = 0; i < rowCount; i++)
        {
            _tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        }

        _tlp.ColumnCount = 1;
        _tlp.RowStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

        Panel.Controls.Add(_tlp);
    }
    #endregion

    #region Public virtual
    /// <inheritdoc/>
    public virtual Color ForeColor { get; set; }
    #endregion

    #region Protected
    /// <summary>
    /// Layout panel row count
    /// </summary>
    protected int RowCount { get; }

    /// <summary>
    /// Layout panel row height
    /// </summary>
    protected int RowHeight { get; }
    #endregion
}
