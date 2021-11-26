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
    /// <summary>
    /// Implement storage for palette border,background and separator padding.
    /// </summary>
    public class PaletteSeparatorPadding : PaletteDouble,
                                           IPaletteMetric
                                            
    {
        #region Instance Fields
        private readonly IPaletteMetric _inherit;
        private Padding _separatorPadding;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteSeparatorPadding class.
        /// </summary>
        /// <param name="inheritDouble">Source for inheriting border and background values.</param>
        /// <param name="inheritMetric">Source for inheriting metric values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteSeparatorPadding(IPaletteDouble inheritDouble,
                                       IPaletteMetric inheritMetric,
                                       NeedPaintHandler needPaint)
            : base(inheritDouble, needPaint)
        {
            Debug.Assert(inheritDouble != null);
            Debug.Assert(inheritMetric != null);

            // Remember the inheritance
            _inherit = inheritMetric;

            // Set default value for padding property
            _separatorPadding = CommonHelper.InheritPadding;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => base.IsDefault &&
                                           Padding.Equals(CommonHelper.InheritPadding);

        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">Which state to populate from.</param>
        /// <param name="metric">Which metric should be used for padding.</param>
        public void PopulateFromBase(PaletteState state, PaletteMetricPadding metric)
        {
            base.PopulateFromBase(state);
            Padding = _inherit.GetMetricPadding(state, metric);
        }
        #endregion

        #region Border
        /// <summary>
        /// Gets access to the border palette details.
        /// </summary>
        [KryptonPersist]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new PaletteBorder Border => base.Border;

        #endregion

        #region Padding
        /// <summary>
        /// Gets the padding used to position the separator.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Padding used to position the separator.")]
        [DefaultValue(typeof(Padding), "-1,-1,-1,-1")]
        [RefreshProperties(RefreshProperties.All)]
        public Padding Padding
        {
            get => _separatorPadding;

            set
            {
                if (_separatorPadding != value)
                {
                    _separatorPadding = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the Padding to the default value.
        /// </summary>
        public void ResetPadding()
        {
            Padding = CommonHelper.InheritPadding;
        }
        #endregion

        #region IPaletteMetric
        /// <summary>
        /// Gets an integer metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Integer value.</returns>
        public int GetMetricInt(PaletteState state, PaletteMetricInt metric) =>
            // Pass onto the inheritance
            _inherit.GetMetricInt(state, metric);

        /// <summary>
        /// Gets a boolean metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>InheritBool value.</returns>
        public InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric) =>
            // Pass onto the inheritance
            _inherit.GetMetricBool(state, metric);

        /// <summary>
        /// Gets a padding metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Padding value.</returns>
        public Padding GetMetricPadding(PaletteState state, PaletteMetricPadding metric)
        {
            // Is this the metric we provide?
            if ((metric == PaletteMetricPadding.SeparatorPaddingLowProfile) ||
                (metric == PaletteMetricPadding.SeparatorPaddingHighProfile) ||
                (metric == PaletteMetricPadding.SeparatorPaddingHighInternalProfile) 
                || (metric == PaletteMetricPadding.SeparatorPaddingCustom1)
                || (metric == PaletteMetricPadding.SeparatorPaddingCustom2)
                || (metric == PaletteMetricPadding.SeparatorPaddingCustom3)
                )
            {
                // If the user has defined an actual value to use
                if (!Padding.Equals(CommonHelper.InheritPadding))
                {
                    return Padding;
                }
            }

            // Pass onto the inheritance
            return _inherit.GetMetricPadding(state, metric);
        }
        #endregion
    }
}
