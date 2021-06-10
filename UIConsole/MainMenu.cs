using System;

namespace UIConsole
{
    class MainMenu : Scene
    {
        public MainMenu()
        {
            // Labels & Buttons
            mLabelList.Add(new Text(10, 1, "logo.txt"));
            mLabelList.Add(new Label(24,10,"TicTacToe"));
            mButtonList.Add(new Button(22,13,"Start New Game", () => SceneManager.Instance.AddScene(new GameScreen())));
            mButtonList.Add(new Button(25, 15, "Credits", () => SceneManager.Instance.AddScene(new Credits())));
            mButtonList.Add(new Button(27, 17, "Quit", () => SceneManager.Instance.RemoveScene(this)));

            mButtonList[mActiveButtonID].IsSelected = true;
        }
        public override void Draw()
        {
            base.Draw();
            // Console.SetCursorPosition(0,0);
            // Console.Write(SceneManager.Instance.FPS); TODO: implement FPS
        }
        // Navigation
        public override void Update()
        {
            //TODO: move this to Scene
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
    }
}
