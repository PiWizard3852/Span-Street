using System;
using UnityEngine;
using Random = System.Random;
using System.Threading;

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
            
            for (_terrainZ = -10; _terrainZ < 50; _terrainZ++) InstantiateLand();
        }

        public void Update()
        {
            if (_terrainZ - player.transform.position.z < 40)
            {
                _terrainZ++;
                InstantiateLand();
            }

            _cars = GameObject.FindGameObjectsWithTag("Car");

            foreach (var car in _cars)
            {
                if (player.transform.position.z - car.transform.position.z > 15)
                {
                    Destroy(car);
                } else if (Math.Abs(car.transform.position.x - transform.position.x) > 30)
                {
                    InstantiateCar((int)car.transform.position.z);
                    Destroy(car);
                }
            }

            _logs = GameObject.FindGameObjectsWithTag("Log");

            foreach (var log in _logs)
            {
                if (player.transform.position.z - log.transform.position.z > 15)
                {
                    Destroy(log);
                } else if (Math.Abs(log.transform.position.x - transform.position.x) > 15)
                {
                    InstantiateLog((int)log.transform.position.z);
                    Destroy(log);
                }
            }

            _trains = GameObject.FindGameObjectsWithTag("Train");

            foreach (var train in _trains)
            {
                if (player.transform.position.z - train.transform.position.z > 15)
                {
                    Destroy(train);
                } else if (Math.Abs(train.transform.position.x - transform.position.x) > 80 && _random.Next() > 0.75)
                {
                    InstantiateTrain((int)train.transform.position.z);
                    Destroy(train);
                }
            }
        }

        private void InstantiateLand()
        {
            switch (GetTerrainType())
            {
                case TerrainTypes.Grass:
                    Instantiate(grass, new Vector3(0, 0, _terrainZ), Quaternion.Euler(0, 0, 0));
                    break;
                case TerrainTypes.Road:
                    Instantiate(road, new Vector3(0, 0, _terrainZ), Quaternion.Euler(0, 0, 0));
                    InstantiateCar();
                    break;
                case TerrainTypes.River:
                    Instantiate(river, new Vector3(0, 0, _terrainZ), Quaternion.Euler(0, 0, 0));
                    InstantiateLog();
                    break;
                case TerrainTypes.Railroad:
                    Instantiate(railroad, new Vector3(0, 0, _terrainZ), Quaternion.Euler(0, 0, 0));
                    InstantiateTrain();
                    break;
            }
        }

        private void InstantiateCar()
        {
            InstantiateCar(_terrainZ);
        }

        private void InstantiateCar(int carZ)
        {
            switch ((CarTypes)_random.Next(Enum.GetNames(typeof(CarTypes)).Length))
            {
                case CarTypes.Yellow:
                    Instantiate(yellowCar, new Vector3(transform.position.x - _random.Next(10, 40), 1.8f, carZ),
                        Quaternion.Euler(0, 0, 0));
                    break;
                case CarTypes.Green:
                    Instantiate(greenCar, new Vector3(transform.position.x - _random.Next(10, 40), 1.8f, carZ),
                        Quaternion.Euler(0, 0, 0));
                    break;
                case CarTypes.Blue:
                    Instantiate(blueCar, new Vector3(transform.position.x - _random.Next(10, 40), 1.8f, carZ),
                        Quaternion.Euler(0, 0, 0));
                    break;
            }
        }

        private void InstantiateLog()
        {
            InstantiateLog(_terrainZ);
        }

        private void InstantiateLog(int logZ)
        {
            switch ((LogTypes)_random.Next(Enum.GetNames(typeof(LogTypes)).Length))
            {
                case LogTypes.One:
                    Instantiate(log1, new Vector3(transform.position.x - _random.Next(10, 20), .6f, logZ),
                        Quaternion.Euler(0, 0, 0));
                    break;
                case LogTypes.Two:
                    Instantiate(log2, new Vector3(transform.position.x - _random.Next(10, 20), .6f, logZ),
                        Quaternion.Euler(0, 0, 0));
                    break;
                case LogTypes.Three:
                    Instantiate(log3, new Vector3(transform.position.x - _random.Next(10, 20), .6f, logZ),
                        Quaternion.Euler(0, 0, 0));
                    break;
            }
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
                    if (_terrainStreak > 3) return GetTerrainType();

                    break;
                case TerrainTypes.Railroad:
                    if (_terrainStreak > 3) return GetTerrainType();

                    break;
            }

            return _lastTerrainType;
        }
    }
}