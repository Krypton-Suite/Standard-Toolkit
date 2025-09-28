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
/// Simplified designer for KryptonLabel optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class KryptonLabelSimpleDesigner : ControlDesigner
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
                actionLists.Add(new KryptonLabelSimpleActionList(Component));
            }

            return actionLists;
        }
    }
    #endregion
}

/// <summary>
/// Simplified action list for KryptonLabel optimized for .NET 8+ out-of-process designer.
/// </summary>
internal class KryptonLabelSimpleActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonLabel _label;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonLabelSimpleActionList class.
    /// </summary>
    /// <param name="component">The component to create actions for.</param>
    public KryptonLabelSimpleActionList(IComponent component)
        : base(component)
    {
        _label = (KryptonLabel)component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the label text.
    /// </summary>
    [Category("Appearance")]
    [Description("Label text")]
    public string Text
    {
        get => _label.Values.Text;
        set
        {
            if (_label.Values.Text != value)
            {
                _label.Values.Text = value;
                NotifyPropertyChanged("Text", value);
            }
        }
    }

    /// <summary>
    /// Gets and sets the label extra text.
    /// </summary>
    [Category("Appearance")]
    [Description("Label extra text")]
    public string ExtraText
    {
        get => _label.Values.ExtraText;
        set
        {
            if (_label.Values.ExtraText != value)
            {
                _label.Values.ExtraText = value;
                NotifyPropertyChanged("ExtraText", value);
            }
        }
    }

    /// <summary>
    /// Gets and sets the label image.
    /// </summary>
    [Category("Appearance")]
    [Description("Label image")]
    public Image? Image
    {
        get => _label.Values.Image;
        set
        {
            if (_label.Values.Image != value)
            {
                _label.Values.Image = value;
                NotifyPropertyChanged("Image", value);
            }
        }
    }

    /// <summary>
    /// Gets and sets the label style.
    /// </summary>
    [Category("Appearance")]
    [Description("Label style")]
    public LabelStyle LabelStyle
    {
        get => _label.LabelStyle;
        set
        {
            if (_label.LabelStyle != value)
            {
                _label.LabelStyle = value;
                NotifyPropertyChanged("LabelStyle", value);
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    [Category("Visuals")]
    [Description("Palette applied to drawing")]
    public PaletteMode PaletteMode
    {
        get => _label.PaletteMode;
        set
        {
            if (_label.PaletteMode != value)
            {
                _label.PaletteMode = value;
                NotifyPropertyChanged("PaletteMode", value);
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
            var propertyDescriptor = TypeDescriptor.GetProperties(_label)[propertyName];
            changeService.OnComponentChanged(_label, propertyDescriptor, null, value);
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

        if (_label != null)
        {
            // Appearance
            actions.Add(new DesignerActionHeaderItem("Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), "Text", "Appearance", "Label text"));
            actions.Add(new DesignerActionPropertyItem(nameof(ExtraText), "Extra Text", "Appearance", "Label extra text"));
            actions.Add(new DesignerActionPropertyItem(nameof(Image), "Image", "Appearance", "Label image"));
            actions.Add(new DesignerActionPropertyItem(nameof(LabelStyle), "Label Style", "Appearance", "Label style"));
            
            // Visuals
            actions.Add(new DesignerActionHeaderItem("Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), "Palette", "Visuals", "Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}
