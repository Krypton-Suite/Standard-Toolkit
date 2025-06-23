#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

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
    private const string DEFAULT_EXCEPTION_DIALOG_LINE = @"Line";
    private const string DEFAULT_EXCEPTION_DIALOG_SEARCH = @"Search...";
    private const string DEFAULT_EXCEPTION_DIALOG_NO_RESULTS_FOUND = @"No results found";
    private const string DEFAULT_EXCEPTION_DIALOG_RESULT = @"result";
    private const string DEFAULT_EXCEPTION_DIALOG_RESULTS_APPENDAGE = @"s";
    private const string DEFAULT_EXCEPTION_DIALOG_RESULTS_FOUND_APPENDAGE = @"found";
    private const string DEFAULT_EXCEPTION_DIALOG_NO_MATCHES_FOUND = @"No matches found.";
    private const string DEFAULT_EXCEPTION_DIALOG_TYPE_TO_SEARCH = @"Type to search...";

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
                             Data.Equals(DEFAULT_EXCEPTION_DIALOG_DATA) &&
                             Line.Equals(DEFAULT_EXCEPTION_DIALOG_LINE) &&
                             SearchBoxCueText.Equals(DEFAULT_EXCEPTION_DIALOG_SEARCH) &&
                             NoResultsFound.Equals(DEFAULT_EXCEPTION_DIALOG_NO_RESULTS_FOUND) &&
                             Result.Equals(DEFAULT_EXCEPTION_DIALOG_RESULT) &&
                             ResultsAppendage.Equals(DEFAULT_EXCEPTION_DIALOG_RESULTS_APPENDAGE) &&
                             ResultsFoundAppendage.Equals(DEFAULT_EXCEPTION_DIALOG_RESULTS_FOUND_APPENDAGE) &&
                             NoMatchesFound.Equals(DEFAULT_EXCEPTION_DIALOG_NO_MATCHES_FOUND) &&
                             TypeToSearch.Equals(DEFAULT_EXCEPTION_DIALOG_TYPE_TO_SEARCH);

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

    /// <summary>Gets or sets the type text.</summary>
    /// <value>The type text.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The exception type text for the exception dialog.")]
    [DefaultValue(DEFAULT_EXCEPTION_DIALOG_TYPE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Type { get; set; }

    /// <summary>Gets or sets the inner exception text.</summary>
    /// <value>The inner exception text.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The exception inner exception text for the exception dialog.")]
    [DefaultValue(DEFAULT_EXCEPTION_DIALOG_INNER_EXCEPTION)]
    [RefreshProperties(RefreshProperties.All)]
    public string InnerException { get; set; }

    /// <summary>Gets or sets the message text.</summary>
    /// <value>The message text.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The exception outline header for the exception dialog.")]
    [DefaultValue(DEFAULT_EXCEPTION_DIALOG_MESSAGE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Message { get; set; }

    /// <summary>Gets or sets the stack trace node text.</summary>
    /// <value>The stack trace node text.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The stack trace node text for the exception dialog.")]
    [DefaultValue(DEFAULT_EXCEPTION_DIALOG_STACK_TRACE)]
    [RefreshProperties(RefreshProperties.All)]
    public string StackTrace { get; set; }

    /// <summary>Gets or sets the 'None' text.</summary>
    /// <value>The 'None' text.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The 'None' text for the exception dialog.")]
    [DefaultValue(DEFAULT_EXCEPTION_DIALOG_NONE)]
    [RefreshProperties(RefreshProperties.All)]
    public string None { get; set; }

    /// <summary>Gets or sets the data text.</summary>
    /// <value>The data text.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The exception outline header for the exception dialog.")]
    [DefaultValue(DEFAULT_EXCEPTION_DIALOG_DATA)]
    [RefreshProperties(RefreshProperties.All)]
    public string Data { get; set; }

    /// <summary>Gets or sets the 'Line' text.</summary>
    /// <value>The 'Line' text.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The 'Line' text for the exception dialog.")]
    [DefaultValue(DEFAULT_EXCEPTION_DIALOG_LINE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Line { get; set; }

    /// <summary>Gets or sets the search box cue text.</summary>
    /// <value>The search box cue text.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The search box cue text for the exception dialog.")]
    [DefaultValue(DEFAULT_EXCEPTION_DIALOG_SEARCH)]
    [RefreshProperties(RefreshProperties.All)]
    public string SearchBoxCueText { get; set; }

    /// <summary>Gets or sets the no results found.</summary>
    /// <value>The no results found.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The no results found text for the exception dialog.")]
    [DefaultValue(DEFAULT_EXCEPTION_DIALOG_NO_RESULTS_FOUND)]
    [RefreshProperties(RefreshProperties.All)]
    public string NoResultsFound { get; set; }

    /// <summary>Gets or sets the result.</summary>
    /// <value>The result.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The result text for the exception dialog.")]
    [DefaultValue(DEFAULT_EXCEPTION_DIALOG_RESULT)]
    [RefreshProperties(RefreshProperties.All)]
    public string Result { get; set; }

    /// <summary>Gets or sets the results appendage.</summary>
    /// <value>The results appendage.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The results appendage text for the exception dialog.")]
    [DefaultValue(DEFAULT_EXCEPTION_DIALOG_RESULTS_APPENDAGE)]
    [RefreshProperties(RefreshProperties.All)]
    public string ResultsAppendage { get; set; }

    /// <summary>Gets or sets the results found appendage.</summary>
    /// <value>The results found appendage.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The results found appendage text for the exception dialog.")]
    [DefaultValue(DEFAULT_EXCEPTION_DIALOG_RESULTS_FOUND_APPENDAGE)]
    [RefreshProperties(RefreshProperties.All)]
    public string ResultsFoundAppendage { get; set; }

    /// <summary>Gets or sets the no matches found.</summary>
    /// <value>The no matches found.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The no matches found text for the exception dialog.")]
    [DefaultValue(DEFAULT_EXCEPTION_DIALOG_NO_MATCHES_FOUND)]
    [RefreshProperties(RefreshProperties.All)]
    public string NoMatchesFound { get; set; }

    /// <summary>Gets or sets the type to search.</summary>
    /// <value>The type to search.</value>
    [Localizable(true)]
    [Category(@"Values")]
    [Description(@"The type to search text for the exception dialog.")]
    [DefaultValue(DEFAULT_EXCEPTION_DIALOG_TYPE_TO_SEARCH)]
    [RefreshProperties(RefreshProperties.All)]
    public string TypeToSearch { get; set; }

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

        Line = DEFAULT_EXCEPTION_DIALOG_LINE;

        SearchBoxCueText = DEFAULT_EXCEPTION_DIALOG_SEARCH;

        NoResultsFound = DEFAULT_EXCEPTION_DIALOG_NO_RESULTS_FOUND;

        Result = DEFAULT_EXCEPTION_DIALOG_RESULT;

        ResultsAppendage = DEFAULT_EXCEPTION_DIALOG_RESULTS_APPENDAGE;

        ResultsFoundAppendage = DEFAULT_EXCEPTION_DIALOG_RESULTS_FOUND_APPENDAGE;

        NoMatchesFound = DEFAULT_EXCEPTION_DIALOG_NO_MATCHES_FOUND;

        TypeToSearch = DEFAULT_EXCEPTION_DIALOG_TYPE_TO_SEARCH;
    }

    #endregion
}