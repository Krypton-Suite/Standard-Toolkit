#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

/// <summary>
/// Represents a form for visually verifying and calculating file checksums using various supported hash algorithms.
/// </summary>
/// <remarks>Use this form to compute and verify file hash values interactively. The form supports multiple hash
/// algorithms and allows users to specify an initial file path and expected hash for verification. It is typically
/// shown via a static API, and provides progress reporting and cancellation for long-running hash operations. The form
/// is intended for scenarios where visual feedback and user interaction are required for file checksum
/// validation.</remarks>
public partial class VisualVerifyFileCheckSumForm : KryptonForm
{
    #region Instance Fields

    private string _fileName;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the initial file path to display when the form is shown via the static API.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? InitialFilePath { get; set; }

    /// <summary>
    /// Gets or sets the initial expected hash value when the form is shown via the static API.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
                tslStatus.Text = @"Hash is invalid.";
                break;
            case CheckSumStatus.Valid:
                tslStatus.Text = @"Hash is valid.";
                break;
            case CheckSumStatus.Verifying:
                tslStatus.Text = @"Verifying hash...";
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
            kbtnVerify.Enabled = false;
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
        HelperMethods.IsValid(fileCheckSum.Trim(), importedCheckSum.Trim());

    private void ImportCheckSumFromFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                using StreamReader reader = new(File.OpenRead(filePath));

                ktxtVarifyCheckSum.Text = reader.ReadToEnd();

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
        UpdateStatus(CheckSumStatus.Verifying);

        if (VerifyCheckSum(kwlHashOutput.Text, ktxtVarifyCheckSum.Text))
        {
            ktxtVarifyCheckSum.StateCommon.Border.Color1 = Color.Green;

            ktxtVarifyCheckSum.StateCommon.Border.Color2 = Color.Green;
            UpdateStatus(CheckSumStatus.Valid);
        }
        else
        {
            ktxtVarifyCheckSum.StateCommon.Border.Color1 = Color.Red;

            ktxtVarifyCheckSum.StateCommon.Border.Color2 = Color.Red;
            UpdateStatus(CheckSumStatus.Invalid);
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
                    return buildHashString(hasher.Hash!);
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
                return buildHashString(hasher.Hash!);
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

        kpbtsiCalculationProgress.Visible = false;
        kpbtsiCalculationProgress.Value = 0;
        kwlHashOutput.Text = $@"{e.Result}";
        kbtnVerify.Enabled = !string.IsNullOrWhiteSpace(kwlHashOutput.Text) && !string.IsNullOrWhiteSpace(ktxtVarifyCheckSum.Text);
        UpdateStatus(CheckSumStatus.Ready);
    }
}
