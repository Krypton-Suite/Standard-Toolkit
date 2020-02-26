// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  Created by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2020 - 2020. All rights reserved. (https://github.com/Wagnerp/Krypton-Toolkit-Suite-NET-Core)
//  Version 5.500.0.0  www.ComponentFactory.com
// *****************************************************************************

using System.ComponentModel;
using System.Drawing;

namespace ComponentFactory.Krypton.Toolkit.Values
{
    /// <summary>
    ///Be More WPF like...
    /// https://docs.microsoft.com/en-us/dotnet/framework/wpf/controls/popup-placement-behavior?view=netframework-4.7.2
    /// https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.tooltip.placement?view=netframework-4.7.2
    /// 
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    public class PopupPositionValues : Storage
    {
        #region Identity
        /// <summary>
        /// 
        /// </summary>
        public PopupPositionValues()
        {
            Reset();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Reset()
        {
            ResetPlacementMode();
            ResetPlacementTarget();
            ResetPlacementRectangle();
        }

        #endregion Identity

        /// <summary>
        /// 
        /// </summary>
        [Description("Describes the placement of where a Popup control appears on the screen.")]
        [DefaultValue(typeof(PlacementMode), "Bottom")]
        public PlacementMode PlacementMode { get; set; }

        private bool ShouldSerializePlacementMode()
        {
            return PlacementMode != PlacementMode.Bottom;
        }

        /// <summary>
        /// Resets the PlacementMode property to its default value.
        /// </summary>
        public void ResetPlacementMode()
        {
            PlacementMode = PlacementMode.Bottom;
        }

        /// <summary>
        /// 
        /// </summary>
        [Description("The element relative to which the Popup is positioned when it opens.")]
        public ViewBase PlacementTarget { get; set; }

        private bool ShouldSerializePlacementTarget()
        {
            return PlacementTarget != null;
        }

        /// <summary>
        /// Resets the PlacementTarget property to its default value.
        /// </summary>
        public void ResetPlacementTarget()
        {
            PlacementTarget = null;
        }

        /// <summary>
        /// 
        /// </summary>
        [Description("The rectangle relative to which the Popup control is positioned when it opens.")]
        public Rectangle PlacementRectangle { get; set; }

        private bool ShouldSerializePlacementRectangle()
        {
            return !PlacementRectangle.IsEmpty;
        }

        /// <summary>
        /// Resets the ToolTipStyle property to its default value.
        /// </summary>
        public void ResetPlacementRectangle()
        {
            PlacementRectangle = new Rectangle();
        }

        #region Default Values
        /// <summary>
        /// 
        /// </summary>
        public override bool IsDefault => (!ShouldSerializePlacementMode()
                                         && !ShouldSerializePlacementTarget()
                                         && !ShouldSerializePlacementRectangle()
                                         );
        #endregion Default Values
    }
}
