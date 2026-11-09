namespace AccessibilityAudit
{
    internal sealed class ControlAuditResult
    {
        public int Visited { get; set; }

        public List<string> FailureKeys { get; } = new List<string>();

        public List<string> HardRoleFailures { get; } = new List<string>();

        public List<string> Notes { get; } = new List<string>();
    }
}