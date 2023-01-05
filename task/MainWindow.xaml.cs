using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace task
{
    // Приложение "Блокнот"
    // Разработать приложение «Блокнот», обладающее той же
    // функциональностью, что и стандартный «Блокнот»
    // операционной системы Windows.

    // Notepad application
    // Develop a notepad application that has the same
    // functionality as the standard Notepad
    // Windows operating system.
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TxtEditor.Text = "";
        }

        private void NewWindowCommand_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewWindowCommand_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
