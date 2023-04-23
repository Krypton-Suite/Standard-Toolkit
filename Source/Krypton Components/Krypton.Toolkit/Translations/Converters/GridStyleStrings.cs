#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class GridStyleStrings : GlobalId
    {
        #region Static Fields

        private const string DEFAULT_GRID_STYLE_CUSTOM_ONE = @"Custom 1";
        private const string DEFAULT_GRID_STYLE_CUSTOM_TWO = @"Custom 2";
        private const string DEFAULT_GRID_STYLE_CUSTOM_THREE = @"Custom 3";
        private const string DEFAULT_GRID_STYLE_LIST = @"List";
        private const string DEFAULT_GRID_STYLE_SHEET = @"Sheet";

        #endregion

        #region Identity

        public GridStyleStrings()
        {
            Reset();
        }

        public override string ToString() => !IsDefault ? "Modified" : string.Empty;

        #endregion

        #region Public

        [Browsable(false)]
        public bool IsDefault => CustomOne.Equals(DEFAULT_GRID_STYLE_CUSTOM_ONE) &&
                                 CustomTwo.Equals(DEFAULT_GRID_STYLE_CUSTOM_TWO) &&
                                 CustomThree.Equals(DEFAULT_GRID_STYLE_CUSTOM_THREE) &&
                                 List.Equals(DEFAULT_GRID_STYLE_LIST) &&
                                 Sheet.Equals(DEFAULT_GRID_STYLE_SHEET);

        public void Reset()
        {
            CustomOne = DEFAULT_GRID_STYLE_CUSTOM_ONE;

            CustomTwo = DEFAULT_GRID_STYLE_CUSTOM_TWO;

            CustomThree = DEFAULT_GRID_STYLE_CUSTOM_THREE;

            List = DEFAULT_GRID_STYLE_LIST;

            Sheet = DEFAULT_GRID_STYLE_SHEET;
        }

        public string CustomOne { get; set; }

        public string CustomTwo { get; set; }

        public string CustomThree { get; set; }

        public string List { get; set; }

        public string Sheet { get; set; }

        #endregion'
    }
}