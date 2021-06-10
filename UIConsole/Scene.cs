using System.Collections.Generic;

namespace UIConsole
{
    abstract class Scene
    {
        protected List<Label> mLabelList;
        protected List<Button> mButtonList;
        protected byte mActiveButtonID;

        public Scene()
        {
            mLabelList = new();
            mButtonList = new();
        }


        public abstract void Update();

        public virtual void Draw()
        {
            foreach (var item in mLabelList)
            {
                item.Draw();
            }
            foreach (var item in mButtonList)
            {
                item.Draw();
            }
        }
    }
}
