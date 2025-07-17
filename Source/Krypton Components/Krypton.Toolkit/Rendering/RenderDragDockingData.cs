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
/// Aggregates information needed for rendering drag and drop indicators.
/// </summary>
public class RenderDragDockingData
{
    #region Instance Fields
    private int _showTotal;
    private BoolFlags31 _flags;
    private readonly Rectangle[] _rects;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteDragData class.
    /// </summary>
    /// <param name="showLeft">Should the left docking indicator be shown.</param>
    /// <param name="showRight">Should the right docking indicator be shown.</param>
    /// <param name="showTop">Should the top docking indicator be shown.</param>
    /// <param name="showBottom">Should the bottom docking indicator be shown.</param>
    /// <param name="showMiddle">Should the middle docking indicator be shown.</param>
    public RenderDragDockingData(bool showLeft, bool showRight, 
        bool showTop, bool showBottom, 
        bool showMiddle)
    {
        _flags = new BoolFlags31();

        // Set initial settings (ShowBack is auto calculated from other flags)
        ShowLeft = showLeft;
        ShowRight = showRight;
        ShowTop = showTop;
        ShowBottom = showBottom;
        ShowMiddle = showMiddle;

        // Default valies
        DockWindowSize = Size.Empty;
        _rects = new Rectangle[5];
        for (var i = 0; i < _rects.Length; i++)
        {
            _rects[i] = Rectangle.Empty;
        }
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets the visible state of the background.
    /// </summary>
    public bool ShowBack => _showTotal > 1;

    /// <summary>
    /// Gets and sets the visible state of the left indicator.
    /// </summary>
    public bool ShowLeft
    {
        get => _flags.AreFlagsSet(0x0002);
        set => UpdateShowFlag(value, 0x0002);
    }

    /// <summary>
    /// Gets and sets the visible state of the right indicator.
    /// </summary>
    public bool ShowRight
    {
        get => _flags.AreFlagsSet(0x0004);
        set => UpdateShowFlag(value, 0x0004);
    }

    /// <summary>
    /// Gets and sets the visible state of the top indicator.
    /// </summary>
    public bool ShowTop
    {
        get => _flags.AreFlagsSet(0x0008);
        set => UpdateShowFlag(value, 0x0008);
    }

    /// <summary>
    /// Gets and sets the visible state of the bottom indicator.
    /// </summary>
    public bool ShowBottom
    {
        get => _flags.AreFlagsSet(0x0010);
        set => UpdateShowFlag(value, 0x0010);
    }

    /// <summary>
    /// Gets and sets the visible state of the middle indicator.
    /// </summary>
    public bool ShowMiddle
    {
        get => _flags.AreFlagsSet(0x0020);
        set => UpdateShowFlag(value, 0x0020);
    }

    /// <summary>
    /// Gets the set of flags associated with active
    /// </summary>
    public int ActiveFlags => _flags.Flags & 0x07C0;

    /// <summary>
    /// Gets and sets the active state of left indicator.
    /// </summary>
    public bool ActiveLeft
    {
        get => _flags.AreFlagsSet(0x0040);
        set => UpdateFlag(value, 0x0040);
    }

    /// <summary>
    /// Gets and sets the active state of right indicator.
    /// </summary>
    public bool ActiveRight
    {
        get => _flags.AreFlagsSet(0x0080);
        set => UpdateFlag(value, 0x0080);
    }

    /// <summary>
    /// Gets and sets the active state of top indicator.
    /// </summary>
    public bool ActiveTop
    {
        get => _flags.AreFlagsSet(0x0100);
        set => UpdateFlag(value, 0x0100);
    }

    /// <summary>
    /// Gets and sets the active state of bottom indicator.
    /// </summary>
    public bool ActiveBottom
    {
        get => _flags.AreFlagsSet(0x0200);
        set => UpdateFlag(value, 0x0200);
    }

    /// <summary>
    /// Gets and sets the active state of middle indicator.
    /// </summary>
    public bool ActiveMiddle
    {
        get => _flags.AreFlagsSet(0x0400);
        set => UpdateFlag(value, 0x0400);
    }

    /// <summary>
    /// Gets if any of the docking indicators are active.
    /// </summary>
    public bool AnyActive => ActiveFlags != 0;

    /// <summary>
    /// Clear all the active flags.
    /// </summary>
    public void ClearActive() => _flags.ClearFlags(0x07C0);

    /// <summary>
    /// Gets and sets the hot rectangle of the left docking indicator.
    /// </summary>
    public Rectangle RectLeft
    {
        get => _rects[0];
        set => _rects[0] = value;
    }

    /// <summary>
    /// Gets and sets the hot rectangle of the right docking indicator.
    /// </summary>
    public Rectangle RectRight
    {
        get => _rects[1];
        set => _rects[1] = value;
    }

    /// <summary>
    /// Gets and sets the hot rectangle of the top docking indicator.
    /// </summary>
    public Rectangle RectTop
    {
        get => _rects[2];
        set => _rects[2] = value;
    }

    /// <summary>
    /// Gets and sets the hot rectangle of the bottom docking indicator.
    /// </summary>
    public Rectangle RectBottom
    {
        get => _rects[3];
        set => _rects[3] = value;
    }

    /// <summary>
    /// Gets and sets the hot rectangle of the middle docking indicator.
    /// </summary>
    public Rectangle RectMiddle
    {
        get => _rects[4];
        set => _rects[4] = value;
    }

    /// <summary>
    /// Gets and sets size of the docking window required.
    /// </summary>
    public Size DockWindowSize { get; set; }

    #endregion

    #region Implementation
    private void UpdateFlag(bool value, int flag)
    {
        if (value)
        {
            _flags.SetFlags(flag);
        }
        else
        {
            _flags.ClearFlags(flag);
        }
    }

    private void UpdateShowFlag(bool value, int flag)
    {
        if (value != _flags.AreFlagsSet(flag))
        {
            if (value)
            {
                _flags.SetFlags(flag);
                _showTotal++;
            }
            else
            {
                _flags.ClearFlags(flag);
                _showTotal--;
            }
        }
    }
    #endregion
}