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
/// Designer for KryptonGroupBox using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonGroupBoxExtensibilityDesigner : KryptonExtensibilityParentDesignerBase
{
    #region Instance Fields
    private KryptonGroupBox? _groupBox;
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
                // Add the groupbox specific list
                new KryptonGroupBoxExtensibilityActionList(this)
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
        _groupBox = component as KryptonGroupBox;

        if (_groupBox != null)
        {
            // Hook into groupbox events if needed
            // _groupBox.SomeEvent += OnSomeEvent;
        }
    }

    /// <summary>
    /// Handles component removal events.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A ComponentEventArgs that contains the event data.</param>
    protected override void OnComponentRemoving(object? sender, ComponentEventArgs e)
    {
        if (_groupBox != null && e.Component == _groupBox)
        {
            // Unhook from groupbox events if needed
            // _groupBox.SomeEvent -= OnSomeEvent;
        }

        base.OnComponentRemoving(sender, e);
    }
    #endregion
}
