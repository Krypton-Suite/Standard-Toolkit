#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>This class does the heavy lifting for <see cref="VisualAboutBoxForm"/> and its associated components.</summary>
internal class KryptonAboutBoxUtilities
{
    #region Identity

    public KryptonAboutBoxUtilities()
    {

    }

    #endregion

    #region Implementation

    public static DateTime AssemblyLastWriteTime(Assembly assembly)
    {
        if (string.IsNullOrEmpty(assembly.Location))
        {
            return DateTime.MaxValue;
        }

        try
        {
            return File.GetLastWriteTime(assembly.Location);
        }
        catch
        {
            return DateTime.MaxValue;
        }
    }

    public static DateTime AssemblyBuildDate(Assembly? assembly, bool forceFileDate)
    {
        if (assembly != null)
        {
            Version assemblyVersion = assembly.GetName().Version!;

            DateTime dateTime;

            if (forceFileDate)
            {
                dateTime = AssemblyLastWriteTime(assembly);
            }
            else
            {
                dateTime = DateTime.Parse(@"01/01/1970").AddDays(assemblyVersion!.Build).AddSeconds(assemblyVersion.Revision * 2);

                // if (TimeZone.IsDaylightSavingTime(dateTime, TimeZone.CurrentTimeZone.GetDaylightChanges(dateTime.Year)))
                // Timezone is deprecated and replaces by TimeZoneInfo

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
        else
        {
            return DateTime.Now;
        }
    }

    public static NameValueCollection AssemblyAttribs(Assembly assembly)
    {
        string typeName;
        string name;
        string value;
        var nvc = new NameValueCollection();
        var r = new Regex(@"(\.Assembly|\.)(?<Name>[^.]*)Attribute$", RegexOptions.IgnoreCase);

        foreach (var attrib in assembly.GetCustomAttributes(false))
        {
            typeName = attrib.GetType().ToString();
            name = r.Match(typeName).Groups["Name"].ToString();
            value = "";
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
                    ComCompatibleVersionAttribute x;
                    x = ((ComCompatibleVersionAttribute)attrib);
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
                    TypeLibVersionAttribute x;
                    x = ((TypeLibVersionAttribute)attrib);
                    value = $"{x.MajorVersion}.{x.MinorVersion}";
                    break;
                }
                case "System.Security.AllowPartiallyTrustedCallersAttribute":
                    value = "(Present)";
                    break;
                default:
                    // debug.writeline("** unknown assembly attribute '" + TypeName + "'")
                    value = typeName;
                    break;
            }

            if (nvc[name] == null)
            {
                nvc.Add(name, value);
            }
        }

        // add some extra values that are not in the AssemblyInfo, but nice to have
        // codebase
        try
        {
            // Warning SYSLIB0012 'Assembly.EscapedCodeBase' is obsolete:
            // 'Assembly.CodeBase and Assembly.EscapedCodeBase are only included for .NET Framework compatibility.
            // Use Assembly.Location.' Krypton.Toolkit 2022(net6.0 - windows), Krypton.Toolkit 2022(net8.0 - windows), Krypton.Toolkit 2022(net9.0 - windows)
            //nvc.Add("CodeBase", assembly.EscapedCodeBase.Replace("file:///", ""));

            string? s = assembly.Location.Replace("file:///", "");
            nvc.Add("CodeBase",  s is not null ? s : string.Empty );
        }
        catch (NotSupportedException)
        {
            nvc.Add("CodeBasee", "(not supported)");
        }
        // build date
        var dt = AssemblyBuildDate(assembly, false);
        if (dt == DateTime.MaxValue)
        {
            nvc.Add("BuildDate", "(unknown)");
        }
        else
        {
            // ToDo: Use current culture format
            nvc.Add("BuildDate", dt.ToString("yyyy-MM-dd hh:mm tt"));
        }
        // location
        try
        {
            nvc.Add("Location", assembly.Location);
        }
        catch (NotSupportedException)
        {
            nvc.Add("Location", "(not supported)");
        }
        // version
        try
        {
            if (assembly.GetName().Version!.Major == 0 && assembly.GetName().Version!.Minor == 0)
            {
                nvc.Add("Version", "(unknown)");
            }
            else
            {
                nvc.Add("Version", assembly.GetName().Version!.ToString());
            }
        }
        catch (Exception)
        {
            nvc.Add("Version", "(unknown)");
        }

        nvc.Add("FullName", assembly.FullName);

        return nvc;
    }

    public static void LaunchSystemInformation() => GlobalToolkitUtilities.LaunchProcess(@"MSInfo32.exe");

    public static void PopulateAssemblyDetails(Assembly assembly, KryptonDataGridView assemblyData)
    {
        assemblyData.Rows.Clear();

        Populate(assemblyData, $@"{KryptonManager.Strings.AboutBoxStrings.ImageRuntimeVersion}", assembly.ImageRuntimeVersion);

        // Global assembly cache APIs are obsolete
        // https://learn.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/5.0/global-assembly-cache-apis-obsolete
        // Statement below commented out to remove the corresponding warning.
        // Populate(assemblyData, $@"{KryptonManager.Strings.AboutBoxStrings.LoadedFromGlobalAssemblyCache}", $@"{assembly.GlobalAssemblyCache}");

        NameValueCollection collection = AssemblyAttribs(assembly);

        foreach (string key in collection)
        {
            Populate(assemblyData, key, collection[key]!);
        }
    }

    private static void Populate(KryptonDataGridView assemblyData, string key, string value) => assemblyData.Rows.Add(key, value);

    public static void PopulateBasicApplicationInformation(KryptonDataGridView dataStore)
    {
        AppDomain domain = AppDomain.CurrentDomain;

        string entryAssemblyName = Assembly.GetEntryAssembly()!.GetName().Name!;

        string executingAssemblyName = Assembly.GetExecutingAssembly().GetName().Name!;

        string callingAssemblyName = Assembly.GetCallingAssembly().GetName().Name!;

        Populate(dataStore, KryptonManager.Strings.AboutBoxBasicStrings.ApplicationName, Assembly.GetEntryAssembly()!.GetName().Name!);

        Populate(dataStore, KryptonManager.Strings.AboutBoxBasicStrings.ApplicationBase, Assembly.GetEntryAssembly()!.Location);

        // ToDo: Move to .NET
        //Populate(dataStore, KryptonManager.Strings.AboutBoxBasicStrings.CachePath, domain.SetupInformation.CachePath);

        //Populate(dataStore, KryptonManager.Strings.AboutBoxBasicStrings.ConfigurationFile, domain.SetupInformation.ConfigurationFile);

        //Populate(dataStore, KryptonManager.Strings.AboutBoxBasicStrings.DynamicBase, domain.SetupInformation.DynamicBase);

        //Populate(dataStore, KryptonManager.Strings.AboutBoxBasicStrings.FriendlyName, domain.FriendlyName);

        //Populate(dataStore, KryptonManager.Strings.AboutBoxBasicStrings.LicenseFile, domain.SetupInformation.LicenseFile);

        //Populate(dataStore, KryptonManager.Strings.AboutBoxBasicStrings.PrivateBinPath, domain.SetupInformation.PrivateBinPath);

        //Populate(dataStore, KryptonManager.Strings.AboutBoxBasicStrings.ShadowCopyDirectories, domain.SetupInformation.ShadowCopyDirectories);

        Populate(dataStore, string.Empty, string.Empty);

        Populate(dataStore, KryptonManager.Strings.AboutBoxBasicStrings.EntryAssembly, entryAssemblyName!);

        Populate(dataStore, KryptonManager.Strings.AboutBoxBasicStrings.ExecutingAssembly, executingAssemblyName!);

        Populate(dataStore, KryptonManager.Strings.AboutBoxBasicStrings.CallingAssembly, callingAssemblyName!);
    }

    public static void PopulateAssemblies(KryptonComboBox assemblyList, KryptonDataGridView dataStore)
    {
        string entryAssemblyName = Assembly.GetEntryAssembly()!.GetName().Name!;

        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            PopulateAssemblySummary(assembly, dataStore, assemblyList);
        }

        assemblyList.SelectedIndex = assemblyList.FindStringExact(entryAssemblyName!);
    }

    private static void PopulateAssemblySummary(Assembly assembly, KryptonDataGridView dataStore, KryptonComboBox assemblyItems)
    {
        NameValueCollection collection = AssemblyAttribs(assembly);

        string assemblyName = assembly.GetName().Name!;

        foreach (var value in collection)
        {
            dataStore.Rows.Add(value);
        }

        assemblyItems.Items.Add(assemblyName!);
    }

    public static FileVersionInfo GetFileVersionInfo(string assemblyLocation)
    {
        FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assemblyLocation);

        return versionInfo;
    }

    #endregion
}