#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBeInternal

namespace Krypton.Toolkit
{
    /// <summary>
    /// Expose a global set of strings used within Krypton and that are localizable.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class GlobalStrings : GlobalId
    {
        #region Static Fields
        private const string DEFAULT_OK = "O&K";
        private const string DEFAULT_CANCEL = "Cance&l";
        private const string DEFAULT_YES = "&Yes";
        private const string DEFAULT_NO = "N&o";
        private const string DEFAULT_ABORT = "A&bort";
        private const string DEFAULT_RETRY = "Ret&ry";
        private const string DEFAULT_IGNORE = "I&gnore";
        private const string DEFAULT_CLOSE = "Cl&ose";
        private const string DEFAULT_TODAY = "T&oday";
        private const string DEFAULT_HELP = "H&elp";

        // NET 6 & newer
        private const string DEFAULT_CONTINUE = "Co&ntinue";
        private const string DEFAULT_TRY_AGAIN = "&Try Again";

        // Custom
        private const string DEFAULT_APPLY = "A&pply";
        private const string DEFAULT_BACK = "Bac&k";
        private const string DEFAULT_COLLAPSE = "C&ollapse";
        private const string DEFAULT_EXPAND = "Ex&pand";
        private const string DEFAULT_EXIT = "E&xit";
        private const string DEFAULT_FINISH = "&Finish";
        private const string DEFAULT_NEXT = "&Next";
        private const string DEFAULT_PREVIOUS = "Pre&vious";

        // Note: The following may not be needed...
        /*private const string DEFAULT_MORE_DETAILS = "M&ore Details...";
        private const string DEFAULT_LESS_DETAILS = "Les&s Details...";*/

        #endregion

        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the Clipping class.
        /// </summary>
        public GlobalStrings()
        {
            Reset();
        }

        /// <summary>
        /// Returns a string that represents the current defaulted state.
        /// </summary>
        /// <returns>A string that represents the current defaulted state.</returns>
        public override string ToString() => !IsDefault ? "Modified" : string.Empty;

        #endregion

        #region Public

        /// <summary>
        /// Gets a value indicating if all the strings are default values.
        /// </summary>
        /// <returns>True if all values are defaulted; otherwise false.</returns>
        [Browsable(false)]
        public bool IsDefault => OK.Equals(DEFAULT_OK) &&
                                 Cancel.Equals(DEFAULT_CANCEL) &&
                                 Yes.Equals(DEFAULT_YES) &&
                                 No.Equals(DEFAULT_NO) &&
                                 Abort.Equals(DEFAULT_ABORT) &&
                                 Retry.Equals(DEFAULT_RETRY) &&
                                 Ignore.Equals(DEFAULT_IGNORE) &&
                                 Close.Equals(DEFAULT_CLOSE) &&
                                 Today.Equals(DEFAULT_TODAY) &&
                                 Help.Equals(DEFAULT_HELP) &&
                                 Continue.Equals(DEFAULT_CONTINUE) &&
                                 TryAgain.Equals(DEFAULT_TRY_AGAIN) &&
                                 Apply.Equals(DEFAULT_APPLY) &&
                                 Collapse.Equals(DEFAULT_COLLAPSE) &&
                                 Expand.Equals(DEFAULT_EXPAND) &&
                                 Apply.Equals(DEFAULT_APPLY) &&
                                 Back.Equals(DEFAULT_BACK) &&
                                 Exit.Equals(DEFAULT_EXIT) &&
                                 Finish.Equals(DEFAULT_FINISH) &&
                                 Next.Equals(DEFAULT_NEXT) &&
                                 Previous.Equals(DEFAULT_PREVIOUS);
                                 // Note: The following may not be needed...
                                 /*MoreDetails.Equals(DEFAULT_MORE_DETAILS) &&
                                 LessDetails.Equals(DEFAULT_LESS_DETAILS);*/

        /// <summary>
        /// Reset all strings to default values.
        /// </summary>
        public void Reset()
        {
            OK = DEFAULT_OK;
            Cancel = DEFAULT_CANCEL;
            Yes = DEFAULT_YES;
            No = DEFAULT_NO;
            Abort = DEFAULT_ABORT;
            Retry = DEFAULT_RETRY;
            Ignore = DEFAULT_IGNORE;
            Close = DEFAULT_CLOSE;
            Today = DEFAULT_TODAY;
            Help = DEFAULT_HELP;

            // NET 6 & newer
            Continue = DEFAULT_CONTINUE;
            TryAgain = DEFAULT_TRY_AGAIN;

            // Custom
            Apply = DEFAULT_APPLY;
            Collapse = DEFAULT_COLLAPSE;
            Expand = DEFAULT_EXPAND;
            Apply = DEFAULT_APPLY;
            Back = DEFAULT_BACK;
            Exit = DEFAULT_EXIT;
            Finish = DEFAULT_FINISH;
            Next = DEFAULT_NEXT;
            Previous = DEFAULT_PREVIOUS;

            // Note: The following may not be needed...
            /*MoreDetails = DEFAULT_MORE_DETAILS;
            LessDetails = DEFAULT_LESS_DETAILS;*/
        }

        /// <summary>
        /// Gets and sets the OK string used in message box buttons.
        /// </summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"OK string used for message box buttons.")]
        [DefaultValue(DEFAULT_OK)]
        [RefreshProperties(RefreshProperties.All)]
        public string OK { get; set; }

        /// <summary>
        /// Gets and sets the Cancel string used in message box buttons.
        /// </summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Cancel string used for message box buttons.")]
        [DefaultValue(DEFAULT_CANCEL)]
        [RefreshProperties(RefreshProperties.All)]
        public string Cancel { get; set; }

        /// <summary>
        /// Gets and sets the Yes string used in message box buttons.
        /// </summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Yes string used for message box buttons.")]
        [DefaultValue(DEFAULT_YES)]
        [RefreshProperties(RefreshProperties.All)]
        public string Yes { get; set; }

        /// <summary>
        /// Gets and sets the No string used in message box buttons.
        /// </summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"No string used for message box buttons.")]
        [DefaultValue(DEFAULT_NO)]
        [RefreshProperties(RefreshProperties.All)]
        public string No { get; set; }

        /// <summary>
        /// Gets and sets the Abort string used in message box buttons.
        /// </summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Abort string used for message box buttons.")]
        [DefaultValue(DEFAULT_ABORT)]
        [RefreshProperties(RefreshProperties.All)]
        public string Abort { get; set; }

        /// <summary>
        /// Gets and sets the Retry string used in message box buttons.
        /// </summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Retry string used for message box buttons.")]
        [DefaultValue(DEFAULT_RETRY)]
        [RefreshProperties(RefreshProperties.All)]
        public string Retry { get; set; }

        /// <summary>
        /// Gets and sets the Ignore string used in message box buttons.
        /// </summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Ignore string used for message box buttons.")]
        [DefaultValue(DEFAULT_IGNORE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Ignore { get; set; }

        /// <summary>
        /// Gets and sets the Close string used in message box buttons.
        /// </summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Close string used for message box buttons.")]
        [DefaultValue(DEFAULT_CLOSE)]

        [RefreshProperties(RefreshProperties.All)]
        public string Close { get; set; }

        /// <summary>
        /// Gets and sets the Close string used in calendars.
        /// </summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Today string used for calendars.")]
        [DefaultValue(DEFAULT_TODAY)]
        [RefreshProperties(RefreshProperties.All)]
        public string Today { get; set; }

        /// <summary>
        /// Gets and sets the Help string used in message box buttons.
        /// </summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Help string used for Message Box Buttons.")]
        [DefaultValue(DEFAULT_HELP)]
        [RefreshProperties(RefreshProperties.All)]
        public string Help { get; set; }

        /// <summary>
        /// Gets and sets the Continue string used in message box buttons.
        /// </summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Continue string used for Message Box Buttons.")]
        [DefaultValue(DEFAULT_CONTINUE)]
        public string Continue { get; set; }

        /// <summary>
        /// Gets and sets the Try Again string used in message box buttons.
        /// </summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Try Again string used for Message Box Buttons.")]
        [DefaultValue(DEFAULT_TRY_AGAIN)]
        public string TryAgain { get; set; }

        /// <summary>Gets or sets the collapse string used in expandable footers.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Collapse string used in expandable footers.")]
        [DefaultValue(DEFAULT_COLLAPSE)]
        public string Collapse { get; set; }

        /// <summary>Gets or sets the expand string used in expandable footers.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Expand string used in expandable footers.")]
        [DefaultValue(DEFAULT_EXPAND)]
        public string Expand { get; set; }

        /// <summary>
        /// Gets and sets the Apply string used in property dialogs.
        /// </summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Apply string used for property dialogs.")]
        [DefaultValue(DEFAULT_APPLY)]
        public string Apply { get; set; }

        /// <summary>
        /// Gets and sets the Back string used in custom situations.
        /// </summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Back string used for custom situations.")]
        [DefaultValue(DEFAULT_BACK)]
        public string Back { get; set; }

        /// <summary>
        /// Gets and sets the Exit string used in custom situations.
        /// </summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Exit string used for custom situations.")]
        [DefaultValue(DEFAULT_EXIT)]
        public string Exit { get; set; }

        /// <summary>
        /// Gets and sets the Finish string used in custom situations.
        /// </summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Finish string used for custom situations.")]
        [DefaultValue(DEFAULT_FINISH)]
        public string Finish { get; set; }

        /// <summary>
        /// Gets and sets the Next string used in custom situations.
        /// </summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Next string used for custom situations.")]
        [DefaultValue(DEFAULT_NEXT)]
        public string Next { get; set; }

        /// <summary>
        /// Gets and sets the Previous string used in custom situations.
        /// </summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Previous string used for custom situations.")]
        [DefaultValue(DEFAULT_PREVIOUS)]
        public string Previous { get; set; }    

        // Note: The following may not be needed...

        /*/// <summary>Gets or sets the more details string used in expandable footers.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"More details string used in expandable footers.")]
        public string MoreDetails { get; set; }

        /// <summary>Gets or sets the less details string used in expandable footers.</summary>
        [Localizable(true)]
        [Category(@"Visuals")]
        [Description(@"Less details string used in expandable footers.")]
        public string LessDetails { get; set; }*/
        #endregion
    }
}