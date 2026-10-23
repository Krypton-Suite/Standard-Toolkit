#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Extends the ViewLayoutRibbonQATContents by providing the definitions from the ribbon control itself.
/// </summary>
internal class ViewLayoutRibbonQATFromRibbon : ViewLayoutRibbonQATContents
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutRibbonQATFromRibbon class.
    /// </summary>
    /// <param name="ribbon">Owning ribbon control instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    /// <param name="showExtraButton">Should the extra button be shown.</param>
    public ViewLayoutRibbonQATFromRibbon(KryptonRibbon ribbon,
        NeedPaintHandler needPaint,
        bool showExtraButton)
        : base(ribbon, needPaint, showExtraButton)
    {
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
            var qatButtons = new IQuickAccessToolbarButton[Ribbon.QATButtons.Count];

            // Copy all the entries into the new array 
            Ribbon.QATButtons.CopyTo(qatButtons, 0);

            return qatButtons;
        }
    }
    #endregion
}