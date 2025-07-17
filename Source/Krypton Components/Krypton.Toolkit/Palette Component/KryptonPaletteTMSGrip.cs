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
/// Storage for grip entries of the professional color table.
/// </summary>
public class KryptonPaletteTMSGrip : KryptonPaletteTMSBase
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteKCTGrip class.
    /// </summary>
    /// <param name="internalKCT">Reference to inherited values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    internal KryptonPaletteTMSGrip(KryptonInternalKCT internalKCT,
        NeedPaintHandler needPaint)
        : base(internalKCT, needPaint)
    {
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (InternalKCT.InternalGripDark == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalGripLight == GlobalStaticValues.EMPTY_COLOR);

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    public void PopulateFromBase()
    {
        GripDark = InternalKCT.GripDark;
        GripLight = InternalKCT.GripLight;
    }
    #endregion

    #region GripDark
    /// <summary>
    /// Gets and sets the color to use for shadow effects on the grip (move handle).
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Color to use for shadow effects on the grip (move handle).")]
    [KryptonDefaultColor]
    public Color GripDark
    {
        get => InternalKCT.InternalGripDark;

        set 
        { 
            InternalKCT.InternalGripDark = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the GripDark property to its default value.
    /// </summary>
    public void ResetGripDark() => GripDark = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region GripLight
    /// <summary>
    /// Gets and sets the color to use for highlight effects on the grip (move handle).
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Color to use for highlight effects on the grip (move handle).")]
    [KryptonDefaultColor]
    public Color GripLight
    {
        get => InternalKCT.InternalGripLight;

        set 
        { 
            InternalKCT.InternalGripLight = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the GripLight property to its default value.
    /// </summary>
    public void ResetGripLight() => GripLight = GlobalStaticValues.EMPTY_COLOR;
    #endregion
}