namespace System.Windows.Forms
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct COMDLG_FILTERSPEC
    {
        public string? pszName;
        public string? pszSpec;
    }
}