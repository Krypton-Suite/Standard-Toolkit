﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Allows the user to change themes using a <see cref="KryptonComboBox"/>.
    /// </summary>
    /// <seealso cref="Krypton.Toolkit.KryptonComboBox" />
    public class KryptonThemeComboBox : KryptonComboBox
    {
        #region Instance Fields
        private KryptonManager _internalKryptonManager;
        #endregion

        #region Public
        /// <summary>Gets or sets the krypton manager.</summary>
        /// <value>The krypton manager.</value>
        public KryptonManager KryptonManager
        {
            get => _internalKryptonManager;

            set 
            {
                if (_internalKryptonManager == null)
                {
                    _internalKryptonManager = new KryptonManager();
                }
                else
                {
                    _internalKryptonManager = value;
                }
            }
        }
        #endregion

        #region Identity
        /// <summary>Initializes a new instance of the <see cref="KryptonThemeComboBox" /> class.</summary>
        public KryptonThemeComboBox()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;

            ThemeManager.PropagateThemeSelector(this);

            SelectedIndex = 24;
        }
        #endregion

        #region Protected
        /// <summary>Raises the SelectedIndexChanged event.</summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            ThemeManager.ApplyGlobalTheme(_internalKryptonManager, ThemeManager.ApplyThemeMode(Text));

            base.OnSelectedIndexChanged(e);
        }

        #endregion
    }
}