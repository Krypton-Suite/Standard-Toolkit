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

internal class PlacementModeConverter : StringLookupConverter<PlacementMode>
{
    #region Static Fields

    [Localizable(true)]
    private static readonly BiDictionary<PlacementMode, string> _pairs = new BiDictionary<PlacementMode, string>(
        new Dictionary<PlacementMode, string>
        {
            {PlacementMode.Absolute, DesignTimeUtilities.DEFAULT_PLACEMENT_MODE_ABSOLUTE},
            {PlacementMode.AbsolutePoint, DesignTimeUtilities.DEFAULT_PLACEMENT_MODE_ABSOLUTE_POINT},
            {PlacementMode.Bottom, DesignTimeUtilities.DEFAULT_PLACEMENT_MODE_BOTTOM},
            {PlacementMode.Center, DesignTimeUtilities.DEFAULT_PLACEMENT_MODE_CENTER},
            {PlacementMode.Left, DesignTimeUtilities.DEFAULT_PLACEMENT_MODE_LEFT},
            {PlacementMode.Mouse, DesignTimeUtilities.DEFAULT_PLACEMENT_MODE_MOUSE},
            {PlacementMode.MousePoint, DesignTimeUtilities.DEFAULT_PLACEMENT_MODE_MOUSE_POINT},
            {PlacementMode.Relative, DesignTimeUtilities.DEFAULT_PLACEMENT_MODE_RELATIVE},
            {PlacementMode.RelativePoint, DesignTimeUtilities.DEFAULT_PLACEMENT_MODE_RELATIVE_POINT},
            {PlacementMode.Right, DesignTimeUtilities.DEFAULT_PLACEMENT_MODE_RIGHT},
            {PlacementMode.Top, DesignTimeUtilities.DEFAULT_PLACEMENT_MODE_TOP}
        });

    #endregion

    #region Protected

    protected override IReadOnlyDictionary<PlacementMode /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
    protected override IReadOnlyDictionary<string /*Display*/, PlacementMode /*Enum*/ > PairsStringToEnum => _pairs.SecondToFirst;

    #endregion
}