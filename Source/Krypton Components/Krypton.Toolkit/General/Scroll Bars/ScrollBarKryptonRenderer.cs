#region BSD License
// TODO: Put in the correct license info
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// The scrollbar renderer class.
    /// </summary>
    internal static class ScrollBarKryptonRenderer
    {
        #region fields

        /// <summary>
        /// The colors of the thumb in the 3 states.
        /// </summary>
        private static readonly Color[,] thumbColours = new Color[3, 8];

        /// <summary>
        /// The arrow colors in the three states.
        /// </summary>
        private static readonly Color[,] arrowColours = new Color[3, 8];

        /// <summary>
        /// The arrow border colors.
        /// </summary>
        private static readonly Color[] arrowBorderColours = new Color[4];

        /// <summary>
        /// The background colors.
        /// </summary>
        private static readonly Color[] backgroundColours = new Color[5];

        /// <summary>
        /// The track colors.
        /// </summary>
        private static readonly Color[] trackColours = new Color[2];

        /// <summary>
        /// The Border colors.
        /// </summary>
        public static Color[] borderColours = new Color[2];

        /// <summary>
        /// The Grip colors.
        /// </summary>
        public static Color[] gripColours = new Color[2];

        private static IPalette _palette;
        private static PaletteRedirect _paletteRedirect;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes static members of the <see cref="ScrollBarKryptonRenderer"/> class.
        /// </summary>
        static ScrollBarKryptonRenderer()
        {
            InitColours();
        }

        public static void InitColours()
        {

            // add Palette Handler
            if (_palette != null)
            {
                _palette.PalettePaint += OnPalettePaint;
            }

            KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

            _palette = KryptonManager.CurrentGlobalPalette;
            _paletteRedirect = new PaletteRedirect(_palette);

            //Init Colors
            // hot state
            thumbColours[0, 0] = _palette.GetBorderColor1(PaletteBorderStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(96, 111, 148); // border color
            thumbColours[0, 1] = _palette.GetBackColor2(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(232, 233, 233); // left/top start color
            thumbColours[0, 2] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(230, 233, 241); // left/top end color
            thumbColours[0, 3] = _palette.GetBackColor2(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(233, 237, 242); // right/bottom line color
            thumbColours[0, 4] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(209, 218, 228); // right/bottom start color
            thumbColours[0, 5] = _palette.GetBackColor2(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(218, 227, 235); // right/bottom end color
            thumbColours[0, 6] = _palette.GetBackColor2(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(190, 202, 219); // right/bottom middle color
            thumbColours[0, 7] = _palette.GetBackColor2(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(96, 11, 148); // left/top line color

            // over state
            thumbColours[1, 0] = _palette.GetBorderColor1(PaletteBorderStyle.ButtonCluster, PaletteState.Normal);//Color.FromArgb(60, 110, 176);
            thumbColours[1, 1] = _palette.GetBackColor2(PaletteBackStyle.ButtonCluster, PaletteState.Normal); //Color.FromArgb(187, 204, 228);
            thumbColours[1, 2] = _palette.GetBackColor1(PaletteBackStyle.ButtonCluster, PaletteState.Normal);  //Color.FromArgb(205, 227, 254);
            thumbColours[1, 3] = _palette.GetBackColor2(PaletteBackStyle.ButtonCluster, PaletteState.Normal);  //Color.FromArgb(252, 253, 255);
            thumbColours[1, 4] = _palette.GetBackColor1(PaletteBackStyle.ButtonCluster, PaletteState.Normal);  //Color.FromArgb(170, 207, 247);
            thumbColours[1, 5] = _palette.GetBackColor2(PaletteBackStyle.ButtonAlternate, PaletteState.Normal);  //Color.FromArgb(219, 232, 251);
            thumbColours[1, 6] = _palette.GetBackColor2(PaletteBackStyle.ButtonCluster, PaletteState.Normal);  //Color.FromArgb(190, 202, 219);
            thumbColours[1, 7] = _palette.GetBackColor2(PaletteBackStyle.ButtonCluster, PaletteState.Normal);  //Color.FromArgb(233, 233, 235);

            // pressed state
            thumbColours[2, 0] = _palette.GetBorderColor1(PaletteBorderStyle.ButtonStandalone, PaletteState.CheckedNormal);//Color.FromArgb(23, 73, 138);
            thumbColours[2, 1] = _palette.GetBackColor2(PaletteBackStyle.ButtonStandalone, PaletteState.CheckedNormal); //Color.FromArgb(154, 184, 225);
            thumbColours[2, 2] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.CheckedNormal); // Color.FromArgb(166, 202, 250);
            thumbColours[2, 3] = _palette.GetBackColor2(PaletteBackStyle.ButtonStandalone, PaletteState.CheckedNormal);//Color.FromArgb(221, 235, 251);
            thumbColours[2, 4] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.CheckedNormal); //Color.FromArgb(110, 166, 240);
            thumbColours[2, 5] = _palette.GetBackColor2(PaletteBackStyle.ButtonStandalone, PaletteState.CheckedNormal); //Color.FromArgb(194, 218, 248);
            thumbColours[2, 6] = _palette.GetBackColor2(PaletteBackStyle.ButtonStandalone, PaletteState.CheckedNormal); //Color.FromArgb(190, 202, 219);
            thumbColours[2, 7] = _palette.GetBackColor2(PaletteBackStyle.ButtonStandalone, PaletteState.CheckedNormal); //Color.FromArgb(194, 211, 231);

            /* picture of colors and indices
             *(0,0)
             * -----------------------------------------------
             * |                                             |
             * | |-----------------------------------------| |
             * | |                  (2)                    | |
             * | | |-------------------------------------| | |
             * | | |                (0)                  | | |
             * | | |                                     | | |
             * | | |                                     | | |
             * | |3|                (1)                  |3| |
             * | |6|                (4)                  |6| |
             * | | |                                     | | |
             * | | |                (5)                  | | |
             * | | |-------------------------------------| | |
             * | |                  (12)                   | |
             * | |-----------------------------------------| |
             * |                                             |
             * ----------------------------------------------- (15,17)
             */

            // hot state
            arrowColours[0, 0] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Normal);//Color.FromArgb(223, 236, 252);
            arrowColours[0, 1] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(207, 225, 248);
            arrowColours[0, 2] = _palette.GetBackColor2(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(245, 249, 255);
            arrowColours[0, 3] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(237, 244, 252);
            arrowColours[0, 4] = _palette.GetBackColor2(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(244, 249, 255);
            arrowColours[0, 5] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(244, 249, 255);
            arrowColours[0, 6] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(251, 253, 255);
            arrowColours[0, 7] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(251, 253, 255);

            // over state
            arrowColours[1, 0] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Normal);//Color.FromArgb(205, 222, 243); //Colore bottone sul tracking
            arrowColours[1, 1] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(186, 208, 235);
            arrowColours[1, 2] = _palette.GetBackColor2(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(238, 244, 252);
            arrowColours[1, 3] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(229, 237, 247);
            arrowColours[1, 4] = _palette.GetBackColor2(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(223, 234, 247);
            arrowColours[1, 5] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(241, 246, 254);
            arrowColours[1, 6] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(243, 247, 252);
            arrowColours[1, 7] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(250, 252, 255);

            // pressed state
            arrowColours[2, 0] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Tracking);//Color.FromArgb(215, 220, 225);
            arrowColours[2, 1] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Tracking); //Color.FromArgb(195, 202, 210);
            arrowColours[2, 2] = _palette.GetBackColor2(PaletteBackStyle.ButtonStandalone, PaletteState.Tracking); //Color.FromArgb(242, 244, 245);
            arrowColours[2, 3] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Tracking); //Color.FromArgb(232, 235, 238);
            arrowColours[2, 4] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Tracking); //Color.FromArgb(226, 228, 230);
            arrowColours[2, 5] = _palette.GetBackColor2(PaletteBackStyle.ButtonStandalone, PaletteState.Tracking); //Color.FromArgb(230, 233, 236);
            arrowColours[2, 6] = _palette.GetBackColor2(PaletteBackStyle.ButtonStandalone, PaletteState.Tracking); //Color.FromArgb(244, 245, 245);
            arrowColours[2, 7] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Tracking); //Color.FromArgb(245, 247, 248);

            // background colors
            backgroundColours[0] = _palette.GetBackColor2(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(235, 237, 239);
            backgroundColours[1] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(252, 252, 252);
            backgroundColours[2] = _palette.GetBackColor1(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(247, 247, 247);
            backgroundColours[3] = _palette.GetBackColor2(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(238, 238, 238);
            backgroundColours[4] = _palette.GetBackColor2(PaletteBackStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(240, 240, 240);

            // track colors
            trackColours[0] = _palette.ColorTable.StatusStripGradientEnd; //Color.FromArgb(204, 204, 204);
            trackColours[1] = _palette.ColorTable.StatusStripGradientBegin; //Color.FromArgb(220, 220, 220);

            // arrow border colors
            arrowBorderColours[0] = _palette.GetBorderColor1(PaletteBorderStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(135, 146, 160);
            arrowBorderColours[1] = _palette.GetBorderColor1(PaletteBorderStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(140, 151, 165);
            arrowBorderColours[2] = _palette.GetBorderColor1(PaletteBorderStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(128, 139, 153);
            arrowBorderColours[3] = _palette.GetBorderColor1(PaletteBorderStyle.ButtonStandalone, PaletteState.Normal); //Color.FromArgb(99, 110, 125);

            //Border colors
            borderColours[0] = _palette.GetBorderColor1(PaletteBorderStyle.InputControlCustom1, PaletteState.Normal);
            borderColours[1] = _palette.GetBorderColor1(PaletteBorderStyle.InputControlCustom1, PaletteState.Normal); ;

            //Grip colors
            gripColours[0] = _palette.ColorTable.GripLight;
            gripColours[1] = _palette.ColorTable.GripDark;

        }
        #endregion

        #region methods

        #region public methods

        /// <summary>
        /// Draws the background.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> used to paint.</param>
        /// <param name="rect">The rectangle in which to paint.</param>
        /// <param name="orientation">The <see cref="ScrollBarOrientation"/>.</param>
        public static void DrawBackground(
           Graphics g,
           Rectangle rect,
           ScrollBarOrientation orientation)
        {
            if (g == null)
            {
                throw new ArgumentNullException(nameof(g));
            }

            if (rect.IsEmpty || g.IsVisibleClipEmpty
               || !g.VisibleClipBounds.IntersectsWith(rect))
            {
                return;
            }

            if (orientation == ScrollBarOrientation.Vertical)
            {
                DrawBackgroundVertical(g, rect);
            }
            else
            {
                DrawBackgroundHorizontal(g, rect);
            }
        }

        /// <summary>
        /// Draws the channel ( or track ).
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> used to paint.</param>
        /// <param name="rect">The rectangle in which to paint.</param>
        /// <param name="state">The scrollbar state.</param>
        /// <param name="orientation">The <see cref="ScrollBarOrientation"/>.</param>
        public static void DrawTrack(
           Graphics g,
           Rectangle rect,
           ScrollBarState state,
           ScrollBarOrientation orientation)
        {
            if (g == null)
            {
                throw new ArgumentNullException(nameof(g));
            }

            if (rect.Width <= 0 || rect.Height <= 0
               || state != ScrollBarState.Pressed || g.IsVisibleClipEmpty
               || !g.VisibleClipBounds.IntersectsWith(rect))
            {
                return;
            }

            if (orientation == ScrollBarOrientation.Vertical)
            {
                DrawTrackVertical(g, rect);
            }
            else
            {
                DrawTrackHorizontal(g, rect);
            }
        }

        /// <summary>
        /// Draws the thumb.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> used to paint.</param>
        /// <param name="rect">The rectangle in which to paint.</param>
        /// <param name="state">The <see cref="ScrollBarState"/> of the thumb.</param>
        /// <param name="orientation">The <see cref="ScrollBarOrientation"/>.</param>
        public static void DrawThumb(
           Graphics g,
           Rectangle rect,
           ScrollBarState state,
           ScrollBarOrientation orientation)
        {
            if (g == null)
            {
                throw new ArgumentNullException(nameof(g));
            }

            if (rect.IsEmpty || g.IsVisibleClipEmpty
               || !g.VisibleClipBounds.IntersectsWith(rect)
               || state == ScrollBarState.Disabled)
            {
                return;
            }

            if (orientation == ScrollBarOrientation.Vertical)
            {
                DrawThumbVertical(g, rect, state);
            }
            else
            {
                DrawThumbHorizontal(g, rect, state);
            }
        }

        /// <summary>
        /// Draws the grip of the thumb.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> used to paint.</param>
        /// <param name="rect">The rectangle in which to paint.</param>
        /// <param name="orientation">The <see cref="ScrollBarOrientation"/>.</param>
        public static void DrawThumbGrip(
           Graphics g,
           Rectangle rect,
           ScrollBarOrientation orientation)
        {
            if (g == null)
            {
                throw new ArgumentNullException(nameof(g));
            }

            if (rect.IsEmpty || g.IsVisibleClipEmpty
               || !g.VisibleClipBounds.IntersectsWith(rect))
            {
                return;
            }

            // get grip image
            using Image gripImage = GetGripNomalBitmap();
            // adjust rectangle and rotate grip image if necessary
            Rectangle r = AdjustThumbGrip(rect, orientation, gripImage);

            // adjust alpha channel of grip image
            using ImageAttributes attr = new();
            attr.SetColorMatrix(
                new ColorMatrix(new float[][] {
                    new[] { 1F, 0, 0, 0, 0 },
                    new[] { 0, 1F, 0, 0, 0 },
                    new[] { 0, 0, 1F, 0, 0 },
                    new[] { 0, 0, 0,  .8F, 0 },
                    new[] { 0, 0, 0, 0, 1F }
                }),
                ColorMatrixFlag.Default,
                ColorAdjustType.Bitmap
            );

            // draw grip image
            g.DrawImage(gripImage, r, 0, 0, r.Width, r.Height, GraphicsUnit.Pixel, attr);
        }

        /// <summary>
        /// Draws the GripNomal Bitmap.
        /// </summary>
        public static Bitmap GetGripNomalBitmap()
        {
            Bitmap btm = new(8, 8);
            btm.SetResolution(72, 72);
            Graphics g = Graphics.FromImage(btm);
            Rectangle rect = new(0, 0, 8, 8);

            g.DrawLine(new Pen(gripColours[1]), new Point(0, 0), new Point(8, 0));//dark
            g.DrawLine(new Pen(gripColours[0]), new Point(1, 1), new Point(7, 1));//light
            g.DrawLine(new Pen(gripColours[1]), new Point(0, 2), new Point(7, 2));//dark
            g.DrawLine(new Pen(gripColours[0]), new Point(1, 3), new Point(7, 3));//light
            g.DrawLine(new Pen(gripColours[1]), new Point(0, 4), new Point(7, 4));//dark
            g.DrawLine(new Pen(gripColours[0]), new Point(1, 5), new Point(7, 5));//light
            g.DrawLine(new Pen(gripColours[1]), new Point(0, 6), new Point(8, 6));//dark
            g.DrawLine(new Pen(gripColours[0]), new Point(1, 7), new Point(7, 7));//light

            return btm;
        }

        /// <summary>
        /// Draws the GripNomal Bitmap.
        /// </summary>
        public static Image GetScrollBarArrowDownBitmap()
        {
            Bitmap img = new(9, 5, PixelFormat.Format32bppArgb);
            img.SetResolution(72, 72);
            Graphics g = Graphics.FromImage(img);

            g.DrawLine(new Pen(gripColours[1]), new Point(0, 0), new Point(8, 0));//dark---------
            g.DrawLine(new Pen(gripColours[1]), new Point(1, 1), new Point(7, 1));//dark -------
            g.DrawLine(new Pen(gripColours[1]), new Point(2, 2), new Point(6, 2));//dark  -----
            g.DrawLine(new Pen(gripColours[1]), new Point(3, 3), new Point(5, 3));//dark   ---
            g.DrawLine(new Pen(gripColours[1]), new Point(4, 0), new Point(4, 4));//dark    -

            g.DrawLine(new Pen(gripColours[0]), new Point(3, 0), new Point(5, 0));//light
            g.DrawLine(new Pen(gripColours[0]), new Point(4, 0), new Point(4, 1));//light


            return img;
        }



        /// <summary>
        /// Draws an arrow button.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> used to paint.</param>
        /// <param name="rect">The rectangle in which to paint.</param>
        /// <param name="state">The <see cref="ScrollBarArrowButtonState"/> of the arrow button.</param>
        /// <param name="arrowUp">true for an up arrow, false otherwise.</param>
        /// <param name="orientation">The <see cref="ScrollBarOrientation"/>.</param>
        public static void DrawArrowButton(
           Graphics g,
           Rectangle rect,
           ScrollBarArrowButtonState state,
           bool arrowUp,
           ScrollBarOrientation orientation)
        {
            if (g == null)
            {
                throw new ArgumentNullException(nameof(g));
            }

            if (rect.IsEmpty || g.IsVisibleClipEmpty
               || !g.VisibleClipBounds.IntersectsWith(rect))
            {
                return;
            }

            if (orientation == ScrollBarOrientation.Vertical)
            {
                DrawArrowButtonVertical(g, rect, state, arrowUp);
            }
            else
            {
                DrawArrowButtonHorizontal(g, rect, state, arrowUp);
            }
        }

        #endregion

        #region private methods

        /// <summary>
        /// Draws the background.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> used to paint.</param>
        /// <param name="rect">The rectangle in which to paint.</param>
        private static void DrawBackgroundVertical(Graphics g, Rectangle rect)
        {
            using (Pen p = new(backgroundColours[0]))
            {
                g.DrawLine(p, rect.Left + 1, rect.Top + 1, rect.Left + 1, rect.Bottom - 1);
                g.DrawLine(p, rect.Right - 2, rect.Top + 1, rect.Right - 2, rect.Bottom - 1);
            }

            using (Pen p = new(backgroundColours[1]))
            {
                g.DrawLine(p, rect.Left + 2, rect.Top + 1, rect.Left + 2, rect.Bottom - 1);
            }

            Rectangle firstRect = new(
               rect.Left + 3,
               rect.Top,
               8,
               rect.Height);

            Rectangle secondRect = new(
               firstRect.Right - 1,
               firstRect.Top,
               7,
               firstRect.Height);

            using (LinearGradientBrush brush = new(
               firstRect,
               backgroundColours[2],
               backgroundColours[3],
               LinearGradientMode.Horizontal))
            {
                g.FillRectangle(brush, firstRect);
            }

            using (LinearGradientBrush brush = new(
               secondRect,
               backgroundColours[3],
               backgroundColours[4],
               LinearGradientMode.Horizontal))
            {
                g.FillRectangle(brush, secondRect);
            }
        }

        /// <summary>
        /// Draws the background.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> used to paint.</param>
        /// <param name="rect">The rectangle in which to paint.</param>
        private static void DrawBackgroundHorizontal(Graphics g, Rectangle rect)
        {
            using (Pen p = new(backgroundColours[0]))
            {
                g.DrawLine(p, rect.Left + 1, rect.Top + 1, rect.Right - 1, rect.Top + 1);
                g.DrawLine(p, rect.Left + 1, rect.Bottom - 2, rect.Right - 1, rect.Bottom - 2);
            }

            using (Pen p = new(backgroundColours[1]))
            {
                g.DrawLine(p, rect.Left + 1, rect.Top + 2, rect.Right - 1, rect.Top + 2);
            }

            Rectangle firstRect = new(
               rect.Left,
               rect.Top + 3,
               rect.Width,
               8);

            Rectangle secondRect = new(
               firstRect.Left,
               firstRect.Bottom - 1,
               firstRect.Width,
               7);

            using (LinearGradientBrush brush = new(
               firstRect,
               backgroundColours[2],
               backgroundColours[3],
               LinearGradientMode.Vertical))
            {
                g.FillRectangle(brush, firstRect);
            }

            using (LinearGradientBrush brush = new(
               secondRect,
               backgroundColours[3],
               backgroundColours[4],
               LinearGradientMode.Vertical))
            {
                g.FillRectangle(brush, secondRect);
            }
        }

        /// <summary>
        /// Draws the channel ( or track ).
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> used to paint.</param>
        /// <param name="rect">The rectangle in which to paint.</param>
        private static void DrawTrackVertical(Graphics g, Rectangle rect)
        {
            Rectangle innerRect = new(rect.Left + 1, rect.Top, 15, rect.Height);

            using LinearGradientBrush brush = new(
                innerRect,
                trackColours[0],
                trackColours[1],
                LinearGradientMode.Horizontal);
            g.FillRectangle(brush, innerRect);
        }

        /// <summary>
        /// Draws the channel ( or track ).
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> used to paint.</param>
        /// <param name="rect">The rectangle in which to paint.</param>
        private static void DrawTrackHorizontal(Graphics g, Rectangle rect)
        {
            Rectangle innerRect = new(rect.Left, rect.Top + 1, rect.Width, 15);

            using LinearGradientBrush brush = new(
                innerRect,
                trackColours[0],
                trackColours[1],
                LinearGradientMode.Vertical);
            g.FillRectangle(brush, innerRect);
        }

        /// <summary>
        /// Adjusts the thumb grip according to the specified <see cref="ScrollBarOrientation"/>.
        /// </summary>
        /// <param name="rect">The rectangle to adjust.</param>
        /// <param name="orientation">The scrollbar orientation.</param>
        /// <param name="gripImage">The grip image.</param>
        /// <returns>The adjusted rectangle.</returns>
        /// <remarks>Also rotates the grip image if necessary.</remarks>
        private static Rectangle AdjustThumbGrip(
           Rectangle rect,
           ScrollBarOrientation orientation,
           Image gripImage)
        {
            Rectangle r = rect;

            r.Inflate(-1, -1);

            if (orientation == ScrollBarOrientation.Vertical)
            {
                r.X += 3;
                r.Y += (r.Height / 2) - (gripImage.Height / 2);
            }
            else
            {
                gripImage.RotateFlip(RotateFlipType.Rotate90FlipNone);

                r.X += (r.Width / 2) - (gripImage.Width / 2);
                r.Y += 3;
            }

            r.Width = gripImage.Width;
            r.Height = gripImage.Height;

            return r;
        }

        /// <summary>
        /// Draws the thumb.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> used to paint.</param>
        /// <param name="rect">The rectangle in which to paint.</param>
        /// <param name="state">The <see cref="ScrollBarState"/> of the thumb.</param>
        private static void DrawThumbVertical(
           Graphics g,
           Rectangle rect,
           ScrollBarState state)
        {
            var index = state switch
            {
                ScrollBarState.Hot => 1,
                ScrollBarState.Pressed => 2,
                _ => 0
            };

            Rectangle innerRect = rect;
            innerRect.Inflate(-1, -1);

            Rectangle r = innerRect;
            r.Width = 6;
            r.Height++;

            // draw left gradient
            using (LinearGradientBrush brush = new(
               r,
               thumbColours[index, 1],
               thumbColours[index, 2],
               LinearGradientMode.Horizontal))
            {
                g.FillRectangle(brush, r);
            }

            r.X = r.Right;

            // draw right gradient
            if (index == 0)
            {
                using LinearGradientBrush brush = new(
                    r,
                    thumbColours[index, 4],
                    thumbColours[index, 5],
                    LinearGradientMode.Horizontal);
                brush.InterpolationColors = new ColorBlend(3)
                {
                    Colors = new[]
                    {
                        thumbColours[index, 4],
                        thumbColours[index, 6],
                        thumbColours[index, 5]
                    },
                    Positions = new[] { 0f, .5f, 1f }
                };

                g.FillRectangle(brush, r);
            }
            else
            {
                using (LinearGradientBrush brush = new(
                   r,
                   thumbColours[index, 4],
                   thumbColours[index, 5],
                   LinearGradientMode.Horizontal))
                {
                    g.FillRectangle(brush, r);
                }

                // draw left line
                using (Pen p = new(thumbColours[index, 7]))
                {
                    g.DrawLine(p, innerRect.X, innerRect.Y, innerRect.X, innerRect.Bottom);
                }
            }

            // draw right line
            using (Pen p = new(thumbColours[index, 3]))
            {
                g.DrawLine(p, innerRect.Right, innerRect.Y, innerRect.Right, innerRect.Bottom);
            }

            g.SmoothingMode = SmoothingMode.AntiAlias;

            // draw border
            using (Pen p = new(thumbColours[index, 0]))
            {
                using (GraphicsPath path = CreateRoundPath(rect, 2f, 2f))
                {
                    g.DrawPath(p, path);
                }
            }

            g.SmoothingMode = SmoothingMode.None;
        }

        /// <summary>
        /// Draws the thumb.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> used to paint.</param>
        /// <param name="rect">The rectangle in which to paint.</param>
        /// <param name="state">The <see cref="ScrollBarState"/> of the thumb.</param>
        private static void DrawThumbHorizontal(
           Graphics g,
           Rectangle rect,
           ScrollBarState state)
        {
            var index = state switch
            {
                ScrollBarState.Hot => 1,
                ScrollBarState.Pressed => 2,
                _ => 0
            };

            Rectangle innerRect = rect;
            innerRect.Inflate(-1, -1);

            Rectangle r = innerRect;
            r.Height = 6;
            r.Width++;

            // draw left gradient
            using (LinearGradientBrush brush = new(
               r, thumbColours[index, 1],
               thumbColours[index, 2],
               LinearGradientMode.Vertical))
            {
                g.FillRectangle(brush, r);
            }

            r.Y = r.Bottom;

            // draw right gradient
            if (index == 0)
            {
                using LinearGradientBrush brush = new(
                    r,
                    thumbColours[index, 4],
                    thumbColours[index, 5],
                    LinearGradientMode.Vertical);
                brush.InterpolationColors = new ColorBlend(3)
                {
                    Colors = new[]
                    {
                        thumbColours[index, 4],
                        thumbColours[index, 6],
                        thumbColours[index, 5]
                    },
                    Positions = new[] { 0f, .5f, 1f }
                };

                g.FillRectangle(brush, r);
            }
            else
            {
                using (LinearGradientBrush brush = new(
                   r, thumbColours[index, 4],
                   thumbColours[index, 5],
                   LinearGradientMode.Vertical))
                {
                    g.FillRectangle(brush, r);
                }

                // draw left line
                using (Pen p = new(thumbColours[index, 7]))
                {
                    g.DrawLine(p, innerRect.X, innerRect.Y, innerRect.Right, innerRect.Y);
                }
            }

            // draw right line
            using (Pen p = new(thumbColours[index, 3]))
            {
                g.DrawLine(p, innerRect.X, innerRect.Bottom, innerRect.Right, innerRect.Bottom);
            }

            g.SmoothingMode = SmoothingMode.AntiAlias;

            // draw border
            using (Pen p = new(thumbColours[index, 0]))
            {
                using (GraphicsPath path = CreateRoundPath(rect, 2f, 2f))
                {
                    g.DrawPath(p, path);
                }
            }

            g.SmoothingMode = SmoothingMode.None;
        }

        /// <summary>
        /// Draws an arrow button.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> used to paint.</param>
        /// <param name="rect">The rectangle in which to paint.</param>
        /// <param name="state">The <see cref="ScrollBarArrowButtonState"/> of the arrow button.</param>
        /// <param name="arrowUp">true for an up arrow, false otherwise.</param>
        private static void DrawArrowButtonVertical(
           Graphics g,
           Rectangle rect,
           ScrollBarArrowButtonState state,
           bool arrowUp)
        {
            using Image arrowImage = GetArrowDownButtonImage(state);
            if (arrowUp)
            {
                arrowImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
            }

            g.DrawImage(arrowImage, rect);
        }

        /// <summary>
        /// Draws an arrow button.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> used to paint.</param>
        /// <param name="rect">The rectangle in which to paint.</param>
        /// <param name="state">The <see cref="ScrollBarArrowButtonState"/> of the arrow button.</param>
        /// <param name="arrowUp">true for an up arrow, false otherwise.</param>
        private static void DrawArrowButtonHorizontal(
           Graphics g,
           Rectangle rect,
           ScrollBarArrowButtonState state,
           bool arrowUp)
        {
            using Image arrowImage = GetArrowDownButtonImage(state);
            if (arrowUp)
            {
                arrowImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            else
            {
                arrowImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }

            g.DrawImage(arrowImage, rect);
        }

        /// <summary>
        /// Draws the arrow down button for the scrollbar.
        /// </summary>
        /// <param name="state">The button state.</param>
        /// <returns>The arrow down button as <see cref="Image"/>.</returns>
        private static Image GetArrowDownButtonImage(
           ScrollBarArrowButtonState state)
        {
            Rectangle rect = new(0, 0, 15, 17);
            Bitmap bitmap = new(15, 17, PixelFormat.Format32bppArgb);
            bitmap.SetResolution(72f, 72f);

            using Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.None;
            g.InterpolationMode = InterpolationMode.Low;

            var index = -1;

            switch (state)
            {
                case ScrollBarArrowButtonState.UpHot:
                case ScrollBarArrowButtonState.DownHot:
                {
                    index = 1;

                    break;
                }

                case ScrollBarArrowButtonState.UpActive:
                case ScrollBarArrowButtonState.DownActive:
                {
                    index = 0;

                    break;
                }

                case ScrollBarArrowButtonState.UpPressed:
                case ScrollBarArrowButtonState.DownPressed:
                {
                    index = 2;

                    break;
                }
            }

            if (index != -1)
            {
                using (Pen p1 = new(arrowBorderColours[0]),
                    p2 = new(arrowBorderColours[1]))
                {
                    g.DrawLine(p1, rect.X, rect.Y, rect.Right - 1, rect.Y);
                    g.DrawLine(p2, rect.X, rect.Bottom - 1, rect.Right - 1, rect.Bottom - 1);
                }

                rect.Inflate(0, -1);

                using (LinearGradientBrush brush = new(
                    rect,
                    arrowBorderColours[2],
                    arrowBorderColours[1],
                    LinearGradientMode.Vertical))
                {
                    ColorBlend blend = new(3)
                    {
                        Positions = new[] { 0f, .5f, 1f },
                        Colors = new[] {
                            arrowBorderColours[2],
                            arrowBorderColours[3],
                            arrowBorderColours[0] }
                    };

                    brush.InterpolationColors = blend;

                    using (Pen p = new(brush))
                    {
                        g.DrawLine(p, rect.X, rect.Y, rect.X, rect.Bottom - 1);
                        g.DrawLine(p, rect.Right - 1, rect.Y, rect.Right - 1, rect.Bottom - 1);
                    }
                }

                rect.Inflate(0, 1);

                Rectangle upper = rect;
                upper.Inflate(-1, 0);
                upper.Y++;
                upper.Height = 7;

                using (LinearGradientBrush brush = new(
                    upper,
                    arrowColours[index, 2],
                    arrowColours[index, 3],
                    LinearGradientMode.Vertical))
                {
                    g.FillRectangle(brush, upper);
                }

                upper.Inflate(-1, 0);
                upper.Height = 6;

                using (LinearGradientBrush brush = new(
                    upper,
                    arrowColours[index, 0],
                    arrowColours[index, 1],
                    LinearGradientMode.Vertical))
                {
                    g.FillRectangle(brush, upper);
                }

                Rectangle lower = rect;
                lower.Inflate(-1, 0);
                lower.Y = 8;
                lower.Height = 8;

                using (LinearGradientBrush brush = new(
                    lower,
                    arrowColours[index, 6],
                    arrowColours[index, 7],
                    LinearGradientMode.Vertical))
                {
                    g.FillRectangle(brush, lower);
                }

                lower.Inflate(-1, 0);

                using (LinearGradientBrush brush = new(
                    lower,
                    arrowColours[index, 4],
                    arrowColours[index, 5],
                    LinearGradientMode.Vertical))
                {
                    g.FillRectangle(brush, lower);
                }
            }

            using Image arrowIcon = (Image)GetScrollBarArrowDownBitmap().Clone();
            if (state == ScrollBarArrowButtonState.DownDisabled
                || state == ScrollBarArrowButtonState.UpDisabled)
            {
                ControlPaint.DrawImageDisabled(
                    g,
                    arrowIcon,
                    3,
                    6,
                    Color.Transparent);
            }
            else
            {
                g.DrawImage(arrowIcon, 3, 6);
            }

            return bitmap;
        }

        /// <summary>
        /// Creates a rounded rectangle.
        /// </summary>
        /// <param name="r">The rectangle to create the rounded rectangle from.</param>
        /// <param name="radiusX">The x-radius.</param>
        /// <param name="radiusY">The y-radius.</param>
        /// <returns>A <see cref="GraphicsPath"/> object representing the rounded rectangle.</returns>
        private static GraphicsPath CreateRoundPath(
           Rectangle r,
           float radiusX,
           float radiusY)
        {
            // create new graphics path object
            GraphicsPath path = new();

            // calculate radius of edges
            PointF d = new(Math.Min(radiusX * 2, r.Width), Math.Min(radiusY * 2, r.Height));

            // make sure radius is valid
            d.X = Math.Max(1, d.X);
            d.Y = Math.Max(1, d.Y);

            // add top left arc
            path.AddArc(r.X, r.Y, d.X, d.Y, 180, 90);

            // add top right arc
            path.AddArc(r.Right - d.X, r.Y, d.X, d.Y, 270, 90);

            // add bottom right arc
            path.AddArc(r.Right - d.X, r.Bottom - d.Y, d.X, d.Y, 0, 90);

            // add bottom left arc
            path.AddArc(r.X, r.Bottom - d.Y, d.X, d.Y, 90, 90);

            // close path
            path.CloseFigure();

            return path;
        }

        #endregion

        #endregion

        #region ... Krypton ...


        //Kripton Palette Events
        private static void OnGlobalPaletteChanged(object sender, EventArgs e)
        {
            if (_palette != null)
            {
                _palette.PalettePaint -= OnPalettePaint;
            }

            _palette = KryptonManager.CurrentGlobalPalette;
            _paletteRedirect.Target = _palette;

            if (_palette != null)
            {
                _palette.PalettePaint += OnPalettePaint;
                //repaint with new values

                InitColours();
            }

            //Invalidate();
        }

        //Kripton Palette Events
        private static void OnPalettePaint(object sender, PaletteLayoutEventArgs e)
        {
            //Invalidate();
        }

        #endregion
    }
}