using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton.Toolkit
{
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
            KryptonCustomPaletteManager pm = new();

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