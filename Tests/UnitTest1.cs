using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameLogic;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        /* checklist for tests
         * parameters of a functiom
         *  - null
         *  - negative values
         *  - empty container
         *  - values too big
         *  - correct exceptions if incorrect values used
         *  
         * return of a function
         *  - null
         *  - empty container
         *  - expected value
         */

        /* test TODO
         * two turns with different coordinates must include different "stones"
         * if last "stone" is set (board is full) and no win than return draw
         * if last "stone" is set (board is full) and win than return win
         * if 3 "stones" in one row, return "win"
         * if 3 "stones" in one column, return "win"
         * if 3 "stones" in diagonal, return "win"
         * repeated invalid moves do not switch player
         * 
         * one and the same player should not be first to place more than 10 times in a row (i.e. losing player starts or counter with starting player)
         * after win: valid moves are invalid
         */
        [TestMethod]
        public void Createable()
        {
            Logic logic = new();
            Assert.IsNotNull(logic);
        }

        [TestMethod]
        // empty field after game start
        public void EmptyFieldAfterGameStart()
        {
            Logic logic = new();
            Board[,] returnedBoard = logic.GetGameBoard();

            foreach (Board item in returnedBoard)
            {
                Assert.IsTrue(item == Board.Empty);
            }
        }

        // turn with valid coordinate, field is filled, rest of board still empty
        [TestMethod]
        public void FirstMove_Valid()
        {
            Logic logic = new();

            TurnResult result = logic.PlayerTurn(0, 0); // first move with valid coordinates must be valid

            Assert.IsTrue(result == TurnResult.Valid); // test if valid returned
            Assert.IsTrue(logic.GetGameBoard()[0, 0] != Board.Empty);// test if move is represented on board
        }

        [TestMethod]
        // turn with invalid coordinate (out of bounds) must be invalid
        public void FirstMove_OutOfBounds()
        {
            Logic logic = new();

            TurnResult result = logic.PlayerTurn(3, 3);
            var returnedBoard = logic.GetGameBoard();

            Assert.IsTrue(result == TurnResult.Invalid);
            foreach (Board item in returnedBoard)
            {
                Assert.IsTrue(item == Board.Empty);
            }
        }

        [TestMethod]
        public void FirstMove_NegativeCoordinates()
        {
            Logic logic = new();

            TurnResult result = logic.PlayerTurn(-1, -1);
            var returnedBoard = logic.GetGameBoard();

            Assert.IsTrue(result == TurnResult.Invalid);
            foreach (Board item in returnedBoard)
            {
                Assert.IsTrue(item == Board.Empty);
            }
        }

        [TestMethod]
        public void PlayerSwitch_ValidMove()
        {
            Logic logic = new();
            // O        00 10 20
            // X X      01 11 21
            //          02 12 22
            Assert.IsTrue(logic.PlayerTurn(1, 1) == TurnResult.Valid); // X
            Assert.IsTrue(logic.PlayerTurn(0, 0) == TurnResult.Valid); // O
            Assert.IsTrue(logic.PlayerTurn(0, 1) == TurnResult.Valid); // X

            var returnedBoard = logic.GetGameBoard();

            Assert.IsTrue(returnedBoard[1, 1] != Board.Empty);
            Assert.IsTrue(returnedBoard[0, 0] != Board.Empty);
            Assert.IsTrue(returnedBoard[1, 0] != Board.Empty);

            Assert.IsTrue(returnedBoard[1, 1] == returnedBoard[1, 0]);
        }

        [TestMethod]
        public void PlayerSwitch_InvalidMove()
        {
            Logic logic = new();
            // O
            //   X
            // 
            Assert.IsTrue(logic.PlayerTurn(1, 1) == TurnResult.Valid);   // X picks mid
            Assert.IsTrue(logic.PlayerTurn(1, 1) == TurnResult.Invalid); // O picks mid (invalid) and keeps his turn
            Assert.IsTrue(logic.PlayerTurn(3, -1) == TurnResult.Invalid);// O picks invalid coordinate
            Assert.IsTrue(logic.PlayerTurn(0, 0) == TurnResult.Valid);   // O picks upper left

            var returnedBoard = logic.GetGameBoard();
            Assert.IsTrue(returnedBoard[1, 1] != Board.Empty);
            Assert.IsTrue(returnedBoard[0, 0] != Board.Empty);

            Assert.IsTrue(returnedBoard[0, 0] != returnedBoard[1, 1]);
        }

        [TestMethod]
        public void EndGame_Win()
        {
            Logic logic = new();
            // X        00 10 20
            // X O      01 11 21
            // X O      02 12 22

            Assert.IsTrue(logic.PlayerTurn(0, 1) == TurnResult.Valid); // X
            Assert.IsTrue(logic.PlayerTurn(1, 1) == TurnResult.Valid); // O
            Assert.IsTrue(logic.PlayerTurn(0, 2) == TurnResult.Valid); // X
            Assert.IsTrue(logic.PlayerTurn(1, 2) == TurnResult.Valid); // O

            var turnResult = logic.PlayerTurn(0, 0); // X and X wins
            Assert.IsTrue(turnResult == TurnResult.WinO || turnResult == TurnResult.WinX);// X wins
        }
        [TestMethod]
        public void EndGame_WinRow()
        {
            Logic logic = new();
            // X X X    00 10 20
            // O O      01 11 21
            //          02 12 22

            Assert.IsTrue(logic.PlayerTurn(0, 0) == TurnResult.Valid); // X
            Assert.IsTrue(logic.PlayerTurn(0, 1) == TurnResult.Valid); // O
            Assert.IsTrue(logic.PlayerTurn(1, 0) == TurnResult.Valid); // X
            Assert.IsTrue(logic.PlayerTurn(1, 1) == TurnResult.Valid); // O

            var turnResult = logic.PlayerTurn(2, 0); // x and X wins
            Assert.IsTrue(turnResult == TurnResult.WinO || turnResult == TurnResult.WinX);// X wins
        }
        [TestMethod]
        public void EndGame_WinColumn()
        {
            Logic logic = new();
            // X O     00 10 20
            // X O     01 11 21
            // X       02 12 22

            Assert.IsTrue(logic.PlayerTurn(0, 0) == TurnResult.Valid); // X
            Assert.IsTrue(logic.PlayerTurn(1, 0) == TurnResult.Valid); // O
            Assert.IsTrue(logic.PlayerTurn(0, 1) == TurnResult.Valid); // X
            Assert.IsTrue(logic.PlayerTurn(1, 1) == TurnResult.Valid); // O

            var turnResult = logic.PlayerTurn(0, 2); // X and X wins
            Assert.IsTrue(turnResult == TurnResult.WinO || turnResult == TurnResult.WinX);// X wins
        }
        [TestMethod]
        public void EndGame_WinDiagonal()
        {
            Logic logic = new();
            // X O     00 10 20
            // O X     01 11 21
            //     X   02 12 22

            Assert.IsTrue(logic.PlayerTurn(0, 0) == TurnResult.Valid); // X
            Assert.IsTrue(logic.PlayerTurn(1, 0) == TurnResult.Valid); // O
            Assert.IsTrue(logic.PlayerTurn(1, 1) == TurnResult.Valid); // X
            Assert.IsTrue(logic.PlayerTurn(0, 1) == TurnResult.Valid); // O

            var turnResult = logic.PlayerTurn(2, 2); // X and X wins
            Assert.IsTrue(turnResult == TurnResult.WinO || turnResult == TurnResult.WinX);//PlayerA gewinnt
        }
        [TestMethod]
        public void EndGame_Draw()
        {
            Logic logic = new();
            // X O X
            // O X O
            // O X O
            Assert.IsTrue(logic.PlayerTurn(0, 1) == TurnResult.Valid); // O
            Assert.IsTrue(logic.PlayerTurn(0, 0) == TurnResult.Valid); // X
            Assert.IsTrue(logic.PlayerTurn(1, 0) == TurnResult.Valid); // O
            Assert.IsTrue(logic.PlayerTurn(0, 2) == TurnResult.Valid); // X
            Assert.IsTrue(logic.PlayerTurn(2, 0) == TurnResult.Valid); // O
            Assert.IsTrue(logic.PlayerTurn(1, 1) == TurnResult.Valid); // X
            Assert.IsTrue(logic.PlayerTurn(1, 2) == TurnResult.Valid); // O
            Assert.IsTrue(logic.PlayerTurn(2, 1) == TurnResult.Valid); // X
            Assert.IsTrue(logic.PlayerTurn(2, 2) == TurnResult.Draw);  // O
        }
        [TestMethod]
        public void EndGame_NoMovesAfterWin()
        {
            Logic logic = new();

            Assert.IsTrue(logic.PlayerTurn(0, 1) == TurnResult.Valid); // X
            Assert.IsTrue(logic.PlayerTurn(1, 1) == TurnResult.Valid); // O

            Assert.IsTrue(logic.PlayerTurn(0, 2) == TurnResult.Valid); // X
            Assert.IsTrue(logic.PlayerTurn(1, 2) == TurnResult.Valid); // O

            var turnResult = logic.PlayerTurn(0, 0);
            Assert.IsTrue(turnResult == TurnResult.WinO || turnResult == TurnResult.WinX);// X wins

            Assert.IsTrue(logic.PlayerTurn(2, 2) == TurnResult.Invalid); // Invalid, because game ended

        }
        //TODO: win on last stone
        [TestMethod]

        public void Player_GetCurrentPlayer()
        {
            Logic logic = new();

            bool result = logic.GetCurrentPlayer();
            logic.PlayerTurn(0, 0);
            Assert.IsTrue(result != logic.GetCurrentPlayer());
        }
        [TestMethod]

        public void Player_GetCurrentPlayer_InvalidMove()
        {
            Logic logic = new();

            bool result = logic.GetCurrentPlayer();
            logic.PlayerTurn(0, 0); // X
            logic.PlayerTurn(0, 1); // O
            logic.PlayerTurn(0, 1); // X invalid, x stays current player
            Assert.IsTrue(result == logic.GetCurrentPlayer());
        }
        [TestMethod]
        public void Reset_SwitchesPlayer()
        {
            Logic logic = new();
            bool firstPlayer = logic.GetCurrentPlayer();

            logic.ResetGame();
            Assert.IsTrue(firstPlayer != logic.GetCurrentPlayer());
            logic.ResetGame();
            Assert.IsTrue(firstPlayer == logic.GetCurrentPlayer());
        }

        [TestMethod]
        public void Reset_ClearsTheBoard()
        {
            Logic logic = new();
            logic.PlayerTurn(1, 1);
            Assert.IsTrue(logic.GetGameBoard()[1, 1] != Board.Empty);
            logic.ResetGame();
            Assert.IsTrue(logic.GetGameBoard()[1, 1] == Board.Empty);

        }

        [TestMethod]
        public void Player_DifferentBeginners()
        {
            List<Logic> logicList = new();

            logicList.Add(new Logic());
            logicList.Add(new Logic());
            logicList.Add(new Logic());
            logicList.Add(new Logic());
            logicList.Add(new Logic());
            logicList.Add(new Logic());
            logicList.Add(new Logic());
            logicList.Add(new Logic());

            bool firstStarter = logicList[0].GetCurrentPlayer();
            bool different = false;

            for (int counter = 1; counter < logicList.Count; counter++)
            {
                if (logicList[counter].GetCurrentPlayer() != firstStarter)
                {
                    different = true;
                    break;
                }
            }

            Assert.IsTrue(different);
        }
    }
}
