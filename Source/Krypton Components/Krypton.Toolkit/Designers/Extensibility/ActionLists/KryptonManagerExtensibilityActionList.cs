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
/// Action list for the KryptonManager control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonManagerExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonManager? _manager;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonManagerExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonManagerExtensibilityActionList(ComponentDesigner owner)
        : base(owner)
    {
        _manager = (KryptonManager?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the global palette mode.
    /// </summary>
    public PaletteMode GlobalPaletteMode
    {
        get => _manager?.GlobalPaletteMode ?? PaletteMode.Office2010Blue;
        set => SetPropertyValue(_manager!, nameof(GlobalPaletteMode), GlobalPaletteMode, value, v => _manager!.GlobalPaletteMode = v);
    }

    /// <summary>
    /// Gets and sets the global custom palette.
    /// </summary>
    public KryptonCustomPaletteBase? GlobalCustomPalette
    {
        get => _manager?.GlobalCustomPalette;
        set => SetPropertyValue(_manager!, nameof(GlobalCustomPalette), GlobalCustomPalette, value, v => _manager!.GlobalCustomPalette = v);
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
        if (_manager != null)
        {
            // Add the list of Manager specific actions
            actions.Add(new DesignerActionHeaderItem(@"Global Settings"));
            actions.Add(new DesignerActionPropertyItem(nameof(GlobalPaletteMode), @"Global Palette Mode", @"Global Settings", @"Global palette mode"));
            actions.Add(new DesignerActionPropertyItem(nameof(GlobalCustomPalette), @"Global Custom Palette", @"Global Settings", @"Global custom palette"));
        }

        return actions;
    }
    #endregion
}
