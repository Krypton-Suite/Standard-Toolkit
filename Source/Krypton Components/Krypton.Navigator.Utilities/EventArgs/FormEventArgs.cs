#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities;

/// <summary>
/// Provides data for tabbed MDI form events.
/// </summary>
public class FormEventArgs : CancelEventArgs
{
    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="FormEventArgs"/> class.
    /// </summary>
    /// <param name="form">The MDI child form associated with the event.</param>
    public FormEventArgs(Form form)
    {
        Form = form;
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the MDI child form associated with the event.
    /// </summary>
    public Form Form { get; }

    #endregion
}