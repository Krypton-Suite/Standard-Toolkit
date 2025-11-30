#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Simplified designer for KryptonProgressBar optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class KryptonProgressBarSimpleDesigner : ControlDesigner
{
    #region Public Overrides
    /// <summary>
    /// Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            var actionLists = new DesignerActionListCollection();
            
            if (Component != null)
            {
                actionLists.Add(new KryptonProgressBarSimpleActionList(Component));
            }

            return actionLists;
        }
    }
    #endregion
}

/// <summary>
/// Simplified action list for KryptonProgressBar optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class KryptonProgressBarSimpleActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonProgressBar _progressBar;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonProgressBarSimpleActionList class.
    /// </summary>
    /// <param name="component">The component to create actions for.</param>
    public KryptonProgressBarSimpleActionList(IComponent component)
        : base(component)
    {
        _progressBar = (KryptonProgressBar)component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the progress bar minimum value.
    /// </summary>
    [Category("Behavior")]
    [Description("Minimum value")]
    public int Minimum
    {
        get => _progressBar.Minimum;
        set
        {
            if (_progressBar.Minimum != value)
            {
                _progressBar.Minimum = value;
                NotifyPropertyChanged("Minimum", value);
            }
        }
    }

    /// <summary>
    /// Gets and sets the progress bar maximum value.
    /// </summary>
    [Category("Behavior")]
    [Description("Maximum value")]
    public int Maximum
    {
        get => _progressBar.Maximum;
        set
        {
            if (_progressBar.Maximum != value)
            {
                _progressBar.Maximum = value;
                NotifyPropertyChanged("Maximum", value);
            }
        }
    }

    /// <summary>
    /// Gets and sets the progress bar current value.
    /// </summary>
    [Category("Behavior")]
    [Description("Current value")]
    public int Value
    {
        get => _progressBar.Value;
        set
        {
            if (_progressBar.Value != value)
            {
                _progressBar.Value = value;
                NotifyPropertyChanged("Value", value);
            }
        }
    }

    /// <summary>
    /// Gets and sets the progress bar step value.
    /// </summary>
    [Category("Behavior")]
    [Description("Step value")]
    public int Step
    {
        get => _progressBar.Step;
        set
        {
            if (_progressBar.Step != value)
            {
                _progressBar.Step = value;
                NotifyPropertyChanged("Step", value);
            }
        }
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Notify that a property has changed.
    /// </summary>
    /// <param name="propertyName">Name of the property that changed.</param>
    /// <param name="value">New value of the property.</param>
    private void NotifyPropertyChanged(string propertyName, object? value)
    {
        var changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
        if (changeService != null)
        {
            var propertyDescriptor = TypeDescriptor.GetProperties(_progressBar)[propertyName];
            changeService.OnComponentChanged(_progressBar, propertyDescriptor, null, value);
        }
    }
    #endregion

    #region Public Override
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        if (_progressBar != null)
        {
            // Behavior
            actions.Add(new DesignerActionHeaderItem("Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Minimum), "Minimum", "Behavior", "Minimum value"));
            actions.Add(new DesignerActionPropertyItem(nameof(Maximum), "Maximum", "Behavior", "Maximum value"));
            actions.Add(new DesignerActionPropertyItem(nameof(Value), "Value", "Behavior", "Current value"));
            actions.Add(new DesignerActionPropertyItem(nameof(Step), "Step", "Behavior", "Step value"));
        }

        return actions;
    }
    #endregion
}
