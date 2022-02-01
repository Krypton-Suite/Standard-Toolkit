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
    /// Extends the ViewLayoutRibbonQATContents by providing the definitions that are overflowing the original source.
    /// </summary>
    internal class ViewLayoutRibbonQATFromOverflow : ViewLayoutRibbonQATContents
    {
        #region Instance Fields

        private readonly ViewLayoutRibbonQATContents _contents;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutRibbonQATFromOverflow class.
        /// </summary>
        /// <param name="parentControl">Owning control used to find screen positions.</param>
        /// <param name="ribbon">Owning ribbon control instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        /// <param name="showExtraButton">Should the extra button be shown.</param>
        /// <param name="contents">Source for finding buttons that are overflowing.</param>
        public ViewLayoutRibbonQATFromOverflow(Control parentControl,
                                               KryptonRibbon ribbon,
                                               NeedPaintHandler needPaint,
                                               bool showExtraButton,
                                               ViewLayoutRibbonQATContents contents)
            : base(ribbon, needPaint, showExtraButton)
        {
            Debug.Assert(parentControl != null);
            Debug.Assert(contents != null);
            
            _contents = contents;
            ParentControl = parentControl;
        }
        #endregion

        #region DisplayButtons
        /// <summary>
        /// Returns a collection of all the quick access toolbar definitions.
        /// </summary>
        public override IQuickAccessToolbarButton[] QATButtons 
        { 
            get 
            {
                var qatOverflow = new List<IQuickAccessToolbarButton>();

                // Scan all the defined buttons for ones to show as overflowing
                foreach (IQuickAccessToolbarButton qatButton in Ribbon.QATButtons)
                {
                    // If the button requests to be shown...
                    if (qatButton.GetVisible())
                    {
                        ViewBase qatView = _contents.ViewForButton(qatButton);

                        //...but the view is not displayed, then show on overflow
                        if (qatView is { Visible: false })
                        {
                            qatOverflow.Add(qatButton);
                        }
                    }
                }

                return qatOverflow.ToArray();
            }
        }
        #endregion

        #region ParentControl
        /// <summary>
        /// Gets a reference to the owning control of this element.
        /// </summary>
        /// <returns>Control reference.</returns>
        public override Control ParentControl { get; }

        #endregion
    }
}
