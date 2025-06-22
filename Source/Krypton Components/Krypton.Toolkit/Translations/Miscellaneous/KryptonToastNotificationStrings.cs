#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Exposes a custom set of strings that are used within the Krypton Toolkit, and are localisable.</summary>
/// <seealso cref="GlobalId" />
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonToastNotificationStrings : GlobalId
{
    #region Static Strings

    private const string DEFAULT_DO_NOT_SHOW_AGAIN = @"Do &not show again";
    private const string DEFAULT_DISMISS = @"&Dismiss";

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonToastNotificationStrings" /> class.</summary>
    public KryptonToastNotificationStrings()
    {
        Reset();
    }

    /// <summary>Converts to string.</summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region IsDefault

    [Browsable(false)]
    public bool IsDefault => DoNotShowAgain.Equals(DEFAULT_DO_NOT_SHOW_AGAIN) &&
                             Dismiss.Equals(DEFAULT_DISMISS);

    #endregion

    #region Public

    /// <summary>Gets or sets the dismiss string used for custom situations.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Dismiss string used for custom situations.")]
    [DefaultValue(DEFAULT_DO_NOT_SHOW_AGAIN)]
    public string DoNotShowAgain { get; set; }

    /// <summary>Gets or sets the dismiss string used for custom situations.</summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Dismiss string used for custom situations.")]
    [DefaultValue(DEFAULT_DISMISS)]
    public string Dismiss { get; set; }

    #endregion

    #region Implementation

    public void Reset()
    {
        Dismiss = DEFAULT_DISMISS;
        DoNotShowAgain = DEFAULT_DO_NOT_SHOW_AGAIN;
    }

    #endregion
}