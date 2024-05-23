using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Shop
{
    public class ShopManager : MonoBehaviour
    {
        public GameState gameState;

        public GameObject[] buttons;
        
        public TextMeshProUGUI coinsText;

        public void Start()
        {
            gameState = GameObject.FindGameObjectWithTag("GameState").gameObject.GetComponent<GameState>();

            coinsText.text = "Coins: $" + gameState.totalScore;

            buttons = GameObject.FindGameObjectsWithTag("ItemButton");

            foreach (var button in buttons) button.GetComponent<Button>().onClick.AddListener(() => Buy(button));

            LoadItems();
            LoadSkin();
            UpdateEachButton();
        }

        private void Buy(GameObject clickedButton)
        {
            var buttonInfo = clickedButton.GetComponent<ItemInfo>();
            var price = gameState.SkinPrices[buttonInfo.itemID];

            if (gameState.totalScore >= price && !buttonInfo.isBought)
            {
                gameState.totalScore -= price;
                coinsText.text = "Coins: $" + gameState.totalScore.ToString();
                buttonInfo.isBought = true;

                PlayerPrefs.SetInt("Bought" + buttonInfo.itemID, 1);

                var button = clickedButton.GetComponent<Button>();
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => ChangeSkin(buttonInfo.itemID));
            }

            UpdateEachButton();
        }

        private void ChangeSkin(int itemID)
        {
            gameState.currentSkin = gameState.Skins[itemID];
            PlayerPrefs.SetInt("CurrentSkin", itemID);

            foreach (var button in buttons)
            {
                var buttonInfo = button.GetComponent<ItemInfo>();
                buttonInfo.isEquipped = buttonInfo.itemID == itemID;
            }

            UpdateEachButton();
        }

        private void UpdateEachButton()
        {
            foreach (var button in buttons) UpdateButtonView(button);
        }

        private void UpdateButtonView(GameObject buttonObject)
        {
            var buttonInfo = buttonObject.GetComponent<ItemInfo>();
            var buttonText = buttonObject.GetComponentInChildren<TextMeshProUGUI>();
            var price = gameState.SkinPrices[buttonInfo.itemID];

            if (buttonInfo.isEquipped)
                buttonText.text = "Equipped";
            else if (buttonInfo.isBought)
                buttonText.text = "Equip";
            else if (buttonInfo.itemID != 0) buttonText.text = "$" + price;
        }

        private void LoadItems()
        {
            foreach (var button in buttons)
            {
                var buttonInfo = button.GetComponent<ItemInfo>();
                buttonInfo.isBought = PlayerPrefs.GetInt("Bought" + buttonInfo.itemID, -1) == 1;

                if (buttonInfo.isBought)
                {
                    button.GetComponent<Button>().onClick.RemoveAllListeners();
                    button.GetComponent<Button>().onClick.AddListener(() => ChangeSkin(buttonInfo.itemID));
                }
            }
        }

        private void LoadSkin()
        {
            var currentSkinID = PlayerPrefs.GetInt("CurrentSkin", -1);

            if (currentSkinID != -1)
            {
                gameState.currentSkin = gameState.Skins[currentSkinID];

                foreach (var button in buttons)
                {
                    var buttonInfo = button.GetComponent<ItemInfo>();
                    buttonInfo.isEquipped = buttonInfo.itemID == currentSkinID;
                }
            }
        }

        public void Home()
        {
            SceneManager.LoadScene(0);
        }
    }
}