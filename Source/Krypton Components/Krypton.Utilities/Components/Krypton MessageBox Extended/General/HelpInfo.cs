#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

public class HelpInfo
{
    #region Instance Fields

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the HelpInfo class.
    /// </summary>
    /// <param name="helpFilePath">Value for HelpFilePath.</param>
    /// <param name="keyword">Value for Keyword</param>
    public HelpInfo(string? helpFilePath = null, string? keyword = null)
        : this(helpFilePath, keyword, !string.IsNullOrWhiteSpace(keyword) ? HelpNavigator.Topic : HelpNavigator.TableOfContents, null)
    {

    }

    /// <summary>
    /// Initialize a new instance of the HelpInfo class.
    /// </summary>
    /// <param name="helpFilePath">Value for HelpFilePath.</param>
    /// <param name="navigator">Value for Navigator</param>
    /// <param name="param"></param>
    public HelpInfo(string helpFilePath, HelpNavigator navigator, object? param = null)
        : this(helpFilePath, null, navigator, param)
    {

    }

    /// <summary>
    /// Initialize a new instance of the HelpInfo class.
    /// </summary>
    /// <param name="helpFilePath">Value for HelpFilePath.</param>
    /// <param name="navigator">Value for Navigator</param>
    /// <param name="keyword">Value for Keyword</param>
    /// <param name="param"></param>
    private HelpInfo(string? helpFilePath, string? keyword, HelpNavigator navigator, object? param)
    {
        HelpFilePath = helpFilePath ?? string.Empty;
        Keyword = keyword ?? string.Empty;
        Navigator = navigator;
        Param = param ?? string.Empty;
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the HelpFilePath property.
    /// </summary>
    public string HelpFilePath { get; }

    /// <summary>
    /// Gets the Keyword property.
    /// </summary>
    public string Keyword { get; }

    /// <summary>
    /// Gets the Navigator property.
    /// </summary>
    public HelpNavigator Navigator { get; }

    /// <summary>
    /// Gets the Param property.
    /// </summary>
    public object Param { get; }

    #endregion
}