#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2024. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Static class that tracks the active form within a WinForms application in an event drive way.<br/>
/// It removes the need for the use of Form.ActiveForm which relies on GetForeGroundWindow().
/// </summary>
public static class ActiveFormTracker
{
    #region Private static values
    private static volatile Form? _activeForm;
    private static volatile Form? _activeMdiChild;
    #endregion

    #region Static Identity
    static ActiveFormTracker()
    {
        _activeForm = null;
        _activeMdiChild = null;
    }
    #endregion

    #region Public
    /// <summary>
    /// The currently active form
    /// </summary>
    public static Form? ActiveForm 
    {
        get => _activeForm;
    }

    /// <summary>
    /// The currently active mdi child form
    /// </summary>
    public static Form? ActiveMdiChild 
    {
        get => _activeMdiChild;
    }

    /// <summary>
    /// Subscribes the form to the tracker.<br/>
    /// KryptonForm objects automatically subscribe to the tracker on instantiation.<br/>
    /// When using a WinForms form in combination with the Krypton Docking module add the following code to each form definition.<br/><br/>
    /// <code>
    /// protected override void OnHandleCreated(EventArgs e)
    /// {
    ///     base.OnHandleCreated(e);
    ///     Tracker.Attach(this);
    /// }
    /// </code><br/><br/>
    /// When the form is destroyed it will automatically be unsubscribed from the tracker.
    /// </summary>
    /// <param name="form">The form to subscribe.</param>
    public static void Attach(Form form)
    {
        if (!form.IsMdiChild)
        {
            // A MDI Container can be handled the same as any "non mdi container" form
            form.Activated += Activated;
            form.Deactivate += Deactivate;
            form.HandleDestroyed += HandleDestroyed;

            // The new form isn't always the top form,
            // so to get started use Form.ActiveForm once, which uses GetForegroundWindow().
            if (form.Equals(Form.ActiveForm))
            {
                Activated(form, EventArgs.Empty);
            }
        }
        else
        {
            // This is a MDI child
            form.Activated += ActivatedMdiChild;
            form.Deactivate += DeactivateMdiChild;
            form.HandleDestroyed += HandleDestroyedMdiChild;

            // Only if the mdi parent is the active form we need to act
            // Is form the active child
            if (IsActiveForm(form.MdiParent) && form.Equals(form.MdiParent!.ActiveMdiChild))
            {
                ActivatedMdiChild(form, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Return if the given form is the active form.
    /// </summary>
    /// <param name="form">Form to check if it's active.</param>
    /// <returns>True if the given form is equal to the active form, otherwise false.</returns>
    public static bool IsActiveForm(Form? form)
    {
        return form is not null && form.Equals(_activeForm);
    }

    /// <summary>
    /// Return if the given form is the active mdi child form.
    /// </summary>
    /// <param name="mdiChild">ChildForm to check if it's active.</param>
    /// <returns>True if the given childform is equal to the active form, otherwise false.</returns>
    public static bool IsActiveMdiChild(Form? mdiChild)
    {
        return mdiChild is not null && mdiChild.Equals(_activeMdiChild);
    }
    #endregion

    #region Private
    /// <summary>
    /// Is executed when the child form is activated.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Not used.</param>
    private static void ActivatedMdiChild(object? sender, EventArgs e)
    {
        if (sender is Form form)
        {
            _activeMdiChild = form;
        }
    }

    /// <summary>
    /// Is executed when the form is activated.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Not used.</param>
    private static void Activated(object? sender, EventArgs e)
    {
        if (sender is Form form)
        {
            _activeForm = form;

            // When a non mdi child form is activated, _activeMdiChild can always be set to null
            // since it may have been activated before by an mdi child.
            // When a form is not a mdi container Form.ActiveMdiChild is null
            // So in any situation Form.ActiveMdiChild will always reflect the correct state.
            _activeMdiChild = form.ActiveMdiChild;
        }
    }

    /// <summary>
    /// Is executed when the form is deactivated.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Not used.</param>
    private static void Deactivate(object? sender, EventArgs e)
    {
        if (sender is Form form && IsActiveForm(form))
        {
            _activeForm = null;

            // If a mdi container becomes inactive the child form in the tracker needs te be set to inactive
            if (_activeMdiChild is not null)
            {
                _activeMdiChild = null;
            }
        }
    }

    /// <summary>
    /// Is executed when the child form is deactivated.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Not used.</param>
    private static void DeactivateMdiChild(object? sender, EventArgs e)
    {
        if (sender is Form mdiChild && IsActiveMdiChild(mdiChild))
        {
            _activeMdiChild = null;
        }
    }

    /// <summary>
    /// Unsubscribes from the form events.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Not used.</param>
    private static void HandleDestroyed(object? sender, EventArgs e)
    {
        if (sender is Form form)
        {
            form.HandleDestroyed -= HandleDestroyed;
            form.Deactivate -= Deactivate;
            form.Activated -= Activated;
        }
    }

    /// <summary>
    /// Unsubscribes from the child form events.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Not used.</param>
    private static void HandleDestroyedMdiChild(object? sender, EventArgs e)
    {
        if (sender is Form form)
        {
            form.HandleDestroyed -= HandleDestroyed;
            form.Deactivate -= DeactivateMdiChild;
            form.Activated -= ActivatedMdiChild;
        }
    }
    #endregion
}
