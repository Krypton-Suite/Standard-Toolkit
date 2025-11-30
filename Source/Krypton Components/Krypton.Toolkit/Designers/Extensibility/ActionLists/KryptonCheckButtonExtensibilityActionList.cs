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
/// Action list for the KryptonCheckButton control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonCheckButtonExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonCheckButton? _checkButton;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCheckButtonExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonCheckButtonExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _checkButton = (KryptonCheckButton?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets whether the button is checked.
    /// </summary>
    public bool Checked
    {
        get => _checkButton?.Checked ?? false;
        set => SetPropertyValue(_checkButton!, nameof(Checked), Checked, value, v => _checkButton!.Checked = v);
    }

    /// <summary>
    /// Gets and sets whether the button can be unchecked.
    /// </summary>
    public bool AllowUncheck
    {
        get => _checkButton?.AllowUncheck ?? false;
        set => SetPropertyValue(_checkButton!, nameof(AllowUncheck), AllowUncheck, value, v => _checkButton!.AllowUncheck = v);
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

        // This can be null when deleting a control instance at design time
        if (_checkButton != null)
        {
            // Add the list of CheckButton specific actions
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Checked), @"Checked", @"Behavior", @"Initially checked"));
            actions.Add(new DesignerActionPropertyItem(nameof(AllowUncheck), @"Allow Uncheck", @"Behavior", @"Allow unchecking"));
        }

        return actions;
    }
    #endregion
}
