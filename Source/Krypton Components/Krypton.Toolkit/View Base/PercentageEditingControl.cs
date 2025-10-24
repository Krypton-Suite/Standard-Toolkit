#region Licences

/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
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
/// Public class for the underlying editing control
/// </summary>
[ToolboxItem(false)]
public class PercentageEditingControl : DataGridViewTextBoxEditingControl
{
    #region Identity

    /// <summary>
    /// Constructor
    /// </summary>
    public PercentageEditingControl()
        : base()
    {
    }

    #endregion

    #region Implementation

    /// <summary>
    /// Returns if the character is a valid digit
    /// </summary>
    /// <param name="c">The character.</param>
    /// <returns>True if valid digit, false otherwise.</returns>
    private bool IsValidForNumberInput(char c)
    {
        return char.IsDigit(c);
        // OrElse c = Chr(8) OrElse c = "."c OrElse c = "-"c OrElse c = "("c OrElse c = ")"c
    }

    #endregion

    #region Protected Overrides

    /// <summary>
    /// Overrides OnKeyPress
    /// </summary>
    /// <param name="e"></param>
    protected override void OnKeyPress(KeyPressEventArgs e)
    {
        if (!IsValidForNumberInput(e.KeyChar))
        {
            e.Handled = true;
        }
    }

    #endregion
}