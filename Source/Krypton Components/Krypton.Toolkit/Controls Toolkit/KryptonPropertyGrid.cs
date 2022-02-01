#region BSD License
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
    [Designer(@"Krypton.Toolkit.KryptonPropertyGridDesigner, Krypton.Toolkit")]
    [ToolboxBitmap(typeof(PropertyGrid), "ToolboxBitmaps.KryptonPropertyGridVersion2.bmp")]
    [ToolboxItem(true)]
    public class KryptonPropertyGrid : PropertyGrid
    {
        #region Variables
        private IPalette _palette;

        private readonly PaletteRedirect _paletteRedirect;

        private Color _gradientMiddleColour = Color.Gray;
        #endregion

        #region Properties
        /// <summary>Gets or sets the gradient middle colour.</summary>
        /// <value>The gradient middle colour.</value>
        [Browsable(true), Category(@"Appearance-Extended"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DefaultValue("Color.Gray")]
        public Color GradientMiddleColour { get => _gradientMiddleColour; set { _gradientMiddleColour = value; Invalidate(); } }
        #endregion

        #region Constructor
        public KryptonPropertyGrid()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);

            UpdateStyles();

            // Add Palette Handler
            if (_palette != null)
            {
                _palette.PalettePaint += OnPalettePaint;
            }

            KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

            _palette = KryptonManager.CurrentGlobalPalette;

            _paletteRedirect = new PaletteRedirect(_palette);

            InitColours();

            Font = new Font("Segoe UI", 9f);
        }
        #endregion

        #region Overrides
        /// <inheritdoc />
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            pevent.Graphics.FillRectangle(new SolidBrush(_gradientMiddleColour), pevent.ClipRectangle);
        }
        #endregion

        #region Krypton
        // Krypton Palette Events
        /// <summary>Called when [global palette changed].</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnGlobalPaletteChanged(object sender, EventArgs e)
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

            Invalidate();
        }

        // Krypton Palette Events
        /// <summary>Called when [palette paint].</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PaletteLayoutEventArgs" /> instance containing the event data.</param>
        private void OnPalettePaint(object sender, PaletteLayoutEventArgs e) => Invalidate();

        /// <summary>Initialises the colours.</summary>
        private void InitColours()
        {
            ToolStripRenderer = ToolStripManager.Renderer;

            _gradientMiddleColour = _palette.ColorTable.ToolStripGradientMiddle;

            HelpBackColor = _palette.ColorTable.MenuStripGradientBegin;

            HelpForeColor = _palette.ColorTable.StatusStripText;

            LineColor = _palette.ColorTable.ToolStripGradientMiddle;

            CategoryForeColor = _palette.ColorTable.StatusStripText;
        }
        #endregion
    }
}