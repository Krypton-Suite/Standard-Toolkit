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

internal class KryptonWrapLabelActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonWrapLabel _wrapLabel;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonWrapLabelActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonWrapLabelActionList(KryptonWrapLabelDesigner owner)
        : base(owner.Component)
    {
        // Remember the label instance
        _wrapLabel = (owner.Component as KryptonWrapLabel)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the label style.
    /// </summary>
    public LabelStyle LabelStyle
    {
        get => _wrapLabel.LabelStyle;

        set
        {
            if (_wrapLabel.LabelStyle != value)
            {
                _service?.OnComponentChanged(_wrapLabel, null, _wrapLabel.LabelStyle, value);
                _wrapLabel.LabelStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _wrapLabel.PaletteMode;

        set
        {
            if (_wrapLabel.PaletteMode != value)
            {
                _service?.OnComponentChanged(_wrapLabel, null, _wrapLabel.PaletteMode, value);
                _wrapLabel.PaletteMode = value;
            }
        }
    }

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    public Font Font
    {
        get => _wrapLabel.StateCommon.Font!;

        set
        {
            if (_wrapLabel.StateCommon.Font != value)
            {
                _service?.OnComponentChanged(_wrapLabel, null, _wrapLabel.StateCommon.Font, value);

                _wrapLabel.StateCommon.Font = value;
            }
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
        // Create a new collection for holding the single item we want to create
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a control instance at design time
        if (_wrapLabel != null)
        {
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(LabelStyle), @"Style", nameof(Appearance), @"Label style"));
            actions.Add(new DesignerActionPropertyItem(nameof(Font), nameof(Font), nameof(Appearance), @"The wrap label font."));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}