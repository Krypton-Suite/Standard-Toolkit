#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public abstract class KryptonColorTrackBarSchemeBase
{
    #region Variables

    public abstract Color TickMarks       { get; set; }
    public abstract Color TopTrack        { get; set; }
    public abstract Color BottomTrack     { get; set; }
    public abstract Color FillTrack       { get; set; }
    public abstract Color OutsidePosition { get; set; }
    public abstract Color BorderPosition  { get; set; }

    #endregion
}