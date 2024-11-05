﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2024. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Navigator
{
    /// <summary>
    /// Custom type converter so that MapKryptonPageText values appear as neat text at design time.
    /// </summary>
    public class MapKryptonPageTextConverter : StringLookupConverter<MapKryptonPageText>
    {
        #region Static Fields

        [Localizable(true)]
        private static readonly BiDictionary<MapKryptonPageText, string> _pairs = new BiDictionary<MapKryptonPageText, string>(
            new Dictionary<MapKryptonPageText, string>
        {
            {MapKryptonPageText.None, @"None (Empty string)"},
            {MapKryptonPageText.Text, @"Text"},
            {MapKryptonPageText.TextTitle, @"Text - Title"},
            {MapKryptonPageText.TextTitleDescription, @"Text - Title - Description"},
            {MapKryptonPageText.TextDescription, @"Text - Description"},
            {MapKryptonPageText.Title, @"Title"},
            {MapKryptonPageText.TitleText, @"Title - Text"},
            {MapKryptonPageText.TitleDescription, @"Title - Description"},
            {MapKryptonPageText.Description, @"Description"},
            {MapKryptonPageText.DescriptionText, @"Description - Text"},
            {MapKryptonPageText.DescriptionTitle, @"Description - Title"},
            {MapKryptonPageText.DescriptionTitleText, @"Description - Title - Text"},
            {MapKryptonPageText.ToolTipTitle, @"ToolTipTitle"},
            {MapKryptonPageText.ToolTipBody, @"ToolTipBody"}
        });

        #endregion

        #region Protected
        /// <summary>
        /// Gets an array of lookup pairs.
        /// </summary>
        protected override IReadOnlyDictionary<string /*Display*/, MapKryptonPageText /*Enum*/ > PairsStringToEnum  => _pairs.SecondToFirst;
        protected override IReadOnlyDictionary<MapKryptonPageText /*Enum*/, string /*Display*/> PairsEnumToString => _pairs.FirstToSecond;
        #endregion
    }
}
