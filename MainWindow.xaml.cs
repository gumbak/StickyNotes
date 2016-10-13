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

namespace StickyNotes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Controller _controller;

        public MainWindow(Controller controller)
        {
            _controller = controller;
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox inputTextBox = (TextBox)sender;
            _controller.triggerDelayedSave();
        }

        public void setContent(string content)
        {
            this.Dispatcher.Invoke(() =>
                {
                    if (content != null)
                    {
                        TextBox inputTextBox = (TextBox)this.FindName("InputTextBox");
                        inputTextBox.Text = content;
                    }
                    this.Show();
                }
            );
        }

        public string getContent()
        {
            string content = "";
            this.Dispatcher.Invoke(() =>
                {
                    TextBox inputTextBox = (TextBox)this.FindName("InputTextBox");
                    content = inputTextBox.Text;
                }
            );
            return content;
        }

        private void New_Executed(object sender, System.EventArgs e)
        {
            _controller.createNote();
        }
    }

    public static class CustomCommands
    {
        public static readonly RoutedUICommand New = new RoutedUICommand(
            "New",
            "New",
            typeof(CustomCommands)
        );
    }
}
