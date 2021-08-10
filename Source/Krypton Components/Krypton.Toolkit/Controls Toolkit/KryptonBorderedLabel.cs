#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonBorderedLabel), "ToolboxBitmaps.KryptonLabel.bmp")]
    [DefaultEvent("Paint")]
    [DefaultProperty("Text")]
    [DefaultBindingProperty("Text")]
    [Designer("Krypton.Toolkit.KryptonLabelDesigner, Krypton.Toolkit")]
    [DesignerCategory("code")]
    [Description("Displays descriptive information. Includes a border.")]
    public class KryptonBorderedLabel : KryptonLabel
    {
        #region Identity

        public KryptonBorderedLabel()
        {
            SetStyle(
                ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);

            UpdateStyles();

            BackColor = Color.Transparent;
        }
        #endregion

        #region Protected Overrides

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            ForeColor = KryptonManager.CurrentGlobalPalette.GetBorderColor1(PaletteBorderStyle.InputControlCustom1,
                PaletteState.Normal);

            Graphics gfx = e.Graphics;

            Rectangle r = new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width - 1,
                e.ClipRectangle.Height - 1);

            gfx.DrawRectangle(new Pen(ForeColor), r);
        }

        #endregion
    }
}