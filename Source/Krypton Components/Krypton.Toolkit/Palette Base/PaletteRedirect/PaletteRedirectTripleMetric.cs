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

using System.Windows.Forms;
using System.Diagnostics;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Redirect back/border/content/metrics based on the incoming state of the request.
    /// </summary>
    public class PaletteRedirectTripleMetric : PaletteRedirectTriple
    {
        #region Instance Fields
        private IPaletteMetric _disabled;
        private IPaletteMetric _normal;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRedirectTripleMetric class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        public PaletteRedirectTripleMetric(IPalette target)
            : this(target, null, null, null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectTripleMetric class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="disableMetric">Redirection for disabled metric requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        /// <param name="normalMetric">Redirection for normal metric requests.</param>
        public PaletteRedirectTripleMetric(IPalette target,
                                           IPaletteTriple disabled, IPaletteMetric disableMetric,
                                           IPaletteTriple normal, IPaletteMetric normalMetric)
            : base(target, disabled, normal)
        {
            // Remember state specific inheritance
            _disabled = disableMetric;
            _normal = normalMetric;
        }
        #endregion

        #region SetRedirectStates
        /// <summary>
        /// Set the redirection states.
        /// </summary>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="disableMetric">Redirection for disabled metric requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        /// <param name="normalMetric">Redirection for normal metric requests.</param>
        public void SetRedirectStates(IPaletteTriple disabled,
                                      IPaletteMetric disableMetric,
                                      IPaletteTriple normal,
                                      IPaletteMetric normalMetric)
        {
            base.SetRedirectStates(disabled, normal);

            _disabled = disableMetric;
            _normal = normalMetric;
        }
        #endregion

        #region ResetRedirectStates
        /// <summary>
        /// Reset the redirection states to null.
        /// </summary>
        public override void ResetRedirectStates()
        {
            base.ResetRedirectStates();

            _disabled = null;
            _normal = null;
        }

        #endregion

        #region Metric
        /// <summary>
        /// Gets an integer metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Integer value.</returns>
        public override int GetMetricInt(PaletteState state, PaletteMetricInt metric)
        {
            IPaletteMetric inherit = GetInherit(state);

            return inherit?.GetMetricInt(state, metric) ?? Target.GetMetricInt(state, metric);
        }

        /// <summary>
        /// Gets a boolean metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric)
        {
            IPaletteMetric inherit = GetInherit(state);

            return inherit?.GetMetricBool(state, metric) ?? Target.GetMetricBool(state, metric);
        }

        /// <summary>
        /// Gets a padding metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Padding value.</returns>
        public override Padding GetMetricPadding(PaletteState state, PaletteMetricPadding metric)
        {
            IPaletteMetric inherit = GetInherit(state);

            return inherit?.GetMetricPadding(state, metric) ?? Target.GetMetricPadding(state, metric);
        }
        #endregion

        #region Implementation
        private IPaletteMetric GetInherit(PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return _disabled;
                case PaletteState.Normal:
                    return _normal;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }
        #endregion
    }
}
