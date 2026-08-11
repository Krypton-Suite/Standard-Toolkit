#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Designer for <see cref="KryptonComboBoxUserControl"/>. Exposes the action list and ensures the
/// drop button collection participates in selection / copy operations.
/// </summary>
internal class KryptonComboBoxUserControlDesigner : ControlDesigner
{
    #region Instance Fields

    private KryptonComboBoxUserControl? _comboUserControl;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonComboBoxUserControlDesigner"/> class.
    /// </summary>
    public KryptonComboBoxUserControlDesigner()
    {
        AutoResizeHandles = true;
    }

    #endregion

    #region Public Overrides

    /// <summary>
    /// Initializes the designer with the specified component.
    /// </summary>
    /// <param name="component">The IComponent to associate the designer with.</param>
    public override void Initialize(IComponent component)
    {
        base.Initialize(component);

        _comboUserControl = component as KryptonComboBoxUserControl;
    }

    /// <summary>
    /// Gets the collection of components associated with the component managed by the designer.
    /// Includes the inherited <c>ButtonSpecs</c> so they participate in select/copy/cut/paste.
    /// </summary>
    public override ICollection AssociatedComponents =>
        _comboUserControl?.ButtonSpecs ?? base.AssociatedComponents;

    /// <summary>
    /// Gets the selection rules that indicate the movement capabilities of a component.
    /// </summary>
    public override SelectionRules SelectionRules
    {
        get
        {
            // Allow horizontal resize but lock the height (matches KryptonTextBox / KryptonComboBox)
            SelectionRules rules = base.SelectionRules;
            rules &= ~(SelectionRules.TopSizeable | SelectionRules.BottomSizeable);
            return rules;
        }
    }

    /// <summary>
    /// Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            DesignerActionListCollection actions = new DesignerActionListCollection();
            actions.Add(new KryptonComboBoxUserControlActionList(this));
            return actions;
        }
    }

    #endregion
}
