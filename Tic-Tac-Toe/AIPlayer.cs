using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
    internal class AIPlayer
    {
        /// <summary>
        /// Random number for when we dont know were to go
        /// </summary>
        Random random = new Random();

        // A copy of the board
        private string[,] saBoard;
        TicTacToe ticTacToe;

        public AIPlayer(TicTacToe ticTacToe)
        {
            this.ticTacToe = ticTacToe;
            this.saBoard = ticTacToe.getsaBord();
        }

        public void loadBoard(TicTacToe ticTacToe)
        {
            this.ticTacToe = ticTacToe;
            this.saBoard = ticTacToe.getsaBord();
        }


        /// <summary>
        /// Returns the index of the next move
        /// </summary>
        /// <returns></returns>
        public int aiPlayerSetSquare()
        {
            int ourWin = checkForWin("O");
            int oppWin = checkForWin("X");
            // Check if there is a win
            if (ourWin != -1)
            {
                return ourWin;
            }
            // Check if opponent would win
            else if (oppWin != -1)
            {
                return oppWin;
            }
            // Else generate a random number to return
            else
            {
                return getRandomSquare();
            }

        }

        private int checkForWin(string turnLetterValue)
        {
            int rows = checkForWinRow(turnLetterValue);
            int cols = checkForWinColumn(turnLetterValue);
            int diag = checkForWinDiagonal(turnLetterValue);
            if (rows != -1)
            {
                return rows;
            }
            else if (cols != -1)
            {
                return cols;
            }
            else if (diag != -1)
            {
                return diag;
            }

            return -1;
        }

        /// <summary>
        /// Get the index if there is a win for Rows. If not return -1
        /// </summary>
        /// <returns></returns>
        private int checkForWinRow(string turnLetterValue)
        {
            string checkLetter;
            string checkEmptySpotLetter;
            // Loop though each row and if there is a spot between two take it 
            for (int row = 0; row < saBoard.GetLength(0); row++)
            {
                checkLetter = saBoard[row, 0];
                if (saBoard[row, 0] == saBoard[row, 1] && checkLetter == turnLetterValue && !(checkLetter == String.Empty || checkLetter == null))
                {
                    // make sure spot is not taken
                    checkEmptySpotLetter = saBoard[row, 2];
                    if ((checkEmptySpotLetter == String.Empty || checkEmptySpotLetter == null))
                    {
                        return row * 3 + 2;
                    }
                }
                checkLetter = saBoard[row, 1];
                if (saBoard[row, 1] == saBoard[row, 2] && checkLetter == turnLetterValue && !(checkLetter == String.Empty || checkLetter == null))
                {
                    checkEmptySpotLetter = saBoard[row, 0];
                    if ((checkEmptySpotLetter == String.Empty || checkEmptySpotLetter == null))
                    {
                        return row * 3;
                    }
                }
                checkLetter = saBoard[row, 0];
                if (saBoard[row, 0] == saBoard[row, 2] && checkLetter == turnLetterValue && !(checkLetter == String.Empty || checkLetter == null))
                {
                    checkEmptySpotLetter = saBoard[row, 1];
                    if ((checkEmptySpotLetter == String.Empty || checkEmptySpotLetter == null))
                    {
                        return row * 3 + 1;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Get the index if there is a win for Columns. If not return -1
        /// </summary>
        /// <returns></returns>
        private int checkForWinColumn(string turnLetterValue)
        {
            string checkLetter;
            string checkEmptySpotLetter;
            for (int col = 0; col < saBoard.GetLength(1); col++)
            {
                checkLetter = saBoard[0, col];
                if (saBoard[0, col] == saBoard[1, col] && checkLetter == turnLetterValue && !(checkLetter == String.Empty || checkLetter == null))
                {
                    checkEmptySpotLetter = saBoard[2, col];
                    if ((checkEmptySpotLetter == String.Empty || checkEmptySpotLetter == null))
                    {
                        return col + 6;
                    }
                }
                checkLetter = saBoard[1, col];
                if (saBoard[1, col] == saBoard[2, col] && checkLetter == turnLetterValue && !(checkLetter == String.Empty || checkLetter == null))
                {
                    checkEmptySpotLetter = saBoard[0, col];
                    if ((checkEmptySpotLetter == String.Empty || checkEmptySpotLetter == null))
                    {
                        return col;
                    }
                }
                checkLetter = saBoard[0, col];
                if (saBoard[0, col] == saBoard[2, col] && checkLetter == turnLetterValue && !(checkLetter == String.Empty || checkLetter == null))
                {
                    checkEmptySpotLetter = saBoard[1, col];
                    if ((checkEmptySpotLetter == String.Empty || checkEmptySpotLetter == null))
                    {
                        return col + 3;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Get the index if there is a win for Diagonals. If not return -1
        /// </summary>
        /// <returns></returns>
        private int checkForWinDiagonal(string turnLetterValue)
        {
            string checkEmptySpotLetter;
            // Digonal 1
            if (saBoard[0, 0] == saBoard[1, 1] && saBoard[1, 1] == turnLetterValue && !(saBoard[1, 1] == String.Empty || saBoard[1, 1] == null))
            {
                checkEmptySpotLetter = saBoard[2, 2];
                if ((checkEmptySpotLetter == String.Empty || checkEmptySpotLetter == null))
                {
                    return 8;
                }
            }
            if (saBoard[1, 1] == saBoard[2, 2] && saBoard[1, 1] == turnLetterValue && !(saBoard[1, 1] == String.Empty || saBoard[1, 1] == null))
            {
                checkEmptySpotLetter = saBoard[0, 0];
                if ((checkEmptySpotLetter == String.Empty || checkEmptySpotLetter == null))
                {
                    return 0;
                }
            }
            if (saBoard[0, 0] == saBoard[2, 2] && saBoard[0, 0] == turnLetterValue && !(saBoard[0, 0] == String.Empty || saBoard[0, 0] == null))
            {
                checkEmptySpotLetter = saBoard[1, 1];
                if ((checkEmptySpotLetter == String.Empty || checkEmptySpotLetter == null))
                {
                    return 4;
                }
            }

            // Digonal 2
            if (saBoard[2, 0] == saBoard[1, 1] && saBoard[1, 1] == turnLetterValue && !(saBoard[1, 1] == String.Empty || saBoard[1, 1] == null))
            {
                checkEmptySpotLetter = saBoard[0, 2];
                if ((checkEmptySpotLetter == String.Empty || checkEmptySpotLetter == null))
                {
                    return 2;
                }
            }
            if (saBoard[1, 1] == saBoard[0, 2] && saBoard[1, 1] == turnLetterValue && !(saBoard[1, 1] == String.Empty || saBoard[1, 1] == null))
            {
                checkEmptySpotLetter = saBoard[2, 0];
                if ((checkEmptySpotLetter == String.Empty || checkEmptySpotLetter == null))
                {
                    return 6;
                }
            }
            if (saBoard[2, 0] == saBoard[0, 2] && saBoard[0, 2] == turnLetterValue && !(saBoard[0, 2] == String.Empty || saBoard[0, 2] == null))
            {
                checkEmptySpotLetter = saBoard[1, 1];
                if ((checkEmptySpotLetter == String.Empty || checkEmptySpotLetter == null))
                {
                    return 4;
                }
            }

            return -1;
        }

        /// <summary>
        /// Try random squares until we can set one
        /// </summary>
        /// <returns></returns>
        private int getRandomSquare()
        {
            int result;
            do
            {
                result = random.Next(0, 9);
            } while (!ticTacToe.setAtSquare(result));
            return result;
        }
    }
}
