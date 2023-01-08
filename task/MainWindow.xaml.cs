using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
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
    // Name of the current file.
    private string NameOfTheCurrentFile { get; set; } = "Untitled";

    public MainWindow()
    {
        InitializeComponent();
        // Window position in the center of the screen.
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
    }

    private void Save_MenuItem_Click(object sender, RoutedEventArgs e)
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

    private void SaveAs_MenuItem_Click(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void PageSetup_MenuItem_Click(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void Exit_MenuItem_Click(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void Undo_MenuItem_Click(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void Cut_MenuItem_Click(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void Copy_MenuItem_Click(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void Paste_MenuItem_Click(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void Delete_MenuItem_Click(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void Replace_MenuItem_Click(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void SelectAll_MenuItem_Click(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void TimeDate_MenuItem_Click(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ZoomIn_MenuItem_Click(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ZoomOut_MenuItem_Click(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ZoomRestore_MenuItem_Click(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ShowStatusBar_MenuItem_Click(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void WordWrap_MenuItem_Click(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void TextArea_TextChanged(object sender, TextChangedEventArgs e)
    {
        //throw new NotImplementedException();
    }

    private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        // todo: Add a question before opening: "Do you want to save changes to the file WITHOUT A NAME?"

        OpenFileDialog openFile = new OpenFileDialog();
        openFile.Filter = "Plain Text File (*.txt)|*.txt|Rich Text File (*.rtf)|*.rtf|All files (*.*)|*.*";
        if (openFile.ShowDialog() == true)
        {
            var fileStream = new FileStream(openFile.FileName, FileMode.Open);
            var range = new TextRange(TextEditor.Document.ContentStart, TextEditor.Document.ContentEnd);
            range.Load(fileStream, DataFormats.Text);
        }
    }

    private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        SaveFileDialog saveFile = new SaveFileDialog();
        saveFile.FileName = NameOfTheCurrentFile;
        saveFile.Title = "Save";
        saveFile.Filter = "Plain Text File (*.txt)|*.txt|Rich Text File (*.rtf)|*.rtf|All files (*.*)|*.*";

        FileStream fileStream = new FileStream(saveFile.FileName, FileMode.CreateNew);
        TextRange range = new TextRange(TextEditor.Document.ContentStart, TextEditor.Document.ContentEnd);
        range.Save(fileStream, DataFormats.Text);

    }

    private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}