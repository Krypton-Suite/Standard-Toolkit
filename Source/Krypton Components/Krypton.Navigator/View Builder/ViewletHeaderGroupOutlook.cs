// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System.ComponentModel;
using Krypton.Toolkit;

namespace Krypton.Navigator
{
    /// <summary>
    /// Provides HeaderGroup functionality modified to work in the Outlook mode.
    /// </summary>
    internal class ViewletHeaderGroupOutlook : ViewletHeaderGroup
    {
        #region Instance Fields
        private readonly bool _full;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewletHeaderGroupOutlook class.
        /// </summary>
        /// <param name="navigator">Reference to navigator instance.</param>
        /// <param name="redirector">Palette redirector.</param>
        /// <param name="needPaintDelegate">Delegate for notifying paint requests.</param>
        public ViewletHeaderGroupOutlook(KryptonNavigator navigator,
                                         PaletteRedirect redirector,
                                         NeedPaintHandler needPaintDelegate)
            : base(navigator, redirector, needPaintDelegate)
        {
            // Are we using the full or mini outlook mode.
            _full = (navigator.NavigatorMode == NavigatorMode.OutlookFull);
        }
        #endregion

        #region Public
        /// <summary>
        /// Process the change in a property that might effect the viewlet.
        /// </summary>
        /// <param name="e">Property changed details.</param>
        public override void ViewBuilderPropertyChanged(PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "HeaderSecondaryVisibleOutlook":
                    // Call base class but update the standard visible property
                    e = new PropertyChangedEventArgs("HeaderVisibleSecondary");
                    break;
            }

            // Let base class handle property
            base.ViewBuilderPropertyChanged(e);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets the visible state of the secondary header.
        /// </summary>
        /// <returns>Boolean value.</returns>
        protected override bool GetHeaderSecondaryVisible()
        {
            // Work out the correct visiblity value to use
            switch (Navigator.Outlook.HeaderSecondaryVisible)
            {
                case InheritBool.Inherit:
                    return Navigator.Header.HeaderVisibleSecondary;
                case InheritBool.True:
                    return true;
                case InheritBool.False:
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets the source of the primary header values.
        /// </summary>
        /// <returns></returns>
        protected override IContentValues GetPrimaryValues() =>
            _full ? Navigator.Header.HeaderValuesPrimary : CommonHelper.NullContentValues;

        /// <summary>
        /// Gets the source of the secondary header values.
        /// </summary>
        /// <returns></returns>
        protected override IContentValues GetSecondaryValues() =>
            _full ? Navigator.Header.HeaderValuesSecondary : CommonHelper.NullContentValues;

        #endregion
    }
}
