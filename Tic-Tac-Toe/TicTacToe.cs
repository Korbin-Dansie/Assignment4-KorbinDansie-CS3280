using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
    public class TicTacToe
    {
        #region Attributes
        private const byte ROW_NUMBER = 3;
        private const byte COLUMN_NUMBER = 3;

        /// <summary>
        /// Who's turn is it. False = X, True = O
        /// </summary>
        private bool turnCounter { get; set; }

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


        #region Constructor
        public TicTacToe()
        {
            saBoard = new string[ROW_NUMBER, COLUMN_NUMBER];
            turnCounter = false;

        }
        #endregion Constructor

        #region Methods
        public bool IsWinningMove()
        {
            return false;
        }

        /// <summary>
        /// Checks if the game has been one with a row
        /// </summary>
        /// <returns></returns>
        private bool horizontalWin()
        {
            for(int i = 0; i < ROW_NUMBER; i++)
            {
                ;
            }
            return false;
        }

        /// <summary>
        /// Checks if the game has been won with a column
        /// </summary>
        /// <returns></returns>
        private bool verticalWin()
        {
            return false;
        }

        /// <summary>
        /// Checks if the game has been won with a diagonal
        /// </summary>
        /// <returns></returns>
        private bool diagonalWin()
        {
            return false;
        }

        /// <summary>
        /// Checks if the game board has been filled and no one won
        /// </summary>
        /// <returns></returns>
        public bool IsTie()
        {
            return false;
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
        /// then change who's turn it is
        /// </summary>
        /// <param name="index"></param>
        /// <returns>If move is value True</returns>
        public bool setAtSquare(int index)
        {
            // Check if square is filled
            if(getsaBoardbyIndex(index) != null || getsaBoardbyIndex(index) != String.Empty)
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

            // Switch whos turn
            turnCounter = !turnCounter;
            return true;
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

        }


        #endregion Methods


    }
}
