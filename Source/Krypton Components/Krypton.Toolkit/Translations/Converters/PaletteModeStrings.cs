#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>Exposes the set of <see cref="PaletteModeConverter"/> strings used within Krypton and that are localizable.</summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PaletteModeStrings : GlobalId
    {
        #region Static Fields

        private const string DEFAULT_PALETTE_SYSTEM = @"Professional - System";
        private const string DEFAULT_PALETTE_OFFICE_2003 = @"Professional - Office 2003";
        private const string DEFAULT_PALETTE_OFFICE_2007_BLACK = @"Office 2007 - Black";
        private const string DEFAULT_PALETTE_OFFICE_2007_BLUE = @"Office 2007 - Blue";
        private const string DEFAULT_PALETTE_OFFICE_2007_SILVER = @"Office 2007 - Silver";
        private const string DEFAULT_PALETTE_OFFICE_2007_WHITE = @"Office 2007 - White";
        private const string DEFAULT_PALETTE_OFFICE_2007_BLACK_DARK_MODE = @"Office 2007 - Black (Dark Mode)";
        private const string DEFAULT_PALETTE_OFFICE_2007_BLUE_DARK_MODE = @"Office 2007 - Blue (Dark Mode)";
        private const string DEFAULT_PALETTE_OFFICE_2007_SILVER_DARK_MODE = @"Office 2007 - Silver (Dark Mode)";
        private const string DEFAULT_PALETTE_OFFICE_2007_DARK_GRAY = @"Office 2007 - Dark Gray";
        private const string DEFAULT_PALETTE_OFFICE_2007_BLUE_LIGHT_MODE = @"Office 2007 - Blue (Light Mode)";
        private const string DEFAULT_PALETTE_OFFICE_2007_SILVER_LIGHT_MODE = @"Office 2007 - Silver (Light Mode)";
        private const string DEFAULT_PALETTE_OFFICE_2007_LIGHT_GRAY = @"Office 2007 - Light Gray";
        private const string DEFAULT_PALETTE_OFFICE_2010_BLACK = @"Office 2010 - Black";
        private const string DEFAULT_PALETTE_OFFICE_2010_BLUE = @"Office 2010 - Blue";
        private const string DEFAULT_PALETTE_OFFICE_2010_SILVER = @"Office 2010 - Silver";
        private const string DEFAULT_PALETTE_OFFICE_2010_WHITE = @"Office 2010 - White";
        private const string DEFAULT_PALETTE_OFFICE_2010_BLACK_DARK_MODE = @"Office 2010 - Black (Dark Mode)";
        private const string DEFAULT_PALETTE_OFFICE_2010_BLUE_DARK_MODE = @"Office 2010 - Blue (Dark Mode)";
        private const string DEFAULT_PALETTE_OFFICE_2010_SILVER_DARK_MODE = @"Office 2010 - Silver (Dark Mode)";
        private const string DEFAULT_PALETTE_OFFICE_2010_DARK_GRAY = @"Office 2010 - Dark Gray";
        private const string DEFAULT_PALETTE_OFFICE_2010_BLUE_LIGHT_MODE = @"Office 2010 - Blue (Light Mode)";
        private const string DEFAULT_PALETTE_OFFICE_2010_SILVER_LIGHT_MODE = @"Office 2010 - Silver (Light Mode)";
        private const string DEFAULT_PALETTE_OFFICE_2010_LIGHT_GRAY = @"Office 2010 - Light Gray";
        private const string DEFAULT_PALETTE_OFFICE_2013_WHITE = @"Office 2013 - White";
        private const string DEFAULT_PALETTE_OFFICE_2013_DARK_GRAY = @"Office 2013 - Dark Gray";
        private const string DEFAULT_PALETTE_OFFICE_2013_LIGHT_GRAY = @"Office 2013 - Light Gray";
        private const string DEFAULT_PALETTE_MICROSOFT_365_BLACK = @"Microsoft 365 - Black";
        private const string DEFAULT_PALETTE_MICROSOFT_365_BLUE = @"Microsoft 365 - Blue";
        private const string DEFAULT_PALETTE_MICROSOFT_365_SILVER = @"Microsoft 365 - Silver";
        private const string DEFAULT_PALETTE_MICROSOFT_365_WHITE = @"Microsoft 365 - White";
        private const string DEFAULT_PALETTE_MICROSOFT_365_BLACK_DARK_MODE = @"Microsoft 365 - Black (Dark Mode)";
        private const string DEFAULT_PALETTE_MICROSOFT_365_BLUE_DARK_MODE = @"Microsoft 365 - Blue (Dark Mode)";
        private const string DEFAULT_PALETTE_MICROSOFT_365_SILVER_DARK_MODE = @"Microsoft 365 - Silver (Dark Mode)";
        private const string DEFAULT_PALETTE_MICROSOFT_365_DARK_GRAY = @"Microsoft 365 - Dark Gray";
        private const string DEFAULT_PALETTE_MICROSOFT_365_BLUE_LIGHT_MODE = @"Microsoft 365 - Blue (Light Mode)";
        private const string DEFAULT_PALETTE_MICROSOFT_365_SILVER_LIGHT_MODE = @"Microsoft 365 - Silver (Light Mode)";
        private const string DEFAULT_PALETTE_MICROSOFT_365_LIGHT_GRAY = @"Microsoft 365 - Light Gray";
        private const string DEFAULT_PALETTE_SPARKLE_BLUE = @"Sparkle - Blue";
        private const string DEFAULT_PALETTE_SPARKLE_ORANGE = @"Sparkle - Orange";
        private const string DEFAULT_PALETTE_SPARKLE_PURPLE = @"Sparkle - Purple";
        private const string DEFAULT_PALETTE_SPARKLE_BLUE_DARK_MODE = @"Sparkle - Blue (Dark Mode)";
        private const string DEFAULT_PALETTE_SPARKLE_ORANGE_DARK_MODE = @"Sparkle - Orange (Dark Mode)";
        private const string DEFAULT_PALETTE_SPARKLE_PURPLE_DARK_MODE = @"Sparkle - Purple (Dark Mode)";
        private const string DEFAULT_PALETTE_SPARKLE_BLUE_LIGHT_MODE = @"Sparkle - Blue (Light Mode)";
        private const string DEFAULT_PALETTE_SPARKLE_ORANGE_LIGHT_MODE = @"Sparkle - Orange (Light Mode)";
        private const string DEFAULT_PALETTE_SPARKLE_PURPLE_LIGHT_MODE = @"Sparkle - Purple (Light Mode)";
        private const string DEFAULT_PALETTE_CUSTOM = @"Custom";

        #endregion

        #region Instance Fields

        private string[] _themeArray;

        #endregion

        #region Identity

        public PaletteModeStrings()
        {
            Reset();
        }

        public override string ToString() => !IsDefault ? "Modified" : string.Empty;

        #endregion

        #region Public

        [Browsable(false)]
        public bool IsDefault =>
            Custom.Equals(DEFAULT_PALETTE_CUSTOM) &&
            Professional.Equals(DEFAULT_PALETTE_SYSTEM) &&
            Professional2003.Equals(DEFAULT_PALETTE_OFFICE_2003) &&
            Office2007Black.Equals(DEFAULT_PALETTE_OFFICE_2007_BLACK) &&
            Office2007BlackDarkMode.Equals(DEFAULT_PALETTE_OFFICE_2007_BLACK_DARK_MODE) &&
            Office2007Blue.Equals(DEFAULT_PALETTE_OFFICE_2007_BLUE) &&
            Office2007BlueDarkMode.Equals(DEFAULT_PALETTE_OFFICE_2007_BLUE_DARK_MODE) &&
            Office2007BlueLightMode.Equals(DEFAULT_PALETTE_OFFICE_2007_BLUE_LIGHT_MODE) &&
            Office2007Silver.Equals(DEFAULT_PALETTE_OFFICE_2007_SILVER) &&
            Office2007SilverDarkMode.Equals(DEFAULT_PALETTE_OFFICE_2007_SILVER_DARK_MODE) &&
            Office2007SilverLightMode.Equals(DEFAULT_PALETTE_OFFICE_2007_SILVER_LIGHT_MODE) &&
            Office2007White.Equals(DEFAULT_PALETTE_OFFICE_2007_WHITE) &&
            Office2007DarkGray.Equals(DEFAULT_PALETTE_OFFICE_2007_DARK_GRAY) &&
            Office2007LightGray.Equals(DEFAULT_PALETTE_OFFICE_2007_LIGHT_GRAY) &&
            Office2010Black.Equals(DEFAULT_PALETTE_OFFICE_2010_BLACK) &&
            Office2010BlackDarkMode.Equals(DEFAULT_PALETTE_OFFICE_2010_BLACK_DARK_MODE) &&
            Office2010Blue.Equals(DEFAULT_PALETTE_OFFICE_2010_BLUE) &&
            Office2010BlueDarkMode.Equals(DEFAULT_PALETTE_OFFICE_2010_BLUE_DARK_MODE) &&
            Office2010BlueLightMode.Equals(DEFAULT_PALETTE_OFFICE_2010_BLUE_LIGHT_MODE) &&
            Office2010Silver.Equals(DEFAULT_PALETTE_OFFICE_2010_SILVER) &&
            Office2010SilverDarkMode.Equals(DEFAULT_PALETTE_OFFICE_2010_SILVER_DARK_MODE) &&
            Office2010SilverLightMode.Equals(DEFAULT_PALETTE_OFFICE_2010_SILVER_LIGHT_MODE) &&
            Office2010White.Equals(DEFAULT_PALETTE_OFFICE_2010_WHITE) &&
            Office2010DarkGray.Equals(DEFAULT_PALETTE_OFFICE_2010_DARK_GRAY) &&
            Office2010LightGray.Equals(DEFAULT_PALETTE_OFFICE_2010_LIGHT_GRAY) &&
            Office2013DarkGray.Equals(DEFAULT_PALETTE_OFFICE_2013_DARK_GRAY) &&
            Office2013LightGray.Equals(DEFAULT_PALETTE_OFFICE_2013_LIGHT_GRAY) &&
            Office2013White.Equals(DEFAULT_PALETTE_OFFICE_2013_WHITE) &&
            Microsoft365Black.Equals(DEFAULT_PALETTE_MICROSOFT_365_BLACK) &&
            Microsoft365BlackDarkMode.Equals(DEFAULT_PALETTE_MICROSOFT_365_BLACK_DARK_MODE) &&
            Microsoft365Blue.Equals(DEFAULT_PALETTE_MICROSOFT_365_BLUE) &&
            Microsoft365BlueDarkMode.Equals(DEFAULT_PALETTE_MICROSOFT_365_BLUE_DARK_MODE) &&
            Microsoft365BlueLightMode.Equals(DEFAULT_PALETTE_MICROSOFT_365_BLUE_LIGHT_MODE) &&
            Microsoft365Silver.Equals(DEFAULT_PALETTE_MICROSOFT_365_SILVER) &&
            Microsoft365SilverDarkMode.Equals(DEFAULT_PALETTE_MICROSOFT_365_SILVER_DARK_MODE) &&
            Microsoft365SilverLightMode.Equals(DEFAULT_PALETTE_MICROSOFT_365_SILVER_LIGHT_MODE) &&
            Microsoft365White.Equals(DEFAULT_PALETTE_MICROSOFT_365_WHITE) &&
            Microsoft365DarkGray.Equals(DEFAULT_PALETTE_MICROSOFT_365_DARK_GRAY) &&
            Microsoft365LightGray.Equals(DEFAULT_PALETTE_MICROSOFT_365_LIGHT_GRAY) &&
            SparkleBlue.Equals(DEFAULT_PALETTE_SPARKLE_BLUE) &&
            SparkleOrange.Equals(DEFAULT_PALETTE_SPARKLE_ORANGE) &&
            SparklePurple.Equals(DEFAULT_PALETTE_SPARKLE_PURPLE) &&
            SparkleBlueDarkMode.Equals(DEFAULT_PALETTE_SPARKLE_BLUE_DARK_MODE) &&
            SparkleOrangeDarkMode.Equals(DEFAULT_PALETTE_SPARKLE_ORANGE_DARK_MODE) &&
            SparklePurpleDarkMode.Equals(DEFAULT_PALETTE_SPARKLE_PURPLE_DARK_MODE) &&
            SparkleBlueLightMode.Equals(DEFAULT_PALETTE_SPARKLE_BLUE_LIGHT_MODE) &&
            SparkleOrangeLightMode.Equals(DEFAULT_PALETTE_SPARKLE_ORANGE_LIGHT_MODE) &&
            SparklePurpleLightMode.Equals(DEFAULT_PALETTE_SPARKLE_PURPLE_LIGHT_MODE);

        public void Reset()
        {
            Custom = DEFAULT_PALETTE_CUSTOM;

            Professional = DEFAULT_PALETTE_SYSTEM;

            Professional2003 = DEFAULT_PALETTE_OFFICE_2003;

            Office2007Black = DEFAULT_PALETTE_OFFICE_2007_BLACK;

            Office2007BlackDarkMode = DEFAULT_PALETTE_OFFICE_2007_BLACK_DARK_MODE;

            Office2007Blue = DEFAULT_PALETTE_OFFICE_2007_BLUE;

            Office2007BlueDarkMode = DEFAULT_PALETTE_OFFICE_2007_BLUE_DARK_MODE;

            Office2007BlueLightMode = DEFAULT_PALETTE_OFFICE_2007_BLUE_LIGHT_MODE;

            Office2007Silver = DEFAULT_PALETTE_OFFICE_2007_SILVER;

            Office2007SilverDarkMode = DEFAULT_PALETTE_OFFICE_2007_SILVER_DARK_MODE;

            Office2007SilverLightMode = DEFAULT_PALETTE_OFFICE_2007_SILVER_LIGHT_MODE;

            Office2007White = DEFAULT_PALETTE_OFFICE_2007_WHITE;

            Office2007DarkGray = DEFAULT_PALETTE_OFFICE_2007_DARK_GRAY;

            Office2007LightGray = DEFAULT_PALETTE_OFFICE_2007_LIGHT_GRAY;

            Office2010Black = DEFAULT_PALETTE_OFFICE_2010_BLACK;

            Office2010BlackDarkMode = DEFAULT_PALETTE_OFFICE_2010_BLACK_DARK_MODE;

            Office2010Blue = DEFAULT_PALETTE_OFFICE_2010_BLUE;

            Office2010BlueDarkMode = DEFAULT_PALETTE_OFFICE_2010_BLUE_DARK_MODE;

            Office2010BlueLightMode = DEFAULT_PALETTE_OFFICE_2010_BLUE_LIGHT_MODE;

            Office2010Silver = DEFAULT_PALETTE_OFFICE_2010_SILVER;

            Office2010SilverDarkMode = DEFAULT_PALETTE_OFFICE_2010_SILVER_DARK_MODE;

            Office2010SilverLightMode = DEFAULT_PALETTE_OFFICE_2010_SILVER_LIGHT_MODE;

            Office2010White = DEFAULT_PALETTE_OFFICE_2010_WHITE;

            Office2010DarkGray = DEFAULT_PALETTE_OFFICE_2010_DARK_GRAY;

            Office2010LightGray = DEFAULT_PALETTE_OFFICE_2010_LIGHT_GRAY;

            Office2013DarkGray = DEFAULT_PALETTE_OFFICE_2013_DARK_GRAY;

            Office2013LightGray = DEFAULT_PALETTE_OFFICE_2013_LIGHT_GRAY;

            Office2013White = DEFAULT_PALETTE_OFFICE_2013_WHITE;

            Microsoft365Black = DEFAULT_PALETTE_MICROSOFT_365_BLACK;

            Microsoft365BlackDarkMode = DEFAULT_PALETTE_MICROSOFT_365_BLACK_DARK_MODE;

            Microsoft365Blue = DEFAULT_PALETTE_MICROSOFT_365_BLUE;

            Microsoft365BlueDarkMode = DEFAULT_PALETTE_MICROSOFT_365_BLUE_DARK_MODE;

            Microsoft365BlueLightMode = DEFAULT_PALETTE_MICROSOFT_365_BLUE_LIGHT_MODE;

            Microsoft365Silver = DEFAULT_PALETTE_MICROSOFT_365_SILVER;

            Microsoft365SilverDarkMode = DEFAULT_PALETTE_MICROSOFT_365_SILVER_DARK_MODE;

            Microsoft365SilverLightMode = DEFAULT_PALETTE_MICROSOFT_365_SILVER_LIGHT_MODE;

            Microsoft365White = DEFAULT_PALETTE_MICROSOFT_365_WHITE;

            Microsoft365DarkGray = DEFAULT_PALETTE_MICROSOFT_365_DARK_GRAY;

            Microsoft365LightGray = DEFAULT_PALETTE_MICROSOFT_365_LIGHT_GRAY;

            SparkleBlue = DEFAULT_PALETTE_SPARKLE_BLUE;

            SparkleOrange = DEFAULT_PALETTE_SPARKLE_ORANGE;

            SparklePurple = DEFAULT_PALETTE_SPARKLE_PURPLE;

            SparkleBlueDarkMode = DEFAULT_PALETTE_SPARKLE_BLUE_DARK_MODE;

            SparkleOrangeDarkMode = DEFAULT_PALETTE_SPARKLE_ORANGE_DARK_MODE;

            SparklePurpleDarkMode = DEFAULT_PALETTE_SPARKLE_PURPLE_DARK_MODE;

            SparkleBlueLightMode = DEFAULT_PALETTE_SPARKLE_BLUE_LIGHT_MODE;

            SparkleOrangeLightMode = DEFAULT_PALETTE_SPARKLE_ORANGE_LIGHT_MODE;

            SparklePurpleLightMode = DEFAULT_PALETTE_SPARKLE_PURPLE_LIGHT_MODE;
        }

        /// <summary>Gets or sets the custom palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The custom palette name.")]
        [DefaultValue(DEFAULT_PALETTE_CUSTOM)]
        [RefreshProperties(RefreshProperties.All)]
        public string Custom { get; set; }

        /// <summary>Gets or sets the professional palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The professional palette name.")]
        [DefaultValue(DEFAULT_PALETTE_SYSTEM)]
        [RefreshProperties(RefreshProperties.All)]
        public string Professional { get; set; }

        /// <summary>Gets or sets the Office 2003 palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2003 palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2003)]
        [RefreshProperties(RefreshProperties.All)]
        public string Professional2003 { get; set; }

        /// <summary>Gets or sets the Office 2007 Black palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2007 Black palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2007_BLACK)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2007Black { get; set; }

        /// <summary>Gets or sets the Office 2007 Blue palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2007 Blue palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2007_BLUE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2007Blue { get; set; }

        /// <summary>Gets or sets the Office 2007 Silver palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2007 Silver palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2007_SILVER)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2007Silver { get; set; }

        /// <summary>Gets or sets the Office 2007 White palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2007 White palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2007_WHITE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2007White { get; set; }

        /// <summary>Gets or sets the Office 2007 Black (Dark Mode) palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2007 Black (Dark Mode) palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2007_BLACK_DARK_MODE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2007BlackDarkMode { get; set; }

        /// <summary>Gets or sets the Office 2007 Blue (Dark Mode) palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2007 Blue (Dark Mode) palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2007_BLUE_DARK_MODE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2007BlueDarkMode { get; set; }

        /// <summary>Gets or sets the Office 2007 Silver (Dark Mode) palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2007 Silver (Dark Mode) palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2007_SILVER_DARK_MODE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2007SilverDarkMode { get; set; }

        /// <summary>Gets or sets the Office 2007 Blue (Light Mode) palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2007 Blue (Light Mode) palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2007_BLUE_LIGHT_MODE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2007BlueLightMode { get; set; }

        /// <summary>Gets or sets the Office 2007 Silver (Light Mode) palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2007 Silver (Light Mode) palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2007_SILVER_LIGHT_MODE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2007SilverLightMode { get; set; }

        /// <summary>Gets or sets the Office 2007 Dark Gray palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2007 Dark Gray palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2007_DARK_GRAY)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2007DarkGray { get; set; }

        /// <summary>Gets or sets the Office 2007 Light Gray palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2007 Light Gray palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2007_LIGHT_GRAY)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2007LightGray { get; set; }

        /// <summary>Gets or sets the Office 2010 Black palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2010 Black palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2010_BLACK)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2010Black { get; set; }

        /// <summary>Gets or sets the Office 2010 Blue palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2010 Blue palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2010_BLUE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2010Blue { get; set; }

        /// <summary>Gets or sets the Office 2010 Silver palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2010 Silver palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2010_SILVER)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2010Silver { get; set; }

        /// <summary>Gets or sets the Office 2010 White palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2010 White palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2010_WHITE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2010White { get; set; }

        /// <summary>Gets or sets the Office 2010 Black (Dark Mode) palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2010 Black (Dark Mode) palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2010_BLACK_DARK_MODE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2010BlackDarkMode { get; set; }

        /// <summary>Gets or sets the Office 2010 Blue (Dark Mode) palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2010 Blue (Dark Mode) palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2010_BLUE_DARK_MODE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2010BlueDarkMode { get; set; }

        /// <summary>Gets or sets the Office 2010 Silver (Dark Mode) palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2010 Silver (Dark Mode) palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2010_SILVER_DARK_MODE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2010SilverDarkMode { get; set; }

        /// <summary>Gets or sets the Office 2010 Blue (Light Mode) palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2010 Blue (Light Mode) palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2010_BLUE_LIGHT_MODE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2010BlueLightMode { get; set; }

        /// <summary>Gets or sets the Office 2010 Silver (Light Mode) palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2010 Silver (Light Mode) palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2010_SILVER_LIGHT_MODE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2010SilverLightMode { get; set; }

        /// <summary>Gets or sets the Office 2010 Dark Gray palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2010 Dark Gray palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2010_DARK_GRAY)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2010DarkGray { get; set; }

        /// <summary>Gets or sets the Office 2010 Light Gray palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2010 Light Gray palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2010_LIGHT_GRAY)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2010LightGray { get; set; }

        /// <summary>Gets or sets the Office 2013 White palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2013 White palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2013_WHITE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2013White { get; set; }

        /// <summary>Gets or sets the Office 2013 Dark Gray palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2013 Dark Gray palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2013_DARK_GRAY)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2013DarkGray { get; set; }

        /// <summary>Gets or sets the Office 2013 Light Gray palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Office 2013 Light Gray palette name.")]
        [DefaultValue(DEFAULT_PALETTE_OFFICE_2013_LIGHT_GRAY)]
        [RefreshProperties(RefreshProperties.All)]
        public string Office2013LightGray { get; set; }

        /// <summary>Gets or sets the Microsoft 365 Black palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Microsoft 365 Black palette name.")]
        [DefaultValue(DEFAULT_PALETTE_MICROSOFT_365_BLACK)]
        [RefreshProperties(RefreshProperties.All)]
        public string Microsoft365Black { get; set; }

        /// <summary>Gets or sets the Microsoft 365 Blue palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Microsoft 365 Blue palette name.")]
        [DefaultValue(DEFAULT_PALETTE_MICROSOFT_365_BLUE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Microsoft365Blue { get; set; }

        /// <summary>Gets or sets the Microsoft 365 Silver palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Microsoft 365 Silver palette name.")]
        [DefaultValue(DEFAULT_PALETTE_MICROSOFT_365_SILVER)]
        [RefreshProperties(RefreshProperties.All)]
        public string Microsoft365Silver { get; set; }

        /// <summary>Gets or sets the Microsoft 365 White palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Microsoft 365 White palette name.")]
        [DefaultValue(DEFAULT_PALETTE_MICROSOFT_365_WHITE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Microsoft365White { get; set; }

        /// <summary>Gets or sets the Microsoft 365 Black (Dark Mode) palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Microsoft 365 Black (Dark Mode) palette name.")]
        [DefaultValue(DEFAULT_PALETTE_MICROSOFT_365_BLACK_DARK_MODE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Microsoft365BlackDarkMode { get; set; }

        /// <summary>Gets or sets the Microsoft 365 Blue (Dark Mode) palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Microsoft 365 Blue (Dark Mode) palette name.")]
        [DefaultValue(DEFAULT_PALETTE_MICROSOFT_365_BLUE_DARK_MODE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Microsoft365BlueDarkMode { get; set; }

        /// <summary>Gets or sets the Microsoft 365 Silver (Dark Mode) palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Microsoft 365 Silver (Dark Mode) palette name.")]
        [DefaultValue(DEFAULT_PALETTE_MICROSOFT_365_SILVER_DARK_MODE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Microsoft365SilverDarkMode { get; set; }

        /// <summary>Gets or sets the Microsoft 365 Blue (Light Mode) palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Microsoft 365 Blue (Light Mode) palette name.")]
        [DefaultValue(DEFAULT_PALETTE_MICROSOFT_365_BLUE_LIGHT_MODE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Microsoft365BlueLightMode { get; set; }

        /// <summary>Gets or sets the Microsoft 365 Silver (Light Mode) palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Microsoft 365 Silver (Light Mode) palette name.")]
        [DefaultValue(DEFAULT_PALETTE_MICROSOFT_365_SILVER_LIGHT_MODE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Microsoft365SilverLightMode { get; set; }

        /// <summary>Gets or sets the Microsoft 365 Dark Gray palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Microsoft 365 Dark Gray palette name.")]
        [DefaultValue(DEFAULT_PALETTE_MICROSOFT_365_DARK_GRAY)]
        [RefreshProperties(RefreshProperties.All)]
        public string Microsoft365DarkGray { get; set; }

        /// <summary>Gets or sets the Microsoft 365 Light Gray palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Microsoft 365 Light Gray palette name.")]
        [DefaultValue(DEFAULT_PALETTE_MICROSOFT_365_LIGHT_GRAY)]
        [RefreshProperties(RefreshProperties.All)]
        public string Microsoft365LightGray { get; set; }

        /// <summary>Gets or sets the Sparkle Blue palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Sparkle Blue palette name.")]
        [DefaultValue(DEFAULT_PALETTE_SPARKLE_BLUE)]
        [RefreshProperties(RefreshProperties.All)]
        public string SparkleBlue { get; set; }

        /// <summary>Gets or sets the Sparkle Orange palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Sparkle Orange palette name.")]
        [DefaultValue(DEFAULT_PALETTE_SPARKLE_ORANGE)]
        [RefreshProperties(RefreshProperties.All)]
        public string SparkleOrange { get; set; }

        /// <summary>Gets or sets the Sparkle Purple palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Sparkle Purple palette name.")]
        [DefaultValue(DEFAULT_PALETTE_SPARKLE_PURPLE)]
        [RefreshProperties(RefreshProperties.All)]
        public string SparklePurple { get; set; }

        /// <summary>Gets or sets the Sparkle Blue (Dark Mode) palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Sparkle Blue (Dark Mode) palette name.")]
        [DefaultValue(DEFAULT_PALETTE_SPARKLE_BLUE_DARK_MODE)]
        [RefreshProperties(RefreshProperties.All)]
        public string SparkleBlueDarkMode { get; set; }

        /// <summary>Gets or sets the Sparkle Orange (Dark Mode) palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Sparkle Orange (Dark Mode) palette name.")]
        [DefaultValue(DEFAULT_PALETTE_SPARKLE_ORANGE_DARK_MODE)]
        [RefreshProperties(RefreshProperties.All)]
        public string SparkleOrangeDarkMode { get; set; }

        /// <summary>Gets or sets the Sparkle Purple (Dark Mode) palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Sparkle Purple (Dark Mode) palette name.")]
        [DefaultValue(DEFAULT_PALETTE_SPARKLE_PURPLE_DARK_MODE)]
        [RefreshProperties(RefreshProperties.All)]
        public string SparklePurpleDarkMode { get; set; }

        /// <summary>Gets or sets the Sparkle Blue (Light Mode) palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Sparkle Blue (Light Mode) palette name.")]
        [DefaultValue(DEFAULT_PALETTE_SPARKLE_BLUE_LIGHT_MODE)]
        [RefreshProperties(RefreshProperties.All)]
        public string SparkleBlueLightMode { get; set; }

        /// <summary>Gets or sets the Sparkle Orange (Light Mode) palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Sparkle Orange (Light Mode) palette name.")]
        [DefaultValue(DEFAULT_PALETTE_SPARKLE_ORANGE_LIGHT_MODE)]
        [RefreshProperties(RefreshProperties.All)]
        public string SparkleOrangeLightMode { get; set; }

        /// <summary>Gets or sets the Sparkle Purple (Light Mode) palette name string.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"The Sparkle Purple (Light Mode) palette name.")]
        [DefaultValue(DEFAULT_PALETTE_SPARKLE_PURPLE_LIGHT_MODE)]
        [RefreshProperties(RefreshProperties.All)]
        public string SparklePurpleLightMode { get; set; }

        public static string[] ThemeArray
        {
            get
            {
                var strings = new PaletteModeStrings();

                return strings._themeArray;
            }
        }

        #endregion

        #region Implementation

        private void AddToThemeArray(string themeName)
        {
            List<string> temporaryList = new List<string>
            {
                themeName
            };

            _themeArray = temporaryList.ToArray();
        }

        #endregion
    }
}