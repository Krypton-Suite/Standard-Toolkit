#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>Manages the interaction between the <see cref="KryptonToastNotification"/> API and the Krypton toast notification user input types.</summary>
    internal class KryptonToastNotificationUserInputController
    {
        #region Implementation

        public static object ShowToast(KryptonUserInputToastNotificationData data)
        {
            switch (data.NotificationInputAreaType)
            {
                case KryptonToastNotificationInputAreaType.ComboBox:
                case KryptonToastNotificationInputAreaType.DomainUpDown:
                case KryptonToastNotificationInputAreaType.MaskedTextBox:
                case KryptonToastNotificationInputAreaType.TextBox:
                    return ReturnStringInput(data);
                case KryptonToastNotificationInputAreaType.DateTime:
                    return ReturnDateTimeInput(data);
                case KryptonToastNotificationInputAreaType.NumericDropDown:
                    return ReturnIntegerInput(data);
                case null:
                    throw new ArgumentNullException();
                default:
                    DebugTools.NotImplemented(data.ToString());
                    break;
            }

            return new object();
        }

        internal static DateTime ReturnDateTimeInput(KryptonUserInputToastNotificationData data)
        {
            return DateTime.Now;
        }

        internal static int ReturnIntegerInput(KryptonUserInputToastNotificationData data)
        {
            return 0;
        }

        internal static string ReturnStringInput(KryptonUserInputToastNotificationData data)
        {
            return GlobalStaticValues.DEFAULT_EMPTY_STRING;
        }

        #endregion
    }
}