#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    public class InformationBoxAutoCloseParameters
    {
        #region Public

        /// <summary>
        /// Gets the default InformationBoxAutoCloseParameters.
        /// </summary>
        /// <value>The default InformationBoxAutoCloseParameters.</value>
        public static InformationBoxAutoCloseParameters Default { get; } = new InformationBoxAutoCloseParameters(30);

        /// <summary>
        /// Gets the seconds.
        /// </summary>
        /// <value>The seconds.</value>
        public int Seconds { get; private set; } = 30;

        /// <summary>
        /// Gets the default button.
        /// </summary>
        /// <value>The default button.</value>
        public InformationBoxDefaultButton DefaultButton { get; private set; } = InformationBoxDefaultButton.Button1;

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>The result.</value>
        public InformationBoxResult Result { get; private set; } = InformationBoxResult.None;

        /// <summary>
        /// Gets the mode.
        /// </summary>
        /// <value>The autoclose mode.</value>
        public AutoCloseDefinedParameters Mode { get; private set; }

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="InformationBoxAutoCloseParameters" /> class.</summary>
        /// <param name="timeToWait">The time to wait.</param>
        public InformationBoxAutoCloseParameters(int timeToWait)
        {
            Mode = AutoCloseDefinedParameters.TimeOnly;

            Seconds = timeToWait;
        }

        /// <summary>Initializes a new instance of the <see cref="InformationBoxAutoCloseParameters" /> class.</summary>
        /// <param name="defaultButton">The default button.</param>
        public InformationBoxAutoCloseParameters(InformationBoxDefaultButton defaultButton)
        {
            Mode = AutoCloseDefinedParameters.Button;

            DefaultButton = defaultButton;
        }

        /// <summary>Initializes a new instance of the <see cref="InformationBoxAutoCloseParameters" /> class.</summary>
        /// <param name="timeToWait">The time to wait.</param>
        /// <param name="defaultButton">The default button.</param>
        public InformationBoxAutoCloseParameters(int timeToWait, InformationBoxDefaultButton defaultButton)
        {
            Mode = AutoCloseDefinedParameters.Button;

            Seconds = timeToWait;

            DefaultButton = defaultButton;
        }

        /// <summary>Initializes a new instance of the <see cref="InformationBoxAutoCloseParameters" /> class.</summary>
        /// <param name="result">The result.</param>
        public InformationBoxAutoCloseParameters(InformationBoxResult result)
        {
            Mode = AutoCloseDefinedParameters.Result;

            Result = result;
        }

        /// <summary>Initializes a new instance of the <see cref="InformationBoxAutoCloseParameters" /> class.</summary>
        /// <param name="timeToWait">The time to wait.</param>
        /// <param name="result">The result.</param>
        public InformationBoxAutoCloseParameters(int timeToWait, InformationBoxResult result)
        {
            Mode = AutoCloseDefinedParameters.Result;

            Seconds = timeToWait;

            Result = result;
        }

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            InformationBoxAutoCloseParameters parameters = (InformationBoxAutoCloseParameters)obj;

            return DefaultButton.Equals(parameters.DefaultButton) &&
                   Mode.Equals(parameters.Mode) &&
                   Result.Equals(parameters.Result) &&
                   Seconds.Equals(parameters.Seconds);
        }

        /// <inheritdoc />
        public override int GetHashCode() => DefaultButton.GetHashCode() ^ Mode.GetHashCode() ^ Result.GetHashCode() ^
                                             Seconds.GetHashCode();

        #endregion
    }
}