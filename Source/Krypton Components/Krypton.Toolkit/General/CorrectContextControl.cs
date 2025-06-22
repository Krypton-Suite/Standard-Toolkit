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
/// Temporary setup of the provided control in the context.
/// </summary>
public class CorrectContextControl : IDisposable
{
    #region Instance Fields
    private readonly ViewLayoutContext _context;
    private readonly Control _startControl;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the CorrectContextControl class.
    /// </summary>
    /// <param name="context">Context to update.</param>
    /// <param name="control">Actual parent control instance.</param>
    public CorrectContextControl([DisallowNull] ViewLayoutContext context,
        Control control)
    {
        Debug.Assert(context is not null);

        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Control is null)
        {
            throw new ArgumentNullException(nameof(context.Control));
        }

        // Remember incoming context
        _context = context;

        // Remember staring setting
        _startControl = context.Control;

        // Update with correct control
        _context.Control = control;
    }

    /// <summary>
    /// Cleanup settings.
    /// </summary>
    public void Dispose()
    {
        // Put back the original setting
        _context.Control = _startControl;
        GC.SuppressFinalize(this);
    }
    #endregion
}