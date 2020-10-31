// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System.Drawing;
using System.ComponentModel;

namespace Krypton.Toolkit
{
    /// <summary>
    /// Storage for ribbon text values.
    /// </summary>
    public class PaletteRibbonText : Storage,
                                     IPaletteRibbonText
    {
        #region Instance Fields
        private IPaletteRibbonText _inheritText;
        private Color _textColor;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRibbonText class.
        /// </summary>
        /// <param name="needPaint">Delegate for notifying changes in value.</param>
        public PaletteRibbonText(NeedPaintHandler needPaint)
            : this(null, needPaint)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRibbonText class.
        /// </summary>
        /// <param name="inheritText">Source for inheriting text values.</param>
        /// <param name="needPaint">Delegate for notifying changes in value.</param>
        public PaletteRibbonText(IPaletteRibbonText inheritText,
                                 NeedPaintHandler needPaint) 
        {
            // Remember inheritance
            _inheritText = inheritText;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Define default values
            _textColor = Color.Empty;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (TextColor == Color.Empty);

        #endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        public void SetInherit(IPaletteRibbonText inheritText)
        {
            if (_inheritText != null)
            {
                _inheritText = inheritText;
            }
        }
        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">Palette state to use when populating.</param>
        public void PopulateFromBase(PaletteState state) => TextColor = GetRibbonTextColor(state);

        #endregion

        #region TextColor
        /// <summary>
        /// Gets and sets the color for the item text.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("Color for the text.")]
        [DefaultValue(typeof(Color), "")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Color TextColor
        {
            get => _textColor;

            set
            {
                if (_textColor != value)
                {
                    _textColor = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the TextColor to the default value.
        /// </summary>
        public void ResetTextColor() => TextColor = Color.Empty;

        /// <summary>
        /// Gets the tab color for the item text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonTextColor(PaletteState state) => TextColor != Color.Empty
            ? TextColor
            : (_inheritText?.GetRibbonTextColor(state) ?? Color.Empty);

        #endregion
    }
}
