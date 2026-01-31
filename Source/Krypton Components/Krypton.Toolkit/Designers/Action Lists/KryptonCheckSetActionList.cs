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

internal class KryptonCheckSetActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonCheckSet? _set;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCheckSetActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonCheckSetActionList(KryptonCheckSetDesigner owner)
        : base(owner.Component) =>
        // Remember the check set component instance
        _set = owner.Component as KryptonCheckSet;

    #endregion

    #region Public
    /*
     /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    public Font StateCommonShortTextFont
    {
        get => _set..StateCommon.Content.ShortText.Font;

        set
        {
            if (_button.StateCommon.Content.ShortText.Font != value)
            {
                _service.OnComponentChanged(_button, null, _button.StateCommon.Content.ShortText.Font, value);

                _button.StateCommon.Content.ShortText.Font = value;
            }
        }
    }

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    public Font StateCommonLongTextFont
    {
        get => _button.StateCommon.Content.LongText.Font;

        set
        {
            if (_button.StateCommon.Content.ShortText.Font != value)
            {
                _service.OnComponentChanged(_button, null, _button.StateCommon.Content.LongText.Font, value);

                _button.StateCommon.Content.LongText.Font = value;
            }
        }
    }
    */
    #endregion

    #region Public Override
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        // Create a new collection for holding the single item we want to create
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a control instance at design time
        if (_set != null)
        {
            // Add the list of check set specific actions
        }

        return actions;
    }
    #endregion
}