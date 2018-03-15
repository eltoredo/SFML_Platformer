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

        Clock _clock = new Clock();

        static Texture _backgroundTexture = new Texture("../Content/Bbackground.jpg");
        static Sprite _backgroundSprite;

        static Texture _mouseTexture = new Texture("../Content/mousesad.png");
        static Sprite _mouseSprite;

        static Texture _playerTexture = new Texture("../Content/playersprite.png");
        static Sprite _playerSprite;
        int _animFrames;
        int _direction;
        int _animStop;
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
            _backgroundTexture.Repeated = true;
            _backgroundSprite = new Sprite(_backgroundTexture);

            _mouseSprite = new Sprite(_mouseTexture);

            _playerSprite = new Sprite(_playerTexture);
            //_playerSprite.TextureRect = new IntRect(0, 0, 64, 96);
            _playerSprite.Position = new Vector2f(640, 580);
            _speed = 400f;

            this.WindowClearColor = windowClearColor;
            this.Window = new RenderWindow(new VideoMode(windowWidth, windowHeight), windowTitle);
            this.GameTime = new GameTime();

            Window.Closed += WindowClosed;
            Window.SetFramerateLimit(60);

            Window.KeyPressed += WindowEscaping;
            //Window.KeyPressed += WindowPlayerMoved;

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
            _animStop = 64;

            float totalTimeBeforeUpdate = 0f;
            float previousTimeElapsed;//= 0f;
            previousTimeElapsed = _clock.ElapsedTime.AsSeconds();
            deltaTime = 0f;
            float totalTimeElapsed = 0f;

            while (Window.IsOpen)
            {
                Window.DispatchEvents();

                totalTimeElapsed = _clock.ElapsedTime.AsSeconds();
                deltaTime = totalTimeElapsed - previousTimeElapsed;
                previousTimeElapsed = totalTimeElapsed;

                totalTimeBeforeUpdate += deltaTime;

                Move();

                if (totalTimeBeforeUpdate >= TIME_UNTIL_UPDATE)
                {
                    GameTime.Update(totalTimeBeforeUpdate, totalTimeElapsed);
                    totalTimeBeforeUpdate = 0f;

                    Update(GameTime);

                    Window.Clear(WindowClearColor);

                    _backgroundSprite.Draw(Window, RenderStates.Default);

                    _mouseSprite.Draw(Window, RenderStates.Default);
                    
                    if (_animFrames == 4) _animFrames = 0;
                    _playerSprite.TextureRect = new IntRect(_animFrames * _animStop, _direction, 64, 96);
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

        private void Move()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                _direction = 96;
                _animStop = 64;
                _playerSprite.Position += new Vector2f(-_speed * deltaTime, 0f);
            }

            else if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                _direction = 192;
                _animStop = 64;
                _playerSprite.Position += new Vector2f(_speed * deltaTime, 0f);
            }
            else _animStop = 0;

            if (_clock.ElapsedTime.AsSeconds() == 5 && (Keyboard.IsKeyPressed(Keyboard.Key.Q) && Keyboard.IsKeyPressed(Keyboard.Key.Space)))
            {
                _playerSprite.Position -= new Vector2f(200, 0f);
                _clock.Restart();
            }
            else if (_clock.ElapsedTime.AsSeconds() == 5 && (Keyboard.IsKeyPressed(Keyboard.Key.D) && Keyboard.IsKeyPressed(Keyboard.Key.Space)))
            {
                _playerSprite.Position += new Vector2f(200, 0f);
                _clock.Restart();
            }

            if (_playerSprite.Position.X >= 960) _backgroundSprite.TextureRect = new IntRect(new Vector2i((int)_playerSprite.Position.X % (int)_backgroundSprite.Texture.Size.X, 0), new Vector2i(1280, 720));
            else if (_playerSprite.Position.X < 320) _backgroundSprite.TextureRect = new IntRect(new Vector2i((int)_playerSprite.Position.X % (int)_backgroundSprite.Texture.Size.X, 0), new Vector2i(1280, 720));

            if (_playerSprite.Position.X < 0) _playerSprite.Position = new Vector2f(0, 580);
            else if (_playerSprite.Position.X > 1220) _playerSprite.Position = new Vector2f(1220, 580);
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            Window.Close();
        }

        private void WindowEscaping(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape) Window.Close();
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
        
    }
}
