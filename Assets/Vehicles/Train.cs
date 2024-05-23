using System;

namespace Vehicles
{
    public class Train : Vehicle
    {
        public void Start()
        {
            Init();

            Speed = .09f * Random.Next(1, 2) +
                    .045f * Math.Min(GameState.totalScore + GameState.currentScore, 300) / 300;
        }
    }
}