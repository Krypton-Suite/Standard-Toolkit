#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
///  Selects which implementation backs the Krypton file and folder dialog wrappers.
/// </summary>
public enum KryptonDialogProviderMode
{
    /// <summary>
    ///  Use the standard Windows Explorer dialog (not reparented into a KryptonForm).
    /// </summary>
    Native = 0,

    /// <summary>
    ///  Use the managed KryptonForm dialog implementation.
    /// </summary>
    Custom = 1
}

internal enum KryptonDialogKind
{
    OpenFile,
    SaveFile,
    SelectFolder
}

internal sealed class KryptonDialogOptions
{
    public KryptonDialogKind Kind { get; set; }

    public KryptonDialogProviderMode ProviderMode { get; set; }

    public string Title { get; set; } = string.Empty;

    public Icon? Icon { get; set; }

    public string InitialDirectory { get; set; } = string.Empty;

    public string CurrentPath { get; set; } = string.Empty;

    public string FileName { get; set; } = string.Empty;

    public string DefaultExt { get; set; } = string.Empty;

    public string Filter { get; set; } = string.Empty;

    public int FilterIndex { get; set; } = 1;

    public bool AddExtension { get; set; }

    public bool CheckFileExists { get; set; }

    public bool CheckPathExists { get; set; }

    public bool ValidateNames { get; set; }

    public bool OverwritePrompt { get; set; }

    public bool CreatePrompt { get; set; }

    public bool Multiselect { get; set; }

    public bool SupportMultiDottedExtensions { get; set; }

    public bool ReadOnlyChecked { get; set; }

    public Environment.SpecialFolder RootFolder { get; set; } = Environment.SpecialFolder.Desktop;

    /// <summary>
    /// When true, the custom dialog shows a Date modified filter beside the search box.
    /// </summary>
    public bool ShowDateModifiedFilter { get; set; }

    public List<string> CustomPlaces { get; } = [];
}

internal sealed class KryptonDialogResult
{
    public DialogResult DialogResult { get; set; }

    public string SelectedPath { get; set; } = string.Empty;

    public string FileName { get; set; } = string.Empty;

    public string[] FileNames { get; set; } = Array.Empty<string>();

    public bool ReadOnlyChecked { get; set; }
}

internal sealed class KryptonDialogProviderContext
{
    public KryptonDialogProviderContext(ShellDialogWrapper wrapper, IWin32Window? owner, KryptonDialogOptions options)
    {
        Wrapper = wrapper;
        Owner = owner;
        Options = options;
    }

    public ShellDialogWrapper Wrapper { get; }

    public IWin32Window? Owner { get; }

    public KryptonDialogOptions Options { get; }
}

internal interface IKryptonDialogProvider
{
    KryptonDialogResult ShowDialog(KryptonDialogProviderContext context);

    Task<KryptonDialogResult> ShowDialogAsync(KryptonDialogProviderContext context);
}

internal static class KryptonDialogProviderFactory
{
    public static IKryptonDialogProvider Create(KryptonDialogProviderMode providerMode) => providerMode switch
    {
        KryptonDialogProviderMode.Custom => CustomKryptonDialogProvider.Instance,
        _ => NativeKryptonDialogProvider.Instance
    };
}

internal sealed class NativeKryptonDialogProvider : IKryptonDialogProvider
{
    public static NativeKryptonDialogProvider Instance { get; } = new NativeKryptonDialogProvider();

    private NativeKryptonDialogProvider()
    {
    }

    public KryptonDialogResult ShowDialog(KryptonDialogProviderContext context)
    {
        var dialogResult = context.Wrapper.ShowNativeDialogCore(context.Owner);
        var result = context.Wrapper.CaptureDialogResult();
        result.DialogResult = dialogResult;
        return result;
    }

    /// <summary>
    /// Awaitable wrapper around nested-modal native ShowDialog (UI remains blocked until close).
    /// </summary>
    public Task<KryptonDialogResult> ShowDialogAsync(KryptonDialogProviderContext context) =>
        Task.FromResult(ShowDialog(context));
}

internal sealed class CustomKryptonDialogProvider : IKryptonDialogProvider
{
    public static CustomKryptonDialogProvider Instance { get; } = new CustomKryptonDialogProvider();

    private CustomKryptonDialogProvider()
    {
    }

    public KryptonDialogResult ShowDialog(KryptonDialogProviderContext context)
    {
        using var dialog = new VisualCustomFileDialogForm(context);
        return dialog.ShowProviderDialog();
    }

    public Task<KryptonDialogResult> ShowDialogAsync(KryptonDialogProviderContext context)
    {
        return ShowProviderDialogAsync(context);
    }

    private static async Task<KryptonDialogResult> ShowProviderDialogAsync(KryptonDialogProviderContext context)
    {
        using var dialog = new VisualCustomFileDialogForm(context);
        return await dialog.ShowProviderDialogAsync().ConfigureAwait(false);
    }
}
