// *****************************************************************************
// BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit)
// by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
//  Version 6.0.0  
// *****************************************************************************

namespace Krypton.Toolkit
{
    internal class IconUtilities
    {
        public IconUtilities()
        {

        }

        public enum SystemIconSize
        {
            SMALL = 0,
            MEDIUM = 1,
            LARGE = 2
        }

        /// <summary>
        /// Loads the icon.
        /// </summary>
        /// <param name="type">The type of icon.</param>
        /// <param name="size">The size.</param>
        /// <returns>The icon.</returns>
        /// <exception cref="PlatformNotSupportedException"></exception>
        public static Icon LoadIcon(IconType type, Size size)
        {
            IntPtr hIcon = PI.LoadImage(IntPtr.Zero, "#" + (int)type, 1, size.Width, size.Height, 0);
            return hIcon == IntPtr.Zero ? null : Icon.FromHandle(hIcon);
        }

        public enum IconType
        {
            Warning = 101,
            Help = 102,
            Error = 103,
            Info = 104,
            Shield = 106
        }

        public static Image SetIcon(Image image, Size size) => (Image)new Bitmap(image, size);
    }

    public enum KryptonMessageBoxIcon
    {
        /// <summary>Specify no icon.</summary>
        NONE = 0,
        /// <summary>Specify a hand icon.</summary>
        HAND = 1,
        /// <summary>Specify a question icon.</summary>
        QUESTION = 2,
        /// <summary>Specify a exclamation icon.</summary>
        EXCLAMATION = 3,
        /// <summary>Specify a asterisk icon.</summary>
        ASTERISK = 4,
        /// <summary>Specify a stop icon.</summary>
        STOP = 5,
        /// <summary>Specify a error icon.</summary>
        ERROR = 6,
        /// <summary>Specify a warning icon.</summary>
        WARNING = 7,
        /// <summary>Specify a information icon.</summary>
        INFORMATION = 8,
        /// <summary>Specify a UAC shield icon.</summary>
        SHIELD = 9,
        /// <summary>Specify a Windows logo icon.</summary>
        WINDOWSLOGO = 10
    }
}