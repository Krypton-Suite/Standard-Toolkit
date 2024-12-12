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
        private const string DEFAULT_EXCEPTION_DIALOG_MORE_DETAILS = @"Please select another node to view more details.";
        private const string DEFAULT_EXCEPTION_DIALOG_TYPE = @"Type";
        private const string DEFAULT_EXCEPTION_DIALOG_INNER_EXCEPTION = @"Inner Exception";
        private const string DEFAULT_EXCEPTION_DIALOG_MESSAGE = @"Message";
        private const string DEFAULT_EXCEPTION_DIALOG_STACK_TRACE = @"Stack Trace";
        private const string DEFAULT_EXCEPTION_DIALOG_NONE = @"None";
        private const string DEFAULT_EXCEPTION_DIALOG_DATA = @"Data";

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
                                 ExceptionOutlineHeader.Equals(DEFAULT_EXCEPTION_DIALOG_EXCEPTION_OUTLINE_HEADER) &&
                                 MoreDetails.Equals(DEFAULT_EXCEPTION_DIALOG_MORE_DETAILS) &&
                                 Type.Equals(DEFAULT_EXCEPTION_DIALOG_TYPE) &&
                                 InnerException.Equals(DEFAULT_EXCEPTION_DIALOG_INNER_EXCEPTION) &&
                                 Message.Equals(DEFAULT_EXCEPTION_DIALOG_MESSAGE) &&
                                 StackTrace.Equals(DEFAULT_EXCEPTION_DIALOG_STACK_TRACE) &&
                                 None.Equals(DEFAULT_EXCEPTION_DIALOG_NONE) &&
                                 Data.Equals(DEFAULT_EXCEPTION_DIALOG_DATA);

        #endregion

        #region Public

        /// <summary>Gets or sets the window title for the exception dialog.</summary>
        /// <value>The window title.</value>
        [Localizable(true)]
        [Category(@"Values")]
        [Description(@"The window title for the exception dialog.")]
        [DefaultValue(DEFAULT_EXCEPTION_DIALOG_WINDOW_TITLE)]
        [RefreshProperties(RefreshProperties.All)]
        public string WindowTitle { get; set; }

        /// <summary>Gets or sets the exception details header for the exception dialog.</summary>
        /// <value>The exception details header.</value>
        [Localizable(true)]
        [Category(@"Values")]
        [Description(@"The exception details header for the exception dialog.")]
        [DefaultValue(DEFAULT_EXCEPTION_DIALOG_EXCEPTION_DETAILS_HEADER)]
        [RefreshProperties(RefreshProperties.All)]
        public string ExceptionDetailsHeader { get; set; }

        /// <summary>Gets or sets the exception outline header for the exception dialog.</summary>
        /// <value>The exception outline header.</value>
        [Localizable(true)]
        [Category(@"Values")]
        [Description(@"The exception outline header for the exception dialog.")]
        [DefaultValue(DEFAULT_EXCEPTION_DIALOG_EXCEPTION_OUTLINE_HEADER)]
        [RefreshProperties(RefreshProperties.All)]
        public string ExceptionOutlineHeader { get; set; }

        /// <summary>Gets or sets the more details when specific <see cref="TreeNode"/> are clicked.</summary>
        /// <value>The more details.</value>
        [Localizable(true)]
        [Category(@"Values")]
        [Description(@"The more details when specific TreeNodes are clicked.")]
        [DefaultValue(DEFAULT_EXCEPTION_DIALOG_MORE_DETAILS)]
        [RefreshProperties(RefreshProperties.All)]
        public string MoreDetails { get; set; }

        [Localizable(true)]
        [Category(@"Values")]
        [Description(@"The exception outline header for the exception dialog.")]
        [DefaultValue(DEFAULT_EXCEPTION_DIALOG_TYPE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Type { get; set; }

        [Localizable(true)]
        [Category(@"Values")]
        [Description(@"The exception outline header for the exception dialog.")]
        [DefaultValue(DEFAULT_EXCEPTION_DIALOG_INNER_EXCEPTION)]
        [RefreshProperties(RefreshProperties.All)]
        public string InnerException { get; set; }

        [Localizable(true)]
        [Category(@"Values")]
        [Description(@"The exception outline header for the exception dialog.")]
        [DefaultValue(DEFAULT_EXCEPTION_DIALOG_MESSAGE)]
        [RefreshProperties(RefreshProperties.All)]
        public string Message { get; set; }

        [Localizable(true)]
        [Category(@"Values")]
        [Description(@"The exception outline header for the exception dialog.")]
        [DefaultValue(DEFAULT_EXCEPTION_DIALOG_STACK_TRACE)]
        [RefreshProperties(RefreshProperties.All)]
        public string StackTrace { get; set; }

        [Localizable(true)]
        [Category(@"Values")]
        [Description(@"The exception outline header for the exception dialog.")]
        [DefaultValue(DEFAULT_EXCEPTION_DIALOG_NONE)]
        [RefreshProperties(RefreshProperties.All)]
        public string None { get; set; }

        [Localizable(true)]
        [Category(@"Values")]
        [Description(@"The exception outline header for the exception dialog.")]
        [DefaultValue(DEFAULT_EXCEPTION_DIALOG_DATA)]
        [RefreshProperties(RefreshProperties.All)]
        public string Data { get; set; }

        #endregion

        #region Implementation

        /// <summary>Resets the strings.</summary>
        public void Reset()
        {
            ExceptionDetailsHeader = DEFAULT_EXCEPTION_DIALOG_EXCEPTION_DETAILS_HEADER;

            ExceptionOutlineHeader = DEFAULT_EXCEPTION_DIALOG_EXCEPTION_OUTLINE_HEADER;

            WindowTitle = DEFAULT_EXCEPTION_DIALOG_WINDOW_TITLE;

            Message = DEFAULT_EXCEPTION_DIALOG_MESSAGE;

            MoreDetails = DEFAULT_EXCEPTION_DIALOG_MORE_DETAILS;

            Type = DEFAULT_EXCEPTION_DIALOG_TYPE;

            InnerException = DEFAULT_EXCEPTION_DIALOG_INNER_EXCEPTION;

            StackTrace = DEFAULT_EXCEPTION_DIALOG_STACK_TRACE;

            None = DEFAULT_EXCEPTION_DIALOG_NONE;

            Data = DEFAULT_EXCEPTION_DIALOG_DATA;
        }

        #endregion
    }
}
