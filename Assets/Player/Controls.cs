using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Player
{
    public class Controls : MonoBehaviour
    {
        public TextMeshProUGUI currentScoreText;
        
        private GameState _gameState;
            
        private bool _lastPress;
        private KeyCode _lastKey;

        public void Start()
        {
            _gameState = GameObject.FindGameObjectWithTag("GameState").gameObject.GetComponent<GameState>();
            
            _gameState.currentScoreText = currentScoreText;
            _gameState.currentScore = 0;
            
            _gameState.isOriginal = true;

            var playerTransform = transform;

            playerTransform.position = new Vector3(0, 1.5f, 0);
            playerTransform.rotation = Quaternion.Euler(0, 180, 0);

            _lastPress = false;
        }

        public void Update()
        {
            var playerTransform = transform;
            var position = playerTransform.position;

            if (transform.position.z > _gameState.currentScore)
            {
                _gameState.currentScore = (int) transform.position.z;
            }
            
            Quaternion rotation;

            if (!Input.GetKey(_lastKey)) _lastPress = false;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (!_lastPress)
                {
                    rotation = Quaternion.Euler(0, 180, 0);
                    playerTransform.rotation = rotation;

                    position = new Vector3(position.x, position.y, position.z + 1);
                    playerTransform.position = position;
                }

                _lastPress = true;
                _lastKey = KeyCode.UpArrow;
            }

            if (Input.GetKey(KeyCode.DownArrow) && _gameState.currentScore - transform.position.z < 3)
            {
                if (!_lastPress)
                {
                    rotation = Quaternion.Euler(0, 0, 0);
                    playerTransform.rotation = rotation;

                    position = new Vector3(position.x, position.y, position.z - 1);
                    playerTransform.position = position;
                }

                _lastPress = true;
                _lastKey = KeyCode.DownArrow;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (!_lastPress)
                {
                    rotation = Quaternion.Euler(0, 90, 0);
                    playerTransform.rotation = rotation;

                    position = new Vector3(position.x - 1, position.y, position.z);
                    playerTransform.position = position;
                }

                _lastPress = true;
                _lastKey = KeyCode.LeftArrow;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (!_lastPress)
                {
                    rotation = Quaternion.Euler(0, 270, 0);
                    playerTransform.rotation = rotation;

                    position = new Vector3(position.x + 1, position.y, position.z);
                    playerTransform.position = position;
                }

                _lastPress = true;
                _lastKey = KeyCode.RightArrow;
            }
            
            var cars = GameObject.FindGameObjectsWithTag("Car");

            foreach (var car in cars)
            {
                if (transform.position.z - car.transform.position.z > 15)
                {
                    Destroy(car);
                }
            }
            
            var logs = GameObject.FindGameObjectsWithTag("Log");

            foreach (var log in logs)
            {
                if (transform.position.z - log.transform.position.z > 15)
                {
                    Destroy(log);
                }
            }
            
            var trains = GameObject.FindGameObjectsWithTag("Train");

            foreach (var train in trains)
            {
                if (transform.position.z - train.transform.position.z > 15)
                {
                    Destroy(train);
                }
            }
            
            var grasses = GameObject.FindGameObjectsWithTag("Grass");

            foreach (var grass in grasses)
            {
                if (transform.position.z - grass.transform.position.z > 15)
                {
                    Destroy(grass);
                }
            }
            
            var roads = GameObject.FindGameObjectsWithTag("Road");

            foreach (var road in roads)
            {
                if (transform.position.z - road.transform.position.z > 15)
                {
                    Destroy(road);
                }
            }
            
            var rivers = GameObject.FindGameObjectsWithTag("River");

            foreach (var river in rivers)
            {
                if (transform.position.z - river.transform.position.z > 15)
                {
                    Destroy(river);
                }
            }
            
            var railroads = GameObject.FindGameObjectsWithTag("Railroad");

            foreach (var railroad in railroads)
            {
                if (transform.position.z - railroad.transform.position.z > 15)
                {
                    Destroy(railroad);
                }
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Car") || collision.gameObject.CompareTag("River"))
            {
                _gameState.totalScore += _gameState.currentScore;
                
                SceneManager.LoadScene(0);
            }
        }
    }
}