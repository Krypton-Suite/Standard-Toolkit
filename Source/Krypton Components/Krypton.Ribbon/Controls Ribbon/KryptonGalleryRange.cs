#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Represents a single gallery range.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonGalleryRange), "ToolboxBitmaps.KryptonGalleryRange.bmp")]
[DefaultProperty(nameof(Heading))]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
public class KryptonGalleryRange : Component
{
    #region Instance Fields
    private string _heading;
    private int _imageIndexStart;
    private int _imageIndexEnd;
    #endregion

    #region Events
    /// <summary>
    /// Occurs after the value of a property has changed.
    /// </summary>
    [Category(@"Gallery")]
    [Description(@"Occurs after the value of a property has changed.")]
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialise a new instance of the KryptonGalleryRange class.
    /// </summary>
    public KryptonGalleryRange()
    {
        // Default fields
        _heading = nameof(Heading);
        _imageIndexStart = -1;
        _imageIndexEnd = -1;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the gallery range heading text.
    /// </summary>
    [Bindable(true)]
    [Localizable(true)]
    [Category(@"Appearance")]
    [Description(@"Gallery range heading text.")]
    [DefaultValue(nameof(Heading))]
    public string Heading
    {
        get => _heading;

        set
        {
            if (value != _heading)
            {
                _heading = value;
                OnPropertyChanged(nameof(Heading));
            }
        }
    }

    /// <summary>
    /// Gets and sets the index of first image in the gallery ImageList for display.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Index of first image in the gallery ImageList for display.")]
    [DefaultValue(-1)]
    public int ImageIndexStart
    {
        get => _imageIndexStart;

        set
        {
            if (_imageIndexStart != value)
            {
                _imageIndexStart = value;
                OnPropertyChanged(nameof(ImageIndexStart));
            }
        }
    }

    /// <summary>
    /// Gets and sets the index of last image in the gallery ImageList for display.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Index of last image in the gallery ImageList for display.")]
    [DefaultValue(-1)]
    public int ImageIndexEnd
    {
        get => _imageIndexEnd;

        set
        {
            if (_imageIndexEnd != value)
            {
                _imageIndexEnd = value;
                OnPropertyChanged(nameof(ImageIndexEnd));
            }
        }
    }
    #endregion

    #region Protected
    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">Name of property that has changed.</param>
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}