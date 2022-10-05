namespace Krypton.Toolkit
{
    /// <summary>
    /// Provides the light gray color scheme variant of the Office 2013 palette.
    /// </summary>
    public class PaletteOffice2013LightGray : PaletteOffice2013Base
    {
        public PaletteOffice2013LightGray(Color[] schemeColors, ImageList checkBoxList, ImageList galleryButtonList, Image[] radioButtonArray, Color[] trackBarColors) : base(schemeColors, checkBoxList, galleryButtonList, radioButtonArray, trackBarColors)
        {
        }

        public override Image GetContextMenuSubMenuImage() => throw new NotImplementedException();
    }
}
