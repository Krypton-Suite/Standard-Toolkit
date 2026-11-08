namespace AccessibilityAudit
{
    internal sealed class ContrastSample
    {
        public ContrastSample(string key, string theme, string style, string state, double ratio, double threshold, string fore, string back, bool countsAsFailure)
        {
            Key = key;
            Theme = theme;
            Style = style;
            State = state;
            Ratio = ratio;
            Threshold = threshold;
            Fore = fore;
            Back = back;
            CountsAsFailure = countsAsFailure;
        }

        public string Key { get; }

        public string Theme { get; }

        public string Style { get; }

        public string State { get; }

        public double Ratio { get; }

        public double Threshold { get; }

        public string Fore { get; }

        public string Back { get; }

        public bool CountsAsFailure { get; }
    }
}