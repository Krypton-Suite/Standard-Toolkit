#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

#if !NET9_0_OR_GREATER

namespace Krypton.Toolkit;

/// <summary>
/// Provides Form ShowDialogAsync / ShowAsync compatible APIs on TFMs before .NET 9.
/// On .NET 9+ the framework instance methods are used instead.
/// </summary>
public static class KryptonFormAsyncExtensions
{
    /// <summary>
    /// Displays the form as a modal dialog. Completes synchronously via <see cref="Form.ShowDialog()"/> on pre-.NET 9 TFMs.
    /// </summary>
    /// <param name="form">The form to show.</param>
    /// <returns>A completed task with the dialog result.</returns>
    public static Task<DialogResult> ShowDialogAsync(this Form form) =>
        Task.FromResult(form.ShowDialog());

    /// <summary>
    /// Displays the form as a modal dialog. Completes synchronously via <see cref="Form.ShowDialog(IWin32Window)"/> on pre-.NET 9 TFMs.
    /// </summary>
    /// <param name="form">The form to show.</param>
    /// <param name="owner">The owner window.</param>
    /// <returns>A completed task with the dialog result.</returns>
    public static Task<DialogResult> ShowDialogAsync(this Form form, IWin32Window? owner) =>
        Task.FromResult(form.ShowDialog(owner));

    /// <summary>
    /// Displays the form modelessly and returns a task that completes when the form is closed.
    /// </summary>
    /// <param name="form">The form to show.</param>
    /// <returns>A task that completes when the form closes.</returns>
    public static Task ShowAsync(this Form form) => ShowAsyncCore(form, owner: null);

    /// <summary>
    /// Displays the form modelessly and returns a task that completes when the form is closed.
    /// </summary>
    /// <param name="form">The form to show.</param>
    /// <param name="owner">The owner window.</param>
    /// <returns>A task that completes when the form closes.</returns>
    public static Task ShowAsync(this Form form, IWin32Window? owner) => ShowAsyncCore(form, owner);

    private static Task ShowAsyncCore(Form form, IWin32Window? owner)
    {
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
    }
}

#endif
