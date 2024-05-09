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
    
    public enum TerrainTypes
    {
        GRASS,
        ROAD,
        RIVER,
        RAILROAD
    }
    
    public class TerrainGen : MonoBehaviour
    {
        private readonly Random _random = new Random();
        
        private int _terrainZ;
        private int _terrainStreak;
        private TerrainTypes _lastTerrainType;

        private GameObject[] _cars;

        public GameObject player;
        
        public GameObject grass;
        public GameObject road;
        public GameObject river;
        public GameObject railroad;
        
        public GameObject yellowCar;
        public GameObject greenCar;
        public GameObject blueCar;

        public void Start()
        {
            _terrainStreak = 1;
            _lastTerrainType = TerrainTypes.GRASS;

            
            for (_terrainZ = -30; _terrainZ < 50; _terrainZ++)
            {
                InstantiateLand();
            }
        }

        public void Update()
        {
            if (_terrainZ - player.transform.position.z < 40)
            {
                _terrainZ++;
                InstantiateLand();
            }

            _cars = GameObject.FindGameObjectsWithTag("Car");

            foreach (GameObject car in _cars)
            {
                if (Math.Abs(car.transform.position.x - transform.position.x) > 30)
                {
                    InstantiateCar((int) car.transform.position.z);
                    Destroy(car);
                }
            }
        }

        private void InstantiateLand()
        {
            switch (GetTerrainType())
            {
                case TerrainTypes.GRASS:
                    Instantiate(grass, new Vector3(0, 0, _terrainZ), Quaternion.Euler(0, 0, 0));
                    break;
                case TerrainTypes.ROAD:
                    Instantiate(road, new Vector3(0, 0, _terrainZ), Quaternion.Euler(0, 0, 0));
                    InstantiateCar();
                    break;
                case TerrainTypes.RIVER:
                    Instantiate(river, new Vector3(0, 0, _terrainZ), Quaternion.Euler(0, 0, 0));
                    break;
                case TerrainTypes.RAILROAD:
                    Instantiate(railroad, new Vector3(0, 0, _terrainZ), Quaternion.Euler(0, 0, 0));
                    break;
            }
        }
        
        private void InstantiateCar()
        {
            InstantiateCar(_terrainZ);
        }

        private void InstantiateCar(int carZ)
        {
            switch ((CarTypes) _random.Next(Enum.GetNames(typeof(CarTypes)).Length))
            {
                case CarTypes.YELLOW:
                    Instantiate(yellowCar, new Vector3(transform.position.x - _random.Next(10, 40), 2f, carZ),
                        Quaternion.Euler(0, 0, 0));
                    break;
                case CarTypes.GREEN:
                    Instantiate(greenCar, new Vector3(transform.position.x - _random.Next(10, 40), 2f, carZ),
                        Quaternion.Euler(0, 0, 0));
                    break;
                case CarTypes.BLUE:
                    Instantiate(blueCar, new Vector3(transform.position.x - _random.Next(10, 40), 2f, carZ),
                        Quaternion.Euler(0, 0, 0));
                    break;
            }
        }
        
        private TerrainTypes GetTerrainType()
        {
            if (_random.Next(3) == 2)
            {
                _lastTerrainType = (TerrainTypes) _random.Next(Enum.GetNames(typeof(TerrainTypes)).Length);
                _terrainStreak = 1;
            }
            else
            {
                _terrainStreak++;
            }
            
            switch (_lastTerrainType)
            {
                case TerrainTypes.GRASS:
                    if (_terrainStreak > 5)
                    {
                        return GetTerrainType();
                    }
                    
                    break;
                case TerrainTypes.ROAD:
                    if (_terrainStreak > 8)
                    {
                        return GetTerrainType();
                    }
                    
                    break;
                case TerrainTypes.RIVER:
                    if (_terrainStreak > 3)
                    {
                        return GetTerrainType();
                    }
                    
                    break;
                case TerrainTypes.RAILROAD:
                    if (_terrainStreak > 3)
                    {
                        return GetTerrainType();
                    }

                    break;
            }
            
            return _lastTerrainType;
        }
    }
}
