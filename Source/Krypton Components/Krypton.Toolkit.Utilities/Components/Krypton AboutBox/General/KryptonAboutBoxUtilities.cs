#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2023 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>This class does the heavy lifting for <see cref="VisualAboutBoxForm"/> and its associated components.</summary>
internal static class KryptonAboutBoxUtilities
{
    #region Implementation

    internal readonly struct AssemblyIdentity
    {
        public AssemblyIdentity(string applicationName, string version, string copyright, string company, string description)
        {
            ApplicationName = applicationName;
            Version = version;
            Copyright = copyright;
            Company = company;
            Description = description;
        }

        public string ApplicationName { get; }
        public string Version { get; }
        public string Copyright { get; }
        public string Company { get; }
        public string Description { get; }
    }

    public static KryptonAboutBoxData CreateDataFromAssembly(Assembly assembly)
    {
        AssemblyIdentity identity = GetAssemblyIdentity(assembly, default);
        return new KryptonAboutBoxData
        {
            CurrentAssembly = assembly,
            ApplicationName = identity.ApplicationName
        };
    }

    public static Assembly ResolveAssembly(KryptonAboutBoxData aboutBoxData) =>
        aboutBoxData.CurrentAssembly ?? Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();

    public static AssemblyIdentity GetAssemblyIdentity(Assembly assembly, KryptonAboutBoxData data)
    {
        FileVersionInfo? info = TryGetFileVersionInfo(assembly);
        NameValueCollection attribs = AssemblyAttribs(assembly);
        Version? assemblyVersion = assembly.GetName().Version;

        return new AssemblyIdentity(
            FirstNonEmpty(data.ApplicationName, info?.ProductName, attribs["Product"], attribs["Title"], assembly.GetName().Name),
            FirstNonEmpty(data.Version, info?.ProductVersion, info?.FileVersion, attribs["InformationalVersion"], attribs["Version"], assemblyVersion?.ToString()),
            FirstNonEmpty(data.Copyright, info?.LegalCopyright, attribs["Copyright"]),
            FirstNonEmpty(data.Company, info?.CompanyName, attribs["Company"]),
            FirstNonEmpty(data.Description, info?.Comments, info?.FileDescription, attribs["Description"]));
    }

    public static DateTime AssemblyLastWriteTime(Assembly assembly)
    {
        string location = TryGetLocation(assembly);
        if (string.IsNullOrEmpty(location))
        {
            return DateTime.MaxValue;
        }

        try
        {
            return File.GetLastWriteTime(location);
        }
        catch
        {
            return DateTime.MaxValue;
        }
    }

    public static DateTime AssemblyBuildDate(Assembly? assembly, bool forceFileDate)
    {
        if (assembly == null)
        {
            return DateTime.Now;
        }

        Version assemblyVersion = assembly.GetName().Version!;
        DateTime dateTime;

        if (forceFileDate)
        {
            dateTime = AssemblyLastWriteTime(assembly);
        }
        else
        {
            dateTime = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Unspecified)
                .AddDays(assemblyVersion.Build)
                .AddSeconds(assemblyVersion.Revision * 2);

            if (TimeZoneInfo.Local.IsDaylightSavingTime(dateTime))
            {
                dateTime = dateTime.AddHours(1);
            }

            if (dateTime > DateTime.Now || assemblyVersion.Build < 730 || assemblyVersion.Revision == 0)
            {
                dateTime = AssemblyLastWriteTime(assembly);
            }
        }

        return dateTime;
    }

    /// <summary>
    /// Returns the binary compile timestamp.
    /// Uses the PE COFF <c>TimeDateStamp</c> only when it is a plausible calendar date;
    /// deterministic SDK builds store a content hash there, which must not be shown as a date.
    /// Otherwise the assembly file last-write time is used.
    /// </summary>
    public static DateTime GetBinaryBuildDateTime(Assembly assembly)
    {
        DateTime? pe = TryReadPeTimeDateStamp(assembly);
        return pe ?? AssemblyLastWriteTime(assembly);
    }

    public static DateTime? TryReadPeTimeDateStamp(Assembly assembly)
    {
        string location = TryGetLocation(assembly);
        if (string.IsNullOrEmpty(location) || !File.Exists(location))
        {
            return null;
        }

        try
        {
            using (var stream = new FileStream(location, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new BinaryReader(stream))
            {
                if (stream.Length < 0x40)
                {
                    return null;
                }

                if (reader.ReadUInt16() != 0x5A4D)
                {
                    return null;
                }

                stream.Seek(0x3C, SeekOrigin.Begin);
                int lfanew = reader.ReadInt32();
                if (lfanew < 0 || (long)lfanew + 12 > stream.Length)
                {
                    return null;
                }

                stream.Seek(lfanew, SeekOrigin.Begin);
                if (reader.ReadUInt32() != 0x00004550)
                {
                    return null;
                }

                stream.Seek(lfanew + 8, SeekOrigin.Begin);
                uint stamp = reader.ReadUInt32();
                if (stamp == 0 || stamp == uint.MaxValue)
                {
                    return null;
                }

                // PE TimeDateStamp is Unix seconds. Deterministic builds put a hash here, not a time.
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                DateTime peUtc = epoch.AddSeconds(stamp);
                DateTime minUtc = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                if (peUtc < minUtc || peUtc > DateTime.UtcNow.AddHours(12))
                {
                    return null;
                }

                return peUtc.ToLocalTime();
            }
        }
        catch
        {
            return null;
        }
    }

    public static string FormatBuildDate(DateTime dateTime, bool useFull) =>
        dateTime == DateTime.MaxValue
            ? string.Empty
            : dateTime.ToString(useFull ? "F" : "G", CultureInfo.CurrentCulture);

    public static string FormatBuildAndBinaryDates(Assembly assembly, bool useFull)
    {
        string buildDateText = FormatBuildDate(AssemblyBuildDate(assembly, false), useFull);
        string binaryText = FormatBuildDate(GetBinaryBuildDateTime(assembly), useFull);
        var strings = KryptonManager.Strings.AboutBoxStrings;
        var lines = new List<string>();
        if (!string.IsNullOrEmpty(buildDateText))
        {
            lines.Add($"{strings.BuildDate}: {buildDateText}");
        }

        if (!string.IsNullOrEmpty(binaryText))
        {
            lines.Add($"{strings.BinaryBuildDate}: {binaryText}");
        }

        return string.Join("\r\n", lines);
    }

    public static LinkArea ResolveLinkArea(string text, LinkArea requested, LinkArea defaultArea, string? autoFragment)
    {
        if (string.IsNullOrEmpty(text))
        {
            return new LinkArea(0, 0);
        }

        bool isCustom = requested.Start != defaultArea.Start || requested.Length != defaultArea.Length;
        if (isCustom && requested.Start >= 0 && requested.Length > 0 && requested.Start + requested.Length <= text.Length)
        {
            return requested;
        }

        string fragment = autoFragment ?? string.Empty;
        if (fragment.Length > 0)
        {
            int index = text.IndexOf(fragment, StringComparison.CurrentCulture);
            if (index >= 0)
            {
                return new LinkArea(index, fragment.Length);
            }
        }

        return new LinkArea(0, text.Length);
    }

    public static NameValueCollection AssemblyAttribs(Assembly assembly)
    {
        var nvc = new NameValueCollection();
        var r = new Regex(@"(\.Assembly|\.)(?<Name>[^.]*)Attribute$", RegexOptions.IgnoreCase);

        foreach (var attrib in assembly.GetCustomAttributes(false))
        {
            var typeName = attrib.GetType().ToString();
            var name = r.Match(typeName).Groups["Name"].ToString();
            var value = string.Empty;
            switch (typeName)
            {
                case "System.CLSCompliantAttribute":
                    value = ((CLSCompliantAttribute)attrib).IsCompliant.ToString();
                    break;
                case "System.Diagnostics.DebuggableAttribute":
                    value = ((DebuggableAttribute)attrib).IsJITTrackingEnabled.ToString();
                    break;
                case "System.Reflection.AssemblyCompanyAttribute":
                    value = ((AssemblyCompanyAttribute)attrib).Company;
                    break;
                case "System.Reflection.AssemblyConfigurationAttribute":
                    value = ((AssemblyConfigurationAttribute)attrib).Configuration;
                    break;
                case "System.Reflection.AssemblyCopyrightAttribute":
                    value = ((AssemblyCopyrightAttribute)attrib).Copyright;
                    break;
                case "System.Reflection.AssemblyDefaultAliasAttribute":
                    value = ((AssemblyDefaultAliasAttribute)attrib).DefaultAlias;
                    break;
                case "System.Reflection.AssemblyDelaySignAttribute":
                    value = ((AssemblyDelaySignAttribute)attrib).DelaySign.ToString();
                    break;
                case "System.Reflection.AssemblyDescriptionAttribute":
                    value = ((AssemblyDescriptionAttribute)attrib).Description;
                    break;
                case "System.Reflection.AssemblyInformationalVersionAttribute":
                    value = ((AssemblyInformationalVersionAttribute)attrib).InformationalVersion;
                    break;
                case "System.Reflection.AssemblyKeyFileAttribute":
                    value = ((AssemblyKeyFileAttribute)attrib).KeyFile;
                    break;
                case "System.Reflection.AssemblyProductAttribute":
                    value = ((AssemblyProductAttribute)attrib).Product;
                    break;
                case "System.Reflection.AssemblyTrademarkAttribute":
                    value = ((AssemblyTrademarkAttribute)attrib).Trademark;
                    break;
                case "System.Reflection.AssemblyTitleAttribute":
                    value = ((AssemblyTitleAttribute)attrib).Title;
                    break;
                case "System.Resources.NeutralResourcesLanguageAttribute":
                    value = ((NeutralResourcesLanguageAttribute)attrib).CultureName;
                    break;
                case "System.Resources.SatelliteContractVersionAttribute":
                    value = ((SatelliteContractVersionAttribute)attrib).Version;
                    break;
                case "System.Runtime.InteropServices.ComCompatibleVersionAttribute":
                {
                    ComCompatibleVersionAttribute x = (ComCompatibleVersionAttribute)attrib;
                    value = $"{x.MajorVersion}.{x.MinorVersion}.{x.RevisionNumber}.{x.BuildNumber}";
                    break;
                }
                case "System.Runtime.InteropServices.ComVisibleAttribute":
                    value = ((ComVisibleAttribute)attrib).Value.ToString();
                    break;
                case "System.Runtime.InteropServices.GuidAttribute":
                    value = ((GuidAttribute)attrib).Value;
                    break;
                case "System.Runtime.InteropServices.TypeLibVersionAttribute":
                {
                    TypeLibVersionAttribute x = (TypeLibVersionAttribute)attrib;
                    value = $"{x.MajorVersion}.{x.MinorVersion}";
                    break;
                }
                case "System.Security.AllowPartiallyTrustedCallersAttribute":
                    value = "(Present)";
                    break;
                default:
                    value = typeName;
                    break;
            }

            if (nvc[name] == null)
            {
                nvc.Add(name, value);
            }
        }

        try
        {
            nvc.Add("CodeBase", TryGetLocation(assembly).Replace("file:///", ""));
        }
        catch (NotSupportedException)
        {
            nvc.Add("CodeBase", "(not supported)");
        }

        var dt = AssemblyBuildDate(assembly, false);
        nvc.Add("BuildDate", dt == DateTime.MaxValue ? "(unknown)" : FormatBuildDate(dt, false));
        DateTime binary = GetBinaryBuildDateTime(assembly);
        nvc.Add("BinaryBuildDate", binary == DateTime.MaxValue ? "(unknown)" : FormatBuildDate(binary, false));

        try
        {
            nvc.Add("Location", TryGetLocation(assembly));
        }
        catch (NotSupportedException)
        {
            nvc.Add("Location", "(not supported)");
        }

        try
        {
            Version? version = assembly.GetName().Version;
            if (version == null || (version.Major == 0 && version.Minor == 0))
            {
                nvc.Add("Version", "(unknown)");
            }
            else
            {
                nvc.Add("Version", version.ToString());
            }
        }
        catch (Exception)
        {
            nvc.Add("Version", "(unknown)");
        }

        nvc.Add("FullName", assembly.FullName);

        return nvc;
    }

    /// <summary>
    /// Shows the Krypton System Information window (msinfo32-style), owned by <paramref name="owner"/> when provided.
    /// When <paramref name="trigger"/> is set, it is disabled until the viewer closes.
    /// </summary>
    public static void LaunchSystemInformation(IWin32Window? owner = null, Control? trigger = null)
    {
        if (trigger != null)
        {
            trigger.Enabled = false;
        }

        var form = KryptonSystemInformation.Show(owner);
        if (trigger == null)
        {
            return;
        }

        form.FormClosed += (_, __) =>
        {
            if (!trigger.IsDisposed)
            {
                trigger.Enabled = true;
            }
        };
    }

    public static void PopulateAssemblyDetails(Assembly assembly, KryptonDataGridView assemblyData)
    {
        assemblyData.Rows.Clear();

        Populate(assemblyData, KryptonManager.Strings.AboutBoxStrings.ImageRuntimeVersion, assembly.ImageRuntimeVersion);

        NameValueCollection collection = AssemblyAttribs(assembly);

        foreach (string key in collection)
        {
            Populate(assemblyData, key, collection[key]!);
        }
    }

    public static void PopulateBasicApplicationInformation(KryptonDataGridView dataStore, Assembly currentAssembly)
    {
        dataStore.Rows.Clear();

        AppDomain domain = AppDomain.CurrentDomain;
        Assembly? entryAssembly = Assembly.GetEntryAssembly();

        Populate(dataStore, KryptonManager.Strings.AboutBoxBasicStrings.ApplicationName, currentAssembly.GetName().Name ?? string.Empty);
        Populate(dataStore, KryptonManager.Strings.AboutBoxBasicStrings.ApplicationBase, domain.BaseDirectory);
        Populate(dataStore, KryptonManager.Strings.AboutBoxBasicStrings.FriendlyName, domain.FriendlyName);
        Populate(dataStore, KryptonManager.Strings.AboutBoxStrings.BuildDate,
            FormatBuildDate(AssemblyBuildDate(currentAssembly, false), false));
        Populate(dataStore, KryptonManager.Strings.AboutBoxStrings.BinaryBuildDate,
            FormatBuildDate(GetBinaryBuildDateTime(currentAssembly), false));
        Populate(dataStore, string.Empty, string.Empty);
        Populate(dataStore, KryptonManager.Strings.AboutBoxBasicStrings.EntryAssembly, entryAssembly?.GetName().Name ?? string.Empty);
        Populate(dataStore, KryptonManager.Strings.AboutBoxBasicStrings.ExecutingAssembly, Assembly.GetExecutingAssembly().GetName().Name ?? string.Empty);
        Populate(dataStore, KryptonManager.Strings.AboutBoxBasicStrings.CallingAssembly, currentAssembly.GetName().Name ?? string.Empty);
    }

    public static void PopulateAssemblies(KryptonDataGridView dataStore, bool useFullBuiltOnDate)
    {
        dataStore.Rows.Clear();

        foreach (Assembly assembly in GetLoadedAssemblies())
        {
            AssemblyName name = assembly.GetName();
            DateTime builtOn = GetBinaryBuildDateTime(assembly);
            dataStore.Rows.Add(
                name.Name ?? string.Empty,
                name.Version?.ToString() ?? string.Empty,
                FormatBuildDate(builtOn, useFullBuiltOnDate),
                TryGetLocation(assembly));
        }
    }

    public static IReadOnlyList<Assembly> GetLoadedAssemblies()
    {
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        Array.Sort(assemblies, (left, right) =>
            string.Compare(left.GetName().Name, right.GetName().Name, StringComparison.CurrentCultureIgnoreCase));
        return assemblies;
    }

    public static FileVersionInfo? TryGetFileVersionInfo(Assembly assembly)
    {
        string location = TryGetLocation(assembly);
        if (string.IsNullOrEmpty(location) || !File.Exists(location))
        {
            return null;
        }

        return FileVersionInfo.GetVersionInfo(location);
    }

    public static FileVersionInfo GetFileVersionInfo(string assemblyLocation) =>
        FileVersionInfo.GetVersionInfo(assemblyLocation);

    public static string TryGetLocation(Assembly assembly)
    {
        try
        {
            return assembly.Location ?? string.Empty;
        }
        catch (NotSupportedException)
        {
            return string.Empty;
        }
    }

    public static void ConfigureReadOnlyGrid(KryptonDataGridView grid)
    {
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.ReadOnly = true;
        grid.MultiSelect = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    }

    private static void Populate(KryptonDataGridView assemblyData, string key, string value) => assemblyData.Rows.Add(key, value);

    private static string FirstNonEmpty(params string?[] values)
    {
        foreach (string? value in values)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value!;
            }
        }

        return string.Empty;
    }

    #endregion
}
