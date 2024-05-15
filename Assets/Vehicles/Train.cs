using System;

namespace Vehicles
{
    public class Train : Vehicle
    {
        public void Start()
        {
            Init();
            
            Speed = .07f * Random.Next(1, 2) + .03f * Math.Min(GameState.totalScore + GameState.currentScore, 300) / 300;
        }
    }
}
