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
    /// <summary>Exposes the set of <see cref="KryptonScrollBar"/> strings used within Krypton and that are localizable.</summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class KryptonScrollBarStrings : GlobalId
    {
        #region Static Strings

        private const string DEFAULT_SCROLL_BAR_PAGE_DOWN = @"Page Down";
        private const string DEFAULT_SCROLL_BAR_PAGE_UP = @"Page Up";
        private const string DEFAULT_SCROLL_BAR_SCROLL_DOWN = @"Scoll Down";
        private const string DEFAULT_SCROLL_BAR_SCROLL_HERE = @"Scroll Here";
        private const string DEFAULT_SCROLL_BAR_SCROLL_UP = @"Scroll Up";

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonScrollBarStrings" /> class.</summary>
        public KryptonScrollBarStrings()
        {
            Reset();
        }

        public override string ToString() => !IsDefault ? "Modified" : string.Empty;

        #endregion

        #region Public

        [Browsable(false)]
        public bool IsDefault => PageDown.Equals(DEFAULT_SCROLL_BAR_PAGE_DOWN) &&
                                 PageUp.Equals(DEFAULT_SCROLL_BAR_PAGE_UP) &&
                                 ScrollDown.Equals(DEFAULT_SCROLL_BAR_SCROLL_DOWN) &&
                                 ScrollHere.Equals(DEFAULT_SCROLL_BAR_SCROLL_HERE) &&
                                 ScrollUp.Equals(DEFAULT_SCROLL_BAR_SCROLL_UP);

        public void Reset()
        {
            PageDown = DEFAULT_SCROLL_BAR_PAGE_DOWN;

            PageUp = DEFAULT_SCROLL_BAR_PAGE_UP;

            ScrollDown = DEFAULT_SCROLL_BAR_SCROLL_DOWN;

            ScrollHere = DEFAULT_SCROLL_BAR_SCROLL_HERE;

            ScrollUp = DEFAULT_SCROLL_BAR_SCROLL_UP;
        }

        public string PageDown { get; set; }

        public string PageUp { get; set; }

        public string ScrollDown { get; set; }

        public string ScrollHere { get; set; }

        public string ScrollUp { get; set; }

        #endregion
    }
}