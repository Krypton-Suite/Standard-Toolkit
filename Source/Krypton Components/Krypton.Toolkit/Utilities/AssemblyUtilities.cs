#region BSD License
/*
 * 
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

#region Class: AssemblyUtilities

public class AssemblyUtilities
{
    //https://www.meziantou.net/getting-the-date-of-build-of-a-dotnet-assembly-at-runtime.htm
    public static DateTime GetLinkerTimestampUTC(Assembly assembly)
    {
        var location = assembly.Location;

        return GetLinkerTimestampUTC(location);
    }

    public static DateTime GetLinkerTimestampUTC(string filePath)
    {
        const int PE_HEADER_OFFSET = 60;

        const int LINKER_TIMESTAMP_OFFSET = 8;

        var byteBuffer = new byte[2048];

        using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            var read = file.Read(byteBuffer, 0, byteBuffer.Length);

            Console.WriteLine(read);
        }

        var headerPosition = BitConverter.ToInt32(byteBuffer, PE_HEADER_OFFSET);

        var secondsSinceUNIXTimeStart = BitConverter.ToInt32(byteBuffer, headerPosition + LINKER_TIMESTAMP_OFFSET);

        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        return dateTime.AddSeconds(secondsSinceUNIXTimeStart);
    }
}

#endregion