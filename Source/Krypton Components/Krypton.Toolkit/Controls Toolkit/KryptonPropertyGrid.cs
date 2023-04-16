#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2021 - 2023. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
namespace Krypton.Toolkit
{
    [Designer(typeof(KryptonPropertyGridDesigner))]
    [ToolboxBitmap(typeof(PropertyGrid), "ToolboxBitmaps.KryptonPropertyGridVersion2.bmp")]
    [ToolboxItem(true)]
    public class KryptonPropertyGrid : PropertyGrid
    {
        #region Instance Fields

        private PaletteBase? _palette;

        private readonly PaletteRedirect _paletteRedirect;
        private readonly PaletteInputControlTripleRedirect _stateCommon;
        private readonly PaletteInputControlTripleStates _stateNormal;
        private readonly PaletteInputControlTripleStates _stateDisabled;
        private readonly PaletteInputControlTripleStates _stateActive;


        #endregion

        #region Identity

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
            _stateCommon = new PaletteInputControlTripleRedirect(_paletteRedirect, PaletteBackStyle.InputControlStandalone, PaletteBorderStyle.InputControlStandalone, PaletteContentStyle.InputControlStandalone, null);
            _stateDisabled = new PaletteInputControlTripleStates(_stateCommon, null);
            _stateNormal = new PaletteInputControlTripleStates(_stateCommon, null);
            _stateActive = new PaletteInputControlTripleStates(_stateCommon, null);

            InitColours();
        }
        #endregion

        #region Public

        /// <summary>Refreshes the colours.</summary>
        public void RefreshColours() => InitColours();

        #endregion

        #region Implementation

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

            if (_palette != null)
            {
                HelpBackColor = _palette.ColorTable.MenuStripGradientBegin;

                HelpForeColor = _palette.ColorTable.StatusStripText;

                LineColor = _palette.ColorTable.ToolStripGradientMiddle;

                CategoryForeColor = _palette.ColorTable.StatusStripText;
            }

            var normalFont = _stateNormal.PaletteContent!.GetContentShortTextFont(PaletteState.ContextNormal);
            var disabledFont = _stateDisabled.PaletteContent!.GetContentShortTextFont(PaletteState.Disabled);

            Font = Enabled ? normalFont : disabledFont;
            BackColor = _stateNormal.PaletteBack.GetBackColor1(PaletteState.Disabled);

            ControlCollection controlsCollection = Controls;
            foreach (Control control in controlsCollection)
            {
                IPaletteTriple triple;
                PaletteState state;
                if (control.Focused)
                {
                    state = PaletteState.FocusOverride;
                    triple = _stateActive;
                    control.Font = _stateActive.PaletteContent!.GetContentShortTextFont(PaletteState.FocusOverride);
                }
                else if (control.Enabled)
                {
                    state = PaletteState.ContextNormal;
                    triple = _stateNormal;
                    control.Font = normalFont;
                }
                else
                {
                    state = PaletteState.Disabled;
                    triple = _stateDisabled;
                    control.Font = disabledFont;
                }

                control.ForeColor = triple.PaletteContent!.GetContentShortTextColor1(state);
                control.BackColor = triple.PaletteBack.GetBackColor1(state);
            }

            Invalidate();

        }

        #endregion
    }
}