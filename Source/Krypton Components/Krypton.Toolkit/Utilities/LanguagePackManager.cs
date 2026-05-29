#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

internal class LanguagePackManager : Storage
{
    #region Instance Fields

    private readonly ArrayList _installedLanguagesList = [];

    #endregion

    #region Public

    public ArrayList InstalledLanguagesList => _installedLanguagesList;

    #endregion

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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