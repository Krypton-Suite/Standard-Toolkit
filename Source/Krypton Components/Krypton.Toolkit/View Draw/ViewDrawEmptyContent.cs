#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// View element that draws empty content.
/// </summary>
public class ViewDrawEmptyContent : ViewDrawContent,
    IContentValues
{
    #region Instance Fields
    private readonly IPaletteContent? _paletteContentNormal;
    private readonly IPaletteContent? _paletteContentDisabled;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawEmptyContent class.
    /// </summary>
    /// <param name="paletteContentDisabled">Palette source for the disabled content.</param>
    /// <param name="paletteContentNormal">Palette source for the normal content.</param>
    public ViewDrawEmptyContent(IPaletteContent? paletteContentDisabled,
        IPaletteContent? paletteContentNormal)
        : base(paletteContentNormal, null, VisualOrientation.Top)
    {
        Values = this;
        _paletteContentDisabled = paletteContentDisabled;
        _paletteContentNormal = paletteContentNormal;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    // Return the class name and instance identifier
    public override string ToString() => $"ViewDrawEmptyContent:{Id}";
    #endregion

    #region Layout

    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // ReSharper disable RedundantBaseQualifier
        base.SetPalette((Enabled ? _paletteContentNormal : _paletteContentDisabled)!);

        return base.GetPreferredSize(context);
        // ReSharper restore RedundantBaseQualifier
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // ReSharper disable RedundantBaseQualifier
        base.SetPalette((Enabled ? _paletteContentNormal : _paletteContentDisabled)!);

        base.Layout(context);
        // ReSharper restore RedundantBaseQualifier
    }
    #endregion

    #region Paint

    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void RenderBefore([DisallowNull] RenderContext context) 
    {
        Debug.Assert(context != null);

        // Validate incoming reference
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // ReSharper disable RedundantBaseQualifier
        base.SetPalette((Enabled ? _paletteContentNormal : _paletteContentDisabled)!);

        base.RenderBefore(context);
        // ReSharper restore RedundantBaseQualifier
    }
    #endregion

    #region IContentValues
    /// <summary>
    /// Gets the content image.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Image value.</returns>
    public Image? GetImage(PaletteState state) => null;

    /// <summary>
    /// Gets the image color that should be transparent.
    /// </summary>
    /// <param name="state">The state for which the image is needed.</param>
    /// <returns>Color value.</returns>
    public Color GetImageTransparentColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    /// <summary>
    /// Gets the content short text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetShortText() => string.Empty;

    /// <summary>
    /// Gets the content long text.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetLongText() => string.Empty;
    #endregion
}