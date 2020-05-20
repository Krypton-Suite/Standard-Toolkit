// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  Created by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2020 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System;
using System.IO;
using System.Windows.Forms;

namespace Krypton.Toolkit
{
    /// <summary>
    /// This class is designed to handle thrown exceptions. (FOR INTERNAL USE ONLY!)
    /// </summary>
    internal class ExceptionHandler
    {
        #region Constructor
        public ExceptionHandler()
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Captures the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="title">The title.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="methodSignature">The method signature.</param>
        public static void CaptureException(Exception exception, string title = @"Exception Caught", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Error, string className = "", string methodSignature = "")
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

        /// <summary>
        /// Captures a stacktrace of the exception.
        /// </summary>
        /// <param name="exc">The incoming exception.</param>
        /// <param name="fileName">The file to write the exception stacktrace to.</param>
        public static void PrintStackTrace(Exception exc, string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    File.Create(fileName);
                }

                StreamWriter writer = new StreamWriter(fileName);

                writer.Write(exc.ToString());

                writer.Close();

                writer.Dispose();
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// Captures a stacktrace of the exception.
        /// </summary>
        /// <param name="exc">The incoming exception.</param>
        /// <param name="fileName">The file to write the exception stacktrace to.</param>
        public static void PrintExceptionStackTrace(Exception exc, string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    File.Create(fileName);
                }

                StreamWriter writer = new StreamWriter(fileName);

                writer.Write(exc.StackTrace);

                writer.Close();

                writer.Dispose();
            }
            catch
            {

                throw;
            }
        }
        #endregion
    }
}