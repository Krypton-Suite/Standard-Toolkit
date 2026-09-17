#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides accessibility information for <see cref="KryptonSplitButton"/> controls.
/// </summary>
internal class KryptonSplitButtonAccessibleObject : KryptonDropButtonAccessibleObject
{
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonSplitButtonAccessibleObject"/> class.
    /// </summary>
    /// <param name="owner">The <see cref="KryptonSplitButton"/> that owns this accessible object.</param>
    public KryptonSplitButtonAccessibleObject(KryptonSplitButton owner)
        : base(owner, AccessibleRole.SplitButton)
    {
    }
}
