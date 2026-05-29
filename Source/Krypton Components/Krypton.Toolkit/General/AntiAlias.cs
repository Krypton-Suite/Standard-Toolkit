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
/// Set the SmoothingMode=AntiAlias until instance disposed.
/// </summary>
public class AntiAlias : GlobalId,
    IDisposable
{
    #region Instance Fields
    private readonly Graphics? _g;
    private readonly SmoothingMode _old;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the UseAntiAlias class.
    /// </summary>
    /// <param name="g">Graphics instance.</param>
    public AntiAlias(Graphics? g)
    {
        _g = g;
        _old = _g!.SmoothingMode;
        _g.SmoothingMode = SmoothingMode.AntiAlias;
    }

    /// <summary>
    /// Revert the SmoothingMode back to original setting.
    /// </summary>
    public void Dispose()
    {
        if (_g != null)
        {
            try
            {
                _g.SmoothingMode = _old;
            }
            catch { }
        }
        GC.SuppressFinalize(this);
    }
    #endregion
}