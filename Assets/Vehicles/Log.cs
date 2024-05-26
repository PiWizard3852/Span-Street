using System;

namespace Vehicles
{
    public class Log : Vehicle
    {
        public void Start()
        {
            Init();

            // Calculate unique speed for logs
            Speed = .03f * Random.Next(1, 2) +
                    .025f * Math.Min(GameState.totalScore + GameState.currentScore, 300) / 300;
        }
    }
}