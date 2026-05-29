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

internal class KryptonGroupPanelDesigner : KryptonPanelDesigner,
    IKryptonDesignerSelect
{
    #region Instance Fields
    private KryptonGroupPanel? _panel;
    private ISelectionService? _selectionService;
    #endregion

    #region Public
    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The IComponent to associate with the designer.</param>
    public override void Initialize([DisallowNull] IComponent component)
    {
        // Perform common base class initializing
        base.Initialize(component);

        Debug.Assert(component != null);

        // Remember references to components involved in design
        _panel = component as KryptonGroupPanel;

        // Acquire service interfaces
        _selectionService = GetService(typeof(ISelectionService)) as ISelectionService;

        // If inside a Krypton group container then always lock the component from user size/location change
        if (_panel != null)
        {
            var descriptor = TypeDescriptor.GetProperties(component)[@"Locked"];
            if ((descriptor != null) && (_panel.Parent is KryptonGroup or KryptonHeaderGroup))
            {
                descriptor.SetValue(component, true);
            }
        }
    }

    /// <summary>
    /// Indicates if this designer's control can be parented by the control of the specified designer.
    /// </summary>
    /// <param name="parentDesigner">The IDesigner that manages the control to check.</param>
    /// <returns>true if the control managed by the specified designer can parent the control managed by this designer; otherwise, false.</returns>
    public override bool CanBeParentedTo(IDesigner parentDesigner) =>
        // We should only ever exist inside a Krypton group container
        (parentDesigner is KryptonGroup or KryptonHeaderGroup);

    /// <summary>
    /// Gets the selection rules that indicate the movement capabilities of a component.
    /// </summary>
    public override SelectionRules SelectionRules =>
        // If the panel is inside our Krypton group container then prevent 
        // user changing the size or location of the group panel instance
        (Control.Parent is KryptonGroup or KryptonHeaderGroup)
            ? SelectionRules.None | SelectionRules.Locked
            : SelectionRules.None;

    /// <summary>
    /// Gets a list of SnapLine objects representing significant alignment points for this control.
    /// </summary>
    public override IList SnapLines
    {
        get
        {
            ArrayList? snapLines = null;

            // ReSharper disable RedundantBaseQualifier
            // Let the base class generate snap lines
            base.AddPaddingSnapLines(ref snapLines);
            // ReSharper restore RedundantBaseQualifier

            return snapLines;
        }
    }

    /// <summary>
    ///  Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists => [];

    /// <summary>
    /// Should painting be performed for the selection glyph.
    /// </summary>
    public bool CanPaint => true;

    /// <summary>
    /// Select the control that contains the group panel.
    /// </summary>
    public void SelectParentControl()
    {
        if (_panel?.Parent != null)
        {
            _selectionService?.SetSelectedComponents(new object[] { _panel.Parent }, SelectionTypes.Primary);
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        try
        {
            if (disposing)
            {
            }
        }
        finally
        {
            // Ensure base class is always disposed
            base.Dispose(disposing);
        }
    }

    /// <summary>
    /// Allows a designer to add to the set of properties that it exposes through a TypeDescriptor.
    /// </summary>
    /// <param name="properties">The properties for the class of the component.</param>
    protected override void PreFilterProperties(IDictionary properties)
    {
        // Let base class filter properties first
        base.PreFilterProperties(properties);

        // Remove the design time properties we do not want
        properties.Remove(@"Modifiers");
        properties.Remove(@"Locked");
        properties.Remove(@"GenerateMember");

        // Scan for the 'Name' property
        foreach (DictionaryEntry entry in properties)
        {
            // Get the property descriptor for the entry
            var descriptor = entry.Value as PropertyDescriptor;

            // Is this the 'Name' we are searching for?
            if (descriptor!.Name.Equals(@"Name") && descriptor.DesignTimeOnly)
            {
                // Hide the 'Name' property so the user cannot modify it
                var attributeArray = new Attribute[2] { BrowsableAttribute.No, DesignerSerializationVisibilityAttribute.Hidden };
                properties[entry.Key] = TypeDescriptor.CreateProperty(descriptor.ComponentType, descriptor, attributeArray);

                // Finished
                break;
            }
        }
    }

    /// <summary>
    /// Gets an attribute that indicates the type of inheritance of the associated component.
    /// </summary>
    protected override InheritanceAttribute? InheritanceAttribute
    {
        get
        {
            // If we have a valid Krypton splitter panel instance
            if (_panel?.Parent != null)
            {
                // Then get the attribute associated with the parent of the panel
                return TypeDescriptor.GetAttributes(_panel.Parent)[typeof(InheritanceAttribute)] as
                    InheritanceAttribute;
            }
            else
            {
                return base.InheritanceAttribute;
            }
        }
    }
    #endregion
}