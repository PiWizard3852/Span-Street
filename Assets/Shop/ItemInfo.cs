using TMPro;
using UnityEngine;

namespace Shop
{
    public class ItemInfo : MonoBehaviour
    {
        public int itemID;
    
        public TextMeshProUGUI priceText;
        public GameObject shopManager;
    
        public bool isBought = false;
        public bool isEquipped = false;
    }
}