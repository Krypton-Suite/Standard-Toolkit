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
/// Storage for taskbar overlay icon value information.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class TaskbarOverlayIconValues : Storage
{
    #region Instance Fields

    private Icon? _icon;
    private string _description;
    internal event Action? OnTaskbarOverlayChanged;
    
    #endregion

    #region Identity
    
    /// <summary>
    /// Initialize a new instance of the TaskbarOverlayIconValues class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public TaskbarOverlayIconValues(NeedPaintHandler needPaint)
    {
        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        Reset();
    }
    
    #endregion

    #region IsDefault
    
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (Icon == null) && (Description == string.Empty);

    #endregion

    #region Icon
    
    /// <summary>
    /// Gets and sets the taskbar overlay icon.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Overlay icon to display on the taskbar button. Typically a small 16x16 icon.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(null)]
    public Icon? Icon
    {
        get => _icon;

        set
        {
            if (_icon != value)
            {
                _icon = value;
                PerformNeedPaint(true);
                // Notify parent form to update taskbar overlay
                OnTaskbarOverlayChanged?.Invoke();
            }
        }
    }

    private bool ShouldSerializeIcon() => Icon != null;

    /// <summary>
    /// Resets the Icon property to its default value.
    /// </summary>
    public void ResetIcon() => Icon = null;
    
    #endregion

    #region Description
    
    /// <summary>
    /// Gets and sets the description text for the overlay icon (shown in tooltip).
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Description text for the overlay icon, shown in the taskbar tooltip.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("")]
    public string Description
    {
        get => _description;

        set
        {
            if (_description != value)
            {
                _description = value ?? string.Empty;
                PerformNeedPaint(true);
                // Notify parent form to update taskbar overlay
                OnTaskbarOverlayChanged?.Invoke();
            }
        }
    }

    private bool ShouldSerializeDescription() => Description != string.Empty;

    /// <summary>
    /// Resets the Description property to its default value.
    /// </summary>
    public void ResetDescription() => Description = string.Empty;
   
    #endregion

    #region CopyFrom
    
    /// <summary>
    /// Value copy from the provided source to ourself.
    /// </summary>
    /// <param name="source">Source instance.</param>
    public void CopyFrom(TaskbarOverlayIconValues source)
    {
        Icon = source.Icon;
        Description = source.Description;
    }
    
    #endregion

    #region Reset

    /// <summary>
    /// Resets all values to their default.
    /// </summary>
    public void Reset()
    {
        ResetIcon();
        ResetDescription();
    }

    #endregion
}
