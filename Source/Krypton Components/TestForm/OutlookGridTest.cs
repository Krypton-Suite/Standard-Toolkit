using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.Expando;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestForm
{
    public partial class OutlookGridTest : KryptonForm
    {
        private bool _expand = true;
        private static Random _random = new Random();

        public OutlookGridTest()
        {
            InitializeComponent();
        }

        DateTime GetRandomDate(DateTime dtStart, DateTime dtEnd)
        {
            int cdayRange = (dtEnd - dtStart).Days;

            return dtStart.AddDays(_random.NextDouble() * cdayRange);
        }

        private void LoadData()
        {
            //Setup Rows
            OutlookGridRow row = new OutlookGridRow();
            List<OutlookGridRow> l = new List<OutlookGridRow>();
            kryptonOutlookGrid1.SuspendLayout();
            kryptonOutlookGrid1.ClearInternalRows();
            kryptonOutlookGrid1.FillMode = GridFillMode.GroupsAndNodes;

            List<Token> tokensList = new List<Token>();
            tokensList.Add(new Token("Best seller", Color.Orange, Color.Black));
            tokensList.Add(new Token("New", Color.LightGreen, Color.Black));
            tokensList.Add(null);
            tokensList.Add(null);
            tokensList.Add(null);

            Random random = new Random();

            // Returns a random number within the range specified in parentheses.
            XmlDocument doc = new XmlDocument();
            doc.Load(@"invoices.xml");
            IFormatProvider culture = new CultureInfo("en-US", true);
            foreach (XmlNode customer in doc.SelectNodes("//invoice")!) //TODO for instead foreach for perfs...
            {
                try
                {
                    row = new OutlookGridRow();
                    row.CreateCells(kryptonOutlookGrid1, new object[] {
                        customer["CustomerID"]!.InnerText,
                        customer["CustomerName"]!.InnerText,
                        customer["Address"]!.InnerText,
                        customer["City"]!.InnerText,
                        new TextAndImage(customer["Country"]!.InnerText,GetFlag(customer["Country"]!.InnerText)),
                        DateTime.Parse(customer["OrderDate"]!.InnerText,culture),
                        customer["ProductName"]!.InnerText,
                        double.Parse(customer["Price"]!.InnerText, CultureInfo.InvariantCulture), //We put a float the formatting in design does the rest
                        (double)random.Next(101) /100,
                        tokensList[random.Next(5)]
                    });
                    if (random.Next(2) == 1)
                    {
                        //Sub row
                        OutlookGridRow row2 = new OutlookGridRow();
                        row2.CreateCells(kryptonOutlookGrid1, new object[] {
                            customer["CustomerID"]!.InnerText + " 2",
                            customer["CustomerName"]!.InnerText + " 2",
                            customer["Address"]!.InnerText + "2",
                            customer["City"]!.InnerText + " 2",
                            new TextAndImage(customer["Country"]!.InnerText,GetFlag(customer["Country"]!.InnerText)),
                            DateTime.Now,
                            customer["ProductName"]!.InnerText + " 2",
                            (double)random.Next(1000),
                            (double)random.Next(101) /100,
                            tokensList[random.Next(5)]
                        });
                        row.Nodes.Add(row2);
                        ((KryptonDataGridViewTreeTextCell)row2.Cells[1]).UpdateStyle(); //Important : after added to the parent node
                    }
                    l.Add(row);
                    ((KryptonDataGridViewTreeTextCell)row.Cells[1]).UpdateStyle(); //Important : after added to the rows list
                }
                catch (Exception ex)
                {
                    KryptonMessageBox.Show("Gasp...Something went wrong ! " + ex.Message, "Error", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                }
            }



            kryptonOutlookGrid1.ResumeLayout();
            kryptonOutlookGrid1.AssignRows(l);
            kryptonOutlookGrid1.ForceRefreshGroupBox();
            kryptonOutlookGrid1.Fill();
        }

        private void OutlookGridTest_Load(object sender, EventArgs e)
        {
            kryptonOutlookGrid1.GroupBox = kryptonOutlookGridGroupBox1;
            kryptonOutlookGrid1.RegisterGroupBoxEvents();

            DataGridViewSetup setup = new DataGridViewSetup();
            setup.SetupDataGridView(this.kryptonOutlookGrid1, true);

            kryptonOutlookGrid1.ShowLines = true;
            LoadData();
        }

        private Image? GetFlag(string country)
        {
            //Icons from http://365icon.com/icon-styles/ethnic/classic2/

            switch (country)
            {
                case "France":
                    return Properties.Resources.fr;
                case "Germany":
                    return Properties.Resources.de;
                case "Argentina":
                    return Properties.Resources.ar;
                case "Austria":
                    return Properties.Resources.au;
                case "Belgium":
                    return Properties.Resources.be;
                case "Brazil":
                    return Properties.Resources.br;
                case "Canada":
                    return Properties.Resources.ca;
                case "Denmark":
                    return Properties.Resources.dk;
                case "Finland":
                    return Properties.Resources.fi;
                case "Ireland":
                    return Properties.Resources.ie;
                case "Italy":
                    return Properties.Resources.it;
                case "Mexico":
                    return Properties.Resources.mx;
                case "Norway":
                    return Properties.Resources.no;
                case "Poland":
                    return Properties.Resources.pl;
                case "Portugal":
                    return Properties.Resources.pt;
                case "Spain":
                    return Properties.Resources.es;
                case "Sweden":
                    return Properties.Resources.se;
                case "Switzerland":
                    return Properties.Resources.ch;
                case "UK":
                    return Properties.Resources.gb;
                case "USA":
                    return Properties.Resources.us;
                case "Venezuela":
                    return Properties.Resources.ve;
                default:
                    return null;
            }
        }

        private void kryptonOutlookGrid1_Resize(object sender, EventArgs e)
        {
            int preferredTotalWidth = 0;
            //Calculate the total preferred width
            foreach (DataGridViewColumn c in kryptonOutlookGrid1.Columns)
            {
                preferredTotalWidth += Math.Min(c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.DisplayedCells, true), 250);
            }

            if (kryptonOutlookGrid1.Width > preferredTotalWidth)
            {
                kryptonOutlookGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                kryptonOutlookGrid1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            }
            else
            {
                kryptonOutlookGrid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                foreach (DataGridViewColumn c in kryptonOutlookGrid1.Columns)
                {
                    c.Width = Math.Min(c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.DisplayedCells, true), 250);
                }
            }
        }

        private void kryptonOutlookGrid1_GroupImageClick(object sender, OutlookGridGroupImageEventArgs e)
        {
            KryptonMessageBox.Show("Group Image clicked for group row : " + e.Row.Group!.Text);
        }

        private void bsahgLoad_Click(object sender, EventArgs e)
        {
            DataGridViewSetup setup = new DataGridViewSetup();
            setup.SetupDataGridView(kryptonOutlookGrid1, true);
            LoadData();
        }

        private void bsahgSave_Click(object sender, EventArgs e)
        {
            kryptonOutlookGrid1.PersistConfiguration(Application.StartupPath + "/grid.xml", StaticInfos._GRIDCONFIG_VERSION.ToString());
        }

        private void bsahgToggle_Click(object sender, EventArgs e)
        {
            if (_expand)
                kryptonOutlookGrid1.ExpandAllNodes();
            else
                kryptonOutlookGrid1.CollapseAllNodes();

            _expand = !_expand;
        }
    }
}
