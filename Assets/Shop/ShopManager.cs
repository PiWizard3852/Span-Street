using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Shop
{
    public class ShopManager : MonoBehaviour
    {
        public GameState _gameState;
        
        public GameObject[] _buttons;
        
        public TextMeshProUGUI _coinsText;

        public void Start()
        {
            _gameState = GameObject.FindGameObjectWithTag("GameState").gameObject.GetComponent<GameState>();

            _coinsText.text = "Coins: $" + _gameState.totalScore;

            _buttons = GameObject.FindGameObjectsWithTag("ItemButton");

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
            var _price = _gameState.SkinPrices[_buttonInfo.ItemID];

            if (_gameState.totalScore >= _price && !_buttonInfo.isBought)
            {
                _gameState.totalScore -= _price;
                _coinsText.text = "Coins: $" + _gameState.totalScore.ToString();
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
            _gameState.currentSkin = _gameState.Skins[itemID];
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
            var _price = _gameState.SkinPrices[_buttonInfo.ItemID];

            if (_buttonInfo.isEquipped)
            {
                _buttonText.text = "Equipped";
            }
            else if (_buttonInfo.isBought)
            {
                _buttonText.text = "Equip";
            }
            else
            {
                _buttonText.text = "Buy for $" + _price;
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
                _gameState.currentSkin = _gameState.Skins[currentSkinID];
                
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