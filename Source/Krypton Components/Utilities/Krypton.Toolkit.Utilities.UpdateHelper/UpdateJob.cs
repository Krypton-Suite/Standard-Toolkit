#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities.UpdateHelper;

/// <summary>Job file written by <c>KryptonApplicationUpdater</c>.</summary>
internal sealed class UpdateJob
{
    public int ProcessId { get; set; }

    public string ZipPath { get; set; } = string.Empty;

    public string DestinationDirectory { get; set; } = string.Empty;

    public string? RestartExecutable { get; set; }

    public string? RestartArguments { get; set; }

    public int WaitTimeoutSeconds { get; set; }

    public static UpdateJob Load(string path)
    {
        var serializer = new JavaScriptSerializer();
        var data = serializer.Deserialize<Dictionary<string, object>>(File.ReadAllText(path));
        if (data == null)
        {
            throw new InvalidDataException("The update job file is empty.");
        }

        return new UpdateJob
        {
            ProcessId = GetInt(data, "processId"),
            ZipPath = GetString(data, "zipPath") ?? string.Empty,
            DestinationDirectory = GetString(data, "destinationDirectory") ?? string.Empty,
            RestartExecutable = GetString(data, "restartExecutable"),
            RestartArguments = GetString(data, "restartArguments"),
            WaitTimeoutSeconds = GetInt(data, "waitTimeoutSeconds")
        };
    }

    private static string? GetString(Dictionary<string, object> data, string name)
    {
        if (!data.TryGetValue(name, out var value) || value == null)
        {
            return null;
        }

        return Convert.ToString(value);
    }

    private static int GetInt(Dictionary<string, object> data, string name)
    {
        if (!data.TryGetValue(name, out var value) || value == null)
        {
            return 0;
        }

        if (value is int number)
        {
            return number;
        }

        if (value is long longNumber)
        {
            return (int)longNumber;
        }

        if (value is ArrayList)
        {
            return 0;
        }

        return Convert.ToInt32(value);
    }
}
