#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2021 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    ///<summary>A property grid control that supports the Krypton render.</summary>
    [Description(@"A property grid control that supports the Krypton render.")]
    [Designer(typeof(KryptonPropertyGridDesigner))]
    [ToolboxBitmap(typeof(PropertyGrid), "ToolboxBitmaps.KryptonPropertyGridVersion2.bmp")]
    [ToolboxItem(true)]
    public class KryptonPropertyGrid : PropertyGrid
    {
        #region Variables
        private PaletteBase? _palette;

        private readonly PaletteRedirect? _paletteRedirect;
        private readonly PaletteInputControlTripleRedirect _stateCommon;
        private readonly PaletteInputControlTripleStates _stateNormal;
        private readonly PaletteInputControlTripleStates _stateDisabled;
        private readonly PaletteInputControlTripleStates _stateActive;


        #endregion

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="KryptonPropertyGrid" /> class.</summary>
        public KryptonPropertyGrid()
        {
            SetStyle(ControlStyles.UserPaint
                     | ControlStyles.OptimizedDoubleBuffer
                     | ControlStyles.SupportsTransparentBackColor,
                true);

            UpdateStyles();

            // Add Palette Handler
            if (_palette != null)
            {
                _palette.PalettePaint += OnPalettePaint;
            }

            KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

            _palette = KryptonManager.CurrentGlobalPalette;

            _paletteRedirect = new PaletteRedirect(_palette);
            // Create the palette provider
            _stateCommon = new PaletteInputControlTripleRedirect(_paletteRedirect, PaletteBackStyle.InputControlStandalone, PaletteBorderStyle.HeaderCalendar, PaletteContentStyle.LabelNormalPanel, null);
            _stateDisabled = new PaletteInputControlTripleStates(_stateCommon, null);
            _stateNormal = new PaletteInputControlTripleStates(_stateCommon, null);
            _stateActive = new PaletteInputControlTripleStates(_stateCommon, null);

            InitColors();
        }
        #endregion

        #region Public
        /// <summary>Refreshes the colours.</summary>
        public void RefreshColors() => InitColors();
        #endregion

        #region Protected Overrides
        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            // Unhook from the static events, otherwise we cannot be garbage collected
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;

            base.Dispose(disposing);
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
            _paletteRedirect!.Target = _palette;

            if (_palette != null)
            {
                _palette.PalettePaint += OnPalettePaint;
                //repaint with new values
                InitColors();
            }

            Invalidate();
        }

        // Krypton Palette Events
        /// <summary>Called when [palette paint].</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PaletteLayoutEventArgs" /> instance containing the event data.</param>
        private void OnPalettePaint(object sender, PaletteLayoutEventArgs e) => Invalidate();

        /// <summary>Initialises the colours.</summary>
        private void InitColors()
        {
            ToolStripRenderer = ToolStripManager.Renderer;

            LineColor = _palette!.ColorTable.ToolStripGradientMiddle;

            CategoryForeColor = _palette!.ColorTable.ToolStripDropDownBackground;

            var normalFont = _stateNormal.PaletteContent?.GetContentShortTextFont(PaletteState.ContextNormal);
            var disabledFont = _stateDisabled.PaletteContent?.GetContentShortTextFont(PaletteState.Disabled);

            Font = (Enabled ? normalFont : disabledFont)!;
            BackColor = _stateNormal.PaletteBack.GetBackColor1(Enabled? PaletteState.Normal : PaletteState.Disabled);

            var controlsCollection = Controls;
            var state = PaletteState.ContextNormal;
            IPaletteTriple triple = _stateNormal;
            foreach (Control control in controlsCollection)
            {
                if (control.Focused)
                {
                    state = PaletteState.FocusOverride;
                    triple = _stateActive;
                    control.Font = _stateActive.PaletteContent?.GetContentShortTextFont(PaletteState.FocusOverride)!;
                }
                else if (control.Enabled)
                {
                    state = PaletteState.ContextNormal;
                    triple = _stateNormal;
                    // Note: tobitege commented out to avoid unrecoverable exception in System.Drawing, when toggling theme back and forth
                    //control.Font = normalFont!;
                }
                else
                {
                    state = PaletteState.Disabled;
                    triple = _stateDisabled;
                    control.Font = disabledFont!;
                }

                control.ForeColor = triple.PaletteContent!.GetContentShortTextColor1(state);
                control.BackColor = triple.PaletteBack.GetBackColor1(state);
            }

            // Original code caused several themes to have white-on-white text.
            // This has been tested as working against all schemes and fixes all previously
            // observed white-on-white/low-contrast colors!
            // Needed to be moved below the loop!
            HelpForeColor = ContrastColor(HelpBackColor);
            ViewForeColor = ContrastColor(ViewBackColor);

            Invalidate();
        }

        private static Color ContrastColor(Color color)
        {
            // Counting the perceptive luminance
            var a = 1
                     - (((0.299 * color.R)
                         + ((0.587 * color.G) + (0.114 * color.B)))
                        / 255);
            var d = a < 0.5 ? 0 : 255;

            //  dark colours - white font and vice versa
            return Color.FromArgb(d, d, d);
        }

        #endregion
    }
}