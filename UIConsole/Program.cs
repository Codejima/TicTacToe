using System;

namespace UIConsole
{
    class Program
    {
        static void Main()
        {
            Console.CursorVisible = false;
            SceneManager.Instance.AddScene(new MainMenu());
            
            while (SceneManager.Instance.GetSceneCounter > 0)
            {
                SceneManager.Instance.Draw();
                SceneManager.Instance.Update();
            }
        }
    }
}
