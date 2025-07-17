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
/// Custom type converter so that NavigatorMode values appear as neat text at design time.
/// </summary>
public class NavigatorModeConverter : StringLookupConverter<NavigatorMode>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<NavigatorMode, string> _pairs = new BiDictionary<NavigatorMode, string>(
        new Dictionary<NavigatorMode, string>
        {
            {NavigatorMode.BarTabGroup, @"Bar - Tab - Group"},
            {NavigatorMode.BarTabOnly, @"Bar - Tab - Only"},
            {NavigatorMode.BarRibbonTabGroup, @"Bar - RibbonTab - Group"},
            {NavigatorMode.BarRibbonTabOnly, @"Bar - RibbonTab - Only"},
            {NavigatorMode.BarCheckButtonGroupOutside, @"Bar - CheckButton - Group - Outside"},
            {NavigatorMode.BarCheckButtonGroupInside, @"Bar - CheckButton - Group - Inside"},
            {NavigatorMode.BarCheckButtonGroupOnly, @"Bar - CheckButton - Group - Only"},
            {NavigatorMode.BarCheckButtonOnly, @"Bar - CheckButton - Only"},
            {NavigatorMode.HeaderBarCheckButtonGroup, @"HeaderBar - CheckButton - Group"},
            {NavigatorMode.HeaderBarCheckButtonHeaderGroup, @"HeaderBar - CheckButton - HeaderGroup"},
            {NavigatorMode.HeaderBarCheckButtonOnly, @"HeaderBar - CheckButton - Only"},
            {NavigatorMode.StackCheckButtonGroup, @"Stack - CheckButton - Group"},
            {NavigatorMode.StackCheckButtonHeaderGroup, @"Stack - CheckButton - HeaderGroup"},
            {NavigatorMode.OutlookFull, @"Outlook - Full"}, 
            {NavigatorMode.OutlookMini, @"Outlook - Mini"},
            {NavigatorMode.Panel, nameof(Panel)}, 
            {NavigatorMode.Group, @"Group"},
            {NavigatorMode.HeaderGroup, @"HeaderGroup"},
            {NavigatorMode.HeaderGroupTab, @"HeaderGroup - Tab"}
        });

    #endregion

    #region Protected
    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<string /*Display*/, NavigatorMode /*Enum*/ > PairsStringToEnum  => _pairs.SecondToFirst;
    protected override IReadOnlyDictionary<NavigatorMode /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    #endregion
}