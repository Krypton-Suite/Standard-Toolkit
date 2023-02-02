namespace Krypton.Toolkit
{
    [ToolboxItem(false)]
    [DesignerCategory(@"code")]
    public static class KryptonInputBox
    {
        #region Public

        /// <summary>
        /// Displays an input box with provided prompt and caption and defaulted response string.
        /// </summary>
        /// <param name="prompt">The text to display as an input prompt.</param>
        /// <param name="caption">The text to display in the title bar of the input box.</param>
        /// <param name="defaultResponse">Default response text..</param>
        /// <param name="cueText">The cue text.</param>
        /// <param name="cueColour">The colour of the cue.</param>
        /// <param name="cueTypeface">The cue font.</param>
        /// <param name="usePasswordOption">Enables the password option.</param>
        /// <returns>Input string.</returns>
        public static string Show(string prompt,
            string caption = @"",
            string defaultResponse = @"",
            string cueText = @"",
            Color cueColour = new(),
            Font cueTypeface = null,
            bool usePasswordOption = false)
            =>  InternalShow(null, prompt, caption, defaultResponse, cueText, cueColour, cueTypeface, usePasswordOption);

        /// <summary>
        /// DDisplays an input box in front of the specified object and with the provided prompt and caption and defaulted response string.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="prompt">The text to display as an input prompt.</param>
        /// <param name="caption">The text to display in the title bar of the input box.</param>
        /// <param name="defaultResponse">Default response text..</param>
        /// <param name="cueText">The cue text.</param>
        /// <param name="cueColour">The colour of the cue.</param>
        /// <param name="cueTypeface">The cue font.</param>
        /// <param name="usePasswordOption">Enables the password option.</param>
        /// <returns>Input string.</returns>
        public static string Show(IWin32Window owner, string prompt,
            string caption = @"",
            string defaultResponse = @"",
            string cueText = @"",
            Color cueColour = new(),
            Font cueTypeface = null,
            bool usePasswordOption = false)
            => InternalShow(owner, prompt, caption, defaultResponse, cueText, cueColour, cueTypeface, usePasswordOption);

        #endregion

        #region Implementation

        private static string InternalShow(IWin32Window owner,
            string prompt,
            string caption,
            string defaultResponse,
            string cueText,
            Color cueColour,
            Font cueTypeface,
            bool usePasswordOption) =>
            KryptonInputBoxForm.InternalShow(owner, prompt, caption, defaultResponse, cueText, cueColour,
                cueTypeface, usePasswordOption);

        #endregion
    }
}