#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Delivers and caches images based on their KryptonTaskDialogIconType.<br/>
/// Images custom sized.
/// </summary>
public class KryptonTaskDialogIconController : IDisposable
{
    #region Types
    /// <summary>
    /// Used internally by KryptonTaskDialogIconController to cache images
    /// </summary>
    public class ImageItem : IEquatable<ImageItem>
    {
        #region Identity
        public ImageItem(KryptonTaskDialogIconType iconType, int size)
        {
            IconType = iconType;
            Size = size;
        }
        #endregion

        #region public 
        public KryptonTaskDialogIconType IconType { get; }
        public int Size { get; }

        /// <inheritdoc/>
        public bool Equals(ImageItem? other)
        {
            return other is not null
                && this.IconType == other.IconType
                && this.Size == other.Size;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is ImageItem imageItem 
                && this.Equals(imageItem);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return IconType.GetHashCode() ^ Size.GetHashCode();
        }
        #endregion
    }
    #endregion

    #region Fields
    private Dictionary<ImageItem, Image> _imageCache;
    private bool _disposed;
    #endregion

    #region Identity
    /// <summary>
    /// KryptonTaskDialog caching image controller that serves images based on KryptonTaskDialogIconType and requested image size.<br/>
    /// If the base image differs in size it will resized to a square of the requested size.
    /// </summary>
    public KryptonTaskDialogIconController()
    {
        _imageCache = [];
    }
    #endregion

    #region Public
    /// <summary>
    /// Retrieves the image type and sizes it to imageItem.Size.<br/>
    /// Using KryptonTaskDialogIconType.None as IconType is invalied and will thrown an exception.
    /// </summary>
    /// <param name="icontype">Except for None, any of the values in KryptonTaskDialogIconType to return the requested image.</param>
    /// <param name="size">The desired size of the image</param>
    /// <returns>The requested image with the desired size.</returns>
    public Image GetImage(KryptonTaskDialogIconType icontype, int size)
    {
        ImageItem imageItem = new(icontype, size);

        if (_imageCache.TryGetValue(imageItem, out Image? image))
        {
            return image;
        }

        Image newImage = GetImageInternal(in imageItem);
        if (newImage.Size.Width != imageItem.Size || newImage.Size.Height != imageItem.Size)
        {
            // Resize the image and cache it.
            newImage = new Bitmap(newImage, imageItem.Size, imageItem.Size);
            _imageCache.Add(imageItem, newImage);
        }

        return newImage;
    }
    #endregion

    #region Private
    private Image GetImageInternal(in ImageItem imageItem)
    {
        Icon icon = imageItem.IconType switch
        {
            KryptonTaskDialogIconType.ArrowGrayDown     => TaskDialogImageResources.arrow_down_gray_multi_icon,
            KryptonTaskDialogIconType.ArrowGrayUp       => TaskDialogImageResources.arrow_up_gray_multi_icon,
            KryptonTaskDialogIconType.CheckGreen        => TaskDialogImageResources.check_green_multi_icon,
            KryptonTaskDialogIconType.Document          => TaskDialogImageResources.document__multi_icon,
            KryptonTaskDialogIconType.Gear              => TaskDialogImageResources.gear__multi_icon,
            KryptonTaskDialogIconType.PowerOff          => TaskDialogImageResources.power_off_multi_icon,
            KryptonTaskDialogIconType.ShieldError       => TaskDialogImageResources.shield_error_multi_icon,
            KryptonTaskDialogIconType.ShieldHelp        => TaskDialogImageResources.shield_help_multi_icon,
            KryptonTaskDialogIconType.ShieldInformation => TaskDialogImageResources.shield_questionmark_multi_icon,
            KryptonTaskDialogIconType.ShieldKrypton     => TaskDialogImageResources.shield_krypton_multi_icon,
            KryptonTaskDialogIconType.ShieldSuccess     => TaskDialogImageResources.shield_success_checked_multi_icon,
            KryptonTaskDialogIconType.ShieldUac         => TaskDialogImageResources.shield_uac_multi_icon,
            KryptonTaskDialogIconType.ShieldWarning     => TaskDialogImageResources.shield_exclamation_warning_multi_icon,

            // KryptonTaskDialogIconType.None is not an image type and will also thrown an exception.
            _ => throw new ArgumentOutOfRangeException($"IconType {imageItem.IconType} is unknown or not an image type.")
        };

        return new Icon(icon, imageItem.Size, imageItem.Size).ToBitmap();
    }
    #endregion

    #region IDisposable
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _imageCache.Clear();
            _disposed = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion
}
