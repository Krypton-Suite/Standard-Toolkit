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
/// Action list for the KryptonCustomPaletteBase control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonCustomPaletteBaseExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonCustomPaletteBase? _customPaletteBase;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCustomPaletteBaseExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonCustomPaletteBaseExtensibilityActionList(ComponentDesigner owner)
        : base(owner)
    {
        _customPaletteBase = (KryptonCustomPaletteBase?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the palette name.
    /// </summary>
    public string PaletteName
    {
        get => _customPaletteBase?.PaletteName ?? string.Empty;
        set => SetPropertyValue(_customPaletteBase!, nameof(PaletteName), PaletteName, value, v => _customPaletteBase!.SetPaletteName(v));
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
        if (_customPaletteBase != null)
        {
            // Add the list of CustomPaletteBase specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteName), @"Palette Name", @"Appearance", @"Palette name"));
        }

        return actions;
    }
    #endregion
}
