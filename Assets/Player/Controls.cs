using UnityEngine;

namespace Player
{
    public class Controls : MonoBehaviour
    {
        public bool lastPress;
        public KeyCode lastKey;
        
        public void Start()
        {
            var playerTransform = transform;
        
            playerTransform.position = new Vector3(0, 1.5f, 0);
            playerTransform.rotation = Quaternion.Euler(0, 180, 0);

            lastPress = false;
        }

        public void Update()
        {
            var playerTransform = transform;
            var position = playerTransform.position;
            var rotation = playerTransform.rotation;

            if (!Input.GetKey(lastKey))
            {
                lastPress = false;
            }
            
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (!lastPress)
                {
                    rotation = Quaternion.Euler(0, 180, 0);
                    playerTransform.rotation = rotation;

                    position = new Vector3(position.x, position.y, position.z + 1);
                    playerTransform.position = position;
                }

                lastPress = true;
                lastKey = KeyCode.UpArrow;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (!lastPress)
                {
                    rotation = Quaternion.Euler(0, 0, 0);
                    playerTransform.rotation = rotation;

                    position = new Vector3(position.x, position.y, position.z - 1);
                    playerTransform.position = position;
                }

                lastPress = true;
                lastKey = KeyCode.DownArrow;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (!lastPress)
                {
                    rotation = Quaternion.Euler(0, 90, 0);
                    playerTransform.rotation = rotation;

                    position = new Vector3(position.x - 1, position.y, position.z);
                    playerTransform.position = position;
                }

                lastPress = true;
                lastKey = KeyCode.LeftArrow;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (!lastPress)
                {
                    rotation = Quaternion.Euler(0, 270, 0);
                    playerTransform.rotation = rotation;

                    position = new Vector3(position.x + 1, position.y, position.z);
                    playerTransform.position = position;
                }

                lastPress = true;
                lastKey = KeyCode.RightArrow;
            }
        }
    }
}
