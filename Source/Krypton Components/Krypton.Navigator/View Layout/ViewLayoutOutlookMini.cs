#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Navigator;

/// <summary>
/// View element that knows how to enforce the visible state of the stacked items.
/// </summary>
internal class ViewLayoutOutlookMini : ViewLayoutDocker
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLayoutOutlookMini class.
    /// </summary>
    /// <param name="viewBuilder">View builder reference.</param>
    public ViewLayoutOutlookMini([DisallowNull] ViewBuilderOutlookBase viewBuilder)
    {
        Debug.Assert(viewBuilder is not null);
        ViewBuilder = viewBuilder ?? throw new ArgumentNullException(nameof(viewBuilder));
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $"ViewLayoutOutlookMini:{Id}";

    #endregion

    #region ViewBuilder
    /// <summary>
    /// Gets access to the associated view builder.
    /// </summary>
    public ViewBuilderOutlookBase ViewBuilder
    {
        [DebuggerStepThrough]
        get;
    }

    #endregion

    #region Layout
    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout(ViewLayoutContext context)
    {
        // Make all stacking items that should be visible are visible
        ViewBuilder.UnShrinkAppropriatePages();

        // Let base class continue with standard layout
        base.Layout(context);
    }
    #endregion
}