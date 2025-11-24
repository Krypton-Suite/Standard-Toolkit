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

internal class KryptonDomainUpDownActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonDomainUpDown _domainUpDown;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonDomainUpDownActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonDomainUpDownActionList(KryptonDomainUpDownDesigner owner)
        : base(owner.Component)
    {
        // Remember the text box instance
        _domainUpDown = (owner.Component as KryptonDomainUpDown)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public
    /// <summary>Gets or sets the Krypton Context Menu.</summary>
    /// <value>The Krypton Context Menu.</value>
    public KryptonContextMenu? KryptonContextMenu
    {
        get => _domainUpDown.KryptonContextMenu;

        set
        {
            if (_domainUpDown.KryptonContextMenu != value)
            {
                _service?.OnComponentChanged(_domainUpDown, null, _domainUpDown.KryptonContextMenu, value);

                _domainUpDown.KryptonContextMenu = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _domainUpDown.PaletteMode;

        set
        {
            if (_domainUpDown.PaletteMode != value)
            {
                _service?.OnComponentChanged(_domainUpDown, null, _domainUpDown.PaletteMode, value);
                _domainUpDown.PaletteMode = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the input control style.
    /// </summary>
    public InputControlStyle InputControlStyle
    {
        get => _domainUpDown.InputControlStyle;

        set
        {
            if (_domainUpDown.InputControlStyle != value)
            {
                _service?.OnComponentChanged(_domainUpDown, null, _domainUpDown.InputControlStyle, value);
                _domainUpDown.InputControlStyle = value;
            }
        }
    }

    public Font Font
    {
        get => _domainUpDown.StateCommon.Content.Font!;

        set
        {
            if (!Equals(_domainUpDown.StateCommon.Content.Font, value))
            {
                _service?.OnComponentChanged(_domainUpDown, null, _domainUpDown.StateCommon.Content.Font, value);

                _domainUpDown.StateCommon.Content.Font = value;
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
        if (_domainUpDown != null)
        {
            // Add the list of label specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(KryptonContextMenu), @"Krypton Context Menu", nameof(Appearance), @"The Krypton Context Menu for the control."));
            actions.Add(new DesignerActionPropertyItem(nameof(InputControlStyle), @"Style", nameof(Appearance), @"DomainUpDown display style."));
            actions.Add(new DesignerActionPropertyItem(nameof(Font), nameof(Font), nameof(Appearance), @"The font for the domain up down."));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}