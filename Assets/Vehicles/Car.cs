using System;

namespace Vehicles
{
    public class Car : Vehicle
    {
        public void Start()
        {
            Init();
            
            Speed = .02f * Random.Next(1, 2) + .02f * Math.Min(GameState.totalScore + GameState.currentScore, 300) / 300;
        }
    }
}
