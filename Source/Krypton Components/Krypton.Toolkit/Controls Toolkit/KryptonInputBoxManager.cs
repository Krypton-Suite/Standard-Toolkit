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

[ToolboxBitmap(typeof(KryptonInputBox), "ToolboxBitmaps.KryptonInputBox.bmp")]
[DesignerCategory(@"code")]
public class KryptonInputBoxManager : Component
{
    #region Variables

    private KryptonInputBoxData _inputBoxData;

    #endregion

    #region Properties

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonInputBoxData InputBoxData { get => _inputBoxData; set => _inputBoxData = value; }

    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="KryptonInputBoxManager" /> class.</summary>
    public KryptonInputBoxManager()
    {
        _inputBoxData = new KryptonInputBoxData();
    }
    #endregion

    #region Setters and Getters
    /// <summary>Sets the Owner to the value of value.</summary>
    /// <param name="value">The desired value of Owner.</param>
    public void SetOwner(IWin32Window value) => _inputBoxData.Owner = value;

    /// <summary>Returns the value of the Owner.</summary>
    /// <returns>The value of the Owner.</returns>
    public IWin32Window? GetOwner() => _inputBoxData.Owner;
    #endregion

    #region Methods
    /// <summary>Displays the krypton input box.</summary>
    public void DisplayKryptonInputBox() => KryptonInputBox.Show(_inputBoxData);

    #endregion
}