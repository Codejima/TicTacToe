using System;
using System.IO;

namespace UIConsole
{
    class Text : Label
    {
        readonly string[] mLines;
        public Text(int X, int Y, string FileName) : base(X, Y, FileName)
        {
            mLines = File.ReadAllLines(FileName);
        }

        public override void Draw()
        {
            Console.ResetColor();
            for (int i = 0; i < mLines.Length; i++)
            {
                Console.SetCursorPosition(mPosX, mPosY + i);
                Console.Write(mLines[i]);
            }
        }
    }
}
