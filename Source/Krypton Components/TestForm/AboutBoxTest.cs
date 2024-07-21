﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm
{
    public partial class AboutBoxTest : KryptonForm
    {
        public AboutBoxTest()
        {
            InitializeComponent();
        }

        private void kbtnShow_Click(object sender, EventArgs e)
        {
            KryptonAboutBoxData aboutBoxData = new KryptonAboutBoxData()
            {
                ApplicationName = kryptonTextBox2.Text,
                CurrentAssembly = Assembly.LoadFile(kryptonTextBox1.Text),
                HeaderImage = new Bitmap(kryptonTextBox3.Text),
                MainImage = new Bitmap(kryptonTextBox4.Text),
                ShowToolkitInformation = kchkShowToolkitInformation.Checked,
                UseFullBuiltOnDate = kchkUseFullBuiltOnDate.Checked
            };

            KryptonAboutToolkitData aboutToolkitData = new KryptonAboutToolkitData();

            KryptonAboutBox.Show(aboutBoxData, aboutToolkitData);
        }

        private void bsaAssemblyBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                kryptonTextBox1.Text = Path.GetFullPath(openFileDialog.SafeFileName);
            }
        }

        private void bsaBrowseHeaderImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                kryptonTextBox3.Text = Path.GetFullPath(openFileDialog.SafeFileName);
            }
        }

        private void bsaBrowseMainImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                kryptonTextBox4.Text = Path.GetFullPath(openFileDialog.SafeFileName);
            }
        }
    }
}
