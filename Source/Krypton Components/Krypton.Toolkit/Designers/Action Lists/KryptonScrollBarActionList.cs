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

internal class KryptonScrollBarActionList : DesignerActionList
{
    #region Instance Fields

    private readonly KryptonScrollBar _scrollBar;
    private readonly IComponentChangeService? _service;

    #endregion

    #region Identity

    public KryptonScrollBarActionList(KryptonScrollBarDesigner owner) : base(owner.Component)
    {
        _scrollBar = (owner.Component as KryptonScrollBar)!;

        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    #endregion

    #region Public

    public int Maximum
    {
        get => _scrollBar.Maximum;

        set
        {
            if (_scrollBar.Maximum != value)
            {
                _service?.OnComponentChanged(_scrollBar, null, _scrollBar.Maximum, value);

                _scrollBar.Maximum = value;
            }
        }
    }

    public int Minimum
    {
        get => _scrollBar.Minimum;

        set
        {
            if (_scrollBar.Minimum != value)
            {
                _service?.OnComponentChanged(_scrollBar, null, _scrollBar.Minimum, value);

                _scrollBar.Minimum = value;
            }
        }
    }

    public int Value
    {
        get => _scrollBar.Value;

        set
        {
            if (_scrollBar.Value != value)
            {
                _service?.OnComponentChanged(_scrollBar, null, _scrollBar.Value, value);

                _scrollBar.Value = value;
            }
        }
    }

    /// <summary>Gets or sets the orientation of the scrollbar.</summary>
    /// <value>The orientation.</value>
    public ScrollBarOrientation Orientation
    {
        get => _scrollBar.Orientation;

        set
        {
            if (_scrollBar.Orientation != value)
            {
                _service?.OnComponentChanged(_scrollBar, null, _scrollBar.Orientation, value);

                _scrollBar.Orientation = value;
            }
        }
    }
    #endregion

    #region Public Override

    /// <summary>Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem">DesignerActionItem</see> objects contained in the list.</summary>
    /// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem">DesignerActionItem</see> array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        if (_scrollBar != null)
        {
            actions.Add(new DesignerActionHeaderItem(nameof(Appearance)));
            actions.Add(new DesignerActionPropertyItem(nameof(Orientation), nameof(Orientation), nameof(Appearance), @"The appearance of the scrollbar."));
            actions.Add(new DesignerActionHeaderItem(@"Values"));
            actions.Add(new DesignerActionPropertyItem(nameof(Maximum), nameof(Maximum), @"Values", @"The maximum value that the scrollbar can accept."));
            actions.Add(new DesignerActionPropertyItem(nameof(Minimum), nameof(Minimum), @"Values", @"The minimum value that the scrollbar can accept."));
            actions.Add(new DesignerActionPropertyItem(nameof(Value), nameof(Value), @"Values", @"The current value of the scrollbar."));

        }

        return actions;
    }

    #endregion
}