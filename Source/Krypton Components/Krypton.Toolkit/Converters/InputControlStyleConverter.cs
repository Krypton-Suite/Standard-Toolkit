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

/// <summary>
/// Custom type converter so that InputControl values appear as neat text at design time.
/// </summary>
internal class InputControlStyleConverter : StringLookupConverter<InputControlStyle>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<InputControlStyle, string> _pairs = new BiDictionary<InputControlStyle, string>(
        new Dictionary<InputControlStyle, string>
        {
            {InputControlStyle.Standalone, DesignTimeUtilities.DEFAULT_INPUT_CONTROL_STYLE_STANDALONE},
            {InputControlStyle.Ribbon, DesignTimeUtilities.DEFAULT_INPUT_CONTROL_STYLE_RIBBON},
            {InputControlStyle.Custom1, DesignTimeUtilities.DEFAULT_INPUT_CONTROL_STYLE_CUSTOM_ONE},
            {InputControlStyle.Custom2, DesignTimeUtilities.DEFAULT_INPUT_CONTROL_STYLE_CUSTOM_TWO},
            {InputControlStyle.Custom3, DesignTimeUtilities.DEFAULT_INPUT_CONTROL_STYLE_CUSTOM_THREE},
            {InputControlStyle.PanelClient, DesignTimeUtilities.DEFAULT_INPUT_CONTROL_STYLE_PANEL_CLIENT},
            {InputControlStyle.PanelAlternate, DesignTimeUtilities.DEFAULT_INPUT_CONTROL_STYLE_PANEL_ALTERNATE}
            // new(InputControlStyle.Disabled, "Disabled")
        });

    #endregion

    #region Protected
    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<InputControlStyle /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    protected override IReadOnlyDictionary<string /*Display*/, InputControlStyle /*Enum*/ > PairsStringToEnum => _pairs.SecondToFirst;

    #endregion
}