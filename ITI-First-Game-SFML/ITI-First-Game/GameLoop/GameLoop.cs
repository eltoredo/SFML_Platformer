using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace ITI_First_Game
{
    public abstract class GameLoop
    {
        #region Fields

        public const int TARGET_FPS = 60;
        public const float TIME_UNTIL_UPDATE = 1f / TARGET_FPS;

        static Texture _mouseTexture = new Texture("../Content/mousesad.png");
        static Sprite _mouseSprite;

        static Texture _playerTexture = new Texture("../Content/playersprite.png");
        static Sprite _playerSprite;
        int _animFrames;
        int _direction;
        float _speed;
        float deltaTime;

        #endregion

        #region Properties

        public RenderWindow Window
        {
            get;
            protected set;
        }

        public GameTime GameTime
        {
            get;
            protected set;
        }

        public Color WindowClearColor
        {
            get;
            protected set;
        }

        #endregion

        protected GameLoop(uint windowWidth, uint windowHeight, string windowTitle, Color windowClearColor)
        {
            _mouseSprite = new Sprite(_mouseTexture);

            _playerSprite = new Sprite(_playerTexture);
            //_playerSprite.TextureRect = new IntRect(0, 0, 64, 96);
            _speed = 1000f;

            this.WindowClearColor = windowClearColor;
            this.Window = new RenderWindow(new VideoMode(windowWidth, windowHeight), windowTitle);
            this.GameTime = new GameTime();

            Window.Closed += WindowClosed;
            Window.SetFramerateLimit(60);

            Window.KeyPressed += WindowTitleChangingOrEscaping;
            Window.KeyPressed += WindowPlayerMoved;

            Window.MouseMoved += MouseMoved;
            Window.MouseLeft += MouseLeft;
            Window.MouseEntered += MouseEntered;
            Window.MouseButtonReleased += WindowMouseButtonReleased;
        }

        public void Run() // Main method of the gameloop
        {
            LoadContent();
            Initialize();

            _animFrames = 0;
            _direction = 192;

            Clock clock = new Clock(); // Automatically starts to elapse time

            float totalTimeBeforeUpdate = 0f;
            float previousTimeElapsed;//= 0f;
            previousTimeElapsed = clock.ElapsedTime.AsSeconds();
            deltaTime = 0f;
            float totalTimeElapsed = 0f;

            while (Window.IsOpen)
            {
                Window.DispatchEvents();

                totalTimeElapsed = clock.ElapsedTime.AsSeconds();
                deltaTime = totalTimeElapsed - previousTimeElapsed;
                previousTimeElapsed = totalTimeElapsed;

                totalTimeBeforeUpdate += deltaTime;

                if (totalTimeBeforeUpdate >= TIME_UNTIL_UPDATE)
                {
                    GameTime.Update(totalTimeBeforeUpdate, /*clock.ElapsedTime.AsSeconds()*/ totalTimeElapsed);
                    totalTimeBeforeUpdate = 0f;

                    Update(GameTime);

                    Window.Clear(WindowClearColor);

                    _mouseSprite.Draw(Window, RenderStates.Default);

                    if (_animFrames == 4) _animFrames = 0;
                    _playerSprite.TextureRect = new IntRect(_animFrames * 64, _direction, 64, 96);
                    ++_animFrames;
                    _playerSprite.Draw(Window, RenderStates.Default);

                    Draw(GameTime);
                    Window.Display();
                }
            }
        }

        public abstract void LoadContent();
        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);

        private void WindowClosed(object sender, EventArgs e)
        {
            Window.Close();
        }

        private void MouseLeft(object sender, EventArgs e)
        {
            Window.SetMouseCursorVisible(true);
        }

        private void MouseEntered(object sender, EventArgs e)
        {
            Window.SetMouseCursorVisible(false);
        }

        private void MouseMoved(object sender, MouseMoveEventArgs e)
        {
            _mouseSprite.Position = new Vector2f(e.X, e.Y);
        }

        private void WindowMouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            //if (e.Button == Mouse.Button.Left) _playerSprite.Position = new Vector2f(e.X, e.Y);
        }

        private void WindowPlayerMoved(object sender, KeyEventArgs e)
        {
            Console.WriteLine(deltaTime);

            if (e.Code == Keyboard.Key.Q)
            {
                _direction = 96;
                _playerSprite.Position += new Vector2f(-_speed * deltaTime, 0f);
            }
            if (e.Code == Keyboard.Key.D)
            {
                _direction = 192;
                _playerSprite.Position += new Vector2f(_speed * deltaTime, 0f);
                Console.WriteLine(_playerSprite.Position);
            }
        }

        private void WindowTitleChangingOrEscaping(object sender, KeyEventArgs e)
        {
            //if (e.Code == Keyboard.Key.Space) _mouseSprite.Scale = new Vector2f(6, 6); // Window.SetTitle("Coucou !");
            if (e.Code == Keyboard.Key.Escape) Window.Close();
        }
    }
}
