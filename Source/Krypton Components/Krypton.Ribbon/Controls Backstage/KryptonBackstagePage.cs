#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Represents a single page hosted by a <see cref="KryptonBackstageView"/>.
/// </summary>
[ToolboxItem(false)]
[DefaultEvent(nameof(Click))]
[DefaultProperty(nameof(Text))]
[Designer(typeof(KryptonBackstagePageDesigner))]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
public class KryptonBackstagePage : KryptonPanel
{
    #region Instance Fields
    private Image? _image;
    private bool _visibleInNavigation;
    private BackstageItemSize _itemSize;
    #endregion

    #region Events
    /// <summary>
    /// Occurs when a navigation-related page property has changed.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Occurs when a navigation-related page property has changed.")]
    public event EventHandler? NavigationPropertyChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonBackstagePage"/> class.
    /// </summary>
    public KryptonBackstagePage()
    {
        _visibleInNavigation = true;
        _itemSize = BackstageItemSize.Small;
        Dock = DockStyle.Fill;
        Visible = false;
    }
    #endregion

    #region Public
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
    /// Gets and sets if this page should be shown in the navigation list.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Determine if this page should be shown in the navigation list.")]
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

    /// <inheritdoc />
    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);
        OnNavigationPropertyChanged(EventArgs.Empty);
    }
    #endregion

    #region Implementation
    private void OnNavigationPropertyChanged(EventArgs e) => NavigationPropertyChanged?.Invoke(this, e);
    #endregion
}

