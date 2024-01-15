namespace Krypton.Toolkit
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory(@"code")]
    public static class KryptonInputBox
    {
        #region Public

        /// <summary>
        /// Displays an input box with provided prompt and caption and defaulted response string.
        /// </summary>
        /// <param name="inputBoxData">The data to feed through to <see cref="VisualInputBoxForm"/>.</param>
        /// <returns>Input string.</returns>
        public static string Show(KryptonInputBoxData inputBoxData)
            =>  InternalShow(inputBoxData);

        #endregion

        #region Implementation

        internal static string InternalShow(KryptonInputBoxData inputBoxData)
        {
            if (inputBoxData.UseRtlReading is { } or true)
            {
                return VisualInputBoxRtlAwareForm.InternalShow(inputBoxData);
            }
            else
            {
                return VisualInputBoxForm.InternalShow(inputBoxData);
            }
        }

        #endregion
    }
}