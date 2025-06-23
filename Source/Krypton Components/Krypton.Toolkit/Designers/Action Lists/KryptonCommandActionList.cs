#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonCommandActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonCommand _command;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the KryptonCommandActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonCommandActionList(KryptonCommandDesigner owner)
        : base(owner.Component)
    {
        // Remember the panel instance
        _command = (owner.Component as KryptonCommand)!;

        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    #endregion

    #region Public

    public string Text
    {
        get => _command.Text;

        set
        {
            if (_command.Text != value)
            {
                _service?.OnComponentChanged(_command, null, _command.Text, value);

                _command.Text = value;
            }
        }
    }

    public Image? ImageSmall
    {
        get => _command.ImageSmall;

        set
        {
            if (_command.ImageSmall != value)
            {
                _service?.OnComponentChanged(_command, null, _command.ImageSmall, value);

                _command.ImageSmall = value;
            }
        }
    }

    public Image? ImageLarge
    {
        get => _command.ImageLarge;

        set
        {
            if (_command.ImageLarge != value)
            {
                _service?.OnComponentChanged(_command, null, _command.ImageLarge, value);

                _command.ImageLarge = value;
            }
        }
    }

    public Color ImageTransparentColor
    {
        get => _command.ImageTransparentColor;

        set
        {
            if (_command.ImageTransparentColor != value)
            {
                _service?.OnComponentChanged(_command, null, _command.ImageTransparentColor, value);

                _command.ImageTransparentColor = value;
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

        // This can be null when deleting a component instance at design time
        if (_command != null)
        {
            // Add the list of command specific actions
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(Text), @"Text", nameof(Appearance), @"Command text."));
            actions.Add(new DesignerActionPropertyItem(nameof(ImageSmall), @"Image Small", nameof(Appearance), @"Command small image."));
            actions.Add(new DesignerActionPropertyItem(nameof(ImageLarge), @"Image Large", nameof(Appearance), @"Command large image."));
            actions.Add(new DesignerActionPropertyItem(nameof(ImageTransparentColor), @"Image Transparent Color", nameof(Appearance), @"Command image transparent color."));
        }

        return actions;
    }
    #endregion
}