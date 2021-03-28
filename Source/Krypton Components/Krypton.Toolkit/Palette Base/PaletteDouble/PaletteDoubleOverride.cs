using System;
using System.Diagnostics;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Allow a palette to be overriden optionally.
    /// </summary>
    public class PaletteDoubleOverride : GlobalId,
                                         IPaletteDouble
    {
        #region Intance Fields
        private readonly PaletteBackInheritOverride _overrideBack;
        private readonly PaletteBorderInheritOverride _overrideBorder;
        #endregion

        #region Identity

        /// <summary>
        /// Initialize a new instance of the PaletteDoubleOverride class.
        /// </summary>
        /// <param name="normalTriple">Normal palette to use.</param>
        /// <param name="overrideTriple">Override palette to use.</param>
        /// <param name="overrideState">State used by the override.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public PaletteDoubleOverride(IPaletteDouble normalTriple,
                                     IPaletteDouble overrideTriple,
                                     PaletteState overrideState)
        {
            Debug.Assert(normalTriple != null);
            Debug.Assert(overrideTriple != null);

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

            // Do not apply an override by default
            Apply = false;

            // Always override the state
            Override = true;
            OverrideState = overrideState;
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

        #endregion
    }
}
