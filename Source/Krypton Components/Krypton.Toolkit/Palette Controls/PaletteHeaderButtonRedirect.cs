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

using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Redirect storage for button metrics.
    /// </summary>
    public class PaletteHeaderButtonRedirect : PaletteTripleMetricRedirect
    {
        #region Instance Fields
        private readonly PaletteRedirect _redirect;
        private Padding _buttonPadding;
        private int _buttonEdgeInset;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteHeaderButtonRedirect class.
        /// </summary>
        /// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="backStyle">Initial background style.</param>
        /// <param name="borderStyle">Initial border style.</param>
        /// <param name="contentStyle">Initial content style.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteHeaderButtonRedirect(PaletteRedirect redirect,
                                           PaletteBackStyle backStyle,
                                           PaletteBorderStyle borderStyle,
                                           PaletteContentStyle contentStyle,
                                           NeedPaintHandler needPaint)
            : base(redirect, backStyle, borderStyle, contentStyle, needPaint)
        {
            Debug.Assert(redirect != null);

            // Remember the redirect reference
            _redirect = redirect;

            // Set default value for padding property
            _buttonPadding = CommonHelper.InheritPadding;
            _buttonEdgeInset = -1;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (base.IsDefault &&
                                           ButtonPadding.Equals(CommonHelper.InheritPadding) &&
                                           (ButtonEdgeInset == -1));

        #endregion

        #region ButtonEdgeInset
        /// <summary>
        /// Gets the sets how far to inset buttons from the header edge.
        /// </summary>
        [Category("Visuals")]
        [Description("How far to inset buttons from the header edge.")]
        [DefaultValue(-1)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public int ButtonEdgeInset
        {
            get => _buttonEdgeInset;

            set
            {
                if (_buttonEdgeInset != value)
                {
                    _buttonEdgeInset = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the ButtonEdgeInset to the default value.
        /// </summary>
        public void ResetButtonEdgeInset()
        {
            ButtonEdgeInset = -1;
        }
        #endregion

        #region ButtonPadding
        /// <summary>
        /// Gets and sets the padding used around each button on the header.
        /// </summary>
        [Category("Visuals")]
        [Description("Padding used around each button on the header.")]
        [DefaultValue(typeof(Padding), "-1,-1,-1,-1")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Padding ButtonPadding
        {
            get => _buttonPadding;

            set
            {
                if (_buttonPadding != value)
                {
                    _buttonPadding = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the ButtonPadding to the default value.
        /// </summary>
        public void ResetButtonPadding()
        {
            ButtonPadding = CommonHelper.InheritPadding;
        }
        #endregion

        #region IPaletteMetric
        /// <summary>
        /// Gets an integer metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Integer value.</returns>
        public override int GetMetricInt(PaletteState state, PaletteMetricInt metric)
        {
            // Is this the metric we provide?
            if ((metric == PaletteMetricInt.HeaderButtonEdgeInsetPrimary) ||
                (metric == PaletteMetricInt.HeaderButtonEdgeInsetSecondary) ||
                (metric == PaletteMetricInt.HeaderButtonEdgeInsetDockInactive) ||
                (metric == PaletteMetricInt.HeaderButtonEdgeInsetDockActive) ||
                (metric == PaletteMetricInt.HeaderButtonEdgeInsetForm) ||
                (metric == PaletteMetricInt.HeaderButtonEdgeInsetInputControl) ||
                (metric == PaletteMetricInt.HeaderButtonEdgeInsetCustom1) 
                ||(metric == PaletteMetricInt.HeaderButtonEdgeInsetCustom2)
                || (metric == PaletteMetricInt.HeaderButtonEdgeInsetCustom3)
                )
            {
                // If the user has defined an actual value to use
                if (ButtonEdgeInset != -1)
                {
                    return ButtonEdgeInset;
                }
            }

            // Pass onto the inheritance
            return _redirect.GetMetricInt(state, metric);
        }

        /// <summary>
        /// Gets a boolean metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric)
        {
            // Always pass onto the inheritance
            return _redirect.GetMetricBool(state, metric);
        }

        /// <summary>
        /// Gets a padding metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Padding value.</returns>
        public override Padding GetMetricPadding(PaletteState state, PaletteMetricPadding metric)
        {
            // Is this the metric we provide?
            if ((metric == PaletteMetricPadding.HeaderButtonPaddingPrimary) ||
                (metric == PaletteMetricPadding.HeaderButtonPaddingSecondary) ||
                (metric == PaletteMetricPadding.HeaderButtonPaddingDockInactive) ||
                (metric == PaletteMetricPadding.HeaderButtonPaddingDockActive) ||
                (metric == PaletteMetricPadding.HeaderButtonPaddingForm) ||
                (metric == PaletteMetricPadding.HeaderButtonPaddingInputControl) ||
                (metric == PaletteMetricPadding.HeaderButtonPaddingCustom1) 
                ||(metric == PaletteMetricPadding.HeaderButtonPaddingCustom2)
                || (metric == PaletteMetricPadding.HeaderButtonPaddingCustom3)
                )
            {
                // If the user has defined an actual value to use
                if (!ButtonPadding.Equals(CommonHelper.InheritPadding))
                {
                    return ButtonPadding;
                }
            }

            // Pass onto the inheritance
            return _redirect.GetMetricPadding(state, metric);
        }
        #endregion
    }
}
