#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal static class IconHelper
{
    #region Instance Fields

    private static byte[] _pngIconHeader =
        new byte[] { 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 24, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    #endregion

    #region Implementation

    /// <summary>Creates an icon from an image. Code adapted from https://www.codeproject.com/tips/627823/fast-and-high-quality-bitmap-to-icon-converter.</summary>
    /// <param name="sourceImage">The source image.</param>
    /// <param name="iconSize">Size of the icon.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public static Icon CreateIconFromImage(Image sourceImage, Size? iconSize)
    {
        Size newSize = iconSize ?? new Size(16, 16);

        using var bmp = new Bitmap(sourceImage, newSize);
        byte[] png;

        using (var ms = new MemoryStream())
        {
            bmp.Save(ms, ImageFormat.Png);

            ms.Position = 0;

            png = ms.ToArray();
        }

        using (var ms2 = new MemoryStream())
        {
            if (newSize.Height > 256 && newSize.Height > 256)
            {
                newSize = new Size(0, 0);
            }

            _pngIconHeader[6] = (byte)newSize.Width;

            _pngIconHeader[7] = (byte)newSize.Height;

            _pngIconHeader[14] = (byte)(png.Length & 255);

            _pngIconHeader[15] = (byte)(png.Length / 256);

            _pngIconHeader[18] = (byte)(_pngIconHeader.Length);

            ms2.Write(_pngIconHeader, 0, _pngIconHeader.Length);

            ms2.Write(png, 0, png.Length);

            ms2.Position = 0;

            return new Icon(ms2);
        }
    }

    internal static Icon? GetIconFromEnum(InformationBoxIcon iconType)
    {
        switch (iconType)
        {
            case InformationBoxIcon.None:
                return null;
            case InformationBoxIcon.Hand:
                return CreateIconFromImage(MessageBoxImageResources.GenericHand, null);
            case InformationBoxIcon.SystemHand:
                return SystemIcons.Hand;
            case InformationBoxIcon.Question:
                return CreateIconFromImage(MessageBoxImageResources.GenericQuestion, null);
            case InformationBoxIcon.SystemQuestion:
                return SystemIcons.Question;
            case InformationBoxIcon.Exclamation:
            case InformationBoxIcon.Warning:
                return CreateIconFromImage(MessageBoxImageResources.GenericWarning, null);
            case InformationBoxIcon.SystemExclamation:
                return SystemIcons.Exclamation;
            case InformationBoxIcon.Asterisk:
                return CreateIconFromImage(MessageBoxImageResources.GenericAsterisk, null);
            case InformationBoxIcon.SystemAsterisk:
                return SystemIcons.Asterisk;
            case InformationBoxIcon.Stop:
            case InformationBoxIcon.Error:
                return CreateIconFromImage(MessageBoxImageResources.GenericStop, null);
            case InformationBoxIcon.Information:
                return CreateIconFromImage(MessageBoxImageResources.GenericInformation, null);
            case InformationBoxIcon.Shield:
                if (OSUtilities.IsAtLeastWindowsEleven)
                {
                    return CreateIconFromImage(UACShieldIconResources.UACShieldWindows11, null);
                }
                else if (OSUtilities.IsWindowsTen)
                {
                    return CreateIconFromImage(UACShieldIconResources.UACShieldWindows10, null);
                }
                else
                {
                    return CreateIconFromImage(UACShieldIconResources.UACShieldWindows7881, null);
                }
            case InformationBoxIcon.WindowsLogo:
                break;
            case InformationBoxIcon.Application:
                break;
            case InformationBoxIcon.SystemApplication:
                return SystemIcons.Application;
            default:
                DebugTools.NotImplemented(iconType.ToString());
                return null;
        }

        return null;
    }

    #endregion
}