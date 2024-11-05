﻿#region BSD License
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
            var main = new Main();
            main.Show();
        }

        private void kbtnBreadCrumb_Click(object sender, EventArgs e)
        {
            var breadCrumb = new BreadCrumbTest();

            breadCrumb.Show();
        }

        private void kbtnButtons_Click(object sender, EventArgs e)
        {
            var buttons = new ButtonsTest();

            buttons.Show();
        }

        private void kbtnCommandLinkButtons_Click(object sender, EventArgs e)
        {
            var commandLinkButtons = new CommandLinkButtons();

            commandLinkButtons.Show();
        }

        private void kbtnFadeForm_Click(object sender, EventArgs e)
        {
            var fadeForm = new FadeFormTest();

            fadeForm.Show();
        }

        private void kbtnFormBorder_Click(object sender, EventArgs e)
        {
            var formBorder = new FormBorderTest();

            formBorder.Show();
        }

        private void kbtnGroupBox_Click(object sender, EventArgs e)
        {
            var groupBox = new GroupBoxTest();

            groupBox.Show();
        }

        private void kbtnMenuToolStatusStrips_Click(object sender, EventArgs e)
        {
            var menuToolBarStatusStrip = new MenuToolBarStatusStripTest();

            menuToolBarStatusStrip.Show();
        }

        private void kbtnMessageBox_Click(object sender, EventArgs e)
        {
            var messageBox = new MessageBoxTest();

            messageBox.Show();
        }

        private void kbtnProgressBar_Click(object sender, EventArgs e)
        {
            var progressBar = new ProgressBarTest();

            progressBar.Show();
        }

        private void kbtnRibbon_Click(object sender, EventArgs e)
        {
            var ribbon = new RibbonTest();

            ribbon.Show();
        }

        private void kbtnTextBox_Click(object sender, EventArgs e)
        {
            var textBoxEvent = new TextBoxEventTest();

            textBoxEvent.Show();
        }

        private void kbtnTheme_Click(object sender, EventArgs e)
        {
            var theme = new ThemeTest();

            theme.Show();
        }

        private void kbtnToast_Click(object sender, EventArgs e)
        {
            var toastNotification = new ToastNotificationTestChoice();

            toastNotification.Show();
        }

        private void kbtnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }

        private void kbtnTreeView_Click(object sender, EventArgs e)
        {
            var treeViewExample = new TreeViewExample();

            treeViewExample.Show();
        }

        private void kbtnOutlookGrid_Click(object sender, EventArgs e)
        {
            var outlookGridTest = new OutlookGridTest();

            outlookGridTest.Show();
        }

        private void kbtnCalendar_Click(object sender, EventArgs e)
        {
            var calendarTest = new CalendarTest();

            calendarTest.Show();
        }

        private void kbtnWorkspace_Click(object sender, EventArgs e)
        {
            var workspaceTest = new WorkspaceTest();

            workspaceTest.Show();
        }

        private void kbtnThemeControls_Click(object sender, EventArgs e)
        {
            var themeControls = new ThemeControlExamples();

            themeControls.Show();
        }

        private void kbtnControlsTest_Click(object sender, EventArgs e)
        {
            var controlsTest = new ControlsTest();

            controlsTest.Show();
        }

        private void kbtnDataGrid_Click(object sender, EventArgs e)
        {
            var dataGrid = new DataGridViewTest();

            dataGrid.Show();
        }

        private void kbtnHeaderExamples_Click(object sender, EventArgs e)
        {
            var headerExamples = new HeaderExamples();

            headerExamples.Show();
        }

        private void kbtnInputBox_Click(object sender, EventArgs e)
        {
            var inputBox = new InputBoxTest();

            inputBox.Show();
        }

        private void kbtnAbout_Click(object sender, EventArgs e)
        {
            var aboutBox = new AboutBoxTest();

            aboutBox.Show();
        }

        private void btnColourTestimonials_Click(object sender, EventArgs e)
        {
            new ColorTestimonials().Show();
        }

        private void kbtnRibbonNavigatorWorkspace_Click(object sender, EventArgs e)
        {
            new RibbonNavigatorWorkspaceTest().Show();
        }

        private void kbtnPropertyGrid_Click(object sender, EventArgs e)
        {
            new PropertyGridTest().Show();
        }
    }
}
