#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion


namespace Krypton.Toolkit;

/// <summary>Access to the integrated and quick access toolbar images.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ToolBarImageStorage : Storage
{
    #region Instance Fields

    // Theme baselines; updated when KryptonManager pushes a new toolbar image pack.
    private Image _new = GenericToolbarImageResources.GenericNewDocument;
    private Image _open = GenericToolbarImageResources.GenericOpenFolder;
    private Image _save = GenericToolbarImageResources.GenericSave;
    private Image _saveAs = GenericToolbarImageResources.GenericSaveAs;
    private Image _saveAll = GenericToolbarImageResources.GenericSaveAll;
    private Image _cut = GenericToolbarImageResources.GenericCut;
    private Image _copy = GenericToolbarImageResources.GenericCopy;
    private Image _paste = GenericToolbarImageResources.GenericPaste;
    private Image _undo = GenericToolbarImageResources.GenericUndo;
    private Image _redo = GenericToolbarImageResources.GenericRedo;
    private Image _pageSetup = GenericToolbarImageResources.GenericPrintSetup;
    private Image _printPreview = GenericToolbarImageResources.GenericPrintPreview;
    private Image _print = GenericToolbarImageResources.GenericPrint;
    private Image _quickPrint = GenericToolbarImageResources.GenericQuickPrint;

    private readonly List<Image> _toolBarImages;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="ToolBarImageStorage" /> class.</summary>
    public ToolBarImageStorage()
    {
        _toolBarImages = new List<Image>(GlobalStaticConstants.REQUIRED_TOOLBAR_IMAGE_COUNT);

        Reset();
    }

    #endregion

    #region Public

    public Image New { get; set; }

    public Image Open { get; set; }

    public Image Save { get; set; }

    public Image SaveAs { get; set; }

    public Image SaveAll { get; set; }

    public Image Cut { get; set; }

    public Image Copy { get; set; }

    public Image Paste { get; set; }

    public Image Undo { get; set; }

    public Image Redo { get; set; }

    public Image PageSetup { get; set; }

    public Image PrintPreview { get; set; }

    public Image Print { get; set; }

    public Image QuickPrint { get; set; }

    #endregion

    #region IsDefault

    /// <summary>
    /// Gets a value indicating if all the images are default values for the current theme pack.
    /// </summary>
    /// <returns>True if all values are defaulted; otherwise false.</returns>
    [Browsable(false)]
    public override bool IsDefault => New.Equals(_new) &&
                                      Open.Equals(_open) &&
                                      Save.Equals(_save) &&
                                      SaveAs.Equals(_saveAs) &&
                                      SaveAll.Equals(_saveAll) &&
                                      Cut.Equals(_cut) &&
                                      Copy.Equals(_copy) &&
                                      Paste.Equals(_paste) &&
                                      Undo.Equals(_undo) &&
                                      Redo.Equals(_redo) &&
                                      PageSetup.Equals(_pageSetup) &&
                                      PrintPreview.Equals(_printPreview) &&
                                      Print.Equals(_print) &&
                                      QuickPrint.Equals(_quickPrint);

    #endregion

    #region Implementation

    public void Reset()
    {
        New = _new;
        Open = _open;
        Save = _save;
        SaveAs = _saveAs;
        SaveAll = _saveAll;
        Cut = _cut;
        Copy = _copy;
        Paste = _paste;
        Undo = _undo;
        Redo = _redo;
        PageSetup = _pageSetup;
        PrintPreview = _printPreview;
        Print = _print;
        QuickPrint = _quickPrint;
    }

    /// <summary>Adds the palette toolbar images to an array.</summary>
    /// <param name="paletteToolBarImages">The palette toolbar images.</param>
    public void AddImagesToArray(Image[] paletteToolBarImages)
    {
        foreach (var image in paletteToolBarImages)
        {
            _toolBarImages.Add(image);
        }
    }

    /// <summary>Returns the toolbar images.</summary>
    /// <returns>A collection of toolbar images.</returns>
    public List<Image> ReturnToolBarImages() => _toolBarImages;

    /// <summary>
    /// Replaces the stored toolbar images with a theme pack and updates public properties.
    /// </summary>
    /// <param name="images">Theme toolbar images in fixed slot order (14 images).</param>
    internal void SetToolBarImages(Image[]? images)
    {
        if (images == null || images.Length < GlobalStaticConstants.REQUIRED_TOOLBAR_IMAGE_COUNT)
        {
            return;
        }

        // Replace, do not append — previous theme packs must not remain at indices 0..13.
        _toolBarImages.Clear();
        for (int i = 0; i < GlobalStaticConstants.REQUIRED_TOOLBAR_IMAGE_COUNT; i++)
        {
            _toolBarImages.Add(images[i]);
        }

        UpdateThemeBaselines(images);
        AssignImageValues(_toolBarImages);
    }

    private void UpdateThemeBaselines(Image[] images)
    {
        _new = images[0];
        _open = images[1];
        _save = images[2];
        _saveAs = images[3];
        _saveAll = images[4];
        _cut = images[5];
        _copy = images[6];
        _paste = images[7];
        _undo = images[8];
        _redo = images[9];
        _pageSetup = images[10];
        _printPreview = images[11];
        _print = images[12];
        _quickPrint = images[13];
    }

    private void AssignImageValues(List<Image> imageCollection)
    {
        if (imageCollection.Count < GlobalStaticConstants.REQUIRED_TOOLBAR_IMAGE_COUNT)
        {
            return;
        }

        New = imageCollection[0];
        Open = imageCollection[1];
        Save = imageCollection[2];
        SaveAs = imageCollection[3];
        SaveAll = imageCollection[4];
        Cut = imageCollection[5];
        Copy = imageCollection[6];
        Paste = imageCollection[7];
        Undo = imageCollection[8];
        Redo = imageCollection[9];
        PageSetup = imageCollection[10];
        PrintPreview = imageCollection[11];
        Print = imageCollection[12];
        QuickPrint = imageCollection[13];
    }

    #endregion
}
