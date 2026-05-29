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

internal class KryptonSplitContainerGlyph : Glyph
{
    #region Instance Fields
    private readonly KryptonSplitContainer? _splitContainer;
    private readonly ISelectionService? _selectionService;
    private readonly BehaviorService _behaviorService; 
    private readonly Adorner _adorner;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonSplitContainerGlyph class.
    /// </summary>
    /// <param name="selectionService">Reference to the selection service.</param>
    /// <param name="behaviorService">Reference to the behavior service.</param>
    /// <param name="adorner">Reference to the containing adorner.</param>
    /// <param name="relatedDesigner">Reference to the containing designer.</param>
    public KryptonSplitContainerGlyph([DisallowNull] ISelectionService? selectionService,
        [DisallowNull] BehaviorService behaviorService,
        [DisallowNull] Adorner adorner,
        [DisallowNull] IDesigner relatedDesigner)
        : base(new KryptonSplitContainerBehavior(relatedDesigner))
    {
        Debug.Assert(selectionService is not null);
        Debug.Assert(behaviorService is not null);
        Debug.Assert(adorner is not null);
        Debug.Assert(relatedDesigner is not null);

        // Remember incoming references
        _selectionService = selectionService ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(selectionService)));
        _behaviorService = behaviorService ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(behaviorService)));
        _adorner = adorner ?? throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(adorner)));

        // Find the related control
        if ( relatedDesigner is null)
        {
            throw new NullReferenceException(GlobalStaticValues.VariableCannotBeNull(nameof(relatedDesigner)));
        }

        _splitContainer = relatedDesigner.Component as KryptonSplitContainer;

        // We want to know whenever the selection has changed or a property has changed
        _selectionService.SelectionChanged += OnSelectionChanged;
    }
    #endregion

    #region Public Overrides
    /// <summary>
    ///  Provides hit test logic.
    /// </summary>
    /// <param name="pt">A point to hit-test.</param>
    /// <returns> A Cursor if the Glyph is associated with p; otherwise, a null reference.</returns>
    public override Cursor? GetHitTest(Point pt)
    {
        if (_splitContainer != null)
        {
            Rectangle bounds = Bounds;

            // Is the point inside the bounds of the split container?
            if (bounds.Contains(pt))
            {
                // Convert from adorner coordinates to the control client coordinates
                var splitPt = new Point(pt.X - bounds.X, pt.Y - bounds.Y);

                // Ask the split container for the correct cursor to use
                return _splitContainer.DesignGetHitTest(splitPt);
            }
        }

        return null;
    }

    /// <summary>
    ///  Provides paint logic.
    /// </summary>
    /// <param name="e">A PaintEventArgs that contains the event data.</param>
    public override void Paint(PaintEventArgs e)
    {
        // We never need to paint over the control itself
    }

    /// <summary>
    ///  Gets the bounds of the Glyph.
    /// </summary>
    public override Rectangle Bounds
    {
        get
        {
            if (_splitContainer != null)
            {
                // Find the location of the control inside the adnorner window
                Point location = _behaviorService.ControlToAdornerWindow(_splitContainer);

                // The bounds of the glyph match the control exactly, the returned rectangle
                // is specified in adorner window coordinates, hence the need to use the
                // behavior service to find the control location in adorner coordinates.
                return new Rectangle(location, _splitContainer.Size);

            }
            else
            {
                return Rectangle.Empty;
            }
        }
    }
    #endregion

    #region Implementation
    private void OnSelectionChanged(object? sender, EventArgs e)
    {
        if (_splitContainer is not null && _selectionService is not null)
        {
            // Make sure there is no splitter movement occuring
            _splitContainer.DesignAbortMoving();

            // Is this the primary selection now?
            _adorner.Enabled = ReferenceEquals(_selectionService.PrimarySelection, _splitContainer);
        }
    }
    #endregion
}