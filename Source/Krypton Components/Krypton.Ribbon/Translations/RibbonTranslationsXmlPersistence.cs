#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Versioned XML persistence for the localizable string tree of a <see cref="KryptonRibbon"/>.
/// Structural <c>Version</c> is informational; unknown elements are ignored and missing keys keep current values.
/// </summary>
internal static class RibbonTranslationsXmlPersistence
{
    internal const string RootElementName = @"KryptonRibbonTranslations";
    internal const string RibbonElementName = @"Ribbon";
    internal const int CurrentSupportedVersion = 1;

    private const string VersionAttribute = @"Version";
    private const string CultureAttribute = @"Culture";
    private const string GeneratedAttribute = @"Generated";
    private const string ToolkitVersionAttribute = @"ToolkitVersion";
    private const string RibbonNameAttribute = @"RibbonName";
    private const string ValueAttribute = @"Value";
    private const string IsNullAttribute = @"IsNull";
    private const string TranslationIdAttribute = @"TranslationId";
    private const string NameAttribute = @"Name";
    private const string TypeAttribute = @"Type";
    private const string IndexAttribute = @"Index";
    private const string UniqueNameAttribute = @"UniqueName";
    private const string ContextNameAttribute = @"ContextName";
    private const string RibbonStringsElement = @"RibbonStrings";
    private const string ExtensionsElement = @"Extensions";

    private static readonly HashSet<string> IdentityPropertyNames =
        new HashSet<string>(StringComparer.Ordinal)
        {
            TranslationIdAttribute, NameAttribute, @"Site", @"Tag",
            ContextNameAttribute, UniqueNameAttribute, @"SelectedContext"
        };

    private static readonly HashSet<string> ContentTypeNames =
        new HashSet<string>(StringComparer.Ordinal)
        {
            nameof(KryptonRibbonGroupTextBox),
            nameof(KryptonRibbonGroupRichTextBox),
            nameof(KryptonRibbonGroupComboBox),
            nameof(KryptonRibbonGroupThemeComboBox),
            nameof(KryptonRibbonGroupMaskedTextBox),
            nameof(KryptonRibbonGroupDomainUpDown)
        };

    private static readonly HashSet<string> ContentPropertyNames =
        new HashSet<string>(StringComparer.Ordinal)
        {
            @"Text", @"Lines", @"Mask", @"CustomFormat", @"CalendarTodayFormat",
            @"PromptChar", @"AutoCompleteCustomSource"
        };

    public static XmlDocument Export(KryptonRibbon ribbon, RibbonTranslationOptions? options)
    {
        if (ribbon == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(ribbon));
        }

        options ??= new RibbonTranslationOptions();

        var doc = new XmlDocument();
        doc.AppendChild(doc.CreateProcessingInstruction(@"xml", @"version=""1.0"""));

        var root = doc.CreateElement(RootElementName);
        root.SetAttribute(VersionAttribute, CurrentSupportedVersion.ToString(CultureInfo.InvariantCulture));
        root.SetAttribute(CultureAttribute, System.Threading.Thread.CurrentThread.CurrentUICulture.Name);
        root.SetAttribute(GeneratedAttribute, DateTime.Now.ToString(CultureInfo.InvariantCulture));
        root.SetAttribute(ToolkitVersionAttribute, ToolkitStringsXmlPersistence.GetToolkitVersionStamp());
        var ribbonKey = ResolveRibbonKey(ribbon);
        if (!string.IsNullOrEmpty(ribbonKey))
        {
            root.SetAttribute(RibbonNameAttribute, ribbonKey);
        }

        doc.AppendChild(root);

        if (options.IncludeChrome)
        {
            ExportChrome(doc, root, options.IncludeDefaults);
        }

        var ribbonElement = doc.CreateElement(RibbonElementName);
        WriteIdentityAttributes(ribbonElement, ribbon, index: -1);
        ExportObject(doc, ribbonElement, ribbon, options);
        ExportRibbonChildren(doc, ribbonElement, ribbon, options);
        root.AppendChild(ribbonElement);

        ribbon.RaiseTranslationsSaving(root);

        return doc;
    }

    public static void Import(KryptonRibbon ribbon, XmlDocument doc, RibbonTranslationOptions? options)
    {
        if (ribbon == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(ribbon));
        }

        if (doc == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(doc));
        }

        options ??= new RibbonTranslationOptions();

        var root = doc.SelectSingleNode(RootElementName) as XmlElement;
        if (root == null)
        {
            ThrowHelper.ThrowArgumentException($@"Root element must be called '{RootElementName}'.");
        }

        WarnOnVersion(root);
        WarnOnCulture(root);
        WarnOnRibbonName(root, ribbon);

        if (options.IncludeChrome)
        {
            ImportChrome(root, options.ResetFirst);
        }

        if (options.ResetFirst)
        {
            ResetExportableStrings(ribbon, options);
        }

        var ribbonElement = root.SelectSingleNode(RibbonElementName) as XmlElement ?? root;
        ImportObject(ribbonElement, ribbon, options);
        ImportRibbonChildren(ribbonElement, ribbon, options);

        ribbon.RaiseTranslationsLoading(root);
        ReportCoverage(ribbon, doc, options, filePath: null);
        ribbon.PerformNeedPaint(true);
    }

    public static RibbonTranslationsCoverage Analyze(KryptonRibbon ribbon, XmlDocument doc, RibbonTranslationOptions? options, string? filePath = null)
    {
        if (ribbon == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(ribbon));
        }

        if (doc == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(doc));
        }

        options ??= new RibbonTranslationOptions { IncludeDefaults = true };

        var coverage = new RibbonTranslationsCoverage { FilePath = filePath };
        var root = doc.SelectSingleNode(RootElementName) as XmlElement;
        if (root != null)
        {
            coverage.Culture = NullIfEmpty(root.GetAttribute(CultureAttribute));
            coverage.ToolkitVersion = NullIfEmpty(root.GetAttribute(ToolkitVersionAttribute));
            if (int.TryParse(root.GetAttribute(VersionAttribute), NumberStyles.Integer,
                    CultureInfo.InvariantCulture, out var formatVersion))
            {
                coverage.FormatVersion = formatVersion;
            }
        }

        var liveOptions = CloneOptions(options);
        liveOptions.IncludeDefaults = true;
        var liveDoc = Export(ribbon, liveOptions);
        var liveKeys = new HashSet<string>(StringComparer.Ordinal);
        var fileKeys = new HashSet<string>(StringComparer.Ordinal);
        CollectValueKeys(liveDoc.DocumentElement, string.Empty, liveKeys);
        if (root != null)
        {
            CollectValueKeys(root, string.Empty, fileKeys);
        }

        foreach (var key in liveKeys.OrderBy(k => k, StringComparer.Ordinal))
        {
            if (fileKeys.Contains(key))
            {
                coverage.Applied.Add(key);
            }
            else
            {
                coverage.MissingInFile.Add(key);
            }
        }

        foreach (var key in fileKeys.OrderBy(k => k, StringComparer.Ordinal))
        {
            if (!liveKeys.Contains(key))
            {
                coverage.ExtraInFile.Add(key);
            }
        }

        return coverage;
    }

    public static RibbonTranslationsCoverage MergeMissingToFile(KryptonRibbon ribbon, string filename, RibbonTranslationOptions? options)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(filename));
        }

        options ??= new RibbonTranslationOptions();
        var doc = new XmlDocument();
        doc.Load(filename);
        var importOptions = CloneOptions(options);
        importOptions.ResetFirst = true;
        Import(ribbon, doc, importOptions);

        var exportOptions = CloneOptions(options);
        exportOptions.IncludeDefaults = true;
        var merged = Export(ribbon, exportOptions);
        merged.Save(filename);
        return Analyze(ribbon, merged, exportOptions, filename);
    }

    public static void ExportToFile(KryptonRibbon ribbon, string filename, RibbonTranslationOptions? options)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(filename));
        }

        Export(ribbon, options).Save(filename);
    }

    public static void ExportToStream(KryptonRibbon ribbon, Stream stream, RibbonTranslationOptions? options)
    {
        if (stream == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(stream));
        }

        Export(ribbon, options).Save(stream);
    }

    public static void ImportFromFile(KryptonRibbon ribbon, string filename, RibbonTranslationOptions? options)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            ThrowHelper.ThrowArgumentNullException(nameof(filename));
        }

        var doc = new XmlDocument();
        doc.Load(filename);
        Import(ribbon, doc, options);
        ReportCoverage(ribbon, doc, options ?? new RibbonTranslationOptions(), filename);
    }

    public static void ImportFromStream(KryptonRibbon ribbon, Stream stream, RibbonTranslationOptions? options)
    {
        if (stream == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(stream));
        }

        var doc = new XmlDocument();
        doc.Load(stream);
        Import(ribbon, doc, options);
    }

    internal static string ResolveRibbonKey(KryptonRibbon ribbon)
    {
        if (!string.IsNullOrWhiteSpace(ribbon.TranslationId))
        {
            return ribbon.TranslationId;
        }

        return ribbon.Name ?? string.Empty;
    }

    internal static bool FileTargetsRibbon(XmlDocument doc, KryptonRibbon ribbon)
    {
        var root = doc.SelectSingleNode(RootElementName) as XmlElement;
        if (root == null)
        {
            return false;
        }

        var fileName = root.GetAttribute(RibbonNameAttribute);
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return true;
        }

        return string.Equals(fileName, ResolveRibbonKey(ribbon), StringComparison.OrdinalIgnoreCase);
    }

    #region Chrome

    private static void ExportChrome(XmlDocument doc, XmlElement root, bool includeDefaults)
    {
        var chromeDoc = ToolkitStringsXmlPersistence.Export(KryptonManager.Strings.RibbonStrings, includeDefaults);
        if (chromeDoc.DocumentElement == null)
        {
            return;
        }

        var container = doc.CreateElement(RibbonStringsElement);
        foreach (XmlNode child in chromeDoc.DocumentElement.ChildNodes)
        {
            if (child is XmlElement)
            {
                container.AppendChild(doc.ImportNode(child, true));
            }
        }

        if (container.HasChildNodes)
        {
            root.AppendChild(container);
        }
    }

    private static void ImportChrome(XmlElement root, bool resetFirst)
    {
        var chrome = root.SelectSingleNode(RibbonStringsElement) as XmlElement;
        if (chrome == null)
        {
            return;
        }

        var wrap = new XmlDocument();
        wrap.AppendChild(wrap.CreateProcessingInstruction(@"xml", @"version=""1.0"""));
        var wrapRoot = wrap.CreateElement(@"KryptonTranslations");
        wrapRoot.SetAttribute(VersionAttribute, @"1");
        wrap.AppendChild(wrapRoot);
        foreach (XmlNode child in chrome.ChildNodes)
        {
            if (child is XmlElement)
            {
                wrapRoot.AppendChild(wrap.ImportNode(child, true));
            }
        }

        ToolkitStringsXmlPersistence.Import(KryptonManager.Strings.RibbonStrings, wrap,
            resetFirst, refreshOpenForms: false, warnOnCultureMismatch: false);
    }

    #endregion

    #region Tree export

    private static void ExportRibbonChildren(XmlDocument doc, XmlElement parent, KryptonRibbon ribbon, RibbonTranslationOptions options)
    {
        ExportNamedObject(doc, parent, @"FileAppTab", ribbon.RibbonFileAppTab, options);
        ExportFileAppButton(doc, parent, ribbon.RibbonFileAppButton, options);
        ExportCollection(doc, parent, @"ButtonSpecs", @"Spec", ribbon.ButtonSpecs, options);
        ExportCollection(doc, parent, @"QATButtons", @"QATButton", ribbon.QATButtons, options);
        ExportCollection(doc, parent, @"Contexts", @"Context", ribbon.RibbonContexts, options);
        ExportNamedObject(doc, parent, @"NotificationBar", ribbon.NotificationBar, options);
        ExportCollection(doc, parent, @"Tabs", @"Tab", ribbon.RibbonTabs, options);

        var backstage = ribbon.RibbonFileAppTab.BackstageView;
        if (backstage != null)
        {
            var backstageElement = doc.CreateElement(@"Backstage");
            ExportCollection(doc, backstageElement, @"Pages", @"Page", backstage.Pages, options);
            ExportCollection(doc, backstageElement, @"Commands", @"Command", backstage.Commands, options);
            if (backstageElement.HasChildNodes)
            {
                parent.AppendChild(backstageElement);
            }
        }
    }

    private static void ExportFileAppButton(XmlDocument doc, XmlElement parent, RibbonFileAppButton button, RibbonTranslationOptions options)
    {
        var element = doc.CreateElement(@"FileAppButton");
        ExportObject(doc, element, button, options);
        ExportCollection(doc, element, @"MenuItems", @"Item", button.AppButtonMenuItems, options);
        ExportCollection(doc, element, @"RecentDocs", @"RecentDoc", button.AppButtonRecentDocs, options);
        ExportCollection(doc, element, @"AppButtonSpecs", @"Spec", button.AppButtonSpecs, options);
        parent.AppendChild(element);
    }

    private static void ExportCollection(XmlDocument doc, XmlElement parent, string wrapperName, string itemName, IEnumerable? items, RibbonTranslationOptions options)
    {
        if (items == null)
        {
            return;
        }

        var wrapper = doc.CreateElement(wrapperName);
        var index = 0;
        foreach (var item in items)
        {
            if (item == null)
            {
                index++;
                continue;
            }

            var child = doc.CreateElement(itemName);
            WriteIdentityAttributes(child, item, index);
            ExportObject(doc, child, item, options);
            ExportKnownChildren(doc, child, item, options);
            wrapper.AppendChild(child);
            index++;
        }

        if (wrapper.HasChildNodes)
        {
            parent.AppendChild(wrapper);
        }
    }

    private static void ExportNamedObject(XmlDocument doc, XmlElement parent, string elementName, object? target, RibbonTranslationOptions options)
    {
        if (target == null)
        {
            return;
        }

        var element = doc.CreateElement(elementName);
        ExportObject(doc, element, target, options);
        ExportKnownChildren(doc, element, target, options);
        if (element.HasChildNodes || element.HasAttributes)
        {
            parent.AppendChild(element);
        }
    }

    private static void ExportKnownChildren(XmlDocument doc, XmlElement parent, object target, RibbonTranslationOptions options)
    {
        switch (target)
        {
            case KryptonRibbonTab tab:
                ExportCollection(doc, parent, @"Groups", @"Group", tab.Groups, options);
                break;
            case KryptonRibbonGroup group:
                ExportCollection(doc, parent, @"Items", @"Item", group.Items, options);
                break;
            case KryptonRibbonGroupContainer container:
                var children = container.GetChildComponents();
                if (children.Length > 0)
                {
                    ExportCollection(doc, parent, @"Items", @"Item", children, options);
                }

                if (container is KryptonRibbonGroupGallery gallery)
                {
                    ExportCollection(doc, parent, @"DropButtonRanges", @"Range", gallery.DropButtonRanges, options);
                }

                break;
            case KryptonContextMenuItemBase menuItem:
                ExportMenuChildren(doc, parent, menuItem, options);
                break;
            case KryptonContextMenu contextMenu:
                ExportCollection(doc, parent, @"Items", @"Item", contextMenu.Items, options);
                break;
        }

        ExportNestedToolTips(doc, parent, target, options);
        ExportNestedButtonSpecs(doc, parent, target, options);
        ExportNestedContextMenu(doc, parent, target, options);
    }

    private static void ExportMenuChildren(XmlDocument doc, XmlElement parent, KryptonContextMenuItemBase menuItem, RibbonTranslationOptions options)
    {
        var itemsProp = menuItem.GetType().GetProperty(@"Items", BindingFlags.Instance | BindingFlags.Public);
        if (itemsProp?.GetValue(menuItem, null) is IEnumerable nested)
        {
            ExportCollection(doc, parent, @"Items", @"Item", nested, options);
        }
    }

    private static void ExportNestedToolTips(XmlDocument doc, XmlElement parent, object target, RibbonTranslationOptions options)
    {
        if (!options.IncludeToolTips)
        {
            return;
        }

        var prop = target.GetType().GetProperty(@"ToolTipValues", BindingFlags.Instance | BindingFlags.Public);
        var values = prop?.GetValue(target, null);
        if (values != null)
        {
            ExportNamedObject(doc, parent, @"ToolTipValues", values, options);
        }
    }

    private static void ExportNestedButtonSpecs(XmlDocument doc, XmlElement parent, object target, RibbonTranslationOptions options)
    {
        if (ReferenceEquals(target, target as KryptonRibbon) || target is RibbonFileAppButton)
        {
            return;
        }

        var prop = target.GetType().GetProperty(@"ButtonSpecs", BindingFlags.Instance | BindingFlags.Public);
        if (prop?.GetValue(target, null) is IEnumerable specs)
        {
            ExportCollection(doc, parent, @"ButtonSpecs", @"Spec", specs, options);
        }
    }

    private static void ExportNestedContextMenu(XmlDocument doc, XmlElement parent, object target, RibbonTranslationOptions options)
    {
        var prop = target.GetType().GetProperty(@"KryptonContextMenu", BindingFlags.Instance | BindingFlags.Public);
        if (prop?.GetValue(target, null) is KryptonContextMenu menu)
        {
            ExportNamedObject(doc, parent, @"KryptonContextMenu", menu, options);
        }
    }

    private static void ExportObject(XmlDocument doc, XmlElement parent, object target, RibbonTranslationOptions options)
    {
        foreach (var prop in GetStringProperties(target, options))
        {
            var value = prop.GetValue(target, null) as string;
            if (!options.IncludeDefaults && IsDefaultStringProperty(prop, value))
            {
                continue;
            }

            parent.AppendChild(CreateValueElement(doc, prop.Name, value));
        }

        var textsProp = target.GetType().GetProperty(nameof(KryptonRibbonNotificationBarData.ActionButtonTexts),
            BindingFlags.Instance | BindingFlags.Public);
        if (textsProp?.PropertyType == typeof(string[]) &&
            textsProp.GetValue(target, null) is string[] texts &&
            texts.Length > 0)
        {
            var wrapper = doc.CreateElement(textsProp.Name);
            for (var i = 0; i < texts.Length; i++)
            {
                var item = CreateValueElement(doc, @"Item", texts[i]);
                item.SetAttribute(IndexAttribute, i.ToString(CultureInfo.InvariantCulture));
                wrapper.AppendChild(item);
            }

            parent.AppendChild(wrapper);
        }
    }

    #endregion

    #region Tree import

    private static void ImportRibbonChildren(XmlElement parent, KryptonRibbon ribbon, RibbonTranslationOptions options)
    {
        ImportNamedObject(parent, @"FileAppTab", ribbon.RibbonFileAppTab, options);
        ImportFileAppButton(parent, ribbon.RibbonFileAppButton, options);
        ImportCollection(parent, @"ButtonSpecs", @"Spec", ribbon.ButtonSpecs, options);
        ImportCollection(parent, @"QATButtons", @"QATButton", ribbon.QATButtons, options);
        ImportCollection(parent, @"Contexts", @"Context", ribbon.RibbonContexts, options);
        ImportNamedObject(parent, @"NotificationBar", ribbon.NotificationBar, options);
        ImportCollection(parent, @"Tabs", @"Tab", ribbon.RibbonTabs, options);

        var backstage = ribbon.RibbonFileAppTab.BackstageView;
        var backstageElement = parent.SelectSingleNode(@"Backstage") as XmlElement;
        if (backstage != null && backstageElement != null)
        {
            ImportCollection(backstageElement, @"Pages", @"Page", backstage.Pages, options);
            ImportCollection(backstageElement, @"Commands", @"Command", backstage.Commands, options);
        }
    }

    private static void ImportFileAppButton(XmlElement parent, RibbonFileAppButton button, RibbonTranslationOptions options)
    {
        var element = parent.SelectSingleNode(@"FileAppButton") as XmlElement;
        if (element == null)
        {
            return;
        }

        ImportObject(element, button, options);
        ImportCollection(element, @"MenuItems", @"Item", button.AppButtonMenuItems, options);
        ImportCollection(element, @"RecentDocs", @"RecentDoc", button.AppButtonRecentDocs, options);
        ImportCollection(element, @"AppButtonSpecs", @"Spec", button.AppButtonSpecs, options);
    }

    private static void ImportCollection(XmlElement parent, string wrapperName, string itemName, IEnumerable? items, RibbonTranslationOptions options)
    {
        if (items == null)
        {
            return;
        }

        var wrapper = parent.SelectSingleNode(wrapperName) as XmlElement;
        if (wrapper == null)
        {
            return;
        }

        var live = ToList(items);
        foreach (XmlNode node in wrapper.ChildNodes)
        {
            var xml = node as XmlElement;
            if (xml == null)
            {
                continue;
            }

            if (!string.Equals(xml.Name, itemName, StringComparison.Ordinal) &&
                !string.Equals(xml.Name, @"Item", StringComparison.Ordinal))
            {
                continue;
            }

            var match = MatchChild(live, xml);
            if (match == null)
            {
                continue;
            }

            ImportObject(xml, match, options);
            ImportKnownChildren(xml, match, options);
        }
    }

    private static void ImportNamedObject(XmlElement parent, string elementName, object? target, RibbonTranslationOptions options)
    {
        if (target == null)
        {
            return;
        }

        var element = parent.SelectSingleNode(elementName) as XmlElement;
        if (element == null)
        {
            return;
        }

        ImportObject(element, target, options);
        ImportKnownChildren(element, target, options);
    }

    private static void ImportKnownChildren(XmlElement parent, object target, RibbonTranslationOptions options)
    {
        switch (target)
        {
            case KryptonRibbonTab tab:
                ImportCollection(parent, @"Groups", @"Group", tab.Groups, options);
                break;
            case KryptonRibbonGroup group:
                ImportCollection(parent, @"Items", @"Item", group.Items, options);
                break;
            case KryptonRibbonGroupContainer container:
                ImportCollection(parent, @"Items", @"Item", container.GetChildComponents(), options);
                if (container is KryptonRibbonGroupGallery gallery)
                {
                    ImportCollection(parent, @"DropButtonRanges", @"Range", gallery.DropButtonRanges, options);
                }

                break;
            case KryptonContextMenuItemBase menuItem:
                var itemsProp = menuItem.GetType().GetProperty(@"Items", BindingFlags.Instance | BindingFlags.Public);
                if (itemsProp?.GetValue(menuItem, null) is IEnumerable nested)
                {
                    ImportCollection(parent, @"Items", @"Item", nested, options);
                }

                break;
            case KryptonContextMenu contextMenu:
                ImportCollection(parent, @"Items", @"Item", contextMenu.Items, options);
                break;
        }

        if (options.IncludeToolTips)
        {
            var tipProp = target.GetType().GetProperty(@"ToolTipValues", BindingFlags.Instance | BindingFlags.Public);
            var values = tipProp?.GetValue(target, null);
            if (values != null)
            {
                ImportNamedObject(parent, @"ToolTipValues", values, options);
            }
        }

        var specsProp = target.GetType().GetProperty(@"ButtonSpecs", BindingFlags.Instance | BindingFlags.Public);
        if (specsProp?.GetValue(target, null) is IEnumerable specs && !(target is KryptonRibbon) && !(target is RibbonFileAppButton))
        {
            ImportCollection(parent, @"ButtonSpecs", @"Spec", specs, options);
        }

        var menuProp = target.GetType().GetProperty(@"KryptonContextMenu", BindingFlags.Instance | BindingFlags.Public);
        if (menuProp?.GetValue(target, null) is KryptonContextMenu menu)
        {
            ImportNamedObject(parent, @"KryptonContextMenu", menu, options);
        }
    }

    private static void ImportObject(XmlElement parent, object target, RibbonTranslationOptions options)
    {
        foreach (var prop in GetStringProperties(target, options))
        {
            var child = parent.SelectSingleNode(prop.Name) as XmlElement;
            if (child == null || !prop.CanWrite)
            {
                continue;
            }

            var value = ReadValue(child);
            AssignString(target, prop, value, options);
        }

        var textsProp = target.GetType().GetProperty(nameof(KryptonRibbonNotificationBarData.ActionButtonTexts),
            BindingFlags.Instance | BindingFlags.Public);
        var textsElement = parent.SelectSingleNode(nameof(KryptonRibbonNotificationBarData.ActionButtonTexts)) as XmlElement;
        if (textsProp != null && textsProp.CanWrite && textsElement != null)
        {
            var values = new List<string>();
            foreach (XmlNode node in textsElement.ChildNodes)
            {
                if (node is XmlElement item)
                {
                    values.Add(ReadValue(item) ?? string.Empty);
                }
            }

            AssignValue(target, textsProp, values.ToArray(), options);
        }
    }

    private static void ResetExportableStrings(KryptonRibbon ribbon, RibbonTranslationOptions options)
    {
        ResetObject(ribbon, options);
        ResetObject(ribbon.RibbonFileAppTab, options);
        ResetObject(ribbon.RibbonFileAppButton, options);
        ResetObject(ribbon.NotificationBar, options);
        foreach (var item in FlattenRibbon(ribbon))
        {
            ResetObject(item, options);
        }
    }

    private static void ResetObject(object target, RibbonTranslationOptions options)
    {
        foreach (var prop in GetStringProperties(target, options))
        {
            var reset = target.GetType().GetMethod(@"Reset" + prop.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (reset != null && reset.GetParameters().Length == 0)
            {
                reset.Invoke(target, null);
                continue;
            }

            if (prop.CanWrite)
            {
                var defaultAttr = prop.GetCustomAttribute<DefaultValueAttribute>(inherit: false);
                if (defaultAttr != null)
                {
                    AssignString(target, prop, defaultAttr.Value as string ?? string.Empty, options);
                }
            }
        }
    }

    #endregion

    #region Property helpers

    private static IEnumerable<PropertyInfo> GetStringProperties(object target, RibbonTranslationOptions options)
    {
        var type = target.GetType();
        foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            if (!prop.CanRead || prop.GetIndexParameters().Length != 0)
            {
                continue;
            }

            if (prop.PropertyType != typeof(string))
            {
                continue;
            }

            if (IdentityPropertyNames.Contains(prop.Name))
            {
                continue;
            }

            var localizable = prop.GetCustomAttribute<LocalizableAttribute>(inherit: true);
            if (localizable?.IsLocalizable != true)
            {
                continue;
            }

            if (!options.IncludeKeyTips && IsKeyTipProperty(prop.Name))
            {
                continue;
            }

            if (!options.IncludeToolTips && IsToolTipProperty(prop.Name, type))
            {
                continue;
            }

            if (!options.IncludeContentStrings && IsContentProperty(prop.Name, type))
            {
                continue;
            }

            yield return prop;
        }
    }

    private static bool IsKeyTipProperty(string name) =>
        name.IndexOf(@"KeyTip", StringComparison.Ordinal) >= 0;

    private static bool IsToolTipProperty(string name, Type type)
    {
        if (string.Equals(name, @"ToolTipTitle", StringComparison.Ordinal) ||
            string.Equals(name, @"ToolTipBody", StringComparison.Ordinal) ||
            string.Equals(name, @"AppButtonToolTipTitle", StringComparison.Ordinal) ||
            string.Equals(name, @"AppButtonToolTipBody", StringComparison.Ordinal))
        {
            return true;
        }

        if (typeof(HeaderValuesBase).IsAssignableFrom(type) &&
            (string.Equals(name, @"Heading", StringComparison.Ordinal) ||
             string.Equals(name, @"Description", StringComparison.Ordinal)))
        {
            return true;
        }

        return false;
    }

    private static bool IsContentProperty(string name, Type type) =>
        ContentTypeNames.Contains(type.Name) && ContentPropertyNames.Contains(name);

    private static bool IsDefaultStringProperty(PropertyInfo prop, string? value)
    {
        var defaultAttr = prop.GetCustomAttribute<DefaultValueAttribute>(inherit: false);
        if (defaultAttr == null)
        {
            return false;
        }

        if (defaultAttr.Value == null)
        {
            return value == null;
        }

        var defaultString = defaultAttr.Value as string;
        return defaultString != null && string.Equals(value, defaultString, StringComparison.Ordinal);
    }

    private static void AssignString(object target, PropertyInfo prop, string? value, RibbonTranslationOptions options)
    {
        AssignValue(target, prop, value ?? string.Empty, options);

        var commandProp = target.GetType().GetProperty(@"KryptonCommand", BindingFlags.Instance | BindingFlags.Public);
        if (commandProp?.GetValue(target, null) is KryptonCommand command)
        {
            var commandString = command.GetType().GetProperty(prop.Name, BindingFlags.Instance | BindingFlags.Public);
            if (commandString != null && commandString.CanWrite && commandString.PropertyType == typeof(string))
            {
                AssignValue(command, commandString, value ?? string.Empty, options);
            }
        }
    }

    private static void AssignValue(object target, PropertyInfo prop, object? value, RibbonTranslationOptions options)
    {
        object? oldValue = null;
        try
        {
            oldValue = prop.GetValue(target, null);
        }
        catch (Exception)
        {
            // Some properties throw when uninitialized; still attempt the write.
        }

        if (Equals(oldValue, value))
        {
            return;
        }

        var component = target as IComponent;
        var descriptor = TypeDescriptor.GetProperties(target)[prop.Name];
        if (options.ChangeService != null && component != null && descriptor != null)
        {
            options.ChangeService.OnComponentChanging(component, descriptor);
        }

        if (descriptor != null && descriptor.IsReadOnly == false)
        {
            descriptor.SetValue(target, value);
        }
        else if (prop.CanWrite)
        {
            prop.SetValue(target, value, null);
        }

        if (options.ChangeService != null && component != null && descriptor != null)
        {
            options.ChangeService.OnComponentChanged(component, descriptor, oldValue, value);
        }
    }

    #endregion

    #region Identity

    private static void WriteIdentityAttributes(XmlElement element, object target, int index)
    {
        var translationId = GetTranslationId(target);
        if (!string.IsNullOrEmpty(translationId))
        {
            element.SetAttribute(TranslationIdAttribute, translationId);
        }

        var siteName = GetSiteName(target);
        if (!string.IsNullOrEmpty(siteName))
        {
            element.SetAttribute(NameAttribute, siteName);
        }

        element.SetAttribute(TypeAttribute, target.GetType().Name);
        if (index >= 0)
        {
            element.SetAttribute(IndexAttribute, index.ToString(CultureInfo.InvariantCulture));
        }

        var unique = GetUniqueName(target);
        if (!string.IsNullOrEmpty(unique))
        {
            element.SetAttribute(UniqueNameAttribute, unique);
        }

        var contextName = GetContextName(target);
        if (!string.IsNullOrEmpty(contextName))
        {
            element.SetAttribute(ContextNameAttribute, contextName);
        }
    }

    private static object? MatchChild(IList live, XmlElement xml)
    {
        var translationId = xml.GetAttribute(TranslationIdAttribute);
        if (!string.IsNullOrEmpty(translationId))
        {
            foreach (var item in live)
            {
                if (item != null && string.Equals(GetTranslationId(item), translationId, StringComparison.Ordinal))
                {
                    return item;
                }
            }
        }

        var unique = xml.GetAttribute(UniqueNameAttribute);
        if (!string.IsNullOrEmpty(unique))
        {
            foreach (var item in live)
            {
                if (item != null && string.Equals(GetUniqueName(item), unique, StringComparison.Ordinal))
                {
                    return item;
                }
            }
        }

        var contextName = xml.GetAttribute(ContextNameAttribute);
        if (!string.IsNullOrEmpty(contextName))
        {
            foreach (var item in live)
            {
                if (item != null && string.Equals(GetContextName(item), contextName, StringComparison.Ordinal))
                {
                    return item;
                }
            }
        }

        var name = xml.GetAttribute(NameAttribute);
        if (!string.IsNullOrEmpty(name))
        {
            foreach (var item in live)
            {
                if (item != null && string.Equals(GetSiteName(item), name, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }
        }

        var type = xml.GetAttribute(TypeAttribute);
        if (int.TryParse(xml.GetAttribute(IndexAttribute), NumberStyles.Integer, CultureInfo.InvariantCulture, out var index) &&
            index >= 0 && index < live.Count)
        {
            var candidate = live[index];
            if (candidate != null &&
                (string.IsNullOrEmpty(type) || string.Equals(candidate.GetType().Name, type, StringComparison.Ordinal)))
            {
                return candidate;
            }
        }

        return null;
    }

    private static string GetTranslationId(object target) =>
        (target as IRibbonTranslationIdentity)?.TranslationId ?? string.Empty;

    private static string GetSiteName(object target)
    {
        if (target is Control control && !string.IsNullOrEmpty(control.Name))
        {
            return control.Name;
        }

        return (target as IComponent)?.Site?.Name ?? string.Empty;
    }

    private static string GetUniqueName(object target)
    {
        var prop = target.GetType().GetProperty(UniqueNameAttribute, BindingFlags.Instance | BindingFlags.Public);
        return prop?.GetValue(target, null) as string ?? string.Empty;
    }

    private static string GetContextName(object target) =>
        (target as KryptonRibbonContext)?.ContextName ?? string.Empty;

    #endregion

    #region XML helpers

    private static XmlElement CreateValueElement(XmlDocument doc, string name, string? value)
    {
        var element = doc.CreateElement(name);
        if (value == null)
        {
            element.SetAttribute(IsNullAttribute, @"true");
            element.SetAttribute(ValueAttribute, string.Empty);
        }
        else
        {
            element.SetAttribute(ValueAttribute, value);
        }

        return element;
    }

    private static string? ReadValue(XmlElement element)
    {
        if (string.Equals(element.GetAttribute(IsNullAttribute), @"true", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        return element.HasAttribute(ValueAttribute) ? element.GetAttribute(ValueAttribute) : element.InnerText;
    }

    private static void CollectValueKeys(XmlNode? node, string prefix, ISet<string> keys)
    {
        if (node == null)
        {
            return;
        }

        if (node is XmlElement element && element.HasAttribute(ValueAttribute))
        {
            var path = string.IsNullOrEmpty(prefix) ? element.Name : prefix + @"." + element.Name;
            var id = element.GetAttribute(TranslationIdAttribute);
            if (string.IsNullOrEmpty(id))
            {
                id = element.GetAttribute(IndexAttribute);
            }

            if (!string.IsNullOrEmpty(id) && string.Equals(element.Name, @"Item", StringComparison.Ordinal)
                || element.Name == @"Tab" || element.Name == @"Group" || element.Name == @"QATButton"
                || element.Name == @"Context" || element.Name == @"RecentDoc" || element.Name == @"Spec"
                || element.Name == @"Range" || element.Name == @"Page" || element.Name == @"Command")
            {
                // Structural nodes with Value are rare; still record the leaf path below.
            }

            keys.Add(path);
        }

        var nextPrefix = prefix;
        if (node is XmlElement named &&
            !named.HasAttribute(ValueAttribute) &&
            named.Name != RootElementName)
        {
            var token = named.Name;
            var id = named.GetAttribute(TranslationIdAttribute);
            if (string.IsNullOrEmpty(id))
            {
                id = named.GetAttribute(IndexAttribute);
            }

            if (!string.IsNullOrEmpty(id))
            {
                token = token + @"[" + id + @"]";
            }

            nextPrefix = string.IsNullOrEmpty(prefix) ? token : prefix + @"." + token;
        }

        foreach (XmlNode child in node.ChildNodes)
        {
            CollectValueKeys(child, nextPrefix, keys);
        }
    }

    private static IEnumerable<object> FlattenRibbon(KryptonRibbon ribbon)
    {
        foreach (var spec in ribbon.ButtonSpecs)
        {
            yield return spec;
        }

        foreach (var button in ribbon.QATButtons)
        {
            yield return button;
        }

        foreach (var context in ribbon.RibbonContexts)
        {
            yield return context;
        }

        foreach (var item in ribbon.RibbonFileAppButton.AppButtonMenuItems)
        {
            yield return item;
        }

        foreach (var docItem in ribbon.RibbonFileAppButton.AppButtonRecentDocs)
        {
            yield return docItem;
        }

        foreach (var spec in ribbon.RibbonFileAppButton.AppButtonSpecs)
        {
            yield return spec;
        }

        foreach (KryptonRibbonTab tab in ribbon.RibbonTabs)
        {
            yield return tab;
            foreach (KryptonRibbonGroup group in tab.Groups)
            {
                yield return group;
                foreach (var nested in FlattenGroupItems(group.Items))
                {
                    yield return nested;
                }
            }
        }
    }

    private static IEnumerable<object> FlattenGroupItems(IEnumerable items)
    {
        foreach (var item in items)
        {
            if (item == null)
            {
                continue;
            }

            yield return item;
            if (item is KryptonRibbonGroupContainer container)
            {
                foreach (var child in FlattenGroupItems(container.GetChildComponents()))
                {
                    yield return child;
                }
            }
        }
    }

    private static IList ToList(IEnumerable items)
    {
        if (items is IList list)
        {
            return list;
        }

        var copy = new ArrayList();
        foreach (var item in items)
        {
            copy.Add(item);
        }

        return copy;
    }

    private static RibbonTranslationOptions CloneOptions(RibbonTranslationOptions options) =>
        new RibbonTranslationOptions
        {
            IncludeDefaults = options.IncludeDefaults,
            IncludeChrome = options.IncludeChrome,
            IncludeToolTips = options.IncludeToolTips,
            IncludeKeyTips = options.IncludeKeyTips,
            IncludeContentStrings = options.IncludeContentStrings,
            ResetFirst = options.ResetFirst,
            ChangeService = options.ChangeService
        };

    private static void WarnOnVersion(XmlElement root)
    {
        var versionText = root.GetAttribute(VersionAttribute);
        if (int.TryParse(versionText, NumberStyles.Integer, CultureInfo.InvariantCulture, out var fileVersion))
        {
            if (fileVersion < CurrentSupportedVersion)
            {
                Debug.WriteLine(
                    $@"[Krypton] RibbonTranslations.xml format version '{fileVersion}' is older than supported structural version {CurrentSupportedVersion}. Import will continue best-effort.");
            }
            else if (fileVersion > CurrentSupportedVersion)
            {
                Debug.WriteLine(
                    $@"[Krypton] RibbonTranslations.xml format version '{fileVersion}' is newer than this toolkit ({CurrentSupportedVersion}). Unknown structure may be ignored.");
            }
        }
        else if (!string.IsNullOrWhiteSpace(versionText))
        {
            Debug.WriteLine($@"[Krypton] RibbonTranslations.xml has unrecognised Version '{versionText}'. Import will continue best-effort.");
        }
    }

    private static void WarnOnCulture(XmlElement root)
    {
        var fileCulture = root.GetAttribute(CultureAttribute);
        var currentCulture = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
        if (!string.IsNullOrEmpty(fileCulture) &&
            !string.Equals(fileCulture, currentCulture, StringComparison.OrdinalIgnoreCase))
        {
            Debug.WriteLine(
                $@"[Krypton] RibbonTranslations.xml was created for culture '{fileCulture}' but the current UI culture is '{currentCulture}'. Strings may not display correctly.");
        }
    }

    private static void WarnOnRibbonName(XmlElement root, KryptonRibbon ribbon)
    {
        var fileName = root.GetAttribute(RibbonNameAttribute);
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return;
        }

        var key = ResolveRibbonKey(ribbon);
        if (!string.Equals(fileName, key, StringComparison.OrdinalIgnoreCase))
        {
            Debug.WriteLine(
                $@"[Krypton] RibbonTranslations.xml RibbonName '{fileName}' does not match ribbon '{key}'. Import will continue overlay matching.");
        }
    }

    private static void ReportCoverage(KryptonRibbon ribbon, XmlDocument doc, RibbonTranslationOptions options, string? filePath)
    {
        try
        {
            var coverage = Analyze(ribbon, doc, options, filePath);
            if (coverage.HasMissing || coverage.HasExtra)
            {
                Debug.WriteLine($@"[Krypton] Ribbon translations coverage: {coverage}.");
            }

            ribbon.RaiseTranslationsCoverageReported(coverage);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($@"[Krypton] Ribbon translations coverage analysis failed: {ex.Message}");
        }
    }

    private static string? NullIfEmpty(string value) => string.IsNullOrWhiteSpace(value) ? null : value;

    #endregion
}
