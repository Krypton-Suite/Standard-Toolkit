#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for taskbar thumbnail toolbar button definitions. Windows allows up to 7 buttons.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class TaskbarThumbnailButtonValues : Storage
{
    #region Instance Fields

    private const int MaxButtons = 7;
    private readonly List<ThumbnailButtonItem> _buttons;
    internal event Action? OnThumbnailButtonsChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the TaskbarThumbnailButtonValues class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public TaskbarThumbnailButtonValues(NeedPaintHandler needPaint)
    {
        NeedPaint = needPaint;
        _buttons = new List<ThumbnailButtonItem>();
    }

    #endregion

    #region IsDefault

    /// <summary>
    /// Gets a value indicating if all values are default (no buttons).
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => _buttons.Count == 0;

    #endregion

    #region Buttons

    /// <summary>
    /// Gets the list of thumbnail toolbar buttons. Maximum 7 buttons. Modify then call Apply to update the taskbar.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Buttons displayed in the taskbar thumbnail preview (e.g. play, pause). Maximum 7.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<ThumbnailButtonItem> Buttons => _buttons;

    private bool ShouldSerializeButtons() => _buttons.Count > 0;

    /// <summary>
    /// Adds a button. Fails if already at maximum (7). Raises OnThumbnailButtonsChanged.
    /// </summary>
    public void Add(ThumbnailButtonItem item)
    {
        if (_buttons.Count >= MaxButtons)
        {
            return;
        }

        _buttons.Add(item);
        PerformNeedPaint(true);
        OnThumbnailButtonsChanged?.Invoke();
    }

    /// <summary>
    /// Removes the button with the given ID. Raises OnThumbnailButtonsChanged if found.
    /// </summary>
    [CLSCompliant(false)]
    public void Remove(uint id)
    {
        var idx = _buttons.FindIndex(b => b.Id == id);
        if (idx < 0)
        {
            return;
        }

        _buttons.RemoveAt(idx);
        PerformNeedPaint(true);
        OnThumbnailButtonsChanged?.Invoke();
    }

    /// <summary>
    /// Removes all buttons and updates the taskbar.
    /// </summary>
    public void Clear()
    {
        if (_buttons.Count == 0)
        {
            return;
        }

        _buttons.Clear();
        PerformNeedPaint(true);
        OnThumbnailButtonsChanged?.Invoke();
    }

    /// <summary>
    /// Notifies that button definitions have changed (e.g. after editing Buttons in code). Call to refresh the taskbar.
    /// </summary>
    public void Apply()
    {
        PerformNeedPaint(true);
        OnThumbnailButtonsChanged?.Invoke();
    }

    /// <summary>
    /// Resets the Buttons property to its default value.
    /// </summary>
    public void ResetButtons()
    {
        Clear();
    }

    #endregion

    #region CopyFrom

    /// <summary>
    /// Value copy from the provided source to this instance.
    /// </summary>
    /// <param name="source">Source instance.</param>
    public void CopyFrom(TaskbarThumbnailButtonValues source)
    {
        _buttons.Clear();
        foreach (var b in source.Buttons)
        {
            _buttons.Add(new ThumbnailButtonItem
            {
                Id = b.Id,
                Icon = b.Icon,
                Tooltip = b.Tooltip ?? string.Empty,
                Enabled = b.Enabled,
                Hidden = b.Hidden
            });
        }

        PerformNeedPaint(true);
        OnThumbnailButtonsChanged?.Invoke();
    }

    #endregion

    #region Reset

    /// <summary>
    /// Resets all values to their default.
    /// </summary>
    public void Reset()
    {
        ResetButtons();
    }

    #endregion
}
