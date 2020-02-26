// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2020. All rights reserved. (https://github.com/Wagnerp/Krypton-Toolkit-Suite-NET-Core)
//  Version 5.500.0.0  www.ComponentFactory.com
// *****************************************************************************

using System.ComponentModel;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// Storage for tooltip related properties.
    /// </summary>
    public class NavigatorToolTips : Storage
    {
        #region Instance Fields
        private KryptonNavigator _navigator;
        private MapKryptonPageImage _mapImage;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the NavigatorPopupPage class.
        /// </summary>
        /// <param name="navigator">Reference to owning navigator instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public NavigatorToolTips(KryptonNavigator navigator,
                                NeedPaintHandler needPaint)
        {
            Debug.Assert(navigator != null);
            Debug.Assert(needPaint != null);
            
            // Remember back reference
            _navigator = navigator;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Default values
            AllowPageToolTips = false;
            AllowButtonSpecToolTips = false;
            _mapImage = MapKryptonPageImage.ToolTip;
            MapText = MapKryptonPageText.ToolTipTitle;
            MapExtraText = MapKryptonPageText.ToolTipBody;
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault => (!AllowPageToolTips &&
                                           !AllowButtonSpecToolTips &&
                                           (MapImage == MapKryptonPageImage.ToolTip) &&
                                           (MapText == MapKryptonPageText.ToolTipTitle) &&
                                           (MapExtraText == MapKryptonPageText.ToolTipBody));

        #endregion

        #region AllowPageToolTips
        /// <summary>
        /// Gets and sets a value indicating if tooltips should be displayed for page headers.
        /// </summary>
        [Category("Visuals")]
        [Description("Should tooltips be displayed for page headers.")]
        [DefaultValue(false)]
        public bool AllowPageToolTips { get; set; }

        #endregion

        #region AllowButtonSpecToolTips
        /// <summary>
        /// Gets and sets a value indicating if tooltips should be displayed for button specs.
        /// </summary>
        [Category("Visuals")]
        [Description("Should tooltips be displayed for button specs.")]
        [DefaultValue(false)]
        public bool AllowButtonSpecToolTips { get; set; }

        #endregion

        #region MapImage
        /// <summary>
        /// Gets and sets the mapping used for the tooltip image.
        /// </summary>
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Mapping used for the tooltip image.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(MapKryptonPageImage), "ToolTip")]
        public virtual MapKryptonPageImage MapImage
        {
            get => _mapImage;
            set => _mapImage = value;
        }

        /// <summary>
        /// Resets the MapImage property to its default value.
        /// </summary>
        public void ResetMapImage()
        {
            MapImage = MapKryptonPageImage.ToolTip;
        }
        #endregion

        #region MapText
        /// <summary>
        /// Gets and sets the mapping used for the tooltip text.
        /// </summary>
        [Category("Visuals")]
        [Description("Mapping used for the tooltip text.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(MapKryptonPageText), "ToolTipTitle")]
        public MapKryptonPageText MapText { get; set; }

        /// <summary>
        /// Resets the MapText property to its default value.
        /// </summary>
        public void ResetMapText()
        {
            MapText = MapKryptonPageText.ToolTipTitle;
        }
        #endregion

        #region MapExtraText
        /// <summary>
        /// Gets and sets the mapping used for the tooltip description.
        /// </summary>
        [Category("Visuals")]
        [Description("Mapping used for the tooltip description.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(typeof(MapKryptonPageText), "ToolTipBody")]
        public MapKryptonPageText MapExtraText { get; set; }

        /// <summary>
        /// Resets the MapExtraText property to its default value.
        /// </summary>
        public void ResetMapExtraText()
        {
            MapExtraText = MapKryptonPageText.ToolTipBody;
        }
        #endregion
    }
}
