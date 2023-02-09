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
using System.Xml;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        bool hasGameStarted = false;

        TicTacToe ticTacToe;

        public MainWindow()
        {
            InitializeComponent();
            ticTacToe = new TicTacToe();
        }

        /// <summary>
        /// On the start button has been click get everything ready
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartgame_Click(object sender, RoutedEventArgs e)
        {
            // set game state to start
            // reset colors
            // reset labels
        }

        /// <summary>
        /// If any background colors have changed reset them
        /// </summary>
        private void resetColors()
        {

        }

        /// <summary>
        /// Reset the scoreboard labels to blank
        /// </summary>
        private void resetLabels()
        {

        }

        /// <summary>
        /// What happens when a scorboard button has been clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayerMoveClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index;
            try
            {
                Int32.TryParse(btn.Tag.ToString(), out index);
            }
            catch
            {
                Console.WriteLine("Unable to parse int from game board button");
                return;
            }

            String gameSquare = ticTacToe.getAtSquare(index);
            bool validMove = ticTacToe.setAtSquare(index);

            // If games has not started do nothing

            // If space is already filled with an x or o do nothing
            if(validMove == false)
            {
                return;
            }
            // Else fill in the square
            else {
                btn.Content = ticTacToe.getAtSquare(index);
            }

            // Is winning move

            // Highlight winning move

            // Is tie

            // If won or tie update scoareboard

            // If won or tie set game started to false

        }

        /// <summary>
        /// Set the game board to the array values 
        /// </summary>
        private void loadBoard()
        {
            int index = 0;
            foreach(Button btn in gBoard.Children)
            {
                btn.Content = ticTacToe.getAtSquare(index);
            }
        }
    }
}
