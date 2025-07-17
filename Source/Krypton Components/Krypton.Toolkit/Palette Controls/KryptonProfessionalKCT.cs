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

internal class KryptonProfessionalKCT : KryptonColorTable
{
    #region Instance Fields
    private readonly Color[] _colors;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonProfessionalKCT class.
    /// </summary>
    /// <param name="colors">Set of colors to customize with.</param>
    /// <param name="useSystemColors">Should be forced to use system colors.</param>
    /// <param name="palette">Reference to associated palette.</param>
    public KryptonProfessionalKCT([DisallowNull] Color[] colors, 
        bool useSystemColors,
        PaletteBase palette)
        : base(palette)
    {
        Debug.Assert(colors != null);
        _colors = colors!;
        UseSystemColors = useSystemColors;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the starting color of the gradient used in the Header1.
    /// </summary>
    public Color Header1Begin => _colors[0];

    /// <summary>
    /// Gets the end color of the gradient used in the Header1.
    /// </summary>
    public Color Header1End => _colors[1];

    #endregion
}