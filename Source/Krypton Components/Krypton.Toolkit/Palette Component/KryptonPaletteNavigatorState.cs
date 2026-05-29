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
/// Storage for an individual navigator states.
/// </summary>
public class KryptonPaletteNavigatorState : Storage
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteNavigatorState class.
    /// </summary>
    /// <param name="redirect">inheritance redirection instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public KryptonPaletteNavigatorState([DisallowNull] PaletteRedirect redirect,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(redirect != null);
            
        // Create the storage objects
        Bar = new KryptonPaletteNavigatorStateBar(redirect!, needPaint);
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => Bar.IsDefault;

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    public void PopulateFromBase() => Bar.PopulateFromBase();

    #endregion

    #region Bar
    /// <summary>
    /// Gets access to the navigator bar appearance entries.
    /// </summary>
    [KryptonPersist]
    [Category(@"Visuals")]
    [Description(@"Overrides for defining navigator bar appearance entries.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonPaletteNavigatorStateBar Bar { get; }

    private bool ShouldSerializeBar() => !Bar.IsDefault;

    #endregion
}