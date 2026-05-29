#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

using Microsoft.Win32;
/// <summary>
/// Simple class to handle program auto start when the user logs on.
/// </summary>
public class RegistryAccess
{
    // Registry access
    private RegistryKey _registryKey;
    private const string _registryPath = @"Software\Krypton-Suite\Standard-ToolKit\TestForm";
    private const string _rvLastFilterString = "LastFilterString";
    private const string _rvDockTopRight = "DockTopRight";
    private const string _rvFormWidth = "FormWidth";
    private const string _rvFormHeight = "FormHeight";

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <exception cref="Exception">Throws an Exception when the registry key cannot be opened.</exception>
    public RegistryAccess()
    {
        _registryKey = Registry.CurrentUser.CreateSubKey(_registryPath)
            ?? throw new Exception("Registry.CurrentUser.CreateSubKey() returned null.");
    }

    public int FormWidth
    {
        get => int.TryParse(_registryKey.GetValue(_rvFormWidth, -1).ToString(), out int width)
            ? width
            : -1;

        set => _registryKey.SetValue(_rvFormWidth, value);
    }

    public int FormHeight
    {
        get => int.TryParse(_registryKey.GetValue(_rvFormHeight, -1).ToString(), out int height)
            ? height
            : -1;

        set => _registryKey.SetValue(_rvFormHeight, value);
    }

    public Size FormSize
    {
        get => new Size(FormWidth, FormHeight);
        
        set
        {
            FormWidth = value.Width;
            FormHeight = value.Height;
        }
    }

    public string LastFilterString
    {
        get => _registryKey.GetValue(_rvLastFilterString) as string ?? string.Empty;
        set => _registryKey.SetValue(_rvLastFilterString, value);
    }

    public bool DockTopRight
    {
        get => (_registryKey.GetValue(_rvDockTopRight, "0") as string) == "1" ? true : false;
        set => _registryKey.SetValue(_rvDockTopRight, value ? "1" : "0");
    }
}

