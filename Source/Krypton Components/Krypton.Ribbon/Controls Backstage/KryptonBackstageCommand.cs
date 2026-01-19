#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2025 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Represents a command-only item in the Backstage View navigation (no associated page).
/// </summary>
[ToolboxItem(false)]
[DefaultEvent(nameof(Click))]
[DefaultProperty(nameof(Text))]
public class KryptonBackstageCommand
{
    #region Instance Fields

    private string _text;
    private Image? _image;
    private bool _visibleInNavigation;
    private BackstageItemSize _itemSize;
    
    #endregion

    #region Events
    
    /// <summary>
    /// Occurs when the command is clicked.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Occurs when the command is clicked.")]
    public event EventHandler? Click;

    /// <summary>
    /// Occurs when a navigation-related property has changed.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Occurs when a navigation-related property has changed.")]
    public event EventHandler? NavigationPropertyChanged;
    
    #endregion

    #region Identity
   
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonBackstageCommand"/> class.
    /// </summary>
    public KryptonBackstageCommand()
    {
        _text = string.Empty;
        _visibleInNavigation = true;
        _itemSize = BackstageItemSize.Small;
    }

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonBackstageCommand"/> class.
    /// </summary>
    /// <param name="text">The display text for the command.</param>
    public KryptonBackstageCommand(string text)
    {
        _text = text ?? string.Empty;
        _visibleInNavigation = true;
        _itemSize = BackstageItemSize.Small;
    }
    
    #endregion

    #region Public
    
    /// <summary>
    /// Gets and sets the display text for the command.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Display text shown in the navigation list.")]
    [DefaultValue("")]
    public string Text
    {
        get => _text;
        set
        {
            if (_text != value)
            {
                _text = value ?? string.Empty;
                OnNavigationPropertyChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Gets and sets the image used in the backstage navigation list.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Image used in the backstage navigation list.")]
    [DefaultValue(null)]
    public Image? Image
    {
        get => _image;
        set
        {
            if (!ReferenceEquals(_image, value))
            {
                _image = value;
                OnNavigationPropertyChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Gets and sets if this command should be shown in the navigation list.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Determine if this command should be shown in the navigation list.")]
    [DefaultValue(true)]
    public bool VisibleInNavigation
    {
        get => _visibleInNavigation;
        set
        {
            if (_visibleInNavigation != value)
            {
                _visibleInNavigation = value;
                OnNavigationPropertyChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Gets and sets the size of this item in the navigation list.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Size of this item in the navigation list (Small or Large).")]
    [DefaultValue(BackstageItemSize.Small)]
    public BackstageItemSize ItemSize
    {
        get => _itemSize;
        set
        {
            if (_itemSize != value)
            {
                _itemSize = value;
                OnNavigationPropertyChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Raises the <see cref="Click"/> event.
    /// </summary>
    public void PerformClick() => OnClick(EventArgs.Empty);
    
    #endregion

    #region Implementation
    
    /// <summary>
    /// Raises the <see cref="Click"/> event.
    /// </summary>
    protected virtual void OnClick(EventArgs e) => Click?.Invoke(this, e);

    /// <summary>
    /// Raises the <see cref="NavigationPropertyChanged"/> event.
    /// </summary>
    protected virtual void OnNavigationPropertyChanged(EventArgs e) => NavigationPropertyChanged?.Invoke(this, e);

    #endregion
}
