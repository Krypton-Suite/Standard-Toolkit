#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved. 
 *  
 */
#endregion

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace TestForm
{
    public class DataGridViewSetup
    {
        private SandBoxGridColumn[] _activeColumns;

        public enum SandBoxGridColumn
        {
            ColumnCustomerId = 0,
            ColumnCustomerName = 1,
            ColumnAddress = 2,
            ColumnCity = 3,
            ColumnCountry = 4,
            ColumnOrderDate = 5,
            ColumnProduct = 6,
            ColumnPrice = 7,
            SatisfactionColumn = 8,
            ColumnToken = 9
        }

        public enum LoadState
        {
            Before,
            After
        }

        /// <summary>
        /// Setups the data grid view.
        /// </summary>
        /// <param name="grid">The grid.</param>
        /// <param name="restoreIfPossible">if set to <c>true</c> [restore if possible].</param>
        public void SetupDataGridView(KryptonOutlookGrid grid, bool restoreIfPossible)
        {
            if (File.Exists(Application.StartupPath + "/grid.xml") & restoreIfPossible)
            {
                try
                {
                    LoadConfigFromFile(Application.StartupPath + "/grid.xml", grid);
                }
                catch (Exception ex)
                {
                    KryptonMessageBox.Show("Error when retrieving configuration : " + ex.Message, "Error",
                        KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                    grid.ClearEverything();
                    LoadDefaultConfiguration(grid);
                }
            }
            else
            {
                LoadDefaultConfiguration(grid);
            }
        }

        /// <summary>
        /// Loads the default configuration.
        /// </summary>
        /// <param name="grid">The grid.</param>
        private void LoadDefaultConfiguration(KryptonOutlookGrid grid)
        {
            grid.ClearEverything();
            grid.GroupBox!.Visible = true;
            grid.HideColumnOnGrouping = false;

            grid.FillMode = GridFillMode.GroupsAndNodes; //treemode enabled;
            grid.ShowLines = true;

            _activeColumns = new SandBoxGridColumn[] {
                SandBoxGridColumn.ColumnCustomerId,
                SandBoxGridColumn.ColumnCustomerName,
                SandBoxGridColumn.ColumnAddress,
                SandBoxGridColumn.ColumnCity,
                SandBoxGridColumn.ColumnCountry,
                SandBoxGridColumn.ColumnOrderDate,
                SandBoxGridColumn.ColumnProduct,
                SandBoxGridColumn.ColumnPrice,
                SandBoxGridColumn.SatisfactionColumn,
                SandBoxGridColumn.ColumnToken
            };

            DataGridViewColumn[] columnsToAdd = new DataGridViewColumn[10]
            {
                SetupColumn(SandBoxGridColumn.ColumnCustomerId),
                SetupColumn(SandBoxGridColumn.ColumnCustomerName),
                SetupColumn(SandBoxGridColumn.ColumnAddress),
                SetupColumn(SandBoxGridColumn.ColumnCity),
                SetupColumn(SandBoxGridColumn.ColumnCountry),
                SetupColumn(SandBoxGridColumn.ColumnOrderDate),
                SetupColumn(SandBoxGridColumn.ColumnProduct),
                SetupColumn(SandBoxGridColumn.ColumnPrice),
                SetupColumn(SandBoxGridColumn.SatisfactionColumn),
                SetupColumn(SandBoxGridColumn.ColumnToken)
            };

            grid.Columns.AddRange(columnsToAdd);

            //Define the columns for a possible grouping
            grid.AddInternalColumn(columnsToAdd[0], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(columnsToAdd[1], new OutlookGridAlphabeticGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(columnsToAdd[2], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(columnsToAdd[3], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(columnsToAdd[4], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(columnsToAdd[5], new OutlookGridDateTimeGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(columnsToAdd[6], new OutlookGridDefaultGroup(null) { OneItemText = "1 product", XxxItemsText = " products" }, SortOrder.None, -1, -1);
            grid.AddInternalColumn(columnsToAdd[7], new OutlookGridPriceGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(columnsToAdd[8], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);
            grid.AddInternalColumn(columnsToAdd[9], new OutlookGridDefaultGroup(null), SortOrder.None, -1, -1);

            //Add a default conditionnal formatting
            ConditionalFormatting cond = new ConditionalFormatting();
            cond.ColumnName = SandBoxGridColumn.ColumnPrice.ToString();
            cond.FormatType = EnumConditionalFormatType.TwoColorsRange;
            cond.FormatParams = new TwoColorsParams(Color.White, Color.FromArgb(255, 255, 58, 61));
            grid.ConditionalFormatting.Add(cond);
        }


        /// <summary>
        /// Loads the configuration from file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="grid">The grid.</param>
        private void LoadConfigFromFile(string file, KryptonOutlookGrid grid)
        {
            if (string.IsNullOrEmpty(file))
                throw new Exception("Grid config file is missing !");

            XDocument doc = XDocument.Load(file);
            int versionGrid = -1;
            int.TryParse(doc.Element("OutlookGrid")?.Attribute("V")?.Value, out versionGrid);

            //Upgrade if necessary the config file
            CheckAndUpgradeConfigFile(versionGrid, doc, grid, LoadState.Before);
            grid.ClearEverything();
            grid.GroupBox!.Visible = CommonHelper.StringToBool(doc.XPathSelectElement("OutlookGrid/GroupBox")?.Value!);
            grid.HideColumnOnGrouping = CommonHelper.StringToBool(doc.XPathSelectElement("OutlookGrid/HideColumnOnGrouping")?.Value!);

            //Initialize
            int nbColsInFile = doc.XPathSelectElements("//Column").Count();
            DataGridViewColumn[] columnsToAdd = new DataGridViewColumn[nbColsInFile];
            SandBoxGridColumn[] enumCols = new SandBoxGridColumn[nbColsInFile];
            OutlookGridColumn[] outlookColumnsToAdd = new OutlookGridColumn[columnsToAdd.Length];
            SortedList<int, int> hash = new SortedList<int, int>();// (DisplayIndex , Index)


            int i = 0;
            IOutlookGridGroup? group;
            XElement? node2;

            foreach (var xNode in doc.XPathSelectElement("OutlookGrid/Columns")?.Nodes()!)
            {
                var node = (XElement)xNode;
                //Create the columns and restore the saved properties
                //As the OutlookGrid receives constructed DataGridViewColumns, only the parent application can recreate them (dgvcolumn not serializable)
                enumCols[i] = (SandBoxGridColumn)Enum.Parse(typeof(SandBoxGridColumn), node.Element("Name")?.Value!);
                columnsToAdd[i] = SetupColumn(enumCols[i]);
                columnsToAdd[i].Width = int.Parse(node.Element("Width")?.Value!);
                columnsToAdd[i].Visible = CommonHelper.StringToBool(node.Element("Visible")?.Value!);
                hash.Add(int.Parse(node.Element("DisplayIndex")?.Value!), i);
                //Reinit the group if it has been set previously
                group = null;
                if (!node.Element("GroupingType")!.IsEmpty && node.Element("GroupingType")!.HasElements)
                {
                    node2 = node.Element("GroupingType");
                    group = (IOutlookGridGroup)Activator.CreateInstance(Type.GetType(TypeConverter.ProcessType(node2?.Element("Name")?.Value!)!, true)!);
                    group.OneItemText = node2?.Element("OneItemText")?.Value;
                    group.XxxItemsText = node2?.Element("XXXItemsText")?.Value;
                    group.SortBySummaryCount = CommonHelper.StringToBool(node2?.Element("SortBySummaryCount")?.Value!);
                    if (!string.IsNullOrEmpty(node2?.Element("ItemsComparer")?.Value))
                    {
                        object comparer = Activator.CreateInstance(Type.GetType(TypeConverter.ProcessType(node2?.Element("ItemsComparer")?.Value)!, true)!);
                        group.ItemsComparer = (IComparer)comparer;
                    }
                    if (node2!.Element("Name")!.Value.Contains("OutlookGridDateTimeGroup"))
                    {
                        ((OutlookGridDateTimeGroup)group).Interval = (DateInterval)Enum.Parse(typeof(DateInterval), node2.Element("GroupDateInterval")?.Value!);
                    }
                }
                outlookColumnsToAdd[i] = new OutlookGridColumn(columnsToAdd[i], group, (SortOrder)Enum.Parse(typeof(SortOrder), node.Element("SortDirection")?.Value!), int.Parse(node.Element("GroupIndex")?.Value!), int.Parse(node.Element("SortIndex")?.Value!));

                i += 1;
            }
            //First add the underlying DataGridViewColumns to the grid
            grid.Columns.AddRange(columnsToAdd);
            //And then the outlookgrid columns
            grid.AddRangeInternalColumns(outlookColumnsToAdd);

            //Add conditional formatting to the grid
            EnumConditionalFormatType conditionFormatType;
            IFormatParams? conditionFormatParams;
            foreach (var xNode in (doc.XPathSelectElement("OutlookGrid/ConditionalFormatting") != null ? doc.XPathSelectElement("OutlookGrid/ConditionalFormatting")?.Nodes() : null)!)
            {
                var node = (XElement)xNode;
                conditionFormatType = (EnumConditionalFormatType)Enum.Parse(typeof(EnumConditionalFormatType), node.Element("FormatType")?.Value!);
                XElement? nodeParams = node.Element("FormatParams");
                switch (conditionFormatType)
                {
                    case EnumConditionalFormatType.Bar:
                        conditionFormatParams = new BarParams(Color.FromArgb(int.Parse(nodeParams?.Element("BarColor")?.Value!)), CommonHelper.StringToBool(nodeParams?.Element("GradientFill")?.Value!));
                        break;
                    case EnumConditionalFormatType.ThreeColorsRange:
                        conditionFormatParams = new ThreeColorsParams(Color.FromArgb(int.Parse(nodeParams?.Element("MinimumColor")?.Value!)), Color.FromArgb(int.Parse(nodeParams?.Element("MediumColor")?.Value!)), Color.FromArgb(int.Parse(nodeParams?.Element("MaximumColor")?.Value!)));
                        break;
                    case EnumConditionalFormatType.TwoColorsRange:
                        conditionFormatParams = new TwoColorsParams(Color.FromArgb(int.Parse(nodeParams?.Element("MinimumColor")?.Value!)), Color.FromArgb(int.Parse(nodeParams?.Element("MaximumColor")?.Value!)));
                        break;
                    default:
                        conditionFormatParams = null;
                        //will never happened but who knows ? throw exception ?
                        break;
                }
                grid.ConditionalFormatting.Add(new ConditionalFormatting(node.Element("ColumnName")?.Value!, conditionFormatType, conditionFormatParams));
            }



            //We need to loop through the columns in the order of the display order, starting at zero; otherwise the columns will fall out of order as the loop progresses.
            foreach (KeyValuePair<int, int> kvp in hash)
            {
                columnsToAdd[kvp.Value].DisplayIndex = kvp.Key;
            }

            _activeColumns = enumCols;
        }

        /// <summary>
        /// Checks the and upgrade configuration file.
        /// </summary>
        /// <param name="versionGrid">The version grid.</param>
        /// <param name="doc">The document.</param>
        /// <param name="grid">The grid.</param>
        /// <param name="state">The state.</param>
        private void CheckAndUpgradeConfigFile(int versionGrid, XDocument doc, KryptonOutlookGrid grid, LoadState state)
        {
            while (versionGrid < StaticInfos._GRIDCONFIG_VERSION)
            {
                UpgradeGridConfigToVx(doc, versionGrid + 1, grid, state);
                versionGrid += 1;
            }
        }

        /// <summary>
        /// Upgrades the grid configuration to vx.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="version">The version.</param>
        /// <param name="grid">The grid.</param>
        /// <param name="state">The state.</param>
        private void UpgradeGridConfigToVx(XDocument doc, int version, KryptonOutlookGrid grid, LoadState state)
        {
            //Do changes according to version
            //For example you can add automatically new columns. This can be useful when you update your application to add columns and would like to display them to the user for the first time.
            //switch (version)
            //{
            //case 2:
            //    // Do changes to match the V2
            //    if (state == DataGridViewSetup.LoadState.Before)
            //    {
            //        doc.Element("OutlookGrid").Attribute("V").Value = version.ToString();
            //        Array.Resize(ref activeColumns, activeColumns.Length + 1);
            //        DataGridViewColumn columnToAdd = SetupColumn(SandBoxGridColumn.ColumnPrice2);
            //        Grid.Columns.Add(columnToAdd);
            //        Grid.AddInternalColumn(columnToAdd, new OutlookGridDefaultGroup(null)
            //        {
            //            OneItemText = "Example",
            //            XXXItemsText = "Examples"
            //        }, SortOrder.None, -1, -1);
            //        activeColumns[activeColumns.Length - 1] = SandBoxGridColumn.ColumnPrice2;

            //        Grid.PersistConfiguration(PublicFcts.GetGridConfigFile, version);
            //    }
            //    break;
            //}
        }


        /// <summary>
        /// Use this function if you do not add your columns at design time.
        /// </summary>
        /// <param name="colType"></param>
        /// <returns></returns>
        private DataGridViewColumn SetupColumn(SandBoxGridColumn colType)
        {
            DataGridViewColumn? column;
            switch (colType)
            {
                case SandBoxGridColumn.ColumnCustomerId:
                    column = new KryptonDataGridViewTextBoxColumn();
                    column.HeaderText = @"Customer ID";
                    column.Name = "ColumnCustomerID";
                    column.SortMode = DataGridViewColumnSortMode.Programmatic;
                    column.Width = 79;
                    return column;
                case SandBoxGridColumn.ColumnCustomerName:
                    column = new KryptonDataGridViewTreeTextColumn();// KryptonDataGridViewTextBoxColumn();
                    column.HeaderText = @"Name";
                    column.Name = "ColumnCustomerName";
                    column.SortMode = DataGridViewColumnSortMode.Programmatic;
                    column.Width = 79;
                    return column;
                case SandBoxGridColumn.ColumnAddress:
                    column = new KryptonDataGridViewTextBoxColumn();
                    column.HeaderText = @"Address";
                    column.Name = "ColumnAddress";
                    column.SortMode = DataGridViewColumnSortMode.Programmatic;
                    column.Width = 79;
                    return column;
                case SandBoxGridColumn.ColumnCity:
                    column = new KryptonDataGridViewTextBoxColumn();
                    column.HeaderText = @"City";
                    column.Name = "ColumnCity";
                    column.SortMode = DataGridViewColumnSortMode.Programmatic;
                    column.Width = 79;
                    return column;
                case SandBoxGridColumn.ColumnCountry:
                    column = new KryptonDataGridViewTextAndImageColumn();
                    column.HeaderText = @"Country";
                    column.Name = "ColumnCountry";
                    column.Resizable = DataGridViewTriState.True;
                    column.SortMode = DataGridViewColumnSortMode.Programmatic;
                    column.Width = 78;
                    return column;
                case SandBoxGridColumn.ColumnOrderDate:
                    column = new KryptonDataGridViewDateTimePickerColumn();
                    ((KryptonDataGridViewDateTimePickerColumn)column).CalendarTodayDate = DateTime.Now;
                    ((KryptonDataGridViewDateTimePickerColumn)column).Checked = false;
                    ((KryptonDataGridViewDateTimePickerColumn)column).Format = DateTimePickerFormat.Short;
                    column.HeaderText = @"Order Date";
                    column.Name = "ColumnOrderDate";
                    column.SortMode = DataGridViewColumnSortMode.Programmatic;
                    column.Width = 79;
                    return column;
                case SandBoxGridColumn.ColumnProduct:
                    column = new KryptonDataGridViewTextBoxColumn();
                    column.HeaderText = @"Product";
                    column.Name = "ColumnProduct";
                    column.SortMode = DataGridViewColumnSortMode.Programmatic;
                    column.Width = 79;
                    return column;
                case SandBoxGridColumn.ColumnPrice:
                    column = new KryptonDataGridViewFormattingColumn();
                    column.Name = colType.ToString();
                    column.ValueType = typeof(decimal); //really  important for formatting
                    DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
                    dataGridViewCellStyle1.Format = "C2";
                    dataGridViewCellStyle1.NullValue = "";
                    column.DefaultCellStyle = dataGridViewCellStyle1;
                    column.HeaderText = @"Price";
                    column.Resizable = DataGridViewTriState.True;
                    column.SortMode = DataGridViewColumnSortMode.Programmatic;
                    column.Width = 79;
                    return column;
                case SandBoxGridColumn.SatisfactionColumn:
                    column = new KryptonDataGridViewPercentageColumn();
                    DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
                    dataGridViewCellStyle2.Format = "0%";
                    column.DefaultCellStyle = dataGridViewCellStyle2;
                    column.HeaderText = @"Satisfaction";
                    column.Name = colType.ToString();
                    column.SortMode = DataGridViewColumnSortMode.Programmatic;
                    return column;
                case SandBoxGridColumn.ColumnToken:
                    column = new KryptonDataGridViewTokenColumn();
                    column.Name = colType.ToString();
                    column.ReadOnly = true;
                    column.SortMode = DataGridViewColumnSortMode.Programmatic;
                    column.HeaderText = @"Tag";
                    return column;
                default:
                    throw new Exception("Unknown Column Type !! TODO improve that !");
            }
        }

    }
}

public class TypeConverter
{
    public static string? ProcessType(string? fullQualifiedName)
    {
        //Translate types here to accomodate code changes, namespaces and version
        //Select Case FullQualifiedName
        //    Case "JDHSoftware.Krypton.Toolkit.KryptonOutlookGrid.OutlookGridAlphabeticGroup, JDHSoftware.Krypton.Toolkit, Version=1.2.0.0, Culture=neutral, PublicKeyToken=e12f297423986ef5",
        //        "JDHSoftware.Krypton.Toolkit.KryptonOutlookGrid.OutlookGridAlphabeticGroup, JDHSoftware.Krypton.Toolkit, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null"
        //        'Change with new version or namespace or both !
        //        FullQualifiedName = "TestMe, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null"
        //        Exit Select
        //End Select
        return fullQualifiedName;
    }
}