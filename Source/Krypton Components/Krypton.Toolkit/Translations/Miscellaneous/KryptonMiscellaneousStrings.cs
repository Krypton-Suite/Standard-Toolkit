#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonMiscellaneousStrings
{
    #region Static Strings

    private const string DEFAULT_FILE_NOT_FOUND_TEXT = @"File not found";
    private const string DEFAULT_POWERED_BY_TEXT = @"&Powered By";
    private const string DEFAULT_TOOLKIT_INFORMATION_TEXT = @"Krypton Toolkit information for";
    private const string DEFAULT_CANARY_TEXT = @"Canary";
    private const string DEFAULT_NIGHTLY_TEXT = @"Nightly";
    private const string DEFAULT_STABLE_TEXT = @"Stable";
    private const string DEFAULT_LONG_TERM_STABLE_TEXT = @"Long Term Stable";
    private const string DEFAULT_CHANGELOG_TEXT = @"&Changelog";
    private const string DEFAULT_README_TEXT = @"&Readme";

    #endregion

    #region Identity

    public KryptonMiscellaneousStrings()
    {
        Reset();
    }

    #endregion

    #region Public

    /// <summary>Gets or sets the file not found text.</summary>
    /// <value>The file not found text.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The file not found text.")]
    [DefaultValue(DEFAULT_FILE_NOT_FOUND_TEXT)]
    public string FileNotFoundText { get; set; }

    /// <summary>Gets or sets the powered by text.</summary>
    /// <value>The powered by text.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The powered by text.")]
    [DefaultValue(DEFAULT_POWERED_BY_TEXT)]
    public string PoweredByText { get; set; }

    /// <summary>Gets or sets the toolkit information text.</summary>
    /// <value>The toolkit information text.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The toolkit information text.")]
    [DefaultValue(DEFAULT_TOOLKIT_INFORMATION_TEXT)]
    public string ToolkitInformationText { get; set; }

    /// <summary>Gets or sets the canary text.</summary>
    /// <value>The canary text.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The canary text.")]
    [DefaultValue(DEFAULT_CANARY_TEXT)]
    public string CanaryText { get; set; }

    /// <summary>Gets or sets the nightly text.</summary>
    /// <value>The nightly text.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The nightly text.")]
    [DefaultValue(DEFAULT_NIGHTLY_TEXT)]
    public string NightlyText { get; set; }

    /// <summary>Gets or sets the stable text.</summary>
    /// <value>The stable text.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The stable text.")]
    [DefaultValue(DEFAULT_STABLE_TEXT)]
    public string StableText { get; set; }

    /// <summary>Gets or sets the long term stable text.</summary>
    /// <value>The long term stable text.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The long term stable text.")]
    [DefaultValue(DEFAULT_LONG_TERM_STABLE_TEXT)]
    public string LongTermStableText { get; set; }

    /// <summary>Gets or sets the change log text.</summary>
    /// <value>The change log text.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The changelog text.")]
    [DefaultValue(DEFAULT_CHANGELOG_TEXT)]
    public string ChangeLogText { get; set; }

    /// <summary>Gets or sets the readme text.</summary>
    /// <value>The readme text.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The readme text.")]
    [DefaultValue(DEFAULT_README_TEXT)]
    public string ReadmeText { get; set; }

    #endregion

    #region IsDefault

    [Browsable(false)]
    public bool IsDefault => FileNotFoundText.Equals(DEFAULT_FILE_NOT_FOUND_TEXT) &&
                             PoweredByText.Equals(DEFAULT_POWERED_BY_TEXT) &&
                             ToolkitInformationText.Equals(DEFAULT_TOOLKIT_INFORMATION_TEXT) &&
                             CanaryText.Equals(DEFAULT_CANARY_TEXT) &&
                             NightlyText.Equals(DEFAULT_NIGHTLY_TEXT) &&
                             StableText.Equals(DEFAULT_STABLE_TEXT) &&
                             LongTermStableText.Equals(DEFAULT_LONG_TERM_STABLE_TEXT) &&
                             ChangeLogText.Equals(DEFAULT_CHANGELOG_TEXT) &&
                             ReadmeText.Equals(DEFAULT_README_TEXT);
    #endregion

    #region Implementation

    public void Reset()
    {
        FileNotFoundText = DEFAULT_FILE_NOT_FOUND_TEXT;

        PoweredByText = DEFAULT_POWERED_BY_TEXT;

        ToolkitInformationText = DEFAULT_TOOLKIT_INFORMATION_TEXT;

        CanaryText = DEFAULT_CANARY_TEXT;

        NightlyText = DEFAULT_NIGHTLY_TEXT;

        StableText = DEFAULT_STABLE_TEXT;

        LongTermStableText = DEFAULT_LONG_TERM_STABLE_TEXT;

        ChangeLogText = DEFAULT_CHANGELOG_TEXT;

        ReadmeText = DEFAULT_README_TEXT;
    }

    #endregion

    #region Public Overrides

    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticValues.DEFAULT_EMPTY_STRING;

    #endregion
}