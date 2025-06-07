#region BSD License
/*
 * 
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    #region Class: AssemblyUtilities

    public class AssemblyUtilities
    {
        /// <summary>Gets the assembly last write time.</summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="System.ArgumentNullException">assembly - Assembly cannot be null or have an empty location.</exception>
        public static DateTime GetAssemblyLastWriteTime(Assembly assembly)
        {
            if (assembly == null || string.IsNullOrEmpty(assembly.Location))
            {
                throw new ArgumentNullException(nameof(assembly),
                    @"Assembly cannot be null or have an empty location.");
            }

            try
            {
                return File.GetLastWriteTime(assembly.Location);
            }
            catch (Exception e)
            {
                KryptonExceptionHandler.CaptureException(e);

                return DateTime.MaxValue;
            }
        }

        /// <summary>Gets the assembly build date.</summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="forceFileDate">if set to <c>true</c> [force file date].</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static DateTime GetAssemblyBuildDate(Assembly assembly, bool forceFileDate)
        {
            Version? assemblyVersion = assembly.GetName().Version;

            DateTime dateTime;

            if (forceFileDate)
            {
                dateTime = GetAssemblyLastWriteTime(assembly);
            }
            else
            {
                dateTime = DateTime.Parse(@"01/01/1970").AddDays(assemblyVersion?.Build ?? 0)
                    .AddSeconds((assemblyVersion?.Revision ?? 0) * 2);

                if (TimeZoneInfo.Local.IsDaylightSavingTime(dateTime))
                {
                    dateTime = dateTime.AddHours(1);
                }

                if (dateTime > DateTime.Now || (assemblyVersion?.Build ?? 0) < 730 || (assemblyVersion?.Revision ?? 0) == 0)
                {
                    dateTime = GetAssemblyLastWriteTime(assembly);
                }
            }

            return dateTime;
        }

        //https://www.meziantou.net/getting-the-date-of-build-of-a-dotnet-assembly-at-runtime.htm
        /// <summary>Gets the linker time stamp UTC.</summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static DateTime GetLinkerTimeStampUTC(Assembly assembly)
        {
            var location = assembly.Location;

            return GetLinkerTimeStampUTC(location);
        }

        /// <summary>Gets the linker time stamp UTC.</summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static DateTime GetLinkerTimeStampUTC(string filePath)
        {
            const int PE_HEADER_OFFSET = 60;

            const int LINKER_TIMESTAMP_OFFSET = 8;

            var byteBuffer = new byte[2048];

            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var read = file.Read(byteBuffer, 0, byteBuffer.Length);

                Console.WriteLine(read);
            }

            var headerPosition = BitConverter.ToInt32(byteBuffer, PE_HEADER_OFFSET);

            var secondsSinceUNIXTimeStart = BitConverter.ToInt32(byteBuffer, headerPosition + LINKER_TIMESTAMP_OFFSET);

            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return dateTime.AddSeconds(secondsSinceUNIXTimeStart);
        }

        /// <summary>
        /// Gets the assembly attributes.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        public static NameValueCollection GetAssemblyAttributes(Assembly assembly)
        {
            string typeName;
            string name;
            string value;
            NameValueCollection nvc = new NameValueCollection();
            Regex r = new Regex(@"(\.Assembly|\.)(?<Name>[^.]*)Attribute$", RegexOptions.IgnoreCase);

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
                nvc.Add("CodeBase", !string.IsNullOrEmpty(assembly.Location) ? assembly.Location : "(not supported)");
            }
            catch (NotSupportedException)
            {
                nvc.Add("CodeBase", "(not supported)");
            }

            // build date
            DateTime dt = GetAssemblyBuildDate(assembly, false);
            if (dt == DateTime.MaxValue)
            {
                nvc.Add("BuildDate", "(unknown)");
            }
            else
            {
                nvc.Add("BuildDate", dt.ToString(CultureInfo.CurrentCulture));
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
                var version = assembly.GetName().Version;
                if (version != null && version is { Major: 0, Minor: 0 })
                {
                    nvc.Add("Version", "(unknown)");
                }
                else
                {
                    nvc.Add("Version", version?.ToString() ?? "(unknown)");
                }
            }
            catch (Exception)
            {
                nvc.Add("Version", "(unknown)");
            }

            nvc.Add("FullName", assembly.FullName);

            return nvc;
        }
    }
    #endregion
}