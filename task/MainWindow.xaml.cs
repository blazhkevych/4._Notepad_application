using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;

namespace task;
// Приложение "Блокнот"
// Разработать приложение «Блокнот», обладающее той же функциональностью,
// что и стандартный «Блокнот» операционной системы Windows.

// Notepad application
// Develop a notepad application that has the same functionality
// as the standard Notepad Windows operating system.

// todo: maybe you need to increase the line with the text editor
// todo: when text is selected, change the indicators on the panel in accordance with the selected
// todo: it is possible to change the indicators in the panel according to the location of the cursor when nothing is selected.

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        // Window position in the center of the screen.
        WindowStartupLocation = WindowStartupLocation.CenterScreen;

        cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
        cmbFontSize.ItemsSource = new List<double> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
    }

    // Name of the current file.
    private string NameOfTheCurrentFile { get; } = "Untitled";

    private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        // todo: Add a question before opening: "Do you want to save changes to the file WITHOUT A NAME?"

        var dlg = new OpenFileDialog();
        dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
        if (dlg.ShowDialog() == true)
        {
            var fileStream = new FileStream(dlg.FileName, FileMode.Open);
            //var range = new TextRange(TextEditor.Document.ContentStart, TextEditor.Document.ContentEnd);
            //range.Load(fileStream, DataFormats.Rtf);
        }
    }

    private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        SaveFileDialog dialog = new SaveFileDialog()
        {
            Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
        };

        if (dialog.ShowDialog() == true)
        {
            File.WriteAllText(dialog.FileName, TextEditor.Text);
        }
    }

    private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        //throw new NotImplementedException();
    }

    // Обработчик нажатия File --> New.
    private void New_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        // Проверить есть ли введенные данные в TextBox
        if (String.IsNullOrEmpty(TextEditor.Text))
            return;
        else
            // Сохранить данные.
            Save_Executed(sender, e);
    }

    private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //if (cmbFontFamily.SelectedItem != null)
        //    TextEditor.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, cmbFontFamily.SelectedItem);
    }

    private void cmbFontSize_TextChanged(object sender, TextChangedEventArgs e)
    {
        //TextEditor.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, cmbFontSize.Text);
    }
}

