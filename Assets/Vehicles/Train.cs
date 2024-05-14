using System;
using UnityEngine;
using Random = System.Random;

namespace Vehicles
{
    public class Train : MonoBehaviour
    {
        private readonly Random _random = new();
        private GameState _gameState;

        private float _speed;

        public void Start()
        {
            _gameState = GameObject.FindGameObjectWithTag("GameState").gameObject.GetComponent<GameState>();
            _speed = .07f * _random.Next(1, 2) + .03f * Math.Min(_gameState.totalScore + _gameState.currentScore, 300) / 300;
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