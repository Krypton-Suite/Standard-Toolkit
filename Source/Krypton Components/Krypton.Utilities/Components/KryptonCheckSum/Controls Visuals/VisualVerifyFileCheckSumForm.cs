using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Krypton.Utilities;

public partial class VisualVerifyFileCheckSumForm : KryptonForm
{
    #region Instance Fields

    private string _fileName;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the initial file path to display when the form is shown via the static API.
    /// </summary>
    public string? InitialFilePath { get; set; }

    /// <summary>
    /// Gets or sets the initial expected hash value when the form is shown via the static API.
    /// </summary>
    public string? InitialExpectedHash { get; set; }

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="VisualVerifyFileCheckSumForm" /> class.</summary>
    public VisualVerifyFileCheckSumForm()
    {
        InitializeComponent();

        CancelButton = kbtnCancel;

        kbtnCancel.Text = KryptonManager.Strings.GeneralStrings.Cancel;

        UpdateStatus(CheckSumStatus.Ready);
    }

    #endregion

    private void VisualVerifyFileCheckSumForm_Load(object sender, EventArgs e)
    {
#if NETCOREAPP3_0_OR_GREATER
        foreach (string hash in Enum.GetNames(typeof(SafeNETAndNewerSupportedHashAlgorithms)))
        {
            kcmbHashType.Items.Add(hash);
        }

        kcmbHashType.SelectedIndex = 0;

        kbtnCancel.Enabled = true;
#else
        foreach (string hashType in Enum.GetNames(typeof(SupportedHashAlgorithims)))
        {
            kcmbHashType.Items.Add(hashType);
        }

        kcmbHashType.SelectedIndex = 0;

        kbtnCancel.Enabled = true;
#endif
        if (InitialFilePath != null && !string.IsNullOrEmpty(InitialFilePath) && File.Exists(InitialFilePath))
        {
            ktxtFilePath.Text = Path.GetFullPath(InitialFilePath);
            _fileName = InitialFilePath;
        }

        if (!string.IsNullOrEmpty(InitialExpectedHash))
        {
            ktxtVarifyCheckSum.Text = InitialExpectedHash;
        }
    }

    private void UpdateStatus(CheckSumStatus status, string? extraText = null, string? hashType = null)
    {
        switch (status)
        {
            case CheckSumStatus.Canceled:
                tslStatus.Text = @"Hash canceled ...";
                break;
            case CheckSumStatus.Cancelling:
                tslStatus.Text = $@"Cancelling hashing progress ...";
                break;
            case CheckSumStatus.Computing:
                if (extraText != null)
                {
                    if (hashType != null)
                    {
                        tslStatus.Text = $@"Computing {hashType} hash for: {extraText}";
                    }
                    else
                    {
                        tslStatus.Text = $@"Computing hash for: {extraText}";
                    }
                }
                else
                {
                    tslStatus.Text = $@"Computing hash...";
                }

                break;
            case CheckSumStatus.Ready:
                tslStatus.Text = @"Ready...";
                break;
            case CheckSumStatus.Invalid:
                break;
            case CheckSumStatus.Valid:
                break;
            case CheckSumStatus.Verifying:
                break;
        }
    }

    private void CalculateHash()
    {
        try
        {
#if NET8_0_OR_GREATER
            HashFile((SafeNETAndNewerSupportedHashAlgorithms)Enum.Parse(typeof(SafeNETAndNewerSupportedHashAlgorithms), kcmbHashType.Text));
#else
            HashFile((SupportedHashAlgorithims)Enum.Parse(typeof(SupportedHashAlgorithims), kcmbHashType.Text));
#endif
        }
        catch (Exception e)
        {
            KryptonExceptionHandler.CaptureException(e);
        }
    }

#if NET8_0_OR_GREATER
    private void HashFile(SafeNETAndNewerSupportedHashAlgorithms hashType)
    {
        switch (hashType)
        {
            case SafeNETAndNewerSupportedHashAlgorithms.MD5:
                bgwMD5.RunWorkerAsync(ktxtFilePath.Text);
                break;
            case SafeNETAndNewerSupportedHashAlgorithms.SHA1:
                bgwSHA1.RunWorkerAsync(ktxtFilePath.Text);
                break;
            case SafeNETAndNewerSupportedHashAlgorithms.SHA256:
                bgwSHA256.RunWorkerAsync(ktxtFilePath.Text);
                break;
            case SafeNETAndNewerSupportedHashAlgorithms.SHA384:
                bgwSHA384.RunWorkerAsync(ktxtFilePath.Text);
                break;
            case SafeNETAndNewerSupportedHashAlgorithms.SHA512:
                bgwSHA512.RunWorkerAsync(ktxtFilePath.Text);
                break;
        }
    }
#else
    private void HashFile(SupportedHashAlgorithims hashType)
    {
        switch (hashType)
        {
            case SupportedHashAlgorithims.MD5:
                bgwMD5.RunWorkerAsync(ktxtFilePath.Text);
                break;
            case SupportedHashAlgorithims.SHA1:
                bgwSHA1.RunWorkerAsync(ktxtFilePath.Text);
                break;
            case SupportedHashAlgorithims.SHA256:
                bgwSHA256.RunWorkerAsync(ktxtFilePath.Text);
                break;
            case SupportedHashAlgorithims.SHA384:
                bgwSHA384.RunWorkerAsync(ktxtFilePath.Text);
                break;
            case SupportedHashAlgorithims.SHA512:
                bgwSHA512.RunWorkerAsync(ktxtFilePath.Text);
                break;
            case SupportedHashAlgorithims.RIPEMD160:
                bgwRIPEMD160.RunWorkerAsync(ktxtFilePath.Text);
                break;
        }
    }
#endif

    private void kbtnCalculate_Click(object sender, EventArgs e)
    {
        try
        {
            CalculateHash();
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc);
        }
    }

    private void kbtnCancel_Click(object sender, EventArgs e)
    {
        if (bgwMD5.IsBusy || bgwSHA1.IsBusy || bgwSHA256.IsBusy || bgwSHA384.IsBusy || bgwSHA512.IsBusy ||
            bgwRIPEMD160.IsBusy)
        {
            DialogResult result = KryptonMessageBox.Show(
                "File hashing is still in progress.\nDo you want to cancel?", "Hashing in Progress",
                KryptonMessageBoxButtons.YesNo, KryptonMessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bgwMD5.CancelAsync();

                    bgwMD5.Dispose();

                    bgwSHA1.CancelAsync();

                    bgwSHA1.Dispose();

                    bgwSHA256.CancelAsync();

                    bgwSHA256.Dispose();

                    bgwSHA384.CancelAsync();

                    bgwSHA384.Dispose();

                    bgwSHA512.CancelAsync();

                    bgwSHA512.Dispose();

                    bgwRIPEMD160.CancelAsync();

                    bgwRIPEMD160.Dispose();
                }
                catch (Exception exc)
                {
                    KryptonExceptionHandler.CaptureException(exc);
                }
            }
        }
    }

    private bool VerifyCheckSum(string fileCheckSum, string importedCheckSum) =>
        fileCheckSum.Equals(importedCheckSum);

    private void ImportCheckSumFromFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                StreamReader reader = new(File.OpenRead(filePath));

                ktxtVarifyCheckSum.Text = reader.ReadToEnd();

                reader.Close();

                reader.Dispose();

                kbtnVerify.Enabled = true;
            }
        }
        catch (Exception e)
        {
            KryptonExceptionHandler.CaptureException(e);
        }
    }

    private void kbtnVerify_Click(object sender, EventArgs e)
    {
        if (VerifyCheckSum(kwlHashOutput.Text, ktxtVarifyCheckSum.Text))
        {
            ktxtVarifyCheckSum.StateCommon.Border.Color1 = Color.Green;

            ktxtVarifyCheckSum.StateCommon.Border.Color2 = Color.Green;
        }
        else
        {
            ktxtVarifyCheckSum.StateCommon.Border.Color1 = Color.Red;

            ktxtVarifyCheckSum.StateCommon.Border.Color2 = Color.Red;
        }
    }

    private void bsaBrowse_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new() { Title = @"Browse for a file:" };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            if (openFileDialog.FileName != null)
            {
                ktxtFilePath.Text = Path.GetFullPath(openFileDialog.FileName);

                _fileName = openFileDialog.FileName;
            }
        }
    }

    private void bsaVerifyBrowse_Click(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new() { Title = @"Browse for a file:" };

        if (ofd.ShowDialog() == DialogResult.OK)
        {
            if (ofd.FileName != null)
            {
                ImportCheckSumFromFile(Path.GetFullPath(ofd.FileName));
            }
        }
    }

    private void bsaReset_Click(object sender, EventArgs e) => ktxtFilePath.Text = string.Empty;

    private void ktxtFilePath_TextChanged(object sender, EventArgs e)
    {
        // if (string.IsNullOrEmpty(ktxtFilePath.Text))
        // {
        //     bsaReset.Enabled = ButtonEnabled.False;
        // }
        // else
        // {
        //     bsaReset.Enabled = ButtonEnabled.True;
        // }
    }
}
