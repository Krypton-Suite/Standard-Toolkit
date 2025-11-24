#region BSD License
/*
 *  
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

internal class IntegratedToolbarManager
{
    #region Instance Fields

    private readonly KryptonForm _parentForm;

    private readonly IntegratedToolBarValues _integratedToolBarValues;

    #endregion

    #region Public

    public KryptonForm ParentForm => _parentForm;

    #endregion

    #region Identity

    public IntegratedToolbarManager()
    {

    }

    public IntegratedToolbarManager(KryptonForm parentForm, IntegratedToolBarValues values)
    {
        _parentForm = parentForm;

        _integratedToolBarValues = values;
    }

    #endregion

    #region Implementation

    internal void SetupToolBar()
    {
        try
        {
            if (_integratedToolBarValues.ShowIntegratedToolBar)
            {
                if (_integratedToolBarValues.IntegratedToolBarItems.Length > 0)
                {
                    foreach (ButtonSpecAny buttons in _integratedToolBarValues.IntegratedToolBarItems)
                    {
                        _parentForm.ButtonSpecs.Add(buttons);
                    }
                }
                else
                {
                    _integratedToolBarValues.SetupToolBar();
                }
            }
        }
        catch (Exception e)
        {
            KryptonExceptionHandler.CaptureException(e, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);
        }
    }

    internal void ShowToolBar(bool visible)
    {
        try
        {
            foreach (ButtonSpecAny buttons in _integratedToolBarValues.ReturnToolBarButtonArray())
            {
                buttons.Visible = visible;
            }
        }
        catch (Exception e)
        {
            KryptonExceptionHandler.CaptureException(e, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);
        }
    }

    #endregion
}