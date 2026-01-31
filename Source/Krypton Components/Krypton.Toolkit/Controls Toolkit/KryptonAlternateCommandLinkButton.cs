#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// A KryptonCommandLink button.
/// Boilerplate code from (https://blogs.msdn.microsoft.com/knom/2007/03/12/vista-command-link-control-with-c-windows-forms/)
/// </summary>
/// <remarks>If used on Windows Vista or higher, the button will be a CommandLink: Basically the same functionality as a Button but looks different.</remarks>
[DesignerCategory("Code")]
[DisplayName("Krypton Command Link")]
[Description("A Krypton Command Link Button.")]
[ToolboxItem(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
[ToolboxBitmap(typeof(KryptonAlternateCommandLinkButton), @"ToolboxBitmaps.KryptonCommandLinkButton.bmp")]
public class KryptonAlternateCommandLinkButton : KryptonButton
{
    #region Static Fields

    private const int BS_COMMANDLINK = 0x0000000E;

    private const int BCM_SETNOTE = 0x00001609;

    private const int BCM_GETNOTE = 0x0000160A;

    private const int BCM_GETNOTELENGTH = 0x0000160B;

    private const int BCM_SETSHIELD = 0x0000160C;

    #endregion

    #region Instance Fields

    private bool _showUACShield;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the shield icon visibility of the command link.
    /// </summary>
    [Category("Command Link"), Description("Gets or sets the shield icon visibility of the command link."), DefaultValue(false)]
    public bool ShowUACShield
    {
        get => _showUACShield;

        set
        {
            _showUACShield = value;

            SendMessage(new HandleRef(this, Handle), BCM_SETSHIELD, IntPtr.Zero, _showUACShield);
        }
    }

    /// <summary>
    /// Gets or sets the note text of the command link.
    /// </summary>
    [Category("Command Link"), Description("Gets or sets the note text of the command link."), DefaultValue("")]
    public string NoteText
    {
        get => GetNoteText();

        set => SetNoteText(value);
    }
    #endregion

    #region WIN32 Calls

    [DllImport(Libraries.User32, CharSet = CharSet.Unicode)]
    static extern int SendMessage(HandleRef hWnd, uint msg, ref int wParam, StringBuilder lParam);

    [DllImport(Libraries.User32, CharSet = CharSet.Unicode)]
    static extern int SendMessage(HandleRef hWnd, uint msg, IntPtr wParam, string lParam);

    [DllImport(Libraries.User32, CharSet = CharSet.Unicode)]
    static extern int SendMessage(HandleRef hWnd, uint msg, IntPtr wParam, bool lParam);

    [DllImport(Libraries.User32, CharSet = CharSet.Unicode)]
    static extern int SendMessage(HandleRef hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonAlternateCommandLinkButton" /> class.</summary>
    public KryptonAlternateCommandLinkButton()
    {

    }

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override Size DefaultSize => new Size(160, 60);

    /// <inheritdoc />
    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams createParams = base.CreateParams;

            createParams.Style |= BS_COMMANDLINK;

            return createParams;
        }
    }

    #endregion

    #region Implementation

    /// <summary>Sets the NoteText to the value of value.</summary>
    /// <param name="value">The desired value of NoteText.</param>
    private void SetNoteText(string value) => SendMessage(new HandleRef(this, Handle), BCM_SETNOTE, IntPtr.Zero, value);

    /// <summary>
    /// Returns the value of the NoteText.
    /// </summary>
    /// <returns>The value of the NoteText.</returns>
    private string GetNoteText()
    {
        int length = SendMessage(new HandleRef(this, Handle), BCM_GETNOTELENGTH, IntPtr.Zero, IntPtr.Zero) + 1;

        StringBuilder stringBuilder = new StringBuilder(length);

        SendMessage(new HandleRef(this, Handle), BCM_GETNOTE, ref length, stringBuilder);

        return stringBuilder.ToString();
    }

    #endregion
}