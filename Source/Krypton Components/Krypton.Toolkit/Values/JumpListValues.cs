#region BSD License
/*
 *  
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for jump list value information.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class JumpListValues : Storage
{
    #region Instance Fields

    private string _appId;
    private readonly List<JumpListItem> _userTasks;
    private readonly Dictionary<string, List<JumpListItem>> _categories;
    private bool _showFrequentCategory;
    private bool _showRecentCategory;
    internal event Action? JumpListChanged;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the JumpListValues class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public JumpListValues(NeedPaintHandler needPaint)
    {
        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        _appId = string.Empty;
        _userTasks = new List<JumpListItem>();
        _categories = new Dictionary<string, List<JumpListItem>>();
        _showFrequentCategory = false;
        _showRecentCategory = false;
    }

    #endregion

    #region IsDefault

    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => string.IsNullOrEmpty(_appId) &&
                                      _userTasks.Count == 0 &&
                                      _categories.Count == 0 &&
                                      !_showFrequentCategory &&
                                      !_showRecentCategory;

    #endregion

    #region AppId

    /// <summary>
    /// Gets and sets the application ID for the jump list.
    /// </summary>
    [Localizable(true)]
    [Category(@"Behavior")]
    [Description(@"Application ID used to identify the application's jump list.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("")]
    public string AppId
    {
        get => _appId;

        set
        {
            if (_appId != value)
            {
                _appId = value ?? string.Empty;
                PerformNeedPaint(true);
                JumpListChanged?.Invoke();
            }
        }
    }

    private bool ShouldSerializeAppId() => !string.IsNullOrEmpty(AppId);

    /// <summary>
    /// Resets the AppId property to its default value.
    /// </summary>
    public void ResetAppId() => AppId = string.Empty;

    #endregion

    #region UserTasks

    /// <summary>
    /// Gets the list of user tasks to display in the jump list.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"User tasks displayed in the jump list.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<JumpListItem> UserTasks => _userTasks;

    private bool ShouldSerializeUserTasks() => _userTasks.Count > 0;

    /// <summary>
    /// Resets the UserTasks property to its default value.
    /// </summary>
    public void ResetUserTasks()
    {
        _userTasks.Clear();
        PerformNeedPaint(true);
        JumpListChanged?.Invoke();
    }

    #endregion

    #region Categories

    /// <summary>
    /// Gets the dictionary of custom categories and their items.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Dictionary<string, List<JumpListItem>> Categories => _categories;

    /// <summary>
    /// Adds a custom category with items to the jump list.
    /// </summary>
    /// <param name="categoryName">Name of the category.</param>
    /// <param name="items">Items in the category.</param>
    public void AddCategory(string categoryName, List<JumpListItem> items)
    {
        if (string.IsNullOrEmpty(categoryName))
        {
            return;
        }

        _categories[categoryName] = items ?? new List<JumpListItem>();
        PerformNeedPaint(true);
        JumpListChanged?.Invoke();
    }

    /// <summary>
    /// Removes a custom category from the jump list.
    /// </summary>
    /// <param name="categoryName">Name of the category to remove.</param>
    public void RemoveCategory(string categoryName)
    {
        if (_categories.Remove(categoryName))
        {
            PerformNeedPaint(true);
            JumpListChanged?.Invoke();
        }
    }

    /// <summary>
    /// Clears all custom categories.
    /// </summary>
    public void ClearCategories()
    {
        _categories.Clear();
        PerformNeedPaint(true);
        JumpListChanged?.Invoke();
    }

    #endregion

    #region ShowFrequentCategory

    /// <summary>
    /// Gets and sets whether to show the Frequent category.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Whether to show the Frequent category in the jump list.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(false)]
    public bool ShowFrequentCategory
    {
        get => _showFrequentCategory;

        set
        {
            if (_showFrequentCategory != value)
            {
                _showFrequentCategory = value;
                PerformNeedPaint(true);
                JumpListChanged?.Invoke();
            }
        }
    }

    private bool ShouldSerializeShowFrequentCategory() => ShowFrequentCategory;

    /// <summary>
    /// Resets the ShowFrequentCategory property to its default value.
    /// </summary>
    public void ResetShowFrequentCategory() => ShowFrequentCategory = false;

    #endregion

    #region ShowRecentCategory

    /// <summary>
    /// Gets and sets whether to show the Recent category.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Whether to show the Recent category in the jump list.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(false)]
    public bool ShowRecentCategory
    {
        get => _showRecentCategory;

        set
        {
            if (_showRecentCategory != value)
            {
                _showRecentCategory = value;
                PerformNeedPaint(true);
                JumpListChanged?.Invoke();
            }
        }
    }

    private bool ShouldSerializeShowRecentCategory() => ShowRecentCategory;

    /// <summary>
    /// Resets the ShowRecentCategory property to its default value.
    /// </summary>
    public void ResetShowRecentCategory() => ShowRecentCategory = false;

    #endregion

    #region CopyFrom

    /// <summary>
    /// Value copy from the provided source to ourself.
    /// </summary>
    /// <param name="source">Source instance.</param>
    public void CopyFrom(JumpListValues source)
    {
        AppId = source.AppId;
        _userTasks.Clear();
        _userTasks.AddRange(source._userTasks);
        _categories.Clear();
        foreach (var kvp in source._categories)
        {
            _categories[kvp.Key] = new List<JumpListItem>(kvp.Value);
        }
        ShowFrequentCategory = source.ShowFrequentCategory;
        ShowRecentCategory = source.ShowRecentCategory;
    }

    #endregion

    #region Reset

    /// <summary>
    /// Resets all values to their default.
    /// </summary>
    public void Reset()
    {
        ResetAppId();
        ResetUserTasks();
        ClearCategories();
        ResetShowFrequentCategory();
        ResetShowRecentCategory();
    }

    #endregion
}
