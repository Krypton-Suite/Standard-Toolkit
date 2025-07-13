#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public sealed class PaletteMicrosoft365Black_TrackBarScheme : KryptonColorTrackBarSchemeBase
{
    public override Color TickMarks         { get; set; } = Color.FromArgb(17, 17, 17);
    public override Color TopTrack          { get; set; } = Color.FromArgb(37, 37, 37);
    public override Color BottomTrack       { get; set; } = Color.FromArgb(174, 174, 174);
    public override Color FillTrack         { get; set; } = Color.FromArgb(131, 132, 132);
    public override Color OutsidePosition   { get; set; } = Color.FromArgb(64, Color.White);
    public override Color BorderPosition    { get; set; } = Color.FromArgb(35, 35, 35);
}
