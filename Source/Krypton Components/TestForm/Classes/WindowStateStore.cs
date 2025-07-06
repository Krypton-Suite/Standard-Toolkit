using System;
using System.IO;
using Newtonsoft.Json;

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
            _filePath = Path.Combine(dir, "windowstate.json");
        }

        public WindowStateInfo Load()
        {
            if (!File.Exists(_filePath)) return null;
            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<WindowStateInfo>(json);
        }

        public void Save(WindowStateInfo info)
        {
            var json = JsonConvert.SerializeObject(info, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}