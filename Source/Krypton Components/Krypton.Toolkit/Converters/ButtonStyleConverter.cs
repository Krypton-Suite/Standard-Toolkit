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
/// Custom type converter so that ButtonStyle values appear as neat text at design time.
/// </summary>
internal class ButtonStyleConverter : StringLookupConverter<ButtonStyle>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<ButtonStyle, string> _pairs = new BiDictionary<ButtonStyle, string>(
        new Dictionary<ButtonStyle, string>
        {
            {ButtonStyle.Standalone, DesignTimeUtilities.DEFAULT_BUTTON_SPEC_STYLE_STANDALONE},
            {ButtonStyle.Alternate, DesignTimeUtilities.DEFAULT_BUTTON_SPEC_STYLE_ALTERNATE},
            {ButtonStyle.LowProfile, DesignTimeUtilities.DEFAULT_BUTTON_SPEC_STYLE_LOW_PROFILE},
            {ButtonStyle.ButtonSpec, DesignTimeUtilities.DEFAULT_BUTTON_SPEC_STYLE_BUTTON_SPEC},
            {ButtonStyle.BreadCrumb, DesignTimeUtilities.DEFAULT_BUTTON_SPEC_STYLE_BREAD_CRUMB},
            {ButtonStyle.CalendarDay, DesignTimeUtilities.DEFAULT_BUTTON_SPEC_STYLE_CALENDAR_DAY},
            {ButtonStyle.Cluster, DesignTimeUtilities.DEFAULT_BUTTON_SPEC_STYLE_CLUSTER},
            {ButtonStyle.Gallery, DesignTimeUtilities.DEFAULT_BUTTON_SPEC_STYLE_GALLERY},
            {ButtonStyle.NavigatorStack, DesignTimeUtilities.DEFAULT_BUTTON_SPEC_STYLE_NAVIGATOR_STACK},
            {ButtonStyle.NavigatorOverflow, DesignTimeUtilities.DEFAULT_BUTTON_SPEC_STYLE_NAVIGATOR_OVERFLOW},
            {ButtonStyle.NavigatorMini, DesignTimeUtilities.DEFAULT_BUTTON_SPEC_STYLE_NAVIGATOR_MINI},
            {ButtonStyle.InputControl, DesignTimeUtilities.DEFAULT_BUTTON_SPEC_STYLE_INPUT_CONTROL},
            {ButtonStyle.ListItem, DesignTimeUtilities.DEFAULT_BUTTON_SPEC_STYLE_LIST_ITEM},
            {ButtonStyle.Form, DesignTimeUtilities.DEFAULT_BUTTON_SPEC_STYLE_FORM},
            {ButtonStyle.FormClose, DesignTimeUtilities.DEFAULT_BUTTON_SPEC_STYLE_FORM_CLOSE},
            {ButtonStyle.Command, DesignTimeUtilities.DEFAULT_BUTTON_SPEC_STYLE_COMMAND},
            {ButtonStyle.Custom1, DesignTimeUtilities.DEFAULT_BUTTON_SPEC_STYLE_CUSTOM_ONE},
            {ButtonStyle.Custom2, DesignTimeUtilities.DEFAULT_BUTTON_SPEC_STYLE_CUSTOM_TWO},
            {ButtonStyle.Custom3, DesignTimeUtilities.DEFAULT_BUTTON_SPEC_STYLE_CUSTOM_THREE}
        });

    #endregion

    #region Protected

    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<ButtonStyle /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    protected override IReadOnlyDictionary<string /*Display*/, ButtonStyle /*Enum*/ > PairsStringToEnum => _pairs.SecondToFirst;

    #endregion
}