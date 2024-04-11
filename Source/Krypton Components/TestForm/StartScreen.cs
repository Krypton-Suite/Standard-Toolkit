#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2024. All rights reserved. 
 *  
 */
#endregion

namespace TestForm
{
    public partial class StartScreen : KryptonForm
    {
        public StartScreen()
        {
            InitializeComponent();
        }

        private void StartScreen_Load(object sender, EventArgs e)
        {

        }

        private void kbtnAboutBox_Click(object sender, EventArgs e)
        {

        }

        private void kbtnBreadCrumb_Click(object sender, EventArgs e)
        {
            BreadCrumbTest breadCrumb = new BreadCrumbTest();

            breadCrumb.Show();
        }

        private void kbtnButtons_Click(object sender, EventArgs e)
        {
            ButtonsTest buttons = new ButtonsTest();

            buttons.Show();
        }

        private void kbtnCommandLinkButtons_Click(object sender, EventArgs e)
        {
            CommandLinkButtons commandLinkButtons = new CommandLinkButtons();

            commandLinkButtons.Show();
        }

        private void kbtnFadeForm_Click(object sender, EventArgs e)
        {
            FadeFormTest fadeForm = new FadeFormTest();

            fadeForm.Show();
        }

        private void kbtnFormBorder_Click(object sender, EventArgs e)
        {
            FormBorderTest formBorder = new FormBorderTest();

            formBorder.Show();
        }

        private void kbtnGroupBox_Click(object sender, EventArgs e)
        {
            GroupBoxTest groupBox = new GroupBoxTest();

            groupBox.Show();
        }

        private void kbtnMenuToolStatusStrips_Click(object sender, EventArgs e)
        {
            MenuToolBarStatusStripTest menuToolBarStatusStrip = new MenuToolBarStatusStripTest();

            menuToolBarStatusStrip.Show();
        }

        private void kbtnMessageBox_Click(object sender, EventArgs e)
        {
            MessageBoxTest messageBox = new MessageBoxTest();

            messageBox.Show();
        }

        private void kbtnProgressBar_Click(object sender, EventArgs e)
        {
            ProgressBarTest progressBar = new ProgressBarTest();

            progressBar.Show();
        }

        private void kbtnRibbon_Click(object sender, EventArgs e)
        {
            RibbonTest ribbon = new RibbonTest();

            ribbon.Show();
        }

        private void kbtnTextBox_Click(object sender, EventArgs e)
        {
            TextBoxEventTest textBoxEvent = new TextBoxEventTest();

            textBoxEvent.Show();
        }

        private void kbtnTheme_Click(object sender, EventArgs e)
        {
            ThemeTest theme = new ThemeTest();

            theme.Show();
        }

        private void kbtnToast_Click(object sender, EventArgs e)
        {
            ToastNotificationTestChoice toastNotification = new ToastNotificationTestChoice();

            toastNotification.Show();
        }

        private void kbtnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }

        private void kbtnTreeView_Click(object sender, EventArgs e)
        {
            TreeViewExample treeViewExample = new TreeViewExample();

            treeViewExample.Show();
        }

        private void kbtnOutlookGrid_Click(object sender, EventArgs e)
        {
            OutlookGridTest outlookGridTest = new OutlookGridTest();

            outlookGridTest.Show();
        }

        private void kbtnCalendar_Click(object sender, EventArgs e)
        {
            CalendarTest calendarTest = new CalendarTest();

            calendarTest.Show();
        }
    }
}
