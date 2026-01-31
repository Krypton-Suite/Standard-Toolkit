#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonImageStorage : Storage
{
    #region Static Properties

    public static GenericImageStorage GenericImages { get; } = new GenericImageStorage();

    public static ToolBarImageStorage ToolbarImageStorage { get; } = new ToolBarImageStorage();

    #endregion

    #region Public

    /// <summary>Gets the toolkit images.</summary>
    [Category(@"Visuals")]
    [Description(@"A collection of images that are used throughout the toolkit.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public GenericImageStorage GenericToolkitImages => GenericImages;

    private bool ShouldSerializeGenericToolkitImages() => !GenericImages.IsDefault;
    private void ResetGenericToolkitImages() => GenericImages.Reset();

    /// <summary>Gets the toolbar images.</summary>
    [Category(@"Visuals")]
    [Description(@"A collection of images that are used in toolbars.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public ToolBarImageStorage ToolbarImages => ToolbarImageStorage;

    private bool ShouldSerializeToolbarImages() => !ToolbarImageStorage.IsDefault;
    private void ResetToolbarImages() => ToolbarImageStorage.Reset();

    #endregion

    #region Identity

    public KryptonImageStorage()
    {
            
    }

    #endregion

    #region IsDefault

    /// <summary>Gets a value indicating whether this instance is default.</summary>
    /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => !(ShouldSerializeGenericToolkitImages() ||
                                        ShouldSerializeGenericToolkitImages());

    #endregion

    #region Implementation

    public void Reset()
    {
        ResetGenericToolkitImages();

        ResetToolbarImages();
    }

    #endregion
}