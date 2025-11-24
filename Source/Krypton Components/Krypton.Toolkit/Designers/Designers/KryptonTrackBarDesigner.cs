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

internal class KryptonTrackBarDesigner : ControlDesigner
{
    #region Instance Fields
    private KryptonTrackBar? _trackBar;
    #endregion

    #region Public Overrides
    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The IComponent to associate the designer with.</param>
    public override void Initialize([DisallowNull] IComponent component)
    {
        // ReSharper disable RedundantBaseQualifier
        // Let base class do standard stuff
        base.Initialize(component);

        Debug.Assert(component != null);

        base.AutoResizeHandles = true;
        // ReSharper restore RedundantBaseQualifier

        // Cast to correct type
        _trackBar = component as KryptonTrackBar;
    }

    /// <summary>
    /// Gets the selection rules that indicate the movement capabilities of a component.
    /// </summary>
    public override SelectionRules SelectionRules
    {
        get
        {
            if (!_trackBar!.AutoSize)
            {
                return SelectionRules.AllSizeable | SelectionRules.Moveable;
            }
            else
            {
                return _trackBar.Orientation == Orientation.Horizontal
                    ? SelectionRules.RightSizeable | SelectionRules.LeftSizeable | SelectionRules.Moveable
                    : SelectionRules.TopSizeable | SelectionRules.BottomSizeable | SelectionRules.Moveable;
            }
        }
    }

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
                // Add the button specific list
                new KryptonTrackBarActionList(this)
            };

            return actionLists;
        }
    }
    #endregion
}