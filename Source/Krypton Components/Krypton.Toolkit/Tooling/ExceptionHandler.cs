#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// This class is designed to handle thrown exceptions. (FOR INTERNAL USE ONLY!)
    /// </summary>
    internal class ExceptionHandler
    {
        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="ExceptionHandler" /> class.</summary>
        public ExceptionHandler()
        {

        }
        #endregion

        #region Implementation

        /// <summary>Captures the exception.</summary>
        /// <param name="exception">The exception.</param>
        /// <param name="title">The title.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="methodSignature">The method signature.</param>
        public static void CaptureException(Exception exception, string title = @"Exception Caught", KryptonMessageBoxButtons buttons = KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon icon = KryptonMessageBoxIcon.Warning, string className = "", string methodSignature = "")
        {
            KryptonCommand internalCommand = new KryptonCommand();

            internalCommand.Execute += (sender, args) => GlobalToolkitUtilities.LaunchProcess(@"https://github.com/Krypton-Suite/Standard-Toolkit/issues/new/choose");

            if (className != "")
            {
                string text =
                    $"An unexpected error has occurred: {exception.Message}.\n\nError in class: '{className}.cs'.\n\nPost a new bug report.";

                KryptonMessageBox.Show(text, title, buttons, icon, actionButtonCommand: internalCommand, contentAreaType: MessageBoxContentAreaType.LinkLabel, contentLinkArea: new LinkArea(text.Length - 7, text.Length - 1));
            }
            else if (methodSignature != "")
            {
                string text =
                    $"An unexpected error has occurred: {exception.Message}.\n\nError in method: '{methodSignature}'.\n\nPost a new bug report.";

                KryptonMessageBox.Show(text, title, buttons, icon, actionButtonCommand: internalCommand, contentAreaType: MessageBoxContentAreaType.LinkLabel, contentLinkArea: new LinkArea(text.Length - 7, text.Length - 1));
            }
            else if (className != "" && methodSignature != "")
            {
                string text =
                    $"An unexpected error has occurred: {exception.Message}.\n\nError in class: '{className}.cs'.\n\nError in method: '{methodSignature}'.\n\nPost a new bug report.";

                KryptonMessageBox.Show(text, title, buttons, icon, actionButtonCommand: internalCommand, contentAreaType: MessageBoxContentAreaType.LinkLabel, contentLinkArea: new LinkArea(text.Length - 7, text.Length - 1));
            }
            else
            {
                string text = $"An unexpected error has occurred: {exception.Message}.\n\nPost a new bug report.";

                KryptonMessageBox.Show(text, title, buttons, icon, actionButtonCommand: internalCommand, contentAreaType: MessageBoxContentAreaType.LinkLabel, contentLinkArea: new LinkArea(text.Length - 7, text.Length - 1));
            }
        }

        /// <summary>Captures a stack trace of the exception.</summary>
        /// <param name="exception">The incoming exception.</param>
        /// <param name="fileName">The file to write the exception stack trace to.</param>
        public static void PrintStackTrace(Exception exception, string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    File.Create(fileName);
                }

                var writer = new StreamWriter(fileName);

                writer.Write(exception.ToString());

                writer.Close();

                writer.Dispose();
            }
            catch (Exception e)
            {
                CaptureException(e);
            }
        }

        /// <summary>Captures a stack trace of the exception.</summary>
        /// <param name="exception">The incoming exception.</param>
        /// <param name="fileName">The file to write the exception stack trace to.</param>
        public static void PrintExceptionStackTrace(Exception exception, string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    File.Create(fileName);
                }

                var writer = new StreamWriter(fileName);

                writer.Write(exception.StackTrace);

                writer.Close();

                writer.Dispose();
            }
            catch (Exception e)
            {
                CaptureException(e);
            }
        }

        #endregion
    }
}