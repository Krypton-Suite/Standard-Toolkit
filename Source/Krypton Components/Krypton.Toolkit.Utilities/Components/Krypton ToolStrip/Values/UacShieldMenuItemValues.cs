#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Expandable configuration for <see cref="KryptonToolStripMenuItemUACShield"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class UacShieldMenuItemValues : Storage
{
    #region Instance Fields

    private bool _elevateApplicationOnClick = true;
    private string _processName = string.Empty;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="UacShieldMenuItemValues"/> class.
    /// </summary>
    /// <param name="owner">Owning UAC shield menu item.</param>
    public UacShieldMenuItemValues(KryptonToolStripMenuItemUACShield owner)
    {
        _ = owner ?? throw new ArgumentNullException(nameof(owner));
    }

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault => _elevateApplicationOnClick && _processName.Length == 0;

    #endregion

    #region Public

    /// <summary>
    /// Elevates the current running application to administrator level when the button is clicked.
    /// </summary>
    /// <remarks>
    /// The application/process will restart when clicked.
    /// </remarks>
    [Category(@"Code")]
    [DefaultValue(true)]
    [Description(@"Elevates the current running application to administrator level when button is clicked. The application/process will restart when clicked.")]
    public bool ElevateApplicationOnClick
    {
        get => _elevateApplicationOnClick;
        set => _elevateApplicationOnClick = value;
    }

    /// <summary>
    /// The application assembly.
    /// </summary>
    /// <remarks>
    /// Use 'Process.GetCurrentProcess().ProcessName;' as a start.
    /// </remarks>
    [Category(@"Code")]
    [DefaultValue("")]
    [Description(@"The application assembly. Use 'Process.GetCurrentProcess().ProcessName;' as a start.")]
    public string ProcessName
    {
        get => _processName;
        set => _processName = value;
    }

    /// <summary>
    /// Resets all values to their defaults.
    /// </summary>
    public void Reset()
    {
        _elevateApplicationOnClick = true;
        _processName = string.Empty;
    }

    #endregion
}
