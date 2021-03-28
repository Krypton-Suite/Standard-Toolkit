using System;
using System.Diagnostics;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Allow the palette to be overriden optionally.
    /// </summary>
    public class PaletteTripleOverride : GlobalId,
                                         IPaletteTriple
    {
        #region Intance Fields
        private readonly PaletteBackInheritOverride _overrideBack;
        private readonly PaletteBorderInheritOverride _overrideBorder;
        private readonly PaletteContentInheritOverride _overrideContent;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteTripleOverride class.
        /// </summary>
        /// <param name="normalTriple">Normal palette to use.</param>
        /// <param name="overrideTriple">Override palette to use.</param>
        /// <param name="overrideState">State used by the override.</param>
        public PaletteTripleOverride(IPaletteTriple normalTriple,
                                     IPaletteTriple overrideTriple,
                                     PaletteState overrideState)
        {
            Debug.Assert(normalTriple != null);
            Debug.Assert(overrideTriple != null);

            // Validate incoming references
            if (normalTriple == null)
            {
                throw new ArgumentNullException(nameof(normalTriple));
            }

            if (overrideTriple == null)
            {
                throw new ArgumentNullException(nameof(overrideTriple));
            }

            // Create the triple override instances
            _overrideBack = new PaletteBackInheritOverride(normalTriple.PaletteBack, overrideTriple.PaletteBack);
            _overrideBorder = new PaletteBorderInheritOverride(normalTriple.PaletteBorder, overrideTriple.PaletteBorder);
            _overrideContent = new PaletteContentInheritOverride(normalTriple.PaletteContent, overrideTriple.PaletteContent);

            // Do not apply an override by default
            Apply = false;

            // Always override the state
            Override = true;
            OverrideState = overrideState;
        }            
        #endregion

        #region SetPalettes
        /// <summary>
        /// Update the the normal and override palettes.
        /// </summary>
        /// <param name="normalTriple">New normal palette.</param>
        /// <param name="overrideTriple">New override palette.</param>
        public void SetPalettes(IPaletteTriple normalTriple,
                                IPaletteTriple overrideTriple)
        {
            _overrideBack.SetPalettes(normalTriple.PaletteBack, overrideTriple.PaletteBack);
            _overrideBorder.SetPalettes(normalTriple.PaletteBorder, overrideTriple.PaletteBorder);
            _overrideContent.SetPalettes(normalTriple.PaletteContent, overrideTriple.PaletteContent);
        }
        #endregion

        #region Apply
        /// <summary>
        /// Gets and sets a value indicating if override should be applied.
        /// </summary>
        public bool Apply
        {
            get => _overrideBack.Apply;

            set
            {
                _overrideBack.Apply = value;
                _overrideBorder.Apply = value;
                _overrideContent.Apply = value;
            }
        }
        #endregion

        #region Override
        /// <summary>
        /// Gets and sets a value indicating if override state should be applied.
        /// </summary>
        public bool Override
        {
            get => _overrideBack.Override;

            set
            {
                _overrideBack.Override = value;
                _overrideBorder.Override = value;
                _overrideContent.Override = value;
            }
        }
        #endregion

        #region OverrideState
        /// <summary>
        /// Gets and sets the override palette state to use.
        /// </summary>
        public PaletteState OverrideState
        {
            get => _overrideBack.OverrideState;

            set
            {
                _overrideBack.OverrideState = value;
                _overrideBorder.OverrideState = value;
                _overrideContent.OverrideState = value;
            }
        }
        #endregion

        #region Palette Accessors
        /// <summary>
        /// Gets the background palette.
        /// </summary>
        public IPaletteBack PaletteBack => _overrideBack;

        /// <summary>
        /// Gets the border palette.
        /// </summary>
        public IPaletteBorder PaletteBorder => _overrideBorder;

        /// <summary>
        /// Gets the border palette.
        /// </summary>
        public IPaletteContent PaletteContent => _overrideContent;

        #endregion
    }
}
