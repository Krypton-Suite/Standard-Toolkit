#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[ToolboxItem(false)]
[Category(@"code")]
[TypeConverter(typeof(ExpandableObjectConverter))]
public class PoweredByValues : Storage
{
    #region Static Fields

    private const string DEFAULT_CAPTION = @"Powered By";

    private const string DEFAULT_DESCRIPTION =
        @"Some of the components used in this application are part of the Krypton Standard Toolkit. To learn more, click here.";

    private const string DEFAULT_DOCUMENTATION = @"Download the latest documentation";

    private const string DEFAULT_DISCORD = @"Join our Discord Server";

    #endregion

    #region Instance Fields



    #endregion

    public PoweredByValues()
    {
        Reset();
    }

    public void Reset()
    {
        CaptionText = DEFAULT_CAPTION;

        DescriptionText = DEFAULT_DESCRIPTION;

        DocumentationText = DEFAULT_DOCUMENTATION;

        DiscordText = DEFAULT_DISCORD;
    }

    [Localizable(true)]
    [Description(@"")]
    [DefaultValue(DEFAULT_CAPTION)]
    public string CaptionText { get; set; }

    public string DescriptionText { get; set; }

    public string DocumentationText { get; set; }

    public string DiscordText { get; set; }

    public LinkArea DescriptionLinkArea { get; set; }

    public LinkArea DocumentationLinkArea { get; set; }

    public LinkArea DiscordLinkArea { get; set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => CaptionText.Equals(DEFAULT_CAPTION) &&
                                      DescriptionText.Equals(DEFAULT_DESCRIPTION) &&
                                      DocumentationText.Equals(DEFAULT_DOCUMENTATION) &&
                                      DiscordText.Equals(DEFAULT_DISCORD);
}