using Hardcodet.Wpf.TaskbarNotification;
using Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AskEpamClientApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            // Создание привязки
            CommandBinding bind = new CommandBinding(ApplicationCommands.Open);

            // Присоединение обработчика событий
            bind.Executed += OpenFormCommand_Executed;

            // Регистрация привязки
            this.CommandBindings.Add(bind);
        }

        //double click
        private void OpenFormCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            this.ShowInTaskbar = true;

            myNotifyIcon.Visibility = Visibility.Hidden;
        }


        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                myNotifyIcon.Visibility = Visibility.Visible;
            }
        }

        private void MsgButton_Click(object sender, RoutedEventArgs e)
        {
            allMsg.Text = myMsg.Text;

            //temporary code
            myNotifyIcon.ShowCustomBalloon(new FancyBalloon(),PopupAnimation.Slide, 4000);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resQuestion = MessageBox.Show("Do you really want to close the program?", "Confirm", MessageBoxButton.OKCancel);

            if (resQuestion == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

    }
}
