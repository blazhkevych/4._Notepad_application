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

// todo: add borders to the menu/submenu
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
            var range = new TextRange(TextEditor.Document.ContentStart, TextEditor.Document.ContentEnd);
            range.Load(fileStream, DataFormats.Rtf);
        }
    }

    private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        var dlg = new SaveFileDialog();
        dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
        if (dlg.ShowDialog() == true)
        {
            var fileStream = new FileStream(dlg.FileName, FileMode.Create);
            var range = new TextRange(TextEditor.Document.ContentStart, TextEditor.Document.ContentEnd);
            range.Save(fileStream, DataFormats.Rtf);
        }
    }

    private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        //throw new NotImplementedException();
    }


    private void New_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        // todo: Add a question before opening: "Do you want to save changes to the file WITHOUT A NAME?" if the file have not been saved yet.
    }

    private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cmbFontFamily.SelectedItem != null)
            TextEditor.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, cmbFontFamily.SelectedItem);
    }

    private void cmbFontSize_TextChanged(object sender, TextChangedEventArgs e)
    {
        TextEditor.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, cmbFontSize.Text);
    }

    private void TextEditor_SelectionChanged(object sender, RoutedEventArgs e)
    {
        var temp = TextEditor.Selection.GetPropertyValue(TextElement.FontWeightProperty);
        btnBold.IsChecked = temp != DependencyProperty.UnsetValue && temp.Equals(FontWeights.Bold);
        temp = TextEditor.Selection.GetPropertyValue(TextElement.FontStyleProperty);
        //btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));
        temp = TextEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
        //btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

        temp = TextEditor.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
        cmbFontFamily.SelectedItem = temp;
        temp = TextEditor.Selection.GetPropertyValue(TextElement.FontSizeProperty);
        cmbFontSize.Text = temp.ToString();
    }
}