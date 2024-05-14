using System;
using UnityEngine;
using Random = System.Random;

namespace Vehicles
{
    public class Log : MonoBehaviour
    {
        private readonly Random _random = new();
        private GameState _gameState;
        
        private float _speed;

        public void Start()
        {
            _gameState = GameObject.FindGameObjectWithTag("GameState").gameObject.GetComponent<GameState>();
            _speed = .025f * _random.Next(1, 2) + .02f * Math.Min(_gameState.totalScore + _gameState.currentScore, 300) / 300;
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