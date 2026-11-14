#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// A standard tool strip menu item control with UAC shield.
/// Modified from the AeroSuite project.
/// </summary>
/// <remarks>
/// The shield is extracted from the system with LoadImage if possible. Otherwise the shield will be enabled by sending the BCM_SETSHIELD Message to the control.
/// If the operating system is not Windows Vista or higher, no shield will be displayed as there's no such thing as UAC on the target system -> the shield is obsolete.
/// </remarks>
[DisplayName("ToolStrip UAC Shield Menu Item")]
[ToolboxBitmap(typeof(ToolStripMenuItem)), ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
public partial class KryptonToolStripMenuItemUACShield : ToolStripMenuItem
{
    #region Variables
    private readonly UacShieldMenuItemValues _values;

    private static bool? _isSystemAbleToLoadShield;
    #endregion

    #region Properties

    /// <summary>
    /// Gets the expandable configuration values for designer and runtime use.
    /// </summary>
    [Category("Code")]
    [Description("Elevation and process name settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public UacShieldMenuItemValues UacValues => _values;

    private bool ShouldSerializeUacValues() => !_values.IsDefault;

    private void ResetUacValues() => _values.Reset();

    /// <summary>
    /// Elevates the current running application to administrator level when button is clicked.
    /// </summary>
    /// <remarks>
    /// The application/process will restart when clicked.
    /// </remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ElevateApplicationOnClick
    {
        get => _values.ElevateApplicationOnClick;

        set => _values.ElevateApplicationOnClick = value;
    }

    /// <summary>
    /// The application assembly.
    /// </summary>
    /// <remarks>
    /// Use 'Process.GetCurrentProcess().ProcessName;' as a start.
    /// </remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string ProcessName
    {
        get => _values.ProcessName;

        set => _values.ProcessName = value;
    }
    #endregion

    #region Events
    /// <summary></summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="ExecuteProcessAsAdministratorEventArgs"/> instance containing the event data.</param>
    public delegate void ExecuteProcessAsAdministratorEventHandler(object sender, ExecuteProcessAsAdministratorEventArgs e);

    /// <summary>The execute process as administrator</summary>
    public ExecuteProcessAsAdministratorEventHandler? ExecuteProcessAsAdministrator;
    #endregion

    #region Constructor
    /// <summary>
    /// Initialises a new instance of the <see cref="KryptonToolStripMenuItemUACShield"/> class.
    /// </summary>
    public KryptonToolStripMenuItemUACShield()
    {
        _values = new UacShieldMenuItemValues(this);

        // UAC (and the shield glyph) exists on Windows Vista and newer (major version >= 6).
        if (Environment.OSVersion.Version.Major >= 6)
        {
            if (!_isSystemAbleToLoadShield.HasValue || _isSystemAbleToLoadShield.Value)
            {
                try
                {
                    var icon = GraphicsExtensions.LoadIcon(IconType.Shield, SystemInformation.SmallIconSize);

                    if (icon != null)
                    {
                        Image = icon.ToBitmap();

                        TextImageRelation = TextImageRelation.ImageBeforeText;

                        ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;

                        _isSystemAbleToLoadShield = true;

                        return;
                    }

                    _isSystemAbleToLoadShield = false;
                }
                catch (Exception exc)
                {
                    KryptonMessageBox.Show(
                        $"Unable to load the UAC shield icon.\nOS: {Environment.OSVersion.VersionString}.\nException: {exc.Message}.",
                        "Unsupported Software",
                        KryptonMessageBoxButtons.OK,
                        KryptonMessageBoxIcon.Error);

                    _isSystemAbleToLoadShield = false;
                }
            }
        }
    }
    #endregion

    #region Overrides
    protected override void OnClick(EventArgs e)
    {
        if (_values.ElevateApplicationOnClick && !string.IsNullOrEmpty(ProcessName))
        {
            ExecuteProcessAsAdministratorEventArgs evt = new ExecuteProcessAsAdministratorEventArgs(ProcessName);

            OnExecuteProcessAsAdministrator(this, evt);
        }

        base.OnClick(e);
    }
    #endregion

    /// <summary>Called when [execute process as administrator].</summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="ExecuteProcessAsAdministratorEventArgs"/> instance containing the event data.</param>
    protected virtual void OnExecuteProcessAsAdministrator(object sender, ExecuteProcessAsAdministratorEventArgs e)
    {
        ExecuteProcessAsAdministrator?.Invoke(sender, e);
    }
}
