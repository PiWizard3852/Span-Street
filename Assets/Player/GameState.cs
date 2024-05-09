using TMPro;
using UnityEngine;

namespace Player
{
    public class GameState : MonoBehaviour
    {
        public TextMeshProUGUI TextPro;
    
        private int _score;
    
        public void Start()
        {
            _score = 0;
        }

        public void Update()
        {
            _score = (int) transform.position.z;
            TextPro.text = "" + _score;
        }
    }
}
