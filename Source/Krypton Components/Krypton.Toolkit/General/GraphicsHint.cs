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
/// Apply a requested smoothing mode to a graphics instance.
/// </summary>
public class GraphicsHint : GlobalId,
    IDisposable
{
    #region Instance Fields
    private readonly Graphics? _graphics;
    private readonly SmoothingMode _smoothingMode;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the GraphicsHint class.
    /// </summary>
    /// <param name="graphics">Graphics context.</param>
    /// <param name="hint">Temporary hint mode to apply.</param>
    public GraphicsHint(Graphics? graphics, PaletteGraphicsHint hint)
    {
        // Cache graphics instance
        _graphics = graphics;

        // Remember current smoothing mode
        _smoothingMode = _graphics!.SmoothingMode;

        // Apply new hint
        switch (hint)
        {
            case PaletteGraphicsHint.None:
                _graphics.SmoothingMode = SmoothingMode.None;
                break;
            case PaletteGraphicsHint.AntiAlias:
                _graphics.SmoothingMode = SmoothingMode.AntiAlias;
                break;
            case PaletteGraphicsHint.HighSpeed:
                _graphics.SmoothingMode = SmoothingMode.HighSpeed;
                break;
            case PaletteGraphicsHint.HighQuality:
                _graphics.SmoothingMode = SmoothingMode.HighQuality;
                break;
            case PaletteGraphicsHint.Inherit:
                // Change nothing!
                break;

            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(hint.ToString());
                break;
        }
    }

    /// <summary>
    /// Reverse the smoothing mode change.
    /// </summary>
    public void Dispose()
    {
        if (_graphics != null)
        {
            try
            {
                // Put back to the original smoothing mode
                _graphics.SmoothingMode = _smoothingMode;
            }
            catch { }
        }
        GC.SuppressFinalize(this);
    }
    #endregion
}