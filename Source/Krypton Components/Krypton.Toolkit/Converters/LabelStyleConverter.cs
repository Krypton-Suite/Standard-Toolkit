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
/// Custom type converter so that LabelStyle values appear as neat text at design time.
/// </summary>
internal class LabelStyleConverter : StringLookupConverter<LabelStyle>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<LabelStyle, string> _pairs = new BiDictionary<LabelStyle, string>(
        new Dictionary<LabelStyle, string>
        {
            {LabelStyle.AlternateControl, DesignTimeUtilities.DEFAULT_LABEL_STYLE_ALTERNATE_CONTROL},
            {LabelStyle.NormalControl, DesignTimeUtilities.DEFAULT_LABEL_STYLE_NORMAL_CONTROL},
            {LabelStyle.BoldControl, DesignTimeUtilities.DEFAULT_LABEL_STYLE_BOLD_CONTROL},
            {LabelStyle.ItalicControl, DesignTimeUtilities.DEFAULT_LABEL_STYLE_ITALIC_CONTROL},
            {LabelStyle.TitleControl, DesignTimeUtilities.DEFAULT_LABEL_STYLE_TITLE_CONTROL},
            {LabelStyle.AlternatePanel, DesignTimeUtilities.DEFAULT_LABEL_STYLE_ALTERNATE_PANEL },
            {LabelStyle.NormalPanel, DesignTimeUtilities.DEFAULT_LABEL_STYLE_NORMAL_PANEL},
            {LabelStyle.BoldPanel, DesignTimeUtilities.DEFAULT_LABEL_STYLE_BOLD_PANEL},
            {LabelStyle.ItalicPanel, DesignTimeUtilities.DEFAULT_LABEL_STYLE_ITALIC_PANEL},
            {LabelStyle.TitlePanel, DesignTimeUtilities.DEFAULT_LABEL_STYLE_TITLE_PANEL},
            {LabelStyle.GroupBoxCaption, DesignTimeUtilities.DEFAULT_LABEL_STYLE_GROUP_BOX_CAPTION},
            {LabelStyle.ToolTip, DesignTimeUtilities.DEFAULT_LABEL_STYLE_TOOL_TIP},
            {LabelStyle.SuperTip, DesignTimeUtilities.DEFAULT_LABEL_STYLE_SUPER_TIP},
            {LabelStyle.KeyTip, DesignTimeUtilities.DEFAULT_LABEL_STYLE_KEY_TIP},
            {LabelStyle.Custom1, DesignTimeUtilities.DEFAULT_LABEL_STYLE_CUSTOM_ONE},
            {LabelStyle.Custom2, DesignTimeUtilities.DEFAULT_LABEL_STYLE_CUSTOM_TWO},
            {LabelStyle.Custom3, DesignTimeUtilities.DEFAULT_LABEL_STYLE_CUSTOM_THREE}
        });

    #endregion

    #region Protected

    /// <summary>
    /// Gets an array of lookup pairs.
    /// </summary>
    protected override IReadOnlyDictionary<LabelStyle /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    protected override IReadOnlyDictionary<string /*Display*/, LabelStyle /*Enum*/ > PairsStringToEnum => _pairs.SecondToFirst;

    #endregion
}