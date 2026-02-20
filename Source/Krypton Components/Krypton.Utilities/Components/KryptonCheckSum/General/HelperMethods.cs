#region MIT License
/*
 * MIT License
 *
 * Copyright (c) 2017 - 2026 Krypton Suite
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *
 */
#endregion

namespace Krypton.Utilities;

public class HelperMethods
{
    #region Instance Fields

    private readonly string[] _hashTypes = ["MD5", "SHA1", "SHA256", "SHA384", "SHA512", "RIPEMD160"];

    private readonly string[] _safeNETAndNewerHashTypes = ["MD5", "SHA1", "SHA256", "SHA384", "SHA512"];

    #endregion

    #region Public

    public string[] HashTypes => _hashTypes;

    public string[] SafeNetAndNewerHashTypes => _safeNETAndNewerHashTypes;

    #endregion

    #region Implementation
    public static void PropagateHashBox(KryptonComboBox hashBox)
    {
        HelperMethods helperMethods = new HelperMethods();

#if NET8_0_OR_GREATER
            foreach (string hashType in helperMethods.SafeNetAndNewerHashTypes)
	        {
                hashBox.Items.Add(hashType);
	        }
#else
        foreach (string hashType in helperMethods.HashTypes)
        {
            hashBox.Items.Add(hashType);
        }
#endif
    }

    public static bool IsValid(string fileCheckSum, string checkSumToCompare) => checkSumToCompare.Contains(fileCheckSum);
    #endregion
}