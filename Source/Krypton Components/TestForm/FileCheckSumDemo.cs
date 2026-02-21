#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Utilities;

namespace TestForm;

public partial class FileCheckSumDemo : KryptonForm
{
    public FileCheckSumDemo()
    {
        InitializeComponent();
    }

    private void kbtnComputeFileCheckSum_Click(object sender, EventArgs e)
    {
        KryptonComputeFileCheckSum.Show(this);
    }

    private void kbtnVerifyFileCheckSum_Click(object sender, EventArgs e)
    {
        KryptonVerifyFileCheckSum.Show(this);
    }
}
