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

public interface IGlobalId
{
    #region Id
    /// <summary>
    /// Gets the unique identifier of the object.
    /// </summary>
    int Id { get; }
    #endregion
}

/// <summary>
/// Contains a global identifier that is unique among objects.
/// </summary>
public class GlobalId : ExpandableObjectConverter, IGlobalId
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the GlobalId class.
    /// </summary>
    [DebuggerStepThrough]
    public GlobalId() =>
        // Assign the next global identifier
        Id = CommonHelper.NextId;

    #endregion

    #region Id
    /// <summary>
    /// Gets the unique identifier of the object.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int Id { get; }

    #endregion
}