#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion


namespace Krypton.Toolkit;

/// <summary>Access to the integrated and quick access toolbar images.</summary>
/// ToDo: Get images from theme, when changed
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ToolBarImageStorage : Storage
{
    #region Static Fields

    private readonly Image _new = GenericToolbarImageResources.GenericNewDocument;
    private readonly Image _open = GenericToolbarImageResources.GenericOpenFolder;
    private readonly Image _save = GenericToolbarImageResources.GenericSave;
    private readonly Image _saveAs = GenericToolbarImageResources.GenericSaveAs;
    private readonly Image _saveAll = GenericToolbarImageResources.GenericSaveAll;
    private readonly Image _cut = GenericToolbarImageResources.GenericCut;
    private readonly Image _copy = GenericToolbarImageResources.GenericCopy;
    private readonly Image _paste = GenericToolbarImageResources.GenericPaste;
    private readonly Image _undo = GenericToolbarImageResources.GenericUndo;
    private readonly Image _redo = GenericToolbarImageResources.GenericRedo;
    private readonly Image _pageSetup = GenericToolbarImageResources.GenericPrintSetup;
    private readonly Image _printPreview = GenericToolbarImageResources.GenericPrintPreview;
    private readonly Image _print = GenericToolbarImageResources.GenericPrint;
    private readonly Image _quickPrint = GenericToolbarImageResources.GenericQuickPrint;

    #endregion

    #region Instance Fields

    private List<Image> _toolBarImages;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="ToolBarImageStorage" /> class.</summary>
    public ToolBarImageStorage()
    {
        _toolBarImages = new List<Image>();

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
    /// Gets a value indicating if all the strings are default values.
    /// </summary>
    /// <returns>True if all values are defaulted; otherwise false.</returns>
    [Browsable(false)]
    public override bool IsDefault => New.Equals(_new) &&
                                      Open.Equals(_open) &&
                                      Save.Equals(_save) &&
                                      SaveAs.Equals(_saveAs) &&
                                      SaveAll.Equals(_saveAll) &&
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
    public /*static*/ void AddImagesToArray(Image[] paletteToolBarImages)
    {
        foreach (var image in paletteToolBarImages)
        {
            _toolBarImages.Add(image);
        }
    }

    /// <summary>Returns the toolbar images.</summary>
    /// <returns>A collection of toolbar images.</returns>
    public List<Image> ReturnToolBarImages() => _toolBarImages;

    internal void SetToolBarImages(Image[] images)
    {
        AddImagesToArray(images);

        AssignImageValues(ReturnToolBarImages());
    }

    private void AssignImageValues(List<Image> imageCollection)
    {
        if (imageCollection.Count > 0)
        {
            Image[] toolBarImages = imageCollection.ToArray();

            New = toolBarImages[0];
            Open = toolBarImages[1];
            Save = toolBarImages[2];
            SaveAs = toolBarImages[3];
            SaveAll = toolBarImages[4];
            Cut = toolBarImages[5];
            Copy = toolBarImages[6];
            Paste = toolBarImages[7];
            Undo = toolBarImages[8];
            Redo = toolBarImages[9];
            PageSetup = toolBarImages[10];
            PrintPreview = toolBarImages[11];
            Print = toolBarImages[12];
            QuickPrint = toolBarImages[13];
        }
    }

    #endregion
}