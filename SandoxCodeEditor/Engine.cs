using SandboxCodeEditor.Editor;
using SandboxCodeEditor.Math;
using SandboxCodeEditor.Particles;
using SandboxCodeEditor.Widgets;
using SandboxCodeEditor.World;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SandboxCodeEditor
{
    public partial class Engine : Form
    {
        private int type = 0;
        private Dictionary<Point, char> text = new Dictionary<Point, char>();

        private GridWorld world;
        private EditorController controller;
        private Timer rendererTimer;
        private RendererContext context;
        private List<Widget> widgets;

        private DateTime now;

        private bool isInitialized = false;

        public Engine()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            var font = new SandboxFont("Winston");

            now = DateTime.Now;
            world = new GridWorld(Width, Height);
            context = new RendererContext(Width, Height);
            controller = new EditorController(font);
            widgets = new List<Widget>();
            context.ResetBuffers();
            rendererBox.InitGDI(Width, Height);
            controller.Setup(Width, Height);

            rendererTimer = new Timer();
            rendererTimer.Interval = 1;
            rendererTimer.Tick += OnUpdate;
            rendererTimer.Start();

            widgets.Add(new ModeWidget(controller.Settings.Borders, Height - controller.GetGlyphHeight() - controller.Settings.Borders - 39, controller.GetGlyphWidth() * 3, controller.GetGlyphHeight(), controller));
            widgets.Add(new CursoreWidget(controller));
            //widgets.Add(new CodeLineWidget(Height, controller));

            isInitialized = true;
        }


        private void OnUpdate(object sender, EventArgs e)
        {
            float deltaTime = MathUtilities.Max((float)(DateTime.Now - now).TotalMilliseconds, 0.02F);
            now = DateTime.Now;

            context.ResetBuffers(controller.Settings.BackgroundColor.ToRGB());

            foreach(var keyPair in text)
            {
                DrawGlyph(keyPair.Key, controller.Font.GetGlyph(keyPair.Value));
            }

            for (int i = 0; i < world.Particles.Count; i++)
            {
                Particle particle = world.Particles[i];

                if (particle == null)
                    continue;

                int x = (int)(particle.Position.X * controller.Settings.PixelSize);
                int y = (int)(particle.Position.Y * controller.Settings.PixelSize);

                for (int _x = x; _x < x + controller.Settings.PixelSize; _x++)
                    for (int _y = y; _y < y + controller.Settings.PixelSize; _y++)
                    {
                        if (context.Rect.Contains(_x, _y))
                        {
                            context.Buffer[_x + _y * context.Rect.Width] = particle.Color.ToRGB();
                        }
                    }
            }

            foreach (var widget in widgets)
            {
                widget.Update();

                if (widget.IsDrawing)
                {
                    widget.Draw(context);
                }
            }

            rendererBox.Draw(context);
            world.Simulate(deltaTime);
            WorldEnviropment.Simulate(deltaTime);

            Text = $"FPS:{1000 / deltaTime}";
        }

        private void Engine_Resize(object sender, EventArgs e)
        {
            if (isInitialized)
            {
                context = new RendererContext(Width, Height);
                context.ResetBuffers(controller.Settings.BackgroundColor.ToRGB());
                rendererBox.InitGDI(Width, Height);
                controller.Setup(Width, Height);
            }
        }

        private void rendererBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (controller.EditorMode == Mode.Text)
            {
                Write(e.KeyChar);
            }
        }

        private void CommandModeInputHandler(Keys key)
        {
            switch (key)
            {
                case Keys.T: controller.EditorMode = Mode.Text; break;
                case Keys.V: Paste(); break;
            }
        }

        private void Paste()
        {
            var text = Clipboard.GetText().ToUpper();
            var lines = text.Split(new[] { '\r', '\n' });

            for (int line = 0; line < lines.Length; line++)
            {
                for (int charPosition = 0; charPosition < lines[line].Length; charPosition++)
                {
                    var currentChar = lines[line][charPosition];
                    var currentPoint = new Point(charPosition, line) + controller.CursorePosition;

                    Write(currentChar, currentPoint);
                }
            }
        }

        private void TextModeInputHandler(Keys key)
        {
            switch (key)
            {
                case Keys.Escape: controller.EditorMode = Mode.Command; break;
                case Keys.Space: controller.Space(); break;
                case Keys.Enter: controller.Enter(); break;
                case Keys.Back: Backspace(); break;
                case Keys.Delete: Delete(); break;
            }
        }

        private void Backspace()
        {
            var prevCurPoint = new Point(controller.CursorePosition.X - 1, controller.CursorePosition.Y);
            var prevScrPoint = controller.TextToScreenPosition(prevCurPoint);

            text.Remove(prevCurPoint);
            controller.MoveCursore(-1, 0);

            for (int y = 0; y < controller.GetGlyphHeight(); y++)
                for (int x = 0; x < controller.GetGlyphWidth(); x++)
                {
                    BombParticle bombParticle = new BombParticle(prevScrPoint.X + x, prevScrPoint.Y + y, 1F, 0.1F);
                    world.InstantiateParticle(bombParticle);
                }
        }

        private void Delete()
        {
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up: controller.MoveCursore(0, -1); break;
                case Keys.Down: controller.MoveCursore(0, 1); break;
                case Keys.Left: controller.MoveCursore(-1, 0); break;
                case Keys.Right: controller.MoveCursore(1, 0); break;
            }

            if (controller.EditorMode == Mode.Command)
            {
                CommandModeInputHandler(keyData);
            }
            else
            {
                TextModeInputHandler(keyData);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Write(char value)
        {
            Write(value, controller.CursorePosition);
        }

        private void Write(char value, Point point)
        {
            if (!text.ContainsKey(point))
            {
                type = (int)(4 * MathUtilities.RandomValue());
                var glyph = controller.Font.GetGlyph(value);

                if (glyph != null)
                {
                    text.Add(point, value);
                    //CreateGlyph(glyph);

                    controller.Space();
                }
            }
        }

        private void CreateGlyph(SandboxGlyph glyph)
        {
            if (glyph == null)
            {
                return;
            }

            var position = controller.TextToScreenPosition(controller.CursorePosition);

            for (int y = 0; y < glyph.Height; y++)
            {
                for (int x = 0; x < glyph.Width; x++)
                {
                    if (glyph.Data[x, y] > 0)
                    {
                        var particle = CreateParticle(position.X + x, position.Y + y);
                        world.InstantiateParticle(particle);
                    }
                }
            }
        }

        private void DrawGlyph(Point point, SandboxGlyph glyph)
        {
            if (glyph == null)
            {
                return;
            }

            var position = point;// controller.TextToScreenPosition(point);

            for (int y = 0; y < glyph.Height; y++)
            {
                for (int x = 0; x < glyph.Width; x++)
                {
                    if (glyph.Data[x, y] > 0)
                    {
                        Point _point = new Point(position.X + x, position.Y + y) * controller.Settings.PixelSize;

                        if (context.Rect.Contains(_point.X, _point.Y))
                        {
                            context.Buffer[_point.X + _point.Y * context.Rect.Width] = controller.Settings.PhantomColor.ToRGB();
                        }
                    }
                }
            }
        }

        private Particle CreateParticle(int x, int y)
        {
            int index = type % 4;

            switch (index)
            {
                case 0: return new RockParticle(x, y);
                case 1: return new SandParticle(x, y);
                case 2: return new IronParticle(x, y);
                case 3: return new BombParticle(x, y);
            }

            return null;
        }
    }
}