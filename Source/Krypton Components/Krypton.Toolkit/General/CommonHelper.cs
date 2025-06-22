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

// ReSharper disable UnusedMember.Global

namespace Krypton.Toolkit;

#region Delegates
/// <summary>
/// Signature of a bare method.
/// </summary>
public delegate void SimpleCall();

/// <summary>
/// Signature of a method that performs an operation.
/// </summary>
/// <param name="parameter">Operation parameter.</param>
/// <returns>Operation result.</returns>
public delegate object Operation(object? parameter);

/// <summary>
/// Signature of a method that returns a ToolStripRenderer instance.
/// </summary>
public delegate ToolStripRenderer GetToolStripRenderer();
#endregion

/// <summary>
/// Set of common helper routines for the Toolkit
/// </summary>
public static class CommonHelper
{
    #region Static Fields
    private const int VK_SHIFT = 0x10;
    private const int VK_CONTROL = 0x11;
    private const int VK_MENU = 0x12;

    private static readonly char[] _singleDateFormat = ['d', 'f', 'F', 'g', 'h', 'H', 'K', 'm', 'M', 's', 't', 'y', 'z'
    ];
    //private static readonly int[] _daysInMonth = new int[12] { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334 };

    private static int _nextId = 1000;
    //private static readonly DateTime _baseDate = new(2000, 1, 1);
    private static PropertyInfo? _cachedShortcutPI;
    private static PropertyInfo? _cachedDesignModePI;
    private static MethodInfo? _cachedShortcutMI;
    private static NullContentValues? _nullContentValues;
    private static readonly DoubleConverter _dc = new DoubleConverter();
    private static readonly SizeConverter _sc = new SizeConverter();
    private static readonly PointConverter _pc = new PointConverter();
    private static readonly BooleanConverter _bc = new BooleanConverter();
    private static readonly ColorConverter _cc = new ColorConverter();

    #endregion

    /// <summary>
    /// Gets access to the global null point value.
    /// </summary>
    public static Point NullPoint
    {
        [DebuggerStepThrough]
        get;
    } = new Point(int.MaxValue, int.MaxValue);

    /// <summary>
    /// Gets access to the global null rectangle value.
    /// </summary>
    public static Rectangle NullRectangle
    {
        [DebuggerStepThrough]
        get;
    } = new Rectangle(int.MaxValue, int.MaxValue, 0, 0);

    /// <summary>
    /// Color matrix used to adjust colors to look disabled.
    /// </summary>
    public static ColorMatrix MatrixDisabled
    {
        [DebuggerStepThrough]
        get;
    } = new ColorMatrix([
        [0.3f, 0.3f, 0.3f, 0, 0], [0.59f, 0.59f, 0.59f, 0, 0],
        [0.11f, 0.11f, 0.11f, 0, 0], [0, 0, 0, 0.5f, 0], [0, 0, 0, 0, 1]
    ]);

    /// <summary>
    /// Gets the next global identifier in sequence.
    /// </summary>
    public static int NextId
    {
        [DebuggerStepThrough]
        get => _nextId++;
    }

    /// <summary>
    /// Converts line breaks in the string to the system default line break, Environment.NewLine.
    /// </summary>
    /// <param name="text">String to process.</param>
    /// <returns>
    /// Normalized resultant string.<br/>
    /// If the input string is an empty string, the input string is returned.
    /// </returns>
    public static string NormalizeLineBreaks(string text)
    {
        string result = text;

        if (result.Length > 0)
        {
            // Convert line breaks to the system provided line break
            if (!Environment.NewLine.Equals("\r\n"))
            {
                result = Regex.Replace(result, @"\r\n", Environment.NewLine);
            }

            if (!Environment.NewLine.Equals("\n"))
            {
                // Replaces \n but not \r\n
                result = Regex.Replace(result, "(?<!\r)\n", Environment.NewLine);
            }

            if (!Environment.NewLine.Equals("\r"))
            {
                // Replaces \r but not \r\n
                result = Regex.Replace(result, "\r(?!\n)", Environment.NewLine);
            }
        }

        return result;
    }

    /// <summary>
    /// Gets a string that is guaranteed to be unique.
    /// </summary>
    public static string UniqueString
    {
        get
        {
            // Generate a GUID that is guaranteed to be unique
            var guid = Guid.NewGuid();
            // Return as a hex formatted string.
            return guid.ToString(@"N");
        }
    }

    /// <summary>
    /// Gets the padding value used when inheritance is needed.
    /// </summary>
    public static Padding InheritPadding
    {
        [DebuggerStepThrough]
        get;
    } = new Padding(-1);

    /// <summary>
    /// Check a short-cut menu for a matching short and invoke that item if found.
    /// </summary>
    /// <param name="cms">ContextMenuStrip instance to check.</param>
    /// <param name="msg">Windows message that generated check.</param>
    /// <param name="keyData">Keyboard shortcut to check.</param>
    /// <returns>True if shortcut processed; otherwise false.</returns>
    public static bool CheckContextMenuForShortcut(ContextMenuStrip? cms,
        ref Message msg,
        Keys keyData)
    {
        if (cms != null)
        {
            // Cache the info needed to sneak access to the context menu strip
            if (_cachedShortcutPI == null)
            {
                _cachedShortcutPI = typeof(ToolStrip).GetProperty(@"Shortcuts",
                    BindingFlags.Instance |
                    BindingFlags.GetProperty |
                    BindingFlags.NonPublic);

                _cachedShortcutMI = typeof(ToolStripMenuItem).GetMethod("ProcessCmdKey",
                    BindingFlags.Instance |
                    BindingFlags.NonPublic);
            }

            // Get any menu item from context strip that matches the shortcut key combination
            var hashTableShortCuts = _cachedShortcutPI!.GetValue(cms, null) as Hashtable;
            ToolStripMenuItem? menuItem = null;

            if (hashTableShortCuts is null)
            {
                if (_cachedShortcutPI.GetValue(cms, null) is Dictionary<Keys, ToolStripMenuItem> dictionaryShortcuts)
                {
                    dictionaryShortcuts.TryGetValue(keyData, out menuItem);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                menuItem = hashTableShortCuts[keyData] as ToolStripMenuItem;
            }

            // If we found a match...
            if (menuItem is not null)
            {
                // Get the menu item to process the shortcut
                var ret = _cachedShortcutMI!.Invoke(menuItem, new object[] { msg, keyData });

                // Return the 'ProcessCmdKey' result
                if (ret != null)
                {
                    return (bool)ret;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Gets reference to a null implementation of the IContentValues interface.
    /// </summary>
    public static IContentValues NullContentValues => _nullContentValues ??= new NullContentValues();

    /// <summary>
    /// Return the provided size with orientation specific padding applied.
    /// </summary>
    /// <param name="orientation">Orientation to apply padding with.</param>
    /// <param name="size">Starting size.</param>
    /// <param name="padding">Padding to be applied.</param>
    /// <returns>Updated size.</returns>
    public static Size ApplyPadding(Orientation orientation, Size size, Padding padding)
    {
        // Ignore an empty padding value
        if (!padding.Equals(InheritPadding))
        {
            // The orientation determines how the border padding is 
            // applied to the preferred size of the children
            switch (orientation)
            {
                case Orientation.Vertical:
                    size.Width += padding.Vertical;
                    size.Height += padding.Horizontal;
                    break;
                case Orientation.Horizontal:
                    size.Width += padding.Horizontal;
                    size.Height += padding.Vertical;
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(orientation.ToString());
                    break;
            }
        }

        return size;
    }

    /// <summary>
    /// Return the provided size with visual orientation specific padding applied.
    /// </summary>
    /// <param name="orientation">Orientation to apply padding with.</param>
    /// <param name="size">Starting size.</param>
    /// <param name="padding">Padding to be applied.</param>
    /// <returns>Updated size.</returns>
    public static Size ApplyPadding(VisualOrientation orientation,
        Size size,
        Padding padding)
    {
        // Ignore an empty padding value
        if (!padding.Equals(InheritPadding))
        {
            // The orientation determines how the border padding is 
            // applied to the preferred size of the children
            switch (orientation)
            {
                case VisualOrientation.Top:
                case VisualOrientation.Bottom:
                    size.Width += padding.Horizontal;
                    size.Height += padding.Vertical;
                    break;
                case VisualOrientation.Left:
                case VisualOrientation.Right:
                    size.Width += padding.Vertical;
                    size.Height += padding.Horizontal;
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(orientation.ToString());
                    break;
            }
        }

        return size;
    }

    /// <summary>
    /// Return the provided rectangle with orientation specific padding applied.
    /// </summary>
    /// <param name="orientation">Orientation to apply padding with.</param>
    /// <param name="rect">Starting rectangle.</param>
    /// <param name="padding">Padding to be applied.</param>
    /// <returns>Updated rectangle.</returns>
    public static Rectangle ApplyPadding(Orientation orientation,
        Rectangle rect,
        Padding padding)
    {
        // Ignore an empty padding value
        if (!padding.Equals(InheritPadding))
        {
            // The orientation determines how the border padding is 
            // applied to the preferred size of the children
            switch (orientation)
            {
                case Orientation.Horizontal:
                    rect.X += padding.Left;
                    rect.Width -= padding.Horizontal;
                    rect.Y += padding.Top;
                    rect.Height -= padding.Vertical;
                    break;
                case Orientation.Vertical:
                    rect.X += padding.Top;
                    rect.Width -= padding.Vertical;
                    rect.Y += padding.Right;
                    rect.Height -= padding.Horizontal;
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(orientation.ToString());
                    break;
            }
        }

        return rect;
    }

    /// <summary>
    /// Return the provided rectangle with visual orientation specific padding applied.
    /// </summary>
    /// <param name="orientation">Orientation to apply padding with.</param>
    /// <param name="rect">Starting rectangle.</param>
    /// <param name="padding">Padding to be applied.</param>
    /// <returns>Updated rectangle.</returns>
    public static Rectangle ApplyPadding(VisualOrientation orientation,
        Rectangle rect,
        Padding padding)
    {
        // Ignore an empty padding value
        if (!padding.Equals(InheritPadding))
        {
            // The orientation determines how the border padding is 
            // used to reduce the space available for children
            switch (orientation)
            {
                case VisualOrientation.Top:
                    rect = new Rectangle(rect.X + padding.Left, rect.Y + padding.Top,
                        rect.Width - padding.Horizontal, rect.Height - padding.Vertical);
                    break;
                case VisualOrientation.Bottom:
                    rect = new Rectangle(rect.X + padding.Right, rect.Y + padding.Bottom,
                        rect.Width - padding.Horizontal, rect.Height - padding.Vertical);
                    break;
                case VisualOrientation.Left:
                    rect = new Rectangle(rect.X + padding.Top, rect.Y + padding.Right,
                        rect.Width - padding.Vertical, rect.Height - padding.Horizontal);
                    break;
                case VisualOrientation.Right:
                    rect = new Rectangle(rect.X + padding.Bottom, rect.Y + padding.Left,
                        rect.Width - padding.Vertical, rect.Height - padding.Horizontal);
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    DebugTools.NotImplemented(orientation.ToString());
                    break;
            }
        }

        return rect;
    }

    /// <summary>
    /// Modify the incoming padding to reflect the visual orientation.
    /// </summary>
    /// <param name="orientation">Orientation to apply to padding.</param>
    /// <param name="padding">Padding to be modified.</param>
    /// <returns>Updated padding.</returns>
    public static Padding OrientatePadding(VisualOrientation orientation,
        Padding padding)
    {
        switch (orientation)
        {
            case VisualOrientation.Top:
                return padding;
            case VisualOrientation.Bottom:
                return new Padding(padding.Right, padding.Bottom, padding.Left, padding.Top);
            case VisualOrientation.Left:
                return new Padding(padding.Top, padding.Right, padding.Bottom, padding.Left);
            case VisualOrientation.Right:
                return new Padding(padding.Bottom, padding.Left, padding.Top, padding.Right);
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(orientation.ToString());
                return padding;
        }
    }

    /// <summary>
    /// Swap the width and height values for the rectangle.
    /// </summary>
    /// <param name="rect">Rectangle to modify.</param>
    [DebuggerStepThrough]
    public static void SwapRectangleSizes(ref Rectangle rect) => (rect.Width, rect.Height) = (rect.Height, rect.Width);

    /// <summary>
    /// Gets the form level right to left setting.
    /// </summary>
    /// <param name="control">Control for which the setting is needed.</param>
    /// <returns>RightToLeft setting.</returns>
    public static bool GetRightToLeftLayout(Control control)
    {
        // Default to left-to-right layout
        var rtl = false;

        // We need a valid control to find a top level form
        // Search for a top level form associated with the control
        Form? topForm = control.FindForm();

        // If can find an owning form
        if (topForm != null)
        {
            // Use the form setting instead
            rtl = topForm.RightToLeftLayout;
        }

        return rtl;
    }

    /// <summary>
    /// Decide if the context menu strip should be Displayed.
    /// </summary>
    /// <param name="cms">Reference to context menu strip.</param>
    /// <returns>True to display; otherwise false.</returns>
    public static bool ValidContextMenuStrip(ContextMenuStrip? cms) =>
        // Must be a valid reference to examine
        cms is { Items.Count: > 0 };

    /// <summary>
    /// Decide if the KryptonContextMenu should be Displayed.
    /// </summary>
    /// <param name="kcm">Reference to context menu strip.</param>
    /// <returns>True to display; otherwise false.</returns>
    public static bool ValidKryptonContextMenu(KryptonContextMenu? kcm) =>
        // Must be a valid reference to examine
        kcm is { Items.Count: > 0 };

    /// <summary>
    /// Perform operation in a worker thread with wait dialog in main thread.
    /// </summary>
    /// <param name="op">Delegate of operation to be performed.</param>
    /// <param name="parameter">Parameter to be passed into the operation.</param>
    /// <returns>Result of performing the operation.</returns>
    public static object PerformOperation(Operation op, object? parameter)
    {
        // Create a modal window for showing feedback
        using var wait = new ModalWaitDialog();
        // Create the object that runs the operation in a separate thread
        var opThread = new OperationThread(op, parameter);

        // Create the actual thread and provide thread entry point
        var thread = new Thread(opThread.Run);

        // Kick off the thread action
        thread.Start();

        // Keep looping until the thread is finished
        while (opThread.State == 0)
        {
            // Sleep to allow thread to perform more work
            Thread.Sleep(25);

            // Give the feedback dialog a chance to update
            wait.UpdateDialog();
        }

        // Process operation result
        switch (opThread.State)
        {
            case 1:
                return opThread.Result;
            case 2:
                throw opThread.Exception;
            default:
                // Should never happen!
                Debug.Assert(false);
                throw DebugTools.NotImplemented(opThread.State.ToString());
        }
    }

    /// <summary>
    /// Gets a value indicating if the provided value is an override state.
    /// </summary>
    /// <param name="state">Specific state.</param>
    /// <returns>True if an override state; otherwise false.</returns>
    [DebuggerStepThrough]
    public static bool IsOverrideState(PaletteState state) => (state & PaletteState.Override) == PaletteState.Override;

    /// <summary>
    /// Gets a value indicating if the provided value is an override state but excludes one value.
    /// </summary>
    /// <param name="state">Specific state.</param>
    /// <param name="exclude">State that should be excluded from test.</param>
    /// <returns>True if an override state; otherwise false.</returns>
    [DebuggerStepThrough]
    public static bool IsOverrideStateExclude(PaletteState state, PaletteState exclude) => (state != exclude) && IsOverrideState(state);

    /// <summary>
    /// Gets a value indicating if the enumeration specifies no borders.
    /// </summary>
    /// <param name="borders">Enumeration for borders.</param>
    /// <returns>True if no border specified; otherwise false.</returns>
    [DebuggerStepThrough]
    public static bool HasNoBorders(PaletteDrawBorders borders) => (borders & PaletteDrawBorders.All) == PaletteDrawBorders.None;

    /// <summary>
    /// Gets a value indicating if the enumeration specifies at least one border.
    /// </summary>
    /// <param name="borders">Enumeration for borders.</param>
    /// <returns>True if at least one border specified; otherwise false.</returns>
    [DebuggerStepThrough]
    public static bool HasABorder(PaletteDrawBorders borders) => (borders & PaletteDrawBorders.All) != PaletteDrawBorders.None;

    /// <summary>
    /// Gets a value indicating if the enumeration specifies at least one border.
    /// </summary>
    /// <param name="borders">Enumeration for borders.</param>
    /// <returns>True if at least one border specified; otherwise false.</returns>
    [DebuggerStepThrough]
    public static bool HasOneBorder(PaletteDrawBorders borders)
    {
        PaletteDrawBorders justBorders = borders & PaletteDrawBorders.All;

        // If borders value equals just one of the edges
        return justBorders is PaletteDrawBorders.Top or PaletteDrawBorders.Bottom or PaletteDrawBorders.Left or PaletteDrawBorders.Right;
    }

    /// <summary>
    /// Gets a value indicating if the enumeration includes the top border.
    /// </summary>
    /// <param name="borders">Enumeration for borders.</param>
    /// <returns>True if includes the top border; otherwise false.</returns>
    [DebuggerStepThrough]
    public static bool HasTopBorder(PaletteDrawBorders borders) => (borders & PaletteDrawBorders.Top) == PaletteDrawBorders.Top;

    /// <summary>
    /// Gets a value indicating if the enumeration includes the bottom border.
    /// </summary>
    /// <param name="borders">Enumeration for borders.</param>
    /// <returns>True if includes the bottom border; otherwise false.</returns>
    [DebuggerStepThrough]
    public static bool HasBottomBorder(PaletteDrawBorders borders) => (borders & PaletteDrawBorders.Bottom) == PaletteDrawBorders.Bottom;

    /// <summary>
    /// Gets a value indicating if the enumeration includes the left border.
    /// </summary>
    /// <param name="borders">Enumeration for borders.</param>
    /// <returns>True if includes the left border; otherwise false.</returns>
    [DebuggerStepThrough]
    public static bool HasLeftBorder(PaletteDrawBorders borders) => (borders & PaletteDrawBorders.Left) == PaletteDrawBorders.Left;

    /// <summary>
    /// Gets a value indicating if the enumeration includes the right border.
    /// </summary>
    /// <param name="borders">Enumeration for borders.</param>
    /// <returns>True if includes the right border; otherwise false.</returns>
    [DebuggerStepThrough]
    public static bool HasRightBorder(PaletteDrawBorders borders) => (borders & PaletteDrawBorders.Right) == PaletteDrawBorders.Right;

    /// <summary>
    /// Gets a value indicating if the enumeration specifies all four borders.
    /// </summary>
    /// <param name="borders">Enumeration for borders.</param>
    /// <returns>True if all four borders specified; otherwise false.</returns>
    [DebuggerStepThrough]
    public static bool HasAllBorders(PaletteDrawBorders borders) => (borders & PaletteDrawBorders.All) == PaletteDrawBorders.All;

    /// <summary>
    /// Apply an orientation to the draw border edges to get a correct value.
    /// </summary>
    /// <param name="borders">Border edges to be drawn.</param>
    /// <param name="orientation">How to adjust the border edges.</param>
    /// <returns>Border edges adjusted for orientation.</returns>
    public static PaletteDrawBorders OrientateDrawBorders(PaletteDrawBorders borders,
        VisualOrientation orientation)
    {
        // No need to perform a change for top orientation
        if (orientation == VisualOrientation.Top)
        {
            return borders;
        }

        // No need to change All or None values
        if (borders is PaletteDrawBorders.All or PaletteDrawBorders.None)
        {
            return borders;
        }

        var ret = PaletteDrawBorders.None;

        // Apply orientation change to each side in turn
        switch (orientation)
        {
            case VisualOrientation.Bottom:
                // Invert sides
                if (HasTopBorder(borders))
                {
                    ret |= PaletteDrawBorders.Bottom;
                }

                if (HasBottomBorder(borders))
                {
                    ret |= PaletteDrawBorders.Top;
                }

                if (HasLeftBorder(borders))
                {
                    ret |= PaletteDrawBorders.Right;
                }

                if (HasRightBorder(borders))
                {
                    ret |= PaletteDrawBorders.Left;
                }
                break;

            case VisualOrientation.Left:
                // Rotate one anti-clockwise
                if (HasTopBorder(borders))
                {
                    ret |= PaletteDrawBorders.Left;
                }

                if (HasBottomBorder(borders))
                {
                    ret |= PaletteDrawBorders.Right;
                }

                if (HasLeftBorder(borders))
                {
                    ret |= PaletteDrawBorders.Bottom;
                }

                if (HasRightBorder(borders))
                {
                    ret |= PaletteDrawBorders.Top;
                }
                break;

            case VisualOrientation.Right:
                // Rotate sides one clockwise
                if (HasTopBorder(borders))
                {
                    ret |= PaletteDrawBorders.Right;
                }

                if (HasBottomBorder(borders))
                {
                    ret |= PaletteDrawBorders.Left;
                }

                if (HasLeftBorder(borders))
                {
                    ret |= PaletteDrawBorders.Top;
                }

                if (HasRightBorder(borders))
                {
                    ret |= PaletteDrawBorders.Bottom;
                }
                break;

            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(orientation.ToString());
                break;
        }

        return ret;
    }

    /// <summary>
    /// Apply a reversed orientation so that when orientated again it comes out with the original value.
    /// </summary>
    /// <param name="borders">Border edges to be drawn.</param>
    /// <param name="orientation">How to adjust the border edges.</param>
    /// <returns>Border edges adjusted for orientation.</returns>
    public static PaletteDrawBorders ReverseOrientateDrawBorders(PaletteDrawBorders borders,
        VisualOrientation orientation)
    {
        // No need to perform a change for top orientation
        if (orientation == VisualOrientation.Top)
        {
            return borders;
        }

        // No need to change the "All" or "None" values
        if (borders is PaletteDrawBorders.All or PaletteDrawBorders.None)
        {
            return borders;
        }

        var ret = PaletteDrawBorders.None;

        // Apply orientation change to each side in turn
        switch (orientation)
        {
            case VisualOrientation.Bottom:
                // Invert sides
                if (HasTopBorder(borders))
                {
                    ret |= PaletteDrawBorders.Bottom;
                }

                if (HasBottomBorder(borders))
                {
                    ret |= PaletteDrawBorders.Top;
                }

                if (HasLeftBorder(borders))
                {
                    ret |= PaletteDrawBorders.Right;
                }

                if (HasRightBorder(borders))
                {
                    ret |= PaletteDrawBorders.Left;
                }
                break;

            case VisualOrientation.Right:
                // Rotate one anti-clockwise
                if (HasTopBorder(borders))
                {
                    ret |= PaletteDrawBorders.Left;
                }

                if (HasBottomBorder(borders))
                {
                    ret |= PaletteDrawBorders.Right;
                }

                if (HasLeftBorder(borders))
                {
                    ret |= PaletteDrawBorders.Bottom;
                }

                if (HasRightBorder(borders))
                {
                    ret |= PaletteDrawBorders.Top;
                }
                break;

            case VisualOrientation.Left:
                // Rotate sides one clockwise
                if (HasTopBorder(borders))
                {
                    ret |= PaletteDrawBorders.Right;
                }

                if (HasBottomBorder(borders))
                {
                    ret |= PaletteDrawBorders.Left;
                }

                if (HasLeftBorder(borders))
                {
                    ret |= PaletteDrawBorders.Top;
                }

                if (HasRightBorder(borders))
                {
                    ret |= PaletteDrawBorders.Bottom;
                }
                break;

            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(orientation.ToString());
                break;
        }

        return ret;
    }

    /// <summary>
    /// Convert from VisualOrientation to Orientation.
    /// </summary>
    /// <param name="orientation">VisualOrientation value.</param>
    /// <returns>Orientation value.</returns>
    public static Orientation VisualToOrientation(VisualOrientation orientation)
    {
        switch (orientation)
        {
            case VisualOrientation.Top:
            case VisualOrientation.Bottom:
                return Orientation.Vertical;
            case VisualOrientation.Left:
            case VisualOrientation.Right:
                return Orientation.Horizontal;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(orientation.ToString());
                return Orientation.Vertical;
        }
    }

    /// <summary>
    /// Convert from ButtonStyle to PaletteButtonStyle.
    /// </summary>
    /// <param name="style">ButtonStyle to convert.</param>
    /// <returns>PaletteButtonStyle enumeration value.</returns>
    public static PaletteButtonStyle ButtonStyleToPalette(ButtonStyle style)
    {
        switch (style)
        {
            case ButtonStyle.Standalone:
                return PaletteButtonStyle.Standalone;
            case ButtonStyle.Alternate:
                return PaletteButtonStyle.Alternate;
            case ButtonStyle.LowProfile:
                return PaletteButtonStyle.LowProfile;
            case ButtonStyle.ButtonSpec:
                return PaletteButtonStyle.ButtonSpec;
            case ButtonStyle.BreadCrumb:
                return PaletteButtonStyle.BreadCrumb;
            case ButtonStyle.Cluster:
                return PaletteButtonStyle.Cluster;
            case ButtonStyle.NavigatorStack:
                return PaletteButtonStyle.NavigatorStack;
            case ButtonStyle.NavigatorOverflow:
                return PaletteButtonStyle.NavigatorOverflow;
            case ButtonStyle.NavigatorMini:
                return PaletteButtonStyle.NavigatorMini;
            case ButtonStyle.InputControl:
                return PaletteButtonStyle.InputControl;
            case ButtonStyle.ListItem:
                return PaletteButtonStyle.ListItem;
            case ButtonStyle.Form:
                return PaletteButtonStyle.Form;
            case ButtonStyle.FormClose:
                return PaletteButtonStyle.FormClose;
            case ButtonStyle.Command:
                return PaletteButtonStyle.Command;
            case ButtonStyle.Custom1:
                return PaletteButtonStyle.Custom1;
            case ButtonStyle.Custom2:
                return PaletteButtonStyle.Custom2;
            case ButtonStyle.Custom3:
                return PaletteButtonStyle.Custom3;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(style.ToString());
                return PaletteButtonStyle.Standalone;
        }
    }

    /// <summary>
    /// Create a graphics path that describes a rounded rectangle.
    /// </summary>
    /// <param name="rect">Rectangle to become rounded.</param>
    /// <param name="rounding">The rounding factor to apply.</param>
    /// <returns>GraphicsPath instance.</returns>
    public static GraphicsPath RoundedRectanglePath(Rectangle rect, int rounding)
    {
        var roundedPath = new GraphicsPath();

        // Only use a rounding that will fit inside the rect
        rounding = Math.Min(rounding, Math.Min(rect.Width / 2, rect.Height / 2) - rounding);

        // If there is no room for any rounding effect...
        if (rounding <= 0)
        {
            // Just add a simple rectangle as a quick way of adding four lines
            roundedPath.AddRectangle(rect);
        }
        else
        {
            // We create the path using a floating point rectangle
            var rectF = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);

            // The border is made of up a quarter of a circle arc, in each corner
            var arcLength = rounding * 2;
            roundedPath.AddArc(rectF.Left, rectF.Top, arcLength, arcLength, 180f, 90f);
            roundedPath.AddArc(rectF.Right - arcLength, rectF.Top, arcLength, arcLength, 270f, 90f);
            roundedPath.AddArc(rectF.Right - arcLength, rectF.Bottom - arcLength, arcLength, arcLength, 0f, 90f);
            roundedPath.AddArc(rectF.Left, rectF.Bottom - arcLength, arcLength, arcLength, 90f, 90f);

            // Make the last and first arc join up
            roundedPath.CloseFigure();
        }

        return roundedPath;
    }

    /// <summary>
    /// Convert the color to a black and white color.
    /// </summary>
    /// <param name="color">Base color.</param>
    /// <returns>Black and White version of color.</returns>
    public static Color ColorToBlackAndWhite(Color color)
    {
        // Use the standard percentages of RGB for the human eye bias
        var gray = (int)((color.R * 0.3f) +
                         (color.G * 0.59f) +
                         (color.B * 0.11f));

        return Color.FromArgb(gray, gray, gray);
    }

    /// <summary>
    /// Whiten a provided color by applying per channel percentages.
    /// </summary>
    /// <param name="color1">Color.</param>
    /// <param name="percentR">Percentage of red to keep.</param>
    /// <param name="percentG">Percentage of green to keep.</param>
    /// <param name="percentB">Percentage of blue to keep.</param>
    /// <returns>Modified color.</returns>
    public static Color WhitenColor(Color color1,
        float percentR,
        float percentG,
        float percentB)
    {
        // Find how much to use of each primary color
        var red = (int)(color1.R / percentR);
        var green = (int)(color1.G / percentG);
        var blue = (int)(color1.B / percentB);

        // Limit check against individual component
        if (red < 0)
        {
            red = 0;
        }

        if (red > 255)
        {
            red = 255;
        }

        if (green < 0)
        {
            green = 0;
        }

        if (green > 255)
        {
            green = 255;
        }

        if (blue < 0)
        {
            blue = 0;
        }

        if (blue > 255)
        {
            blue = 255;
        }

        // Return the whitened color
        return Color.FromArgb(color1.A, red, green, blue);
    }

    /// <summary>
    /// Blacken a provided color by applying per channel percentages.
    /// </summary>
    /// <param name="color1">Color.</param>
    /// <param name="percentR">Percentage of red to keep.</param>
    /// <param name="percentG">Percentage of green to keep.</param>
    /// <param name="percentB">Percentage of blue to keep.</param>
    /// <returns>Modified color.</returns>
    public static Color BlackenColor(Color color1,
        float percentR,
        float percentG,
        float percentB)
    {
        // Find how much to use of each primary color
        var red = (int)(color1.R * percentR);
        var green = (int)(color1.G * percentG);
        var blue = (int)(color1.B * percentB);

        // Limit check against individual component
        if (red < 0)
        {
            red = 0;
        }

        if (red > 255)
        {
            red = 255;
        }

        if (green < 0)
        {
            green = 0;
        }

        if (green > 255)
        {
            green = 255;
        }

        if (blue < 0)
        {
            blue = 0;
        }

        if (blue > 255)
        {
            blue = 255;
        }

        // Return the whitened color
        return Color.FromArgb(color1.A, red, green, blue);
    }

    /// <summary>
    /// Merge two colors together using relative percentages.
    /// </summary>
    /// <param name="color1">First color.</param>
    /// <param name="percent1">Percentage of first color to use.</param>
    /// <param name="color2">Second color.</param>
    /// <param name="percent2">Percentage of second color to use.</param>
    /// <returns>Merged color.</returns>
    public static Color MergeColors(Color color1, float percent1,
        Color color2, float percent2) =>
        // Use existing three color merge
        MergeColors(color1, percent1, color2, percent2, GlobalStaticValues.EMPTY_COLOR, 0f);

    /// <summary>
    /// Merge three colors together using relative percentages.
    /// </summary>
    /// <param name="color1">First color.</param>
    /// <param name="percent1">Percentage of first color to use.</param>
    /// <param name="color2">Second color.</param>
    /// <param name="percent2">Percentage of second color to use.</param>
    /// <param name="color3">Third color.</param>
    /// <param name="percent3">Percentage of third color to use.</param>
    /// <returns>Merged color.</returns>
    public static Color MergeColors(Color color1, float percent1,
        Color color2, float percent2,
        Color color3, float percent3)
    {
        // Find how much to use of each primary color
        var red = (int)((color1.R * percent1) + (color2.R * percent2) + (color3.R * percent3));
        var green = (int)((color1.G * percent1) + (color2.G * percent2) + (color3.G * percent3));
        var blue = (int)((color1.B * percent1) + (color2.B * percent2) + (color3.B * percent3));

        // Limit check against individual component
        if (red < 0)
        {
            red = 0;
        }

        if (red > 255)
        {
            red = 255;
        }

        if (green < 0)
        {
            green = 0;
        }

        if (green > 255)
        {
            green = 255;
        }

        if (blue < 0)
        {
            blue = 0;
        }

        if (blue > 255)
        {
            blue = 255;
        }

        // Return the merged color
        return Color.FromArgb(red, green, blue);
    }

    /// <summary>
    /// Get the number of bits used to define the color depth of the display.
    /// </summary>
    /// <returns>Number of bits in color depth.</returns>
    public static int ColorDepth()
    {
        // Get access to the desktop DC
        IntPtr desktopDC = PI.GetDC(IntPtr.Zero);

        // Find raw values that define the color depth
        var planes = PI.GetDeviceCaps(desktopDC, PI.DeviceCap.PLANES);
        var bitsPerPixel = PI.GetDeviceCaps(desktopDC, PI.DeviceCap.BITSPIXEL);

        // Must remember to release it!
        PI.ReleaseDC(IntPtr.Zero, desktopDC);

        return planes * bitsPerPixel;
    }

    /// <summary>
    /// Gets a value indicating if the SHIFT key is pressed.
    /// </summary>
    public static bool IsShiftKeyPressed
    {
        [DebuggerStepThrough]
        get => (PI.GetKeyState(VK_SHIFT) & 0x00008000) != 0;
    }

    /// <summary>
    /// Gets a value indicating if the CTRL key is pressed.
    /// </summary>
    public static bool IsCtrlKeyPressed
    {
        [DebuggerStepThrough]
        get => (PI.GetKeyState(VK_CONTROL) & 0x00008000) != 0;
    }

    /// <summary>
    /// Gets a value indicating if the ALT key is pressed.
    /// </summary>
    public static bool IsAltKeyPressed
    {
        [DebuggerStepThrough]
        get => (PI.GetKeyState(VK_MENU) & 0x00008000) != 0;
    }

    /// <summary>
    /// Search the hierarchy of the provided control looking for one that has the focus.
    /// </summary>
    /// <param name="control">Top of the hierarchy to search.</param>
    /// <returns>Control with focus; otherwise null.</returns>
    public static Control? GetControlWithFocus(Control control)
    {
        // Does the provided control have the focus?
        if (control.Focused
            && control is not IContainedInputControl
           )
        {
            return control;
        }
        else
        {
            // Check each child hierarchy in turn
            return (from Control child in control.Controls
                    where child.ContainsFocus
                    select GetControlWithFocus(child)
                ).FirstOrDefault();
        }
    }

    /// <summary>
    /// Add the provided control to a parent collection.
    /// </summary>
    /// <param name="parent">Parent control.</param>
    /// <param name="c">Control to be added.</param>
    public static void AddControlToParent([DisallowNull] Control parent, [DisallowNull] Control? c)
    {
        Debug.Assert(parent != null);
        Debug.Assert(c != null);

        // If the control is already inside a control collection, then remove it
        if (c?.Parent != null)
        {
            RemoveControlFromParent(c);
        }
        // Then must use the internal method for adding a new instance

        // If the control collection is one of our internal collections...
        if (parent?.Controls is KryptonControlCollection cc)
        {
            cc.AddInternal(c!);
        }
        else
        {
            // Inside a standard collection, add it the usual way
            parent?.Controls?.Add(c!);
        }
    }

    /// <summary>
    /// Remove the provided control from its parent collection.
    /// </summary>
    /// <param name="c">Control to be removed.</param>
    public static void RemoveControlFromParent([DisallowNull] Control? c)
    {
        Debug.Assert(c != null);

        // If the control is inside a parent collection
        if (c?.Parent != null)
        {
            // Then must use the internal method for adding a new instance
            // If the control collection is one of our internal collections...
            if (c.Parent.Controls is KryptonControlCollection cc)
            {
                cc.RemoveInternal(c);
            }
            else
            {
                // Inside a standard collection, remove it the usual way
                c.Parent.Controls.Remove(c);
            }
        }
    }

    /// <summary>
    /// Gets the size of the borders requested by the real window.
    /// </summary>
    /// <param name="cp">Window style parameters.</param>
    /// <param name="form">Optional VisualForm base to detect usage of Chrome drawing</param>
    /// <returns>Border sizing.</returns>
    public static Padding GetWindowBorders(CreateParams cp, KryptonForm? form)
    {
        int xOffset = 0;
        int yOffset = 0;
        uint dwStyle = (uint)cp.Style;
        bool useAdjust = false;
        if (form is { StateCommon.Border: PaletteFormBorder formBorder } kryptonForm)
        {
            useAdjust = true;
            var (xOffset1, yOffset1) = formBorder.BorderWidths(kryptonForm.FormBorderStyle);
            xOffset = Math.Max(0, xOffset1);
            yOffset = Math.Max(0, yOffset1);
        }

        var rect = new PI.RECT
        {
            // Start with a zero sized rectangle
            top = -yOffset,
            bottom = yOffset
        };
        if (useAdjust)
        {
            // Adjust rectangle to add on the borders required
            PI.AdjustWindowRectEx(ref rect, dwStyle, false, cp.ExStyle);
            PaletteBase? resolvedPalette = form?.GetResolvedPalette();
            if (resolvedPalette == null)
            {
                // Need to breakout when the form is closing
                return new Padding(-rect.left, -rect.top, rect.right, rect.bottom);
            }

            if (!CommonHelper.IsFormMaximized(form!))
            {
                // Set the values determined by the formBorder.BorderWidths etc.
                rect.left = -xOffset;
                rect.right = xOffset;
                rect.bottom = yOffset;

                switch (form!.StateCommon!.Border.GetBorderDrawBorders(PaletteState.Normal))
                {
                    case PaletteDrawBorders.None:
                        rect.left = 0;
                        rect.right = 0;
                        rect.bottom = 0;
                        break;
                    case PaletteDrawBorders.Bottom:
                    case PaletteDrawBorders.TopBottom:
                        rect.left = 0;
                        rect.right = 0;
                        break;
                    case PaletteDrawBorders.Left:
                    case PaletteDrawBorders.TopLeft:
                        rect.right = 0;
                        rect.bottom = 0;
                        break;
                    case PaletteDrawBorders.BottomLeft:
                    case PaletteDrawBorders.TopBottomLeft:
                        rect.right = 0;
                        break;
                    case PaletteDrawBorders.Right:
                    case PaletteDrawBorders.TopRight:
                        rect.left = 0;
                        rect.bottom = 0;
                        break;
                    case PaletteDrawBorders.BottomRight:
                    case PaletteDrawBorders.TopBottomRight:
                        rect.left = 0;
                        break;
                    case PaletteDrawBorders.LeftRight:
                    case PaletteDrawBorders.TopLeftRight:
                        rect.bottom = 0;
                        break;
                    //case PaletteDrawBorders.Inherit:
                    //case PaletteDrawBorders.BottomLeftRight:
                    //case PaletteDrawBorders.All:
                    default:
                        break;
                }
            }
            else if (form?.IsMdiChild ?? false)
            {
                rect.top = 0;
                rect.bottom = 0;
            }
            else
            {
                rect.bottom -= yOffset;
                rect.top -= rect.bottom;
            }
        }

        // Return the per side border values
        return new Padding(-rect.left, -rect.top, rect.right, rect.bottom);
    }

    /// <summary>
    /// Discover if the provided Form is currently minimized.
    /// </summary>
    /// <param name="f">Form reference.</param>
    /// <returns>True if minimized; otherwise false.</returns>
    public static bool IsFormMinimized(Form f)
    {
        // Get the current window style (cannot use the 
        // WindowState property as it can be slightly out of date)
        uint style = f.IsDisposed ? 0 : PI.GetWindowLong(f.Handle, PI.GWL_.STYLE);

        return (style & PI.WS_.MINIMIZE) != 0;
    }

    /// <summary>
    /// Discover if the provided Form is currently maximized.
    /// </summary>
    /// <param name="f">Form reference.</param>
    /// <returns>True if maximized; otherwise false.</returns>
    public static bool IsFormMaximized(Form f)
    {
        // Get the current window style (cannot use the 
        // WindowState property as it can be slightly out of date)
        uint style = f.IsDisposed ? 0 : PI.GetWindowLong(f.Handle, PI.GWL_.STYLE);

        return (style & PI.WS_.MAXIMIZE) != 0;
    }

    /// <summary>
    /// Gets the real client rectangle of the list.
    /// </summary>
    /// <param name="handle">Window handle of the control.</param>
    public static Rectangle RealClientRectangle(IntPtr handle)
    {
        // Grab the actual current size of the window, this is more accurate than using
        // the 'this.Size' which is out of date when performing a resize of the window.
        var windowRect = new PI.RECT();
        PI.GetWindowRect(handle, ref windowRect);

        // Create rectangle that encloses the entire window
        return new Rectangle(0, 0,
            windowRect.right - windowRect.left,
            windowRect.bottom - windowRect.top);
    }

    /// <summary>
    /// Find the appropriate content style to match the incoming label style.
    /// </summary>
    /// <param name="style">LabelStyle enumeration.</param>
    /// <returns>Matching PaletteContentStyle enumeration value.</returns>
    public static PaletteContentStyle ContentStyleFromLabelStyle(LabelStyle style)
    {
        switch (style)
        {
            case LabelStyle.AlternateControl:
                return PaletteContentStyle.LabelAlternateControl;
            case LabelStyle.NormalControl:
                return PaletteContentStyle.LabelNormalControl;
            case LabelStyle.BoldControl:
                return PaletteContentStyle.LabelBoldControl;
            case LabelStyle.ItalicControl:
                return PaletteContentStyle.LabelItalicControl;
            case LabelStyle.TitleControl:
                return PaletteContentStyle.LabelTitleControl;
            case LabelStyle.AlternatePanel:
                return PaletteContentStyle.LabelAlternatePanel;
            case LabelStyle.NormalPanel:
                return PaletteContentStyle.LabelNormalPanel;
            case LabelStyle.BoldPanel:
                return PaletteContentStyle.LabelBoldPanel;
            case LabelStyle.ItalicPanel:
                return PaletteContentStyle.LabelItalicPanel;
            case LabelStyle.TitlePanel:
                return PaletteContentStyle.LabelTitlePanel;
            case LabelStyle.GroupBoxCaption:
                return PaletteContentStyle.LabelGroupBoxCaption;
            case LabelStyle.ToolTip:
                return PaletteContentStyle.LabelToolTip;
            case LabelStyle.SuperTip:
                return PaletteContentStyle.LabelSuperTip;
            case LabelStyle.KeyTip:
                return PaletteContentStyle.LabelKeyTip;
            case LabelStyle.Custom1:
                return PaletteContentStyle.LabelCustom1;
            case LabelStyle.Custom2:
                return PaletteContentStyle.LabelCustom2;
            case LabelStyle.Custom3:
                return PaletteContentStyle.LabelCustom3;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(style.ToString());
                return PaletteContentStyle.LabelNormalPanel;
        }
    }

    /// <summary>
    /// Convert from palette rendering hint to actual rendering hint.
    /// </summary>
    /// <param name="hint">Palette rendering hint.</param>
    /// <returns>Converted value for use with a Graphics instance.</returns>
    public static TextRenderingHint PaletteTextHintToRenderingHint(PaletteTextHint hint)
    {
        switch (hint)
        {
            case PaletteTextHint.AntiAlias:
                return TextRenderingHint.AntiAlias;
            case PaletteTextHint.AntiAliasGridFit:
                return TextRenderingHint.AntiAliasGridFit;
            case PaletteTextHint.ClearTypeGridFit:
                return TextRenderingHint.ClearTypeGridFit;
            case PaletteTextHint.SingleBitPerPixel:
                return TextRenderingHint.SingleBitPerPixel;
            case PaletteTextHint.SingleBitPerPixelGridFit:
                return TextRenderingHint.SingleBitPerPixelGridFit;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(hint.ToString());
                return TextRenderingHint.SystemDefault;
        }
    }

    /// <summary>
    /// Get the correct metric padding for the provided separator style.
    /// </summary>
    /// <param name="separatorStyle">Separator style.</param>
    /// <returns>Matching metric padding.</returns>
    public static PaletteMetricPadding SeparatorStyleToMetricPadding(SeparatorStyle separatorStyle)
    {
        switch (separatorStyle)
        {
            case SeparatorStyle.LowProfile:
                return PaletteMetricPadding.SeparatorPaddingLowProfile;
            case SeparatorStyle.HighProfile:
                return PaletteMetricPadding.SeparatorPaddingHighProfile;
            case SeparatorStyle.HighInternalProfile:
                return PaletteMetricPadding.SeparatorPaddingHighInternalProfile;
            case SeparatorStyle.Custom1:
                return PaletteMetricPadding.SeparatorPaddingCustom1;
            case SeparatorStyle.Custom2:
                return PaletteMetricPadding.SeparatorPaddingCustom2;
            case SeparatorStyle.Custom3:
                return PaletteMetricPadding.SeparatorPaddingCustom3;
            default:
                // Should never happen!
                Debug.Assert(false);
                DebugTools.NotImplemented(separatorStyle.ToString());
                return PaletteMetricPadding.SeparatorPaddingLowProfile;
        }
    }

    /// <summary>
    /// Ensure that a single character format string is treated as a custom format.
    /// </summary>
    /// <param name="format">Incoming format.</param>
    /// <returns>Corrected format.</returns>
    public static string MakeCustomDateFormat(string? format)
    {
        // Is this a single character format?
        if (format!.Length == 1)
        {
            // If the character is one of the predefined entries...
            if (format.IndexOfAny(_singleDateFormat) == 0)
            {
                // Insert the percentage sign so it is a custom format and not a predefined one
                format = $"%{format}";
            }
        }

        return format;
    }

    /// <summary>
    /// Create new instance of specified type within the designer host, if provided.
    /// </summary>
    /// <param name="itemType">Type of the item to create.</param>
    /// <param name="host">Designer host used if provided.</param>
    /// <returns>Reference to new instance.</returns>
    public static object CreateInstance(Type itemType, IDesignerHost? host)
    {
        object? retObj;

        // Cannot use the designer host to create component unless the type implements IComponent
        if (typeof(IComponent).IsAssignableFrom(itemType) && (host != null))
        {
            // Ask host to create component for us
            retObj = host.CreateComponent(itemType, null!);

            // If the new object has an associated designer then use that now to initialize the instance
            if (host.GetDesigner((IComponent)retObj) is IComponentInitializer designer)
            {
                designer.InitializeNewComponent(null);
            }
        }
        else
        {
            // Cannot use host for creation, so do it the standard way instead
            retObj = TypeDescriptor.CreateInstance(host, itemType, null!, null!);
        }

        return retObj ?? false;
    }

    /// <summary>
    /// Destroy instance of an object using the provided designer host.
    /// </summary>
    /// <param name="instance">Reference to item for destroying.</param>
    /// <param name="host">Designer host used if provided.</param>
    public static void DestroyInstance(object instance, IDesignerHost? host)
    {
        switch (instance)
        {
            // Use designer to remove component if possible
            case IComponent component when host != null:
                host.DestroyComponent(component);
                break;
            case IComponent component:
                component.Dispose();
                break;
            // Fallback to calling any exposed dispose method
            case IDisposable disposable:
                disposable.Dispose();
                break;
        }
    }

    /// <summary>
    /// Output some debug data to a log file that exists in same directory as the application.
    /// </summary>
    /// <param name="str">String to output.</param>
    public static void LogOutput(string str)
    {
        // TODO: Make this thread aware !
        // TODO: DO NOT WRITE to the application path, as that might / will be UAC protected !!
        //var fi = new FileInfo(Application.ExecutablePath);
        //using var writer = new StreamWriter($@"{fi.DirectoryName}LogOutput.txt", true, Encoding.ASCII);
        //writer.Write($@"{DateTime.Now.ToLongTimeString()} :  ");
        //writer.WriteLine(str);
        //writer.Flush();
        Debug.WriteLine(str);
    }

    /// <summary>
    /// Discover if the component is in design mode.
    /// </summary>
    /// <param name="c">Component to test.</param>
    /// <returns>True if in design mode; otherwise false.</returns>
    public static bool DesignMode(Component? c)
    {
        // Cache the info needed to sneak access to the component protected property
        if (_cachedDesignModePI == null)
        {
            _cachedDesignModePI = typeof(ToolStrip).GetProperty(nameof(DesignMode),
                BindingFlags.Instance |
                BindingFlags.GetProperty |
                BindingFlags.NonPublic);
        }

        return (bool)_cachedDesignModePI!.GetValue(c, null)!;
    }

    /// <summary>
    /// Convert a double to a culture invariant string value.
    /// </summary>
    /// <param name="d">Double to convert.</param>
    /// <returns>Culture invariant string representation.</returns>
    public static string? DoubleToString(double d) => _dc.ConvertToInvariantString(d);

    /// <summary>
    /// Convert a culture invariant string value to a double.
    /// </summary>
    /// <param name="s">String to convert.</param>
    /// <returns>Double value.</returns>
    public static double StringToDouble(string s) => (double)_dc.ConvertFromInvariantString(s)!;

    /// <summary>
    /// Convert a Size to a culture invariant string value.
    /// </summary>
    /// <param name="s">Size to convert.</param>
    /// <returns>Culture invariant string representation. String.Empty if the conversion failed.</returns>
    public static string SizeToString(Size s) => _sc.ConvertToInvariantString(s) is string str
        ? str
        : string.Empty;

    /// <summary>
    /// Convert a culture invariant string value to a Size.
    /// </summary>
    /// <param name="s">String to convert.</param>
    /// <returns>Size value. If s is null, Size(0, 0) is returned.</returns>
    public static Size StringToSize(string? s) => s is not null && _sc.ConvertFromInvariantString(s) is Size size
        ? size
        : new Size(0, 0);

    /// <summary>
    /// Convert a Point to a culture invariant string value.
    /// </summary>
    /// <param name="s">Size to convert.</param>
    /// <returns>Culture invariant string representation.</returns>
    public static string PointToString(Point s) => _pc.ConvertToInvariantString(s) ?? string.Empty;

    /// <summary>
    /// Convert a culture invariant string value to a Point.
    /// </summary>
    /// <param name="s">String to convert.</param>
    /// <returns>Point value if s was not null. If s is null a new Point(0) will be returned.</returns>
    public static Point StringToPoint(string? s) => s is not null
        ? (Point)_pc.ConvertFromInvariantString(s)!
        : new Point(0);

    /// <summary>
    /// Convert a Boolean to a culture invariant string value.
    /// </summary>
    /// <param name="b">Boolean to convert.</param>
    /// <returns>Culture invariant string representation.</returns>
    public static string? BoolToString(bool b) => _bc.ConvertToInvariantString(b);

    /// <summary>
    /// Convert a culture invariant string value to a Boolean.
    /// </summary>
    /// <param name="s">String to convert.</param>
    /// <returns>Boolean value.</returns>
    public static bool StringToBool(string s) => (bool)_bc.ConvertFromInvariantString(s)!;

    /// <summary>
    /// Convert a Color to a culture invariant string value.
    /// </summary>
    /// <param name="c">Color to convert.</param>
    /// <returns>Culture invariant string representation.</returns>
    public static string? ColorToString(Color c) => _cc.ConvertToInvariantString(c);

    /// <summary>
    /// Convert a culture invariant string value to a Color.
    /// </summary>
    /// <param name="s">String to convert.</param>
    /// <returns>Color value.</returns>
    public static Color StringToColor(string s) => (Color)_cc.ConvertFromInvariantString(s)!;

    /// <summary>
    /// Convert a client mouse position inside a windows message into a screen position.
    /// </summary>
    /// <param name="m">Window message.</param>
    /// <returns>Screen point.</returns>
    public static Point ClientMouseMessageToScreenPt(Message m)
    {
        // Extract the x and y mouse position from message
        var clientPt = new PI.POINTC
        {
            x = PI.LOWORD((int)m.LParam),
            y = PI.HIWORD((int)m.LParam)
        };

        // Negative positions are in the range 32767 -> 65535, 
        // so convert to actual int values for the negative positions
        if (clientPt.x >= 32767)
        {
            clientPt.x -= 65536;
        }

        if (clientPt.y >= 32767)
        {
            clientPt.y -= 65536;
        }

        // Convert a 0,0 point from client to screen to find offsetting
        var zeroPIPt = new PI.POINTC
        {
            x = 0,
            y = 0
        };
        PI.MapWindowPoints(m.HWnd, IntPtr.Zero, zeroPIPt, 1);

        // Adjust the client coordinate by the offset to get to screen
        clientPt.x += zeroPIPt.x;
        clientPt.y += zeroPIPt.y;

        // Return as a managed point type
        return new Point(clientPt.x, clientPt.y);
    }

    /// <summary>
    /// Gets a reference to the currently active floating window.
    /// </summary>
    public static Form? ActiveFloatingWindow { get; set; }

    /// <summary>
    /// Gets the current active cursor, and if that is null use the current default cursor
    /// </summary>
    /// <returns>Cursor Hotspot</returns>
    public static Point CaptureCursor()
    {
        Cursor cur = Cursor.Current ?? Cursors.Default;

        return cur.HotSpot;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="margins"></param>
    public static void Deflate(this Rectangle rect, Padding margins)
    {
        rect.X += margins.Left;
        rect.Y += margins.Top;
        rect.Width -= margins.Left + margins.Right;
        rect.Height -= margins.Top + margins.Bottom;
    }

    /// <summary>
    /// Do not use the `DpiHandler.ScaleBitmapLogicalToDevice` as that will introduce the "purple artifact" lines
    /// Also, Using the int version of the `DrawImage` produces better upscale for the 125% images
    /// </summary>
    /// <param name="src"></param>
    /// <param name="trgtWidth"></param>
    /// <param name="trgtHeight"></param>
    /// <param name="avoidPurple"></param>
    /// <returns></returns>
    /// <exception >thrown if targets are negative</exception>
    public static Bitmap? ScaleImageForSizedDisplay(Image? src, float trgtWidth, float trgtHeight, bool avoidPurple)
    {
        if (trgtWidth <= 1.0 || trgtHeight <= 1.0)
        {
            // For some reason, in the designer it can send a rect that has a negative size element,
            // therefore the targets will also be negative
            // Also When collapsing / expanding ribbons the `trgtHeight` will > 0 BUT < 1.0
            //return new Bitmap(0, 0);    // This will throw an exception
            return null;
        }

        var newImage = new Bitmap((int)trgtWidth, (int)trgtHeight);
        using Graphics gr = Graphics.FromImage(newImage);
        gr.Clear(Color.Transparent);
        gr.SmoothingMode = SmoothingMode.AntiAlias;
        // Got to be careful with this setting, otherwise "Purple" artifacts will be introduced !
        gr.InterpolationMode = avoidPurple ? InterpolationMode.NearestNeighbor : InterpolationMode.High;
        gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
        //var srcRect = new RectangleF(0.0f, 0.0f, src.Width, src.Height);
        //var destRect = new RectangleF(0.0f, 0.0f, trgtWidth, trgtHeight);
        //// Handle rounding down of the target `newImage` dimensions
        //srcRect.Offset(-trgtWidth%1, -trgtHeight%1);
        //gr.DrawImage(src, destRect, srcRect, GraphicsUnit.Pixel);
        gr.DrawImage(src!, 0, 0, (int)trgtWidth, (int)trgtHeight);

        return newImage;
    }
}