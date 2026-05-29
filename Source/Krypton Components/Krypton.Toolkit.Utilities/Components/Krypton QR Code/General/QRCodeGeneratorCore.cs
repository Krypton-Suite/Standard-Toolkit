#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Native QR code generator. Produces QR code module matrices without external dependencies.
/// </summary>
internal static class QRCodeGeneratorCore
{
    #region Constants

    private const int GF_SIZE = 256;
    private const int GF_PRIMITIVE = 0x11D; // x^8 + x^4 + x^3 + x^2 + 1
    private const int MAX_VERSION = 40;

    #endregion

    #region Capacity Table (Versions 1-40, byte mode)

    /// <summary>Data capacity in bytes for each version (1-40) and ECC level (L, M, Q, H).</summary>
    private static readonly int[,] ByteCapacity =
    {
        { 17, 14, 11, 7 }, // V1
        { 32, 26, 20, 14 }, // V2
        { 53, 42, 32, 24 }, // V3
        { 78, 62, 46, 34 }, // V4
        { 106, 84, 60, 44 }, // V5
        { 134, 106, 74, 58 }, // V6
        { 154, 122, 86, 64 }, // V7
        { 192, 152, 108, 84 }, // V8
        { 230, 180, 130, 98 }, // V9
        { 271, 213, 151, 119 }, // V10
        { 321, 251, 177, 137 }, // V11
        { 367, 287, 203, 155 }, // V12
        { 425, 331, 241, 177 }, // V13
        { 458, 362, 258, 194 }, // V14
        { 520, 412, 292, 220 }, // V15
        { 586, 450, 322, 250 }, // V16
        { 644, 504, 364, 280 }, // V17
        { 718, 560, 394, 310 }, // V18
        { 792, 624, 442, 338 }, // V19
        { 858, 666, 482, 382 }, // V20
        { 929, 711, 509, 403 }, // V21
        { 1003, 779, 565, 439 }, // V22
        { 1091, 857, 611, 461 }, // V23
        { 1171, 911, 661, 511 }, // V24
        { 1273, 997, 715, 535 }, // V25
        { 1367, 1059, 751, 593 }, // V26
        { 1465, 1125, 805, 625 }, // V27
        { 1528, 1190, 868, 658 }, // V28
        { 1628, 1264, 908, 698 }, // V29
        { 1732, 1370, 982, 742 }, // V30
        { 1840, 1452, 1030, 790 }, // V31
        { 1952, 1538, 1112, 842 }, // V32
        { 2068, 1628, 1168, 898 }, // V33
        { 2188, 1722, 1228, 958 }, // V34
        { 2303, 1809, 1283, 983 }, // V35
        { 2431, 1911, 1351, 1051 }, // V36
        { 2563, 1989, 1423, 1093 }, // V37
        { 2699, 2099, 1499, 1139 }, // V38
        { 2809, 2213, 1579, 1219 }, // V39
        { 2953, 2331, 1663, 1273 } // V40
    };

    /// <summary>ECC block structure: [versionIndex, eccLevel] -> (totalDataCodewords, ecPerBlock, block1Count, block1Size, block2Count, block2Size)</summary>
    private static readonly (int TotalData, int EcPerBlock, int Block1Count, int Block1Size, int Block2Count, int Block2Size)[,] EccBlocks =
    {
        { (19, 7, 1, 19, 0, 0), (16, 10, 1, 16, 0, 0), (13, 13, 1, 13, 0, 0), (9, 17, 1, 9, 0, 0) }, // V1
        { (34, 10, 1, 34, 0, 0), (28, 16, 1, 28, 0, 0), (22, 22, 1, 22, 0, 0), (16, 28, 1, 16, 0, 0) }, // V2
        { (55, 15, 1, 55, 0, 0), (44, 26, 1, 44, 0, 0), (34, 18, 2, 17, 0, 0), (26, 22, 2, 13, 0, 0) }, // V3
        { (80, 20, 1, 80, 0, 0), (64, 18, 2, 32, 0, 0), (48, 26, 2, 24, 0, 0), (36, 16, 4, 9, 0, 0) }, // V4
        { (108, 26, 1, 108, 0, 0), (86, 24, 2, 43, 0, 0), (62, 18, 2, 15, 2, 16), (46, 22, 2, 11, 2, 12) }, // V5
        { (136, 18, 2, 68, 0, 0), (108, 16, 4, 27, 0, 0), (76, 24, 4, 19, 0, 0), (60, 28, 4, 15, 0, 0) }, // V6
        { (156, 20, 2, 78, 0, 0), (124, 18, 4, 31, 0, 0), (88, 18, 2, 14, 4, 15), (66, 26, 4, 13, 1, 14) }, // V7
        { (194, 24, 2, 97, 0, 0), (154, 22, 2, 38, 2, 39), (110, 22, 4, 18, 2, 19), (86, 26, 4, 14, 2, 15) }, // V8
        { (232, 30, 2, 116, 0, 0), (182, 22, 3, 36, 2, 37), (132, 20, 4, 16, 4, 17), (100, 24, 4, 12, 4, 13) }, // V9
        { (274, 18, 2, 68, 2, 69), (216, 26, 4, 43, 1, 44), (154, 24, 6, 19, 2, 20), (122, 28, 6, 15, 2, 16) }, // V10
        { (324, 20, 4, 81, 0, 0), (254, 30, 1, 50, 4, 51), (180, 28, 4, 22, 4, 23), (140, 24, 3, 12, 8, 13) }, // V11
        { (370, 24, 2, 92, 2, 93), (290, 22, 6, 36, 2, 37), (206, 26, 4, 20, 6, 21), (158, 28, 7, 14, 4, 15) }, // V12
        { (428, 26, 4, 107, 0, 0), (334, 22, 8, 37, 1, 38), (244, 24, 8, 20, 4, 21), (180, 22, 12, 11, 4, 12) }, // V13
        { (461, 30, 3, 115, 1, 116), (365, 24, 4, 40, 5, 41), (261, 20, 11, 16, 5, 17), (197, 24, 11, 12, 5, 13) }, // V14
        { (523, 22, 5, 87, 1, 88), (415, 24, 5, 41, 5, 42), (295, 30, 5, 24, 7, 25), (223, 24, 11, 12, 7, 13) }, // V15
        { (589, 24, 5, 98, 1, 99), (453, 28, 7, 45, 3, 46), (325, 24, 15, 19, 2, 20), (253, 30, 3, 15, 13, 16) }, // V16
        { (647, 28, 1, 107, 5, 108), (507, 28, 10, 46, 1, 47), (367, 28, 1, 22, 15, 23), (283, 28, 2, 14, 17, 15) }, // V17
        { (721, 30, 5, 120, 1, 121), (563, 26, 9, 43, 4, 44), (397, 28, 17, 22, 1, 23), (313, 28, 2, 14, 19, 15) }, // V18
        { (795, 28, 3, 113, 4, 114), (627, 26, 3, 44, 11, 45), (445, 26, 17, 21, 4, 22), (341, 26, 9, 13, 16, 14) }, // V19
        { (861, 28, 3, 107, 5, 108), (669, 26, 3, 41, 13, 42), (485, 30, 15, 24, 5, 25), (385, 28, 15, 15, 10, 16) }, // V20
        { (932, 28, 4, 116, 4, 117), (714, 26, 17, 42, 0, 0), (512, 28, 17, 22, 6, 23), (406, 30, 19, 16, 6, 17) }, // V21
        { (1006, 28, 2, 111, 7, 112), (782, 28, 17, 46, 0, 0), (568, 30, 7, 24, 16, 25), (442, 24, 34, 13, 0, 0) }, // V22
        { (1094, 30, 4, 121, 5, 122), (860, 28, 4, 47, 14, 48), (614, 30, 11, 24, 14, 25), (464, 30, 16, 15, 14, 16) }, // V23
        { (1174, 30, 6, 117, 4, 118), (914, 28, 6, 45, 14, 46), (664, 30, 11, 24, 16, 25), (514, 30, 30, 16, 2, 17) }, // V24
        { (1276, 26, 8, 106, 4, 107), (1000, 28, 8, 47, 13, 48), (718, 30, 7, 24, 22, 25), (538, 30, 22, 15, 13, 16) }, // V25
        { (1370, 28, 10, 114, 2, 115), (1062, 28, 19, 46, 4, 47), (754, 28, 28, 22, 6, 23), (596, 30, 33, 16, 4, 17) }, // V26
        { (1468, 30, 8, 122, 4, 123), (1128, 28, 22, 45, 3, 46), (808, 30, 8, 23, 26, 24), (628, 30, 12, 15, 28, 16) }, // V27
        { (1531, 30, 3, 117, 10, 118), (1193, 28, 3, 45, 23, 46), (871, 30, 4, 24, 31, 25), (661, 30, 11, 15, 31, 16) }, // V28
        { (1631, 30, 7, 116, 7, 117), (1267, 28, 21, 45, 7, 46), (911, 30, 1, 23, 37, 24), (701, 30, 19, 15, 26, 16) }, // V29
        { (1735, 30, 5, 115, 10, 116), (1373, 28, 19, 47, 10, 48), (985, 30, 15, 24, 25, 25), (745, 30, 23, 15, 25, 16) }, // V30
        { (1843, 30, 13, 115, 3, 116), (1455, 28, 2, 46, 29, 47), (1033, 30, 42, 24, 1, 25), (793, 30, 23, 15, 28, 16) }, // V31
        { (1955, 30, 17, 115, 0, 0), (1541, 28, 10, 46, 23, 47), (1115, 30, 10, 24, 35, 25), (845, 30, 19, 15, 35, 16) }, // V32
        { (2071, 30, 17, 115, 1, 116), (1631, 28, 14, 46, 21, 47), (1171, 30, 29, 24, 19, 25), (901, 30, 11, 15, 46, 16) }, // V33
        { (2191, 30, 13, 115, 6, 116), (1725, 28, 14, 46, 23, 47), (1231, 30, 44, 24, 7, 25), (961, 30, 59, 16, 1, 17) }, // V34
        { (2306, 30, 12, 121, 7, 122), (1812, 28, 12, 47, 26, 48), (1286, 30, 39, 24, 14, 25), (986, 30, 22, 15, 41, 16) }, // V35
        { (2434, 30, 6, 121, 14, 122), (1914, 28, 6, 47, 34, 48), (1354, 30, 46, 24, 10, 25), (1054, 30, 2, 15, 64, 16) }, // V36
        { (2566, 30, 17, 122, 4, 123), (1992, 28, 29, 46, 14, 47), (1426, 30, 49, 24, 10, 25), (1096, 30, 24, 15, 46, 16) }, // V37
        { (2702, 30, 4, 122, 18, 123), (2102, 28, 13, 46, 32, 47), (1502, 30, 48, 24, 14, 25), (1142, 30, 42, 15, 32, 16) }, // V38
        { (2812, 30, 20, 117, 4, 118), (2216, 28, 40, 47, 7, 48), (1582, 30, 43, 24, 22, 25), (1222, 30, 10, 15, 67, 16) }, // V39
        { (2956, 30, 19, 118, 6, 119), (2334, 28, 18, 47, 31, 48), (1666, 30, 34, 24, 34, 25), (1276, 30, 20, 15, 61, 16) } // V40
    };

    /// <summary>Alignment pattern center coordinates per version (index = version − 1). From ISO/IEC 18004.</summary>
    private static readonly int[][] AlignmentCenters =
    [
        [],
        [6, 18],
        [6, 22],
        [6, 26],
        [6, 30],
        [6, 34],
        [6, 22, 38],
        [6, 24, 42],
        [6, 26, 46],
        [6, 28, 50],
        [6, 30, 54],
        [6, 32, 58],
        [6, 34, 62],
        [6, 26, 46, 66],
        [6, 26, 48, 70],
        [6, 26, 50, 74],
        [6, 30, 54, 78],
        [6, 30, 56, 82],
        [6, 30, 58, 86],
        [6, 34, 62, 90],
        [6, 28, 50, 72, 94],
        [6, 26, 50, 74, 98],
        [6, 30, 54, 78, 102],
        [6, 28, 54, 80, 106],
        [6, 32, 58, 84, 110],
        [6, 30, 58, 86, 114],
        [6, 34, 62, 90, 118],
        [6, 26, 50, 74, 98, 122],
        [6, 30, 54, 78, 102, 126],
        [6, 26, 52, 78, 104, 130],
        [6, 30, 56, 82, 108, 134],
        [6, 34, 60, 86, 112, 138],
        [6, 30, 58, 86, 114, 142],
        [6, 34, 62, 90, 118, 146],
        [6, 30, 54, 78, 102, 126, 150],
        [6, 24, 50, 76, 102, 128, 154],
        [6, 28, 54, 80, 106, 132, 158],
        [6, 32, 58, 84, 110, 136, 162],
        [6, 26, 54, 82, 110, 138, 166],
        [6, 30, 58, 86, 114, 142, 170]
    ];

    /// <summary>BCH(version, 6) &lt;&lt; 12 | version, 18 bits, for versions 7–40 (index = version − 7).</summary>
    private static readonly int[] VersionInfoBch =
    [
        31892, 34236, 39577, 42195, 48118, 51042, 55367, 58893, 63784, 68472, 70749, 76311, 79154, 84390, 87683,
        92361, 96236, 102084, 102881, 110507, 110734, 117786, 119615, 126325, 127568, 133589, 136944, 141498,
        145311, 150283, 152622, 158308, 161089, 167017
    ];

    /// <summary>Format string bits for each (eccLevel, maskPattern). 15 bits.</summary>
    private static readonly int[,] FormatBits =
    {
        { 0x77C4, 0x72F3, 0x7DAA, 0x789D, 0x662F, 0x6318, 0x6C41, 0x6976 }, // L
        { 0x5412, 0x5125, 0x5E7C, 0x5B4B, 0x45F9, 0x40CE, 0x4F97, 0x4AA0 }, // M
        { 0x355F, 0x3068, 0x3F31, 0x3A06, 0x24B4, 0x2183, 0x2EDA, 0x2BED }, // Q
        { 0x1689, 0x13BE, 0x1CE7, 0x19D0, 0x0762, 0x0255, 0x0D0C, 0x083B }  // H
    };

    #endregion

    #region Public API

    /// <summary>
    /// Generates a QR code module matrix for the given content.
    /// </summary>
    /// <param name="content">The text or data to encode (UTF-8).</param>
    /// <param name="eccLevel">Error correction level.</param>
    /// <returns>A 2D bool array where true = dark module.</returns>
    public static bool[,] Generate(string content, QRErrorCorrectionLevel eccLevel)
    {
        if (string.IsNullOrEmpty(content))
        {
            throw new ArgumentException(@"Content cannot be null or empty.", nameof(content));
        }

        byte[] data = Encoding.UTF8.GetBytes(content);
        int version = GetMinimumVersion(data.Length, eccLevel);
        byte[] dataCodewords = EncodeData(data, version, eccLevel);
        byte[] fullMessage = AddErrorCorrection(dataCodewords, version, eccLevel);
        return BuildMatrix(fullMessage, version, eccLevel);
    }

    /// <summary>
    /// Generates a QR code module matrix for raw bytes.
    /// </summary>
    public static bool[,] Generate(byte[] data, QRErrorCorrectionLevel eccLevel)
    {
        if (data == null || data.Length == 0)
        {
            throw new ArgumentException(@"Data cannot be null or empty.", nameof(data));
        }

        int version = GetMinimumVersion(data.Length, eccLevel);
        byte[] dataCodewords = EncodeData(data, version, eccLevel);
        byte[] fullMessage = AddErrorCorrection(dataCodewords, version, eccLevel);
        return BuildMatrix(fullMessage, version, eccLevel);
    }

    #endregion

    #region Version Selection

    private static int GetMinimumVersion(int byteCount, QRErrorCorrectionLevel eccLevel)
    {
        int eccIndex = (int)eccLevel;
        for (int v = 0; v < MAX_VERSION; v++)
        {
            if (ByteCapacity[v, eccIndex] >= byteCount)
            {
                return v + 1;
            }
        }

        throw new ArgumentException(
            $@"Data too long for QR code. Maximum ~{ByteCapacity[MAX_VERSION - 1, eccIndex]} bytes for ECC {eccLevel} (version {MAX_VERSION}).",
            nameof(byteCount));
    }

    #endregion

    #region Data Encoding (Byte Mode)

    private static byte[] EncodeData(byte[] data, int version, QRErrorCorrectionLevel eccLevel)
    {
        int capacity = ByteCapacity[version - 1, (int)eccLevel];
        if (data.Length > capacity)
        {
            throw new ArgumentException($@"Data exceeds capacity for version {version}.", nameof(data));
        }

        int countIndicatorBits = version < 10 ? 8 : 16;
        int totalBits = 4 + countIndicatorBits + (data.Length * 8); // Mode(4) + Count(8/16) + Data
        int totalDataBits = EccBlocks[version - 1, (int)eccLevel].TotalData * 8;
        int padBits = totalDataBits - totalBits;

        var bits = new List<bool>();
        bits.AddRange(ToBits(4, 4));  // Byte mode indicator
        bits.AddRange(ToBits(data.Length, countIndicatorBits));
        foreach (byte b in data)
        {
            bits.AddRange(ToBits(b, 8));
        }

        // Terminator (up to 4 zeros)
        for (int i = 0; i < 4 && bits.Count < totalDataBits; i++)
        {
            bits.Add(false);
        }

        // Pad to byte boundary
        while (bits.Count % 8 != 0)
        {
            bits.Add(false);
        }

        // Padding bytes: 11101100 00010001 alternating
        byte[] padBytes = [0xEC, 0x11];
        int padIndex = 0;
        while (bits.Count < totalDataBits)
        {
            bits.AddRange(ToBits(padBytes[padIndex], 8));
            padIndex = 1 - padIndex;
        }

        return BitsToBytes(bits);
    }

    #endregion

    #region Reed-Solomon Error Correction

    private static byte[] AddErrorCorrection(byte[] dataCodewords, int version, QRErrorCorrectionLevel eccLevel)
    {
        var ecc = EccBlocks[version - 1, (int)eccLevel];
        var allBlocks = new List<byte[]>();
        int offset = 0;

        for (int i = 0; i < ecc.Block1Count; i++)
        {
            byte[] block = new byte[ecc.Block1Size + ecc.EcPerBlock];
            Array.Copy(dataCodewords, offset, block, 0, ecc.Block1Size);
            ReedSolomonEncode(block, ecc.Block1Size, ecc.EcPerBlock);
            allBlocks.Add(block);
            offset += ecc.Block1Size;
        }

        for (int i = 0; i < ecc.Block2Count; i++)
        {
            byte[] block = new byte[ecc.Block2Size + ecc.EcPerBlock];
            Array.Copy(dataCodewords, offset, block, 0, ecc.Block2Size);
            ReedSolomonEncode(block, ecc.Block2Size, ecc.EcPerBlock);
            allBlocks.Add(block);
            offset += ecc.Block2Size;
        }

        return Interleave(allBlocks, ecc);
    }

    private static void ReedSolomonEncode(byte[] data, int dataLen, int ecCount)
    {
        int[] toEncode = new int[dataLen + ecCount];
        for (int i = 0; i < dataLen; i++)
        {
            toEncode[i] = data[i] & 0xFF;
        }

        int[] generator = GetGeneratorPolynomial(ecCount);
        for (int i = 0; i < dataLen; i++)
        {
            int coef = toEncode[i];
            for (int j = 0; j < generator.Length; j++)
            {
                toEncode[i + j] ^= GfMultiply(generator[j], coef);
            }
        }

        for (int i = 0; i < ecCount; i++)
        {
            data[dataLen + i] = (byte)toEncode[dataLen + i];
        }
    }

    /// <summary>Build generator polynomial (x-α^0)(x-α^1)...(x-α^{degree-1}). Returns [0]=1, [1..degree]=remaining coefficients.</summary>
    private static int[] GetGeneratorPolynomial(int degree)
    {
        InitGaloisField();
        // Start as constant 1; each iteration multiplies by (x + α^i). Final length = degree + 1.
        int[] poly = [1];
        for (int i = 0; i < degree; i++)
        {
            int alphaI = GfExp[i];
            int[] next = new int[poly.Length + 1];
            next[0] = poly[0];
            for (int j = 1; j < poly.Length; j++)
            {
                next[j] = poly[j] ^ GfMultiply(alphaI, poly[j - 1]);
            }
            next[poly.Length] = GfMultiply(alphaI, poly[poly.Length - 1]);
            poly = next;
        }
        return poly;
    }

    private static byte[] Interleave(List<byte[]> blocks, (int TotalData, int EcPerBlock, int Block1Count, int Block1Size, int Block2Count, int Block2Size) ecc)
    {
        int maxData = Math.Max(ecc.Block1Size, ecc.Block2Size);
        var result = new List<byte>();

        for (int i = 0; i < maxData; i++)
        {
            foreach (var block in blocks)
            {
                int dataLen = block.Length - ecc.EcPerBlock;
                if (i < dataLen)
                {
                    result.Add(block[i]);
                }
            }
        }

        for (int i = 0; i < ecc.EcPerBlock; i++)
        {
            foreach (var block in blocks)
            {
                result.Add(block[block.Length - ecc.EcPerBlock + i]);
            }
        }

        return result.ToArray();
    }

    #endregion

    #region Galois Field (GF256)

    private static readonly int[] GfExp = new int[512];
    private static readonly int[] GfLog = new int[256];
    private static bool _gfInitialized;

    private static void InitGaloisField()
    {
        if (_gfInitialized)
        {
            return;
        }

        int x = 1;
        for (int i = 0; i < 255; i++)
        {
            GfExp[i] = x;
            GfLog[x] = i;
            x <<= 1;
            if (x >= 256)
            {
                x ^= GF_PRIMITIVE;
            }
        }
        for (int i = 255; i < 512; i++)
        {
            GfExp[i] = GfExp[i - 255];
        }
        _gfInitialized = true;
    }

    private static int GfMultiply(int a, int b)
    {
        if (a == 0 || b == 0)
        {
            return 0;
        }

        InitGaloisField();
        return GfExp[GfLog[a] + GfLog[b]];
    }

    #endregion

    #region Bit Helpers

    private static IEnumerable<bool> ToBits(int value, int bitCount)
    {
        for (int i = bitCount - 1; i >= 0; i--)
        {
            yield return ((value >> i) & 1) != 0;
        }
    }

    private static byte[] BitsToBytes(List<bool> bits)
    {
        byte[] result = new byte[bits.Count / 8];
        for (int i = 0; i < result.Length; i++)
        {
            int b = 0;
            for (int j = 0; j < 8; j++)
            {
                if (bits[i * 8 + j])
                {
                    b |= 1 << (7 - j);
                }
            }
            result[i] = (byte)b;
        }
        return result;
    }

    #endregion

    #region Matrix Construction

    private static bool[,] BuildMatrix(byte[] codewords, int version, QRErrorCorrectionLevel eccLevel)
    {
        int size = 17 + version * 4;
        var matrix = new bool[size, size];
        int matrixSize = size;

        PlaceFinderPatterns(matrix);
        PlaceTimingPatterns(matrix);
        PlaceAlignmentPatterns(matrix, version);

        int codewordIndex = 0;
        int bitIndex = 7;
        bool up = true;
        int col = matrixSize - 1;

        while (col > 0)
        {
            if (col == 6)
            {
                col = 5;
            }

            for (int row = up ? matrixSize - 1 : 0; up ? row >= 0 : row < matrixSize; row += up ? -1 : 1)
            {
                for (int c = 0; c < 2; c++)
                {
                    int actualCol = col - c;
                    if (actualCol < 0)
                    {
                        break;
                    }

                    if (IsReserved(matrix, row, actualCol, version))
                    {
                        continue;
                    }

                    bool dark = false;
                    if (codewordIndex < codewords.Length)
                    {
                        dark = ((codewords[codewordIndex] >> bitIndex) & 1) != 0;
                        bitIndex--;
                        if (bitIndex < 0)
                        {
                            bitIndex = 7;
                            codewordIndex++;
                        }
                    }
                    matrix[row, actualCol] = dark;
                }
            }
            up = !up;
            col -= 2;
        }

        ApplyMask(matrix, version, eccLevel);
        PlaceFormatInfo(matrix, eccLevel, 0);
        PlaceVersionInfo(matrix, version);

        return matrix;
    }

    private static void PlaceFinderPatterns(bool[,] matrix)
    {
        int height = matrix.GetLength(0);
        int width = matrix.GetLength(1);
        if (height < 7 || width < 7)
        {
            return;
        }

        int[] rowStarts = [0, height - 7];
        int[] colStarts = [0, width - 7];
        foreach (int row in rowStarts)
        {
            foreach (int col in colStarts)
            {
                for (int r = 0; r < 7; r++)
                {
                    for (int c = 0; c < 7; c++)
                    {
                        int rr = row + r;
                        int cc = col + c;
                        if ((uint)rr < (uint)height && (uint)cc < (uint)width)
                        {
                            bool fill = r == 0 || r == 6 || c == 0 || c == 6 || (r is >= 2 and <= 4 && c is >= 2 and <= 4);
                            matrix[rr, cc] = fill;
                        }
                    }
                }

                // One-module separator inside the symbol; up to 8 cells toward data/timing (clipped at edges).
                for (int i = 0; i < 8; i++)
                {
                    int hr = row - 1;
                    int hc = col + i;
                    if ((uint)hr < (uint)height && (uint)hc < (uint)width)
                    {
                        matrix[hr, hc] = false;
                    }

                    int vr = row + i;
                    int vc = col - 1;
                    if ((uint)vr < (uint)height && (uint)vc < (uint)width)
                    {
                        matrix[vr, vc] = false;
                    }
                }
            }
        }

        for (int r = 0; r < 7; r++)
        {
            for (int c = 0; c < 7; c++)
            {
                if ((uint)r < (uint)height && (uint)c < (uint)width)
                {
                    bool fill = r == 0 || r == 6 || c == 0 || c == 6 || (r is >= 2 and <= 4 && c is >= 2 and <= 4);
                    matrix[r, c] = fill;
                }
            }
        }
    }

    private static void PlaceTimingPatterns(bool[,] matrix)
    {
        int size = matrix.GetLength(0);
        for (int i = 8; i < size - 8; i++)
        {
            matrix[6, i] = i % 2 == 0;
            matrix[i, 6] = i % 2 == 0;
        }
    }

    private static void PlaceAlignmentPatterns(bool[,] matrix, int version)
    {
        if (version < 2)
        {
            return;
        }

        int[] positions = GetAlignmentPositions(version);
        foreach (int row in positions)
        {
            foreach (int col in positions)
            {
                if ((row < 9 && col < 9) || (row < 9 && col > matrix.GetLength(0) - 10) || (row > matrix.GetLength(0) - 10 && col < 9))
                {
                    continue;
                }

                for (int r = -2; r <= 2; r++)
                {
                    for (int c = -2; c <= 2; c++)
                    {
                        bool fill = Math.Abs(r) == 2 || Math.Abs(c) == 2 || (r == 0 && c == 0);
                        matrix[row + r, col + c] = fill;
                    }
                }
            }
        }
    }

    private static int[] GetAlignmentPositions(int version)
    {
        if (version is < 1 or > MAX_VERSION)
        {
            return [];
        }

        return AlignmentCenters[version - 1];
    }

    private static bool IsReserved(bool[,] matrix, int row, int col, int version)
    {
        int size = matrix.GetLength(0);
        switch (row)
        {
            case < 9 when col < 9:
            case < 9 when col > size - 9:
                return true;
        }

        if (row > size - 9 && col < 9)
        {
            return true;
        }

        if (row == 6 || col == 6)
        {
            return true;
        }

        if (version >= 7)
        {
            int x = size - 11;
            if (row is >= 0 and <= 5 && col >= x && col <= x + 2)
            {
                return true;
            }

            if (col is >= 0 and <= 5 && row >= x && row <= x + 2)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>Places 18-bit version information (versions ≥ 7) after masking, matching ISO/IEC 18004 layout.</summary>
    private static void PlaceVersionInfo(bool[,] matrix, int version)
    {
        if (version < 7)
        {
            return;
        }

        int size = matrix.GetLength(0);
        int bits = VersionInfoBch[version - 7];
        for (int i = 0; i < 18; i++)
        {
            bool dark = ((bits >> i) & 1) != 0;
            matrix[i / 3, i % 3 + size - 11] = dark;
            matrix[i % 3 + size - 11, i / 3] = dark;
        }
    }

    private static void ApplyMask(bool[,] matrix, int version, QRErrorCorrectionLevel eccLevel)
    {
        int size = matrix.GetLength(0);
        int formatBits = FormatBits[(int)eccLevel, 0];

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                if (matrix[row, col])
                {
                    continue;
                }

                bool mask = ((row + col) % 2) == 0;
                matrix[row, col] = mask;
            }
        }
    }

    private static void PlaceFormatInfo(bool[,] matrix, QRErrorCorrectionLevel eccLevel, int maskPattern)
    {
        int bits = FormatBits[(int)eccLevel, maskPattern];
        int size = matrix.GetLength(0);

        for (int i = 0; i < 15; i++)
        {
            bool dark = ((bits >> (14 - i)) & 1) != 0;
            switch (i)
            {
                case < 6:
                    matrix[8, i] = dark;
                    break;
                case < 8:
                    matrix[8, i + 1] = dark;
                    break;
                default:
                    matrix[8, size - 15 + i] = dark;
                    break;
            }

            switch (i)
            {
                case < 8:
                    matrix[size - 1 - i, 8] = dark;
                    break;
                case < 9:
                    matrix[15 - i, 8] = dark;
                    break;
                default:
                    matrix[14 - i, 8] = dark;
                    break;
            }
        }
        matrix[size - 8, 8] = true;
    }

    #endregion
}
