using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class Controls : MonoBehaviour
    {
        public TextMeshProUGUI currentScoreText;

        private GameState _gameState;

        private int _maxZ;

        private bool _onLog;
        private int _lastLog;
        private float _logOffset;

        private bool _lost;

        public void Start()
        {
            // Access and set game state and local variable
            _gameState = GameObject.FindGameObjectWithTag("GameState").gameObject.GetComponent<GameState>();

            _gameState.currentScoreText = currentScoreText;
            _gameState.currentScore = 0;

            _gameState.isOriginal = true;

            GetComponent<MeshRenderer>().material = _gameState.currentSkin;

            var playerTransform = transform;

            playerTransform.position = new Vector3(0, 1.5f, 0);
            playerTransform.rotation = Quaternion.Euler(0, 180, 0);

            _onLog = false;
            _lost = false;
        }

        public void Update()
        {
            var playerTransform = transform;
            var position = playerTransform.position;

            _maxZ = Math.Max((int)position.z, _maxZ);

            Quaternion rotation;

            // Handle user key input
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rotation = Quaternion.Euler(0, 180, 0);
                playerTransform.rotation = rotation;

                position = new Vector3(position.x, position.y, position.z + 1);
                playerTransform.position = position;

                // Use raycasting to detect a collision with the river
                if (Physics.Raycast(transform.position, -Vector3.up, out var hit, 100.0f) &&
                    hit.transform.CompareTag("River") && !_onLog) Lose();

                // Delete terrain far behind the player

                var grasses = GameObject.FindGameObjectsWithTag("Grass");

                foreach (var grass in grasses)
                    if (transform.position.z - grass.transform.position.z > 30)
                        Destroy(grass);

                var roads = GameObject.FindGameObjectsWithTag("Road");

                foreach (var road in roads)
                    if (transform.position.z - road.transform.position.z > 30)
                        Destroy(road);

                var rivers = GameObject.FindGameObjectsWithTag("River");

                foreach (var river in rivers)
                    if (transform.position.z - river.transform.position.z > 30)
                        Destroy(river);

                var railroads = GameObject.FindGameObjectsWithTag("Railroad");

                foreach (var railroad in railroads)
                    if (transform.position.z - railroad.transform.position.z > 30)
                        Destroy(railroad);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && _maxZ - transform.position.z < 3)
            {
                rotation = Quaternion.Euler(0, 0, 0);
                playerTransform.rotation = rotation;

                position = new Vector3(position.x, position.y, position.z - 1);
                playerTransform.position = position;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                rotation = Quaternion.Euler(0, 90, 0);
                playerTransform.rotation = rotation;

                _logOffset--;

                position = new Vector3(position.x - 1, position.y, position.z);
                playerTransform.position = position;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                rotation = Quaternion.Euler(0, 270, 0);
                playerTransform.rotation = rotation;

                _logOffset++;

                position = new Vector3(position.x + 1, position.y, position.z);
                playerTransform.position = position;
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            // Handle lose case collisions
            if (collision.gameObject.CompareTag("Car") || collision.gameObject.CompareTag("River") ||
                collision.gameObject.CompareTag("Train")) Lose();

            if (collision.gameObject.CompareTag("Log")) _onLog = true;

            // Collect coins
            if (collision.gameObject.CompareTag("Coin"))
            {
                _gameState.currentScore++;

                Destroy(collision.gameObject);
            }
        }

        public void OnBecameInvisible()
        {
            // Lose if the player goes out of screen
            Lose();
        }

        public void OnCollisionStay(Collision collision)
        {
            // Handle continous collisions while on logs
            if (collision.gameObject.CompareTag("Log"))
            {
                if (!_lastLog.Equals(collision.gameObject.GetHashCode()))
                {
                    _lastLog = collision.gameObject.GetHashCode();
                    _logOffset = transform.position.x - collision.transform.position.x;
                }
                else
                {
                    var playerTransform = transform;
                    var playerPosition = playerTransform.position;
                    playerPosition = new Vector3(collision.transform.position.x + _logOffset, playerPosition.y,
                        playerPosition.z);
                    playerTransform.position = playerPosition;
                }
            }
            else
            {
                _lastLog = -1;
            }
        }

        private void Lose()
        {
            // Render correct scene after losing
            if (!_lost)
            {
                _gameState.totalScore += _gameState.currentScore;

                SceneManager.LoadScene(0);
            }

            _lost = true;
        }
    }
}