#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provide a context menu separator.
/// </summary>
[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonContextMenuSeparator), "ToolboxBitmaps.KryptonContextMenuSeparator.bmp")]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultProperty(nameof(Horizontal))]
public class KryptonContextMenuSeparator : KryptonContextMenuItemBase
{
    #region Instance Fields
    private bool _horizontal;
    private readonly PaletteRedirectDouble _redirectSeparator;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonContextMenuSeparator class.
    /// </summary>
    public KryptonContextMenuSeparator()
    {
        // Default fields
        _horizontal = true;

        // Create the redirector that can get values from the krypton context menu
        _redirectSeparator = new PaletteRedirectDouble();

        // Create the separator storage for overriding specific values
        StateNormal = new PaletteDoubleRedirect(_redirectSeparator,
            PaletteBackStyle.ContextMenuSeparator,
            PaletteBorderStyle.ContextMenuSeparator);
    }

    /// <summary>
    /// Returns a description of the instance.
    /// </summary>
    /// <returns>String representation.</returns>
    public override string ToString() => "(Separator)";

    #endregion

    #region Public
    /// <summary>
    /// Returns the number of child menu items.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override int ItemChildCount => 0;

    /// <summary>
    /// Returns the indexed child menu item.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override KryptonContextMenuItemBase? this[int index] => null;

    /// <summary>
    /// Test for the provided shortcut and perform relevant action if a match is found.
    /// </summary>
    /// <param name="keyData">Key data to check against shortcut definitions.</param>
    /// <returns>True if shortcut was handled, otherwise false.</returns>
    public override bool ProcessShortcut(Keys keyData) => false;

    /// <summary>
    /// Returns a view appropriate for this item based on the object it is inside.
    /// </summary>
    /// <param name="provider">Provider of context menu information.</param>
    /// <param name="parent">Owning object reference.</param>
    /// <param name="columns">Containing columns.</param>
    /// <param name="standardStyle">Draw items with standard or alternate style.</param>
    /// <param name="imageColumn">Draw an image background for the item images.</param>
    /// <returns>ViewBase that is the root of the view hierarchy being added.</returns>
    public override ViewBase GenerateView(IContextMenuProvider provider,
        object parent,
        ViewLayoutStack columns,
        bool standardStyle,
        bool imageColumn)
    {
        SetProvider(provider);
        if (Horizontal && (parent is KryptonContextMenuItemCollection))
        {
            // Create a stack of horizontal items inside the item
            var docker = new ViewLayoutDocker();

            // Take up same space as the image column, so separator starts close to actual text
            var imageContent = new ViewDrawContent(provider.ProviderStateCommon.ItemImage.Content,
                new FixedContentValue(null, null, null, GlobalStaticValues.EMPTY_COLOR), VisualOrientation.Top);
            var imageCanvas = new ViewDrawMenuImageCanvas(provider.ProviderStateCommon.ItemImage.Back,
                provider.ProviderStateCommon.ItemImage.Border, 0, true)
            {
                imageContent
            };
            docker.Add(new ViewLayoutCenter(imageCanvas), ViewDockStyle.Left);
            docker.Add(new ViewLayoutSeparator(1, 0), ViewDockStyle.Left);

            // Gap that matches left padding of text/extra text
            docker.Add(new ViewLayoutMenuSepGap(provider.ProviderStateCommon, standardStyle), ViewDockStyle.Left);

            // Separator Display
            var separatorStack = new ViewLayoutStack(false)
            {
                new ViewLayoutSeparator(1, 1),
                new ViewDrawMenuSeparator(this, provider.ProviderStateCommon.Separator),
                new ViewLayoutSeparator(1, 1)
            };
            docker.Add(separatorStack, ViewDockStyle.Fill);

            return docker;
        }
        else
        {
            return new ViewDrawMenuSeparator(this, provider.ProviderStateCommon.Separator);
        }
    }

    /// <summary>
    /// Gets and sets if the separator is a horizontal or vertical break.
    /// </summary>
    [KryptonPersist]
    [Category(@"Behavior")]
    [Description(@"Is this a horizontal or vertical break in the menu.")]
    [DefaultValue(true)]
    public bool Horizontal
    {
        get => _horizontal;

        set
        {
            if (_horizontal != value)
            {
                _horizontal = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Horizontal)));
            }
        }
    }

    /// <summary>
    /// Gets access to the separator instance specific appearance values.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining separator instance specific appearance values.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public PaletteDoubleRedirect StateNormal { get; }

    private bool ShouldSerializeStateNormal() => !StateNormal.IsDefault;

    #endregion

    #region Internal
    internal void SetPaletteRedirect(PaletteDoubleRedirect redirector) => _redirectSeparator?.SetRedirectStates(redirector, redirector);

    #endregion
}