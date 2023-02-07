#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBeInternal

namespace Krypton.Toolkit
{
    /// <summary>
    /// Expose a global set of color strings used within Krypton and that are localizable.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class GlobalColorStrings : GlobalId
    {
    }
}