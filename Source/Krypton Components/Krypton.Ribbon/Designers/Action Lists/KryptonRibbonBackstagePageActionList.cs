#region BSD License
/*
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Smart tag action list for KryptonRibbonBackstagePage.
/// </summary>
internal class KryptonRibbonBackstagePageActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonRibbonBackstagePageDesigner _designer;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRibbonBackstagePageActionList class.
    /// </summary>
    /// <param name="designer">Designer that owns this action list.</param>
    public KryptonRibbonBackstagePageActionList(KryptonRibbonBackstagePageDesigner designer)
        : base(designer.Component)
    {
        _designer = designer;
        _service = (IComponentChangeService?)GetService(typeof(IComponentChangeService));
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the page text.
    /// </summary>
    public string Text
    {
        get => BackstagePage.Text;
        set
        {
            if (BackstagePage.Text != value)
            {
                _service?.OnComponentChanged(BackstagePage, null, BackstagePage.Text, value);
                BackstagePage.Text = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the page title text.
    /// </summary>
    public string TextTitle
    {
        get => BackstagePage.TextTitle;
        set
        {
            if (BackstagePage.TextTitle != value)
            {
                _service?.OnComponentChanged(BackstagePage, null, BackstagePage.TextTitle, value);
                BackstagePage.TextTitle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the page description text.
    /// </summary>
    public string TextDescription
    {
        get => BackstagePage.TextDescription;
        set
        {
            if (BackstagePage.TextDescription != value)
            {
                _service?.OnComponentChanged(BackstagePage, null, BackstagePage.TextDescription, value);
                BackstagePage.TextDescription = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the page visibility.
    /// </summary>
    public bool Visible
    {
        get => BackstagePage.Visible;
        set
        {
            if (BackstagePage.Visible != value)
            {
                _service?.OnComponentChanged(BackstagePage, null, BackstagePage.Visible, value);
                BackstagePage.Visible = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the page enabled state.
    /// </summary>
    public bool Enabled
    {
        get => BackstagePage.Enabled;
        set
        {
            if (BackstagePage.Enabled != value)
            {
                _service?.OnComponentChanged(BackstagePage, null, BackstagePage.Enabled, value);
                BackstagePage.Enabled = value;
            }
        }
    }

    /// <summary>
    /// Gets information about the content panel.
    /// </summary>
    public string ContentInfo
    {
        get
        {
            if (BackstagePage.ContentPanel != null)
            {
                var controlType = BackstagePage.ContentPanel.GetType().Name;
                var controlCount = BackstagePage.ContentPanel.Controls.Count;
                return $"{controlType} ({controlCount} controls)";
            }
            return "Default text content";
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Create a new KryptonPanel for custom content.
    /// </summary>
    public void CreateCustomContent()
    {
        if (GetService(typeof(IDesignerHost)) is IDesignerHost host)
        {
            // Create a new KryptonPanel
            var panel = (KryptonPanel?)host.CreateComponent(typeof(KryptonPanel));
            if (panel != null)
            {
                panel.Dock = DockStyle.Fill;
                panel.PanelBackStyle = PaletteBackStyle.PanelClient;
                
                // Set it as the content panel
                _service?.OnComponentChanged(BackstagePage, null, BackstagePage.ContentPanel, panel);
                BackstagePage.ContentPanel = panel;
            }
        }
    }

    /// <summary>
    /// Remove custom content and revert to text content.
    /// </summary>
    public void RemoveCustomContent()
    {
        if (BackstagePage.ContentPanel != null)
        {
            _service?.OnComponentChanged(BackstagePage, null, BackstagePage.ContentPanel, null);
            BackstagePage.ContentPanel = null;
        }
    }

    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        // Create a new collection for holding the single item we want to create
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a control instance at design time
        if (BackstagePage != null)
        {
            // Add the list of backstage page specific actions
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), @"Navigation Text", @"Appearance", @"Text displayed on the navigation button."));
            actions.Add(new DesignerActionPropertyItem(nameof(TextTitle), @"Content Title", @"Appearance", @"Title displayed in the content area (when no custom content is set)."));
            actions.Add(new DesignerActionPropertyItem(nameof(TextDescription), @"Content Description", @"Appearance", @"Description displayed in the content area (when no custom content is set)."));
            
            actions.Add(new DesignerActionHeaderItem(@"Content"));
            actions.Add(new DesignerActionPropertyItem(nameof(ContentInfo), @"Current Content", @"Content", @"Information about the current content."));
            
            if (BackstagePage.ContentPanel == null)
            {
                actions.Add(new DesignerActionMethodItem(this, nameof(CreateCustomContent), @"Create Custom Content", @"Content", @"Create a KryptonPanel for custom content.", true));
            }
            else
            {
                actions.Add(new DesignerActionMethodItem(this, nameof(RemoveCustomContent), @"Remove Custom Content", @"Content", @"Remove custom content and revert to text content.", true));
            }
            
            actions.Add(new DesignerActionHeaderItem(@"Behavior"));
            actions.Add(new DesignerActionPropertyItem(nameof(Visible), @"Visible", @"Behavior", @"Whether the page is visible in the navigation."));
            actions.Add(new DesignerActionPropertyItem(nameof(Enabled), @"Enabled", @"Behavior", @"Whether the page is enabled for user interaction."));
        }

        return actions;
    }
    #endregion

    #region Implementation
    /// <summary>
    /// Gets the KryptonRibbonBackstagePage associated with this action list.
    /// </summary>
    public KryptonRibbonBackstagePage BackstagePage => (KryptonRibbonBackstagePage)Component!;
    #endregion
}
