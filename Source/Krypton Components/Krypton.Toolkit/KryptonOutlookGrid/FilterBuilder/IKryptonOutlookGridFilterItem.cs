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

internal interface IKryptonOutlookGridFilterItem
{

    #region Properties

    KryptonOutlookGridFilterItemMenuButton.Items SelectedMenuItem { get; set; }
    string Filter { get; } // The filter string for the object
    string ReadableFilter { get; } // The readable filter for the object
    string Conjunction { get; } // The conjunction following the object
    KryptonOutlookGridFilterField FieldValue { get; set; } // The field for the object

    #endregion Properties

}