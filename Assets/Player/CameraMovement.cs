using UnityEngine;

namespace Player
{
    public class CameraMovement : MonoBehaviour
    {
        public GameObject player;

        void Start()
        {
            transform.rotation = Quaternion.Euler(30, 0, 0);
            Update();
        }
        
        void Update()
        {
            var cameraTransform = transform;
            
            var cameraPosition = cameraTransform.position;
            var playerPosition = player.transform.position;
            
            cameraPosition = new Vector3(cameraPosition.x, playerPosition.y + 10, playerPosition.z - 12);
            
            cameraTransform.position = cameraPosition;
        }
    }
}
