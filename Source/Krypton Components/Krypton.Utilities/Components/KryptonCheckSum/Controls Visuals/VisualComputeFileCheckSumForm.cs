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

internal partial class VisualComputeFileCheckSumForm : KryptonForm
{
    #region Instance Fields

    private string _fileName;

    private SupportedHashAlgorithims? _hashAlgorithm;

    private SafeNETAndNewerSupportedHashAlgorithms? _safeNETAndNewerHashAlgorithm;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the initial file path to display when the form is shown via the static API.
    /// </summary>
    public string? InitialFilePath { get; set; }

    public SupportedHashAlgorithims HashAlgorithm { get; set; }

    public SafeNETAndNewerSupportedHashAlgorithms SafeNETAndNewerHashAlgorithm { get; set; }

    #endregion

    #region Identity

    public VisualComputeFileCheckSumForm(string? initialFilePath , SupportedHashAlgorithims? hashAlgorithm, SafeNETAndNewerSupportedHashAlgorithms? safeNETAndNewerHashAlgorithm)
    {
        InitializeComponent();

        CancelButton = kbtnCancel;

        kbtnCancel.Text = KryptonManager.Strings.GeneralStrings.Cancel;

        UpdateStatus(CheckSumStatus.Ready);

        InitialFilePath = initialFilePath;
        _fileName = initialFilePath ?? string.Empty;

        _hashAlgorithm = hashAlgorithm ?? SupportedHashAlgorithims.SHA256;
        _safeNETAndNewerHashAlgorithm = safeNETAndNewerHashAlgorithm ?? SafeNETAndNewerSupportedHashAlgorithms.SHA256;
    }

    #endregion

    #region Implementation

    private void KryptonComputeFileCheckSum_Load(object sender, EventArgs e)
    {
#if NETCOREAPP3_0_OR_GREATER
        foreach (string hash in Enum.GetNames(typeof(SafeNETAndNewerSupportedHashAlgorithms)))
        {
            kcmbHashType.Items.Add(hash);
        }

        kcmbHashType.SelectedIndex = 0;

        kbtnCalculate.Enabled = true;

        if (_safeNETAndNewerHashAlgorithm != null)
        {
            kcmbHashType.SelectedItem = _safeNETAndNewerHashAlgorithm.ToString();
        }

#else
        foreach (var hashType in Enum.GetNames(typeof(SupportedHashAlgorithims)))
        {
            kcmbHashType.Items.Add(hashType);
        }

        kcmbHashType.SelectedIndex = 0;

        kbtnCalculate.Enabled = true;

        if (_hashAlgorithm != null)
        {
            kcmbHashType.SelectedItem = _hashAlgorithm.ToString();
        }
#endif
        if (!string.IsNullOrEmpty(InitialFilePath) && File.Exists(InitialFilePath))
        {
            if (InitialFilePath != null)
            {
                ktxtFilePath.Text = Path.GetFullPath(InitialFilePath);
                _fileName = InitialFilePath;
            }
        }
    }

    private void kchkToggleCasing_CheckedChanged(object sender, EventArgs e)
    {
        string tempHashString = kwlHashOutput.Text;

        if (kchkToggleCasing.Checked)
        {
            tempHashString = tempHashString.ToUpperInvariant();

            kwlHashOutput.Text = tempHashString;
        }
        else
        {
            tempHashString = tempHashString.ToLowerInvariant();

            kwlHashOutput.Text = tempHashString;
        }
    }

    private void bsaBrowse_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog() { Title = @"Browse for a file:" };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            if (openFileDialog.FileName != null)
            {
                ktxtFilePath.Text = Path.GetFullPath(openFileDialog.FileName);

                _fileName = openFileDialog.FileName;
            }
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
            case CheckSumStatus.Saving:
                break;
        }
    }

    private void kbtnSaveToFile_Click(object sender, EventArgs e) => SaveHashFile();

    private void SaveHashFile()
    {
        SaveFileDialog sfd = new()
        {
            Title = @"Save hash to:",
            FileName = $@"Hash for {_fileName}",
            Filter = @"Text Files|*.txt"
        };

        if (sfd.ShowDialog() == DialogResult.OK)
        {
            if (!string.IsNullOrEmpty(kwlHashOutput.Text))
            {
                File.WriteAllText(Path.GetFullPath(sfd.FileName), kwlHashOutput.Text);
            }
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
        if (bgwMD5.IsBusy || bgwSHA1.IsBusy || bgwSHA256.IsBusy || bgwSHA384.IsBusy || bgwSHA512.IsBusy || bgwRIPEMD160.IsBusy)
        {
            DialogResult result = KryptonMessageBox.Show("File hashing is still in progress.\nDo you want to cancel?", "Hashing in Progress", KryptonMessageBoxButtons.YesNo, KryptonMessageBoxIcon.Question);

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

    #endregion

    private static string? ComputeFileHash(string? filePath, Func<HashAlgorithm> createHasher, Func<byte[], string> buildHashString, Action<int> reportProgress)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            return null;
        }

        using (Stream file = File.OpenRead(filePath))
        {
            long size = file.Length;
            using (HashAlgorithm hasher = createHasher())
            {
                if (size == 0)
                {
                    hasher.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
                    reportProgress(100);
                    return buildHashString(hasher.Hash);
                }

                byte[] buffer;
                int bytesRead;
                long totalBytesRead = 0;
                do
                {
                    buffer = new byte[4096];
                    bytesRead = file.Read(buffer, 0, buffer.Length);
                    totalBytesRead += bytesRead;
                    hasher.TransformBlock(buffer, 0, bytesRead, null, 0);
                    reportProgress((int)((double)totalBytesRead / size * 100));
                } while (bytesRead != 0);

                hasher.TransformFinalBlock(buffer, 0, 0);
                return buildHashString(hasher.Hash);
            }
        }
    }

    private void bgwMD5_DoWork(object sender, DoWorkEventArgs e) => e.Result = ComputeFileHash(e.Argument?.ToString(), MD5.Create, HashingHelpers.BuildMD5HashString, p => bgwMD5.ReportProgress(p));

    private void bgwSHA1_DoWork(object sender, DoWorkEventArgs e) => e.Result = ComputeFileHash(e.Argument?.ToString(), SHA1.Create, HashingHelpers.BuildSHA1HashString, p => bgwSHA1.ReportProgress(p));

    private void bgwSHA256_DoWork(object sender, DoWorkEventArgs e) => e.Result = ComputeFileHash(e.Argument?.ToString(), SHA256.Create, HashingHelpers.BuildSHA256HashString, p => bgwSHA256.ReportProgress(p));

    private void bgwSHA384_DoWork(object sender, DoWorkEventArgs e) => e.Result = ComputeFileHash(e.Argument?.ToString(), SHA384.Create, HashingHelpers.BuildSHA384HashString, p => bgwSHA384.ReportProgress(p));

    private void bgwSHA512_DoWork(object sender, DoWorkEventArgs e) => e.Result = ComputeFileHash(e.Argument?.ToString(), SHA512.Create, HashingHelpers.BuildSHA512HashString, p => bgwSHA512.ReportProgress(p));

    private void bgwRIPEMD160_DoWork(object sender, DoWorkEventArgs e)
    {
#if !NET8_0_OR_GREATER
        e.Result = ComputeFileHash(e.Argument?.ToString(), RIPEMD160Managed.Create, HashingHelpers.BuildRIPEMD160HashString, p => bgwRIPEMD160.ReportProgress(p));
#endif
    }

    private void Calculation_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        if (IsDisposed || Disposing)
        {
            return;
        }

        // ToDo: Redo when https://github.com/Krypton-Suite/Standard-Toolkit/issues/2890 is completed.
        /*if (_useAPICodePackFeatures && TaskbarManager.IsPlatformSupported)
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);

            TaskbarManager.Instance.SetProgressValue(e.ProgressPercentage, null);
        }*/

        kpbtsiCalculationProgress.Visible = true;

        kpbtsiCalculationProgress.Value = e.ProgressPercentage;

        kwlHashOutput.Text = @"Please wait ...";

        UpdateStatus(CheckSumStatus.Computing);
    }

    private void Calculation_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        if (IsDisposed || Disposing)
        {
            return;
        }

        // ToDo: Redo when https://github.com/Krypton-Suite/Standard-Toolkit/issues/2890 is completed.
        /*
        if (_useAPICodePackFeatures && TaskbarManager.IsPlatformSupported)
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);

            TaskbarManager.Instance.SetProgressValue(0, null);
        }*/

        kpbtsiCalculationProgress.Visible = false;

        kpbtsiCalculationProgress.Value = 0;

        kwlHashOutput.Text = $@"{e.Result}";

        kbtnSaveToFile.Enabled = true;

        UpdateStatus(CheckSumStatus.Ready);
    }

    private void bsaReset_Click(object sender, EventArgs e) => ktxtFilePath.Text = string.Empty;
}
