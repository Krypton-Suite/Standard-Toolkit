﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

using System.Drawing;
using System.Windows.Forms;

namespace Krypton.Toolkit.Utilities
{
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
        /// <param name="frm">Form</param>
        /// <param name="pal">KryptonPalette</param>
        public static void ScalePalette(Form frm, KryptonPalette pal)
        {
            SizeF dpi = new SizeF();
            SizeF scaleFactor = new SizeF();

            // Get System Dpi setting. Note this does not handle per monitor Dpi
            // but should be the same Dpi as AutoScaleFont
            using (Graphics g = frm.CreateGraphics())
            {
                dpi.Width = g.DpiX;
                dpi.Height = g.DpiY;
            }

            // set scale factor from current Dpi / the Dpi the images were created for (96)
            scaleFactor.Width = dpi.Width / 96.0F;
            scaleFactor.Height = dpi.Height / 96.0F;

            // if the scale is the same then no further processing needed (we are at 96 dpi).
            if ((scaleFactor.Width == 1.0F) && (scaleFactor.Height == 1.0F))
            {
                return;
            }

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
            // DropDownButton
            KryptonPaletteImagesDropDownButton ddb = pal.Images.DropDownButton;
            ddb.Disabled = GetScaledImage(ddb.Disabled, scaleFactor);
            ddb.Normal = GetScaledImage(ddb.Normal, scaleFactor);
            ddb.Pressed = GetScaledImage(ddb.Pressed, scaleFactor);
            ddb.Tracking = GetScaledImage(ddb.Tracking, scaleFactor);
            // GalleryButtons
            // I'm not using these so I'm skipping it
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
        private static Image GetScaledImage(Image img, SizeF scaleFactor)
        {
            if (img == null)
            {
                return null;
            }

            if ((scaleFactor.Width == 1) && (scaleFactor.Height == 1))
            {
                return img;
            }

            Bitmap bmp = new Bitmap((int)(img.Width * scaleFactor.Width), (int)(img.Height * scaleFactor.Height), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            using (Bitmap tmpBmp = new Bitmap(img))
            {
                tmpBmp.MakeTransparent(Color.Magenta);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    g.DrawImage(tmpBmp, 0, 0, bmp.Width, bmp.Height);
                }
            }

            return bmp;
        }
    }
}