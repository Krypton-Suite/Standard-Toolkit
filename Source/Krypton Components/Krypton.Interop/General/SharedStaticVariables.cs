#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

// ReSharper disable InconsistentNaming
namespace Krypton.Interop;

/// <summary>Provides a collection of static values, used within the toolkit.</summary>
public class SharedStaticVariables
{
    /// <summary>The default highlight debugging color</summary>
    public static Color DEFAULT_HIGHLIGHT_DEBUGGING_COLOR = Color.Red;

    // Used for version reporting
    internal static string DEFAULT_DOCKING_FILE = @"Krypton.Docking.dll";
    internal static string DEFAULT_NAVIGATOR_FILE = @"Krypton.Navigator.dll";
    internal static string DEFAULT_RIBBON_FILE = @"Krypton.Ribbon.dll";
    internal static string DEFAULT_TOOLKIT_FILE = @"Krypton.Toolkit.dll";
    internal static string DEFAULT_WORKSPACE_FILE = @"Krypton.Workspace.dll";

    internal const string DEFAULT_NOT_IMPLEMENTED_YET_MESSAGE =
        $"This feature has not been currently implemented yet.\nPlease check back again soon!";

    internal static string DEFAULT_EMPTY_STRING = string.Empty;

    /// <summary>The OS major version</summary>
    public static readonly int OS_MAJOR_VERSION = Environment.OSVersion.Version.Major;

    /// <summary>The empty color</summary>
    public static readonly Color EMPTY_COLOR = Color.Empty;

    /// <summary>The transparency key color</summary>
    public static readonly Color TRANSPARENCY_KEY_COLOR = Color.Magenta;

    /// <summary>The tab row gradient first color</summary>
    public static readonly Color TAB_ROW_GRADIENT_FIRST_COLOR = Color.Transparent;

    /// <summary>The default ribbon application button dark color</summary>
    public static readonly Color DEFAULT_RIBBON_FILE_APP_TAB_BOTTOM_COLOR = Color.FromArgb(31, 72, 161);

    /// <summary>The default ribbon application button light color</summary>
    public static readonly Color DEFAULT_RIBBON_FILE_APP_TAB_TOP_COLOR = Color.FromArgb(84, 158, 243);

    /// <summary>The default ribbon application button text color</summary>
    public static readonly Color DEFAULT_RIBBON_FILE_APP_TAB_TEXT_COLOR = Color.White;

    /// <summary>
    /// The default group row height
    /// </summary>
    public static int DefaultGroupRowHeight = 34;
    /// <summary>
    /// The group row height for 2013 palettes
    /// </summary>
    public static int Office2013GroupRowHeight = 24;
    /// <summary>
    /// The default offset height
    /// </summary>
    public static int DefaultOffsetHeight = 22;
    /// <summary>
    /// The offset height for 2013 palettes
    /// </summary>
    public static int Office2013OffsetHeight = 11;
    /// <summary>
    /// The image offset width
    /// </summary>
    public static int ImageOffsetWidth = 18;
    /// <summary>
    /// The group level multiplier
    /// </summary>
    public static int GroupLevelMultiplier = 15;
    /// <summary>
    /// The group image side size
    /// </summary>
    public static int GroupImageSide = 16;

    // For when we need some text to test with
    public static readonly string DEFAULT_SHORT_SEED_TEXT = $"Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)\r\n\u00a9 Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.\r\n\r\nNew BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)\r\nModifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - {DateTime.Now.Year}. All rights reserved.";

    /// <summary>
    /// The default long seed text
    /// </summary>
    public static readonly string DEFAULT_LONG_SEED_TEXT = $"BSD 3-Clause License\r\n\r\nCopyright (c) 2017 - {DateTime.Now.Year}, Krypton Suite\r\n\r\nAll rights reserved.\r\n\r\nRedistribution and use in source and binary forms, with or without\r\nmodification, are permitted provided that the following conditions are met:\r\n\r\n1. Redistributions of source code must retain the above copyright notice, this\r\n   list of conditions and the following disclaimer.\r\n\r\n2. Redistributions in binary form must reproduce the above copyright notice,\r\n   this list of conditions and the following disclaimer in the documentation\r\n   and/or other materials provided with the distribution.\r\n\r\n3. Neither the name of the copyright holder nor the names of its\r\n   contributors may be used to endorse or promote products derived from\r\n   this software without specific prior written permission.\r\n\r\nTHIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS \"AS IS\"\r\nAND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE\r\nIMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE\r\nDISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE\r\nFOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL\r\nDAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR\r\nSERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER\r\nCAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,\r\nOR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE\r\nOF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.";

    /// <summary>
    /// The toolkit DLL names, used for version reporting and other internal uses. The order of the names in this array should be the same as the order of the versions in the GlobalStaticFunctions.GetToolkitVersion method.
    /// </summary>
    public static readonly string[] TOOLKIT_DLL_NAMES =
    [
        "Krypton.Docking.dll",
        "Krypton.Navigator.dll",
        "Krypton.Ribbon.dll",
        "Krypton.Toolkit.dll",
        "Krypton.Workspace.dll"
    ];
}