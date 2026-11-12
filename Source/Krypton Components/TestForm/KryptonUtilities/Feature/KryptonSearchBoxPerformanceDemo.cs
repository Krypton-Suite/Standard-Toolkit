#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Issue #4580: exercises <see cref="KryptonSearchBox"/> suggestion performance with a large item set.
/// Type quickly, open the dropdown, and select items; typing and selection should stay responsive.
/// </summary>
public partial class KryptonSearchBoxPerformanceDemo : KryptonForm
{
    private const int SuggestionCount = 5000;

    public KryptonSearchBoxPerformanceDemo()
    {
        InitializeComponent();
    }

    private void KryptonSearchBoxPerformanceDemo_Load(object? sender, EventArgs e)
    {
        var suggestions = new List<string>(SuggestionCount);
        for (var i = 0; i < SuggestionCount; i++)
        {
            suggestions.Add($"Item {i:D4} — sample suggestion for search performance");
        }

        ksbPerf.SetSearchSuggestions(suggestions);
        ksbPerf.SearchBoxValues.EnableSuggestions = true;
        ksbPerf.SearchBoxValues.SuggestionMaxCount = 10;
        ksbPerf.SearchBoxValues.MinimumSearchLength = 1;
        ksbPerf.SearchBoxValues.SuggestionUpdateDelay = 75;
        ksbPerf.SearchBoxValues.PlaceholderText = @"Type to filter 5,000 suggestions…";

        nudDelay.Value = ksbPerf.SearchBoxValues.SuggestionUpdateDelay;
        klblStatus.Text = $@"Loaded {SuggestionCount:N0} suggestions. Delay = {ksbPerf.SearchBoxValues.SuggestionUpdateDelay} ms.";
    }

    private void nudDelay_ValueChanged(object? sender, EventArgs e)
    {
        ksbPerf.SearchBoxValues.SuggestionUpdateDelay = (int)nudDelay.Value;
        klblStatus.Text = $@"SuggestionUpdateDelay = {ksbPerf.SearchBoxValues.SuggestionUpdateDelay} ms (0 = immediate).";
    }

    private void ksbPerf_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e) =>
        klblStatus.Text = $@"Selected: {e.Suggestion}";

    private void ksbPerf_Search(object? sender, SearchEventArgs e) =>
        klblStatus.Text = $@"Search: {e.SearchText}";
}