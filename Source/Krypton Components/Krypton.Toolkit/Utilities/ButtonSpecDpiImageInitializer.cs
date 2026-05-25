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

