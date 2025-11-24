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

internal class KryptonLinkLabelActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonLinkLabel _linkLabel;
    private readonly IComponentChangeService? _service;
    private string _action;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonLinkLabelActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonLinkLabelActionList(KryptonLinkLabelDesigner owner)
        : base(owner.Component)
    {
        // Remember the link label instance
        _linkLabel = (owner.Component as KryptonLinkLabel)!;

        // Assuming we were correctly passed an actual component...
        if (_linkLabel != null)
        {
            // Decide on the next action to take given the current setting
            _action = _linkLabel.LinkVisited ? "Link has not been visited" : "Link has been visited";
        }

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the link label style.
    /// </summary>
    public LabelStyle LabelStyle
    {
        get => _linkLabel.LabelStyle;

        set
        {
            if (_linkLabel.LabelStyle != value)
            {
                _service?.OnComponentChanged(_linkLabel, null, _linkLabel.LabelStyle, value);
                _linkLabel.LabelStyle = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the visual orientation.
    /// </summary>
    public VisualOrientation Orientation
    {
        get => _linkLabel.Orientation;

        set
        {
            if (_linkLabel.Orientation != value)
            {
                _service?.OnComponentChanged(_linkLabel, null, _linkLabel.Orientation, value);
                _linkLabel.Orientation = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the link behavior.
    /// </summary>
    public KryptonLinkBehavior LinkBehavior
    {
        get => _linkLabel.LinkBehavior;

        set
        {
            if (_linkLabel.LinkBehavior != value)
            {
                _service?.OnComponentChanged(_linkLabel, null, _linkLabel.LinkBehavior, value);
                _linkLabel.LinkBehavior = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the link visited.
    /// </summary>
    public bool LinkVisited
    {
        get => _linkLabel.LinkVisited;

        set
        {
            if (_linkLabel.LinkVisited != value)
            {
                _service?.OnComponentChanged(_linkLabel, null, _linkLabel.LinkVisited, value);
                _linkLabel.LinkVisited = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the link label text.
    /// </summary>
    public string Text
    {
        get => _linkLabel.Values.Text;

        set
        {
            if (_linkLabel.Values.Text != value)
            {
                _service?.OnComponentChanged(_linkLabel, null, _linkLabel.Values.Text, value);
                _linkLabel.Values.Text = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the extra link label text.
    /// </summary>
    public string ExtraText
    {
        get => _linkLabel.Values.ExtraText;

        set
        {
            if (_linkLabel.Values.ExtraText != value)
            {
                _service?.OnComponentChanged(_linkLabel, null, _linkLabel.Values.ExtraText, value);
                _linkLabel.Values.ExtraText = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the link label image.
    /// </summary>
    public Image? Image
    {
        get => _linkLabel.Values.Image;

        set
        {
            if (_linkLabel.Values.Image != value)
            {
                _service?.OnComponentChanged(_linkLabel, null, _linkLabel.Values.Image, value);
                _linkLabel.Values.Image = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _linkLabel.PaletteMode;

        set
        {
            if (_linkLabel.PaletteMode != value)
            {
                _service?.OnComponentChanged(_linkLabel, null, _linkLabel.PaletteMode, value);
                _linkLabel.PaletteMode = value;
            }
        }
    }

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    public Font StateCommonShortTextFont
    {
        get => _linkLabel.StateCommon.ShortText.Font!;

        set
        {
            if (_linkLabel.StateCommon.ShortText.Font != value)
            {
                _service?.OnComponentChanged(_linkLabel, null, _linkLabel.StateCommon.ShortText.Font, value);

                _linkLabel.StateCommon.ShortText.Font = value;
            }
        }
    }

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    public Font StateCommonLongTextFont
    {
        get => _linkLabel.StateCommon.LongText.Font!;

        set
        {
            if (_linkLabel.StateCommon.LongText.Font != value)
            {
                _service?.OnComponentChanged(_linkLabel, null, _linkLabel.StateCommon.LongText.Font, value);

                _linkLabel.StateCommon.LongText.Font = value;
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
        // Create a new collection for holding the single item we want to create
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a control instance at design time
        if (_linkLabel != null)
        {
            // Add the list of label specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(LabelStyle), @"Style", nameof(Appearance), @"Label style"));
            actions.Add(new DesignerActionPropertyItem(nameof(Orientation), nameof(Orientation), nameof(Appearance), @"Visual orientation"));
            actions.Add(new DesignerActionPropertyItem(nameof(LinkBehavior), @"Link Behavior", nameof(Appearance), @"Underline behavior"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(_action, OnLinkVisitedClick), nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(StateCommonShortTextFont), @"State Common Short Text Font", nameof(Appearance), @"The State Common Short Text Font."));
            actions.Add(new DesignerActionPropertyItem(nameof(StateCommonLongTextFont), @"State Common State Common Long Text Font", nameof(Appearance), @"The State Common State Common Long Text Font."));
            actions.Add(new DesignerActionHeaderItem(@"Values"));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), nameof(Text), @"Values", @"Label text"));
            actions.Add(new DesignerActionPropertyItem(nameof(ExtraText), nameof(ExtraText), @"Values", @"Label extra text"));
            actions.Add(new DesignerActionPropertyItem(nameof(Image), nameof(Image), @"Values", @"Label image"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion

    #region Implementation
    private void OnLinkVisitedClick(object? sender, EventArgs e)
    {
        // Cast to the correct type

        // Double check the source is the expected type
        if (sender is DesignerVerb)
        {
            // Invert the visited setting
            _linkLabel.LinkVisited = !_linkLabel.LinkVisited;

            // Decide on the next action to take given the new setting
            _action = _linkLabel.LinkVisited ? "Link has not been visited" : "Link has been visited";

            // Get the user interface service associated with actions

            // If we managed to get it then request it update to reflect new action setting
            if (GetService(typeof(DesignerActionUIService)) is DesignerActionUIService service)
            {
                service.Refresh(_linkLabel);
            }
        }
    }
    #endregion   
}