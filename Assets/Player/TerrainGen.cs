using System;
using UnityEngine;
using Random = System.Random;

namespace Player
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
        public Random random = new Random();

        public GameObject grass;
        public GameObject road;
        public GameObject river;
        public GameObject railroad;
        
        void Start()
        {
            for (int i = -30; i < 50; i++)
            {
                TerrainTypes terrainType = (TerrainTypes) random.Next(Enum.GetNames(typeof(TerrainTypes)).Length);

                switch (terrainType)
                {
                    case TerrainTypes.GRASS:
                        Instantiate(grass, new Vector3(0, 0, i), Quaternion.Euler(0, 0, 0));
                        break;
                    case TerrainTypes.ROAD:
                        Instantiate(road, new Vector3(0, 0, i), Quaternion.Euler(0, 0, 0));
                        break;
                    case TerrainTypes.RIVER:
                        Instantiate(river, new Vector3(0, 0, i), Quaternion.Euler(0, 0, 0));
                        break;
                    case TerrainTypes.RAILROAD:
                        Instantiate(railroad, new Vector3(0, 0, i), Quaternion.Euler(0, 0, 0));
                        break;
                }
            }
        }

        void Update()
        {
        
        }
    }
}
