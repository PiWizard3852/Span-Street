using System;
using UnityEngine;

namespace Player
{
    public class CameraMovement : MonoBehaviour
    {
        public GameObject player;

        private float _targetZ;
        private float _targetPower;

        public void Start()
        {
            // Set initial camera position behind the player
            var playerPosition = player.transform.position;

            transform.position = new Vector3(playerPosition.x, playerPosition.y + 10,
                playerPosition.z - 13);
            transform.rotation = Quaternion.Euler(30, 0, 0);
        }

        public void Update()
        {
            // Move the camera forward each frame at a slow speed
            var cameraTransform = transform;
            var cameraPosition = cameraTransform.position;

            var playerPosition = player.transform.position;

            _targetZ = -Math.Max(playerPosition.z - 13, cameraPosition.z + .01f);
            _targetPower = playerPosition.z - 13 > cameraPosition.z + .01f ? -Math.Min(_targetZ / 50, -.01f) : .01f;

            SetRotation(Quaternion.Euler(0, 0, 0));

            cameraPosition += cameraTransform.forward * _targetPower;
            cameraTransform.position = cameraPosition;

            SetRotation(Quaternion.Euler(30, 0, 0));
        }

        private void SetRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
        }
    }
}