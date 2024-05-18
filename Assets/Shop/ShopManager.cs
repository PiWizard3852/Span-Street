using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Shop
{
    public class ShopManager : MonoBehaviour
    {
        public readonly int[,] ShopItems = new int[1, 5];
        private readonly Material[,] _skins = new Material[1, 5];
        
        public int coins;
        public TextMeshProUGUI coinsText;
        
        private GameState _gameState;
        
        public ItemInfo buttonInfo;
        public EnableInfo enableButtonInfo;
    
        public Material skin1;
        public Material skin2;
        public Material skin3;
        public Material skin4;
        public Material skin5;

        public void Start()
        {
            _gameState = GameObject.FindGameObjectWithTag("GameState").gameObject.GetComponent<GameState>();
            
            coins = 100;
            coinsText.text = "Coins: " + coins;

            var itemButtons = GameObject.FindGameObjectsWithTag("ItemButton");
            
            foreach (var buttonObject in itemButtons)
            {
                buttonObject.GetComponent<Button>().onClick.AddListener(() => Buy(buttonObject));
            }

            var enableButtons = GameObject.FindGameObjectsWithTag("EnableButton");
            
            foreach (var buttonObject in enableButtons)
            {
                buttonObject.GetComponent<Button>().onClick.AddListener(() => ChangeSkin(buttonObject));
            }

            ShopItems[0, 0] = 10;
            ShopItems[0, 1] = 20;
            ShopItems[0, 2] = 30;
            ShopItems[0, 3] = 40;
            ShopItems[0, 4] = 50;

            _skins[0, 0] = skin1;
            _skins[0, 1] = skin2;
            _skins[0, 2] = skin3;
            _skins[0, 3] = skin4;
            _skins[0, 4] = skin5;
        }

        private void Buy(GameObject clickedButton)
        {
            buttonInfo = clickedButton.GetComponent<ItemInfo>();

            if (coins >= ShopItems[0, buttonInfo.ItemID])
            {
                coins -= ShopItems[0, buttonInfo.ItemID];
                coinsText.text = "Coins: $" + coins.ToString();
                buttonInfo.isBought = true;
            }
        }

        private void ChangeSkin(GameObject button)
        {
            _gameState.currentSkin = _skins[0, button.GetComponent<EnableInfo>().EnableID];
        }

        public void ReturnHome()
        {
            SceneManager.LoadScene(0);
        }
    }
}
