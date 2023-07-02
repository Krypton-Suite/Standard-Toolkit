#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    [ToolboxItem(false)] //, System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public partial class KryptonCustomPaletteManager : Component
    {
        #region Instance Fields

        private BasePaletteType _paletteType;

        private string _themeName;

        private PaletteBase _basePalette;

        #endregion

        #region Public

        public BasePaletteType PaletteType { get => _paletteType; set => _paletteType = value; }

        public string ThemeName { get => _themeName; private set => _themeName = value; }

        public PaletteBase BasePalette { get => _basePalette; set => _basePalette = value; }

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonCustomPaletteManager" /> class.</summary>
        public KryptonCustomPaletteManager()
        {
            _paletteType = BasePaletteType.Office2007;

            _themeName = null;

            _basePalette = null;
        }

        #endregion

        #region Implementation

        public static void LoadExternalPalette(PaletteBase palette)
        {
            KryptonCustomPaletteManager pm = new KryptonCustomPaletteManager();

            try
            {
                pm.BasePalette = palette;
            }
            catch (Exception e)
            {
                ExceptionHandler.CaptureException(e);
            }
        }

        #endregion
    }
}