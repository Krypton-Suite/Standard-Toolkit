namespace Krypton.Toolkit
{
    #region Enumerations

    /// <summary>Specifies constants defining the default button on a <seealso cref="KryptonMessageBox"/>.</summary>
    public enum KryptonMessageBoxDefaultButton
    {
        /// <summary>The first button on the message box is the default button.</summary>
        Button1 = 0,
        /// <summary>The second button on the message box is the default button.</summary>
        Button2 = 256,
        /// <summary>The third button on the message box is the default button.</summary>
        Button3 = 512,
        /// <summary>Specifies that the Help button on the message box should be the default button.</summary>
        Button4 = 768,
        /// <summary>The accelerator button.</summary>
        Button5 = 1024
    }

    /// <summary>Specifies a custom color preview shape for a <seealso cref="KryptonColorButton"/>.</summary>
    public enum KryptonColorButtonCustomColorPreviewShape
    {
        Circle = 0,
        Square = 1,
        RoundedSquare = 2,
        None = 3
    }

    #endregion

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
                file.Read(byteBuffer, 0, byteBuffer.Length);
            }

            var headerPosition = BitConverter.ToInt32(byteBuffer, PE_HEADER_OFFSET);

            var secondsSinceUNIXTimeStart = BitConverter.ToInt32(byteBuffer, headerPosition + LINKER_TIMESTAMP_OFFSET);

            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return dateTime.AddSeconds(secondsSinceUNIXTimeStart);
        }
    }

    #endregion
}