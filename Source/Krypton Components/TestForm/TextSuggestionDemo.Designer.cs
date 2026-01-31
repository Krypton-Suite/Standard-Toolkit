using Krypton.Utilities;

namespace TestForm
{
    partial class TextSuggestionDemo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            SuspendLayout();

            // Main form
            Text = "Krypton Auto Text Suggest Demo";
            ClientSize = new Size(900, 650);
            StartPosition = FormStartPosition.CenterScreen;

            // Info label
            _labelInfo = new KryptonLabel
            {
                Text = "Type in the text boxes below to see suggestions. Use arrow keys to navigate, Enter to select, Escape to close.",
                Location = new Point(10, 10),
                Size = new Size(880, 30)
            };

            // Examples group box
            _groupBoxExamples = new KryptonGroupBox
            {
                Text = "Examples",
                Location = new Point(10, 50),
                Size = new Size(430, 280)
            };
            _groupBoxExamples.StateCommon.Content.Padding = new Padding(10);

            // Fruits example
            _labelFruits = new KryptonLabel
            {
                Text = "Fruits (StartsWith match):",
                Location = new Point(10, 20),
                Size = new Size(200, 20)
            };

            _textBoxFruits = new KryptonTextBox
            {
                Location = new Point(10, 45),
                Size = new Size(400, 25),
                Text = string.Empty,
                Name = "textBoxFruits"
            };

            _suggestionProvider1 = new KryptonAutoTextSuggestion(this.components)
            {
                AttachedControl = _textBoxFruits,
                MinCharsToShow = 1,
                ShowDelay = 200,
                MaxVisibleItems = 6,
                PopupWidth = 300,
                MatchMode = KryptonAutoTextSuggestMatchMode.StartsWith
            };

            // Populate fruits
            string[] fruits = {
                "Apple", "Apricot", "Avocado",
                "Banana", "Blackberry", "Blueberry",
                "Cherry", "Coconut", "Cranberry",
                "Date",
                "Elderberry",
                "Fig",
                "Grape", "Grapefruit", "Guava",
                "Kiwi",
                "Lemon", "Lime", "Lychee",
                "Mango", "Melon",
                "Orange",
                "Papaya", "Peach", "Pear", "Pineapple", "Plum",
                "Raspberry",
                "Strawberry",
                "Tangerine",
                "Watermelon"
            };

            foreach (string fruit in fruits)
            {
                _suggestionProvider1.Suggestions.Add(new KryptonAutoTextSuggestItem(fruit));
            }

            // Countries example
            _labelCountries = new KryptonLabel
            {
                Text = "Countries (Contains match):",
                Location = new Point(10, 85),
                Size = new Size(200, 20)
            };

            _textBoxCountries = new KryptonTextBox
            {
                Location = new Point(10, 110),
                Size = new Size(400, 25),
                Text = string.Empty,
                Name = "textBoxCountries"
            };

            _suggestionProvider2 = new KryptonAutoTextSuggestion(this.components)
            {
                AttachedControl = _textBoxCountries,
                MinCharsToShow = 2,
                ShowDelay = 300,
                MaxVisibleItems = 8,
                PopupWidth = 350,
                MatchMode = KryptonAutoTextSuggestMatchMode.Contains
            };

            // Populate countries
            string[] countries = {
                "United States", "United Kingdom", "Canada",
                "Australia", "Austria",
                "Brazil", "Belgium", "Bulgaria",
                "China", "Chile", "Colombia",
                "Denmark",
                "Egypt", "Estonia",
                "France", "Finland",
                "Germany", "Greece",
                "India", "Italy", "Ireland", "Iceland", "Indonesia",
                "Japan",
                "Kenya",
                "Mexico", "Malaysia",
                "Netherlands", "Norway", "New Zealand",
                "Poland", "Portugal",
                "Russia",
                "Spain", "Sweden", "Switzerland", "South Africa", "South Korea",
                "Thailand", "Turkey",
                "Ukraine",
                "Vietnam"
            };

            foreach (string country in countries)
            {
                _suggestionProvider2.Suggestions.Add(new KryptonAutoTextSuggestItem(country));
            }

            // Custom example with different display text
            _labelCustom = new KryptonLabel
            {
                Text = "Programming Languages (Fuzzy match):",
                Location = new Point(10, 150),
                Size = new Size(250, 20)
            };

            _textBoxCustom = new KryptonTextBox
            {
                Location = new Point(10, 175),
                Size = new Size(400, 25),
                Text = string.Empty,
                Name = "textBoxCustom"
            };

            _suggestionProvider3 = new KryptonAutoTextSuggestion(this.components)
            {
                AttachedControl = _textBoxCustom,
                MinCharsToShow = 1,
                ShowDelay = 250,
                MaxVisibleItems = 7,
                PopupWidth = 320,
                MatchMode = KryptonAutoTextSuggestMatchMode.Fuzzy
            };

            // Populate programming languages with descriptions
            var languages = new[]
            {
                new KryptonAutoTextSuggestItem("C#", "C# (.NET)", "Microsoft's object-oriented programming language"),
                new KryptonAutoTextSuggestItem("JavaScript", "JavaScript (JS)", "Dynamic scripting language for web"),
                new KryptonAutoTextSuggestItem("Python", "Python", "High-level interpreted programming language"),
                new KryptonAutoTextSuggestItem("Java", "Java", "Object-oriented programming language"),
                new KryptonAutoTextSuggestItem("C++", "C++", "General-purpose programming language"),
                new KryptonAutoTextSuggestItem("TypeScript", "TypeScript (TS)", "Typed superset of JavaScript"),
                new KryptonAutoTextSuggestItem("Go", "Go (Golang)", "Google's compiled programming language"),
                new KryptonAutoTextSuggestItem("Rust", "Rust", "Systems programming language"),
                new KryptonAutoTextSuggestItem("Swift", "Swift", "Apple's programming language"),
                new KryptonAutoTextSuggestItem("Kotlin", "Kotlin", "JVM-based programming language"),
                new KryptonAutoTextSuggestItem("PHP", "PHP", "Server-side scripting language"),
                new KryptonAutoTextSuggestItem("Ruby", "Ruby", "Dynamic object-oriented language"),
                new KryptonAutoTextSuggestItem("R", "R", "Statistical computing language"),
                new KryptonAutoTextSuggestItem("Scala", "Scala", "Functional programming language"),
                new KryptonAutoTextSuggestItem("Dart", "Dart", "Google's programming language")
            };

            foreach (var lang in languages)
            {
                _suggestionProvider3.Suggestions.Add(lang);
            }

            _groupBoxExamples.Panel.Controls.AddRange(new Control[]
            {
                _labelFruits, _textBoxFruits,
                _labelCountries, _textBoxCountries,
                _labelCustom, _textBoxCustom
            });

            // Settings group box
            _groupBoxSettings = new KryptonGroupBox
            {
                Text = "Settings (applies to all examples)",
                Location = new Point(450, 50),
                Size = new Size(440, 280)
            };
            _groupBoxSettings.StateCommon.Content.Padding = new Padding(10);

            _checkBoxEnabled = new KryptonCheckBox
            {
                Text = "Enabled",
                Checked = true,
                Location = new Point(10, 20),
                Size = new Size(100, 20)
            };

            _checkBoxCaseSensitive = new KryptonCheckBox
            {
                Text = "Case Sensitive",
                Checked = false,
                Location = new Point(120, 20),
                Size = new Size(120, 20)
            };

            _labelMatchMode = new KryptonLabel
            {
                Text = "Match Mode:",
                Location = new Point(10, 55),
                Size = new Size(100, 20)
            };

            _comboBoxMatchMode = new KryptonComboBox
            {
                Location = new Point(120, 52),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _comboBoxMatchMode.Items.AddRange(new object[] { "StartsWith", "Contains", "Fuzzy" });
            _comboBoxMatchMode.SelectedIndex = 0;

            _labelMinChars = new KryptonLabel
            {
                Text = "Min Characters:",
                Location = new Point(10, 90),
                Size = new Size(100, 20)
            };

            _numericUpDownMinChars = new KryptonNumericUpDown
            {
                Location = new Point(120, 87),
                Size = new Size(100, 25),
                Minimum = 0,
                Maximum = 10,
                Value = 1
            };

            _labelDelay = new KryptonLabel
            {
                Text = "Show Delay (ms):",
                Location = new Point(10, 125),
                Size = new Size(100, 20)
            };

            _numericUpDownDelay = new KryptonNumericUpDown
            {
                Location = new Point(120, 122),
                Size = new Size(100, 25),
                Minimum = 0,
                Maximum = 2000,
                Value = 300,
                Increment = 50
            };

            _labelMaxItems = new KryptonLabel
            {
                Text = "Max Visible Items:",
                Location = new Point(10, 160),
                Size = new Size(100, 20)
            };

            _numericUpDownMaxItems = new KryptonNumericUpDown
            {
                Location = new Point(120, 157),
                Size = new Size(100, 25),
                Minimum = 1,
                Maximum = 20,
                Value = 8
            };

            _labelWidth = new KryptonLabel
            {
                Text = "Popup Width:",
                Location = new Point(10, 195),
                Size = new Size(100, 20)
            };

            _numericUpDownWidth = new KryptonNumericUpDown
            {
                Location = new Point(120, 192),
                Size = new Size(100, 25),
                Minimum = 100,
                Maximum = 800,
                Value = 250,
                Increment = 10
            };

            _groupBoxSettings.Panel.Controls.AddRange(new Control[]
            {
                _checkBoxEnabled, _checkBoxCaseSensitive,
                _labelMatchMode, _comboBoxMatchMode,
                _labelMinChars, _numericUpDownMinChars,
                _labelDelay, _numericUpDownDelay,
                _labelMaxItems, _numericUpDownMaxItems,
                _labelWidth, _numericUpDownWidth
            });

            // Log group box
            _groupBoxLog = new KryptonGroupBox
            {
                Text = "Event Log",
                Location = new Point(10, 340),
                Size = new Size(880, 300)
            };
            _groupBoxLog.StateCommon.Content.Padding = new Padding(10);

            _listBoxLog = new KryptonListBox
            {
                Location = new Point(10, 20),
                Size = new Size(860, 240)
            };

            _buttonClearLog = new KryptonButton
            {
                Text = "Clear Log",
                Location = new Point(10, 270),
                Size = new Size(100, 25)
            };

            _groupBoxLog.Panel.Controls.AddRange(new Control[]
            {
                _listBoxLog, _buttonClearLog
            });

            // Add all controls to form
            Controls.AddRange(new Control[]
            {
                _labelInfo,
                _groupBoxExamples,
                _groupBoxSettings,
                _groupBoxLog
            });

            ResumeLayout(false);
        }

        #endregion

        private KryptonAutoTextSuggestion _suggestionProvider1;
        private KryptonAutoTextSuggestion _suggestionProvider2;
        private KryptonAutoTextSuggestion _suggestionProvider3;
        private KryptonTextBox _textBoxFruits;
        private KryptonTextBox _textBoxCountries;
        private KryptonTextBox _textBoxCustom;
        private KryptonLabel _labelFruits;
        private KryptonLabel _labelCountries;
        private KryptonLabel _labelCustom;
        private KryptonLabel _labelInfo;
        private KryptonGroupBox _groupBoxExamples;
        private KryptonGroupBox _groupBoxSettings;
        private KryptonCheckBox _checkBoxEnabled;
        private KryptonCheckBox _checkBoxCaseSensitive;
        private KryptonComboBox _comboBoxMatchMode;
        private KryptonNumericUpDown _numericUpDownMinChars;
        private KryptonNumericUpDown _numericUpDownDelay;
        private KryptonNumericUpDown _numericUpDownMaxItems;
        private KryptonNumericUpDown _numericUpDownWidth;
        private KryptonLabel _labelMinChars;
        private KryptonLabel _labelDelay;
        private KryptonLabel _labelMaxItems;
        private KryptonLabel _labelWidth;
        private KryptonLabel _labelMatchMode;
        private KryptonListBox _listBoxLog;
        private KryptonButton _buttonClearLog;
        private KryptonGroupBox _groupBoxLog;
    }
}