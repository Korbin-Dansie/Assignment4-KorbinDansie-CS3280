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
        #endregion Attributes

        private string[,] saBoard;
        private int iPlayer1Wins;
        private int iPlayer2Wins;
        private int iTies;

        #region Constructor
        #endregion Constructor

        #region Methods
        public bool IsWinningMove()
        {
            return false;
        }

        public bool IsTie()
        {
            return false;
        }
        #endregion Methods

    }
}
