#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Action list for the KryptonProgressBar control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonProgressBarExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonProgressBar? _progressBar;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonProgressBarExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonProgressBarExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _progressBar = (KryptonProgressBar?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the minimum value.
    /// </summary>
    public int Minimum
    {
        get => _progressBar?.Minimum ?? 0;
        set => SetPropertyValue(_progressBar!, nameof(Minimum), Minimum, value, v => _progressBar!.Minimum = v);
    }

    /// <summary>
    /// Gets and sets the maximum value.
    /// </summary>
    public int Maximum
    {
        get => _progressBar?.Maximum ?? 100;
        set => SetPropertyValue(_progressBar!, nameof(Maximum), Maximum, value, v => _progressBar!.Maximum = v);
    }

    /// <summary>
    /// Gets and sets the current value.
    /// </summary>
    public int Value
    {
        get => _progressBar?.Value ?? 0;
        set => SetPropertyValue(_progressBar!, nameof(Value), Value, value, v => _progressBar!.Value = v);
    }

    /// <summary>
    /// Gets and sets the step amount.
    /// </summary>
    public int Step
    {
        get => _progressBar?.Step ?? 10;
        set => SetPropertyValue(_progressBar!, nameof(Step), Step, value, v => _progressBar!.Step = v);
    }

    /// <summary>
    /// Gets and sets the style of the progress bar.
    /// </summary>
    public ProgressBarStyle Style
    {
        get => _progressBar?.Style ?? ProgressBarStyle.Blocks;
        set => SetPropertyValue(_progressBar!, nameof(Style), Style, value, v => _progressBar!.Style = v);
    }
    #endregion

    #region Public Override
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a control instance at design time
        if (_progressBar != null)
        {
            // Add the list of ProgressBar specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(Style), @"Style", @"Appearance", @"Progress bar style"));
            actions.Add(new DesignerActionHeaderItem(@"Values"));
            actions.Add(new DesignerActionPropertyItem(nameof(Minimum), @"Minimum", @"Values", @"Minimum value"));
            actions.Add(new DesignerActionPropertyItem(nameof(Maximum), @"Maximum", @"Values", @"Maximum value"));
            actions.Add(new DesignerActionPropertyItem(nameof(Value), @"Value", @"Values", @"Current value"));
            actions.Add(new DesignerActionPropertyItem(nameof(Step), @"Step", @"Values", @"Step amount"));
        }

        return actions;
    }
    #endregion
}
