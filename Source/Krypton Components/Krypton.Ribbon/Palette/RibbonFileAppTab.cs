#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Ribbon;

//// <summary>
/// Storage for the "File application tab" related properties.
/// </summary>
public class RibbonFileAppTab : Storage
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private string _fileAppTabText;
    private bool _useBackstageView;
    private Control? _backstageContent;
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
        _useBackstageView = false;
        _backstageContent = null;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => !ShouldSerializeFileAppTabText()
                                      && !UseBackstageView
                                      && (BackstageContent is null);
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

    #region UseBackstageView
    /// <summary>
    /// Gets and sets if the Office 2010 style File application tab should show a backstage view when selected.
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Determine if selecting the File application tab shows a backstage view (if provided).")]
    [DefaultValue(false)]
    public bool UseBackstageView
    {
        get => _useBackstageView;

        set
        {
            if (_useBackstageView != value)
            {
                _useBackstageView = value;
                _ribbon.PerformNeedPaint(true);
            }
        }
    }
    #endregion

    #region BackstageContent
    /// <summary>
    /// Gets and sets the control that will be hosted inside the backstage view overlay.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Control hosted inside the backstage view overlay. If null then the default app menu popup is used.")]
    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Control? BackstageContent
    {
        get => _backstageContent;

        set
        {
            if (!ReferenceEquals(_backstageContent, value))
            {
                _backstageContent = value;
                _ribbon.PerformNeedPaint(false);
            }
        }
    }
    #endregion

    #region BackstageView
    /// <summary>
    /// Gets and sets a <see cref="KryptonBackstageView"/> instance to be hosted as the backstage content.
    /// </summary>
    /// <remarks>
    /// This is a designer-friendly convenience wrapper around <see cref="BackstageContent"/>.
    /// </remarks>
    [Category(@"Backstage")]
    [Description(@"Backstage view instance to host when the File tab is selected.")]
    [DefaultValue(null)]
    [TypeConverter(typeof(ReferenceConverter))]
    public KryptonBackstageView? BackstageView
    {
        get => BackstageContent as KryptonBackstageView;
        set => BackstageContent = value;
    }
    #endregion
}
