﻿using System;
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

        public void setContent(string content)
        {
            this.Dispatcher.Invoke(() =>
                {
                    if (content != null)
                    {
                        TextBox inputTextBox = (TextBox)this.FindName("InputTextBox");
                        inputTextBox.Text = content;
                    }
                }
            );
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox inputTextBox = (TextBox)sender;
            _controller.triggerDelayedSave();
        }

        private void New_Executed(object sender, System.EventArgs e)
        {
            _controller.createEmptyNote();
        }

        private void Delete_Executed(object sender, System.EventArgs e)
        {
            _controller.deleteNote();
        }
    }

    public static class CustomCommands
    {
        public static readonly RoutedUICommand New = new RoutedUICommand(
            "New",
            "New",
            typeof(CustomCommands)
        );

        public static readonly RoutedUICommand Delete = new RoutedUICommand(
            "Delete",
            "Delete",
            typeof(CustomCommands)
        );
    }
}
