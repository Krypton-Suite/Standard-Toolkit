namespace AccessibilityAudit
{
    internal sealed class AuditOptions
    {
        private AuditOptions(string? baselinePath, string? reportPath, bool updateBaseline, AuditSuite suite)
        {
            BaselinePath = baselinePath;
            ReportPath = reportPath;
            UpdateBaseline = updateBaseline;
            Suite = suite;
        }

        public string? BaselinePath { get; }

        public string? ReportPath { get; }

        public bool UpdateBaseline { get; }

        public AuditSuite Suite { get; }

        public static AuditOptions Parse(string[] args)
        {
            string? baseline = null;
            string? report = null;
            bool update = false;
            AuditSuite suite = AuditSuite.Accessibility;
            const string usage = "Usage: AccessibilityAudit --baseline <path> --report <path> [--suite accessibility|dpi|touch] [--update-baseline]";

            for (int index = 0; index < args.Length; index++)
            {
                string arg = args[index];
                if (string.Equals(arg, "--update-baseline", StringComparison.Ordinal))
                {
                    update = true;
                    continue;
                }

                if (string.Equals(arg, "--baseline", StringComparison.Ordinal) && index + 1 < args.Length)
                {
                    baseline = args[++index];
                    continue;
                }

                if (string.Equals(arg, "--report", StringComparison.Ordinal) && index + 1 < args.Length)
                {
                    report = args[++index];
                    continue;
                }

                if (string.Equals(arg, "--suite", StringComparison.Ordinal) && index + 1 < args.Length)
                {
                    suite = ParseSuite(args[++index], usage);
                    continue;
                }

                throw new ArgumentException(usage);
            }

            if (string.IsNullOrWhiteSpace(baseline) || string.IsNullOrWhiteSpace(report))
            {
                throw new ArgumentException(usage);
            }

            return new AuditOptions(baseline, report, update, suite);
        }

        private static AuditSuite ParseSuite(string value, string usage)
        {
            if (string.Equals(value, "accessibility", StringComparison.OrdinalIgnoreCase))
            {
                return AuditSuite.Accessibility;
            }

            if (string.Equals(value, "dpi", StringComparison.OrdinalIgnoreCase))
            {
                return AuditSuite.Dpi;
            }

            if (string.Equals(value, "touch", StringComparison.OrdinalIgnoreCase))
            {
                return AuditSuite.Touch;
            }

            throw new ArgumentException(usage);
        }
    }
}