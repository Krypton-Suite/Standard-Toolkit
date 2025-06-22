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

namespace Krypton.Navigator;

internal class KryptonPageActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonPage _page;
    private readonly IComponentChangeService? _serviceComponentChange;
    private DesignerActionItemCollection _actions;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPageActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonPageActionList(KryptonPageDesigner owner)
        : base(owner.Component)
    {
        // Remember designer and actual component instance being designed
        _page = (owner.Component as KryptonPage)!;

        // Cache service used to notify when a property has changed
        _serviceComponentChange = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion
        
    #region Public
    /// <summary>
    /// Gets and sets the page text.
    /// </summary>
    public string TextShort
    {
        get => _page.Text;

        set
        {
            if (!_page.Text.Equals(value))
            {
                _serviceComponentChange?.OnComponentChanged(_page, null, _page.Text, value);
                _page.Text = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the page title text.
    /// </summary>
    public string TextTitle
    {
        get => _page.TextTitle;

        set
        {
            if (!_page.TextTitle.Equals(value))
            {
                _serviceComponentChange?.OnComponentChanged(_page, null, _page.TextTitle, value);
                _page.TextTitle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the page description text.
    /// </summary>
    public string TextDescription
    {
        get => _page.TextDescription;

        set
        {
            if (!_page.TextDescription.Equals(value))
            {
                _serviceComponentChange?.OnComponentChanged(_page, null, _page.TextDescription, value);
                _page.TextDescription = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the page tooltip title text.
    /// </summary>
    public string ToolTipTitle
    {
        get => _page.ToolTipTitle;

        set
        {
            if (!_page.ToolTipTitle.Equals(value))
            {
                _serviceComponentChange?.OnComponentChanged(_page, null, _page.ToolTipTitle, value);
                _page.ToolTipTitle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the page tooltip body text.
    /// </summary>
    public string ToolTipBody
    {
        get => _page.ToolTipBody;

        set
        {
            if (!_page.ToolTipBody.Equals(value))
            {
                _serviceComponentChange?.OnComponentChanged(_page, null, _page.ToolTipBody, value);
                _page.ToolTipBody = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the page tooltip image.
    /// </summary>
    public Bitmap? ToolTipImage
    {
        get => _page.ToolTipImage;

        set
        {
            if (_page.ToolTipImage != value)
            {
                _serviceComponentChange?.OnComponentChanged(_page, null, _page.ToolTipImage, value);
                _page.ToolTipImage = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the small page image.
    /// </summary>
    public Bitmap? ImageSmall
    {
        get => _page.ImageSmall;

        set
        {
            if (_page.ImageSmall != value)
            {
                _serviceComponentChange?.OnComponentChanged(_page, null, _page.ImageSmall, value);
                _page.ImageSmall = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the medium page image.
    /// </summary>
    public Bitmap? ImageMedium
    {
        get => _page.ImageMedium;

        set
        {
            if (_page.ImageMedium != value)
            {
                _serviceComponentChange?.OnComponentChanged(_page, null, _page.ImageMedium, value);
                _page.ImageMedium = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the large page image.
    /// </summary>
    public Bitmap? ImageLarge
    {
        get => _page.ImageLarge;

        set
        {
            if (_page.ImageLarge != value)
            {
                _serviceComponentChange?.OnComponentChanged(_page, null, _page.ImageLarge, value);
                _page.ImageLarge = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the large page image.
    /// </summary>
    public bool PageInOverflowBarForOutlookMode
    {
        get => _page.AreFlagsSet(KryptonPageFlags.PageInOverflowBarForOutlookMode);

        set
        {
            _serviceComponentChange?.OnComponentChanged(_page, null, _page.AreFlagsSet(KryptonPageFlags.PageInOverflowBarForOutlookMode), value);

            if (value)
            {
                _page.SetFlags(KryptonPageFlags.PageInOverflowBarForOutlookMode);
            }
            else
            {
                _page.ClearFlags(KryptonPageFlags.PageInOverflowBarForOutlookMode);
            }
        }
    }
    #endregion

    #region Public Override
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        // Only create the list of items once
        _actions ??= new DesignerActionItemCollection
        {
            new DesignerActionHeaderItem(nameof(Appearance)),
            new DesignerActionPropertyItem(nameof(TextShort), "Text", nameof(Appearance), "The page text."),
            new DesignerActionPropertyItem(nameof(TextTitle), "Text Title", nameof(Appearance), "The title text for the page."),
            new DesignerActionPropertyItem(nameof(TextDescription), "Text Description", nameof(Appearance), "The description text for the page."),
            new DesignerActionPropertyItem(nameof(ImageSmall), "Image Small", nameof(Appearance), "The small image that represents the page."),
            new DesignerActionPropertyItem(nameof(ImageMedium), "Image Medium", nameof(Appearance), "The medium image that represents the page."),
            new DesignerActionPropertyItem(nameof(ImageLarge), "Image Large", nameof(Appearance), "The large image that represents the page."),
            new DesignerActionPropertyItem(nameof(ToolTipTitle), "Tooltip Title", nameof(Appearance), "The tooltip title text for the page."),
            new DesignerActionPropertyItem(nameof(ToolTipBody), "Tooltip Body", nameof(Appearance), "The tooltip body text for the page."),
            new DesignerActionPropertyItem(nameof(ToolTipImage), "Tooltip Image", nameof(Appearance), "The tooltip image that represents the page."),
            new DesignerActionHeaderItem("Flags"),
            new DesignerActionPropertyItem(nameof(PageInOverflowBarForOutlookMode), "Page in Overflow Bar for Outlook mode", "Flags", "Should the page be shown on the overflow bar for the Outlook mode.")
        };

        return _actions;
    }
    #endregion
}