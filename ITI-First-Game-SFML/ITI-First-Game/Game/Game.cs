using System;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace ITI_First_Game
{
    public class Game : GameLoop
    {
        public const uint DEFAULT_WINDOW_WIDTH = 1280;
        public const uint DEFAULT_WINDOW_HEIGHT = 960;

        public const string WINDOW_TITLE = "My new game";

        Tilemap _map;
        Texture _tileset;

        public Game() : base (DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT, WINDOW_TITLE, Color.Black)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            Window.Draw(_map);

            DebugUtility.DrawPerformanceData(this, Color.White);
        }

        public override void Initialize()
        {
            _map = new Tilemap(_tileset, 1, 1, 16.0f, 32.0f);
        }

        public override void LoadContent()
        {
            _tileset = new Texture("../Content/basictiles.png");

            DebugUtility.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
