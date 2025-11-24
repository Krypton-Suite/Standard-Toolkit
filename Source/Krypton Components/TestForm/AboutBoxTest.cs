#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

using System.IO;
using System.Reflection;

namespace TestForm;

public partial class AboutBoxTest : KryptonForm
{
    public AboutBoxTest()
    {
        InitializeComponent();
    }

    private void kbtnShow_Click(object sender, EventArgs e)
    {
        // Validate and load assembly
        if (string.IsNullOrWhiteSpace(kryptonTextBox1.Text))
        {
            MessageBox.Show("Please choose an assembly (.dll or .exe) first.", "No Assembly Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var assemblyPath = kryptonTextBox1.Text;

        if (!File.Exists(assemblyPath))
        {
            MessageBox.Show($"The file '{assemblyPath}' does not exist.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var ext = Path.GetExtension(assemblyPath).ToLowerInvariant();
        if (ext != ".dll" && ext != ".exe")
        {
            MessageBox.Show("The selected file must be a .dll or .exe assembly.", "Invalid File Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        Assembly selectedAssembly;
        try
        {
            selectedAssembly = Assembly.LoadFile(assemblyPath);
        }
        catch (BadImageFormatException)
        {
            MessageBox.Show("The selected file is not a valid .NET assembly.", "Invalid Assembly", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred while loading the assembly:\n{ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        Image? headerImage = null;
        if (!string.IsNullOrWhiteSpace(kryptonTextBox3.Text) && File.Exists(kryptonTextBox3.Text))
        {
            headerImage = new Bitmap(kryptonTextBox3.Text);
        }

        Image? mainImage = null;
        if (!string.IsNullOrWhiteSpace(kryptonTextBox4.Text) && File.Exists(kryptonTextBox4.Text))
        {
            mainImage = new Bitmap(kryptonTextBox4.Text);
        }

        KryptonAboutBoxData aboutBoxData = new KryptonAboutBoxData()
        {
            ApplicationName = kryptonTextBox2.Text,
            CurrentAssembly = selectedAssembly,
            HeaderImage = headerImage,
            MainImage = mainImage,
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
            kryptonTextBox1.Text = openFileDialog.FileName;
        }
    }

    private void bsaBrowseHeaderImage_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            kryptonTextBox3.Text = openFileDialog.FileName;
        }
    }

    private void bsaBrowseMainImage_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            kryptonTextBox4.Text = openFileDialog.FileName;
        }
    }

    private void kryptonButton1_Click(object sender, EventArgs e)
    {
        Close();
    }
}