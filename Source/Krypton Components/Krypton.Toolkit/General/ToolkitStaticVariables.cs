#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public class ToolkitStaticVariables
{
    // Before any toolbar image arrays load preserialized resources (see GitHub #3330).
    private static readonly int _preserializedResourceAssemblyResolveHook = KryptonPreserializedResourceAssemblyResolve.Register();

    /// <summary>The default UAC shield icon size</summary>
    public static IconSize DEFAULT_UAC_SHIELD_ICON_SIZE = IconSize.ExtraSmall;

    #region Arrays

    #region Images

    #region ToolBar

    #region Generic

    /// <summary>The generic toolbar images</summary>
    public static Image[] GenericToolBarImages =
    [
        GenericToolbarImageResources.GenericNewDocument,
        GenericToolbarImageResources.GenericOpenFolder,
        GenericToolbarImageResources.GenericSave,
        GenericToolbarImageResources.GenericSaveAs,
        GenericToolbarImageResources.GenericSaveAll,
        GenericToolbarImageResources.GenericCut,
        GenericToolbarImageResources.GenericCopy,
        GenericToolbarImageResources.GenericPaste,
        GenericToolbarImageResources.GenericUndo,
        GenericToolbarImageResources.GenericRedo,
        GenericToolbarImageResources.GenericPrintSetup,
        GenericToolbarImageResources.GenericPrintPreview,
        GenericToolbarImageResources.GenericPrint,
        GenericToolbarImageResources.GenericQuickPrint
    ];

    #endregion

    #region Microsoft 365

    /// <summary>The Microsoft 365 toolbar images</summary>
    public static Image[] Microsoft365ToolBarImages =
    [
        Office2019ToolbarImageResources.Office2019ToolbarNewNormal,
        Office2019ToolbarImageResources.Office2019ToolbarOpenNormal,
        Office2019ToolbarImageResources.Office2019ToolbarSaveNormal,
        Office2019ToolbarImageResources.Office2019ToolbarSaveAsNormal,
        Office2019ToolbarImageResources.Office2019ToolbarSaveAllNormal,
        Office2019ToolbarImageResources.Office2019ToolbarCutNormal,
        Office2019ToolbarImageResources.Office2019ToolbarCopyNormal,
        Office2019ToolbarImageResources.Office2019ToolbarPasteNormal,
        Office2019ToolbarImageResources.Office2019ToolbarUndoNormal,
        Office2019ToolbarImageResources.Office2019ToolbarRedoNormal,
        Office2019ToolbarImageResources.Office2019ToolbarPageSetupNormal,
        Office2019ToolbarImageResources.Office2019ToolbarPrintPreviewNormal,
        Office2019ToolbarImageResources.Office2019ToolbarPrintNormal,
        Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal
    ];

    #endregion

    #region Office 2003

    /// <summary>The Office 2003 toolbar images</summary>
    public static Image[] Office2003ToolBarImages =
    [
        Office2003ToolbarImageResources.Office2003ToolbarNewNormal,
        Office2003ToolbarImageResources.Office2003ToolbarOpenNormal,
        Office2003ToolbarImageResources.Office2003ToolbarSaveNormal,
        Office2007ToolbarImageResources.Office2007ToolbarSaveAsNormal,
        Office2003ToolbarImageResources.Office2003ToolbarSaveAllNormal,
        Office2003ToolbarImageResources.Office2003ToolbarCutNormal,
        Office2003ToolbarImageResources.Office2003ToolbarCopyNormal,
        Office2003ToolbarImageResources.Office2003ToolbarPasteNormal,
        Office2003ToolbarImageResources.Office2003ToolbarUndoNormal,
        Office2003ToolbarImageResources.Office2003ToolbarRedoNormal,
        Office2003ToolbarImageResources.Office2003ToolbarPageSetupNormal,
        Office2003ToolbarImageResources.Office2003ToolbarPrintPreviewNormal,
        Office2003ToolbarImageResources.Office2003ToolbarPrintNormal,
        GenericToolbarImageResources.GenericQuickPrint
    ];

    #endregion

    #region Office 2007

    /// <summary>The Office 2007 toolbar images</summary>
    public static Image[] Office2007ToolBarImages =
    [
        Office2007ToolbarImageResources.Office2007ToolbarNewNormal,
        Office2007ToolbarImageResources.Office2007ToolbarOpenNormal,
        Office2007ToolbarImageResources.Office2007ToolbarSaveNormal,
        Office2007ToolbarImageResources.Office2007ToolbarSaveAsNormal,
        Office2007ToolbarImageResources.Office2007ToolbarSaveAllNormal,
        Office2007ToolbarImageResources.Office2007ToolbarCutNormal,
        Office2007ToolbarImageResources.Office2007ToolbarCopyNormal,
        Office2007ToolbarImageResources.Office2007ToolbarPasteNormal,
        Office2007ToolbarImageResources.Office2007ToolbarUndoNormal,
        Office2007ToolbarImageResources.Office2007ToolbarRedoNormal,
        Office2007ToolbarImageResources.Office2007ToolbarPageSetupNormal,
        Office2007ToolbarImageResources.Office2007ToolbarPrintPreviewNormal,
        Office2007ToolbarImageResources.Office2007ToolbarPrintNormal,
        Office2007ToolbarImageResources.Office2007ToolbarQuickPrintNormal
    ];

    #endregion

    #region Office 2010

    /// <summary>The Office 2010 toolbar images</summary>
    public static Image[] Office2010ToolBarImages =
    [
        Office2010ToolbarImageResources.Office2010ToolbarNewNormal,
        Office2010ToolbarImageResources.Office2010ToolbarOpenNormal,
        Office2010ToolbarImageResources.Office2010ToolbarSaveNormal,
        Office2010ToolbarImageResources.Office2010ToolbarSaveAsNormal,
        Office2010ToolbarImageResources.Office2010ToolbarSaveAllNormal,
        Office2010ToolbarImageResources.Office2010ToolbarCutNormal,
        Office2010ToolbarImageResources.Office2010ToolbarCopyNormal,
        Office2010ToolbarImageResources.Office2010ToolbarPasteNormal,
        Office2010ToolbarImageResources.Office2010ToolbarUndoNormal,
        Office2010ToolbarImageResources.Office2010ToolbarRedoNormal,
        Office2010ToolbarImageResources.Office2010ToolbarPageSetupNormal,
        Office2010ToolbarImageResources.Office2010ToolbarPrintPreviewNormal,
        Office2010ToolbarImageResources.Office2010ToolbarPrintNormal,
        Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal
    ];

    #endregion

    #region Office 2013

    /// <summary>
    /// The Office 2013 toolbar images
    /// </summary>
    public static Image[] Office2013ToolBarImages =
    [
        Office2013ToolbarImageResources.Office2013ToolbarNewNormal,
        Office2013ToolbarImageResources.Office2013ToolbarOpenNormal,
        Office2013ToolbarImageResources.Office2013ToolbarSaveNormal,
        Office2013ToolbarImageResources.Office2013ToolbarSaveAsNormal,
        Office2013ToolbarImageResources.Office2013ToolbarSaveAllNormal,
        Office2013ToolbarImageResources.Office2013ToolbarCutNormal,
        Office2013ToolbarImageResources.Office2013ToolbarCopyNormal,
        Office2013ToolbarImageResources.Office2013ToolbarPasteNormal,
        Office2013ToolbarImageResources.Office2013ToolbarUndoNormal,
        Office2013ToolbarImageResources.Office2013ToolbarRedoNormal,
        Office2013ToolbarImageResources.Office2013ToolbarPageSetupNormal,
        Office2013ToolbarImageResources.Office2013ToolbarPrintPreviewNormal,
        Office2013ToolbarImageResources.Office2013ToolbarPrintNormal,
        Office2013ToolbarImageResources.Office2013ToolbarQuickPrintNormal
    ];

    #endregion

    #region Office 2016

    /// <summary>
    /// The Office 2016 toolbar images
    /// </summary>
    public static Image[] Office2016ToolBarImages =
    [
        Office2016ToolbarImageResources.Office2016ToolbarNewNormal,
        Office2016ToolbarImageResources.Office2016ToolbarOpenNormal,
        Office2016ToolbarImageResources.Office2016ToolbarSaveNormal,
        Office2016ToolbarImageResources.Office2016ToolbarSaveAsNormal,
        Office2016ToolbarImageResources.Office2016ToolbarSaveAllNormal,
        Office2016ToolbarImageResources.Office2016ToolbarCutNormal,
        Office2016ToolbarImageResources.Office2016ToolbarCopyNormal,
        Office2016ToolbarImageResources.Office2016ToolbarPasteNormal,
        Office2016ToolbarImageResources.Office2016ToolbarUndoNormal,
        Office2016ToolbarImageResources.Office2016ToolbarRedoNormal,
        Office2016ToolbarImageResources.Office2016ToolbarPageSetupNormal,
        Office2016ToolbarImageResources.Office2016ToolbarPrintPreviewNormal,
        Office2016ToolbarImageResources.Office2016ToolbarPrintNormal,
        Office2016ToolbarImageResources.Office2016ToolbarQuickPrintNormal
    ];

    #endregion

    #region Office 2019

    /// <summary>
    /// The Office 2019 toolbar images
    /// </summary>
    public static Image[] Office2019ToolBarImages =
    [
        Office2019ToolbarImageResources.Office2019ToolbarNewNormal,
        Office2019ToolbarImageResources.Office2019ToolbarOpenNormal,
        Office2019ToolbarImageResources.Office2019ToolbarSaveNormal,
        Office2019ToolbarImageResources.Office2019ToolbarSaveAsNormal,
        Office2019ToolbarImageResources.Office2019ToolbarSaveAllNormal,
        Office2019ToolbarImageResources.Office2019ToolbarCutNormal,
        Office2019ToolbarImageResources.Office2019ToolbarCopyNormal,
        Office2019ToolbarImageResources.Office2019ToolbarPasteNormal,
        Office2019ToolbarImageResources.Office2019ToolbarUndoNormal,
        Office2019ToolbarImageResources.Office2019ToolbarRedoNormal,
        Office2019ToolbarImageResources.Office2019ToolbarPageSetupNormal,
        Office2019ToolbarImageResources.Office2019ToolbarPrintPreviewNormal,
        Office2019ToolbarImageResources.Office2019ToolbarPrintNormal,
        Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal
    ];

    #endregion

    #region System

    /// <summary>
    /// The system toolbar images
    /// </summary>
    public static Image[] SystemToolBarImages =
    [
        SystemToolbarImageResources.SystemToolbarNewNormal,
        SystemToolbarImageResources.SystemToolbarOpenNormal,
        SystemToolbarImageResources.SystemToolbarSaveNormal,
        SystemToolbarImageResources.SystemToolbarSaveNormal,
        SystemToolbarImageResources.SystemToolbarSaveAllNormal,
        SystemToolbarImageResources.SystemToolbarCutNormal,
        SystemToolbarImageResources.SystemToolbarCopyNormal,
        SystemToolbarImageResources.SystemToolbarPasteNormal,
        SystemToolbarImageResources.SystemToolbarUndoNormal,
        SystemToolbarImageResources.SystemToolbarRedoNormal,
        SystemToolbarImageResources.SystemToolbarPageSetupNormal,
        SystemToolbarImageResources.SystemToolbarPrintPreviewNormal,
        SystemToolbarImageResources.SystemToolbarPrintNormal,
        GenericToolbarImageResources.GenericQuickPrint
    ];

    #endregion

    #region Visual Studio

    /// <summary>
    /// The Visual Studio toolbar images
    /// </summary>
    public static Image[] VisualStudioToolBarImages =
    [
        VisualStudioToolbarImageResources.VisualStudio2022ToolbarNewFile,
        VisualStudioToolbarImageResources.VisualStudio2022ToolbarOpen,
        VisualStudioToolbarImageResources.VisualStudio2022ToolbarSave,
        VisualStudioToolbarImageResources.VisualStudio2022ToolbarSaveAs,
        VisualStudioToolbarImageResources.VisualStudio2022ToolbarSaveAll,
        VisualStudioToolbarImageResources.VisualStudio2022ToolbarCut,
        VisualStudioToolbarImageResources.VisualStudio2022ToolbarCopy,
        VisualStudioToolbarImageResources.VisualStudio2022ToolbarPaste,
        VisualStudioToolbarImageResources.VisualStudio2022ToolbarUndo,
        VisualStudioToolbarImageResources.VisualStudio2022ToolbarRedo,
        VisualStudioToolbarImageResources.VisualStudio2022ToolbarPageSetup,
        VisualStudioToolbarImageResources.VisualStudio2022ToolbarPrintPreview,
        VisualStudioToolbarImageResources.VisualStudio2022ToolbarPrint,
        VisualStudioToolbarImageResources.VisualStudio2022ToolbarQuickPrint
    ];

    #endregion

    #endregion

    #endregion

    #endregion

    #region Properties
    /// <summary> 
    /// KryptonMessageBoxes that use the KRichtTextBox need another color for the text.<br/>
    /// Set the text colour to the one a non-input control uses.
    /// </summary>
    public static Color KryptonMessageBoxRichTextBoxTextColor
    {
        // per ticket #1692
        get => KryptonManager.CurrentGlobalPalette.GetContentLongTextColor1(PaletteContentStyle.LabelNormalPanel, PaletteState.Normal);
    }
    #endregion
}