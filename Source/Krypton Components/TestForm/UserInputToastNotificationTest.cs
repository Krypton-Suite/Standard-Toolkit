#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved. 
 *  
 */
#endregion

using System.Collections;

namespace TestForm
{
    public partial class UserInputToastNotificationTest : KryptonForm
    {
        #region Instance Fields

        private bool _useProgressBar;

        private bool _useRTLReading;

        private bool _boolResult;

        private CheckState _checkStateResult;

        private DateTime _dateTimeResult;

        private string _stringResult;

        private int _integerResult;

        private KryptonToastNotificationInputAreaType _toastNotificationInputAreaType;

        private KryptonToastNotificationIcon _toastNotificationIcon;

        #endregion

        public UserInputToastNotificationTest()
        {
            InitializeComponent();

            foreach (var value in Enum.GetValues(typeof(KryptonToastNotificationInputAreaType)))
            {
                kcmbUserInputType.Items.Add(value.ToString());
            }

            kcmbUserInputType.SelectedIndex = 0;

            foreach (var value in Enum.GetValues(typeof(KryptonToastNotificationIcon)))
            {
                kcmbToastIcon.Items.Add(value.ToString());
            }

            kcmbToastIcon.SelectedIndex = 0;
        }

        private void kbtnShow_Click(object sender, EventArgs e)
        {
            KryptonUserInputToastNotificationData data = new KryptonUserInputToastNotificationData()
            {
                BorderColor1 = kcbtnBorderColor1.SelectedColor,
                BorderColor2 = kcbtnBorderColor2.SelectedColor,
                CountDownSeconds = (int)knudCountdownSeconds.Value,
                CountDownTimerInterval = 1000,
                CustomImage = null,
                DoNotShowAgainOptionCheckState = GetDoNotShowAgainOptionCheckState(),
                ShowDoNotShowAgainOption = kchkShowDoNotShowAgain.Checked,
                DoNotShowAgainOptionChecked = false,
                FocusOnUserInputArea = true,
                NotificationContent = ktxtToastContent.Text,
                NotificationTitle = ktxtToastTitle.Text,
                NotificationIcon = GetNotificationIcon(),
                NotificationContentFont = null,
                //UserInputItemCollection = new ComboBox.ObjectCollection()
                UserInputList = TemporaryArrayList()
            };

            if (_useProgressBar)
            {
                if (_useRTLReading)
                {
                    switch (GetInputAreaType())
                    {
                        case KryptonToastNotificationInputAreaType.None:
                            break;
                        case KryptonToastNotificationInputAreaType.ComboBox:
                            _stringResult =
                                KryptonToastNotification.ShowRtlUserInputNotificationWithProgressBarAndStringReturnValue(data);

                            KryptonMessageBox.Show($"Result = {_stringResult}");
                            break;
                        case KryptonToastNotificationInputAreaType.DomainUpDown:
                            _stringResult =
                                KryptonToastNotification.ShowRtlUserInputNotificationWithProgressBarAndStringReturnValue(data);

                            KryptonMessageBox.Show($"Result = {_stringResult}");
                            break;
                        case KryptonToastNotificationInputAreaType.NumericDropDown:
                            _integerResult =
                                KryptonToastNotification.ShowRtlUserInputNotificationWithProgressBarAndIntegerReturnValue(data);

                            KryptonMessageBox.Show($"Result = {_integerResult}");
                            break;
                        case KryptonToastNotificationInputAreaType.MaskedTextBox:
                            _stringResult =
                                KryptonToastNotification.ShowRtlUserInputNotificationWithProgressBarAndStringReturnValue(data);

                            KryptonMessageBox.Show($"Result = {_stringResult}");
                            break;
                        case KryptonToastNotificationInputAreaType.TextBox:
                            _stringResult =
                                KryptonToastNotification.ShowRtlUserInputNotificationWithProgressBarAndStringReturnValue(data);

                            KryptonMessageBox.Show($"Result = {_stringResult}");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    switch (GetInputAreaType())
                    {
                        case KryptonToastNotificationInputAreaType.None:
                            break;
                        case KryptonToastNotificationInputAreaType.ComboBox:
                            _stringResult = KryptonToastNotification
                                .ShowUserInputNotificationWithProgressBarAndStringReturnValue(data);

                            KryptonMessageBox.Show($"Result = {_stringResult}");
                            break;
                        case KryptonToastNotificationInputAreaType.DomainUpDown:
                            _stringResult = KryptonToastNotification
                                .ShowUserInputNotificationWithProgressBarAndStringReturnValue(data);

                            KryptonMessageBox.Show($"Result = {_stringResult}");
                            break;
                        case KryptonToastNotificationInputAreaType.NumericDropDown:
                            _integerResult = KryptonToastNotification
                                .ShowUserInputNotificationWithProgressBarAndIntegerReturnValue(data);

                            KryptonMessageBox.Show($"Result = {_integerResult}");
                            break;
                        case KryptonToastNotificationInputAreaType.MaskedTextBox:
                            _stringResult = KryptonToastNotification
                                .ShowUserInputNotificationWithProgressBarAndStringReturnValue(data);

                            KryptonMessageBox.Show($"Result = {_stringResult}");
                            break;
                        case KryptonToastNotificationInputAreaType.TextBox:
                            _stringResult = KryptonToastNotification
                                .ShowUserInputNotificationWithProgressBarAndStringReturnValue(data);

                            KryptonMessageBox.Show($"Result = {_stringResult}");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            else
            {
                if (_useRTLReading)
                {
                    switch (GetInputAreaType())
                    {
                        case KryptonToastNotificationInputAreaType.None:
                            break;
                        case KryptonToastNotificationInputAreaType.ComboBox:
                            _stringResult =
                                KryptonToastNotification.ShowRtlUserInputNotificationWithStringReturnValue(data);

                            KryptonMessageBox.Show($"Result = {_stringResult}");
                            break;
                        case KryptonToastNotificationInputAreaType.DomainUpDown:
                            _stringResult =
                                KryptonToastNotification.ShowRtlUserInputNotificationWithStringReturnValue(data);

                            KryptonMessageBox.Show($"Result = {_stringResult}");
                            break;
                        case KryptonToastNotificationInputAreaType.NumericDropDown:
                            _integerResult =
                                KryptonToastNotification.ShowRtlUserInputNotificationWithIntegerReturnValue(data);

                            KryptonMessageBox.Show($"Result = {_integerResult}");
                            break;
                        case KryptonToastNotificationInputAreaType.MaskedTextBox:
                            _stringResult =
                                KryptonToastNotification.ShowRtlUserInputNotificationWithStringReturnValue(data);

                            KryptonMessageBox.Show($"Result = {_stringResult}");
                            break;
                        case KryptonToastNotificationInputAreaType.TextBox:
                            _stringResult =
                                KryptonToastNotification.ShowRtlUserInputNotificationWithStringReturnValue(data);

                            KryptonMessageBox.Show($"Result = {_stringResult}");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    switch (GetInputAreaType())
                    {
                        case KryptonToastNotificationInputAreaType.None:
                            break;
                        case KryptonToastNotificationInputAreaType.ComboBox:
                            _stringResult =
                                KryptonToastNotification.ShowUserInputNotificationWithStringReturnValue(data);

                            KryptonMessageBox.Show($"Result = {_stringResult}");
                            break;
                        case KryptonToastNotificationInputAreaType.DomainUpDown:
                            _stringResult =
                                KryptonToastNotification.ShowUserInputNotificationWithStringReturnValue(data);

                            KryptonMessageBox.Show($"Result = {_stringResult}");
                            break;
                        case KryptonToastNotificationInputAreaType.NumericDropDown:
                            _integerResult =
                                KryptonToastNotification.ShowUserInputNotificationWithIntegerReturnValue(data);

                            KryptonMessageBox.Show($"Result = {_integerResult}");
                            break;
                        case KryptonToastNotificationInputAreaType.MaskedTextBox:
                            _stringResult =
                                KryptonToastNotification.ShowUserInputNotificationWithStringReturnValue(data);

                            KryptonMessageBox.Show($"Result = {_stringResult}");
                            break;
                        case KryptonToastNotificationInputAreaType.TextBox:
                            _stringResult =
                                KryptonToastNotification.ShowUserInputNotificationWithStringReturnValue(data);

                            KryptonMessageBox.Show($"Result = {_stringResult}");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        private void SetNotificationIcon(KryptonToastNotificationIcon icon) => _toastNotificationIcon = icon;

        private KryptonToastNotificationIcon? GetNotificationIcon() => _toastNotificationIcon;

        private CheckState GetDoNotShowAgainOptionCheckState() => kchkShowDoNotShowAgain.CheckState;

        private void kchkShowProgressBar_CheckedChanged(object sender, EventArgs e) => _useProgressBar = kchkShowProgressBar.Checked;

        private void kchkUseRTL_CheckedChanged(object sender, EventArgs e) => _useRTLReading = kchkUseRTL.Checked;

        private void SetInputType(KryptonToastNotificationInputAreaType inputAreaType) =>
            _toastNotificationInputAreaType = inputAreaType;

        private KryptonToastNotificationInputAreaType GetInputAreaType() => _toastNotificationInputAreaType;

        private void kcmbToastIcon_SelectedIndexChanged(object sender, EventArgs e) => SetNotificationIcon((KryptonToastNotificationIcon)Enum.Parse(typeof(KryptonToastNotificationIcon), kcmbToastIcon.Text));

        private void kcmbUserInputType_SelectedIndexChanged(object sender, EventArgs e) => SetInputType((KryptonToastNotificationInputAreaType)Enum.Parse(typeof(KryptonToastNotificationInputAreaType), kcmbUserInputType.Text));

        private ArrayList TemporaryArrayList()
        {
            ArrayList tempList;

            tempList = new ArrayList();

            for (int i = 0; i < 10; i++)
            {
                tempList.Add(i);
            }

            return tempList;
        }
    }
}
