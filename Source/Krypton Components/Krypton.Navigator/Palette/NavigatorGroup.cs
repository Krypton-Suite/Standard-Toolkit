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


namespace Krypton.Navigator
{
    /// <summary>
    /// Storage for group related properties.
    /// </summary>
    public class NavigatorGroup : Storage
    {
        #region Instance Fields
        private readonly KryptonNavigator _navigator;
        private PaletteBackStyle _groupBackStyle;
        private PaletteBorderStyle _groupBorderStyle;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the NavigatorGroup class.
        /// </summary>
        /// <param name="navigator">Reference to owning navigator instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public NavigatorGroup(KryptonNavigator navigator,
                              NeedPaintHandler needPaint)
        {
            Debug.Assert(navigator != null);
            
            // Remember back reference
            _navigator = navigator;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Default values
            _groupBackStyle = PaletteBackStyle.ControlClient;
            _groupBorderStyle = PaletteBorderStyle.ControlClient;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => ((GroupBackStyle == PaletteBackStyle.ControlClient) &&
                                           (GroupBorderStyle == PaletteBorderStyle.ControlClient));

        #endregion

        #region GroupBackStyle
        /// <summary>
        /// Gets and sets the group back style.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Group back style.")]
        [DefaultValue(typeof(PaletteBackStyle), "ControlClient")]
        public PaletteBackStyle GroupBackStyle
        {
            get => _groupBackStyle;

            set
            {
                if (_groupBackStyle != value)
                {
                    _groupBackStyle = value;
                    _navigator.OnViewBuilderPropertyChanged(nameof(GroupBackStyle));
                }
            }
        }
        #endregion

        #region GroupBorderStyle
        /// <summary>
        /// Gets and sets the group border style.
        /// </summary>
        [Category(@"Visuals")]
        [Description(@"Group border style.")]
        [DefaultValue(typeof(PaletteBorderStyle), "ControlClient")]
        public PaletteBorderStyle GroupBorderStyle
        {
            get => _groupBorderStyle;

            set
            {
                if (_groupBorderStyle != value)
                {
                    _groupBorderStyle = value;
                    _navigator.OnViewBuilderPropertyChanged(nameof(GroupBorderStyle));
                }
            }
        }
        #endregion
    }
}
