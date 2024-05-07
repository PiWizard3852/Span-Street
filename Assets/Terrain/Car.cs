using System;
using UnityEngine;
using Random = System.Random;

namespace Terrain
{
    public enum CarTypes
    {
        YELLOW,
        GREEN,
        BLUE
    }
    
    public class Car : MonoBehaviour
    {
        private readonly Random _random = new Random();

        public GameObject yellowCar;
        public GameObject greenCar;
        public GameObject blueCar;

        private int _roadZ;
        
        public void Start()
        {
            switch ((CarTypes) _random.Next(Enum.GetNames(typeof(CarTypes)).Length))
            {
                case CarTypes.YELLOW:
                    Instantiate(yellowCar, new Vector3(0, 0, _roadZ), Quaternion.Euler(0, 0, 0));
                    break;
                case CarTypes.GREEN:
                    Instantiate(greenCar, new Vector3(0, 0, _roadZ), Quaternion.Euler(0, 0, 0));
                    break;
                case CarTypes.BLUE:
                    Instantiate(blueCar, new Vector3(0, 0, _roadZ), Quaternion.Euler(0, 0, 0));
                    break;
            }
        }
    }
}