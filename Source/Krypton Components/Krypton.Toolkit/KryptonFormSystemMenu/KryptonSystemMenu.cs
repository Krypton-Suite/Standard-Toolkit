#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public class KryptonSystemMenu
{
    private KryptonForm _form;
    private ViewDrawDocker _drawHeading;
    private KryptonSystemMenuListener _listener;
    private KryptonContextMenu _contextMenu;

    public KryptonSystemMenu(KryptonForm kryptonForm, ViewDrawDocker drawHeading, KryptonContextMenu contextMenu)
    {
        _form = kryptonForm;
        _contextMenu = contextMenu;
        _drawHeading = drawHeading;

        // Instantiate the listener
        _listener = new(_form, _drawHeading);

        // Subscribe to enabled changed events
        _form.SystemMenuValues.PropertyChanged += OnMenuValuesPropertyChanged;
    }

    /// <summary>
    /// Stop listening for mouse and keyboard events that trigger the system menu.
    /// </summary>
    public void DisableListener()
    {
        _listener.DisableListener();
    }

    /// <summary>
    /// Enable listening for mouse and keyboard events that trigger the system menu.
    /// </summary>
    public void EnableListener()
    {
        _listener.EnableListener();
    }

    #region Private
    private void OnMenuValuesPropertyChanged(object? sender, PropertyChangedEventArgs eventArgs)
    {
        if (eventArgs.PropertyName == nameof(_form.SystemMenuValues.Enabled))
        {
            if (_form.SystemMenuValues.Enabled)
            {
                EnableListener();
            }
            else
            {
                DisableListener();
            }
        }
        else if (eventArgs.PropertyName == nameof(_form.SystemMenuValues.ShowOnAltSpace))
        {
            //OnMenuValuesEnabledChanged();
        }
        else if (eventArgs.PropertyName == nameof(_form.SystemMenuValues.ShowOnIconClick))
        {
            //OnMenuValuesEnabledChanged();
        }
        else if (eventArgs.PropertyName == nameof(_form.SystemMenuValues.ShowOnRightClick))
        {
           //OnMenuValuesEnabledChanged();
        }
    }
#endregion
}
