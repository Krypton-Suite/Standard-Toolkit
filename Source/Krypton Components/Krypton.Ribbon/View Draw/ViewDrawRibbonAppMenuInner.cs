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
/// Extends the ViewLayoutDocker by drawing the ribbon application button inner background.
/// </summary>
internal class ViewDrawRibbonAppMenuInner : ViewLayoutDocker
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private IDisposable? _memento;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonAppMenuInner class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon instance.</param>
    public ViewDrawRibbonAppMenuInner(KryptonRibbon ribbon) => _ribbon = ribbon;

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonAppMenuInner:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_memento != null)
            {
                _memento.Dispose();
                _memento = null;
            }
        }

        base.Dispose(disposing);
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore([DisallowNull] RenderContext context)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        // Draw the application menu outer background
        _memento = context.Renderer.RenderRibbon.DrawRibbonBack(_ribbon.RibbonShape, context, ClientRectangle, State,
            _ribbon.StateCommon.RibbonAppMenuInner,
            VisualOrientation.Top, _memento);
    }
    #endregion
}