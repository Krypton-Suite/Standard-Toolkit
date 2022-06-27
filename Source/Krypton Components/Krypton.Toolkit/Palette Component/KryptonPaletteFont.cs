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
    /// <summary>Storage of user supplied font values, not used by Krypton.</summary>
    public class KryptonPaletteFont : Storage
    {
        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonPaletteFont" /> class.</summary>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteFont(NeedPaintHandler needPaint)
        {
            NeedPaint = needPaint;
        }

        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (Font1 == new Font("Segoe UI", 9f)) && (Font2 == new Font("Segoe UI", 9f));

        #endregion

        #region Font1
        /// <summary>
        /// Gets and sets a user supplied font value.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"User supplied font value.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Font Font1 { get; set; }

        /// <summary>
        /// Resets the Font1 property to its default value.
        /// </summary>
        public void ResetFont1() => Font1 = new Font("Segoe UI", 9f);

        #endregion

        #region Font2
        /// <summary>
        /// Gets and sets a user supplied font value.
        /// </summary>
        [KryptonPersist(false)]
        [Category(@"Visuals")]
        [Description(@"User supplied font value.")]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public Font Font2 { get; set; }

        /// <summary>
        /// Resets the Font2 property to its default value.
        /// </summary>
        public void ResetFont2() => Font2 = new Font("Segoe UI", 9f);

        #endregion
    }
}
