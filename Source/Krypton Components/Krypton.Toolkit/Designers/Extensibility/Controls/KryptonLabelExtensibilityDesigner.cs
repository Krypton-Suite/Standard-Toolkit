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
/// Designer for KryptonLabel using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonLabelExtensibilityDesigner : KryptonExtensibilityDesignerBase
{
    #region Instance Fields
    private KryptonLabel? _label;
    #endregion

    #region Public Overrides
    /// <summary>
    /// Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            // Create a collection of action lists
            var actionLists = new DesignerActionListCollection
            {
                // Add the label specific list
                new KryptonLabelExtensibilityActionList(this)
            };

            return actionLists;
        }
    }
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Initializes the specific designer implementation.
    /// </summary>
    /// <param name="component">The component being designed.</param>
    protected override void InitializeDesigner(IComponent component)
    {
        // Remember the actual control being designed
        _label = component as KryptonLabel;

        if (_label != null)
        {
            // Hook into label events if needed
            // _label.SomeEvent += OnSomeEvent;
        }
    }

    /// <summary>
    /// Handles component removal events.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A ComponentEventArgs that contains the event data.</param>
    protected override void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        if (_label != null && e.Component == _label)
        {
            // Unhook from label events if needed
            // _label.SomeEvent -= OnSomeEvent;
        }

        base.OnComponentRemoving(sender, e);
    }
    #endregion
}
