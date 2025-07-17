#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// This class is used  within advanved columns that make use of embedded controls.
/// </summary>
[ToolboxItem(false)]
internal class KryptonDataGridViewCellIndicatorImage : IDisposable
{
    #region Fields
    // Cell indicator image
    private Image? _image = null;
    // Size of the image which is always square
    private int _size;
    // Datagridview the column belongs to.
    private KryptonDataGridView? _dataGridView;
    // State of disposal
    private bool _disposed = false;
        
    // type and state of the image
    private PaletteRibbonGalleryButton _paletteRibbonGalleryButton = PaletteRibbonGalleryButton.Down;
    private PaletteState _paletteState = PaletteState.Normal;
    #endregion Fields

    #region Identity
    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="imageSize">The image size in pixels. Which will always be used to square the image. Default is 14.</param>
    public KryptonDataGridViewCellIndicatorImage(int imageSize = 14)
    {
        _size = imageSize;

        UpdateCellIndicatorImage(true);
        KryptonManager.GlobalPaletteChanged += OnKryptonManagerGlobalPaletteChanged;
    }
    #endregion Identity

    #region Public
    /// <summary>
    /// Reference to the column's DataGridView.<br/>
    /// Set this property via the column's 'protected override void OnDataGridViewChanged()'.
    /// </summary>
    public KryptonDataGridView? DataGridView
    { 
        get => _dataGridView;

        set
        {
            if (_dataGridView != value)
            {
                if (_dataGridView is not null)
                {
                    _dataGridView.PaletteChanged -= OnDataGridViewPaletteChanged;
                }

                _dataGridView = value;

                if (_dataGridView is not null)
                {
                    _dataGridView.PaletteChanged += OnDataGridViewPaletteChanged;
                    UpdateCellIndicatorImage(false);
                }
            }
        }
    }
        
    /// <summary>
    /// Cell indicator image.
    /// </summary>
    public virtual Image? Image => _image;

    /// <inheritdoc/>>
    public void Dispose()
    {
        Dispose(true);
    }

    /// <inheritdoc cref="Dispose()"/>
    public void Dispose(bool disposing)
    {
        // If the column is disposed at runtime the eventhandlers need to unsubscribe from the grid and kmananger
        try
        {
            if (!_disposed && disposing)
            {
                // Since the DataGridView property is controlled internally,
                // use the cached reference to unsubscribe from the event
                if (_dataGridView is not null)
                {
                    _dataGridView.PaletteChanged -= OnDataGridViewPaletteChanged;
                }

                KryptonManager.GlobalPaletteChanged -= OnKryptonManagerGlobalPaletteChanged;
                _dataGridView = null;

                _disposed = true;
            }
        }
        catch { }
    }
    #endregion Public

    #region Private
    /// <summary>
    /// Subscribe this handler to: KryptonDataGridView.PaletteChanged
    /// </summary>
    /// <param name="sender">Not used.</param>
    /// <param name="e">Not used.</param>
    private void OnDataGridViewPaletteChanged(object? sender, EventArgs e) => UpdateCellIndicatorImage(false);

    /// <summary>
    /// Subscribe this handler to: KryptonManager.GlobalPaletteChanged
    /// </summary>
    /// <param name="sender">Not used.</param>
    /// <param name="e">Not used.</param>
    private void OnKryptonManagerGlobalPaletteChanged(object? sender, EventArgs e) => UpdateCellIndicatorImage(true);

    /// <summary>
    /// Updates the cell indicator image based on the source from where the theme change originated.
    /// </summary>
    /// <param name="updateFromKryptonManager">True if KryptonManager fired the theme change, otherwise from KryptonDataGridView.</param>
    private void UpdateCellIndicatorImage(bool updateFromKryptonManager)
    {
        if (updateFromKryptonManager)
        {
            // Probably the case used most, so first to check.
            _image = KryptonManager.CurrentGlobalPalette.GetGalleryButtonImage(_paletteRibbonGalleryButton, _paletteState)!;
            ResizeCellIndicatorImage();
        }
        else if (DataGridView is KryptonDataGridView dataGridView)
        {
            if (dataGridView.Palette is not null && dataGridView.PaletteMode == PaletteMode.Custom)
            {
                // The grid has a custom palette instance assigned to it and PaletteMode is Custom
                _image = dataGridView.Palette.GetGalleryButtonImage(_paletteRibbonGalleryButton, _paletteState);
            }
            else
            {
                // Fetch through KryptonManager using the locally set palette mode
                _image = KryptonManager
                    .GetPaletteForMode(dataGridView.PaletteMode)
                    .GetGalleryButtonImage(_paletteRibbonGalleryButton, _paletteState)!;
            }

            ResizeCellIndicatorImage();
        }
    }

    /// <summary>
    /// This method may only be used by UpdateCellIndicatorImage()
    /// </summary>
    private void ResizeCellIndicatorImage()
    {
        if (_image is not null && (_image.Width != _size || _image.Height != _size))
        {
            _image = new Bitmap(_image, _size, _size);
        }
    }
    #endregion Private
}