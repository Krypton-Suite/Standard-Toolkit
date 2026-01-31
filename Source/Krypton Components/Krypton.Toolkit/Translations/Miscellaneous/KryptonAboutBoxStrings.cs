#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonAboutBoxStrings : GlobalId
{
    #region Static Strings

    private const string DEFAULT_ABOUT = @"About";

    private const string DEFAULT_TITLE = @"Title";

    private const string DEFAULT_COPYRIGHT = @"Copyright";

    private const string DEFAULT_DESCRIPTION = @"Description";

    private const string DEFAULT_COMPANY = @"Company";

    private const string DEFAULT_PRODUCT = @"Product";

    private const string DEFAULT_TRADE_MARK = @"Trademark";

    private const string DEFAULT_VERSION = @"Version";

    private const string DEFAULT_BUILD_DATE = @"Build Date";

    private const string DEFAULT_IMAGE_RUNTIME_VERSION = @"Image Runtime Version";

    private const string DEFAULT_LOADED_FROM_GLOBAL_ASSEMBLY_CACHE = @"Loaded from GAC";

    #endregion

    #region Identity

    public KryptonAboutBoxStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    /// <summary>Gets or sets the about string.</summary>
    /// <value>The about string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The about box 'About' string.")]
    [DefaultValue(DEFAULT_ABOUT)]
    public string About { get; set; }

    /// <summary>Gets or sets the 'title' label string.</summary>
    /// <value>The 'title' label string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The about box 'Title' label string.")]
    [DefaultValue(DEFAULT_TITLE)]
    public string Title { get; set; }

    /// <summary>Gets or sets the 'copyright' label string.</summary>
    /// <value>The 'copyright' label string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The about box 'Copyright' label string.")]
    [DefaultValue(DEFAULT_COPYRIGHT)]
    public string Copyright { get; set; }

    /// <summary>Gets or sets the 'description' label string.</summary>
    /// <value>The 'description' label string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The about box 'Description' label string.")]
    [DefaultValue(DEFAULT_DESCRIPTION)]
    public string Description { get; set; }

    /// <summary>Gets or sets the 'company' label string.</summary>
    /// <value>The 'company' label string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The about box 'Company' label string.")]
    [DefaultValue(DEFAULT_COMPANY)]
    public string Company { get; set; }

    /// <summary>Gets or sets the 'product' label string.</summary>
    /// <value>The 'product' label string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The about box 'Product' label string.")]
    [DefaultValue(DEFAULT_PRODUCT)]
    public string Product { get; set; }

    /// <summary>Gets or sets the 'trademark' label string.</summary>
    /// <value>The 'trademark' label string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The about box 'Trademark' label string.")]
    [DefaultValue(DEFAULT_TRADE_MARK)]
    public string Trademark { get; set; }

    /// <summary>Gets or sets the 'version' label string.</summary>
    /// <value>The 'version' label string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The about box 'Version' label string.")]
    [DefaultValue(DEFAULT_VERSION)]
    public string Version { get; set; }

    /// <summary>Gets or sets the 'build date' label string.</summary>
    /// <value>The 'build date' label string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The about box 'Build Date' label string.")]
    [DefaultValue(DEFAULT_BUILD_DATE)]
    public string BuildDate { get; set; }

    /// <summary>Gets or sets the image runtime version string.</summary>
    /// <value>The image runtime version string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The image runtime version string.")]
    [DefaultValue(DEFAULT_IMAGE_RUNTIME_VERSION)]
    public string ImageRuntimeVersion { get; set; }

    /// <summary>Gets or sets the loaded from global assembly cache string.</summary>
    /// <value>The loaded from global assembly cache string.</value>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"The loaded from global assembly cache string.")]
    [DefaultValue(DEFAULT_LOADED_FROM_GLOBAL_ASSEMBLY_CACHE)]
    public string LoadedFromGlobalAssemblyCache { get; set; }

    #endregion

    #region Implementation

    /// <summary>Gets a value indicating whether this instance is default.</summary>
    /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
    [Browsable(false)]
    public bool IsDefault => About.Equals(DEFAULT_ABOUT) &&
                             Title.Equals(DEFAULT_TITLE) &&
                             Copyright.Equals(DEFAULT_COPYRIGHT) &&
                             Description.Equals(DEFAULT_DESCRIPTION) &&
                             Company.Equals(DEFAULT_COMPANY) &&
                             Product.Equals(DEFAULT_PRODUCT) &&
                             Trademark.Equals(DEFAULT_TRADE_MARK) &&
                             Version.Equals(DEFAULT_VERSION) &&
                             BuildDate.Equals(DEFAULT_BUILD_DATE) &&
                             ImageRuntimeVersion.Equals(DEFAULT_IMAGE_RUNTIME_VERSION) &&
                             LoadedFromGlobalAssemblyCache.Equals(DEFAULT_LOADED_FROM_GLOBAL_ASSEMBLY_CACHE);

    /// <summary>Resets the strings.</summary>
    public void Reset()
    {
        About = DEFAULT_ABOUT;

        Title = DEFAULT_TITLE;

        Copyright = DEFAULT_COPYRIGHT;

        Description = DEFAULT_DESCRIPTION;

        Company = DEFAULT_COMPANY;

        Product = DEFAULT_PRODUCT;

        Trademark = DEFAULT_TRADE_MARK;

        Version = DEFAULT_VERSION;

        BuildDate = DEFAULT_BUILD_DATE;

        ImageRuntimeVersion = DEFAULT_IMAGE_RUNTIME_VERSION;

        LoadedFromGlobalAssemblyCache = DEFAULT_LOADED_FROM_GLOBAL_ASSEMBLY_CACHE;
    }

    #endregion
}