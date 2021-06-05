#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *
 */
#endregion


namespace Krypton.Toolkit
{
    internal class ListViewColumnSorter : IComparer
    {
        #region Variables
        private int _columnToSort;

        private SortOrder _sortOrder;

        private CaseInsensitiveComparer _comparer;
        #endregion

        #region Constructor
        public ListViewColumnSorter()
        {
            _columnToSort = 0;

            _sortOrder = SortOrder.None;

            _comparer = new CaseInsensitiveComparer();
        }
        #endregion

        #region Properties
        [DefaultValue(0)]
        public int SortColumn { get => _columnToSort; set => _columnToSort = value; }

        public SortOrder Order { get => _sortOrder; set => _sortOrder = value; }
        #endregion

        #region IComparer Initialisation
        public int Compare(object x, object y)
        {
            int compareResult;

            ListViewItem listViewX, listViewY;

            listViewX = (ListViewItem)x;

            listViewY = (ListViewItem)y;

            string itemX, itemY;

            try
            {
                itemX = listViewX.SubItems[_columnToSort].Text;
            }
            catch (Exception e)
            {
                ExceptionHandler.CaptureException(e);

                itemX = " ";
            }

            try
            {
                itemY = listViewY.SubItems[_columnToSort].Text;
            }
            catch (Exception e)
            {
                ExceptionHandler.CaptureException(e);

                itemY = " ";
            }

            compareResult = _comparer.Compare(itemX, itemY);

            if (_sortOrder == SortOrder.Ascending)
            {
                return compareResult;
            }
            else if (_sortOrder == SortOrder.Descending)
            {
                return (-compareResult);
            }
            else
            {
                return 0;
            }
        }
        #endregion
    }
}