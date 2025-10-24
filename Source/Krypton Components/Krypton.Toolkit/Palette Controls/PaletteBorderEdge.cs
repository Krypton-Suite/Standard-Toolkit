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

namespace Krypton.Toolkit;

/// <summary>
/// Implement storage for palette border edge details.
/// </summary>
public class PaletteBorderEdge : PaletteBack
{
    #region Instance Fields
    private readonly PaletteBorderEdgeRedirect _inherit;
    private int _borderWidth;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteBorderEdge class.
    /// </summary>
    /// <param name="inherit">Source for inheriting defaulted values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteBorderEdge([DisallowNull] PaletteBorderEdgeRedirect inherit,
        NeedPaintHandler? needPaint)
        : base(inherit, needPaint)
    {
        Debug.Assert(inherit != null);

        // Remember inheritance
        _inherit = inherit!;

        // Default properties
        _borderWidth = -1;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (_borderWidth == -1) && base.IsDefault;

    #endregion

    #region Width
    /// <summary>
    /// Gets and sets the border width.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"Visuals")]
    [Description(@"Border width.")]
    [DefaultValue(-1)]
    [RefreshProperties(RefreshProperties.All)]
    public int Width
    {
        get => _borderWidth;

        set
        {
            if (value != _borderWidth)
            {
                _borderWidth = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets the border width.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Border width.</returns>
    public int GetBorderWidth(PaletteState state) => Width != -1 ? Width : _inherit.GetBorderWidth(state);

    #endregion
}