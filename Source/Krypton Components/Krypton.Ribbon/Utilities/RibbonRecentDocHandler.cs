#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Delegate used for hooking into a KryptonRibbonContext typed collection.
/// </summary>
public delegate void RibbonRecentDocHandler(object sender, TypedCollectionEventArgs<KryptonRibbonRecentDoc> e);