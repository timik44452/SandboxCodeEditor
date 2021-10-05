using SandboxCodeEditor.Math;
using System;

namespace SandboxCodeEditor.Editor
{
    public enum Mode
    {
        Text,
        Command
    }

    public class EditorController
    {
        public event EventHandler<Mode> OnModeChanged;

        public event EventHandler<Point> OnCursorMoved;

        public Mode EditorMode
        {
            get => editorMode;
            set
            {
                editorMode = value;
                OnModeChanged?.Invoke(this, value);
            }
        }

        public SandboxFont Font { get; }
        public EditorSettings Settings { get; }

        public Point CursorePosition
        {
            get => cursorPosition;
            private set
            {
                cursorPosition = value;
                OnCursorMoved?.Invoke(this, value);
            }
        }

        private int width;
        private int height;
        private int glyphWidth;
        private int glyphHeight;
        private int screenCharsWidth;
        private int screenCharsHeight;
        private Point cursorPosition;
        private Mode editorMode;

        public EditorController(SandboxFont font)
        {
            EditorMode = Mode.Text;
            Font = font;
            Settings = new EditorSettings();
        }

        public void Setup(int width, int height)
        {
            this.width = width;
            this.height = height;

            glyphWidth = MathUtilities.Max(Font.Size + Settings.SpaceBetweenGlyps, 1);
            glyphHeight = MathUtilities.Max(Font.Size + Settings.SpaceBetweenLines, 1);
            screenCharsWidth = width / GetGlyphWidth();
            screenCharsHeight = height / GetGlyphHeight();
        }

        public void Enter()
        {
            MoveCursore(0, 1);
            StartPage();
        }

        public void Space()
        {
            MoveCursore(1, 0);
        }

        public void StartPage()
        {
            CursorePosition = new Point(0, CursorePosition.Y);
        }

        public void MoveCursore(int characterDelta, int lineDelta)
        {
            int x = cursorPosition.X + characterDelta;
            int y = cursorPosition.Y + lineDelta;

            if (x > GetScreenCharsWidth())
            {
                y++;
                x = 0;
            }
            else if (x < 0)
            {
                y--;
                x = 0;
            }

            if(y < 0)
            {
                y = 0;
            }

            CursorePosition = new Point(x, y);
        }

        public int GetGlyphWidth()
        {
            return glyphWidth;
        }

        public int GetGlyphHeight()
        {
            return glyphHeight;
        }

        public int GetScreenCharsWidth()
        {
            return screenCharsWidth;
        }

        public int GetScreenCharsHeight()
        {
            return screenCharsHeight;
        }

        public Point TextToScreenPosition(Point point)
        {
            return TextToScreenPosition(point.Y, point.X);
        }

        public Point TextToScreenPosition(int line, int charPosition)
        {
            int x = Settings.Borders + charPosition * GetGlyphWidth();
            int y = Settings.Borders + line * GetGlyphHeight();

            return new Point(x, y);
        }
    }
}