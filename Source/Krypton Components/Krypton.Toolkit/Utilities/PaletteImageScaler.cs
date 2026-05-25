#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Helper class for high DPI.
/// 
/// How to use? Simply put ' PaletteImageScaler.ScalePalette(this, PALETTENAME);' in your initialization, and put the following method in your code:
/// 
/// 'private void ChangePalette(PaletteMode palMode)
///  {
///      PALETTENAME.SuspendUpdates();
///      PALETTENAME.BasePaletteMode = palMode;
///      PaletteImageScaler.ScalePalette(this, PALETTENAME);
///      PALETTENAME.ResumeUpdates();
///  }'
///
/// Use the above mentioned method on the 'Click' event of the control.
/// </summary>
public static class PaletteImageScaler
{
    /// <summary>
    /// scales the custom KryptonPalette images using the current Dpi
    /// </summary>
    /// <param name="factorDpiX">multiplier from dpi of 96 X</param>
    /// <param name="factorDpiY">multiplier from dpi of 96 Y</param>
    /// <param name="pal">KryptonPalette</param>
    public static void ScalePalette(float factorDpiX, float factorDpiY, KryptonCustomPaletteBase? pal)
    {
        if (pal == null
            //|| pal.HasAlreadyBeenScaled
           )
        {
            return;
        }

        //pal.HasAlreadyBeenScaled = true;

        var scaleFactor = new SizeF(factorDpiX, factorDpiY);

        // suspend palette updates
        pal.SuspendUpdates();

        // scale buttonspec images
        KryptonPaletteButtonSpecs bs = pal.ButtonSpecs;
        bs.PopulateFromBase(); // populate images first so we can scale them
        ScaleButtonSpecImageType(bs.ArrowDown, scaleFactor, PaletteButtonSpecStyle.ArrowDown);
        ScaleButtonSpecImageType(bs.ArrowLeft, scaleFactor, PaletteButtonSpecStyle.ArrowLeft);
        ScaleButtonSpecImageType(bs.ArrowRight, scaleFactor, PaletteButtonSpecStyle.ArrowRight);
        ScaleButtonSpecImageType(bs.ArrowUp, scaleFactor, PaletteButtonSpecStyle.ArrowUp);
        ScaleButtonSpecImageType(bs.Close, scaleFactor, PaletteButtonSpecStyle.Close);
        ScaleButtonSpecImageType(bs.Common, scaleFactor, PaletteButtonSpecStyle.Generic);
        ScaleButtonSpecImageType(bs.Context, scaleFactor, PaletteButtonSpecStyle.Context);
        ScaleButtonSpecImageType(bs.DropDown, scaleFactor, PaletteButtonSpecStyle.DropDown);
        ScaleButtonSpecImageType(bs.FormClose, scaleFactor, PaletteButtonSpecStyle.FormClose);
        ScaleButtonSpecImageType(bs.FormMax, scaleFactor, PaletteButtonSpecStyle.FormMax);
        ScaleButtonSpecImageType(bs.FormMin, scaleFactor, PaletteButtonSpecStyle.FormMin);
        ScaleButtonSpecImageType(bs.FormRestore, scaleFactor, PaletteButtonSpecStyle.FormRestore);
        ScaleButtonSpecImageType(bs.FormHelp, scaleFactor, PaletteButtonSpecStyle.FormHelp);
        ScaleButtonSpecImageType(bs.Generic, scaleFactor, PaletteButtonSpecStyle.Generic);
        ScaleButtonSpecImageType(bs.Next, scaleFactor, PaletteButtonSpecStyle.Next);
        ScaleButtonSpecImageType(bs.PendantClose, scaleFactor, PaletteButtonSpecStyle.PendantClose);
        ScaleButtonSpecImageType(bs.PendantMin, scaleFactor, PaletteButtonSpecStyle.PendantMin);
        ScaleButtonSpecImageType(bs.PendantRestore, scaleFactor, PaletteButtonSpecStyle.PendantRestore);
        ScaleButtonSpecImageType(bs.PinHorizontal, scaleFactor, PaletteButtonSpecStyle.PinHorizontal);
        ScaleButtonSpecImageType(bs.PinVertical, scaleFactor, PaletteButtonSpecStyle.PinVertical);
        ScaleButtonSpecImageType(bs.Previous, scaleFactor, PaletteButtonSpecStyle.Previous);
        ScaleButtonSpecImageType(bs.RibbonExpand, scaleFactor, PaletteButtonSpecStyle.RibbonExpand);
        ScaleButtonSpecImageType(bs.RibbonMinimize, scaleFactor, PaletteButtonSpecStyle.RibbonMinimize);
        ScaleButtonSpecImageType(bs.WorkspaceMaximize, scaleFactor, PaletteButtonSpecStyle.WorkspaceMaximize);
        ScaleButtonSpecImageType(bs.WorkspaceRestore, scaleFactor, PaletteButtonSpecStyle.WorkspaceRestore);

        // scale images
        pal.Images.PopulateFromBase(); //populate images first so we can scale them
        // CheckBox
        KryptonPaletteImagesCheckBox cb = pal.Images.CheckBox;
        cb.CheckedDisabled = GetScaledImage(cb.CheckedDisabled, scaleFactor);
        cb.CheckedNormal = GetScaledImage(cb.CheckedNormal, scaleFactor);
        cb.CheckedPressed = GetScaledImage(cb.CheckedPressed, scaleFactor);
        cb.CheckedTracking = GetScaledImage(cb.CheckedTracking, scaleFactor);
        cb.UncheckedDisabled = GetScaledImage(cb.UncheckedDisabled, scaleFactor);
        cb.UncheckedNormal = GetScaledImage(cb.UncheckedNormal, scaleFactor);
        cb.UncheckedPressed = GetScaledImage(cb.UncheckedPressed, scaleFactor);
        cb.UncheckedTracking = GetScaledImage(cb.UncheckedTracking, scaleFactor);
        // ContextMenu
        KryptonPaletteImagesContextMenu cm = pal.Images.ContextMenu;
        cm.Checked = GetScaledImage(cm.Checked, scaleFactor);
        cm.Indeterminate = GetScaledImage(cm.Indeterminate, scaleFactor);
        cm.SubMenu = GetScaledImage(cm.SubMenu, scaleFactor);
        // GalleryButtons
        // I'm not using GalleryButtons, so I'm skipping them
        // Radio Buttons
        KryptonPaletteImagesRadioButton rb = pal.Images.RadioButton;
        rb.CheckedDisabled = GetScaledImage(rb.CheckedDisabled, scaleFactor);
        rb.CheckedNormal = GetScaledImage(rb.CheckedNormal, scaleFactor);
        rb.CheckedPressed = GetScaledImage(rb.CheckedPressed, scaleFactor);
        rb.CheckedTracking = GetScaledImage(rb.CheckedTracking, scaleFactor);
        rb.UncheckedDisabled = GetScaledImage(rb.UncheckedDisabled, scaleFactor);
        rb.UncheckedNormal = GetScaledImage(rb.UncheckedNormal, scaleFactor);
        rb.UncheckedPressed = GetScaledImage(rb.UncheckedPressed, scaleFactor);
        rb.UncheckedTracking = GetScaledImage(rb.UncheckedTracking, scaleFactor);

        // resume palette updates
        pal.ResumeUpdates();
    }

    // helper method for scaling KyrptonPaletteButtonSpecTyped
    /// <summary>Scales the type of the button spec image.</summary>
    /// <param name="bst">The ButtonSpecType.</param>
    /// <param name="scaleFactor">The scale factor.</param>
    /// <param name="style">The palette button spec style.</param>
    private static void ScaleButtonSpecImageType(KryptonPaletteButtonSpecTyped bst, SizeF scaleFactor,
        PaletteButtonSpecStyle style)
    {
        bst.Image = GetScaledButtonSpecImage(bst.Image, scaleFactor, style);
        CheckButtonImageStates imgState = bst.ImageStates;
        imgState.ImageCheckedNormal = GetScaledButtonSpecImage(imgState.ImageCheckedNormal, scaleFactor, style);
        imgState.ImageCheckedPressed = GetScaledButtonSpecImage(imgState.ImageCheckedPressed, scaleFactor, style);
        imgState.ImageCheckedTracking = GetScaledButtonSpecImage(imgState.ImageCheckedTracking, scaleFactor, style);
        imgState.ImageDisabled = GetScaledButtonSpecImage(imgState.ImageDisabled, scaleFactor, style);
        imgState.ImageNormal = GetScaledButtonSpecImage(imgState.ImageNormal, scaleFactor, style);
        imgState.ImagePressed = GetScaledButtonSpecImage(imgState.ImagePressed, scaleFactor, style);
        imgState.ImageTracking = GetScaledButtonSpecImage(imgState.ImageTracking, scaleFactor, style);
    }

    /// <summary>
    /// Scales a button spec image using <see cref="ButtonSpecImageResolver"/> (Issue #978).
    /// </summary>
    private static Image? GetScaledButtonSpecImage(Image? img, SizeF scaleFactor, PaletteButtonSpecStyle style)
    {
        if (img == null)
        {
            return null;
        }

        if (scaleFactor is { Width: 1, Height: 1 })
        {
            return img;
        }

        Image? scale2x = ButtonSpecDpiImageRegistry.GetScale2x(img, style);
        Image? scale3x = ButtonSpecDpiImageRegistry.GetScale3x(img, style);
        Image? resolved = ButtonSpecImageResolver.ResolveForDpi(img, scale2x, scale3x, scaleFactor.Width,
            scaleFactor.Height, 1f, img.Width, img.Height);
        if (resolved == null)
        {
            return null;
        }

        using var tmpBmp = new Bitmap(resolved);
        tmpBmp.MakeTransparent(GlobalStaticVariables.TRANSPARENCY_KEY_COLOR);
        return new Bitmap(tmpBmp);
    }

    // scales an image and also makes magenta transparent
    /// <summary>Gets the scaled image.</summary>
    /// <param name="img">The image.</param>
    /// <param name="scaleFactor">The scale factor.</param>
    /// <returns>A scaled image, based on the scaleFactor.</returns>
    private static Image? GetScaledImage(Image? img, SizeF scaleFactor)
    {
        if (img == null)
        {
            return null;
        }

        if (scaleFactor is { Width: 1, Height: 1 })
        {
            return img;
        }
        using var tmpBmp = new Bitmap(img);
        tmpBmp.MakeTransparent(GlobalStaticVariables.TRANSPARENCY_KEY_COLOR);
        return CommonHelper.ScaleImageForSizedDisplay(tmpBmp, img.Width * scaleFactor.Width, img.Height * scaleFactor.Height, false);
    }
}