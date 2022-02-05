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

        #region Methods
        /// <summary>Captures the exception.</summary>
        /// <param name="exception">The exception.</param>
        /// <param name="title">The title.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="methodSignature">The method signature.</param>
        public static void CaptureException(Exception exception, string title = @"Exception Caught", MessageBoxButtons buttons = MessageBoxButtons.OK, KryptonMessageBoxIcon icon = KryptonMessageBoxIcon.ERROR, string className = "", string methodSignature = "")
        {
            if (className != "")
            {
                KryptonMessageBox.Show($"An unexpected error has occurred: { exception.Message }.\n\nError in class: '{ className }.cs'.", title, buttons, icon);
            }
            else if (methodSignature != "")
            {
                KryptonMessageBox.Show($"An unexpected error has occurred: { exception.Message }.\n\nError in method: '{ methodSignature }'.", title, buttons, icon);
            }
            else if (className != "" && methodSignature != "")
            {
                KryptonMessageBox.Show($"An unexpected error has occurred: { exception.Message }.\n\nError in class: '{ className }.cs'.\n\nError in method: '{ methodSignature }'.", title, buttons, icon);
            }
            else
            {
                KryptonMessageBox.Show($"An unexpected error has occurred: { exception.Message }.", title, buttons, icon);
            }
        }

        /// <summary>Captures a stack trace of the exception.</summary>
        /// <param name="exc">The incoming exception.</param>
        /// <param name="fileName">The file to write the exception stack trace to.</param>
        public static void PrintStackTrace(Exception exc, string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    File.Create(fileName);
                }

                StreamWriter writer = new(fileName);

                writer.Write(exc.ToString());

                writer.Close();

                writer.Dispose();
            }
            catch (Exception e)
            {
                CaptureException(e);
            }
        }

        /// <summary>Captures a stack trace of the exception.</summary>
        /// <param name="exc">The incoming exception.</param>
        /// <param name="fileName">The file to write the exception stack trace to.</param>
        public static void PrintExceptionStackTrace(Exception exc, string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    File.Create(fileName);
                }

                StreamWriter writer = new(fileName);

                writer.Write(exc.StackTrace);

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