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

            // Add event listeners to each button in the shop
            foreach (var button in buttons) button.GetComponent<Button>().onClick.AddListener(() => Buy(button));

            // Load previous shop data from player prefs
            LoadItems();
            LoadSkin();
            UpdateButtons();
        }

        private void Buy(GameObject clickedButton)
        {
            var buttonInfo = clickedButton.GetComponent<ItemInfo>();
            var price = gameState.SkinPrices[buttonInfo.itemID];

            if (gameState.totalScore >= price && !buttonInfo.isBought)
            {
                // Update coins
                gameState.totalScore -= price;
                coinsText.text = "Coins: $" + gameState.totalScore.ToString();
                buttonInfo.isBought = true;

                // Save bought skin in player prefs
                PlayerPrefs.SetInt("Bought" + buttonInfo.itemID, 1);

                // Update the button's event listener to call the ChangeSkin method
                var button = clickedButton.GetComponent<Button>();
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => ChangeSkin(buttonInfo.itemID));
            }

            //Update UI
            UpdateButtons();
        }

        private void ChangeSkin(int itemID)
        {
            // Update the player's skin and save it in player prefs
            gameState.currentSkin = gameState.Skins[itemID];
            PlayerPrefs.SetInt("CurrentSkin", itemID);

            foreach (var button in buttons)
            {
                var buttonInfo = button.GetComponent<ItemInfo>();
                buttonInfo.isEquipped = buttonInfo.itemID == itemID;
            }

            // Update UI
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            foreach (var button in buttons) UpdateButton(button);
        }

        private void UpdateButton(GameObject buttonObject)
        {
            var buttonInfo = buttonObject.GetComponent<ItemInfo>();
            var buttonText = buttonObject.GetComponentInChildren<TextMeshProUGUI>();
            var price = gameState.SkinPrices[buttonInfo.itemID];

            // Update the text on the button according to its current state
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
                // Check if the skin was previously purchased
                var buttonInfo = button.GetComponent<ItemInfo>();
                buttonInfo.isBought = PlayerPrefs.GetInt("Bought" + buttonInfo.itemID, -1) == 1;

                // Update event listeners
                if (buttonInfo.isBought)
                {
                    button.GetComponent<Button>().onClick.RemoveAllListeners();
                    button.GetComponent<Button>().onClick.AddListener(() => ChangeSkin(buttonInfo.itemID));
                }
            }
        }

        private void LoadSkin()
        {
            // Find the last skin equipped in previous runs of the game using player prefs
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
            // Render the menu scene
            SceneManager.LoadScene(0);
        }
    }
}