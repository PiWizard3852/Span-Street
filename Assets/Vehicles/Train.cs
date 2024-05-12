using UnityEngine;
using Random = System.Random;

namespace Vehicles
{
    public class Train : MonoBehaviour
    {
        private readonly Random _random = new();

        private float _speed;

        public void Start()
        {
            _speed = .075f * _random.Next(1, 2);
        }

        public void Update()
        {
            var trainTransform = transform;
            var trainPosition = trainTransform.position;
            trainPosition += trainTransform.right * _speed;
            trainTransform.position = trainPosition;
        }
    }
}