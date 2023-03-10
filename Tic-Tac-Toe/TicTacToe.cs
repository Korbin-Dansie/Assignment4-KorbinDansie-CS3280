using System;
using System.Reflection.Metadata;

namespace Tic_Tac_Toe
{
    public class TicTacToe
    {
        #region Attributes
        /// <summary>
        /// Number of row is the game board
        /// </summary>
        private const byte ROW_NUMBER = 3;

        /// <summary>
        /// Number of columns in the game board
        /// </summary>
        private const byte COLUMN_NUMBER = 3;

        /// <summary>
        /// Sees if game has started
        /// </summary>
        public bool hasGameStarted { get; set; } = false;

        /// <summary>
        /// Sees if game is over
        /// </summary>
        private bool gameOver = false;

        /// <summary>
        /// Who's turn is it. False = X, True = O
        /// </summary>
        public bool turnCounter { get; set; }

        /// <summary>
        /// Sees if Ai player is active
        /// </summary>
        public bool isAIPlayerEnabled { get; set; } = false;

        /// <summary>
        /// The Ai player
        /// </summary>
        private AIPlayer aiPlayer;

        /// <summary>
        /// The game board
        /// </summary>
        private string[,] saBoard;

        /// <summary>
        /// Number of wins for X
        /// </summary>
        private int iPlayer1Wins;

        /// <summary>
        /// Number of wins for O
        /// </summary>
        private int iPlayer2Wins;

        /// <summary>
        /// Number of ties
        /// </summary>
        private int iTies;

        /// <summary>
        /// Variable to store how the game was won
        /// </summary>
        private WinningMove eWiningMove;

        /// <summary>
        /// The different ways to win
        /// </summary>
        private enum WinningMove
        {
            None,
            Row1,
            Row2,
            Row3,
            Col1,
            Col2,
            Col3,
            Diag1, /*Top    left - Bottom    Right*/
            Diag2, /*Bottom left - Top       Right*/
        }

        #endregion Attributes

        #region GettersAndSetter
        /// <summary>
        /// Get X's score
        /// </summary>
        /// <returns></returns>
        public int getiPlayer1Wins()
        {
            return iPlayer1Wins;
        }

        /// <summary>
        /// Get O's score
        /// </summary>
        /// <returns></returns>
        public int getiPlayer2Wins()
        {
            return iPlayer2Wins;
        }

        /// <summary>
        /// Get number of ties
        /// </summary>
        /// <returns></returns>
        public int getiTies()
        {
            return iTies;
        }

        /// <summary>
        /// Return which match won
        /// None    0,
        /// Rows    1-3,
        /// Col     4-6,
        /// Dia     7 = \,
        /// Dia     8 = /,
        /// </summary>
        /// <returns></returns>
        public int whichSquareWon()
        {
            return (int)eWiningMove;
        }

        /// <summary>
        /// Checks to see if the game is over
        /// </summary>
        /// <returns></returns>
        public bool isgameOver()
        {
            hasWon();
            IsTie();
            return gameOver;
        }

        /// <summary>
        /// Return the gameboard
        /// </summary>
        /// <returns></returns>
        public string[,] getsaBord()
        {
            return saBoard;
        }

        #endregion GettersAndSetter

        #region Constructor

        /// <summary>
        /// Construct the game
        /// </summary>
        public TicTacToe()
        {
            saBoard = new string[ROW_NUMBER, COLUMN_NUMBER];
            turnCounter = false;
            aiPlayer = new AIPlayer(this);
        }

        #endregion Constructor

        #region Methods
        /// <summary>
        /// Return if somebody has won the game yet
        /// If so update who scores
        /// </summary>
        /// <returns></returns>
        public bool hasWon()
        {
            // Which character won
            string winingCharacter;

            // did somebody win
            bool won =  horizontalWin(out winingCharacter) || verticalWin(out winingCharacter) || diagonalWin(out winingCharacter);
            if (won && !gameOver)
            {
                // Set game over and update score
                gameOver = true;
                if(winingCharacter == "X")
                {
                    iPlayer1Wins++;

                }
                else
                {
                    iPlayer2Wins++;

                }
            }
            return won;
        }

        /// <summary>
        /// Checks if the game has been one with a row
        /// </summary>
        /// <param name="winingCharacter">Which character won</param>
        /// <returns></returns>
        private bool horizontalWin(out string winingCharacter)
        {
            string startingChar;
            string nextChar;
            for (int row = 0; row < ROW_NUMBER; row++)
            {
                startingChar = saBoard[row, 0];

                // If every character in the row is the same return true
                for (int col = 1; col < COLUMN_NUMBER; col++)
                {
                    nextChar = saBoard[row, col];
                    if(nextChar != startingChar)
                    {
                        break;
                    }

                    // If we reached the end of the row and all strings are the same then somebody won
                    if(col + 1 == COLUMN_NUMBER && !(startingChar == null || startingChar == string.Empty))
                    {
                        eWiningMove = (WinningMove)row + 1;
                        winingCharacter = saBoard[row, 0];
                        return true;
                    }
                }
            }
            winingCharacter = string.Empty;
            return false;
        }

        /// <summary>
        /// Checks if the game has been won with a column
        /// </summary>
        /// <param name="winingCharacter">Which character won</param>
        /// <returns></returns>
        private bool verticalWin(out string winingCharacter)
        {
            string startingChar;
            string nextChar;
            for (int col = 0; col < COLUMN_NUMBER; col++)
            {
                startingChar = saBoard[0, col];
                // If every character in the column is the same return true
                for (int row = 1; row < ROW_NUMBER; row++)
                {
                    nextChar = saBoard[row, col];
                    if (nextChar != startingChar)
                    {
                        break;
                    }

                    // If we reached the end of the column and all strings are the same then somebody won
                    if (row + 1 == ROW_NUMBER && !(startingChar == null || startingChar == string.Empty))
                    {
                        eWiningMove = (WinningMove)col + 4;
                        winingCharacter = saBoard[0, col];
                        return true;
                    }
                }
            }
            winingCharacter = string.Empty;
            return false;
        }

        /// <summary>
        /// Checks if the game has been won with a diagonal
        /// </summary>
        /// <param name="winingCharacter">Which character won</param>
        /// <returns></returns>
        private bool diagonalWin(out string winingCharacter)
        {
            // First diagonal \
            if (saBoard[0,0] == saBoard[1, 1] && saBoard[1, 1] == saBoard[2, 2] && !(saBoard[1, 1] == null || saBoard[1, 1] == string.Empty))
            {
                winingCharacter = saBoard[1, 1];
                eWiningMove = WinningMove.Diag1;
                return true;
            }
            // Second diagonal /
            else if (saBoard[2, 0] == saBoard[1, 1] && saBoard[1, 1] == saBoard[0, 2] && !(saBoard[1, 1] == null || saBoard[1, 1] == string.Empty))
            {
                winingCharacter = saBoard[1, 1];
                eWiningMove = WinningMove.Diag2;
                return true;
            }

            winingCharacter = string.Empty;
            return false;
        }

        /// <summary>
        /// Checks if the game board has been filled and no one won
        /// </summary>
        /// <returns></returns>
        public bool IsTie()
        {
            // If somebody has won return false
            if (hasWon())
            {
                return false;
            }
            // If every row is filled return true
            for (int row = 0; row < ROW_NUMBER; row++)
            {
                for (int col = 0; col < COLUMN_NUMBER; col++)
                {
                    if(saBoard[row,col] == null || saBoard[row, col] == string.Empty)
                    {
                        return false;
                    }
                }
            }

            // Update the game status
            eWiningMove = WinningMove.None;
            gameOver = true;
            iTies++;
            return true;
        }


        /// <summary>
        /// Returns the string at a given index
        /// </summary>
        /// <param name="index">Based on 1d array</param>
        /// <returns></returns>
        public string getAtSquare(int index)
        {
            int row = index / ROW_NUMBER;
            int col = index % COLUMN_NUMBER;

            return saBoard[row, col];
        }

        /// <summary>
        /// Set X or O to a game board square if valid
        /// </summary>
        /// <param name="index">Based on 1d array</param>
        /// <returns>If move is valid return true</returns>
        public bool setAtSquare(int index)
        {
            // Make sure the game is not over, or game has not started
            if (gameOver)
            {
                return false;
            }
            // Check if square is filled
            string indexString = getsaBoardbyIndex(index);
            if (!(indexString == null || indexString == String.Empty))
            {
                return false;
            }

            // Check whos turn then insert a value
            if (!turnCounter)
            {
                setsaBoardbyIndex(index, "X");
            }
            else
            {
                setsaBoardbyIndex(index, "O");
            }

            // Change hows turn it is
            changeTurns();
            return true;
        }

        /// <summary>
        /// Change who's turn it is
        /// </summary>
        public void changeTurns()
        {
            turnCounter = !turnCounter;
        }

        /// <summary>
        /// returns value of saBoard based on a one dimensional array
        /// </summary>
        /// <param name="index"></param>
        private string getsaBoardbyIndex(int index)
        {
            int row = index / ROW_NUMBER;
            int col = index % COLUMN_NUMBER;

            return saBoard[row, col];
        }

        /// <summary>
        /// set a saBoard based on a one dimensional array
        /// </summary>
        private void setsaBoardbyIndex(int index, string value)
        {
            int row = index / ROW_NUMBER;
            int col = index % COLUMN_NUMBER;

            saBoard[row, col] = value;
        }

        /// <summary>
        /// Reset all the variables
        /// </summary>
        public void restartGame()
        {
            for (int k = 0; k < saBoard.GetLength(0); k++)
            {
                for (int l = 0; l < saBoard.GetLength(1); l++)
                {
                    saBoard[k, l] = string.Empty;
                }
            }

            // Reset all the score values
            eWiningMove = WinningMove.None;
            iPlayer1Wins = 0;
            iPlayer2Wins = 0;
            iTies = 0;
            turnCounter = false;
            hasGameStarted = false;
            gameOver = false;
        }

        /// <summary>
        /// Clear the board and get ready for a new game
        /// </summary>
        public void clearBoard()
        {
            for (int k = 0; k < saBoard.GetLength(0); k++)
            {
                for (int l = 0; l < saBoard.GetLength(1); l++)
                {
                    saBoard[k, l] = String.Empty;
                }
            }


            // Reset the board but keep the score
            eWiningMove = WinningMove.None;
            turnCounter = false;
            gameOver = false;
            hasGameStarted = true;
        }

        public void copyBoard(TicTacToe ticTacToe)
        {
            saBoard = ticTacToe.saBoard;
        }

        /// <summary>
        /// Sets a square made by the AI player
        /// </summary>
        public void aiPlayerSetSquare()
        {
            aiPlayer.loadBoard(this);
            int index = aiPlayer.aiPlayerSetSquare();
            setAtSquare(index);
        }


        #endregion Methods

    }
}
