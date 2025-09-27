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
/// Action list for the KryptonPoweredByButton control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonPoweredByButtonExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonPoweredByButton? _poweredByButton;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPoweredByButtonExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonPoweredByButtonExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _poweredByButton = (KryptonPoweredByButton?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the text associated with the control.
    /// </summary>
    public string Text
    {
        get => _poweredByButton?.Text ?? string.Empty;
        set => SetPropertyValue(_poweredByButton!, nameof(Text), Text, value, v => _poweredByButton!.Text = v);
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
        if (_poweredByButton != null)
        {
            // Add the list of PoweredByButton specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), @"Text", @"Appearance", @"Text content"));
        }

        return actions;
    }
    #endregion
}
