using GameLogic;
using System;

namespace UIConsole
{
    internal class GameEndScreen : Scene
    {
        private readonly Logic logic;
        private readonly TurnResult turnResult;

        public GameEndScreen(Logic logic, GameScreen gameScreen)
        {
            //button
            //label
            mLabelList.Add(new Label(20, 5, "Game Over"));
            mLabelList.Add(new Label(20, 7, (logic.GetCurrentPlayer() ? "Player O WON!" : "Player X WON!")));
            mButtonList.Add(new Button(20, 9, "Play Again", () => {  Console.Clear(); logic.ResetGame(); SceneManager.Instance.RemoveScene(this); }));
            mButtonList.Add(new Button(20, 11, "Main Menu", () => { Console.ResetColor(); Console.Clear(); SceneManager.Instance.RemoveScene(this); SceneManager.Instance.RemoveScene(gameScreen); }));
            mButtonList[mActiveButtonID].IsSelected = true;
        }

        public override void Update()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:
                    mButtonList[mActiveButtonID].IsSelected = false;
                    mActiveButtonID = (byte)(mActiveButtonID == 0 ? mButtonList.Count - 1 : mActiveButtonID - 1);
                    mButtonList[mActiveButtonID].IsSelected = true;
                    break;
                case ConsoleKey.DownArrow:
                    mButtonList[mActiveButtonID].IsSelected = false;
                    mActiveButtonID = (byte)(mActiveButtonID == mButtonList.Count - 1 ? 0 : mActiveButtonID + 1);
                    mButtonList[mActiveButtonID].IsSelected = true;
                    break;
                case ConsoleKey.Enter:
                    mButtonList[mActiveButtonID].Execute();
                    break;
            }
        }

        public override void Draw()
        {
            base.Draw();
            //endboard + winner
            //buttons to reset/return to menu
        }

        //TODO: implement here
    }
}
