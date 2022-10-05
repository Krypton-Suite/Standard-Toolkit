namespace Krypton.Toolkit
{
    public class PaletteOffice365LightGray : PaletteOffice365Base
    {
        public PaletteOffice365LightGray(Color[] schemeColours, ImageList checkBoxList, ImageList galleryButtonList, Image[] radioButtonArray, Color[] trackBarColours) : base(schemeColours, checkBoxList, galleryButtonList, radioButtonArray, trackBarColours)
        {
        }

        public override Image GetContextMenuSubMenuImage()
        {
            throw new NotImplementedException();
        }
    }
}
