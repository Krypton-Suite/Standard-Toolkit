namespace Krypton.Toolkit
{
    public class PaletteOffice2007LightGray : PaletteOffice2007Base
    {
        public PaletteOffice2007LightGray(Color[] schemeColors, ImageList checkBoxList, ImageList galleryButtonList, Image[] radioButtonArray, Color[] trackBarColors) : base(schemeColors, checkBoxList, galleryButtonList, radioButtonArray, trackBarColors)
        {
        }

        public override Image GetContextMenuSubMenuImage()
        {
            throw new NotImplementedException();
        }
    }
}
