using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Krypton.Toolkit;

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
    }
}
