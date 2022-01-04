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


namespace Krypton.Ribbon
{
    /// <summary>
    /// Implementation for the fixed mdi child pendant buttons.
    /// </summary>
    public abstract class ButtonSpecMdiChildFixed : ButtonSpec
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecMdiChildFixed class.
        /// </summary>
        /// <param name="fixedStyle">Fixed style to use.</param>
        public ButtonSpecMdiChildFixed(PaletteButtonSpecStyle fixedStyle) =>
            // Fix the type
            ProtectedType = fixedStyle;

        #endregion   

        #region AllowComponent
        /// <summary>
        /// Gets a value indicating if the component is allowed to be selected at design time.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool AllowComponent => false;

        #endregion

        #region MdiChild
        /// <summary>
        /// Gets access to the owning krypton form.
        /// </summary>
        public Form MdiChild { get; set; }

        #endregion

        #region ButtonSpecStype
        /// <summary>
        /// Gets and sets the actual type of the button.
        /// </summary>
        public PaletteButtonSpecStyle ButtonSpecType
        {
            get => ProtectedType;
            set => ProtectedType = value;
        }
        #endregion

        #region IButtonSpecValues
        /// <summary>
        /// Gets the button style.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button style.</returns>
        public override ButtonStyle GetStyle(IPalette palette) => ButtonStyle.ButtonSpec;

        #endregion
    }
}
