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

internal class KryptonGroupBoxDesigner : ParentControlDesigner
{
    #region Instance Fields
    private KryptonGroupBox? _groupBox;
    private IDesignerHost? _designerHost;
    #endregion

    #region Public
    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The IComponent to associate the designer with.</param>
    public override void Initialize([DisallowNull] IComponent component)
    {
        // Let base class do standard stuff
        base.Initialize(component);

        Debug.Assert(component != null);

        // Cast to correct type
        _groupBox = component as KryptonGroupBox;

        // The resizing handles around the control need to change depending on the
        // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
        // do not get the resizing handles, otherwise you do.
        AutoResizeHandles = true;

        // Acquire service interfaces
        _designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;

        // Let the internal panel in the container be designable
        if (_groupBox != null)
        {
            EnableDesignMode(_groupBox.Panel, nameof(Panel));
        }
    }

    /// <summary>
    /// Indicates whether the specified control can be a child of the control managed by a designer.
    /// </summary>
    /// <param name="control">The Control to test.</param>
    /// <returns>true if the specified control can be a child of the control managed by this designer; otherwise, false.</returns>
    public override bool CanParent(Control control) =>
        // We never allow anything to be added to the header group
        false;

    /// <summary>
    /// Returns the internal control designer with the specified index in the ControlDesigner.
    /// </summary>
    /// <param name="internalControlIndex">A specified index to select the internal control designer. This index is zero-based.</param>
    /// <returns>A ControlDesigner at the specified index.</returns>
    public override ControlDesigner? InternalControlDesigner(int internalControlIndex) =>
        // Get the control designer for the requested indexed child control
        (_groupBox != null) && (internalControlIndex == 0) 
            ? _designerHost?.GetDesigner(_groupBox.Panel) as ControlDesigner 
            : null;

    /// <summary>
    /// Returns the number of internal control designers in the ControlDesigner.
    /// </summary>
    /// <returns>The number of internal control designers in the ControlDesigner.</returns>
    public override int NumberOfInternalControlDesigners() => _groupBox != null ? 1 : 0;

    /// <summary>
    ///  Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            // Create a collection of action lists
            var actionLists = new DesignerActionListCollection
            {
                // Add the group box specific list
                new KryptonGroupBoxActionList(this)
            };

            return actionLists;
        }
    }
    #endregion
}