#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class GeneralRibbonStrings : GlobalId
    {
        #region Static Values

        private const string DEFAULT_APPLICATION_BUTTON_TEXT = @"File";
        private const string DEFAULT_APPLICATION_BUTTON_KEY_TIP = @"F";
        private const string DEFAULT_CUSTOMIZE_QUICK_ACCESS_TOOLBAR = @"Customize Quick Access Toolbar";
        private const string DEFAULT_MINIMIZE = @"Mi&nimize the Ribbon";
        private const string DEFAULT_MORE_COLORS = @"&More Colors...";
        private const string DEFAULT_NO_COLOR = @"&No Color";
        private const string DEFAULT_RECENT_DOCUMENTS = @"Recent Documents";
        private const string DEFAULT_RECENT_COLORS = @"Recent Colors";
        private const string DEFAULT_SHOW_QAT_ABOVE_RIBBON = @"&Show Quick Access Toolbar Above the Ribbon";
        private const string DEFAULT_SHOW_QAT_BELOW_RIBBON = @"&Show Quick Access Toolbar Below the Ribbon";
        private const string DEFAULT_SHOW_ABOVE_RIBBON = @"&Show Above the Ribbon";
        private const string DEFAULT_SHOW_BELOW_RIBBON = @"&Show Below the Ribbon";
        private const string DEFAULT_STANDARD_COLORS = @"Standard Colors";
        private const string DEFAULT_THEME_COLORS = @"Theme Colors";

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="GeneralRibbonStrings" /> class.</summary>
        public GeneralRibbonStrings()
        {
            Reset();
        }

        #endregion

        #region IsDefault

        [Browsable(false)] 
        public bool IsDefault => false;

        #endregion

        #region Public



        #endregion

        #region Implementation

        public void Reset()
        {

        }

        #endregion
    }
}