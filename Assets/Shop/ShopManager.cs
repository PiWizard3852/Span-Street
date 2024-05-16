using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int[,] shopItems = new int[5, 5];
    public int coins;
    public TextMeshProUGUI CoinsText;
    private GameState _gameState;
    private ButtonInfo _buttonInfo;
    public GameObject _player;

    public Material skin1;
    public Material skin2;
    public Material skin3;

    public void Start()
    {
        coins = 100;
        CoinsText.text = "Coins: " + coins;

        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => Buy(button.gameObject));
        }

        shopItems[0, 0] = 1;
        shopItems[0, 1] = 2;
        shopItems[0, 2] = 3;
        shopItems[0, 3] = 4;
        shopItems[0, 4] = 5;

        shopItems[1, 0] = 10;
        shopItems[1, 1] = 20;
        shopItems[1, 2] = 30;
        shopItems[1, 3] = 40;
        shopItems[1, 4] = 50;

    }

    public void Buy(GameObject clickedButton)
    {
        _buttonInfo = clickedButton.GetComponent<ButtonInfo>();

        int buttonID = _buttonInfo.ItemID;
        if (coins >= shopItems[1, buttonID])
        {
            coins -= shopItems[1, buttonID];
            CoinsText.text = "Coins: $" + coins.ToString();
            _buttonInfo.isBought = true;
        }
    }
}
