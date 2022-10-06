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
    internal class IconUtilities
    {
        /// <summary>Initializes a new instance of the <see cref="IconUtilities" /> class.</summary>
        public IconUtilities()
        {

        }

        public enum SystemIconSize
        {
            Small = 0,
            Medium = 1,
            Large = 2,
            Custom = 3
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

        /// <summary>Resize the image to the specified width and height. Copied from: https://stackoverflow.com/questions/1922040/how-to-resize-an-image-c-sharp</summary>
        /// <param name="sourceImage">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        internal static Bitmap ScaleImage(Image sourceImage, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);

            var destImage = new Bitmap(width, height);

            destImage.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;

                graphics.CompositingQuality = CompositingQuality.HighQuality;

                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);

                    graphics.DrawImage(sourceImage, destRect, 0, 0, sourceImage.Width, sourceImage.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
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
        None = 0,
        /// <summary>Specify a hand icon.</summary>
        Hand = 1,
        /// <summary>Specify a question icon.</summary>
        Question = 2,
        /// <summary>Specify a exclamation icon.</summary>
        Exclamation = 3,
        /// <summary>Specify a asterisk icon.</summary>
        Asterisk = 4,
        /// <summary>Specify a stop icon.</summary>
        Stop = 5,
        /// <summary>Specify a error icon.</summary>
        Error = 6,
        /// <summary>Specify a warning icon.</summary>
        Warning = 7,
        /// <summary>Specify a information icon.</summary>
        Information = 8,
        /// <summary>Specify a UAC shield icon.</summary>
        Shield = 9,
        /// <summary>Specify a Windows logo icon.</summary>
        WindowsLogo = 10
    }
}