#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

using System.IO;

namespace TestForm;

public partial class CodeEditorTest : KryptonForm
{
    public CodeEditorTest()
    {
        InitializeComponent();
        InitializeEditor();
    }

    private void InitializeEditor()
    {
        // Set up sample C# code
        var sampleCode = @"using System;
using System.Collections.Generic;

namespace Example
{
    public class Program
    {
        private int _value = 42;
        
        public static void Main(string[] args)
        {
            Console.WriteLine(""Hello World"");
            
            var list = new List<int> { 1, 2, 3, 4, 5 };
            
            foreach (var item in list)
            {
                if (item % 2 == 0)
                {
                    Console.WriteLine($""Even: {item}"");
                }
                else
                {
                    Console.WriteLine($""Odd: {item}"");
                }
            }
            
            // This is a comment
            /* Multi-line
               comment */
        }
        
        public int Calculate(int x, int y)
        {
            return x + y;
        }
    }
}";

        kceEditor.Text = sampleCode;
        kceEditor.Language = Krypton.Utilities.Language.CSharp;
        kceEditor.ShowLineNumbers = true;
        kceEditor.EnableCodeFolding = true;
        kceEditor.AutoCompleteEnabled = true;
        kceEditor.Theme = Krypton.Utilities.EditorThemeType.Light;

        // Initialize theme combo
        kcmbTheme.Items.Clear();
        kcmbTheme.Items.Add("Light");
        kcmbTheme.Items.Add("Dark");
        kcmbTheme.Items.Add("High Contrast");
        kcmbTheme.Items.Add("Monokai");
        kcmbTheme.Items.Add("Solarized Light");
        kcmbTheme.Items.Add("Solarized Dark");
        kcmbTheme.SelectedIndex = 0;

        // Initialize language combo
        kcmbLanguage.Items.Clear();
        kcmbLanguage.Items.Add("None");
        kcmbLanguage.Items.Add("C#");
        kcmbLanguage.Items.Add("C++");
        kcmbLanguage.Items.Add("VB.NET");
        kcmbLanguage.Items.Add("XML");
        kcmbLanguage.Items.Add("HTML");
        kcmbLanguage.Items.Add("CSS");
        kcmbLanguage.Items.Add("JavaScript");
        kcmbLanguage.Items.Add("TypeScript");
        kcmbLanguage.Items.Add("Python");
        kcmbLanguage.Items.Add("Rust");
        kcmbLanguage.Items.Add("Go");
        kcmbLanguage.Items.Add("Java");
        kcmbLanguage.Items.Add("PHP");
        kcmbLanguage.Items.Add("Ruby");
        kcmbLanguage.Items.Add("Swift");
        kcmbLanguage.Items.Add("Kotlin");
        kcmbLanguage.Items.Add("SQL");
        kcmbLanguage.Items.Add("JSON");
        kcmbLanguage.Items.Add("YAML");
        kcmbLanguage.Items.Add("TOML");
        kcmbLanguage.Items.Add("Markdown");
        kcmbLanguage.Items.Add("Batch");
        kcmbLanguage.Items.Add("PowerShell");
        kcmbLanguage.SelectedIndex = 1; // C#

        // Initialize sample code for different languages
        InitializeSampleCode();
    }

    private void InitializeSampleCode()
    {
        var samples = new Dictionary<string, string>
        {
            ["C#"] = @"using System;

namespace Example
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine(""Hello World"");
        }
    }
}",
            ["C++"] = @"#include <iostream>

using namespace std;

int main()
{
    cout << ""Hello World"" << endl;
    return 0;
}",
            ["VB.NET"] = @"Imports System

Namespace Example
    Public Class Program
        Public Shared Sub Main()
            Console.WriteLine(""Hello World"")
        End Sub
    End Class
End Namespace",
            ["XML"] = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<root>
    <element attribute=""value"">
        <child>Content</child>
    </element>
</root>",
            ["HTML"] = @"<!DOCTYPE html>
<html>
<head>
    <title>Example</title>
</head>
<body>
    <h1>Hello World</h1>
    <p>This is a paragraph.</p>
</body>
</html>",
            ["CSS"] = @"body {
    font-family: Arial, sans-serif;
    color: #333;
}

.header {
    background-color: #f0f0f0;
    padding: 10px;
}",
            ["JavaScript"] = @"function greet(name) {
    console.log('Hello, ' + name);
}

const numbers = [1, 2, 3, 4, 5];
numbers.forEach(n => {
    if (n % 2 === 0) {
        console.log(n + ' is even');
    }
});",
            ["Python"] = @"def greet(name):
    print(f'Hello, {name}')

numbers = [1, 2, 3, 4, 5]
for n in numbers:
    if n % 2 == 0:
        print(f'{n} is even')
    else:
        print(f'{n} is odd')",
            ["SQL"] = @"SELECT 
    u.Id,
    u.Name,
    u.Email
FROM Users u
WHERE u.Active = 1
    AND u.CreatedDate > '2024-01-01'
ORDER BY u.Name;",
            ["JSON"] = @"{
    ""name"": ""Example"",
    ""version"": ""1.0.0"",
    ""dependencies"": {
        ""package1"": ""^1.0.0"",
        ""package2"": ""^2.0.0""
    },
    ""scripts"": {
        ""start"": ""node index.js""
    }
}",
            ["Markdown"] = @"# Markdown Example

This is **bold** and *italic* text.

## Code Block

```csharp
public void Example()
{
    // Code here
}
```

- Item 1
- Item 2
- Item 3",
            ["TypeScript"] = @"interface Person {
    name: string;
    age: number;
}

class Employee implements Person {
    constructor(public name: string, public age: number) {}
    
    greet(): void {
        console.log(`Hello, I'm ${this.name}`);
    }
}

const emp = new Employee('John', 30);
emp.greet();",
            ["Rust"] = @"fn main() {
    let mut vec = Vec::new();
    vec.push(1);
    vec.push(2);
    
    for item in &vec {
        println!(""{}"", item);
    }
}

struct Point {
    x: i32,
    y: i32,
}",
            ["Go"] = @"package main

import ""fmt""

type Person struct {
    Name string
    Age  int
}

func main() {
    p := Person{Name: ""John"", Age: 30}
    fmt.Printf(""Name: %s, Age: %d\n"", p.Name, p.Age)
}",
            ["Java"] = @"public class HelloWorld {
    private String message;
    
    public HelloWorld(String msg) {
        this.message = msg;
    }
    
    public void print() {
        System.out.println(message);
    }
    
    public static void main(String[] args) {
        HelloWorld hw = new HelloWorld(""Hello, World!"");
        hw.print();
    }
}",
            ["PHP"] = @"<?php
class Person {
    private $name;
    private $age;
    
    public function __construct($name, $age) {
        $this->name = $name;
        $this->age = $age;
    }
    
    public function greet() {
        echo ""Hello, I'm "" . $this->name;
    }
}

$person = new Person('John', 30);
$person->greet();
?>",
            ["Ruby"] = @"class Person
    attr_accessor :name, :age
    
    def initialize(name, age)
        @name = name
        @age = age
    end
    
    def greet
        puts ""Hello, I'm #{@name}""
    end
end

person = Person.new('John', 30)
person.greet",
            ["Swift"] = @"struct Person {
    var name: String
    var age: Int
    
    func greet() {
        print(""Hello, I'm \\(name)"")
    }
}

let person = Person(name: ""John"", age: 30)
person.greet()",
            ["Kotlin"] = @"data class Person(val name: String, val age: Int) {
    fun greet() {
        println(""Hello, I'm $name"")
    }
}

fun main() {
    val person = Person(""John"", 30)
    person.greet()
}",
            ["YAML"] = @"name: Example Application
version: 1.0.0
description: A sample YAML file

database:
  host: localhost
  port: 5432
  name: mydb

features:
  - authentication
  - authorization
  - logging",
            ["TOML"] = @"[project]
name = ""example""
version = ""1.0.0""
description = ""A sample TOML file""

[database]
host = ""localhost""
port = 5432
name = ""mydb""

[features]
enabled = true",
            ["Batch"] = @"@echo off
setlocal

set NAME=John
set AGE=30

echo Name: %NAME%
echo Age: %AGE%

if %AGE% GEQ 18 (
    echo Person is an adult
) else (
    echo Person is a minor
)

pause",
            ["PowerShell"] = @"function Get-PersonInfo {
    param(
        [string]$Name,
        [int]$Age
    )
    
    Write-Host ""Name: $Name""
    Write-Host ""Age: $Age""
    
    if ($Age -ge 18) {
        Write-Host ""Person is an adult""
    } else {
        Write-Host ""Person is a minor""
    }
}

Get-PersonInfo -Name ""John"" -Age 30"
        };

        // Store samples for later use
        _sampleCode = samples;
    }

    private Dictionary<string, string> _sampleCode = new Dictionary<string, string>();

    private void kcmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selected = kcmbLanguage.SelectedItem?.ToString();
        if (selected != null && selected.Length > 0)
        {
            kceEditor.Language = selected switch
            {
                "C#" => Krypton.Utilities.Language.CSharp,
                "C++" => Krypton.Utilities.Language.Cpp,
                "VB.NET" => Krypton.Utilities.Language.VbNet,
                "XML" => Krypton.Utilities.Language.Xml,
                "HTML" => Krypton.Utilities.Language.Html,
                "CSS" => Krypton.Utilities.Language.Css,
                "JavaScript" => Krypton.Utilities.Language.JavaScript,
                "TypeScript" => Krypton.Utilities.Language.TypeScript,
                "Python" => Krypton.Utilities.Language.Python,
                "Rust" => Krypton.Utilities.Language.Rust,
                "Go" => Krypton.Utilities.Language.Go,
                "Java" => Krypton.Utilities.Language.Java,
                "PHP" => Krypton.Utilities.Language.Php,
                "Ruby" => Krypton.Utilities.Language.Ruby,
                "Swift" => Krypton.Utilities.Language.Swift,
                "Kotlin" => Krypton.Utilities.Language.Kotlin,
                "SQL" => Krypton.Utilities.Language.Sql,
                "JSON" => Krypton.Utilities.Language.Json,
                "YAML" => Krypton.Utilities.Language.Yaml,
                "TOML" => Krypton.Utilities.Language.Toml,
                "Markdown" => Krypton.Utilities.Language.Markdown,
                "Batch" => Krypton.Utilities.Language.Batch,
                "PowerShell" => Krypton.Utilities.Language.PowerShell,
                _ => Krypton.Utilities.Language.None
            };

            // Load sample code if available
            if (_sampleCode.TryGetValue(selected, out var value))
            {
                kceEditor.Text = value;
            }
        }
    }

    private void kchkShowLineNumbers_CheckedChanged(object sender, EventArgs e)
    {
        kceEditor.ShowLineNumbers = kchkShowLineNumbers.Checked;
    }

    private void kchkCodeFolding_CheckedChanged(object sender, EventArgs e)
    {
        kceEditor.EnableCodeFolding = kchkCodeFolding.Checked;
    }

    private void kchkAutoComplete_CheckedChanged(object sender, EventArgs e)
    {
        kceEditor.AutoCompleteEnabled = kchkAutoComplete.Checked;
    }

    private void kcmbTheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selected = kcmbTheme.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(selected))
            return;

        kceEditor.Theme = selected switch
        {
            "Light" => Krypton.Utilities.EditorThemeType.Light,
            "Dark" => Krypton.Utilities.EditorThemeType.Dark,
            "High Contrast" => Krypton.Utilities.EditorThemeType.HighContrast,
            "Monokai" => Krypton.Utilities.EditorThemeType.Monokai,
            "Solarized Light" => Krypton.Utilities.EditorThemeType.SolarizedLight,
            "Solarized Dark" => Krypton.Utilities.EditorThemeType.SolarizedDark,
            _ => Krypton.Utilities.EditorThemeType.Light
        };
    }

    private void kbtnLoadFile_Click(object sender, EventArgs e)
    {
        using var dialog = new KryptonOpenFileDialog
        {
            Filter = "Code Files (*.cs;*.cpp;*.vb;*.xml;*.html;*.css;*.js;*.py;*.sql;*.json;*.md)|*.cs;*.cpp;*.vb;*.xml;*.html;*.css;*.js;*.py;*.sql;*.json;*.md|All Files (*.*)|*.*",
            Title = "Open Code File"
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                kceEditor.Text = File.ReadAllText(dialog.FileName);

                // Auto-detect language from extension
                var ext = Path.GetExtension(dialog.FileName).ToLower();
                kceEditor.Language = ext switch
                {
                    ".cs" => Krypton.Utilities.Language.CSharp,
                    ".cpp" or ".cxx" or ".cc" or ".h" or ".hpp" => Krypton.Utilities.Language.Cpp,
                    ".vb" => Krypton.Utilities.Language.VbNet,
                    ".xml" => Krypton.Utilities.Language.Xml,
                    ".html" or ".htm" => Krypton.Utilities.Language.Html,
                    ".css" => Krypton.Utilities.Language.Css,
                    ".js" => Krypton.Utilities.Language.JavaScript,
                    ".ts" => Krypton.Utilities.Language.TypeScript,
                    ".py" => Krypton.Utilities.Language.Python,
                    ".rs" => Krypton.Utilities.Language.Rust,
                    ".go" => Krypton.Utilities.Language.Go,
                    ".java" => Krypton.Utilities.Language.Java,
                    ".php" => Krypton.Utilities.Language.Php,
                    ".rb" => Krypton.Utilities.Language.Ruby,
                    ".swift" => Krypton.Utilities.Language.Swift,
                    ".kt" or ".kts" => Krypton.Utilities.Language.Kotlin,
                    ".sql" => Krypton.Utilities.Language.Sql,
                    ".json" => Krypton.Utilities.Language.Json,
                    ".yaml" or ".yml" => Krypton.Utilities.Language.Yaml,
                    ".toml" => Krypton.Utilities.Language.Toml,
                    ".md" => Krypton.Utilities.Language.Markdown,
                    ".bat" or ".cmd" => Krypton.Utilities.Language.Batch,
                    ".ps1" => Krypton.Utilities.Language.PowerShell,
                    _ => Krypton.Utilities.Language.None
                };

                // Update combo box
                var langIndex = kcmbLanguage.Items.IndexOf(ext switch
                {
                    ".cs" => "C#",
                    ".cpp" or ".cxx" or ".cc" or ".h" or ".hpp" => "C++",
                    ".vb" => "VB.NET",
                    ".xml" => "XML",
                    ".html" or ".htm" => "HTML",
                    ".css" => "CSS",
                    ".js" => "JavaScript",
                    ".ts" => "TypeScript",
                    ".py" => "Python",
                    ".rs" => "Rust",
                    ".go" => "Go",
                    ".java" => "Java",
                    ".php" => "PHP",
                    ".rb" => "Ruby",
                    ".swift" => "Swift",
                    ".kt" or ".kts" => "Kotlin",
                    ".sql" => "SQL",
                    ".json" => "JSON",
                    ".yaml" or ".yml" => "YAML",
                    ".toml" => "TOML",
                    ".md" => "Markdown",
                    ".bat" or ".cmd" => "Batch",
                    ".ps1" => "PowerShell",
                    _ => "None"
                });

                if (langIndex >= 0)
                {
                    kcmbLanguage.SelectedIndex = langIndex;
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(this,
                    $"Error loading file:\n{ex.Message}",
                    "Error",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Error);
            }
        }
    }

    private void kbtnSaveFile_Click(object sender, EventArgs e)
    {
        using var dialog = new KryptonSaveFileDialog
        {
            Filter = "Code Files (*.cs)|*.cs|All Files (*.*)|*.*",
            Title = "Save Code File"
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                File.WriteAllText(dialog.FileName, kceEditor.Text);
                KryptonMessageBox.Show(this,
                    "File saved successfully.",
                    "Success",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(this,
                    $"Error saving file:\n{ex.Message}",
                    "Error",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Error);
            }
        }
    }

    private void kbtnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}
