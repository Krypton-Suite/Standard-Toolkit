#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Cross-TFM helpers for WinForms async form APIs used by Utilities dialogs/toasts.
/// On .NET 9+ delegates to Form.ShowDialogAsync / Form.ShowAsync.
/// On earlier TFMs degrades to synchronous show (modal) or FormClosed-backed Task (modeless).
/// </summary>
internal static class KryptonFormAsync
{
    /// <summary>
    /// Displays the form as a modal dialog asynchronously.
    /// </summary>
    /// <param name="form">The form to show.</param>
    /// <param name="owner">Optional owner window.</param>
    /// <returns>A task that completes with the dialog result.</returns>
    public static Task<DialogResult> ShowDialogAsync(Form form, IWin32Window? owner = null)
    {
#if NET9_0_OR_GREATER
        return owner is null
            ? form.ShowDialogAsync()
            : form.ShowDialogAsync(owner);
#else
        return Task.FromResult(owner is null ? form.ShowDialog() : form.ShowDialog(owner));
#endif
    }

    /// <summary>
    /// Displays the form modelessly and returns a task that completes when the form is closed.
    /// </summary>
    /// <param name="form">The form to show.</param>
    /// <param name="owner">Optional owner window.</param>
    /// <returns>A task that completes when the form closes.</returns>
    public static Task ShowAsync(Form form, IWin32Window? owner = null)
    {
#if NET9_0_OR_GREATER
        return form.ShowAsync(owner);
#else
        var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);

        void OnClosed(object? sender, FormClosedEventArgs e)
        {
            form.FormClosed -= OnClosed;
            tcs.TrySetResult(null);
        }

        form.FormClosed += OnClosed;
        if (owner is null)
        {
            form.Show();
        }
        else
        {
            form.Show(owner);
        }

        return tcs.Task;
#endif
    }
}
