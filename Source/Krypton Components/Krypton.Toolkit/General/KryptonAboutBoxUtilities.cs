#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>This class does the heavy lifting for <see cref="KryptonAboutBoxForm"/> and its associated components.</summary>
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
            catch (Exception e)
            {
                return DateTime.MaxValue;
            }
        }

        public static DateTime AssemblyBuildDate(Assembly assembly, bool forceFileDate)
        {
            Version assemblyVersion = assembly.GetName().Version;

            DateTime dateTime;

            if (forceFileDate)
            {
                dateTime = AssemblyLastWriteTime(assembly);
            }
            else
            {
                dateTime = DateTime.Parse(@"01/01/1970").AddDays(assemblyVersion.Build).AddSeconds(assemblyVersion.Revision * 2);

                if (TimeZone.IsDaylightSavingTime(dateTime, TimeZone.CurrentTimeZone.GetDaylightChanges(dateTime.Year)))
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
                            value = x.MajorVersion + "." + x.MinorVersion + "." + x.RevisionNumber + "." + x.BuildNumber;
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
                            value = x.MajorVersion + "." + x.MinorVersion;
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
                nvc.Add("CodeBase", assembly.CodeBase.Replace("file:///", ""));
            }
            catch (NotSupportedException)
            {
                nvc.Add("CodeBase", "(not supported)");
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
                if (assembly.GetName().Version.Major == 0 && assembly.GetName().Version.Minor == 0)
                {
                    nvc.Add("Version", "(unknown)");
                }
                else
                {
                    nvc.Add("Version", assembly.GetName().Version.ToString());
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

            Populate(assemblyData, $@"{KryptonManager.Strings.AboutBoxStrings.LoadedFromGlobalAssemblyCache}", $@"{assembly.GlobalAssemblyCache}");

            NameValueCollection collection = AssemblyAttribs(assembly);

            foreach (string key in collection)
            {
                Populate(assemblyData, key, collection[key]);
            }
        }

        private static void Populate(KryptonDataGridView assemblyData, string key, string value)
        {

        }

        #endregion
    }
}