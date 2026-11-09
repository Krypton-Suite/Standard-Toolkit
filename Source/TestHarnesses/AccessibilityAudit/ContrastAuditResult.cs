namespace AccessibilityAudit
{
    internal sealed class ContrastAuditResult
    {
        public int ThemesScored { get; set; }

        public int PairsScored { get; set; }

        public int PairsSkippedNotOpaque { get; set; }

        public int PairsSkippedThrew { get; set; }

        public List<string> ThrowExamples { get; } = new List<string>();

        public List<ContrastSample> Misses { get; } = new List<ContrastSample>();

        public List<string> FailureKeys { get; } = new List<string>();
    }
}