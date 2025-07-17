#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for gallery button images.
/// </summary>
public class KryptonPaletteImagesGalleryButtons : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteImagesGalleryButtons class.
    /// </summary>
    /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteImagesGalleryButtons(PaletteRedirect? redirector,
        NeedPaintHandler needPaint) 
    {
        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Create the storage
        Up = new KryptonPaletteImagesGalleryButton(PaletteRibbonGalleryButton.Up, redirector, needPaint);
        Down = new KryptonPaletteImagesGalleryButton(PaletteRibbonGalleryButton.Down, redirector, needPaint);
        DropDown = new KryptonPaletteImagesGalleryButton(PaletteRibbonGalleryButton.DropDown, redirector, needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => Up.IsDefault &&
                                      Down.IsDefault &&
                                      DropDown.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    public void PopulateFromBase()
    {
        Up.PopulateFromBase();
        Down.PopulateFromBase();
        DropDown.PopulateFromBase();
    }
    #endregion

    #region Up
    /// <summary>
    /// Gallery up button images.
    /// </summary>
    [KryptonPersist(true)]
    [Category(@"Visuals")]
    [Description(@"Gallery up button images.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteImagesGalleryButton Up { get; }

    #endregion

    #region Down
    /// <summary>
    /// Gallery down button images.
    /// </summary>
    [KryptonPersist(true)]
    [Category(@"Visuals")]
    [Description(@"Gallery down button images.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteImagesGalleryButton Down { get; }

    #endregion

    #region DropDown
    /// <summary>
    /// Gallery drop-down button images.
    /// </summary>
    [KryptonPersist(true)]
    [Category(@"Visuals")]
    [Description(@"Gallery drop-down button images.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteImagesGalleryButton DropDown { get; }

    #endregion
}