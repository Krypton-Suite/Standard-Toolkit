#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>A structure that contains basic information for <see cref="VisualInputBoxForm"/>.</summary>
public struct KryptonInputBoxData
{
    #region Public


    /// <summary>Gets or sets the owner of the <see cref="VisualInputBoxForm"/>.</summary>
    /// <value>The owner of the <see cref="VisualInputBoxForm"/>.</value>
    public IWin32Window? Owner { get; set; }

    /// <summary>Gets or sets the prompt text.</summary>
    /// <value>The prompt text.</value>
    public string Prompt { get; set; }

    /// <summary>Gets or sets the caption.</summary>
    /// <value>The caption.</value>
    public string Caption { get; set; }

    /// <summary>Gets or sets the default response.</summary>
    /// <value>The default response.</value>
    public string DefaultResponse { get; set; }

    /// <summary>Gets or sets the cue text.</summary>
    /// <value>The cue text.</value>
    public string CueText { get; set; }

    /// <summary>Gets or sets the color of the cue text.</summary>
    /// <value>The color of the cue text.</value>
    public Color? CueColor { get; set; }

    /// <summary>Gets or sets the cue typeface.</summary>
    /// <value>The cue typeface.</value>
    public Font? CueTypeface { get; set; }

    /// <summary>Gets or sets the use password option.</summary>
    /// <value>The use password option.</value>
    public bool? UsePasswordOption { get; set; }

    /// <summary>Gets or sets the use RTL layout of the <see cref="KryptonInputBox"/> UI.</summary>
    /// <value>The use RTL layout in an <see cref="KryptonInputBox"/>.</value>
    public KryptonUseRTLLayout UseRTLLayout { get; set; }

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonInputBoxData" /> struct.</summary>
    public KryptonInputBoxData()
    {
        UseRTLLayout = KryptonUseRTLLayout.No;
    }

    #endregion
}