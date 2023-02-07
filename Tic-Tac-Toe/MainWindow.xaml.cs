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

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TicTacToe ticTacToe;

        public MainWindow()
        {
            InitializeComponent();
            ticTacToe = new TicTacToe();
        }

        // CLick stat game reset game, set game state to start

        // Click scoareboard 
        private void PlayerMoveClick(object sender, RoutedEventArgs e)
        {
           string messageBoxText = "Do you want to save changes?";
            string caption = "Word Processor";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnTopLeft_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            btnTopLeft.Background = Brushes.Blue;
        }

        // LoadBoard - Set the content of the scoareboard based off of TicTacToeClass

        // Reset Colors



    }
}
