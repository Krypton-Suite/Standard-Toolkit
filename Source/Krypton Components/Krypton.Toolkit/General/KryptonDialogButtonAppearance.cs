#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Applies optional semantic (accept / cancel / help / neutral) colours to dialog action buttons.
/// </summary>
/// <remarks>
/// Colours are painted through palette triples (<c>StateCommon</c>, tracking / pressed, and
/// <c>OverrideDefault</c>) so default-button chrome remains intentional. Localized button text
/// remains the accessibility primary; colour is reinforcement only. Help buttons must be styled
/// via <see cref="Apply(KryptonButton, KryptonDialogButtonRole, KryptonDialogButtonColorOptions?)"/>
/// because they do not assign a <see cref="DialogResult"/>.
/// </remarks>
public static class KryptonDialogButtonAppearance
{
    #region Nested Types

    /// <summary>
    /// Resolved fill, border, and text colours for a single dialog button role.
    /// </summary>
    public readonly struct RoleColors
    {
        /// <summary>Initializes a new instance of the <see cref="RoleColors"/> struct.</summary>
        /// <param name="back">Fill colour.</param>
        /// <param name="border">Border colour.</param>
        /// <param name="text">Text colour.</param>
        public RoleColors(Color back, Color border, Color text)
        {
            Back = back;
            Border = border;
            Text = text;
        }

        /// <summary>Gets the fill colour.</summary>
        public Color Back { get; }

        /// <summary>Gets the border colour.</summary>
        public Color Border { get; }

        /// <summary>Gets the text colour.</summary>
        public Color Text { get; }
    }

    #endregion

    #region Public

    /// <summary>
    /// Resolves call-site options against the optional <see cref="KryptonManager.DialogButtonColors"/> default.
    /// </summary>
    /// <param name="callSiteOptions">Options supplied by the caller; when null, the manager default is used.</param>
    /// <returns>Effective options, or null when neither call-site nor manager options are set.</returns>
    public static KryptonDialogButtonColorOptions? GetEffectiveOptions(KryptonDialogButtonColorOptions? callSiteOptions) =>
        callSiteOptions ?? KryptonManager.DialogButtonColors;

    /// <summary>
    /// Maps a <see cref="DialogResult"/> to a semantic button role.
    /// </summary>
    /// <param name="dialogResult">The dialog result associated with the button.</param>
    /// <returns>The semantic role used to pick colours.</returns>
    public static KryptonDialogButtonRole GetRole(DialogResult dialogResult)
    {
        switch (dialogResult)
        {
            case DialogResult.OK:
            case DialogResult.Yes:
                return KryptonDialogButtonRole.Accept;

            case DialogResult.Cancel:
            case DialogResult.No:
            case DialogResult.Abort:
                return KryptonDialogButtonRole.Cancel;

            default:
#if NET8_0_OR_GREATER
                if (dialogResult == DialogResult.Continue)
                {
                    return KryptonDialogButtonRole.Accept;
                }
#endif
                return KryptonDialogButtonRole.Neutral;
        }
    }

    /// <summary>
    /// Resolves concrete colours for a role from the given options.
    /// </summary>
    /// <param name="options">Colour options (scheme plus optional overrides).</param>
    /// <param name="role">Semantic button role.</param>
    /// <param name="colors">When this method returns true, the resolved colours for the role.</param>
    /// <returns>
    /// True when colours should be applied; false when the button should keep themed chrome
    /// (inactive options, or Neutral with no override under Standard / colour-blind presets).
    /// </returns>
    public static bool TryResolveColors(KryptonDialogButtonColorOptions? options, KryptonDialogButtonRole role, out RoleColors colors)
    {
        colors = default;

        if (options == null || !options.IsActive)
        {
            return false;
        }

        RoleColors? preset = null;
        switch (options.Scheme)
        {
            case KryptonDialogButtonColorScheme.Standard:
                preset = GetStandardPreset(role);
                break;
            case KryptonDialogButtonColorScheme.Deuteranopia:
                preset = GetDeuteranopiaPreset(role);
                break;
            case KryptonDialogButtonColorScheme.Protanopia:
                preset = GetProtanopiaPreset(role);
                break;
            case KryptonDialogButtonColorScheme.HighContrast:
                preset = GetHighContrastPreset(role);
                break;
            case KryptonDialogButtonColorScheme.Custom:
            case KryptonDialogButtonColorScheme.None:
                break;
        }

        Color? backOverride;
        Color? borderOverride;
        Color? textOverride;
        switch (role)
        {
            case KryptonDialogButtonRole.Accept:
                backOverride = options.AcceptBackColor;
                borderOverride = options.AcceptBorderColor;
                textOverride = options.AcceptTextColor;
                break;
            case KryptonDialogButtonRole.Cancel:
                backOverride = options.CancelBackColor;
                borderOverride = options.CancelBorderColor;
                textOverride = options.CancelTextColor;
                break;
            case KryptonDialogButtonRole.Help:
                backOverride = options.HelpBackColor;
                borderOverride = options.HelpBorderColor;
                textOverride = options.HelpTextColor;
                break;
            default:
                backOverride = options.NeutralBackColor;
                borderOverride = options.NeutralBorderColor;
                textOverride = options.NeutralTextColor;
                break;
        }

        // No preset and no overrides for this role → leave themed.
        if (!preset.HasValue && !backOverride.HasValue && !borderOverride.HasValue && !textOverride.HasValue)
        {
            return false;
        }

        var baseColors = preset ?? new RoleColors(SystemColors.Control, SystemColors.ControlDark, SystemColors.ControlText);
        colors = new RoleColors(
            backOverride ?? baseColors.Back,
            borderOverride ?? baseColors.Border,
            textOverride ?? baseColors.Text);
        return true;
    }

    /// <summary>
    /// Applies semantic colours to a <see cref="KryptonButton"/> when options resolve colours for its <see cref="DialogResult"/>.
    /// </summary>
    /// <param name="button">The button to style.</param>
    /// <param name="dialogResult">Dialog result used to select the role.</param>
    /// <param name="options">Call-site or effective colour options; null uses <see cref="KryptonManager.DialogButtonColors"/>.</param>
    public static void Apply(KryptonButton button, DialogResult dialogResult, KryptonDialogButtonColorOptions? options) =>
        Apply(button, GetRole(dialogResult), options);

    /// <summary>
    /// Applies semantic colours to a <see cref="KryptonButton"/> for an explicit semantic role.
    /// </summary>
    /// <param name="button">The button to style.</param>
    /// <param name="role">Semantic role used to pick colours (use <see cref="KryptonDialogButtonRole.Help"/> for Help buttons).</param>
    /// <param name="options">Call-site or effective colour options; null uses <see cref="KryptonManager.DialogButtonColors"/>.</param>
    public static void Apply(KryptonButton button, KryptonDialogButtonRole role, KryptonDialogButtonColorOptions? options)
    {
        if (button == null)
        {
            throw new ArgumentNullException(nameof(button));
        }

        Apply(button.StateCommon, button.StateTracking, button.StatePressed, button.OverrideDefault, role, options);
    }

    /// <summary>
    /// Applies semantic colours to palette triples used by dialog buttons (including Utilities internal button clones).
    /// </summary>
    /// <param name="stateCommon">Common state palette.</param>
    /// <param name="stateTracking">Tracking (hover) state palette.</param>
    /// <param name="statePressed">Pressed state palette.</param>
    /// <param name="overrideDefault">Default-button override palette.</param>
    /// <param name="dialogResult">Dialog result used to select the role.</param>
    /// <param name="options">Call-site or effective colour options; null uses <see cref="KryptonManager.DialogButtonColors"/>.</param>
    public static void Apply(
        PaletteTripleRedirect stateCommon,
        PaletteTriple stateTracking,
        PaletteTriple statePressed,
        PaletteTripleRedirect overrideDefault,
        DialogResult dialogResult,
        KryptonDialogButtonColorOptions? options) =>
        Apply(stateCommon, stateTracking, statePressed, overrideDefault, GetRole(dialogResult), options);

    /// <summary>
    /// Applies semantic colours to palette triples for an explicit semantic role.
    /// </summary>
    /// <param name="stateCommon">Common state palette.</param>
    /// <param name="stateTracking">Tracking (hover) state palette.</param>
    /// <param name="statePressed">Pressed state palette.</param>
    /// <param name="overrideDefault">Default-button override palette.</param>
    /// <param name="role">Semantic role used to pick colours.</param>
    /// <param name="options">Call-site or effective colour options; null uses <see cref="KryptonManager.DialogButtonColors"/>.</param>
    public static void Apply(
        PaletteTripleRedirect stateCommon,
        PaletteTriple stateTracking,
        PaletteTriple statePressed,
        PaletteTripleRedirect overrideDefault,
        KryptonDialogButtonRole role,
        KryptonDialogButtonColorOptions? options)
    {
        if (stateCommon == null)
        {
            throw new ArgumentNullException(nameof(stateCommon));
        }

        if (stateTracking == null)
        {
            throw new ArgumentNullException(nameof(stateTracking));
        }

        if (statePressed == null)
        {
            throw new ArgumentNullException(nameof(statePressed));
        }

        if (overrideDefault == null)
        {
            throw new ArgumentNullException(nameof(overrideDefault));
        }

        var effective = GetEffectiveOptions(options);
        if (!TryResolveColors(effective, role, out var colors))
        {
            return;
        }

        var tracking = Shade(colors.Back, 1.08f);
        var pressed = Shade(colors.Back, 0.88f);

        ApplySurface(stateCommon, colors.Back, colors.Border, colors.Text);
        ApplySurface(stateTracking, tracking, colors.Border, colors.Text);
        ApplySurface(statePressed, pressed, colors.Border, colors.Text);
        ApplySurface(overrideDefault, colors.Back, colors.Border, colors.Text);
    }

    #endregion

    #region Implementation

    private static RoleColors? GetStandardPreset(KryptonDialogButtonRole role)
    {
        // macOS system green / red / blue; Neutral stays themed.
        switch (role)
        {
            case KryptonDialogButtonRole.Accept:
                return new RoleColors(Color.FromArgb(52, 199, 89), Color.FromArgb(40, 170, 72), Color.White);
            case KryptonDialogButtonRole.Cancel:
                return new RoleColors(Color.FromArgb(255, 59, 48), Color.FromArgb(220, 45, 35), Color.White);
            case KryptonDialogButtonRole.Help:
                return new RoleColors(Color.FromArgb(0, 122, 255), Color.FromArgb(0, 100, 210), Color.White);
            default:
                return null;
        }
    }

    private static RoleColors? GetDeuteranopiaPreset(KryptonDialogButtonRole role)
    {
        // Blue accept / orange cancel / purple help (not red–green).
        switch (role)
        {
            case KryptonDialogButtonRole.Accept:
                return new RoleColors(Color.FromArgb(0, 114, 178), Color.FromArgb(0, 90, 140), Color.White);
            case KryptonDialogButtonRole.Cancel:
                return new RoleColors(Color.FromArgb(230, 159, 0), Color.FromArgb(180, 120, 0), Color.Black);
            case KryptonDialogButtonRole.Help:
                return new RoleColors(Color.FromArgb(123, 44, 191), Color.FromArgb(90, 30, 150), Color.White);
            default:
                return null;
        }
    }

    private static RoleColors? GetProtanopiaPreset(KryptonDialogButtonRole role)
    {
        // Blue accept / brown cancel / magenta help.
        switch (role)
        {
            case KryptonDialogButtonRole.Accept:
                return new RoleColors(Color.FromArgb(0, 90, 181), Color.FromArgb(0, 70, 140), Color.White);
            case KryptonDialogButtonRole.Cancel:
                return new RoleColors(Color.FromArgb(153, 79, 0), Color.FromArgb(120, 60, 0), Color.White);
            case KryptonDialogButtonRole.Help:
                return new RoleColors(Color.FromArgb(204, 121, 167), Color.FromArgb(160, 90, 130), Color.Black);
            default:
                return null;
        }
    }

    private static RoleColors? GetHighContrastPreset(KryptonDialogButtonRole role)
    {
        switch (role)
        {
            case KryptonDialogButtonRole.Accept:
                return new RoleColors(Color.FromArgb(0, 255, 0), Color.Black, Color.Black);
            case KryptonDialogButtonRole.Cancel:
                return new RoleColors(Color.FromArgb(255, 255, 0), Color.Black, Color.Black);
            case KryptonDialogButtonRole.Help:
                return new RoleColors(Color.FromArgb(0, 255, 255), Color.Black, Color.Black);
            case KryptonDialogButtonRole.Neutral:
                return new RoleColors(Color.White, Color.Black, Color.Black);
            default:
                return null;
        }
    }

    private static void ApplySurface(PaletteTripleRedirect palette, Color back, Color border, Color text)
    {
        palette.Back.Color1 = back;
        palette.Back.Color2 = back;
        palette.Back.ColorStyle = PaletteColorStyle.Solid;
        palette.Border.Color1 = border;
        palette.Border.ColorStyle = PaletteColorStyle.Solid;
        palette.Border.DrawBorders = PaletteDrawBorders.All;
        palette.Border.Width = 1;
        palette.Content.ShortText.Color1 = text;
    }

    private static void ApplySurface(PaletteTriple palette, Color back, Color border, Color text)
    {
        palette.Back.Color1 = back;
        palette.Back.Color2 = back;
        palette.Back.ColorStyle = PaletteColorStyle.Solid;
        palette.Border.Color1 = border;
        palette.Border.ColorStyle = PaletteColorStyle.Solid;
        palette.Border.DrawBorders = PaletteDrawBorders.All;
        palette.Border.Width = 1;
        palette.Content.ShortText.Color1 = text;
    }

    private static Color Shade(Color color, float factor)
    {
        int r = ClampByte((int)Math.Round(color.R * factor));
        int g = ClampByte((int)Math.Round(color.G * factor));
        int b = ClampByte((int)Math.Round(color.B * factor));
        return Color.FromArgb(color.A, r, g, b);
    }

    private static int ClampByte(int value)
    {
        if (value < 0)
        {
            return 0;
        }

        if (value > 255)
        {
            return 255;
        }

        return value;
    }

    #endregion
}