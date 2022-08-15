using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton.Toolkit
{
    internal class LanguagePackManager : Storage
    {
        #region Instance Fields

        private readonly ArrayList _installedLanguagesList = new();

        #endregion

        #region Public

        public ArrayList InstalledLanguagesList => _installedLanguagesList;

        #endregion

        public override bool IsDefault { get; }

        #region Identity

        public LanguagePackManager()
        {
            
        }

        #endregion

        #region Implementation

        public void GetInstalledLanguagePacks()
        {
            // Code from: https://social.msdn.microsoft.com/Forums/vstudio/en-US/080649c6-6cc1-4230-91d9-ea681777051d/is-it-possible-to-find-os-installed-language-by-c?forum=csharpgeneral

            foreach (InputLanguage language in InputLanguage.InstalledInputLanguages)
            {
                InstalledLanguagesList.Add(language.Culture.EnglishName);
            }
        }

        #endregion
    }
}
