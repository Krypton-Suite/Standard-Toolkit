#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonModalWaitDialogStrings : GlobalId
{
    #region Static Strings

    private const string DEFAULT_TITLE = @"Processing";

    private const string DEFAULT_TEXT = @"Please wait for operation to complete...";

    #endregion
}