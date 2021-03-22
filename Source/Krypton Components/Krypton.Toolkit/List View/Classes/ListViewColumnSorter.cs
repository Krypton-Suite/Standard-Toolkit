using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

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