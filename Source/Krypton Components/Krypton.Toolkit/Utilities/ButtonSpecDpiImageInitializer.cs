#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Registers dedicated 2x/3x ButtonSpec artwork keyed by baseline ResourceFiles images (Issue #978).
/// </summary>
internal static class ButtonSpecDpiImageInitializer
{
    private static bool _initialized;

    /// <summary>
    /// Registers dedicated high-DPI sources. Safe to call multiple times.
    /// </summary>
    internal static void EnsureInitialized()
    {
        if (_initialized)
        {
            return;
        }

        _initialized = true;

        Register(ProfessionalButtonSpecResources.ProfessionalCloseButton,
            ButtonSpecDpiImageResources.ProfessionalCloseButton_2x,
            ButtonSpecDpiImageResources.ProfessionalCloseButton_3x);

        Register(GenericProfessionalImageResources.ProfessionalContextButton,
            ButtonSpecDpiImageResources.ProfessionalContextButton_2x,
            ButtonSpecDpiImageResources.ProfessionalContextButton_3x);
        Register(GenericProfessionalImageResources.ProfessionalNextButton,
            ButtonSpecDpiImageResources.ProfessionalNextButton_2x,
            ButtonSpecDpiImageResources.ProfessionalNextButton_3x);
        Register(GenericProfessionalImageResources.ProfessionalPreviousButton,
            ButtonSpecDpiImageResources.ProfessionalPreviousButton_2x,
            ButtonSpecDpiImageResources.ProfessionalPreviousButton_3x);
        Register(GenericProfessionalImageResources.ProfessionalArrowLeftButton,
            ButtonSpecDpiImageResources.ProfessionalArrowLeftButton_2x,
            ButtonSpecDpiImageResources.ProfessionalArrowLeftButton_3x);
        Register(GenericProfessionalImageResources.ProfessionalArrowRightButton,
            ButtonSpecDpiImageResources.ProfessionalArrowRightButton_2x,
            ButtonSpecDpiImageResources.ProfessionalArrowRightButton_3x);
        Register(GenericProfessionalImageResources.ProfessionalArrowUpButton,
            ButtonSpecDpiImageResources.ProfessionalArrowUpButton_2x,
            ButtonSpecDpiImageResources.ProfessionalArrowUpButton_3x);
        Register(GenericProfessionalImageResources.ProfessionalArrowDownButton,
            ButtonSpecDpiImageResources.ProfessionalArrowDownButton_2x,
            ButtonSpecDpiImageResources.ProfessionalArrowDownButton_3x);
        Register(GenericProfessionalImageResources.ProfessionalDropDownButton,
            ButtonSpecDpiImageResources.ProfessionalDropDownButton_2x,
            ButtonSpecDpiImageResources.ProfessionalDropDownButton_3x);
        Register(ProfessionalPinImageResources.ProfessionalPinVerticalButton,
            ButtonSpecDpiImageResources.ProfessionalPinVerticalButton_2x,
            ButtonSpecDpiImageResources.ProfessionalPinVerticalButton_3x);
        Register(ProfessionalPinImageResources.ProfessionalPinHorizontalButton,
            ButtonSpecDpiImageResources.ProfessionalPinHorizontalButton_2x,
            ButtonSpecDpiImageResources.ProfessionalPinHorizontalButton_3x);
        Register(ProfessionalControlBoxResources.ProfessionalMaximize,
            ButtonSpecDpiImageResources.ProfessionalMaximize_2x,
            ButtonSpecDpiImageResources.ProfessionalMaximize_3x);
        Register(GenericProfessionalImageResources.ProfessionalRestore,
            ButtonSpecDpiImageResources.ProfessionalRestore_2x,
            ButtonSpecDpiImageResources.ProfessionalRestore_3x);
        Register(Office2010MDIImageResources.Office2010ButtonMDIClose,
            ButtonSpecDpiImageResources.Office2010ButtonMDIClose_2x,
            ButtonSpecDpiImageResources.Office2010ButtonMDIClose_3x);
        Register(Office2010MDIImageResources.Office2010ButtonMDIMin,
            ButtonSpecDpiImageResources.Office2010ButtonMDIMin_2x,
            ButtonSpecDpiImageResources.Office2010ButtonMDIMin_3x);
        Register(Office2010MDIImageResources.Office2010ButtonMDIRestore,
            ButtonSpecDpiImageResources.Office2010ButtonMDIRestore_2x,
            ButtonSpecDpiImageResources.Office2010ButtonMDIRestore_3x);
        Register(RibbonArrowImageResources.RibbonUp2010,
            ButtonSpecDpiImageResources.RibbonUp2010_2x,
            ButtonSpecDpiImageResources.RibbonUp2010_3x);
        Register(RibbonArrowImageResources.RibbonDown2010,
            ButtonSpecDpiImageResources.RibbonDown2010_2x,
            ButtonSpecDpiImageResources.RibbonDown2010_3x);
        Register(Office2010MDIImageResources.Office2010ButtonMDICloseBlack,
            ButtonSpecDpiImageResources.Office2010ButtonMDICloseBlack_2x,
            ButtonSpecDpiImageResources.Office2010ButtonMDICloseBlack_3x);
        Register(Office2010MDIImageResources.Office2010ButtonMDIMinBlack,
            ButtonSpecDpiImageResources.Office2010ButtonMDIMinBlack_2x,
            ButtonSpecDpiImageResources.Office2010ButtonMDIMinBlack_3x);
        Register(Office2010MDIImageResources.Office2010ButtonMDIRestoreBlack,
            ButtonSpecDpiImageResources.Office2010ButtonMDIRestoreBlack_2x,
            ButtonSpecDpiImageResources.Office2010ButtonMDIRestoreBlack_3x);
        Register(RibbonArrowImageResources.RibbonUp2010Black,
            ButtonSpecDpiImageResources.RibbonUp2010Black_2x,
            ButtonSpecDpiImageResources.RibbonUp2010Black_3x);
        Register(RibbonArrowImageResources.RibbonDown2010Black,
            ButtonSpecDpiImageResources.RibbonDown2010Black_2x,
            ButtonSpecDpiImageResources.RibbonDown2010Black_3x);
        Register(GenericMDIImageResources.MdiClose,
            ButtonSpecDpiImageResources.MdiClose_2x,
            ButtonSpecDpiImageResources.MdiClose_3x);
        Register(GenericMDIImageResources.MdiMin,
            ButtonSpecDpiImageResources.MdiMin_2x,
            ButtonSpecDpiImageResources.MdiMin_3x);
        Register(GenericMDIImageResources.MdiRestore,
            ButtonSpecDpiImageResources.MdiRestore_2x,
            ButtonSpecDpiImageResources.MdiRestore_3x);
        Register(GenericMDIImageResources.MdiRibbonMinimize,
            ButtonSpecDpiImageResources.MdiRibbonMinimize_2x,
            ButtonSpecDpiImageResources.MdiRibbonMinimize_3x);
        Register(GenericMDIImageResources.MdiRibbonExpand,
            ButtonSpecDpiImageResources.MdiRibbonExpand_2x,
            ButtonSpecDpiImageResources.MdiRibbonExpand_3x);

        RegisterToolbarOffice2010();
        RegisterToolbarOffice2019();
        RegisterControlBoxOffice2010();
    }

    private static void RegisterControlBoxOffice2010()
    {
        Register(Office2010ControlBoxResources.Office2010SilverCloseNormal,
            ButtonSpecDpiImageResources.Office2010SilverCloseNormal_2x,
            ButtonSpecDpiImageResources.Office2010SilverCloseNormal_3x);
        Register(Office2010ControlBoxResources.Office2010SilverCloseActive,
            ButtonSpecDpiImageResources.Office2010SilverCloseActive_2x,
            ButtonSpecDpiImageResources.Office2010SilverCloseActive_3x);
        Register(Office2010ControlBoxResources.Office2010SilverCloseDisabled,
            ButtonSpecDpiImageResources.Office2010SilverCloseDisabled_2x,
            ButtonSpecDpiImageResources.Office2010SilverCloseDisabled_3x);
        Register(Office2010ControlBoxResources.Office2010SilverClosePressed,
            ButtonSpecDpiImageResources.Office2010SilverClosePressed_2x,
            ButtonSpecDpiImageResources.Office2010SilverClosePressed_3x);
        Register(Office2010ControlBoxResources.Office2010SilverMinimiseNormal,
            ButtonSpecDpiImageResources.Office2010SilverMinimiseNormal_2x,
            ButtonSpecDpiImageResources.Office2010SilverMinimiseNormal_3x);
        Register(Office2010ControlBoxResources.Office2010SilverMinimiseActive,
            ButtonSpecDpiImageResources.Office2010SilverMinimiseActive_2x,
            ButtonSpecDpiImageResources.Office2010SilverMinimiseActive_3x);
        Register(Office2010ControlBoxResources.Office2010SilverMinimiseDisabled,
            ButtonSpecDpiImageResources.Office2010SilverMinimiseDisabled_2x,
            ButtonSpecDpiImageResources.Office2010SilverMinimiseDisabled_3x);
        Register(Office2010ControlBoxResources.Office2010SilverMinimisePressed,
            ButtonSpecDpiImageResources.Office2010SilverMinimisePressed_2x,
            ButtonSpecDpiImageResources.Office2010SilverMinimisePressed_3x);
        Register(Office2010ControlBoxResources.Office2010SilverMaximiseNormal,
            ButtonSpecDpiImageResources.Office2010SilverMaximiseNormal_2x,
            ButtonSpecDpiImageResources.Office2010SilverMaximiseNormal_3x);
        Register(Office2010ControlBoxResources.Office2010SilverMaximiseActive,
            ButtonSpecDpiImageResources.Office2010SilverMaximiseActive_2x,
            ButtonSpecDpiImageResources.Office2010SilverMaximiseActive_3x);
        Register(Office2010ControlBoxResources.Office2010SilverMaximiseDisabled,
            ButtonSpecDpiImageResources.Office2010SilverMaximiseDisabled_2x,
            ButtonSpecDpiImageResources.Office2010SilverMaximiseDisabled_3x);
        Register(Office2010ControlBoxResources.Office2010SilverMaximisePressed,
            ButtonSpecDpiImageResources.Office2010SilverMaximisePressed_2x,
            ButtonSpecDpiImageResources.Office2010SilverMaximisePressed_3x);
        Register(Office2010ControlBoxResources.Office2010SilverRestoreNormal,
            ButtonSpecDpiImageResources.Office2010SilverRestoreNormal_2x,
            ButtonSpecDpiImageResources.Office2010SilverRestoreNormal_3x);
        Register(Office2010ControlBoxResources.Office2010SilverRestoreActive,
            ButtonSpecDpiImageResources.Office2010SilverRestoreActive_2x,
            ButtonSpecDpiImageResources.Office2010SilverRestoreActive_3x);
        Register(Office2010ControlBoxResources.Office2010SilverRestoreDisabled,
            ButtonSpecDpiImageResources.Office2010SilverRestoreDisabled_2x,
            ButtonSpecDpiImageResources.Office2010SilverRestoreDisabled_3x);
        Register(Office2010ControlBoxResources.Office2010SilverRestorePressed,
            ButtonSpecDpiImageResources.Office2010SilverRestorePressed_2x,
            ButtonSpecDpiImageResources.Office2010SilverRestorePressed_3x);
        Register(Office2010ControlBoxResources.Office2010HelpIconNormal,
            ButtonSpecDpiImageResources.Office2010HelpIconNormal_2x,
            ButtonSpecDpiImageResources.Office2010HelpIconNormal_3x);
        Register(Office2010ControlBoxResources.Office2010HelpIconHover,
            ButtonSpecDpiImageResources.Office2010HelpIconHover_2x,
            ButtonSpecDpiImageResources.Office2010HelpIconHover_3x);
        Register(Office2010ControlBoxResources.Office2010HelpIconDisabled,
            ButtonSpecDpiImageResources.Office2010HelpIconDisabled_2x,
            ButtonSpecDpiImageResources.Office2010HelpIconDisabled_3x);
        Register(Office2010ControlBoxResources.Office2010HelpIconPressed,
            ButtonSpecDpiImageResources.Office2010HelpIconPressed_2x,
            ButtonSpecDpiImageResources.Office2010HelpIconPressed_3x);

        Register(Office2010ControlBoxResources.Office2010BlackCloseNormal,
            ButtonSpecDpiImageResources.Office2010BlackCloseNormal_2x,
            ButtonSpecDpiImageResources.Office2010BlackCloseNormal_3x);
        Register(Office2010ControlBoxResources.Office2010BlackCloseActive,
            ButtonSpecDpiImageResources.Office2010BlackCloseActive_2x,
            ButtonSpecDpiImageResources.Office2010BlackCloseActive_3x);
        Register(Office2010ControlBoxResources.Office2010BlackCloseDisabled,
            ButtonSpecDpiImageResources.Office2010BlackCloseDisabled_2x,
            ButtonSpecDpiImageResources.Office2010BlackCloseDisabled_3x);
        Register(Office2010ControlBoxResources.Office2010BlackClosePressed,
            ButtonSpecDpiImageResources.Office2010BlackClosePressed_2x,
            ButtonSpecDpiImageResources.Office2010BlackClosePressed_3x);
        Register(Office2010ControlBoxResources.Office2010BlackMinimiseNormal,
            ButtonSpecDpiImageResources.Office2010BlackMinimiseNormal_2x,
            ButtonSpecDpiImageResources.Office2010BlackMinimiseNormal_3x);
        Register(Office2010ControlBoxResources.Office2010BlackMinimiseActive,
            ButtonSpecDpiImageResources.Office2010BlackMinimiseActive_2x,
            ButtonSpecDpiImageResources.Office2010BlackMinimiseActive_3x);
        Register(Office2010ControlBoxResources.Office2010BlackMinimiseDisabled,
            ButtonSpecDpiImageResources.Office2010BlackMinimiseDisabled_2x,
            ButtonSpecDpiImageResources.Office2010BlackMinimiseDisabled_3x);
        Register(Office2010ControlBoxResources.Office2010BlackMinimisePressed,
            ButtonSpecDpiImageResources.Office2010BlackMinimisePressed_2x,
            ButtonSpecDpiImageResources.Office2010BlackMinimisePressed_3x);
        Register(Office2010ControlBoxResources.Office2010BackMaximiseNormal,
            ButtonSpecDpiImageResources.Office2010BlackMaximiseNormal_2x,
            ButtonSpecDpiImageResources.Office2010BlackMaximiseNormal_3x);
        Register(Office2010ControlBoxResources.Office2010BlackMaximiseActive,
            ButtonSpecDpiImageResources.Office2010BlackMaximiseActive_2x,
            ButtonSpecDpiImageResources.Office2010BlackMaximiseActive_3x);
        Register(Office2010ControlBoxResources.Office2010BlackMaximiseDisabled,
            ButtonSpecDpiImageResources.Office2010BlackMaximiseDisabled_2x,
            ButtonSpecDpiImageResources.Office2010BlackMaximiseDisabled_3x);
        Register(Office2010ControlBoxResources.Office2010BlackMaximisePressed,
            ButtonSpecDpiImageResources.Office2010BlackMaximisePressed_2x,
            ButtonSpecDpiImageResources.Office2010BlackMaximisePressed_3x);
        Register(Office2010ControlBoxResources.Office2010BlackRestoreNormal,
            ButtonSpecDpiImageResources.Office2010BlackRestoreNormal_2x,
            ButtonSpecDpiImageResources.Office2010BlackRestoreNormal_3x);
        Register(Office2010ControlBoxResources.Office2010BlackRestoreActive,
            ButtonSpecDpiImageResources.Office2010BlackRestoreActive_2x,
            ButtonSpecDpiImageResources.Office2010BlackRestoreActive_3x);
        Register(Office2010ControlBoxResources.Office2010BlackRestoreDisabled,
            ButtonSpecDpiImageResources.Office2010BlackRestoreDisabled_2x,
            ButtonSpecDpiImageResources.Office2010BlackRestoreDisabled_3x);
        Register(Office2010ControlBoxResources.Office2010BlackRestorePressed,
            ButtonSpecDpiImageResources.Office2010BlackRestorePressed_2x,
            ButtonSpecDpiImageResources.Office2010BlackRestorePressed_3x);
    }

    private static void RegisterToolbarOffice2010()
    {
        Register(Office2010ToolbarImageResources.Office2010ToolbarNewNormal,
            ButtonSpecDpiImageResources.Office2010ToolbarNewNormal_2x,
            ButtonSpecDpiImageResources.Office2010ToolbarNewNormal_3x);
        Register(Office2010ToolbarImageResources.Office2010ToolbarOpenNormal,
            ButtonSpecDpiImageResources.Office2010ToolbarOpenNormal_2x,
            ButtonSpecDpiImageResources.Office2010ToolbarOpenNormal_3x);
        Register(Office2010ToolbarImageResources.Office2010ToolbarSaveNormal,
            ButtonSpecDpiImageResources.Office2010ToolbarSaveNormal_2x,
            ButtonSpecDpiImageResources.Office2010ToolbarSaveNormal_3x);
        Register(Office2010ToolbarImageResources.Office2010ToolbarSaveAsNormal,
            ButtonSpecDpiImageResources.Office2010ToolbarSaveAsNormal_2x,
            ButtonSpecDpiImageResources.Office2010ToolbarSaveAsNormal_3x);
        Register(Office2010ToolbarImageResources.Office2010ToolbarSaveAllNormal,
            ButtonSpecDpiImageResources.Office2010ToolbarSaveAllNormal_2x,
            ButtonSpecDpiImageResources.Office2010ToolbarSaveAllNormal_3x);
        Register(Office2010ToolbarImageResources.Office2010ToolbarCutNormal,
            ButtonSpecDpiImageResources.Office2010ToolbarCutNormal_2x,
            ButtonSpecDpiImageResources.Office2010ToolbarCutNormal_3x);
        Register(Office2010ToolbarImageResources.Office2010ToolbarCopyNormal,
            ButtonSpecDpiImageResources.Office2010ToolbarCopyNormal_2x,
            ButtonSpecDpiImageResources.Office2010ToolbarCopyNormal_3x);
        Register(Office2010ToolbarImageResources.Office2010ToolbarPasteNormal,
            ButtonSpecDpiImageResources.Office2010ToolbarPasteNormal_2x,
            ButtonSpecDpiImageResources.Office2010ToolbarPasteNormal_3x);
        Register(Office2010ToolbarImageResources.Office2010ToolbarUndoNormal,
            ButtonSpecDpiImageResources.Office2010ToolbarUndoNormal_2x,
            ButtonSpecDpiImageResources.Office2010ToolbarUndoNormal_3x);
        Register(Office2010ToolbarImageResources.Office2010ToolbarRedoNormal,
            ButtonSpecDpiImageResources.Office2010ToolbarRedoNormal_2x,
            ButtonSpecDpiImageResources.Office2010ToolbarRedoNormal_3x);
        Register(Office2010ToolbarImageResources.Office2010ToolbarPageSetupNormal,
            ButtonSpecDpiImageResources.Office2010ToolbarPageSetupNormal_2x,
            ButtonSpecDpiImageResources.Office2010ToolbarPageSetupNormal_3x);
        Register(Office2010ToolbarImageResources.Office2010ToolbarPrintPreviewNormal,
            ButtonSpecDpiImageResources.Office2010ToolbarPrintPreviewNormal_2x,
            ButtonSpecDpiImageResources.Office2010ToolbarPrintPreviewNormal_3x);
        Register(Office2010ToolbarImageResources.Office2010ToolbarPrintNormal,
            ButtonSpecDpiImageResources.Office2010ToolbarPrintNormal_2x,
            ButtonSpecDpiImageResources.Office2010ToolbarPrintNormal_3x);
        Register(Office2010ToolbarImageResources.Office2010ToolbarQuickPrintNormal,
            ButtonSpecDpiImageResources.Office2010ToolbarQuickPrintNormal_2x,
            ButtonSpecDpiImageResources.Office2010ToolbarQuickPrintNormal_3x);
    }

    private static void RegisterToolbarOffice2019()
    {
        Register(Office2019ToolbarImageResources.Office2019ToolbarNewNormal,
            ButtonSpecDpiImageResources.Office2019ToolbarNewNormal_2x,
            ButtonSpecDpiImageResources.Office2019ToolbarNewNormal_3x);
        Register(Office2019ToolbarImageResources.Office2019ToolbarOpenNormal,
            ButtonSpecDpiImageResources.Office2019ToolbarOpenNormal_2x,
            ButtonSpecDpiImageResources.Office2019ToolbarOpenNormal_3x);
        Register(Office2019ToolbarImageResources.Office2019ToolbarSaveNormal,
            ButtonSpecDpiImageResources.Office2019ToolbarSaveNormal_2x,
            ButtonSpecDpiImageResources.Office2019ToolbarSaveNormal_3x);
        Register(Office2019ToolbarImageResources.Office2019ToolbarSaveAsNormal,
            ButtonSpecDpiImageResources.Office2019ToolbarSaveAsNormal_2x,
            ButtonSpecDpiImageResources.Office2019ToolbarSaveAsNormal_3x);
        Register(Office2019ToolbarImageResources.Office2019ToolbarSaveAllNormal,
            ButtonSpecDpiImageResources.Office2019ToolbarSaveAllNormal_2x,
            ButtonSpecDpiImageResources.Office2019ToolbarSaveAllNormal_3x);
        Register(Office2019ToolbarImageResources.Office2019ToolbarCutNormal,
            ButtonSpecDpiImageResources.Office2019ToolbarCutNormal_2x,
            ButtonSpecDpiImageResources.Office2019ToolbarCutNormal_3x);
        Register(Office2019ToolbarImageResources.Office2019ToolbarCopyNormal,
            ButtonSpecDpiImageResources.Office2019ToolbarCopyNormal_2x,
            ButtonSpecDpiImageResources.Office2019ToolbarCopyNormal_3x);
        Register(Office2019ToolbarImageResources.Office2019ToolbarPasteNormal,
            ButtonSpecDpiImageResources.Office2019ToolbarPasteNormal_2x,
            ButtonSpecDpiImageResources.Office2019ToolbarPasteNormal_3x);
        Register(Office2019ToolbarImageResources.Office2019ToolbarUndoNormal,
            ButtonSpecDpiImageResources.Office2019ToolbarUndoNormal_2x,
            ButtonSpecDpiImageResources.Office2019ToolbarUndoNormal_3x);
        Register(Office2019ToolbarImageResources.Office2019ToolbarRedoNormal,
            ButtonSpecDpiImageResources.Office2019ToolbarRedoNormal_2x,
            ButtonSpecDpiImageResources.Office2019ToolbarRedoNormal_3x);
        Register(Office2019ToolbarImageResources.Office2019ToolbarPageSetupNormal,
            ButtonSpecDpiImageResources.Office2019ToolbarPageSetupNormal_2x,
            ButtonSpecDpiImageResources.Office2019ToolbarPageSetupNormal_3x);
        Register(Office2019ToolbarImageResources.Office2019ToolbarPrintPreviewNormal,
            ButtonSpecDpiImageResources.Office2019ToolbarPrintPreviewNormal_2x,
            ButtonSpecDpiImageResources.Office2019ToolbarPrintPreviewNormal_3x);
        Register(Office2019ToolbarImageResources.Office2019ToolbarPrintNormal,
            ButtonSpecDpiImageResources.Office2019ToolbarPrintNormal_2x,
            ButtonSpecDpiImageResources.Office2019ToolbarPrintNormal_3x);
        Register(Office2019ToolbarImageResources.Office2019ToolbarQuickPrintNormal,
            ButtonSpecDpiImageResources.Office2019ToolbarQuickPrintNormal_2x,
            ButtonSpecDpiImageResources.Office2019ToolbarQuickPrintNormal_3x);
    }

    private static void Register(Image baseline, Image? scale2x, Image? scale3x)
    {
        if (scale2x != null)
        {
            ButtonSpecDpiImageRegistry.RegisterScale2x(baseline, scale2x);
        }

        if (scale3x != null)
        {
            ButtonSpecDpiImageRegistry.RegisterScale3x(baseline, scale3x);
        }
    }
}

