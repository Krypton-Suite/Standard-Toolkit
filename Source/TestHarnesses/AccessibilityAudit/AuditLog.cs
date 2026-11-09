namespace AccessibilityAudit
{
    internal static class AuditLog
    {
        public static TextWriter? Writer { get; set; }

        public static void Write(string message)
        {
            try
            {
                Console.WriteLine(message);
            }
            catch (Exception)
            {
                // WinExe has no console unless a parent attached one.
            }

            try
            {
                Writer?.WriteLine(message);
            }
            catch (Exception)
            {
                // The progress log is best-effort.
            }
        }
    }
}