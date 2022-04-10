namespace Krypton.Toolkit
{
    public class KryptonPaletteCueHintText : Storage
    {
        #region Identity

        public KryptonPaletteCueHintText(NeedPaintHandler needPaint)
        {
            NeedPaint = needPaint;

            CueColor = Color.Empty;

            CueFont = null;
        }

        #endregion
        public override bool IsDefault => (CueColor == Color.Empty) && (CueFont == null);

        #region Colour

        public Color CueColor { get; set; }

        public void ResetCueColor() => CueColor = Color.Empty;

        #endregion

        #region Font

        public Font CueFont { get; set; }

        public void ResetCueFont() => CueFont = null;

        #endregion
    }
}
