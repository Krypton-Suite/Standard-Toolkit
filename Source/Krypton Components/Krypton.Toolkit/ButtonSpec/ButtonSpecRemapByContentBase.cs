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
/// Redirect requests for image/text colors to remap.
/// </summary>
public abstract class ButtonSpecRemapByContentBase : PaletteRedirect
{
    #region Instance Fields
    private readonly ButtonSpec _buttonSpec;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ButtonSpecRemapByContentBase class.
    /// </summary>
    /// <param name="target">Initial palette target for redirection.</param>
    /// <param name="buttonSpec">Reference to button specification.</param>
    protected ButtonSpecRemapByContentBase(PaletteBase target,
        [DisallowNull] ButtonSpec buttonSpec)
        : base(target)
    {
        Debug.Assert(buttonSpec != null);
        _buttonSpec = buttonSpec!;
    }
    #endregion

    #region PaletteContent
    /// <summary>
    /// Gets the palette content to use for remapping.
    /// </summary>
    public abstract IPaletteContent? PaletteContent { get; }
    #endregion

    #region PaletteState
    /// <summary>
    /// Gets the state of the remapping area
    /// </summary>
    public abstract PaletteState PaletteState { get; }
    #endregion

    #region GetContentImageColorMap
    /// <summary>
    /// Gets the image color to remap into another color.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentImageColorMap(PaletteContentStyle style, PaletteState state)
    {
        // If allowed to override then get the map color
        Color mapColor = OverrideImageColor(state);

        // If a map color provided then return is
        return (mapColor != GlobalStaticValues.EMPTY_COLOR) && (PaletteContent != null) ? mapColor : base.GetContentImageColorMap(style, state);
    }
    #endregion

    #region GetContentImageColorTo
    /// <summary>
    /// Gets the color to use in place of the image map color.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentImageColorTo(PaletteContentStyle style, PaletteState state)
    {
        // If allowed to override then get the map color
        Color mapColor = OverrideImageColor(state);

        // If mapping occurring then return the target remap color
        if ((mapColor != GlobalStaticValues.EMPTY_COLOR) && (PaletteContent != null))
        {
            PaletteState getState = PaletteState;

            // Honor the button disabled state
            if (state == PaletteState.Disabled)
            {
                getState = PaletteState.Disabled;
            }

            return PaletteContent.GetContentShortTextColor1(getState);
        }
        else
        {
            return base.GetContentImageColorTo(style, state);
        }
    }
    #endregion

    #region GetContentShortTextColor1
    /// <summary>
    /// Gets the first back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state) =>
        // Do we need to override the text color
        OverrideTextColor(state) && (PaletteContent != null)
            ? PaletteContent.GetContentShortTextColor1(state)
            : base.GetContentShortTextColor1(style, state);

    #endregion

    #region GetContentLongTextColor1
    /// <summary>
    /// Gets the first back color for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state) =>
        // Do we need to override the text color
        OverrideTextColor(state) && (PaletteContent != null)
            ? PaletteContent.GetContentShortTextColor1(state)
            : base.GetContentLongTextColor1(style, state);

    #endregion

    #region Implementation
    private Color OverrideImageColor(PaletteState state)
    {
        // We only intercept if we have a content to use for redirection
        if (PaletteContent != null)
        {
            // We only override the normal/disabled states
            if (state is PaletteState.Normal or PaletteState.Disabled)
            {
                // ReSharper disable RedundantBaseQualifier
                // Get the color map from the button spec
                Color mapColor = _buttonSpec.GetColorMap(base.Target);
                // ReSharper restore RedundantBaseQualifier

                // If we are supposed to remap a color
                if (mapColor != GlobalStaticValues.EMPTY_COLOR)
                {
                    // ReSharper disable RedundantBaseQualifier
                    // Get the button style requested
                    ButtonStyle buttonStyle = _buttonSpec.GetStyle(base.Target);
                    // ReSharper restore RedundantBaseQualifier

                    // Only for ButtonSpec do we use the palette value
                    if (buttonStyle == ButtonStyle.ButtonSpec)
                    {
                        return mapColor;
                    }
                }
            }
        }

        return GlobalStaticValues.EMPTY_COLOR;
    }

    private bool OverrideTextColor(PaletteState state)
    {
        // We are only interested in overriding the disabled or normal colors
        if (state == PaletteState.Normal)
        {
            // ReSharper disable RedundantBaseQualifier
            // Get the button style requested
            ButtonStyle buttonStyle = _buttonSpec.GetStyle(base.Target);
            // ReSharper restore RedundantBaseQualifier

            // If we are checking for button styles of ButtonSpec only, then do so
            if (buttonStyle == ButtonStyle.ButtonSpec)
            {
                return true;
            }
        }

        return false;
    }
    #endregion
}