#region BSD License
/*
 *  
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable InconsistentNaming
namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class IntegratedToolBarValues : GlobalId
{
    #region Static Fields

    private const bool DEFAULT_SHOW_INTEGRATED_TOOLBAR = false;

    private const bool DEFAULT_SHOW_ALL_TOOLBAR_ITEMS = false;

    private const int MAXIMUM_INTEGRATED_TOOLBAR_ITEMS = 14;

    private const string DEFAULT_BUTTON_ORIENTATION = @"PaletteButtonOrientation.Auto";

    private const string DEFAULT_BUTTON_ALIGNMENT = @"PaletteRelativeEdgeAlign.Near";

    #endregion

    #region Instance Fields

    private bool _flipArrayItems;

    private bool _showIntegratedToolBar;

    private bool _showAllToolbarButtons;

    private ButtonSpecAny[] _integratedToolBarItems;

    private PaletteButtonOrientation _buttonOrientation;

    private PaletteRelativeEdgeAlign _buttonAlignment;

    private IntegratedToolbarManager _integratedToolbarManager;

    #endregion

    #region Public

    [Category(@"Visuals")]
    [Description(@"")]
    [DefaultValue(DEFAULT_SHOW_INTEGRATED_TOOLBAR)]
    public bool ShowIntegratedToolBar { get => _showIntegratedToolBar; set { _showIntegratedToolBar = value; ShowToolBar(value); } }

    [Category(@"Visuals")]
    [Description(@"")]
    [DefaultValue(DEFAULT_SHOW_ALL_TOOLBAR_ITEMS)]
    public bool ShowAllToolbarItems { get => _showAllToolbarButtons; set => _showAllToolbarButtons = value;
    }

    public ButtonSpecAny[] IntegratedToolBarItems { get => _integratedToolBarItems; private set => _integratedToolBarItems = value; }

    [Category(@"Visuals")]
    [Description(@"")]
    [DefaultValue(typeof(PaletteButtonOrientation), DEFAULT_BUTTON_ORIENTATION)]
    public PaletteButtonOrientation ButtonOrientation { get => _buttonOrientation; set => _buttonOrientation = value;
    }

    [Category(@"Visuals")]
    [Description(@"")]
    [DefaultValue(typeof(PaletteRelativeEdgeAlign), DEFAULT_BUTTON_ALIGNMENT)]
    public PaletteRelativeEdgeAlign ButtonAlignment { get => _buttonAlignment; set => _buttonAlignment = value;
    }

    #endregion

    #region Identity

    public IntegratedToolBarValues()
    {
        Reset();
    }

    public override string ToString()
    {
        return base.ToString() is string s
            ? s
            : GlobalStaticValues.DEFAULT_EMPTY_STRING;
    }

    #endregion

    #region Implementation

    [Browsable(false)]
    public bool IsDefault => FlipArrayItems.Equals(false) &&
                             ShowIntegratedToolBar.Equals(DEFAULT_SHOW_INTEGRATED_TOOLBAR) &&
                             ShowAllToolbarItems.Equals(DEFAULT_SHOW_ALL_TOOLBAR_ITEMS) &&
                             IntegratedToolBarItems.Equals(SetupToolbarArray()) &&
                             ButtonOrientation.Equals(PaletteButtonOrientation.Auto) &&
                             ButtonAlignment.Equals(PaletteRelativeEdgeAlign.Near);

    public void Reset()
    {
        ShowIntegratedToolBar = DEFAULT_SHOW_INTEGRATED_TOOLBAR;

        ShowAllToolbarItems = DEFAULT_SHOW_ALL_TOOLBAR_ITEMS;

        IntegratedToolBarItems = SetupToolbarArray();

        ButtonOrientation = PaletteButtonOrientation.Auto;

        ButtonAlignment = PaletteRelativeEdgeAlign.Near;

        _integratedToolbarManager = new IntegratedToolbarManager();
    }

    private ButtonSpecAny[] SetupToolbarArray()
    {
        ButtonSpecAny[] buttons = new ButtonSpecAny[MAXIMUM_INTEGRATED_TOOLBAR_ITEMS];

        ButtonSpecAny newToolbarButton = new(),
            openToolbarButton = new(),
            saveToolbarButton = new(),
            saveAsToolbarButton = new(),
            saveAllToolbarButton = new(),
            cutToolbarButton = new(),
            copyToolbarButton = new(),
            pasteToolbarButton = new(),
            undoToolbarButton = new(),
            redoToolbarButton = new(),
            pageSetupToolbarButton = new(),
            printPreviewToolbarButton = new(),
            printToolbarButton = new(),
            quickPrintToolbarButton = new();

        newToolbarButton.Type = PaletteButtonSpecStyle.New;

        openToolbarButton.Type = PaletteButtonSpecStyle.Open;

        saveAsToolbarButton.Type = PaletteButtonSpecStyle.SaveAs;

        saveAllToolbarButton.Type = PaletteButtonSpecStyle.SaveAll;

        saveToolbarButton.Type = PaletteButtonSpecStyle.Save;

        cutToolbarButton.Type = PaletteButtonSpecStyle.Cut;

        copyToolbarButton.Type = PaletteButtonSpecStyle.Copy;

        pasteToolbarButton.Type = PaletteButtonSpecStyle.Paste;

        undoToolbarButton.Type = PaletteButtonSpecStyle.Undo;

        redoToolbarButton.Type = PaletteButtonSpecStyle.Redo;

        pageSetupToolbarButton.Type = PaletteButtonSpecStyle.PageSetup;

        printPreviewToolbarButton.Type = PaletteButtonSpecStyle.PrintPreview;

        printToolbarButton.Type = PaletteButtonSpecStyle.Print;

        quickPrintToolbarButton.Type = PaletteButtonSpecStyle.QuickPrint;

        buttons[0] = newToolbarButton;

        buttons[1] = openToolbarButton;

        buttons[2] = saveToolbarButton;

        buttons[3] = saveAsToolbarButton;

        buttons[4] = saveAllToolbarButton;

        buttons[5] = cutToolbarButton;

        buttons[6] = copyToolbarButton;

        buttons[7] = pasteToolbarButton;

        buttons[8] = undoToolbarButton;

        buttons[9] = redoToolbarButton;

        buttons[10] = pageSetupToolbarButton;

        buttons[11] = printPreviewToolbarButton;

        buttons[12] = printToolbarButton;

        buttons[13] = quickPrintToolbarButton;

        return buttons;
    }

    private void ShowToolBar(bool showToolBar) => _integratedToolbarManager.ShowToolBar(showToolBar);

    internal void SetupToolBar() => _integratedToolBarItems = SetupToolbarArray();

    public bool FlipArrayItems { get => _flipArrayItems; set => _flipArrayItems = value; }

    public ButtonSpecAny[] ReturnToolBarButtonArray() => _integratedToolBarItems;

    //private void SetupIntegratedToolBar(bool value, KryptonForm? owner)
    //{
    //    throw new NotImplementedException();
    //}

    #endregion
}