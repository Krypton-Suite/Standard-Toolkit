#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

// ReSharper disable MemberCanBePrivate.Global

namespace Krypton.Toolkit;

/// <summary>
/// Combines button functionality with the styling features of the Krypton Toolkit.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonButton), "ToolboxBitmaps.KryptonButton.bmp")]
[DefaultEvent(nameof(Click))]
[DefaultProperty(nameof(Text))]
[DesignerCategory(@"code")]
[Description(@"Raises an event when the user clicks it.")]
[Designer("Krypton.Toolkit.KryptonButtonDesigner, " + KryptonWinFormsDesignerSdk.AssemblyName)]
public class KryptonButton : KryptonDropButton
{
    #region Instance Fields
    private const string RunAsVerb = "runas";

    private EventHandler? _elevatedClick;
    private string _elevatedProcessPath = string.Empty;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonButton class.
    /// </summary>
    public KryptonButton()
    {
        // Create the view button instance
        _drawButton.DropDown = false;
        _drawButton.Splitter = false;

        // Create a button controller to handle button style behaviour
        _buttonController.BecomesFixed = false;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the visual orientation of the control
    /// </summary>
    [Browsable(true)]
    [Localizable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(VisualOrientation.Top)]
    public virtual VisualOrientation Orientation
    {
        // Backward compatible fix.
        get => ButtonOrientation;
        set => ButtonOrientation = value;
    }

    /// <summary>Gets or sets a value indicating whether the split/drop-down chevron is shown.</summary>
    /// <value><c>true</c> if the chevron is shown; otherwise, <c>false</c>.</value>
    /// <remarks>
    /// Prefer <see cref="KryptonSplitButton"/> when the control is always a split button.
    /// When enabled, the drop-down is aligned to the chevron (same as <see cref="KryptonDropButton"/>).
    /// </remarks>
    [Category(@"Visuals")]
    [DefaultValue(false)]
    [Description(@"Displays the split/dropdown option.")]
    public bool ShowSplitOption
    {
        // Backward compatible fix.
        get => base.Splitter;
        set
        {
            _drawButton.DropDown = value;
            base.Splitter = value;
        }
    }

    [Browsable(false)]
    [Localizable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    public new bool Splitter
    {
        get => base.Splitter;
        set => base.Splitter = value;
    }

    /// <summary>
    /// Occurs when the button is clicked and the action is expected to require elevation.
    /// </summary>
    /// <remarks>
    /// <para>
    /// While at least one handler is attached, the UAC shield is shown and cannot be turned off.
    /// A content image stays in <see cref="ButtonValues.Image"/> and the shield is drawn on
    /// <see cref="ButtonValues.OverlayImage"/> (bottom-right, half size). With no content image the shield
    /// stays in <see cref="ButtonValues.Image"/>. <see cref="ButtonValues.OverlayImage"/> cannot replace that shield.
    /// Clearing <see cref="ButtonValues.Image"/> moves the shield back onto the image so it stays visible.
    /// </para>
    /// <para>
    /// When the process is not already an administrator, the button starts <see cref="ElevatedProcessPath"/>
    /// with <see cref="ProcessStartInfo.UseShellExecute"/> and the RunAs verb (<c>runas</c>).
    /// An empty path uses <see cref="Application.ExecutablePath"/>.
    /// The handler does not need to start that process.
    /// </para>
    /// <para>
    /// <see cref="Control.Click"/> still follows this event, including dialog result and <see cref="KryptonCommand"/>,
    /// when the process is already elevated or the RunAs launch succeeds.
    /// Dismissing the UAC prompt raises neither event.
    /// Removing the last handler clears the lock. The shield image remains until
    /// <see cref="ButtonValues.UseAsUACElevationButton"/> is set to <c>false</c>.
    /// </para>
    /// </remarks>
    [Category(@"Action")]
    [Description(@"Occurs when the button is clicked. While a handler is attached, the UAC shield stays visible and overlays a content image.")]
    public event EventHandler? ElevatedClick
    {
        add
        {
            if (value == null)
            {
                return;
            }

            bool firstSubscriber = _elevatedClick == null;
            _elevatedClick += value;
            if (firstSubscriber)
            {
                Values.LockUacShieldForElevation();
            }
        }
        remove
        {
            _elevatedClick -= value;
            if (_elevatedClick == null)
            {
                Values.UnlockUacShieldForElevation();
            }
        }
    }

    /// <summary>
    /// Gets or sets the executable started with the RunAs verb when <see cref="ElevatedClick"/> runs
    /// and the process is not already an administrator.
    /// </summary>
    /// <value>The executable path. An empty string uses <see cref="Application.ExecutablePath"/>.</value>
    /// <remarks>
    /// <see cref="ProcessStartInfo.UseShellExecute"/> is set so the <c>runas</c> verb is honored on modern target frameworks.
    /// </remarks>
    [Category(@"Behavior")]
    [DefaultValue("")]
    [Description(@"Executable started with RunAs when ElevatedClick runs and the process is not already an administrator. Empty uses the current application.")]
    public string ElevatedProcessPath
    {
        get => _elevatedProcessPath;

        set => _elevatedProcessPath = value ?? string.Empty;
    }

    private bool ShouldSerializeElevatedProcessPath() => !string.IsNullOrEmpty(_elevatedProcessPath);

    private void ResetElevatedProcessPath() => _elevatedProcessPath = string.Empty;
    #endregion

    #region Protected
    /// <summary>
    /// Raises the <see cref="ElevatedClick"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    protected virtual void OnElevatedClick(EventArgs e) => _elevatedClick?.Invoke(this, e);
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Starts the RunAs process when needed, then raises <see cref="ElevatedClick"/> and the standard <see cref="Control.Click"/> path.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    /// <remarks>
    /// A <see cref="Win32Exception"/> from the RunAs launch, including the user dismissing the UAC prompt (native error 1223),
    /// skips both events.
    /// </remarks>
    protected override void OnClick(EventArgs e)
    {
        if (_elevatedClick != null)
        {
            if (!TryStartRunAsProcess())
            {
                return;
            }

            OnElevatedClick(e);
        }

        base.OnClick(e);
    }

    /// <summary>
    /// Starts <see cref="ElevatedProcessPath"/> with the RunAs verb when the process is not already an administrator.
    /// </summary>
    /// <returns>
    /// <c>true</c> when <see cref="ElevatedClick"/> and <see cref="Control.Click"/> should run;
    /// <c>false</c> when the RunAs launch failed or the user dismissed the prompt.
    /// </returns>
    private bool TryStartRunAsProcess()
    {
        if (IsCurrentProcessAdministrator())
        {
            return true;
        }

        string path = string.IsNullOrWhiteSpace(_elevatedProcessPath)
            ? Application.ExecutablePath
            : _elevatedProcessPath;

        var startInfo = new ProcessStartInfo
        {
            FileName = path,
            UseShellExecute = true,
            Verb = RunAsVerb
        };

        try
        {
            Process.Start(startInfo);
            return true;
        }
        catch (Win32Exception)
        {
            // Includes the user dismissing the UAC prompt (native error 1223).
            return false;
        }
    }

    private static bool IsCurrentProcessAdministrator()
    {
        using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
        {
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }

    /// <inheritdoc />
    protected override bool OpensDropDownOnNonSplitterClick => false;

    /// <inheritdoc />
    protected override bool SuppressSystemContextMenu => false;
    #endregion
}
