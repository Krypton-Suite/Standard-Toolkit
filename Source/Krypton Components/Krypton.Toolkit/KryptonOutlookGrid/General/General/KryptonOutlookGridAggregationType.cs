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
/// Specifies the type of aggregation to perform on a column in group rows.
/// </summary>
public enum KryptonOutlookGridAggregationType
{
    /// <summary>
    /// No aggregation will be performed.
    /// </summary>
    None,
    /// <summary>
    /// Calculates the sum of numeric values in the group.
    /// </summary>
    Sum,
    /// <summary>
    /// Counts the number of non-null values in the group.
    /// </summary>
    Count,
    /// <summary>
    /// Calculates the average of numeric values in the group.
    /// </summary>
    Average,
    /// <summary>
    /// Finds the minimum value in the group.
    /// </summary>
    Min,
    /// <summary>
    /// Finds the maximum value in the group.
    /// </summary>
    Max,
    /// <summary>
    /// Finds the minimum and maximum value in the group.
    /// </summary>
    MinMax
}
