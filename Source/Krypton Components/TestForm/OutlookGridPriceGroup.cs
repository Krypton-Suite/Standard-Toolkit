#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved. 
 *  
 */
#endregion

using System.Globalization;

namespace TestForm
{
    public class OutlookGridPriceGroup : OutlookGridDefaultGroup
    {

        private int _priceCode;
        private string _currency;
        private object val;

        private const int noPrice = 999999;
        public OutlookGridPriceGroup() : base()
        {
            AllowHiddenWhenGrouped = false;
            _currency = CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parentGroup">The parentGroup if any.</param>
        public OutlookGridPriceGroup(IOutlookGridGroup? parentGroup) : base(parentGroup)
        {
            AllowHiddenWhenGrouped = false;
            _currency = CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol;
        }

        /// <summary>
        /// Gets or sets the displayed text.
        /// </summary>
        public override string Text
        {
            get { return
                $"{Column.DataGridViewColumn.HeaderText}: {GetPriceString(_priceCode)} ({(ItemCount == 1 ? OneItemText : ItemCount.ToString() + XxxItemsText)})"; }
        }

        private int GetPriceCode(decimal price)
        {
            if ((price == 0))
            {
                return 0;
            }
            else if ((price > 0) && (price <= 100))
            {
                return 100;
            }
            else if ((price > 100) && (price <= 200))
            {
                return 200;
            }
            else if ((price > 200) && (price <= 300))
            {
                return 300;
            }
            else if ((price > 300) && (price <= 600))
            {
                return 600;
            }
            else if ((price > 600) && (price <= 1000))
            {
                return 1000;
            }
            else if ((price > 1000) && (price <= 5000))
            {
                return 5000;
            }
            else if ((price > 5000) && (price <= 10000))
            {
                return 10000;
            }
            else if (price > 10000)
            {
                return 20000;
            }
            else
            {
                return 999999;
            }
        }


        private string GetPriceString(int priceCode)
        {
            switch (priceCode)
            {
                case 0:
                    return "Free";
                case 100:
                    return "Below 100 " + _currency;
                case 200:
                    return "Between 100 and 200 " + _currency;
                case 300:
                    return "Between 200 and 300 " + _currency;
                case 600:
                    return "Between 300 and 600 " + _currency;
                case 1000:
                    return "Between 600 and 1000 " + _currency;
                case 5000:
                    return "Between 1000 and 5000 " + _currency;
                case 10000:
                    return "Between 5000 and 10000 " + _currency;
                case 20000:
                    return "Above 10000 " + _currency;
                case 999999:
                    return "No price";
                default:
                    return "";
            }
        }


        /// <summary>
        /// Gets or sets the Alphabetic value
        /// </summary>
        public override object? Value
        {
            get { return val; }
            set
            {
                if (object.ReferenceEquals(value, DBNull.Value) || value == null)
                {
                    _priceCode = noPrice;
                    val = _priceCode;
                }
                else
                {
                    _priceCode = GetPriceCode(decimal.Parse(value.ToString()));
                    val = _priceCode;
                }
            }
        }

        #region "ICloneable Members"

        /// <summary>
        /// Overrides the Clone() function
        /// </summary>
        /// <returns>OutlookGridAlphabeticGroup</returns>
        public override object Clone()
        {
            OutlookGridPriceGroup gr = new OutlookGridPriceGroup(this.ParentGroup);

            gr.Column = this.Column;
            gr.Value = this.val;
            gr.Collapsed = this.Collapsed;
            gr.Height = this.Height;
            gr.GroupImage = this.GroupImage;
            gr.FormatStyle = this.FormatStyle;
            gr.XxxItemsText = this.XxxItemsText;
            gr.OneItemText = this.OneItemText;
            gr.AllowHiddenWhenGrouped = this.AllowHiddenWhenGrouped;
            gr.SortBySummaryCount = this.SortBySummaryCount;
            gr._currency = _currency;
            gr._priceCode = _priceCode;
            return gr;
        }

        #endregion

        #region "IComparable Members"
        /// <summary>
        /// overide the CompareTo, so only the first character is compared, instead of the whole string
        /// this will result in classifying each item into a letter of the Alphabet.
        /// for instance, this is usefull when grouping names, they will be categorized under the letters A, B, C etc..
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object obj)
        {
            int orderModifier = (Column.SortDirection == SortOrder.Ascending ? 1 : -1);
            int priceOther = 0;

            if (obj is OutlookGridPriceGroup)
            {
                priceOther = ((OutlookGridPriceGroup)obj)._priceCode;
            }
            else
            {
                priceOther = noPrice;
            }
            return _priceCode.CompareTo(priceOther) * orderModifier;
        }
        #endregion
    }

}