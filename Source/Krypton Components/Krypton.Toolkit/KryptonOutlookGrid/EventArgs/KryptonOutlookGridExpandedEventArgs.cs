#region Licences

/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege & Ahmed Abdelhameed et al. 2024 - 2025. All rights reserved.
 *
 */

//--------------------------------------------------------------------------------
// Copyright (C) 2013-2021 JDH Software - <support@jdhsoftware.com>
//
// This program is provided to you under the terms of the Microsoft Public
// License (Ms-PL) as published at https://github.com/Cocotteseb/Krypton-OutlookGrid/blob/master/LICENSE.md
//
// Visit https://www.jdhsoftware.com and follow @jdhsoftware on Twitter
//
//--------------------------------------------------------------------------------

#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Class for Node expanding events
/// </summary>
/// <seealso cref="KryptonOutlookGridRowNodeEventBase" />
public class KryptonOutlookGridExpandedEventArgs : KryptonOutlookGridRowNodeEventBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonOutlookGridExpandedEventArgs"/> class.
    /// </summary>
    /// <param name="node">The node.</param>
    public KryptonOutlookGridExpandedEventArgs(KryptonOutlookGridRow node) : base(node)
    {
    }
}