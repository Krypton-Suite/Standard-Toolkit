#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Hosts a collection of KryptonDataGridViewLinkColumn cells.
/// </summary>
[ToolboxBitmap(typeof(KryptonDataGridViewLinkColumn), "ToolboxBitmaps.KryptonLinkLabel.bmp")]
public class KryptonDataGridViewLinkColumn : KryptonDataGridViewIconColumn
{
    #region Static Fields
    private MethodInfo _miColumnCommonChange;
    private PropertyInfo _piUseColumnTextForLinkValueInternal;
    private PropertyInfo _piTrackVisitedStateInternal;
    #endregion

    #region Instance Fields
    private string? _text;
    private LabelStyle _labelStyle;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDataGridViewLinkColumn class.
    /// </summary>
    public KryptonDataGridViewLinkColumn()
        : base(new KryptonDataGridViewLinkCell()) =>
        // Define defaults
        _labelStyle = LabelStyle.NormalPanel;

    /// <summary>
    /// Returns a String that represents the current Object.
    /// </summary>
    /// <returns>A String that represents the current Object.</returns>
    public override string ToString()
    {
        var builder = new StringBuilder(0x40);
        builder.Append("KryptonDataGridViewLinkColumn { Name=");
        // ReSharper disable RedundantBaseQualifier
        builder.Append(base.Name);
        builder.Append(", Index=");
        builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
        // ReSharper restore RedundantBaseQualifier
        builder.Append(" }");
        return builder.ToString();
    }

    /// <summary>
    /// This member overrides DataGridViewButtonColumn.Clone.
    /// </summary>
    /// <returns>New object instance.</returns>
    public override object Clone()
    {
        // Create a new instance
        var clone = base.Clone() as KryptonDataGridViewLinkColumn ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("clone"));
        clone.Text = Text;
        clone.LabelStyle = LabelStyle;

        return clone;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets or sets the template used to model cell appearance.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override DataGridViewCell? CellTemplate
    {
        get => base.CellTemplate;

        set
        {
            if ((value is not null) && value is not KryptonDataGridViewLinkCell)
            {
                throw new InvalidCastException("Can only assign a object of type KryptonDataGridViewLinkCell");
            }

            base.CellTemplate = value as KryptonDataGridViewLinkCell;
        }
    }

    /// <summary>
    /// Gets or sets the default text Displayed on the link cell.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(null)]
    public string? Text
    {
        get => _text;
        set
        {
            if (!string.Equals(value, _text, StringComparison.Ordinal))
            {
                _text = value;
                if (DataGridView != null)
                {
                    if (UseColumnTextForLinkValue)
                    {
                        ColumnCommonChange(Index);
                    }
                    else
                    {
                        DataGridViewRowCollection rows = DataGridView.Rows;
                        var count = rows.Count;
                        for (var i = 0; i < count; i++)
                        {
                            if ((rows.SharedRow(i).Cells[Index] is KryptonDataGridViewLinkCell
                                {
                                    UseColumnTextForLinkValue: true
                                }))
                            {
                                ColumnCommonChange(Index);
                                return;
                            }
                        }
                        DataGridView.InvalidateColumn(Index);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets the default label style of link cell.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(LabelStyle.NormalPanel)]
    public LabelStyle LabelStyle
    {
        get => _labelStyle;
        set
        {
            if (_labelStyle != value)
            {
                _labelStyle = value;
                ((KryptonDataGridViewLinkCell)CellTemplate!).LabelStyleInternal = value;
                // ReSharper disable RedundantBaseQualifier
                if (base.DataGridView != null)
                    // ReSharper restore RedundantBaseQualifier
                {
                    DataGridViewRowCollection rows = DataGridView.Rows;
                    var count = rows.Count;
                    for (var i = 0; i < count; i++)
                    {
                        if (rows.SharedRow(i).Cells[Index] is KryptonDataGridViewLinkCell cell)
                        {
                            cell.LabelStyleInternal = value;
                        }
                    }
                    DataGridView.InvalidateColumn(Index);
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets a value that represents the behavior of links within cells in the column.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(LinkBehavior.AlwaysUnderline)]
    public LinkBehavior LinkBehavior
    {
        get =>
            ((KryptonDataGridViewLinkCell)CellTemplate!)?.LinkBehavior ?? throw new InvalidOperationException("KryptonDataGridViewLinkCell cell template required");
        set
        {
            if (!LinkBehavior.Equals(value))
            {
                ((KryptonDataGridViewLinkCell)CellTemplate!).LinkBehaviorInternal = value;
                // ReSharper disable RedundantBaseQualifier
                if (base.DataGridView != null)
                    // ReSharper restore RedundantBaseQualifier
                {
                    DataGridViewRowCollection rows = DataGridView.Rows;
                    var count = rows.Count;
                    for (var i = 0; i < count; i++)
                    {
                        if (rows.SharedRow(i).Cells[Index] is KryptonDataGridViewLinkCell cell)
                        {
                            cell.LinkBehaviorInternal = value;
                        }
                    }
                    DataGridView.InvalidateColumn(Index);
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the link changes color when it is visited.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    public bool TrackVisitedState
    {
        get =>
            ((KryptonDataGridViewLinkCell)CellTemplate!)?.TrackVisitedState ?? throw new InvalidOperationException("KryptonDataGridViewLinkCell cell template required");
        set
        {
            if (TrackVisitedState != value)
            {
                TrackVisitedStateInternal(CellTemplate!, value);
                if (DataGridView != null)
                {
                    DataGridViewRowCollection rows = DataGridView.Rows;
                    var count = rows.Count;
                    for (var i = 0; i < count; i++)
                    {
                        if (rows.SharedRow(i).Cells[Index] is DataGridViewLinkCell cell)
                        {
                            TrackVisitedStateInternal(cell, value);
                        }
                    }
                    DataGridView.InvalidateColumn(Index);
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the Text property value is Displayed as the link text for cells in this column.
    /// </summary>
    [Category(@"Appearance")]
    [DefaultValue(false)]
    public bool UseColumnTextForLinkValue
    {
        get =>
            ((KryptonDataGridViewLinkCell)CellTemplate!)?.UseColumnTextForLinkValue ?? throw new InvalidOperationException("KryptonDataGridViewLinkCell cell template required");

        set
        {
            if (UseColumnTextForLinkValue != value)
            {
                SetUseColumnTextForLinkValueInternal(CellTemplate!, value);
                if (DataGridView != null)
                {
                    DataGridViewRowCollection rows = DataGridView.Rows;
                    var count = rows.Count;
                    for (var i = 0; i < count; i++)
                    {
                        if (rows.SharedRow(i).Cells[Index] is DataGridViewLinkCell cell)
                        {
                            SetUseColumnTextForLinkValueInternal(cell, value);
                        }
                    }
                    ColumnCommonChange(Index);
                }
            }
        }
    }
    #endregion

    #region Private
    private void ColumnCommonChange(int columnIndex)
    {
        // Only need to cache reflection info the first time around
        if (_miColumnCommonChange is null)
        {
            // Cache access to the internal method 'OnColumnCommonChange'
            _miColumnCommonChange = typeof(DataGridView).GetMethod("OnColumnCommonChange", BindingFlags.Instance |
                BindingFlags.NonPublic |
                BindingFlags.GetField)!;

        }

        _miColumnCommonChange.Invoke(DataGridView, [columnIndex]);
    }

    private void SetUseColumnTextForLinkValueInternal(object instance, bool value)
    {
        // Only need to cache reflection info the first time around
        if (_piUseColumnTextForLinkValueInternal is null)
        {
            // Cache access to the internal property sette 'UseColumnTextForLinkValueInternal'
            _piUseColumnTextForLinkValueInternal = typeof(DataGridViewLinkCell).GetProperty(@"UseColumnTextForLinkValueInternal", BindingFlags.Instance |
                BindingFlags.NonPublic |
                BindingFlags.SetProperty)!;

        }

        _piUseColumnTextForLinkValueInternal.SetValue(instance, value, null);
    }

    private void TrackVisitedStateInternal(object instance, bool value)
    {
        // Only need to cache reflection info the first time around
        if (_piTrackVisitedStateInternal is null)
        {
            // Cache access to the internal property sette 'TrackVisitedStateInternal'
            _piTrackVisitedStateInternal = typeof(DataGridViewLinkCell).GetProperty(nameof(TrackVisitedStateInternal), BindingFlags.Instance |
                BindingFlags.NonPublic |
                BindingFlags.SetProperty)!;

        }

        _piTrackVisitedStateInternal.SetValue(instance, value, null);
    }
    #endregion
}