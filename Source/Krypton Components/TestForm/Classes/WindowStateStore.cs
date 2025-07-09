using System;
using System.IO;
using System.Collections.Generic;

namespace TestForm
{
    public class WindowStateInfo
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public System.Windows.Forms.FormWindowState State { get; set; }
        public int SelectedTab { get; set; }
        public int ContentSplitterDistance { get; set; }
        public string LastWtsFolder { get; set; }
        public string LastTheme { get; set; }
        public string SourcePath { get; set; }
    }

    public class WindowStateStore
    {
        private readonly string _filePath;

        public WindowStateStore()
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TestForm");
            Directory.CreateDirectory(dir);
            _filePath = Path.Combine(dir, "windowstate.ini");
        }

        public WindowStateInfo? Load()
        {
            if (!File.Exists(_filePath))
            {
                return null;
            }

            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var line in File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith(";") || line.StartsWith("#")) continue;
                var idx = line.IndexOf('=');
                if (idx <= 0) continue;
                var key = line.Substring(0, idx).Trim();
                var value = line.Substring(idx + 1).Trim();
                dict[key] = value;
            }

            System.Windows.Forms.FormWindowState state;
            Enum.TryParse(dict.TryGetValue("State", out var s) ? s : null, out state);

            int ParseInt(string key)
            {
                return dict.TryGetValue(key, out var v) && int.TryParse(v, out var result) ? result : 0;
            }

            var info = new WindowStateInfo
            {
                Left = ParseInt("Left"),
                Top = ParseInt("Top"),
                Width = ParseInt("Width"),
                Height = ParseInt("Height"),
                State = state,
                SelectedTab = ParseInt("SelectedTab"),
                ContentSplitterDistance = ParseInt("ContentSplitterDistance"),
                LastWtsFolder = dict.TryGetValue("LastWtsFolder", out var l1) ? l1 : string.Empty,
                LastTheme = dict.TryGetValue("LastTheme", out var l2) ? l2 : string.Empty,
                SourcePath = dict.TryGetValue("SourcePath", out var l3) ? l3 : string.Empty
            };

            return info;
        }

        public void Save(WindowStateInfo info)
        {
            var lines = new List<string>
            {
                $"Left={info.Left}",
                $"Top={info.Top}",
                $"Width={info.Width}",
                $"Height={info.Height}",
                $"State={info.State}",
                $"SelectedTab={info.SelectedTab}",
                $"ContentSplitterDistance={info.ContentSplitterDistance}",
                $"LastWtsFolder={info.LastWtsFolder ?? string.Empty}",
                $"LastTheme={info.LastTheme ?? string.Empty}",
                $"SourcePath={info.SourcePath ?? string.Empty}"
            };
            File.WriteAllLines(_filePath, lines);
        }
    }
}