using System;
using UnityEngine;
using Random = System.Random;

namespace Terrain
{
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

        public GameObject player;
        
        public GameObject grass;
        public GameObject road;
        public GameObject river;
        public GameObject railroad;
        
        void Start()
        {
            _terrainStreak = 1;
            _lastTerrainType = TerrainTypes.GRASS;
            
            for (_terrainZ = -30; _terrainZ < 50; _terrainZ++)
            {
                InstantiateLand();
            }
        }

        void Update()
        {
            if (_terrainZ - player.transform.position.z < 40)
            {
                _terrainZ++;
                InstantiateLand();
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
                    break;
                case TerrainTypes.RIVER:
                    Instantiate(river, new Vector3(0, 0, _terrainZ), Quaternion.Euler(0, 0, 0));
                    break;
                case TerrainTypes.RAILROAD:
                    Instantiate(railroad, new Vector3(0, 0, _terrainZ), Quaternion.Euler(0, 0, 0));
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
                    if (_terrainStreak > 10)
                    {
                        return GetTerrainType();
                    }
                    
                    break;
                case TerrainTypes.RIVER:
                    if (_terrainStreak > 4)
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
