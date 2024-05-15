using UnityEngine;
using Random = System.Random;

namespace Vehicles
{
    public class Vehicle : MonoBehaviour
    {
        protected readonly Random Random = new();
        protected GameState GameState;
        
        protected float Speed;
        
        public bool invisible;
        public bool passed;

        protected void Init()
        {
            GameState = GameObject.FindGameObjectWithTag("GameState").gameObject.GetComponent<GameState>();
            
            invisible = false;
            passed = false;
        }
        
        public void Update()
        {
            var vehicleTransform = transform;
            var vehiclePosition = vehicleTransform.position;
            vehiclePosition += vehicleTransform.right * Speed;
            vehicleTransform.position = vehiclePosition;
        }
        
        public void OnBecameVisible()
        {
            passed = true;
        }
        
        public void OnBecameInvisible()
        {
            if (passed)
            {
                invisible = true;
            }
        }
    }
}