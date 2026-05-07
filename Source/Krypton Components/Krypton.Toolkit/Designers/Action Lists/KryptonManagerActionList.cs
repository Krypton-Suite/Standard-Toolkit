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

internal class KryptonManagerActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonManager _manager;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonManagerActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonManagerActionList(KryptonManagerDesigner owner)
        : base(owner.Component)
    {
        // Remember the panel instance
        _manager = (owner.Component as KryptonManager)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the global palette mode.
    /// </summary>
    public PaletteMode GlobalPaletteMode
    {
        get => _manager.GlobalPaletteMode;

        set
        {
            if (_manager.GlobalPaletteMode != value)
            {
                _service?.OnComponentChanged(_manager, null, _manager.GlobalPaletteMode, value);
                _manager.GlobalPaletteMode = value;
            }
        }
    }

    #endregion

    #region Implementation

    private void OnReset(object? sender, EventArgs e)
    {
        if (_manager != null)
        {
            DialogResult result = KryptonMessageBox.Show(
                @"This will reset the current theme back to 'Microsoft 365 - Blue'. Do you want to continue?",
                @"Reset Theme",
                KryptonMessageBoxButtons.YesNo,
                KryptonMessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                _manager.GlobalPaletteMode = PaletteMode.Microsoft365Blue;

                _service?.OnComponentChanged(_manager, null, _manager.GlobalPaletteMode, PaletteMode.Microsoft365Blue);

                //UpdateVerbStatus();
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

        // This can be null when deleting a component instance at design time
        if (_manager != null)
        {
            // Add the list of panel specific actions
            actions.Add(new DesignerActionHeaderItem(@"Actions"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Reset to Default Theme", OnReset), @"Actions"));
            /*actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Add language manager", OnAddLanguageManager), "Actions"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Remove language manager", OnRemoveLanguageManager), "Actions"));
            actions.Add(new DesignerActionHeaderItem(@"Data"));*/
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(GlobalPaletteMode), @"Global Palette", @"Visuals", @"Global palette setting"));
        }

        return actions;
    }

    #endregion
}