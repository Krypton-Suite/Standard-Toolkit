namespace Krypton.Toolkit
{
    internal static class DebugTools
    {
        #region Implementation
        internal static void NotImplemented(string methodSignature, string className, int lineNumber = 0)
        {
            if (lineNumber > 0)
            {
                KryptonMessageBox.Show($"If you are seeing this message, please submit a new bug report at: https://github.com/Krypton-Suite/Standard-Toolkit/issues/new/choose.\n\nAdditional details:-\nMethod Signature: {methodSignature}\nClass Name: {className}\nLine Number: {lineNumber}", "Not Implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                KryptonMessageBox.Show($"If you are seeing this message, please submit a new bug report at: https://github.com/Krypton-Suite/Standard-Toolkit/issues/new/choose.\n\nAdditional details:-\nMethod Signature: {methodSignature}\nClass Name: {className}", "Not Implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
    }
}