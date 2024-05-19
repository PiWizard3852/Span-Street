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
        public GameObject[] _buttons;
        
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
            _coinsText.text = "Coins: $" + _coins;

            _buttons = GameObject.FindGameObjectsWithTag("ItemButton");

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

            foreach (var _button in _buttons)
            {
                _button.GetComponent<Button>().onClick.AddListener(() => Buy(_button));
            }

            LoadItems();
            LoadSkin();
            UpdateEachButton();
        }

        public void Buy(GameObject clickedButton)
        {
            var _buttonInfo = clickedButton.GetComponent<ItemInfo>();
            var _price = _shopItems[0, _buttonInfo.ItemID];

            if (_coins >= _price && !_buttonInfo.isBought)
            {
                _coins -= _price;
                _coinsText.text = "Coins: $" + _coins.ToString();
                _buttonInfo.isBought = true;

                PlayerPrefs.SetInt("Bought" + _buttonInfo.ItemID, 1);

                var _button = clickedButton.GetComponent<Button>();
                _button.onClick.RemoveAllListeners();
                _button.onClick.AddListener(() => ChangeSkin(_buttonInfo.ItemID));
            }

            UpdateEachButton();
        }

        public void ChangeSkin(int itemID)
        {
            _gameState.currentSkin = _skins[0, itemID];
            PlayerPrefs.SetInt("CurrentSkin", itemID);

            foreach (var _button in _buttons)
            {
                var _buttonInfo = _button.GetComponent<ItemInfo>();
                _buttonInfo.isEquipped = (_buttonInfo.ItemID == itemID);
            }

            UpdateEachButton();
        }

        public void UpdateEachButton()
        {
            foreach (var _button in _buttons)
            {  
                UpdateButtonView(_button);
            }
        }

        public void UpdateButtonView(GameObject buttonObject)
        {
            var _buttonInfo = buttonObject.GetComponent<ItemInfo>();
            var _buttonText = buttonObject.GetComponentInChildren<TextMeshProUGUI>();
            var _price = _shopItems[0, _buttonInfo.ItemID];

            if (_buttonInfo.isEquipped)
            {
                _buttonText.text = "Equipped";
            }
            else if (_buttonInfo.isBought)
            {
                _buttonText.text = "Equip";
            }
            else if (_coins >= _price)
            {
                _buttonText.text = "Buy for $" + _price;
            }
            else
            {
                _buttonText.text = "Cannot Buy\n$" + _price + " needed to buy";
            }
        }

        public void LoadItems()
        {
            foreach (var _button in _buttons)
            {
                var _buttonInfo = _button.GetComponent<ItemInfo>();
                _buttonInfo.isBought = (PlayerPrefs.GetInt("Bought" + _buttonInfo.ItemID, -1) == 1);

                if (_buttonInfo.isBought)
                {
                    _button.GetComponent<Button>().onClick.RemoveAllListeners();
                    _button.GetComponent<Button>().onClick.AddListener(() => ChangeSkin(_buttonInfo.ItemID));
                }
            }
        }

        public void LoadSkin()
        {
            int currentSkinID = PlayerPrefs.GetInt("CurrentSkin", -1);
            
            if (currentSkinID != -1)
            {
                _gameState.currentSkin = _skins[0, currentSkinID];
                
                foreach (var _button in _buttons)
                {
                    var _buttonInfo = _button.GetComponent<ItemInfo>();
                    _buttonInfo.isEquipped = (_buttonInfo.ItemID == currentSkinID);
                }
            }
        }

        public void Home()
        {
            SceneManager.LoadScene(0);
        }
    }
}