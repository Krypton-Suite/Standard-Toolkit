#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Storage for the "File application tab" related properties.
/// </summary>
public class RibbonFileAppTab : Storage
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private string _fileAppTabText;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the RibbonFileAppButton class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon instance.</param>
    public RibbonFileAppTab([DisallowNull] KryptonRibbon ribbon)
    {
        Debug.Assert(ribbon != null);

        _ribbon = ribbon!;

        ResetFileAppTabText();
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => !ShouldSerializeFileAppTabText();
    #endregion

    #region FileAppTabText
    /// <summary>
    /// Gets and sets the text used for drawing an Office 2010 style application button.
    /// </summary>
    [Bindable(true)]
    [Category(@"Appearance")]
    [Description(@"Text used for drawing an Office 2010 style application button.")]
    [KryptonDefaultColor]
    [DefaultValue(nameof(File))]
    [Localizable(true)]
    public string FileAppTabText
    {
        get => _fileAppTabText;

        set
        {
            if (_fileAppTabText != value)
            {
                _fileAppTabText = value;
                _ribbon.PerformNeedPaint(true);
            }
        }
    }
    private void ResetFileAppTabText() => _fileAppTabText = KryptonManager.Strings.RibbonStrings.AppButtonText;
    private bool ShouldSerializeFileAppTabText() => _fileAppTabText != KryptonManager.Strings.RibbonStrings.AppButtonText;
    #endregion
}