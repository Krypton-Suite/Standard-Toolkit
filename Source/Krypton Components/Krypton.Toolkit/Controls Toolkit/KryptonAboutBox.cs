#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>The public interface to the <see cref="VisualAboutBoxForm"/> class.</summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public static class KryptonAboutBox
{
    #region Public

    /// <summary>Shows a new <see cref="VisualAboutBoxForm"/>.</summary>
    /// <param name="aboutBoxData">The data to pass through.</param>
    /// <returns>A new <see cref="VisualAboutBoxForm"/> with the specified data.</returns>
    public static DialogResult Show(KryptonAboutBoxData aboutBoxData)
        => ShowCore(aboutBoxData);

    /// <summary>Shows a new <see cref="VisualAboutBoxForm"/></summary>
    /// <param name="aboutBoxData">The about box data.</param>
    /// <param name="aboutToolkitData">The about toolkit data.</param>
    /// <returns>A new <see cref="VisualAboutBoxForm"/> with the specified data.</returns>
    public static DialogResult Show(KryptonAboutBoxData aboutBoxData, KryptonAboutToolkitData aboutToolkitData) =>
        ShowCore(aboutBoxData, aboutToolkitData);

    #endregion

    #region Implementation

    private static DialogResult ShowCore(KryptonAboutBoxData aboutBoxData)
    {
        using var kab = new VisualAboutBoxForm(aboutBoxData);

        return kab.ShowDialog();
    }

    private static DialogResult ShowCore(KryptonAboutBoxData aboutBoxData, KryptonAboutToolkitData aboutToolkitData)
    {
        using var kab = new VisualAboutBoxForm(aboutBoxData, aboutToolkitData);

        return kab.ShowDialog();
    }

    #endregion
}