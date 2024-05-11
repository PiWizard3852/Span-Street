using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class Controls : MonoBehaviour
    {
        public TextMeshProUGUI CurrentScore;
        private GameState _gameState;
        private bool _lastPress;
        private KeyCode _lastKey;
        
        public void Start()
        {
            _gameState = GameObject.FindGameObjectWithTag("GameState").gameObject.GetComponent<GameState>();
            _gameState.currentScore = CurrentScore;

            var playerTransform = transform;
        
            playerTransform.position = new Vector3(0, 1.5f, 0);
            playerTransform.rotation = Quaternion.Euler(0, 180, 0);

            _lastPress = false;
        }

        public void Update()
        {
            var playerTransform = transform;
            var position = playerTransform.position;
            
            _gameState._currentScore = (int) position.z;
            
            Quaternion rotation;

            if (!Input.GetKey(_lastKey))
            {
                _lastPress = false;
            }
            
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

            if (Input.GetKey(KeyCode.DownArrow))
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
        }

        public void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "Car")
            {
                SceneManager.LoadScene(0);
                _gameState._endMessage = "Game Lost. Final Score: " + _gameState._currentScore;
            }

            if(collision.gameObject.tag == "River")
            {
                SceneManager.LoadScene(0);
                _gameState._endMessage = "Game Lost. Final Score: " + _gameState._currentScore;
            }
        }
    }
}
