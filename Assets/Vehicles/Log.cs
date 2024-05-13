using UnityEngine;
using Random = System.Random;

namespace Vehicles
{
    public class Log : MonoBehaviour
    {
        private readonly Random _random = new();

        private float _speed;

        public void Start()
        {
            _speed = .02f * _random.Next(1, 2);
        }

        public void Update()
        {
            var logTransform = transform;
            var logPosition = logTransform.position;
            logPosition += logTransform.right * _speed;
            logTransform.position = logPosition;
        }
    }
}