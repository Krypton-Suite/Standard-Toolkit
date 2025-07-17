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

/// <inheritdoc/>
public class KryptonDataGridViewImageColumn : DataGridViewImageColumn, IIconCell
{
    private KryptonDataGridView? _dataGridView = null;

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDataGridViewImageColumn class.
    /// </summary>
    public KryptonDataGridViewImageColumn()
        : this(false)
    {
    }

    /// <summary>
    /// Initialize a new instance of the KryptonDataGridViewImageColumn class.
    /// </summary>
    /// <param name="valuesAreIcons">When set to true values of type Icon are expected, otherwise Image.</param>
    public KryptonDataGridViewImageColumn(bool valuesAreIcons)
        : base(valuesAreIcons)
    {
        IconSpecs = [];
    }
    #endregion

    #region IIconCell implementation
    protected override void OnDataGridViewChanged()
    {
        // KDGV needs a column refresh only
        if (DataGridView is KryptonDataGridView dataGridView)
        {
            _dataGridView = dataGridView;
            IconSpecs.CollectionChanged += OnIconSpecsCollectionChanged;
        }
        else if (_dataGridView is not null)
        {
            // only unhook on KDGV type
            IconSpecs.CollectionChanged -= OnIconSpecsCollectionChanged;
            _dataGridView = null;
        }

        base.OnDataGridViewChanged();
    }

    /// <summary>
    /// Will inform the KGDV that the column needs a repaint. 
    /// </summary>
    /// <param name="sender">Not used.</param>
    /// <param name="e">Not used.</param>
    private void OnIconSpecsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        _dataGridView?.InvalidateColumn(this.Index);
    }

    /// <summary>
    /// Create a cloned copy of the column.
    /// </summary>
    /// <returns>The cloned object.</returns>
    public override object Clone()
    {
        var cloned = base.Clone() as KryptonDataGridViewImageColumn ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull("cloned"));

        foreach (IconSpec sp in IconSpecs)
        {
            cloned.IconSpecs.Add((sp.Clone() as IconSpec)!);
        }

        return cloned;
    }

    /// <summary>
    /// Gets the collection of the icon specifications.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Set of extra icons to appear on the column header.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ObservableCollection<IconSpec> IconSpecs { get; }
}

#endregion