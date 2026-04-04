using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Krypton.Utilities;

namespace TestForm
{
    public partial class AdvancedDataGridViewTest : KryptonForm
    {
        #region Instance Fields

        private DataTable? _dataTable = null;
        private DataSet? _dataSet = null;

        private SortedDictionary<int, string?> _filtersaved = new SortedDictionary<int, string?>();
        private SortedDictionary<int, string?> _sortsaved = new SortedDictionary<int, string?>();

        private bool _testtranslations = false;
        private bool _testtranslationsFromFile = false;

        private static int DisplayItemsCounter = 100;

        private static bool MemoryTestEnabled = true;
        private const int MEMORY_TEST_NUM = 100;
        private bool _memorytest = false;
        private object[][] _inrows = new object[][] { };

        private Timer? _memorytestclosetimer = null;
        private Timer? _timermemoryusage = null;

        private static bool CollectGarbageOnTimerMemoryUsageUpdate = true;

        #endregion

        #region Identity

        public AdvancedDataGridViewTest()
        {
            InitializeComponent();

            //set timers
            if (components != null)
            {
                _memorytestclosetimer = new Timer(components)
                {
                    Interval = 10
                };
                _timermemoryusage = new Timer(components)
                {
                    Interval = 2000
                };
            }

            //trigger the memory usage show
            _timermemoryusage_Tick(null, null);

            //set localization strings
            Dictionary<string, string> translations = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> translation in KryptonAdvancedDataGridView.Translations)
            {
                if (!translations.ContainsKey(translation.Key))
                {
                    translations.Add(translation.Key, $".{translation.Value}");
                }
            }
            foreach (KeyValuePair<string, string> translation in KryptonAdvancedDataGridViewSearchToolBar.Translations)
            {
                if (!translations.ContainsKey(translation.Key))
                {
                    translations.Add(translation.Key, $".{translation.Value}");
                }
            }
            if (_testtranslations)
            {
                KryptonAdvancedDataGridView.SetTranslations(translations);
                KryptonAdvancedDataGridViewSearchToolBar.SetTranslations(translations);
            }
            if (_testtranslationsFromFile)
            {
                KryptonAdvancedDataGridView.SetTranslations(KryptonAdvancedDataGridView.LoadTranslationsFromFile("lang.json"));
                KryptonAdvancedDataGridViewSearchToolBar.SetTranslations(KryptonAdvancedDataGridViewSearchToolBar.LoadTranslationsFromFile("lang.json"));
            }

            //set filter and sort saved
            _filtersaved.Add(0, "");
            _sortsaved.Add(0, "");
            kcmbSavedFilters.DataSource = new BindingSource(_filtersaved, null!);
            kcmbSavedFilters.DisplayMember = "Key";
            kcmbSavedFilters.ValueMember = "Value";
            kcmbSavedFilters.SelectedIndex = -1;
            kcmbSortSaved.DataSource = new BindingSource(_sortsaved, null!);
            kcmbSortSaved.DisplayMember = "Key";
            kcmbSortSaved.ValueMember = "Value";
            kcmbSortSaved.SelectedIndex = -1;

            //set memory test button
            kbtnMemoryTest.Enabled = MemoryTestEnabled;

            //initialize dataset
            _dataTable = new DataTable();
            _dataSet = new DataSet();

            //initialize bindingsource
            bsData.DataSource = _dataSet;

            //initialize datagridview
            kadgvMain.SetDoubleBuffered();
            kadgvMain.DataSource = bsData;

            //set bindingsource
            SetTestData();
        }

        public AdvancedDataGridViewTest(bool memoryTest, object[][] inRows) : this()
        {
            _memorytest = memoryTest;

            _inrows = inRows;
        }

        #endregion

        #region Implementation

        /// <summary>
        /// Loads a sample flag PNG from the output directory when present; otherwise builds a small solid-color placeholder
        /// so the Advanced DataGridView image column demo runs without shipping binary assets.
        /// </summary>
        private static Image LoadOrCreateSampleFlag(string fileName, Color fallbackColor)
        {
            string path = Path.Combine(Application.StartupPath, fileName);
            if (File.Exists(path))
            {
                return Image.FromFile(path);
            }

            var bmp = new Bitmap(24, 24);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(fallbackColor);
            }

            return bmp;
        }

        private void kbtnLoadRandomData_Click(object sender, EventArgs e)
        {
            //add test data to bindsource
            AddTestData();
        }

        private void SetTestData()
        {
            _dataTable = _dataSet?.Tables.Add("TableTest");
            if (_dataTable != null)
            {
                _dataTable.Columns.Add("int", typeof(int));
                _dataTable.Columns.Add("decimal", typeof(decimal));
                _dataTable.Columns.Add("double", typeof(double));
                _dataTable.Columns.Add("date", typeof(DateTime));
                _dataTable.Columns.Add("datetime", typeof(DateTime));
                _dataTable.Columns.Add("string", typeof(string));
                _dataTable.Columns.Add("boolean", typeof(bool));
                _dataTable.Columns.Add("guid", typeof(Guid));
                _dataTable.Columns.Add("image", typeof(Bitmap));
                _dataTable.Columns.Add("timespan", typeof(TimeSpan));

                bsData.DataMember = _dataTable.TableName;
            }

            kryptonAdvancedDataGridViewSearchToolBar1.SetColumns(kadgvMain.Columns);
        }

        private void AddTestData()
        {
            Random r = new Random();
            Image[] sampleImages = new Image[2];
            sampleImages[0] = LoadOrCreateSampleFlag("flag-green_24.png", Color.FromArgb(72, 170, 72));
            sampleImages[1] = LoadOrCreateSampleFlag("flag-red_24.png", Color.FromArgb(210, 72, 72));

            int maxMinutes = (int)((TimeSpan.FromHours(20) - TimeSpan.FromHours(10)).TotalMinutes);

            if (_inrows.Length == 0)
            {
                for (int i = 0; i < DisplayItemsCounter; i++)
                {
                    object[] newRow =
                    [
                        i,
                        Math.Round((decimal)i*2/3, 6),
                        Math.Round(i % 2 == 0 ? (double)i*2/3 : (double)i/2, 6),
                        DateTime.Today.AddHours(i*2).AddHours(i%2 == 0 ?i*10+1:0).AddMinutes(i%2 == 0 ?i*10+1:0).AddSeconds(i%2 == 0 ?i*10+1:0).AddMilliseconds(i%2 == 0 ?i*10+1:0).Date,
                        DateTime.Today.AddHours(i*2).AddHours(i%2 == 0 ?i*10+1:0).AddMinutes(i%2 == 0 ?i*10+1:0).AddSeconds(i%2 == 0 ?i*10+1:0).AddMilliseconds(i%2 == 0 ?i*10+1:0),
                        (i*2 % 3 == 0 ? null : $"{i} str")!,
                        i % 2 == 0 ? true:false,
                        Guid.NewGuid(),
                        sampleImages[r.Next(0, 2)],
                        TimeSpan.FromHours(10).Add(TimeSpan.FromMinutes(r.Next(maxMinutes)))
                    ];

                    _dataTable?.Rows.Add(newRow);
                }
            }
            else
            {
                for (int i = 0; i < _inrows.Length; i++)
                {
                    _dataTable?.Rows.Add(_inrows[i]);
                }
            }

        }

        private void AdvancedDataGridView_Load(object sender, EventArgs e)
        {
            //add test data to bindsource
            AddTestData();

            //setup datagridview
            kadgvMain.SetFilterDateAndTimeEnabled(kadgvMain.Columns["datetime"], true);
            kadgvMain.SetSortEnabled(kadgvMain.Columns["guid"], false);
            kadgvMain.SetFilterChecklistEnabled(kadgvMain.Columns["guid"], false);
            kadgvMain.SortAscending(kadgvMain.Columns["datetime"]);
            kadgvMain.SortDescending(kadgvMain.Columns["double"]);
            kadgvMain.SetTextFilterRemoveNodesOnSearch(kadgvMain.Columns["double"], false);
            kadgvMain.SetChecklistTextFilterRemoveNodesOnSearchMode(kadgvMain.Columns["decimal"], false);
            kadgvMain.SetFilterChecklistEnabled(kadgvMain.Columns["double"], false);
            kadgvMain.SetFilterCustomEnabled(kadgvMain.Columns["timespan"], false);
            kadgvMain.CleanSort(kadgvMain.Columns["datetime"]);
            kadgvMain.SetFilterChecklistTextFilterTextChangedDelayNodes(kadgvMain.Columns["string"], 10);
            kadgvMain.SetFilterChecklistTextFilterTextChangedDelayMs(kadgvMain.Columns["string"], 500);

            //memory test
            if (!_memorytest)
            {
                //set timer memory usage
                _timermemoryusage?.Enabled = true;
                _timermemoryusage?.Tick += _timermemoryusage_Tick;
            }
            else
            {
                kryptonPanel1.Visible = false;

                _memorytestclosetimer?.Enabled = true;
                _memorytestclosetimer?.Tick += _memorytestclosetimer_Tick;

                foreach (DataGridViewColumn column in kadgvMain.Columns)
                    kadgvMain.ShowMenuStrip(column);
            }
        }

        private void kadgvMain_FilterStringChanged(object sender, KryptonAdvancedDataGridView.FilterEventArgs e)
        {
            //eventually set the FilterString here
            //if e.Cancel is set to true one have to update the datasource here using
            //bindingSource_main.Filter = kadgvMain.FilterString;
            //otherwise it will be updated by the component

            //sample use of the override string filter
            string stringColumnFilter = ktxtStringFilter.Text;
            if (!string.IsNullOrEmpty(stringColumnFilter))
            {
                e.FilterString += (!string.IsNullOrEmpty(e.FilterString) ? " AND " : "") +
                                  $"string LIKE '%{stringColumnFilter.Replace("'", "''")}%'";
            }

            ktxtFilterString.Text = e.FilterString;
        }

        private void kadgvMain_SortStringChanged(object sender, KryptonAdvancedDataGridView.SortEventArgs e)
        {
            //eventually set the SortString here
            //if e.Cancel is set to true one have to update the datasource here
            //bindingSource_main.Sort = kadgvMain.SortString;
            //otherwise it will be updated by the component

            ktxtSortString.Text = e.SortString;
        }

        private void ktxtStringFilter_TextChanged(object sender, EventArgs e)
        {
            //trigger the filter string changed function when text is changed
            kadgvMain.TriggerFilterStringChanged();
        }

        private void bsData_ListChanged(object sender, ListChangedEventArgs e)
        {
            ktxtTotalRows.Text = bsData.List.Count.ToString();
        }

        private void kbtnSaveFilters_Click(object sender, EventArgs e)
        {
            _filtersaved.Add((kcmbSavedFilters.Items.Count - 1) + 1, kadgvMain.FilterString);
            kcmbSavedFilters.DataSource = new BindingSource(_filtersaved, null!);
            kcmbSavedFilters.SelectedIndex = kcmbSavedFilters.Items.Count - 1;
            _sortsaved.Add((kcmbSortSaved.Items.Count - 1) + 1, kadgvMain.SortString);
            kcmbSortSaved.DataSource = new BindingSource(_sortsaved, null!);
            kcmbSortSaved.SelectedIndex = kcmbSortSaved.Items.Count - 1;
        }

        private void kbtnApplySavedFilters_Click(object sender, EventArgs e)
        {
            if (kcmbSavedFilters.SelectedIndex != -1 && kcmbSortSaved.SelectedIndex != -1)
            {
                kadgvMain.LoadFilterAndSort(kcmbSavedFilters.SelectedValue?.ToString(), kcmbSortSaved.SelectedValue?.ToString());
            }
        }

        private void kbtnClearFilters_Click(object sender, EventArgs e)
        {
            kadgvMain.CleanFilterAndSort();
            kcmbSavedFilters.SelectedIndex = -1;
            kcmbSortSaved.SelectedIndex = -1;
        }

        private void kryptonAdvancedDataGridViewSearchToolBar1_Search(object sender, AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartSearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endColumn = kadgvMain.CurrentCell != null && kadgvMain.CurrentCell.ColumnIndex + 1 >= kadgvMain.ColumnCount;
                bool endRow = kadgvMain.CurrentCell != null && kadgvMain.CurrentCell.RowIndex + 1 >= kadgvMain.RowCount;

                if (endColumn && endRow)
                {
                    if (kadgvMain.CurrentCell != null)
                    {
                        startColumn = kadgvMain.CurrentCell.ColumnIndex;
                        startRow = kadgvMain.CurrentCell.RowIndex;
                    }
                }
                else
                {
                    if (kadgvMain.CurrentCell != null)
                    {
                        startColumn = endColumn ? 0 : kadgvMain.CurrentCell.ColumnIndex + 1;
                        startRow = kadgvMain.CurrentCell.RowIndex + (endColumn ? 1 : 0);
                    }
                }
            }
            DataGridViewCell? c = kadgvMain.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartSearch)
            {
                c = kadgvMain.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch != null ? e.ColumnToSearch.Name : null,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            }

            if (c != null)
            {
                kadgvMain.CurrentCell = c;
            }
        }

        private void _timermemoryusage_Tick(object? sender, EventArgs? e)
        {
            if (CollectGarbageOnTimerMemoryUsageUpdate)
            {
                GC.Collect();
            }

            tsslMemoryUsage.Text = $@"Memory Usage: {GC.GetTotalMemory(false) / (1024 * 1024)}Mb";
        }

        private void kbtnMemoryTest_Click(object sender, EventArgs e)
        {
            if (kcmbMemoryTest.SelectedItem != null && kcmbMemoryTest.SelectedItem.ToString() == "FullForm")
            {
                //build random data
                Random r = new Random();
                Image[] sampleimages = new Image[2];
                sampleimages[0] = LoadOrCreateSampleFlag("flag-green_24.png", Color.FromArgb(72, 170, 72));
                sampleimages[1] = LoadOrCreateSampleFlag("flag-red_24.png", Color.FromArgb(210, 72, 72));
                int maxMinutes = (int)((TimeSpan.FromHours(20) - TimeSpan.FromHours(10)).TotalMinutes);
                object[][] testrows = new object[100][];
                for (int i = 0; i < 100; i++)
                {
                    object[] newrow = new object[] {
                        i,
                        Math.Round((decimal)i*2/3, 6),
                        Math.Round(i % 2 == 0 ? (double)i*2/3 : (double)i/2, 6),
                        DateTime.Today.AddHours(i*2).AddHours(i%2 == 0 ?i*10+1:0).AddMinutes(i%2 == 0 ?i*10+1:0).AddSeconds(i%2 == 0 ?i*10+1:0).AddMilliseconds(i%2 == 0 ?i*10+1:0).Date,
                        DateTime.Today.AddHours(i*2).AddHours(i%2 == 0 ?i*10+1:0).AddMinutes(i%2 == 0 ?i*10+1:0).AddSeconds(i%2 == 0 ?i*10+1:0).AddMilliseconds(i%2 == 0 ?i*10+1:0),
                        (i*2 % 3 == 0 ? null : $"{i} str")!,
                        i % 2 == 0 ? true:false,
                        Guid.NewGuid(),
                        sampleimages[r.Next(0, 2)],
                        TimeSpan.FromHours(10).Add(TimeSpan.FromMinutes(r.Next(maxMinutes)))
                    };

                    testrows.SetValue(newrow, i);
                }

                //show the forms
                for (int i = 0; i < MEMORY_TEST_NUM; i++)
                {
                    AdvancedDataGridViewTest formtest = new AdvancedDataGridViewTest(true, testrows);
                    formtest.Show();
                    //wait for the form to be disposed
                    while (!formtest.IsDisposed)
                    {
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
            else if (kcmbMemoryTest.SelectedItem != null && kcmbMemoryTest.SelectedItem.ToString() == "DataSource")
            {
                object? datasourcePrev = kadgvMain.DataSource;

                //initialize dataset
                DataTable dataTableTest = new DataTable();
                DataSet dataSetTest = new DataSet();
                dataTableTest = dataSetTest.Tables.Add("TableTest");
                dataTableTest.Columns.Add("int", typeof(int));
                dataTableTest.Columns.Add("decimal", typeof(decimal));
                dataTableTest.Columns.Add("double", typeof(double));
                //add data
                for (int i = 0; i < 100; i++)
                {
                    object[] newrow = new object[] {
                            i,
                            Math.Round((decimal)i*2/3, 6),
                            Math.Round(i % 2 == 0 ? (double)i*2/3 : (double)i/2, 6)
                        };
                    dataTableTest.Rows.Add(newrow);
                }

                //update the DataSource
                for (int i = 0; i < MEMORY_TEST_NUM; i++)
                {
                    using (BindingSource bindingSourceTest = new BindingSource())
                    {
                        bindingSourceTest.DataSource = dataSetTest;
                        bindingSourceTest.DataMember = dataTableTest.TableName;
                        kadgvMain.DataSource = null;
                        kadgvMain.ColumnHeadersVisible = false;
                        kadgvMain.DataSource = bindingSourceTest;
                        kadgvMain.Refresh();
                        kadgvMain.ColumnHeadersVisible = true;
                        Application.DoEvents();
                    }
                }

                //restore original datasource
                kadgvMain.DataSource = datasourcePrev;
            }
            else
            {
                KryptonMessageBox.Show("Select a Memory Test", "Warning", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
            }
        }

        private void _memorytestclosetimer_Tick(object? sender, EventArgs e)
        {
            _dataTable?.Rows.Clear();
            
            Close();
        }

        #endregion
    }
}
