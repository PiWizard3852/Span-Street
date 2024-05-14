using System;
using UnityEngine;
using Random = System.Random;

namespace Vehicles
{
    public class Car : MonoBehaviour
    {
        private readonly Random _random = new();
        private GameState _gameState;
        
        private float _speed;

        public void Start()
        {
            _gameState = GameObject.FindGameObjectWithTag("GameState").gameObject.GetComponent<GameState>();
            _speed = .02f * _random.Next(1, 2) + .02f * Math.Min(_gameState.totalScore + _gameState.currentScore, 300) / 300;
        }

        public void Update()
        {
            var carTransform = transform;
            var carPosition = carTransform.position;
            carPosition += carTransform.right * _speed;
            carTransform.position = carPosition;
        }
    }
}