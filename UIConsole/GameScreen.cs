using GameLogic;
using System;

namespace UIConsole
{
    class GameScreen : Scene
    {
        readonly Logic logic = new();
        public override void Draw()
        {
            Console.ResetColor();
            Console.Clear();
            Board[,] board = logic.GetGameBoard();
            // show board
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    switch (board[y,x])
                    {
                        case Board.Empty:
                            Console.Write("|_| ");
                            break;
                        case Board.X:
                            Console.Write(" X  ");
                            break;
                        case Board.O:
                            Console.Write(" O  ");
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n 00 10  20");
            Console.WriteLine(" 01 11  21");
            Console.WriteLine(" 02 12  22");
            // TODO: implement different colors
        }

        public override void Update()
        {
            // ask current player for coordinates
            Console.WriteLine("\nPlayer " + (logic.GetCurrentPlayer() ? "O" : "X") + " please enter coordinates:");
            // convert string coordinates to int
            string coords = Console.ReadLine();
            int x = coords[0] - 48;
            int y = coords[1] - 48;
            TurnResult turnResult = logic.PlayerTurn(x, y);
            // send converted data to logic
            // evaluate logics return
            switch (turnResult)
            {
                case TurnResult.Valid:
                    Console.WriteLine("Valid move.");
                    break;
                case TurnResult.Invalid:
                    Console.WriteLine("Invalid move.");
                    break;
                // put cases draw, x wins & o wins into one with: cw via SceneManager.Instance.AddScene(new GameEndScreen(turnResult.ToString())); TODO: implement GameEndScreen
                case TurnResult.Draw:
                case TurnResult.WinX:
                case TurnResult.WinO:
                    Console.Clear();
                    SceneManager.Instance.AddScene(new GameEndScreen(logic, this));
                    break;
            }
        }
    }
}
