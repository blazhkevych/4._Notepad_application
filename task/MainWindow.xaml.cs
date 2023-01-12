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
    string NameOfTheCurrentFile { get; set; } = "Untitled - Notepad";

    // Отрабатывает на ctr + o.
    private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        var contentStartCheck = TextEditor.Document.ContentStart;
        var contentEndCheck = TextEditor.Document.ContentEnd;
        var rangeCheck = new TextRange(contentStartCheck, contentEndCheck);
        // Если в текстовом редакторе пусто.
        if (rangeCheck.Text == "\r\n")
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                NameOfTheCurrentFile = dlg.FileName;
                var fileStream = new FileStream(dlg.FileName, FileMode.Open);
                var range = new TextRange(TextEditor.Document.ContentStart, TextEditor.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);
                fileStream.Close();
            }
            // Изменить название окна на имя файла.
            Title = NameOfTheCurrentFile + " - Notepad";
        }
        else
        {
            // Если в текстовом редакторе есть текст, то сохраняем его.
            Save_Executed(sender, e);
            var dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                NameOfTheCurrentFile = dlg.FileName;
                var fileStream = new FileStream(dlg.FileName, FileMode.Open);
                var range = new TextRange(TextEditor.Document.ContentStart, TextEditor.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);
                fileStream.Close();
            }
            // Изменить название окна на имя файла.
            Title = NameOfTheCurrentFile + " - Notepad";
        }
    }

    // Отрабатывает на ctr + s.
    private void Save_Executed(object sender, ExecutedRoutedEventArgs e) // OK
    {
        //NameOfTheCurrentFile = Title;
        var contentStartCheck = TextEditor.Document.ContentStart;
        var contentEndCheck = TextEditor.Document.ContentEnd;
        var rangeCheck = new TextRange(contentStartCheck, contentEndCheck);
        // Если не сохранялся еще.
        if (NameOfTheCurrentFile == "Untitled - Notepad")
        {
            var dlg = new SaveFileDialog();
            NameOfTheCurrentFile = dlg.FileName;
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            dlg.FileName = "*.rtf";
            if (dlg.ShowDialog() == true)
            {
                NameOfTheCurrentFile = dlg.FileName;
                var fileStream = new FileStream(dlg.FileName, FileMode.Create);
                rangeCheck.Save(fileStream, DataFormats.Rtf);
                fileStream.Close();
            }
        }
        // Если уже сохранялся.
        else
        {
            var fileStream = new FileStream(NameOfTheCurrentFile, FileMode.Create);
            rangeCheck.Save(fileStream, DataFormats.Rtf);
            fileStream.Close();
        }
        // Изменить название окна на имя файла.
        if (Title == NameOfTheCurrentFile)
            return;
        else
            Title = NameOfTheCurrentFile + " - Notepad";
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
}