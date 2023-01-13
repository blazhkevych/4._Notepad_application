using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;

namespace task;

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

    // Name of the current file name by default.
    private string NameOfTheCurrentFile { get; set; } = "Untitled - Notepad";

    // Works on ctrl + o.
    private void Open_Executed(object sender, ExecutedRoutedEventArgs e) 
    {
        var contentStartCheck = TextEditor.Document.ContentStart;
        var contentEndCheck = TextEditor.Document.ContentEnd;
        var rangeCheck = new TextRange(contentStartCheck, contentEndCheck);
        // If the text editor is empty.
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

            // Change window title to file name.
            Title = NameOfTheCurrentFile + " - Notepad";
        }
        else
        {
            // If there is text in the text editor, then save it.
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

            // Change window title to file name.
            Title = NameOfTheCurrentFile + " - Notepad";
        }
    }

    // Works on ctrl + s.
    private void Save_Executed(object sender, ExecutedRoutedEventArgs e) // OK
    {
        var contentStartCheck = TextEditor.Document.ContentStart;
        var contentEndCheck = TextEditor.Document.ContentEnd;
        var rangeCheck = new TextRange(contentStartCheck, contentEndCheck);
        // If not saved yet.
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
        // If already saved.
        else
        {
            var fileStream = new FileStream(NameOfTheCurrentFile, FileMode.Create);
            rangeCheck.Save(fileStream, DataFormats.Rtf);
            fileStream.Close();
        }

        // Change window title to file name.
        if (Title == NameOfTheCurrentFile)
            return;
        Title = NameOfTheCurrentFile + " - Notepad";
    }

    private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        var dlg = new SaveFileDialog();
        NameOfTheCurrentFile = dlg.FileName;
        dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
        dlg.FileName = "*.rtf";
        if (dlg.ShowDialog() == true)
        {
            NameOfTheCurrentFile = dlg.FileName;
            var fileStream = new FileStream(dlg.FileName, FileMode.Create);
            var range = new TextRange(TextEditor.Document.ContentStart, TextEditor.Document.ContentEnd);
            range.Save(fileStream, DataFormats.Rtf);
            fileStream.Close();
        }

        // Change window title to file name.
        Title = NameOfTheCurrentFile + " - Notepad";
    }

    // Works on ctrl + n.
    private void New_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        var contentStartCheck = TextEditor.Document.ContentStart;
        var contentEndCheck = TextEditor.Document.ContentEnd;
        var rangeCheck = new TextRange(contentStartCheck, contentEndCheck);
        // If the text editor is empty.
        if (rangeCheck.Text == "\r\n" && NameOfTheCurrentFile == "Untitled - Notepad") return;

        // If there is text in the text editor, then save it.
        Save_Executed(sender, e);
        // Clear the text editor.
        TextEditor.Document.Blocks.Clear();
        // Change window title to default filename.
        Title = "Untitled - Notepad";
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

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        // Do you want to save changes before closing ?
        var contentStartCheck = TextEditor.Document.ContentStart;
        var contentEndCheck = TextEditor.Document.ContentEnd;
        var rangeCheck = new TextRange(contentStartCheck, contentEndCheck);
        if (rangeCheck.Text == "\r\n" && NameOfTheCurrentFile == "Untitled - Notepad")
        {
            Close();
        }
        else
        {
            var result = MessageBox.Show("Do you want to save changes to " + NameOfTheCurrentFile + "?", "Notepad",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // If not saved yet.
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
                // If already saved.
                else
                {
                    var fileStream = new FileStream(NameOfTheCurrentFile, FileMode.Create);
                    rangeCheck.Save(fileStream, DataFormats.Rtf);
                    fileStream.Close();
                }

                // Change window title to file name.
                if (Title == NameOfTheCurrentFile)
                    return;
                Title = NameOfTheCurrentFile + " - Notepad";
            }
            else if (result == MessageBoxResult.No)
            {
            }
            else if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}