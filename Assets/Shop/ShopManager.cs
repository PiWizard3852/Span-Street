using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int[,] shopItems = new int[1, 5];
    public Material[,] skins = new Material[1, 5];
    public int coins;
    public TextMeshProUGUI CoinsText;
    public GameState gameState;
    public ItemInfo buttonInfo;
    public EnableInfo enableButtonInfo;
    public Material skin1;
    public Material skin2;
    public Material skin3;
    public Material skin4;
    public Material skin5;

    public void Start()
    {
        coins = 100;
        CoinsText.text = "Coins: " + coins;

        GameObject[] itemButtons = GameObject.FindGameObjectsWithTag("ItemButton");
        foreach (GameObject buttonObject in itemButtons)
        {
            Button button = buttonObject.GetComponent<Button>();
            button.onClick.AddListener(() => Buy(buttonObject));
        }

        GameObject[] enableButtons = GameObject.FindGameObjectsWithTag("EnableButton");
        foreach (GameObject buttonObject in enableButtons)
        {
            Button button = buttonObject.GetComponent<Button>();
            button.onClick.AddListener(() => ChangeSkin(buttonObject));
        }

        shopItems[0, 0] = 10;
        shopItems[0, 1] = 20;
        shopItems[0, 2] = 30;
        shopItems[0, 3] = 40;
        shopItems[0, 4] = 50;

        skins[0, 0] = skin1;
        skins[0, 1] = skin2;
        skins[0, 2] = skin3;
        skins[0, 3] = skin4;
        skins[0, 4] = skin5;

    }

    public void Buy(GameObject clickedButton)
    {
        buttonInfo = clickedButton.GetComponent<ItemInfo>();
        int buttonID = buttonInfo.ItemID;

        if (coins >= shopItems[0, buttonID])
        {
            coins -= shopItems[0, buttonID];
            CoinsText.text = "Coins: $" + coins.ToString();
            buttonInfo.isBought = true;
        }
    }

    public void ChangeSkin(GameObject clickedButton)
    {
        enableButtonInfo = clickedButton.GetComponent<EnableInfo>();
        int buttonID = enableButtonInfo.EnableID;

        Material newSkin = skins[0, buttonID];
       // player.GetComponent<MeshRenderer>().material = newSkin;
    }

    public void ReturnHome()
    {
        SceneManager.LoadScene(0);
    }
}
