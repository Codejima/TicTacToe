using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIConsole
{
    internal class Label
    {
        protected readonly int mPosX;
        protected readonly int mPosY;
        protected readonly string mText;

        public Label(int X, int Y, string Text)
        {
            mPosX = X;
            mPosY = Y;
            mText = Text;
        }
        public virtual void Draw()
        {
            Console.SetCursorPosition(mPosX, mPosY);
            Console.ResetColor();
            Console.WriteLine(mText);
        }
    }
}
