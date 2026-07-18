#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Creates native WinForms designer editors from <c>System.Design</c> / <c>System.Windows.Forms.Design</c>.
/// </summary>
internal static class KryptonDesignerNativeEditorBridge
{
    private static Assembly? _designAssembly;

    /// <summary>
    /// Creates a native <see cref="UITypeEditor"/> instance.
    /// </summary>
    /// <param name="typeName">Short or fully-qualified native editor type name.</param>
    /// <returns>Native editor instance.</returns>
    internal static UITypeEditor CreateUITypeEditor(string typeName) =>
        (UITypeEditor)CreateInstance(typeName)!;

    /// <summary>
    /// Creates a native <see cref="CollectionEditor"/> instance.
    /// </summary>
    /// <param name="typeName">Short or fully-qualified native editor type name.</param>
    /// <param name="collectionType">Collection item type.</param>
    /// <returns>Native editor instance.</returns>
    internal static CollectionEditor CreateCollectionEditor(string typeName, Type collectionType) =>
        (CollectionEditor)CreateInstance(typeName, collectionType)!;

    private static object? CreateInstance(string typeName, Type? collectionType = null)
    {
        var editorType = GetDesignType(typeName);
        if (collectionType is null)
        {
            return Activator.CreateInstance(
                editorType,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                binder: null,
                args: null,
                culture: null);
        }

        return Activator.CreateInstance(
            editorType,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
            binder: null,
            args: [collectionType],
            culture: null);
    }

    private static Type GetDesignType(string typeName)
    {
        if (typeName.Contains('.'))
        {
            return Type.GetType(typeName, throwOnError: true)!;
        }

        var assembly = DesignAssembly;
        var fullName = $"System.Windows.Forms.Design.{typeName}";
        var type = assembly.GetType(fullName, throwOnError: false);
        if (type is not null)
        {
            return type;
        }

        return Type.GetType($"{fullName}, System.Design", throwOnError: true)!;
    }

    private static Assembly DesignAssembly
    {
        get
        {
            if (_designAssembly is not null)
            {
                return _designAssembly;
            }

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var name = assembly.GetName().Name;
                if (name is "System.Windows.Forms.Design" or "System.Design")
                {
                    _designAssembly = assembly;
                    return _designAssembly;
                }
            }

            foreach (var assemblyName in new[] { "System.Windows.Forms.Design", "System.Design" })
            {
                try
                {
                    _designAssembly = Assembly.Load(assemblyName);
                    return _designAssembly;
                }
                catch (FileNotFoundException)
                {
                }
                catch (FileLoadException)
                {
                }
            }

            throw new InvalidOperationException("Unable to locate a WinForms designer assembly.");
        }
    }
}

/// <summary>
/// Forwards designer editing to a native WinForms <see cref="UITypeEditor"/>.
/// </summary>
public abstract class KryptonDesignerNativeUITypeEditor : UITypeEditor
{
    #region Instance Fields
    private UITypeEditor? _nativeEditor;
    #endregion

    #region Protected
    /// <summary>
    /// Gets the short native editor type name in <c>System.Windows.Forms.Design</c>.
    /// </summary>
    protected abstract string NativeEditorTypeName { get; }

    /// <summary>
    /// Gets the native editor instance.
    /// </summary>
    protected UITypeEditor NativeEditor =>
        _nativeEditor ??= KryptonDesignerNativeEditorBridge.CreateUITypeEditor(NativeEditorTypeName);

    /// <inheritdoc />
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context) =>
        NativeEditor.GetEditStyle(context);

    /// <inheritdoc />
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value) =>
        NativeEditor.EditValue(context, provider, value);

    /// <inheritdoc />
    public override bool GetPaintValueSupported(ITypeDescriptorContext? context) =>
        NativeEditor.GetPaintValueSupported(context);

    /// <inheritdoc />
    public override void PaintValue(PaintValueEventArgs e) => NativeEditor.PaintValue(e);

    /// <inheritdoc />
    public override bool IsDropDownResizable => NativeEditor.IsDropDownResizable;
    #endregion
}

/// <summary>
/// Forwards designer editing to a native WinForms <see cref="CollectionEditor"/>.
/// </summary>
public abstract class KryptonDesignerNativeCollectionEditor : CollectionEditor
{
    #region Instance Fields
    private CollectionEditor? _nativeEditor;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonDesignerNativeCollectionEditor"/> class.
    /// </summary>
    /// <param name="nativeEditorTypeName">Short native editor type name.</param>
    /// <param name="collectionType">Collection item type.</param>
    protected KryptonDesignerNativeCollectionEditor(string nativeEditorTypeName, Type collectionType)
        : base(collectionType)
    {
        NativeEditorTypeName = nativeEditorTypeName;
    }
    #endregion

    #region Protected
    /// <summary>
    /// Gets the short native editor type name in <c>System.Windows.Forms.Design</c>.
    /// </summary>
    protected string NativeEditorTypeName { get; }

    /// <summary>
    /// Gets the native editor instance.
    /// </summary>
    protected CollectionEditor NativeEditor =>
        _nativeEditor ??= KryptonDesignerNativeEditorBridge.CreateCollectionEditor(NativeEditorTypeName, CollectionType);

    /// <inheritdoc />
    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value) =>
        NativeEditor.EditValue(context, provider, value);
    #endregion
}
