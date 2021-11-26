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
    /// Storage of user supplied values not used by Krypton.
    /// </summary>
    public class KryptonPaletteCargo : Storage
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteCargo class.
        /// </summary>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteCargo(NeedPaintHandler needPaint) 
        {
            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Default cargo values
            Color1 = Color.Empty;
            Color2 = Color.Empty;
            Color3 = Color.Empty;
            Color4 = Color.Empty;
            Color5 = Color.Empty;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (Color1 == Color.Empty) &&
                                          (Color2 == Color.Empty) &&
                                          (Color3 == Color.Empty) &&
                                          (Color4 == Color.Empty) &&
                                          (Color5 == Color.Empty) &&
                                          (Font1 == null) &&
                                          (Font2 == null);

        #endregion

        #region Color1
        /// <summary>
        /// Gets and sets a user supplied color value.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("User supplied color value.")]
        [KryptonDefaultColor()]
        [RefreshProperties(RefreshProperties.All)]
        public Color Color1 { get; set; }

        /// <summary>
        /// esets the Color1 property to its default value.
        /// </summary>
        public void ResetColor1()
        {
            Color1 = Color.Empty;
        }
        #endregion

        #region Color2
        /// <summary>
        /// Gets and sets a user supplied color value.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("User supplied color value.")]
        [KryptonDefaultColor()]
        [RefreshProperties(RefreshProperties.All)]
        public Color Color2 { get; set; }

        /// <summary>
        /// esets the Color2 property to its default value.
        /// </summary>
        public void ResetColor2()
        {
            Color2 = Color.Empty;
        }
        #endregion

        #region Color3
        /// <summary>
        /// Gets and sets a user supplied color value.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("User supplied color value.")]
        [KryptonDefaultColor()]
        [RefreshProperties(RefreshProperties.All)]
        public Color Color3 { get; set; }

        /// <summary>
        /// esets the Color3 property to its default value.
        /// </summary>
        public void ResetColor3()
        {
            Color3 = Color.Empty;
        }
        #endregion

        #region Color4
        /// <summary>
        /// Gets and sets a user supplied color value.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("User supplied color value.")]
        [KryptonDefaultColor()]
        [RefreshProperties(RefreshProperties.All)]
        public Color Color4 { get; set; }

        /// <summary>
        /// esets the Color4 property to its default value.
        /// </summary>
        public void ResetColor4()
        {
            Color4 = Color.Empty;
        }
        #endregion

        #region Color5
        /// <summary>
        /// Gets and sets a user supplied color value.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("User supplied color value.")]
        [KryptonDefaultColor()]
        [RefreshProperties(RefreshProperties.All)]
        public Color Color5 { get; set; }

        /// <summary>
        /// esets the Color5 property to its default value.
        /// </summary>
        public void ResetColor5()
        {
            Color5 = Color.Empty;
        }
        #endregion

        #region Font1
        /// <summary>
        /// Gets and sets a user supplied font value.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("User supplied font value.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Font Font1 { get; set; }

        /// <summary>
        /// esets the Font1 property to its default value.
        /// </summary>
        public void ResetFont1()
        {
            Font1 = null;
        }
        #endregion

        #region Font2
        /// <summary>
        /// Gets and sets a user supplied font value.
        /// </summary>
        [KryptonPersist(false)]
        [Category("Visuals")]
        [Description("User supplied font value.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Font Font2 { get; set; }

        /// <summary>
        /// esets the Font2 property to its default value.
        /// </summary>
        public void ResetFont2()
        {
            Font2 = null;
        }
        #endregion
    }
}
