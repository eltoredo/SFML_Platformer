using System;
using SFML.Graphics;
using SFML.System;

namespace ITI_First_Game
{
    public static class DebugUtility
    {
        public const string CONSOLE_FONT_PATH = "./fonts/arial.ttf";

        public static Font consoleFont;

        public static void LoadContent()
        {
            consoleFont = new Font(CONSOLE_FONT_PATH);
        }

        public static void DrawPerformanceData(GameLoop gameLoop, Color fontColor)
        {
            if (consoleFont == null) return;

            string totalTimeElapsedStr = gameLoop.GameTime.TotalTimeElapsed.ToString("0.0000");
            string deltaTimeStr = gameLoop.GameTime.DeltaTime.ToString("0.00000");
            float fps = 1f / gameLoop.GameTime.DeltaTime;
            string fpsStr = fps.ToString("0.00");

            Text text = new Text(totalTimeElapsedStr, consoleFont, 14);
            text.Position = new Vector2f(4f, 8f);
            text.Color = fontColor;

            Text text2 = new Text(deltaTimeStr, consoleFont, 14);
            text2.Position = new Vector2f(4f, 28f);
            text2.Color = fontColor;

            Text text3 = new Text(fpsStr, consoleFont, 14);
            text3.Position = new Vector2f(4f, 48f);
            text3.Color = fontColor;

            gameLoop.Window.Draw(text);
            gameLoop.Window.Draw(text2);
            gameLoop.Window.Draw(text3);
        }
    }
}
