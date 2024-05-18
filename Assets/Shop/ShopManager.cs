using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Shop
{
    public class ShopManager : MonoBehaviour
    {
        public readonly int[,] _shopItems = new int[1, 5];
        private readonly Material[,] _skins = new Material[1, 5];
        
        public int _coins;
        public TextMeshProUGUI _coinsText;
        
        public GameState _gameState;
    
        public Material _skin1;
        public Material _skin2;
        public Material _skin3;
        public Material _skin4;
        public Material _skin5;

        public void Start()
        {
            _gameState = GameObject.FindGameObjectWithTag("GameState").gameObject.GetComponent<GameState>();
            
            _coins = 100;
            // _coins = _gameState.totalScore;
            _coinsText.text = "Coins: $" + _coins;

            var _itemButtons = GameObject.FindGameObjectsWithTag("ItemButton");
            
            foreach (var _buttonObject in _itemButtons)
            {
                _buttonObject.GetComponent<Button>().onClick.AddListener(() => Buy(_buttonObject));
                UpdateButtonView(_buttonObject);
            }

            _shopItems[0, 0] = 10;
            _shopItems[0, 1] = 20;
            _shopItems[0, 2] = 30;
            _shopItems[0, 3] = 40;
            _shopItems[0, 4] = 50;

            _skins[0, 0] = _skin1;
            _skins[0, 1] = _skin2;
            _skins[0, 2] = _skin3;
            _skins[0, 3] = _skin4;
            _skins[0, 4] = _skin5;
        }

        public void Buy(GameObject clickedButton)
        {
            var _buttonInfo = clickedButton.GetComponent<ItemInfo>();

            if (_coins >= _shopItems[0, _buttonInfo.ItemID] && !_buttonInfo.isBought)
            {
                _coins -= _shopItems[0, _buttonInfo.ItemID];
                _coinsText.text = "Coins: $" + _coins.ToString();
                _buttonInfo.isBought = true;

                var _button = clickedButton.GetComponent<Button>();
                _button.onClick.RemoveAllListeners();
                _button.onClick.AddListener(() => ChangeSkin(_buttonInfo.ItemID));

            UpdateButtonView(clickedButton);
            }
        }

        public void ChangeSkin(int itemID)
        {
            _gameState.currentSkin = _skins[0, itemID];

            var _itemButtons = GameObject.FindGameObjectsWithTag("ItemButton");
            foreach (var _buttonObject in _itemButtons)
            {
                var _buttonInfo = _buttonObject.GetComponent<ItemInfo>();
                _buttonInfo.isEquipped = (_buttonInfo.ItemID == itemID);
               
                UpdateButtonView(_buttonObject);
            }
        }

        public void UpdateButtonView(GameObject buttonObject)
        {
            var _buttonInfo = buttonObject.GetComponent<ItemInfo>();
            var _button = buttonObject.GetComponent<Button>();
            var _buttonText = buttonObject.GetComponentInChildren<TextMeshProUGUI>();

            if (_buttonInfo.isEquipped)
            {
                _buttonText.text = "Equipped";
            }
            else if (_buttonInfo.isBought)
            {
                _buttonText.text = "Equip";
            }
            else if (_coins >= _shopItems[0, _buttonInfo.ItemID])
            {
                _buttonText.text = "Buy";
            }
            else
            {
                _buttonText.text = "Cannot Buy";
            }
        }

        public void ReturnHome()
        {
            SceneManager.LoadScene(0);
        }
    }
}