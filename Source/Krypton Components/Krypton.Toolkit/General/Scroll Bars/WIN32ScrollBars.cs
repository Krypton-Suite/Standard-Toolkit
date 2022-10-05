namespace Krypton.Toolkit
{
    public class WIN32ScrollBars
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct ScrollInfo
        {
            public int cbSize;
            public int fMask;
            public int nMin;
            public int nMax;
            public int nPage;
            public int nPos;
            public int nTrackPos;
        }
    }
}
