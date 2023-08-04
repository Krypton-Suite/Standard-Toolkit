#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    internal class PlacementModeConverter : StringLookupConverter<PlacementMode>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly IReadOnlyDictionary<PlacementMode, string> _pairs = new Dictionary<PlacementMode, string>
        {
            {PlacementMode.Absolute, KryptonLanguageManager.PlacementModeStrings.Absolute},
            {PlacementMode.AbsolutePoint, KryptonLanguageManager.PlacementModeStrings.AbsolutePoint},
            {PlacementMode.Bottom, KryptonLanguageManager.PlacementModeStrings.Bottom},
            {PlacementMode.Center, KryptonLanguageManager.PlacementModeStrings.Center},
            {PlacementMode.Left, KryptonLanguageManager.PlacementModeStrings.Left},
            {PlacementMode.Mouse, KryptonLanguageManager.PlacementModeStrings.Mouse},
            {PlacementMode.MousePoint, KryptonLanguageManager.PlacementModeStrings.MousePoint},
            {PlacementMode.Relative, KryptonLanguageManager.PlacementModeStrings.Relative},
            {PlacementMode.RelativePoint, KryptonLanguageManager.PlacementModeStrings.RelativePoint},
            {PlacementMode.Right, KryptonLanguageManager.PlacementModeStrings.Right},
            {PlacementMode.Top, KryptonLanguageManager.PlacementModeStrings.Top }
        };

        #endregion

        #region Protected

        protected override IReadOnlyDictionary<PlacementMode /*Enum*/, string /*Display*/> Pairs => _pairs;

        #endregion
    }
}
