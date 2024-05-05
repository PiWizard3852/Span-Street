using UnityEngine;

namespace Player
{
    public class CameraMovement : MonoBehaviour
    {
public bool lastPress;
        public KeyCode lastKey;
        private GameObject _player;

        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            var cameraTransform = transform;
        
            cameraTransform.position = new Vector3(0, 1.5f, 0);
            cameraTransform.rotation = Quaternion.Euler(0, 180, 0);

            lastPress = false;
        }

        void Update()
        {
            var playerTransform = transform;
            var position = playerTransform.position;
        }
    }
}
