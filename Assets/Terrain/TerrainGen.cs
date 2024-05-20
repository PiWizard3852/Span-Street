using System;
using Player;
using UnityEngine;
using Vehicles;
using Random = System.Random;

namespace Terrain
{
    public enum TerrainTypes
    {
        Grass,
        Road,
        River,
        Railroad
    }

    public enum CarTypes
    {
        Yellow,
        Green,
        Blue
    }

    public enum LogTypes
    {
        One,
        Two,
        Three
    }

    public class TerrainGen : MonoBehaviour
    {
        private readonly Random _random = new();

        private int _terrainZ;
        private int _terrainStreak;
        private TerrainTypes _lastTerrainType;

        private GameObject[] _cars;
        private GameObject[] _logs;
        private GameObject[] _trains;

        public GameObject player;
        
        public GameObject grass;
        public GameObject road;
        public GameObject river;
        public GameObject railroad;

        public GameObject coin;

        public GameObject yellowCar;
        public GameObject greenCar;
        public GameObject blueCar;

        public GameObject log1;
        public GameObject log2;
        public GameObject log3;
        
        public GameObject train;
        
        public void Start()
        {
            _terrainStreak = 1;
            _lastTerrainType = TerrainTypes.Grass;
            
            for (_terrainZ = -10; _terrainZ < 70; _terrainZ++) InstantiateLand();
        }

        public void Update()
        {
            if (_terrainZ - player.transform.position.z < 40)
            {
                _terrainZ++;
                InstantiateLand();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _cars = GameObject.FindGameObjectsWithTag("Car");

                foreach (var car in _cars)
                {
                    if (player.transform.position.z - car.transform.position.z > 15)
                    {
                        Destroy(car);
                    } else if (car.GetComponent<Car>().invisible)
                    {
                        InstantiateCar(1, (int)car.transform.position.z);
                        Destroy(car);
                    }
                }

                _logs = GameObject.FindGameObjectsWithTag("Log");

                foreach (var log in _logs)
                {
                    if (player.transform.position.z - log.transform.position.z > 15)
                    {
                        Destroy(log);
                    } else if (log.GetComponent<Log>().invisible)
                    {
                        InstantiateLog(1, (int)log.transform.position.z);
                        Destroy(log);
                    }
                }

                _trains = GameObject.FindGameObjectsWithTag("Train");

                foreach (var train in _trains)
                {
                    if (player.transform.position.z - train.transform.position.z > 15)
                    {
                        Destroy(train);
                    } else if (train.GetComponent<Train>().invisible)
                    {
                        InstantiateTrain((int)train.transform.position.z);
                        Destroy(train);
                    }
                }
            }
        }

        private void InstantiateLand()
        {
            switch (GetTerrainType())
            {
                case TerrainTypes.Grass:
                    Instantiate(grass, new Vector3(0, 0, _terrainZ), Quaternion.Euler(0, 0, 0));
                    
                    if (_random.Next(5) > 1)
                    {
                        InstantiateCoin(_terrainZ);
                    }
                    
                    break;
                case TerrainTypes.Road:
                    Instantiate(road, new Vector3(0, 0, _terrainZ), Quaternion.Euler(0, 0, 90));
                    
                    InstantiateCar(1);
                    
                    if (_random.Next(5) == 4)
                    {
                        InstantiateCar(2);
                    }
                    
                    break;
                case TerrainTypes.River:
                    Instantiate(river, new Vector3(0, 0, _terrainZ), Quaternion.Euler(0, 0, 0));
                    
                    InstantiateLog(1);
                    InstantiateLog(2);
                    
                    break;
                case TerrainTypes.Railroad:
                    Instantiate(railroad, new Vector3(0, 0, _terrainZ), Quaternion.Euler(0, 0, 90));
                    
                    InstantiateTrain();
                    
                    break;
            }
        }

        private void InstantiateCoin(int coinZ)
        {
            Instantiate(coin, new Vector3(_random.Next(-12, 12), 1, coinZ),
                Quaternion.Euler(90, 0, 0));
        }

        private void InstantiateCar(int cardinal)
        {
            InstantiateCar(cardinal, _terrainZ);
        }

        private void InstantiateCar(int cardinal, int carZ)
        {
            var car = yellowCar;
            
            switch ((CarTypes)_random.Next(Enum.GetNames(typeof(CarTypes)).Length))
            {
                case CarTypes.Yellow:
                    car = yellowCar;
                    break;
                case CarTypes.Green:
                    car = greenCar;
                    break;
                case CarTypes.Blue:
                    car = blueCar;
                    break;
            }
            
            Instantiate(car, new Vector3(transform.position.x - _random.Next(15, 25) - _random.Next(10, 20) * (cardinal - 1), 1.8f, carZ),
                Quaternion.Euler(0, 0, 0));
        }
        
        private void InstantiateLog(int cardinal)
        {
            InstantiateLog(cardinal, _terrainZ);
        }

        private void InstantiateLog(int cardinal, int logZ)
        {
            var log = log1;
            
            switch ((LogTypes)_random.Next(Enum.GetNames(typeof(LogTypes)).Length))
            {
                case LogTypes.One:
                    log = log1;
                    break;
                case LogTypes.Two:
                    log = log2;
                    break;
                case LogTypes.Three:
                    log = log3;
                    break;
            }

            Instantiate(log, new Vector3(transform.position.x - _random.Next(15, 20) - _random.Next(5, 15) * (cardinal - 1), .6f, logZ),
                Quaternion.Euler(0, 0, 0));
        }

        private void InstantiateTrain()
        {
            InstantiateTrain(_terrainZ);
        }

        private void InstantiateTrain(int trainZ)
        {
            Instantiate(train, new Vector3(transform.position.x - _random.Next(10, 40), 1.8f, trainZ),
                        Quaternion.Euler(0, 0, 0));
        }

        private TerrainTypes GetTerrainType()
        {
            if (_terrainZ == 0)
            {
                if (_lastTerrainType == TerrainTypes.Grass)
                {
                    _terrainStreak++;
                }
                else
                {
                    _lastTerrainType = TerrainTypes.Grass;
                    _terrainStreak = 1;
                }

                return _lastTerrainType;
            }

            if (_random.Next(3) == 2)
            {
                _lastTerrainType = (TerrainTypes)_random.Next(Enum.GetNames(typeof(TerrainTypes)).Length);
                _terrainStreak = 1;
            }
            else
            {
                _terrainStreak++;
            }

            switch (_lastTerrainType)
            {
                case TerrainTypes.Grass:
                    if (_terrainStreak > 5) return GetTerrainType();

                    break;
                case TerrainTypes.Road:
                    if (_terrainStreak > 8) return GetTerrainType();

                    break;
                case TerrainTypes.River:
                    if (_terrainStreak > 2) return GetTerrainType();

                    break;
                case TerrainTypes.Railroad:
                    if (_terrainStreak > 2) return GetTerrainType();

                    break;
            }

            return _lastTerrainType;
        }
    }
}