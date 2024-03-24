#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Delegate used when a box is closed to inform the caller.
    /// </summary>
    /// <param name="result">The result.</param>
    public delegate void AsyncResultCallback(InformationBoxResult result);
}