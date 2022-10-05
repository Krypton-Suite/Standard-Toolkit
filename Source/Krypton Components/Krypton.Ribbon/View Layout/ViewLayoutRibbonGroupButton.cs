﻿namespace Krypton.Ribbon
{
    /// <summary>
    /// Layout area for the group button.
    /// </summary>
    internal class ViewLayoutRibbonGroupButton : ViewLayoutDocker
    {
        #region Instance Fields
        private readonly ViewDrawRibbonGroupDialogButton _groupButton;
        private readonly ViewLayoutRibbonCenter _centerButton;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutRibbonGroupButton class.
        /// </summary>
        /// <param name="ribbon">Owning control instance.</param>
        /// <param name="ribbonGroup">Reference to ribbon group this represents.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ViewLayoutRibbonGroupButton(KryptonRibbon ribbon,
                                           KryptonRibbonGroup ribbonGroup,
                                           NeedPaintHandler needPaint)
        {
            _groupButton = new ViewDrawRibbonGroupDialogButton(ribbon, ribbonGroup, needPaint);
            _centerButton = new ViewLayoutRibbonCenter
            {

                // Fill remainder with the actual button, but centered within space
                _groupButton
            };
            Add(_centerButton, ViewDockStyle.Fill);
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString() =>
            // Return the class name and instance identifier
            "ViewLayoutRibbonGroupButton:" + Id;

        #endregion

        #region DialogButtonController
        /// <summary>
        /// Gets access to the controller used for the button.
        /// </summary>
        public DialogLauncherButtonController DialogButtonController => _groupButton.DialogButtonController;

        #endregion

        #region GetFocusView
        /// <summary>
        /// Gets the view to use for the group dialog button.
        /// </summary>
        /// <returns>ViewBase if valid as a focus item; otherwise false.</returns>
        public ViewBase GetFocusView()
        {
            if (Visible && Enabled && _groupButton.Visible && _groupButton.Enabled)
            {
                return _groupButton;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
