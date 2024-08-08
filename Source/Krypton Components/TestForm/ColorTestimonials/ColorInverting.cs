namespace TestForm
{
    public class ColorInverting
    {

        /// <summary>
        /// Maximum value a channel van have.
        /// </summary>
        public static int ChannelMaxValue => 255;

        /// <summary>
        /// Minimum value a channel van have.
        /// </summary>
        public static int ChannelMinValue => 0;

        /// <summary>
        /// Inverts the given input integers to a color.<br/>
        /// The Alpha channel will be set to 255, which means no transparency.
        /// </summary>
        /// <param name="red">Red channel.</param>
        /// <param name="green">Green channel.</param>
        /// <param name="blue">Blue channel.</param>
        /// <returns>Resultant color.</returns>
        public static Color InvertRGBFromInt(byte red, byte green, byte blue)
        {
            return Color.FromArgb(
                ChannelMaxValue,
                ChannelMaxValue - red,
                ChannelMaxValue - green,
                ChannelMaxValue - blue);
        }

        /// <summary>
        /// Inverts the given input integers to a color.<br/>
        /// This method also inverts the Alpha channel.
        /// </summary>
        /// <param name="alpha">Alpa channel (transparency).</param>
        /// <param name="red">Red channel.</param>
        /// <param name="green">Green channel.</param>
        /// <param name="blue">Blue channel.</param>
        /// <returns>Resultant color.</returns>
        /// 
        public static Color InvertARGBFromInt(byte alpha, byte red, byte green, byte blue)
        {
            return Color.FromArgb(
                ChannelMaxValue - alpha,
                ChannelMaxValue - red,
                ChannelMaxValue - green,
                ChannelMaxValue - blue);
        }

        /// <summary>
        /// Inverts all four channels (ARGB).<br/>
        /// Meaning if Alpha = 255 the color will be fully transparent after inversion.
        /// </summary>
        /// <param name="color">Input color object.</param>
        /// <returns>Resultant color.</returns>
        public static Color InvertARGB(Color color)
        {
            return Color.FromArgb(
                ChannelMaxValue - color.A,
                ChannelMaxValue - color.R,
                ChannelMaxValue - color.G,
                ChannelMaxValue - color.B);

        }

        /// <summary>
        /// Inverts the RGB channels and leaves the Alpha channel untouched.
        /// </summary>
        /// <param name="color">Input color object</param>
        /// <returns>Resultant color.</returns>
        public static Color InvertRGB(Color color)
        {
            return Color.FromArgb(
                color.A,
                ChannelMaxValue - color.R,
                ChannelMaxValue - color.G,
                ChannelMaxValue - color.B);
        }

        /// <summary>
        /// Inverts a single color channel value.
        /// </summary>
        /// <param name="i">Channel value.</param>
        /// <returns>Inverted value.</returns>
        public static int Invert(byte i)
        {
            return ChannelMaxValue - i;
        }
    }
}
