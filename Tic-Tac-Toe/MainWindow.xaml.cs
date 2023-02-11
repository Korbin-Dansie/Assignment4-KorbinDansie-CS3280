﻿using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TicTacToe ticTacToe;
        int gameMode;

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
            ComboBoxItem typeItem = (ComboBoxItem)cbPlayMode.SelectedItem;
            Int32.TryParse(typeItem.Tag.ToString(), out gameMode);
            if (gameMode == 0)
            {
                ticTacToe.isAIPlayerEnabled = true;
            }
            else
            {
                ticTacToe.isAIPlayerEnabled = false;
            }

            // set game state to start
            ticTacToe.clearBoard();

            // reset borders
            setActivePlayerBorder();

            // reset colors

            // reset labels
            resetLabels();
            resetHighlightWinningMove();

            // Reset the board
            loadBoard();
        }

        /// <summary>
        /// Updates the border depending on whos turn it is
        /// or if the game is over
        /// </summary>
        private void setActivePlayerBorder()
        {
            resetPlayerBorders();
            if (ticTacToe.isgameOver())
            {
                return;
            }

            if (ticTacToe.turnCounter == false)
            {
                // Show x is active player
                Color color = (Color)this.FindResource("oneLight");
                Brush brush = new SolidColorBrush(color);
                borderX.BorderBrush = brush;
            }
            else if (ticTacToe.turnCounter == true)
            {
                // Show o is active player
                Color color = (Color)this.FindResource("circleBlue");
                Brush brush = new SolidColorBrush(color);
                borderO.BorderBrush = brush;
            }

        }

        /// <summary>
        /// Reset the game and the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            reset();
        }

        /// <summary>
        /// On change how many players start a new game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbPlayMode_Changed(object sender, EventArgs e)
        {
            int newGameMode;
            ComboBoxItem typeItem = (ComboBoxItem)cbPlayMode.SelectedItem;
            if (Int32.TryParse(typeItem.Tag.ToString(), out newGameMode))
            {
                if (newGameMode != gameMode)
                {
                    gameMode = newGameMode;
                    reset();
                }

                // If single player is selected enable AI
                if (newGameMode == 0)
                {
                    ticTacToe.isAIPlayerEnabled = true;
                }
                else
                {
                    ticTacToe.isAIPlayerEnabled = false;
                }
            }
        }

        /// <summary>
        /// Set game mode to initulized value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbPlayMode_Initialized(object sender, EventArgs e)
        {
            int newGameMode;
            ComboBoxItem typeItem = (ComboBoxItem)cbPlayMode.SelectedItem;
            if (Int32.TryParse(typeItem.Tag.ToString(), out newGameMode))
            {
                gameMode = newGameMode;
            }

        }


        /// <summary>
        /// Reset the UI and TicTacToe
        /// </summary>
        private void reset()
        {
            ticTacToe.restartGame();

            resetPlayerBorders();
            resetLabels();
            resetHighlightWinningMove();
            resetColors();
            loadBoard();

            lbClickStartToBegin.Visibility = Visibility.Visible;

            lbXScore.Content = "-";
            lbOScore.Content = "-";
            lbTieScore.Content = "-";
        }

        /// <summary>
        /// If any background colors have changed reset them
        /// </summary>
        private void resetColors()
        {

        }

        /// <summary>
        /// Reset the active player borders to black
        /// </summary>
        private void resetPlayerBorders()
        {
            Brush brush = new SolidColorBrush(Colors.Black);
            borderX.BorderBrush = brush;
            borderO.BorderBrush = brush;
        }

        private void resetHighlightWinningMove()
        {
            pRow1.Visibility = Visibility.Hidden;
            pRow2.Visibility = Visibility.Hidden;
            pRow3.Visibility = Visibility.Hidden;

            pCol1.Visibility = Visibility.Hidden;
            pCol2.Visibility = Visibility.Hidden;
            pCol3.Visibility = Visibility.Hidden;

            pDia1.Visibility = Visibility.Hidden;
            pDia2.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Reset the scoreboard labels to blank
        /// </summary>
        private void resetLabels()
        {
            lbPlayer1Wins.Visibility = Visibility.Hidden;
            lbPlayer2Wins.Visibility = Visibility.Hidden;
            lbItsATie.Visibility = Visibility.Hidden;

            lbClickStartToBegin.Visibility = Visibility.Hidden;

        }

        /// <summary>
        /// What happens when a scorboard button has been clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayerMoveClick(object sender, RoutedEventArgs e)
        {
            // If the game has not stared do noting and tell user to click start
            if (!ticTacToe.hasGameStarted)
            {
                resetLabels();
                lbClickStartToBegin.Visibility = Visibility.Visible;
                return;
            }

            // If AI is enabled and its their turn do nothing
            bool currentPlayerTurn = ticTacToe.turnCounter;
            if (ticTacToe.isAIPlayerEnabled && currentPlayerTurn == true)
            {
                return;
            }

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

            // Set the square and 
            bool validMove = ticTacToe.setAtSquare(index);

            // If games has not started do nothing

            // If space is already filled with an x or o do nothing
            if (validMove == false)
            {
                return;
            }

            // Else fill in the square
            else
            {
                string inputString = ticTacToe.getAtSquare(index);

                btn.Content = inputString;

                if (inputString.ToUpper() == "X")
                {
                    Color color = (Color)this.FindResource("oneLight");
                    Brush brush = new SolidColorBrush(color);
                    btn.Foreground = brush;
                }
                else
                {
                    Color color = (Color)this.FindResource("circleBlue");
                    Brush brush = new SolidColorBrush(color);
                    btn.Foreground = brush;
                }

            }

            // Is winning move
            if (ticTacToe.hasWon())
            {
                switch (currentPlayerTurn)
                {
                    case false: //xs' turn
                        lbXScore.Content = ticTacToe.getiPlayer1Wins();
                        lbPlayer1Wins.Visibility = Visibility.Visible;

                        break;
                    case true: // o's turn
                        lbOScore.Content = ticTacToe.getiPlayer2Wins();
                        lbPlayer2Wins.Visibility = Visibility.Visible;
                        break;
                }

                // Hightlight wining move
                hightlightWinningMove(currentPlayerTurn);
            }

            // If won or tie update scoareboard
            // Is tie
            if (ticTacToe.IsTie())
            {
                lbTieScore.Content = ticTacToe.getiTies();
                lbItsATie.Visibility = Visibility.Visible;
            }


            // If won or tie set game started to false
            setActivePlayerBorder();

            // If AI is enabled let them take their turn
            if (ticTacToe.isAIPlayerEnabled && !ticTacToe.isgameOver())
            {
                DispatcherTimer dispatcherTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.5) };
                dispatcherTimer.Tick += (sender, args) =>
                {
                    ticTacToe.aiPlayerSetSquare();
                    loadBoard();
                    // Is winning move
                    if (ticTacToe.hasWon())
                    {


                        dispatcherTimer.Start();

                        lbOScore.Content = ticTacToe.getiPlayer2Wins();
                        lbPlayer2Wins.Visibility = Visibility.Visible;
                        // Hightlight wining move
                        hightlightWinningMove(true);
                    }
                    setActivePlayerBorder();

                    dispatcherTimer.Stop();
                };

                dispatcherTimer.Start();
            }
        }

        /// <summary>
        /// Show a line connecting the winning squares
        /// <para>Takes whos turn it is</para>
        /// </summary>
        private void hightlightWinningMove(bool currentPlayerTurn)
        {
            Color color;

            // Set the winning colors to the line color 
            if (!currentPlayerTurn)
            {
                // Show x is active player
                color = (Color)this.FindResource("oneLight");

            }
            else
            {
                // Show o is active player
                color = (Color)this.FindResource("circleBlue");
            }

            switch (ticTacToe.whichSquareWon())
            {
                case 1:
                    pRow1.Visibility = Visibility.Visible;
                    pRow1.Stroke = new SolidColorBrush(color);
                    break;
                case 2:
                    pRow2.Visibility = Visibility.Visible;
                    pRow2.Stroke = new SolidColorBrush(color);
                    break;
                case 3:
                    pRow3.Visibility = Visibility.Visible;
                    pRow3.Stroke = new SolidColorBrush(color);
                    break;
                case 4:
                    pCol1.Visibility = Visibility.Visible;
                    pCol1.Stroke = new SolidColorBrush(color);
                    break;
                case 5:
                    pCol2.Visibility = Visibility.Visible;
                    pCol2.Stroke = new SolidColorBrush(color);
                    break;
                case 6:
                    pCol3.Visibility = Visibility.Visible;
                    pCol3.Stroke = new SolidColorBrush(color);
                    break;
                case 7:
                    pDia1.Visibility = Visibility.Visible;
                    pDia1.Stroke = new SolidColorBrush(color);
                    break;
                case 8:
                    pDia2.Visibility = Visibility.Visible;
                    pDia2.Stroke = new SolidColorBrush(color);
                    break;
            }
        }

        /// <summary>
        /// Set the game board to the array values 
        /// </summary>
        private void loadBoard()
        {
            int index = 0;
            foreach (Button btn in gBoard.Children)
            {
                btn.Content = ticTacToe.getAtSquare(index);
                if (btn.Content.ToString() == "X")
                {
                    // Show x is active player
                    Color color = (Color)this.FindResource("oneLight");
                    Brush brush = new SolidColorBrush(color);
                    btn.Foreground = brush;
                }
                else if (btn.Content.ToString() == "O")
                {
                    // Show o is active player
                    Color color = (Color)this.FindResource("circleBlue");
                    Brush brush = new SolidColorBrush(color);
                    btn.Foreground = brush;
                }
                else
                {
                    btn.Content = "";
                }

                index++;
            }
        }
    }
}
