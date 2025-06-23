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
/// Custom type converter so that ButtonDisplay values appear as neat text at design time.
/// </summary>
public class ButtonDisplayConverter : StringLookupConverter<ButtonDisplay>
{
    private static readonly BiDictionary<ButtonDisplay, string> _pairs = new BiDictionary<ButtonDisplay, string>(
        new Dictionary<ButtonDisplay, string>
        {
            {ButtonDisplay.Hide, @"Hide"},
            {ButtonDisplay.ShowDisabled, @"Show Disabled"},
            {ButtonDisplay.ShowEnabled, @"Show Enabled"},
            {ButtonDisplay.Logic, @"Logic"}
        });

    #region Protected
    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<string /*Display*/, ButtonDisplay /*Enum*/ > PairsStringToEnum  => _pairs.SecondToFirst;
    protected override IReadOnlyDictionary<ButtonDisplay /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    #endregion
}