#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Reads a JSON manifest that is either one package object or <c>{ "packages": [ ... ] }</c>.
/// </summary>
internal static class KryptonUpdateManifestParser
{
    internal static KryptonUpdateCatalog Parse(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            KryptonUpdateThrowHelper.Throw("The update manifest is empty.");
        }

        var raw = ReadRawPackages(json, out var feed);
        var packages = new List<KryptonUpdatePackage>(raw.Count);
        foreach (var item in raw)
        {
            packages.Add(Build(item, feed));
        }

        return new KryptonUpdateCatalog(packages);
    }

    private static KryptonUpdatePackage Build(RawPackage raw, FeedPolicy feed)
    {
        if (!KryptonUpdateVersion.TryParse(raw.Version, out var version))
        {
            KryptonUpdateThrowHelper.Throw("The manifest version is not a System.Version value.");
        }

        var packageUrl = raw.PackageUrl;
        if (packageUrl == null || string.IsNullOrWhiteSpace(packageUrl))
        {
            KryptonUpdateThrowHelper.Throw("The manifest is missing packageUrl.");
        }

        var sha256 = raw.Sha256;
        if (sha256 == null || string.IsNullOrWhiteSpace(sha256))
        {
            KryptonUpdateThrowHelper.Throw("The manifest is missing sha256.");
        }

        DateTime? released = null;
        if (!string.IsNullOrWhiteSpace(raw.ReleasedUtc))
        {
            if (!DateTime.TryParse(raw.ReleasedUtc, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var parsed))
            {
                KryptonUpdateThrowHelper.Throw("The manifest releasedUtc value is not a time.");
            }

            released = parsed.ToUniversalTime();
        }

        return new KryptonUpdatePackage(
            version,
            ParseSeverity(raw.Severity),
            ParseKind(raw.PackageKind, packageUrl),
            packageUrl.Trim(),
            sha256.Trim(),
            raw.CertificateThumbprint,
            raw.ReleaseNotes,
            raw.InstallerArguments,
            released,
            raw.MaximumPostponeDays ?? feed.MaximumPostponeDays,
            raw.AllowPostpone ?? feed.AllowPostpone,
            raw.AllowSkip ?? feed.AllowSkip,
            raw.AllowResume ?? feed.AllowResume,
            raw.AllowRetry ?? feed.AllowRetry,
            raw.Banner ?? feed.Banner,
            raw.Pulse ?? feed.Pulse);
    }

    private static void ValidateImageUrl(string? imageUrl)
    {
        if (imageUrl == null || string.IsNullOrWhiteSpace(imageUrl))
        {
            return;
        }

        var trimmed = imageUrl.Trim();
        if (KryptonUpdateClient.IsHttps(trimmed))
        {
            return;
        }

        if (Uri.TryCreate(trimmed, UriKind.Absolute, out var uri) && uri.Scheme == Uri.UriSchemeHttp)
        {
            KryptonUpdateThrowHelper.Throw("The banner imageUrl must be https or a local file.");
        }

        if (Uri.TryCreate(trimmed, UriKind.Absolute, out uri) && uri.IsFile)
        {
            return;
        }

        // Local paths are accepted at parse time even when the file is not on this machine.
        if (trimmed.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
        {
            KryptonUpdateThrowHelper.Throw("The banner imageUrl is not a valid path or https address.");
        }
    }

    private static Color ParseColor(string? text, string name)
    {
        if (text == null || string.IsNullOrWhiteSpace(text))
        {
            return Color.Empty;
        }

        var value = text.Trim();
        if (value.StartsWith("#", StringComparison.Ordinal))
        {
            var hex = value.Substring(1);
            // uint so AARRGGBB values with the high bit set still parse; FromArgb takes the bits as Int32.
            if (hex.Length is 6 or 8 &&
                uint.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var argb))
            {
                if (hex.Length == 6)
                {
                    argb |= 0xFF000000u;
                }

                return Color.FromArgb(unchecked((int)argb));
            }

            return KryptonUpdateThrowHelper.Throw<Color>("The manifest " + name + " value is not a colour.");
        }

        try
        {
            var converted = ColorTranslator.FromHtml(value);
            if (converted.IsEmpty && !value.Equals("Empty", StringComparison.OrdinalIgnoreCase))
            {
                return KryptonUpdateThrowHelper.Throw<Color>("The manifest " + name + " value is not a colour.");
            }

            return converted;
        }
        catch (Exception exception) when (exception is ArgumentException or FormatException or KryptonUpdateException)
        {
            return KryptonUpdateThrowHelper.Throw<Color>("The manifest " + name + " value is not a colour.");
        }
    }

    private static InputPulsingBorderStyle ParsePulseStyle(string? text)
    {
        if (text == null || string.IsNullOrWhiteSpace(text))
        {
            return InputPulsingBorderStyle.All;
        }

        if (Enum.TryParse(text.Trim(), true, out InputPulsingBorderStyle style) &&
            style is InputPulsingBorderStyle.All or InputPulsingBorderStyle.Bottom)
        {
            return style;
        }

        return KryptonUpdateThrowHelper.Throw<InputPulsingBorderStyle>("Unknown pulse style '" + text + "'.");
    }

    private static KryptonUpdateSeverity ParseSeverity(string? text)
    {
        if (text == null || string.IsNullOrWhiteSpace(text))
        {
            return KryptonUpdateSeverity.Recommended;
        }

        if (Enum.TryParse(text.Trim(), true, out KryptonUpdateSeverity severity))
        {
            return severity;
        }

        return KryptonUpdateThrowHelper.Throw<KryptonUpdateSeverity>("Unknown update severity '" + text + "'.");
    }

    private static KryptonUpdatePackageKind ParseKind(string? text, string packageUrl)
    {
        if (text != null && !string.IsNullOrWhiteSpace(text))
        {
            if (text.Equals("archive", StringComparison.OrdinalIgnoreCase) || text.Equals("zip", StringComparison.OrdinalIgnoreCase))
            {
                return KryptonUpdatePackageKind.Archive;
            }

            if (text.Equals("installer", StringComparison.OrdinalIgnoreCase))
            {
                return KryptonUpdatePackageKind.Installer;
            }

            return KryptonUpdateThrowHelper.Throw<KryptonUpdatePackageKind>("Unknown packageKind '" + text + "'.");
        }

        return packageUrl.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
            ? KryptonUpdatePackageKind.Archive
            : KryptonUpdatePackageKind.Installer;
    }

    private static List<RawPackage> ReadRawPackages(string json, out FeedPolicy feed)
    {
#if NET8_0_OR_GREATER
        return ReadWithJsonDocument(json, out feed);
#else
        return ReadWithSerializer(json, out feed);
#endif
    }

#if NET8_0_OR_GREATER
    private static List<RawPackage> ReadWithJsonDocument(string json, out FeedPolicy feed)
    {
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;
        var list = new List<RawPackage>();
        feed = default;
        if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("packages", out var packages) && packages.ValueKind == JsonValueKind.Array)
        {
            feed = ReadPolicy(root);
            foreach (var item in packages.EnumerateArray())
            {
                list.Add(ReadElement(item));
            }

            return list;
        }

        list.Add(ReadElement(root));
        return list;
    }

    private static RawPackage ReadElement(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Object)
        {
            KryptonUpdateThrowHelper.Throw("The update manifest must be a JSON object.");
        }

        return new RawPackage
        {
            Version = GetString(element, "version"),
            Severity = GetString(element, "severity"),
            PackageKind = GetString(element, "packageKind"),
            PackageUrl = GetString(element, "packageUrl"),
            Sha256 = GetString(element, "sha256"),
            CertificateThumbprint = GetString(element, "certificateThumbprint"),
            ReleaseNotes = GetString(element, "releaseNotes"),
            InstallerArguments = GetString(element, "installerArguments"),
            ReleasedUtc = GetString(element, "releasedUtc"),
            MaximumPostponeDays = GetInt32(element, "maximumPostponeDays"),
            AllowPostpone = GetBool(element, "allowPostpone"),
            AllowSkip = GetBool(element, "allowSkip"),
            AllowResume = GetBool(element, "allowResume"),
            AllowRetry = GetBool(element, "allowRetry"),
            Banner = ReadBanner(element),
            Pulse = ReadPulse(element)
        };
    }

    private static FeedPolicy ReadPolicy(JsonElement element) =>
        new FeedPolicy
        {
            MaximumPostponeDays = GetInt32(element, "maximumPostponeDays"),
            AllowPostpone = GetBool(element, "allowPostpone"),
            AllowSkip = GetBool(element, "allowSkip"),
            AllowResume = GetBool(element, "allowResume"),
            AllowRetry = GetBool(element, "allowRetry"),
            Banner = ReadBanner(element),
            Pulse = ReadPulse(element)
        };

    private static KryptonUpdateBanner? ReadBanner(JsonElement element)
    {
        if (!element.TryGetProperty("banner", out var banner) || banner.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        if (banner.ValueKind != JsonValueKind.Object)
        {
            KryptonUpdateThrowHelper.Throw("The manifest banner value must be a JSON object.");
        }

        var imageUrl = GetString(banner, "imageUrl");
        ValidateImageUrl(imageUrl);
        var result = new KryptonUpdateBanner(
            GetString(banner, "text"),
            ParseColor(GetString(banner, "backColor"), "banner.backColor"),
            ParseColor(GetString(banner, "foreColor"), "banner.foreColor"),
            imageUrl,
            ParseColor(GetString(banner, "backColor2"), "banner.backColor2"));
        return result.HasContent || !result.BackColor.IsEmpty || !result.BackColor2.IsEmpty || !result.ForeColor.IsEmpty
            ? result
            : null;
    }

    private static KryptonUpdatePulse? ReadPulse(JsonElement element)
    {
        if (!element.TryGetProperty("pulse", out var pulse) || pulse.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        if (pulse.ValueKind != JsonValueKind.Object)
        {
            KryptonUpdateThrowHelper.Throw("The manifest pulse value must be a JSON object.");
        }

        return new KryptonUpdatePulse(
            GetBool(pulse, "enable") ?? false,
            ParseColor(GetString(pulse, "color"), "pulse.color"),
            ParsePulseStyle(GetString(pulse, "style")),
            GetSingle(pulse, "speed") ?? 1f);
    }

    private static float? GetSingle(JsonElement element, string name)
    {
        if (!element.TryGetProperty(name, out var value) || value.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        if (value.ValueKind == JsonValueKind.Number && value.TryGetSingle(out var number))
        {
            return number;
        }

        if (value.ValueKind == JsonValueKind.String && float.TryParse(value.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture, out var parsed))
        {
            return parsed;
        }

        return KryptonUpdateThrowHelper.Throw<float?>("The manifest " + name + " value is not a number.");
    }

    private static int? GetInt32(JsonElement element, string name)
    {
        if (!element.TryGetProperty(name, out var value) || value.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        if (value.ValueKind == JsonValueKind.Number && value.TryGetInt32(out var number))
        {
            return number;
        }

        if (value.ValueKind == JsonValueKind.String && int.TryParse(value.GetString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsed))
        {
            return parsed;
        }

        return KryptonUpdateThrowHelper.Throw<int?>("The manifest " + name + " value is not a whole number.");
    }

    private static bool? GetBool(JsonElement element, string name)
    {
        if (!element.TryGetProperty(name, out var value) || value.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        if (value.ValueKind == JsonValueKind.True)
        {
            return true;
        }

        if (value.ValueKind == JsonValueKind.False)
        {
            return false;
        }

        if (value.ValueKind == JsonValueKind.String && bool.TryParse(value.GetString(), out var parsed))
        {
            return parsed;
        }

        return KryptonUpdateThrowHelper.Throw<bool?>("The manifest " + name + " value is not true or false.");
    }

    private static string? GetString(JsonElement element, string name)
    {
        if (!element.TryGetProperty(name, out var value) || value.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        return value.ValueKind == JsonValueKind.String ? value.GetString() : value.ToString();
    }
#else
    private static List<RawPackage> ReadWithSerializer(string json, out FeedPolicy feed)
    {
        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var data = serializer.DeserializeObject(json);
        var list = new List<RawPackage>();
        feed = default;
        if (data is Dictionary<string, object> root && root.TryGetValue("packages", out var packages))
        {
            feed = ReadPolicy(root);
            foreach (var item in EnumerateObjects(packages))
            {
                list.Add(ReadDictionary(item));
            }

            return list;
        }

        if (data is Dictionary<string, object> single)
        {
            list.Add(ReadDictionary(single));
            return list;
        }

        return KryptonUpdateThrowHelper.Throw<List<RawPackage>>("The update manifest must be a JSON object.");
    }

    private static RawPackage ReadDictionary(Dictionary<string, object> element) =>
        new RawPackage
        {
            Version = GetString(element, "version"),
            Severity = GetString(element, "severity"),
            PackageKind = GetString(element, "packageKind"),
            PackageUrl = GetString(element, "packageUrl"),
            Sha256 = GetString(element, "sha256"),
            CertificateThumbprint = GetString(element, "certificateThumbprint"),
            ReleaseNotes = GetString(element, "releaseNotes"),
            InstallerArguments = GetString(element, "installerArguments"),
            ReleasedUtc = GetString(element, "releasedUtc"),
            MaximumPostponeDays = GetInt32(element, "maximumPostponeDays"),
            AllowPostpone = GetBool(element, "allowPostpone"),
            AllowSkip = GetBool(element, "allowSkip"),
            AllowResume = GetBool(element, "allowResume"),
            AllowRetry = GetBool(element, "allowRetry"),
            Banner = ReadBanner(element),
            Pulse = ReadPulse(element)
        };

    private static FeedPolicy ReadPolicy(Dictionary<string, object> element) =>
        new FeedPolicy
        {
            MaximumPostponeDays = GetInt32(element, "maximumPostponeDays"),
            AllowPostpone = GetBool(element, "allowPostpone"),
            AllowSkip = GetBool(element, "allowSkip"),
            AllowResume = GetBool(element, "allowResume"),
            AllowRetry = GetBool(element, "allowRetry"),
            Banner = ReadBanner(element),
            Pulse = ReadPulse(element)
        };

    private static KryptonUpdateBanner? ReadBanner(Dictionary<string, object> element)
    {
        if (!element.TryGetValue("banner", out var value) || value == null)
        {
            return null;
        }

        if (!(value is Dictionary<string, object> banner))
        {
            return KryptonUpdateThrowHelper.Throw<KryptonUpdateBanner?>("The manifest banner value must be a JSON object.");
        }

        var imageUrl = GetString(banner, "imageUrl");
        ValidateImageUrl(imageUrl);
        var result = new KryptonUpdateBanner(
            GetString(banner, "text"),
            ParseColor(GetString(banner, "backColor"), "banner.backColor"),
            ParseColor(GetString(banner, "foreColor"), "banner.foreColor"),
            imageUrl,
            ParseColor(GetString(banner, "backColor2"), "banner.backColor2"));
        return result.HasContent || !result.BackColor.IsEmpty || !result.BackColor2.IsEmpty || !result.ForeColor.IsEmpty
            ? result
            : null;
    }

    private static KryptonUpdatePulse? ReadPulse(Dictionary<string, object> element)
    {
        if (!element.TryGetValue("pulse", out var value) || value == null)
        {
            return null;
        }

        if (!(value is Dictionary<string, object> pulse))
        {
            return KryptonUpdateThrowHelper.Throw<KryptonUpdatePulse?>("The manifest pulse value must be a JSON object.");
        }

        return new KryptonUpdatePulse(
            GetBool(pulse, "enable") ?? false,
            ParseColor(GetString(pulse, "color"), "pulse.color"),
            ParsePulseStyle(GetString(pulse, "style")),
            GetSingle(pulse, "speed") ?? 1f);
    }

    private static float? GetSingle(Dictionary<string, object> element, string name)
    {
        if (!element.TryGetValue(name, out var value) || value == null)
        {
            return null;
        }

        try
        {
            return Convert.ToSingle(value, CultureInfo.InvariantCulture);
        }
        catch (Exception exception) when (exception is FormatException or InvalidCastException or OverflowException)
        {
            return KryptonUpdateThrowHelper.Throw<float?>("The manifest " + name + " value is not a number.");
        }
    }

    private static int? GetInt32(Dictionary<string, object> element, string name)
    {
        if (!element.TryGetValue(name, out var value) || value == null)
        {
            return null;
        }

        try
        {
            return Convert.ToInt32(value, CultureInfo.InvariantCulture);
        }
        catch (Exception exception) when (exception is FormatException or InvalidCastException or OverflowException)
        {
            return KryptonUpdateThrowHelper.Throw<int?>("The manifest " + name + " value is not a whole number.");
        }
    }

    private static bool? GetBool(Dictionary<string, object> element, string name)
    {
        if (!element.TryGetValue(name, out var value) || value == null)
        {
            return null;
        }

        if (value is bool flag)
        {
            return flag;
        }

        if (value is string text && bool.TryParse(text, out var parsed))
        {
            return parsed;
        }

        return KryptonUpdateThrowHelper.Throw<bool?>("The manifest " + name + " value is not true or false.");
    }

    private static string? GetString(Dictionary<string, object> element, string name)
    {
        if (!element.TryGetValue(name, out var value) || value == null)
        {
            return null;
        }

        return Convert.ToString(value, CultureInfo.InvariantCulture);
    }

    private static List<Dictionary<string, object>> EnumerateObjects(object value)
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

    private sealed class RawPackage
    {
        public string? Version { get; set; }

        public string? Severity { get; set; }

        public string? PackageKind { get; set; }

        public string? PackageUrl { get; set; }

        public string? Sha256 { get; set; }

        public string? CertificateThumbprint { get; set; }

        public string? ReleaseNotes { get; set; }

        public string? InstallerArguments { get; set; }

        public string? ReleasedUtc { get; set; }

        public int? MaximumPostponeDays { get; set; }

        public bool? AllowPostpone { get; set; }

        public bool? AllowSkip { get; set; }

        public bool? AllowResume { get; set; }

        public bool? AllowRetry { get; set; }

        public KryptonUpdateBanner? Banner { get; set; }

        public KryptonUpdatePulse? Pulse { get; set; }
    }

    private struct FeedPolicy
    {
        public int? MaximumPostponeDays;

        public bool? AllowPostpone;

        public bool? AllowSkip;

        public bool? AllowResume;

        public bool? AllowRetry;

        public KryptonUpdateBanner? Banner;

        public KryptonUpdatePulse? Pulse;
    }
}
