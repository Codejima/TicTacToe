using System;

namespace UIConsole
{
    internal class Button : Label
    {
        private readonly Action mCommand;
        private const ConsoleColor mColorNotSelected = ConsoleColor.DarkYellow;
        private const ConsoleColor mColorSelected = ConsoleColor.Magenta;

        public bool IsSelected { get; internal set; }

        public Button(int X, int Y, string Text, Action btnStartGame) : base(X, Y, Text)
        {
            mCommand = btnStartGame;
        }
        public void Execute()
        {
            mCommand();
        }
        public override void Draw()
        {
            Console.SetCursorPosition(mPosX, mPosY);
            Console.BackgroundColor = IsSelected ? mColorSelected : mColorNotSelected;
            Console.WriteLine(mText);
        }
    }
}
