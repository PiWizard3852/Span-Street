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
            var playerPosition = player.transform.position;

            transform.position = new Vector3(playerPosition.x, playerPosition.y + 10,
                playerPosition.z - 13);
            transform.rotation = Quaternion.Euler(30, 0, 0);
        }

        public void Update()
        {
            var cameraTransform = transform;
            var cameraPosition = cameraTransform.position;

            var playerPosition = player.transform.position;

            _targetZ = -Math.Max(playerPosition.z - 13, cameraPosition.z + .005f);
            Math.Abs(_targetZ - cameraPosition.z);

            _targetPower = playerPosition.z - 13 > cameraPosition.z + .005f ? -Math.Min(_targetZ / 50, -.01f) : .005f;

            transform.rotation = Quaternion.Euler(0, 0, 0);

            cameraPosition += cameraTransform.forward * _targetPower;
            cameraTransform.position = cameraPosition;

            transform.rotation = Quaternion.Euler(30, 0, 0);
        }
    }
}