﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Redirect storage for a palette with metrics.
    /// </summary>
    public class PaletteMetricRedirect : Storage,
                                         IPaletteMetric
    {
        #region Instance Fields
        private PaletteRedirect? _redirect;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteMetricRedirect class.
        /// </summary>
        /// <param name="redirect">inheritance redirection instance.</param>
        public PaletteMetricRedirect([DisallowNull] PaletteRedirect redirect)
        {
            Debug.Assert(redirect != null);

            // Remember the redirect reference
            _redirect = redirect;
        }
        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public virtual void SetRedirector(PaletteRedirect? redirect)
        {
            _redirect = redirect;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => true;

        #endregion

        #region IPaletteMetric
        /// <summary>
        /// Gets an integer metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Integer value.</returns>
        public virtual int GetMetricInt(PaletteState state, PaletteMetricInt metric) =>
            // Pass onto the inheritance
            _redirect.GetMetricInt(state, metric);

        /// <summary>
        /// Gets a boolean metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>InheritBool value.</returns>
        public virtual InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric) =>
            // Pass onto the inheritance
            _redirect.GetMetricBool(state, metric);

        /// <summary>
        /// Gets a padding metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Padding value.</returns>
        public virtual Padding GetMetricPadding(PaletteState state, PaletteMetricPadding metric) =>
            // Pass onto the inheritance
            _redirect.GetMetricPadding(state, metric);

        #endregion
    }
}
