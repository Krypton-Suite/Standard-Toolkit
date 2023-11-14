#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>Specifies constants defining which buttons to display on a <see cref="T:KryptonMessageBox" />.</summary>
    public enum KryptonMessageBoxButtons
    {
        /// <summary>
        ///  Specifies that the message box contains an OK button.
        /// </summary>
        OK = MessageBoxButtons.OK,

        /// <summary>
        ///  Specifies that the message box contains OK and Cancel buttons.
        /// </summary>
        OKCancel = MessageBoxButtons.OKCancel,

        /// <summary>
        ///  Specifies that the message box contains Abort, Retry, and Ignore buttons.
        /// </summary>
        AbortRetryIgnore = MessageBoxButtons.AbortRetryIgnore,

        /// <summary>
        ///  Specifies that the message box contains Yes, No, and Cancel buttons.
        /// </summary>
        YesNoCancel = MessageBoxButtons.YesNoCancel,

        /// <summary>
        ///  Specifies that the message box contains Yes and No buttons.
        /// </summary>
        YesNo = MessageBoxButtons.YesNo,

        /// <summary>
        ///  Specifies that the message box contains Retry and Cancel buttons.
        /// </summary>
        RetryCancel = MessageBoxButtons.RetryCancel,

        /// <summary>
        ///  Specifies that the message box contains Cancel, Try Again, and Continue buttons.
        /// </summary>
#if NET60_OR_GREATER
            CancelTryContinue = MessageBoxButtons.CancelTryContinue
#else
        CancelTryContinue = 0x00000006
#endif
    }
}