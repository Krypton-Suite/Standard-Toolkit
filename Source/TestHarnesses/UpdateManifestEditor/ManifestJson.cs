#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

#if NET8_0_OR_GREATER
using System.Text.Json;
#else
using System.Collections;
using System.Web.Script.Serialization;
#endif

namespace UpdateManifestEditor
{
    /// <summary>Reads and writes the update manifest JSON accepted by KryptonJsonManifestUpdateProvider.</summary>
    internal static class ManifestJson
    {
        internal static IReadOnlyList<ManifestPackage> Read(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                throw new InvalidOperationException("The manifest file is empty.");
            }

#if NET8_0_OR_GREATER
            return ReadModern(json);
#else
            return ReadFramework(json);
#endif
        }

        internal static string Write(IReadOnlyList<ManifestPackage> packages)
        {
            var builder = new StringBuilder();
            if (packages.Count == 1)
            {
                WritePackage(builder, packages[0], 0);
                builder.AppendLine();
                return builder.ToString();
            }

            builder.AppendLine("{");
            builder.AppendLine("  \"packages\": [");
            for (var index = 0; index < packages.Count; index++)
            {
                WritePackage(builder, packages[index], 4);
                if (index < packages.Count - 1)
                {
                    builder.Append(',');
                }

                builder.AppendLine();
            }

            builder.AppendLine("  ]");
            builder.AppendLine("}");
            return builder.ToString();
        }

        /// <summary>Returns the first problem, or null when every package can be saved.</summary>
        internal static string? Validate(IReadOnlyList<ManifestPackage> packages)
        {
            if (packages.Count == 0)
            {
                return "Add at least one package.";
            }

            for (var index = 0; index < packages.Count; index++)
            {
                var package = packages[index];
                var label = "Package " + (index + 1);
                if (!TryNormalizeVersion(package.Version, out _))
                {
                    return label + " version is not a System.Version value.";
                }

                var urlError = ValidatePackageUrl(package.PackageUrl);
                if (urlError != null)
                {
                    return label + " " + urlError;
                }

                if (!IsSha256(package.Sha256))
                {
                    return label + " SHA-256 must be 64 hex characters. Hash the package file.";
                }

                if (!string.IsNullOrWhiteSpace(package.ReleasedUtc)
                    && !DateTime.TryParse(package.ReleasedUtc, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out _))
                {
                    return label + " released UTC is not a time.";
                }

                if (!string.IsNullOrWhiteSpace(package.MaximumPostponeDays)
                    && (!int.TryParse(package.MaximumPostponeDays.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var days) || days < 1))
                {
                    return label + " maximum postpone days must be a whole number of at least 1.";
                }
            }

            return null;
        }

        internal static bool TryNormalizeVersion(string? text, out string version)
        {
            version = string.Empty;
            if (text == null || string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            var source = text.Trim();
            if (source.Length > 1 && (source[0] == 'v' || source[0] == 'V'))
            {
                source = source.Substring(1);
            }

            if (!Version.TryParse(source, out _))
            {
                return false;
            }

            version = source;
            return true;
        }

        internal static string NormalizeSha256(string? text)
        {
            if (text == null || string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            var source = text.Trim();
            if (source.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                source = source.Substring(2);
            }

            var builder = new StringBuilder(source.Length);
            foreach (var character in source)
            {
                if (character == ' ' || character == '-')
                {
                    continue;
                }

                builder.Append(character);
            }

            return builder.ToString().ToLowerInvariant();
        }

        private static bool IsSha256(string? text)
        {
            var hash = NormalizeSha256(text);
            if (hash.Length != 64)
            {
                return false;
            }

            foreach (var character in hash)
            {
                var hex = (character >= '0' && character <= '9')
                    || (character >= 'a' && character <= 'f');
                if (!hex)
                {
                    return false;
                }
            }

            return true;
        }

        private static string? ValidatePackageUrl(string? text)
        {
            if (text == null || string.IsNullOrWhiteSpace(text))
            {
                return "package URL is required.";
            }

            var url = text.Trim();
            if (url.IndexOf("://", StringComparison.Ordinal) < 0)
            {
                return null;
            }

            if (url.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
                || url.StartsWith("file://", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return "package URL must be https or a local path.";
        }

        private static void WritePackage(StringBuilder builder, ManifestPackage package, int indent)
        {
            var pad = new string(' ', indent);
            var inner = new string(' ', indent + 2);
            var fields = new List<string>
            {
                Quote("version", DisplayVersion(package.Version)),
                Quote("severity", string.IsNullOrWhiteSpace(package.Severity) ? "recommended" : package.Severity.Trim()),
                Quote("packageKind", string.IsNullOrWhiteSpace(package.PackageKind) ? "installer" : package.PackageKind.Trim()),
                Quote("packageUrl", (package.PackageUrl ?? string.Empty).Trim()),
                Quote("sha256", NormalizeSha256(package.Sha256))
            };
            AddOptional(fields, "certificateThumbprint", package.CertificateThumbprint);
            AddOptional(fields, "releaseNotes", package.ReleaseNotes);
            AddOptional(fields, "installerArguments", package.InstallerArguments);
            AddOptional(fields, "releasedUtc", package.ReleasedUtc);
            AddMaximumPostponeDays(fields, package.MaximumPostponeDays);
            AddOptionalBool(fields, "allowPostpone", package.AllowPostpone);
            AddOptionalBool(fields, "allowSkip", package.AllowSkip);
            AddOptionalBool(fields, "allowResume", package.AllowResume);
            AddOptionalBool(fields, "allowRetry", package.AllowRetry);
            AddBanner(fields, package);
            AddPulse(fields, package);

            builder.Append(pad).AppendLine("{");
            for (var index = 0; index < fields.Count; index++)
            {
                builder.Append(inner).Append(fields[index]);
                if (index < fields.Count - 1)
                {
                    builder.Append(',');
                }

                builder.AppendLine();
            }

            builder.Append(pad).Append('}');
        }

        private static void AddBanner(List<string> fields, ManifestPackage package)
        {
            var parts = new List<string>();
            AddOptional(parts, "text", package.BannerText);
            AddOptional(parts, "backColor", package.BannerBackColor);
            AddOptional(parts, "backColor2", package.BannerBackColor2);
            AddOptional(parts, "foreColor", package.BannerForeColor);
            AddOptional(parts, "imageUrl", package.BannerImageUrl);
            if (parts.Count == 0)
            {
                return;
            }

            fields.Add("\"banner\": { " + string.Join(", ", parts) + " }");
        }

        private static void AddPulse(List<string> fields, ManifestPackage package)
        {
            if (package.PulseEnable == null || string.IsNullOrWhiteSpace(package.PulseEnable))
            {
                return;
            }

            if (!bool.TryParse(package.PulseEnable.Trim(), out var enable))
            {
                return;
            }

            var parts = new List<string>
            {
                "\"enable\": " + (enable ? "true" : "false")
            };
            AddOptional(parts, "color", package.PulseColor);
            if (package.PulseStyle != null && !string.IsNullOrWhiteSpace(package.PulseStyle))
            {
                parts.Add(Quote("style", package.PulseStyle.Trim()));
            }

            if (package.PulseSpeed != null
                && !string.IsNullOrWhiteSpace(package.PulseSpeed)
                && float.TryParse(package.PulseSpeed.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out var speed)
                && speed > 0f)
            {
                parts.Add("\"speed\": " + speed.ToString(CultureInfo.InvariantCulture));
            }

            fields.Add("\"pulse\": { " + string.Join(", ", parts) + " }");
        }

        private static string DisplayVersion(string? text)
        {
            if (TryNormalizeVersion(text, out var version))
            {
                return version;
            }

            return text == null ? string.Empty : text.Trim();
        }

        private static void AddMaximumPostponeDays(List<string> fields, string? value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            if (!int.TryParse(value.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var days) || days < 1)
            {
                return;
            }

            fields.Add("\"maximumPostponeDays\": " + days.ToString(CultureInfo.InvariantCulture));
        }

        private static void AddOptionalBool(List<string> fields, string name, string? value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            if (!bool.TryParse(value.Trim(), out var allow))
            {
                return;
            }

            fields.Add("\"" + name + "\": " + (allow ? "true" : "false"));
        }

        private static void ApplyRoot(ManifestPackage package, ManifestPackage root)
        {
            if (string.IsNullOrWhiteSpace(package.MaximumPostponeDays))
            {
                package.MaximumPostponeDays = root.MaximumPostponeDays;
            }

            if (string.IsNullOrWhiteSpace(package.AllowPostpone))
            {
                package.AllowPostpone = root.AllowPostpone;
            }

            if (string.IsNullOrWhiteSpace(package.AllowSkip))
            {
                package.AllowSkip = root.AllowSkip;
            }

            if (string.IsNullOrWhiteSpace(package.AllowResume))
            {
                package.AllowResume = root.AllowResume;
            }

            if (string.IsNullOrWhiteSpace(package.AllowRetry))
            {
                package.AllowRetry = root.AllowRetry;
            }

            // Root banner/pulse apply only when the package omitted that object entirely.
            if (string.IsNullOrWhiteSpace(package.BannerText)
                && string.IsNullOrWhiteSpace(package.BannerBackColor)
                && string.IsNullOrWhiteSpace(package.BannerBackColor2)
                && string.IsNullOrWhiteSpace(package.BannerForeColor)
                && string.IsNullOrWhiteSpace(package.BannerImageUrl))
            {
                package.BannerText = root.BannerText;
                package.BannerBackColor = root.BannerBackColor;
                package.BannerBackColor2 = root.BannerBackColor2;
                package.BannerForeColor = root.BannerForeColor;
                package.BannerImageUrl = root.BannerImageUrl;
            }

            if (string.IsNullOrWhiteSpace(package.PulseEnable))
            {
                package.PulseEnable = root.PulseEnable;
                package.PulseColor = root.PulseColor;
                package.PulseStyle = root.PulseStyle;
                package.PulseSpeed = root.PulseSpeed;
            }
        }

        private static void AddOptional(List<string> fields, string name, string? value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            fields.Add(Quote(name, value.Trim()));
        }

        private static string Quote(string name, string value) =>
            "\"" + name + "\": \"" + Escape(value) + "\"";

        private static string Escape(string value)
        {
            var builder = new StringBuilder(value.Length + 8);
            foreach (var character in value)
            {
                switch (character)
                {
                    case '\\':
                        builder.Append("\\\\");
                        break;
                    case '"':
                        builder.Append("\\\"");
                        break;
                    case '\r':
                        builder.Append("\\r");
                        break;
                    case '\n':
                        builder.Append("\\n");
                        break;
                    case '\t':
                        builder.Append("\\t");
                        break;
                    default:
                        if (character < ' ')
                        {
                            builder.Append("\\u");
                            builder.Append(((int)character).ToString("x4", CultureInfo.InvariantCulture));
                        }
                        else
                        {
                            builder.Append(character);
                        }

                        break;
                }
            }

            return builder.ToString();
        }

#if NET8_0_OR_GREATER
        private static IReadOnlyList<ManifestPackage> ReadModern(string json)
        {
            using (var document = JsonDocument.Parse(json))
            {
                var root = document.RootElement;
                var list = new List<ManifestPackage>();
                if (root.ValueKind == JsonValueKind.Object
                    && root.TryGetProperty("packages", out var packages)
                    && packages.ValueKind == JsonValueKind.Array)
                {
                    var rootDefaults = ReadElement(root);
                    foreach (var item in packages.EnumerateArray())
                    {
                        var package = ReadElement(item);
                        ApplyRoot(package, rootDefaults);
                        list.Add(package);
                    }

                    return list;
                }

                list.Add(ReadElement(root));
                return list;
            }
        }

        private static ManifestPackage ReadElement(JsonElement element)
        {
            if (element.ValueKind != JsonValueKind.Object)
            {
                throw new InvalidOperationException("The manifest must be a JSON object.");
            }

            return new ManifestPackage
            {
                Version = GetString(element, "version"),
                Severity = DefaultIfEmpty(GetString(element, "severity"), "recommended"),
                PackageKind = DefaultIfEmpty(GetString(element, "packageKind"), "installer"),
                PackageUrl = GetString(element, "packageUrl"),
                Sha256 = GetString(element, "sha256"),
                CertificateThumbprint = GetString(element, "certificateThumbprint"),
                ReleaseNotes = GetString(element, "releaseNotes"),
                InstallerArguments = GetString(element, "installerArguments"),
                ReleasedUtc = GetString(element, "releasedUtc"),
                MaximumPostponeDays = FormatOptionalInt(element, "maximumPostponeDays"),
                AllowPostpone = FormatOptionalBool(element, "allowPostpone"),
                AllowSkip = FormatOptionalBool(element, "allowSkip"),
                AllowResume = FormatOptionalBool(element, "allowResume"),
                AllowRetry = FormatOptionalBool(element, "allowRetry"),
                BannerText = GetNestedString(element, "banner", "text"),
                BannerBackColor = GetNestedString(element, "banner", "backColor"),
                BannerBackColor2 = GetNestedString(element, "banner", "backColor2"),
                BannerForeColor = GetNestedString(element, "banner", "foreColor"),
                BannerImageUrl = GetNestedString(element, "banner", "imageUrl"),
                PulseEnable = GetNestedBool(element, "pulse", "enable"),
                PulseColor = GetNestedString(element, "pulse", "color"),
                PulseStyle = GetNestedString(element, "pulse", "style"),
                PulseSpeed = GetNestedNumber(element, "pulse", "speed")
            };
        }

        private static string GetNestedString(JsonElement element, string parent, string name)
        {
            if (!element.TryGetProperty(parent, out var objectElement) || objectElement.ValueKind != JsonValueKind.Object)
            {
                return string.Empty;
            }

            return GetString(objectElement, name);
        }

        private static string GetNestedBool(JsonElement element, string parent, string name)
        {
            if (!element.TryGetProperty(parent, out var objectElement) || objectElement.ValueKind != JsonValueKind.Object)
            {
                return string.Empty;
            }

            return FormatOptionalBool(objectElement, name);
        }

        private static string GetNestedNumber(JsonElement element, string parent, string name)
        {
            if (!element.TryGetProperty(parent, out var objectElement) || objectElement.ValueKind != JsonValueKind.Object)
            {
                return string.Empty;
            }

            if (!objectElement.TryGetProperty(name, out var value) || value.ValueKind == JsonValueKind.Null)
            {
                return string.Empty;
            }

            if (value.ValueKind == JsonValueKind.Number && value.TryGetSingle(out var number))
            {
                return number.ToString(CultureInfo.InvariantCulture);
            }

            return value.ValueKind == JsonValueKind.String ? value.GetString() ?? string.Empty : string.Empty;
        }

        private static string FormatOptionalInt(JsonElement element, string name)
        {
            if (!element.TryGetProperty(name, out var value) || value.ValueKind == JsonValueKind.Null)
            {
                return string.Empty;
            }

            if (value.ValueKind == JsonValueKind.Number && value.TryGetInt32(out var number))
            {
                return number.ToString(CultureInfo.InvariantCulture);
            }

            return value.ValueKind == JsonValueKind.String ? value.GetString() ?? string.Empty : string.Empty;
        }

        private static string FormatOptionalBool(JsonElement element, string name)
        {
            if (!element.TryGetProperty(name, out var value) || value.ValueKind == JsonValueKind.Null)
            {
                return string.Empty;
            }

            if (value.ValueKind == JsonValueKind.True)
            {
                return "true";
            }

            if (value.ValueKind == JsonValueKind.False)
            {
                return "false";
            }

            return value.ValueKind == JsonValueKind.String ? value.GetString() ?? string.Empty : string.Empty;
        }

        private static string GetString(JsonElement element, string name)
        {
            if (!element.TryGetProperty(name, out var value) || value.ValueKind == JsonValueKind.Null)
            {
                return string.Empty;
            }

            return value.ValueKind == JsonValueKind.String ? value.GetString() ?? string.Empty : value.ToString();
        }
#else
        private static IReadOnlyList<ManifestPackage> ReadFramework(string json)
        {
            var serializer = new JavaScriptSerializer
            {
                MaxJsonLength = int.MaxValue
            };
            var data = serializer.DeserializeObject(json);
            var list = new List<ManifestPackage>();
            if (data is Dictionary<string, object> root && root.TryGetValue("packages", out var packages))
            {
                var rootDefaults = ReadDictionary(root);
                foreach (var item in Enumerate(packages))
                {
                    var package = ReadDictionary(item);
                    ApplyRoot(package, rootDefaults);
                    list.Add(package);
                }

                return list;
            }

            if (data is Dictionary<string, object> single)
            {
                list.Add(ReadDictionary(single));
                return list;
            }

            throw new InvalidOperationException("The manifest must be a JSON object.");
        }

        private static ManifestPackage ReadDictionary(Dictionary<string, object> element) =>
            new ManifestPackage
            {
                Version = GetString(element, "version"),
                Severity = DefaultIfEmpty(GetString(element, "severity"), "recommended"),
                PackageKind = DefaultIfEmpty(GetString(element, "packageKind"), "installer"),
                PackageUrl = GetString(element, "packageUrl"),
                Sha256 = GetString(element, "sha256"),
                CertificateThumbprint = GetString(element, "certificateThumbprint"),
                ReleaseNotes = GetString(element, "releaseNotes"),
                InstallerArguments = GetString(element, "installerArguments"),
                ReleasedUtc = GetString(element, "releasedUtc"),
                MaximumPostponeDays = FormatOptionalInt(element, "maximumPostponeDays"),
                AllowPostpone = FormatOptionalBool(element, "allowPostpone"),
                AllowSkip = FormatOptionalBool(element, "allowSkip"),
                AllowResume = FormatOptionalBool(element, "allowResume"),
                AllowRetry = FormatOptionalBool(element, "allowRetry"),
                BannerText = GetNestedString(element, "banner", "text"),
                BannerBackColor = GetNestedString(element, "banner", "backColor"),
                BannerBackColor2 = GetNestedString(element, "banner", "backColor2"),
                BannerForeColor = GetNestedString(element, "banner", "foreColor"),
                BannerImageUrl = GetNestedString(element, "banner", "imageUrl"),
                PulseEnable = GetNestedBool(element, "pulse", "enable"),
                PulseColor = GetNestedString(element, "pulse", "color"),
                PulseStyle = GetNestedString(element, "pulse", "style"),
                PulseSpeed = GetNestedNumber(element, "pulse", "speed")
            };

        private static string GetNestedString(Dictionary<string, object> element, string parent, string name)
        {
            if (!element.TryGetValue(parent, out var value) || !(value is Dictionary<string, object> nested))
            {
                return string.Empty;
            }

            return GetString(nested, name);
        }

        private static string GetNestedBool(Dictionary<string, object> element, string parent, string name)
        {
            if (!element.TryGetValue(parent, out var value) || !(value is Dictionary<string, object> nested))
            {
                return string.Empty;
            }

            return FormatOptionalBool(nested, name);
        }

        private static string GetNestedNumber(Dictionary<string, object> element, string parent, string name)
        {
            if (!element.TryGetValue(parent, out var value) || !(value is Dictionary<string, object> nested))
            {
                return string.Empty;
            }

            if (!nested.TryGetValue(name, out var number) || number == null)
            {
                return string.Empty;
            }

            try
            {
                return Convert.ToSingle(number, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception exception) when (exception is FormatException || exception is InvalidCastException || exception is OverflowException)
            {
                return Convert.ToString(number, CultureInfo.InvariantCulture) ?? string.Empty;
            }
        }

        private static string FormatOptionalInt(Dictionary<string, object> element, string name)
        {
            if (!element.TryGetValue(name, out var value) || value == null)
            {
                return string.Empty;
            }

            try
            {
                return Convert.ToInt32(value, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception exception) when (exception is FormatException || exception is InvalidCastException || exception is OverflowException)
            {
                return Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty;
            }
        }

        private static string FormatOptionalBool(Dictionary<string, object> element, string name)
        {
            if (!element.TryGetValue(name, out var value) || value == null)
            {
                return string.Empty;
            }

            if (value is bool flag)
            {
                return flag ? "true" : "false";
            }

            return Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty;
        }

        private static string GetString(Dictionary<string, object> element, string name)
        {
            if (!element.TryGetValue(name, out var value) || value == null)
            {
                return string.Empty;
            }

            return Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty;
        }

        private static List<Dictionary<string, object>> Enumerate(object value)
        {
            var list = new List<Dictionary<string, object>>();
            if (value is object[] array)
            {
                foreach (var item in array)
                {
                    if (item is Dictionary<string, object> dictionary)
                    {
                        list.Add(dictionary);
                    }
                }
            }
            else if (value is ArrayList arrayList)
            {
                foreach (var item in arrayList)
                {
                    if (item is Dictionary<string, object> dictionary)
                    {
                        list.Add(dictionary);
                    }
                }
            }

            return list;
        }
#endif

        private static string DefaultIfEmpty(string value, string fallback) =>
            string.IsNullOrWhiteSpace(value) ? fallback : value.Trim();
    }
}
