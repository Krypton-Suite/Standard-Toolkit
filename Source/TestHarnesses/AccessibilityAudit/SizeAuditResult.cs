#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.Collections.Generic;

namespace AccessibilityAudit;

internal sealed class SizeAuditResult
{
    public SizeAuditResult(string title)
    {
        Title = title;
    }

    public string Title { get; }

    public int Visited { get; set; }

    public int Skipped { get; set; }

    public List<string> FailureKeys { get; } = new List<string>();

    public List<string> HardFailures { get; } = new List<string>();

    public List<string> Notes { get; } = new List<string>();

    public void AddFailure(string key)
    {
        if (!FailureKeys.Contains(key))
        {
            FailureKeys.Add(key);
        }
    }
}
