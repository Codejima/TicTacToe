using System;

namespace GameLogic
{
    public class Logic
    {
        private Board[,] mGameBoard = new Board[3, 3];
        private bool mCurrentPlayer;
        private int mTurnCounter = 0;
        private bool mGameRunning = true;

        // Generate random starting(current) Player
        public Logic()
        {
            Random random = new();
            mCurrentPlayer = random.Next() % 2 == 1;
        }

        public Board[,] GetGameBoard()
        {
            return mGameBoard;
        }
        public TurnResult PlayerTurn(int x, int y)
        {
            if (x > 2 || y > 2) return TurnResult.Invalid;
            if (x < 0 || y < 0) return TurnResult.Invalid;
            if (mGameBoard[y, x] != Board.Empty) return TurnResult.Invalid;
            if (!mGameRunning) return TurnResult.Invalid;

            mGameBoard[y, x] = mCurrentPlayer ? Board.O : Board.X;
            mTurnCounter++;

            if (WinCondition())
            {
                mGameRunning = false;
                return mCurrentPlayer ? TurnResult.WinO : TurnResult.WinX;
            }
            else
            {
                if (mTurnCounter > 8) return TurnResult.Draw;
                mCurrentPlayer = !mCurrentPlayer;
                return TurnResult.Valid;
            }
        }
        private bool WinCondition()
        {
            if (mTurnCounter < 5) return false;
            
            // 00 10 20
            // 01 11 21
            // 02 12 22
            if (mGameBoard[0, 0] != Board.Empty)
            {
                // row 1
                if (mGameBoard[0, 0] == mGameBoard[0, 1] && mGameBoard[0, 0] == mGameBoard[0, 2]) return true;
                // column 1
                if (mGameBoard[0, 0] == mGameBoard[1, 0] && mGameBoard[0, 0] == mGameBoard[2, 0]) return true;
                // diagonal upper left to bottom right
                if (mGameBoard[0, 0] == mGameBoard[1, 1] && mGameBoard[0, 0] == mGameBoard[2, 2]) return true;
            }
            if (mGameBoard[0, 2] != Board.Empty)
            {
                // row 3
                if (mGameBoard[0, 2] == mGameBoard[1, 2] && mGameBoard[0, 2] == mGameBoard[2, 2]) return true;
                // diagonal bottom left to upper right
                if (mGameBoard[0, 2] == mGameBoard[1, 1] && mGameBoard[0, 2] == mGameBoard[2, 0]) return true;
            }
            // column 2
            if (mGameBoard[1, 0] != Board.Empty && mGameBoard[1, 0] == mGameBoard[1, 1] && mGameBoard[1, 0] == mGameBoard[1, 2]) return true;
            // column 3
            if (mGameBoard[2, 0] != Board.Empty && mGameBoard[2, 0] == mGameBoard[2, 1] && mGameBoard[2, 0] == mGameBoard[2, 2]) return true;
            // row 2
            if (mGameBoard[0, 1] != Board.Empty && mGameBoard[0, 1] == mGameBoard[1, 1] && mGameBoard[0, 1] == mGameBoard[2, 1]) return true;
            return false;

        }

        public bool GetCurrentPlayer()
        {
            return mCurrentPlayer;
        }
        public void ResetGame()
        {
            mGameRunning = true;
            mCurrentPlayer = !mCurrentPlayer;
            mGameBoard = new Board[3, 3];
            mTurnCounter = 0;
        }
        //public static List<Score> GetScoreList()
        //{
        //    var score = new List<Score>();
        //    return score;
        //}
    }
}
