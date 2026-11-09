#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace AccessibilityAudit;

internal sealed class BaselineFile
{
    private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        DefaultIgnoreCondition = JsonIgnoreCondition.Never
    };

    public List<string> ContrastFailures { get; set; } = new List<string>();

    public List<string> ControlFailures { get; set; } = new List<string>();

    public static BaselineFile Load(string? path)
    {
        if (path != null)
        {
            string json = File.ReadAllText(path);
            BaselineFile? baseline = JsonSerializer.Deserialize<BaselineFile>(json, JsonOptions);
            if (baseline == null)
            {
                throw new InvalidOperationException($"Baseline file '{path}' did not contain a JSON object.");
            }

            baseline.ContrastFailures ??= new List<string>();
            baseline.ControlFailures ??= new List<string>();
            return baseline;
        }
        else
        {
            return new BaselineFile();
        }
    }

    public void Save(string? path)
    {
        ContrastFailures.Sort(StringComparer.Ordinal);
        ControlFailures.Sort(StringComparer.Ordinal);

        string? directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string json = JsonSerializer.Serialize(this, JsonOptions).Replace("\r\n", "\n").Replace("\n", "\r\n");
        if (!json.EndsWith("\r\n", StringComparison.Ordinal))
        {
            json += "\r\n";
        }

        File.WriteAllText(path!, json, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));
    }
}
