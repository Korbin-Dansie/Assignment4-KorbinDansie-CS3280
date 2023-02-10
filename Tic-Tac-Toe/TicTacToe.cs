﻿using System;

namespace Tic_Tac_Toe
{
    public class TicTacToe
    {
        #region Attributes
        private const byte ROW_NUMBER = 3;
        private const byte COLUMN_NUMBER = 3;
        public bool hasGameStarted { get; set; } = false;
        private bool gameOver = false;

        /// <summary>
        /// Who's turn is it. False = X, True = O
        /// </summary>
        public bool turnCounter { get; set; }

        private string[,] saBoard;

        private int saBoardIndex
        {
            get { return saBoard.Length; }
        }

        private int iPlayer1Wins;
        private int iPlayer2Wins;
        private int iTies;

        private WinningMove eWiningMove;
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
        public int getiPlayer1Wins()
        {
            return iPlayer1Wins;
        }

        public int getiPlayer2Wins()
        {
            return iPlayer2Wins;
        }

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

        #endregion GettersAndSetter

        #region Constructor
        public TicTacToe()
        {
            saBoard = new string[ROW_NUMBER, COLUMN_NUMBER];
            turnCounter = false;
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
            String winingCharacter;
            bool won =  horizontalWin(out winingCharacter) || verticalWin(out winingCharacter) || diagonalWin(out winingCharacter);
            if (won && !gameOver)
            {
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
        /// <returns></returns>
        private bool horizontalWin(out String winingCharacter)
        {
            string startingChar;
            string nextChar;
            for (int row = 0; row < ROW_NUMBER; row++)
            {
                startingChar = saBoard[row, 0];
                // If every column in the row is the same return true
                for (int col = 1; col < COLUMN_NUMBER; col++)
                {
                    nextChar = saBoard[row, col];
                    if(nextChar != startingChar)
                    {
                        break;
                    }

                    // If we reached the end of the row and all strings are the same then somebody won
                    if(col + 1 == COLUMN_NUMBER && !(startingChar == null || startingChar == String.Empty))
                    {
                        eWiningMove = (WinningMove)row + 1;
                        winingCharacter = saBoard[row, 0];
                        return true;
                    }
                }
            }
            winingCharacter = String.Empty;
            return false;
        }

        /// <summary>
        /// Checks if the game has been won with a column
        /// </summary>
        /// <returns></returns>
        private bool verticalWin(out String winingCharacter)
        {
            string startingChar;
            string nextChar;
            for (int col = 0; col < COLUMN_NUMBER; col++)
            {
                startingChar = saBoard[0, col];
                // If every column in the row is the same return true
                for (int row = 1; row < ROW_NUMBER; row++)
                {
                    nextChar = saBoard[row, col];
                    if (nextChar != startingChar)
                    {
                        break;
                    }

                    // If we reached the end of the row and all strings are the same then somebody won
                    if (row + 1 == ROW_NUMBER && !(startingChar == null || startingChar == String.Empty))
                    {
                        eWiningMove = (WinningMove)col + 4;
                        winingCharacter = saBoard[0, col];
                        return true;
                    }
                }
            }
            winingCharacter = String.Empty;
            return false;
        }

        /// <summary>
        /// Checks if the game has been won with a diagonal
        /// </summary>
        /// <returns></returns>
        private bool diagonalWin(out String winingCharacter)
        {

            if (saBoard[0,0] == saBoard[1, 1] && saBoard[1, 1] == saBoard[2, 2] && !(saBoard[1, 1] == null || saBoard[1, 1] == String.Empty))
            {
                winingCharacter = saBoard[1, 1];
                eWiningMove = WinningMove.Diag1;
                return true;
            }
            else if (saBoard[2, 0] == saBoard[1, 1] && saBoard[1, 1] == saBoard[0, 2] && !(saBoard[1, 1] == null || saBoard[1, 1] == String.Empty))
            {
                winingCharacter = saBoard[1, 1];
                eWiningMove = WinningMove.Diag2;
                return true;
            }

            winingCharacter = String.Empty;
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
                    if(saBoard[row,col] == null || saBoard[row, col] == String.Empty)
                    {
                        return false;
                    }
                }
            }

            eWiningMove = WinningMove.None;
            gameOver = true;
            iTies++;
            return true;
        }


        /// <summary>
        /// Returns the string at a given index
        /// </summary>
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
        /// <param name="index"></param>
        /// <returns>If move is value True</returns>
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
                    saBoard[k, l] = String.Empty;
                }
            }

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

            eWiningMove = WinningMove.None;
            turnCounter = false;
            gameOver = false;
            hasGameStarted = true;
        }



        #endregion Methods

    }
}
