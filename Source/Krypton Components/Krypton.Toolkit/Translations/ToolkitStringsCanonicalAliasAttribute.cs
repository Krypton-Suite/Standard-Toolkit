#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Marks a <see cref="KryptonGlobalToolkitStrings"/> Content property as a compatibility alias that should be
/// skipped during canonical export (<see cref="CommonStrings"/> is written instead) but still accepted on import.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
internal sealed class ToolkitStringsCanonicalAliasAttribute : Attribute
{
}
