using System;

namespace ITI_First_Game
{
    public class GameTime
    {
        #region Fields

        private float _deltaTime = 0f;
        private float _timeScale = 1f;

        #endregion

        #region Properties

        public float TimeScale // It scales the Delta Time to simulate a slowing down or speeding up time
        {
            get { return _timeScale; }
            set { _timeScale = value; }
        }

        public float DeltaTime // Time elapsed between the previous frame and the current frame (to make the game framerate independent)
        {
            get { return _deltaTime * _timeScale; }
            private set { _deltaTime = value; }
        }

        public float DeltaTimeUnscaled
        {
            get { return _deltaTime; }
        }

        public float TotalTimeElapsed // Total time elapsed since the beginning of the game
        {
            get;
            private set;
        }

        #endregion

        public GameTime()
        {
        }

        public void Update(float deltaTime, float totalTimeElapsed)
        {
            _deltaTime = deltaTime;
            TotalTimeElapsed = totalTimeElapsed;
        }
    }
}
