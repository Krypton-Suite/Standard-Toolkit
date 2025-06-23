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
/// Custom type converter so that PopupPageAllow values appear as neat text at design time.
/// </summary>
public class PopupPageAllowConverter : StringLookupConverter<PopupPageAllow>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<PopupPageAllow, string> _pairs = new BiDictionary<PopupPageAllow, string>(
        new Dictionary<PopupPageAllow, string>
        {
            {PopupPageAllow.Never, @"Never"},
            {PopupPageAllow.OnlyCompatibleModes, @"Only Compatible Modes"},
            {PopupPageAllow.OnlyOutlookMiniMode, @"Only Outlook Mini Mode"}
        });

    #endregion

    #region Protected
    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<string /*Display*/, PopupPageAllow /*Enum*/ > PairsStringToEnum  => _pairs.SecondToFirst;
    protected override IReadOnlyDictionary<PopupPageAllow /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    #endregion
}