using System.IO;
using System.Windows;
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
    public MainWindow()
    {
        InitializeComponent();
        // Window position in the center of the screen.
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
    }

    // Name of the current file.
    private string NameOfTheCurrentFile { get; } = "Untitled";

    private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        // todo: Add a question before opening: "Do you want to save changes to the file WITHOUT A NAME?"

        var openFile = new OpenFileDialog();
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
        var saveFile = new SaveFileDialog();
        saveFile.FileName = NameOfTheCurrentFile;
        saveFile.Title = "Save";
        saveFile.Filter = "Plain Text File (*.txt)|*.txt|Rich Text File (*.rtf)|*.rtf|All files (*.*)|*.*";

        var fileStream = new FileStream(saveFile.FileName, FileMode.CreateNew);
        var range = new TextRange(TextEditor.Document.ContentStart, TextEditor.Document.ContentEnd);
        range.Save(fileStream, DataFormats.Text);
    }

    private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        //throw new NotImplementedException();
    }


    private void New_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        // todo: Add a question before opening: "Do you want to save changes to the file WITHOUT A NAME?" if the file have not been saved yet.
    }
}