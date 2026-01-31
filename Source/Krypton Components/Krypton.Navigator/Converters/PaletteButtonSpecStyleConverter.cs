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

namespace Krypton.Navigator;

/// <summary>
/// Custom type converter so that PaletteNavButtonSpecStyle values appear as neat text at design time.
/// </summary>
internal class PaletteNavButtonSpecStyleConverter : StringLookupConverter<PaletteNavButtonSpecStyle>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<PaletteNavButtonSpecStyle, string> _pairs = new BiDictionary<PaletteNavButtonSpecStyle, string>(
        new Dictionary<PaletteNavButtonSpecStyle, string>
        {
            {PaletteNavButtonSpecStyle.Generic, @"Generic"},
            {PaletteNavButtonSpecStyle.ArrowLeft, @"Arrow Left"},
            {PaletteNavButtonSpecStyle.ArrowRight, @"Arrow Right"},
            {PaletteNavButtonSpecStyle.ArrowUp, @"Arrow Up"},
            {PaletteNavButtonSpecStyle.ArrowDown, @"Arrow Down"},
            {PaletteNavButtonSpecStyle.DropDown, @"Drop Down"},
            {PaletteNavButtonSpecStyle.PinVertical, @"Pin Vertical"},
            {PaletteNavButtonSpecStyle.PinHorizontal, @"Pin Horizontal"},
            {PaletteNavButtonSpecStyle.FormClose, @"Form Close"},
            {PaletteNavButtonSpecStyle.FormMax, @"Form Max"},
            {PaletteNavButtonSpecStyle.FormMin, @"Form Min"},
            {PaletteNavButtonSpecStyle.FormRestore, @"Form Restore"},
            {PaletteNavButtonSpecStyle.FormHelp, @"Form Help"},
            {PaletteNavButtonSpecStyle.PendantClose, @"Pendant Close"},
            {PaletteNavButtonSpecStyle.PendantMin, @"Pendant Min"},
            {PaletteNavButtonSpecStyle.PendantRestore, @"Pendant Restore"},
            {PaletteNavButtonSpecStyle.WorkspaceMaximize, @"Workspace Maximize"},
            {PaletteNavButtonSpecStyle.WorkspaceRestore, @"Workspace Restore"},
            {PaletteNavButtonSpecStyle.RibbonMinimize, @"Ribbon Minimize"},
            {PaletteNavButtonSpecStyle.RibbonExpand, @"Ribbon Expand" }
        });

    #endregion

    #region Protected
    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<string /*Display*/, PaletteNavButtonSpecStyle /*Enum*/ > PairsStringToEnum  => _pairs.SecondToFirst;
    protected override IReadOnlyDictionary<PaletteNavButtonSpecStyle /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    #endregion

}