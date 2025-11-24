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
/// Action item that presents as a method call but calls a property.
/// </summary>
public class KryptonDesignerActionItem : DesignerActionMethodItem
{
    #region Instance Fields
    private readonly DesignerVerb _verb;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KrpytonDesignerActionVerbItem class.
    /// </summary>
    /// <param name="verb">Verb instance to wrap.</param>
    /// <param name="category">Name of the category the action belongs to.</param>
    public KryptonDesignerActionItem([DisallowNull] DesignerVerb verb, [DisallowNull] string category)
        : base(null, null, null)
    {
        Debug.Assert(verb != null);
        Debug.Assert(category != null);

        // Remember details
        _verb = verb ?? throw new ArgumentNullException(nameof(verb));
        Category = category ?? throw new ArgumentNullException(nameof(category));
    }
    #endregion

    #region Public Overrides
    /// <summary>
    ///  Programmatically executes the method associated with the item.
    /// </summary>
    public override void Invoke() => _verb.Invoke();

    /// <summary>
    /// Gets the group name for an item.
    /// </summary>
    public override string Category { get; }

    /// <summary>
    /// Gets the supplemental text for the item.
    /// </summary>
    public override string Description => _verb.Description;

    /// <summary>
    /// Gets the text for this item.
    /// </summary>
    public override string DisplayName => _verb.Text;

    /// <summary>
    /// Gets a value that indicates the item should appear in other user interface contexts.
    /// </summary>
    public override bool IncludeAsDesignerVerb => false;

    /// <summary>
    /// Gets the name of the method that this item is associated with.
    /// </summary>
    public override string? MemberName => null;

    #endregion
}