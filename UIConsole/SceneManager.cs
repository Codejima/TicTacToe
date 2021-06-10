using System.Collections.Generic;

namespace UIConsole
{
    class SceneManager
    {
        private static SceneManager mInstance;
        private readonly LinkedList<Scene> mSceneList;

        public static SceneManager Instance
        {
            get {
                if (mInstance is null)
                    mInstance = new();
                return mInstance; }
        }
        private SceneManager()
        {
            mSceneList = new();
        }

        public void Update() => mSceneList.Last.Value.Update();
        public void Draw() => mSceneList.Last.Value.Draw();
        

        public int GetSceneCounter
        {
            get
            {
                return mSceneList.Count;
            }
           
        }

        public void AddScene(Scene SceneToAdd)
        {
            mSceneList.AddLast(SceneToAdd);
        }
        public void RemoveScene(Scene SceneToRemove)
        {
            mSceneList.Remove(SceneToRemove);
        }
    }
}
