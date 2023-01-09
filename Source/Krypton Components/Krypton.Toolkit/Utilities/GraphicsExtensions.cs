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
    /// <summary>Allows the manipulation of graphics.</summary>
    public static class GraphicsExtensions
    {
        #region Identity

        static GraphicsExtensions()
        {

        }

        #endregion

        #region Implementation

        /// <summary>Loads the icon.</summary>
        /// <param name="type">The type of icon.</param>
        /// <param name="size">The size.</param>
        /// <returns>The icon.</returns>
        /// <exception cref="System.PlatformNotSupportedException"></exception>
        public static Icon LoadIcon(IconType type, Size size)
        {
            IntPtr hIcon = ImageNativeMethods.LoadImage(IntPtr.Zero, "#" + (int)type, 1, size.Width, size.Height, 0);
            return hIcon == IntPtr.Zero ? null : Icon.FromHandle(hIcon);
        }

        /// <summary>Returns an icon representation of an image that is contained in the specified file.</summary>
        /// <param name="executablePath"></param>
        /// <returns></returns>
        public static Icon ExtractIconFromFilePath(string executablePath)
        {
            Icon result = (Icon)null;

            try
            {
                result = Icon.ExtractAssociatedIcon(executablePath);
            }
            catch (Exception e)
            {
                //Console.WriteLine("Unable to extract the icon from the binary");

                ExceptionHandler.CaptureException(e);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public enum SystemIconSize
        {
            Small = 0,
            Medium = 1,
            Large = 2,
            Custom = 3
        }

        /*
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
        */

        /// <summary>Resize the image to the specified width and height. Copied from: https://stackoverflow.com/questions/1922040/how-to-resize-an-image-c-sharp</summary>
        /// <param name="sourceImage">The image to resize.</param>
        /// <param name="imageSize">The size that you want to resize the image to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ScaleImage(Image sourceImage, Size? imageSize)
        {
            Size tmpSize = imageSize ?? new Size(16, 16);

            var destRect = new Rectangle(0, 0, tmpSize.Width, tmpSize.Height);

            var destImage = new Bitmap(tmpSize.Width, tmpSize.Height);

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

    #endregion
}