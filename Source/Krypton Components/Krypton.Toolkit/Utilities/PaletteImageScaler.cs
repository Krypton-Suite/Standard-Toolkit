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
        ScaleButtonSpecImageType(bs.ArrowDown, scaleFactor);
        ScaleButtonSpecImageType(bs.ArrowLeft, scaleFactor);
        ScaleButtonSpecImageType(bs.ArrowRight, scaleFactor);
        ScaleButtonSpecImageType(bs.ArrowUp, scaleFactor);
        ScaleButtonSpecImageType(bs.Close, scaleFactor);
        ScaleButtonSpecImageType(bs.Common, scaleFactor);
        ScaleButtonSpecImageType(bs.Context, scaleFactor);
        ScaleButtonSpecImageType(bs.DropDown, scaleFactor);
        ScaleButtonSpecImageType(bs.FormClose, scaleFactor);
        ScaleButtonSpecImageType(bs.FormMax, scaleFactor);
        ScaleButtonSpecImageType(bs.FormMin, scaleFactor);
        ScaleButtonSpecImageType(bs.FormRestore, scaleFactor);
        ScaleButtonSpecImageType(bs.FormHelp, scaleFactor);
        ScaleButtonSpecImageType(bs.Generic, scaleFactor);
        ScaleButtonSpecImageType(bs.Next, scaleFactor);
        ScaleButtonSpecImageType(bs.PendantClose, scaleFactor);
        ScaleButtonSpecImageType(bs.PendantMin, scaleFactor);
        ScaleButtonSpecImageType(bs.PendantRestore, scaleFactor);
        ScaleButtonSpecImageType(bs.PinHorizontal, scaleFactor);
        ScaleButtonSpecImageType(bs.PinVertical, scaleFactor);
        ScaleButtonSpecImageType(bs.Previous, scaleFactor);
        ScaleButtonSpecImageType(bs.RibbonExpand, scaleFactor);
        ScaleButtonSpecImageType(bs.RibbonMinimize, scaleFactor);
        ScaleButtonSpecImageType(bs.WorkspaceMaximize, scaleFactor);
        ScaleButtonSpecImageType(bs.WorkspaceRestore, scaleFactor);

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
    private static void ScaleButtonSpecImageType(KryptonPaletteButtonSpecTyped bst, SizeF scaleFactor)
    {
        bst.Image = GetScaledImage(bst.Image, scaleFactor);
        CheckButtonImageStates imgState = bst.ImageStates;
        imgState.ImageCheckedNormal = GetScaledImage(imgState.ImageCheckedNormal, scaleFactor);
        imgState.ImageCheckedPressed = GetScaledImage(imgState.ImageCheckedPressed, scaleFactor);
        imgState.ImageCheckedTracking = GetScaledImage(imgState.ImageCheckedTracking, scaleFactor);
        imgState.ImageDisabled = GetScaledImage(imgState.ImageDisabled, scaleFactor);
        imgState.ImageNormal = GetScaledImage(imgState.ImageNormal, scaleFactor);
        imgState.ImagePressed = GetScaledImage(imgState.ImagePressed, scaleFactor);
        imgState.ImageTracking = GetScaledImage(imgState.ImageTracking, scaleFactor);
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
        tmpBmp.MakeTransparent(GlobalStaticValues.TRANSPARENCY_KEY_COLOR);
        return CommonHelper.ScaleImageForSizedDisplay(tmpBmp, img.Width * scaleFactor.Width, img.Height * scaleFactor.Height, false);
    }
}