#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>Exposes a general set of strings that are used within the Krypton exception dialog, and are localisable.</summary>
    /// <seealso cref="GlobalId" />
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class KryptonExceptionDialogStrings : GlobalId
    {
        #region Static Values

        private const string DEFAULT_EXCEPTION_DIALOG_WINDOW_TITLE = @"Exception Caught";
        private const string DEFAULT_EXCEPTION_DIALOG_EXCEPTION_OUTLINE_HEADER = @"Exception Outline";
        private const string DEFAULT_EXCEPTION_DIALOG_EXCEPTION_DETAILS_HEADER = @"Exception Details";

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonExceptionDialogStrings" /> class.</summary>
        public KryptonExceptionDialogStrings()
        {
            Reset();
        }

        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => !IsDefault ? "Modified" : string.Empty;

        #endregion

        #region IsDefault

        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDefault => WindowTitle.Equals(DEFAULT_EXCEPTION_DIALOG_WINDOW_TITLE) &&
                                 ExceptionDetailsHeader.Equals(DEFAULT_EXCEPTION_DIALOG_EXCEPTION_DETAILS_HEADER) &&
                                 ExceptionOutlineHeader.Equals(DEFAULT_EXCEPTION_DIALOG_EXCEPTION_OUTLINE_HEADER);

        #endregion

        #region Public

        /// <summary>
        /// Gets and sets the collapse text on the expand button.
        /// </summary>
        [Localizable(true)]
        [Category(@"Values")]
        [Description(@"Collapse text on the expand button.")]
        [DefaultValue(DEFAULT_EXCEPTION_DIALOG_WINDOW_TITLE)]
        [RefreshProperties(RefreshProperties.All)]
        public string WindowTitle { get; set; }

        /// <summary>
        /// Gets and sets the collapse text on the expand button.
        /// </summary>
        [Localizable(true)]
        [Category(@"Values")]
        [Description(@"Collapse text on the expand button.")]
        [DefaultValue(DEFAULT_EXCEPTION_DIALOG_EXCEPTION_DETAILS_HEADER)]
        [RefreshProperties(RefreshProperties.All)]
        public string ExceptionDetailsHeader { get; set; }

        /// <summary>
        /// Gets and sets the collapse text on the expand button.
        /// </summary>
        [Localizable(true)]
        [Category(@"Values")]
        [Description(@"Collapse text on the expand button.")]
        [DefaultValue(DEFAULT_EXCEPTION_DIALOG_EXCEPTION_OUTLINE_HEADER)]
        [RefreshProperties(RefreshProperties.All)]
        public string ExceptionOutlineHeader { get; set; }

        #endregion

        #region Implementation

        public void Reset()
        {
            ExceptionDetailsHeader = DEFAULT_EXCEPTION_DIALOG_EXCEPTION_DETAILS_HEADER;

            ExceptionOutlineHeader = DEFAULT_EXCEPTION_DIALOG_EXCEPTION_OUTLINE_HEADER;

            WindowTitle = DEFAULT_EXCEPTION_DIALOG_WINDOW_TITLE;
        }

        #endregion
    }
}
